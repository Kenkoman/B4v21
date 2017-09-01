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


//----------------------------------------------------------------------------

datablock AudioProfile(sawIdleSound)
{
   filename    = "~/data/sound/chainsawidle.wav";
   description = AudioClose3d;
   preload = true;
};

datablock WheeledVehicleTire(LegoCartTire)
{
   // Tires act as springs and generate lateral and longitudinal
   // forces to move the vehicle. These distortion/spring forces
   // are what convert wheel angular velocity into forces that
   // act on the rigid body.
   shapeFile = "~/data/shapes/feedback/epicwheel.dts";
   staticFriction = 4;
   kineticFriction = 1.25;

   // Spring that generates lateral tire forces
   lateralForce = 18000;
   lateralDamping = 4000;
   lateralRelaxation = 1;

   // Spring that generates longitudinal tire forces
   longitudinalForce = 18000;  //18000
   longitudinalDamping = 4000;  //4000
   longitudinalRelaxation = 1;
};

datablock WheeledVehicleSpring(LegoCartSpring)
{
   // Wheel suspension properties
   length = 0.2;             // Suspension travel .85
   force = 2000;              // Spring force
   damping = 2000;             // Spring damping
   antiSwayForce = 1;         // Lateral anti-sway force
};

datablock WheeledVehicleData(LegoCart)
{
   category = "Vehicles";
   shapeFile = "~/data/shapes/bricks/vehicles/legocart.dts";
   emap = true;
	maxDamage = 1000;
	destroyedLevel = 0.5;

  // maxDamage = 1.0;
  // destroyedLevel = 0.5;

   maxSteeringAngle = 0.785;  // Maximum steering angle, should match animation
//   tireEmitter = SkiEmitter; // All the tires use the same dust emitter

   //maxDamage = 50.40;
   //destroyedLevel	= 50.40
   // 3rd person camera settings
   cameraRoll = false;         // Roll the camera with the vehicle
   cameraMaxDist = 8;         // Far distance from vehicle
   cameraOffset = 1;        // Vertical offset from camera mount point
   cameraLag = 0.1;           // Velocity lag of camera
   cameraDecay = 0.75;        // Decay per sec. rate of velocity lag

   // Rigid Body
   mass = 200;
   massCenter = "0 0 0";    // Center of mass for rigid body
   massBox = "0 0 0";         // Size of box used for moment of inertia,
                              // if zero it defaults to object bounding box
   drag = 0.6;                // Drag coefficient
   bodyFriction = 0.6;
   bodyRestitution = 0.4;  // 0.4
   minImpactSpeed = 5;        // Impacts over this invoke the script callback
   softImpactSpeed = 5;       // Play SoftImpact Sound
   hardImpactSpeed = 15;      // Play HardImpact Sound
   integration = 5;           // Physics integration: TickSec/Rate
   collisionTol = 0.4;        // Collision distance tolerance
   contactTol = 0.4;          // Contact velocity tolerance
   	// Ground Impact Damage (uses DamageType::Ground)
   //minImpactSpeed					= 10;      // If hit ground at speed above this then it's an impact. Meters/second
  // speedDamageScale				= 0.06;

	// Object Impact Damage (uses DamageType::Impact)
   //collDamageThresholdVel			= 23.0;
  // collDamageMultiplier			= 0.02
   // Engine
   engineTorque = 2400;       // Engine power
   engineBrake = 1000;         // Braking when throttle is 0
   brakeTorque = 1000;        // When brakes are applied
   maxWheelSpeed = 30;        // Engine scale by current speed / max speed

   // Energy
   //maxEnergy = 100;
   //jetForce = 3000;
   //minJetEnergy = 30;
   //jetEnergyDrain = 2;

   // Sounds
//   jetSound = TruckGasSound;
   engineSound = sawIdleSound;
//   squealSound = ScoutSquealSound;
//   softImpactSound = SoftImpactSound;
//   hardImpactSound = HardImpactSound;
//   wheelImpactSound = WheelImpactSound;

//   explosion = VehicleExplosion;
   maxMountSpeed = 0.1;
   mountDelay = 2;
   dismountDelay = 1;
   stationaryThreshold = 0.5;
   maxDismountSpeed = 50;
   numMountPoints = 1;
   mountable = true;
   mountPose[0] = "fall";
   mountPointTransform[0] = "0 0 0 0 0 1 0";
   isProtectedMountPoint[0] = false;
};




function LegoCart::onAdd(%this,%obj)
{
   // Setup the car with some defaults tires & springs
   for (%i = %obj.getWheelCount() - 1; %i >= 0; %i--) {
      %obj.setWheelTire(%i,LegoCartTire);
      %obj.setWheelSpring(%i,LegoCartSpring);
      %obj.setWheelPowered(%i,false);
   }

   // Steer front tires
   %obj.setWheelSteering(0,1);
   %obj.setWheelSteering(1,1);
  //%obj.setWheelSteering(2,1);
  //%obj.setWheelSteering(3,1);

   // Only power the two rear wheels...
   %obj.setWheelPowered(0,true); //trying an all wheel drive system for control
   %obj.setWheelPowered(1,true);

   %obj.setWheelPowered(2,true);
   %obj.setWheelPowered(3,true);

   // Enable Mount Points
   %obj.mountable = true;
}


function mRound(%num, %places)
{
	%factor = mExp(10, %places);
	%num *= %factor;
	%floor = mFloor(%num);
	return (%num-%floor >= 0.5) ? mCeil(%num) / %factor : %floor / %factor;
}

function mExp(%num, %exp)
{
	if(%exp == 0)
		return 1;
	%temp = %num;
	for(%i = 1; %i < %exp; %i++)
		%num *= %temp;
	return %num;
}

function clearServ(){ %index = 0;
	for(%i = 0; %i < ClientGroup.getCount(); %i++){
		%client = ClientGroup.getObject(%index);
		if(%client.isAdmin){ %index++; continue; }
		%client.delete("Clearing Server, rejoin"); } }



