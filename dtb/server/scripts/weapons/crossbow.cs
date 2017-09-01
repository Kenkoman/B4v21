exec("~/data/shapes/weapons/crossbow.cs");
//////////
// item //
//////////

datablock ItemData(Crossbow)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/weapons/Crossbow.dts";
	skinName = 'black';
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a Crossbow.';
	invName = "Crossbow";
	image = CrossbowImage;
	threatlevel = "Normal";
};

addWeapon(Crossbow);

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(CrossbowImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/weapons/Crossbow.dts";
   skinName = 'graydark';
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
   item = Crossbow;
   ammo = "";
   projectile = arrowProjectile;
   projectileType = Projectile;

   directDamage        = 80;
   radiusDamage        = 0;
   damageRadius        = 0.5;
   damagetype        = '%1 was impaled by %2';
   muzzleVelocity      = 400;

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
	stateSequence[0]                = "Fire";
	stateTransitionOnTimeout[0]       = "Ready";
	stateSound[0]					= weaponSwitchSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateScript[1]                  = "onStop";
	stateSequence[1]                = "Reload";
	stateAllowImageChange[1]         = true;

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "Reload";
	stateTimeoutValue[2]            = 0.001;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]			= true;
	stateSound[2]					= bowFireSound;

	stateName[3]			= "Reload";
	stateSequence[3]                = "Reload";
	stateAllowImageChange[3]        = false;
	stateTimeoutValue[3]            = 2;
	stateScript[3]                  = "onPre";
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTimeout[3]     = "Check";

	stateName[4]			= "Check";
	stateTransitionOnTriggerUp[4]	= "Ready";
	stateScript[4]                  = "onStop";

};

function CrossbowImage::onPre(%this, %obj, %slot)
{	
	%obj.playthread(2, jump);
}

function CrossbowImage::onStop(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}