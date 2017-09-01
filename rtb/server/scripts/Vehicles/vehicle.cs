// *******************************************
// vehicle.cs
//
// Copyright (c) 2002 Pixellent Interactive
//
// This script file contains generic code for
// mounting and dismounting vehicles, and for
// switching seats.
// *******************************************


// Define the move maps for each position. This may need
// to be defined on a per vehicle basis in the future
// once mounted weapons are implemented.

$Vehicle::moveMaps[0] = "vehicleDriverMap";
$Vehicle::moveMaps[1] = "vehiclePassengerMap";
$Vehicle::moveMaps[2] = "vehiclePassengerMap";
$Vehicle::moveMaps[3] = "vehiclePassengerMap";
$Vehicle::moveMaps[4] = "vehiclePassengerMap";
$Vehicle::moveMaps[5] = "vehiclePassengerMap";
$Vehicle::moveMaps[6] = "vehiclePassengerMap";
$Vehicle::moveMaps[7] = "vehiclePassengerMap";
$Vehicle::moveMaps[8] = "vehiclePassengerMap";
//-------------------------------------------
//Humvee
//-------------------------------------------
$Humvee::moveMaps[1] = "vehicleDriverMap";
$Humvee::moveMaps[2] = "vehiclePassengerMap";
$Humvee::moveMaps[3] = "vehiclePassengerMap";
$Humvee::moveMaps[4] = "vehiclePassengerMap";

// *******************************************
// Function: findEmptySeat
//
// Inputs: Vehicle object
//         Vehicle datablock
//
// Outputs: -1 if there are no empty seats
//          The seat number that is free
//
// This function locates the next free seat in
// the vehicle.
// *******************************************
function findEmptySeat(%vehicle, %vehicleblock)
{
   echo("This vehicle has " @ %vehicleblock.numMountPoints @ " mount points.");
   for (%i = 0; %i <  %vehicleblock.numMountPoints; %i++)
   {
      %node = %vehicle.getMountNodeObject(%i);
      if (%node == 0)
      {
         return %i;
      }
   }
   return -1;
}

// *******************************************
// Function: onMountVehicle
//
// Inputs: Vehicle object
//         Player datablock
//         Vehicle object
//
// Outputs: None
//
// This function is called by the player's
// onMount function. It locates an empty
// seat in the vehicle and calls the function
// to perform the mount.
// *******************************************
function onMountVehicle(%vehicle, %obj, %col)
{
   // Is the vehicle currently mountable?
   if (%col.mountable == false)
   {
      echo("Sorry, the vehicle is not mountable.");
      return;
   }

   // Check the speed of the vehicle. If it's moving, we can't mount it.
   // Note there is a threshold that determines if the vehicle is moving.
   %vel = %col.getVelocity();
   %speed = vectorLen(%vel);

   if ( %speed <= 10 )
   {
    //   Find an empty seat
      %seat = findEmptySeat(%col, %vehicle);

      %obj.mVehicle = %col;
      %obj.mSeat = %seat;
      %obj.isMounted = true;

      echo("Mounting vehicle in seat " @ %seat);

      // Now mount the vehicle.
      %col.mountObject(%obj,%seat);
   }
   else
   {
      echo("You cannot mount a moving vehicle.");
   }

}


// *******************************************
// Function: onPlayerVehicle
//
// Inputs: Player object
//         Player object
//         Vehicle object
//         Seat (node)
//
// Outputs: None
//
// This function is called when the player
// mounts a vehicle. It commands the client to
// display the correct movement map, removes
// the player's weapon and gives them control
// of the vehicle if they are in the driver's
// seat.
// *******************************************
function onPlayerMount(%player,%obj,%vehicle,%node)
{
   CommandToClient(%obj.client, 'PopActionMap', moveMap);
   CommandToClient(%obj.client, 'PushActionMap', $Vehicle::moveMaps[%node]);
   //CommandToClient(%obj.client,'showEscMenu');
   Canvas.PopDialog(Speedometer);

   %obj.setTransform(%vehicle.getDataBlock().mountPointTransform[%node]);
   %obj.lastWeapon = %obj.getMountedImage($WeaponSlot);
   %obj.unmountImage($WeaponSlot);
   %obj.setActionThread(%vehicle.getDatablock().mountPose[%node],true,true);

   // Are we driving this vehicle?
   if (%node == 0)  {
      %obj.setControlObject(%vehicle);
   }
}

