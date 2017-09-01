function flamebigImage::shootParticles(%this, %obj, %slot)
{
   %projectile=getrandom(1);  
   switch (%projectile) {
     case 0:
       %projectile=flame1Projectile;
     }
   %muzzleVector = %obj.getMuzzleVector(%slot);
   %objectVelocity = %obj.getVelocity();
   %muzzleVelocity = VectorAdd(
      VectorScale(%muzzleVector, %projectile.muzzleVelocity),
      VectorScale(%objectVelocity, %projectile.velInheritFactor));
   %p = new (%this.projectileType)() {
      dataBlock        = %projectile;
      initialVelocity  = %muzzleVelocity;
      initialPosition  = %obj.getMuzzlePoint(%slot);
      sourceObject     = %obj;
      sourceSlot       = %slot;
      client           = %obj.client;
   };

   MissionCleanup.add(%p);
   return %p;
}

datablock ParticleData(flame1part)
{
    dragCoefficient = 0;
    gravityCoefficient = -0.925519;
    windCoefficient = 0.5;
    inheritedVelFactor = 0;
    constantAcceleration = -2;
    lifetimeMS = 1344;
    lifetimeVarianceMS = 0;
    useInvAlpha = 0;
    spinRandomMin = -750;
    spinRandomMax = 0;
    textureName = "~/data/shapes/weapons/plasma.png";
    spinSpeed = 0;
    times[0] = 0;
    times[1] = 0.537255;
    times[2] = 1;
    colors[0] = "1.000000 1.000000 0.000000 1.000000";
    colors[1] = "1.000000 0.500000 0.000000 1.000000";
    colors[2] = "1.000000 0.000000 0.000000 1.000000";
    sizes[0] = 12.9921;
    sizes[1] = 9.70213;
    sizes[2] = 2.67045;
};

datablock ParticleEmitterData(flame1Emitter)
{
    ejectionPeriodMS = 50;
    periodVarianceMS = 0;
    ejectionVelocity = 1;
    velocityVariance = 1;
    ejectionOffset = 0;
    thetaMin = 90;
    thetaMax = 90;
    phiReferenceVel = 0;
    phiVariance = 0;
    overrideAdvances = 0;
    orientParticles= 0;
    orientOnVelocity = 1;
    particles = "flame1part";
};


datablock ProjectileData(flame1Projectile) {
   areaImpulse = 0;
   directDamage        = 0;
   radiusDamage        = 0;
   damageRadius        = 5.0 ;
   particleEmitter     = flame1Emitter;
   muzzleVelocity      = 0;
   velInheritFactor    = 0.0;
   armingDelay         = 100;
   isFluid             = true;
   lifetime            = 500;
   fadeDelay           = 5000;
   bounceElasticity    = 0.0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0.10;
   hasLight    = true;
   lightRadius = 10;
   lightColor  = "1.0 0.65 0.0";
};

datablock ShapeBaseImageData(flamebigImage)
{
   shapeFile = "~/data/shapes/dummy1.dts";
   mountPoint = 5;
   Offset = "0 0 0"; // We're not going to need this
   correctMuzzleVector = false; // We're not going to need this
   className = "WeaponImage";
   item = " ";
   ammo = " "; // We're not going to need this
   projectile = flame1Projectile; // We're not going to need this
   projectileType = Projectile;
   stateName[0]                     = "Preactivate";
   stateTimeoutValue[0]             = 0.1;
   stateTransitionOnTimeout[0]      = "Preactivate";
   stateScript[0]                   = "shootParticles";
   stateName[1]                     = "Ready";
   stateTransitionOnTriggerDown[2]  = "Preactivate";
};   

function flameImage::shootParticles(%this, %obj, %slot)
{
   %projectile=getrandom(1);  
   switch (%projectile) {
     case 0:
       %projectile=flame2Projectile;
     }
   %muzzleVector = %obj.getMuzzleVector(%slot);
   %objectVelocity = %obj.getVelocity();
   %muzzleVelocity = VectorAdd(
      VectorScale(%muzzleVector, %projectile.muzzleVelocity),
      VectorScale(%objectVelocity, %projectile.velInheritFactor));
   %p = new (%this.projectileType)() {
      dataBlock        = %projectile;
      initialVelocity  = %muzzleVelocity;
      initialPosition  = %obj.getMuzzlePoint(%slot);
      sourceObject     = %obj;
      sourceSlot       = %slot;
      client           = %obj.client;
   };

   MissionCleanup.add(%p);
   return %p;
}

