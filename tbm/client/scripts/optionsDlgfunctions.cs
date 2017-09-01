//New functions

//////////////////////
//Use slot functions//
//////////////////////

function useFirstSlot( %val )
{
	if ( %val) {
        if($invtog == 1)
		  commandToServer('useInventory', 0);
        else
        commandToServer('useInventory', 10);
    }
}
function useSecondSlot( %val )
{
	if ( %val) {
        if($invtog == 1)
		  commandToServer('useInventory', 1);
        else
        commandToServer('useInventory', 11);
    }
}
function useThirdSlot( %val )
{
	if ( %val) {
        if($invtog == 1)
		  commandToServer('useInventory', 2);
        else
        commandToServer('useInventory', 12);
    }
}
function useFourthSlot( %val )
{
	if ( %val) {
        if($invtog == 1)
		  commandToServer('useInventory', 3);
        else
        commandToServer('useInventory', 13);
    }
}
function useFifthSlot( %val )
{
	if ( %val) {
        if($invtog == 1)
		  commandToServer('useInventory', 4);
        else
        commandToServer('useInventory', 14);
    }
}
function useSixthSlot( %val )
{
	if ( %val) {
        if($invtog == 1)
		  commandToServer('useInventory', 5);
        else
        commandToServer('useInventory', 15);
    }
}
function useSeventhSlot( %val )
{
	if ( %val) {
        if($invtog == 1)
		  commandToServer('useInventory', 6);
        else
        commandToServer('useInventory', 16);
    }
}
function useEighthSlot( %val )
{
	if ( %val) {
        if($invtog == 1)
		  commandToServer('useInventory', 7);
        else
        commandToServer('useInventory', 17);
    }
}
function useNinthSlot( %val )
{
	if ( %val) {
        if($invtog == 1)
		  commandToServer('useInventory', 8);
        else
        commandToServer('useInventory', 18);
    }
}
function useTenthSlot( %val )
{
	if ( %val) {
        if($invtog == 1)
		  commandToServer('useInventory', 9);
        else
        commandToServer('useInventory', 19);
    }
}


//////////////////////
//Drop slot functions//
//////////////////////
function dropFirstSlot( %val )
{
	if ( %val) {
        if($invtog == 1)
		commandToServer('dropInventory', 0);
        else
        commandToServer('dropInventory',10);
  }
}
function dropSecondSlot( %val )
{
	if ( %val) {
        if($invtog == 1)
		commandToServer('dropInventory', 1);
        else
        commandToServer('dropInventory',11);
  }
}
function dropThirdSlot( %val )
{
	if ( %val) {
        if($invtog == 1)
		commandToServer('dropInventory', 2);
        else
        commandToServer('dropInventory',12);
  }
}
function dropFourthSlot( %val )
{
	if ( %val) {
        if($invtog == 1)
		commandToServer('dropInventory', 3);
        else
        commandToServer('dropInventory',13);
  }
}
function dropFifthSlot( %val )
{
	if ( %val) {
        if($invtog == 1)
		commandToServer('dropInventory', 4);
        else
        commandToServer('dropInventory',14);
  }
}
function dropSixthSlot( %val )
{
	if ( %val) {
        if($invtog == 1)
		commandToServer('dropInventory', 5);
        else
        commandToServer('dropInventory',15);
  }
}
function dropSeventhSlot( %val )
{
	if ( %val) {
        if($invtog == 1)
		commandToServer('dropInventory', 6);
        else
        commandToServer('dropInventory',16);
  }
}
function dropEighthSlot( %val )
{
	if ( %val) {
        if($invtog == 1)
		commandToServer('dropInventory', 7);
        else
        commandToServer('dropInventory',17);
  }
}
function dropNinthSlot( %val )
{
	if ( %val) {
        if($invtog == 1)
		commandToServer('dropInventory', 8);
        else
        commandToServer('dropInventory',18);
  }
}
function dropTenthSlot( %val )
{
	if ( %val) {
        if($invtog == 1)
		commandToServer('dropInventory', 9);
        else
        commandToServer('dropInventory',19);
  }
}
function igobcut( %val ) {if ( %val ) openfile("iGob_Clipboard",1);}
function igobcopy( %val ) {if ( %val ) openfile("iGob_Clipboard");}
function igobpaste( %val ) {if ( %val ) iGob_load("iGob_Clipboard");}
function toggleigob( %val ) {if ( %val ) commandToServer('toggleigob');}
function shiftBrickAway( %val ) {if ( %val ) commandToServer('shiftBrick', 1, 0, 0);}
function sshiftBrickAway( %val ) {if ( %val ) commandToServer('shiftBrick', 10, 0, 0);}
function shiftBrickTowards( %val ) {if ( %val ) commandToServer('shiftBrick', -1, 0, 0);}
function sshiftBrickTowards( %val ) {if ( %val ) commandToServer('shiftBrick', -10, 0, 0);}
function shiftBrickLeft( %val ) {if ( %val ) commandToServer('shiftBrick', 0, 1, 0);}
function sshiftBrickLeft( %val ) {if ( %val ) commandToServer('shiftBrick', 0, 10, 0);}
function shiftBrickRight( %val ) {if ( %val ) commandToServer('shiftBrick', 0, -1, 0);}
function sshiftBrickRight( %val ) {if ( %val ) commandToServer('shiftBrick', 0, -10, 0);}
function shiftBrickUp( %val ) {if ( %val ) commandToServer('shiftBrick', 0, 0, 3);}
function sshiftBrickUp( %val ) {if ( %val ) commandToServer('shiftBrick', 0, 0, 30);}
function shiftBrickDown( %val ) {if ( %val ) commandToServer('shiftBrick', 0, 0, -3);}
function sshiftBrickDown( %val ) {if ( %val ) commandToServer('shiftBrick', 0, 0, -30);}
function shiftBrickThirdUp( %val ) {if ( %val )	commandToServer('shiftBrick', 0, 0, 1);}
function shiftBrickThirdDown( %val ) {if ( %val ) commandToServer('shiftBrick', 0, 0, -1);}


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
function switchinv ( %val )
{
	if ( %val )
		toginv();
}
$AdminGuiTog = 0; //Noob protect
function admingui::OnWake() {
$AdminGuiTog = 1;
}
function admingui::OnSleep() {
$AdminGuiTog = 0;
}
function openAdminWindow (%val)
{
	if(%val && $AdminGuiTog == 0)	
		canvas.pushDialog(admingui);
	else if(%val && $AdminGuiTog == 1)
		canvas.popDialog(admingui);
}

