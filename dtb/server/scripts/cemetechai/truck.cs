//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------
//RTB's Truck vehicle mod, shamelessly stolen by DShiznit (If you're reading
//This and are a dev of special forces or any other mod, feel free to use this,
//but give credit to Me and whoever made RTB's Truck mod)
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
//This is the part where we add shit. I got rid of the tire emitters and other
//useless datablocks already exec'd by previous scripts

datablock WheeledVehicleTire(truckTire)
{
   // Tires act as springs and generate lateral and longitudinal
   // forces to move the vehicle. These distortion/spring forces
   // are what convert wheel angular velocity into forces that
   // act on the rigid body.
   shapeFile = "tbm/data/shapes/feedback/large/epicwheellarge.dts";
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

datablock WheeledVehicleSpring(truckSpring)
{
   // Wheel suspension properties
   length = 0.5;             // Suspension travel
   force = 2000;              // Spring force
   damping = 300;             // Spring damping
   antiSwayForce = 3;         // Lateral anti-sway force
};

datablock WheeledVehicleData(truck)
{
   category = "Vehicles";
   shapeFile = "tbm/data/shapes/bricks/vehicles/pimpmobile2.dts"; //I modified the existing truck model pretty extensively
   emap = true;

  // maxDamage = 1.0;
  // destroyedLevel = 0.5;

   maxSteeringAngle = 0.785;  // Maximum steering angle, should match animation
   tireEmitter = skiEmitter; // All the tires use the same dust emitter
	explosion						= MissileLauncherExplosion; // How bout now beeotch!
   maxDamage = 1000;
   //destroyedLevel	= 50.40
   // 3rd person camera settings
   cameraRoll = false;         // Roll the camera with the vehicle, I'll leave this off
   cameraMaxDist = 18;         // Far distance from vehicle, a little close, so i made it biggerer
   cameraOffset = 1.5;        // Vertical offset from camera mount point
   cameraLag = 0.1;           // Velocity lag of camera
   cameraDecay = 0.75;        // Decay per sec. rate of velocity lag

   // Rigid Body
   mass = 150;
   massCenter = "0 0 0";    // Center of mass for rigid body
   massBox = "0 0 0";         // Size of box used for moment of inertia,
                              // if zero it defaults to object bounding box
   drag = 0.6;                // Drag coefficient
   bodyFriction = 0.6;
   bodyRestitution = 0.4;
   minImpactSpeed = 5;        // Impacts over this invoke the script callback
   softImpactSpeed = 5;       // Play SoftImpact Sound, these are sound hooks, not that they are of any use
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
   engineBrake = 1000;         // Braking when throttle is 0
   brakeTorque = 2000;        // When brakes are applied, you should really make this the same or less than the engine power retard
   maxWheelSpeed = 40;        // Engine scale by current speed / max speed, i'm almost doubling this, built ford tough.
 
        forwardThrust           = 400;
        reverseThrust           = 400;
        lift                    = 50;
        maxForwardVel           = 40;
        maxReverseVel           = 40;
        horizontalSurfaceForce  = 50;   // Horizontal center "wing" (provides "bite" into the wind for climbing/diving and turning)
        verticalSurfaceForce    = 0; 
        rollForce               = 200;
        yawForce                = 200;
        pitchForce              = 200;
        rotationalDrag          = 3;
        stallSpeed              = 0;

   // Energy
   maxEnergy = 100;
   jetForce = 3000;
   minJetEnergy = 30;
   jetEnergyDrain = 2;
//   buildable = 1; //not sure what this does but I don't think TBM uses it

   // Sounds
//   jetSound = sawfireidleSound;
   engineSound = sawIdleSound;
//   squealSound = ScoutSquealSound;
//   softImpactSound = SoftImpactSound;
//   hardImpactSound = HardImpactSound;
//   wheelImpactSound = WheelImpactSound;

//   explosion = VehicleExplosion;
   maxMountSpeed = 0.5;   
   mountDelay = 2;   
   dismountDelay = 1;   
   stationaryThreshold = 0.5;   
   maxDismountSpeed = 0.5;   
   numMountPoints = 8;   
   mountable = true;   
   mountPose[0] = "fall";   
   mountPose[1] = "fall";
   mountPose[2] = "fall";
   mountPose[3] = "fall";
   mountPose[4] = "fall";
   mountPose[5] = "fall";
   mountPose[6] = "fall";
   mountPose[7] = "fall";
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

function truck::onAdd(%this,%obj)
{ 


   // Setup the car with some defaults tires & springs
   for (%i = %obj.getWheelCount() - 1; %i >= 0; %i--) {
      %obj.setWheelTire(%i,truckTire);
      %obj.setWheelSpring(%i,truckSpring);
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

function ServerCMDpimpmobile(%client) {
	if(%client.isAdmin || %client.isSuperAdmin)      //Easy access console command for spawning truck
	{
		%t = WheeledVehicleData::create(truck);
		%t.setTransform(%client.player.getTransform());
	}
}
 