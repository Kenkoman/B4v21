//KittyCarbine.cs
//Kitty Carbine - MEOWMEOWMEOW

//////////
// item //
//////////
datablock ItemData(KittyAR)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "dtb/data/shapes/weapons/KittyAR2.dts";
	skinName = 'base';
	rotate = true;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a Hello Kitty Assault Rifle';
	invName = "Hello Kitty AR";
	image = KittyARImage;
	threatlevel = "Normal";
};

addWeapon(KittyAR);

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(KittyARImage)
{
   // Basic Item properties
   shapeFile = "dtb/data/shapes/weapons/KittyAR2.dts";
   skinName = 'base';
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
   item = KittyAR;
   ammo = "";
   projectile = hackProjectile;
   projectileType = Projectile;

   projectileSpread = 7/1000;
   projectileSpreadWalking = 10.5/1000;
   projectileSpreadMax = 14/1000;
   recoil = 1.03; //Slightly more than the AR
   recoilSeconds = 2;

   directDamage        = 12;
   radiusDamage        = 0;
   damageRadius        = 1;
   damagetype          = '%1 was crocheted into pretty lead frill by %2';
   shellCount          = 1;
   muzzleVelocity      = 4600;
   velInheritFactor    = 1;

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
	stateScript[1]                  = "onPreFire";
	stateAllowImageChange[1]         = true;

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "Reload";
	stateTimeoutValue[2]            = 0.001;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]			= true;
	stateSound[2]					= BrifleFireSound;

	stateName[3]			= "Reload";
	stateSequence[3]                = "Reload";
	stateAllowImageChange[3]        = false;
	stateTimeoutValue[3]            = 0.06;
	stateScript[3]                  = "onPreFire";
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTimeout[3]     = "Ready";




};

function KittyARImage::onPreFire(%this, %obj, %slot)
{	
	//Reset so it can fire consistantly -DShiznit
	%obj.playthread(2, root);
}

function KittyARImage::onFire(%this, %obj, %slot)
{
	%obj.playthread(2, jump);
	%obj.KARShots++;
	if(%obj.KARShots < 8)
	{
		%projectile = %this.projectile;
	   	%projectile.scale               = ".5 1.5 .5";
   		%projectile.armingDelay         = 0;
	   	%projectile.lifetime            = 8000;
   		%projectile.fadeDelay           = 7500;
	   	%projectile.bounceElasticity    = 0;
   		%projectile.bounceFriction      = 0;
	   	%projectile.isBallistic         = true;
   		%projectile.gravityMod = 0;

		// Get the weapons projectile spread and ensure it is never 0
		//   (we need some spread direction even if it is extremely tiny)
		%spread = calculateSpread(%this, %obj);

		// Create each projectile and send it on its way
		for(%shell = 0; %shell < %this.shellcount; %shell++)
		{
			// Get the muzzle vector.  This is the dead straight aiming point of the gun
			%vector = %obj.getMuzzleVector(%slot);

			// Get our players velocity.  We must ensure that the players velocity is added
			//   onto the projectile
			%objectVelocity = %obj.getVelocity();

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
				initialPosition  = %obj.getMuzzlePoint(%slot);
				sourceObject     = %obj;
				sourceSlot       = %slot;
				client           = %obj.client;
			};
			MissionCleanup.add(%p);
			%p.directDamage = %this.directDamage;
			%p.radiusDamage = %this.radiusDamage;
			%p.damageRadius = %this.damageRadius;
			%p.damagetype   = %this.damagetype;
			%p.impulse      = %this.impulse;
			onAddProjectile(%projectile, %p, %this);
			if(%this.recoil)
				%obj.modSpread(%this.recoil, %this.recoilSeconds);
		}//EO SHELL
	}
	else
	{
		// Determine scaled projectile vector.  This is still in a straight line as
		//   per the default example
		%vector1 = VectorScale(%obj.getMuzzleVector(%slot), 80);
		%velocity2 = VectorAdd(%vector1,%obj.getVelocity());
		ServerPlay3D(GLFireSound, %obj.getTransform());
		// Create our projectile
		%p = new (%this.projectileType)()
		{
			dataBlock        = GLprojectile;
			initialVelocity  = %velocity2;
			initialPosition  = %obj.getMuzzlePoint(%slot);
			sourceObject     = %obj;
			sourceSlot       = %slot;
			client           = %obj.client;
		};
		MissionCleanup.add(%p);
		%p.directDamage = 20;
		%p.radiusDamage = 20;
		%p.damageRadius = 3;
		%p.damagetype   = %this.damagetype;
		%p.impulse      = 20;
		onAddProjectile(%projectile, %p, %this);
		if(%this.recoil)
			%obj.modSpread(%this.recoil, %this.recoilSeconds);
		%obj.KARShots = 0;
	}
	muzzleflash(%this, %obj, %slot, 0.75, 2, 0.75);
	return %p;
}