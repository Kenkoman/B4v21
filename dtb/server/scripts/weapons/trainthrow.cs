//Throwing train
//look both ways...

datablock ExplosionData(trainExplosion)
{
   //explosionShape = "";
	soundProfile = GrenadeExplosionSound;

   lifeTimeMS = 150;

   particleEmitter = SpearExplosionEmitter;
   particleDensity = 30;
   particleRadius = 8;

   emitter[0] = SpearExplosionEmitter;
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
   lightStartColor = "0.3 0.6 0.7";
   lightEndColor = "0 0 0";
};

//projectile
datablock ProjectileData(trainProjectile)
{
   projectileShapeName = "tbm/data/shapes/bricks/vehicles/tbmtrain.dts";
   explosion           = trainExplosion;
//   particleEmitter     = bullet2TrailEmitter;
   muzzleVelocity      = 100;

   armingDelay         = 0;
   lifetime            = 8000;
   fadeDelay           = 7500;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 0;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";
};


//////////
// item //
//////////
datablock ItemData(train)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "tbm/data/shapes/bricks/vehicles/tbmtrain.dts";
	skinName = 'base';
	rotate = true;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a f***ing train';
	invName = "Train Throw";
	image = TrainthrowImage;
	threatlevel = "Dangerous";
};

addWeapon(train);

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(TrainthrowImage)
{
   // Basic Item properties
   shapeFile = "tbm/data/shapes/bricks/vehicles/tbmtrain.dts";
   skinName = 'base';
   emap = true;
    
   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "1.7 0 2";
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
   item = train;
   ammo = " ";
   projectile = trainProjectile;
   projectileType = Projectile;

   directDamage        = 0;
   radiusDamage        = 200;
   damageRadius        = 8;
   damagetype          = '%2 threw a !@#$ing train at %1!';
   muzzleVelocity      = 100;
   velInheritFactor    = 1;

   deathAnimationClass = "explosion";
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
	stateTimeoutValue[0]             = 0.5;
	stateTransitionOnTimeout[0]       = "Ready";
	stateSound[0]					= weaponSwitchSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "PreFire";
	stateAllowImageChange[1]         = true;

	stateName[2]                     = "PreFire";
	stateTransitionOnTimeout[2]     = "Fire";
	stateTimeoutValue[2]            = 0.02;
	stateScript[2]                  = "onPreFire";
	stateWaitForTimeout[2]			= true;
	
	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "Reload";
	stateTimeoutValue[3]            = 0.001;
	stateFire[3]                    = true;
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	stateScript[3]                  = "onFire";
	stateWaitForTimeout[3]			= true;
	stateSound[3]					= spearFireSound;

	stateName[4]			= "Reload";
	stateSequence[4]                = "Reload";
	stateAllowImageChange[4]        = false;
	stateTimeoutValue[4]            = 0.001;
	stateWaitForTimeout[4]		= true;
	stateTransitionOnTimeout[4]     = "Check";

	stateName[5]			= "Check";
	stateTransitionOnTriggerUp[5]	= "StopFire";
	stateTransitionOnTriggerDown[5]	= "StopFire";

	stateName[6]                    = "StopFire";
	stateTransitionOnTimeout[6]     = "Ready";
	stateTimeoutValue[6]            = 0.02;
	stateAllowImageChange[6]        = false;
	stateWaitForTimeout[6]		= true;
	//stateSequence[6]                = "Reload";
	stateScript[6]                  = "onStopFire";


};

function trainthrowImage::onPreFire(%this, %obj, %slot)
{	
	%obj.playthread(2, jump);
}

function trainthrowImage::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}

function trainProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	tbmcollison(%this, %obj, %col, %fade, %pos, %normal);
       tbmradiusDamage
     (%obj, VectorAdd(%pos, VectorScale(%normal, 0.01)),
      %obj.damageRadius,%obj.radiusDamage,%obj.damageType,20);
}

function trainthrowImage::onMount(%this, %obj)
{
	%obj.playthread(1, armreadyright);
	messageAll( 'MsgClientKilled','%1 \c2pulled out a \c9Train\c2 to throw at someone. Holy Sh**!',%obj.client.name);
}
