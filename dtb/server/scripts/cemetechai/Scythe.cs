//sword.cs
datablock AudioProfile(ScytheDrawSound)
{
   filename    = "tbm/data/sound/weaponSwitch.wav";
   description = AudioClosest3d;
   preload = true;
};
datablock AudioProfile(ScytheHitSound)
{
   filename    = "tbm/data/sound/swordDraw.wav";
   description = AudioClosest3d;
   preload = true;
};


//effects
datablock ParticleData(ScytheExplosionParticle)
{
   dragCoefficient      = 5;
   gravityCoefficient   = 0.2;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 500;
   lifetimeVarianceMS   = 300;
   textureName          = "~/data/particles/bubble";
   colors[0] = "1.1 1.1 1.1 1.1";
   colors[1] = "1.5 1.5 1.5 1.5";
   sizes[0]      = 0.5;
   sizes[1]      = 1.0;
	times[0]	= 0.5;
	times[1]	= 1.0;
};

datablock ParticleEmitterData(ScytheExplosionEmitter)
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   ejectionVelocity = 8;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 60;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "ScytheExplosionParticle";
};

datablock ExplosionData(ScytheExplosion)
{
   //explosionShape = "";
   lifeTimeMS = 500;

   soundProfile = ScytheHitSound;

   particleEmitter = ScytheExplosionEmitter;
   particleDensity = 10;
   particleRadius = 0.2;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = true;
   camShakeFreq = "20.0 22.0 20.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 0.5;
   camShakeRadius = 10.0;

   // Dynamic light
   lightStartRadius = 3;
   lightEndRadius = 0;
   lightStartColor = "0.1 0.1 0.1 0.1";
   lightEndColor = "0.5 0.5 0.5 0.5";
};


//projectile
datablock ProjectileData(ScytheProjectile)
{
   //projectileShapeName = "~/data/shapes/arrow.dts";
   explosion           = ScytheExplosion;
   //particleEmitter     = as;
   muzzleVelocity      = 50;

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
datablock ItemData(Scythe)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/player/left/sickle.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a Scythe';
	invName = "Scythe";
	image = ScytheImage;
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(ScytheImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/player/left/sickle.dts";
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
   correctMuzzleVector = false;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = Scythe;
   ammo = " ";
   projectile = ScytheProjectile;
   projectileType = Projectile;

   directDamage        = 60;
   radiusDamage        = 0;
   damageRadius        = 0.5;
   muzzleVelocity      = 50;
   velInheritFactor    = 1;
   damagetype          = '%1 was sent to the underworld by %2';

   //melee particles shoot from eye node for consistancy
   melee = true;
   //raise your arm up or not
   armReady = false;

   //casing = " ";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]                     = "Activate";
	
	
	stateTimeoutValue[0]             = 0.5;
	stateTransitionOnTimeout[0]       = "Ready";
	stateSound[0]					= ScytheDrawSound;
	
	

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "PreFire";
	stateAllowImageChange[1]         = true;

	stateName[2]			= "PreFire";
	stateScript[2]                  = "onPreFire";
	stateAllowImageChange[2]        = false;
	stateTimeoutValue[2]            = 0.1;
	stateTransitionOnTimeout[2]     = "Fire";

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "CheckFire";
	stateTimeoutValue[3]            = 0.2;
	stateFire[3]                    = true;
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	stateScript[3]                  = "onFire";
	stateWaitForTimeout[3]		= true;
	

	stateName[4]			= "CheckFire";
	stateTransitionOnTriggerUp[4]	= "StopFire";
	stateTransitionOnTriggerDown[4]	= "Fire";

	
	stateName[5]                    = "StopFire";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 0.2;
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;
	stateSequence[5]                = "StopFire";
	stateScript[5]                  = "onStopFire";


};




function ScytheImage::onPreFire(%this, %obj, %slot)
{

	%obj.playthread(2, armattack);
}

function ScytheImage::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);

}

function ScytheProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
tbmcollison(%this,%obj,%col,%fade,%pos,%normal);
}