// *******************************************
// Function: doPlayerDismount
//
// Inputs: Player object
//         Player datablock
//         Forced flag
//
// Outputs: None
//
// This function is called when the player
// presses the dismount key. The player can only
// dismount when the vehicle is stationery.
// *******************************************
function doPlayerDismount(%player, %obj, %forced)
{
   %vel = %obj.getVelocity();
   %speed = vectorLen(%vel);

   // Check our speed. If we're still moving, we can't dismount.
   if (%speed >= 10 || !%obj.isMounted())
   {
      // The vehicle is moving or the player is not mounted.
      return;
   }

   // Find the position above dismount point.
   %pos    = getWords(%obj.getTransform(), 0, 2);
   %oldPos = %pos;
   %vec[0] = " 2  0  0";
   %vec[1] = " 0  -2  0";
   %vec[2] = " 0  2  0";
   %vec[3] = "-2  0  0";
   %vec[4] = " 4  0  0";
   %impulseVec  = "0 0 0";
   %vec[0] = MatrixMulVector( %obj.getTransform(), %vec[0]);

   // Make sure the point is valid
   %pos = "0 0 0";
   %numAttempts = 5;
   %success     = -1;
   for (%i = 0; %i < %numAttempts; %i++) {
      %pos = VectorAdd(%oldPos, VectorScale(%vec[%i], 3));
      if (%obj.checkDismountPoint(%oldPos, %pos)) {
         %success = %i;
         %impulseVec = %vec[%i];
         break;
      }
   }
   if (%forced && %success == -1)
      %pos = %oldPos;

   if(%obj.mVehicle)
   {
      %data = %obj.mVehicle.getDataBlock();
      if (%data.className $= TurretData)
         %data.playerDismounted(%obj.mVehicle, %obj);

      %obj.mVehicle.mountable = true;
	  %client = %obj.client;
 	  if(%obj.mVehicle.mountedTurret.currentDriver == %client)
 		%obj.mVehicle.mountedTurret.currentDriver = "";
   }
   
   %obj.mountVehicle = false;
   %obj.schedule(4000, "setMountVehicle", true);
   %obj.schedule(4000, "mountVehicles", true);
	
   %obj.unmount();
   %obj.setControlObject(%obj);
   %obj.mountVehicle = false;

   // Schedule the function to set the mount flag, so that the player
   // can mount another vehicle in the future.
   //%obj.schedule(4000, "MountVehicles", true);

   // Position above dismount point
   %obj.setTransform(%pos);
   %obj.applyImpulse(%pos, VectorScale(%impulseVec, %obj.getDataBlock().mass));

   %obj.setActionThread("run",true,true);
   %obj.setArmThread("look");

   // Command the client to display the correct movement map and
   // activate the command menu.
   CommandToClient(%obj.client, 'PopActionMap', $Vehicle::moveMaps[%obj.mSeat]);
   CommandToClient(%obj.client, 'PushActionMap', moveMap);
   //CommandToClient(%obj.client,'activateCommandMenu');
}

// *******************************************
// Function: onPlayerUmount
//
// Inputs: Player object
//         Player datablock
//         Vehicle object
//         Seat
//
// Outputs: None
//
// This function mounts the player's weapon
// after they dismount from the vehicle.
// *******************************************
function onPlayerUnmount(%player, %obj, %vehicle, %node)
{
   %obj.mountImage(%obj.lastWeapon, $WeaponSlot);
}

// *******************************************
// Function: findNextFreeSeat
//
// Inputs: Client connection object
//         Vehicle object
//         Vehicle datablock
//
// Outputs: FALSE if a seat is not found
//          The next free seat number
//
// This function searches the vehicle for a
// free seat.
// *******************************************
function findNextFreeSeat(%client, %vehicle, %vehicleblock)
{
   // Check the next seat
   %seat = %client.player.mSeat + 1;

   if (%seat == %vehicleblock.numMountPoints)
   {
      // Check from seat 0, we've run out of seats to check.
      %seat = 0;
   }

   // Reset the flag.
   %found = false;

   // Search through the seats.
   while ((%seat != %client.player.mSeat) && (%found == false))
   {
      if (%seat >= %vehicleblock.numMountPoints)
      {
         // Go back to the first (driver's) seat
         %seat = 0;
      }

      // Is this seat free?
      %node = %vehicle.getMountNodeObject(%seat);

      if (%node == 0)
      {
         // Yes it is free.
         %found = true;
      }
      if (%found == false)
      {
         // We couldn't find a free seat.
         %seat++;
      }
   }

   if (%found == false)
      return -1;
   else
      return %seat;
}

