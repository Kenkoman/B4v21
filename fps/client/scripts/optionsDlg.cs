function optionsDlg::setPane(%this, %pane)
{
   OptAudioPane.setVisible(false);
   OptGraphicsPane.setVisible(false);
   OptNetworkPane.setVisible(false);
   OptControlsPane.setVisible(false);
   OptPlayerPane.setVisible(false);
   ("Opt" @ %pane @ "Pane").setVisible(true);
   OptRemapList.fillList();
}

function OptionsDlg::onWake(%this)
{
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

	//player prefs
	TxtPlayerName.setValue($pref::Player::Name);

	//clear the menu first to prevent dup entries
	OptPlayerSkinColorMenu.clear();
	OptPlayerSkinColorMenu.add("Red", 0);
	OptPlayerSkinColorMenu.add("Yellow", 1);
	OptPlayerSkinColorMenu.add("Green", 2);
	OptPlayerSkinColorMenu.add("Blue", 3);
	OptPlayerSkinColorMenu.add("White", 4);
	OptPlayerSkinColorMenu.add("Gray", 5);
	OptPlayerSkinColorMenu.add("Dark Gray", 6);
	OptPlayerSkinColorMenu.add("Black", 7);
	OptPlayerSkinColorMenu.add("Brown", 8);
	OptPlayerSkinColorMenu.setSelected($pref::Color::skin);

	OptPlayerHeadMenu.clear();
	OptPlayerHeadMenu.add("None", -1);
	OptPlayerHeadMenu.add("Space Helmet", 0);
	OptPlayerHeadMenu.add("Scout Hat", 1);
	OptPlayerHeadMenu.add("Pointy Helmet", 2);
	OptPlayerHeadMenu.setSelected($pref::Accessory::headCode);
	OptPlayerHeadMenu.onSelect();

	OptPlayerHeadColorMenu.clear();
	OptPlayerHeadColorMenu.add("Red", 0);
	OptPlayerHeadColorMenu.add("Yellow", 1);
	OptPlayerHeadColorMenu.add("Green", 2);
	OptPlayerHeadColorMenu.add("Blue", 3);
	OptPlayerHeadColorMenu.add("White", 4);
	OptPlayerHeadColorMenu.add("Gray", 5);
	OptPlayerHeadColorMenu.add("Dark Gray", 6);
	OptPlayerHeadColorMenu.add("Black", 7);
	OptPlayerHeadColorMenu.add("Brown", 8);
	OptPlayerHeadColorMenu.setSelected($pref::Accessory::headColor);


	//visor colors
	OptPlayerVisorColorMenu.clear();
	OptPlayerVisorColorMenu.add("Red", 0);
	OptPlayerVisorColorMenu.add("Yellow", 1);
	OptPlayerVisorColorMenu.add("Green", 2);
	OptPlayerVisorColorMenu.add("Blue", 3);
	OptPlayerVisorColorMenu.add("Light Blue", 9);
	OptPlayerVisorColorMenu.add("White", 4);
	OptPlayerVisorColorMenu.add("Black", 7);
	OptPlayerVisorColorMenu.setSelected($pref::Accessory::VisorColor);

	OptPlayerBackMenu.clear();
	OptPlayerBackMenu.add("None", -1);
	OptPlayerBackMenu.add("Cape", 0);
	OptPlayerBackMenu.add("Bucket", 1);
	OptPlayerBackMenu.add("Quiver", 2);
	OptPlayerBackMenu.add("Armor", 3);
	OptPlayerBackMenu.add("Pack", 4);
	OptPlayerBackMenu.add("Air Tank", 5);
	OptPlayerBackMenu.setSelected($pref::Accessory::BackCode);

	OptPlayerBackColorMenu.clear();
	OptPlayerBackColorMenu.add("Red", 0);
	OptPlayerBackColorMenu.add("Yellow", 1);
	OptPlayerBackColorMenu.add("Green", 2);
	OptPlayerBackColorMenu.add("Blue", 3);
	OptPlayerBackColorMenu.add("White", 4);
	OptPlayerBackColorMenu.add("Gray", 5);
	OptPlayerBackColorMenu.add("Dark Gray", 6);
	OptPlayerBackColorMenu.add("Black", 7);
	OptPlayerBackColorMenu.add("Brown", 8);
	OptPlayerBackColorMenu.setSelected($pref::Accessory::BackColor);

	//OptPlayerLeftHandMenu.clear();
	//OptPlayerLeftHandMenu.add("None", -1);
	//OptPlayerLeftHandMenu.add("Shield", 0);
	//OptPlayerLeftHandMenu.add("Goblet", 1);
	//OptPlayerLeftHandMenu.setSelected($pref::Accessory::LeftHandCode);
	//OptPlayerLeftHandMenu.onSelect();
	
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
	$pref::Player::Name = TxtPlayerName.getValue();
	$pref::Input::MouseSensitivity = SliderControlsMouseSensitivity.getValue();
	$pref::OpenGL::Anisotropy = SliderGraphicsAnisotropy.getValue();
	$pref::visibleDistanceMod = SliderGraphicsDistanceMod.getValue();

	$pref::Accessory::headCode = OptPlayerHeadMenu.getSelected();
	$pref::Accessory::headColor = OptPlayerHeadColorMenu.getSelected();
	
	$pref::Accessory::visorCode = OptPlayerVisorMenu.getSelected();
	$pref::Accessory::visorColor = OptPlayerVisorColorMenu.getSelected();

	$pref::Accessory::BackCode = OptPlayerBackMenu.getSelected();
	$pref::Accessory::BackColor = OptPlayerBackColorMenu.getSelected();

	//$pref::Accessory::leftHandCode = OptPlayerLeftHandMenu.getSelected();
	//$pref::Accessory::leftHandColor = OptPlayerLeftHandColorMenu.getSelected();

	$pref::Color::skin = OptPlayerSkinColorMenu.getSelected();

	$pref::Master0 = TxtMasterServer.getValue();

	//send our prefs to the server
	clientCmdUpdatePrefs();
}

