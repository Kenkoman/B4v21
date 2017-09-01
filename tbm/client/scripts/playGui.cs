//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// PlayGui is the main TSControl through which the game is viewed.
// The PlayGui also contains the hud controls.
//-----------------------------------------------------------------------------

function PlayGui::onWake(%this)
{
   // Turn off any shell sounds...
   // alxStop( ... );

   $enableDirectInput = "1";
   activateDirectInput();

   // Message hud dialog
   Canvas.pushDialog( MainChatHud );

   // just update the action map here
   moveMap.push();
   
   // hack city - these controls are floating around and need to be clamped
   schedule(0, 0, "refreshCenterTextCtrl");
   schedule(0, 0, "refreshBottomTextCtrl");
   setFPSvis();
}

function PlayGui::onSleep(%this)
{
   Canvas.popDialog( MainChatHud  );
   
   // pop the keymaps
   moveMap.pop();
}


//-----------------------------------------------------------------------------

function refreshBottomTextCtrl()
{
   BottomPrintText.position = "0 0";
}

function refreshCenterTextCtrl()
{
   CenterPrintText.position = "0 0";
}
$ChTotal = 0;
function getcrosshaircount() {
    %file_path = "tbm/client/ui/crosshairs/*.png";
    for(%file_name = findFirstFile(%file_path); %file_name !$= ""; %file_name = findNextFile(%file_path))
        $Crosshair[$ChTotal++] = %file_name;
}
getcrosshaircount();
function crosshairswitch(){
$pref::crosshair++;
if($pref::crosshair > $ChTotal)
$pref::crosshair = 1;
HudCrosshair.setBitmap($Crosshair[$pref::crosshair]);
}
cancel($fpsHUD);
function getFPS() {
FPS_txt.setText("\c3"@$FPS::real);
$fpsHUD = schedule(1000,0,getfps);
}
getfps();

function ep() {
exec("tbm/client/scripts/playgui.cs");
}

function setFPSvis() {
   if($Pref::Gui::ShowFPS)
   FPS_HUD.setVisible(1);
   else
   FPS_HUD.setVisible(0);
}

function clientCmdSetStudCounter(%studs, %name)
{
  if(%name $= "") %name = "$tuds";
   StudCounter_txt.setText("\c7" @ %name @ ":" SPC %studs);
}

$playGUIDamageZone    = playGUIOverlay; //This is [""]
$playGUIDamageZone[0] = playGUIOverlay;
$playGUIDamageZone[1] = playGUIDamageTop;
$playGUIDamageZone[2] = playGUIDamageRight;
$playGUIDamageZone[3] = playGUIDamageBottom;
$playGUIDamageZone[4] = playGUIDamageLeft;

function clientCmdPlayGUIOverlay(%r, %g, %b, %o, %time, %max, %zone) {
$playGUIOverlayLastControlObject = serverConnection.getControlObject();
PlayGUIOverlay(%r, %g, %b, %o, %time, %max, %zone);
}

function playGUIOverlay(%r, %g, %b, %o, %time, %max, %zone) {
//Sets the overlay to the supplied RGB color with %o opacity.
//Stays at that opacity for %max seconds.
//After that, gradually fades to absolute transparency after a total of %time seconds have passed.
cancel($playGUIOverlayTimer[%zone]);
if(serverConnection.getControlObject() == $playGUIOverlayLastControlObject) {
  if(%r > 1)   //If someone passed 0-255 colors by mistake, scale them down.
    %r /= 255;
  if(%g > 1)
    %g /= 255;
  if(%b > 1)
    %b /= 255;
  if(%o > 1)
    %o /= 255;
  $playGUIDamageZone[%zone].fillColor = %r SPC %g SPC %b SPC %o;
  $playGUIDamageZone[%zone].setVisible(%time > 0 || %time $= "");
  if(%time > 0)
    $playGUIOverlayTimer[%zone] = schedule(10, 0, playGUIOverlay, %r, %g, %b, %o * (%max > 0 ? 1 : (%time - 0.01) / %time), %time - 0.01, %max - 0.01, %zone);  //If there's time left on the timer, tell it to keep going in a hundredth of a second.  If there's time left on the max timer, don't scale the opacity down; otherwise, do that.
}
else
  $playGUIDamageZone[%zone].setVisible(0);
}