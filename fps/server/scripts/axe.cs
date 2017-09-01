//axe.cs

//axe weapon

//effects
datablock ParticleData(axeExplosionParticle)
{
   dragCoefficient      = 5;
   gravityCoefficient   = 0.1;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 500;
   lifetimeVarianceMS   = 300;
   textureName          = "~/data/particles/chunk";
   colors[0]     = "0.9 0.9 0.7 0.9";
   colors[1]     = "0.9 0.9 0.9 0.0";
   sizes[0]      = 0.2;
   sizes[1]      = 0.1;
};

datablock ParticleEmitterData(axeExplosionEmitter)
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   ejectionVelocity = 9;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 60;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "axeExplosionParticle";
};

datablock ExplosionData(axeExplosion)
{
   //explosionShape = "";
   lifeTimeMS = 500;

   particleEmitter = axeExplosionEmitter;
   particleDensity = 10;
   particleRadius = 0.2;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = true;
   camShakeFreq = "20.0 22.0 20.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 0.5;
   camShakeRadius = 10.0;

   // Dynamic light
   lightStartRadius = 3;
   lightEndRadius = 1;
   lightStartColor = "00.6 0.0 0.0";
   lightEndColor = "0 0 0";
};


//projectile
datablock ProjectileData(axeProjectile)
{
   //projectileShapeName = "~/data/shapes/spearProjectile.dts";
   directDamage        = 10;
   radiusDamage        = 10;
   damageRadius        = 0.5;
   explosion           = axeExplosion;
   //particleEmitter     = as;

   muzzleVelocity      = 50;
   velInheritFactor    = 1;

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
datablock ItemData(axe)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/axe.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'an axe';
	invName = 'Axe';
	image = axeImage;
};

//function axe::onUse(%this,%user)
//{
//	//mount the image in the right hand slot
//	%user.mountimage(%this.image, $RightHandSlot);
//}

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(axeImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/axe.dts";
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
   correctMuzzleVector = false;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = axe;
   ammo = " ";
   projectile = axeProjectile;
   projectileType = Projectile;

   //melee particles shoot from eye node for consistancy
   melee = true;
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
	stateTransitionOnTriggerDown[1]  = "PreFire";
	stateAllowImageChange[1]         = true;

	stateName[2]			= "PreFire";
	stateScript[2]                  = "onPreFire";
	stateAllowImageChange[2]        = false;
	stateTimeoutValue[2]            = 0.1;
	stateTransitionOnTimeout[2]     = "Fire";

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "Fire";
	stateTimeoutValue[3]            = 0.2;
	stateFire[3]                    = true;
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	stateScript[3]                  = "onFire";
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTriggerUp[3]	= "StopFire";


	stateName[4]                    = "StopFire";
	stateTransitionOnTimeout[4]     = "Ready";
	stateTimeoutValue[4]            = 0.2;
	stateAllowImageChange[4]        = false;
	stateWaitForTimeout[4]		= true;
	stateSequence[4]                = "StopFire";
	stateScript[4]                  = "onStopFire";


};

function axeImage::onPreFire(%this, %obj, %slot)
{
	//messageAll( 'MsgClient', 'axe prefired!!!');
	//Parent::onFire(%this, %obj, %slot);
	%obj.playthread(2, armattack);
}

function axeImage::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
	//messageAll( 'MsgClient', 'stopfire');
}