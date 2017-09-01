datablock ExplosionData( FlashlightExplosion )
{
	lifeTimeMS = 100;
    lifetimeVarianceMS = 0;

	// This will make the camera shake when a player gets hit by a rocket.
    // Shoot your own feet to see this effect in action.
	shakeCamera      = false;

	// This will create a dynamic lighting effect in the vicinity of the 
    // rocket's explosion.
	lightStartRadius = 10;
	lightEndRadius   = 10;
	lightColor  = "1.0 1.0 0.9 1";
	lightEndColor    = "1.0 1.0 0.9 0";
};

//weapon stuff

datablock ProjectileData(FlashlightProjectile)
{
   projectileShapeName = "";
   explosion           = FlashlightExplosion;
   muzzleVelocity      = 10000;
   velInheritFactor    = 0.5;

   armingDelay         = 0;
   lifetime            = 1000;
   fadeDelay           = 1000;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 0;

   hasLight    = false;
   lightRadius = 10.0;
   lightColor  = "1.0 1.0 0.9 0.8";
};

//////////
// item //
//////////
datablock ItemData(Flashlight)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "tbm/data/shapes/flashlight.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = false;
	 // Dynamic properties defined by the scripts
	pickUpName = 'a Flashlight.';
        spawnname = "Flashlight";
	invName = "Maglite SB";
	image = FlashlightImage;
	threatlevel = "Safe";
};

addWeapon(Flashlight);

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(FlashlightImage)
{
   // Basic Item properties
   shapeFile = "tbm/data/shapes/flashlight.dts";
   emap = false;
   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";
   // Projectile && Ammo.
   item = Flashlight;
   ammo = "";
   projectile = FlashlightProjectile;
   projectileType = Projectile;

   directDamage        = 0;
   radiusDamage        = 0;
   damageRadius        = 0;
   muzzleVelocity      = 10000;
   velInheritFactor    = 0.5;

   deathAnimationClass = "melee";
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
   // predict state changes and animate accordinRPGy.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.125;
	stateTransitionOnTimeout[0]       = "Ready";
	stateSound[0]					= weaponSwitchSound;

	stateName[1]                     = "Ready";
	stateTransitionOnAmmo[1]         = "ReadyOff";
	stateTransitionOnTriggerDown[1]  = "Attack";
	stateTransitionOnTimeout[1]      = "Reset";
	stateTimeoutValue[1]             = 0.001;
	stateScript[1]                   = "onFire";
	stateAllowImageChange[1]         = true;
	stateSequence[1]                = "root";

	stateName[2]                    = "Attack";
	stateTransitionOnTimeout[2]     = "stop";
	stateTimeoutValue[2]            = 0.2;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onAttack";
	stateWaitForTimeout[2]		= true;

	stateName[3]                     = "Stop";
	stateTransitionOnTimeout[3]     = "PreReady";
	stateTimeoutValue[3]            = 0.001;
	stateScript[3]                  = "onStop";
	stateAllowImageChange[3]         = false;

	stateName[4]                     = "Reset";
	stateTransitionOnTimeout[4]     = "Ready";
	stateTimeoutValue[4]            = 0.001;
	stateAllowImageChange[4]         = false;
	
	stateName[5]                     = "PreReady";
	stateTransitionOnTriggerUp[5]     = "Ready";
	stateTransitionOnTimeout[5]     = "ResetTwo";
	stateTimeoutValue[5]            = 0.001;
	stateAllowImageChange[5]         = false;
	
	stateName[6]                     = "ResetTwo";
	stateTransitionOnTimeout[6]     = "PreReady";
	stateTimeoutValue[6]            = 0.001;
	stateScript[6]                  = "onFire";
	stateAllowImageChange[6]         = false;

	stateName[7]                     = "ReadyOff";
	stateTransitionOnNoAmmo[7]       = "Ready";
	stateTransitionOnTriggerDown[7]  = "AttackOff";
	stateTransitionOnTimeout[7]      = "ResetOff";
	stateTimeoutValue[7]             = 0.001;
	stateAllowImageChange[7]         = true;
	stateSequence[7]                = "fade";

	stateName[8]                    = "AttackOff";
	stateTransitionOnTimeout[8]     = "StopOff";
	stateTimeoutValue[8]            = 0.2;
	stateFire[8]                    = true;
	stateAllowImageChange[8]        = false;
	stateSequence[8]                = "Fire";
	stateScript[8]                  = "onAttack";
	stateWaitForTimeout[8]		= true;

	stateName[9]                     = "StopOff";
	stateTransitionOnTimeout[9]     = "PreReadyOff";
	stateTimeoutValue[9]            = 0.001;
	stateScript[9]                  = "onStop";
	stateAllowImageChange[9]         = false;

	stateName[10]                     = "ResetOff";
	stateTransitionOnTimeout[10]     = "ReadyOff";
	stateTimeoutValue[10]            = 0.001;
	stateAllowImageChange[10]         = false;
	
	stateName[11]                     = "PreReadyOff";
	stateTransitionOnTriggerUp[11]     = "ReadyOff";
	stateAllowImageChange[11]         = false;
};

