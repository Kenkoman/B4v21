//UZI.cs
//UZI - BANBANGBANG

//////////
// item //
//////////
datablock ItemData(UZI)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "dtb/data/shapes/weapons/uzi.dts";
	skinName = 'base';
	rotate = true;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a pair of Micro Uzi Machine Guns';
	invName = "Micro-UZIs";
	image = UZIImage;
	threatlevel = "Normal";
};

addWeapon(UZI);

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(UZIImage)
{
   // Basic Item properties
   shapeFile = "dtb/data/shapes/weapons/uzi.dts";
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
   item = UZI;
   ammo = "";
   projectile = hackProjectile;
   projectileType = Projectile;

   projectileSpread = 14/1000;
   projectileSpreadWalking = 21/1000;
   projectileSpreadMax = 28/1000;
   recoil = 1.05;
   recoilSeconds = 2;

   directDamage        = 7.5;
   radiusDamage        = 0;
   damageRadius        = 1;
   damagetype          = '%1 got sprayed by %2';
   muzzleVelocity      = 1600;
   velInheritFactor    = 1;

   deathAnimationClass = "projectile";
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
	stateTimeoutValue[0]             = 0.5;
	stateScript[0]                  = "onArm";
	stateTransitionOnTimeout[0]       = "Ready";
	stateSound[0]					= weaponSwitchSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateScript[1]                  = "onPreFire";
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
	stateTimeoutValue[3]            = 0.03;
	stateScript[3]                  = "onPreFire";
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTimeout[3]     = "Fire2";

	stateName[4]                    = "Fire2";
	stateTransitionOnTimeout[4]     = "Reload2";
	stateTimeoutValue[4]            = 0.001;
	stateFire[4]                    = true;
	stateAllowImageChange[4]        = false;
	stateSequence[4]                = "Fire";
	stateScript[4]                  = "onFire2";
	stateWaitForTimeout[4]			= true;
	stateSound[4]					= BrifleFireSound;

	stateName[5]			= "Reload2";
	stateSequence[5]                = "Reload";
	stateAllowImageChange[5]        = false;
	stateTimeoutValue[5]            = 0.03;
	stateScript[5]                  = "onPreFire";
	stateWaitForTimeout[5]		= true;
	stateTransitionOnTimeout[5]     = "Ready";

};

datablock ShapeBaseImageData(UZIImage2)
{
   // Basic Item properties
   shapeFile = "dtb/data/shapes/weapons/uzi.dts";
   skinName = 'base';
   emap = true;
    
   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 1;
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
   item = "";
   ammo = "";
   projectile = hackProjectile;
   projectileType = Projectile;
};

function UZIImage::onArm(%this, %obj, %slot)
{	
	//Reset so it can fire consistantly -DShiznit
	%obj.playThread(1, armreadyboth);
	%obj.lastleft = %obj.getMountedImage(1);
	%obj.mountImage(UZIImage2, 1);
}

function UZIImage::onUnmount(%this, %obj, %slot)
{	
	//Reset so it can fire consistantly -DShiznit
	%obj.playThread(1, root);
	%obj.unMountImage(1);
	%obj.mountImage(%obj.lastleft, 1);
}

function UZIImage::onPreFire(%this, %obj, %slot)
{	
	//Reset so it can fire consistantly -DShiznit
	%obj.playthread(2, root);
}

function UZIImage::onFire(%this, %obj, %slot)
{
	//Add recoil -DShiznit
	%obj.playthread(2, jump);
   	%this.projectile.scale               = ".5 .5 .5";
   	%this.projectile.armingDelay         = 0;
   	%this.projectile.lifetime            = 8000;
   	%this.projectile.fadeDelay           = 7500;
   	%this.projectile.bounceElasticity    = 0;
   	%this.projectile.bounceFriction      = 0;
   	%this.projectile.isBallistic         = true;
   	%this.projectile.gravityMod = 0;

	%p = Parent::onFire(%this, %obj, %slot);
	muzzleflash(%this, %obj, %slot, 0.75, 0.75, 0.75);
	return %p;
}

function UZIImage::onFire2(%this, %obj, %slot)
{
	%this.onFire(%obj, 1);
}