GlobalActionMap.bindCmd(keyboard, "F5", "", "openRules();");
function optionsDlg::setPane(%this, %pane)
{
   OptAudioPane.setVisible(false);
   OptGraphicsPane.setVisible(false);
   OptNetworkPane.setVisible(false);
   OptControlsPane.setVisible(false);
   ("Opt" @ %pane @ "Pane").setVisible(true);
   OptRemapList.fillList();
   //echo("OPTIONS MENU");
}

function OptionsDlg::onWake(%this)
{
    //echo("OPTIONS MENU Works");
   %this.setPane(Graphics);
   %buffer = getDisplayDeviceList();
   %count = getFieldCount( %buffer );
   OptGraphicsDriverMenu.clear();
   OptScreenshotMenu.init();
   OptScreenshotMenu.setValue($pref::Video::screenShotFormat);
   for(%i = 0; %i < %count; %i++)
      OptGraphicsDriverMenu.add(getField(%buffer, %i), %i);
   %selId = OptGraphicsDriverMenu.findText( $pref::Video::displayDevice );
	if ( %selId == -1 )
		%selId = 0; // How did THAT happen?
	OptGraphicsDriverMenu.setSelected( %selId );
	OptGraphicsDriverMenu.onSelect( %selId, "" );

   // Audio 
   OptAudioUpdate();
   OptAudioVolumeMaster.setValue( $pref::Audio::masterVolume);
   OptAudioVolumeShell.setValue(  $pref::Audio::channelVolume[$GuiAudioType]);
   OptAudioVolumeSim.setValue(    $pref::Audio::channelVolume[$SimAudioType]);
   OptAudioDriverList.clear();
   OptAudioDriverList.add("OpenAL", 1);
   OptAudioDriverList.add("none", 2);
   %selId = OptAudioDriverList.findText($pref::Audio::driver);
	if ( %selId == -1 )
		%selId = 0; // How did THAT happen?
	OptAudioDriverList.setSelected( %selId );
	OptAudioDriverList.onSelect( %selId, "" );

   	//controls
	SliderControlsMouseSensitivity.setValue($pref::Input::MouseSensitivity);

	//graphics
	SliderGraphicsAnisotropy.setValue($pref::OpenGL::Anisotropy);
	SliderGraphicsDistanceMod.setValue($pref::visibleDistanceMod);

	//network
	PacketSizeDisplay.setValue($pref::Net::PacketSize);
	SliderPacketSize.setValue($pref::Net::PacketSize);

	LagThresholdDisplay.setValue($pref::Net::LagThreshold);
	SliderLagThreshold.setValue($pref::Net::LagThreshold);

	RateToClientDisplay.setValue($pref::Net::PacketRateToClient);
	SliderRateToClient.setValue($pref::Net::PacketRateToClient);

	RateToServerDisplay.setValue($pref::Net::PacketRateToServer);
	SliderRateToServer.setValue($pref::Net::PacketRateToServer);

	TxtMasterServer.setValue($pref::Master0);
}

function OptionsDlg::onSleep(%this)
{
   // write out the control config into the fps/config.cs file
   moveMap.save( "~/client/config.cs" );

	//save player prefs
	$pref::Input::MouseSensitivity = SliderControlsMouseSensitivity.getValue();
	$pref::OpenGL::Anisotropy = SliderGraphicsAnisotropy.getValue();
	$pref::visibleDistanceMod = SliderGraphicsDistanceMod.getValue();
	$pref::Video::screenShotFormat = OptScreenshotMenu.getValue();
	
	$pref::Master0 = TxtMasterServer.getValue();

	//send our prefs to the server
	clientCmdUpdatePrefs();
}

function UpdatePacketSize()
{
	PacketSizeDisplay.setValue(mFloor(SliderPacketSize.getValue()));
	$pref::Net::PacketSize = PacketSizeDisplay.getValue();
}
function UpdateLagThreshold()
{
	LagThresholdDisplay.setValue(mFloor(SliderLagThreshold.getValue()));
	$pref::Net::LagThreshold = LagThresholdDisplay.getValue();
}

function UpdateRateToClient()
{
	RateToClientDisplay.setValue(mFloor(SliderRateToClient.getValue()));
	$pref::Net::PacketRateToClient = RateToClientDisplay.getValue();
}

function UpdateRateToServer()
{
	RateToServerDisplay.setValue(mFloor(SliderRateToServer.getValue()));
	$pref::Net::PacketRateToServer = RateToServerDisplay.getValue();
}

