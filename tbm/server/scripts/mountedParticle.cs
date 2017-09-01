//Most of these projectiles have their own muzzleVelocity and velInheritFactor fields because it's possible that other people 

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
    colors[0] = "1.000000 1.000000 0.100000 1.000000";
    colors[1] = "1.000000 0.500000 0.100000 1.000000";
    colors[2] = "1.000000 0.100000 0.100000 1.000000";
    sizes[0] = 12.9921;
    sizes[1] = 9.70213;
    sizes[2] = 2.67045;
};

datablock ParticleEmitterData(flame1Emitter)
{
    ejectionPeriodMS = 100;
    periodVarianceMS = 0;
    ejectionVelocity = 1;
    velocityVariance = 1;
    ejectionOffset = 1;
    thetaMin = 90;
    thetaMax = 90;
    phiReferenceVel = 360;
    phiVariance = 360;
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
   lifetime            = 500;
   fadeDelay           = 5000;
   bounceElasticity    = 0.0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 0.0;
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
   item = "";
   ammo = ""; // We're not going to need this
   projectile = flame1Projectile;
   projectileType = Projectile;
   stateName[0]                     = "Preactivate";
   stateTimeoutValue[0]             = 0.1;
   stateTransitionOnTimeout[0]      = "Preactivate";
   stateScript[0]                   = "onFire";
};   

datablock ParticleData(flame2part)
{
    dragCoefficient = 0;
    gravityCoefficient = -1.10623;
    windCoefficient = 0.1;
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
    colors[0] = "1.000000 1.000000 0.100000 1.000000";
    colors[1] = "1.000000 0.500000 0.100000 1.000000";
    colors[2] = "1.000000 0.100000 0.100000 1.000000";
    sizes[0] = 4.95331;
    sizes[1] = 3.71422;
    sizes[2] = 1.22383;
};

datablock ParticleEmitterData(flame2Emitter)
{
    ejectionPeriodMS = 100;
    periodVarianceMS = 0;
    ejectionVelocity = 1;
    velocityVariance = 1;
    ejectionOffset = 0.2;
    thetaMin = 90;
    thetaMax = 90;
    phiReferenceVel = 360;
    phiVariance = 360;
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
   lifetime            = 500;
   fadeDelay           = 5000;
   bounceElasticity    = 0.0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 0.0;
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
   item = "";
   ammo = ""; // We're not going to need this
   projectile = flame2Projectile;
   projectileType = Projectile;
   stateName[0]                     = "Preactivate";
   stateTimeoutValue[0]             = 0.1;
   stateTransitionOnTimeout[0]      = "Preactivate";
   stateScript[0]                   = "onFire";
   stateAllowImageChange[0]         = false;
};   

datablock ParticleData(flame3part)
{
    dragCoefficient = 0;
    gravityCoefficient = -0.25641;
    windCoefficient = 0;
    inheritedVelFactor = 1;
    constantAcceleration = -2;
    lifetimeMS = 480;
    lifetimeVarianceMS = 0;
    useInvAlpha = 0;
    spinRandomMin = -750;
    spinRandomMax = 0;
    textureName = "~/data/shapes/weapons/plasma.png";
    spinSpeed = 0;
    times[0] = 0;
    times[1] = 0.3;
    times[2] = 1;
    colors[0] = "1.000000 1.000000 0.100000 1.000000";
    colors[1] = "1.000000 0.500000 0.100000 1.000000";
    colors[2] = "1.000000 0.100000 0.100000 1.000000";
    sizes[0] = 0;
    sizes[1] = 0.3;
    sizes[2] = 0;
};

datablock ParticleEmitterData(flame3Emitter)
{
    ejectionPeriodMS = 10;
    periodVarianceMS = 0;
    ejectionVelocity = 0;
    velocityVariance = 0;
    ejectionOffset = 0.05;
    thetaMin = 90;
    thetaMax = 90;
    phiReferenceVel = 360;
    phiVariance = 360;
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
   velInheritFactor    = 1.0;
   armingDelay         = 100;
   lifetime            = 500;
   fadeDelay           = 5000;
   bounceElasticity    = 0.0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 0.0;
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
   item = "";
   ammo = ""; // We're not going to need this
   projectile = flame3Projectile;
   projectileType = Projectile;
   stateName[0]                     = "Preactivate";
   stateTimeoutValue[0]             = 0.1;
   stateTransitionOnTimeout[0]      = "Preactivate";
   stateScript[0]                   = "onFire";
};   

