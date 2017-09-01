//////////
// item //
//////////
datablock ItemData(saw)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "tbm/data/shapes/bricks/chainsaw.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a saw.';
	invName = "Saw";
	image = sawImage;
	threatlevel = "Normal";
};

addWeapon(saw);

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(sawImage)
{
   // Basic Item properties
   shapeFile = "tbm/data/shapes/bricks/chainsaw.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
    
   offset = "-0.6 0.4 0";
   rotation = "0.476164 -0.300869 0.826284 186.128";
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
   item = saw;
   ammo = " ";
   projectile = swordProjectile;
   projectileType = Projectile;

   directDamage        = 35;
   radiusDamage        = 0;
   damageRadius        = 0.5;
   damagetype          = '%1 was hacked up by %2';
   muzzleVelocity      = 46;
   velInheritFactor    = 1;

   deathAnimationClass = "melee";
   deathAnimation = "death5";
   deathAnimationPercent = 0.4;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = false;

   //casing = " ";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 2.0;
	stateTransitionOnTimeout[0]       = "Ready";
	stateSound[0]					= sawStartSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "PreFire";
	stateTransitionOnTimeout[1]     = "Ready2";
	stateTimeoutValue[1]            = 1.6;
	stateSound[1]					= sawIdleSound;
	stateAllowImageChange[1]         = true;

	stateName[2]			= "PreFire";
	stateScript[2]                  = "onPreFire";
	stateAllowImageChange[2]        = false;
	stateSound[2]					= sawFireSound;
	stateSequence[2]			  = "chain";
	stateTimeoutValue[2]            = 0.1;
	stateTransitionOnTimeout[2]     = "Fire";

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "CheckFire";
	stateSound[3]					= sawFireIdleSound;
	stateTimeoutValue[3]            = 0.1;
	stateFire[3]                    = true;
	stateAllowImageChange[3]        = false;
	stateScript[3]                  = "onFire";
	stateWaitForTimeout[3]		= true;
	stateSequence[3]			  = "chain";

	stateName[4]			= "CheckFire";
	stateTransitionOnTriggerUp[4]	= "StopFire";
	stateTransitionOnTriggerDown[4]	= "Fire2";

	
	stateName[5]                    = "StopFire";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 0.1;
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;
	stateSequence[5]                = "root";
	stateScript[5]                  = "onStopFire";

	stateName[6]                     = "Ready2";
	stateTransitionOnTriggerDown[6]  = "PreFire";
	stateTransitionOnTimeout[6]     = "Ready";
	stateTimeoutValue[6]            = 1.6;
	stateSound[6]					= sawIdleSound;
	stateAllowImageChange[6]         = true;

	stateName[7]                    = "Fire2";
	stateTransitionOnTimeout[7]     = "CheckFire2";
	stateSound[7]			= sawFireIdleSound;
	stateTimeoutValue[7]            = 0.1;
	stateFire[7]                    = true;
	stateAllowImageChange[7]        = false;
	stateScript[7]                  = "onFire";
	stateWaitForTimeout[7]		= true;
	stateSequence[7]		= "chain";

	stateName[8]			= "CheckFire2";
	stateTransitionOnTriggerUp[8]	= "StopFire";
	stateTransitionOnTriggerDown[8]	= "Fire";
};

function sawImage::onMount(%this, %obj, %slot)
{
	%obj.playthread(1, armreadyboth);
}

function sawImage::onUnMount(%this, %obj, %slot)
{
	%obj.playthread(1, root);
}

function sawImage::onFire(%this, %obj, %slot)
{
   	%this.projectile.armingDelay         = 0;
   	%this.projectile.lifetime            = 65;
   	%this.projectile.fadeDelay           = 60;
   	%this.projectile.bounceElasticity    = 0;
   	%this.projectile.bounceFriction      = 0;
   	%this.projectile.isBallistic         = false;
   	%this.projectile.gravityMod = 0.0;
   	%this.projectile.hasLight    = false;

	%p = Parent::onFire(%this, %obj, %slot);
	return %p;
}