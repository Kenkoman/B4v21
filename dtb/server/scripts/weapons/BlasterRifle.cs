//Brifle.cs
//Brifle Stuff - BANBANGBANG

//projectile
datablock ProjectileData(bulletProjectile)
{
   projectileShapeName = "~/data/shapes/weapons/lasergreen.dts";
   explosion           = bulletExplosion;
//   particleEmitter     = bulletTrailEmitter;
   muzzleVelocity      = 200;

   armingDelay         = 0;
   lifetime            = 80000;
   fadeDelay           = 75000;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   scale               = "1 4 1";
   gravityMod = 0;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "0 0.7 0";
};


//////////
// item //
//////////
datablock ItemData(Brifle)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/weapons/rifle.dts";
	skinName = 'black';
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a Blaster Rifle.';
	invName = "Blaster Rifle";
	image = BrifleImage;
	threatlevel = "Normal";
};

addWeapon(Brifle);

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(BrifleImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/weapons/rifle.dts";
   skinnable = 1; //This indicates that if the weapon pickup is skinned the image will be skinned too.
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
   item = Brifle;
   ammo = " ";
   projectile = bulletProjectile;
   projectileType = Projectile;

   projectileSpreadMax = 4/1000;
   projectileSpreadWalking = 6/1000;
   projectileSpreadMax = 8/1000;
   recoil = 1.05;
   recoilSeconds = 2;

   directDamage        = 40;
   radiusDamage        = 0;
   damageRadius        = 0.5;
   damagetype          = '%1 was blasted by %2';
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
	stateSound[2]					= gunpackFireSound;

	stateName[3]			= "Reload";
	stateSequence[3]                = "Reload";
	stateAllowImageChange[3]        = false;
	stateTimeoutValue[3]            = 0.3;
	stateScript[3]                  = "onPre";
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTimeout[3]     = "Check";

	stateName[4]			= "Check";
	stateScript[4]                  = "onStop";
	stateTransitionOnTriggerUp[4]	= "Ready";
	stateAllowImageChange[4]        = true;

};

function BrifleImage::onPre(%this, %obj, %slot)
{	
	%obj.playthread(2, jump);

}

function BrifleImage::onStop(%this, %obj, %slot)
{	
	%obj.playthread(2, root);

}

////////////////////////////
//Deflection code by DShiz//
////////////////////////////
function bulletProjectile::onCollision(%this, %obj, %col, %fade, %pos, %normal) {
	if (%col.DMShield == 1) {						//If the victim is invincible or parrying,
	      // Get the type of projectile we are gonna fire		//recreate the projectile fired.
      %projectile = bulletProjectile;

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