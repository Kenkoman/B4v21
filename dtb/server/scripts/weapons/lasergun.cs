//saw explosion, moved here to be exec'd first
datablock ParticleData(sawExplosionParticle)
{
   dragCoefficient      = 3;
   gravityCoefficient   = 0.2;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 500;
   lifetimeVarianceMS   = 300;
   textureName          = "~/data/shapes/weapons/spark";
   colors[0]     = "1.0 0.5 0.0 0.9";
   colors[1]     = "1.0 0.6 0.0 0.5";
   sizes[0]      = 0.025;
   sizes[1]      = 0.05;
};

datablock ParticleEmitterData(sawExplosionEmitter)
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
   particles = "sawExplosionParticle";
};

datablock ExplosionData(sawExplosion)
{
   //explosionShape = "";
   lifeTimeMS = 500;

  // soundProfile = sawHitSound;

   particleEmitter = sawExplosionEmitter;
   particleDensity = 20;
   particleRadius = 0.1;

   emitter[0] = sawExplosionEmitter;
   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = true;
   camShakeFreq = "20.0 22.0 20.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 0.5;
   camShakeRadius = 10.0;

   // Dynamic light
   lightStartRadius = 0;
   lightEndRadius = 0;
   lightStartColor = "00.0 0.2 0.6";
   lightEndColor = "0 0 0";
};

//bullet trail
datablock ParticleData(LaserGunTrailParticle)
{
	dragCoefficient		= 1.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.05;
	constantAcceleration	= 0.0;
	lifetimeMS		= 100;
	lifetimeVarianceMS	= 0;
	spinSpeed		= 0;
	spinRandomMin		= 0;
	spinRandomMax		= 0;
	useInvAlpha		= false;
	animateTexture		= false;
	textureName		= "~/data/particles/dot";


	// Interpolation variables
    colors[0] = "1.000000 1.000000 0.300000 1.000000";

    colors[1] = "1.000000 0.000000 0.000000 1.000000";
	sizes[0]	= 0.45;
	sizes[1]	= 1;
	times[0]	= 0.0;
	times[1]	= 1.0;
};

datablock ParticleEmitterData(LaserGunTrailEmitter)
{
  ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   ejectionVelocity = 0; //0.25;
   velocityVariance = 0; //0.10;
   ejectionOffset = 0;
   thetaMin         = 0.0;
   thetaMax         = 90.0;  


   particles = LaserGunTrailParticle;
};

//effects -- got rid of this, saved 3 datablocks. will use saw explosion instead.
// datablock ParticleData(LaserGunExplosionParticle)
// {
	// dragCoefficient      = 5;
	// gravityCoefficient   = 0.1;
	// inheritedVelFactor   = 0.2;
	// constantAcceleration = 0.0;
	// lifetimeMS           = 500;
	// lifetimeVarianceMS   = 300;
	// textureName          = "~/data/particles/chunk";
	// spinSpeed		= 10.0;
	// spinRandomMin		= -50.0;
	// spinRandomMax		= 50.0;
	// colors[0]     = "0.9 0.9 0.6 0.9";
	// colors[1]     = "0.9 0.5 0.6 0.0";
	// sizes[0]      = 0.25;
	// sizes[1]      = 0.0;
// };

// datablock ParticleEmitterData(LaserGunExplosionEmitter)
// {
   // ejectionPeriodMS = 7;
   // periodVarianceMS = 0;
   // ejectionVelocity = 5;
   // velocityVariance = 1.0;
   // ejectionOffset   = 0.0;
   // thetaMin         = 0;
   // thetaMax         = 90;
   // phiReferenceVel  = 0;
   // phiVariance      = 360;
   // overrideAdvance = false;
   // particles = "LaserGunExplosionParticle";
// };

// datablock ExplosionData(LaserGunExplosion)
// {
   //explosionShape = "";
	// soundProfile = bulletExplosionSound;

   // lifeTimeMS = 150;

   // particleEmitter = LaserGunExplosionEmitter;
   // particleDensity = 10;
   // particleRadius = 0.2;

   // emitter[0] = LaserGunExplosionEmitter;
   // faceViewer     = true;
   // explosionScale = "1 1 1";

   // shakeCamera = false;
   // camShakeFreq = "10.0 11.0 10.0";
   // camShakeAmp = "1.0 1.0 1.0";
   // camShakeDuration = 0.5;
   // camShakeRadius = 10.0;

   //Dynamic light
   // lightStartRadius = 0;
   // lightEndRadius = 2;
   // lightStartColor = "0.3 0.6 0.7";
   // lightEndColor = "0 0 0";
// };


//projectile
datablock ProjectileData(LaserGunProjectile)
{
   projectileShapeName = "";
   explosion           = sawExplosion;
   particleEmitter     = LaserGunTrailEmitter;
   muzzleVelocity      = 500;

   armingDelay         = 0;
   lifetime            = 500;
   fadeDelay           = 400;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 0;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";
};


