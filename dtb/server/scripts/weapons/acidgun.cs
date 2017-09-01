datablock ParticleData(AcidGunTrailParticle)
{
textureName = "~/data/shapes/weapons/fire";
	useInvAlpha =  false;

	dragCoeffiecient = 100.0;
	inheritedVelFactor = 1.0;

	lifetimeMS = 1500;
	
	lifetimeVarianceMS = 0;

	times[0] = 0.1;
	times[1] = 0.6;
	times[2] = 1.0;

	colors[0] = "0.0 0.8 0.0 0.8";
	colors[1] = "0.0 0.7 0.01 0.8";
	colors[2] = "0.0 0.0 0.0 0.0";
	sizes[0] = 1.0;
	sizes[1] = 5.0;
	sizes[2] = 10.0;
};

datablock ParticleEmitterData(AcidgunTrailEmitter)
{
   ejectionPeriodMS = 100;
   periodVarianceMS = 0;

   ejectionVelocity = 0.5;
   velocityVariance = 0.10;

   ejectionOffset = 0;

   thetaMin         = 0.0;
   thetaMax         = 90.0;  

   particles = AcidGunTrailParticle;
};

datablock ExplosionData(AcidgunExplosion)
{
   //explosionShape = "";
	soundProfile = FlamethrowerFireSound;

   lifeTimeMS = 150;

   particleEmitter = AcidgunTrailEmitter;
	particleDensity = 10;
	particleRadius  = 1.0;

   emitter[0] = AcidgunTrailEmitter;
   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = false;
   camShakeFreq = "10.0 11.0 10.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 0.5;
   camShakeRadius = 10.0;

   // Dynamic light
   lightStartRadius = 0;
   lightEndRadius = 0;
   lightStartColor = "0.2 0.055 0.0 0.8";
   lightEndColor = "0 0 0";
};


//projectile
datablock ProjectileData(AcidgunProjectile)
{
   projectileShapeName = "";
   explosion           = AcidgunExplosion;
   particleEmitter     = AcidgunTrailEmitter;
   muzzleVelocity      = 30;

   dragCoeffiecient = 50.0;

   armingDelay         = 0;
   lifetime            = 2000;
   fadeDelay           = 0;
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
datablock ItemData(Acidgun)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/weapons/Flamethrower.dts";
	skinName = 'grayDark';
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = false;

	 // Dynamic properties defined by the scripts
	pickUpName = 'an Acid Gun.';
	invName = "Acid Gun";
	image = AcidgunImage;
	threatlevel = "Normal";
};

addWeapon(Acidgun);

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(AcidgunImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/weapons/Flamethrower.dts";
   skinName = 'brown';
   emap = false;
   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0.15";
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
   item = Acidgun;
   ammo = " ";
   projectile = AcidgunProjectile;
   projectileType = Projectile;

   projectileSpread = 10/1000;
   projectileSpreadWalking = 15/1000;
   projectileSpreadMax = 20/1000;

   directDamage        = 15;
   radiusDamage        = 5;
   damageRadius        = 5;
   damagetype          = '%1 was melted by %2';
   muzzleVelocity      = 30;
   velInheritFactor    = 1;

   deathAnimationClass = "plasma";
   deathAnimation = "greengooblob";
   deathAnimationPercent = 1;

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
	stateTimeoutValue[2]            = 0.09;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]			= true;
	stateSound[2]					= sprayFireSound;

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

function AcidGunProjectile::onCollision(%this, %obj, %col, %fade, %pos, %normal)
{
	tbmcollison(%this,%obj,%col,%fade,%pos,%normal);
       tbmradiusDamage
     (%obj, VectorAdd(%pos, VectorScale(%normal, 0.01)),
      %obj.damageRadius, %obj.radiusDamage, %obj.damageType, 20);
}
