//M23.cs
//SOCOM Pistol, - BANBANGBANG
//////////
// item //
//////////
datablock ItemData(m23)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "dtb/data/shapes/weapons/m23.dts";
	skinName = 'base';
	rotate = true;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a surpressed M23 SOCOM pistol';
	invName = "M23 SOCOM";
	image = m23Image;
	threatlevel = "Normal";
};

addWeapon(m23);

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(m23Image)
{
   // Basic Item properties
   shapeFile = "dtb/data/shapes/weapons/m23.dts";
   skinName = 'base';
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
   item = nine;
   ammo = " ";
   projectile = hackProjectile;
   projectileType = Projectile;

   projectileSpread = 2/1000;
   projectileSpreadWalking = 3/1000;
   projectileSpreadMax = 4/1000;
   recoil = 1.3;
   recoilSeconds = 1;

   directDamage        = 30;
   radiusDamage        = 0;
   damageRadius        = 1;
   damagetype          = '%1 was ghosted by %2';
   muzzleVelocity      = 1600;
   velInheritFactor    = 1;
   impulse	       = 20;

   deathAnimationClass = "projectile";
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
	stateScript[1]                  = "onStop";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateAllowImageChange[1]         = true;

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "Reload";
	stateTimeoutValue[2]            = 0.001;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]			= true;
	stateSound[2]					= bulletExplosionSound;

	stateName[3]			= "Reload";
	stateSequence[3]                = "Reload";
	stateAllowImageChange[3]        = false;
	stateTimeoutValue[3]            = 0.2;
	stateScript[3]                  = "onPre";
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTimeout[3]     = "Check";

	stateName[4]			= "Check";
	stateScript[4]                  = "onStop";
	stateTransitionOnTriggerUp[4]	= "Ready";
	stateAllowImageChange[4]        = true;

};

function m23Image::onPre(%this, %obj, %slot)
{	
	%obj.playthread(2, jump);
}

function m23Image::onStop(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}

function m23Image::onFire(%this, %obj, %slot)
{
	%this.projectile.scale               = ".5 .5 .5";
	%this.projectile.armingDelay         = 0;
	%this.projectile.lifetime            = 8000;
	%this.projectile.fadeDelay           = 7500;
	%this.projectile.bounceElasticity    = 0;
	%this.projectile.bounceFriction      = 0;
	%this.projectile.isBallistic         = true;
	%this.projectile.gravityMod = 0;

	%p = Parent::onFire(%this, %obj, %slot);

	muzzleflash(%this, %obj, %slot, 0.75, 1.5, 0.75);
	return %p;
}