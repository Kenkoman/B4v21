//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

// Load dts shapes and merge animations
exec("~/data/shapes/player/player.cs");

// Timeouts for corpse deletion.
$CorpseTimeoutValue = 1 * 1000;

// Damage Rate for entering Liquid
$DamageLava       = 0.01;
$DamageHotLava    = 0.01;
$DamageCrustyLava = 0.01;

//
$PlayerDeathAnim::TorsoFrontFallForward = 1;
$PlayerDeathAnim::TorsoFrontFallBack = 2;
$PlayerDeathAnim::TorsoBackFallForward = 3;
$PlayerDeathAnim::TorsoLeftSpinDeath = 4;
$PlayerDeathAnim::TorsoRightSpinDeath = 5;
$PlayerDeathAnim::LegsLeftGimp = 6;
$PlayerDeathAnim::LegsRightGimp = 7;
$PlayerDeathAnim::TorsoBackFallForward = 8;
$PlayerDeathAnim::HeadFrontDirect = 9;
$PlayerDeathAnim::HeadBackFallForward = 10;
$PlayerDeathAnim::ExplosionBlowBack = 11;


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

datablock AudioProfile(ArmorMoveBubblesSound)
{
   filename    = "~/data/sound/replaceme.wav";
   description = AudioCloseLooping3d;
   preload = true;
};

datablock AudioProfile(WaterBreathMaleSound)
{
   filename    = "~/data/sound/replaceme.wav";
   description = AudioClosestLooping3d;
   preload = true;
};


//----------------------------------------------------------------------------

