//weapon stuff


datablock ProjectileData(ThrowingSabreProjectile)
{
   projectileShapeName = "~/data/shapes/weapons/ls.dts";
   explosion           = batswordExplosion;
   particleEmitter     = " ";
   muzzleVelocity      = 100;
   //rotation        = 90 0 0 0

   armingDelay         = 0;
   lifetime            = 16000;
   fadeDelay           = 15500;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 0.1;


   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "0.0 1.0 0.0";
};
	


//////////
// item //
//////////
datablock ItemData(ThrowingSabre)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system
        skinname = 'green';

	 // Basic Item Properties
	shapeFile = "~/data/shapes/weapons/ls.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a Throwing sabre.';
	invName = "Throwing Sabre";
	image = ThrowingSabreImage;
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(ThrowingSabreImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/weapons/ls.dts";
   emap = true;
   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   skinname = 'green';
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
   item = ThrowingSabre;
   ammo = " ";
   projectile = ThrowingSabreProjectile;
   projectileType = Projectile;

   directDamage        = 0;
   radiusDamage        = 200;
   damageRadius        = 1;
   damagetype          = '%1 got force owned by %2';
   muzzleVelocity      = 100;
   velInheritFactor    = 1;

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
	stateSound[0]					= lsabreDrawSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateAllowImageChange[1]         = true;

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "Reload";
	stateTimeoutValue[2]            = 0.05;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]		= true;
	stateSound[2]			= lsabreHitSound;

	stateName[3]			= "Reload";
	stateSequence[3]                = "Reload";
	stateAllowImageChange[3]        = false;
	stateTimeoutValue[3]            = 0.8;
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

function ThrowingSabreImage::onPreFire(%this, %obj, %slot)
{
	//messageAll( 'MsgClient', 'lsabre prefired!!!');
	//Parent::onFire(%this, %obj, %slot);
	%obj.playthread(2, armattack);
}

function ThrowingSabreImage::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
	//messageAll( 'MsgClient', 'stopfire');
}

function ThrowingSabreProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	tbmcollison(%this,%obj,%col,%fade,%pos,%normal);
       tbmradiusDamage
     (%obj, VectorAdd(%pos, VectorScale(%normal, 0.01)),
      %obj.damageRadius,%obj.radiusDamage,%obj.damageType,20);
}
