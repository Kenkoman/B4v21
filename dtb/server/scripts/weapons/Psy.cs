//bullet trail
datablock ParticleData(PsybeamTrailParticle)
{
	dragCoefficient		= 0.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 800;
	lifetimeVarianceMS	= 0;
	spinSpeed		= 0;
	spinRandomMin		= 0;
	spinRandomMax		= 0;
	useInvAlpha		= false;
	animateTexture		= false;
	textureName		= "~/data/particles/bubble";


	// Interpolation variables
	colors[0]	= "7.7 7.7 7.7 7.7";
	colors[1]	= "9.9 9.9 9.9 9.9";
	sizes[0]	= 3000.3000;
	sizes[1]	= 3000.3000;
	times[0]	= 35.35;
	times[1]	= 35.35;
};

datablock ParticleEmitterData(PsybeamTrailEmitter)
{
  ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   ejectionVelocity = 0; //0.25;
   velocityVariance = 0; //0.10;
   ejectionOffset = 0;
   thetaMin         = 0.0;
   thetaMax         = 90.0;  


   particles = PsybeamTrailParticle;
};

//effects
datablock ParticleData(PsybeamExplosionParticle)
{
	dragCoefficient      = 5;
	gravityCoefficient   = 0.1;
	inheritedVelFactor   = 0.2;
	constantAcceleration = 0.0;
	lifetimeMS           = 500;
	lifetimeVarianceMS   = 300;
	textureName          = "~/data/particles/bubble";
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	colors[0]     = "7.7 7.7 7.7 7.7";
	colors[1]     = "9.9 9.9 9.9 9.9";
	sizes[0]      = 3000.3000;
	sizes[1]      = 3000.3000;
};

datablock ParticleEmitterData(PsybeamExplosionEmitter)
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   ejectionVelocity = 5;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "PsybeamExplosionParticle";
};

datablock ExplosionData(PsybeamExplosion)
{
   //explosionShape = "";
	soundProfile = bulletExplosionSound;

   lifeTimeMS = 150;

   particleEmitter = PsybeamExplosionEmitter;
   particleDensity = 10;
   particleRadius = 0.2;

   emitter[0] = PsybeamExplosionEmitter;
   faceViewer     = true;
   explosionScale = "35 35 35";

   shakeCamera = true;
   camShakeFreq = "1.0 2.0 1.0";
   camShakeAmp = "360.0 180.0 360.0";
   camShakeDuration = 30.0;
   camShakeRadius = 2.0;

   // Dynamic light
   lightStartRadius = 0;
   lightEndRadius = 2;
   lightStartColor = "0.3 0.6 0.7";
   lightEndColor = "0 0 0";
};


//projectile
datablock ProjectileData(PsybeamProjectile)
{
   projectileShapeName = "";
   explosion           = PsybeamExplosion;
   particleEmitter     = PsybeamTrailEmitter;
   muzzleVelocity      = 300;

   armingDelay         = 0;
   lifetime            = 8000;
   fadeDelay           = 7500;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 0;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";
};


//////////
// item //
//////////
datablock ItemData(Psybeam)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "dtb/data/shapes/weapons/riftcannon.dts";
	skinName = 'green';
	rotate = true;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a funlaser.';
	invName = "Psybeam";
	image = psybeamImage;
	threatlevel = "Normal";
};

addWeapon(Psybeam);

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(PsybeamImage)
{
   // Basic Item properties
   shapeFile = "dtb/data/shapes/weapons/riftcannon.dts";
   skinName = 'black';
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
   item = Psybeam;
   ammo = " ";
   projectile = PsybeamProjectile;
   projectileType = Projectile;

   directDamage        = 0;
   radiusDamage        = 5;
   damageRadius        = 1;
   damagetype          = '%1 got bubblized by %2';
   muzzleVelocity      = 300;
   velInheritFactor    = 1;

   deathAnimationClass = "default";
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
	stateSound[0]					= weaponSwitchSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateAllowImageChange[1]         = true;

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "Reload";
	stateTimeoutValue[2]            = 0.05;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]			= true;
	stateSound[2]					= kickerFireSound;

	stateName[3]			= "Reload";
	stateSequence[3]                = "Reload";
	stateAllowImageChange[3]        = false;
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

function PsybeamProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	tbmcollison(%this, %obj, %col, %fade, %pos, %normal);
       tbmradiusDamage
     (%obj, VectorAdd(%pos, VectorScale(%normal, 0.01)),
      %obj.damageRadius,%obj.radiusDamage,%obj.damageType,20);
}