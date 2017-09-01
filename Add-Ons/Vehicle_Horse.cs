//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

// Load dts shapes and merge animations
datablock TSShapeConstructor(HorseDts)
{
	baseShape  = "./shapes/horse.dts";
	sequence0  = "./shapes/h_root.dsq root";

	sequence1  = "./shapes/h_run.dsq run";
	sequence2  = "./shapes/h_run.dsq walk";
	sequence3  = "./shapes/h_back.dsq back";
	sequence4  = "./shapes/h_side.dsq side";

	sequence5  = "./shapes/h_root.dsq crouch";
	sequence6  = "./shapes/h_run.dsq crouchRun";
	sequence7  = "./shapes/h_back.dsq crouchBack";
	sequence8  = "./shapes/h_side.dsq crouchSide";

	sequence9  = "./shapes/h_look.dsq look";
	sequence10 = "./shapes/h_root.dsq headside";
	sequence11 = "./shapes/h_root.dsq headUp";

	sequence12 = "./shapes/h_jump.dsq jump";
	sequence13 = "./shapes/h_jump.dsq standjump";
	sequence14 = "./shapes/h_root.dsq fall";
	sequence15 = "./shapes/h_root.dsq land";

	sequence16 = "./shapes/h_armAttack.dsq armAttack";
	sequence17 = "./shapes/h_root.dsq armReadyLeft";
	sequence18 = "./shapes/h_root.dsq armReadyRight";
	sequence19 = "./shapes/h_root.dsq armReadyBoth";
	sequence20 = "./shapes/h_spearReady.dsq spearready";  
	sequence21 = "./shapes/h_root.dsq spearThrow";

	sequence22 = "./shapes/h_root.dsq talk";  

	sequence23 = "./shapes/h_death1.dsq death1"; 
	
	sequence24 = "./shapes/h_shiftUp.dsq shiftUp";
	sequence25 = "./shapes/h_shiftDown.dsq shiftDown";
	sequence26 = "./shapes/h_shiftAway.dsq shiftAway";
	sequence27 = "./shapes/h_shiftTo.dsq shiftTo";
	sequence28 = "./shapes/h_shiftLeft.dsq shiftLeft";
	sequence29 = "./shapes/h_shiftRight.dsq shiftRight";
	sequence30 = "./shapes/h_rotCW.dsq rotCW";
	sequence31 = "./shapes/h_rotCCW.dsq rotCCW";

	sequence32 = "./shapes/h_root.dsq undo";
	sequence33 = "./shapes/h_plant.dsq plant";

	sequence34 = "./shapes/h_root.dsq sit";

	sequence35 = "./shapes/h_root.dsq wrench";
};    


