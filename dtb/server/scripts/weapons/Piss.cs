datablock ParticleData(PissTrailParticle)
{
textureName = "~/data/particles/dot";
	useInvAlpha =  	true;

	dragCoeffiecient = 0.0;
	inheritedVelFactor = 0.0;

	lifetimeMS = 200;
	
	lifetimeVarianceMS = 100;

	times[0] = 0.0;
	times[1] = 0.8;
	times[2] = 1.0;

	colors[0] = "0.9 0.8 0.0 0.1";
	colors[1] = "0.8 0.7 0.0 0.1";
	colors[2] = "0.8 0.8 0.0 0.1";
	sizes[0] = 0.25;
	sizes[1] = 0.125;
	sizes[2] = 0.025;
};

datablock ParticleEmitterData(PissTrailEmitter)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;

   ejectionVelocity = 0;
   velocityVariance = 0;

   ejectionOffset = 0;

   thetaMin         = 0.0;
   thetaMax         = 90.0;  

   particles = PissTrailParticle;
};





datablock ExplosionData(PissExplosion)
{
   //explosionShape = "";
	soundProfile = sprayHitSound;

   lifeTimeMS = 150;

   particleEmitter = PissTrailEmitter;
	particleDensity = 20;
	particleRadius  = 0.5;

   emitter[0] = PissTrailEmitter;
   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = true;
   camShakeFreq = "10.0 11.0 10.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 0.5;
   camShakeRadius = 1.0;

   // Dynamic light
   lightStartRadius = 0;
   lightEndRadius = 0;
   lightStartColor = "0.2 0.055 0.0 0.8";
   lightEndColor = "0 0 0";
};


//projectile
datablock ProjectileData(PissProjectile)
{
   projectileShapeName = "";
   explosion           = PissExplosion;
   particleEmitter     = PissTrailEmitter;
   muzzleVelocity      = 20;
   dragCoeffiecient = 50.0;

   armingDelay         = 0;
   lifetime            = 3000;
   fadeDelay           = 3000;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 1;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0.2 0.055 0.0 0.8";
};


//////////
// item //
//////////
datablock ItemData(Piss)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "tbm/data/shapes/urinade.dts";
	skinName = 'yellow';
	rotate = true;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = false;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a bottle of Piss.';
	invName = "Piss";
	image = PissImage;
	threatlevel = "Normal";
};

addWeapon(Piss);

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(PissImage)
{
   // Basic Item properties
   shapeFile = "dtb/data/shapes/weapons/sithlightning.dts";
   skinName = 'yellow';
   emap = false;
   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "-0.5 -0.5 -0.5";
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
   item = Piss;
   ammo = " ";
   projectile = PissProjectile;
   projectileType = Projectile;

   directDamage        = 0;
   radiusDamage        = 5;
   damageRadius        = 1.5;
   damagetype          = '%2 pissed on %1';
   muzzleVelocity      = 20;
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
	stateTimeoutValue[0]             = 0.01;
	stateTransitionOnTimeout[0]       = "Ready";
	stateSound[0]					= weaponSwitchSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateAllowImageChange[1]         = true;

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "Reload";
	stateTimeoutValue[2]            = 0.01;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]			= true;
		stateSound[2]					= SprayFireSound;

	stateName[3]			= "Reload";
	stateSequence[3]                = "Reload";
	stateAllowImageChange[3]        = false;
	stateTimeoutValue[3]            = 0.01;
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTimeout[3]     = "Check";

	stateName[4]			= "Check";
	stateTransitionOnTriggerUp[4]	= "StopFire";
	stateTransitionOnTriggerDown[4]	= "Fire";

	stateName[5]                    = "StopFire";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 0.01;
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;
	//stateSequence[5]                = "Reload";
	stateScript[5]                  = "onStopFire";


};

function PissProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal){
	tbmcollison(%this, %obj, %col, %fade, %pos, %normal);
       tbmradiusDamage
     (%obj, VectorAdd(%pos, VectorScale(%normal, 0.01)),
      %obj.damageRadius,%obj.radiusDamage,%obj.damageType,20);
	if(%col.getType() == 67108869)
		return;
	if(%col.getMountedImage(0) == nameToID(flameSmallImage) || %col.getMountedImage(0) == nameToID(flameImage) || %col.getMountedImage(0) == nameToID(flameBigImage) || %col.getMountedImage(0) == nameToID(smokeImage) || %col.getMountedImage(0) == nameToID(smokeBigImage))
		%col.unmountImage(0);
	if(%col.getClassName() $= "Player" || %col.getClassName() $= "AIPlayer")
		%col.extinguish();

}