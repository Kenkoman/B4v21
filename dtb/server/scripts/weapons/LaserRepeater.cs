//projectile
datablock ProjectileData(LaserRepeaterProjectile)
{
   projectileShapeName = "~/data/shapes/weapons/laserblast.dts";
   explosion           = bulletExplosion;
//   particleEmitter     = bulletTrailEmitter;
   muzzleVelocity      = 200;

   armingDelay         = 0;
   lifetime            = 80000;
   fadeDelay           = 75000;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 0;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "0.7 0 0";
};


//////////
// item //
//////////
datablock ItemData(LaserRepeater)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/weapons/lasergun.dts";
	skinName = 'black';
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a LaserRepeater.';
	invName = "LaserRepeater";
	spawnName = "Laser Repeater";
	image = LaserRepeaterImage;
	threatlevel = "Normal";
};

addWeapon(LaserRepeater);

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(LaserRepeaterImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/weapons/lasergun.dts";
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
   item = LaserRepeater;
   ammo = " ";
   projectile = LaserRepeaterProjectile;
   projectileType = Projectile;

   projectileSpread = 12/1000;
   projectileSpreadWalking = 18/1000;
   projectileSpreadMax = 24/1000;
   recoil = 1.1;
   recoilSeconds = 1.5;

   directDamage        = 40;
   radiusDamage        = 10;
   damageRadius        = 0.5;
   damagetype          = '%1 got blasted repeatedly by %2';
   muzzleVelocity      = 200;
   velInheritFactor    = 0;

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
	stateTransitionOnTimeout[0]       = "Ready";
	stateSound[0]					= weaponSwitchSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateAllowImageChange[1]         = true;
	stateScript[1]                  = "onStopFire";

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "Reload";
	stateTimeoutValue[2]            = 0.05;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]			= true;
	stateSound[2]					= LaserRepeaterFireSound;

	stateName[3]			= "Reload";
	stateSequence[3]                = "Reload";
	stateAllowImageChange[3]        = false;
	stateTimeoutValue[3]            = 0.04;
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTimeout[3]     = "Check";
	stateScript[3]                  = "onPreFire";

	stateName[4]			= "Check";
	stateTransitionOnTriggerUp[4]	= "StopFire";
	stateTransitionOnTriggerDown[4]	= "StopFire";

	stateName[5]                    = "StopFire";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 0.0001;
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;
	//stateSequence[5]                = "Reload";
	stateScript[5]                  = "onStopFire";


};
function LaserRepeaterImage::onPreFire(%this, %obj, %slot)
{	
	%obj.playthread(2, jump);

}

function LaserRepeaterImage::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}

function LaserRepeaterProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	tbmcollison(%this,%obj,%col,%fade,%pos,%normal);
}

//////////////////////////////////////////////////////
//Deflection code by DShiz -Uses Shotgun spread code//
//////////////////////////////////////////////////////
function LaserRepeaterProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal) {
	if (%col.DMShield == 1) {						//If the victim is invincible or parrying,
	      // Get the type of projectile we are gonna fire		//recreate the projectile fired.
      %projectile = LaserRepeaterProjectile;

      // Get the weapons projectile spread and ensure it is never 0
      //   (we need some spread direction even if it is extremely tiny)
      %spread = 0.1;

        %shellcount = 1;

      // Create each projectile and send it on its way
      for(%shell=0; %shell<%shellcount; %shell++)
      {
              // Get the muzzle vector.  This is the dead straight aiming point of the gun
         %vector = %col.getMuzzleVector(%slot);

         // Get our players velocity.  We must ensure that the players velocity is added
         //   onto the projectile
         %objectVelocity = %col.getVelocity();

         // Determine scaled projectile vector.  This is still in a straight line as
         //   per the default example
         %vector1 = VectorScale(%vector, %projectile.muzzleVelocity);
         %vector2 = VectorScale(%objectVelocity, %projectile.velInheritFactor);
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
            initialPosition  = %col.getMuzzlePoint(%slot);
            sourceObject     = %col;
            sourceSlot       = %slot;
            client           = %obj.client;				//This should keep the client the same as who fired the projectile,
         };										//so any code that calls upon the client still works (deathmessages, etc.)
         MissionCleanup.add(%p);
         onAddProjectile(%projectile, %p, %this);
      }

      return %p;
	}
else {
	tbmcollison(%this,%obj,%col,%fade,%pos,%normal);
	}
}
////////////////////////////