function openOptionsWindow (%val)
{
	if(%val && $OptionsGuiTog == 0)	
		canvas.pushDialog(optionsDlg);
	else if( %val && $OptionsGuiTog == 1)
		canvas.popDialog(optionsDlg);
}

function jet(%val)
{
	if(%val)
		$mvTriggerCount3++;
}
function brickparseup(%val)
{
	if(%val)
		commandToServer('BrickParse', 1);
}
function brickparsedown(%val)
{
	if(%val)
		commandToServer('BrickParse', -1);
}
function handleMouseWheel(%val)
{
if(QuickInventoryGui.isAwake())
return;
if( $mousewheelcontrol == 0 )
{
   // Check if the wheel scrolled up or down
   if( %val > 0 )
         commandToServer('brickparse', 1); //Mwheel Up
   else
         commandToServer('brickparse', -1);//Mwheel Down
}
else
{
if( %val > 0 )
	commandToServer('mouseshiftinv', 1); //Shift up
else
	commandToServer('mouseshiftinv', -1); //Shift down
}
}

function handleSuperMouseWheel(%val)
{
if(QuickInventoryGui.isAwake())
return;
if( $mousewheelcontrol == 0 )
{
   // Check if the wheel scrolled up or down
   if( %val > 0 )
         commandToServer('brickparse', 5); //Mwheel Up
   else
         commandToServer('brickparse', -5);//Mwheel Down
}
else
{
if( %val > 0 )
	commandToServer('mouseshiftinv', 5); //Shift up
else
	commandToServer('mouseshiftinv', -5); //Shift down
}
}

