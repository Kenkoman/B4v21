//NTS: make someone else edit the particle things to make them match up with the damage

datablock ParticleData(nukebubbleExplosionParticle)
{
	dragCoefficient      = 1;
	gravityCoefficient   = 0;
	inheritedVelFactor   = 0;
	constantAcceleration = 1.0;
	lifetimeMS           = 1000;
	lifetimeVarianceMS   = 100;
	textureName          = "~/data/shapes/weapons/fire";
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= false;
	colors[0]     = "1 1 1 1";
	colors[1]     = "1 1 1 1";
	colors[2]     = "1 1 1 1";
	colors[3]     = "1 1 1 1";
	sizes[0]      = 0.0;
	sizes[1]      = 5;
	sizes[2]      = 10;
	sizes[3]      = 20;
	times[0]	= 0.0;
	times[1]	= 0.1;
	times[2]	= 0.5;
	times[3]	= 1.0;
};

datablock ParticleEmitterData(nukebubbleExplosionEmitter)
{
   ejectionPeriodMS = 4000;
   periodVarianceMS = 25;
   ejectionVelocity = 100;
   velocityVariance = 1.0;
   ejectionOffset   = 15.0;
   thetaMin         = 0;
   thetaMax         = 0;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = true;
   particles = "nukebubbleExplosionParticle";
};


//effects
datablock ParticleData(nukeExplosionParticle)
{
    dragCoefficient = 0;
    gravityCoefficient = -0.3;
    windCoefficient = 0;
    inheritedVelFactor = 0;
    constantAcceleration = 0;
    lifetimeMS = 5000;
    lifetimeVarianceMS = 0;
    useInvAlpha = 0;
    spinRandomMin = 0;
    spinRandomMax = 50;
    textureName = "~/data/particles/cloud.png";
    spinSpeed = 0;
    times[0] = 0;
    times[1] = 0.8;
    times[2] = 1;
    colors[0] = "1.000000 0.200000 0.200000 0.800000";
    colors[1] = "1.000000 0.500000 0.200000 1.000000";
    colors[2] = "1.000000 1.000000 0.200000 0.800000";
    sizes[0] = 30;
    sizes[1] = 5;
    sizes[2] = 100;
};

datablock ParticleEmitterData(nukeExplosionEmitter)
{
    ejectionPeriodMS = 10;
    periodVarianceMS = 0;
    ejectionVelocity = 0;
    velocityVariance = 0;
    ejectionOffset = 32;
    thetaMin = 0;
    thetaMax = 90;
    phiReferenceVel = 0;
    phiVariance = 360;
    overrideAdvances = 0;
    lifetimeMS = 10000;
    orientParticles= 0;
    orientOnVelocity = 1;
    particles = "nukeExplosionParticle";
};

datablock ExplosionData(nukeExplosion)
{
//   explosionShape = "tbm/data/shapes/DShiznit/mushrroomcloud.dts";
	soundProfile = MissileLauncherExplosionSound;

   lifeTimeMS = 10000;

   particleemitter = nukebubbleExplosionEmitter;
   emitter[0] = nukeExplosionEmitter;
   particleDensity = 1000;
   particleRadius = 0.1;

   faceViewer     = true;
   explosionScale = "0.1 0.1 0.1";

   shakeCamera = true;
   camShakeFreq = "10.0 11.0 10.0";
   camShakeAmp = "10.0 10.0 10.0";
   camShakeDuration = 10;
   camShakeRadius = 2048.0;

   // Dynamic light
   lightStartRadius = 0;
   lightEndRadius = 20;
   lightStartColor = "0.2 0.1 0.0 0.8";
   lightEndColor = "0 0 0";
};


//projectile
datablock ProjectileData(nukeProjectile)
{
   projectileShapeName = "~/data/shapes/weapons/Missile.dts";
   muzzleVelocity      = 45;

   closeRadiusDamage   = 2400;
   closeDamageRadius   = 12;

   radRadiusDamage     = 2;
   radDamageRadius     = 128;
   radTime             = 20;

   explosion           = nukeExplosion;
   particleEmitter     = MortarCannonTrailEmitter;

   scale               = "1 4 1";
   armingDelay         = 0;
   lifetime            = 60*1000;
   fadeDelay           = 40*1000;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 0;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "0.7 0 0";
};

//////////
// item //
//////////
datablock ItemData(nuke)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/weapons/missilelauncher.dts";
	skinName = 'black';
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a Nuke.';
	invName = "Nuke";
	image = nukeImage;
	threatlevel = "Dangerous";
};

addWeapon(Nuke);

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(nukeImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/weapons/missilelauncher.dts";
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
   item = nuke;
   ammo = " ";
   projectile = nukeProjectile;
   projectileType = Projectile;

   directDamage        = 1200;
   radiusDamage        = 2400;
   damageRadius        = 128;
   damagetype          = '%1 was vaporized by %2';
   muzzleVelocity      = 45;
   velInheritFactor    = 0.3;

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

function nukeImage::onMount(%this, %obj)
{
	%obj.playthread(1, armreadyright);
	messageAll( 'MsgClientKilled', '%1 \c2pulled out a \c9NUKE!\c2 It will be a red dawn...', %obj.client.name);
}

function nukeImage::onFire(%this, %obj, %slot)
{
	Parent::onFire(%this, %obj, %slot);
	if(%obj.getClassName() $= "Player") {
		%obj.inventory[%obj.currWeaponslot] = "";
		schedule(1000, 0, messageClient, %obj.client, 'MsgDropItem', '', %obj.currWeaponSlot);
		%obj.schedule(1000, unmountImage, 0);
		%obj.currWeaponSlot = -1;
	}
}

function nukeProjectile::onCollision(%this, %obj, %col, %fade, %pos, %normal) 
{ 
	tbmcollison(%this, %obj, %col, %fade, %pos, %normal);
       //Short-range unblockable damage (references datablock damage values)
       tbmradiusDamage
     (%obj, VectorAdd(%pos, VectorScale(%normal, 0.01)),
      %this.closeDamageRadius, %this.closeRadiusDamage, %obj.damageType, 100, 1);
       //Standard long-range damage
       tbmradiusDamage
     (%obj, VectorAdd(%pos, VectorScale(%normal, 0.01)),
      %obj.damageRadius, %obj.radiusDamage, %obj.damageType, 100, 0);

	//To keep all of the variables intact after the projectile is deleted
	%a = new ScriptObject();
	  %a.initialVelocity  = %obj.initialVelocity;
	  %a.initialPosition  = %obj.initialPosition;
	  %a.sourceObject     = %obj.sourceObject;
	  %a.sourceSlot       = %obj.sourceSlot;
	  %a.client           = %obj.client;
	  %a.deathAnim        = "gooblob";
	  %a.damageRadius     = %this.radDamageRadius;
	  %a.radiusDamage     = %this.radRadiusDamage;
      MissionCleanup.add(%a);

      for(%i = 1; %i <= %this.radTime; %i++)
	schedule(1000 * %i, 0, tbmradiusDamage, %a, VectorAdd(%pos, VectorScale(%normal, 0.01)), %a.damageRadius, %a.radiusDamage, "Radiation", 0, 0.3);
      %a.schedule(1001 * %this.radTime, delete);
}