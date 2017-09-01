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
    windCoefficient = 3.52941;
    inheritedVelFactor = 0;
    constantAcceleration = -2;
    lifetimeMS = 1344;
    lifetimeVarianceMS = 448;
    useInvAlpha = 0;
    spinRandomMin = -750;
    spinRandomMax = 0;
    textureName = "tbm/data/particles/chunk.png";
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
    ejectionPeriodMS = 137;
    periodVarianceMS = 4;
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