datablock ParticleData(RingofFireParticle)
{
    dragCoefficient = 0;
    gravityCoefficient = -1;
    windCoefficient = 0.5;
    inheritedVelFactor = 0;
    constantAcceleration = 0;
    lifetimeMS = 1152;
    lifetimeVarianceMS = 0;
    useInvAlpha = 0;
    spinRandomMin = -750;
    spinRandomMax = 0;
    textureName = "~/data/shapes/weapons/plasma.png";
    spinSpeed = 0;
    times[0] = 0;
    times[1] = 0.74902;
    times[2] = 1;
    colors[0] = "1.000000 1.000000 0.100000 1.000000";
    colors[1] = "1.000000 0.500000 0.100000 1.000000";
    colors[2] = "1.000000 0.100000 0.100000 1.000000";
    sizes[0] = 3.07636;
    sizes[1] = 2.02344;
    sizes[2] = 1.44052;
};

datablock ParticleEmitterData(RingofFireEmitter)
{
    ejectionPeriodMS = 1;
    periodVarianceMS = 0;
    ejectionVelocity = 0;
    velocityVariance = 0;
    ejectionOffset = 32;
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
   lifetime            = 500;
   fadeDelay           = 5000;
   bounceElasticity    = 0.0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 0.0;
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
   item = "";
   ammo = ""; // We're not going to need this
   projectile = flame4Projectile;
   projectileType = Projectile;
   stateName[0]                     = "Preactivate";
   stateTimeoutValue[0]             = 0.1;
   stateTransitionOnTimeout[0]      = "Preactivate";
   stateScript[0]                   = "onFire";
};   

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
   lifetime            = 500;
   fadeDelay           = 5000;
   bounceElasticity    = 0.0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 0.0;
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
   item = "";
   ammo = ""; // We're not going to need this
   projectile = flame5Projectile;
   projectileType = Projectile;
   stateName[0]                     = "Preactivate";
   stateTimeoutValue[0]             = 0.1;
   stateTransitionOnTimeout[0]      = "Preactivate";
   stateScript[0]                   = "onFire";
};   


datablock ParticleData(MortarCannonTrailParticle)
{
	dragCoefficient		= 3.0;
	windCoefficient		= 0.05;
	gravityCoefficient	= -0.5;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 4000;
	lifetimeVarianceMS	= 0;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= true;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "~/data/particles/cloud";
	//animTexName		= " ";

	// Interpolation variables
	colors[0]	= "0.45 0.45 0.45 0.5";
	colors[1]	= "0.5 0.5 0.5 0.0";
	sizes[0]	= 0.5;
	sizes[1]	= 5.0;
	times[0]	= 0.0;
	times[1]	= 1.0;
};

datablock ParticleEmitterData(MortarCannonTrailEmitter)
{
   ejectionPeriodMS = 50;
   periodVarianceMS = 0;

   ejectionVelocity = 0; //0.25;
   velocityVariance = 0; //0.10;

   ejectionOffset = 0.45;
   phiReferenceVel = 360;
   phiVariance = 360;
   thetaMin         = 0.0;
   thetaMax         = 90.0;  

   particles = MortarCannonTrailParticle;
};

datablock ProjectileData(flame6Projectile) {
   areaImpulse = 0;
   directDamage        = 0;
   radiusDamage        = 0;
   damageRadius        = 5.0 ;
   particleEmitter     = MortarCannonTrailEmitter;
   muzzleVelocity      = 0;
   velInheritFactor    = 0.0;
   armingDelay         = 500;
   isFluid             = true;
   lifetime            = 100;
   fadeDelay           = 5000;
   bounceElasticity    = 0.0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 0.0;
   hasLight    = false;
   lightRadius = 10;
   lightColor  = "1.0 0.65 0.0";
};

datablock ShapeBaseImageData(Smokeimage)
{
   shapeFile = "~/data/shapes/dummy1.dts";
   mountPoint = 5;
   Offset = "0 0 0"; // We're not going to need this
   correctMuzzleVector = false; // We're not going to need this
   className = "WeaponImage";
   item = "";
   ammo = ""; // We're not going to need this
   projectile = flame5Projectile;
   projectileType = Projectile;
   stateName[0]                     = "Preactivate";
   stateTimeoutValue[0]             = 0.1;
   stateTransitionOnTimeout[0]      = "Preactivate";
   stateScript[0]                   = "onFire";
};   

