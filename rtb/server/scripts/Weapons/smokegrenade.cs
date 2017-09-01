//smokegrenade.cs

//smokegrenade trail
datablock ParticleData(smokegrenadeTrailParticle)
{
	dragCoefficient		= 3.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 600;
	lifetimeVarianceMS	= 0;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= true;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "~/data/particles/ring";
	//animTexName		= " ";

	// Interpolation variables
	colors[0]	= "0.75 0.75 0.75 0.3";
	colors[1]	= "0.75 0.75 0.75 0.2";
	colors[2]	= "1 1 1 0.0";
	sizes[0]	= 0.15;
	sizes[1]	= 0.35;
	sizes[2]	= 0.05;
	times[0]	= 0.0;
	times[1]	= 0.1;
	times[2]	= 1.0;
};

datablock ParticleEmitterData(smokegrenadeTrailEmitter)
{
   ejectionPeriodMS = 5;
   periodVarianceMS = 0;

   ejectionVelocity = 0; //0.25;
   velocityVariance = 0; //0.10;

   ejectionOffset = 0;

   thetaMin         = 0.0;
   thetaMax         = 90.0;  

   particles = smokegrenadeTrailParticle;
};


//effects
datablock ParticleData(smokegrenadeExplosionParticle)
{
	dragCoefficient		= 3.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 25000;
	lifetimeVarianceMS	= 1000;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= false;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "~/data/particles/cloud";
	//animTexName		= "~/data/particles/cloud";

	// Interpolation variables
	colors[0]	= "1 1 1 1";
	colors[1]	= "1 1 1 1";
	colors[2]	= "1 1 1 1";
	sizes[0]	= 6.0;
	sizes[1]	= 6.0;
	sizes[2]	= 0.0;
	times[0]	= 0.0;
	times[1]	= 0.9;
	times[2]	= 1.0;
};

datablock ParticleEmitterData(smokegrenadeExplosionEmitter)
{
   ejectionPeriodMS = 100;
   periodVarianceMS = 0;
   lifeTimeMS	    = 1000;
   ejectionVelocity = 12;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "smokegrenadeExplosionParticle";
};

datablock ParticleData(smokegrenadeExplosionParticle2)
{
	dragCoefficient		= 0.1;
	windCoefficient		= 0.0;
	gravityCoefficient	= 2.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 1000;
	lifetimeVarianceMS	= 500;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= true;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "~/data/particles/chunk";
	//animTexName		= "~/data/particles/cloud";

	// Interpolation variables
	colors[0]	= "0.0 0.0 0.0 1.0";
	colors[1]	= "0.0 0.0 0.0 0.0";
	sizes[0]	= 0.5;
	sizes[1]	= 0.5;
	times[0]	= 0.0;
	times[1]	= 1.0;
};

datablock ParticleEmitterData(smokegrenadeExplosionEmitter2)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   lifetimeMS       = 7;
   ejectionVelocity = 15;
   velocityVariance = 5.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "smokegrenadeExplosionParticle2";
};

datablock ExplosionData(smokegrenadeExplosion)
{
   //explosionShape = "";
   lifeTimeMS = 5000;

   soundProfile = spearExplosionSound;

   emitter[0] = smokegrenadeExplosionEmitter;
   emitter[1] = smokegrenadeExplosionEmitter2;
   particleDensity = 20;
   particleRadius = 5.0;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = true;
   camShakeFreq = "7.0 8.0 7.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 0.5;
   camShakeRadius = 15.0;

   // Dynamic light
   lightStartRadius = 20;
   lightEndRadius = 0;
   lightStartColor = "1 1 1 1";
   lightEndColor = "0 0 0 1";
};


//projectile
datablock ProjectileData(smokegrenadeProjectile)
{
   projectileShapeName = "~/data/shapes/bricks/brickWeapon.dts";
   directDamage        = 0;
   radiusDamage        = 0;
   damageRadius        = 5;
   explosion           = smokegrenadeExplosion;
   particleEmitter     = smokegrenadeTrailEmitter;

   muzzleVelocity      = 25;
   velInheritFactor    = 1;

   armingDelay         = 0;
   lifetime            = 20000;
   fadeDelay           = 19500;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 1;

   hasLight    = false;
   lightRadius = 20.0;
   lightColor  = "1 1 1 1";
};


//////////
// item //
//////////
datablock ItemData(smokegrenade)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
        shapeFile = "~/data/shapes/bricks/brickWeapon.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	cost = "250";
	skinname = "green";
	 // Dynamic properties defined by the scripts
	pickUpName = 'a smokegrenade';
	invName = 'smokegrenade';
	image = smokegrenadeImage;
};

//function smokegrenade::onUse(%this,%user)
//{
//	//mount the image in the right hand slot
//	%user.mountimage(%this.image, $RightHandSlot);
//}

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(smokegrenadeImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/bricks/brickWeapon.dts";
   PreviewFileName = "rtb/data/shapes/bricks/Previews/smokegrenade.png";
   emap = false;
   skinname = "green";
	cloakable = false;
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
   item = smokegrenade;
   ammo = " ";
   projectile = smokegrenadeProjectile;
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
	stateName[0]			= "Activate";
	stateTimeoutValue[0]		= 0.5;
	stateTransitionOnTimeout[0]	= "Ready";
	stateSequence[0]		= "ready";
	stateSound[0]			= weaponSwitchSound;

	stateName[1]			= "Ready";
	stateTransitionOnTriggerDown[1]	= "Charge";
	stateAllowImageChange[1]	= true;
	
	stateName[2]                    = "Charge";
	stateTransitionOnTimeout[2]	= "Armed";
	stateTimeoutValue[2]            = 0.7;
	stateWaitForTimeout[2]		= false;
	stateTransitionOnTriggerUp[2]	= "AbortCharge";
	stateScript[2]                  = "onCharge";
	stateAllowImageChange[2]        = false;
	
	stateName[3]			= "AbortCharge";
	stateTransitionOnTimeout[3]	= "Ready";
	stateTimeoutValue[3]		= 0.3;
	stateWaitForTimeout[3]		= true;
	stateScript[3]			= "onAbortCharge";
	stateAllowImageChange[3]	= false;

	stateName[4]			= "Armed";
	stateTransitionOnTriggerUp[4]	= "Fire";
	stateAllowImageChange[4]	= false;

	stateName[5]			= "Fire";
	stateTransitionOnTimeout[5]	= "Ready";
	stateTimeoutValue[5]		= 30;
	stateFire[5]			= true;
	stateSequence[5]		= "fire";
	stateScript[5]			= "onFire";
	stateWaitForTimeout[5]		= true;
	stateAllowImageChange[5]	= true;
	stateSound[0]			= spearFireSound;
};

function smokegrenadeImage::onCharge(%this, %obj, %slot)
{
	%obj.playthread(2, spearReady);
}

function smokegrenadeImage::onAbortCharge(%this, %obj, %slot)
{
	%obj.playthread(2, root);
}

function smokegrenadeImage::onFire(%this, %obj, %slot)
{
	%obj.playthread(2, spearThrow);
	Parent::onFire(%this, %obj, %slot);
}
function smokegrenadeProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
}