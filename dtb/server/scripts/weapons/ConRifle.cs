datablock ParticleData( PlasmaBulletSparkParticle )
{
	textureName = "~/data/shapes/weapons/spark";

	dragCoefficient      = 1;
	gravityCoefficient   = 0.0;
	inheritedVelFactor   = 0.1;
	constantAcceleration = 0.0;

	lifetimeMS = 500;
	lifetimeVarianceMS = 350;

	times[0] = 0.0;
	times[1] = 0.5;
	times[2] = 1.0;

	colors[0] = "0.0 0.1 0.8 0.8";
	colors[1] = "0.0 0.2 0.8 0.65";
	colors[2] = "0.0 0.0 0.8 0.5";

	sizes[0] = 0.20;
	sizes[1] = 0.15;
	sizes[2] = 0.5;
};

datablock ParticleEmitterData( PlasmaBulletSparkParticleEmitter )
{
	particles = "PlasmaBulletSparkParticle";

	lifetimeMS = 500;
    lifetimeVarianceMS = 0;

	ejectionPeriodMS = 2;
	periodVarianceMS = 0;

	ejectionVelocity = 17;
	velocityVariance = 5;
};

datablock ParticleData( PlasmaBulletFireParticle )
{
	textureName = "~/data/shapes/weapons/plasma";
	useInvAlpha =  false;

	dragCoeffiecient = 10.0;
	inheritedVelFactor = 1;
      spinRandomMin = -750;
      spinRandomMax = 0;

	lifetimeMS = 100;
	lifetimeVarianceMS = 0;

	times[0] = 0.0;
	times[1] = 0.5;
	times[2] = 1.0;

	colors[0] = "0.0 0.1 0.8 0.9";
	colors[1] = "0.0 0.2 0.8 0.9";
	colors[2] = "0.0 0.0 0.8 0.9";

	sizes[0] = 2.5;
	sizes[1] = 1.5;
	sizes[2] = 1.5;
};

datablock ParticleEmitterData( PlasmaBulletFireParticleEmitter )
{
	particles = "PlasmaBulletFireParticle";

	lifetimeMS = 1000;
	lifetimeVarianceMS = 0;

	ejectionPeriodMS = 3;
	periodVarianceMS = 0;

	ejectionVelocity = 0;
	velocityVariance = 0.5;
};

datablock ExplosionData(PlasmaBulletExplosion)
{
   //explosionShape = "";
	soundProfile = QuantumGunExplosionSound;

   lifeTimeMS = 150;

   particleEmitter = PlasmaBulletFireParticleEmitter;
   particleDensity = 10;
   particleRadius = 0.2;

  emitter[0] = PlasmaBulletSparkParticleEmitter;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = false;
   camShakeFreq = "10.0 11.0 10.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 0.5;
   camShakeRadius = 10.0;

   // Dynamic light
   lightStartRadius = 15;
   lightEndRadius = 1;
   lightStartColor = "0.0 0.2 0.8 0.8";
   lightEndColor = "0 0 0";

};

datablock ParticleData(ConRifleTrailParticle)
{
textureName = "~/data/shapes/weapons/plasma";
	useInvAlpha =  false;
	dragCoeffiecient = 100.0;
	inheritedVelFactor = 0;
      spinRandomMin = -750;
      spinRandomMax = 0;
	lifetimeMS = 2000;
	
	lifetimeVarianceMS = 10;

	times[0] = 0.0;
	times[1] = 0.8;
	times[2] = 1.0;

	colors[0] = "0.0 0.0 0.8 1.0";
	colors[1] = "0.2 0.0 1.0 0.9";
	colors[2] = "0.6 0.6 1.0 0.8";

	sizes[0] = 2.5;
	sizes[1] = 1.5;
	sizes[2] = 1.5;
};

datablock ParticleEmitterData(ConRifleTrailEmitter)
{
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;
	lifetimeMS = 2000;
   ejectionVelocity = 0;
   velocityVariance = 0;

   ejectionOffset = 0;

   thetaMin         = 0.0;
   thetaMax         = 90.0;  

   particles = ConRifleTrailParticle;
};



