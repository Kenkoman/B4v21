datablock ParticleData(HealMistParticle)
{
    dragCoefficient = 0;
    gravityCoefficient = 0;
    windCoefficient = 0;
    inheritedVelFactor = 0;
    constantAcceleration = 0;
    lifetimeMS = 200;
    lifetimeVarianceMS = 100;
    useInvAlpha = false;
    spinRandomMin = 0;
    spinRandomMax = 0;
    textureName = "~/data/particles/cloud";
    spinSpeed = 0;
    times[0] = 0;
    times[1] = 0.7;
    times[2] = 1;
    colors[0] = "0.1 0.2 0.3 0.5"; 
    colors[1] = "0.3 0.4 0.5 0.25"; 
    colors[2] = "0.5 0.6 0.7 0."; 
    sizes[0] = 1.0451;
    sizes[1] = 1.2451;
    sizes[2] = 1.4451;
};

datablock ParticleEmitterData(HealMistEmitter)
{
    ejectionPeriodMS = 20;
    periodVarianceMS = 2;
    ejectionVelocity = 0;
    velocityVariance = 0;
    ejectionOffset = 0.3;
    thetaMin = 0;
    thetaMax = 90;
    phiReferenceVel = 36;
    phiVariance = 36;
    orientParticles= 0;
    orientOnVelocity = 1;
    particles = "HealMistParticle";
};

datablock ParticleData(HealingTrail) 
{ 
textureName = "tbm/data/particles/waterfall"; 
dragCoeffiecient = 0.0; 
gravityCoefficient = 1; 
inheritedVelFactor = 0.00; 
lifetimeMS = 500; 
lifetimeVarianceMS = 300; 
useInvAlpha = false; 
spinRandomMin = -10.0; 
spinRandomMax = 10.0; 

colors[0] = "0.1 0.2 0.3 0.4"; 
colors[1] = "0.3 0.4 0.5 0.6"; 
colors[2] = "0.5 0.6 0.7 0.8"; 

sizes[0] = 1.5; 
sizes[1] = 2.85; 
sizes[2] = 4.2; 

times[0] = 0.4; 
times[1] = 2; 
times[2] = 4; 
}; 

datablock ParticleEmitterData(HealingTrailEmitter) 
{ 
ejectionPeriodMS = 50; 
periodVarianceMS = 5; 

ejectionVelocity = 1; 
velocityVariance = 0.50; 

thetaMin = 0; 
thetaMax = 90; 
phiReferenceVel = 3;
phiVariance = 3;
particles = HealingTrail; 
}; 

datablock ExplosionData(HealingExplosion)
{
   //explosionShape = "";
	soundProfile = sprayHitSound;

   lifeTimeMS = 500;

   particleEmitter = HealMistEmitter;
	particleDensity = 10;
	particleRadius  = 4;

   emitter[0] = healMistEmitter;
   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = false;
   camShakeFreq = "10.0 11.0 10.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 1;
   camShakeRadius = 8;

   // Dynamic light
   lightStartRadius = 0;
   lightEndRadius = 0;
   lightStartColor = "1 1 1";
   lightEndColor = "1 1 1";
};


//projectile
datablock ProjectileData(HealingProjectile)
{
   projectileShapeName = "";
   explosion           = HealingExplosion;
   particleEmitter     = HealingTrailEmitter;
   muzzleVelocity      = 40;

   dragCoeffiecient = 0.0;
   repairAmount = 3200;

   armingDelay         = 0;
   lifetime            = 10000;
   fadeDelay           = 10000;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 1;

   hasLight    = false;
   lightRadius = 10.0;
   lightColor  = "1 1 1 1";
};


//////////
// item //
//////////
datablock ItemData(HealingFountain)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "dtb/data/shapes/weapons/loudhailer.dts";
	skinName = 'doveblue';
	rotate = true;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'A Healing Fire Hose';
	invName = "Fire Hose";
	image = HealingImage;
	threatlevel = "Safe";
};

addWeapon(HealingFountain);

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(HealingImage)
{
   // Basic Item properties
   shapeFile = "dtb/data/shapes/weapons/loudhailer.dts";
   skinName = 'base';
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
   item = God;
   ammo = "";
   projectile = HealingProjectile;
   projectileType = Projectile;

   projectileSpread = 30/1000;
   projectileSpreadWalking = 45/1000;
   projectileSpreadMax = 60/1000;

   directDamage        = 0;
   radiusDamage        = 0;
   damageRadius        = 4;
   damagetype          = 'water is not supposed to hurt smartass';
   shellCount          = 4;
   muzzleVelocity      = 40;
   velInheritFactor    = 0;

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

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "Reload";
	stateTimeoutValue[2]            = 0.008;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]			= true;
	stateSound[2]					= SprayFireSound;

	stateName[3]			= "Reload";
	stateSequence[3]                = "Reload";
	stateAllowImageChange[3]        = false;
	stateTimeoutValue[3]            = 0.001;
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTimeout[3]     = "Check";

	stateName[4]			= "Check";
	stateTransitionOnTriggerUp[4]	= "StopFire";
	stateTransitionOnTriggerDown[4]	= "Fire";

	stateName[5]                    = "StopFire";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 0.001;
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;
	//stateSequence[5]                = "Reload";
	stateScript[5]                  = "onStopFire";


};

function HealingProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal){
       tbmradiusDamage
     (%obj, VectorAdd(%pos, VectorScale(%normal, 0.01)),
      %obj.damageRadius,%obj.radiusDamage,%obj.damageType,20,2);
	if(%col.getType() == 67108869)
		return;
	if(%col.getMountedImage(0) == nameToID(flameSmallImage) || %col.getMountedImage(0) == nameToID(flameImage) || %col.getMountedImage(0) == nameToID(flameBigImage) || %col.getMountedImage(0) == nameToID(smokeImage) || %col.getMountedImage(0) == nameToID(smokeBigImage))
		%col.unmountImage(0);
	if(%col.getClassName() $= "Player" || %col.getClassName() $= "AIPlayer") {
		%col.extinguish();
		if (%col.getDamageLevel() != 0 && %col.getState() !$= "Dead")
			%col.applyRepair(%this.repairAmount);
	}
}