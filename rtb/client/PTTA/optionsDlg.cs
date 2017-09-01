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
   moveMap.save( "~/client/config2.cs" );

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
$RemapName[$RemapCount] = "Toggle Magic Wand Mode";
$RemapCmd[$RemapCount] = "toggleWandMode";
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
$RemapName[$RemapCount] = "Free Zoom";
$RemapCmd[$RemapCount] = "zoomin";
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
$RemapName[$RemapCount] = "Use 11th Slot";
$RemapCmd[$RemapCount] = "useSlot11";
$RemapCount++;
$RemapName[$RemapCount] = "Use 12th Slot";
$RemapCmd[$RemapCount] = "useSlot12";
$RemapCount++;
$RemapName[$RemapCount] = "Use 13th Slot";
$RemapCmd[$RemapCount] = "useSlot13";
$RemapCount++;
$RemapName[$RemapCount] = "Use 14th Slot";
$RemapCmd[$RemapCount] = "useSlot14";
$RemapCount++;
$RemapName[$RemapCount] = "Use 15th Slot";
$RemapCmd[$RemapCount] = "useSlot15";
$RemapCount++;
$RemapName[$RemapCount] = "Use 16th Slot";
$RemapCmd[$RemapCount] = "useSlot16";
$RemapCount++;
$RemapName[$RemapCount] = "Use 17th Slot";
$RemapCmd[$RemapCount] = "useSlot17";
$RemapCount++;
$RemapName[$RemapCount] = "Use 18th Slot";
$RemapCmd[$RemapCount] = "useSlot18";
$RemapCount++;
$RemapName[$RemapCount] = "Use 19th Slot";
$RemapCmd[$RemapCount] = "useSlot19";
$RemapCount++;
$RemapName[$RemapCount] = "Use 20th Slot";
$RemapCmd[$RemapCount] = "useSlot20";
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
$RemapName[$RemapCount] = "Drop 11th Slot";
$RemapCmd[$RemapCount] = "dropSlot11";
$RemapCount++;
$RemapName[$RemapCount] = "Drop 12th Slot";
$RemapCmd[$RemapCount] = "dropSlot12";
$RemapCount++;
$RemapName[$RemapCount] = "Drop 13th Slot";
$RemapCmd[$RemapCount] = "dropSlot13";
$RemapCount++;
$RemapName[$RemapCount] = "Drop 14th Slot";
$RemapCmd[$RemapCount] = "dropSlot14";
$RemapCount++;
$RemapName[$RemapCount] = "Drop 15th Slot";
$RemapCmd[$RemapCount] = "dropSlot15";
$RemapCount++;
$RemapName[$RemapCount] = "Drop 16th Slot";
$RemapCmd[$RemapCount] = "dropSlot16";
$RemapCount++;
$RemapName[$RemapCount] = "Drop 17th Slot";
$RemapCmd[$RemapCount] = "dropSlot17";
$RemapCount++;
$RemapName[$RemapCount] = "Drop 18th Slot";
$RemapCmd[$RemapCount] = "dropSlot18";
$RemapCount++;
$RemapName[$RemapCount] = "Drop 19th Slot";
$RemapCmd[$RemapCount] = "dropSlot19";
$RemapCount++;
$RemapName[$RemapCount] = "Drop 20th Slot";
$RemapCmd[$RemapCount] = "dropSlot20";
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

$RemapName[$RemapCount] = "CustomMove Forward";
$RemapCmd[$RemapCount] = "custommoveforward";
$RemapCount++;
$RemapName[$RemapCount] = "CustomMove Backward";
$RemapCmd[$RemapCount] = "custommovebackward";
$RemapCount++;
$RemapName[$RemapCount] = "CustomMove Left";
$RemapCmd[$RemapCount] = "custommoveleft";
$RemapCount++;
$RemapName[$RemapCount] = "CustomMove Right";
$RemapCmd[$RemapCount] = "custommoveright";
$RemapCount++;
$RemapName[$RemapCount] = "CustomMove Up";
$RemapCmd[$RemapCount] = "custommoveup";
$RemapCount++;
$RemapName[$RemapCount] = "CustomMove Small Up";
$RemapCmd[$RemapCount] = "custommoveupsmall";
$RemapCount++;
$RemapName[$RemapCount] = "CustomMove Down";
$RemapCmd[$RemapCount] = "custommovedown";
$RemapCount++;
$RemapName[$RemapCount] = "CustomMove Small Down";
$RemapCmd[$RemapCount] = "custommovedownsmall";
$RemapCount++;
$RemapName[$RemapCount] = "CustomRotate Z CW";
$RemapCmd[$RemapCount] = "customrotzcw";
$RemapCount++;
$RemapName[$RemapCount] = "CustomRotate Z CCW";
$RemapCmd[$RemapCount] = "customrotzccw";
$RemapCount++;
$RemapName[$RemapCount] = "CustomRotate X CW";
$RemapCmd[$RemapCount] = "customrotxcw";
$RemapCount++;
$RemapName[$RemapCount] = "CustomRotate X CCW";
$RemapCmd[$RemapCount] = "customrotxccw";
$RemapCount++;
$RemapName[$RemapCount] = "CustomRotate Y CW";
$RemapCmd[$RemapCount] = "customrotycw";
$RemapCount++;
$RemapName[$RemapCount] = "CustomRotate Y CCW";
$RemapCmd[$RemapCount] = "customrotyccw";
$RemapCount++;
$RemapName[$RemapCount] = "CustomScale Z Plus";
$RemapCmd[$RemapCount] = "customscalezplus";
$RemapCount++;
$RemapName[$RemapCount] = "CustomScale Z Minus";
$RemapCmd[$RemapCount] = "customscalezminus";
$RemapCount++;
$RemapName[$RemapCount] = "CustomScale X Plus";
$RemapCmd[$RemapCount] = "customscalexplus";
$RemapCount++;
$RemapName[$RemapCount] = "CustomScale X Minus";
$RemapCmd[$RemapCount] = "customscalexminus";
$RemapCount++;
$RemapName[$RemapCount] = "CustomScale Y Plus";
$RemapCmd[$RemapCount] = "customscaleyplus";
$RemapCount++;
$RemapName[$RemapCount] = "CustomScale Y Minus";
$RemapCmd[$RemapCount] = "customscaleyminus";
$RemapCount++;
$RemapName[$RemapCount] = "Open/Close CustomMoverGui";
$RemapCmd[$RemapCount] = "openfactorset";
$RemapCount++;
$RemapName[$RemapCount] = "ShiftFavorite Toggle";
$RemapCmd[$RemapCount] = "shiftfavtog";
$RemapCount++;
$RemapName[$RemapCount] = "ShiftFavorite ReverseToggle";
$RemapCmd[$RemapCount] = "shiftfavrevtog";
$RemapCount++;
$RemapName[$RemapCount] = "RotateFavorite Toggle";
$RemapCmd[$RemapCount] = "rotfavtog";
$RemapCount++;
$RemapName[$RemapCount] = "RotateFavorite ReverseToggle";
$RemapCmd[$RemapCount] = "rotfavrevtog";
$RemapCount++;
$RemapName[$RemapCount] = "ScaleFavorite Toggle";
$RemapCmd[$RemapCount] = "scalefavtog";
$RemapCount++;
$RemapName[$RemapCount] = "ScaleFavorite ReverseToggle";
$RemapCmd[$RemapCount] = "scalefavrevtog";
$RemapCount++;