datablock ParticleData(flame2part)
{
    dragCoefficient = 0;
    gravityCoefficient = -1.10623;
    windCoefficient = 0.5;
    inheritedVelFactor = 0;
    constantAcceleration = -2;
    lifetimeMS = 800;
    lifetimeVarianceMS = 0;
    useInvAlpha = 0;
    spinRandomMin = -750;
    spinRandomMax = 0;
    textureName = "~/data/shapes/weapons/plasma.png";
    spinSpeed = 0;
    times[0] = 0;
    times[1] = 0.537255;
    times[2] = 1;
    colors[0] = "1.000000 1.000000 0.000000 1.000000";
    colors[1] = "1.000000 0.500000 0.000000 1.000000";
    colors[2] = "1.000000 0.000000 0.000000 1.000000";
    sizes[0] = 4.95331;
    sizes[1] = 3.71422;
    sizes[2] = 1.22383;
};

datablock ParticleEmitterData(flame2Emitter)
{
    ejectionPeriodMS = 50;
    periodVarianceMS = 0;
    ejectionVelocity = 1;
    velocityVariance = 1;
    ejectionOffset = 0;
    thetaMin = 90;
    thetaMax = 90;
    phiReferenceVel = 0;
    phiVariance = 0;
    overrideAdvances = 0;
    orientParticles= 0;
    orientOnVelocity = 1;
    particles = "flame2part";
};



datablock ProjectileData(flame2Projectile) {
   areaImpulse = 0;
   directDamage        = 0;
   radiusDamage        = 0;
   damageRadius        = 5.0 ;
   particleEmitter     = flame2Emitter;
   muzzleVelocity      = 0;
   velInheritFactor    = 0.0;
   armingDelay         = 100;
   isFluid             = true;
   lifetime            = 500;
   fadeDelay           = 5000;
   bounceElasticity    = 0.0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0.10;
   hasLight    = true;
   lightRadius = 5;
   lightColor  = "1.0 0.65 0.0";
};

datablock ShapeBaseImageData(flameImage)
{
   shapeFile = "~/data/shapes/dummy1.dts";
   mountPoint = 5;
   Offset = "0 0 0"; // We're not going to need this
   correctMuzzleVector = false; // We're not going to need this
   className = "WeaponImage";
   item = " ";
   ammo = " "; // We're not going to need this
   projectile = flame2Projectile; // We're not going to need this
   projectileType = Projectile;
   stateName[0]                     = "Preactivate";
   stateTimeoutValue[0]             = 0.1;
   stateTransitionOnTimeout[0]      = "Preactivate";
   stateScript[0]                   = "shootParticles";
   stateName[1]                     = "Ready";
   stateTransitionOnTriggerDown[2]  = "Preactivate";
};   

function flamesmallImage::shootParticles(%this, %obj, %slot)
{
   %projectile=getrandom(1);  
   switch (%projectile) {
     case 0:
       %projectile=flame3Projectile;
     }
   %muzzleVector = %obj.getMuzzleVector(%slot);
   %objectVelocity = %obj.getVelocity();
   %muzzleVelocity = VectorAdd(
      VectorScale(%muzzleVector, %projectile.muzzleVelocity),
      VectorScale(%objectVelocity, %projectile.velInheritFactor));
   %p = new (%this.projectileType)() {
      dataBlock        = %projectile;
      initialVelocity  = %muzzleVelocity;
      initialPosition  = %obj.getMuzzlePoint(%slot);
      sourceObject     = %obj;
      sourceSlot       = %slot;
      client           = %obj.client;
   };

   MissionCleanup.add(%p);
   return %p;
}

datablock ParticleData(flame3part)
{
    dragCoefficient = 0;
    gravityCoefficient = -0.25641;
    windCoefficient = 0.5;
    inheritedVelFactor = 0;
    constantAcceleration = -2;
    lifetimeMS = 480;
    lifetimeVarianceMS = 0;
    useInvAlpha = 0;
    spinRandomMin = -750;
    spinRandomMax = 0;
    textureName = "~/data/shapes/weapons/plasma.png";
    spinSpeed = 0;
    times[0] = 0;
    times[1] = 0.537255;
    times[2] = 1;
    colors[0] = "1.000000 1.000000 0.000000 1.000000";
    colors[1] = "1.000000 0.500000 0.000000 1.000000";
    colors[2] = "1.000000 0.000000 0.000000 1.000000";
    sizes[0] = 0;
    sizes[1] = 0.592077;
    sizes[2] = 0;
};

