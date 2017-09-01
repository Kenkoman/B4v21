package Sniper_Overlay {
function toggleZoom(%val) {
sniperoverlay_activate(0);
Parent::togglezoom(%val);
}

function optionsDlg::applyGraphics(%this) {
Parent::applyGraphics(%this);
schedule(100,0,updatesniperoverlay);
}

function staticzoom(%val) {
if(%val) {
 switch$ ($staticZoomvar) {
  case 3:
  if($Pref::GUI::ManualSniperScopeEnabled)
  sniperoverlay_activate(1);
  case 4:
  sniperoverlay_activate(0);
 }
}
Parent::Staticzoom(%val);
}

function ToggleHuds (%val)
{
if(%val && $sniperoverlay) {
 $Pref::GUI::SniperChatHUD = !$Pref::GUI::SniperChatHUD;
 screenhuds(0);
 return;
}
Parent::ToggleHUDS(%val);
}

function screenhuds(%x) {
Parent::screenhuds(%x);
 if(%x == 0 && !$SniperOverlay)
 Watermark.setVisible(1);
 else
 Watermark.setvisible(0);
 StudCounter_txt.setvisible(%x);
if($sniperoverlay)
      OuterChatHud.setVisible($Pref::GUI::SniperChatHUD);
}

function doScreenShot( %val ) {
if (%val) {
 $pref::interior::showdetailmaps = false;
 if(!$sniperoverlay) schedule(0, 0, screenHuds, 0);
 if($pref::Video::screenShotFormat $= "JPEG") {
  %jpgimage = ("tbm/screenshots/pic_"@formatImageNumber($pref::Video::screenshotNumber++) @ ".jpg");
  schedule($screenshotdelay * 1, 0, screenShot, %jpgimage, "JPEG");
 }
 else {
  %pngimage = ("tbm/screenshots/pic_"@formatImageNumber($pref::Video::screenshotNumber++) @ ".png");
  schedule($screenshotdelay * 1, 0, screenShot, %pngimage, "PNG");
 }
 schedule($screenshotdelay * 2, 0, screenHuds, 1);
 echo("Exporting client prefs");
 export("$pref::*", "tbm/client/prefs.cs", False);
}
}

function getsniperoverlayfile() {
if($Pref::GUI::SniperOverlay $= "")
 $Pref::GUI::SniperOverlay = 0;
//%a = mfloor($Pref::GUI::SniperOverlay/10);
//%b = $Pref::GUI::SniperOverlay - 10*mfloor($Pref::GUI::SniperOverlay/10);
//return "tbm/client/ui/Sniper Overlays/sniperoverlay_"@ %a @ %b @".png";
return "tbm/client/ui/Sniper Overlays/"@ $sniperfile[$Pref::GUI::SniperOverlay] @".png";
}

function updatesniperoverlay() {
if(nametoid(Sniper_Overlay) == -1) {
 echo("O_o The sniper overlay doesn't exist.  It looks like you're screwed.");
 return;
}
nametoid(Sniper_Overlay).extent = getwords($pref::Video::resolution,0,1);
nametoid(Sniper_Overlay).bitmap = getsniperoverlayfile();
if($sniperoverlay) {
 GuiGroup.add(nametoid(PlayGUI));
 Canvas.setContent(nametoid(PlayGUI));
}
nametoid(Sniper_Overlay).setvisible($SniperOverlay);
}

function sniperoverlay_activate(%val) {
if(!$Pref::GUI::SniperEnable) return;
if(%val == 2) {
 $Sniperoverlay = !$Sniperoverlay;
 if($Sniperoverlay == 1) {
  $staticzoomvar = 3;
  staticzoom(1);
  }
  else {
  $staticzoomvar = 4;
  staticzoom(1);
 }
  screenhuds(!$SniperOverlay);
  updatesniperoverlay();
  return;
}

if($SniperOverlay == %val) return;
$SniperOverlay = %val;
screenhuds(!%val);
updatesniperoverlay();
}

};
activatepackage(Sniper_Overlay);

function clientcmdsniperscope(%val) {
//0 = off, 1 = on, 2 = toggle
SniperOverlay_Activate(%val);
}



//////////GUI stuff//////////

