//--------------------------------------------------------------------------
// Weapon Item.  This is the item that exists in the world, i.e. when it's
// been dropped, thrown or is acting as re-spawnable item.  When the weapon
// is mounted onto a shape, the RifleImage is used.

datablock ItemData(Needler)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/needler/needler.dts";
	skinName = 'grayDark';
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a Needle Gun.';
	invName = "Needler";
	image = NeedlerImage;
	texture = "~/data/shapes/needler/needler.png";
};

datablock ParticleData(NeedlerTrailParticle)
{
	dragCoefficient		= 3.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 100;
	lifetimeVarianceMS	= 0;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= false;
	animateTexture		= false;

	textureName		= "dtb/data/shapes/weapons/laser.png";

	// Interpolation variables
	colors[0]	= "0.3 0.3 1 1";
	sizes[0]	= 1;
	times[0]	= 0.0;
};

datablock ParticleEmitterData(NeedlerTrailEmitter)
{
	ejectionPeriodMS = 2;
	periodVarianceMS = 0;
	ejectionVelocity = 0.5;
	velocityVariance = 0.1;
	ejectionOffset   = 0.1;
	thetaMin         = 0.0;
	thetaMax         = 90.0;  
	phiReferenceVel  = 0;
	phiVariance      = 360;
	overrideAdvance = false;
	particles = NeedlerTrailParticle;
};

datablock ProjectileData(NeedlerProjectile)
{
   projectileShapeName = "tbm/data/shapes/dummy1.dts";
   explosion           = bullet3Explosion;
   particleEmitter     = NeedlerTrailEmitter;
   muzzleVelocity      = 600;

   armingDelay         = 100;
   lifetime            = 1000;
   fadeDelay           = 1000;
   bounceElasticity    = 1;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 0;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "0.3 0.3 1";
};

////////////////
//weapon image//
////////////////

datablock ShapeBaseImageData(NeedlerImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/needler/needler.dts";
   skinName = 'brown';
   emap = true;
    
   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   eyeOffset = "0.8 0.2 -0.75";

   projectileSpread = 22/1000;
   projectileSpreadWalking = 33/1000;
   projectileSpreadMax = 44/1000;

   recoil = 1.05;
   recoilSeconds = 1;

   directDamage        = 0;
   radiusDamage        = 100;
   damageRadius        = 1;
   damagetype          = '%1 got fried by %2';
   shellCount          = 5;
   muzzleVelocity      = 600;
   velInheritFactor    = 0;

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = Needler;
   ammo = "";
   projectile = NeedlerProjectile;
   projectileType = Projectile;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = true;
   

   


   // Initial start up state
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.0625;
	stateTransitionOnTimeout[0]      = "Ready";
	stateSound[0]			 = weaponSwitchSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateAllowImageChange[1]         = true;

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "Reload";
	stateTimeoutValue[2]            = 0.0125;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]			= true;
	stateSound[2]					= GLFireSound;
	
	stateName[3]			= "Reload";
	stateSequence[3]                = "Reload";
	stateAllowImageChange[3]        = false;
	stateTimeoutValue[3]            = 0.0625;
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTimeout[3]     = "Check";

	stateName[4]			= "Check";
	stateTransitionOnTriggerUp[4]	= "StopFire";
	stateTransitionOnTriggerDown[4]	= "Fire";

	stateName[5]                    = "StopFire";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 0.0125;
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;
	//stateSequence[5]                = "Reload";
	stateScript[5]                  = "onStopFire";


};

function NeedlerProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	tbmcollison(%this, %obj, %col, %fade, %pos, %normal);
       tbmradiusDamage
     (%obj, VectorAdd(%pos, VectorScale(%normal, 0.01)),
      %obj.damageRadius,%obj.radiusDamage,%obj.damageType,1000);
}

//-----------------------------------------------------------------------------