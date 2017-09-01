//--------------------------------------------------------------------------
// Weapon Item.  This is the item that exists in the world, i.e. when it's
// been dropped, thrown or is acting as re-spawnable item.  When the weapon
// is mounted onto a shape, the RifleImage is used.

datablock ItemData(ElectroGun)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/weapons/agc.dts";
	skinName = 'grayDark';
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'an Electric Arc Cannon.';
	invName = "Electro-gun";
	image = ElectrogunImage;
	threatlevel = "Dangerous";
};

addWeapon(ElectroGun);

datablock ParticleData(ElectroGunTrailParticle)
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

	textureName		= "tbm/data/particles/lbolt";

	// Interpolation variables
	colors[0]	= "0.3 0.3 1 1";
	sizes[0]	= 1;
	times[0]	= 0.0;
};

datablock ParticleEmitterData(ElectroGunTrailEmitter)
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
	particles = ElectroGunTrailParticle;
};

datablock ProjectileData(ElectroGunProjectile)
{
   projectileShapeName = "tbm/data/shapes/dummy1.dts";
   explosion           = bullet3Explosion;
   particleEmitter     = ElectroGunTrailEmitter;
   muzzleVelocity      = 600;

   armingDelay         = 0;
   lifetime            = 100;
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

datablock ShapeBaseImageData(ElectroGunImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/weapons/agc.dts";
   skinName = 'brown';
   emap = true;
    
   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   eyeOffset = "0.8 0.4 -0.75";

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = ElectroGun;
   ammo = " ";
   projectile = ElectroGunProjectile;
   projectileType = Projectile;

   projectileSpread = 32/1000;
   projectileSpreadWalking = 48/1000;
   projectileSpreadMax = 64/1000;

   directDamage        = 10;
   radiusDamage        = 0;
   damageRadius        = 1;
   damagetype          = '%1 got fried by %2';
   shellCount          = 5;
   muzzleVelocity      = 600;
   velInheritFactor    = 0;

   deathAnimationClass = "plasma";
   deathAnimation = "death5";
   deathAnimationPercent = 0.4;

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
	stateSound[2]					= lsabreHitSound;
	
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

function ElectroGunProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	tbmcollison(%this, %obj, %col, %fade, %pos, %normal);
       tbmradiusDamage
     (%obj, VectorAdd(%pos, VectorScale(%normal, 0.01)),
      %obj.damageRadius,%obj.radiusDamage,%obj.damageType,1000);
}

//The Electro-Gun used to display really cheap homing abilities.  I assume that this was by design on DShiznit's part.
//I'm just dropping this refined homing code here and waiting to see if anyone wants to hook this back up beause frankly I think it makes this ridiculously overpowered. -Wiggy
function ElectroGunProjectile::homeIn(%this, %obj)
{
	if(!isobject(%obj))
		return;
	%initRot = vectorToEuler(%obj.initialvelocity);
	initContainerRadiusSearch(%obj.getPosition(), 500, $TypeMasks::PlayerObjectType);
	while(%search = containerSearchNext()) {
		if(%obj.client == %search.client)
			continue;
		if(isObject(%search))
			%t = %search;
		break;
	}
	if(isObject(%t)) {
		%pos = vectorAdd(%t.getPosition(), "0 0" SPC (getWord(%t.getScale(), 2) * 1.5) -1);
		%vec = vectorSub(%pos, %obj.getPosition());
		if(vectorLen(%vec) > 15) {
			%vec = vectorScale(vectorNormalize(%vec), vectorLen(%obj.initialVelocity));
			%rotdiff = vectorSub(%initrot, vectorToEuler(%vec));
			if(getWord(%rotDiff, 2) > 180)
				%rotDiff = vectorAdd(%rotDiff, "0 0 -360");
			if(getWord(%rotDiff, 2) < -180)
				%rotDiff = vectorAdd(%rotDiff, "0 0 360");
			if(vectorLen(%rotDiff) > 80) {
				%rotDiff = vectorScale(%rotdiff, 80 / vectorLen(%rotDiff));
				%trim = 1;
			}
			%len = vectorLen(%vec);
			if(%trim)
				%newrot = vectorSub(%initrot, vectorScale(%rotDiff, 0.5));
			else
				%newrot = vectorSub(%initrot, %rotdiff);
			%xs = mSin($pi / 180 * getWord(%newrot, 0)) * %len;
			%xc = mCos($pi / 180 * getWord(%newrot, 0)) * %len; //I forget what the purpose of this was
			%zs = mSin($pi / 180 * getWord(%newrot, 2)) * %len;
			%zc = mCos($pi / 180 * getWord(%newrot, 2)) * %len;
			%vec = %zs SPC %zc SPC %xs;
			%p = new Projectile() {
				initialVelocity		= %vec;
				initialPosition		= %obj.getPosition();
				datablock		= %this;
				sourceObject		= %obj.client.player;
				sourceSlot		= %obj.sourceSlot;
				client			= %obj.client;
				c			= %obj.c + 1;
			};
			%p.directDamage = %obj.directDamage;
			%p.radiusDamage = %obj.radiusDamage;
			%p.damageRadius = %obj.damageRadius;
			%p.damagetype   = %obj.damagetype;
			%p.impulse      = %obj.impulse;
			%obj.delete();
		}
	}
	if(%p.c < 30)
		%p.homeSchedule = %this.schedule(getRandom(300, 400), "homeIn", %p);
}

function electroGunImage::onMount(%this, %obj)
{
	%obj.playThread(1, armreadyright);
	messageAll('MsgClientKilled', '%1 \c2pulled out a \c9HIGH-VOLTAGE-ELECTRO-STATIC-GUN!\c2 Someone\'s compensating...', %obj.client.name);
}

//Does this really need homing?
//package Weapon_Electrogun {
//function onAddProjectile(%projectile, %p, %image) {
//if(%projectile $= ElectroGunProjectile)
//  ElectroGunProjectile.schedule(100, "homeIn", %p);
//Parent::onAddProjectile(%projectile, %p, %image);
//}
//};
//activatepackage(Weapon_Electrogun);