//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

// Load dts shapes and merge animations
exec("~/data/shapes/player/player.cs");
// Timeouts for corpse deletion.
$CorpseTimeoutValue = 3000;

// Damage Rate for entering Liquid
$DamageLava       = 10;
$DamageHotLava    = 16;
$DamageCrustyLava = 8;

// Player death animations
$PlayerDeathAnim["default", 1] = "death5";
$PlayerDeathAnim["default", 2] = "death6";
$PlayerDeathAnim["default", 3] = "death7";
//$PlayerDeathAnim["default", 4] = "death8";
//$PlayerDeathAnim["default", 5] = "death9";
$PlayerDeathAnimCount["default"]   = 3;
$PlayerDeathAnim["melee", 1] = "death5";
$PlayerDeathAnim["melee", 2] = "death6";
$PlayerDeathAnim["melee", 3] = "death7";
//$PlayerDeathAnim["melee", 4] = "death8";
//$PlayerDeathAnim["melee", 5] = "death9";
$PlayerDeathAnimCount["melee"]   = 3;
$PlayerDeathAnim["projectile", 1] = "death5";
$PlayerDeathAnim["projectile", 2] = "death6";
$PlayerDeathAnim["projectile", 3] = "death7";
//$PlayerDeathAnim["projectile", 4] = "death8";
//$PlayerDeathAnim["projectile", 5] = "death9";
$PlayerDeathAnimCount["projectile"]   = 3;
$PlayerDeathAnim["explosion", 1] = "debris";
$PlayerDeathAnim["explosion", 2] = "debris";
$PlayerDeathAnim["explosion", 3] = "debris";
//$PlayerDeathAnim["explosion", 4] = "death5";
//$PlayerDeathAnim["explosion", 5] = "death6";
$PlayerDeathAnimCount["explosion"]   = 3;
$PlayerDeathAnim["plasma", 1] = "gooblob";
$PlayerDeathAnimCount["plasma"]   = 1;

//Bodytypes
$BodytypeCount = -1;
$Bodytype[$BodytypeCount++] = "LightMaleHumanArmor";
$Bodytype[$BodytypeCount++] = "MBlue";
$Bodytype[$BodytypeCount++] = "MGreen";
$Bodytype[$BodytypeCount++] = "MRed";
$Bodytype[$BodytypeCount++] = "MYellow";
$Bodytype[$BodytypeCount++] = "MBrown";
$Bodytype[$BodytypeCount++] = "MGray";
$Bodytype[$BodytypeCount++] = "MGrayDark";
$Bodytype[$BodytypeCount++] = "MWhite";
$Bodytype[$BodytypeCount++] = "MDroid";
$Bodytype[$BodytypeCount++] = "MShortie";

//----------------------------------------------------------------------------
// Player Audio Profiles
//----------------------------------------------------------------------------

//----------------------------------------------------------------------------

