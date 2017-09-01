//Molotov Cocktails
//By DShiznit Bitch.

//projectile
datablock ProjectileData(MolotovProjectile)
{
   projectileShapeName = "dtb/data/shapes/weapons/molotovpro.dts";
   explosion           = swordExplosion;
   particleEmitter     = Flame3Emitter;
   muzzleVelocity      = 20;

   armingDelay         = 0;
   lifetime		= 80000;
   fadeDelay           = 75000;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 1;

   hasLight    = false;
};


//////////
// item //
//////////
datablock ItemData(Molotov)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "dtb/data/shapes/weapons/molotov.dts";
	skinName = 'green';
	rotate = true;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'some Molotovs.';
	invName = "Molotov Cocktails";
	image = MolotovImage;
	threatlevel = "Normal";
};

addWeapon(Molotov);

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(MolotovImage)
{
   // Basic Item properties
   shapeFile = "dtb/data/shapes/weapons/molotov.dts";
   skinName = 'green';
   emap = true;
 
   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
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
   item = Molotov;
   ammo = " ";
   projectile = MolotovProjectile;
   projectileType = Projectile;

   directDamage        = 200;
   radiusDamage        = 200;
   damageRadius        = 6;
   damagetype          = '%1 was incinerated by %2';
   muzzleVelocity      = 20;
   velInheritFactor    = 0;

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
	stateName[0]			= "Activate";
	stateTimeoutValue[0]		= 0.5;
	stateTransitionOnTimeout[0]	= "Ready";
	stateSequence[0]		= "ready";
	stateSound[0]					= weaponSwitchSound;

	stateName[1]			= "Ready";
	stateTransitionOnTriggerDown[1]	= "Charge";
	stateAllowImageChange[1]	= true;
	
	stateName[2]		        = "Charge";
	stateTransitionOnTimeout[2]	= "Armed";
	stateTimeoutValue[2]		= 0.3;
	stateWaitForTimeout[2]		= false;
	stateTransitionOnTriggerUp[2]	= "AbortCharge";
	stateScript[2]		      = "onCharge";
	stateAllowImageChange[2]        = false;
	
	stateName[3]			= "AbortCharge";
	stateTransitionOnTimeout[3]	= "Ready";
	stateTimeoutValue[3]		= 0.5;
	stateWaitForTimeout[3]		= true;
	stateScript[3]			= "onAbortCharge";
	stateAllowImageChange[3]	= false;

	stateName[4]			= "Armed";
	stateTransitionOnTriggerUp[4]	= "Fire";
	stateAllowImageChange[4]	= false;

	stateName[5]			= "Fire";
	stateTransitionOnTimeout[5]	= "Ready";
	stateTimeoutValue[5]		= 0.5;
	stateFire[5]			= true;
	stateSequence[5]		= "fire";
	stateScript[5]			= "onFire";
	stateWaitForTimeout[5]		= true;
	stateAllowImageChange[5]	= false;
	stateSound[5]				= spearFireSound;
};

function MolotovImage::onCharge(%this, %obj, %slot)
{
	//messageAll( 'MsgClient', 'sword prefired!!!');
	//Parent::onFire(%this, %obj, %slot);
	%obj.playthread(2, spearready);
}

function MolotovImage::onAbortCharge(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
	//messageAll( 'MsgClient', 'stopfire');
}

function MolotovImage::onFire(%this, %obj, %slot)
{
	%obj.playthread(2, spearThrow);
	Parent::onFire(%this, %obj, %slot);
}

function MolotovProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	serverPlay3D(FlamethrowerFireSound, %pos);
	%a = new (projectile)()
	{
		dataBlock        = flame2projectile;
		initialVelocity  = getrandom(-5,5)*5@" "@getrandom(-5,5)*5@" 0.1";
		initialPosition  = %pos;
	};
	MissionCleanup.add(%a);   //I tried using a for loop for this, it didn't work, so bite me.
	for(%i = 0; %i < 9; %i++) //Oh christ Shiznit just do this
	{
		%a = new (projectile)()
		{
			dataBlock        = flame2projectile;
			initialVelocity  = getRandom(-25, 25) SPC getRandom(-25, 25) SPC 0.1;
			initialPosition  = %pos;
	         };
        	 MissionCleanup.add(%a);
	}
	tbmcollison(%this, %obj, %col, %fade, %pos, %normal);
       tbmradiusDamage
     (%obj, VectorAdd(%pos, VectorScale(%normal, 0.01)),
      %obj.damageRadius, %obj.radiusDamage, %obj.damageType, 20);
}