function OptGraphicsDriverMenu::onSelect( %this, %id, %text )
{
	// Attempt to keep the same res and bpp settings:
	if ( OptGraphicsResolutionMenu.size() > 0 )
		%prevRes = OptGraphicsResolutionMenu.getText();
	else
		%prevRes = getWords( $pref::Video::resolution, 0, 1 );

	// Check if this device is full-screen only:
	if ( isDeviceFullScreenOnly( %this.getText() ) )
	{
		OptGraphicsFullscreenToggle.setValue( true );
		OptGraphicsFullscreenToggle.setActive( false );
		OptGraphicsFullscreenToggle.onAction();
	}
	else
		OptGraphicsFullscreenToggle.setActive( true );

	if ( OptGraphicsFullscreenToggle.getValue() )
	{
		if ( OptGraphicsBPPMenu.size() > 0 )
			%prevBPP = OptGraphicsBPPMenu.getText();
		else
			%prevBPP = getWord( $pref::Video::resolution, 2 );
	}

	// Fill the resolution and bit depth lists:
	OptGraphicsResolutionMenu.init( %this.getText(), OptGraphicsFullscreenToggle.getValue() );
	OptGraphicsBPPMenu.init( %this.getText() );

	// Try to select the previous settings:
	%selId = OptGraphicsResolutionMenu.findText( %prevRes );
	if ( %selId == -1 )
		%selId = 0;
	OptGraphicsResolutionMenu.setSelected( %selId );

	if ( OptGraphicsFullscreenToggle.getValue() )
	{
		%selId = OptGraphicsBPPMenu.findText( %prevBPP );
		if ( %selId == -1 )
			%selId = 0;
		OptGraphicsBPPMenu.setSelected( %selId );
		OptGraphicsBPPMenu.setText( OptGraphicsBPPMenu.getTextById( %selId ) );
	}
	else
		OptGraphicsBPPMenu.setText( "Default" );

}

function OptGraphicsResolutionMenu::init( %this, %device, %fullScreen )
{
	%this.clear();
	%resList = getResolutionList( %device );
	%resCount = getFieldCount( %resList );
	%deskRes = getDesktopResolution();

   %count = 0;
	for ( %i = 0; %i < %resCount; %i++ )
	{
		%res = getWords( getField( %resList, %i ), 0, 1 );

		if ( !%fullScreen )
		{
			if ( firstWord( %res ) >= firstWord( %deskRes ) )
				continue;
			if ( getWord( %res, 1 ) >= getWord( %deskRes, 1 ) )
				continue;
		}

		// Only add to list if it isn't there already:
		if ( %this.findText( %res ) == -1 )
      {
			%this.add( %res, %count );
         %count++;
      }
	}
}

function OptGraphicsFullscreenToggle::onAction(%this)
{
   Parent::onAction();
   %prevRes = OptGraphicsResolutionMenu.getText();

   // Update the resolution menu with the new options
   OptGraphicsResolutionMenu.init( OptGraphicsDriverMenu.getText(), %this.getValue() );

   // Set it back to the previous resolution if the new mode supports it.
   %selId = OptGraphicsResolutionMenu.findText( %prevRes );
   if ( %selId == -1 )
   	%selId = 0;
 	OptGraphicsResolutionMenu.setSelected( %selId );
}


function OptGraphicsBPPMenu::init( %this, %device )
{
	%this.clear();

	if ( %device $= "Voodoo2" )
		%this.add( "16", 0 );
	else
	{
		%resList = getResolutionList( %device );
		%resCount = getFieldCount( %resList );
      %count = 0;
		for ( %i = 0; %i < %resCount; %i++ )
		{
			%bpp = getWord( getField( %resList, %i ), 2 );

			// Only add to list if it isn't there already:
			if ( %this.findText( %bpp ) == -1 )
         {
				%this.add( %bpp, %count );
            %count++;
         }
		}
	}
}

function OptScreenshotMenu::init( %this )
{
   if( %this.findText("PNG") == -1 )
      %this.add("PNG", 0);
   if( %this.findText("JPEG") == - 1 )
      %this.add("JPEG", 1);
}

function optionsDlg::applyGraphics( %this )
{
	%newDriver = OptGraphicsDriverMenu.getText();
	%newRes = OptGraphicsResolutionMenu.getText();
	%newBpp = OptGraphicsBPPMenu.getText();
	%newFullScreen = OptGraphicsFullscreenToggle.getValue();
	$pref::Video::screenShotFormat = OptScreenshotMenu.getText();

	if ( %newDriver !$= $pref::Video::displayDevice )
	{
		setDisplayDevice( %newDriver, firstWord( %newRes ), getWord( %newRes, 1 ), %newBpp, %newFullScreen );
		//OptionsDlg::deviceDependent( %this );
	}
	else
		setScreenMode( firstWord( %newRes ), getWord( %newRes, 1 ), %newBpp, %newFullScreen );
}

function optionsDlg::applyPlayer( %this )
{
	echo("applying player attributes");
	$pref::Player::Name = TxtPlayerName.getValue();
}


