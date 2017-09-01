
//bullet trail
datablock ParticleData(FlameCannonTrailParticle)
{
	dragCoefficient		= 100.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 1500;
	lifetimeVarianceMS	= 10;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= false;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "~/data/particles/cloud";
	//animTexName		= "~/data/particles/dot";

	// Interpolation variables
	colors[0]	= "10 0 0 0.5";
	colors[1]	= "10 0.0 0 0.5";
	sizes[0]	= 0.2;
	sizes[1]	= 0.01;
	times[0]	= 0.0;
	times[1]	= 1.0;
};

datablock ParticleEmitterData(FlameCannonTrailEmitter)
{
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;

   ejectionVelocity = 0.5; //0.25;
   velocityVariance = 0.10; //0.10;

   ejectionOffset = 0;

   thetaMin         = 0.0;
   thetaMax         = 90.0;  

   particles = "FlameCannonTrailParticle";
};

//effects
datablock ParticleData(FlameCannonExplosionParticle)
{
	dragCoefficient      = 1;
	gravityCoefficient   = 0;
	inheritedVelFactor   = 0;
	constantAcceleration = 1.0;
	lifetimeMS           = 2000;
	lifetimeVarianceMS   = 100;
	textureName          = "~/data/particles/cloud";
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	times[0] = 0.0;
	times[1] = 0.6;
	times[2] = 1.0;
	colors[0] = "5.0 0.0 0.0 0.8";
	colors[1] = "5.0 0.0 0.0 0.8";
	colors[2] = "5.0 0.0 0.0 0.8";
	sizes[0]      = 10.0;
	sizes[1]      = 5.0;
	sizes[2]      = 1.0;
};

datablock ParticleEmitterData(FlameCannonExplosionEmitter)
{
   ejectionPeriodMS = 4000;
   periodVarianceMS = 25;
   ejectionVelocity = 18;
   velocityVariance = 1.0;
   ejectionOffset   = 15.0;
   thetaMin         = 0;
   thetaMax         = 0;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = true;
   particles = "FlameCannonExplosionParticle";
};

datablock ExplosionData(FlameCannonExplosion)
{
   //explosionShape = "";
	soundProfile = GrenadeExplosionSound;

   lifeTimeMS = 400;

   particleEmitter = FlameCannonExplosionEmitter;
   particleDensity = 100;
   particleRadius = 0.1;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = false;
   camShakeFreq = "10.0 11.0 10.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 0.5;
   camShakeRadius = 10.0;

   // Dynamic light
   lightStartRadius = 0;
   lightEndRadius = 4;
   lightStartColor = "5 0 0";
   lightEndColor = "2 0 0";
};


//projectile
datablock ProjectileData(FlameCannonProjectile)
{
   projectileShapeName = "~/data/shapes/weapons/shotgunbullet.dts";
   explosion           = FlameCannonExplosion;
   particleEmitter     = FlameCannonTrailEmitter;
   muzzleVelocity      = 100;

   armingDelay         = 0;
   lifetime            = 60*1000;
   fadeDelay           = 40*1000;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 0;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "0.7 0 0";
};


//////////
// item //
//////////
datablock ItemData(FlameCannon)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/weapons/MissileLauncher.dts";
	skinName = 'black';
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a FlameCannon.';
	invName = "Flame Cannon";
	image = FlameCannonImage;
};


////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(FlameCannonImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/weapons/MissileLauncher.dts";
   skinName = 'brown';
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
   item = FlameCannon;
   ammo = " ";
   projectile = FlameCannonProjectile;
   projectileType = Projectile;

   directDamage        = 20;
   radiusDamage        = 45;
   damageRadius        = 30;
   damagetype          = '%1 was hit by %2\'s flames of doom';
   muzzleVelocity      = 100;
   velInheritFactor    = 1;

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
	stateTimeoutValue[2]            = 0.5;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]			= true;
	stateSound[2]					= FlamethrowerFireSound;

	stateName[3]			= "Reload";
	stateSequence[3]                = "Reload";
	stateAllowImageChange[3]        = false;
	stateTimeoutValue[3]            = 3;
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

function FlameCannonProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	tbmcollison(%this,%obj,%col,%fade,%pos,%normal);
       tbmradiusDamage
     (%obj, VectorAdd(%pos, VectorScale(%normal, 0.01)),
      %obj.damageRadius,%obj.radiusDamage,%obj.damageType,1000);
}
