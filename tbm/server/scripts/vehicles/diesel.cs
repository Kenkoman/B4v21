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
datablock AudioProfile(CarEngineSound)
{
 filename = "~/data/sound/car_idle.wav";
 description = AudioDefaultLooping3d;
 preload = true;
};


//----------------------------------------------------------------------------

datablock WheeledVehicleTire(TBMDieselTireFront)
{
   // Tires act as springs and generate lateral and longitudinal
   // forces to move the vehicle. These distortion/spring forces
   // are what convert wheel angular velocity into forces that
   // act on the rigid body.
   shapeFile = "~/data/shapes/bricks/vehicles/tbmtrainwheel.dts";
   //friction = 1.5;
   friction = 6.5;

   // Spring that generates lateral tire forces
   lateralForce = 6000;
   lateralDamping = 5000;
   lateralRelaxation = 1;

   // Spring that generates longitudinal tire forces
   longitudinalForce = 6000;
   longitudinalDamping = 400;
   longitudinalRelaxation = 1;
};

datablock WheeledVehicleSpring(TBMDieselSpringFront)
{
   // Wheel suspension properties
   length = 0.1;             // Suspension travel
   force = 3000;              // Spring force
   damping = 600;             // Spring damping
   antiSwayForce = 3;         // Lateral anti-sway force
};

datablock WheeledVehicleData(TBMDiesel)
{
   category = "Vehicles";
   shapeFile = "~/data/shapes/bricks/vehicles/diesel.dts";
   emap = true;

   // -- Adrian
   mountPose[0] = sitting;
   numMountPoints = 8;
   isProtectedMountPoint[0] = true;
   // -------------
//	explosion						= MissileLauncherExplosion; // How bout now beeotch! //Nope
   maxDamage = 1000;
   destroyedLevel = 1.0;

   maxSteeringAngle = 0.5;  // Maximum steering angle, should match animation
   integration = 4;           // Force integration time: TickSec/Rate
   massCenter = "0 0 -5";     // Stop it from tipping over so easily

   // 3rd person camera settings
   cameraRoll = false;         // Roll the camera with the vehicle
   cameraMaxDist = 20;         // Far distance from vehicle
   cameraOffset = 2.5;        // Vertical offset from camera mount point
   cameraLag = 0.1;           // Velocity lag of camera
   cameraDecay = 0.75;        // Decay per sec. rate of velocity lag

   // Rigid Body
   mass = 200;
   drag = 0.6;
   bodyFriction = 0.6;
   bodyRestitution = 0.4;
   minImpactSpeed = 5;        // Impacts over this invoke the script callback
   softImpactSpeed = 5;       // Play SoftImpact Sound
   hardImpactSpeed = 15;      // Play HardImpact Sound

   // Engine
   engineTorque = 5000;       // Engine power
   engineBrake = 600;         // Braking when throttle is 0
   brakeTorque = 2000;        // When brakes are applied
   maxWheelSpeed = 40;        // Engine scale by current speed / max speed

   // Energy
   maxEnergy = 100;
   jetForce = 3000;
   minJetEnergy = 30;
   jetEnergyDrain = 2;

   // Sounds
//   jetSound = ScoutThrustSound;
//   engineSound = ScoutEngineSound;
    engineSound = CarEngineSound;
//   squealSound = ScoutSquealSound;
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

   // Adrian
   %obj.mountable = true;
   
   return(%obj);
}

//-----------------------------------------------------------------------------

function WheeledVehicleData::onAdd(%this,%obj)
{
   // Setup the car with some defaults tires & springs
   //for (%i = %obj.getWheelCount() - 1; %i >= 0; %i--) {
   //   %obj.setWheelTire(%i,TBMDieselTire);
   //   %obj.setWheelSpring(%i,TBMDieselSpring);
   //}
   for(%i = 4; %i >= 0; %i--) {
    %obj.setWheelTire(%i,TBMDieselTireFront);
    %obj.setWheelSpring(%i,TBMDieselSpringFront);
   }

   for(%i = 7; %i >= 4; %i--) {
    %obj.setWheelTire(%i,TBMDieselTireFront);
    %obj.setWheelSpring(%i,TBMDieselSpringFront);
   }
}

function WheeledVehicleData::onCollision(%this,%obj,%col,%vec,%speed)
{
   // Collision with other objects, including items
}   

function WheeledVehicleData::damage(%this, %obj, %sourceObject, %position, %damage, %damageType)
{
   %obj.applyDamage(%damage);
   if(%obj.getDamageLevel() == %obj.getDatablock().maxDamage) %obj.onDisabled(%obj,%this,"Dead");
}

function WheeledVehicleData::onDamage(%this, %obj, %delta)
{
   // Empty function. Use it if you think you can do something witty with it.
}

function WheeledVehicle::onDisabled(%this,%obj,%state)
{
   // Schedule corpse removal.  Just keeping the place clean.
   %this.startfade(1000, 0, true);
   %this.schedule(1000,"delete");
}

function WheeledVehicleData::onEnterLiquid(%this, %obj, %coverage, %type)
{
   switch(%type)
   {
      case 0: //Water
      case 1: //Ocean Water
      case 2: //River Water
      case 3: //Stagnant Water
      case 4: //Lava
         %obj.damage("null", "0 0 0", 100000, "Lava");
      case 5: //Hot Lava
         %obj.damage("null", "0 0 0", 100000, "Lava");
      case 6: //Crusty Lava
         %obj.damage("null", "0 0 0", 100000, "Lava");
      case 7: //Quick Sand
   }
}

function WheeledVehicle::getState()
{
  //shut up, console
}
