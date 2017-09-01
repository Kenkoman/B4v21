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

$BotCount = 0;
$GuardBot = 1;

function Armor::onStuck(%this,%obj) 
{
   // Invoked if the player is stuck while moving
   // This method is currently not being invoked correctly.
   echo( "I'm stuck" );
}

function Armor::onReachDestination(%this,%obj)
{
   // Invoked when the player arrives at his destination point.
   if (%obj.isCMBot)
   {
   NextBMark(%obj);
   }
   if (%obj.BotType $= "Patrol")
   {
   NextBMark2(%obj);
   }
}

function Armor::onTargetEnterLOS(%this,%obj)
{
   // If an aim target object is set, this method is invoked when
   // that object becomes visible.
//   %obj.setImageTrigger($RightHandSlot,true);
   //echo( "onTargetEnterLOS" );
}

function Armor::onTargetExitLOS(%this,%obj)
{
   // If an aim target object is set, this method is invoked when
   // the object is not longer visible.
   //echo( "onTargetExitLOS" );
   //%obj.setImageTrigger($RightHandSlot,false);
   %obj.TargetObj = 0;
}


//-----------------------------------------------------------------------------

function AIPlayer::spawnPlayer(%pos, %name, %type)
{
   // An example function which creates a new AIPlayer object
   // using the the example player datablock.
$BotCount++;   
$Bot[$BotCount] = new AIPlayer() {
      dataBlock = LightMaleHumanArmor;
      aiPlayer = true;
   };
   MissionCleanup.add($Bot[$BotCount]);

   // Player setup
   $Bot[$BotCount].setMoveSpeed(1);
   $Bot[$BotCount].setTransform(%pos);
   $Bot[$BotCount].setEnergyLevel(60);
   $Bot[$BotCount].setShapeName(%name);
   $Bot[$BotCount].mountimage("shieldimage",$LeftHandSlot);
   $Bot[$BotCount].mountimage("bowimage",$RightHandSlot);
   $Bot[$BotCount].mountimage("platemailImage",$BackSlot);
   $Bot[$BotCount].TargetObj = 0;
   $Bot[$BotCount].Type = %type;

   $Bot[$BotCount].OrignialRotation = $Bot[$BotCount].GetAimLocation();
   BotThink($BotCount);
}   

function BotThink(%bot)
{
	
	switch($Bot[%bot].type)
	{
	case $GuardBot:
		if(isObject($Bot[%bot].targetObj))
		{
			$Bot[%bot].setAimObject($Bot[%bot].targetObj);
			//$Bot[%bot].setImageTrigger($RightHandSlot,true);
		}
		else
		{
			$Bot[%bot].clearAim();
			//$Bot[%bot].setAimLocation($Bot[$BotCount].OrignialRotation);
			$Bot[%bot].setImageTrigger($RightHandSlot,false);
		}
	case $PassiveBot:
	}
	schedule(500,0,"BotThink",%bot);
}


function AIplayer::jump(%this)
{
	%this.setImageTrigger(2,true);
	schedule( 100, 0, endjump, %this );
}

function endjump(%bot)
{
	%bot.setImageTrigger(2, false);
}








function ffly(%per, %bot)
{
echo("being called");
if (isObject(%bot) && isObject(%per))
{
%bot.setaimobject(%per);
%ptrans=%per.getTransform();
%pz = getword(%ptrans, 2);
%btrans=%bot.getTransform();
%bz=getword(%btrans, 2);
if (%bz < %pz - 1)
{
%val = 0;
schedule(1, 0, "hbotf", %per, %bot, %val);
}
if (%bz > %pz + 1)
{
%val = 1;
schedule(1, 0, "hbotf", %per, %bot, %val);
}
else if(%bz > %pz - 1 && %bz < %pz + 1)
{
%bot.setmovedestination(%ptrans);
schedule(1000, 0, "ffly", %per, %bot);
}
else
{
schedule(1000, 0, "ffly", %per, %bot);
}
}
else
{
%bot.setMoveDestination(%btrans);
%bot.clearAim();
%bot.setImageTrigger(2, 0);
%bot.setImageTrigger(4, 0);
}
}
function hbotf(%per, %bot, %val)
{
if (isObject(%bot) && isObject(%per))
{
%ptrans=%per.getTransform();
%pz = getword(%ptrans, 2);
%btrans=%bot.getTransform();
%bz=getword(%btrans, 2);


if (%bot.fstat == 0 && %val == 0)
{
%bot.setimagetrigger(4, 1);
%bot.setmovedestination(%btrans);
%bot.fstat = 1;
schedule(1300, 0, "ffly", %per, %bot);
}


else if (%bot.fstat == 0 && %val == 0)
{
%bot.setimagetrigger(4, 1);
%bot.setmovedestination(%btrans);
%bot.fstat = 1;
schedule(1300, 0, "ffly", %per, %bot);
}


else if (%bot.fstat == 1 && %val == 1)
{
%bot.fstat = 0;
%bot.setmovedestination(%ptrans);
schedule(1300, 0, "ffly", %per, %bot);
}
else
{
echo("num3");
schedule(1500, 0, "ffly", %per, %bot);
}
}
else
{
%bot.setMoveDestination(%btrans);
%bot.clearAim();
%bot.setImageTrigger(2, 0);
%bot.setImageTrigger(4, 0);
}
}