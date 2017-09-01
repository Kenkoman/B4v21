//moved the saw explosion to lasergun.cs so it could be used by the phaser

datablock ProjectileData(pickaxeProjectile)
{
   //projectileShapeName = "~/data/shapes/arrow.dts";
   explosion           = sawExplosion;
   //particleEmitter     = as;
   muzzleVelocity      = 60;

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


datablock ItemData(pickaxe)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/weapons/pickaxe.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'pickaxe';
	invName = "Pick Axe";
	image = pickaxeImage;
	threatlevel = "Normal";
};

addWeapon(pickaxe);

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(pickaxeImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/weapons/pickaxe.dts";
   skinnable = 1; //This indicates that if the weapon pickup is skinned the image will be skinned too.
   emap = true;
    
   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 -0.1";
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
   item = pickaxe;
   ammo = " ";
   projectile = pickaxeProjectile;
   projectileType = Projectile;

   directDamage        = 30;
   radiusDamage        = 0;
   damageRadius        = 0.5;
   damagetype          = '%1 shouldn\'t have messed with %2\'s level 34 druid mining skills';
   muzzleVelocity      = 60;
   velInheritFactor    = 1;

   deathAnimationClass = "melee";
   deathAnimation = "death5";
   deathAnimationPercent = 0.4;

   //melee particles shoot from eye node for consistancy
   melee = true;
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
	stateSound[0]					= swordDrawSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "PreFire";
	stateAllowImageChange[1]         = true;

	stateName[2]			= "PreFire";
	stateScript[2]                  = "onPreFire";
	stateAllowImageChange[2]        = false;
	stateTimeoutValue[2]            = 0.1;
	stateTransitionOnTimeout[2]     = "Fire";

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "CheckFire";
	stateTimeoutValue[3]            = 0.2;
	stateFire[3]                    = true;
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	stateScript[3]                  = "onFire";
	stateWaitForTimeout[3]		= true;
	//stateTransitionOnTriggerUp[3]	= "StopFire";

	stateName[4]			= "CheckFire";
	stateTransitionOnTriggerUp[4]	= "StopFire";
	stateTransitionOnTriggerDown[4]	= "Fire";

	
	stateName[5]                    = "StopFire";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 0.2;
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;
	stateSequence[5]                = "StopFire";
	stateScript[5]                  = "onStopFire";


};

function pickaxeImage::onMount(%this, %obj)
{
	if(%obj.client.player == %obj)
	{
		%obj.client.spewCashPBuff += 10;
		if((%s = %obj.weaponSkin[%this.item.getID()]) $= "metal3" || %s $= "lightorangebrown" || %s $= "mediumyelloworange" || %s $= "fireyellow")
		{
			%obj.goldPickaxe = 1; //Because onUnMount won't have access to %obj.weaponSkin if the weapon is dropped
			%obj.client.spewCashPBuff += 30;
		}
	}
	return Parent::onMount(%this, %obj);
}

function pickaxeImage::onUnMount(%this, %obj)
{
	if(%obj.client.player == %obj)
	{
		%obj.client.spewCashPBuff -= 10;
		if(%obj.goldPickaxe)
			%obj.client.spewCashPBuff -= 30;
	}
	%obj.goldPickaxe = 0;
	return Parent::onUnMount(%this, %obj);
}

function pickaxeImage::onPreFire(%this, %obj, %slot)
{
	//messageAll( 'MsgClient', 'katana prefired!!!');
	//Parent::onFire(%this, %obj, %slot);
	%obj.playthread(2, armattack);
}

function pickaxeImage::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
	//messageAll( 'MsgClient', 'stopfire');
}

function pickaxeImage::onFire(%this, %obj, %slot)
{
   	%this.projectile.hasLight    = false;
	%p = Parent::onFire(%this, %obj, %slot);
	return %p;
}

function pickaxeProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal) {
	tbmcollison(%this, %obj, %col, %fade, %pos, %normal);
}