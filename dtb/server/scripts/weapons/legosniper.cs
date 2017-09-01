//////////
// item //
//////////
datablock ItemData(legosniper)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/weapons/M82.dts";
	skinName = 'black';
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a Sniper.';
	invName = "Sniper";
	spawnName = "Lego Sniper";
	image = legosniperImage;
	threatlevel = "Normal";
};

addWeapon(legosniper);

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(legosniperImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/weapons/M82.dts";
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
   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = legosniper;
   ammo = " ";
   projectile = hackProjectile;
   projectileType = Projectile;

   projectileSpread = 0;
   projectileSpreadWalking = 8/1000;
   projectileSpreadMax = 12/1000;
   recoil = 1.6;
   recoilSeconds = 3;

   directDamage        = 80;
   radiusDamage        = 0;
   damageRadius        = 0.5;
   damagetype          = '%1 got JFK\'d by %2';
   muzzleVelocity      = 4600;
   velInheritFactor    = 0;
   impulse	       = 20;

   deathAnimationClass = "projectile";
   deathAnimation = "death5";
   deathAnimationPercent = 0.4;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = true;
   
   weaponmode = 1;
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
	stateSound[2]					= legosniperFireSound;

	stateName[3]			= "Reload";
	stateSequence[3]                = "Reload";
	stateAllowImageChange[3]        = false;
	stateTimeoutValue[3]            = 3;
	stateScript[3]                  = "onPre";
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTimeout[3]     = "Check";

	stateName[4]			= "Check";
	stateTransitionOnTriggerUp[4]	= "Ready";
	stateScript[4]                  = "onStop";

};

function legosniperImage::onPre(%this, %obj, %slot)
{	
	%obj.playthread(2, jump);
}

function legosniperImage::onStop(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}

function legosniperImage::onFire(%this, %obj, %slot)
{
   	%this.projectile.scale               = ".5 1.5 .5";
   	%this.projectile.armingDelay         = 0;
   	%this.projectile.lifetime            = 80000;
   	%this.projectile.fadeDelay           = 75000;
   	%this.projectile.bounceElasticity    = 0;
   	%this.projectile.bounceFriction      = 0;
   	%this.projectile.isBallistic         = true;
   	%this.projectile.gravityMod = 0;

	%p = Parent::onFire(%this, %obj, %slot);
	muzzleflash(%this, %obj, %slot, 0.9, 2, 0.9,%spread);
	return %p;
}

package LegoSniper_Use {
function serverCmdMountVehicle(%client) {
Parent::serverCmdMountVehicle(%client);
if(%client.player.getmountedimage(0) == nametoID(LegoSniperImage) && !%client.usedUse) {
  %client.usedUse = 1;
  %client.schedule(50,resetUsedUse);
  commandtoclient(%client,'sniperscope',2);
  return;
}
}
};
activatepackage(LegoSniper_Use);

package Weapon_Legosniper {
function onAddProjectile(%projectile, %p, %image) {
if(%image == nameToID(legoSniperImage) && %projectile $= hackProjectile)
  %p.headshot = 1;
Parent::onAddProjectile(%projectile, %p, %image);
}
};
activatepackage(Weapon_Legosniper);