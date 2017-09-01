//trail
datablock ParticleData(QuantumGunParticle)
{
	dragCoefficient		= 0.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0;
	lifetimeMS		= 1000;
	lifetimeVarianceMS	= 0;
	spinSpeed		= 0;
	spinRandomMin		= 0;
	spinRandomMax		= 0;
	colors[0]	= "1 1 1 1";
	colors[1]	= "0.1 0.75 0.1 0.75";
	colors[2]	= "0 0.85 0 0.5";
	colors[3]	= "0 1 0 0.25";
	sizes[0]	= 1;
	sizes[1]	= 1;
	sizes[2]	= 1;
	sizes[3]	= 1;
	times[0]	= 0.0;
	times[1]	= 0.3;
	times[2]	= 0.9;
	times[3]	= 1;
	useInvAlpha		= false;
	animateTexture		= false;
	textureName		= "~/data/shapes/weapons/QuantumRing";
};

datablock ParticleEmitterData(QuantumGunEmitter)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   ejectionVelocity = 0; //0.25;
   velocityVariance = 0; //0.10;
   ejectionOffset = 0;
   thetaMin         = 0.0;
   thetaMax         = 90.0;  
   particles = QuantumGunParticle;
};

//effects
datablock ParticleData(QuantumGunExplosionParticle)
{
	dragCoefficient      = 5;
	gravityCoefficient   = 0.1;
	inheritedVelFactor   = 0.2;
	constantAcceleration = 0.0;
	lifetimeMS           = 1000;
	lifetimeVarianceMS   = 300;
	textureName          = "~/data/shapes/weapons/QuantumEx";
	useInvAlpha		= true;
	animateTexture		= false;
	spinSpeed		= 400.0;
	spinRandomMin		= -800.0;
	spinRandomMax		= 0.0;
	colors[0]	= "1 1 1 1";
	colors[1]	= "0.3 0.75 0.3 0.75";
	colors[2]	= "0.1 0.85 0.1 0.5";
	colors[3]	= "0 1 0 0.25";
	sizes[0]	= 1.5;
	sizes[1]	= 1;
	sizes[2]	= 0.5;
	sizes[3]	= 0;
	times[0]	= 0.0;
	times[1]	= 0.4;
	times[2]	= 0.7;
	times[3]	= 1;
};

datablock ParticleEmitterData(QuantumGunExplosionEmitter)
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
   particles = "QuantumGunExplosionParticle";
};

datablock ExplosionData(QuantumGunExplosion)
{
   //explosionShape = "";
	soundProfile = QuantumGunExplosionSound;

   lifeTimeMS = 500;

   particleEmitter = QuantumGunExplosionEmitter;
   particleDensity = 50;
   particleRadius = 3;
   emitter[0] = QuantumGunExplosionEmitter;
   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = true;
   camShakeFreq = "10.0 11.0 10.0";
   camShakeAmp = "10.0 10.0 10.0";
   camShakeDuration = 3;
   camShakeRadius = 60.0;

   // Dynamic light
   lightStartRadius = 8;
   lightEndRadius = 0;
   lightStartColor = "0 1 0";
   lightEndColor = "0 0 0";
};


//projectile
datablock ProjectileData(QuantumGunProjectile)
{
   projectileShapeName = "~/data/shapes/weapons/quantumbeam.dts";
   explosion           = QuantumGunExplosion;
   particleEmitter     = QuantumGunEmitter;
   muzzleVelocity      = 500;

   armingDelay         = 0;
   lifetime            = 8000;
   fadeDelay           = 7500;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 0;

   hasLight    = true;
   lightRadius = 10.0;
   lightColor  = "0 1 0";
};

//////////
// item //
//////////
datablock ItemData(QuantumGun)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/weapons/QuantumGun.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a QuantumGun.';
	invName = "QuantumGun";
	spawnName = "Quantum Gun";
	image = QuantumGunImage;
	threatlevel = "Normal";
};

addWeapon(QuantumGun);

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(QuantumGunImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/weapons/QuantumGun.dts";
   emap = true;
   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0.1";
   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = false;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = QuantumGun;
   ammo = " ";
   projectile = QuantumGunProjectile;
   projectileType = Projectile;

   directDamage        = 100;
   radiusDamage        = 25;
   damageRadius        = 5;
   damagetype          = '%1 now knows not to mess with %2\'s radioactive materials';
   muzzleVelocity      = 500;
   velInheritFactor    = 0;

   deathAnimationClass = "explosion";
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
	stateSound[0]			= QuantumGunSelectSound;

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
	stateWaitForTimeout[2]		= true;
	stateSound[2]			= QuantumGunFireSound;

	stateName[3]			= "Reload";
	stateAllowImageChange[3]        = false;
	stateTimeoutValue[3]            = 2;
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTimeout[3]     = "Check";

	stateName[4]			= "Check";
	stateTransitionOnTriggerUp[4]	= "StopFire";
	stateTransitionOnTriggerDown[4]	= "Fire";

	stateName[5]                    = "StopFire";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 2;
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;
	//stateSequence[5]                = "Reload";
	stateScript[5]                  = "onStopFire";


};


function QuantumGunProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	if (%obj.client && (%col.getClassName() $= "Player" || %col.getClassName() $= "AIPlayer")) 
		%col.setVelocity(vectorScale(%obj.client.player.getEyeVector(), 50));
	tbmcollison(%this, %obj, %col, %fade, %pos, %normal);
}
function QuantumGunImage::onFire(%this,%obj,%slot)
{
	%p = Parent::onFire(%this, %obj, %slot);
	//%projectile.schedule(2000,MakeExplosion, %p); //Who the hell keeps putting these everywhere?
	%obj.applyImpulse(0, vectorScale(%obj.getMuzzleVector(%slot), -2000));
	return %p;
}