//Bow.cs
//bow and arrow weapon stuff

datablock AudioProfile(arrowExplosionSound)
{
   filename    = "~/data/sound/arrowHit.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(bowFireSound)
{
   filename    = "~/data/sound/bowFire.wav";
   description = AudioClosest3d;
   preload = true;
};

//spear trail
datablock ParticleData(spearTrailParticle)
{
	dragCoefficient		= 3.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 600;
	lifetimeVarianceMS	= 0;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= true;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "~/data/particles/ring";
	//animTexName		= " ";

	// Interpolation variables
	colors[0]	= "0.75 0.75 0.75 0.3";
	colors[1]	= "0.75 0.75 0.75 0.2";
	colors[2]	= "1 1 1 0.0";
	sizes[0]	= 0.15;
	sizes[1]	= 0.35;
	sizes[2]	= 0.05;
	times[0]	= 0.0;
	times[1]	= 0.1;
	times[2]	= 1.0;
};

datablock ParticleEmitterData(spearTrailEmitter)
{
   ejectionPeriodMS = 5;
   periodVarianceMS = 0;

   ejectionVelocity = 0; //0.25;
   velocityVariance = 0; //0.10;

   ejectionOffset = 0;

   thetaMin         = 0.0;
   thetaMax         = 90.0;  

   particles = spearTrailParticle;
};

//arrow trail
//datablock ParticleData(arrowTrailParticle)
//{
//	dragCoefficient		= 0.0;
//	windCoefficient		= 0.0;
//	gravityCoefficient	= 0.0;
//	inheritedVelFactor	= 0.0;
//	constantAcceleration	= 0.0;
//	lifetimeMS		= 1000;
//	lifetimeVarianceMS	= 0;
//	spinSpeed		= 10.0;
//	spinRandomMin		= -50.0;
//	spinRandomMax		= 50.0;
//	useInvAlpha		= false;
//	animateTexture		= false;
//	//framesPerSec		= 1;
//
//	textureName		= "~/data/particles/dot";
//	//animTexName		= "~/data/particles/dot";
//
//	// Interpolation variables
//	colors[0]	= "1 1 1 0.5";
//	colors[1]	= "1 1 1 0.0";
//	sizes[0]	= 0.2;
//	sizes[1]	= 0.01;
//	times[0]	= 0.0;
//	times[1]	= 1.0;
//};

//datablock ParticleEmitterData(arrowTrailEmitter)
//{
//   ejectionPeriodMS = 7;
//   periodVarianceMS = 0;
//
//   ejectionVelocity = 0; //0.25;
//   velocityVariance = 0; //0.10;
//
//   ejectionOffset = 0;
//
//   thetaMin         = 0.0;
//   thetaMax         = 90.0;  
//
//   particles = arrowTrailParticle;
//};

//effects
datablock ParticleData(arrowExplosionParticle)
{
	dragCoefficient      = 5;
	gravityCoefficient   = 0.1;
	inheritedVelFactor   = 0.2;
	constantAcceleration = 0.0;
	lifetimeMS           = 500;
	lifetimeVarianceMS   = 300;
	textureName          = "~/data/particles/chunk";
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	colors[0]     = "0.9 0.9 0.6 0.9";
	colors[1]     = "0.9 0.5 0.6 0.0";
	sizes[0]      = 0.25;
	sizes[1]      = 0.0;
};

datablock ParticleEmitterData(arrowExplosionEmitter)
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   ejectionVelocity = 5;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "arrowExplosionParticle";
};

datablock ExplosionData(arrowExplosion)
{
   //explosionShape = "";
	soundProfile = arrowExplosionSound;

   lifeTimeMS = 150;

   particleEmitter = arrowExplosionEmitter;
   particleDensity = 10;
   particleRadius = 0.2;

   emitter[0] = arrowExplosionEmitter;
   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = false;
   camShakeFreq = "10.0 11.0 10.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 0.5;
   camShakeRadius = 10.0;

   // Dynamic light
   lightStartRadius = 0;
   lightEndRadius = 0;
   lightStartColor = "0.3 0.6 0.7";
   lightEndColor = "0 0 0";
};


//projectile
datablock ProjectileData(arrowProjectile)
{
   projectileShapeName = "~/data/shapes/arrow.dts";
   explosion           = arrowExplosion;
   particleEmitter     = spearTrailEmitter;
   muzzleVelocity      = 200;

   armingDelay         = 0;
   lifetime            = 20000;
   fadeDelay           = 16000;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 1;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";
};


//////////
// item //
//////////
datablock ItemData(bow)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/bow.dts";
	skinName = 'brown';
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a bow and arrows.';
	invName = "Bow";
	image = bowImage;
};

/////////
//weapon image//
////////////////
datablock ShapeBaseImageData(bowImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/bow.dts";
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
   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = bow;
   ammo = " ";
   projectile = arrowProjectile;
   projectileType = Projectile;

   directDamage        = 60;
   radiusDamage        = 0;
   damageRadius        = 0.5;
   damagetype          = '%1 was skewered by %2';
   muzzleVelocity      = 200;
   velInheritFactor    = 1;

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
	stateTransitionOnTriggerUp[0]       = "Ready";
	stateTransitionOnTriggerDown[0]       = "Ready";
	stateSound[0]					= weaponSwitchSound;

	stateName[1]                     = "Ready";
	stateSequence[1]                = "Fire";
	stateTransitionOnTriggerDown[1]  = "Load";
	stateAllowImageChange[1]         = true;

	stateName[2]			= "Load";
	stateSequence[2]                = "Reload";
	stateAllowImageChange[2]        = false;
	stateTimeoutValue[2]            = 0.2;
	stateTransitionOnTriggerUp[2]     = "Ready";
	stateTransitionOnTimeout[2]     = "Set";

	stateName[3]                     = "Set";
	stateTransitionOnTriggerUp[3]  = "Fire";
	stateAllowImageChange[3]         = true;

	stateName[4]                    = "Fire";
	stateTransitionOnTimeout[4]     = "Ready";
	stateTimeoutValue[4]		= 0.0001;
	stateFire[4]                    = true;
	stateAllowImageChange[4]        = false;
	stateScript[4]                  = "onFire";
	stateSound[4]					= bowFireSound;

};

function arrowProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal) {
  if (%obj.client.isadmin)
    if (%col.getClassName() $= "AIPlayer") {
      %col.damage(%obj,%pos,100,"builshit");
      MessageAll(%client, '\c3Nice shot master %1', %obj.client.name);
      }
}