datablock ParticleEmitterData(flame3Emitter)
{
    ejectionPeriodMS = 50;
    periodVarianceMS = 0;
    ejectionVelocity = 1;
    velocityVariance = 1;
    ejectionOffset = 0;
    thetaMin = 90;
    thetaMax = 90;
    phiReferenceVel = 0;
    phiVariance = 0;
    overrideAdvances = 0;
    orientParticles= 0;
    orientOnVelocity = 1;
    particles = "flame3part";
};



datablock ProjectileData(flame3Projectile) {
   areaImpulse = 0;
   directDamage        = 0;
   radiusDamage        = 0;
   damageRadius        = 5.0 ;
   particleEmitter     = flame3Emitter;
   muzzleVelocity      = 0;
   velInheritFactor    = 0.0;
   armingDelay         = 100;
   isFluid             = true;
   lifetime            = 500;
   fadeDelay           = 5000;
   bounceElasticity    = 0.0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0.10;
   hasLight    = true;
   lightRadius = 1;
   lightColor  = "1.0 0.65 0.0";
};

datablock ShapeBaseImageData(flamesmallImage)
{
   shapeFile = "~/data/shapes/dummy1.dts";
   mountPoint = 5;
   Offset = "0 0 0"; // We're not going to need this
   correctMuzzleVector = false; // We're not going to need this
   className = "WeaponImage";
   item = " ";
   ammo = " "; // We're not going to need this
   projectile = flame3Projectile; // We're not going to need this
   projectileType = Projectile;
   stateName[0]                     = "Preactivate";
   stateTimeoutValue[0]             = 0.1;
   stateTransitionOnTimeout[0]      = "Preactivate";
   stateScript[0]                   = "shootParticles";
   stateName[1]                     = "Ready";
   stateTransitionOnTriggerDown[2]  = "Preactivate";
};   

function ringoffireImage::shootParticles(%this, %obj, %slot)
{
   %projectile=getrandom(1);  
   switch (%projectile) {
     case 0:
       %projectile=flame4Projectile;
     }
   %muzzleVector = %obj.getMuzzleVector(%slot);
   %objectVelocity = %obj.getVelocity();
   %muzzleVelocity = VectorAdd(
      VectorScale(%muzzleVector, %projectile.muzzleVelocity),
      VectorScale(%objectVelocity, %projectile.velInheritFactor));
   %p = new (%this.projectileType)() {
      dataBlock        = %projectile;
      initialVelocity  = %muzzleVelocity;
      initialPosition  = %obj.getMuzzlePoint(%slot);
      sourceObject     = %obj;
      sourceSlot       = %slot;
      client           = %obj.client;
   };

   MissionCleanup.add(%p);
   return %p;
}

datablock ParticleData(RingofFireParticle)
{
    dragCoefficient = 0;
    gravityCoefficient = -0.915751;
    windCoefficient = 0.5;
    inheritedVelFactor = 0;
    constantAcceleration = 0;
    lifetimeMS = 1152;
    lifetimeVarianceMS = 0;
    useInvAlpha = 0;
    spinRandomMin = -649;
    spinRandomMax = 13;
    textureName = "~/data/shapes/weapons/plasma.png";
    spinSpeed = 0;
    times[0] = 0;
    times[1] = 0.74902;
    times[2] = 1;
    colors[0] = "1.000000 1.000000 0.000000 1.000000";
    colors[1] = "1.000000 0.500000 0.000000 1.000000";
    colors[2] = "1.000000 0.000000 0.000000 1.000000";
    sizes[0] = 3.07636;
    sizes[1] = 2.02344;
    sizes[2] = 1.44052;
};

datablock ParticleEmitterData(RingofFireEmitter)
{
    ejectionPeriodMS = 1;
    periodVarianceMS = 0;
    ejectionVelocity = 2;
    velocityVariance = 2;
    ejectionOffset = 10;
    thetaMin = 90;
    thetaMax = 90;
    phiReferenceVel = 360;
    phiVariance = 360;
    overrideAdvances = 1;
    orientParticles= 0;
    orientOnVelocity = 1;
    particles = "RingofFireParticle";
};



