//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------

// Information extacted from the shape.
//
// Wheel Sequences
//    spring#        Wheel spring motion: time 0 = wheel fully extended,
//                   the hub must be displaced, but not directly animated
//                   as it will be rotated in code.
// Other Sequences
//    steering       Wheel steering: time 0 = full right, 0.5 = center
//    breakLight     Break light, time 0 = off, 1 = breaking
//
// Wheel Nodes
//    hub#           Wheel hub, the hub must be in it's upper position
//                   from which the springs are mounted.
//
// The steering and animation sequences are optional.
// The center of the shape acts as the center of mass for the car.

//-----------------------------------------------------------------------------
datablock AudioProfile(legobaseEngineSound)
{
   filename    = "~/data/sound/vehicles/legobase/engine_idle.wav";
   description = AudioClosestLooping3d;
   preload = true;
};

datablock ParticleData(TireParticle)
{
   textureName          = "~/data/particles/smoke";
   dragCoefficient      = 2.0;
   gravityCoefficient   = -0.1;
   inheritedVelFactor   = 0.1;
   constantAcceleration = 0.0;
   lifetimeMS           = 1000;
   lifetimeVarianceMS   = 0;
   colors[0]     = "0.46 0.36 0.26 1.0";
   colors[1]     = "0.46 0.46 0.36 0.0";
   sizes[0]      = 0.50;
   sizes[1]      = 1.0;
};

datablock ParticleEmitterData(TireEmitter)
{
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;
   ejectionVelocity = 1;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 5;
   thetaMax         = 20;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "TireParticle";
};


//----------------------------------------------------------------------------

datablock WheeledVehicleTire(legobaseTire)
{
   // Tires act as springs and generate lateral and longitudinal
   // forces to move the vehicle. These distortion/spring forces
   // are what convert wheel angular velocity into forces that
   // act on the rigid body.
   shapeFile = "~/data/shapes/vehicles/legobase/legobasewheel.dts";
   staticFriction = 4;
   kineticFriction = 1.25;

   // Spring that generates lateral tire forces
   lateralForce = 18000;
   lateralDamping = 4000;
   lateralRelaxation = 1;

   // Spring that generates longitudinal tire forces
   longitudinalForce = 18000;
   longitudinalDamping = 4000;
   longitudinalRelaxation = 1;
};

datablock WheeledVehicleSpring(legobaseSpring)
{
   // Wheel suspension properties
   length = 0.50;             // Suspension travel
   force = 2000;              // Spring force
   damping = 300;             // Spring damping
   antiSwayForce = 3;         // Lateral anti-sway force
};

datablock WheeledVehicleData(legobase)
{
   category = "Vehicles";
   shapeFile = "~/data/shapes/bricks/legobase.dts";
   emap = true;

  // maxDamage = 1.0;
  // destroyedLevel = 0.5;

   maxSteeringAngle = 0.785;  // Maximum steering angle, should match animation
   tireEmitter = TireEmitter; // All the tires use the same dust emitter

   //maxDamage = 50.40;
   //destroyedLevel	= 50.40
   // 3rd person camera settings
   cameraRoll = false;         // Roll the camera with the vehicle
   cameraMaxDist = 6;         // Far distance from vehicle
   cameraOffset = 1.5;        // Vertical offset from camera mount point
   cameraLag = 0.1;           // Velocity lag of camera
   cameraDecay = 0.75;        // Decay per sec. rate of velocity lag

   // Rigid Body
   mass = 200;
   massCenter = "0 0 0";    // Center of mass for rigid body
   massBox = "0 0 0";         // Size of box used for moment of inertia,
                              // if zero it defaults to object bounding box
   drag = 0.6;                // Drag coefficient
   bodyFriction = 0.6;
   bodyRestitution = 0.4;
   minImpactSpeed = 5;        // Impacts over this invoke the script callback
   softImpactSpeed = 5;       // Play SoftImpact Sound
   hardImpactSpeed = 15;      // Play HardImpact Sound
   integration = 4;           // Physics integration: TickSec/Rate
   collisionTol = 0.1;        // Collision distance tolerance
   contactTol = 0.1;          // Contact velocity tolerance
   	// Ground Impact Damage (uses DamageType::Ground)
   //minImpactSpeed					= 10;      // If hit ground at speed above this then it's an impact. Meters/second
  // speedDamageScale				= 0.06;

	// Object Impact Damage (uses DamageType::Impact)
   //collDamageThresholdVel			= 23.0;
  // collDamageMultiplier			= 0.02
   // Engine
   engineTorque = 2000;       // Engine power
   engineBrake = 600;         // Braking when throttle is 0
   brakeTorque = 8000;        // When brakes are applied
   maxWheelSpeed = 45;        // Engine scale by current speed / max speed

   // Energy
   maxEnergy = 100;
   jetForce = 3000;
   minJetEnergy = 30;
   jetEnergyDrain = 2;
   buildable = 1;

   // Sounds
//   jetSound = ScoutThrustSound;
//   engineSound = legobaseEngineSound;
//   squealSound = ScoutSquealSound;
//   softImpactSound = SoftImpactSound;
//   hardImpactSound = HardImpactSound;
//   wheelImpactSound = WheelImpactSound;

//   explosion = VehicleExplosion;
   maxMountSpeed = 0.1;   
   mountDelay = 2;   
   dismountDelay = 1;   
   stationaryThreshold = 0.5;   
   maxDismountSpeed = 0.1;   
   numMountPoints = 1;   
   mountable = true;   
   mountPose[0] = "sitting";   
   mountPointTransform[0] = "0 0 0 0 0 1 0";   
   isProtectedMountPoint[0] = false;
};


//-----------------------------------------------------------------------------

//function WheeledVehicleData::create(%block)
//{

  // }%obj = new WheeledVehicle() {
   //   dataBlock = %block;
  // };
  // return(%obj);
   //%obj.mountable = true;

//}
function WheeledVehicleData::create(%block)
{
   %obj = new WheeledVehicle() {
      dataBlock = %block;
   };
   return(%obj);
   %obj.mountable = true;

}
//-----------------------------------------------------------------------------

function legobase::onAdd(%this,%obj)
{ 


   // Setup the car with some defaults tires & springs
   for (%i = %obj.getWheelCount() - 1; %i >= 0; %i--) {
      %obj.setWheelTire(%i,legobaseTire);
      %obj.setWheelSpring(%i,legobaseSpring);
      %obj.setWheelPowered(%i,false);
    //setup the base car with bricks
   }
   
   // Steer front tires
   %obj.setWheelSteering(0,1);
   %obj.setWheelSteering(1,1);

   // Only power the two rear wheels...
   %obj.setWheelPowered(0,true);
   %obj.setWheelPowered(1,true);

   %obj.setWheelPowered(2,true);
   %obj.setWheelPowered(3,true);

   // Enable Mount Points
   %obj.mountable = true;
}

function legobase::onCollision(%this,%obj,%col,%vec,%speed)
{
   // Collision with other objects, including items
   	Parent::onDamage(%this, %obj);
} 