//////////
// item //
//////////
datablock ItemData(LaserGun)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "tbm/data/shapes/FeedBack/lazerst.dts";
	skinName = 'black';
	rotate = true;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a LaserGun.';
	invName = "Laser Phaser";
	image = LaserGunImage;
	threatlevel = "Normal";
};

addWeapon(LaserGun);

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(LaserGunImage)
{
   // Basic Item properties
	shapeFile = "tbm/data/shapes/FeedBack/lazerst.dts";
   skinName = 'black';
   emap = true;
    
   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0.05 0.05 0";
   rotation = "1 0 0 -65";
   //eyeOffset = "0 0 0";

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = LaserGun;
   ammo = " ";
   projectile = LaserGunProjectile;
   projectileType = Projectile;

   directDamage        = 6;
   radiusDamage        = 0;
   damageRadius        = 0.5;
   damagetype          = '%1 got laser-owned by %2';
   muzzleVelocity      = 500;
   velInheritFactor    = 1;

   deathAnimationClass = "projectile";
   deathAnimation = "gooblob";
   deathAnimationPercent = 1;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = false;

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
	stateScript[0]                  = "onStopFire";

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateAllowImageChange[1]         = true;

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "Reload";
	stateTimeoutValue[2]            = 0.005;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]			= true;
	stateSound[2]					= PhaserSound;

	stateName[3]			= "Reload";
	stateSequence[3]                = "Reload";
	stateAllowImageChange[3]        = false;
	stateTimeoutValue[3]            = 0.005;
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTimeout[3]     = "Check";

	stateName[4]			= "Check";
	stateTransitionOnTriggerUp[4]	= "StopFire";
	stateTransitionOnTriggerDown[4]	= "Fire";

	stateName[5]                    = "StopFire";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 0.002;
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;
	//stateSequence[5]                = "Reload";
	stateScript[5]                  = "onStopFire";


};

function LaserGunImage::onFire(%this,%obj,%col,%fade,%pos,%normal)
{
	Parent::onFire(%this,%obj,%col,%fade,%pos,%normal);
	%obj.playthread(2, root);
}

function LaserGunImage::onStopFire(%this,%obj,%col,%fade,%pos,%normal)
{
	Parent::onStopFire(%this,%obj,%col,%fade,%pos,%normal);
	%obj.playthread(2, armreadyright);
}

function LaserGunProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	if (%col.DMShield == 1) {						//If the victim is invincible or parrying,
	      // Get the type of projectile we are gonna fire		//recreate the projectile fired.
      %projectile = HandgunProjectile;

      // Get the weapons projectile spread and ensure it is never 0
      //   (we need some spread direction even if it is extremely tiny)
      %spread = 0.1;

        %shellcount = 1;

      // Create each projectile and send it on its way
      for(%shell=0; %shell<%shellcount; %shell++)
      {
              // Get the muzzle vector.  This is the dead straight aiming point of the gun
         %vector = %col.getMuzzleVector(%slot);

         // Get our players velocity.  We must ensure that the players velocity is added
         //   onto the projectile
         %objectVelocity = %col.getVelocity();

         // Determine scaled projectile vector.  This is still in a straight line as
         //   per the default example
         %vector1 = VectorScale(%vector, %projectile.muzzleVelocity);
         %vector2 = VectorScale(%objectVelocity, %projectile.velInheritFactor);
         %velocity = VectorAdd(%vector1,%vector2);

         // Determine our random x, y and z points in our spread circle and create
         //   a spread matrix.
         %x = (getRandom() - 0.5) * 2 * 3.1415926 * %spread;
         %y = (getRandom() - 0.5) * 2 * 3.1415926 * %spread;
         %z = (getRandom() - 0.5) * 2 * 3.1415926 * %spread;
         %mat = MatrixCreateFromEuler(%x @ " " @ %y @ " " @ %z);

         // Alter our projectile vector with our spread matrix
         %velocity = MatrixMulVector(%mat, %velocity);


         // Create our projectile
         %p = new (%this.projectileType)()
         {
            dataBlock        = %projectile;
            initialVelocity  = %velocity;
            initialPosition  = %col.getMuzzlePoint(%slot);
            sourceObject     = %col;
            sourceSlot       = %slot;
            client           = %obj.client;				//This should keep the client the same as who fired the projectile,
         };										//so any code that calls upon the client still works (deathmessages, etc.)
         MissionCleanup.add(%p);
        onAddProjectile(%projectile, %p, %this);
        if(%this.recoil)
          %obj.modSpread(%this.recoil, %this.recoilSeconds);
      }

      return %p;
	}
else {
	tbmcollison(%this,%obj,%col,%fade,%pos,%normal);
	}
}