//--------------------------------------------------------------------------
// Weapon Item.  This is the item that exists in the world, i.e. when it's
// been dropped, thrown or is acting as re-spawnable item.  When the weapon
// is mounted onto a shape, the RifleImage is used.

datablock ItemData(CombatShotgun)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/weapons/SPAS12.dts";
	skinName = 'grayDark';
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a SPAS 12-Guage Shotgun.';
	invName = "Combat Shotgun";
	image = CombatShotgunImage;
	threatlevel = "Normal";
};

addWeapon(CombatShotgun);

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(CombatShotgunImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/weapons/SPAS12.dts";
   skinName = 'brown';
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
   item = CombatShotgun;
   ammo = "";
   projectile = hackProjectile;
   projectileType = Projectile;

   projectileSpread = 16/1000;
   projectileSpreadWalking = 18/1000;
   projectileSpreadMax = 24/1000;
   recoil = 1.2;
   recoilSeconds = 1;

   directDamage        = 12.5;
   radiusDamage        = 0;
   damageRadius        = 2;
   damagetype          = '%1 was blown away by %2';
   shellCount          = 5;
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
	stateTimeoutValue[3]            = 0.2;
	stateScript[3]                  = "onPre";
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTimeout[3]     = "Check";

	stateName[4]			= "Check";
	stateScript[4]                  = "onStop";
	stateTransitionOnTriggerUp[4]	= "Ready";
	stateAllowImageChange[4]        = true;

};


//-----------------------------------------------------------------------------


function CombatshotgunImage::onFire(%this, %obj, %slot)
{
      // Decrease the weapons ammo on fire
      // %obj.decInventory(%this.ammo,1);
   	%this.projectile.scale               = ".25 .25 .25";
   	%this.projectile.armingDelay         = 0;
   	%this.projectile.lifetime            = 2000;
   	%this.projectile.fadeDelay           = 1500;
   	%this.projectile.bounceElasticity    = 0;
   	%this.projectile.bounceFriction      = 0;
   	%this.projectile.isBallistic         = true;
   	%this.projectile.gravityMod          = 0.10;

	%p = Parent::onFire(%this, %obj, %slot);

	for(%i = 0; %i < getWordCount(%p); %i++)
		getWord(%p, %i).isShotGun = true; //Not adding this to onAddProjectile (the right way) since this is unused anyways

	return %p;
}

function CombatShotgunImage::onPre(%this, %obj, %slot)
{	
	%obj.playthread(2, jump);
}

function CombatShotgunImage::onStop(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}