//////////
// item //
//////////
datablock ItemData(Bat)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/feedback/legobat.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a Baseball Bat';
	invName = "Baseball Bat";
	image = BatImage;
	threatlevel = "Normal";
};

addWeapon(Bat);

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(BatImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/feedback/legobat.dts";
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
   correctMuzzleVector = false;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = Bat;
   ammo = "";
   projectile = swordProjectile;
   projectileType = Projectile;

   directDamage        = 20;
   radiusDamage        = 0;
   damageRadius        = 0.5;
   damagetype          = '%2 beat the stuffing out of %1';
   muzzleVelocity      = 50;
   velInheritFactor    = 1;
   impulse             = 0;

   deathAnimationClass = "projectile";
   deathAnimation = "melee";
   deathAnimationPercent = 0.4;

   //melee particles shoot from eye node for consistancy
   melee = true;
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

	stateName[2]			= "PreFire";
	stateScript[2]                  = "onPreFire";
	stateAllowImageChange[2]        = false;
	stateTimeoutValue[2]            = 0.0999;
	stateTransitionOnTimeout[2]     = "Fire";

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "CheckFire";
	stateTimeoutValue[3]            = 0.1;
	stateFire[3]                    = true;
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	stateScript[3]                  = "onFire";
	stateWaitForTimeout[3]		= true;
	//stateTransitionOnTriggerUp[3]	= "StopFire";

	stateName[4]			= "CheckFire";
	stateTimeoutValue[4]            = 0.0001;
	stateScript[4]                  = "onStopFire";
	stateTransitionOnTimeout[4]     = "StopFire";

	
	stateName[5]                    = "StopFire";
	stateTransitionOnTriggerUp[5]     = "Ready";
	stateAllowImageChange[5]        = false;
	stateSequence[5]                = "StopFire";
	stateScript[5]                  = "onStopFire";


};

function BatImage::onPreFire(%this, %obj, %slot)
{
	%obj.playthread(2, armattack);
}

function BatImage::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
	//messageAll( 'MsgClient', 'stopfire');
}

function BatImage::onFire(%this, %obj, %slot)
{
   	%this.projectile.hasLight    = false;
	%p = Parent::onFire(%this, %obj, %slot);
	return %p;
}

package Weapon_Bat {
function onAddProjectile(%projectile, %p, %image) {
if(%image == nameToID(batImage) && %projectile $= swordProjectile) {
  %p.isBat = 1;
  %p.isBlunt = 1;
}
Parent::onAddProjectile(%projectile, %p, %image);
}
};
activatepackage(Weapon_Bat);