$RemapCount = 0;
$RemapName[$RemapCount] = "Fire Weapon";
$RemapCmd[$RemapCount] = "mouseFire";
$RemapCount++;
$RemapName[$RemapCount] = "Forward";
$RemapCmd[$RemapCount] = "moveforward";
$RemapCount++;
$RemapName[$RemapCount] = "Backward";
$RemapCmd[$RemapCount] = "movebackward";
$RemapCount++;
$RemapName[$RemapCount] = "Strafe Left";
$RemapCmd[$RemapCount] = "moveleft";
$RemapCount++;
$RemapName[$RemapCount] = "Strafe Right";
$RemapCmd[$RemapCount] = "moveright";
$RemapCount++;
$RemapName[$RemapCount] = "Turn Left";
$RemapCmd[$RemapCount] = "turnLeft";
$RemapCount++;
$RemapName[$RemapCount] = "Turn Right";
$RemapCmd[$RemapCount] = "turnRight";
$RemapCount++;
$RemapName[$RemapCount] = "Look Up";
$RemapCmd[$RemapCount] = "panUp";
$RemapCount++;
$RemapName[$RemapCount] = "Look Down";
$RemapCmd[$RemapCount] = "panDown";
$RemapCount++;
$RemapName[$RemapCount] = "Jump";
$RemapCmd[$RemapCount] = "jump";
$RemapCount++;
$RemapName[$RemapCount] = "Crouch";  //FWAR 
$RemapCmd[$RemapCount] = "crouch";
$RemapCount++;
$RemapName[$RemapCount] = "Jetpack";  //FWAR 
$RemapCmd[$RemapCount] = "jet";
$RemapCount++;
$RemapName[$RemapCount] = "Sit Down"; 
$RemapCmd[$RemapCount] = "sit";
$RemapCount++;
$RemapName[$RemapCount] = "Freeze Me"; 
$RemapCmd[$RemapCount] = "freeze";
$RemapCount++;

$RemapName[$RemapCount] = "Action Key"; 
$RemapCmd[$RemapCount] = "doAction";
$RemapCount++;
$RemapName[$RemapCount] = "Open Editor Wand"; 
$RemapCmd[$RemapCount] = "openInGameEditor";
$RemapCount++;
$RemapName[$RemapCount] = "Cancel Auto Editor Settings"; 
$RemapCmd[$RemapCount] = "cancelAuto";
$RemapCount++;
$RemapName[$RemapCount] = "Change SprayCan";
$RemapCmd[$RemapCount] = "mouseWheelClick";
$RemapCount++;
$RemapName[$RemapCount] = "Toggle Wrench Mode";
$RemapCmd[$RemapCount] = "toggleWrenchMode";
$RemapCount++;

$RemapName[$RemapCount] = "Undo Last Brick";
$RemapCmd[$RemapCount] = "UndoLast";
$RemapCount++;
$RemapName[$RemapCount] = "Free Hands";
$RemapCmd[$RemapCount] = "FreeHands";
$RemapCount++;
$RemapName[$RemapCount] = "Screen Shot"; 
$RemapCmd[$RemapCount] = "takeScreenshot";
$RemapCount++;

$RemapName[$RemapCount] = "Free Look";
$RemapCmd[$RemapCount] = "toggleFreeLook";
$RemapCount++;