// *******************************************
// Function: findPreviousFreeSeat
//
// Inputs: Client connection object
//         Vehicle object
//         Vehicle datablock
//
// Outputs: FALSE if a seat is not found
//          The previous free seat number
//
// This function searches the vehicle for a
// free seat. It searches in the opposite
// direction to the findNextFreeSeat function.
// *******************************************
function findPreviousFreeSeat(%client, %vehicle, %vehicleblock)
{
   // Check the previous seat
   %seat = %client.player.mSeat - 1;

   if (%seat == -1)
   {
      // Check from the last seat
      echo("Checking from seat " @ %seat);
   }

   %found = false;

   // Search through the seats.
   while ((%seat != %client.player.mSeat) && (%found == false))
   {
      if (%seat < 0)
      {
         %seat = %vehicleblock.numMountPoints - 1;
      }

      %node = %vehicle.getMountNodeObject(%seat);

      if (%node == 0)
      {
         %found = true;
      }
      if (%found == false)
         %seat--;
   }

   if (%found == false)
      return -1;
   else
      return %seat;
}

// *******************************************
// Function: setActiveSeat
//
// Inputs: Client connection object
//         Vehicle object
//         Vehicle datablock
//         Seat number
//
// Outputs: none
//
// This function re-seats the player into the
// given seat. The player is rotated to face
// the direction defined for the seat. The
// player's pose is also modified and the
// client is ordered to use the correct
// movement map.
// *******************************************
function setActiveSeat(%client, %vehicle, %vehicleblock, %seat)
{
   %client.setTransform(%vehicle.getDataBlock().mountPointTransform[%seat]);
   %vehicle.mountObject(%client,%seat);
   %client.mVehicle = %vehicle;

   CommandToClient(%obj.client, 'PopActionMap', moveMap);
   CommandToClient(%obj.client, 'PushActionMap', $Vehicle::moveMaps[%seat]);

   %client.mSeat = %seat;

   %client.setActionThread(%vehicle.getDatablock().mountPose[%seat],true,true);

   // Are we driving this vehicle?
   if (%seat == 0)
   {
      %client.setControlObject(%vehicle);
      %client.setArmThread("sitting");
   }
   else
   {
      %client.setControlObject(%client);
      %client.setArmThread("looknw");
   }
}

// *******************************************
// Function: isVehicleMoving
//
// Inputs: Vehicle object
//
// Outputs: FALSE if the vehicle is NOT moving
//          TRUE if the vehicle is moving
//
// This function calcuates if the vehicle is
// moving according to a threshold value
// defined on a per-vehicle basis.
// *******************************************
function isVehicleMoving(%vehicle)
{
   // Calculate the vehicle's velocity
   %vel = %vehicle.getVelocity();
   %speed = vectorLen(%vel);

   // Determine if the vehicle is moving according to the
   // threshold value defined in the vehicle's datablock.
   if (%speed > %vehicle.getDataBlock().stationaryThreshold)
      return true;
   else
      return false;
}

// *******************************************
// Function: getVehicleSpeed
//
// Inputs: Vehicle object
//
// Outputs: Vehicle's speed
//
// This utility function calculates the
// vehicle's velocity. Note that the speed
// does not define the vehicle's direction.
// *******************************************
function getVehicleSpeed(%vehicle)
{
   %vel = %vehicle.getVelocity();
   %speed = vectorLen(%vel);
   return %speed;
}

// *******************************************
// Function: dumpMounts
//
// Inputs: Vehicle object
//         Vehicle datablock
//
// Outputs: None
//
// This utility function simply dumps the
// mounts for a given vehicle. It is used to
// check which players the game engine thinks
// are currently mounted for each seat.
// *******************************************
function dumpMounts(%vehicle, %vehicleBlock)
{
   echo("**************");
   echo("Dumping mounts");
   echo("--------------");
   for (%ii=0; %ii<%vehicleblock.numMountPoints;%ii++)
   {
     echo(%ii @ ": " @ %vehicle.getMountNodeObject(%ii));
   }
   echo("**************");
}


