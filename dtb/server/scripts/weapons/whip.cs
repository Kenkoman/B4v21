//whip.cs
//projectile
exec('dtb/data/shapes/weapons/whip.cs');

datablock ProjectileData(IndyWhipProjectile)
{
   //projectileShapeName = "~/data/shapes/arrow.dts";
   explosion           = BatswordExplosion;
   //particleEmitter     = as;
   muzzleVelocity      = 200;

   armingDelay         = 0;
   lifetime            = 100;
   fadeDelay           = 70;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0.0;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";
};


//////////
// item //
//////////
datablock ItemData(IndyWhip)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/weapons/whip.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a BullWhip';
	invName = "Whip";
	image = IndyWhipImage;
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(IndyWhipImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/weapons/whip.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   scale = '5 5 5';
   rotation = "-1 0 0 180";
   offset = "0 0 0";
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
   item = Sabre;
   ammo = " ";
   projectile = IndyWhipProjectile;
   projectileType = Projectile;

   directDamage        = 2;
   radiusDamage        = 0;
   damageRadius        = 0.5;
   damagetype          = '%2 whipped %1\'s ass';
   muzzleVelocity      = 200;
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
	stateName[0]                     = "Activate";
	stateSequence[0] = "ignite";
	
	stateTimeoutValue[0]             = 0.5;
	stateTransitionOnTimeout[0]       = "Ready";
	stateSound[0]					= weaponswitchSound;
	
	

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "PreFire";
	stateAllowImageChange[1]         = true;

	stateName[2]			= "PreFire";
	stateScript[2]                  = "onPreFire";
	stateAllowImageChange[2]        = false;
	stateTimeoutValue[2]            = 0.27;
	stateSequence[2]                = "PreFire";
	stateTransitionOnTimeout[2]     = "Fire";
	stateSound[2]					= bowfireSound;

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "CheckFire";
	stateTimeoutValue[3]            = 0.23;
	stateFire[3]                    = true;
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	stateScript[3]                  = "onFire";
	stateWaitForTimeout[3]		= true;
	stateSound[3]					= arrowexplosionSound;	

	stateName[4]			= "CheckFire";
	stateTransitionOnTriggerUp[4]	= "Ready";
	stateScript[4]                  = "onReload";
	stateTransitionOnTriggerDown[4]	= "Fire";

	
};

function IndyWhipImage::onPreFire(%this, %obj, %slot)
{
	%obj.playthread(2, armattack);
}

function IndyWhipImage::onStopReload(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}

function SabreProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	tbmcollison(%this, %obj, %col, %fade, %pos, %normal);
}