function togham(%val) { if (%val) commandToServer('ToggleHammer'); }
function togsc(%val) { if (%val) commandToServer('ToggleSprayCan'); }
function togeyed(%val) { if (%val) commandToServer('toggleeyedropper'); }
function togscan(%val) { if (%val) commandToServer('togglescanner'); }
function colorpl(%val) { if (%val) ColorPalletGui.toggle(); $colorsetting=0; }
function scaleup(%val) { if (%val) commandToServer('ShiftScale', 1); }
function sscaleup(%val) { if (%val) commandToServer('ShiftScale', 10); }
function scaledown(%val) { if (%val) commandToServer('ShiftScale', -1); }
function sscaledown(%val) { if (%val) commandToServer('ShiftScale', -10); }
function togedit(%val) { if (%val) commandToServer('EditorMode'); }
function toggunedit(%val) { if (%val) commandToServer('EditorGunMode'); }
function cspy(%val) { if (%val) commandToServer('spy',0); }
function cspy1(%val) { if (%val) commandToServer('spy',1); }
function deleteselected(%val) { if (%val) commandToServer('adjustobj',100); }
function emoteQuestion(%val) { if (%val) commandToServer('EmoteQuestion'); }
function emoteExclaim(%val) { if (%val) commandToServer('EmoteExclaim'); }
function emoteLove(%val) { if (%val) commandToServer('EmoteLove'); }
function emoteAFK(%val) { if (%val) commandToServer('EmoteAFK'); }

$hudson = 1;
function ToggleHuds (%val)
{
	if(%val && $hudson == 0)
		screenHuds(1);
	else if(%val && $hudson == 1)
		screenHuds(0);
}
function screenHuds(%x)
{
if($SSmode $= "showHuds")
return;
      OuterChatHud.setVisible(%x);

      //HudClock.setVisible(%x);
      StatusBars.setVisible(%x);
      HudShapeName.setVisible(%x);
      if($Pref::Gui::ShowFPS) { FPS_HUD.setVisible(%x); FPS_txt.setVisible(%x); }
      HudCrosshair.setVisible(%x);
      Slot1BG.setVisible(%x);
      Slot2BG.setVisible(%x);
      Slot3BG.setVisible(%x);
      Slot4BG.setVisible(%x);
      Slot5BG.setVisible(%x);
      Slot6BG.setVisible(%x);
      Slot7BG.setVisible(%x);
      Slot8BG.setVisible(%x);
      Slot9BG.setVisible(%x);
      if($invtog == 1)
      inv1.setVisible(%x);
      else
      inv2.setVisible(%x);
      Slot10BG.setVisible(%x);
      Watermark.setVisible(!%x);
      StudCounter_HUD.setvisible(%x);
      $hudson = %x;
}

function clientCmdupdatecrosshair()
{
   HudCrosshair.setBitmap("tbm/client/ui/crosshairs/"@$Crosshair[$pref::Crosshair]);
}
$staticZoomvar = 1; //setting it for always defaulting ( noob protect )
function staticzoom(%val)
{
	if(%val)
	{
		switch$ ($staticZoomvar)
		{
			case 1:
				setFOV(60);
				$staticZoomvar = 2;
			case 2:
				setFOV(30);
				$staticZoomvar = 3;
			case 3:
				setFOV(0);
				$staticZoomvar = 4;
			case 4:
				setFOV(90);
				$staticZoomvar = 1;
		}
	}
}
$mousewheelcontrol = 0; //defaulting for TBM inventory
function mousewheelcontrol()
{
if($mousewheelcontrol == 0)
{
	$mousewheelcontrol = 1;
	commandToServer('mousewheelcontroltext', 1);
}
else
{
	$mousewheelcontrol = 0;
	commandToServer('mousewheelcontroltext', 0);
}
}

$ColorGuiTog = 0; //Noob Protect
function SpecialOpGui(%val) 
{
	if(%val) {
		if(SwitchAddActionGUI.isAwake())
			canvas.popDialog(SwitchAddActionGUI);
		else if(SwitchActionGUI.isAwake())
			canvas.popDialog(SwitchActionGUI);
		else
			SpecialOpGUI.toggle();
	}
}
$TBMPlayeropGuiTog = 0; //Noob Protect
function TBMplayeropGUI(%val) 
{
	if(%val && $TBMPlayeropGuiTog == 0)     
		canvas.pushDialog("TBMPlayerOpGui");
	else if(%val && $TBMPlayeropGuiTog == 1)
		canvas.popDialog("TBMPlayerOpGui");
}

function getinbuggy(%val) 
{
	if(%val)
		commandToServer('MountVehicle'); 
}
function screenshotdelay(%x)
{
	$screenshotdelay = %x;
}



function ClientCmdsetFavBrick(%brick, %slot)
{
	$pref::favbrick[%slot] = %brick;
}