function OptPlayerHeadMenu::onSelect( %this, %id, %text )
{
	//update visor and visor color menus based on id
	%headcode = OptPlayerHeadMenu.getSelected();

	OptPlayerVisorMenu.clear();
	OptPlayerVisorMenu.add("None", -1);
	OptPlayerVisorMenu.setSelected(-1);

	if(%headCode == 0)
	{
		//space Helmet
		OptPlayerVisorMenu.add("Visor", 1);
	}
	else if(%headCode == 1)
	{
		//scout hat
		OptPlayerVisorMenu.add("Tri-Plume", 0);
	}	

	//if the pref isnt in the list, pick none
	

	%text = OptPlayerVisorMenu.getTextById($pref::Accessory::visorCode);

	if(%text !$= "")
	{
		OptPlayerVisorMenu.setSelected($pref::Accessory::visorCode);
	}
	
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
$RemapName[$RemapCount] = "Walk";  //FWAR 
$RemapCmd[$RemapCount] = "walk";
$RemapCount++;
$RemapName[$RemapCount] = "Crouch";  //FWAR 
$RemapCmd[$RemapCount] = "crouch";
$RemapCount++;
$RemapName[$RemapCount] = "Jet";  //FWAR 
$RemapCmd[$RemapCount] = "jet";
$RemapCount++;
$RemapName[$RemapCount] = "Fire Weapon";
$RemapCmd[$RemapCount] = "mouseFire";
$RemapCount++;
$RemapName[$RemapCount] = "Adjust Zoom";
$RemapCmd[$RemapCount] = "setZoomFov";
$RemapCount++;
$RemapName[$RemapCount] = "Toggle Zoom";
$RemapCmd[$RemapCount] = "toggleZoom";
$RemapCount++;
$RemapName[$RemapCount] = "Free Look";
$RemapCmd[$RemapCount] = "toggleFreeLook";
$RemapCount++;
$RemapName[$RemapCount] = "Switch 1st/3rd";
$RemapCmd[$RemapCount] = "toggleFirstPerson";
$RemapCount++;
$RemapName[$RemapCount] = "Toggle Message Hud";
$RemapCmd[$RemapCount] = "toggleMessageHud";
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
$RemapName[$RemapCount] = "Toggle Camera";
$RemapCmd[$RemapCount] = "toggleCamera";
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

$RemapName[$RemapCount] = "Rotate Brick CW";
$RemapCmd[$RemapCount] = "RotateBrickCW";
$RemapCount++;
$RemapName[$RemapCount] = "Rotate Brick CCW";
$RemapCmd[$RemapCount] = "RotateBrickCCW";
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

function openAdminWindow (%val)
{
	if(%val)	
		canvas.pushDialog(admingui);
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