//spear.cs


datablock AudioProfile(spearExplosionSound)
{
   filename    = "./sound/spearHit.wav";
   description = AudioClose3d;
   preload = false;
};

datablock AudioProfile(spearFireSound)
{
   filename    = "./sound/spearFire.wav";
   description = AudioClose3d;
   preload = true;
};


//spear trail
datablock ParticleData(spearTrailParticle)
{
	dragCoefficient		= 3.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 600;
	lifetimeVarianceMS	= 0;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= true;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "base/data/particles/ring";
	//animTexName		= " ";

	// Interpolation variables
	colors[0]	= "0.75 0.75 0.75 0.3";
	colors[1]	= "0.75 0.75 0.75 0.2";
	colors[2]	= "1 1 1 0.0";
	sizes[0]	= 0.15;
	sizes[1]	= 0.35;
	sizes[2]	= 0.05;
	times[0]	= 0.0;
	times[1]	= 0.1;
	times[2]	= 1.0;
};

datablock ParticleEmitterData(spearTrailEmitter)
{
   ejectionPeriodMS = 5;
   periodVarianceMS = 0;

   ejectionVelocity = 0; //0.25;
   velocityVariance = 0; //0.10;

   ejectionOffset = 0;

   thetaMin         = 0.0;
   thetaMax         = 90.0;  

   particles = spearTrailParticle;
};


//effects
datablock ParticleData(spearExplosionParticle)
{
	dragCoefficient		= 3.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= 0.5;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 900;
	lifetimeVarianceMS	= 300;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= true;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "base/data/particles/cloud";
	//animTexName		= "~/data/particles/cloud";

	// Interpolation variables
	colors[0]	= "0.3 0.3 0.2 0.9";
	colors[1]	= "0.2 0.2 0.2 0.0";
	sizes[0]	= 4.0;
	sizes[1]	= 7.0;
	times[0]	= 0.0;
	times[1]	= 1.0;
};

datablock ParticleEmitterData(spearExplosionEmitter)
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   lifeTimeMS	   = 21;
   ejectionVelocity = 8;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "spearExplosionParticle";
};

datablock ParticleData(spearExplosionParticle2)
{
	dragCoefficient		= 0.1;
	windCoefficient		= 0.0;
	gravityCoefficient	= 2.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 1000;
	lifetimeVarianceMS	= 500;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= true;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "base/data/particles/chunk";
	//animTexName		= "~/data/particles/cloud";

	// Interpolation variables
	colors[0]	= "0.0 0.0 0.0 1.0";
	colors[1]	= "0.0 0.0 0.0 0.0";
	sizes[0]	= 0.5;
	sizes[1]	= 0.5;
	times[0]	= 0.0;
	times[1]	= 1.0;
};

datablock ParticleEmitterData(spearExplosionEmitter2)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   lifetimeMS       = 7;
   ejectionVelocity = 15;
   velocityVariance = 5.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "spearExplosionParticle2";
};

datablock ExplosionData(spearExplosion)
{
   //explosionShape = "";
   lifeTimeMS = 150;

   soundProfile = spearExplosionSound;

   emitter[0] = spearExplosionEmitter;
   emitter[1] = spearExplosionEmitter2;
   //particleDensity = 30;
   //particleRadius = 1.0;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = true;
   camShakeFreq = "7.0 8.0 7.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 0.5;
   camShakeRadius = 15.0;

   // Dynamic light
   lightStartRadius = 4;
   lightEndRadius = 3;
   lightStartColor = "0.45 0.3 0.1";
   lightEndColor = "0 0 0";

   //impulse
   impulseRadius = 3.5;
   impulseForce = 2000;

   //radius damage
   radiusDamage        = 40;
   damageRadius        = 3.5;
};