datablock AudioProfile(FootLightSoftSound)
{
   filename    = "~/data/sound/footfall.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(FootLightHardSound)
{
   filename    = "~/data/sound/footfall.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(FootLightMetalSound)
{
   filename    = "~/data/sound/footfall.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(FootLightSnowSound)
{
   filename    = "~/data/sound/footfall.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(FootLightShallowSplashSound)
{
   filename    = "~/data/sound/footfall.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(FootLightWadingSound)
{
   filename    = "~/data/sound/footfall.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(FootLightUnderwaterSound)
{
   filename    = "~/data/sound/footfall.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(FootLightBubblesSound)
{
   filename    = "~/data/sound/replaceme.wav";
   description = AudioClose3d;
   preload = true;
};

//----------------------------------------------------------------------------

datablock AudioProfile(ImpactLightSoftSound)
{
   filename    = "~/data/sound/replaceme.wav";
   description = AudioClose3d;
   preload = true;
   effect = ImpactSoftEffect;
};

datablock AudioProfile(ImpactLightHardSound)
{
   filename    = "~/data/sound/replaceme.wav";
   description = AudioClose3d;
   preload = true;
   effect = ImpactHardEffect;
};

datablock AudioProfile(ImpactLightMetalSound)
{
   filename    = "~/data/sound/replaceme.wav";
   description = AudioClose3d;
   preload = true;
   effect = ImpactMetalEffect;
};

datablock AudioProfile(ImpactLightSnowSound)
{
   filename    = "~/data/sound/replaceme.wav";
   description = AudioClosest3d;
   preload = true;
   effect = ImpactSnowEffect;
};

datablock AudioProfile(ImpactLightWaterEasySound)
{
   filename    = "~/data/sound/replaceme.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(ImpactLightWaterMediumSound)
{
   filename    = "~/data/sound/replaceme.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(ImpactLightWaterHardSound)
{
   filename    = "~/data/sound/replaceme.wav";
   description = AudioDefault3d;
   preload = true;
};

datablock AudioProfile(ExitingWaterLightSound)
{
   filename    = "~/data/sound/replaceme.wav";
   description = AudioClose3d;
   preload = true;
};


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
   gravityCoefficient   = -1.0; //-0.150;
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
   gravityCoefficient   = -0.4;
   inheritedVelFactor   = 0.2;
   constantAcceleration = -0.0;
   lifetimeMS           = 5000;
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

datablock DecalData(LightMaleFootprint)
{
   sizeX       = 0.125;
   sizeY       = 0.25;
   textureName = "~/data/shapes/player/footprint";
};

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

   mass = $Pref::Server::JetLag;
   drag = 0.1;
   maxdrag = 0.2;
   density = 0.7;
   maxDamage = 100;
   maxEnergy =  $Pref::Server::MaxEnergy;
   repairRate = 0.33;
   energyPerDamagePoint = 75.0;

   rechargeRate = 1000; //0.256;

   runForce = 48 * $Pref::Server::JetLag;
   runEnergyDrain = 0;
   minRunEnergy = 0;
   maxForwardSpeed = 7;
   maxBackwardSpeed = 4;
   maxSideSpeed = 6;

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

   jumpForce = 12 * $Pref::Server::JetLag; //8.3 * 90;
   jumpEnergyDrain = 0;
   minJumpEnergy = 0;
   jumpDelay = 0;

   recoverDelay = 0;
   recoverRunForceScale = 1.2;

   minImpactSpeed = 25;
   speedDamageScale = 3.8;

   boundingBox = "1.25 1.25 2.65";
   crouchBoundingBox = "1.25 2.5 1.0";
   proneBoundingBox = "1 2.3 1";

   pickupRadius = 0.75;
   
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
   
   footPuffNumParts = 10;
   footPuffRadius = 0.25;

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
   FootHardSound        = FootLightHardSound;
   FootMetalSound       = FootLightMetalSound;
   FootSnowSound        = FootLightSnowSound;
   FootShallowSound     = FootLightShallowSplashSound;
   FootWadingSound      = FootLightWadingSound;
   FootUnderwaterSound  = FootLightUnderwaterSound;
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
	maxItems   = 10;	//total number of usable things you can carry including weapons
	maxWeapons = 5;

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

	if(%this.maxEnergy == 0)
	{
		%obj.setRechargeRate(0);
	}
	else
	{
		%obj.setRechargeRate(%this.rechargeRate);
	}

   %obj.setRepairRate(0);
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
   %client = %obj.client;

   if(%col.Elevator)
   {
	%col.Elevated[%obj.client] = 1;
	%obj.client.currentElevatorObj = %col;
	%obj.client.isElevating = 1;	
   }

   if (%obj.getState() $= "Dead")
      return;

	if (%col.getClassName() $= "Item")
	{
		//echo("calling pickup ", %col.thrower);
		//echo("pickup obj = ", %obj);
		if(%col.thrower != %obj)
		{
			if($Pref::Server::ItemsCostMoney $= 1)
			{
				messageClient(%client,'','\c4Please use your \c0Use\c4 Key!');
				return;
			}
			else
			{
				%obj.pickup(%col);
			}
		}
	}

	if(%col.getClassName() $= "StaticShape")
	{

	if(%col.isDoorbell $= 1 && %obj.client.BellTimeout[%col] !$= 1)
	{
		messageClient(%col.owner,'','\c0%1 \c4rings your Doorbell.', %obj.client.name);
		%obj.client.BellTimeout[%col] = 1;
		schedule(20000,0,"clearBellTimeout",%obj.client, %col);
	}

	if(%col.isImp && %col.isTriggerImp !$= 1)
	{
			%pos = getwords(%col.getTransform(), 0, 2);
			%obj.client.player.applyimpulse(%pos,%col.Imp);
	}


if($Pref::Server::BombRigging $= 1)
{

	if(%col.brickType $= 1)
	{
		if(%col.riggerID.plantedbomb >= 1)
		{
			messageAll('name', '\c0%1\'s\c5 Bomb was Detonated!', %col.RiggerID.name);
			for(%t = 0; %t < MissionCleanup.getCount(); %t++)
			{
				%bomb = MissionCleanup.getObject(%t);
				if(%bomb.brickType $= 2 && %bomb.bombID $= %col.bombID)
				{
					servercmdblowbricks(%client,%bomb);
					%col.RiggerID.plantedbomb = 0;
					%col.RiggerID.plantedplunger = 0;
					%col.dead = true;
					%col.schedule(10, explode);
				}
			}
		}
	}

}



		if(%col.teleportObj > 0)
		{
			if(%obj.client.NoTeleport == 0)
			{
				for(%t = 0; %t < MissionCleanup.getCount(); %t++)
				{
					%teleObj = MissionCleanup.getObject(%t);
					if(%teleObj.teleportObj == %col.teleportObj && %teleObj != %col)
					{
						Schedule(200,0,"ServerCmdTeleportToObj",%client,%teleObj,500);
						break;
					}
				}
				
			}
		}

		testbrick(%obj.client, %col);

}
}



function Armor::onImpact(%this, %obj, %collidedObject, %vec, %vecLen)
{
	//echo("armor impact");
	
if($Pref::Server::FallDamage == 1)
{
	   %obj.damage(0, VectorAdd(%obj.getPosition(),%vec), %vecLen * %this.speedDamageScale, "Impact");
	if(%vecLen * %this.speedDamageScale > 100)
	{
		messageAll("",'\c0%1 made a pancake shape on the floor...',%obj.client.name);
	}
	      %time = ( ( (%vecLen - 10) / 40) * 6) + 2;
	 
 	     %time = %time * 1000;
		if(%time > 8000)
			%time = 8000;
		if(%time < 2000)
			%time = 2000;

	//	tumble(%obj, %time);
}      
}


//----------------------------------------------------------------------------

function Armor::damage(%this, %obj, %sourceObject, %position, %damage, %damageType)
{
   if (%obj.getState() $= "Dead" || %obj.client.isImprisoned)
      return;
   %obj.applyDamage(%damage);
   %location = "Body";

   // Deal with client callbacks here because we don't have this
   // information in the onDamage or onDisable methods
   %client = %obj.client;
   %sourceClient = %sourceObject ? %sourceObject.client : 0;

   if (%obj.getState() $= "Dead")
      %client.onDeath(%sourceObject, %sourceClient, %damageType, %location);
}

function Armor::onDamage(%this, %obj, %delta)
{
   // This method is invoked by the ShapeBase code whenever the 
   // object's damage level changes.
   if (%delta > 0 && %obj.getState() !$= "Dead") {

      // Increment the flash based on the amount.
      %flash = %obj.getDamageFlash() + ((%delta / %this.maxDamage) * 2);
      if (%flash > 0.75)
         %flash = 0.75;
      %obj.setDamageFlash(%flash);

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

   // Release the main weapon trigger
   %obj.setImageTrigger(0,false);

	//Remove the working brick
	if(isObject(%obj.tempBrick))
		{
			if(%obj.tempBrick.isBrickGhostMoving $= 1)
			{
				%obj.tempBrick.setSkinName(%obj.tempBrick.oldskinname);
				%obj.tempBrick.isBrickGhost = "";
				%obj.tempBrick.isBrickGhostMoving = "";
				%obj.tempBrick = "";
			}
			else
			{
				%obj.tempBrick.delete();
				%obj.tempBrick = "";
			}
		}


   // Schedule corpse removal.  Just keeping the place clean.
   %obj.schedule($CorpseTimeoutValue - 1000, "startFade", 1000, 0, true);
   %obj.schedule($CorpseTimeoutValue, "delete");
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
      case 1: //Ocean Water
      case 2: //River Water
      case 3: //Stagnant Water
      case 4: //Lava
         %obj.kill();
	 messageAll("",'\c0%1 got Dissolved in Fiery Lava.',%obj.client.name);
      case 5: //Hot Lava
         %obj.setDamageDt(%this, $DamageHotLava, "Lava");
      case 6: //Crusty Lava
         %obj.setDamageDt(%this, $DamageCrustyLava, "Lava");
      case 7: //Quick Sand
   }
}

function Armor::onLeaveLiquid(%this, %obj, %type)
{
   %obj.clearDamageDt();
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

function Player::playDeathAnimation(%this)
{
   if (%this.deathIdx++ > 11)
      %this.deathIdx = 1;
   %this.setActionThread("Death" @ %this.deathIdx);
}

function Player::playCelAnimation(%this,%anim)
{
   if (%this.getState() !$= "Dead")
      %this.setActionThread("cel"@%anim);
}


//----------------------------------------------------------------------------

function Player::playDeathCry( %this )
{
   %client = %this.client;

   //playTargetAudio( %client.target, "replaceme", AudioClosest3d, false );
   %this.playAudio(0,DeathCrySound);
}

function Player::playPain( %this )
{
   %client = %this.client;
  // playTargetAudio( %client.target, "replaceme", AudioClosest3d, false);
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

//	%newcar = new WheeledVehicle() 
//	{
//		dataBlock = deathVehicle;
//		client = %client;
//		initialPosition = %posX @ " " @ %posY @ " " @ %posZ;
//	};
	

	%newcar = %client.tumbleVehicle;
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

function testbrick(%client, %col)
{
		if(%col.isDoor $= 1 && %col.isMoving !$= 1 && %col.noCollision !$= 1 && %col.isLocked != 1)
		{
			%isTrusted = checkSafe(%col,%obj.client);

			if(%obj.client.isSuperAdmin || %obj.client.isAdmin)
			{
				executemoverinstructions(%obj.client, %col);	
				return;
			}

			if(%col.Password !$= "" && (!%client.isAdmin || !%client.isSuperAdmin))
			{
			%client.PWDoor = %col;
			commandtoclient(%client,'OpenPWBox');
			return;
			}

			if(%col.owner $= %obj.client && !$Pref::Server::CopsAndRobbers)
			{
				executemoverinstructions(%obj.client, %col);	
				return;
			}

			if(%col.Private $= "" && %col.Team $= "" && %col.Group $= "") 
			{
				executemoverinstructions(%obj.client, %col);	
				return;
			}
		
			if(%col.group !$= "")
			{
				if(%col.group $= "Friend" && %isTrusted)
				{
				executemoverinstructions(%obj.client, %col);	
				return;
				}
				else if(%col.group $= "Safe" && %isTrusted)
				{
				executemoverinstructions(%obj.client, %col);	
				return;
				}
				else if(%col.group $= "Admin" && %obj.client.isTempAdmin || %col.group $= "Admin" && %obj.client.isSuperAdmin)
				{
				executemoverinstructions(%obj.client, %col);	
				return;
				}
				else if(%col.group $= "Super Admin" && %obj.client.isSuperAdmin)
				{
				executemoverinstructions(%obj.client, %col);	
				return;
				}
				else
				{
				return;
				}
			}



			if(%col.Team !$= "" &&  %obj.client.team $= %col.team)
			{
				executemoverinstructions(%obj.client, %col);	
				return;
			}

			if(%isTrusted && %col.Private $= 1 && %col.Team $= "" && %col.Group $= "")
			{
				executemoverinstructions(%obj.client, %col);	
				return;
			}

			if(%col.Elevator $= 1 && $Pref::Server::EnabledElevator != 1)
			{
				messageClient(%obj.client,"","\c4Elevators are Disabled on this Server!");
				return;
			}

			

}
}

function clearBellTimeout(%client, %col)
{
	%client.BellTimeout[%col] = 0;
}
