//Simba's Elcipse Cannon edited by DShiznit

datablock ParticleData(GodTrailParticle)
{
textureName = "dtb/data/shapes/weapons/plasma";
	useInvAlpha =  false;

	dragCoeffiecient = 100.0;
	inheritedVelFactor = 0.01;

	lifetimeMS = 1500;
	
	lifetimeVarianceMS = 10;

	times[0] = 9.9;
	times[1] = 9.9;
	times[2] = 9.9;

	colors[0] = "0 1 0 1";
	colors[1] = ".5 1 .5 1";
	colors[2] = "1 1 1 1";

	sizes[0] = 25.25;
	sizes[1] = 25.25;
	sizes[2] = 25.25;
};

datablock ParticleEmitterData(GodTrailEmitter)
{
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;

   ejectionVelocity = 0.5;
   velocityVariance = 0.10;

   ejectionOffset = 0;

   thetaMin         = 0.0;
   thetaMax         = 90.0;  

   particles = GodTrailParticle;
};



datablock ParticleData(GodExplosionParticle)
{
	textureName = "dtb/data/shapes/weapons/plasma";
	useInvAlpha =  false;

	dragCoeffiecient = 100.0;
	inheritedVelFactor = 0.3;

	lifetimeMS = 1300;
	lifetimeVarianceMS = 200;

	times[0] = 0;
	times[1] = 0.8;
	times[2] = 1;

	colors[0] = "1 1 1 1";
	colors[1] = "1 1 0 1";
	colors[2] = "1 0.5 0 1";

	sizes[0] = 25.25;
	sizes[1] = 25.25;
	sizes[2] = 25.25;
};

datablock ParticleEmitterData(GodExplosionEmitter)
{
	particles = "GodExplosionParticle";

	lifetimeMS = 250;
	lifetimeVarianceMS = 0;

	ejectionPeriodMS = 10;
	periodVarianceMS = 0;

	ejectionVelocity = 10.5;
	velocityVariance = 4.5;
};

datablock ExplosionData(GodExplosion)
{
   //explosionShape = "";
	soundProfile = QuantumGunExplosionSound;

   lifeTimeMS = 1500;

   particleEmitter = GodExplosionEmitter;
   particleDensity = 50;
   particleRadius = 20;

   emitter[0] = GodExplosionEmitter;

   shakeCamera = true;
   camShakeFreq = "10.0 11.0 10.0";
   camShakeAmp = "10.0 10.0 10.0";
   camShakeDuration = 4.0;
   camShakeRadius = 20.0;

   // Dynamic light
   lightStartRadius = 10;
   lightEndRadius = 10;
   lightStartColor = "0.0 0.8 0.2 0.8";
   lightEndColor = "0 0 0";
};


//projectile
datablock ProjectileData(GodProjectile)
{
   projectileShapeName = ""; //this needs some sort of invisible disc for collision purposes
   explosion           = GodExplosion;
   particleEmitter     = GodTrailEmitter;
   muzzleVelocity      = 1000;

   scale               = "20 20 20";
   armingDelay         = 0;
   lifetime            = 8000;
   fadeDelay           = 7500;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 0;

   hasLight    = true;
   lightRadius = 5.0;
   lightColor  = "0.0 0.8 0.2 0.8";
};


//////////
// item //
//////////
datablock ItemData(God)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "dtb/data/shapes/weapons/handgun.dts";
	skinName = 'grayDark';
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'The fist of God.';
	invName = "God's Fist";
	spawnName = "God's Fist";
	image = Godimage;
	threatlevel = "Dangerous";
};

addWeapon(God);

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(GodImage)
{
   // Basic Item properties
   shapeFile = "dtb/data/shapes/weapons/handgun.dts";
   skinName = 'brown';
   emap = true;
    
   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0.1";
//   eyeOffset = "0 0 0";

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = God;
   ammo = "";
   projectile = GodProjectile;
   projectileType = Projectile;

   directDamage        = 0;
   radiusDamage        = 1200;
   damageRadius        = 16;
   damagetype          = '%1 hath been owned by thy lord and father %2';
   muzzleVelocity      = 1000;
   velInheritFactor    = 1;

   deathAnimationClass = "explosion";
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
	stateTimeoutValue[0]             = 2.0;
	stateTransitionOnTimeout[0]       = "Ready";

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateAllowImageChange[1]         = true;

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "Reload";
	stateTimeoutValue[2]            = 0.5;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]			= true;
	stateSound[2]					= ConRifleFireSound;

	stateName[3]			= "Reload";
	stateSequence[3]                = "Reload";
	stateAllowImageChange[3]        = true;
	stateTimeoutValue[3]            = 0.5;
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTimeout[3]     = "Check";
	
	stateName[4]			= "Check";
	stateTransitionOnTriggerUp[4]	= "StopFire";
	stateTransitionOnTriggerDown[4]	= "Fire";

	stateName[5]                    = "StopFire";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 0.2;
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;
	//stateSequence[5]                = "Reload";
	stateScript[5]                  = "onStopFire";


};

function GodProjectile::onCollision(%this, %obj, %col, %fade, %pos, %normal)
{
	tbmcollison(%this, %obj, %col, %fade, %pos, %normal);
       tbmradiusDamage
     (%obj, VectorAdd(%pos, VectorScale(%normal, 0.01)),
      %obj.damageRadius, %obj.radiusDamage, %obj.damageType, 60);
}

function GodImage::onMount(%this, %obj)
{
	%obj.playthread(1, armreadyright);
	messageAll( 'MsgClientKilled','%1 \c2pulled out a \c9GOD\c2 gun. Fear %1\'s wrath!', %obj.client.name);
}