datablock ProjectileData(flame4Projectile) {
   areaImpulse = 0;
   directDamage        = 0;
   radiusDamage        = 0;
   damageRadius        = 5.0 ;
   particleEmitter     = RingofFireEmitter;
   muzzleVelocity      = 0;
   velInheritFactor    = 0.0;
   armingDelay         = 100;
   isFluid             = true;
   lifetime            = 500;
   fadeDelay           = 5000;
   bounceElasticity    = 0.0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0.10;
   hasLight    = true;
   lightRadius = 10;
   lightColor  = "1.0 0.65 0.0";
};

datablock ShapeBaseImageData(ringoffireImage)
{
   shapeFile = "~/data/shapes/dummy1.dts";
   mountPoint = 5;
   Offset = "0 0 0"; // We're not going to need this
   correctMuzzleVector = false; // We're not going to need this
   className = "WeaponImage";
   item = " ";
   ammo = " "; // We're not going to need this
   projectile = flame4Projectile; // We're not going to need this
   projectileType = Projectile;
   stateName[0]                     = "Preactivate";
   stateTimeoutValue[0]             = 0.1;
   stateTransitionOnTimeout[0]      = "Preactivate";
   stateScript[0]                   = "shootParticles";
   stateName[1]                     = "Ready";
   stateTransitionOnTriggerDown[2]  = "Preactivate";
};   

function SmokebigImage::shootParticles(%this, %obj, %slot)
{
   %projectile=getrandom(1);  
   switch (%projectile) {
     case 0:
       %projectile=flame5Projectile;
     }
   %muzzleVector = %obj.getMuzzleVector(%slot);
   %objectVelocity = %obj.getVelocity();
   %muzzleVelocity = VectorAdd(
      VectorScale(%muzzleVector, %projectile.muzzleVelocity),
      VectorScale(%objectVelocity, %projectile.velInheritFactor));
   %p = new (%this.projectileType)() {
      dataBlock        = %projectile;
      initialVelocity  = %muzzleVelocity;
      initialPosition  = %obj.getMuzzlePoint(%slot);
      sourceObject     = %obj;
      sourceSlot       = %slot;
      client           = %obj.client;
   };

   MissionCleanup.add(%p);
   return %p;
}

datablock ParticleData(SmokebigParticle)
{
    dragCoefficient = 0;
    gravityCoefficient = -0.710623;
    windCoefficient = 0.5;
    inheritedVelFactor = 0;
    constantAcceleration = 0;
    lifetimeMS = 5120;
    lifetimeVarianceMS = 192;
    useInvAlpha = 1;
    spinRandomMin = 0;
    spinRandomMax = 50;
    textureName = "tbm/data/particles/cloud.png";
    spinSpeed = 0;
    times[0] = 0;
    times[1] = 1;
    colors[0] = "0.000000 0.000000 0.000000 0.141732";
    colors[1] = "0.307087 0.307087 0.291339 0.055118";
    sizes[0] = 11.5516;
    sizes[1] = 25.615;
};

datablock ParticleEmitterData(SmokebigEmitter)
{
    ejectionPeriodMS = 108;
    periodVarianceMS = 58;
    ejectionVelocity = 1;
    velocityVariance = 1;
    ejectionOffset = 3.47;
    thetaMin = 90;
    thetaMax = 90;
    phiReferenceVel = 360;
    phiVariance = 360;
    overrideAdvances = 0;
    orientParticles= 0;
    orientOnVelocity = 1;
    particles = "SmokebigParticle";
};



datablock ProjectileData(flame5Projectile) {
   areaImpulse = 0;
   directDamage        = 0;
   radiusDamage        = 0;
   damageRadius        = 5.0 ;
   particleEmitter     = SmokebigEmitter;
   muzzleVelocity      = 0;
   velInheritFactor    = 0.0;
   armingDelay         = 100;
   isFluid             = true;
   lifetime            = 500;
   fadeDelay           = 5000;
   bounceElasticity    = 0.0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0.10;
   hasLight    = false;
   lightRadius = 10;
   lightColor  = "1.0 0.65 0.0";
};