$RemapName[$RemapCount] = "Open BAC's MENU";
$RemapCmd[$RemapCount] = "openBACsM";
$RemapCount++;
$RemapName[$RemapCount] = "Open Wrench Menu";
$RemapCmd[$RemapCount] = "WrenchM";
$RemapCount++;
$RemapName[$RemapCount] = "Open Wand Menu";
$RemapCmd[$RemapCount] = "WandM";
$RemapCount++;

$RemapName[$RemapCount] = "Open Radar Menu";
$RemapCmd[$RemapCount] = "RadarM";
$RemapCount++;
//$RemapName[$RemapCount] = "Start/Stop Timescale";
//$RemapCmd[$RemapCount] = "timescale";
//$RemapCount++;
$RemapName[$RemapCount] = "Toggle Brickscrolling";
$RemapCmd[$RemapCount] = "isbrickscroll";
$RemapCount++;


function restoreDefaultMappings()
{
   moveMap.delete();
   exec( "~/client/PTTA/default.bind.cs" );
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
	if ( %val ){
		commandToServer('ScaleZup', 0);
		if ($isBrickScroll == 1)
			{
			schedule( 75, 0, repeatScaleZPlus);
			$ScaleBrickZPlus = 1;
			}
		}
	if ( %val == 0 )
		$ScaleBrickZPlus = 0;
}
function repeatScaleZPlus()
{
	if( $ScaleBrickZPlus == 1 ){
		commandToServer('ScaleZup', 0);
		schedule( 75, 0, repeatScaleZPlus);
	}
}
function ScaleZMinus( %val )
{
	if ( %val ){
		commandToServer('ScaleZdown', 0);	 
		if ($isBrickScroll == 1)
			{
			schedule( 75, 0, repeatScaleZMinus);
			$ScaleBrickZMinus = 1;
			}
		}
	if ( %val == 0 )
		$ScaleBrickZMinus = 0;
}
function repeatScaleZMinus()
{
	if( $ScaleBrickZMinus == 1 ){
		commandToServer('ScaleZdown', 0);
		schedule( 75, 0, repeatScaleZMinus);
	}
}
function cancelauto( %val )
{
	if ( %val )
	{
		commandToServer('cancelautosettings', 0);	 
	}
}
function openBACsM( %val )
{
	if ( %val )
	{
		BACsM.toggle();
	}
}
function WrenchM( %val )
{
	if ( %val )
	{
		WrenchM.toggle();
	}
}
function WandM( %val )
{
	if ( %val )
	{
		WandM.toggle();
	}
}

