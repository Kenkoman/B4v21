//Original weapon: Beach Ball - just a ball to pass around. Made by EmperorWiggy and FeedBack.
//Basketball skin and edits by DShiznit. 

//////////
// item //
//////////
datablock ItemData(BasketBall)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	// Basic Item Properties
	ShapeFile = "tbm/data/shapes/decal/Basketball2.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a BasketBall.';
	invName = "Basketball";
	image = BasketBallImage;
	threatlevel = "Safe";
};

addWeapon(BasketBall);

////////////////
//weapon image//
////////////////

datablock ProjectileData(BasketBallProjectile)
{
   projectileShapeName = "tbm/data/shapes/decal/Basketball2.dts";
// explosion   = GLExplosion;
// particleEmitter = arrowTrailEmitter;
   muzzleVelocity      = 25;

   armingDelay = 10000;
   lifetime= 10500;
   fadeDelay   = 10000;
   bounceElasticity= 0.85;
   bounceFriction  = 0.3;
   isBallistic = true;
   gravityMod = 1;

   hasLight= false;
   lightRadius = 10.0;
   lightColor  = "0.8 0.4 0.0";
};

datablock ShapeBaseImageData(BasketBallImage)
{
   // Basic Item properties
   shapeFile = "tbm/data/shapes/decal/Basketball2.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;

   offset = "-0.55 0 0";
   //eyeOffset = "0 0 -0.1";

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off. 
   correctMuzzleVector = false;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = BasketBall;
   ammo = " ";
   projectile = BasketBallProjectile;
   projectileType = Projectile;

   directDamage        = 0;
   radiusDamage        = 0;
   damageRadius        = 0;
   muzzleVelocity      = 25;
   velInheritFactor    = 1;

   //melee particles shoot from eye node for consistancy
   melee = true;
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
	stateName[0] = "Activate";
	stateTimeoutValue[0] = 0.5;
	stateScript[0]  = "onArm";
	stateTransitionOnTimeout[0]   = "Ready";
	stateSound[0]					= weaponSwitchSound;

	stateName[1] = "Ready";
	stateTransitionOnTriggerDown[1]  = "PreFire";
	stateAllowImageChange[1] = true;

	stateName[2]   = "PreFire";
	stateScript[2]  = "onPreFire";
	stateAllowImageChange[2]= false;
	stateTimeoutValue[2]= 0.0999;
	stateTransitionOnTimeout[2] = "Fire";

	stateName[3]= "Fire";
	stateSound[3]					= bowFireSound;
	stateTransitionOnTimeout[3] = "CheckFire";
	stateTimeoutValue[3]= 0.1;

	stateFire[3]= true;
	stateAllowImageChange[3]= false;
	stateSequence[3]= "Fire";
	stateScript[3]  = "onFire";
	stateWaitForTimeout[3]  = true;
	//stateTransitionOnTriggerUp[3] = "StopFire";

	stateName[4]   = "CheckFire";
	stateTimeoutValue[4]= 0.0001;
	stateScript[4]  = "onStopFire";
	stateTransitionOnTimeout[4] = "StopFire";

 	stateName[5]= "StopFire";
	stateTransitionOnTriggerUp[5] = "Ready";
	stateAllowImageChange[5]= false;
	stateSequence[5]= "StopFire";
	stateScript[5]  = "onStopFire";

};


function BasketBallImage::onMount(%this, %obj)
{
	%obj.playthread(1, armreadyboth);
	%obj.extraBasketball = 0;
	%this.schedule(1000, theftCheck, %obj);
}

function BasketBallImage::onUnmount(%this, %obj)
{
	%obj.playThread(1, root);
}

