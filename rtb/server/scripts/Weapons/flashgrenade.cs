//flashGrenade.cs

//flashGrenade trail
datablock ParticleData(flashGrenadeTrailParticle)
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

datablock ParticleEmitterData(flashGrenadeTrailEmitter)
{
   ejectionPeriodMS = 5;
   periodVarianceMS = 0;

   ejectionVelocity = 0; //0.25;
   velocityVariance = 0; //0.10;

   ejectionOffset = 0;

   thetaMin         = 0.0;
   thetaMax         = 90.0;  

   particles = flashGrenadeTrailParticle;
};


//effects
datablock ParticleData(flashGrenadeExplosionParticle)
{
	dragCoefficient		= 3.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= 0.5;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 900;
	lifetimeVarianceMS	= 300;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= true;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "~/data/particles/cloud";
	//animTexName		= "~/data/particles/cloud";

	// Interpolation variables
	colors[0]	= "0.3 0.3 0.2 0.9";
	colors[1]	= "0.2 0.2 0.2 0.0";
	sizes[0]	= 4.0;
	sizes[1]	= 7.0;
	times[0]	= 0.0;
	times[1]	= 1.0;
};

datablock ParticleEmitterData(flashGrenadeExplosionEmitter)
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   lifeTimeMS	   = 21;
   ejectionVelocity = 8;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "flashGrenadeExplosionParticle";
};

datablock ParticleData(flashGrenadeExplosionParticle2)
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

datablock ParticleEmitterData(flashGrenadeExplosionEmitter2)
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
   particles = "flashGrenadeExplosionParticle2";
};

datablock ExplosionData(flashGrenadeExplosion)
{
   //explosionShape = "";
   lifeTimeMS = 700;

   soundProfile = spearExplosionSound;

   emitter[0] = flashGrenadeExplosionEmitter;
   emitter[1] = flashGrenadeExplosionEmitter2;
   particleDensity = 30;
   particleRadius = 1.0;

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
   lightEndColor = "1 0 0 1";
};


//projectile
datablock ProjectileData(flashGrenadeProjectile)
{
   projectileShapeName = "~/data/shapes/bricks/brickWeapon.dts";
   directDamage        = 0;
   radiusDamage        = 0;
   damageRadius        = 5;
   explosion           = flashGrenadeExplosion;
   particleEmitter     = flashGrenadeTrailEmitter;

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
datablock ItemData(flashGrenade)
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
	pickUpName = 'a flashGrenade';
	invName = 'flashGrenade';
	image = flashGrenadeImage;
};

//function flashGrenade::onUse(%this,%user)
//{
//	//mount the image in the right hand slot
//	%user.mountimage(%this.image, $RightHandSlot);
//}

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(flashGrenadeImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/bricks/brickWeapon.dts";
   PreviewFileName = "rtb/data/shapes/bricks/Previews/flashGrenade.png";
   emap = true;
   skinname="green";
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
   item = flashGrenade;
   ammo = " ";
   projectile = flashGrenadeProjectile;
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
	stateSound[5]		        = spearFireSound;
};

function flashGrenadeImage::onCharge(%this, %obj, %slot)
{
	%obj.playthread(2, spearReady);
}

function flashGrenadeImage::onAbortCharge(%this, %obj, %slot)
{
	%obj.playthread(2, root);
}

function flashGrenadeImage::onFire(%this, %obj, %slot)
{
	%obj.playthread(2, spearThrow);
	Parent::onFire(%this, %obj, %slot);
}
function flashGrenadeProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	echo(%this);
	echo(%obj);
	echo(%col);
	echo(%pos);
	%position = %pos;
	%radius = 20;
	InitContainerRadiusSearch(%position, %radius, $TypeMasks::ShapeBaseObjectType);

	%halfRadius = %radius / 2;

	while ((%targetObject = containerSearchNext()) != 0) {
		%coverage = calcExplosionCoverage(%position, %targetObject,
         	$TypeMasks::InteriorObjectType |  $TypeMasks::TerrainObjectType |
         	$TypeMasks::ForceFieldObjectType | $TypeMasks::VehicleObjectType);
      		if (%coverage == 0)
        		continue;

		%dist = containerSearchCurrRadiusDist();
	  	%distScale = (%dist < %halfRadius)? 1.0:
	      	   1.0 - ((%dist - %halfRadius) / %halfRadius);

		if(%targetObject.getClassname() $= "Player" && %targetObject.client.team !$= "Cops" && %targetObject.client.isImprisoned !$= "1")
		{
			$Times = 0;
			whiteout(%targetObject);
		}
	}
}
function whiteout(%targetObject)
{
	%targetObject.setWhiteout(100000000000000);
	if($Times <= 2)
	{
	$Times++;
	Schedule(2000,0,"whiteOut",%targetObject);
	}
}