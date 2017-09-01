//////////
// item //
//////////
datablock ItemData(lsabre)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system //so this is the little bastard responsible for the lightsaber inventory glitch -DShiznit

	 // Basic Item Properties
	shapeFile = "~/data/shapes/weapons/lightsaber/ls2.dts";
	skinName = 'green';
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = false;

	 // Dynamic properties defined by the scripts
	pickUpName = 'Lightsabre';
	invName = "Lightsaber";
	spawnName = "Lightsaber";
	image = lsabreImage;
	threatlevel = "Normal";
};

addWeapon(lsabre);

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(lsabreImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/weapons/lightsaber/ls2.dts";
   skinnable = 1; //This indicates that if the weapon pickup is skinned the image will be skinned too.
   emap = false;
    
   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
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
   item = lsabre;
   ammo = " ";
   projectile = swordProjectile;
   projectileType = Projectile;

   directDamage        = 40;
   radiusDamage        = 0;
   damageRadius        = 0.5;
   damagetype          = '%2 let the Force flow through %1';
   muzzleVelocity      = 50;
   velInheritFactor    = 1;

   deathAnimationClass = "melee";
   deathAnimation = "death5";
   deathAnimationPercent = 0.4;

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
	stateTimeoutValue[0]             = 0.5;
	stateTransitionOnTimeout[0]       = "Ready";
	stateSound[0]					= lsabreDrawSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "PreFire";
	stateAllowImageChange[1]         = true;

	stateName[2]			= "PreFire";
	stateScript[2]                  = "onPreFire";
	stateAllowImageChange[2]        = false;
	stateTimeoutValue[2]            = 0.0999;
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
	stateTimeoutValue[4]            = 0.0001;
	stateScript[4]                  = "onStopFire";
	stateTransitionOnTimeout[4]     = "StopFire";
	
	stateName[5]                    = "StopFire";
	stateTransitionOnTriggerUp[5]     = "Ready";
	stateAllowImageChange[5]        = false;
	stateSequence[5]                = "StopFire";
	stateScript[5]                  = "onStopFire";


};

function lsabre::onPickup(%this, %obj, %user, %amount)
{
	if(!%user.client.edit) {
		%s = 3 * vectorLen(%obj.getVelocity());
		if(getRandom(2, 300) < %s) {
			%user.damage("Message", %obj.getWorldBoxCenter(), getRandom(2 * %s, 120), '%1 couldn\'t catch the lightsaber.');
			return;
		}
		%s = vectorLen(%user.getVelocity()) - 15;
		if(getRandom(1, 450) < %s) {
			%user.damage("Message", %obj.getWorldBoxCenter(), getRandom(3 * %s, 120), '%1 impaled himself on a lightsaber.');
			return;
		}
	}
	Parent::onPickup(%this, %obj, %user, %amount);
}

function lsabreImage::onMount(%this, %obj)
{
	if(strStr(%obj.weaponSkin[%this.item.getID()], "green") != -1)
		%obj.playThread(1, armReadyRight);
}

function lsabreImage::onPreFire(%this, %obj, %slot)
{
	//messageAll( 'MsgClient', 'lsabre prefired!!!');
	//Parent::onFire(%this, %obj, %slot);
	%obj.playthread(2, armattack);
	%obj.parrying = 1;
}

function lsabreImage::onFire(%this, %obj, %slot)
{
   	%this.projectile.armingDelay         = 0;
   	%this.projectile.lifetime            = 100;
   	%this.projectile.fadeDelay           = 70;
   	%this.projectile.bounceElasticity    = 0;
   	%this.projectile.bounceFriction      = 0;
   	%this.projectile.isBallistic         = false;
   	%this.projectile.gravityMod = 0.0;

   	%this.projectile.lightColor  = strStr(%obj.weaponSkin[%this.item.getID()], "red") != -1 ? "1 0 0" : (
		strStr(%obj.weaponSkin[%this.item.getID()], "green") != -1 ? "0 1 0" : (
		strStr(%obj.weaponSkin[%this.item.getID()], "blue") != -1 ? "0 0 1" : (
		strStr(%obj.weaponSkin[%this.item.getID()], "yellow") != -1 ? "1 1 0.4" : (
		strStr(%obj.weaponSkin[%this.item.getID()], "orange") != -1 ? "1 0.66 0" : (
		strStr(%obj.weaponSkin[%this.item.getID()], "purple") != -1 ? "0.9 0.1 0.9" : (
		strStr(%obj.weaponSkin[%this.item.getID()], "lava") != -1 ? "1 0.33 0" : (
		strStr(%obj.weaponSkin[%this.item.getID()], "water") != -1 ? "0 0 1" : (
		"0 0 0"))))))));

   	%this.projectile.hasLight    = (%this.projectile.lightColor !$= "0 0 0");
   	%this.projectile.lightRadius = 3.0;

	%p = Parent::onFire(%this, %obj, %slot);
	for(%i = 0; %i < getWordCount(%p); %i++)
		getWord(%p, %i).isLsabre = 1;
	return %p;
}

function lsabreImage::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
	//messageAll( 'MsgClient', 'stopfire');
	%obj.parrying = 0;
}