function flashlightImage::onMount(%this,%obj)
{
	%obj.playthread(1, armreadyright);
	%obj.playthread(2, spearready);
}

function flashlightImage::onUnmount(%this,%obj)
{
	%obj.playthread(1, root);
	%obj.playthread(2, root);
}

function flashlightImage::onStop(%this,%obj)
{
	%obj.stopthread(1);
	%obj.playthread(1, armreadyright);
}

function FlashlightImage::onAttack(%this, %obj, %slot)
{
	%obj.playthread(1, armattack);
	%projectile = swordProjectile;
	%spread = calculateSpread(%this, %obj);
	%muzzleVelocity = 50;
	%velInheritFactor = 1;
        %shellcount = 1;
	// Create each projectile and send it on its way
	for(%shell=0; %shell<%shellcount; %shell++)
	{
		// Get the muzzle vector.  This is the dead straight aiming point of the gun
		%vector = %obj.getMuzzleVector(%slot);

		// Get our players velocity.  We must ensure that the players velocity is added
		//   onto the projectile
		%objectVelocity = %obj.getVelocity();

		// Determine scaled projectile vector.  This is still in a straight line as
		//   per the default example
		%vector1 = VectorScale(%vector, %muzzleVelocity);
		%vector2 = VectorScale(%objectVelocity, %velInheritFactor);
		%velocity = VectorAdd(%vector1,%vector2);

		// Determine our random x, y and z points in our spread circle and create
		//   a spread matrix.
		%x = (getRandom() - 0.5) * 2 * 3.1415926 * %spread;
		%y = (getRandom() - 0.5) * 2 * 3.1415926 * %spread;
		%z = (getRandom() - 0.5) * 2 * 3.1415926 * %spread;
		%mat = MatrixCreateFromEuler(%x @ " " @ %y @ " " @ %z);

		// Alter our projectile vector with our spread matrix
		%velocity = MatrixMulVector(%mat, %velocity);

		// Create our projectile
		%p = new (%this.projectileType)()
		{
			dataBlock        = %projectile;
			initialVelocity  = %velocity;
			initialPosition  = %obj.getMuzzlePoint(%slot);
			sourceObject     = %obj;
			sourceSlot       = %slot;
			client           = %obj.client;
		};
		MissionCleanup.add(%p);
	%p.directDamage = 13;
	%p.radiusDamage = 0;
	%p.damageRadius = 0;
	%p.damagetype = '%2 beat the stuffing out of %1';
	%p.impulse = 0;
        onAddProjectile(%projectile, %p, %this);
      }

      return %p;
   }

package Flashlight_Use {
function serverCmdMountVehicle(%client) {
Parent::serverCmdMountVehicle(%client);
if(%client.player.getmountedimage(0) == nametoID(FlashlightImage) && !%client.usedUse) {
  %client.usedUse = 1;
  %client.schedule(50,resetUsedUse);
  %client.player.setImageAmmo(0, !%client.player.getImageAmmo(0));
  return;
}
}
};
activatepackage(Flashlight_Use);

package Weapon_Flashlight {
function onAddProjectile(%projectile, %p, %image) {
if(%image == nameToID(flashLightImage) && %projectile $= swordProjectile) {
  %p.isBlunt = 1;
}
Parent::onAddProjectile(%projectile, %p, %image);
}
};
activatepackage(Weapon_Flashlight);