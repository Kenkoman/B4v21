//blackholeGun trail
//datablock ParticleData(blackholeGunTrailParticle)
//{
//	dragCoefficient		= 0.0;
//	windCoefficient		= 0.0;
//	gravityCoefficient	= 0.0;
//	inheritedVelFactor	= 1;
//	constantAcceleration	= 0.0;
//	lifetimeMS		= 300;
//	lifetimeVarianceMS	= 0;
//	spinSpeed		= 10.0;
//	spinRandomMin		= -50.0;
//	spinRandomMax		= 50.0;
//	useInvAlpha		= true;
//	animateTexture		= false;
//	//framesPerSec		= 1;
//
//	textureName		= "~/data/shapes/weapons/fire";
//	//animTexName		= "~/data/particles/dot";
//
//	// Interpolation variables
//	colors[0]	= "0 0 0 0.0";
//	colors[1]	= "0 0 0 0.5";
//	colors[2]	= "0 0 0 0";
//	sizes[0]	= 2;
//	sizes[1]	= 2;
//	sizes[2]	= 2;
//	times[0]	= 0.0;
//	times[1]	= 0.9;
//	times[2]	= 1.0;
//};

//datablock ParticleEmitterData(blackholeGunTrailEmitter)
//{
//   ejectionPeriodMS = 1;
//   periodVarianceMS = 0;
//
//   ejectionVelocity = 0; //-25;
//   velocityVariance = 0;
//
//   ejectionOffset = 2.5; //10;
//
//   thetaMin         = 0.0;
//   thetaMax         = 180.0;  
//
//   particles = blackholeGunTrailParticle;
//};

//effects
datablock ParticleData(blackholeGunExplosionParticle)
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
	useInvAlpha		= true;
	colors[0]     = "0 0 0 1";
	colors[1]     = "0 0 0 1";
	colors[2]     = "0 0 0 1";
	colors[3]     = "0 0 0 0";
	sizes[0]      = 0.0;
	sizes[1]      = 5;
	sizes[2]      = 10;
	sizes[3]      = 20;
	times[0]	= 0.0;
	times[1]	= 0.1;
	times[2]	= 0.9;
	times[3]	= 1.0;
};

datablock ParticleEmitterData(blackholeGunExplosionEmitter)
{
   ejectionPeriodMS = 4000;
   periodVarianceMS = 25;
   ejectionVelocity = 18;
   velocityVariance = 1.0;
   ejectionOffset   = 15.0;
   thetaMin         = 0;
   thetaMax         = 0;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = true;
   particles = "blackholeGunExplosionParticle";
};

datablock ExplosionData(blackholeGunExplosion)
{
   //explosionShape = "";
	soundProfile = blackholeGunExplosionSound;

   lifeTimeMS = 1000;

   particleEmitter = blackholeGunExplosionEmitter;
   particleDensity = 1000;
   particleRadius = 0.1;

   emitter[0] = blackHoleGunExplosionEmitter;
   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = true;
   camShakeFreq = "1.0 2.0 1.0";
   camShakeAmp = "20.0 180.0 20.0";
   camShakeDuration = 5.0;
   camShakeRadius = 1024;

   // Dynamic light
   lightStartRadius = 15;
   lightEndRadius = 0;
   lightStartColor = "1 1 1";
   lightEndColor = "1 1 1";
};


//projectile
datablock ProjectileData(blackholeGunProjectile)
{
   projectileShapeName = "dtb/data/shapes/weapons/redmatter.dts";
   explosion           = blackholeGunExplosion;
   particleEmitter     = blackholeGunTrailEmitter;
   muzzleVelocity      = 45;

   armingDelay         = 0;
   lifetime            = 8000;
   fadeDelay           = 7500;
   bounceElasticity    = 0.5;
   bounceFriction      = 0.1;
   isBallistic         = true;
   gravityMod = 0;

   hasLight    = true;
   lightRadius = 5.0;
   lightColor  = "1 1 1";
};


//////////
// item //
//////////
datablock ItemData(blackholeGun)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/weapons/redmatterlauncher.dts";
	skinName = 'brown';
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'Rift Cannon';
	invName = "Rift Cannon";
	spawnName = "Black Hole Gun";
	image = blackholeGunImage;
	threatlevel = "Dangerous";
};

