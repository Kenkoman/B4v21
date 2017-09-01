datablock ParticleData(FlareGunExpParticle)
{
	dragCoefficient		= 0.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.3;
	constantAcceleration	= 0.0;
	lifetimeMS		= 50;
	lifetimeVarianceMS	= 0;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= false;
	animateTexture		= false;
	

	textureName = "~/data/shapes/weapons/spark";


	
	times[0] = 0.5;
	times[1] = 1.0;


	colors[0] = "1.0 0.5 0.0 0.8";
	colors[1] = "1.0 0.2 0.0 0.5";


	sizes[0] = 50.0;
	sizes[1] = 2.0;
};

datablock ParticleEmitterData(FlareGunExpEmitter)
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;

   ejectionVelocity = 0.25;
   velocityVariance = 0.10;

   ejectionOffset = 0;

   thetaMin         = 0.0;
   thetaMax         = 90.0;  

   particles = FlareGunExpParticle;
};





//datablock ParticleData(FlareGunParticle)
//{
//	dragCoefficient		= 0.0;
//	windCoefficient		= 0.0;
//	gravityCoefficient	= 0.0;
//	inheritedVelFactor	= 0.3;
//	constantAcceleration	= 0.0;
//	lifetimeMS		= 50;
//	lifetimeVarianceMS	= 0;
//	spinSpeed		= 10.0;
//	spinRandomMin		= -50.0;
//	spinRandomMax		= 50.0;
//	useInvAlpha		= false;
//	animateTexture		= false;
//	
//
//	textureName = "~/data/shapes/weapons/spark";
//
//
//	
//	times[0] = 0.5;
//	times[1] = 1.0;
//
//
//	colors[0] = "1.0 0.5 0.0 0.8";
//	colors[1] = "1.0 0.2 0.0 0.5";
//
//
//	sizes[0] = 3.0;
//	sizes[1] = 2.0;
//};

//datablock ParticleEmitterData(FlareGunEmitter)
//{
//   ejectionPeriodMS = 7;
//   periodVarianceMS = 0;
//
//   ejectionVelocity = 0.25;
//   velocityVariance = 0.10;
//
//   ejectionOffset = 0;
//
//   thetaMin         = 0.0;
//   thetaMax         = 90.0;  
//
//   particles = FlareGunParticle;
//};






datablock ExplosionData(FlareGunExplosion)
{
   //explosionShape = "";
	//soundProfile = FlareGunExplosionSound;

   lifeTimeMS = 500;

	particleEmitter = FlareGunExpEmitter;
	particleDensity = 1;
	particleRadius  = 5;
   emitter[0] = FlareGunExpEmitter;
   faceViewer     = true;
   explosionScale = "1 1 1";

	shakeCamera      = false;
	camShakeFreq     = "10.0 11.0 10.0";
	camShakeAmp      = "0.5 0.5 0.5";
	camShakeDuration = 0.5;
	camShakeRadius   = 10.0;

	// This will create a dynamic lighting effect in the vicinity of the 
    // rocket's explosion.
	lightStartRadius = 20;
	lightEndRadius   = 0;
	lightStartColor  = "1.0 0.5 0.0 0.8";
	lightEndColor    = "0.0 0.0 0.0 0.0";
};

//weapon stuff

datablock ProjectileData(FlareGunProjectile)
{
   projectileShapeName = "tbm/data/shapes/halo/red/halo.dts";
   particleEmitter     = MortarCannontrailEmitter;
   explosion           = FlareGunExplosion;
   muzzleVelocity      = 120;

   armingDelay         = 3000;
   lifetime            = 3100;
   fadeDelay           = 3100;
   bounceElasticity    = 0.5;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 1;

   hasLight    = true;
   lightRadius = 20.0;
   lightColor  = "1.0 0.5 0.0 0.8";
};
	
function FlareGunProjectile::MakeFlareExplosion(%this, %obj) {   
createExplosion(nameToID(FlareGunExplosion), %obj.getPosition());
%obj.delete();
}

//////////
// item //
//////////
datablock ItemData(FlareGun)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/weapons/Flaregun.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a FlareGun.';
	invName = "FlareGun";
	spawnName = "Flare Gun";
	image = FlareGunImage;
	threatlevel = "Safe";
};

addWeapon(FlareGun);

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(FlareGunImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/weapons/Flaregun.dts";
   emap = true;
   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0.2";
   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = FlareGun;
   ammo = " ";
   projectile = FlareGunProjectile;
   projectileType = Projectile;

   directDamage        = 0;
   radiusDamage        = 0;
   damageRadius        = 0;
   muzzleVelocity      = 120;
   velInheritFactor    = 1;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = true;

   //casing = " ";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordinRPGy.  The following
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
	stateTimeoutValue[2]            = 0.05;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]		= true;
	stateSound[2]			= GLFireSound;

	stateName[3]			= "Reload";
	stateSequence[3]                = "Reload";
	stateAllowImageChange[3]        = false;
	stateTimeoutValue[3]            = 2.0;
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




function FlareGunImage::onFire(%this,%obj,%slot)
{
	%p = Parent::onFire(%this, %obj, %slot);
	for(%i = 0; %i < getWordCount(%p); %i++)
		%this.projectile.schedule(3000, MakeFlareExplosion, getWord(%p, %i));
	muzzleflash(%this, %obj, %slot, 0.75, 0.75, 0.75);
	return %p;
}