//projectile
AddDamageType("SpearDirect",   '<bitmap:add-ons/ci/spear> %1',    '%2 <bitmap:add-ons/ci/spear> %1',1,1);
AddDamageType("SpearRadius",   '<bitmap:add-ons/ci/spearRadius> %1',    '%2 <bitmap:add-ons/ci/spearRadius> %1',1,0);
datablock ProjectileData(spearProjectile)
{
   projectileShapeName = "./shapes/spearProjectile.dts";
   directDamage        = 50;
   directDamageType  = $DamageType::SpearDirect;
   radiusDamageType  = $DamageType::SpearRadius;
   impactImpulse	   = 1000;
   verticalImpulse	   = 1000;
   explosion           = spearExplosion;
   particleEmitter     = spearTrailEmitter;

   brickExplosionRadius = 0;
   brickExplosionImpact = true; //destroy a brick if we hit it directly?
   brickExplosionForce  = 20;
   brickExplosionMaxVolume = 200;

   muzzleVelocity      = 50;
   velInheritFactor    = 1;

   armingDelay         = 0;
   lifetime            = 20000;
   fadeDelay           = 19500;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 0.50;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";
};


//////////
// item //
//////////
datablock ItemData(spearItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./shapes/spear.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Spear";
	iconName = "./ItemIcons/spear";
	doColorShift = true;
	colorShiftColor = "0.400 0.196 0 1.000";

	 // Dynamic properties defined by the scripts
	image = spearImage;
	canDrop = true;
};

//function spear::onUse(%this,%user)
//{
//	//mount the image in the right hand slot
//	%user.mountimage(%this.image, $RightHandSlot);
//}

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(spearImage)
{
   // Basic Item properties
   shapeFile = "./shapes/spear.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   //eyeOffset = "0.1 0.2 -0.55";

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = spearItem;
   ammo = " ";
   projectile = spearProjectile;
   projectileType = Projectile;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = true;

   //casing = " ";
   doColorShift = true;
   colorShiftColor = "0.400 0.196 0 1.000";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]			= "Activate";
	stateTimeoutValue[0]		= 0.1;
	stateTransitionOnTimeout[0]	= "Ready";
	stateSequence[0]		= "ready";
	stateSound[0]					= weaponSwitchSound;

	stateName[1]			= "Ready";
	stateTransitionOnTriggerDown[1]	= "Charge";
	stateAllowImageChange[1]	= true;
	
	stateName[2]                    = "Charge";
	stateTransitionOnTimeout[2]	= "Armed";
	stateTimeoutValue[2]            = 0.7;
	stateWaitForTimeout[2]		= false;
	stateTransitionOnTriggerUp[2]	= "AbortCharge";
	stateScript[2]                  = "onCharge";
	stateAllowImageChange[2]        = false;
	
	stateName[3]			= "AbortCharge";
	stateTransitionOnTimeout[3]	= "Ready";
	stateTimeoutValue[3]		= 0.3;
	stateWaitForTimeout[3]		= true;
	stateScript[3]			= "onAbortCharge";
	stateAllowImageChange[3]	= false;

	stateName[4]			= "Armed";
	stateTransitionOnTriggerUp[4]	= "Fire";
	stateAllowImageChange[4]	= false;

	stateName[5]			= "Fire";
	stateTransitionOnTimeout[5]	= "Ready";
	stateTimeoutValue[5]		= 0.5;
	stateFire[5]			= true;
	stateSequence[5]		= "fire";
	stateScript[5]			= "onFire";
	stateWaitForTimeout[5]		= true;
	stateAllowImageChange[5]	= false;
	stateSound[5]				= spearFireSound;
};

function spearImage::onCharge(%this, %obj, %slot)
{
	%obj.playthread(2, spearReady);
}

function spearImage::onAbortCharge(%this, %obj, %slot)
{
	%obj.playthread(2, root);
}

function spearImage::onFire(%this, %obj, %slot)
{
	%obj.playthread(2, spearThrow);
	Parent::onFire(%this, %obj, %slot);
}