//Client Visible Lights - no pulsing, sorry

datablock ProjectileData(redlightProjectile) {
   projectileShapeName = "~/data/shapes/halo/red/halo.dts";
   areaImpulse = 0;
   directDamage        = 0;
   radiusDamage        = 0;
   damageRadius        = 5.0 ;
//   particleEmitter     = redlightemitter;
   scale               = ".001 .001 .001";
   muzzleVelocity      = 0;
   velInheritFactor    = 1.0;
   armingDelay         = 100000;
   isFluid             = true;
   lifetime            = 5010;
   fadeDelay           = 100000;
   bounceElasticity    = 0.0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0;
   hasLight    = true;
   lightRadius = 10;
   lightColor  = "1 0 0";
   movable             = 1;
};

datablock ProjectileData(greenlightProjectile) {
   projectileShapeName = "~/data/shapes/halo/green/halo.dts";
   areaImpulse = 0;
   directDamage        = 0;
   radiusDamage        = 0;
   damageRadius        = 5.0 ;
//   particleEmitter     = greenlightemitter;
   scale               = ".001 .001 .001";
   muzzleVelocity      = 0;
   velInheritFactor    = 1.0;
   armingDelay         = 100000;
   isFluid             = true;
   lifetime            = 5010;
   fadeDelay           = 100000;
   bounceElasticity    = 0.0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0;
   hasLight    = true;
   lightRadius = 10;
   lightColor  = "0 1 0";
   movable             = 1;
};

datablock ProjectileData(bluelightProjectile) {
   projectileShapeName = "~/data/shapes/halo/blue/halo.dts";
   areaImpulse = 0;
   directDamage        = 0;
   radiusDamage        = 0;
   damageRadius        = 5.0 ;
//   particleEmitter     = bluelightemitter;
   scale               = ".001 .001 .001";
   muzzleVelocity      = 0;
   velInheritFactor    = 1.0;
   armingDelay         = 100000;
   isFluid             = true;
   lifetime            = 5010;
   fadeDelay           = 100000;
   bounceElasticity    = 0.0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0;
   hasLight    = true;
   lightRadius = 10;
   lightColor  = "0 0 1";
   movable             = 1;
};

datablock ShapeBaseImageData(constantredlight)
{
   shapeFile = "~/data/shapes/dummy1.dts";
//   emap = true;

   mountPoint = 0;
    
   offset = "0.25 0.25 0.3";

   correctMuzzleVector = false;

   className = "WeaponImage";

   item = "";
   ammo = "";
   projectile = redlightProjectile;
   projectileType = Projectile;

   melee = false;

   armReady = false;

	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 5;
	stateTransitionOnTimeout[0]       = "Activate";
	stateFire[0]                    = true;
	stateSequence[0]                = "Fire";
	stateScript[0]                  = "onFire";
	stateWaitForTimeout[0]		= true;
};

datablock ShapeBaseImageData(constantgreenlight)
{
   shapeFile = "~/data/shapes/dummy1.dts";
//   emap = true;

   mountPoint = 0;
    
   offset = "0.25 0.25 0.3";

   correctMuzzleVector = false;

   className = "WeaponImage";

   item = "";
   ammo = "";
   projectile = greenlightProjectile;
   projectileType = Projectile;

   melee = false;

   armReady = false;

	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 5;
	stateTransitionOnTimeout[0]       = "Activate";
	stateFire[0]                    = true;
	stateSequence[0]                = "Fire";
	stateScript[0]                  = "onFire";
	stateWaitForTimeout[0]		= true;
};

datablock ShapeBaseImageData(constantbluelight)
{
   shapeFile = "~/data/shapes/dummy1.dts";
//   emap = true;

   mountPoint = 0;
    
   offset = "0.25 0.25 0.3";

   correctMuzzleVector = false;

   className = "WeaponImage";

   item = "";
   ammo = "";
   projectile = bluelightProjectile;
   projectileType = Projectile;

   melee = false;

   armReady = false;

	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 5;
	stateTransitionOnTimeout[0]       = "Activate";
	stateFire[0]                    = true;
	stateSequence[0]                = "Fire";
	stateScript[0]                  = "onFire";
	stateWaitForTimeout[0]		= true;
};
