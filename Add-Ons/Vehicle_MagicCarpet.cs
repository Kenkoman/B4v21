if(!isObject(jeepExplosionSound))
{
   exec("./Support_Jeep.cs");
}

datablock ExplosionData(carpetExplosion: jeepExplosion)
{
   debrisNum = 0;
};

datablock ProjectileData(carpetExplosionProjectile)
{
   directDamage        = 0;
   radiusDamage        = 0;
   damageRadius        = 0;
   explosion           = carpetExplosion;

   directDamageType  = $DamageType::JeepExplosion;
   radiusDamageType  = $DamageType::JeepExplosion;

   explodeOnDeath		= 1;

   armingDelay         = 0;
   lifetime            = 10;
};


datablock ExplosionData(carpetFinalExplosion)
{
     //explosionShape = "";
   lifeTimeMS = 150;

   soundProfile = jeepExplosionSound;
   
   emitter[0] = jeepFinalExplosionEmitter3;
   emitter[1] = jeepFinalExplosionEmitter2;

   particleEmitter = jeepFinalExplosionEmitter;
   particleDensity = 20;
   particleRadius = 1.0;

   debris = jeepDebris;
   debrisNum = 0;
   debrisNumVariance = 0;
   debrisPhiMin = 0;
   debrisPhiMax = 360;
   debrisThetaMin = 0;
   debrisThetaMax = 20;
   debrisVelocity = 18;
   debrisVelocityVariance = 3;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = true;
   camShakeFreq = "7.0 8.0 7.0";
   camShakeAmp = "10.0 10.0 10.0";
   camShakeDuration = 0.75;
   camShakeRadius = 15.0;

   // Dynamic light
   lightStartRadius = 0;
   lightEndRadius = 20;
   lightStartColor = "0.45 0.3 0.1";
   lightEndColor = "0 0 0";

   //impulse
   impulseRadius = 15;
   impulseForce = 1000;
   impulseVertical = 2000;

   //radius damage
   radiusDamage        = 30;
   damageRadius        = 8.0;

   //burn the players?
   playerBurnTime = 5000;
};

datablock ProjectileData(carpetFinalExplosionProjectile)
{
   directDamage        = 0;
   radiusDamage        = 0;
   damageRadius        = 0;
   explosion           = carpetFinalExplosion;

   directDamageType  = $DamageType::JeepExplosion;
   radiusDamageType  = $DamageType::JeepExplosion;

   explodeOnDeath		= 1;

   armingDelay         = 0;
   lifetime            = 10;
};

datablock FlyingVehicleData(MagicCarpetVehicle)
{
   //Tagged fields for mission editor
      category = "Vehicles";
      displayName = " ";

   //Shapebase Fields
      shapeFile   = "./shapes/carpet.dts";  //.dts File
      emap        = true;
      mass        = 200;
      drag        = 1.7;
      density     = 4;
      
      maxDamage = 100.00;
      destroyedLevel = 100.00;
      energyPerDamagePoint = 160;
      speedDamageScale = 1.04;
      collDamageThresholdVel = 20.0;
      collDamageMultiplier   = 0.02;

   //Tagged fields for mounting
      minMountDist = 3;   
      numMountPoints = 7;
      mountThread[0] = "sit";
      mountThread[1] = "sit";
      mountThread[2] = "sit";
      mountThread[3] = "sit";
      mountThread[4] = "sit";
      mountThread[5] = "sit";
      mountThread[6] = "sit";
      mountThread[7] = "sit";

      lookUpLimit = 0.75;
		lookDownLimit = 0.35;

   //Vehicle Fields:
      jetForce          = 500;
      jetEnergyDrain    = 8;
      minJetEnergy      = 1;

      massCenter        = "0 0 -1";
      //massBox           = "1 1 1";
      bodyRestitution   = 0.5;
      bodyFriction      = 0.5;
      //softImpactSound   = ; //AudioProfile
      //hardImpactSound   = ; //AudioProfile

      minImpactSpeed    = 25;
      softImpactSpeed   = 25;
      hardImpactSpeed   = 50;
      minRollSpeed      = 0;
      maxSteeringAngle  = 0.785;

      maxDrag        = 40;
      minDrag        = 50;
      integration    = 4;
      collisionTol   = 0.1;
      contactTol     = 0.1;

      cameraRoll     = false;
      cameraMaxDist  = 13;        
      cameraLag      = 0.0;
      cameraDecay    = 0.0;
      cameraOffset   = 2.5;
      cameraTilt     = 0.0;

      //dustEmitter       = ; //ParticleEmitterData
      triggerDustHeight = 3.0;
      dustHeight        = 1.0;

      numDmgEmitterAreas   = 0;
      
      damageEmitter[0] = JeepBurnEmitter;
      damageEmitterOffset[0] = "0.0 0.0 0.0 ";
      damageLevelTolerance[0] = 0.99;

      damageEmitter[1] = JeepBurnEmitter;
      damageEmitterOffset[1] = "0.0 0.0 0.0 ";
      damageLevelTolerance[1] = 1.0;

      //splashEmitter[0]        = ; //ParticleEmitterData

      splashFreqMod     = 300.0;
      splashVelEpsilon  = 0.50;

      exitSplashSoundVelocity    = 2.0;
      softSplashSoundVelocity    = 1.0;
      mediumSplashSoundVelocity  = 2.0;
      hardSplashSoundVelocity    = 3.0;
      //exitingWater               = ;   //AudioProfile
      //impactWaterEasy            = ;   //AudioProfile
      //impactWaterMedium          = ;   //AudioProfile
      //impactWaterHard            = ;   //AudioProfile
      //waterWakeSound             = ;   //AudioProfile

      collDamageThresholdVel  = 20;
      collDamageMultiplier    = 0.05;

   //For Wrench Gui
      uiName   = "Magic Carpet";
      rideAble = true;
      paintable = true;
		
   //Flying vehicle fields
      //jetSound = ;      //AudioProfile
      //engineSound = ;   //AudioProfile

      maneuveringForce        = 6400;
      horizontalSurfaceForce  = 20;
      verticalSurfaceForce    = 20;
      autoInputDamping        = 0.55;
      steeringForce           = 1500;
      steeringRollForce       = 200;
      rollForce               = 10;
      autoAngularForce        = 1400;
      rotationalDrag          = 20;
      autoLinearForce         = 100;
      maxAutoSpeed            = 10;
      hoverHeight             = 2.0;
      createHoverHeight       = 2.0;

      //forwardJetEmitter    = ; //ParticleEmitterData
      //backwardJetEmitter   = ; //ParticleEmitterData
      //downJetEmitter       = ; //ParticleEmitterData
      //trailEmitter         = ; //ParticleEmitterData

      minTrailSpeed        = 1;
      vertThrustMultiple   = 1.0;
   
   //Tagged fields for damage
      initialExplosionProjectile = carpetExplosionProjectile;
      initialExplosionOffset = 0;         //offset only uses a z value for now

      burnTime = 500;

      finalExplosionProjectile = carpetFinalExplosionProjectile;
      finalExplosionOffset = 0.5;          //offset only uses a z value for now

      minRunOverSpeed    = 2;   //how fast you need to be going to run someone over (do damage)
      runOverDamageScale = 5;   //when you run over someone, speed * runoverdamagescale = damage amt
      runOverPushScale   = 1.2; //how hard a person you're running over gets pushed
};


