//Magnum.cs
//MAGNUM - BANBANGBANG

//////////
// item //
//////////

datablock ItemData(Magnum)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/weapons/revolver2.dts";
	skinName = 'white';
	rotate = true;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a Magnum revolver';
	invName = "Magnum";
	image = MagnumImage;
	threatlevel = "Normal";
};

addWeapon(Magnum);

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(MagnumImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/weapons/revolver2.dts";
   skinnable = 1; //This indicates that if the weapon pickup is skinned the image will be skinned too.
   emap = true;
    
   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0.05";
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
   item = Magnum;
   ammo = " ";
   projectile = hackProjectile;
   projectileType = Projectile;

   projectileSpread = 2/1000;
   projectileSpreadWalking = 3/1000;
   projectileSpreadMax = 4/1000;
   recoil = 1.5;
   recoilSeconds = 1.5;

   directDamage        = 45;
   radiusDamage        = 0;
   damageRadius        = 1;
   damagetype          = '%1\'s face was blown off by %2';
   muzzleVelocity      = 2600;
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
	stateScript[1]                  = "onStop";
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
	stateTimeoutValue[3]            = 0.5;
	stateScript[3]                  = "onPre";
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTimeout[3]     = "Check";

	stateName[4]			= "Check";
	stateTransitionOnTriggerUp[4]	= "Ready2";
	stateScript[4]                  = "onStop";

	stateName[5]                     = "Ready2";
	stateTransitionOnTriggerDown[5]  = "Fire2";
	stateScript[5]                  = "onStop";
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
	stateTimeoutValue[7]            = 0.5;
	stateScript[7]                  = "onPre";
	stateWaitForTimeout[7]		= true;
	stateTransitionOnTimeout[7]     = "Check2";

	stateName[8]			= "Check2";
	stateTransitionOnTriggerUp[8]	= "Ready3";
	stateScript[8]                  = "onStop";

	stateName[9]                     = "Ready3";
	stateTransitionOnTriggerDown[9]  = "Fire3";
	stateScript[9]                  = "onStop";
	stateAllowImageChange[9]         = true;

	stateName[10]                    = "Fire3";
	stateTransitionOnTimeout[10]     = "Reload3";
	stateTimeoutValue[10]            = 0.001;
	stateFire[10]                    = true;
	stateAllowImageChange[10]        = false;
	stateSequence[10]                = "Fire3";
	stateScript[10]                  = "onFire";
	stateWaitForTimeout[10]			= true;
	stateSound[10]					= ShotgunFireSound;

	stateName[11]			= "Reload3";
	stateSequence[11]                = "Reload3";
	stateAllowImageChange[11]        = false;
	stateTimeoutValue[11]            = 0.5;
	stateScript[11]                  = "onPre";
	stateWaitForTimeout[11]		= true;
	stateTransitionOnTimeout[11]     = "Check3";

	stateName[12]			= "Check3";
	stateTransitionOnTriggerUp[12]	= "Ready4";
	stateScript[12]                  = "onStop";


	stateName[13]                     = "Ready4";
	stateTransitionOnTriggerDown[13]  = "Fire4";
	stateScript[13]                  = "onStop";
	stateAllowImageChange[13]         = true;

	stateName[14]                    = "Fire4";
	stateTransitionOnTimeout[14]     = "Reload4";
	stateTimeoutValue[14]            = 0.001;
	stateFire[14]                    = true;
	stateAllowImageChange[14]        = false;
	stateSequence[14]                = "Fire4";
	stateScript[14]                  = "onFire";
	stateWaitForTimeout[14]			= true;
	stateSound[14]					= ShotgunFireSound;

	stateName[15]			= "Reload4";
	stateSequence[15]                = "Reload4";
	stateAllowImageChange[15]        = false;
	stateTimeoutValue[15]            = 0.5;
	stateScript[15]                  = "onPre";
	stateWaitForTimeout[15]		= true;
	stateTransitionOnTimeout[15]     = "Check4";

	stateName[16]			= "Check4";
	stateTransitionOnTriggerUp[16]	= "Ready5";
	stateScript[16]                  = "onStop";

	stateName[17]                     = "Ready5";
	stateTransitionOnTriggerDown[17]  = "Fire5";
	stateScript[17]                  = "onStop";
	stateAllowImageChange[17]         = true;

	stateName[18]                    = "Fire5";
	stateTransitionOnTimeout[18]     = "Reload5";
	stateTimeoutValue[18]            = 0.001;
	stateFire[18]                    = true;
	stateAllowImageChange[18]        = false;
	stateSequence[18]                = "Fire5";
	stateScript[18]                  = "onFire";
	stateWaitForTimeout[18]		= true;
	stateSound[18]			= ShotgunFireSound;

	stateName[19]			= "Reload5";
	stateSequence[19]                = "Reload5";
	stateAllowImageChange[19]        = false;
	stateTimeoutValue[19]            = 0.5;
	stateScript[19]                  = "onPre";
	stateWaitForTimeout[19]		= true;
	stateTransitionOnTimeout[19]     = "Check5";

	stateName[20]			= "Check5";
	stateTransitionOnTriggerUp[20]	= "Ready6";
	stateScript[20]                  = "onStop";

	stateName[21]                     = "Ready6";
	stateTransitionOnTriggerDown[21]  = "Fire6";
	stateScript[21]                  = "onStop";
	stateAllowImageChange[21]         = true;

	stateName[22]                    = "Fire6";
	stateTransitionOnTimeout[22]     = "Reload6";
	stateTimeoutValue[22]            = 0.001;
	stateFire[22]                    = true;
	stateAllowImageChange[22]        = false;
	stateSequence[22]                = "Fire6";
	stateScript[22]                  = "onFire";
	stateWaitForTimeout[22]			= true;
	stateSound[22]					= ShotgunFireSound;

	stateName[23]			= "Reload6";
	stateSequence[23]                = "Reload6";
	stateAllowImageChange[23]        = false;
	stateTimeoutValue[23]            = 2.5;
	stateScript[23]                  = "onPre";
	stateWaitForTimeout[23]		= true;
	stateTransitionOnTimeout[23]     = "Check6";

	stateName[24]			= "Check6";
	stateTransitionOnTriggerUp[24]	= "Ready";
	stateScript[24]                  = "onStop";
};

function MagnumImage::onStop(%this, %obj, %slot)
{	
	%obj.playthread(2, root);

}

function MagnumImage::onPre(%this, %obj, %slot)
{	
	%obj.playthread(2, jump);
}

function MagnumImage::onFire(%this, %obj, %slot)
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

	muzzleflash(%this, %obj, %slot, 1, 1.2, 1);
	return %p;
}
   
package Weapon_Magnum {
function onAddProjectile(%projectile, %p, %image) {
if(%image == nameToID(MagnumImage) && %projectile $= hackProjectile)
  %p.headshot = 1;
Parent::onAddProjectile(%projectile, %p, %image);
}
};
activatepackage(Weapon_Magnum);