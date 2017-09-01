//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

if ( isObject( moveMap ) )
   moveMap.delete();
new ActionMap(moveMap);


//------------------------------------------------------------------------------
// Non-remapable binds
//------------------------------------------------------------------------------

function escapeFromGame()
{
   if($Pref::Gui::OldEscapeMenu) {
      if($Server::ServerType $= "SinglePlayer")
         MessageBoxYesNo( "Quit Mission", "Exit from this Mission?", "disconnect();", "");
      else
         MessageBoxYesNo( "Disconnect", "Disconnect from the server?", "disconnect();", "");
   }
   else
      escapeMenuGUI.toggle();
}

moveMap.bindCmd(keyboard, "escape", "", "escapeFromGame();");
moveMap.bindcmd(keyboard, "F2", "", "PlayerListGui.toggle();");


//------------------------------------------------------------------------------
// Movement Keys
//------------------------------------------------------------------------------

$movementSpeed = 1; // m/s

function setSpeed(%speed)
{
   if(%speed)
      $movementSpeed = %speed;
}

function moveleft(%val)
{
   $mvLeftAction = %val * $RunMultiplier;
}

function moveright(%val)
{
   $mvRightAction = %val * $RunMultiplier;
}

function moveforward(%val)
{
   $mvForwardAction = %val * $RunMultiplier;
}

function movebackward(%val)
{
   $mvBackwardAction = %val * $RunMultiplier;
}

function moveup(%val)
{
   $mvUpAction = %val;
}

function movedown(%val)
{
   $mvDownAction = %val;
}

function turnLeft( %val )
{
   $mvYawRightSpeed = %val ? $Pref::Input::KeyboardTurnSpeed : 0;
}

function turnRight( %val )
{
   $mvYawLeftSpeed = %val ? $Pref::Input::KeyboardTurnSpeed : 0;
}

function panUp( %val )
{
   $mvPitchDownSpeed = %val ? $Pref::Input::KeyboardTurnSpeed : 0;
}

function panDown( %val )
{
   $mvPitchUpSpeed = %val ? $Pref::Input::KeyboardTurnSpeed : 0;
}

function getMouseAdjustAmount(%val)
{
   // based on a default camera fov of 90'
   return(%val * ($cameraFov / 90) * 0.001);
}

function getMouseAdjustAmount(%val){   return(%val * ($cameraFov / 90) * 0.005); }


function yaw(%val)
{
	%sens = $pref::Input::MouseSensitivity;
	$mvYaw += %sens*getMouseAdjustAmount(%val);
}

function pitch(%val)
{
	%sens = $pref::Input::MouseSensitivity;
	if($pref::Input::MouseInvert){
		$mvPitch -= %sens*(getMouseAdjustAmount(%val));
	}
	else{
		$mvPitch += %sens*getMouseAdjustAmount(%val);
	}
}

function jump(%val)
{
   $mvTriggerCount2++;
}
//------------------
//FWAR key functions
//------------------
$RunMultiplier = 1;
function walk(%val)		//$runmultiplier is used in foward/back/left/right
{
	if(%val)
		$RunMultiplier = 0.4;
	else
		$RunMultiplier = 1;

	if ($mvLeftAction)
		$mvLeftAction = $RunMultiplier;
	if ($mvRightAction)
		$mvRightAction = $RunMultiplier;
	if ($mvForwardAction)
		$mvForwardAction = $RunMultiplier;
	if ($mvBackwardAction)
		$mvBackwardAction = $RunMultiplier;
}
function crouch(%val)
{
   //$mvCrouch = %val;
   if(!MessageHud.isVisible())
   $mvTriggerCount3++;
}
function jet(%val)
{
   //$mvCrouch = %val;
   $mvTriggerCount4++;
}

moveMap.bind( keyboard, a, moveleft );
moveMap.bind( keyboard, d, moveright );
moveMap.bind( keyboard, w, moveforward );
moveMap.bind( keyboard, s, movebackward );
moveMap.bind( keyboard, space, jump );
moveMap.bind( mouse, xaxis, yaw );
moveMap.bind( mouse, yaxis, pitch );


//------------------------------------------------------------------------------
// Mouse Trigger
//------------------------------------------------------------------------------

function mouseFire(%val)
{
   $mvTriggerCount0++;
}

function altTrigger(%val)
{
   $mvTriggerCount1++;
}

moveMap.bind( mouse, button0, mouseFire );
//moveMap.bind( mouse, button1, altTrigger );


//------------------------------------------------------------------------------
// Zoom and FOV functions
//------------------------------------------------------------------------------

if($Pref::player::CurrentFOV $= "")
   $Pref::player::CurrentFOV = 45;

function setZoomFOV(%val)
{
   if(%val)
      toggleZoomFOV();
}
      
function toggleZoom( %val )
{
   if ( %val )
   {
      $ZoomOn = true;
      setFov( $Pref::player::CurrentFOV );
   }
   else
   {
      $ZoomOn = false;
      setFov( $Pref::player::DefaultFov );
   }
}

moveMap.bind(keyboard, r, setZoomFOV);
moveMap.bind(keyboard, e, toggleZoom);


//------------------------------------------------------------------------------
// Camera & View functions
//------------------------------------------------------------------------------

function toggleFreeLook( %val )
{
   if ( %val )
      $mvFreeLook = true;
   else
      $mvFreeLook = false;
}

function toggleFirstPerson(%val)
{
   if (%val)
   {
      $firstPerson = !$firstPerson;
   }
}

function toggleCamera(%val)
{
   if (%val)
      commandToServer('ToggleCamera');
}

