//-----------------------------------------------------------------------------
// Torque Game Engine
// 
// Copyright (c) 2001 GarageGames.Com
// Portions Copyright (c) 2001 by Sierra Online, Inc.
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

datablock ParticleData(TireParticle)
{
   textureName          = "~/data/particles/cloud";
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
   overrideAdvances = false;
   particles = "TireParticle";
};


//-----------------------------------------------------------------------------

function WheeledVehicleData::create(%block)
{
   %obj = new WheeledVehicle() {
      dataBlock = %block;
   };
   return(%obj);
}

//-----------------------------------------------------------------------------

function WheeledVehicleData::onAdd(%this,%obj)
{
   // Setup the car with some defaults tires & springs
//   for (%i = %obj.getWheelCount() - 1; %i >= 0; %i--) {
//      %obj.setWheelTire(%i,DefaultCarTire);
//      %obj.setWheelSpring(%i,DefaultCarSpring);
//      %obj.setWheelPowered(%i,false);
//   }
   
   // Steer front tires
   %obj.setWheelSteering(0,1);
   %obj.setWheelSteering(1,1);

   // Only power the two rear wheels...
   %obj.setWheelPowered(2,true);
   %obj.setWheelPowered(3,true);
}

function WheeledVehicleData::onCollision(%this,%obj,%col,%vec,%speed)
{
   // Collision with other objects, including items
}   