datablock ParticleData( ConRifleBulletFireParticle )
{
	textureName = "~/data/shapes/weapons/plasma";
	useInvAlpha =  false;

	dragCoeffiecient = 100.0;
	inheritedVelFactor = 0;
      spinRandomMin = -750;
      spinRandomMax = 0;
	lifetimeMS = 1300;
	lifetimeVarianceMS = 200;

	times[0] = 0.0;
	times[1] = 0.5;
	times[2] = 1.0;

	colors[0] = "0.15 0.0 0.5 0.4";
	colors[1] = "0.2 0.0 0.7 0.5";
	colors[2] = "0.0 0.0 0.7 0.7";

	sizes[0] = 3;
	sizes[1] = 2;
	sizes[2] = 2;
};

datablock ParticleEmitterData( ConRifleBulletFireParticleEmitter )
{
	particles = "ConRifleBulletFireParticle";

	lifetimeMS = 250;
	lifetimeVarianceMS = 0;

	ejectionPeriodMS = 3;
	periodVarianceMS = 0;

	ejectionVelocity = 0;
	velocityVariance = 0;
};


datablock ExplosionData(ConRifleBulletExplosion)
{
   //explosionShape = "";
	soundProfile = QuantumGunExplosionSound;

   lifeTimeMS = 150;

   particleEmitter = ConRifleBulletFireParticleEmitter;
   particleDensity = 50;
   particleRadius = 7;

   emitter[0] = ConRifleBulletFireParticleEmitter;

   shakeCamera = true;
   camShakeFreq = "10.0 11.0 10.0";
   camShakeAmp = "10.0 10.0 10.0";
   camShakeDuration = 4.0;
   camShakeRadius = 20.0;

   // Dynamic light
   lightStartRadius = 0;
   lightEndRadius = 2;
   lightStartColor = "0.3 0.6 0.7";
   lightEndColor = "0 0 0";
};

//projectile
datablock ProjectileData(ConRifleBulletProjectile)
{
   projectileShapeName = "";
   explosion           = PlasmaBulletExplosion;
   particleEmitter     = PlasmaBulletFireParticleEmitter;
   muzzleVelocity      = 500;

   armingDelay         = 0;
   lifetime            = 8000;
   fadeDelay           = 7500;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 0;

   hasLight    = true;
   lightRadius = 15.0;
   lightColor  = "0.0 0.2 0.8 0.8";
};

//////////
// item //
//////////
datablock ItemData(ConRifle)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/weapons/plasmasniper.dts";
	skinName = 'grayDark';
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a ConRifle.';
	invName = "ConRifle";
	spawnName = "Con Rifle";
	image = ConRifleImage;
	threatlevel = "Normal";
};

addWeapon(ConRifle);

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(ConRifleImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/weapons/plasmasniper.dts";
   skinName = 'brown';
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
   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = ConRifle;
   ammo = " ";
   projectile = ConRifleBulletProjectile;
   projectileType = Projectile;

   projectileSpreadMax = 0;
   projectileSpreadWalking = 8/1000;
   projectileSpreadMax = 12/1000;

   directDamage        = 10;
   radiusDamage        = 7;
   damageRadius        = 1.5;
   damagetype          = '%1 was shattered by %2';
   muzzleVelocity      = 500;
   velInheritFactor    = 1;

   deathAnimationClass = "plasma";
   deathAnimation = "death5";
   deathAnimationPercent = 0.4;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = true;

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

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateAllowImageChange[1]         = true;

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "Reload";
	stateTimeoutValue[2]            = 0.1;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]			= true;
	stateSound[2]					= ConRifleFireSound;

	stateName[3]			= "Reload";
	stateSequence[3]                = "Reload";
	stateAllowImageChange[3]        = true;
	stateTimeoutValue[3]            = 0.1;
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTimeout[3]     = "Check";
	stateScript[3]                  = "onPreFire";
	
	stateName[4]			= "Check";
	stateTransitionOnTriggerUp[4]	= "StopFire";
	stateTransitionOnTriggerDown[4]	= "StopFire";

	stateName[5]                    = "StopFire";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 0.001;
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;
	//stateSequence[5]                = "Reload";
	stateScript[5]                  = "onStopFire";


};

function ConrifleImage::onPreFire(%this, %obj, %slot)
{	
	%obj.playthread(2, jump);
	muzzleflash(%this, %obj, %slot, 0.7, 0.7, 0.7);
}

function ConrifleImage::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}

function ConRifleBulletProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	tbmcollison(%this,%obj,%col,%fade,%pos,%normal);
       tbmradiusDamage
     (%obj, VectorAdd(%pos, VectorScale(%normal, 0.01)),
      %obj.damageRadius,%obj.radiusDamage,%obj.damageType,60);
}