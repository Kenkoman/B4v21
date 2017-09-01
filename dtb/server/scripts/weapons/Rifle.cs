//Rifle.cs
//Rifle Stuff - BANBANGBANG

//////////
// item //
//////////
datablock ItemData(Rifle)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/weapons/Rifle.dts";
	skinName = 'brown';
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = false;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a Rifle.';
	invName = "Rifle";
	image = RifleImage;
	threatlevel = "Normal";
};

addWeapon(Rifle);

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(RifleImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/weapons/Rifle.dts";
   skinnable = 1; //This indicates that if the weapon pickup is skinned the image will be skinned too.
   emap = false;
    
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
   item = Rifle;
   ammo = " ";
   projectile = hackProjectile;
   projectileType = Projectile;

   projectileSpread = 0;
   projectileSpreadWalking = 8/1000;
   projectileSpreadMax = 12/1000;
   recoil = 1.2;
   recoilSeconds = 2;

   directDamage        = 35;
   radiusDamage        = 0;
   damageRadius        = 0.5;
   damagetype          = '%1 was hunted by %2';
   muzzleVelocity      = 2600;
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
	stateSound[2]					= BrifleFireSound;

	stateName[3]			= "Reload";
	stateSequence[3]                = "Reload";
	stateAllowImageChange[3]        = false;
	stateTimeoutValue[3]            = 0.5;
	stateScript[3]                  = "onPre";
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTimeout[3]     = "Check";

	stateName[4]			= "Check";
	stateScript[4]                  = "onStop";
	stateTransitionOnTriggerUp[4]	= "Ready";
	stateAllowImageChange[4]        = true;

};

function RifleImage::onPre(%this, %obj, %slot)
{	
	%obj.playthread(2, jump);
}

function RifleImage::onStop(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}

function RifleImage::onFire(%this, %obj, %slot)
{
   	%this.projectile.scale               = ".5 1.5 .5";
   	%this.projectile.armingDelay         = 0;
   	%this.projectile.lifetime            = 8000;
   	%this.projectile.fadeDelay           = 7500;
   	%this.projectile.bounceElasticity    = 0;
   	%this.projectile.bounceFriction      = 0;
   	%this.projectile.isBallistic         = true;
   	%this.projectile.gravityMod = 0;

	%p = Parent::onFire(%this, %obj, %slot);

	muzzleflash(%this, %obj, %slot, 1, 2.2, 1);
	return %p;
}