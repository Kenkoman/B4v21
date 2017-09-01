//bullet trail
datablock ParticleData(agcTrailParticle)
{
	dragCoefficient		= 3.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.9;
	constantAcceleration	= 0.0;
	lifetimeMS		= 900;
	lifetimeVarianceMS	= 0;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= false;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "~/data/particles/cloud";
	//animTexName		= "~/data/particles/dot";

	// Interpolation variables
	colors[0]	= "1 0 0 0.125";
	colors[1]	= "0 0 1 1.0";
	sizes[0]	= 1.0;
	sizes[1]	= 0.0;
	times[0]	= 0.0;
	times[1]	= 1.0;
};

datablock ParticleEmitterData(agcTrailEmitter)
{
   ejectionPeriodMS = 2;
   periodVarianceMS = 0;

   ejectionVelocity = 10; //0.25;
   velocityVariance = 0; //0.10;

   ejectionOffset = 0;

   thetaMin         = 0.0;
   thetaMax         = 90.0;  

   particles = "agcTrailParticle";
};

//effects
datablock ParticleData(agcExplosionParticle)
{
	dragCoefficient      = 5;
	gravityCoefficient   = 0.1;
	inheritedVelFactor   = 0.2;
	constantAcceleration = 0.0;
	lifetimeMS           = 500;
	lifetimeVarianceMS   = 300;
	textureName          = "~/data/particles/chunk";
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	colors[0]     = "0.9 0.9 0.6 0.9";
	colors[1]     = "0.9 0.5 0.6 0.0";
	sizes[0]      = 0.25;
	sizes[1]      = 0.0;
};

datablock ParticleEmitterData(agcExplosionEmitter)
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
   particles = "agcExplosionParticle";
};

datablock ExplosionData(agcExplosion)
{
   //explosionShape = "";
	soundProfile = bulletExplosionSound;

   lifeTimeMS = 150;

   particleEmitter = agcExplosionEmitter;
   particleDensity = 10;
   particleRadius = 0.2;

   emitter[0] = agcExplosionEmitter;
   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = false;
   camShakeFreq = "10.0 11.0 10.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 0.5;
   camShakeRadius = 10.0;

   // Dynamic light
   lightStartRadius = 0;
   lightEndRadius = 2;
   lightStartColor = "0.7 0 0";
   lightEndColor = "0 0 0";
};


//projectile
datablock ProjectileData(agcProjectile)
{
//   projectileShapeName = "~/data/shapes/weapons/redblast.dts";
   explosion           = agcExplosion;
   particleEmitter     = agcTrailEmitter;
   muzzleVelocity      = 60;

   armingDelay         = 0;
   lifetime            = 80000;
   fadeDelay           = 75000;
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
datablock ItemData(agc)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/weapons/agc.dts";
	skinName = 'black';
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'an Anti-Gravitational Cannon.';
        spawnname = "Anti-Gravity Cannon";
	invName = "A.G.C";
	image = agcImage;
	threatlevel = "Safe";
};

addWeapon(agc);

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(agcImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/weapons/agc.dts";
   skinName = 'brown';
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   eyeOffset = "0.8 0.4 -0.75";

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = agc;
   ammo = " ";
   projectile = agcProjectile;
   projectileType = Projectile;

   directDamage        = 0;
   radiusDamage        = 0;
   damageRadius        = 0.5;
   muzzleVelocity      = 60;
   velInheritFactor    = 0;

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
	stateTimeoutValue[2]            = 0.2;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]			= true;
	stateSound[2]					= gunpackFireSound;

	stateName[3]			= "Reload";
	stateSequence[3]                = "Reload";
	stateAllowImageChange[3]        = false;
	stateTimeoutValue[3]            = 0.6;
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
function agcProjectile::onCollision(%this, %obj, %col, %fade, %pos, %normal)
{
   %pos[0] = getWord(%pos,0) - 16;
   %pos[1] = getWord(%pos,1) + 16;
   %pos[2] = getWord(%pos,2) - 16;
   %zone = new PhysicalZone() {
      position = %pos[0] SPC %pos[1] SPC %pos[2];
      rotation = "1 0 0 0";
      scale = "32 32 32";
      velocityMod = "1.0";
      gravityMod = "-0.5";
      appliedForce = "0 0 512";
      polyhedron = "0.0000000 0.0000000 0.0000000 1.0000000 0.0000000 0.0000000 0.0000000 -1.0000000 0.0000000 0.0000000 0.0000000 1.0000000";
   };
   %zone.schedule(10000, delete);
}