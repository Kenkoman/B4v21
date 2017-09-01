//spear.cs


datablock AudioProfile(FWRocket2ExplosionSound)
{
   filename    = "~/data/sound/spearHit.wav";
   description = AudioClose3d;
   preload = false;
};

datablock AudioProfile(FWRocket2FireSound)
{
   filename    = "~/data/sound/spearFire.wav";
   description = AudioClose3d;
   preload = true;
};


//FWRocket2 trail
datablock ParticleData(FWRocket2TrailParticle)
{
	dragCoefficient		= 3.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 900;
	lifetimeVarianceMS	= 0;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= false;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "~/data/particles/cloud";
	//animTexName		= " ";

	// Interpolation variables
	colors[0]	= "1 0 0 0.5";
	colors[1]	= "1 0.25 0 0.0";
	sizes[0]	= 0.2;
	sizes[1]	= 0.01;
	times[0]	= 0.0;
	times[1]	= 1.0;
};

datablock ParticleEmitterData(FWRocket2TrailEmitter)
{
   ejectionPeriodMS = 2;
   periodVarianceMS = 0;

   ejectionVelocity = 5; //0.25;
   velocityVariance = 0; //0.10;

   ejectionOffset = 0;

   thetaMin         = 0.0;
   thetaMax         = 90.0;  

   particles = FWRocket2TrailParticle;
};


//effects
datablock ParticleData(FWRocket2ExplosionParticle)
{
	dragCoefficient		= 3.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= -1.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 2000;
	lifetimeVarianceMS	= 1000;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= false;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "common/lighting/corona";
	//animTexName		= "~/../data/particles/cloud";

	// Interpolation variables
	colors[0]	= "1 0 0 1";
	colors[1]	= "1 0 1 1";
	colors[2]	= "1 1 1 1";
	sizes[0]	= 1;
	sizes[1]	= 1;
	sizes[2]	= 0.6;
	times[0]	= 0.0;
	times[1]	= 0.7;
	times[2]	= 0.8;
};

datablock ParticleEmitterData(FWRocket2ExplosionEmitter)
{
   ejectionPeriodMS = 2.5;
   periodVarianceMS = 0;
   ejectionVelocity = 10;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "FWRocket2ExplosionParticle";
};

datablock ParticleData(FWRocket2ExplosionParticle2)
{
	dragCoefficient		= 0.1;
	windCoefficient		= 0.0;
	gravityCoefficient	= 1.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 1000;
	lifetimeVarianceMS	= 100;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= false;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "~/data/particles/chunk";
	//animTexName		= "~/data/particles/cloud";

	// Interpolation variables
	colors[0]	= "1 1 1 0.0";
	colors[1]	= "1 1 1 0.5";
	colors[2]	= "1 1 1 0.0";
	sizes[0]	= 0.0;
	sizes[1]	= 1.5;
	sizes[2]	= 0.0;
	times[0]	= 0.0;
	times[1]	= 0.6;
	times[2]	= 1.0;
};

datablock ParticleEmitterData(FWRocket2ExplosionEmitter2)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   lifetimeMS       = 100;
   ejectionVelocity = 40;
   velocityVariance = 5.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "FWRocket2ExplosionParticle2";
};

datablock ParticleData(FWRocket2ExplosionParticle3)
{
	dragCoefficient		= 0.1;
	windCoefficient		= 0.0;
	gravityCoefficient	= 0.2;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 1000;
	lifetimeVarianceMS	= 500;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= false;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "~/data/particles/cloud";
	//animTexName		= "~/data/particles/cloud";

	// Interpolation variables
	colors[0]	= "1 1 1.0";
	colors[1]	= "1 1 1 0.0";
	sizes[0]	= 0.5;
	sizes[1]	= 0.5;
	times[0]	= 0.0;
	times[1]	= 1.0;
};

datablock ParticleEmitterData(FWRocket2ExplosionEmitter3)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   lifetimeMS       = 7;
   ejectionVelocity = 15;
   velocityVariance = 5.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "FWRocket2ExplosionParticle3";
};

datablock ExplosionData(FWRocket2Explosion)
{
   //explosionShape = "";
   lifeTimeMS = 2000;

   soundProfile = FWRocket2ExplosionSound;

   emitter[0] = FWRocket2ExplosionEmitter;
 //  emitter[1] = FWRocket2ExplosionEmitter2;
   particleDensity = 90;
   particleRadius = 9.0;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = true;
   camShakeFreq = "7.0 8.0 7.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 0.5;
   camShakeRadius = 15.0;

   // Dynamic light
   lightStartRadius = 20;
   lightEndRadius = 20;
   lightStartColor = "1 0.0 0.0 1";
   lightEndColor = "1 0.0 0.0 0.0";
};

datablock ExplosionData(FWRocket2Explosion3)
{
   //explosionShape = "";
   lifeTimeMS = 2000;

   soundProfile = FWRocket2ExplosionSound;

   emitter[0] = FWRocket2ExplosionEmitter3;
   particleDensity = 90;
   particleRadius = 9.0;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = true;
   camShakeFreq = "7.0 8.0 7.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 0.5;
   camShakeRadius = 15.0;

   // Dynamic light
   lightStartRadius = 10;
   lightEndRadius = 20;
   lightStartColor = "0.9 0.0 0.0 0.9";
   lightEndColor = "0 0 0";
};