function saveFavBrick(%slot)
{
	commandToServer('validfavbrick', %slot);
}

function ServerCmdsetSign(%client, %text)
{
	%client.signtext = %text;
}

function testfordecal()
{
	for (%filename = findFirstFile("tbm/data/shapes/player/*.jpg"); %filename !$= ""; %filename = findNextFile("tbm/data/shapes/player/*.jpg"))
		if (strstr(%filename, "decal.jpg") != -1)
			return true;
}
$screenshotdelay = 500;

//PIG:added a simple load check
if ($pref::Video::screenshotNumber < 0)
{
   $pref::Video::screenshotNumber = 0;
}

function doScreenShot( %val )
{
   if (%val)
   {
      $pref::interior::showdetailmaps = false;
      //nameToID(WaterMark).schedule(0, setVisible, 1);
      //nameToID(hudshapename).schedule(100, setVisible, 1);
      if($pref::Video::screenShotFormat $= "JPEG")
      {
        %jpgimage = ("tbm/screenshots/pic_"@formatImageNumber($pref::Video::screenshotNumber++) @ ".jpg");
        schedule($screenshotdelay * 1, 0, screenShot, %jpgimage, "JPEG");
      }
      else
      {
        %pngimage = ("tbm/screenshots/pic_"@formatImageNumber($pref::Video::screenshotNumber++) @ ".png");
        schedule($screenshotdelay * 1, 0, screenShot, %pngimage, "PNG");
      }
//schedule($screenshotdelay * 2, 0, screenHuds, 1);
//Real thing here

//nameToID(WaterMark).schedule($screenshotdelay * 2, setVisible, 0);
//DShiznit- Added simple prefs exporter to screenshot function.
//This should prevent screenshot overwriting after crashing. 
//
//I'm an idiot. I initially had 
//"./client/prefs.cs" instead of "tbm/client/prefs.cs" below. 
//
echo("Exporting client prefs");
export("$pref::*", "tbm/client/prefs.cs", False);
   }
}


// bind key to take screenshots
//GlobalActionMap.bind(keyboard, "ctrl p", doScreenShot);