datablock AudioProfile(HorseFootFallSound)
{
   fileName = "base/data/sound/pain.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(HorseJumpSound)
{
   fileName = "./sound/jumpHorse.wav";
   description = AudioClose3d;
   preload = true;
};


datablock DebrisData( HorseDebris )
{
   explodeOnMaxBounce = false;

   elasticity = 0.15;
   friction = 0.5;

   lifetime = 4.0;
   lifetimeVariance = 0.0;

   minSpinSpeed = 40;
   maxSpinSpeed = 600;

   numBounces = 5;
   bounceVariance = 0;

   staticOnMaxBounce = true;
   gravModifier = 1.0;

   useRadiusMass = true;
   baseRadius = 1;

   velocity = 20.0;
   velocityVariance = 12.0;
};             

datablock PlayerData(HorseArmor)
{
   renderFirstPerson = false;
   emap = false;
   
   className = Armor;
   shapeFile = "./shapes/horse.dts";
   cameraMaxDist = 8;
   cameraTilt = 0.261;//0.174 * 2.5; //~25 degrees
   cameraVerticalOffset = 2.3;
   computeCRC = false;
  
   canObserve = true;
   cmdCategory = "Clients";

   cameraDefaultFov = 90.0;
   cameraMinFov = 5.0;
   cameraMaxFov = 120.0;
   
   //debrisShapeName = "~/data/shapes/player/debris_player.dts";
   //debris = horseDebris;

   aiAvoidThis = true;

   minLookAngle = -1.5708;
   maxLookAngle = 1.5708;
   maxFreelookAngle = 3.0;

   mass = 90;
   drag = 0.1;
   maxdrag = 0.52;
   density = 0.7;
   maxDamage = 250;
   maxEnergy =  10;
   repairRate = 0.33;
   energyPerDamagePoint = 75.0;

   rechargeRate = 0.4;

   runForce = 28 * 90;
   runEnergyDrain = 0;
   minRunEnergy = 0;
   maxForwardSpeed = 12;
   maxBackwardSpeed = 6;
   maxSideSpeed = 1;

   maxForwardCrouchSpeed = 12;
   maxBackwardCrouchSpeed = 6;
   maxSideCrouchSpeed = 1;

   maxForwardProneSpeed = 0;
   maxBackwardProneSpeed = 0;
   maxSideProneSpeed = 0;

   maxForwardWalkSpeed = 0;
   maxBackwardWalkSpeed = 0;
   maxSideWalkSpeed = 0;

   maxUnderwaterForwardSpeed = 8.4;
   maxUnderwaterBackwardSpeed = 7.8;
   maxUnderwaterSideSpeed = 7.8;

   jumpForce = 17 * 90; //8.3 * 90;
   jumpEnergyDrain = 0;
   minJumpEnergy = 0;
   jumpDelay = 0;

   minJetEnergy = 0;
	jetEnergyDrain = 0;
	canJet = 0;

   recoverDelay = 0;
   recoverRunForceScale = 1.2;

   minImpactSpeed = 250;
   speedDamageScale = 3.8;

   boundingBox			= vectorScale("2.5 2.5 2.4", 4); //"2.5 2.5 2.4";
   crouchBoundingBox	= vectorScale("2.5 2.5 2.4", 4); //"2.5 2.5 2.4";
   proneBoundingBox		= vectorScale("2.5 2.5 2.4", 4); //"2.5 2.5 2.4";

   pickupRadius = 0.75;
   
   // Damage location details
   boxNormalHeadPercentage       = 0.83;
   boxNormalTorsoPercentage      = 0.49;
   boxHeadLeftPercentage         = 0;
   boxHeadRightPercentage        = 1;
   boxHeadBackPercentage         = 0;
   boxHeadFrontPercentage        = 1;

   // Foot Prints
   //decalData   = HorseFootprint;
   //decalOffset = 0.25;
	
   jetEmitter = playerJetEmitter;
   jetGroundEmitter = playerJetGroundEmitter;
   jetGroundDistance = 4;
  
   //footPuffEmitter = LightPuffEmitter;
   footPuffNumParts = 10;
   footPuffRadius = 0.25;

   //dustEmitter = LiftoffDustEmitter;

   splash = PlayerSplash;
   splashVelocity = 4.0;
   splashAngle = 67.0;
   splashFreqMod = 300.0;
   splashVelEpsilon = 0.60;
   bubbleEmitTime = 0.1;
   splashEmitter[0] = PlayerFoamDropletsEmitter;
   splashEmitter[1] = PlayerFoamEmitter;
   splashEmitter[2] = PlayerBubbleEmitter;
   mediumSplashSoundVelocity = 10.0;   
   hardSplashSoundVelocity = 20.0;   
   exitSplashSoundVelocity = 5.0;

   // Controls over slope of runnable/jumpable surfaces
   runSurfaceAngle  = 85;
   jumpSurfaceAngle = 86;

   minJumpSpeed = 20;
   maxJumpSpeed = 30;

   horizMaxSpeed = 68;
   horizResistSpeed = 33;
   horizResistFactor = 0.35;

   upMaxSpeed = 80;
   upResistSpeed = 25;
   upResistFactor = 0.3;
   
   footstepSplashHeight = 0.35;

   //NOTE:  some sounds commented out until wav's are available

   JumpSound			= HorseJumpSound;

   // Footstep Sounds
   FootSoftSound        = HorseFootFallSound;
   FootHardSound        = HorseFootFallSound;
   FootMetalSound       = HorseFootFallSound;
   FootSnowSound        = HorseFootFallSound;
   FootShallowSound     = HorseFootFallSound;
   FootWadingSound      = HorseFootFallSound;
   FootUnderwaterSound  = HorseFootFallSound;
   //FootBubblesSound     = FootLightBubblesSound;
   //movingBubblesSound   = ArmorMoveBubblesSound;
   //waterBreathSound     = WaterBreathMaleSound;

   //impactSoftSound      = ImpactLightSoftSound;
   //impactHardSound      = ImpactLightHardSound;
   //impactMetalSound     = ImpactLightMetalSound;
   //impactSnowSound      = ImpactLightSnowSound;
   
   //impactWaterEasy      = ImpactLightWaterEasySound;
   //impactWaterMedium    = ImpactLightWaterMediumSound;
   //impactWaterHard      = ImpactLightWaterHardSound;
   
   groundImpactMinSpeed    = 10.0;
   groundImpactShakeFreq   = "4.0 4.0 4.0";
   groundImpactShakeAmp    = "1.0 1.0 1.0";
   groundImpactShakeDuration = 0.8;
   groundImpactShakeFalloff = 10.0;
   
   //exitingWater         = ExitingWaterLightSound;
   
   observeParameters = "0.5 4.5 4.5";

   // Inventory Items
	maxItems   = 10;	//total number of bricks you can carry
	maxWeapons = 5;		//this will be controlled by mini-game code
	maxTools = 5;
	
	uiName = "Horse";
	rideable = true;
		lookUpLimit = 0.6;
		lookDownLimit = 0.2;

	canRide = false;
	showEnergyBar = false;
	paintable = true;

	brickImage = horseBrickImage;	//the imageData to use for brick deployment

   numMountPoints = 1;
   mountThread[0] = "root";
};



function HorseArmor::onAdd(%this,%obj)
{
   // Vehicle timeout
   %obj.mountVehicle = true;

   // Default dynamic armor stats
   %obj.setRechargeRate(%this.rechargeRate);
   %obj.setRepairRate(0);

}

//function HorseArmor::onCollision(%this,%obj,%col,%vec,%speed)
//{
//	//echo("HorseArmor collided");
//
//	//echo("obj = ", %obj);
//	//echo("col = ", %col);
//
//	//dont mount dead horses
//	if (%obj.getState() $= "Dead")
//		return;
//	
//	//don't let dead people ride
//	if(%col.getDamagePercent() >= 1.0)
//		return;
//
//
//	if(%col.getClassName() $= "Item")
//	{
//		Parent::onCollision(%this,%obj,%col,%vec,%speed);
//	}
//	else if(%col.getClassName() $= "Player" || %col.getClassName() $= "AIPlayer")
//	{
//		if (%col.getDataBlock().canRide) // should probably have a "can ride" flag in player datablock
//		{
//			if(!isObject(%col.client))	//no client = no mount
//				return;
//			
//			//doesnt have a spawn brick = probably a real player
//			//if(!isObject(%obj.spawnBrick))
//			//	return;
//
//			//dont re-ride within a certain time
//			if(getSimTime() - %col.lastMountTime <= $Game::MinMountTime)
//				return;
//
//			//must jump up onto the horse
//			%colZpos = getWord(%col.getPosition(), 2);
//			%objZpos = getWord(%obj.getPosition(), 2);
//			if(%colZpos <= %objZpos + 0.2)
//				return;
//
//			//dont get on if someone is already there
//			if(%obj.getMountedObject(0)) //%obj.getMountNodeObject(0))
//				return;
//		
//			if(isObject(%obj.spawnBrick))
//			{
//				%horseClient = %obj.spawnBrick.getGroup().client;
//				%horseGroup = %obj.spawnBrick.getGroup();
//			}
//			else
//			{
//				%horseClient = %obj.client;
//				%horseGroup = %obj.client.brickGroup;
//			}
//
//			%mg = %col.client.miniGame;
//			
//
//			if(!$Server::LAN)
//			{
//
//				if(isObject(%mg))
//				{
//					//person jumping on horse is in a minigame
//
//					//owner of horse must exist
//						if(!isObject(%horseGroup.client))
//							return;
//					//owner of horse must be in this minigame
//						if(%horseGroup.client.miniGame != %mg)
//						{
//							CommandToClient(%col.client,'CenterPrint', "This vehicle is not part of the mini-game.", 2);
//							return;
//						}
//
//					if(%mg.useAllPlayersBricks)
//					{
//						if(%mg.playersUseOwnBricks)
//						{
//							//person jumping on horse must own the horse
//								if(%col.client.brickGroup != %horseGroup)
//								{
//									CommandToClient(%col.client,'CenterPrint', "You do not own this vehicle.", 2);
//									return;
//								}
//						}
//					}
//					else
//					{
//						//owner of horse must exist
//							if(!isObject(%horseGroup.client))
//								return;
//
//						//owner of horse must be the OWNER of this minigame
//							if(isObject(%obj.spawnBrick))	//if it's a vehicle, not a player
//							{
//								if(%horseGroup.client != %mg.owner)
//								{
//									CommandToClient(%col.client,'CenterPrint', "This vehicle is not part of the mini-game.", 2);
//									return;
//								}
//							}
//					}
//				}
//				else
//				{
//					//person jumping on horse is NOT in minigame
//
//					//owner of horse must NOT be in minigame
//						if(isObject(%horseClient))
//						{
//							if(isObject(%horseClient.miniGame))
//							{
//								CommandToClient(%col.client,'CenterPrint', "This vehicle is part of a mini-game.", 2);
//								return;
//							}
//						}
//
//					//person jumping on horse must be trusted by horse builder
//					//(horse builder may not be in the game, so check by brick group)			
//						if(%horseGroup != %col.client.brickGroup)
//						{
//							%trustLevel = %horseGroup.trust[%col.client.bl_id];
//							if(%trustLevel < $TrustLevel::RideVehicle)
//							{
//								CommandToClient(%col.client, 'CenterPrint', %horseGroup.name @ " does not trust you enough to do that.", 2);
//								return;
//							}
//						}
//				}
//			}	//end server::lan check
//		
//
//			%obj.mountObject(%col, 2);
//			%col.setTransform("0 0 0 0 0 1 0");
//
//			//doesnt have a spawn brick = probably a real player
//			if(isObject(%obj.spawnBrick))
//				%col.setControlObject(%obj);
//
//			%col.setActionThread(root,0);
//
//			//if(getSimTime() - %col.lastMountTime > 500)
//			//{
//			//	%colZpos = getWord(%col.getPosition(), 2);
//			//	%objZpos = getWord(%obj.getPosition(), 2);
//			//	if(%colZpos > %objZpos + 0.2)
//			//	{
//			//		if(!%obj.getMountedObject(0)) //%obj.getMountNodeObject(0))
//			//		{
//			//			%obj.mountObject(%col, 2);
//			//			%col.setTransform("0 0 0 0 0 1 0");
//			//			%col.setControlObject(%obj);			
//			//		}
//			//	}
//			//}
//		}
//	}
//}


//called when the driver of a player-vehicle is unmounted
function HorseArmor::onDriverLeave(%obj, %player)
{
	//do nothing
}