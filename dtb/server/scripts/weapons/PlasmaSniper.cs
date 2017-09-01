//projectile
datablock ProjectileData(PlasmaBulletProjectile)
{
   projectileShapeName = "~/data/shapes/weapons/plasmashot.dts";
   explosion           = ConRifleBulletExplosion;
   particleEmitter     = ConRifleBulletFireParticleEmitter;
   muzzleVelocity      = 750;

   armingDelay         = 0;
   lifetime            = 8000;
   fadeDelay           = 7500;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 0;

   hasLight    = true;
   lightRadius = 5.0;
   lightColor  = "0.7 0 0.8";
};


//////////
// item //
//////////
datablock ItemData(PlasmaSniper)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/weapons/legosniper.dts";
	skinName = 'grayDark';
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a PlasmaSniper.';
	invName = "PlasmaSniper";
	spawnName = "Plasma Sniper";
	image = PlasmaSniperImage;
	threatlevel = "Normal";
};

addWeapon(PlasmaSniper);

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(PlasmaSniperImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/weapons/legosniper.dts";
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
   item = PlasmaSniper;
   ammo = " ";
   projectile = PlasmaBulletProjectile;
   projectileType = Projectile;

   directDamage        = 50;
   radiusDamage        = 50;
   damageRadius        = 0.5;
   damagetype          = '%1 was plasma sniped by %2';
   muzzleVelocity      = 750;
   velInheritFactor    = 1;

   deathAnimationClass = "plasma";
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
	stateTimeoutValue[0]             = 5.0;
	stateTransitionOnTimeout[0]       = "Ready";
	stateSound[0]					= PlasmaChargeSound;

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
	stateSound[2]					= PlasmaSniperFireSound;

	stateName[3]			= "Reload";
	stateSequence[3]                = "Reload";
	stateAllowImageChange[3]        = true;
	stateTimeoutValue[3]            = 5.0;
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTimeout[3]     = "Check";
	stateSound[3]					= PlasmaChargeSound;
	stateScript[3]                  = "onPreFire";

	stateName[4]			= "Check";
	stateTransitionOnTriggerUp[4]	= "StopFire";
	stateTransitionOnTriggerDown[4]	= "StopFire";

	stateName[5]                    = "StopFire";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 0.001;
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;
	//stateSequence[5]                = "Reload";
	stateScript[5]                  = "onStopFire";


};

function PlasmaSniperImage::onPreFire(%this, %obj, %slot)
{	
	%obj.playthread(2, jump);
}

function PlasmaSniperImage::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}

function PlasmaBulletProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	tbmcollison(%this,%obj,%col,%fade,%pos,%normal);
       tbmradiusDamage
     (%obj, VectorAdd(%pos, VectorScale(%normal, 0.01)),
      %obj.damageRadius,%obj.radiusDamage,%obj.damageType,20);
}