$RemapName[$RemapCount] = "Shift Brick Away";
$RemapCmd[$RemapCount] = "shiftBrickAway";
$RemapCount++;
$RemapName[$RemapCount] = "Shift Brick Towards";
$RemapCmd[$RemapCount] = "shiftBrickTowards";
$RemapCount++;
$RemapName[$RemapCount] = "Shift Brick Left";
$RemapCmd[$RemapCount] = "shiftBrickLeft";
$RemapCount++;
$RemapName[$RemapCount] = "Shift Brick Right";
$RemapCmd[$RemapCount] = "shiftBrickRight";
$RemapCount++;
$RemapName[$RemapCount] = "Shift Brick Up";
$RemapCmd[$RemapCount] = "shiftBrickUp";
$RemapCount++;
$RemapName[$RemapCount] = "Shift Brick Down";
$RemapCmd[$RemapCount] = "shiftBrickDown";
$RemapCount++;
$RemapName[$RemapCount] = "Shift Brick Up 1/3";
$RemapCmd[$RemapCount] = "shiftBrickThirdUp";
$RemapCount++;
$RemapName[$RemapCount] = "Shift Brick Down 1/3";
$RemapCmd[$RemapCount] = "shiftBrickThirdDown";
$RemapCount++;
$RemapName[$RemapCount] = "Super Shift";
$RemapCmd[$RemapCount] = "togSuperShift";
$RemapCount++;
$RemapName[$RemapCount] = "Tiny Shift";
$RemapCmd[$RemapCount] = "togTinyShift";
$RemapCount++;
$RemapName[$RemapCount] = "Rotate Brick CW";
$RemapCmd[$RemapCount] = "RotateBrickCW";
$RemapCount++;
$RemapName[$RemapCount] = "Rotate Brick CCW";
$RemapCmd[$RemapCount] = "RotateBrickCCW";
$RemapCount++;
$RemapName[$RemapCount] = "Switch 1st/3rd";
$RemapCmd[$RemapCount] = "toggleFirstPerson";
$RemapCount++;
$RemapName[$RemapCount] = "Message All";
$RemapCmd[$RemapCount] = "toggleMessageHud";
$RemapCount++;
$RemapName[$RemapCount] = "Message Team";
$RemapCmd[$RemapCount] = "teamMessageHud";
$RemapCount++;
$RemapName[$RemapCount] = "Message Local";
$RemapCmd[$RemapCount] = "localMessageHud";
$RemapCount++;
$RemapName[$RemapCount] = "Ingame IRC";
$RemapCmd[$RemapCount] = "IRCToggle";
$RemapCount++;
$RemapName[$RemapCount] = "Message Hud PageUp";
$RemapCmd[$RemapCount] = "pageMessageHudUp";
$RemapCount++;
$RemapName[$RemapCount] = "Message Hud PageDown";
$RemapCmd[$RemapCount] = "pageMessageHudDown";
$RemapCount++;
$RemapName[$RemapCount] = "Resize Message Hud";
$RemapCmd[$RemapCount] = "resizeMessageHud";
$RemapCount++;
$RemapName[$RemapCount] = "Drop Camera at Player";
$RemapCmd[$RemapCount] = "dropCameraAtPlayer";
$RemapCount++;
$RemapName[$RemapCount] = "Drop Player at Camera";
$RemapCmd[$RemapCount] = "dropPlayerAtCamera";
$RemapCount++;
$RemapName[$RemapCount] = "Use 1st Slot";
$RemapCmd[$RemapCount] = "useFirstSlot";
$RemapCount++;
$RemapName[$RemapCount] = "Use 2nd Slot";
$RemapCmd[$RemapCount] = "useSecondSlot";
$RemapCount++;
$RemapName[$RemapCount] = "Use 3rd Slot";
$RemapCmd[$RemapCount] = "useThirdSlot";
$RemapCount++;
$RemapName[$RemapCount] = "Use 4th Slot";
$RemapCmd[$RemapCount] = "useFourthSlot";
$RemapCount++;
$RemapName[$RemapCount] = "Use 5th Slot";
$RemapCmd[$RemapCount] = "useFifthSlot";
$RemapCount++;
$RemapName[$RemapCount] = "Use 6th Slot";
$RemapCmd[$RemapCount] = "useSixthSlot";
$RemapCount++;
$RemapName[$RemapCount] = "Use 7th Slot";
$RemapCmd[$RemapCount] = "useSeventhSlot";
$RemapCount++;
$RemapName[$RemapCount] = "Use 8th Slot";
$RemapCmd[$RemapCount] = "useEighthSlot";
$RemapCount++;
$RemapName[$RemapCount] = "Use 9th Slot";
$RemapCmd[$RemapCount] = "useNinthSlot";
$RemapCount++;
$RemapName[$RemapCount] = "Use 10th Slot";
$RemapCmd[$RemapCount] = "useTenthSlot";
$RemapCount++;

$RemapName[$RemapCount] = "Drop 1st Slot";
$RemapCmd[$RemapCount] = "dropFirstSlot";
$RemapCount++;
$RemapName[$RemapCount] = "Drop 2nd Slot";
$RemapCmd[$RemapCount] = "dropSecondSlot";
$RemapCount++;
$RemapName[$RemapCount] = "Drop 3rd Slot";
$RemapCmd[$RemapCount] = "dropThirdSlot";
$RemapCount++;
$RemapName[$RemapCount] = "Drop 4th Slot";
$RemapCmd[$RemapCount] = "dropFourthSlot";
$RemapCount++;
$RemapName[$RemapCount] = "Drop 5th Slot";
$RemapCmd[$RemapCount] = "dropFifthSlot";
$RemapCount++;
$RemapName[$RemapCount] = "Drop 6th Slot";
$RemapCmd[$RemapCount] = "dropSixthSlot";
$RemapCount++;
$RemapName[$RemapCount] = "Drop 7th Slot";
$RemapCmd[$RemapCount] = "dropSeventhSlot";
$RemapCount++;
$RemapName[$RemapCount] = "Drop 8th Slot";
$RemapCmd[$RemapCount] = "dropEighthSlot";
$RemapCount++;
$RemapName[$RemapCount] = "Drop 9th Slot";
$RemapCmd[$RemapCount] = "dropNinthSlot";
$RemapCount++;
$RemapName[$RemapCount] = "Drop 10th Slot";
$RemapCmd[$RemapCount] = "dropTenthSlot";
$RemapCount++;

$RemapName[$RemapCount] = "Scale Brick Up";
$RemapCmd[$RemapCount] = "ScaleZPlus";
$RemapCount++;
$RemapName[$RemapCount] = "Scale Brick Down";
$RemapCmd[$RemapCount] = "ScaleZMinus";
$RemapCount++;

