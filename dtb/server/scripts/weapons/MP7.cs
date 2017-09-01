//MP7.cs
//MP7 - BANBANGBANG

//////////
// item //
//////////

datablock ItemData(MP7)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "dtb/data/shapes/weapons/MP7.dts";
	skinName = 'base';
	rotate = true;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'an MP7 Machine Gun';
	invName = "MP7";
	image = MP7Image;
	threatlevel = "Normal";
};

addWeapon(MP7);

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(MP7Image)
{
   // Basic Item properties
   shapeFile = "dtb/data/shapes/weapons/MP7.dts";
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
   item = MP7;
   ammo = " ";
   projectile = hackProjectile;
   projectileType = Projectile;

   projectileSpread = 14/1000;
   projectileSpreadWalking = 21/1000;
   projectileSpreadMax = 28/1000;
   recoil = 1.03;       //Slightly higher and shorter than the AR
   recoilSeconds = 1.75;

   directDamage        = 7.5;
   radiusDamage        = 0;
   damageRadius        = 1;
   damagetype          = '%1 was hosed down by %2';
   muzzleVelocity      = 1600;
   velInheritFactor    = 1;

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
	stateTimeoutValue[3]            = 0.06;
	stateScript[3]                  = "onPreFire";
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTimeout[3]     = "Ready";




};

function MP7Image::onPreFire(%this, %obj, %slot)
{	
	//Reset so it can fire consistantly -DShiznit
	%obj.playthread(2, root);
}

function MP7Image::onFire(%this, %obj, %slot)
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
	muzzleflash(%this, %obj, %slot, 0.75, 1.35, 0.75);
	return %p;
}