//projectile
datablock ProjectileData(FWRocket2Projectile)
{
//   projectileShapeName = "~/data/shapes/bricks/a_briks/cylinder1x1f.dts";
   directDamage        = 1000;
   radiusDamage        = 1000;
   damageRadius        = 1;
   explosion           = FWRocket2Explosion;
   particleEmitter     = FWRocket2TrailEmitter;

   muzzleVelocity      = 25;
   velInheritFactor    = 1;

   armingDelay         = 0;
   lifetime            = 20000;
   fadeDelay           = 19500;
   bounceElasticity    = 0.8;
   bounceFriction      = 0.1;
   isBallistic         = true;
   gravityMod = 0.0;

   hasLight    = true;
   lightRadius = 20;
   lightColor  = "1 0 0 1.0";
};


//////////
// item //
//////////
datablock ItemData(FWRocket2)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
        shapeFile = "~/data/shapes/bricks/a_briks/cylinder1x1f.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	cost = "250";
	 // Dynamic properties defined by the scripts
	pickUpName = 'a FWRocket2';
	invName = 'FWRocket2';
	image = FWRocket2Image;
};

//function FWRocket2::onUse(%this,%user)
//{
//	//mount the image in the right hand slot
//	%user.mountimage(%this.image, $RightHandSlot);
//}

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(FWRocket2Image)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/bricks/a_briks/cylinder1x1f.dts";
   PreviewFileName = "rtb/data/shapes/bricks/Previews/spear.png";
   emap = true;
   skinName=green;
   cloakable = false;
   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "-0.3 -0.2 0";
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
   item = FWRocket2;
   ammo = " ";
   projectile = FWRocket2Projectile;
   projectileType = Projectile;

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
	stateName[0]			= "Activate";
	stateTimeoutValue[0]		= 0.5;
	stateTransitionOnTimeout[0]	= "Ready";
	stateSequence[0]		= "ready";
	stateSound[0]					= weaponSwitchSound;

	stateName[1]			= "Ready";
	stateTransitionOnTriggerDown[1]	= "Charge";
	stateAllowImageChange[1]	= true;
	
	stateName[2]                    = "Charge";
	stateTransitionOnTimeout[2]	= "Armed";
	stateTimeoutValue[2]            = 0.7;
	stateWaitForTimeout[2]		= false;
	stateTransitionOnTriggerUp[2]	= "AbortCharge";
	stateScript[2]                  = "onCharge";
	stateAllowImageChange[2]        = false;
	
	stateName[3]			= "AbortCharge";
	stateTransitionOnTimeout[3]	= "Ready";
	stateTimeoutValue[3]		= 0.3;
	stateWaitForTimeout[3]		= true;
	stateScript[3]			= "onAbortCharge";
	stateAllowImageChange[3]	= false;

	stateName[4]			= "Armed";
	stateTransitionOnTriggerUp[4]	= "Fire";
	stateAllowImageChange[4]	= false;

	stateName[5]			= "Fire";
	stateTransitionOnTimeout[5]	= "Ready";
	stateTimeoutValue[5]		= 0.5;
	stateFire[5]			= true;
	stateSequence[5]		= "fire";
	stateScript[5]			= "onFire";
	stateWaitForTimeout[5]		= true;
	stateAllowImageChange[5]	= false;
	stateSound[5]				= FWRocket2FireSound;
};

function FWRocket2Image::onCharge(%this, %obj, %slot)
{
	%obj.playthread(2, spearReady);
}

function FWRocket2Image::onAbortCharge(%this, %obj, %slot)
{
	%obj.playthread(2, root);
}

function FWRocket2Image::onFire(%this, %obj, %slot)
{
	%obj.playthread(2, spearThrow);
//	Parent::onFire(%this, %obj, %slot);

  %projectile = %this.projectile; 
  %initPos = %obj.getMuzzlePoint(%slot); 
  %muzzleVector = %obj.getMuzzleVector(%slot); 
  %objectVelocity = %obj.getVelocity(); 
  %muzzleVelocity = VectorAdd(VectorScale(%muzzleVector, %projectile.muzzleVelocity),VectorScale(%objectVelocity,  
%projectile.velInheritFactor)); 
  %p = new (%this.projectileType)()  
   { 
          dataBlock        = %projectile; 
          initialVelocity  = %muzzleVelocity; 
          initialPosition  = %initPos; 
          sourceObject     = %obj; 
          sourceSlot       = %slot; 
          client           = %obj.client; 
     }; 
     MissionCleanup.add(%p);
   echo("Projectile Type:"@%this.ProjectileType);
   echo("dataBlock:"@%projectile);
   echo("initialVelocity:"@%muzzleVelocity);
   echo("initialPosition:"@%initPos);
   echo("sourceObject:"@%obj);
   echo("sourceSlot:"@%slot);
   echo("client:"@%obj.client);
 echo(%projectile);
   %projectile.schedule(4000,MakeExplosion, %p, %projectile); 
   %projectile.schedule(1000,MakeExplosion, %p); 
}

function FWRocket2Projectile::MakeExplosion2(%this, %obj, %projectile) {    
if(isObject(%obj))
{
  %pos = %obj.getPosition(); 

  %p = new explosion() {       
         dataBlock = FWRocket2Explosion3;         
         position = %pos;   }; 


}
}

function FWRocket2Projectile::MakeExplosion(%this, %obj, %projectile) {    
if(isObject(%obj))
{
  %pos = %obj.getPosition(); 
  %obj.delete();
  %p = new explosion() {       
         dataBlock = FWRocket2Explosion;         
         position = %pos;   }; 
  // %projectile.schedule(500,MakeExplosion, %obj, %projectile); 

}
  }