$RemapName[$RemapCount] = "Plant Brick";
$RemapCmd[$RemapCount] = "plantBrick";
$RemapCount++;

$RemapName[$RemapCount] = "Cancel Brick";
$RemapCmd[$RemapCount] = "cancelBrick";
$RemapCount++;

$RemapName[$RemapCount] = "Open Admin Window";
$RemapCmd[$RemapCount] = "openAdminWindow";
$RemapCount++;

$RemapName[$RemapCount] = "Open Options Window";
$RemapCmd[$RemapCount] = "openOptionsWindow";
$RemapCount++;

$RemapName[$RemapCount] = "Open Player Window";
$RemapCmd[$RemapCount] = "openPlayerWindow";
$RemapCount++;

$RemapName[$RemapCount] = "Open Team Window";
$RemapCmd[$RemapCount] = "openTeamWindow";
$RemapCount++;

$RemapName[$RemapCount] = "Open Deathmatch Window";
$RemapCmd[$RemapCount] = "openDeathmatchWindow";
$RemapCount++;

$RemapName[$RemapCount] = "Open Messaging Window";
$RemapCmd[$RemapCount] = "openMessageWindow";
$RemapCount++;



function restoreDefaultMappings()
{
   moveMap.delete();
   exec( "~/client/scripts/default.bind.cs" );
   OptRemapList.fillList();
}

function getMapDisplayName( %device, %action )
{
	if ( %device $= "keyboard" )
		return( %action );		
	else if ( strstr( %device, "mouse" ) != -1 )
	{
		// Substitute "mouse" for "button" in the action string:
		%pos = strstr( %action, "button" );
		if ( %pos != -1 )
		{
			%mods = getSubStr( %action, 0, %pos );
			%object = getSubStr( %action, %pos, 1000 );
			%instance = getSubStr( %object, strlen( "button" ), 1000 );
			return( %mods @ "mouse" @ ( %instance + 1 ) );
		}
		else
			error( "Mouse input object other than button passed to getDisplayMapName!" );
	}
	else if ( strstr( %device, "joystick" ) != -1 )
	{
		// Substitute "joystick" for "button" in the action string:
		%pos = strstr( %action, "button" );
		if ( %pos != -1 )
		{
			%mods = getSubStr( %action, 0, %pos );
			%object = getSubStr( %action, %pos, 1000 );
			%instance = getSubStr( %object, strlen( "button" ), 1000 );
			return( %mods @ "joystick" @ ( %instance + 1 ) );
		}
		else
	   { 
	      %pos = strstr( %action, "pov" );
         if ( %pos != -1 )
         {
            %wordCount = getWordCount( %action );
            %mods = %wordCount > 1 ? getWords( %action, 0, %wordCount - 2 ) @ " " : "";
            %object = getWord( %action, %wordCount - 1 );
            switch$ ( %object )
            {
               case "upov":   %object = "POV1 up";
               case "dpov":   %object = "POV1 down";
               case "lpov":   %object = "POV1 left";
               case "rpov":   %object = "POV1 right";
               case "upov2":  %object = "POV2 up";
               case "dpov2":  %object = "POV2 down";
               case "lpov2":  %object = "POV2 left";
               case "rpov2":  %object = "POV2 right";
               default:       %object = "??";
            }
            return( %mods @ %object );
         }
         else
            error( "Unsupported Joystick input object passed to getDisplayMapName!" );
      }
	}
		
	return( "??" );		
}

function buildFullMapString( %index )
{
   %name       = $RemapName[%index];
   %cmd        = $RemapCmd[%index];

	%temp = moveMap.getBinding( %cmd );
   %device = getField( %temp, 0 );
   %object = getField( %temp, 1 );
   if ( %device !$= "" && %object !$= "" )
	   %mapString = getMapDisplayName( %device, %object );
   else
      %mapString = "";

	return( %name TAB %mapString );
}

function OptRemapList::fillList( %this )
{
	%this.clear();
   for ( %i = 0; %i < $RemapCount; %i++ )
      %this.addRow( %i, buildFullMapString( %i ) );
}

//------------------------------------------------------------------------------
function OptRemapList::doRemap( %this )
{
	%selId = %this.getSelectedId();
   %name = $RemapName[%selId];

	OptRemapText.setValue( "REMAP \"" @ %name @ "\"" );
	OptRemapInputCtrl.index = %selId;
	Canvas.pushDialog( RemapDlg );
}

//------------------------------------------------------------------------------
function redoMapping( %device, %action, %cmd, %oldIndex, %newIndex )
{
	//%actionMap.bind( %device, %action, $RemapCmd[%newIndex] );
	moveMap.bind( %device, %action, %cmd );
	OptRemapList.setRowById( %oldIndex, buildFullMapString( %oldIndex ) );
	OptRemapList.setRowById( %newIndex, buildFullMapString( %newIndex ) );
}