//***********************************[Function SetNetPrefs]***************************************
//*                                                                                              *
//* This function allows for preset connection speeds, for users who may not know how to set     *
//* up their Net prefs.                                                                          *
//* Also, the available choices are added before the function.                                   *
//*                                                                                              *
//************************************************************************************************
NetPopupmenu.add("56k",0);
NetPopupmenu.add("DSL",1);
NetPopupmenu.add("Cable",2);
NetPopupmenu.add("LAN",3);
NetPopupmenu.setselected($Pref::Net::ConnectionSpeed);
function setNetprefs() {
    $Pref::Net::ConnectionSpeed = NetPopupmenu.getselected();
    switch$($Pref::Net::ConnectionSpeed) {
    case "0": // 56.6K Modem
        $Pref::Net::PacketRateToClient = 16;
        $Pref::Net::PacketRateToServer = 20;
        $Pref::Net::PacketSize = 240;
    case "1": // DSL/ADSL Connection
        $Pref::Net::PacketRateToClient = 20;
        $Pref::Net::PacketRateToServer = 24;
        $Pref::Net::PacketSize = 350;
    case "2": // Cable Connection
        $Pref::Net::PacketRateToClient = 24;
        $Pref::Net::PacketRateToServer = 24;
        $Pref::Net::PacketSize = 400;
    case "3": // T1/LAN Connection (Maximums)
        $Pref::Net::PacketRateToClient = 32;
        $Pref::Net::PacketRateToServer = 32;
        $Pref::Net::PacketSize = 450;
    }
    //set the new values on the sliders
    SliderRateToServer.setValue($Pref::Net::PacketRateToServer);
    SliderRateToClient.setValue($Pref::Net::PacketRateToClient);
    SliderPacketSize.setvalue($Pref::Net::PacketSize);
}
//*****************************[Funcion handlesavedplayer]************************************
//*                                                                                          *
//* Funcion handlesavedplayer is used to both save and load a saved character.               *
//* It is called whenever a user hits the save button located in optionsdlg.gui or when      *
//* they use the popup menu "Savedcharpopup" located in the same file                        *
//*                                                                                          *
//********************************************************************************************
function handlesavedplayer(%var)
{
    switch$(%var)
    {
        case "save":
            $savingplayer = true;
            Canvas.popdialog(Optionsdlg);
            %name = TxtPlayerName.getValue();
            %savename = %name;
            %realsavename = %name;
            %savename = parsestring(%savename);
            %chestdecal = strreplace($pref::Decal::chest,"-","_9");
            %facedecal = strreplace($pref::Decal::face,"-","_9");
            $SavedCharacter_[%savename] ="setClientPrefs("@%savename@","@$pref::Color::skin@","@$pref::Accessory::headCode@","@$pref::Accessory::visorCode@","@$pref::Accessory::backCode@","@$pref::Accessory::leftHandCode@","@$pref::Accessory::headColor@","@$pref::Accessory::visorColor@","@$pref::Accessory::backColor@","@$pref::Accessory::leftHandColor@","@%chestdecal@","@%facedecal@","@$pref::Accessory::Armscolor@");";
            $SavedCharacter_Name_[%savename] = %realsavename;
            autoaddplayer(%savename);
            echo("Character has been saved ==> " @ %savename);
            Canvas.pushdialog(Optionsdlg);
            $savingplayer = false;
        case "load":
            %name2 = $SavedCharname[Savedcharpopup.getSelected()];
            %name2 = strreplace(%name2," ","");
            if($SavedCharacter_[%name2] !$= "") {
            Canvas.popdialog(Optionsdlg);
            eval($SavedCharacter_[%name2]); //setclientprefs(...); called here
            Canvas.pushdialog(Optionsdlg);
            echo("Saved Character has been loaded ==> " @ %name2);
            }
            else error("Saved Character does not exist!");
    }
}
//******************************[Funcion setClientprefs]**************************************
//*                                                                                          *
//* Funcion setClientprefs is used as a transition from the client's saved prefs             *
//* to the server.                                                                           *
//*                                                                                          *
//********************************************************************************************
function setClientprefs(%savename,%skincolor,%headcode,%visorcode,%backcode,%lefthandcode,%headcolor,%visorcolor,%backcolor,%lefthandcolor,%chestdecal,%facedecal,%armsncrotch)
{
   %chestdecal = strreplace(%chestdecal, "_9","-");
   %facedecal = strreplace(%facedecal, "_9","-");
    if($Pref::Player::OverWriteName == true)
    $pref::Player::Name = $SavedCharacter_Name_[%savename];
    $pref::Color::skin = %skincolor;
    $pref::Accessory::headCode = %headcode;
    $pref::Accessory::visorCode = %visorcode;
    $pref::Accessory::backCode = %backcode;
    $pref::Accessory::leftHandCode = %lefthandcode;
    $pref::Accessory::headColor = %headcolor;
    $pref::Accessory::visorColor = %visorcolor;
    $pref::Accessory::backColor = %backcolor;
    $pref::Accessory::leftHandColor = %lefthandcolor;
    $pref::Decal::chest = %chestdecal;
    $pref::Decal::face = %facedecal;
    $pref::Accessory::Armscolor = %armsncrotch;
    clientcmdupdateprefs();
}
$Savecharline = 0;
//******************************[Funcion loadprofilelist]*************************************
//*                                                                                          *
//* Funcion loadprofilelist loads all the user's saved character profiles                    *
//*                                                                                          *
//********************************************************************************************
function loadprofilelist()
{
    Savedcharpopup.clear();
    %file = new FileObject();
    %file.openForRead("TBM/client/savedcharacters.cs");
    %line = 0;
    while( !%file.isEOF() )
    {
        %text = %file.readLine();
        %text = disectFileName(%text," ",0);
        if(getSubStr(%text,0,21) $= "$SavedCharacter_Name_")
        {
        %textname = strreplace(%text,"$SavedCharacter_Name_","");
        Savedcharpopup.add(%textname, $Savecharline);
        $SavedCharname[$Savecharline] = %textname;
        $Savecharline++;
        }
    }
    %file.close();
    %file.delete();
}
loadprofilelist();
//*****************************[Funcion deletesavedplayer]****************************************
//*                                                                                              *
//* Funcion deletesavedplayer will delete the user's currently selected profile. However, it is  *
//* not working yet.                                                                             *
//*                                                                                              *
//************************************************************************************************
function deletesavedplayer() {
%charname = $SavedCharname[Savedcharpopup.getSelected()];
echo(%charname);
cancel($SavedCharacter_Name_[%charname]);
cancel($SavedCharacter_[%charname]);
Savedcharpopup.remove(Savedcharpopup.getSelected());
}

