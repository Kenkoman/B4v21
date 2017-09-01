//sword.cs
datablock AudioProfile(swordDrawSound)
{
   filename    = "~/data/sound/swordDraw.wav";
   description = AudioClosest3d;
   preload = true;
};
datablock AudioProfile(swordHitSound)
{
   filename    = "~/data/sound/swordHit.wav";
   description = AudioClosest3d;
   preload = true;
};


//effects
datablock ParticleData(swordExplosionParticle)
{
   dragCoefficient      = 5;
   gravityCoefficient   = 0.2;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 500;
   lifetimeVarianceMS   = 300;
   textureName          = "~/data/particles/chunk";
   colors[0]     = "0.7 0.7 0.9 0.9";
   colors[1]     = "0.9 0.9 0.9 0.0";
   sizes[0]      = 0.5;
   sizes[1]      = 1.0;
};

datablock ParticleEmitterData(swordExplosionEmitter)
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
   particles = "swordExplosionParticle";
};

//datablock ExplosionData(swordExplosion)
//{
//   //explosionShape = "";
//   lifeTimeMS = 100;
//
//   soundProfile = swordHitSound;
//
//   Emitter[0] = swordExplosionEmitter;
//   particleDensity = 10;
//   particleRadius = 0.2;
//
//   faceViewer     = true;
//   explosionScale = "1 1 1";
//
//   shakeCamera = true;
//   camShakeFreq = "20.0 22.0 20.0";
//   camShakeAmp = "1.0 1.0 1.0";
//   camShakeDuration = 0.5;
//   camShakeRadius = 10.0;
//
//   // Dynamic light
//   lightStartRadius = 3;
//   lightEndRadius = 0;
//   lightStartColor = "00.0 0.2 0.6";
//   lightEndColor = "0 0 0";
//};


//projectile
datablock ProjectileData(swordProjectile)
{
   //projectileShapeName = "~/data/shapes/arrow.dts";
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
datablock ItemData(sword)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/sword.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a sword';
	invName = "Sword";
	image = swordImage;
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(swordImage)
{
   // Basic Item properties
   shapeFile = "tbm/data/shapes/sword.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
    
   offset = "0 0 0.1";
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
   item = sword;
   ammo = " ";
   projectile = swordProjectile;
   projectileType = Projectile;

   directDamage        = 50;
   radiusDamage        = 0;
   damageRadius        = 0.5;
   damagetype          = '%1 was slashed by %2';
   muzzleVelocity      = 50;
   velInheritFactor    = 1;

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
	stateSound[0]					= swordDrawSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "PreFire";
	stateAllowImageChange[1]         = true;

	stateName[2]			= "PreFire";
	stateScript[2]                  = "onPreFire";
	stateAllowImageChange[2]        = false;
	stateTimeoutValue[2]            = 0.0999;
	stateTransitionOnTimeout[2]     = "Fire";

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "CheckFire";
	stateTimeoutValue[3]            = 0.2;
	stateFire[3]                    = true;
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	stateScript[3]                  = "onFire";
	stateWaitForTimeout[3]		= true;
	//stateTransitionOnTriggerUp[3]	= "StopFire";

	stateName[4]			= "CheckFire";
	stateTimeoutValue[4]            = 0.0501;
	stateScript[4]                  = "onStopFire";
	stateTransitionOnTimeout[4]     = "StopFire";

	
	stateName[5]                    = "StopFire";
	stateTransitionOnTriggerUp[5]     = "Ready";
	stateAllowImageChange[5]        = false;
	stateSequence[5]                = "StopFire";
	stateScript[5]                  = "onStopFire";


};

function swordImage::onPreFire(%this, %obj, %slot)
{
	//messageAll( 'MsgClient', 'sword prefired!!!');
	//Parent::onFire(%this, %obj, %slot);
	%obj.playthread(2, armattack);
}

function swordImage::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
	//messageAll( 'MsgClient', 'stopfire');
}

function swordProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal) {
    if($gobblesdm)
    Gobprojectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,20,25);
}