//------------------------------------------------------------------------------
function findRemapCmdIndex( %command )
{
	for ( %i = 0; %i < $RemapCount; %i++ )
	{
		if ( %command $= $RemapCmd[%i] )
			return( %i );			
	}
	return( -1 );	
}

function OptRemapInputCtrl::onInputEvent( %this, %device, %action )
{
	//error( "** onInputEvent called - device = " @ %device @ ", action = " @ %action @ " **" );
	Canvas.popDialog( RemapDlg );

	// Test for the reserved keystrokes:
	if ( %device $= "keyboard" )
	{
      // Cancel...
      if ( %action $= "escape" )
      {
         // Do nothing...
		   return;
      }
	}
	
   %cmd  = $RemapCmd[%this.index];
   %name = $RemapName[%this.index];

	// First check to see if the given action is already mapped:
	%prevMap = moveMap.getCommand( %device, %action );
   if ( %prevMap !$= %cmd )
   {
	   if ( %prevMap $= "" )
	   {
         moveMap.bind( %device, %action, %cmd );
		   OptRemapList.setRowById( %this.index, buildFullMapString( %this.index ) );
	   }
	   else
	   {
         %mapName = getMapDisplayName( %device, %action );
		   %prevMapIndex = findRemapCmdIndex( %prevMap );
		   if ( %prevMapIndex == -1 )
			   MessageBoxOK( "REMAP FAILED", "\"" @ %mapName @ "\" is already bound to a non-remappable command!" );
		   else
         {
            %prevCmdName = $RemapName[%prevMapIndex];
			   MessageBoxYesNo( "WARNING", 
				   "\"" @ %mapName @ "\" is already bound to \"" 
					   @ %prevCmdName @ "\"!\nDo you want to undo this mapping?", 
				   "redoMapping(" @ %device @ ", \"" @ %action @ "\", \"" @ %cmd @ "\", " @ %prevMapIndex @ ", " @ %this.index @ ");", "" );
         }
		   return;
	   }
   }
}

// Audio 
function OptAudioUpdate()
{
   // set the driver text
   %text =   "Vendor: " @ alGetString("AL_VENDOR") @
           "\nVersion: " @ alGetString("AL_VERSION") @
           "\nRenderer: " @ alGetString("AL_RENDERER") @
           "\nExtensions: " @ alGetString("AL_EXTENSIONS");
   OptAudioInfo.setText(%text);

}


// Channel 0 is unused in-game, but is used here to test master volume.

new AudioDescription(AudioChannel0)
{
   volume   = 1.0;
   isLooping= false;
   is3D     = false;
   type     = 0;
};

new AudioDescription(AudioChannel1)
{
   volume   = 1.0;
   isLooping= false;
   is3D     = false;
   type     = 1;
};

new AudioDescription(AudioChannel2)
{
   volume   = 1.0;
   isLooping= false;
   is3D     = false;
   type     = 2;
};

new AudioDescription(AudioChannel3)
{
   volume   = 1.0;
   isLooping= false;
   is3D     = false;
   type     = 3;
};

new AudioDescription(AudioChannel4)
{
   volume   = 1.0;
   isLooping= false;
   is3D     = false;
   type     = 4;
};

new AudioDescription(AudioChannel5)
{
   volume   = 1.0;
   isLooping= false;
   is3D     = false;
   type     = 5;
};

new AudioDescription(AudioChannel6)
{
   volume   = 1.0;
   isLooping= false;
   is3D     = false;
   type     = 6;
};

new AudioDescription(AudioChannel7)
{
   volume   = 1.0;
   isLooping= false;
   is3D     = false;
   type     = 7;
};

new AudioDescription(AudioChannel8)
{
   volume   = 1.0;
   isLooping= false;
   is3D     = false;
   type     = 8;
};

$AudioTestHandle = 0;

function OptAudioUpdateMasterVolume(%volume)
{
   if (%volume == $pref::Audio::masterVolume)
      return;
   alxListenerf(AL_GAIN_LINEAR, %volume);
   $pref::Audio::masterVolume = %volume;
   if (!alxIsPlaying($AudioTestHandle))
   {
      $AudioTestHandle = alxCreateSource("AudioChannel0", expandFilename("~/data/sound/testing.wav"));
      alxPlay($AudioTestHandle);
   }
}

function OptAudioUpdateChannelVolume(%channel, %volume)
{
   if (%channel < 1 || %channel > 8)
      return;
         
   if (%volume == $pref::Audio::channelVolume[%channel])
      return;

   alxSetChannelVolume(%channel, %volume);
   $pref::Audio::channelVolume[%channel] = %volume;
   if (!alxIsPlaying($AudioTestHandle))
   {
      $AudioTestHandle = alxCreateSource("AudioChannel"@%channel, expandFilename("~/data/sound/testing.wav"));
      alxPlay($AudioTestHandle);
   }
}