addWeapon(blackholeGun);

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(blackholeGunImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/weapons/redmatterlauncher.dts";
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
   item = blackholeGun;
   ammo = " ";
   projectile = blackholeGunProjectile;
   projectileType = Projectile;

   directDamage        = 5000;
   radiusDamage        = 5000;
   damageRadius        = 1024;
   damagetype          = '%1 was sucked into oblivion by %2';
   muzzleVelocity      = 45;
   velInheritFactor    = 1;

   deathAnimationClass = "default";
   deathAnimation = "delete";
   deathAnimationPercent = 1;

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
	stateSound[0]					= weaponswitchSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "Charge";
	stateAllowImageChange[1]         = true;
	
	stateName[2]                    = "Charge";
	stateTransitionOnTimeout[2]     = "Fire";
	stateTransitionOnTriggerUp[2]	= "StopFire";
	stateTimeoutValue[2]            = 2.5;
	stateFire[2]                    = false;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Charge";
	stateWaitForTimeout[2]			= true;
	stateSound[2]					= blackholeGunChargeSound;

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "Reload";
	stateTimeoutValue[3]            = 0.05;
	stateFire[3]                    = true;
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	stateScript[3]                  = "onFire";
	stateWaitForTimeout[3]			= true;
	stateSound[3]					= blackholeGunFireSound;


	stateName[4]			= "Reload";
	stateSequence[4]                = "Reload";
	stateAllowImageChange[4]        = false;
	stateTimeoutValue[4]            = 0.5;
	stateWaitForTimeout[4]		= true;
	stateTransitionOnTimeout[4]     = "Check";

	stateName[5]			= "Check";
	stateTransitionOnTriggerUp[5]	= "StopFire";
	stateTransitionOnTriggerDown[5]	= "StopFire";

	stateName[6]                    = "StopFire";
	stateTransitionOnTimeout[6]     = "Ready";
	stateTimeoutValue[6]            = 0.2;
	stateAllowImageChange[6]        = false;
	stateWaitForTimeout[6]		= true;
	//stateSequence[6]                = "Reload";
	stateScript[6]                  = "onStopFire";


};

function blackholeGunImage::onMount(%this, %obj)
{
	%obj.playthread(1, armreadyright);
	messageAll( 'MsgClientKilled','%1 \c2pulled out a \c9BLACK HOLE\c2 gun. Keep in mind that the intense gravity of the event horizon is hazardous to human health.',%obj.client.name);
}

function blackholeGunImage::onFire(%this, %obj, %slot)
{
	%p = Parent::onFire(%this, %obj, %slot);
	%obj.applyimpulse(0, vectorScale(%obj.getMuzzleVector(%slot), -200));
	%obj.playthread(2, jump);
	return %p;
}

function blackholeGunImage::onStopFire(%this, %obj, %slot)
{
	%obj.playthread(2, root);
}

//None of this works.
//It's also not written correctly.
//Don't use it.
function shizspin(%p)
{
	if(%p)
	{
		if(%p.spincycle == 0)
		{
		%p.velocity = VectorAdd(%p.velocity,"1 1 0");
		%p.spincycle++;
		}
		if(%p.spincycle == 1)
		{
		%p.velocity = VectorAdd(%p.velocity,"0 0 1");
		%p.spincycle++;
		}
		if(%p.spincycle == 2)
		{
		%p.velocity = VectorAdd(%p.velocity,"-1 -1 0");
		%p.spincycle++;
		}
		if(%p.spincycle == 3)
		{
		%p.velocity = VectorAdd(%p.velocity,"0 0 1");
		%p.spincycle = 0;
		}
		schedule(100,0,shizspin,%p);
	}
}

function blackholeGunProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	tbmcollison(%this,%obj,%col,%fade,%pos,%normal);
       tbmradiusDamage
     (%obj, VectorAdd(%pos, VectorScale(%normal, 0.01)),
      %obj.damageRadius, 0, %obj.damageType,-10000);

      //To keep all of the variables intact after the projectile is deleted
      %a = new ScriptObject();
	%a.initialVelocity  = %obj.initialVelocity;
	%a.initialPosition  = %obj.initialPosition;
	%a.sourceObject     = %obj.sourceObject;
	%a.sourceSlot       = %obj.sourceSlot;
	%a.client           = %obj.client;
	%a.deathAnim        = %obj.deathAnim;
	%a.damageRadius     = %obj.damageRadius;
	%a.radiusDamage     = %obj.radiusDamage;
	%a.damageType       = %obj.damageType;
	%obj = %a;
      MissionCleanup.add(%obj);

      for(%i = 1; %i <= 12; %i++)
	schedule(200 * %i, 0, blackHoleBoom, %this, %obj, %col, %fade, %pos, %normal);
      %obj.schedule(3000, delete);
}

function blackHoleBoom(%this, %obj, %col, %fade, %pos, %normal) {
      //Sucky part
       tbmradiusDamage
     (%obj, VectorAdd(%pos, vectorScale(%normal, 0.01)),
      %obj.damageRadius, 0, %obj.damageType, -8000);
      //Killy part
       tbmradiusDamage
     (%obj, VectorAdd(%pos, VectorScale(%normal, 0.01)),
      5 ,%obj.radiusDamage, %obj.damageType, -100);
}