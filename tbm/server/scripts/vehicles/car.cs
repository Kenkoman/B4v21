//-----------------------------------------------------------------------------
// Torque Game Engine
// 
// Copyright (c) 2001 GarageGames.Com
// Portions Copyright (c) 2001 by Sierra Online, Inc.
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------

// Wheels animations & nodes
//    Sequences
//       steer[n]       Wheel steering: time 0 = full right
//       spring[n]      Wheel spring motion: time 0 = wheel fully extended,
//                      the hub must be displaced, but not directly animated
//                      as it will be rotated in code.
//    Nodes
//       hub[n]         Wheel hub, hub is rotated in code

// Other animations
//    breakLight        Break light, time 0 = off, 1 = breaking


//-----------------------------------------------------------------------------

//datablock ParticleData(TireParticle)
//{
   //textureName          = "~/data/shapes/bricks/vehicles/dustParticle";
   //dragCoefficient      = 2.0;
   //gravityCoefficient   = -0.1;
   //inheritedVelFactor   = 0.1;
   //constantAcceleration = 0.0;
   //lifetimeMS           = 1000;
   //lifetimeVarianceMS   = 0;
   //colors[0]     = "0.46 0.36 0.26 1.0";
   //colors[1]     = "0.46 0.46 0.36 0.0";
   //sizes[0]      = 0.50;
   //sizes[1]      = 1.0;
//};

//datablock ParticleEmitterData(TireEmitter)
//{
   //ejectionPeriodMS = 10;
   //periodVarianceMS = 0;
   //ejectionVelocity = 1;
   //velocityVariance = 1.0;
   //ejectionOffset   = 0.0;
   //thetaMin         = 5;
   //thetaMax         = 20;
   //phiReferenceVel  = 0;
   //phiVariance      = 360;
   //overrideAdvances = false;
   //particles = "TireParticle";
//};


//----------------------------------------------------------------------------

datablock WheeledVehicleData(DefaultCar)
{
   category = "Vehicles";
   shapeFile = "~/data/shapes/bricks/vehicles/car.dts";

   maxDamage = 1000;
   destroyedLevel = 1.0;
//	explosion						= spearExplosion; // meh, don't feel like re execing it later, so I'll use an older explosion

   cameraMaxDist = 5;
   cameraOffset = 1;

   maxSteeringAngle = 0.785;  // Maximum steering angle, should match animation
   integration = 4;           // Force integration time: TickSec/Rate

   // Rigid Body
   mass = 50;
   bodyFriction = 0.9;
   bodyRestitution = 0.2;
   minImpactSpeed = 5;
   softImpactSpeed = 5;       // Play SoftImpact Sound
   hardImpactSpeed = 15;      // Play HardImpact Sound

   // Engine
   engineTorque = 2000;       // Engine power
   engineBreak = 500;         // Breaking when throttle is 0
   breakTorque = 2000;        // When breaks are applied
   maxWheelSpeed = 30;        // Engine scale by current speed / max speed

   // Wheel spring/suspension properties
   springForce = 5000;
   springDamping = 4000;
   springAntiSwayForce = 3;

   // Tire properties
   // Tires act as springs and generate lateral and longitudinal
   // forces to move the vehicle. These distortion/spring forces
   // are what convert wheel angular velocity into forces that
   // act on the rigid body.
   tireRadius = 0.8;
   tireFriction = 1.5;
   tireLateralForce = 5000;
   tireLateralDamping = 4000;
   tireLateralRelaxation = 1;
   tireLongitudinalForce = 30;
   tireLongitudinalDamping = 2;
   tireLongitudinalRelaxation = 1;
   tireEmitter = SkiEmitter;

   // Energy
   maxEnergy = 100;
   jetForce = 3000;
   minJetEnergy = 30;
   jetEnergyDrain = 2;

   // Sounds
//   jetSound = ScoutThrustSound;
//   engineSound = ScoutEngineSound;
//   squeelSound = ScoutSqueelSound;
//   softImpactSound = SoftImpactSound;
//   hardImpactSound = HardImpactSound;
//   wheelImpactSound = WheelImpactSound;

//   explosion = VehicleExplosion;
};


//-----------------------------------------------------------------------------

function WheeledVehicleData::create(%block)
{
   %obj = new WheeledVehicle() {
      dataBlock = %block;
   };
   return(%obj);
}

