//--------------------------------------------------------------------------
// Weapon Item.  This is the item that exists in the world, i.e. when it's
// been dropped, thrown or is acting as re-spawnable item.  When the weapon
// is mounted onto a shape, the RifleImage is used.

datablock ItemData(HailfireRocket)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "tbm/data/shapes/bricks/hailfire4.dts";
	skinName = 'grayDark';
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a Hailfire Rocket Launcher.';
	invName = "Hailfire Rocket";
	image = HailfireRocketImage;
	threatlevel = "Dangerous";
};

addWeapon(HailfireRocket);

////////////////
//weapon image//
////////////////

datablock ShapeBaseImageData(HailfireRocketImage)
{
   // Basic Item properties
   shapeFile = "tbm/data/shapes/bricks/hailfire4.dts";
   skinName = 'brown';
   emap = true;
    
   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0.1";
   rotation = "0 0 1 -2.3";
   //eyeOffset = "0.1 0.2 -0.55";


   projectileSpread = 22/1000; 
   projectileSpreadWalking = 33/1000;
   projectileSpreadMax = 44/1000;
   recoil = 1.05;
   recoilSeconds = 1;

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = HailfireRocket;
   ammo = " ";
   projectile = hack2Projectile;
   projectileType = Projectile;

   directDamage        = 0;
   radiusDamage        = 100;
   damageRadius        = 8;
   damagetype          = '%1 got ****ed in the *** by %2';
   muzzleVelocity      = 200;
   velInheritFactor    = 1;
   impulse	       = 20;

   deathAnimationClass = "explosion";
   deathAnimation = "death5";
   deathAnimationPercent = 0.4;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = true;
   
   // Initial start up state
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.125;
	stateTransitionOnTimeout[0]      = "Ready";
	stateSound[0]			 = weaponSwitchSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateAllowImageChange[1]         = true;

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "Reload";
	stateTimeoutValue[2]            = 0.025;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]			= true;
	stateSound[2]					= MissileLauncherFireSound;
	
	stateName[3]			= "Reload";
	stateSequence[3]                = "Reload";
	stateAllowImageChange[3]        = false;
	stateTimeoutValue[3]            = 0.125;
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTimeout[3]     = "Check";

	stateName[4]			= "Check";
	stateTransitionOnTriggerUp[4]	= "StopFire";
	stateTransitionOnTriggerDown[4]	= "Fire";

	stateName[5]                    = "StopFire";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 0.025;
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;
	//stateSequence[5]                = "Reload";
	stateScript[5]                  = "onStopFire";


};

//-----------------------------------------------------------------------------


function HailfireRocketImage::onFire(%this, %obj, %slot)
{
	// Decrease the weapons ammo on fire
	// %obj.decInventory(%this.ammo,1);

   	%this.projectile.armingDelay         = 0;
   	%this.projectile.lifetime            = 4000;
   	%this.projectile.fadeDelay           = 4000;
   	%this.projectile.bounceElasticity    = 0;
   	%this.projectile.bounceFriction      = 0;
   	%this.projectile.isBallistic         = true;
   	%this.projectile.gravityMod = 0.1;
   	%this.projectile.hasLight    = true;
   	%this.projectile.lightRadius = 3.0;
   	%this.projectile.lightColor  = "0.7 0 0";

	%p = Parent::onFire(%this, %obj, %slot);
	return %p;
}

function hailfirerocketImage::onMount(%this, %obj)
{
	%obj.playthread(1, armreadyright);
	messageAll( 'MsgClientKilled','%1 \c2pulled out a \c9HAIL-FIRE-ROCKET-LAUNCHER\c2. We\'re all F***ed.',%obj.client.name);
}
