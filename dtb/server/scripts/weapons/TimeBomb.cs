//Time bomb By DShiznit, designed for Hudson Hawkness

datablock ItemData(TimeBomb)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "tbm/data/shapes/dynamite.dts";
	skinName = 'grayDark';
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a Time Bomb.';
	invName = "Time Bomb";
	image = TimeBombImage;
	threatlevel = "Normal";
};

addWeapon(TimeBomb);

datablock ProjectileData(TimeBombProjectile)
{
   projectileShapeName = "tbm/data/shapes/dynamite.dts";
   explosion           = GLExplosion;
//   particleEmitter     = LasergunTrailEmitter;
   muzzleVelocity      = 20;

   armingDelay         = 10000;
   lifetime            = 10500;
   fadeDelay           = 10000;
   bounceElasticity    = 0.5;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 1;
   scale               = "1 1 1";
   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "0.7 0.4 0";
};

////////////////
//weapon image//
////////////////

datablock ShapeBaseImageData(TimeBombImage)
{
   // Basic Item properties
   shapeFile = "tbm/data/shapes/dynamite.dts";
   skinName = 'brown';
   emap = true;
    
   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0.17 0.32";
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
   item = TimeBomb;
   ammo = " ";
   projectile = TimeBombProjectile;
   projectileType = Projectile;
   projectileSpread = 22/1000;

   directDamage        = 0;
   radiusDamage        = 250;
   damageRadius        = 8;
   damagetype          = '%1 got obsessed with shiny things, particulary %2\'s Time Bomb';
   muzzleVelocity      = 20;
   velInheritFactor    = 1;

   deathAnimationClass = "explosion";
   deathAnimation = "death5";
   deathAnimationPercent = 0.4;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = true;
   
   // Initial start up state
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 1;
	stateTransitionOnTimeout[0]      = "Ready";
	stateSound[0]			 = weaponSwitchSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "PreFire";
	stateAllowImageChange[1]         = true;

	stateName[5]                     = "PreFire";
	stateTransitionOnTimeout[5]     = "Fire";
	stateTimeoutValue[5]            = 0.1;
	stateAllowImageChange[5]         = true;
	stateWaitForTimeout[5]			= true;
	stateScript[5]                  = "onThrow";

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "Reload";
	stateTimeoutValue[2]            = 0.2;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]			= true;
	stateSound[2]					= bowFireSound;
	
	stateName[3]			= "Reload";
	stateSequence[3]                = "Reload";
	stateAllowImageChange[3]        = false;
	stateTimeoutValue[3]            = 1;
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTimeout[3]     = "Check";
	stateScript[3]                  = "onStopThrow";

	stateName[4]			= "Check";
	stateTransitionOnTriggerUp[4]	= "Ready";
	stateScript[4]                  = "onStopFire";


};

function TimeBombProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	tbmcollison(%this, %obj, %col, %fade, %pos, %normal);
       tbmradiusDamage
     (%obj, VectorAdd(%pos, VectorScale(%normal, 0.01)),
      %obj.damageRadius,%obj.radiusDamage,%obj.damageType,100);
}

function TimeBombImage::onThrow(%this, %obj)
{
	%obj.playthread(2, armattack);
}

function TimeBombImage::onStopThrow(%this, %obj)
{
	%obj.playthread(2, root);
}

function TimeBombImage::onMount(%this, %obj)
{
	%obj.playthread(1, armreadyright);
}

datablock ShapeBaseImageData(TimeBombMountImage) {
	shapeFile = "tbm/data/shapes/mountdynamite.dts";
	cloaktexture = "tbm/data/shapes/base.transmore";
	mountPoint = 5;
	rotation = "0 0 1 90";
	className = "ItemImage";
	cloakable = false;
};

function TimeBombMountImage::onMount(%this, %obj)
{
	%this.schedule(3000, onFuck, %obj);
	%obj.playthread(3, side);
}

function TimeBombMountImage::onFuck(%this,%obj)
{
	createExplosion(nameToID(GLExplosion), %obj.getPosition());
	tbmradiusDamage(%obj.bombclient.player, %obj.getPosition(), 8, 200, TimeBombImage.damagetype, 100);
	%obj.unMountImage(4);
	%obj.playthread(3, root);
}

function TimeBombProjectile::stick(%this,%prj) {
if(!isobject(%prj))
return;
initContainerRadiusSearch(%prj.getPosition(), 0.5, $TypeMasks::ShapeBaseObjectType);
while(1) {
	%search=containerSearchNext();
	if(%prj.client==%search.client) {
		//continue;
	}
	if(isObject(%search)) {
		%obj = %search;
	}
	break;
}
if(isObject(%obj)) {
	if (%obj.getClassName() $= "Player" || %obj.getClassName() $= "AIPlayer" || %obj.getClassName() $= "FlyingVehicle" || %obj.getClassName() $= "WheeledVehicle" || ($bricks[8,0] <= nameToID(%obj.getDatablock()) && nameToID(%obj.getDatablock()) <= $bricks[8,1])) {
		%obj.bombclient = %prj.client;
		%prj.delete();
		%obj.mountimage(TimeBombMountImage, 4);
	}
}
TimeBombProjectile.schedule(50, "stick", %prj);
}

package Weapon_Timebomb {
function onAddProjectile(%projectile, %p, %image) {
if(%projectile $= timeBombProjectile)
  TimeBombProjectile.schedule(150, "stick", %p);
Parent::onAddProjectile(%projectile, %p, %image);
}
};
activatepackage(Weapon_Timebomb);