datablock ShapeBaseImageData(Smokebigimage)
{
   shapeFile = "~/data/shapes/dummy1.dts";
   mountPoint = 5;
   Offset = "0 0 0"; // We're not going to need this
   correctMuzzleVector = false; // We're not going to need this
   className = "WeaponImage";
   item = " ";
   ammo = " "; // We're not going to need this
   projectile = flame5Projectile; // We're not going to need this
   projectileType = Projectile;
   stateName[0]                     = "Preactivate";
   stateTimeoutValue[0]             = 0.1;
   stateTransitionOnTimeout[0]      = "Preactivate";
   stateScript[0]                   = "shootParticles";
   stateName[1]                     = "Ready";
   stateTransitionOnTriggerDown[2]  = "Preactivate";
};   

//Client Visable Lights - to strobe/pulse, mount using a turret and set the milliseconds between shots to a high number.

datablock ProjectileData(redlightProjectile) {
   areaImpulse = 0;
   directDamage        = 0;
   radiusDamage        = 0;
   damageRadius        = 5.0 ;
//   particleEmitter     = ;
   muzzleVelocity      = 0;
   velInheritFactor    = 0.0;
   armingDelay         = 100;
   isFluid             = true;
   lifetime            = 500;
   fadeDelay           = 5000;
   bounceElasticity    = 0.0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0;
   hasLight    = true;
   lightRadius = 10;
   lightColor  = "1 0 0";
};

datablock ProjectileData(greenlightProjectile) {
   areaImpulse = 0;
   directDamage        = 0;
   radiusDamage        = 0;
   damageRadius        = 5.0 ;
//   particleEmitter     = ;
   muzzleVelocity      = 0;
   velInheritFactor    = 0.0;
   armingDelay         = 100;
   isFluid             = true;
   lifetime            = 500;
   fadeDelay           = 5000;
   bounceElasticity    = 0.0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0;
   hasLight    = true;
   lightRadius = 10;
   lightColor  = "0 1 0";
};

datablock ProjectileData(bluelightProjectile) {
   areaImpulse = 0;
   directDamage        = 0;
   radiusDamage        = 0;
   damageRadius        = 5.0 ;
//   particleEmitter     = ;
   muzzleVelocity      = 0;
   velInheritFactor    = 0.0;
   armingDelay         = 100;
   isFluid             = true;
   lifetime            = 500;
   fadeDelay           = 5000;
   bounceElasticity    = 0.0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0;
   hasLight    = true;
   lightRadius = 10;
   lightColor  = "0 0 1";
};

datablock ShapeBaseImageData(constantredlight)
{
   shapeFile = "~/data/shapes/dummy1.dts";
   emap = true;

   mountPoint = 0;
    
   offset = "0 0 0";

   correctMuzzleVector = false;

   className = "WeaponImage";

   item = constantredlight;
   ammo = " ";
   projectile = redlightProjectile;
   projectileType = Projectile;

   melee = true;

   armReady = false;

	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.1;
	stateTransitionOnTimeout[0]       = "Activate";
	stateFire[0]                    = true;
	stateSequence[0]                = "Fire";
};

datablock ShapeBaseImageData(constantgreenlight)
{
   shapeFile = "~/data/shapes/dummy1.dts";
   emap = true;

   mountPoint = 0;
    
   offset = "0 0 0";

   correctMuzzleVector = false;

   className = "WeaponImage";

   item = constantgreenlight;
   ammo = " ";
   projectile = greenlightProjectile;
   projectileType = Projectile;

   melee = true;

   armReady = false;

	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.1;
	stateTransitionOnTimeout[0]       = "Activate";
	stateFire[0]                    = true;
	stateSequence[0]                = "Fire";
};

datablock ShapeBaseImageData(constantbluelight)
{
   shapeFile = "~/data/shapes/dummy1.dts";
   emap = true;

   mountPoint = 0;
    
   offset = "0 0 0";

   correctMuzzleVector = false;

   className = "WeaponImage";

   item = constantbluelight;
   ammo = " ";
   projectile = bluelightProjectile;
   projectileType = Projectile;

   melee = true;

   armReady = false;

	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.1;
	stateTransitionOnTimeout[0]       = "Activate";
	stateFire[0]                    = true;
	stateSequence[0]                = "Fire";
};