//////////
//dualls//
//////////
datablock ItemData(duallsabre)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/weapons/lightsaber/dualls2.dts";
	skinName = 'red';
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = false;


	 // Dynamic properties defined by the scripts
	pickUpName = 'Dual Lightsabre';
	invName = "Dual LightSabre";
	spawnName = "Lightsaber - Dual";
	image = duallsabreImage;
	threatlevel = "Normal";
};

addWeapon(duallsabre);

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(duallsabreImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/weapons/lightsaber/dualls2.dts";
   skinnable = 1; //This indicates that if the weapon pickup is skinned the image will be skinned too.
   emap = false;
    
   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   //eyeOffset = "0.1 0.2 -0.55";
   scale = "1.75 1.75 1.75";

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = false;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = duallsabre;
   ammo = " ";
   projectile = swordProjectile;
   projectileType = Projectile;

   directDamage        = 50;
   radiusDamage        = 0;
   damageRadius        = 0.5;
   damagetype          = '%2 showed %1 the power of the dark side, qui-gon style';
   muzzleVelocity      = 50;
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
	stateSound[0]					= lsabreDrawSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "PreFire";
	stateAllowImageChange[1]         = true;

	stateName[2]			= "PreFire";
	stateScript[2]                  = "onPreFire";
	stateAllowImageChange[2]        = false;
	stateTimeoutValue[2]            = 0.3;
	stateTransitionOnTimeout[2]     = "Fire";

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "CheckFire";
	stateTimeoutValue[3]            = 0.0001;
	stateFire[3]                    = true;
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	stateScript[3]                  = "onFire";
	stateWaitForTimeout[3]		= true;
	//stateTransitionOnTriggerUp[3]	= "StopFire";

	stateName[4]			= "CheckFire";
	stateTransitionOnTimeout[4]	= "Acheles";
	stateTimeoutValue[4]            = 0.2;
	stateScript[4]                  = "onCharge";

	stateName[5]			= "Acheles"; //The Acheles Heal of the Powerful Dual Saber -DShiznit
	stateTransitionOnTimeout[5]	= "StopFire";
	stateTimeoutValue[5]            = 0.0999;
	stateScript[5]                  = "onAcheles";
	
	stateName[6]                    = "StopFire";
	stateTransitionOnTriggerUp[6]     = "Ready";
	stateAllowImageChange[6]        = false;
	stateSequence[6]                = "StopFire";
	stateScript[6]                  = "onStopFire";


};

function duallsabre::onPickup(%this, %obj, %user, %amount)
{
	if(!%user.client.edit) {
		%s = 5 * vectorLen(%obj.getVelocity());
		if(getRandom(2, 300) < %s) {
			%user.damage("Message", %obj.getWorldBoxCenter(), getRandom(2 * %s, 200), '%1 couldn\'t catch the lightsaber.');
			return;
		}
		%s = vectorLen(%user.getVelocity()) - 15;
		if(getRandom(1, 450) < %s) {
			%user.damage("Message", %obj.getWorldBoxCenter(), getRandom(3 * %s, 120), '%1 impaled himself on a lightsaber.');
			return;
		}
	}
	Parent::onPickup(%this, %obj, %user, %amount);
}

function duallsabreImage::onPreFire(%this, %obj, %slot)
{
	%obj.playthread(2, spearready);
	%obj.parrying = 1;
	duallsabreImage::onFire(%this, %obj, %slot);
}

function duallsabreImage::onCharge(%this, %obj, %slot)
{
	%obj.playthread(2, spearthrow);
}

function duallsabreImage::onFire(%this, %obj, %slot)
{
   	%this.projectile.armingDelay         = 0;
   	%this.projectile.lifetime            = 100;
   	%this.projectile.fadeDelay           = 70;
   	%this.projectile.bounceElasticity    = 0;
   	%this.projectile.bounceFriction      = 0;
   	%this.projectile.isBallistic         = false;
   	%this.projectile.gravityMod = 0.0;

   	%this.projectile.lightColor  = strStr(%obj.weaponSkin[%this.item.getID()], "red") != -1 ? "1 0 0" : (
		strStr(%obj.weaponSkin[%this.item.getID()], "green") != -1 ? "0 1 0" : (
		strStr(%obj.weaponSkin[%this.item.getID()], "blue") != -1 ? "0 0 1" : (
		strStr(%obj.weaponSkin[%this.item.getID()], "yellow") != -1 ? "1 1 0.4" : (
		strStr(%obj.weaponSkin[%this.item.getID()], "orange") != -1 ? "1 0.66 0" : (
		strStr(%obj.weaponSkin[%this.item.getID()], "purple") != -1 ? "0.9 0.1 0.9" : (
		strStr(%obj.weaponSkin[%this.item.getID()], "lava") != -1 ? "1 0.33 0" : (
		strStr(%obj.weaponSkin[%this.item.getID()], "water") != -1 ? "0 0 1" : (
		"0 0 0"))))))));

   	%this.projectile.hasLight    = (%this.projectile.lightColor !$= "0 0 0");
   	%this.projectile.lightRadius = 3.0;

	%p = Parent::onFire(%this, %obj, %slot);
	for(%i = 0; %i < getWordCount(%p); %i++)
		getWord(%p, %i).isLsabre = 1;
	return %p;
}

function duallsabreImage::onAcheles(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
	%obj.parrying = 0;
}