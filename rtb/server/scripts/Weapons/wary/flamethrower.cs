datablock AudioProfile(FlamethrowerFireSound)
{
   filename    = "~/data/sound/wary/flamethrowerfire.wav";
   description = AudioClose3d;
   preload = true;
};

datablock ParticleData( GLFireParticle )
{
	textureName = "~/data/shapes/wary/fire";
	useInvAlpha =  false;

	dragCoeffiecient = 100.0;
	inheritedVelFactor = 0.3;

	lifetimeMS = 1200;
	
	lifetimeVarianceMS = 300;

	times[0] = 0.0;
	times[1] = 0.5;
	times[2] = 1.0;

	colors[0] = "0.7 0.2 0.0 0.8";
	colors[1] = "0.2 0.0 0.0 0.8";
	colors[2] = "0.0 0.0 0.0 0.0";

	sizes[0] = 1.5;
	sizes[1] = 0.9;
	sizes[2] = 0.5;
};

datablock ParticleEmitterData( GLFireParticleEmitter )
{
	particles = "GLFireParticle";

	lifetimeMS = 250;

	lifetimeVarianceMS = 0;

	
	ejectionPeriodMS = 10;
	periodVarianceMS = 0;

	ejectionVelocity = 0.8;
	velocityVariance = 0.5;
};
datablock ParticleData(FlamethrowerTrailParticle)
{
textureName = "~/data/shapes/wary/smoke";
	useInvAlpha =  false;

	dragCoeffiecient = 100.0;
	inheritedVelFactor = 1.0;

	lifetimeMS = 1000;
	
	lifetimeVarianceMS = 10;

	times[0] = 0.0;
	times[1] = 0.8;
	times[2] = 1.0;

	colors[0] = "0.2 0.1 0.0 0.8";
	colors[1] = "0.2 0.055 0.0 0.8";
	colors[2] = "0.0 0.0 0.0 0.0";
	sizes[0] = 1.0;
	sizes[1] = 3.5;
	sizes[2] = 4.5;
};

datablock ParticleEmitterData(FlamethrowerTrailEmitter)
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;

   ejectionVelocity = 0.5;
   velocityVariance = 0.10;

   ejectionOffset = 0;

   thetaMin         = 0.0;
   thetaMax         = 90.0;  

   particles = FlamethrowerTrailParticle;
};


datablock ExplosionData(FlamethrowerExplosion)
{
   //explosionShape = "";
	soundProfile = FlamethrowerFireSound;

   lifeTimeMS = 150;

   particleEmitter = GLFireParticleEmitter;
	particleDensity = 25;
	particleRadius  = 0.5;

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
datablock ProjectileData(FlamethrowerProjectile)
{
   projectileShapeName = "";
   directDamage        = 2;
   radiusDamage        = 1;
   damageRadius        = 1.5;
   explosion           = FlamethrowerExplosion;
 particleEmitter     = FlamethrowerTrailEmitter;

   muzzleVelocity      = 60;
dragCoeffiecient = 50.0;
   velInheritFactor    = 1;

   armingDelay         = 0;
   lifetime            = 1500;
   fadeDelay           = 1500;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 1;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0.2 0.055 0.0 0.8";
damagetype        = '%1 got BURNED by %2';
};


//////////
// item //
//////////
datablock ItemData(Flamethrower)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/wary/Flamethrower.dts";
	skinName = 'grayDark';
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a Flamethrower.';
	invName = "Flamethrower";
	image = FlamethrowerImage;
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(FlamethrowerImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/wary/Flamethrower.dts";
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
   item = Flamethrower;
   ammo = " ";
   projectile = FlamethrowerProjectile;
   projectileType = Projectile;

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
	stateTimeoutValue[2]            = 0.01;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]			= true;
		stateSound[2]					= FlamethrowerFireSound;

	stateName[3]			= "Reload";
	stateSequence[3]                = "Reload";
	stateAllowImageChange[3]        = false;
	stateTimeoutValue[3]            = 0.0002;
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






function FlamethrowerProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal) 
{ 
	weaponDamage(%obj,%col,%this,%pos,Flamethrower);
	radiusDamage(%obj,%pos,%this.damageRadius,%this.radiusDamage,bow,%this.areaImpulse);
}