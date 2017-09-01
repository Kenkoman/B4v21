//bullet trail
datablock ParticleData(freezeGunTrailParticle)
{
	dragCoefficient		= 3.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
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
	colors[0]	= "0.9 0.9 1 0.75";
	colors[1]	= "0.5 0.5 1 0.0";
	sizes[0]	= 1;
	sizes[1]	= 2;
	times[0]	= 0.0;
	times[1]	= 1.0;
};

datablock ParticleEmitterData(freezeGunTrailEmitter)
{
   ejectionPeriodMS = 2;
   periodVarianceMS = 0;

   ejectionVelocity = 5; //0.25;
   velocityVariance = 0; //0.10;

   ejectionOffset = 0;

   thetaMin         = 0.0;
   thetaMax         = 90.0;  

   particles = "freezeGunTrailParticle";
};

//effects
datablock ParticleData(freezeGunExplosionParticle)
{
	dragCoefficient      = 1;
	gravityCoefficient   = 1;
	inheritedVelFactor   = 0.2;
	constantAcceleration = 0.0;
	lifetimeMS           = 2000;
	lifetimeVarianceMS   = 300;
	textureName          = "~/data/particles/cloud";
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	colors[0]	= "0.9 0.9 1 0.5";
	colors[1]	= "0.5 0.5 1 0.0";
	sizes[0]	= 1;
	sizes[1]	= 2;
	times[0]	= 0.0;
	times[1]	= 1.0;
};

datablock ParticleEmitterData(freezeGunExplosionEmitter)
{
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;
   ejectionVelocity = 7.5;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "freezeGunExplosionParticle";
};

datablock ExplosionData(freezeGunExplosion)
{
   //explosionShape = "";
	soundProfile = freezeGunExplosionSound;

   lifeTimeMS = 150;

   particleEmitter = freezeGunExplosionEmitter;
   particleDensity = 500;
   particleRadius = 0.2;

   emitter[0] = freezeGunExplosionEmitter;
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
   lightStartColor = "0.9 0.9 1";
   lightEndColor = "0.5 0.5 1";
};


//projectile
datablock ProjectileData(freezeGunProjectile)
{
//   projectileShapeName = "~/data/shapes/redblast.dts";
   explosion           = freezeGunExplosion;
   particleEmitter     = freezeGunTrailEmitter;
   freezeTime          = 1000;
   muzzleVelocity      = 60;

   armingDelay         = 0;
   lifetime            = 80000;
   fadeDelay           = 75000;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 0;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0.7 0 0";
};


//////////
// item //
//////////
datablock ItemData(freezeGun)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/weapons/Cryogun.dts";
	skinName = 'black';
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a Cryo Gun.';
        spawnname = "Freeze Gun";
	invName = "Cryo Gun";
	image = freezeGunImage;
	threatlevel = "Normal";
};

addWeapon(freezeGun);

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(freezeGunImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/weapons/Cryogun.dts";
   skinName = 'brown';
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
   item = freezeGun;
   ammo = " ";
   projectile = freezeGunProjectile;
   projectileType = Projectile;

   directDamage        = 5;
   radiusDamage        = 5;
   damageRadius        = 0.5;
   damagetype          = '%1 was put on ice by %2';
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
	stateTimeoutValue[2]            = 0.5;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]			= true;
	stateSound[2]					= gunpackFireSound;

	stateName[3]			= "Reload";
	stateSequence[3]                = "Reload";
	stateAllowImageChange[3]        = false;
	stateTimeoutValue[3]            = 1.5;
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

//This is how I did Freezing before I saw GreyMario's method in action.
//
//function freezeGunDoFreeze(%obj) {
//		%obj.setVelocity("0 0 -100000");
//}
//
//function ShizFreeze(%delay,%times,%obj) {
//%a=0;
//for (%i=0; %i<%times; %i++) {
//  schedule(%delay*%a++,0,freezeGunDoFreeze(%obj));
//  }
//}

function freezeGunProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	if(%col.getType() == 67108869)
		return;
	if (%col.isOnIce != 1)
	{
		if (%col.getClassName() $= "Player" || %col.getClassName() $= "AIPlayer")
			IceThatNigga(%col,10000); //new command I'm writing to freeze people.
	}
}