function RadarM( %val )
{
	if ( %val )
	{
		GuiRadar.toggle();
	}
}
//function timescale( %val )
//{
//	if ( %val )
//	{
//		$timescale = 0.25;
//	}
//	if ( !%val )
//	{
//		$timescale = 1;
//	}
//}

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
function useSlot11( %val )
{
	if ( %val )
	{
		commandToServer('useInventory', 10);	  
	}
}
function useSlot12( %val )
{
	if ( %val )
	{
		commandToServer('useInventory', 11);	  
	}
}
function useSlot13( %val )
{
	if ( %val )
	{
		commandToServer('useInventory', 12);	  
	}
}
function useSlot14( %val )
{
	if ( %val )
	{
		commandToServer('useInventory', 13);	  
	}
}
function useSlot15( %val )
{
	if ( %val )
	{
		commandToServer('useInventory', 14);	  
	}
}
function useSlot16( %val )
{
	if ( %val )
	{
		commandToServer('useInventory', 15);	  
	}
}
function useSlot17( %val )
{
	if ( %val )
	{
		commandToServer('useInventory', 16);	  
	}
}
function useSlot18( %val )
{
	if ( %val )
	{
		commandToServer('useInventory', 17);	  
	}
}
function useSlot19( %val )
{
	if ( %val )
	{
		commandToServer('useInventory', 18);	  
	}
}
function useSlot20( %val )
{
	if ( %val )
	{
		commandToServer('useInventory', 19);	  
	}
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
function dropSlot11( %val )
{
	if ( %val )
	{
		commandToServer('dropInventory', 10);
	}	   
}
function dropSlot12( %val )
{
	if ( %val )
	{
		commandToServer('dropInventory', 11);
	}	   
}
function dropSlot13( %val )
{
	if ( %val )
	{
		commandToServer('dropInventory', 12);
	}	   
}
function dropSlot14( %val )
{
	if ( %val )
	{
		commandToServer('dropInventory', 13);
	}	   
}
function dropSlot15( %val )
{
	if ( %val )
	{
		commandToServer('dropInventory', 14);
	}	   
}
function dropSlot16( %val )
{
	if ( %val )
	{
		commandToServer('dropInventory', 15);
	}	   
}
function dropSlot17( %val )
{
	if ( %val )
	{
		commandToServer('dropInventory', 16);
	}	   
}
function dropSlot18( %val )
{
	if ( %val )
	{
		commandToServer('dropInventory', 17);
	}	   
}
function dropSlot19( %val )
{
	if ( %val )
	{
		commandToServer('dropInventory', 18);
	}	   
}
function dropSlot20( %val )
{
	if ( %val )
	{
		commandToServer('dropInventory', 19);
	}	   
}

//###############
//Shift Functions
//###############
function isbrickscroll( %val )
{
	if ( %val )
	{
	$isBrickScroll = 1;	
	}
	if ( !%val )
	{
	$isBrickScroll = 0;		
	}
}
function shiftBrickAway( %val )
{
	if ( %val ){
		commandToServer('shiftBrick', 1, 0, 0);
		if ($isBrickScroll == 1)
			{
			schedule( 75, 0, repeatshiftaway);
			$ShiftBrickAway = 1;
			}
		}
	if ( %val == 0 )
		$ShiftBrickAway = 0;
}
function repeatshiftaway()
{
	if( $ShiftBrickAway == 1 ){
		commandToServer('shiftBrick', 1, 0, 0);
		schedule( 75, 0, repeatshiftaway);
	}
}
function shiftBrickTowards( %val )
{
	if ( %val ){
		commandToServer('shiftBrick', -1, 0, 0);
		if ($isBrickScroll == 1)
			{
			schedule( 75, 0, repeatshifttowards);
			$ShiftBricktowards = 1;
			}
		}
	if ( %val == 0 )
		$ShiftBricktowards = 0;
}
function repeatshifttowards()
{
	if( $ShiftBricktowards == 1 ){
		commandToServer('shiftBrick', -1, 0, 0);
		schedule( 75, 0, repeatshifttowards);
	}
}
function shiftBrickleft( %val )
{
	if ( %val ){
		commandToServer('shiftBrick', 0, 1, 0);
		if ($isBrickScroll == 1)
			{
			schedule( 75, 0, repeatshiftleft);
			$ShiftBrickleft = 1;
			}
		}
	if ( %val == 0 )
		$ShiftBrickleft = 0;
}
function repeatshiftleft()
{
	if( $ShiftBrickleft == 1 ){
		commandToServer('shiftBrick', 0, 1, 0);
		schedule( 75, 0, repeatshiftleft);
	}
}
function shiftBrickright( %val )
{
	if ( %val ){
		commandToServer('shiftBrick', 0, -1, 0);
		if ($isBrickScroll == 1)
			{
			schedule( 75, 0, repeatshiftright);
			$ShiftBrickright = 1;
			}
		}
	if ( %val == 0 )
		$ShiftBrickright = 0;
}
function repeatshiftright()
{
	if( $ShiftBrickright == 1 ){
		commandToServer('shiftBrick', 0, -1, 0);
		schedule( 75, 0, repeatshiftright);
	}
}
function shiftBrickup( %val )
{
	if ( %val ){
		commandToServer('shiftBrick', 0, 0, 3);
		if ($isBrickScroll == 1)
			{
			schedule( 75, 0, repeatshiftup);
			$ShiftBrickup = 1;
			}
		}
	if ( %val == 0 )
		$ShiftBrickup = 0;
}
function repeatshiftup()
{
	if( $ShiftBrickup == 1 ){
		commandToServer('shiftBrick', 0, 0, 3);
		schedule( 75, 0, repeatshiftup);
	}
}
function shiftBrickdown( %val )
{
	if ( %val ){
		commandToServer('shiftBrick', 0, 0, -3);
		if ($isBrickScroll == 1)
			{
			schedule( 75, 0, repeatshiftdown);
			$ShiftBrickdown = 1;
			}
		}
	if ( %val == 0 )
		$ShiftBrickdown = 0;
}
function repeatshiftdown()
{
	if( $ShiftBrickdown == 1 ){
		commandToServer('shiftBrick', 0, 0, -3);
		schedule( 75, 0, repeatshiftdown);
	}
}

function rotateBrickCW( %val )
{
	if ( %val ){
		commandToServer('rotateBrick', 1);	   
		if ($isBrickScroll == 1)
			{
			schedule( 75, 0, repeatrotateCW);
			$rotateBrickCW = 1;
			}
		}
	if ( %val == 0 )
		$rotateBrickCW = 0;
}
function repeatrotateCW()
{
	if( $rotateBrickCW == 1 ){
		commandToServer('rotateBrick', 1);
		schedule( 75, 0, repeatrotateCW);
	}
}
function rotateBrickCCW( %val )
{
	if ( %val ){
		commandToServer('rotateBrick', -1);	   
		if ($isBrickScroll == 1)
			{
			schedule( 75, 0, repeatrotateCCW);
			$rotateBrickCCW = 1;
			}
		}
	if ( %val == 0 )
		$rotateBrickCCW = 0;
}
function repeatrotateCCW()
{
	if( $rotateBrickCCW == 1 ){
		commandToServer('rotateBrick', -1);
		schedule( 75, 0, repeatrotateCCW);
	}
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
		$mvTriggerCount4++;
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

function toggleWandMode(%val)
{
	if(%val)
		commandtoServer('toggleWandMode');
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
function openInGameEditor(%val)
{
	if(%val)
	{
		canvas.pushdialog(InGameEditorGui);
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

function shiftBrickthirdup( %val )
{
	if ( %val ){
		commandToServer('shiftBrick', 0, 0, 1);
		if ($isBrickScroll == 1)
			{
			schedule( 75, 0, repeatshiftthirdup);
			$ShiftBrickthirdup = 1;
			}
		}
	if ( %val == 0 )
		$ShiftBrickthirdup = 0;
}
function repeatshiftthirdup()
{
	if( $ShiftBrickthirdup == 1 ){
		commandToServer('shiftBrick', 0, 0, 1);
		schedule( 75, 0, repeatshiftthirdup);
	}
}
function shiftBrickthirddown( %val )
{
	if ( %val ){
		commandToServer('shiftBrick', 0, 0, -1);
		if ($isBrickScroll == 1)
			{
			schedule( 75, 0, repeatshiftthirddown);
			$ShiftBrickthirddown = 1;
			}
		}
	if ( %val == 0 )
		$ShiftBrickthirddown = 0;
}
function repeatshiftthirddown()
{
	if( $ShiftBrickthirddown == 1 ){
		commandToServer('shiftBrick', 0, 0, -1);
		schedule( 75, 0, repeatshiftthirddown);
	}
}

/////////////////////////////
//Bleh's Custom Brick Shift//
/////////////////////////////
//The Gui is in here because I was too lazy to exec the gui file.
//--- OBJECT WRITE BEGIN ---
new GuiControl(factorsetgui) {
   profile = "GuiDefaultProfile";
   horizSizing = "right";
   vertSizing = "bottom";
   position = "0 0";
   extent = "640 480";
   minExtent = "8 2";
   visible = "1";
   helpTag = "0";

   new GuiWindowCtrl(factorwindow) {
      profile = "GuiWindowProfile";
      horizSizing = "right";
      vertSizing = "bottom";
      position = "200 175";
      extent = "250 225";
      minExtent = "8 2";
      visible = "1";
      helpTag = "0";
      text = "Custom Movement Factor Settings";
      maxLength = "255";
      resizeWidth = "0";
      resizeHeight = "0";
      canMove = "0";
      canClose = "0";
      canMinimize = "0";
      canMaximize = "0";
      minSize = "50 50";
      closeCommand = "canvas.popDialog(factorsetgui);";

      new GuiTextEditCtrl(txtshiftfactor) {
         profile = "GuiTextEditProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "100 30";
         extent = "20 18";
         minExtent = "8 2";
         visible = "1";
         helpTag = "0";
         maxLength = "10";
         historySize = "0";
         password = "0";
         tabComplete = "0";
         sinkAllKeyEvents = "0";
      };
      new GuiTextCtrl() {
         profile = "GuiTextProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "10 30";
         extent = "86 18";
         minExtent = "8 2";
         visible = "1";
         helpTag = "0";
         text = "Movement Factor:";
         maxLength = "255";
      };
      new GuiTextEditCtrl(txtscalefactor) {
         profile = "GuiTextEditProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "80 50";
         extent = "20 18";
         minExtent = "8 2";
         visible = "1";
         helpTag = "0";
         maxLength = "10";
         historySize = "0";
         password = "0";
         tabComplete = "0";
         sinkAllKeyEvents = "0";
      };
      new GuiTextCtrl() {
         profile = "GuiTextProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "10 50";
         extent = "64 18";
         minExtent = "8 2";
         visible = "1";
         helpTag = "0";
         text = "Scale Factor:";
         maxLength = "255";
      };
      new GuiTextEditCtrl(txtrotdegrees) {
         profile = "GuiTextEditProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "220 30";
         extent = "20 18";
         minExtent = "8 2";
         visible = "1";
         helpTag = "0";
         maxLength = "10";
         historySize = "0";
         password = "0";
         tabComplete = "0";
         sinkAllKeyEvents = "0";
      };
      new GuiTextCtrl() {
         profile = "GuiTextProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "130 30";
         extent = "86 18";
         minExtent = "8 2";
         visible = "1";
         helpTag = "0";
         text = "Rotation Degrees:";
         maxLength = "255";
      };
      new GuiTextCtrl() {
         profile = "GuiTextProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "10 65";
         extent = "227 18";
         minExtent = "8 2";
         visible = "1";
         helpTag = "0";
         text = "Factors are autosaved.  \'NumpadDecimal\' closes window.";
         maxLength = "255";
      };
      new GuiTextCtrl(txtCompatibility) {
         profile = "GuiTextProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "10 80";
         extent = "208 18";
         minExtent = "8 2";
         visible = "1";
         helpTag = "0";
         maxLength = "255";
      };
      new GuiButtonCtrl(AdvancedToggle) {
         profile = "GuiButtonProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "10 100";
         extent = "230 20";
         minExtent = "8 2";
         visible = "1";
         command = "toggleadvanced();";
         helpTag = "0";
         text = "Advanced...";
         groupNum = "-1";
         buttonType = "PushButton";
      };
      new GuiButtonCtrl(TogCustoms) {
         profile = "GuiButtonProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "90 125";
         extent = "80 20";
         minExtent = "8 2";
         visible = "1";
         command = "commandtoserver(\'admintogglecustoms\');";
         helpTag = "0";
         text = "Toggle Customs";
         groupNum = "-1";
         buttonType = "PushButton";
      };
      new GuiTextCtrl() {
         profile = "GuiTextProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "10 125";
         extent = "73 18";
         minExtent = "8 2";
         visible = "1";
         helpTag = "0";
         text = "Admin Options:";
         maxLength = "255";
      };
      new GuiTextCtrl() {
         profile = "GuiTextProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "10 185";
         extent = "78 18";
         minExtent = "8 2";
         visible = "1";
         helpTag = "0";
         text = "Scale Favorites:";
         maxLength = "255";
      };
      new GuiTextCtrl() {
         profile = "GuiTextProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "10 165";
         extent = "82 18";
         minExtent = "8 2";
         visible = "1";
         helpTag = "0";
         text = "Rotate Favorites:";
         maxLength = "255";
      };
      new GuiTextCtrl() {
         profile = "GuiTextProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "10 145";
         extent = "73 18";
         minExtent = "8 2";
         visible = "1";
         helpTag = "0";
         text = "Shift Favorites:";
         maxLength = "255";
      };
      new GuiTextEditCtrl(txtshiftfav1) {
         profile = "GuiTextEditProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "90 145";
         extent = "20 18";
         minExtent = "8 2";
         visible = "1";
         helpTag = "0";
         maxLength = "10";
         historySize = "0";
         password = "0";
         tabComplete = "0";
         sinkAllKeyEvents = "0";
      };
      new GuiTextEditCtrl(txtshiftfav2) {
         profile = "GuiTextEditProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "110 145";
         extent = "20 18";
         minExtent = "8 2";
         visible = "1";
         helpTag = "0";
         maxLength = "10";
         historySize = "0";
         password = "0";
         tabComplete = "0";
         sinkAllKeyEvents = "0";
      };
      new GuiTextEditCtrl(txtshiftfav3) {
         profile = "GuiTextEditProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "130 145";
         extent = "20 18";
         minExtent = "8 2";
         visible = "1";
         helpTag = "0";
         maxLength = "10";
         historySize = "0";
         password = "0";
         tabComplete = "0";
         sinkAllKeyEvents = "0";
      };
      new GuiTextEditCtrl(txtshiftfav4) {
         profile = "GuiTextEditProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "150 145";
         extent = "20 18";
         minExtent = "8 2";
         visible = "1";
         helpTag = "0";
         maxLength = "10";
         historySize = "0";
         password = "0";
         tabComplete = "0";
         sinkAllKeyEvents = "0";
      };
      new GuiTextEditCtrl(txtrotfav1) {
         profile = "GuiTextEditProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "90 165";
         extent = "20 18";
         minExtent = "8 2";
         visible = "1";
         helpTag = "0";
         maxLength = "10";
         historySize = "0";
         password = "0";
         tabComplete = "0";
         sinkAllKeyEvents = "0";
      };
      new GuiTextEditCtrl(txtrotfav2) {
         profile = "GuiTextEditProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "110 165";
         extent = "20 18";
         minExtent = "8 2";
         visible = "1";
         helpTag = "0";
         maxLength = "10";
         historySize = "0";
         password = "0";
         tabComplete = "0";
         sinkAllKeyEvents = "0";
      };
      new GuiTextEditCtrl(txtrotfav3) {
         profile = "GuiTextEditProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "130 165";
         extent = "20 18";
         minExtent = "8 2";
         visible = "1";
         helpTag = "0";
         maxLength = "10";
         historySize = "0";
         password = "0";
         tabComplete = "0";
         sinkAllKeyEvents = "0";
      };
      new GuiTextEditCtrl(txtrotfav4) {
         profile = "GuiTextEditProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "150 165";
         extent = "20 18";
         minExtent = "8 2";
         visible = "1";
         helpTag = "0";
         maxLength = "10";
         historySize = "0";
         password = "0";
         tabComplete = "0";
         sinkAllKeyEvents = "0";
      };
      new GuiTextEditCtrl(txtscalefav1) {
         profile = "GuiTextEditProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "90 185";
         extent = "20 18";
         minExtent = "8 2";
         visible = "1";
         helpTag = "0";
         maxLength = "10";
         historySize = "0";
         password = "0";
         tabComplete = "0";
         sinkAllKeyEvents = "0";
      };
      new GuiTextEditCtrl(txtscalefav2) {
         profile = "GuiTextEditProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "110 185";
         extent = "20 18";
         minExtent = "8 2";
         visible = "1";
         helpTag = "0";
         maxLength = "10";
         historySize = "0";
         password = "0";
         tabComplete = "0";
         sinkAllKeyEvents = "0";
      };
      new GuiTextEditCtrl(txtscalefav3) {
         profile = "GuiTextEditProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "130 185";
         extent = "20 18";
         minExtent = "8 2";
         visible = "1";
         helpTag = "0";
         maxLength = "10";
         historySize = "0";
         password = "0";
         tabComplete = "0";
         sinkAllKeyEvents = "0";
      };
      new GuiTextEditCtrl(txtscalefav4) {
         profile = "GuiTextEditProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "150 185";
         extent = "20 18";
         minExtent = "8 2";
         visible = "1";
         helpTag = "0";
         maxLength = "10";
         historySize = "0";
         password = "0";
         tabComplete = "0";
         sinkAllKeyEvents = "0";
      };
   };
};
//--- OBJECT WRITE END ---

function shiftfavtog( %val )
{
	if ( %val )
	{
		if($CurrentShiftFavPos $= "")
			$CurrentShiftFavPos = 0;
		
		$ShiftFavPos1 = $pref::player::customshiftshiftfav1;
		$ShiftFavPos2 = $pref::player::customshiftshiftfav2;
		$ShiftFavPos3 = $pref::player::customshiftshiftfav3;
		$ShiftFavPos4 = $pref::player::customshiftshiftfav4;

		if($CurrentShiftFavPos == 0){
			$CurrentShiftFavPos = 1;
			$Pref::player::customshiftfactor = $ShiftFavPos1;
			ChatHud.addLine("Your Custom Shift Factor is now " @ $ShiftFavPos1);
			return;	
		}
		if($CurrentShiftFavPos == 1){
			$CurrentShiftFavPos = 2;
			$Pref::player::customshiftfactor = $ShiftFavPos2;
			ChatHud.addLine("Your Custom Shift Factor is now " @ $ShiftFavPos2);
			return;
		}
		if($CurrentShiftFavPos == 2){
			$CurrentShiftFavPos = 3;
			$Pref::player::customshiftfactor = $ShiftFavPos3;
			ChatHud.addLine("Your Custom Shift Factor is now " @ $ShiftFavPos3);
			return;
		}
		if($CurrentShiftFavPos == 3){
			$CurrentShiftFavPos = 0;
			$Pref::player::customshiftfactor = $ShiftFavPos4;
			ChatHud.addLine("Your Custom Shift Factor is now " @ $ShiftFavPos4);
			return;
		}
	}
}
function shiftfavrevtog( %val )
{
	if ( %val )
	{
		if($CurrentShiftFavPos $= "")
			$CurrentShiftFavPos = 0;
		
		$ShiftFavPos1 = $pref::player::customshiftshiftfav1;
		$ShiftFavPos2 = $pref::player::customshiftshiftfav2;
		$ShiftFavPos3 = $pref::player::customshiftshiftfav3;
		$ShiftFavPos4 = $pref::player::customshiftshiftfav4;

		if($CurrentShiftFavPos == 0){
			$CurrentShiftFavPos = 3;
			$Pref::player::customshiftfactor = $ShiftFavPos1;
			ChatHud.addLine("Your Custom Shift Factor is now " @ $ShiftFavPos1);
			return;	
		}
		if($CurrentShiftFavPos == 1){
			$CurrentShiftFavPos = 0;
			$Pref::player::customshiftfactor = $ShiftFavPos2;
			ChatHud.addLine("Your Custom Shift Factor is now " @ $ShiftFavPos2);
			return;
		}
		if($CurrentShiftFavPos == 2){
			$CurrentShiftFavPos = 1;
			$Pref::player::customshiftfactor = $ShiftFavPos3;
			ChatHud.addLine("Your Custom Shift Factor is now " @ $ShiftFavPos3);
			return;
		}
		if($CurrentShiftFavPos == 3){
			$CurrentShiftFavPos = 2;
			$Pref::player::customshiftfactor = $ShiftFavPos4;
			ChatHud.addLine("Your Custom Shift Factor is now " @ $ShiftFavPos4);
			return;
		}
	}
}
function rotfavtog( %val )
{
	if ( %val )
	{
		if($CurrentRotFavPos $= "")
			$CurrentRotFavPos = 0;
		
		$RotFavPos1 = $pref::player::customshiftrotfav1;
		$RotFavPos2 = $pref::player::customshiftrotfav2;
		$RotFavPos3 = $pref::player::customshiftrotfav3;
		$RotFavPos4 = $pref::player::customshiftrotfav4;

		if($CurrentrotFavPos == 0){
			$CurrentrotFavPos = 1;
			$Pref::player::customrotatedegrees = $rotFavPos1;
			ChatHud.addLine("Your Custom Rotate Factor is now " @ $RotFavPos1);
			return;
		}
		if($CurrentrotFavPos == 1){
			$CurrentrotFavPos = 2;
			$Pref::player::customrotatedegrees = $rotFavPos2;
			ChatHud.addLine("Your Custom Rotate Factor is now " @ $RotFavPos2);
			return;
		}
		if($CurrentrotFavPos == 2){
			$CurrentrotFavPos = 3;
			$Pref::player::customrotatedegrees = $rotFavPos3;
			ChatHud.addLine("Your Custom Rotate Factor is now " @ $RotFavPos3);
			return;
		}
		if($CurrentrotFavPos == 3){
			$CurrentrotFavPos = 0;
			$Pref::player::customrotatedegrees = $rotFavPos4;
			ChatHud.addLine("Your Custom Rotate Factor is now " @ $RotFavPos4);
			return;
		}
	}
}

function rotfavrevtog( %val )
{
	if ( %val )
	{
		if($CurrentRotFavPos $= "")
			$CurrentRotFavPos = 0;
		
		$RotFavPos1 = $pref::player::customshiftrotfav1;
		$RotFavPos2 = $pref::player::customshiftrotfav2;
		$RotFavPos3 = $pref::player::customshiftrotfav3;
		$RotFavPos4 = $pref::player::customshiftrotfav4;

		if($CurrentrotFavPos == 0){
			$CurrentrotFavPos = 3;
			$Pref::player::customrotatedegrees = $rotFavPos1;
			ChatHud.addLine("Your Custom Rotate Factor is now " @ $RotFavPos1);
			return;
		}
		if($CurrentrotFavPos == 1){
			$CurrentrotFavPos = 0;
			$Pref::player::customrotatedegrees = $rotFavPos2;
			ChatHud.addLine("Your Custom Rotate Factor is now " @ $RotFavPos2);
			return;
		}
		if($CurrentrotFavPos == 2){
			$CurrentrotFavPos = 1;
			$Pref::player::customrotatedegrees = $rotFavPos3;
			ChatHud.addLine("Your Custom Rotate Factor is now " @ $RotFavPos3);
			return;
		}
		if($CurrentrotFavPos == 3){
			$CurrentrotFavPos = 2;
			$Pref::player::customrotatedegrees = $rotFavPos4;
			ChatHud.addLine("Your Custom Rotate Factor is now " @ $RotFavPos4);
			return;
		}
	}
}
function scalefavtog( %val )
{
	if ( %val )
	{
		if($CurrentscaleFavPos $= "")
			$CurrentscaleFavPos = 0;
		
		$scaleFavPos1 = $pref::player::customshiftscalefav1;
		$scaleFavPos2 = $pref::player::customshiftscalefav2;
		$scaleFavPos3 = $pref::player::customshiftscalefav3;
		$scaleFavPos4 = $pref::player::customshiftscalefav4;

		if($CurrentscaleFavPos == 0){
			$CurrentscaleFavPos = 1;
			$Pref::player::customscalefactor = $scaleFavPos1;
			ChatHud.addLine("Your Custom Scale Factor is now " @ $ScaleFavPos1);
			return;
		}
		if($CurrentscaleFavPos == 1){
			$CurrentscaleFavPos = 2;
			$Pref::player::customscalefactor = $scaleFavPos2;
			ChatHud.addLine("Your Custom Scale Factor is now " @ $ScaleFavPos2);
			return;
		}
		if($CurrentscaleFavPos == 2){
			$CurrentscaleFavPos = 3;
			$Pref::player::customscalefactor = $scaleFavPos3;
			ChatHud.addLine("Your Custom Scale Factor is now " @ $ScaleFavPos3);
			return;
		}
		if($CurrentscaleFavPos == 3){
			$CurrentscaleFavPos = 0;
			$Pref::player::customscalefactor = $scaleFavPos4;
			ChatHud.addLine("Your Custom Scale Factor is now " @ $ScaleFavPos4);
			return;
		}
	}
}

function scalefavrevtog( %val )
{
	if ( %val )
	{
		if($CurrentscaleFavPos $= "")
			$CurrentscaleFavPos = 0;
		
		$scaleFavPos1 = $pref::player::customshiftscalefav1;
		$scaleFavPos2 = $pref::player::customshiftscalefav2;
		$scaleFavPos3 = $pref::player::customshiftscalefav3;
		$scaleFavPos4 = $pref::player::customshiftscalefav4;

		if($CurrentscaleFavPos == 0){
			$CurrentscaleFavPos = 3;
			$Pref::player::customscalefactor = $scaleFavPos1;
			ChatHud.addLine("Your Custom Scale Factor is now " @ $ScaleFavPos1);
			return;
		}
		if($CurrentscaleFavPos == 1){
			$CurrentscaleFavPos = 0;
			$Pref::player::customscalefactor = $scaleFavPos2;
			ChatHud.addLine("Your Custom Scale Factor is now " @ $ScaleFavPos2);
			return;
		}
		if($CurrentscaleFavPos == 2){
			$CurrentscaleFavPos = 1;
			$Pref::player::customscalefactor = $scaleFavPos3;
			ChatHud.addLine("Your Custom Scale Factor is now " @ $ScaleFavPos3);
			return;
		}
		if($CurrentscaleFavPos == 3){
			$CurrentscaleFavPos = 2;
			$Pref::player::customscalefactor = $scaleFavPos4;
			ChatHud.addLine("Your Custom Scale Factor is now " @ $ScaleFavPos4);
			return;
		}
	}
}
function customscalezplus( %val )
{
	if ( %val ){
		%factor = $Pref::player::customscalefactor;
		commandToServer('customscaleBrick', 0, 0, %factor);
		if ($isBrickScroll == 1)
			{
			schedule( 75, 0, repeatcszplus);
			$csBrickzplus = 1;
			}
		}
	if ( %val == 0 )
		$csBrickzplus = 0;
}
function repeatcszplus()
{
	if( $csBrickzplus == 1 ){
		%factor = $Pref::player::customscalefactor;
		commandToServer('customscaleBrick', 0, 0, %factor);
		schedule( 75, 0, repeatcszplus);
	}
}
function customscalezminus( %val )
{
	if ( %val ){
		%factor = 0 - $Pref::player::customscalefactor;
		commandToServer('customscaleBrick', 0, 0, %factor);
		if ($isBrickScroll == 1)
			{
			schedule( 75, 0, repeatcszminus);
			$csBrickzminus = 1;
			}
		}
	if ( %val == 0 )
		$csBrickzminus = 0;
}
function repeatcszminus()
{
	if( $csBrickzminus == 1 ){
		%factor = 0 - $Pref::player::customscalefactor;
		commandToServer('customscaleBrick', 0, 0, %factor);
		schedule( 75, 0, repeatcszminus);
	}
}
function customscalexplus( %val )
{
	if ( %val ){
		%factor = $Pref::player::customscalefactor;
		commandToServer('customscaleBrick', %factor, 0, 0);
		if ($isBrickScroll == 1)
			{
			schedule( 75, 0, repeatcsxplus);
			$csBrickxplus = 1;
			}
		}
	if ( %val == 0 )
		$csBrickxplus = 0;
}
function repeatcsxplus()
{
	if( $csBrickxplus == 1 ){
		%factor = $Pref::player::customscalefactor;
		commandToServer('customscaleBrick', %factor, 0, 0);
		schedule( 75, 0, repeatcsxplus);
	}
}
function customscalexminus( %val )
{
	if ( %val ){
		%factor = 0 - $Pref::player::customscalefactor;
		commandToServer('customscaleBrick', %factor, 0, 0);
		if ($isBrickScroll == 1)
			{
			schedule( 75, 0, repeatcsxminus);
			$csBrickxminus = 1;
			}
		}
	if ( %val == 0 )
		$csBrickxminus = 0;
}
function repeatcsxminus()
{
	if( $csBrickxminus == 1 ){
		%factor = 0 - $Pref::player::customscalefactor;
		commandToServer('customscaleBrick', %factor, 0, 0);
		schedule( 75, 0, repeatcsxminus);
	}
}
function customscaleyplus( %val )
{
	if ( %val ){
		%factor = $Pref::player::customscalefactor;
		commandToServer('customscaleBrick', 0, %factor, 0);
		if ($isBrickScroll == 1)
			{
			schedule( 75, 0, repeatcsyplus);
			$csBrickyplus = 1;
			}
		}
	if ( %val == 0 )
		$csBrickyplus = 0;
}
function repeatcsyplus()
{
	if( $csBrickyplus == 1 ){
		%factor = $Pref::player::customscalefactor;
		commandToServer('customscaleBrick', 0, %factor, 0);
		schedule( 75, 0, repeatcsyplus);
	}
}
function customscaleyminus( %val )
{
	if ( %val ){
		%factor = 0 - $Pref::player::customscalefactor;
		commandToServer('customscaleBrick', 0, %factor, 0);
		if ($isBrickScroll == 1)
			{
			schedule( 75, 0, repeatcsyminus);
			$csBrickyminus = 1;
			}
		}
	if ( %val == 0 )
		$csBrickyminus = 0;
}
function repeatcsyminus()
{
	if( $csBrickyminus == 1 ){
		%factor = 0 - $Pref::player::customscalefactor;
		commandToServer('customscaleBrick', 0, %factor, 0);
		schedule( 75, 0, repeatcsyminus);
	}
}
function customrotzccw( %val )
{
	if ( %val ){
		%factor = $Pref::player::customrotatedegrees;
		commandToServer('customrotateBrick', 0, 0, %factor);
		if ($isBrickScroll == 1)
			{
			schedule( 75, 0, repeatcrzccw);
			$crBrickzccw = 1;
			}
		}
	if ( %val == 0 )
		$crBrickzccw = 0;
}
function repeatcrzccw()
{
	if( $crBrickzccw == 1 ){
		%factor = $Pref::player::customrotatedegrees;
		commandToServer('customrotateBrick', 0, 0, %factor);
		schedule( 75, 0, repeatcrzccw);
	}
}
function customrotzcw( %val )
{
	if ( %val ){
		%factor = $Pref::player::customrotatedegrees;
		commandToServer('customrotateBrick', 0, 0, -%factor);
		if ($isBrickScroll == 1)
			{
			schedule( 75, 0, repeatcrzcw);
			$crBrickzcw = 1;
			}
		}
	if ( %val == 0 )
		$crBrickzcw = 0;
}
function repeatcrzcw()
{
	if( $crBrickzcw == 1 ){
		%factor = $Pref::player::customrotatedegrees;
		commandToServer('customrotateBrick', 0, 0, -%factor);
		schedule( 75, 0, repeatcrzcw);
	}
}
function customrotxccw( %val )
{
	if ( %val ){
		%factor = $Pref::player::customrotatedegrees;
		commandToServer('customrotateBrick', %factor, 0, 0);
		if ($isBrickScroll == 1)
			{
			schedule( 75, 0, repeatcrxccw);
			$crBrickxccw = 1;
			}
		}
	if ( %val == 0 )
		$crBrickxccw = 0;
}
function repeatcrxccw()
{
	if( $crBrickxccw == 1 ){
		%factor = $Pref::player::customrotatedegrees;
		commandToServer('customrotateBrick', %factor, 0, 0);
		schedule( 75, 0, repeatcrxccw);
	}
}
function customrotxcw( %val )
{
	if ( %val ){
		%factor = $Pref::player::customrotatedegrees;
		commandToServer('customrotateBrick', -%factor, 0, 0);
		if ($isBrickScroll == 1)
			{
			schedule( 75, 0, repeatcrxcw);
			$crBrickxcw = 1;
			}
		}
	if ( %val == 0 )
		$crBrickxcw = 0;
}
function repeatcrxcw()
{
	if( $crBrickxcw == 1 ){
		%factor = $Pref::player::customrotatedegrees;
		commandToServer('customrotateBrick', -%factor, 0, 0);
		schedule( 75, 0, repeatcrxcw);
	}
}
function customrotyccw( %val )
{
	if ( %val ){
		%factor = $Pref::player::customrotatedegrees;
		commandToServer('customrotateBrick', 0, %factor, 0);
		if ($isBrickScroll == 1)
			{
			schedule( 75, 0, repeatcryccw);
			$crBrickyccw = 1;
			}
		}
	if ( %val == 0 )
		$crBrickyccw = 0;
}
function repeatcryccw()
{
	if( $crBrickyccw == 1 ){
		%factor = $Pref::player::customrotatedegrees;
		commandToServer('customrotateBrick', 0, %factor, 0);
		schedule( 75, 0, repeatcryccw);
	}
}
function customrotycw( %val )
{
	if ( %val ){
		%factor = $Pref::player::customrotatedegrees;
		commandToServer('customrotateBrick', 0, -%factor, 0);
		if ($isBrickScroll == 1)
			{
			schedule( 75, 0, repeatcrycw);
			$crBrickycw = 1;
			}
		}
	if ( %val == 0 )
		$crBrickycw = 0;
}
function repeatcrycw()
{
	if( $crBrickycw == 1 ){
		%factor = $Pref::player::customrotatedegrees;
		commandToServer('customrotateBrick', 0, -%factor, 0);
		schedule( 75, 0, repeatcrycw);
	}
}
function custommoveforward( %val )
{
	if ( %val ){
		%factor = $Pref::player::customshiftfactor;
		commandToServer( 'shiftBrick', %factor , 0, 0 );
		if ($isBrickScroll == 1)
			{
			schedule( 75, 0, repeatcmforward);
			$cmBrickforward = 1;
			}
		}
	if ( %val == 0 )
		$cmBrickforward = 0;
}
function repeatcmforward()
{
	if( $cmBrickforward == 1 ){
		%factor = $Pref::player::customshiftfactor;
		commandToServer( 'shiftBrick', %factor , 0, 0 );
		schedule( 75, 0, repeatcmforward);
	}
}
function custommovebackward( %val )
{
	if ( %val ){
		%factor = $Pref::player::customshiftfactor;
		commandToServer( 'shiftBrick', -1 * %factor , 0, 0 );
		if ($isBrickScroll == 1)
			{
			schedule( 75, 0, repeatcmbackward);
			$cmBrickbackward = 1;
			}
		}
	if ( %val == 0 )
		$cmBrickbackward = 0;
}
function repeatcmbackward()
{
	if( $cmBrickbackward == 1 ){
		%factor = $Pref::player::customshiftfactor;
		commandToServer( 'shiftBrick', -1 * %factor , 0, 0 );
		schedule( 75, 0, repeatcmbackward);
	}
}
function custommoveleft( %val )
{
	if ( %val ){
		%factor = $Pref::player::customshiftfactor;
		commandToServer( 'shiftBrick', 0, %factor , 0 );
		if ($isBrickScroll == 1)
			{
			schedule( 75, 0, repeatcmleft);
			$cmBrickleft = 1;
			}
		}
	if ( %val == 0 )
		$cmBrickleft = 0;
}
function repeatcmleft()
{
	if( $cmBrickleft == 1 ){
		%factor = $Pref::player::customshiftfactor;
		commandToServer( 'shiftBrick', 0, %factor , 0 );
		schedule( 75, 0, repeatcmleft);
	}
}
function custommoveright( %val )
{
	if ( %val ){
		%factor = $Pref::player::customshiftfactor;
		commandToServer( 'shiftBrick', 0, -1 * %factor , 0 );
		if ($isBrickScroll == 1)
			{
			schedule( 75, 0, repeatcmright);
			$cmBrickright = 1;
			}
		}
	if ( %val == 0 )
		$cmBrickright = 0;
}
function repeatcmright()
{
	if( $cmBrickright == 1 ){
		%factor = $Pref::player::customshiftfactor;
		commandToServer( 'shiftBrick', 0, -1 * %factor , 0 );
		schedule( 75, 0, repeatcmright);
	}
}
function custommoveup( %val )
{
	if ( %val ){
		%factor = $Pref::player::customshiftfactor;
		commandToServer( 'shiftBrick', 0, 0, 3 * %factor );
		if ($isBrickScroll == 1)
			{
			schedule( 75, 0, repeatcmup);
			$cmBrickup = 1;
			}
		}
	if ( %val == 0 )
		$cmBrickup = 0;
}
function repeatcmup()
{
	if( $cmBrickup == 1 ){
		%factor = $Pref::player::customshiftfactor;
		commandToServer( 'shiftBrick', 0, 0, 3 * %factor );
		schedule( 75, 0, repeatcmup);
	}
}
function custommovedown( %val )
{
	if ( %val ){
		%factor = $Pref::player::customshiftfactor;
		commandToServer( 'shiftBrick', 0, 0, -3 * %factor );
		if ($isBrickScroll == 1)
			{
			schedule( 75, 0, repeatcmdown);
			$cmBrickdown = 1;
			}
		}
	if ( %val == 0 )
		$cmBrickdown = 0;
}
function repeatcmdown()
{
	if( $cmBrickdown == 1 ){
		%factor = $Pref::player::customshiftfactor;
		commandToServer( 'shiftBrick', 0, 0, -3 * %factor );
		schedule( 75, 0, repeatcmdown);
	}
}
function custommoveupsmall( %val )
{
	if ( %val ){
		%factor = $Pref::player::customshiftfactor;
		commandToServer( 'shiftBrick', 0, 0, %factor );
		if ($isBrickScroll == 1)
			{
			schedule( 75, 0, repeatcmupsmall);
			$cmBrickupsmall = 1;
			}
		}
	if ( %val == 0 )
		$cmBrickupsmall = 0;
}
function repeatcmupsmall()
{
	if( $cmBrickupsmall == 1 ){
		%factor = $Pref::player::customshiftfactor;
		commandToServer( 'shiftBrick', 0, 0, %factor );
		schedule( 75, 0, repeatcmupsmall);
	}
}
function custommovedownsmall( %val )
{
	if ( %val ){
		%factor = $Pref::player::customshiftfactor;
		commandToServer( 'shiftBrick', 0, 0, -%factor );
		if ($isBrickScroll == 1)
			{
			schedule( 75, 0, repeatcmdownsmall);
			$cmBrickdownsmall = 1;
			}
		}
	if ( %val == 0 )
		$cmBrickdownsmall = 0;
}
function repeatcmdownsmall()
{
	if( $cmBrickdownsmall == 1 ){
		%factor = $Pref::player::customshiftfactor;
		commandToServer( 'shiftBrick', 0, 0, -%factor );
		schedule( 75, 0, repeatcmdownsmall);
	}
}
function openfactorset( %val )
{
	if ( %val ){
		if($factorsetguiOpen $= 1)
		   canvas.popDialog(factorsetgui);
		else
		   canvas.pushDialog(factorsetgui);
	}
}
//What TBM calls noob protect
$factorsetguiOpen = 0;
function factorsetgui::onWake(%this)
{
	$factorsetguiOpen = 1;
	commandtoserver('rotcompatibility');
	txtshiftfactor.setvalue($pref::player::customshiftfactor);
	txtrotdegrees.setvalue($pref::player::customrotatedegrees);
	txtscalefactor.setvalue($pref::player::customscalefactor);
	txtshiftfav1.setvalue($pref::player::customshiftshiftfav1);
	txtshiftfav2.setvalue($pref::player::customshiftshiftfav2);
	txtshiftfav3.setvalue($pref::player::customshiftshiftfav3);
	txtshiftfav4.setvalue($pref::player::customshiftshiftfav4);
	txtrotfav1.setvalue($pref::player::customshiftrotfav1);
	txtrotfav2.setvalue($pref::player::customshiftrotfav2);
	txtrotfav3.setvalue($pref::player::customshiftrotfav3);
	txtrotfav4.setvalue($pref::player::customshiftrotfav4);
	txtscalefav1.setvalue($pref::player::customshiftscalefav1);
	txtscalefav2.setvalue($pref::player::customshiftscalefav2);
	txtscalefav3.setvalue($pref::player::customshiftscalefav3);
	txtscalefav4.setvalue($pref::player::customshiftscalefav4);
	txtcompatibility.setvalue("This server is only compatible with shifting.");
}
function factorsetgui::onSleep(%this)
{
	$factorsetguiOpen = 0;
	$pref::player::customshiftshiftfav1 = txtshiftfav1.getvalue();
	$pref::player::customshiftshiftfav2 = txtshiftfav2.getvalue();
	$pref::player::customshiftshiftfav3 = txtshiftfav3.getvalue();
	$pref::player::customshiftshiftfav4 = txtshiftfav4.getvalue();
	$pref::player::customshiftrotfav1 = txtrotfav1.getvalue();
	$pref::player::customshiftrotfav2 = txtrotfav2.getvalue();
	$pref::player::customshiftrotfav3 = txtrotfav3.getvalue();
	$pref::player::customshiftrotfav4 = txtrotfav4.getvalue();
	$pref::player::customshiftscalefav1 = txtscalefav1.getvalue();
	$pref::player::customshiftscalefav2 = txtscalefav2.getvalue();
	$pref::player::customshiftscalefav3 = txtscalefav3.getvalue();
	$pref::player::customshiftscalefav4 = txtscalefav4.getvalue();
	%txtshiftfactor = txtshiftfactor.getvalue();
	if( %txtshiftfactor < 0 ){
		MessageBoxOK( "ERROR!", "Your shifting factor is less than 0.  It has not been saved." );
	}
	if( %txtshiftfactor > 0 ){
		$pref::player::customshiftfactor = txtshiftfactor.getvalue();
	}
	%txtrotfactor = txtrotdegrees.getvalue();
	if( %txtrotfactor < 0 ){
		MessageBoxOK( "ERROR!", "Your rotating factor is less than 0.  It has not been saved." );
	}
	if( %txtrotfactor > 0 ){
		$pref::player::customrotatedegrees = txtrotdegrees.getvalue();
	}
	%txtscalefactor = txtscalefactor.getvalue();
	if( %txtscalefactor >= 250.001 ){
		MessageBoxOK( "ERROR!", "Your scale factor is greater than 250.  It has not been saved." );
	}
	if( %txtscalefactor < 0 ){
		MessageBoxOK( "ERROR!", "Your scale factor is less than 0.  It has not been saved." );
	}
	if( %txtscalefactor <= 250.001 && %txtscalefactor > 0){
		$pref::player::customscalefactor = txtscalefactor.getvalue();
	}
}
function clientcmdrotiscompatible(%really)
{
	if(%really == 1)
	{
	txtcompatibility.setvalue("This server is incompatible with scaling.");
	}
	if(%really == 0)
	{
	txtcompatibility.setvalue("This server has only shifting enabled.");
	}
	if(%really == 2)
	{
	txtcompatibility.setvalue("This server is compatible with everything.");
	}
}
//Now for the server stuff.
$customadvanced = 0;
function toggleadvanced()
{
	if( $customadvanced == 1 )
	{
	$customadvanced = 0;
	factorwindow.resize(200, 175, 250, 125);
	}
	else
	{
	$customadvanced = 1;
	factorwindow.resize(200, 175, 250, 225);
	}
}
//END!