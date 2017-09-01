//bullet trail
datablock ParticleData(pullerTrailParticle)
{
textureName = "~/data/shapes/weapons/plasma";
	useInvAlpha =  false;

	dragCoeffiecient = 100.0;
	inheritedVelFactor = 0.01;

	lifetimeMS = 1500;
	
	lifetimeVarianceMS = 10;

	times[0] = 0.0;
	times[1] = 0.8;
	times[2] = 1.0;

	colors[0] = "0.6 0.0 1.0 0.8";
	colors[1] = "0.4 0.0 1.0 0.8";
	colors[2] = "0.8 0.0 0.6 0.8";
	sizes[0] = 1.5;
	sizes[1] = 0.5;
	sizes[2] = 0.1;
};

datablock ParticleEmitterData(pullerTrailEmitter)
{
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;

   ejectionVelocity = 0.5;
   velocityVariance = 0.05;

   ejectionOffset = 0;

   thetaMin         = 0.0;
   thetaMax         = 90.0;  


   particles = "pullerTrailParticle";
};



datablock ExplosionData(pullerExplosion)
{
   //explosionShape = "";
   //soundProfile = kickerExplosionSound;

   lifeTimeMS = 150;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = false;
   camShakeFreq = "10.0 11.0 10.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 0.5;
   camShakeRadius = 10.0;

   // Dynamic light
   lightStartRadius = 0;
   lightEndRadius = 2;
   lightStartColor = "0.6 0.0 1.0 0.8";
   lightEndColor = "0 0 0";
};


//projectile
datablock ProjectileData(pullerProjectile)
{
   projectileShapeName = "";
   explosion           = pullerExplosion;
   particleEmitter     = pullerTrailEmitter;
   muzzleVelocity      = 50;

   armingDelay         = 0;
   lifetime            = 800;
   fadeDelay           = 800;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 0;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "0.6 0.0 1.0 0.8";
};


//////////
// item //
//////////
datablock ItemData(puller)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/weapons/purplepuller/puller.dts";
	skinName = 'black';
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a Kineticpuller.';
	invName = "Kineticpuller";
	image = pullerImage;
	threatlevel = "Safe";
};

addWeapon(puller);

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(pullerImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/weapons/purplepuller/puller.dts";
   skinName = 'brown';
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
   correctMuzzleVector = false;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = puller;
   ammo = " ";
   projectile = pullerProjectile;
   projectileType = Projectile;

   directDamage        = 0;
   radiusDamage        = 0;
   damageRadius        = 0.5;
   damagetype          = '%1 was given an atomic wedgie by %2';
   muzzleVelocity      = 50;
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
	stateTransitionOnTriggerDown[1]  = "Charge";
	stateAllowImageChange[1]         = true;
	
	stateName[2]                    = "Charge";
	stateTransitionOnTimeout[2]     = "Fire";
	stateTransitionOnTriggerUp[2]	= "StopFire";
	stateTimeoutValue[2]            = 1.0;
	stateFire[2]                    = false;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "chrg";
	stateWaitForTimeout[2]			= true;
	stateSound[2]					= kickerChargeSound;

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "Reload";
	stateTimeoutValue[3]            = 0.01;
	stateFire[3]                    = true;
	stateAllowImageChange[3]        = false;
	stateScript[3]                  = "onFire";
	stateWaitForTimeout[3]			= true;
	stateSound[3]					= kickerFireSound;

	stateName[4]			= "Reload";
	stateSequence[4]                = "Reload";
	stateAllowImageChange[4]        = false;
	stateTimeoutValue[4]            = 0.01;
	stateWaitForTimeout[4]		= true;
	stateTransitionOnTimeout[4]     = "Check";

	stateName[5]			= "Check";
	stateTransitionOnTriggerUp[5]	= "StopFire";
	stateTransitionOnTriggerDown[5]	= "Charge";

	stateName[6]                    = "StopFire";
	stateTransitionOnTimeout[6]     = "Ready";
	stateTimeoutValue[6]            = 0.2;
	stateAllowImageChange[6]        = false;
	stateWaitForTimeout[6]		= true;
	//stateSequence[6]                = "Reload";
	stateScript[6]                  = "onStopFire";


};
function pullerProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	if (%obj.client && (%col.getClassName() $= "Player" || %col.getClassName() $= "AIPlayer")) 
		%col.setvelocity(vectorScale(%obj.client.player.getEyeVector(), -1000));
	tbmcollison(%this, %obj, %col, %fade, %pos, %normal);
}