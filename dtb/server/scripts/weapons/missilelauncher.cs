//bullet trail
//datablock ParticleData(MissileLauncherTrailParticle)
//{
//	dragCoefficient		= 3.0;
//	windCoefficient		= 0.0;
//	gravityCoefficient	= 0.0;
//	inheritedVelFactor	= 0.0;
//	constantAcceleration	= 0.0;
//	lifetimeMS		= 900;
//	lifetimeVarianceMS	= 0;
//	spinSpeed		= 10.0;
//	spinRandomMin		= -50.0;
//	spinRandomMax		= 50.0;
//	useInvAlpha		= false;
//	animateTexture		= false;
//	//framesPerSec		= 1;
//
//	textureName		= "~/data/particles/cloud";
//	//animTexName		= "~/data/particles/dot";
//
//	// Interpolation variables
//	colors[0]	= "1 0 0 0.5";
//	colors[1]	= "1 0.25 0 0.0";
//	sizes[0]	= 0.2;
//	sizes[1]	= 0.01;
//	times[0]	= 0.0;
//	times[1]	= 1.0;
//};

//datablock ParticleEmitterData(MissileLauncherTrailEmitter)
//{
//   ejectionPeriodMS = 2;
//   periodVarianceMS = 0;
//
//   ejectionVelocity = 5; //0.25;
//   velocityVariance = 1; //0.10;
//
//   ejectionOffset = 0;
//
//   thetaMin         = 0.0;
//   thetaMax         = 90.0;  
//
//   particles = "MissileLauncherTrailParticle";
//};
//Commented out to save datablocks, besides, my flame3emitter looks better in my opinion.

//////////
// item //
//////////
datablock ItemData(MissileLauncher)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/weapons/MissileLauncher.dts";
	skinName = 'black';
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a Nega Cannon.';
        spawnname = "Missile Launcher";
	invName = "Nega Cannon";
	image = MissileLauncherImage;
	threatlevel = "Normal";
};

addWeapon(MissileLauncher);

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(MissileLauncherImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/weapons/MissileLauncher.dts";
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
   item = MissileLauncher;
   ammo = " ";
   projectile = hack2Projectile;
   projectileType = Projectile;

   recoil = 1.5;
   recoilSeconds = 3;

   directDamage        = 30;
   radiusDamage        = 90;
   damageRadius        = 12;
   damagetype          = '%1 rode %2\'s rocket to hell';
   muzzleVelocity      = 100;
   velInheritFactor    = 0;
   impulse             = 2500;

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
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.5;
	stateTransitionOnTimeout[0]       = "Ready";
	stateSound[0]					= weaponSwitchSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateAllowImageChange[1]         = true;

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "Reload";
	stateTimeoutValue[2]            = 0.5;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]			= true;
	stateSound[2]					= MissileLauncherFireSound;

	stateName[3]			= "Reload";
	stateSequence[3]                = "Reload";
	stateAllowImageChange[3]        = false;
	stateTimeoutValue[3]            = 3;
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

function MissileLauncherImage::onFire(%this,%obj,%slot)
{
	%this.projectile.scale               = "1 2 1";
	%this.projectile.armingDelay         = 0;
	%this.projectile.lifetime            = 60*1000;
	%this.projectile.fadeDelay           = 40*1000;
	%this.projectile.bounceElasticity    = 0;
	%this.projectile.bounceFriction      = 0;
	%this.projectile.isBallistic         = true;
	%this.projectile.gravityMod = 0;
	%this.projectile.hasLight    = true;
	%this.projectile.lightRadius = 3.0;
	%this.projectile.lightColor  = "0.7 0 0";

	%p = Parent::onFire(%this, %obj, %slot);
	%obj.applyImpulse(0, vectorScale(%obj.getMuzzleVector(%slot), -20));
	return %p;
}
