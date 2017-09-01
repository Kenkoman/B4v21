//sword.cs
datablock AudioProfile(swordDrawSound)
{
   filename    = "./sound/swordDraw.wav";
   description = AudioClosest3d;
   preload = true;
};
datablock AudioProfile(swordHitSound)
{
   filename    = "./sound/swordHit.wav";
   description = AudioClosest3d;
   preload = true;
};


//effects
datablock ParticleData(swordExplosionParticle)
{
   dragCoefficient      = 2;
   gravityCoefficient   = 1.0;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   spinRandomMin = -90;
   spinRandomMax = 90;
   lifetimeMS           = 500;
   lifetimeVarianceMS   = 300;
   textureName          = "base/data/particles/chunk";
   colors[0]     = "0.7 0.7 0.9 0.9";
   colors[1]     = "0.9 0.9 0.9 0.0";
   sizes[0]      = 0.5;
   sizes[1]      = 0.25;
};

datablock ParticleEmitterData(swordExplosionEmitter)
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   ejectionVelocity = 8;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 60;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "swordExplosionParticle";
};

datablock ExplosionData(swordExplosion)
{
   //explosionShape = "";
   lifeTimeMS = 500;

   soundProfile = swordHitSound;

   particleEmitter = swordExplosionEmitter;
   particleDensity = 10;
   particleRadius = 0.2;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = true;
   camShakeFreq = "20.0 22.0 20.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 0.5;
   camShakeRadius = 10.0;

   // Dynamic light
   lightStartRadius = 3;
   lightEndRadius = 0;
   lightStartColor = "00.0 0.2 0.6";
   lightEndColor = "0 0 0";
};


//projectile
AddDamageType("Sword",   '<bitmap:add-ons/ci/sword> %1',    '%2 <bitmap:add-ons/ci/sword> %1',1,1);
datablock ProjectileData(swordProjectile)
{
   //projectileShapeName = "~/data/shapes/arrow.dts";
   directDamage        = 35;
   directDamageType  = $DamageType::Sword;
   radiusDamageType  = $DamageType::Sword;
   explosion           = swordExplosion;
   //particleEmitter     = as;

   muzzleVelocity      = 50;
   velInheritFactor    = 1;

   armingDelay         = 0;
   lifetime            = 100;
   fadeDelay           = 70;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0.0;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";
};


//////////
// item //
//////////
datablock ItemData(swordItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./shapes/sword.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Sword";
	iconName = "./ItemIcons/sword";
	doColorShift = true;
	colorShiftColor = "0.471 0.471 0.471 1.000";

	 // Dynamic properties defined by the scripts
	image = swordImage;
	canDrop = true;
};

//function sword::onUse(%this,%user)
//{
//	//if the image is mounted already, unmount it
//	//if it isnt, mount it
//
//	%mountPoint = %this.image.mountPoint;
//	%mountedImage = %user.getMountedImage(%mountPoint); 
//	
//	if(%mountedImage)
//	{
//		//echo(%mountedImage);
//		if(%mountedImage == %this.image.getId())
//		{
//			//our image is already mounted so unmount it
//			%user.unMountImage(%mountPoint);
//		}
//		else
//		{
//			//something else is there so mount our image
//			%user.mountimage(%this.image, %mountPoint);
//		}
//	}
//	else
//	{
//		//nothing there so mount 
//		%user.mountimage(%this.image, %mountPoint);
//	}
//}

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(swordImage)
{
   // Basic Item properties
   shapeFile = "./shapes/sword.dts";
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
   correctMuzzleVector = false;

   eyeOffset = "0.7 1.2 -0.25";

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = swordItem;
   ammo = " ";
   projectile = swordProjectile;
   projectileType = Projectile;

   //melee particles shoot from eye node for consistancy
   melee = true;
   doRetraction = false;
   //raise your arm up or not
   armReady = true;

   //casing = " ";
   doColorShift = true;
   colorShiftColor = "0.471 0.471 0.471 1.000";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.5;
	stateTransitionOnTimeout[0]      = "Ready";
	stateSound[0]                    = swordDrawSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "PreFire";
	stateAllowImageChange[1]         = true;

	stateName[2]			= "PreFire";
	stateScript[2]                  = "onPreFire";
	stateAllowImageChange[2]        = false;
	stateTimeoutValue[2]            = 0.1;
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
	stateTransitionOnTriggerUp[4]	= "StopFire";
	stateTransitionOnTriggerDown[4]	= "Fire";

	
	stateName[5]                    = "StopFire";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 0.2;
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;
	stateSequence[5]                = "StopFire";
	stateScript[5]                  = "onStopFire";


};

function swordImage::onPreFire(%this, %obj, %slot)
{
	//messageAll( 'MsgClient', 'sword prefired!!!');
	//Parent::onFire(%this, %obj, %slot);
	%obj.playthread(2, armattack);
}

function swordImage::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
	//messageAll( 'MsgClient', 'stopfire');
}