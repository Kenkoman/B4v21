//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Misc. server commands avialable to clients
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------

function serverCmdToggleCamera(%client)
{
   //if ($Server::TestCheats || $Server::ServerType $= "SinglePlayer")
   if(%client.isAdmin || %client.isSuperAdmin || $Server::ServerType $= "SinglePlayer")
   {
      %control = %client.getControlObject();
      if (%control == %client.player)
      {
         %control = %client.camera;
         %control.mode = toggleCameraFly;
      }
      else
      {
         %control = %client.player;
         %control.mode = observerFly;
      }
      %client.setControlObject(%control);
   }
}

function serverCmdDropPlayerAtCamera(%client)
{
   //if ($Server::TestCheats)
   if(%client.isAdmin || %client.isSuperAdmin)
   {
     if (%client.team !$= "") {
        messageClient(%client, 'Msg', "\c2You are on a team and giving you the ability do this would let you cheat.  Sorry.");
        return;
     }
      if (!%client.player.isMounted())
         %client.player.setTransform(%client.camera.getTransform());
      %client.player.setVelocity("0 0 0");
      %client.setControlObject(%client.player);
   }
}

function serverCmdDropCameraAtPlayer(%client)
{
   //if ($Server::TestCheats)
   if(%client.isAdmin || %client.isSuperAdmin)
   {
    if (%client.team !$= "") {
       messageClient(%client, 'Msg', "\c2You are on a team and giving you the ability do this would let you cheat.  Sorry.");
       return;
     }
      %client.camera.setTransform(%client.player.getEyeTransform());
      %client.camera.setVelocity("0 0 0");
      %client.setControlObject(%client.camera);
   }
}

function serverCmdMountVehicle(%client)
{
   //Determine how far should the picking ray extend into the world?
   %selectRange = 3;

   // Only search for vehicles
   %searchMasks = $TypeMasks::vehicleObjectType ;
   %searchMask2 = $TypeMasks::TurretObjectType;

   %pos = %client.player.getEyePoint();

   // Start with the shape&#180;s eye vector...
   %eye = %client.player.getEyeVector();
   %eye = vectorNormalize(%eye);
   %vec = vectorScale(%eye, %selectRange);

   %end = vectorAdd(%vec, %pos);

   %scanTarg = ContainerRayCast (%pos, %end, %searchMasks);
   %scanTarg2 = ContainerRayCast (%pos, %end, %searchMask2);

   // a target in range was found so select it
   if (%scanTarg)
   {
      %targetObject = firstWord(%scanTarg);
      echo("Found a vehicle: " @ %targetObject);
      onMountVehicle(%targetObject.getDataBlock(),
                     %client.player,
                     %targetObject);
   }
   else if (%scanTarg2)
   {
      %targetObject = firstWord(%scanTarg2);
      echo("Found a turret: " @ %targetObject);
      onMountVehicle(%targetObject.getDataBlock(),
                     %client.player,
                     %targetObject);
   }
   else
   {
      echo("No object found");
      doPlayerDismount(%client, %client.player, %true);
   }
}

function serverCmdDismountVehicle(%client)
{
   doPlayerDismount(%client, %client.player, %true);
}

function serverCmdFindNextFreeSeat(%client)
{
   echo("serverCmdFindNextFreeSeat " @ %client.nameBase);

   // Is the vehicle moving? If so, prevent the player from switching seats
   if (isVehicleMoving(%client.player.mvehicle) == true)
      return;

   %newSeat = findNextFreeSeat(%client,
                               %client.player.mvehicle,
                               %client.player.mvehicle.getDataBlock());

   if (%newSeat != -1)
   {
      echo("Found new seat " @ %newSeat);

      setActiveSeat(%client.player,
                    %client.player.mvehicle,
                    %client.player.mvehicle.getDataBlock(),
                    %newSeat);
   }
   else
   {
      echo("No next free seat");
   }
}
//-----------------------------------------------------------------------------
function serverCmdSuicide(%client)
{
   if (isObject(%client.player) && !%client.poon && !%client.carrier)     
      %client.player.kill("Suicide");
}   


function serverCmdPlayCel(%client,%anim)
{
   if (isObject(%client.player))
      %client.player.playCelAnimation(%anim);
}

function serverCmdPlayDeath(%client)
{
   if (isObject(%client.player))
      %client.player.playDeathAnimation();
}