//*********************************[Funcion autoaddplayer]****************************************
//*                                                                                              *
//* Funcion autoaddplayer automatically adds a newly saved profile in the popupmenu for quick    *
//* and easy access to new saved characters.                                                     *
//*                                                                                              *
//************************************************************************************************
function autoaddplayer(%savename) {
    for(%i=0;%i<$Savecharline; %i++) {
        if($SavedCharname[%i] $= %savename)
            %diemfer = "die";
    }
    if(%diemfer !$= "die") {
    Savedcharpopup.add(%savename, $Savecharline++);
    $SavedCharname[$Savecharline] = %savename;
    }
}

//************************************[Funcion parsestring]***************************************
//*                                                                                              *
//* Funcion parsestring is used to clean a string of all bad characters. It is used mainly in    *
//* the function handlesavedplayer(); located in optionsdlgfunctions.cs                          *
//*                                                                                              *
//************************************************************************************************
function parsestring(%word) {
%word = strreplace(%word," ", "");
%word = strreplace(%word,"`", "");
%word = strreplace(%word,"~", "");
%word = strreplace(%word,"!", "");
%word = strreplace(%word,"@", "");
%word = strreplace(%word,"#", "");
%word = strreplace(%word,"$", "");
%word = strreplace(%word,"%", "");
%word = strreplace(%word,"^", "");
%word = strreplace(%word,"&", "");
%word = strreplace(%word,"*", "");
%word = strreplace(%word,"(", "");
%word = strreplace(%word,")", "");
%word = strreplace(%word,"-", "");
%word = strreplace(%word,"_", "");
%word = strreplace(%word,"=", "");
%word = strreplace(%word,"+", "");
%word = strreplace(%word,"[", "");
%word = strreplace(%word,"]", "");
%word = strreplace(%word,"{", "");
%word = strreplace(%word,"}", "");
%word = strreplace(%word,"|", "");
%word = strreplace(%word,";", "");
%word = strreplace(%word,":", "");
%word = strreplace(%word,",", "");
%word = strreplace(%word,"<", "");
%word = strreplace(%word,".", "");
%word = strreplace(%word,">", "");
%word = strreplace(%word,"?", "");
return %word;
}

//**************************************[Cursor Scripts]******************************************
//*                                                                                              *
//* Code seen below here adds the cursors to a popupmenu, and also a function to automatically   *
//* change the cursor once a new one is selected                                                 *
//*                                                                                              *
//************************************************************************************************
$CurTot = -1;
$Cursor[$Curtot++] = "MacCursor";
$Cursor[$Curtot++] = "PcCursor";
$Cursor[$Curtot++] = "BlueCursor";
$Cursor[$Curtot++] = "MacCursorTrans";
$Cursor[$Curtot++] = "BluePurpleCursor";
$Cursor[$Curtot++] = "GreenPurpleCursor";
$Cursor[$Curtot++] = "TransRedBlueCursor";
$Cursor[$Curtot++] = "YellowCursor";
OptWindowCursorMenu.add("Mac", 0);
OptWindowCursorMenu.add("Pc", 1);
OptWindowCursorMenu.add("Blue", 2);
OptWindowCursorMenu.add("Mac Trans", 3);
OptWindowCursorMenu.add("BluePurple", 4);
OptWindowCursorMenu.add("GreenPurple", 5);
OptWindowCursorMenu.add("TransRedBlue", 6);
OptWindowCursorMenu.add("Yellow", 7);
OptWindowCursorMenu.setSelected($Pref::Gui::Cursor);
function setCursorStyle() {
   $Pref::Gui::Cursor = OptWindowCursorMenu.getSelected();
   if($Cursor[$Pref::Gui::Cursor] < 0)
   return;
   Canvas.setCursor($Cursor[$Pref::Gui::Cursor]);
   OptWindowCursorMenu.setSelected($Pref::Gui::Cursor);
}

function toginv() {
if(inv1.isVisible()) {
%vis1 = 0;
%vis2 = 1;
} else {
%vis1 = 1;
%vis2 = 0;
}
inv1.setVisible(%vis1);
inv2.setVisible(%vis2);
$invtog = %vis2++;
}