//--- OBJECT WRITE BEGIN ---
new GuiControl(Sniperselectorgui) {
   profile = "GuiDefaultProfile";
   horizSizing = "right";
   vertSizing = "bottom";
   position = "0 0";
   extent = "640 480";
   minExtent = "8 8";
   visible = "1";
   helpTag = "0";

   new GuiWindowCtrl(SniperSelectorWindow) {
      profile = "TBM_WindowProfile";
      horizSizing = "right";
      vertSizing = "bottom";
      position = "230 38";
      extent = "300 80";
      minExtent = "8 2";
      visible = "1";
      helpTag = "0";
      text = "SniperSelectorGui";
      maxLength = "255";
      resizeWidth = "1";
      resizeHeight = "1";
      canMove = "1";
      canClose = "1";
      canMinimize = "0";
      canMaximize = "0";
      minSize = "50 50";
      closeCommand = "canvas.popDialog(SniperSelectorGUI);";

      new GuiCheckBoxCtrl(boxsniperenable) {
         profile = "TBM_CheckBoxProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "30 30";
         extent = "80 18";
         minExtent = "8 2";
         visible = "1";
         helpTag = "0";
 	command = "togglesniperenable();";
         text = "Enable scope";
         groupNum = "-1";
         buttonType = "ToggleButton";
      };

      new GuiButtonCtrl(btnBack) {
         profile = "TBM_ButtonProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "180 30";
         extent = "80 23";
         minExtent = "8 8";
         visible = "1";
         command = "canvas.popDialog(SniperSelectorGui);";
         helpTag = "0";
         text = "Close";
         groupNum = "-1";
         buttonType = "PushButton";
      };
   new GuiBitmapCtrl(Sniper_Overlay_Preview) {
      profile = "GuiDefaultProfile";
      horizSizing = "right";
      vertSizing = "bottom";
      position = "30 60";
      extent = "240 180";
      minExtent = "8 8";
      visible = "0";
      helpTag = "0";
      bitmap = getsniperoverlayfile();
      wrap = "0";
   };

      new GuiButtonCtrl(btnSniperBack) {
         profile = "TBM_ButtonProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "30 260";
         extent = "80 23";
         minExtent = "8 8";
         visible = "1";
         command = "adjustsniperfile(-1);";
         helpTag = "0";
         text = "< Back";
         groupNum = "-1";
         buttonType = "PushButton";
      };
      new GuiButtonCtrl(btnSniperForward) {
         profile = "TBM_ButtonProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "190 260";
         extent = "80 23";
         minExtent = "8 8";
         visible = "1";
         command = "adjustsniperfile(1);";
         helpTag = "0";
         text = "Next >";
         groupNum = "-1";
         buttonType = "PushButton";
      };
   };
};
//--- OBJECT WRITE END ---

//----------------------------------------
function togglesnipergui (%val)
{
	if(%val && $sniperselectortog == 0)
		canvas.pushDialog(sniperselectorgui);
	else if(%val && $sniperselectortog == 1)
		canvas.popDialog(sniperselectorgui);
}
function sniperselectorgui::OnWake() {
$sniperselectortog = 1;
refreshsniperselectorGUI();
}
function sniperselectorgui::OnSleep() {
$sniperselectortog = 0;
}


function togglesniperenable() {
$Pref::GUI::SniperEnable = boxsniperenable.getvalue();
refreshsniperselectorGUI();
}

function refreshsniperselectorGUI() {
SniperSelectorWindow.resize(getword(SniperSelectorWindow.position,0),140,getword(SniperSelectorWindow.extent,0),80 + 220*(!!$Pref::GUI::SniperEnable));
btnSniperForward.setvisible(!!$Pref::GUI::SniperEnable);
btnSniperBack.setvisible(!!$Pref::GUI::SniperEnable);

Sniper_Overlay_Preview.setvisible(!!$Pref::GUI::SniperEnable);
Sniper_Overlay_Preview.setbitmap(getsniperoverlayfile());
boxsniperenable.setvalue($Pref::GUI::SniperEnable);
}

function adjustsniperfile(%dir) {
$Pref::GUI::SniperOverlay = $Pref::GUI::SniperOverlay + %dir;
if($Pref::GUI::SniperOverlay > $numsniperscopes)
$Pref::GUI::SniperOverlay = 0;
if($Pref::GUI::SniperOverlay < 0)
$Pref::GUI::SniperOverlay = $numsniperscopes;
refreshsniperselectorGUI();
}

function loadallscopes() {
setmodpaths(getmodpaths());
$numsniperscopes = -1;
%loc = "tbm/client/ui/Sniper overlays/*.png";
for (%filename = findFirstFile(%loc); %filename !$= ""; %filename = findNextFile(%loc)) {
%filename = strreplace(%filename,"tbm/client/ui/Sniper overlays/","");
%filename = strreplace(%filename,".png","");
$sniperfile[$numsniperscopes++] = %filename;
}
}

schedule(500,0,loadallscopes);

if(!$Sniper_Overlay_Installed) {
$RemapName[$RemapCount] = "Scope Selector Menu";
$RemapCmd[$RemapCount] = "togglesnipergui";
$RemapCount++;
$Sniper_Overlay_Installed = 1;
}

if($Pref::GUI::SniperEnable $= "")
 $Pref::GUI::SniperEnable = 1;