function serverCmdSelectObject(%client, %mouseVec, %cameraPoint)
{
   //Determine how far should the picking ray extend into the world?
   %selectRange = 200;
   // scale mouseVec to the range the player is able to select with mouse
   %mouseScaled = VectorScale(%mouseVec, %selectRange);
   // cameraPoint = the world position of the camera
   // rangeEnd = camera point + length of selectable range
   %rangeEnd = VectorAdd(%cameraPoint, %mouseScaled);

   // Search for anything that is selectable – below are some examples
   %searchMasks = $TypeMasks::PlayerObjectType | $TypeMasks::CorpseObjectType |
      				$TypeMasks::ItemObjectType | $TypeMasks::TriggerObjectType;

   // Search for objects within the range that fit the masks above
   // If we are in first person mode, we make sure player is not selectable by setting fourth parameter (exempt
   // from collisions) when calling ContainerRayCast
   %player = %client.player;
   if ($firstPerson)
   {
	  %scanTarg = ContainerRayCast (%cameraPoint, %rangeEnd, %searchMasks, %player);
   }
   else //3rd person - player is selectable in this case
   {
	  %scanTarg = ContainerRayCast (%cameraPoint, %rangeEnd, %searchMasks);
   }

   // a target in range was found so select it
   if (%scanTarg)
   {
      %targetObject = firstWord(%scanTarg);

      %client.setSelectedObj(%targetObject);
   }
}


////////////////////////////////
//New Lego commands start here//
////////////////////////////////
function serverCmdBrickPlusX(%client)
{
	Echo(%client, " is moving his brick +1 X");
}
function serverCmdBrickMinusX(%client)
{
	Echo(%client, " is moving his brick -1 X");
}

function serverCmdBrickPlusY(%client)
{
	Echo(%client, " is moving his brick +1 X");
}
function serverCmdBrickMinusY(%client)
{
	Echo(%client, " is moving his brick -1 X");
}

function serverCmdBrickPlusZ(%client)
{
	Echo(%client, " is moving his brick +1 Z");
}
function serverCmdBrickMinusZ(%client)
{
	Echo(%client, " is moving his brick -1 Z");
}

function serverCmdBrickPlant(%client)
{
	Echo(%client, " is planting his brick");
}

function serverCmdBrickCancel(%client)
{
	Echo(%client, " is canceling his brick");
}

///////////////////////
//Emotes -By DShiznit//
//  Model by Elrune  //
///////////////////////

function serverCmdEmoteQuestion(%client) {
    if (%client.player.getMountedImage(0)==nametoid(emoteShowImage) && %client.player.emote $= "question")
      %client.player.unMountImage(0);
    else {
      messageClient(%client, 'MsgHilightInv', '', -1);
      %client.player.currWeaponSlot = -1;
      %client.player.mountImage(emoteShowImage, 0, 1, 'question');
      %client.player.emote = "question";
	commandToClient(%client, 'BottomPrint', "\c2You are displaying a \c3Question\c2 emote.", 3000, 1); 
      }
}

function serverCmdEmoteExclaim(%client) {
    if (%client.player.getMountedImage(0)==nametoid(emoteShowImage) && %client.player.emote $= "exclaim")
      %client.player.unmountImage(0);
    else {
      messageClient(%client, 'MsgHilightInv', '', -1);
      %client.player.currWeaponSlot = -1;
      %client.player.mountImage(emoteShowImage, 0, 1, 'exclaim');
      %client.player.emote = "exclaim";
	commandToClient(%client, 'BottomPrint', "\c2You are displaying an \c3Exclamation\c2 emote.", 3000, 1); 
      }
}

function serverCmdEmoteLove(%client) {
    if (%client.player.getMountedImage(0)==nametoid(emoteShowImage) && %client.player.emote $= "love")
      %client.player.unMountImage(0);
    else {
      messageClient(%client, 'MsgHilightInv', '', -1);
      %client.player.currWeaponSlot = -1;
      %client.player.mountImage(emoteShowImage, 0, 1, 'love');
      %client.player.emote = "love";
	commandToClient(%client, 'BottomPrint', "\c2You are displaying a \c3Love\c2 emote.", 3000, 1); 
      }
}

function serverCmdEmoteAFK(%client) {
    if (%client.player.getMountedImage(0)==nametoid(emoteShowImage) && %client.player.emote $= "afk")
      %client.player.unMountImage(0);
    else {
      messageClient(%client, 'MsgHilightInv', '', -1);
      %client.player.currWeaponSlot = -1;
      %client.player.mountImage(emoteShowImage, 0, 1, 'afk');
      %client.player.emote = "afk";
	commandToClient(%client, 'BottomPrint', "\c2You are displaying an \c3AFK\c2 emote.", 3000, 1); 
      }
}
