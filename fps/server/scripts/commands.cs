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
      %client.camera.setTransform(%client.player.getEyeTransform());
      %client.camera.setVelocity("0 0 0");
      %client.setControlObject(%client.camera);
   }
}


//-----------------------------------------------------------------------------

function serverCmdSuicide(%client)
{
   if (isObject(%client.player))
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

   // Search for anything that is selectable � below are some examples
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

//////////////////////////////