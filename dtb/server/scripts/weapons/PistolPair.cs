//PistolPair.cs
//Pistol Stuff - BANBANGBANG

//////////
// item //
//////////
datablock ItemData(Pistol)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/weapons/1911.dts";
	skinName = 'base';
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a pair of pistols.';
	invName = "Pistol Pair";
	image = PistolImage;
	threatlevel = "Normal";
};

addWeapon(Pistol);

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(PistolImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/weapons/1911.dts";
   skinnable = 1; //This indicates that if the weapon pickup is skinned the image will be skinned too.
   emap = true;
    
   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0.1 0.1";
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
   item = Pistol;
   ammo = "";
   projectile = hackProjectile;
   projectileType = Projectile;

   projectileSpread = 16/1000;
   projectileSpreadWalking = 24/1000;
   projectileSpreadMax = 32/1000;
   recoil = 1.1;
   recoilSeconds = 2;

   directDamage        = 22.5;
   radiusDamage        = 0;
   damageRadius        = 0.5;
   damagetype          = '%1 got shot in the @$$ by %2';
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
	stateSound[2]					= MinigunFireSound;

	stateName[3]			= "Reload";
	stateSequence[3]                = "Reload";
	stateAllowImageChange[3]        = false;
	stateTimeoutValue[3]            = 0.05;
	stateScript[3]                  = "onPre";
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTimeout[3]     = "Check";

	stateName[4]			= "Check";
	stateScript[4]                  = "onStop";
	stateTransitionOnTriggerUp[4]	= "Ready2";
	stateAllowImageChange[4]        = true;

	stateName[5]                    = "Fire2";
	stateTransitionOnTimeout[5]     = "Reload2";
	stateTimeoutValue[5]            = 0.001;
	stateFire[5]                    = true;
	stateAllowImageChange[5]        = false;
	stateSequence[5]                = "Fire";
	stateScript[5]                  = "onFire2";
	stateWaitForTimeout[5]			= true;
	stateSound[5]					= MinigunFireSound;

	stateName[6]			= "Reload2";
	stateSequence[6]                = "Reload";
	stateAllowImageChange[6]        = false;
	stateTimeoutValue[6]            = 0.05;
	stateScript[6]                  = "onPre";
	stateWaitForTimeout[6]		= true;
	stateTransitionOnTimeout[6]     = "Check2";

	stateName[7]			= "Check2";
	stateScript[7]                  = "onStop";
	stateTransitionOnTriggerUp[7]	= "Ready";
	stateAllowImageChange[7]        = true;

	stateName[8]                     = "Ready2";
	stateScript[8]                  = "onStop";
	stateTransitionOnTriggerDown[8]  = "Fire2";
	stateAllowImageChange[8]         = true;
};

datablock ShapeBaseImageData(PistolImage2)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/weapons/1911.dts";
   skinName = 'base';
   emap = true;
    
   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 1;
   offset = "0 0.1 0.1";
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
   item = Pistol;
   ammo = "";
   projectile = hackProjectile;
   projectileType = Projectile;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = false;

};

function PistolImage::onArm(%this, %obj, %slot)
{	
	%obj.playThread(1, armreadyboth);
	%obj.lastleft = %obj.getMountedImage(1);
	%obj.mountImage(PistolImage2, 1, 1, addTaggedString(%obj.weaponSkin[%this.item.getID()]));
}

function PistolImage::onStop(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}

function PistolImage::onUnmount(%this, %obj, %slot)
{	
	//Reset so it can fire consistantly -DShiznit
	%obj.playThread(1, root);
	%obj.playThread(2, root);
	%obj.unMountImage(1);
	%obj.mountImage(%obj.lastleft, 1);
}

function PistolImage::onFire(%this, %obj, %slot)
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
	muzzleflash(%this, %obj, %slot, 0.6, 0.6, 0.6);
	%obj.playthread(2, jump);
	return %p;
}

function PistolImage::onFire2(%this, %obj, %slot)
{
	%this.onFire(%obj, 1);
}