function OptAudioDriverList::onSelect( %this, %id, %text )
{
   if (%text $= "")
      return;
   
   if ($pref::Audio::driver $= %text)
      return;

   $pref::Audio::driver = %text;
   OpenALInit();
}



//New functions
function ScaleZPlus( %val )
{
	if ( %val )
	{
		commandToServer('ScaleZup', 0);	 
	}
}
function ScaleZMinus( %val )
{
	if ( %val )
	{
		commandToServer('ScaleZdown', 0);	 
	}
}
function cancelauto( %val )
{
	if ( %val )
	{
		commandToServer('cancelautosettings', 0);	 
	}
}
//////////////////////
//Use slot functions//
//////////////////////
function useFirstSlot( %val )
{
	if ( %val )
	{
		commandToServer('useInventory', 0);	 
	}
}
function useSecondSlot( %val )
{
	if ( %val )
	{
		commandToServer('useInventory', 1);	
	}
}
function useThirdSlot( %val )
{
	if ( %val )
	{
		commandToServer('useInventory', 2);	
	}
}
function useFourthSlot( %val )
{
	if ( %val )
	{
		commandToServer('useInventory', 3);	
	}
}
function useFifthSlot( %val )
{
	if ( %val )
	{
		commandToServer('useInventory', 4);	   	   
	}
}
function useSixthSlot( %val )
{
	if ( %val )
		commandToServer('useInventory', 5);	   
}
function useSeventhSlot( %val )
{
	if ( %val )
		commandToServer('useInventory', 6);	   
}
function useEighthSlot( %val )
{
	if ( %val )
		commandToServer('useInventory', 7);	   
}
function useNinthSlot( %val )
{
	if ( %val )
		commandToServer('useInventory', 8);	   
}
function useTenthSlot( %val )
{
	if ( %val )
		commandToServer('useInventory', 9);	   
}

//////////////////////
//Drop slot functions//
//////////////////////
function dropFirstSlot( %val )
{
	if ( %val )
		commandToServer('dropInventory', 0);	      
}
function dropSecondSlot( %val )
{
	if ( %val )
		commandToServer('dropInventory', 1);	   
}
function dropThirdSlot( %val )
{
	if ( %val )
		commandToServer('dropInventory', 2);	   
}
function dropFourthSlot( %val )
{
	if ( %val )
		commandToServer('dropInventory', 3);	   
}
function dropFifthSlot( %val )
{
	if ( %val )
		commandToServer('dropInventory', 4);	   
}
function dropSixthSlot( %val )
{
	if ( %val )
		commandToServer('dropInventory', 5);	   
}
function dropSeventhSlot( %val )
{
	if ( %val )
		commandToServer('dropInventory', 6);	   
}
function dropEighthSlot( %val )
{
	if ( %val )
		commandToServer('dropInventory', 7);	   
}
function dropNinthSlot( %val )
{
	if ( %val )
		commandToServer('dropInventory', 8);	   
}
function dropTenthSlot( %val )
{
	if ( %val )
		commandToServer('dropInventory', 9);	   
}

//###############
//Shift Functions
//###############
function shiftBrickAway( %val )
{
	if ( %val )
		commandToServer('shiftBrick', 1, 0, 0);	   
}
function shiftBrickTowards( %val )
{
	if ( %val )
		commandToServer('shiftBrick', -1, 0, 0);	   
}
function shiftBrickLeft( %val )
{
	if ( %val )
		commandToServer('shiftBrick', 0, 1, 0);	   
}
function shiftBrickRight( %val )
{
	if ( %val )
		commandToServer('shiftBrick', 0, -1, 0);	   
}


function shiftBrickUp( %val )
{
	if ( %val )
		commandToServer('shiftBrick', 0, 0, 3);	   
}
function shiftBrickDown( %val )
{
	if ( %val )
		commandToServer('shiftBrick', 0, 0, -3);	   
}
function rotateBrickCW( %val )
{
	if ( %val )
		commandToServer('rotateBrick', 1);	   
}
function rotateBrickCCW( %val )
{
	if ( %val )
		commandToServer('rotateBrick', -1);	   
}

function plantBrick( %val )
{
	if ( %val )
		commandToServer('plantBrick');	   
}

function cancelBrick( %val )
{
	if ( %val )
		commandToServer('cancelBrick');	   
}

function openPlayerWindow (%val)
{
	if(%val)
	{	
		canvas.pushDialog(admingui);
		playerA.setvisible(true);
		minigamesA.setvisible(false);
		teamsA.setVisible(false);
		AdminA.setVisible(false);
	}
}

