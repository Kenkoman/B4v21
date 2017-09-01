datablock ParticleData(FlamethrowerTrailParticle)
{
textureName = "tbm/data/shapes/weapons/plasma";
	useInvAlpha =  false;

	dragCoeffiecient = 300.0;
	inheritedVelFactor = 1.0;
      spinRandomMin = -750;
      spinRandomMax = 0;

	lifetimeMS = 1000;
	
	lifetimeVarianceMS = 0;

	times[0] = 1;
	times[1] = 1;
	times[2] = 1;

	colors[0] = "0.5 0.3 0.05 0.8";
	colors[1] = "0.5 0.2 0.05 0.9";
	colors[2] = "0.5 0.05 0.05 1";
	sizes[0] = 1;
	sizes[1] = 10;
	sizes[2] = 25;
};

datablock ParticleEmitterData(FlamethrowerTrailEmitter)
{
   ejectionPeriodMS = 100;
   periodVarianceMS = 0;

   ejectionVelocity = 0.5;
   velocityVariance = 0.10;

   ejectionOffset = 0;

   thetaMin         = 0.0;
   thetaMax         = 90.0;  
   orientOnVelocity = 1;
   particles = FlamethrowerTrailParticle;
};


datablock ExplosionData(FlamethrowerExplosion)
{
   //explosionShape = "";
	soundProfile = FlamethrowerFireSound;

   lifeTimeMS = 150;

   //particleEmitter = GLFireParticleEmitter;
	//particleDensity = 25;
	//particleRadius  = 4;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = false;
   camShakeFreq = "10.0 11.0 10.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 0.5;
   camShakeRadius = 10.0;

   // Dynamic light
   lightStartRadius = 10;
   lightEndRadius = 10;
   lightStartColor = "0.2 0.055 0.0 0.8";
   lightEndColor = "0 0 0";
};


//projectile
datablock ProjectileData(FlamethrowerProjectile)
{
   projectileShapeName = "";
   explosion           = FlamethrowerExplosion;
   particleEmitter     = FlamethrowerTrailEmitter;
   muzzleVelocity      = 40;

   dragCoeffiecient = 50.0;

   armingDelay         = 0;
   lifetime            = 1000;
   fadeDelay           = 0;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 0.2;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "0.2 0.1 0.0 0.8";
};


//////////
// item //
//////////
datablock ItemData(Flamethrower)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/weapons/Flamethrower.dts";
	skinName = 'grayDark';
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = false;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a Flamethrower.';
	invName = "Flamethrower";
	image = FlamethrowerImage;
	threatlevel = "Normal";
};

addWeapon(Flamethrower);

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(FlamethrowerImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/weapons/Flamethrower.dts";
   skinName = 'brown';
   emap = false;
    
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
   item = Flamethrower;
   ammo = " ";
   projectile = FlamethrowerProjectile;
   projectileType = Projectile;

   projectileSpread = 1/1000;
   projectileSpreadWalking = 1.5/1000;
   projectileSpreadMax = 2/1000;

   directDamage        = 2;
   radiusDamage        = 1;
   damageRadius        = 1.5;
   damagetype          = '%1 got BURNED by %2';
   muzzleVelocity      = 40;
   velInheritFactor    = 1;

   deathAnimationClass = "fire";
   deathAnimation = "gooblob";
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
	stateSound[0]					= weaponSwitchSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateAllowImageChange[1]         = true;

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "Reload";
	stateTimeoutValue[2]            = 0.09;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]			= true;
	stateSound[2]					= bowFireSound;

	stateName[3]			= "Reload";
	stateSequence[3]                = "Reload";
	stateAllowImageChange[3]        = false;
	stateTimeoutValue[3]            = 0.01;
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTimeout[3]     = "Check";

	stateName[4]			= "Check";
	stateTransitionOnTriggerUp[4]	= "StopFire";
	stateTransitionOnTriggerDown[4]	= "Fire2";

	stateName[5]                    = "StopFire";
	stateTransitionOnTimeout[5]     = "Ready2";
	stateTimeoutValue[5]            = 0.01;
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;
	//stateSequence[5]                = "Reload";
	stateScript[5]                  = "onStopFire";

	stateName[6]			= "Ready2";
	stateScript[6]                  = "onFlameon";
	stateTransitionOnTimeout[6]     = "Ready2";
	stateTimeoutValue[6]            = 0.2;
	stateTransitionOnTriggerDown[6]  = "Fire2";
	stateAllowImageChange[6]         = true;

	stateName[7]                    = "Fire2";
	stateTransitionOnTimeout[7]     = "Reload";
	stateTimeoutValue[7]            = 0.01;
	stateFire[7]                    = true;
	stateAllowImageChange[7]        = false;
	stateSequence[7]                = "Fire";
	stateScript[7]                  = "onFire";
	stateWaitForTimeout[7]			= true;
	stateSound[7]					= FlamethrowerFireSound;

};

function FlamethrowerImage::onFlameon(%this, %obj, %slot)
{
	%projectile = flame3projectile;
	%spread = calculateSpread(%this, %obj);

	%shellcount = 1;

	// Create each projectile and send it on its way
	for(%shell = 0; %shell < %shellcount; %shell++) 
	{
              // Get the muzzle vector.  This is the dead straight aiming point of the gun
         %vector = %obj.getMuzzleVector(%slot);

         // Get our players velocity.  We must ensure that the players velocity is added
         //   onto the projectile
         %objectVelocity = %obj.getVelocity();

         // Determine scaled projectile vector.  This is still in a straight line as
         //   per the default example
         %vector = VectorScale(%vector, 0.1);
         %velocity = VectorAdd(%vector, %objectVelocity);

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
            initialPosition  = %obj.getMuzzlePoint(%slot);
            sourceObject     = %obj;
            sourceSlot       = %slot;
            client           = %obj.client;
         };
         MissionCleanup.add(%p);
        onAddProjectile(%projectile, %p, %this);
      }
      return %p;
}


function FlamethrowerProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	if(%col.getDatablock() == nameToID(staticIceBlock)) {
		%col.getMountNodeObject(0).flameProtect = 1;
		schedule(1000, 0, eval, %col.getMountNodeObject(0) @ ".flameProtect = 0;");
		%col.delete();
		return;
	}

	tbmcollison(%this, %obj, %col, %fade, %pos, %normal);
       tbmradiusDamage
     (%obj, VectorAdd(%pos, VectorScale(%normal, 0.01)),
      %obj.damageRadius,%obj.radiusDamage,%obj.damageType,20);

	if(%col.flameProtect)
		return;
	if(%col.getClassName() $= "Player" || %col.getClassName() $= "AIPlayer" && !getRandom(0, 2)) {
		if(!(%col.inWater && 0 <= %col.waterType && %col.waterType <= 3)) {
			%col.mountimage(flameimage,0,1,black);
			%col.clearDamageDT();
			%obj.sourceObject.deathAnim = "gooblob";
			%col.setDamageDT(1, %obj.damageType, %obj.sourceObject);
		}
		//Else, this player is in water.
	}
}