datablock AudioProfile(jumpSound)
{
   filename    = "~/data/sound/jump.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(DeathCrySound)
{
   fileName = "~/data/sound/death.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(PainCrySound)
{
   fileName = "~/data/sound/pain.wav";
   description = AudioClose3d;
   preload = true;
};

//These are being commented out because they are unused.
//datablock AudioProfile(PredTaunt01)
//{
//   filename    = "~/data/sound/Pred_Taunt_01.wav";
//   description = AudioDefault3d;
//   preload = true;
//};
//
//datablock AudioProfile(PredTaunt02)
//{
//   filename    = "~/data/sound/Pred_Taunt_02.wav";
//   description = AudioClosest3d;
//   preload = true;
//};
//
//datablock AudioProfile(PredPain)
//{
//   filename    = "~/data/sound/Pred_Pain.wav";
//   description = AudioClose3d;
//   preload = true;
//};

//----------------------------------------------------------------------------

//This is being commented out because it doesn't work.
//datablock AudioProfile(FootLightSoftSound)
//{
//   filename    = "~/data/sound/footfall.wav";
//   description = AudioClosest3d;
//   preload = true;
//};

//----------------------------------------------------------------------------
// Splash
//----------------------------------------------------------------------------

datablock ParticleData(PlayerSplashMist)
{
   dragCoefficient      = 2.0;
   gravityCoefficient   = -0.05;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 400;
   lifetimeVarianceMS   = 100;
   useInvAlpha          = false;
   spinRandomMin        = -90.0;
   spinRandomMax        = 500.0;
   textureName          = "~/data/shapes/player/splash";
   colors[0]     = "0.7 0.8 1.0 1.0";
   colors[1]     = "0.7 0.8 1.0 0.5";
   colors[2]     = "0.7 0.8 1.0 0.0";
   sizes[0]      = 10.5;
   sizes[1]      = 10.5;
   sizes[2]      = 10.8;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(PlayerSplashMistEmitter)
{
   ejectionPeriodMS = 5;
   periodVarianceMS = 0;
   ejectionVelocity = 3.0;
   velocityVariance = 2.0;
   ejectionOffset   = 0.0;
   thetaMin         = 85;
   thetaMax         = 85;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   lifetimeMS       = 250;
   particles = "PlayerSplashMist";
};


datablock ParticleData(PlayerBubbleParticle)
{
   dragCoefficient      = 0.0;
   gravityCoefficient   = 1.0; //-0.150;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 1200;
   lifetimeVarianceMS   = 400;
   useInvAlpha          = false;
   textureName          = "~/data/particles/bubble";
   colors[0]     = "0.7 0.8 1.0 0.8";
   colors[1]     = "0.7 0.8 1.0 0.8";
   colors[2]     = "0.7 0.8 1.0 0.0";
   sizes[0]      = 0.2;
   sizes[1]      = 0.2;
   sizes[2]      = 0.2;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(PlayerBubbleEmitter)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   ejectionVelocity = 7.0;
   ejectionOffset   = 0.4;
   velocityVariance = 3.0;
   thetaMin         = 0;
   thetaMax         = 30;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "PlayerBubbleParticle";
};

datablock ParticleData(PlayerFoamParticle)
{
   dragCoefficient      = 0.0;
   gravityCoefficient   = 1.0;
   inheritedVelFactor   = 0.5;
   constantAcceleration = 0.0;
   lifetimeMS           = 400;
   lifetimeVarianceMS   = 100;
   useInvAlpha          = false;
   spinRandomMin        = -90.0;
   spinRandomMax        = 500.0;
   textureName          = "~/data/particles/bubble";
   colors[0]     = "0.7 0.8 1.0 1.0";
   colors[1]     = "0.7 0.8 1.0 0.85";
   colors[2]     = "0.7 0.8 1.0 0.00";
   sizes[0]      = 0.1;
   sizes[1]      = 0.1;
   sizes[2]      = 0.1;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(PlayerFoamEmitter)
{
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;
   ejectionVelocity = 3.0;
   velocityVariance = 1.0;
   ejectionOffset   = 0.4;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "PlayerFoamParticle";
};


datablock ParticleData( PlayerFoamDropletsParticle )
{
   dragCoefficient      = 0.0;
   gravityCoefficient   = 1.0;
   inheritedVelFactor   = 0.5;
   constantAcceleration = -0.0;
   lifetimeMS           = 1600;
   lifetimeVarianceMS   = 0;
   textureName          = "~/data/particles/bubble";
   colors[0]     = "0.7 0.8 1.0 1.0";
   colors[1]     = "0.7 0.8 1.0 0.85";
   colors[2]     = "0.7 0.8 1.0 0.0";
   sizes[0]      = 0.15;
   sizes[1]      = 0.15;
   sizes[2]      = 0.15;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData( PlayerFoamDropletsEmitter )
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   ejectionVelocity = 2;
   velocityVariance = 1.0;
   ejectionOffset   = 0.4;
   thetaMin         = 40;
   thetaMax         = 70;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   orientParticles  = false;
   particles = "PlayerFoamDropletsParticle";
};


datablock ParticleData( PlayerSplashParticle )
{
   dragCoefficient      = 1;
   gravityCoefficient   = 0.2;
   inheritedVelFactor   = 0.2;
   constantAcceleration = -0.0;
   lifetimeMS           = 600;
   lifetimeVarianceMS   = 0;
   colors[0]     = "0.7 0.8 1.0 1.0";
   colors[1]     = "0.7 0.8 1.0 0.5";
   colors[2]     = "0.7 0.8 1.0 0.0";
   sizes[0]      = 10.5;
   sizes[1]      = 10.5;
   sizes[2]      = 10.5;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData( PlayerSplashEmitter )
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   ejectionVelocity = 3;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 60;
   thetaMax         = 80;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   orientParticles  = true;
   lifetimeMS       = 100;
   particles = "PlayerSplashParticle";
};

datablock SplashData(PlayerSplash)
{
   numSegments = 15;
   ejectionFreq = 15;
   ejectionAngle = 40;
   ringLifetime = 0.5;
   lifetimeMS = 300;
   velocity = 4.0;
   startRadius = 0.0;
   acceleration = -3.0;
   texWrap = 5.0;

   texture = "~/data/shapes/player/splash";

   emitter[0] = PlayerSplashEmitter;
   emitter[1] = PlayerSplashMistEmitter;

   colors[0] = "0.7 0.8 1.0 0.0";
   colors[1] = "0.7 0.8 1.0 0.3";
   colors[2] = "0.7 0.8 1.0 0.7";
   colors[3] = "0.7 0.8 1.0 0.0";
   times[0] = 0.0;
   times[1] = 0.4;
   times[2] = 0.8;
   times[3] = 1.0;
};


//----------------------------------------------------------------------------
// Foot puffs
//----------------------------------------------------------------------------

datablock ParticleData(LightPuff)
{
   dragCoefficient      = 2.0;
   gravityCoefficient   = -0.01;
   inheritedVelFactor   = 0.6;
   constantAcceleration = 0.0;
   lifetimeMS           = 800;
   lifetimeVarianceMS   = 100;
   useInvAlpha          = true;
   spinRandomMin        = -35.0;
   spinRandomMax        = 35.0;
   colors[0]     = "1.0 1.0 1.0 1.0";
   colors[1]     = "1.0 1.0 1.0 0.0";
   sizes[0]      = 0.1;
   sizes[1]      = 0.8;
   times[0]      = 0.3;
   times[1]      = 1.0;
   textureName = "~/data/particles/cloud";
};

datablock ParticleEmitterData(LightPuffEmitter)
{
   ejectionPeriodMS = 35;
   periodVarianceMS = 10;
   ejectionVelocity = 0.2;
   velocityVariance = 0.1;
   ejectionOffset   = 0.0;
   thetaMin         = 20;
   thetaMax         = 60;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   useEmitterColors = true;
   particles = LightPuff;
};

//----------------------------------------------------------------------------
// Liftoff dust
//----------------------------------------------------------------------------

datablock ParticleData(LiftoffDust)
{
   dragCoefficient      = 1.0;
   gravityCoefficient   = -0.01;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 1000;
   lifetimeVarianceMS   = 100;
   useInvAlpha          = true;
   spinRandomMin        = -90.0;
   spinRandomMax        = 500.0;
   colors[0]     = "1.0 1.0 1.0 1.0";
   sizes[0]      = 1.0;
   times[0]      = 1.0;
   textureName = "~/data/particles/cloud";
};

datablock ParticleEmitterData(LiftoffDustEmitter)
{
   ejectionPeriodMS = 5;
   periodVarianceMS = 0;
   ejectionVelocity = 2.0;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 90;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   useEmitterColors = true;
   particles = LiftoffDust;
};


//----------------------------------------------------------------------------

//datablock DecalData(LightMaleFootprint)
//{
//   sizeX       = 0.125;
//   sizeY       = 0.25;
//   textureName = "~/data/shapes/player/footprint";
//};

datablock DebrisData( PlayerDebris )
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

datablock PlayerData(LightMaleHumanArmor)
{
   renderFirstPerson = true;
   emap = true;
   
   className = Armor;
   shapeFile = "~/data/shapes/player/minifig.dts";
   cloaktexture = "tbm/data/shapes/base.transmore";
   cameraMaxDist = 6;
   computeCRC = false;
  
   canObserve = true;
   cmdCategory = "Clients";

   cameraDefaultFov = 90.0;
   cameraMinFov = 5.0;
   cameraMaxFov = 120.0;
   
   debrisShapeName = "~/data/shapes/player/debris_player.dts";
   debris = playerDebris;

   aiAvoidThis = true;

   minLookAngle = -1.5708;
   maxLookAngle = 1.5708;
   maxFreelookAngle = 3.0;

   mass = $Pref::Server::Jet;
   drag = 0.1;
   maxdrag = 0.2;
   density = 0.7;
   maxDamage = 100;
   maxEnergy =  60;
   repairRate = 1000;
   energyPerDamagePoint = 0.0;

   rechargeRate = 1000;

   runForce = 48 * 90;
   runEnergyDrain = 0;
   minRunEnergy = 0;
   maxForwardSpeed = 12;
   maxBackwardSpeed = 7;
   maxSideSpeed = 10;

   maxForwardCrouchSpeed = 3;
   maxBackwardCrouchSpeed = 2;
   maxSideCrouchSpeed = 2;

   maxForwardProneSpeed = 4;
   maxBackwardProneSpeed = 3;
   maxSideProneSpeed = 3;

   maxForwardWalkSpeed = 7;
   maxBackwardWalkSpeed = 6;
   maxSideWalkSpeed = 6;

   maxUnderwaterForwardSpeed = 8.4;
   maxUnderwaterBackwardSpeed = 7.8;
   maxUnderwaterSideSpeed = 7.8;

   jumpForce = 600; //8.3 * 90;
   jumpEnergyDrain = 0;
   minJumpEnergy = 0;
   jumpDelay = 0;

   recoverDelay = 9;
   recoverRunForceScale = 1.2;

   minImpactSpeed = 999;
   speedDamageScale = 0;

   boundingBox = "1.25 1.25 2.65";
   crouchBoundingBox = "1.25 1.25 1";
   proneBoundingBox = "1 2.3 1";
   waistHeight = 1.01;
   neckHeight = 1.8;

   pickupRadius = 2;//0.75;
   
   // Damage location details
   boxNormalHeadPercentage       = 0.83;
   boxNormalTorsoPercentage      = 0.49;
   boxHeadLeftPercentage         = 0;
   boxHeadRightPercentage        = 1;
   boxHeadBackPercentage         = 0;
   boxHeadFrontPercentage        = 1;

   // Foot Prints
   decalData   = LightMaleFootprint;
   decalOffset = 0.25;
   
  // footPuffEmitter = bluePaintExplosionEmitter; //LightPuffEmitter;
  // footPuffNumParts = 10;
  // footPuffRadius = 0.25;

   dustEmitter = LiftoffDustEmitter;

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
   runSurfaceAngle  = 70;
   jumpSurfaceAngle = 80;

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

   JumpSound			= jumpSound;

   // Footstep Sounds
   FootSoftSound        = FootLightSoftSound;
   FootHardSound        = FootLightSoftSound;
   FootMetalSound       = FootLightSoftSound;
   FootSnowSound        = FootLightSoftSound;
   FootShallowSound     = FootLightSoftSound;
   FootWadingSound      = FootLightSoftSound;
   FootUnderwaterSound  = FootLightSoftSound;
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
	maxItems   = 20;	//total number of usable things you can carry including weapons
	maxWeapons = 20;
	dynamicType = $TypeMasks::StationObjectType;

   // For connected clients to display in the Options dialog
	name = "Black";
};


//----------------------------------------------------------------------------
// Armor Datablock methods
//----------------------------------------------------------------------------

//----------------------------------------------------------------------------

function Armor::onAdd(%this,%obj)
{
   // Vehicle timeout
   %obj.mountVehicle = true;

   // Default dynamic armor stats
   %obj.setRechargeRate(%this.rechargeRate);
   %obj.setRepairRate(0);

   //Add to PlayerSet so things (mainly mover switches) can use it
   PlayerSet.add(%obj);
}

function Armor::onRemove(%this, %obj)
{
   if (%obj.client.player == %obj)
      %obj.client.player = 0;
}

function Armor::onNewDataBlock(%this,%obj)
{
}


//----------------------------------------------------------------------------

function Armor::onMount(%this,%obj,%vehicle,%node)
{
   if (%node == 0)  {
      %obj.setTransform("0 0 0 0 0 1 0");
   //   %obj.setActionThread(%vehicle.getDatablock().mountPose[%node],true,true);
   //   %obj.lastWeapon = %obj.getMountedImage($WeaponSlot);

   //   %obj.unmountImage($WeaponSlot);
        %obj.setControlObject(%vehicle);
   //   %obj.client.setObjectActiveImage(%vehicle, 2);
   }
}

function Armor::onUnmount( %this, %obj, %vehicle, %node )
{
  // if (%node == 0)
   //   %obj.mountImage(%obj.lastWeapon, $WeaponSlot);

}

function Armor::doDismount(%this, %obj, %forced)
{
return;
   // This function is called by player.cc when the jump trigger
   // is true while mounted
   if (!%obj.isMounted())
      return;

   // Position above dismount point
   %pos    = getWords(%obj.getTransform(), 0, 2);
   %oldPos = %pos;
   %vec[0] = " 0  0  1";
   %vec[1] = " 0  0  1";
   %vec[2] = " 0  0 -1";
   %vec[3] = " 1  0  0";
   %vec[4] = "-1  0  0";
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

   %obj.mountVehicle = false;
   %obj.schedule(4000, "mountVehicles", true);
   
   // Mount last weapon used, unmount from node, and give player control.
   %obj.unMount();
   %this.onUnMount(%obj);
   %obj.setControlObject(%obj);   

   // Position above dismount point
   %obj.setTransform(%pos);
   %obj.applyImpulse(%pos, VectorScale(%impulseVec, %obj.getDataBlock().mass));
   %obj.setPilot(false);
   %obj.vehicleTurret = "";
}


//----------------------------------------------------------------------------

function Armor::onCollision(%this,%obj,%col,%vec,%speed)
{
//echo("armor collided" SPC %col.getDatablock());
//if(%col.getDatablock())
// ad();
	//echo("obj = ", %obj);
	//echo("col = ", %col);

   if (%obj.getState() $= "Dead")
      return;

	//messageAll('asom', "hit something!");
	//echo("hit ", %col.getClassName());
   // Try and pickup all items
	if (%col.getClassName() $= "Item")
	{
		//echo("calling pickup ", %col.thrower);
		//echo("pickup obj = ", %obj);
		if(%col.thrower != %obj)
		{
			%obj.pickup(%col);
		}
	}

	//if (%col.getDataBlock().className $= 'Weapon')
	//	%obj.pickup(%col);

   // Mount vehicles
//   %this = %col.getDataBlock();
//   if ((%this.className $= WheeledVehicleData) && %obj.mountVehicle &&
//         %obj.getState() $= "Move" && %col.mountable) {
//
//      // Only mount drivers for now.
//      %node = 0;
//      %col.mountObject(%obj,%node);
//      %obj.mVehicle = %col;
//   }
}

function Armor::onImpact(%this, %obj, %collidedObject, %vec, %vecLen)
{
	//echo("armor impact");
	
   %obj.deathAnim = "debris";

   %x = mSqrt(mPow(getWord(%vec, 0), 2) + mPow(getWord(%vec, 1), 2));
   if(%x > mAbs(getWord(%vec, 2)))
      %type = "SideImpact";
   else
      %type = getWord(%vec, 2) > 0 ? "DownImpact" : "UpImpact";

   %obj.damage(%obj, VectorAdd(%obj.getPosition(),%vec),
      %vecLen * %this.speedDamageScale, %type);
   %obj.deathAnim = "";

      %time = ( ( (%vecLen - 10) / 40) * 6) + 2;

      %time = %time * 1000;
	if(%time > 8000)
		%time = 8000;
	if(%time < 2000)
		%time = 2000;

//	tumble(%obj, %time);
      
}


//----------------------------------------------------------------------------

function Armor::damage(%this, %obj, %sourceObject, %position, %damage, %damageType) {
   if (%obj.getState() $= "Dead")
      return;
   %obj.deathAnim = %sourceObject.deathAnim;
   if(%damageType $= "Radiation")
      if(getRandom(5) == 0)
         %obj.deathAnim = "zombify";
   %obj.damageSourcePos = %position; //So onDamage has access to the position
   %obj.applyDamage(%damage);
   %obj.damageSourcePos = "";
   %location = "Body";

   // Deal with client callbacks here because we don't have this
   // information in the onDamage or onDisable methods
   if ( %obj.getClassName() $= "AIPlayer" )
     {
     %client = %obj;
     }
   else
     {
     %client = %obj.client;
     }
   %sourceClient = %sourceObject ? %sourceObject.client : 0;

   if (%obj.getState() $= "Dead" && !%obj.zombified)
      %client.onDeath(%sourceObject, %sourceClient, %damageType, %location);
   %obj.deathAnim = "";
}

function Armor::onDamage(%this, %obj, %delta)
{
  // This method is invoked by the ShapeBase code whenever the 
  // object's damage level changes.
   if (%delta > 0 && %obj.getState() !$= "Dead") {
      %client = %obj.client;
      if (%client.bodytype==666 && %delta > 5) {
        %obj.setcloaked(0);
        cancel(%client.restorecloak);
        %client.restorecloak=%client.player.schedule(5000,setcloaked,1);
        }
      // Increment the flash based on the amount.
      %flash = %obj.getDamageFlash() + ((%delta / %this.maxDamage) * 2);
      if (%flash > 0.75)
         %flash = 0.75;
      %obj.setDamageFlash(%flash, %obj.damageSourcePos !$= "" ? %obj.getPointSightZone(%obj.damageSourcePos) : "");

      // If the pain is excessive, let's hear about it.
      if (%delta > 10)
         %obj.playPain();
   }
}

function Armor::onDisabled(%this,%obj,%state)
{
   // The player object sets the "disabled" state when damage exceeds
   // it's maxDamage value.  This is method is invoked by ShapeBase
   // state mangement code.

   // If we want to deal with the damage information that actually
   // caused this death, then we would have to move this code into
   // the script "damage" method.
   %obj.playDeathCry();
   %obj.playDeathAnimation();
   %obj.setDamageFlash(0.75);
   %obj.setRepairRate(0);     //A repair rate greater than 0 will keep resetting getState to Move

   // Release the main weapon trigger
   %obj.setImageTrigger(0,false);

	//Remove the working brick
	if(%obj.tempBrick > 0){
		%obj.tempBrick.delete();
		%obj.tempBrick = 0;
	}

   // Schedule corpse removal.  Just keeping the place clean.
   if(!%obj.zombified) {
     %obj.startfade($CorpseTimeoutValue - 10, 0, true);
     %obj.schedule($CorpseTimeoutValue, "delete");
   }
}


//-----------------------------------------------------------------------------

function Armor::onLeaveMissionArea(%this, %obj)
{
   // Inform the client
   %obj.client.onLeaveMissionArea();
}

function Armor::onEnterMissionArea(%this, %obj)
{
   // Inform the client
   %obj.client.onEnterMissionArea();
}

//-----------------------------------------------------------------------------

function Armor::onEnterLiquid(%this, %obj, %coverage, %type)
{
   switch(%type)
   {
      case 0: //Water
	   %obj.extinguish();
      case 1: //Ocean Water
	   %obj.extinguish();
      case 2: //River Water
	   %obj.extinguish();
      case 3: //Stagnant Water
	   %obj.extinguish();
      case 4: //Lava
         %obj.setDamageDt($DamageLava, "Lava");
      case 5: //Hot Lava
         %obj.setDamageDt($DamageHotLava, "Lava");
      case 6: //Crusty Lava
         %obj.setDamageDt($DamageCrustyLava, "Lava");
      case 7: //Quick Sand
	   if(%obj.getmountedimage(0) $= "flameimage") %obj.unmountimage(0);
   }
%obj.inWater = 1;
%obj.waterType = %type;
}

function Armor::onLeaveLiquid(%this, %obj, %type) {
%obj.clearDamageDt(%obj);
%obj.inWater = 0;
%obj.waterType = 0;
}

function Player::Extinguish(%this) {
%this.clearDamageDt();
if(%this.getMountedImage(0) == nameToID(flameSmallImage) || %this.getMountedImage(0) == nameToID(flameImage) || %this.getMountedImage(0) == nameToID(flameBigImage) || %this.getMountedImage(0) == nameToID(smokeImage) || %this.getMountedImage(0) == nameToID(smokeBigImage))
  %this.unmountImage(0);
}

function AIPlayer::Extinguish(%this) {
%this.clearDamageDt();
if(%this.getMountedImage(0) == nameToID(flameImage))
  %this.unmountImage(0);
}

//-----------------------------------------------------------------------------

function Armor::onTrigger(%this, %obj, %triggerNum, %val)
{
   // This method is invoked when the player receives a trigger
   // move event.  The player automatically triggers slot 0 and
   // slot one off of triggers # 0 & 1.  Trigger # 2 is also used
   // as the jump key.
}


//-----------------------------------------------------------------------------
// Player methods
//-----------------------------------------------------------------------------

//----------------------------------------------------------------------------

function Player::kill(%this, %damageType)
{
   %this.damage(0, %this.getPosition(), 10000, %damageType);
}


//----------------------------------------------------------------------------

function Player::mountVehicles(%this,%bool)
{
   // If set to false, this variable disables vehicle mounting.
   %this.mountVehicle = %bool;
}

function Player::isPilot(%this)
{
   %vehicle = %this.getObjectMount();
   // There are two "if" statements to avoid a script warning.
   if (%vehicle)
      if (%vehicle.getMountNodeObject(0) == %this)
         return true;
   return false;
}


//----------------------------------------------------------------------------

function Player::playDeathAnimation(%this) {
if(%this.deathAnim $= "")
  %this.deathAnim = "default";
if($PlayerDeathAnimCount[%this.deathAnim] $= "")
  %d = %this.deathAnim;
else
  %d = $PlayerDeathAnim[%this.deathAnim, getRandom(1, $PlayerDeathAnimCount[%this.deathAnim])];
if(%d $= "delete") {
  %this.schedule(10, startFade, 0, 0, 1);
  return;
}
if(%d $= "debris") {
  schedule(10, 0, tbmplayerdebris, %this); 
  %this.schedule(10, startFade, 0, 0, 1);
  return;
}
if(%d $= "zombify") {
  if(%this.getMountedImage(0) == nameToID(zombieImage) || %this.getDatablock() == nameToID(MDroid))
    %d = "death5";
  else {
    %this.zombified = 1;
    %this.controllable = 1;
    %this.playThread(0, death5);
    %this.schedule(5000, playThread, 0, root);
    %this.schedule(5001, mountImage, zombieImage, 0);
    %this.schedule(5002, playThread, 0, getUp);
    schedule(5002, 0, eval, %this @ ".controllable = 0;");
    %this.schedule(8000, playThread, 0, root);
    schedule(1, 0, eval, %this @ ".zombified = 0;");
    if(%this.getClassName() $= "AIPlayer") {
      %this.setMoveSpeed(0);
      %this.schedule(6000, setMoveSpeed, 1);
    }
    return;
  }
}
if(%d $= "gooblob" || %d $= "greengooblob") {
  %x = new item() {
    static = "false";
    rotate = "false";
    position = %this.getPosition();
    rotation = "1 0 0 0";
    scale = %this.getScale();
    dataBlock = gooBlob;
  };
  MissionCleanup.add(%x);
  %x.startFade($CorpseTimeoutValue - 10, 0, 0);
  if(%d $= "gooblob")
    %x.setSkinName(%this.getSkinName());
  else {
    %x.setSkinName('neongreen');
    tbmBoneDebris(%this);
  }
  %x.schedule(10000, delete);
  %x.schedule(6000, startFade, 0, 0, 1);
  %x.setVelocity(%this.getVelocity());
  %x.setCollisionTimeout(%this);
  %d = "melt";
}
if(%d $= "headdecap") {
  %d = "death8";
  if(%this.getDatablock() != nameToID(MDroid) && %this.getMountedImage($backSlot) != nameToID(R2ShowImage)) {
    %color = "yellow";
    if(%this.getDatablock() == nameToID("MGrayDark"))
      %color = "brown";
    if(%this.getDatablock() == nameToID("MZombie") || %this.getDatablock() == nameToID("MZombie2") || %this.getDatablock() == nameToID("MZombie3"))
      %color = "white";
    %head = new(item)() {
      position = %this.getEyeTransform();
      rotation = getWords(%this.getTransform(), 3, 6);
      scale = %this.getScale();
      datablock = skelhead;			
    };
    MissionCleanup.add(%head);
    %head.setTransform(%this.getEyeTransform() SPC getWords(%this.getTransform(), 3, 6));
    %head.setSkinName(addTaggedString(%Color));
    %head.setVelocity(getRandom(-2, 2) SPC getRandom(-2, 2) SPC getRandom(3, 5));
    if(%this.client.headCode !$= "")
      %head.mountImage(%this.client.headCode, $headSlot, 1, %this.client.headCodeColor);
    if(%this.client.visorCode !$= "")
      %head.mountImage(%this.client.visorCode, $visorSlot, 1, %this.client.visorCodeColor);
    if(%this.client.leftHandCode $= "BeardShowImage")
      %head.mountImage(BeardShowImage, $leftHandSlot, 1, %this.client.leftHandCodeColor);
    if(%color !$= "white")
      %head.mountImage(faceplateShowImage, $faceSlot, 1, %this.client.faceDecalCode);
    %head.deleteSchedule = %head.schedule(10000, delete);
    if(%this.getClassName() $= "Player")
      %this.client.camera.schedule(3, setOrbitMode, %head, %head.getTransForm(), 2, 5, 3);
  }
}
if(%d $= "axe") {
  %this.mountImage(ThrowAxeMountImage, 4);
  %d = $PlayerDeathAnim["default", getRandom(1, $PlayerDeathAnimCount["default"])];
}
%this.playThread(0, %d);
}

function AIPlayer::playDeathAnimation(%this) {
if(%this.deathAnimation) return;
%this.deathAnimation = 1;
schedule(1000, 0, eval, %this @ ".deathAnimation = 0;");
%this.stop();
%this.clearAim();
Player::playDeathAnimation(%this);
}

function Player::playCelAnimation(%this,%anim)
{
   if (%this.getState() !$= "Dead")
      %this.setActionThread("cel"@%anim);
}

datablock ItemData(gooBlob) {
  shapeFile = "~/data/shapes/blob.dts";
  cloaktexture = "~/data/specialfx/cloakTexture";
  mass = 1;
  friction = 1;
  elasticity = 0;
  rotate = false;
  maxInventory = 0;
  pickUpName = '';
  invName = '';
  dynamicType = $TypeMasks::StationObjectType;
};

function gooBlob::onPickup(%this,%obj,%user,%amount)
{
if (%user.client.bodytype==666)//this isn't even useful anymore, but meh
  %user.applyRepair(50);
}

//----------------------------------------------------------------------------

function Player::playDeathCry( %this )
{
   %client = %this.client;

   //playTargetAudio( %client.target, "replaceme", AudioClosest3d, false );
  if (%client.bodytype == 666)
    %this.playAudio(0,PredTaunt01);
  else
   %this.playAudio(0,DeathCrySound);
}

function Player::playPain( %this )
{
   %client = %this.client;
  // playTargetAudio( %client.target, "replaceme", AudioClosest3d, false);
  if (%client.bodytype == 666)
    %this.playAudio(0,PredPain);
  else
    %this.playAudio(0,PainCrySound);
}


function fixArmReady(%obj)
{
	%leftImage = %obj.getMountedImage($LeftHandSlot);
	%rightImage = %obj.getMountedImage($RightHandSlot);

	%leftReady = 0;
	%rightReady = 0;

	if(%leftImage)
		%leftReady = %leftImage.armReady;

	if(%rightImage)
		%rightReady = %rightImage.armReady;
	
	if(%rightReady)
	{
		if(%leftReady)
			%obj.playThread(1, armReadyBoth);
		else
			%obj.playThread(1, armReadyRight);
	}
	else
	{
		if(%leftReady)
			%obj.playThread(1, armReadyLeft);
		else
			%obj.playThread(1, root);
	}
}

function tumble(%obj, %time)
{
	//mount the object on a new deathvehicle for %time milliseconds
	%currentVehicle = %obj.getObjectMount();
	%client = %obj.client;

	%newcar = new WheeledVehicle() 
	{
		dataBlock = deathVehicle;
		client = %client;
		initialPosition = %posX @ " " @ %posY @ " " @ %posZ;
	};
	

	 %client.tumbleVehicle = %newcar;
	%newcar.setVelocity("0 0 0");

	if(!%newcar)
		return;
	
	//neutralize current velocity
	%newcar.applyImpulse( %newcar.getPosition(), vectorScale(%newcar.getVelocity() * -1, %newcar.getDataBlock().mass) );

	//error("player tumbling!");

	if(%currentVehicle && (%currentVehicle.getDataBlock().getName() $= "skiVehicle") )
	{
		//%obj.client.setControlObject(%obj);
		//match ski vehicle
		//neutralize velocity first
		
		%newcar.setTransform(%currentVehicle.getTransform());
		%newcar.applyImpulse( %newcar.getPosition(), vectorScale(%currentVehicle.getVelocity(), %newcar.getDataBlock().mass) );
		%newcar.mountObject(%obj, 0);

		%currentVehicle.setTransform("0 0 -1000");
		%currentVehicle.schedule(500, delete);
	}
	else
	{
		//match player
		error("transform = ",%obj.getTransform());
		%newcar.setTransform(%obj.getTransform());
		%newcar.applyImpulse( %newcar.getPosition(), vectorScale(%obj.getVelocity(), %newcar.getDataBlock().mass) );
		//%newcar.setTransform(%obj.getTransform());

		

		//%obj.setTransform("0 0 0 0 0 1 0");

		%newcar.mountObject(%obj, 0);

		

		//error("not skiing");
		//error("transform = ",%obj.getTransform());
	}	

	
	%newcar.schedule(%time, delete);


	%nextTumbleVehicle = new WheeledVehicle() 
	{
		dataBlock = deathVehicle;
		client = %client;
		initialPosition = %posX @ " " @ %posY @ " " @ %posZ;
	};
	%obj.client.tumbleVehicle = %nextTumbleVehicle;
	%nextTumbleVehicle.setTransform("0 0 -90");

	//%newcar.schedule(%time, unmountobject, %obj);
	//%obj.schedule(%time, setcontrolobject, 0);
	//%newcar.schedule(%time + 250, setTransform, "0 0 -90");

	
}

function tumble2(%obj, %time)
{
	//mount the object on a new deathvehicle for %time milliseconds
	%currentVehicle = %obj.getObjectMount();
	%client = %obj.client;

	//tried a couple different ideas for improving the physics on bricks,
	//but none of them worked, so I'm commenting everything out -DShiznit

	//%objData = %obj.getDataBlock();
	//%objDataClass = %objData.classname;
	//%objlastscale = %obj.getscale();

	//%xhack = getword(%obj.getscale(), 0); 
	//%yhack = getword(%obj.getscale(), 1);
	//%zhack = getword(%obj.getscale(), 2);

	//error("brick scale = ",%xhack SPC %yhack SPC %zhack);

	//%xscale = %xhack * %objdata.x / 3;
	//%yscale = %yhack * %objdata.y / 3;
	//%zscale = %zhack * %objdata.z / 13;

	//error("tumble scale = ",%xscale SPC %yscale SPC %zscale);

	//%hackdatablock = deathVehicle;
	//%hackdatablock.massBox = %xscale SPC %yscale SPC %zscale;
	//%hackdatablock.massCenter = %xscale / 2 SPC %yscale / 2 SPC %zscale / 2;

	%newcar = new WheeledVehicle() 
	{
		dataBlock = deathVehicle; //%hackdatablock;
		client = %client;
		initialPosition = %posX @ " " @ %posY @ " " @ %posZ;
	};

	//%newcar = %client.tumbleVehicle;
	%newcar.setVelocity("0 0 0");
	%newcar.setTransform(%obj.gettransform());

	if(!%newcar)
		return;
	
	//neutralize current velocity
	%newcar.applyImpulse( %newcar.getPosition(), vectorScale(%newcar.getVelocity() * -1, %newcar.getDataBlock().mass) );

	//error("player tumbling!");

	if(%currentVehicle && (%currentVehicle.getDataBlock().getName() $= "skiVehicle") )
	{
		//%obj.client.setControlObject(%obj);
		//match ski vehicle
		//neutralize velocity first
		
		%newcar.setTransform(%currentVehicle.getTransform());
		%newcar.applyImpulse( %newcar.getPosition(), vectorScale(%currentVehicle.getVelocity(), %newcar.getDataBlock().mass) );
		%newcar.mountObject(%obj, 0);

		%currentVehicle.setTransform("0 0 -1000");
		%currentVehicle.schedule(500, delete);
	}
	else
	{
		//match player
		error("transform = ",%obj.getTransform());
		//%newcar.setTransform(%obj.getTransform());
		%newcar.applyImpulse( %newcar.getPosition(), vectorScale(%obj.getVelocity(), %newcar.getDataBlock().mass) );
		//%newcar.setTransform(%obj.getTransform());

		

		//%obj.setTransform("0 0 0 0 0 1 0");

		%newcar.mountObject(%obj, 0);
		//error("not skiing");
		//error("transform = ",%obj.getTransform());
	}	

	
	%newcar.schedule(%time, delete);

	%nextTumbleVehicle = new WheeledVehicle() 
	{
		dataBlock = deathVehicle;
		client = %client;
		initialPosition = %posX @ " " @ %posY @ " " @ %posZ;
	};
	%obj.client.tumbleVehicle = %nextTumbleVehicle;
	%nextTumbleVehicle.setTransform("0 0 -90");

	//%newcar.schedule(%time, unmountobject, %obj);
	//%obj.schedule(%time, setcontrolobject, 0);
	//%newcar.schedule(%time + 250, setTransform, "0 0 -90");

	
}

function IceThatNigga(%obj, %time)
{
	%currentVehicle = %obj.getObjectMount();
	%client = %obj.client;

	%newblok = new StaticShape() 
	{
		dataBlock = staticIceBlock;
		client = %client;
		initialPosition = %posX @ " " @ %posY @ " " @ %posZ; //don't get confused by these, they're meaningless
	};

	//%newblok = new AIPlayer() //This didn't work, the physics got screwy, so for now it's commented out
	//{
	//	dataBlock = IceBlok;
	//	client = %client;
	//	initialPosition = %posX @ " " @ %posY @ " " @ %posZ; //don't get confused by these, they're meaningless
	//};
	
	%newBlok.setScale(%scale = %obj.getScale());
	%obj.setScale("1 1 1");
	%obj.schedule(%time, setScale, %scale);

	//%newblok.setVelocity("0 0 1");
	%newblok.setTransform(%obj.gettransform());

	if(!%newblok)
		return;
		
	//%newblok.applyImpulse( %newblok.getPosition(), vectorScale(%newblok.getVelocity() * -1, %newblok.getDataBlock().mass) );
	if(%currentVehicle && (%currentVehicle.getDataBlock().getName() $= "skiVehicle") )
	{
		%newblok.setTransform(%currentVehicle.getTransform());
		//%newblok.applyImpulse( %newblok.getPosition(), vectorScale(%currentVehicle.getVelocity(), %newblok.getDataBlock().mass) );
		%newblok.mountObject(%obj, 0);

		%currentVehicle.setTransform("0 0 -1000");
		%currentVehicle.schedule(500, delete);
	}
	else
	{
		error("transform = ",%obj.getTransform());
		//%newblok.applyImpulse( %newblok.getPosition(), vectorScale(%obj.getVelocity(), %newblok.getDataBlock().mass) );
		%newblok.mountObject(%obj, 0);
	}	

	%newblok.schedule(%time, delete);
	%obj.isOnIce = 1;
	schedule(%time, 0, eval, %obj @ ".isOnIce = 0;");
}

datablock PlayerData(MBlue : LightMaleHumanArmor) 
{
   shapeFile = "~/data/shapes/player/mBlue/minifig.dts";
   name = "Blue";
};
datablock PlayerData(MGreen : LightMaleHumanArmor) 
{
   shapeFile = "~/data/shapes/player/mGreen/minifig.dts";
   name = "Green";
};
datablock PlayerData(MRed : LightMaleHumanArmor) 
{
   shapeFile = "~/data/shapes/player/mRed/minifig.dts";
   name = "Red";
};
datablock PlayerData(MYellow : LightMaleHumanArmor) 
{
   shapeFile = "~/data/shapes/player/mYellow/minifig.dts";
   name = "Yellow";
};
datablock PlayerData(MBrown : LightMaleHumanArmor) 
{
   shapeFile = "~/data/shapes/player/mBrown/minifig.dts";
   name = "Brown";
};
datablock PlayerData(MGray : LightMaleHumanArmor) 
{
   shapeFile = "~/data/shapes/player/mGray/minifig.dts";
   name = "Gray";
};
datablock PlayerData(MGrayDark : LightMaleHumanArmor) 
{
   shapeFile = "~/data/shapes/player/mGrayDark/minifig.dts";
   name = "Dark Gray";
};
datablock PlayerData(MWhite : LightMaleHumanArmor) 
{
   shapeFile = "~/data/shapes/player/mWhite/minifig.dts";
   name = "White";
};
datablock PlayerData(MZombie : LightMaleHumanArmor) 
{
   shapeFile = "~/data/shapes/player/mZombie/minifig.dts";
   maxForwardSpeed = 9;
   maxBackwardSpeed = 5.25;
   maxSideSpeed = 7.5;
   name = "Zombie";
};

datablock PlayerData(MZombie2 : LightMaleHumanArmor) 
{
   shapeFile = "~/data/shapes/player/mZombie2/minifig.dts";
   maxForwardSpeed = 9;
   maxBackwardSpeed = 5.25;
   maxSideSpeed = 7.5;
   name = "Zombie 2";
};

datablock PlayerData(MZombie3 : LightMaleHumanArmor) 
{
   shapeFile = "~/data/shapes/player/mZombie3/minifig.dts";
   maxForwardSpeed = 9;
   maxBackwardSpeed = 5.25;
   maxSideSpeed = 7.5;
   name = "Zombie 3";
};

datablock PlayerData(MDroid : LightMaleHumanArmor) 
{
   shapeFile = "~/data/shapes/player/mDroid/droid.dts";
   name = "Droid";
   maxFreelookAngle = 0.0;
   
   boundingBox = "1 1 2.90";
   crouchBoundingBox = "1 1 1";
   waistHeight = 1.1;
   neckHeight = 2;
};

datablock PlayerData(MShortie : LightMaleHumanArmor) 
{
   shapeFile = "~/data/shapes/player/mShortie/Shortie.dts";
   name = "Shortie";
   maxStepHeight = 0.6;
   maxForwardSpeed = 10.8;
   maxBackwardSpeed = 6.3;
   maxSideSpeed = 9;
   maxForwardCrouchSpeed = 3.6;
   maxBackwardCrouchSpeed = 2.4;
   maxSideCrouchSpeed = 2.4;
   
   boundingBox = "1.25 1.25 2.25";
   waistHeight = 0.55;
   neckHeight = 1.4;
};

datablock ItemData(skelarmr)
{
   shapeFile = "~/data/shapes/right-arm.dts";
   category = "debris";
   mass = 1;
   friction = 1;
   elasticity = 0.5;
   rotate = true;
   maxInventory = 0;
   pickUpName = '';
   invName = '';
};

function skelarmr::onPickup(%this,%obj,%user,%amount)
{
if (%user.client.bodytype==666)
  %user.applyRepair(10);
}

datablock ItemData(skelarml)
{
   shapeFile = "~/data/shapes/left-arm.dts";
   category = "debris";
   mass = 1;
   friction = 1;
   elasticity = 0.5;
   rotate = true;
   maxInventory = 0;
   pickUpName = '';
   invName = '';
};

function skelarml::onPickup(%this,%obj,%user,%amount)
{
if (%user.client.bodytype==666)
  %user.applyRepair(10);
}

datablock ItemData(skelleg)
{
   shapeFile = "~/data/shapes/leg.dts";
   category = "debris";
   mass = 1;
   friction = 1;
   elasticity = 0.5;
   rotate = true;
   maxInventory = 0;
   pickUpName = '';
   invName = '';
};

function skelleg::onPickup(%this,%obj,%user,%amount)
{
if (%user.client.bodytype==666)
  %user.applyRepair(10);
}

datablock ItemData(skelhead)
{
   shapeFile = "~/data/shapes/headobject.dts";
   category = "debris";
   mass = 1;
   skinName = 'white';
   friction = 0.5;
   elasticity = 0.5;
   rotate = true;
   maxInventory = 0;
   pickUpName = '';
   invName = '';
   density = 0.2;
};

function skelhead::onPickup(%this,%obj,%user,%amount) {
%obj.setVelocity(vectorAdd("0 0 5", vectorAdd(%obj.getVelocity(), vScale(%user.getVelocity(), "1.2 1.2 1.2")))); //There ought to be a delay or something, but hey, it's ten seconds.
if (%user.client.bodytype==666)
  %user.applyRepair(10);
%obj.lastCol = getSimTime();
%obj.setCollisionTimeout(%user);
if(%obj.deleteSchedule !$= "") {
  cancel(%obj.deleteSchedule);
  %obj.deleteSchedule = %obj.schedule(10000, delete);
}
}

datablock ItemData(skeltorso)
{
   shapeFile = "~/data/shapes/torso.dts";
   category = "debris";
   mass = 1;
   friction = 1;
   elasticity = 0.5;
   rotate = true;
   maxInventory = 0;
   pickUpName = '';
   invName = '';
};

function skeltorso::onPickup(%this,%obj,%user,%amount)
{
if (%user.client.bodytype==666)
  %user.applyRepair(10);
}

//datablock ItemData(bloodsplash) //we aren't going to use this anymore anyway, but I'll keep the splat in case we use that.
//{
//   shapeFile = "~/data/shapes/gore2/splash.dts";
//   category = "debris";
//   mass = 1;
//   friction = 1;
//   elasticity = 0;
//   rotate = false;
//   maxInventory = 0;
//   pickUpName = 'blood, blood, gallons of the stuff';
//   invName = 'blood';
//};

//function bloodsplash::onPickup(%this,%obj,%user,%amount)
//{
//if (%user.client.bodytype==666)
//  %user.applyRepair(0);
//}

datablock ItemData(bloodsplat)
{
   shapeFile = "~/data/shapes/gore2/splat.dts";
   category = "debris";
   mass = 1;
   friction = 1;
   elasticity = 0;
   rotate = false;
   maxInventory = 0;
   pickUpName = 'more blood than you can drink but it will never be enough';
   invName = 'blood stain';
};

function bloodsplat::onPickup(%this,%obj,%user,%amount)
{
if (%user.client.bodytype==666)
  %user.applyRepair(0);
}

function tbmplayerdebris(%player) {
	%part[0] = nametoid(skelhead);
	%part[1] = nametoid(skelarmr);
	%part[2] = nametoid(skelarml);
	%part[3] = nametoid(skelleg);
	%part[4] = nametoid(skelleg);
	%part[5] = nametoid(skeltorso);
        %velc[0] = getrandom(-10,10)@" "@getrandom(-10,10)@" "@getrandom(0,10);
        %velc[1] = getrandom(-10,10)@" "@getrandom(-10,10)@" "@getrandom(0,10);
        %velc[2] = getrandom(-10,10)@" "@getrandom(-10,10)@" "@getrandom(0,10);
        %velc[3] = getrandom(-10,10)@" "@getrandom(-10,10)@" "@getrandom(0,10);
        %velc[4] = getrandom(-10,10)@" "@getrandom(-10,10)@" "@getrandom(0,10);
        %velc[5] = getrandom(-10,10)@" "@getrandom(-10,10)@" "@getrandom(0,10);
	%muzzlepoint = %player.getposition();
	%muzzlevector = %player.getEyeVector();
	%muzzlepoint = VectorAdd(%muzzlepoint, %muzzleVector);
	%playerRot = rotFromTransform(%player.getTransform());
     for (%i=0;%i<6;%i++) {
	%thrownItem = new (item)()
		{
			datablock = %part[%i];			
		};
	%thrownItem.setScale(%player.getScale());
	MissionCleanup.add(%thrownItem);
        %thrownItem.client = %player.client;
	%thrownItem.settransform(%muzzlepoint @ " " @ %playerRot);
	%objectVelocity = %player.getVelocity();
	%muzzleVelocity = VectorAdd(%velc[%i],%objectVelocity);
	%thrownItem.setVelocity(%muzzleVelocity);
	%thrownItem.schedule(10000, delete);
        %thrownItem.setCollisionTimeout(%player);
        }
}

function tbmbonedebris(%player) {//new fuction for leaving a skeleton without really shooting it like an explosion, for the quantum and acid-gun deaths
	%part[0] = nametoid(skelhead);
	%part[1] = nametoid(skelarmr);
	%part[2] = nametoid(skelarml);
	%part[3] = nametoid(skeltorso);
        %velc[0] = "0 0 5";
        %velc[1] = "0 0 0";
        %velc[2] = "0 0 0";
        %velc[3] = "0 0 0";
	%muzzlepoint = %player.getposition();
	%muzzlevector = %player.getEyeVector();
	%muzzlepoint = VectorAdd(%muzzlepoint, %muzzleVector);
	%playerRot = rotFromTransform(%player.getTransform());
     for (%i=0;%i<6;%i++) {
	%thrownItem = new (item)()
		{
			datablock = %part[%i];			
		};
	%thrownItem.setScale(%player.getScale());
	MissionCleanup.add(%thrownItem);
        %thrownItem.client = %player.client;
	%thrownItem.settransform(%muzzlepoint SPC %playerRot);
	%objectVelocity = %player.getVelocity();
	%muzzleVelocity = VectorAdd(%velc[%i],%objectVelocity/2);
	%thrownItem.setVelocity(%muzzleVelocity);
	%thrownItem.schedule(10000, delete);
        %thrownItem.setCollisionTimeout(%player);
        }
}

function tbmbotdebris(%obj) {
	%part[0] = nametoid(skelhead);
	%part[1] = nametoid(skelarmr);
	%part[2] = nametoid(skelarml);
	%part[3] = nametoid(skelleg);
	%part[4] = nametoid(skelleg);
	%part[5] = nametoid(skeltorso);
        %velc[0] = getrandom(-10,10)@" "@getrandom(-10,10)@" "@getrandom(0,10);
        %velc[1] = getrandom(-10,10)@" "@getrandom(-10,10)@" "@getrandom(0,10);
        %velc[2] = getrandom(-10,10)@" "@getrandom(-10,10)@" "@getrandom(0,10);
        %velc[3] = getrandom(-10,10)@" "@getrandom(-10,10)@" "@getrandom(0,10);
        %velc[4] = getrandom(-10,10)@" "@getrandom(-10,10)@" "@getrandom(0,10);
        %velc[5] = getrandom(-10,10)@" "@getrandom(-10,10)@" "@getrandom(0,10);
	%muzzlepoint = %obj.getposition();
	%muzzlevector = %obj.getEyeVector();
	%muzzlepoint = VectorAdd(%muzzlepoint, %muzzleVector);
	%playerRot = rotFromTransform(%obj.getTransform());
     for (%i=0;%i<6;%i++) {
	%thrownItem = new (item)()
		{
			datablock = %part[%i];			
		};
	%thrownItem.setScale(%obj.getScale());
		MissionCleanup.add(%thrownItem);
                %thrownItem.client = %obj.player.client;
	%thrownItem.settransform(%muzzlepoint @ " " @ %playerRot);
	%objectVelocity = %obj.getVelocity();
	%muzzleVelocity = VectorAdd(%velc[%i],%objectVelocity);
	%thrownItem.setVelocity(%muzzleVelocity);
	%thrownItem.schedule(10000, delete);
        %thrownItem.setCollisionTimeout(%obj);
        }
}

function serverCmdKickball(%client, %type) { //this function will create a new head at the client's igob position, and delete/replace it if there already is one
if(%client.isAdmin || %client.isSuperAdmin) {
	if(isObject(%client.igob)) {
		if(isObject($kickball))
			$kickball.delete();
		%color = "yellow";
		if(%client.player.getDatablock() == nameToID("MGrayDark"))
			%color = "brown";
		if(%client.player.getDatablock() == nameToID("MZombie") || %this.getDatablock() == nameToID("MZombie2") || %this.getDatablock() == nameToID("MZombie3") || !%type)
			%color = "white";
		$kickball = new(item)() {
			position = %client.igob.position;
			rotation = "1 0 0 0";
			datablock = skelhead;
		};
		MissionCleanup.add($kickball);
		$kickball.setSkinName(addTaggedString(%Color));
		$kickball.setVelocity("0 0 5");
		if(%type == 1) { //this will allow the user clone a kickable head from themselves, otherwise a skull will be created
			if(%client.headCode !$= "")
				$kickball.mountImage(%client.headCode, $headSlot, 1, %client.headCodeColor);
			if(%client.visorCode !$= "")
				$kickball.mountImage(%client.visorCode, $visorSlot, 1, %client.visorCodeColor);
			if(%client.leftHandCode $= "BeardShowImage")
				$kickball.mountImage(BeardShowImage, $leftHandSlot, 1, %client.leftHandCodeColor);
			if(%color !$= "white")
				$kickball.mountImage(faceplateShowImage, $faceSlot, 1, %client.faceDecalCode);
		}
	}
}
}

//function servercmdnoblood() { //I'm disabling these since we're going to completely re-work blood, or remove it entirely.
//exec("./shizscripts/noblood.cs");
//messageAll( 'MsgAdminForce', '\c2Blood has been disabled.');
//}

//function servercmdblood() {
//exec("./shizscripts/blood.cs");
//messageAll( 'MsgAdminForce', '\c3Normal Blood\c2 has been enabled.');
//}

//function servercmdassloads() {
//exec("./shizscripts/assloads.cs");
//messageAll( 'MsgAdminForce', '\c9ASSLOADS\c3 of blood\c2 has been enabled.');
//}

function serverCmdFallDamageOn(%client) {
if(%client.isAdmin || %client.isSuperAdmin) {//0.o who the hell forgot to add an admin check to this? //You did, Shiznit.
  for(%i = 0; %i <= $BodytypeCount; %i++) {
    $Bodytype[%i].minImpactSpeed = 30;
    $Bodytype[%i].speedDamageScale = 2;
  }
  MZombie.minImpactSpeed = 40;
  MZombie.speedDamageScale = 2;
  MZombie2.minImpactSpeed = 40;
  MZombie2.speedDamageScale = 2;
  MZombie3.minImpactSpeed = 40;
  MZombie3.speedDamageScale = 2;
  messageAll('MsgImpactOn', '\c3Impact damage\c2 has been \c7enabled.');
}
}

function serverCmdFallDamageOff(%client) {
if(%client.isAdmin || %client.isSuperAdmin) {
  for(%i = 0; %i <= $BodytypeCount; %i++) {
    $Bodytype[%i].minImpactSpeed = 999;
    $Bodytype[%i].speedDamageScale = 0;
  }
  MZombie.minImpactSpeed = 999;
  MZombie.speedDamageScale = 0;
  MZombie2.minImpactSpeed = 999;
  MZombie2.speedDamageScale = 0;
  MZombie3.minImpactSpeed = 999;
  MZombie3.speedDamageScale = 0;
  messageAll('MsgImpactOff', '\c3Impact damage\c2 has been \c7disabled.');
}
}

package Droid {
function Player::mountImage(%this, %image, %slot, %pre, %skin) {
if(%this.getDatablock() == nameToID(MDroid) && (%image $= faceplateShowImage || %image $= chestShowImage))
  return;
Parent::mountImage(%this, %image, %slot, %pre, %skin);
}

function AIPlayer::mountImage(%this, %image, %slot, %pre, %skin) {
if(%this.getDatablock() == nameToID(MDroid) && (%image $= faceplateShowImage || %image $= chestShowImage))
  return;
Parent::mountImage(%this, %image, %slot, %pre, %skin);
}

function GameConnection::setControlObject(%this, %obj) {
if(%obj.getClassName() $= "Player" || %obj.getClassName() $= "AIPlayer") {
  if(%obj.getState() $= "Dead" && !%obj.controllable)
    return;
}
Parent::setControlObject(%this, %obj);
}

};
activatepackage(Droid);

function isPlayerInSwitchGroup(%this, %group) {
switch$(%group) {
  case "none":
    return 1;
  case "red":
    return %this.client.team $= "red";
  case "blue":
    return %this.client.team $= "blue";
  case "green":
    return %this.client.team $= "green";
  case "yellow":
    return %this.client.team $= "yellow";
  case "mod":
    return isObject(%this.client) ? %this.client.rankCheck(1) : 0;
  case "admin":
    return isObject(%this.client) ? %this.client.rankCheck(2) : 0;
  case "super":
    return isObject(%this.client) ? %this.client.rankCheck(3) : 0;
  case "belowmod":
    return isObject(%this.client) ? !%this.client.rankCheck(1) : 1;
  case "belowadmin":
    return isObject(%this.client) ? !%this.client.rankCheck(2) : 1;
  case "belowsuper":
    return isObject(%this.client) ? !%this.client.rankCheck(3) : 1;
  case "human":
    return (%this.getDatablock() != nameToID("MZombie") && %this.getDatablock() != nameToID("MZombie2") && %this.getDatablock() != nameToID("MZombie3"));
  case "zombie":
    return (%this.getDatablock() == nameToID("MZombie") || %this.getDatablock() == nameToID("MZombie2") || %this.getDatablock() == nameToID("MZombie3"));
  default:
    return 0;
}
}