moveMap.bind( keyboard, z, toggleFreeLook );
moveMap.bind(keyboard, tab, toggleFirstPerson );
moveMap.bind(keyboard, "alt c", toggleCamera);


//------------------------------------------------------------------------------
// Misc. Player stuff
//------------------------------------------------------------------------------

moveMap.bindCmd(keyboard, "ctrl w", "commandToServer('playCel',\"wave\");", "");
moveMap.bindCmd(keyboard, "ctrl s", "commandToServer('playCel',\"salute\");", "");
moveMap.bindCmd(keyboard, "ctrl k", "commandToServer('suicide');", "");


//------------------------------------------------------------------------------
// Item manipulation
//------------------------------------------------------------------------------

moveMap.bindCmd(keyboard, "h", "commandToServer('use',\"HealthKit\");", "");
moveMap.bindCmd(keyboard, "1", "commandToServer('use',\"Rifle\");", "");
moveMap.bindCmd(keyboard, "2", "commandToServer('use',\"Crossbow\");", "");

//------------------------------------------------------------------------------
// Message HUD functions
//------------------------------------------------------------------------------

function pageMessageHudUp( %val )
{
   if ( %val )
      pageUpMessageHud();
}

function pageMessageHudDown( %val )
{
   if ( %val )
      pageDownMessageHud();
}

function resizeMessageHud( %val )
{
   if ( %val )
      cycleMessageHudSize();
}

moveMap.bind(keyboard, u, toggleMessageHud );
moveMap.bind(keyboard, y, teamMessageHud );
moveMap.bind(keyboard, "pageUp", pageMessageHudUp );
moveMap.bind(keyboard, "pageDown", pageMessageHudDown );
moveMap.bind(keyboard, "p", resizeMessageHud );


//------------------------------------------------------------------------------
// Demo recording functions
//------------------------------------------------------------------------------

function startRecordingDemo( %val )
{
   if ( %val )
      startDemoRecord();
}

function stopRecordingDemo( %val )
{
   if ( %val )
      stopDemoRecord();
}

moveMap.bind( keyboard, F3, startRecordingDemo );
moveMap.bind( keyboard, F4, stopRecordingDemo );


//------------------------------------------------------------------------------
// Helper Functions
//------------------------------------------------------------------------------

function dropCameraAtPlayer(%val)
{
   if (%val)
      commandToServer('dropCameraAtPlayer');
}

function dropPlayerAtCamera(%val)
{
   if (%val)
      commandToServer('DropPlayerAtCamera');
}

moveMap.bind(keyboard, "F8", dropCameraAtPlayer);
moveMap.bind(keyboard, "F7", dropPlayerAtCamera);


//------------------------------------------------------------------------------
// Dubuging Functions
//------------------------------------------------------------------------------

$MFDebugRenderMode = 0;
function cycleDebugRenderMode(%val)
{
   if (!%val)
      return;

   if (getBuildString() $= "Debug")
   {
      if($MFDebugRenderMode == 0)
      {
         // Outline mode, including fonts so no stats
         $MFDebugRenderMode = 1;
         GLEnableOutline(true);
      }
      else if ($MFDebugRenderMode == 1)
      {
         // Interior debug mode
         $MFDebugRenderMode = 2;
         GLEnableOutline(false);
         setInteriorRenderMode(7);
         showInterior();
      }
      else if ($MFDebugRenderMode == 2)
      {
         // Back to normal
         $MFDebugRenderMode = 0;
         setInteriorRenderMode(0);
         GLEnableOutline(false);
         show();
      }
   }
   else
   {
      echo("Debug render modes only available when running a Debug build.");
   }
}

GlobalActionMap.bind(keyboard, "F9", cycleDebugRenderMode);


//------------------------------------------------------------------------------
// Misc.
//------------------------------------------------------------------------------
function megashift(%x,%y,%z) {
commandtoserver('shiftbrick',%x,%y,%z);
}
GlobalActionMap.bind(keyboard, "tilde", toggleConsole);
GlobalActionMap.bindCmd(keyboard, "alt enter", "", "toggleFullScreen();");
GlobalActionMap.bindCmd(keyboard, "F1","","contextTBMHelp();");
GlobalActionMap.bindCmd(keyboard, "ctrl *", "commandToServer(\'ShiftScale\', 5);", "");
GlobalActionMap.bindCmd(keyboard, "ctrl numpaddivide", "commandToServer(\'ShiftScale\', -5);", "");
GlobalActionMap.bindCmd(keyboard, "ctrl-alt *", "commandToServer(\'ShiftScale\', 50);", "");
GlobalActionMap.bindCmd(keyboard, "ctrl-alt numpaddivide", "commandToServer(\'ShiftScale\', -50);", "");
GlobalActionMap.bindCmd(keyboard, "ctrl numpad4", "megaShift(0,32,0);", "");
GlobalActionMap.bindCmd(keyboard, "ctrl numpad6", "megaShift(0,-32,0);", "");
GlobalActionMap.bindCmd(keyboard, "ctrl numpad2", "megaShift(-32,0,0);", "");
GlobalActionMap.bindCmd(keyboard, "ctrl numpad8", "megaShift(32,0,0);", "");
GlobalActionMap.bindcmd(keyboard, "ctrl +", "megaShift(0,0,15);", "");
GlobalActionMap.bindCmd(keyboard, "ctrl numpad5", "megaShift(0,0,-15);", "");
GlobalActionMap.bindCmd(keyboard, "ctrl-alt +", "megaShift(0,0,150);", "");
GlobalActionMap.bindCmd(keyboard, "ctrl-alt numpad5", "megaShift(0,0,-150);", "");