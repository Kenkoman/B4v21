//rocketLauncher.cs

//audio
datablock AudioProfile(rocketFireSound)
{
   filename    = "./sound/rocketFire.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(rocketExplodeSound)
{
   filename    = "./sound/tntExplode.wav";
   description = AudioDefault3d;
   preload = true;
};
datablock AudioProfile(rocketLoopSound)
{
   filename    = "./sound/rocketLoop.wav";
   description = AudioCloseLooping3d;
   preload = true;
};

//muzzle flash effects
datablock ParticleData(rocketLauncherFlashParticle)
{
	dragCoefficient      = 1;
	gravityCoefficient   = 1.5;
	inheritedVelFactor   = 0.2;
	constantAcceleration = 0.0;
	lifetimeMS           = 1500;
	lifetimeVarianceMS   = 150;
	textureName          = "base/data/particles/star1";
	spinSpeed		= 10.0;
	spinRandomMin		= -500.0;
	spinRandomMax		= 500.0;
	colors[0]     = "0.9 0.4 0.0 0.9";
	colors[1]     = "0.9 0.5 0.0 0.0";
	sizes[0]      = 0.25;
	sizes[1]      = 0.0;

	useInvAlpha = false;
};
datablock ParticleEmitterData(rocketLauncherFlashEmitter)
{
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;
   ejectionVelocity = 10.0;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 180;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "rocketLauncherFlashParticle";
};

datablock ParticleData(rocketLauncherSmokeParticle)
{
	dragCoefficient      = 5;
	gravityCoefficient   = -0.5;
	inheritedVelFactor   = 0.2;
	constantAcceleration = 0.0;
	lifetimeMS           = 300;
	lifetimeVarianceMS   = 250;
	textureName          = "base/data/particles/cloud";
	spinSpeed		= 10.0;
	spinRandomMin		= -500.0;
	spinRandomMax		= 500.0;

   colors[0]     = "0.5 0.5 0.5 0.0";
	colors[1]     = "0.5 0.5 0.5 0.9";
	colors[2]     = "0.5 0.5 0.5 0.0";

	sizes[0]      = 0.25;
   sizes[1]      = 1.0;
	sizes[2]      = 1.75;

   times[0] = 0.0;
   times[1] = 0.5;
   times[2] = 1.0;

	useInvAlpha = false;
};
datablock ParticleEmitterData(rocketLauncherSmokeEmitter)
{
   ejectionPeriodMS = 5;
   periodVarianceMS = 0;
   ejectionVelocity = 10.0;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 25;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "rocketLauncherSmokeParticle";
};


//bullet trail effects
datablock ParticleData(rocketTrailParticle)
{
	dragCoefficient      = 3;
	gravityCoefficient   = -0.0;
	inheritedVelFactor   = 0.15;
	constantAcceleration = 0.0;
	lifetimeMS           = 1000;
	lifetimeVarianceMS   = 805;
	textureName          = "base/data/particles/cloud";
	spinSpeed		= 10.0;
	spinRandomMin		= -150.0;
	spinRandomMax		= 150.0;
	colors[0]     = "1.0 1.0 0.0 0.4";
	colors[1]     = "1.0 0.2 0.0 0.5";
   colors[2]     = "0.20 0.20 0.20 0.3";
   colors[3]     = "0.0 0.0 0.0 0.0";

	sizes[0]      = 0.25;
	sizes[1]      = 0.85;
   sizes[2]      = 0.35;
 	sizes[3]      = 0.05;

   times[0] = 0.0;
   times[1] = 0.05;
   times[2] = 0.3;
   times[3] = 1.0;

	useInvAlpha = false;
};
datablock ParticleEmitterData(rocketTrailEmitter)
{
   ejectionPeriodMS = 5;
   periodVarianceMS = 1;
   ejectionVelocity = 0.25;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "rocketTrailParticle";
};


datablock ParticleData(rocketExplosionParticle)
{
	dragCoefficient      = 3;
	gravityCoefficient   = -0.5;
	inheritedVelFactor   = 0.2;
	constantAcceleration = 0.0;
	lifetimeMS           = 700;
	lifetimeVarianceMS   = 400;
	textureName          = "base/data/particles/cloud";
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	colors[0]     = "0.9 0.9 0.6 0.9";
	colors[1]     = "0.9 0.5 0.6 0.0";
	sizes[0]      = 10.0;
	sizes[1]      = 15.0;

	useInvAlpha = true;
};
datablock ParticleEmitterData(rocketExplosionEmitter)
{
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;
   ejectionVelocity = 10;
   velocityVariance = 1.0;
   ejectionOffset   = 3.0;
   thetaMin         = 89;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "rocketExplosionParticle";
};


datablock ParticleData(rocketExplosionRingParticle)
{
	dragCoefficient      = 8;
	gravityCoefficient   = -0.5;
	inheritedVelFactor   = 0.2;
	constantAcceleration = 0.0;
	lifetimeMS           = 40;
	lifetimeVarianceMS   = 10;
	textureName          = "base/data/particles/star1";
	spinSpeed		= 10.0;
	spinRandomMin		= -500.0;
	spinRandomMax		= 500.0;
	colors[0]     = "1 0.5 0.2 0.5";
	colors[1]     = "0.9 0.0 0.0 0.0";
	sizes[0]      = 8;
	sizes[1]      = 13;

	useInvAlpha = false;
};
datablock ParticleEmitterData(rocketExplosionRingEmitter)
{
	lifeTimeMS = 50;

   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   ejectionVelocity = 5;
   velocityVariance = 0.0;
   ejectionOffset   = 3.0;
   thetaMin         = 0;
   thetaMax         = 180;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "rocketExplosionRingParticle";
};

datablock ExplosionData(rocketExplosion)
{
   //explosionShape = "";
   explosionShape = "./shapes/explosionSphere1.dts";
	soundProfile = rocketExplodeSound;

   lifeTimeMS = 150;

   particleEmitter = rocketExplosionEmitter;
   particleDensity = 10;
   particleRadius = 0.2;

   emitter[0] = rocketExplosionRingEmitter;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = true;
   camShakeFreq = "10.0 11.0 10.0";
   camShakeAmp = "3.0 10.0 3.0";
   camShakeDuration = 0.5;
   camShakeRadius = 20.0;

   // Dynamic light
   lightStartRadius = 5;
   lightEndRadius = 20;
   lightStartColor = "1 1 1 1";
   lightEndColor = "0 0 0 0";

   damageRadius = 3;
   radiusDamage = 100;

   impulseRadius = 6;
   impulseForce = 4000;
};


AddDamageType("RocketDirect",   '<bitmap:add-ons/ci/rocket> %1',    '%2 <bitmap:add-ons/ci/rocket> %1',1,1);
AddDamageType("RocketRadius",   '<bitmap:add-ons/ci/rocketRadius> %1',    '%2 <bitmap:add-ons/ci/rocketRadius> %1',1,0);
datablock ProjectileData(rocketLauncherProjectile)
{
   projectileShapeName = "./shapes/RocketProjectile.dts";
   directDamage        = 30;
   directDamageType = $DamageType::RocketDirect;
   radiusDamageType = $DamageType::RocketRadius;
   impactImpulse	   = 1000;
   verticalImpulse	   = 1000;
   explosion           = rocketExplosion;
   particleEmitter     = rocketTrailEmitter;

   brickExplosionRadius = 3;
   brickExplosionImpact = false;          //destroy a brick if we hit it directly?
   brickExplosionForce  = 30;             
   brickExplosionMaxVolume = 30;          //max volume of bricks that we can destroy
   brickExplosionMaxVolumeFloating = 60;  //max volume of bricks that we can destroy if they aren't connected to the ground (should always be >= brickExplosionMaxVolume)

   sound = rocketLoopSound;

   muzzleVelocity      = 65;
   velInheritFactor    = 1.0;

   armingDelay         = 00;
   lifetime            = 4000;
   fadeDelay           = 3500;
   bounceElasticity    = 0.5;
   bounceFriction      = 0.20;
   isBallistic         = false;
   gravityMod = 0.0;

   hasLight    = true;
   lightRadius = 5.0;
   lightColor  = "1 0.5 0.0";
};

//////////
// item //
//////////
datablock ItemData(rocketLauncherItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./shapes/rocketLauncher.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Rocket L.";
	iconName = "./ItemIcons/rocketLauncher";
	doColorShift = true;
	colorShiftColor = "0.100 0.500 0.250 1.000";

	 // Dynamic properties defined by the scripts
	image = rocketLauncherImage;
	canDrop = true;
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(rocketLauncherImage)
{
   // Basic Item properties
   shapeFile = "./shapes/rocketLauncher.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   eyeOffset = 0; //"0.7 1.2 -0.5";
   rotation = eulerToMatrix( "0 0 0" );

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = BowItem;
   ammo = " ";
   projectile = rocketLauncherProjectile;
   projectileType = Projectile;

	//casing = rocketLauncherShellDebris;
	shellExitDir        = "1.0 -1.3 1.0";
	shellExitOffset     = "0 0 0";
	shellExitVariance   = 15.0;	
	shellVelocity       = 7.0;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = true;
   minShotTime = 700;   //minimum time allowed between shots (needed to prevent equip/dequip exploit)

   doColorShift = true;
   colorShiftColor = rocketLauncherItem.colorShiftColor;//"0.400 0.196 0 1.000";

   //casing = " ";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.1;
	stateTransitionOnTimeout[0]       = "Ready";
	stateSound[0]					= weaponSwitchSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateAllowImageChange[1]         = true;
   stateTransitionOnNoAmmo[1]       = "NoAmmo";
	stateSequence[1]	= "Ready";

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "Smoke";
	stateTimeoutValue[2]            = 0.1;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]			= true;
	stateEmitter[2]					= rocketLauncherFlashEmitter;
	stateEmitterTime[2]				= 0.05;
	stateEmitterNode[2]				= tailNode;
	stateSound[2]					= rocketFireSound;
   stateSequence[2]                = "Fire";
	//stateEjectShell[2]       = true;

	stateName[3] = "Smoke";
	stateEmitter[3]					= rocketLauncherSmokeEmitter;
	stateEmitterTime[3]				= 0.05;
	stateEmitterNode[3]				= "muzzleNode";
	stateTimeoutValue[3]            = 0.1;
   stateSequence[3]                = "TrigDown";
	stateTransitionOnTimeout[3]     = "CoolDown";

   stateName[5] = "CoolDown";
   stateTimeoutValue[5]            = 0.5;
	stateTransitionOnTimeout[5]     = "Reload";
   stateSequence[5]                = "TrigDown";


	stateName[4]			= "Reload";
	stateTransitionOnTriggerUp[4]     = "Ready";
	stateSequence[4]	= "TrigDown";

   stateName[6]   = "NoAmmo";
   stateTransitionOnAmmo[6] = "Ready";

};

