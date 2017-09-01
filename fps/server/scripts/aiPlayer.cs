//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------

// The AIPlayer C++ class implements the following methods.
//
// AIPlayer::stopMove();
//    Stop the player where he is.
//
// AIPlayer::clearAim();
//    Clear any current aim location or aim object.  The player will
//    maintain his current orientation, or face towards his destination
//    if moving.
//
// AIPlayer::setMoveSpeed( float );
//    Set the speed (0-1) at which the player moves.
//
// AIPlayer::setMoveDestination( "x y z" );
//    Set the destination point to move towards. The Z location is ignored
//    though the player will face upwards (or downards) towards the destination
//    if no other aim target is set.
//
// AIPlayer::getMoveDestination();
//    Returns the current destination point.
//
// AIPlayer::setAimLocation( \"x y z\" );
//    Set a point to look at.  The player maintains his orientation towards
//    this point even while moving towards a destination point.
//
// AIPlayer::setAimObject( obj );
//    Set an object to look at.  The player maintains his orientation towards
//    this object, even while moving towards a destination point. The aim
//    object can also be moving.
//
// AIPlayer::getAimLocation();
//    Returns the current point the player is looking at. If the player has
//    an aimObject set, this would be the current position of the target
//    object.
//
// AIPlayer::getAimObject();
//    Returns the current target object, if there is one.

//-----------------------------------------------------------------------------
// Callback Handlers
//-----------------------------------------------------------------------------

function Armor::onStuck(%this,%obj) 
{
   // Invoked if the player is stuck while moving
   // This method is currently not being invoked correctly.
   echo( "I'm stuck" );
}

function Armor::onReachDestination(%this,%obj)
{
   // Invoked when the player arrives at his destination point.
   echo( "onReachDestination" );
}

function Armor::onTargetEnterLOS(%this,%obj)
{
   // If an aim target object is set, this method is invoked when
   // that object becomes visible.
   echo( "onTargetEnterLOS" );
}

function Armor::onTargetExitLOS(%this,%obj)
{
   // If an aim target object is set, this method is invoked when
   // the object is not longer visible.
   echo( "onTargetExitLOS" );
}


//-----------------------------------------------------------------------------

function AIPlayer::spawnPlayer()
{
   // An example function which creates a new AIPlayer object
   // using the the example player datablock.
   %player = new AIPlayer() {
      dataBlock = LightMaleHumanArmor;
      aiPlayer = true;
   };
   MissionCleanup.add(%player);

   // Player setup
   %player.setMoveSpeed(1);
   %player.setTransform(pickSpawnPoint());
   %player.setEnergyLevel(60);
   %player.setShapeName(%this.name);
   return %player;
}   
