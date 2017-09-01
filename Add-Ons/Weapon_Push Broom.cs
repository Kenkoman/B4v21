//pushBroom.cs
//A weapon that pushes people but does no damage

//sound
datablock AudioProfile(pushBroomHitSound)
{
   filename    = "./sound/pushBroomHit.wav";
   description = AudioClosest3d;
   preload = true;
};
datablock AudioProfile(pushBroomSwingSound)
{
   filename    = "./sound/pushBroomSwing.wav";
   description = AudioClosestLooping3d;
   preload = true;
};



//effects
datablock ParticleData(pushBroomSparkParticle)
{
   dragCoefficient      = 4;
   gravityCoefficient   = 1;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 400;
   lifetimeVarianceMS   = 300;
   textureName          = "base/data/particles/chunk";
	
	useInvAlpha = false;
	spinSpeed		= 150.0;
	spinRandomMin		= -150.0;
	spinRandomMax		= 150.0;

   colors[0]     = "0.30 0.10 0.0 0.0";
   colors[1]     = "0.30 0.10 0.0 0.5";
   colors[2]     = "0.30 0.10 0.0 0.0";
   sizes[0]      = 0.15;
   sizes[1]      = 0.15;
   sizes[2]      = 0.15;

   times[0]	= 0.1;
   times[1] = 0.5;
   times[2] = 1.0;

   useInvAlpha = true;
};

datablock ParticleEmitterData(pushBroomSparkEmitter)
{
	lifeTimeMS = 10;

   ejectionPeriodMS = 3;
   periodVarianceMS = 0;
   ejectionVelocity = 5;
   velocityVariance = 3.0;
   ejectionOffset   = 1.50;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = pushBroomSparkParticle;
};




datablock ParticleData(pushBroomExplosionParticle)
{
   dragCoefficient      = 10;
   gravityCoefficient   = -0.15;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 800;
   lifetimeVarianceMS   = 500;
   textureName          = "base/data/particles/cloud";

	spinSpeed		= 50.0;
	spinRandomMin		= -500.0;
	spinRandomMax		= 500.0;

   colors[0]     = "1.0 1.0 1.0 0.25";
   colors[1]     = "0.0 0.0 0.0 0.0";
   sizes[0]      = 0.5;
   sizes[1]      = 1.0;

   useInvAlpha = true;
};

datablock ParticleEmitterData(pushBroomExplosionEmitter)
{
	lifeTimeMS = 50;

   ejectionPeriodMS = 3;
   periodVarianceMS = 0;
   ejectionVelocity = 10;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 95;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = pushBroomExplosionParticle;
};

datablock ExplosionData(pushBroomExplosion)
{
   //explosionShape = "";
   lifeTimeMS = 400;
   
   //emitter[0] = pushBroomExplosionEmitter;
   emitter[0] = pushBroomSparkEmitter;
   particleEmitter = pushBroomExplosionEmitter;
   particleDensity = 30;
	particleRadius = 1.0;
   
   faceViewer     = true;
   explosionScale = "1 1 1";

   soundProfile = pushBroomHitSound;

   
   shakeCamera = true;
   cameraShakeFalloff = false;
   camShakeFreq = "2.0 3.0 1.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 2.5;
   camShakeRadius = 0.0001;

   // Dynamic light
   lightStartRadius = 0;
   lightEndRadius = 0;
   lightStartColor = "0.0 0.0 0.0";
   lightEndColor = "0 0 0";
};


//projectile
datablock ProjectileData(pushBroomProjectile)
{
   //projectileShapeName = "~/data/shapes/arrow.dts";
   directDamage        = 0;
   impactImpulse       = 1300;
   verticalImpulse     = 1300;
   explosion           = pushBroomExplosion;
   //particleEmitter     = as;

   muzzleVelocity      = 50;
   velInheritFactor    = 1;

   armingDelay         = 0;
   lifetime            = 100;
   fadeDelay           = 70;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0.0;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";
};


//////////
// item //
//////////
datablock ItemData(pushBroomItem)
{
	category = "Tools";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./shapes/pushBroom.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui properties
	uiName = "Push Broom";
	iconName = "./ItemIcons/pushBroom";
	doColorShift = true;
	colorShiftColor = (102/255) SPC (50/255) SPC (0/255) SPC (255/255);
	

	 // Dynamic properties defined by the scripts
	image = pushBroomImage;
	canDrop = true;
};

//function pushBroom::onUse(%this,%user)
//{
//	//mount the image in the right hand slot
//	%user.mountimage(%this.image, $RightHandSlot);
//}

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(pushBroomImage)
{
   // Basic Item properties
   shapeFile = "./shapes/pushBroom.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   //eyeOffset = "0.7 1.2 -0.15";

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = false;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = pushBroomItem;
   ammo = " ";
   projectile = pushBroomProjectile;
   projectileType = Projectile;

   //melee particles shoot from eye node for consistancy
   melee = true;
   doRetraction = false;
   //raise your arm up or not
   armReady = true;

   //casing = " ";

   doColorShift = true;
   colorShiftColor = pushBroomItem.colorShiftColor;

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.0;
	stateTransitionOnTimeout[0]       = "Ready";

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "PreFire";
	stateAllowImageChange[1]         = true;

	stateName[2]			= "PreFire";
	stateScript[2]                  = "onPreFire";
	stateAllowImageChange[2]        = true;
	stateTimeoutValue[2]            = 0.01;
	stateTransitionOnTimeout[2]     = "Fire";

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "Fire";
	stateTimeoutValue[3]            = 0.2;
	stateFire[3]                    = true;
	stateAllowImageChange[3]        = true;
	stateSequence[3]                = "Fire";
	stateScript[3]                  = "onFire";
	stateWaitForTimeout[3]			= true;
	stateSequence[3]				= "Fire";
   stateTransitionOnTriggerUp[3]	= "StopFire";
   stateSound[3] = pushBroomSwingSound;
	//stateTransitionOnTriggerUp[3]	= "StopFire";

	stateName[4]			= "CheckFire";
	stateTransitionOnTriggerUp[4]	= "StopFire";
	stateTransitionOnTriggerDown[4]	= "Fire";
   stateSound[4] = pushBroomSwingSound;

	stateName[5]                    = "StopFire";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 0.2;
	stateAllowImageChange[5]        = true;
	stateWaitForTimeout[5]		= true;
	stateSequence[5]                = "StopFire";
	stateScript[5]                 = "onStopFire";
};

function pushBroomImage::onFire(%this, %obj, %slot)
{
	//messageAll( 'MsgClient', 'pushBroom prefired!!!');
	Parent::onFire(%this, %obj, %slot);
	%obj.playthread(2, rotCW);
}

function pushBroomImage::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
	//messageAll( 'MsgClient', 'stopfire');
}
