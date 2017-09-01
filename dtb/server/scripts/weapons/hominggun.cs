datablock ProjectileData(hominggunProjectile)
{
   projectileShapeName = "dtb/data/shapes/weapons/bulletbill.dts";
   particleEmitter     = MortarCannontrailEmitter;
   muzzleVelocity      = 40;
   armingDelay         = 0;
   lifetime            = 20000;
   fadeDelay           = 16000;
   explosion           = MissileLauncherExplosion;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 0;
};

//////////
// item //
//////////
datablock ItemData(HomingGun)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/weapons/HomingCannon.dts";
	skinName = 'black';
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a Homing Cannon.';
	invName = "Homing Cannon";
	image = hominggunImage;
	threatlevel = "Normal";
};

addWeapon(HomingGun);

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(hominggunImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/weapons/HomingCannon.dts";
   skinName = 'brown';
   emap = true;
    
   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0.995 0.05 0";
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
   item = HomingGun;
   ammo = " ";
   projectile = hominggunProjectile;
   projectileType = Projectile;

   directDamage        = 200;
   radiusDamage        = 200;
   damageRadius        = 8;
   damagetype          = '%2 homed in on %1!';
   muzzleVelocity      = 40;
   velInheritFactor    = 1;

   deathAnimationClass = "explosion";
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
	stateTimeoutValue[0]             = 3;
	stateTransitionOnTimeout[0]       = "Ready";
	stateSound[0]					= weaponSwitchSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateAllowImageChange[1]         = true;

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "Reload";
	stateTimeoutValue[2]            = 0.1;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]			= true;
	stateSound[2]					= MissileLauncherFireSound;

	stateName[3]			= "Reload";
	stateSequence[3]                = "Reload";
	stateAllowImageChange[3]        = false;
	stateTimeoutValue[3]            = 3;
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTimeout[3]     = "Check";

	stateName[4]			= "Check";
	stateTransitionOnTriggerUp[4]	= "StopFire";
	stateTransitionOnTriggerDown[4]	= "Fire";

	stateName[5]                    = "StopFire";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 0.2;
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;
	//stateSequence[5]                = "Reload";
	stateScript[5]                  = "onStopFire";


};

function hominggunImage::onFire(%this,%obj,%slot) {
	%p = Parent::onFire(%this, %obj, %slot);
	%obj.applyImpulse(0, vectorScale(%obj.getMuzzleVector(%slot), -20));
	return %p;
}

function homingGunProjectile::homeIn(%this, %obj)
{
	if(!isobject(%obj))
		return;
	%initRot = vectorToEuler(%obj.initialvelocity);
	initContainerRadiusSearch(%obj.getPosition(), 500, $TypeMasks::PlayerObjectType);
	while(%search = containerSearchNext()) {
		if(%obj.client == %search.client)
			continue;
		if(isObject(%search))
			%t = %search;
		break;
	}
	if(isObject(%t)) {
		%pos = vectorAdd(%t.getPosition(), "0 0" SPC (getWord(%t.getScale(), 2) * 1.5) -1);
		%vec = vectorSub(%pos, %obj.getPosition());
		if(vectorLen(%vec) > 15) {
			%vec = vectorScale(vectorNormalize(%vec), vectorLen(%obj.initialVelocity));
			%rotdiff = vectorSub(%initrot, vectorToEuler(%vec));
			if(getWord(%rotDiff, 2) > 180)
				%rotDiff = vectorAdd(%rotDiff, "0 0 -360");
			if(getWord(%rotDiff, 2) < -180)
				%rotDiff = vectorAdd(%rotDiff, "0 0 360");
			if(vectorLen(%rotDiff) > 80) {
				%rotDiff = vectorScale(%rotdiff, 80 / vectorLen(%rotDiff));
				%trim = 1;
			}
			%len = vectorLen(%vec);
			if(%trim)
				%newrot = vectorSub(%initrot, vectorScale(%rotDiff, 0.5));
			else
				%newrot = vectorSub(%initrot, %rotdiff);
			%xs = mSin($pi / 180 * getWord(%newrot, 0)) * %len;
			%xc = mCos($pi / 180 * getWord(%newrot, 0)) * %len; //I forget what the purpose of this was
			%zs = mSin($pi / 180 * getWord(%newrot, 2)) * %len;
			%zc = mCos($pi / 180 * getWord(%newrot, 2)) * %len;
			%vec = %zs SPC %zc SPC %xs;
			%p = new Projectile() {
				initialVelocity		= %vec;
				initialPosition		= %obj.getPosition();
				datablock		= %this;
				sourceObject		= %obj.client.player;
				sourceSlot		= %obj.sourceSlot;
				client			= %obj.client;
				c			= %obj.c + 1;
			};
			%p.directDamage = %obj.directDamage;
			%p.radiusDamage = %obj.radiusDamage;
			%p.damageRadius = %obj.damageRadius;
			%p.damagetype   = %obj.damagetype;
			%p.impulse      = %obj.impulse;
			%obj.delete();
		}
	}
	if(%p.c < 30)
		%p.homeSchedule = %this.schedule(getRandom(300, 400), "homeIn", %p);
}

function hominggunProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal) {
	tbmcollison(%this, %obj, %col, %fade, %pos, %normal);
	tbmradiusDamage
	(%obj, VectorAdd(%pos, VectorScale(%normal, 0.01)),
	%obj.damageRadius,%obj.radiusDamage,%obj.damageType,60);
	cancel(%obj.homeschedule);
}

package Weapon_HomingGun {
function onAddProjectile(%projectile, %p, %image) {
if(%projectile $= homingGunProjectile)
  hominggunProjectile.schedule(500, "homeIn", %p);
Parent::onAddProjectile(%projectile, %p, %image);
}
};
activatepackage(Weapon_HomingGun);