//--------------------------------------------------------------------------
// Weapon Item.  This is the item that exists in the world, i.e. when it's
// been dropped, thrown or is acting as re-spawnable item.  When the weapon
// is mounted onto a shape, the RifleImage is used.

datablock ItemData(Shotgun)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/weapons/yeahboy/shotgun.dts";
	skinName = 'brown';
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a Shotgun.';
	invName = "Shotgun";
	image = ShotgunImage;
	threatlevel = "Normal";
};

addWeapon(Shotgun);

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(ShotgunImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/weapons/yeahboy/shotgun.dts";
   skinnable = 1; //This indicates that if the weapon pickup is skinned the image will be skinned too.
   emap = true;
    
   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
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
   item = shotgun;
   ammo = "";
   projectile = hackProjectile;
   projectileType = Projectile;

   projectileSpread = 32/1000;
   projectileSpreadWalking = 36/1000;
   projectileSpreadMax = 40/1000;
   recoil = 1.2;
   recoilSeconds = 1;

   directDamage        = 15;
   radiusDamage        = 0;
   damageRadius        = 2;
   damagetype          = '%1 saw %2\'s smiley face';
   shellCount          = 7;
   muzzleVelocity      = 1600;
   velInheritFactor    = 1;
   impulse             = 10;

   deathAnimationClass = "projectile";
   deathAnimation = "death5";
   deathAnimationPercent = 0.4;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = true;   

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
	stateSound[2]					= ShotgunFireSound;

	stateName[3]			= "Reload";
	stateSequence[3]                = "Reload";
	stateAllowImageChange[3]        = false;
	stateTimeoutValue[3]            = 0.8;
	stateScript[3]                  = "onPre";
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTimeout[3]     = "Check";

	stateName[4]			= "Check";
	stateScript[4]                  = "onStop";
	stateTransitionOnTriggerUp[4]	= "Ready2";
	stateAllowImageChange[4]        = true;

	stateName[5]                     = "Ready2";
	stateScript[5]                  = "onStop";
	stateTransitionOnTriggerDown[5]  = "Fire2";
	stateAllowImageChange[5]         = true;

	stateName[6]                    = "Fire2";
	stateTransitionOnTimeout[6]     = "Reload2";
	stateTimeoutValue[6]            = 0.001;
	stateFire[6]                    = true;
	stateAllowImageChange[6]        = false;
	stateSequence[6]                = "Fire2";
	stateScript[6]                  = "onFire";
	stateWaitForTimeout[6]		= true;
	stateSound[6]			= ShotgunFireSound;

	stateName[7]			= "Reload2";
	stateSequence[7]                = "Reload2";
	stateAllowImageChange[7]        = false;
	stateTimeoutValue[7]            = 1.5;
	stateScript[7]                  = "onPre";
	stateWaitForTimeout[7]		= true;
	stateTransitionOnTimeout[7]     = "Check2";

	stateName[8]			= "Check2";
	stateScript[8]                  = "onStop";
	stateTransitionOnTriggerUp[8]	= "Ready";
	stateAllowImageChange[8]        = true;

};


//-----------------------------------------------------------------------------


function shotgunImage::onFire(%this, %obj, %slot)
{
   	%this.projectile.scale               = ".25 .25 .25";
   	%this.projectile.armingDelay         = 0;
   	%this.projectile.lifetime            = 2000;
   	%this.projectile.fadeDelay           = 1500;
   	%this.projectile.bounceElasticity    = 0;
   	%this.projectile.bounceFriction      = 0;
   	%this.projectile.isBallistic         = true;
   	%this.projectile.gravityMod = 0.10;


	%p = Parent::onFire(%this, %obj, %slot);
	muzzleflash(%this, %obj, %slot, 1, 1, 1);
	return %p;
}

function ShotgunImage::onPre(%this, %obj, %slot)
{	
	%obj.playthread(2, jump);
}

function ShotgunImage::onStop(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}

package Weapon_Shotgun {
function onAddProjectile(%projectile, %p, %image) {
if(%image == nameToID(ShotgunImage) && %projectile $= hackProjectile) {
  %p.headshot = getRandom(0,1);
  %p.isShotgun = 1;
}
Parent::onAddProjectile(%projectile, %p, %image);
}
};
activatepackage(Weapon_Shotgun);