function openTeamWindow (%val)
{
	if(%val)
	{	
		canvas.pushDialog(admingui);
		playerA.setvisible(false);
		minigamesA.setvisible(false);
		teamsA.setVisible(true);
		AdminA.setVisible(false);
	}
}
function openDeathmatchWindow (%val)
{
	if(%val)
	{	
		canvas.pushDialog(admingui);
		playerA.setvisible(false);
		minigamesA.setvisible(true);
		teamsA.setVisible(false);
		AdminA.setVisible(false);
	}
}
function openMessageWindow (%val)
{
	if(%val)	
		canvas.pushDialog(messagegui);
}
function openAdminWindow (%val)
{
	if(%val)
	{	
		canvas.pushDialog(admingui);
		playerA.setvisible(false);
		minigamesA.setvisible(false);
		teamsA.setVisible(false);
		AdminA.setVisible(true);
	}
}

function openOptionsWindow (%val)
{
	if(%val)	
		canvas.pushDialog(optionsDlg);
}

function jet(%val)
{
	if(%val)
		$mvTriggerCount3++;
}

function undoLast(%val)
{
	if(%val)
		commandtoServer('undoLast');
}

function toggleWrenchMode(%val)
{
	if(%val)
		commandtoServer('toggleWrenchMode');
}

function handleMouseWheel(%val)
{
      if ( %val < 0 )
      commandToServer('MouseScroll', 1);
      else if ( %val > 0 )
      commandToServer('MouseScroll', -1);
}
function FreeHands(%val)
{
	if(%val)
		commandtoserver('freeHands');
}
function mouseWheelClick(%val)
{
	if(%val)
		commandtoserver('MouseWheelClick');

}

function freeze(%val)
{
	if(%val)
		commandToServer('freezeme');
}
function doAction(%val)
{
	if(%val)
	{
		commandtoserver('doAction');
	}
}
function sit(%val)
{
	if(%val)
		commandToServer('sit');

}


function takeScreenShot( %val )
{
   if (%val)
   {
	MoneyBG.visible = 0;
	Money.visible = 0;
	hbarHUD.visible = 0;
	crosshairHUD.visible = 0;
	HudClock.visible = 0;
	hbarHUD2.visible = 0;
	InventoryControl.visible = 0;
	BrickIcon.visible = 0;
	LagIcon.visible = 0;
	TxtType.visible = 0;
	TxtType.visible = 0;
	TxtTyping.visible = 0;
	this.visible = 0;
	OuterChatHud.visible = 0;
	ChatHud.visible = 0;
	this.visible = 0;


	schedule(500,0,ascreenShot);
   }
}

function toggleFilmMode()
{
	if(Money.visible == 1)
	{
		Money.visible = 0;
		hbarHUD.visible = 0;
		crosshairHUD.visible = 0;
		HudClock.visible = 0;
		hbarHUD2.visible = 0;
		InventoryControl.visible = 0;
		BrickIcon.visible = 0;
		LagIcon.visible = 0;
		TxtType.visible = 0;
		TxtType.visible = 0;
		TxtTyping.visible = 0;
		OuterChatHud.visible = 0;
		ChatHud.visible = 0;
	}
	else
	{
		fixScreen();
	}
}

function ascreenshot()
{
      schedule(500,0,fixScreen);
      $pref::interior::showdetailmaps = false;
      if($pref::Video::screenShotFormat $= "JPEG")
         screenShot("screenshot_" @ formatImageNumber($Pref::screenshotNumber++) @ ".jpg", "JPEG");
      else if($pref::Video::screenShotFormat $= "PNG")
         screenShot("screenshot_" @ formatImageNumber($Pref::screenshotNumber++) @ ".png", "PNG");
      else
         screenShot("screenshot_" @ formatImageNumber($Pref::screenshotNumber++) @ ".png", "PNG");
}

function fixScreen()
{
	MoneyBG.visible = 1;
	Money.visible = 1;
	hbarHUD.visible = 1;
	crosshairHUD.visible = 1;
	HudClock.visible = 1;
	hbarHUD2.visible = 1;
	InventoryControl.visible = 1;
	BrickIcon.visible = 1;
	TxtType.visible = 1;
	TxtType.visible = 1;
	TxtTyping.visible = 1;
	OuterChatHud.visible = 1;
	ChatHud.visible = 1;
}
function openIngameEditor(%val)
{
	if(%val)
	{
		commandtoserver('openIGEditor', $IngameEditorOpen);
	}
}

function openRules()
{
	canvas.pushDialog(serverrulesGui);
}

function togTinyShift()
{
	commandtoserver('ModifyShiftSize', 1);
}

function togSuperShift()
{
	commandtoserver('ModifyShiftSize', 2);
}

function IRCToggle(%val)
{
	if(%val)
	{
		if($IRCOpen !$= 1)
		{
			canvas.pushDialog(guilobby);
		}
		else
		{
			canvas.popDialog(guilobby);
		}
	}
}

function shiftBrickThirdUp( %val )
{
	if ( %val )
		commandToServer('shiftBrick', 0, 0, 1);	   
}

function shiftBrickThirdDown( %val )
{
	if ( %val )
		commandToServer('shiftBrick', 0, 0, -1);	   
}