function BasketBallImage::theftCheck(%this, %obj)
{
	if(isObject(%obj)) {
		if(%obj.getState() !$= "Dead") {
			if(%obj.getMountedImage(0) == nameToID(%this)) {
				initContainerRadiusSearch(%obj.getWorldBoxCenter(), 1.5, $TypeMasks::PlayerObjectType);
				if((%o = containerSearchNext()) == %obj)
					%o = containerSearchNext();
				if(isObject(%o)) {
					if(%o.getClassName() $= "Player" && !%o.getMountedImage(0)) {
						if(%obj.getPointSightZone(%o.getWorldBoxCenter()) == 1) {
							%o.mountimage(BasketBallImage, 0);
							if(%a = %o.giveItem(BasketBall))
								messageClient(%o.client, 'MsgHilightInv', '', %obj.currWeaponSlot = %a);
							if(%a == -2)
								%o.extraBasketball = 1;
							%obj.unmountImage(0);
							%obj.playThread(1, root);
							%obj.playThread(3, root);
							if(!%obj.extraBasketball)
								removeFromInventory(%obj,BasketBall);
							%obj.extraBasketball = 0;
							return;
						}
					}
				}
				%this.schedule(100, theftCheck, %obj);
			}	
		}
	}
}

function BasketBallImage::onFire(%this, %obj, %slot) {   
	%muzzleVector = %obj.getMuzzleVector(%slot);
	%prj = Parent::onFire(%this, %obj, %slot);
	%obj.unmountImage(0);
	%obj.playThread(1, root);
	%obj.playThread(3, root);
	if(!%obj.extraBasketball)
		removeFromInventory(%obj,BasketBall);
	%obj.extraBasketball = 0;
}

package Weapon_Basketball {
function onAddProjectile(%projectile, %p, %image) {
if(%projectile $= basketBallProjectile)
  BasketBallProjectile.schedule(150, "colCheck", %p);
Parent::onAddProjectile(%projectile, %p, %image);
}
};
activatepackage(Weapon_Basketball);

//Semi-hacky way of detecting collisions with players.
//It had been tweaked for maximum success rate with the beach ball, but I don't think Shiz changed the values any.
//It's still highly accurate.
function BasketBallProjectile::colCheck(%this, %prj) {
if(!isobject(%prj))
return;
initContainerRadiusSearch(%prj.getPosition(), 0.55, $TypeMasks::PlayerObjectType);
if(isObject(%obj = containerSearchNext())) {
	if(!%obj.getMountedImage(0)) {
		%obj.mountimage(BasketBallImage, 0);
		if (%obj.getClassName() $= "AIPlayer") {
			%obj.schedule(1000, setImageTrigger, 0, 1);
			%obj.schedule(4000, setImageTrigger, 0, 0);
		}
		else {
			if(%a = %obj.giveItem(BasketBall))
				messageClient(%obj.client, 'MsgHilightInv', '', %obj.currWeaponSlot = %a);
			if(%a == -2)
				%obj.extraBasketball = 1;
		}
		%prj.delete();
	}
}
BasketBallProjectile.schedule(50, "colCheck", %prj);
}

function BasketBallProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal) {
if (%col.getClassName() $= "Player" || %col.getClassName() $= "AIPlayer") {
	if(!%col.getMountedImage(0)) {
		%col.mountimage(BasketBallImage, 0);
		if (%col.getClassName() $= "AIPlayer") {
			%col.schedule(1000, setImageTrigger,0,1);
			%col.schedule(4000, setImageTrigger,0,0);
		}
		else {
			if(%a = %obj.giveItem(BasketBall))
				messageClient(%obj.client, 'MsgHilightInv', '', %obj.currWeaponSlot = %a);
			if(%a == -2)
				%obj.extraBasketball = 1;
		}
	}


}
}

function removeFromInventory (%player, %item) {
for(%i = 0; %i < %player.getdatablock().maxItems; %i++) {
	if (%player.inventory[%i] == nametoid(%item)) {
		%player.inventory[%i] = "";
		messageClient(%player.client, 'MsgDropItem', '', %i);
		if(%player.currWeaponSlot == %i) {
			%player.unmountImage(0);
			%player.currWeaponSlot = -1;
		}
	}
}
}