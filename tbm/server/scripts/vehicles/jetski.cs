datablock WheeledVehicleData(jetski)
{
   category = "Vehicles";
   shapeFile = "~/data/shapes/bricks/vehicles/jetski2.dts";
   emap = true;
 
   maxDamage = 1.0;
   destroyedLevel = 0.5;
 
   maxSteeringAngle = 0.01;  // Maximum steering angle, should match animation
tireEmitter = SkiEmitter; // All the tires use the same dust emitter
 
        damageEmitterOffset[0]                  = "0.5 -1.5 2";
        damageEmitterOffset[1]                  = "-0.5 -1.5 2";
        damageLevelTolerance[0]                 = 0.0;
        damageLevelTolerance[1]                 = 0.5;
        damageEmitter[0]                                = playerbubbleEmitter;
        damageEmitter[1]                                = playerbubbleEmitter;
        numDmgEmitterAreas                              = 2;
 
   // 3rd person camera settings
   cameraRoll = true;         // Roll the camera with the vehicle?
   cameraMaxDist = 6;         // Far distance from vehicle
   cameraOffset = 0;        // Vertical offset from camera mount point
   cameraLag = 1.0;           // Velocity lag of camera
   cameraDecay = 0.75;  //0.75;      // Decay per sec. rate of velocity lag
 
   // Rigid Body
   mass = 80;           //300 lbs (about) //bullshit
   density = 0.7;
   massCenter = "0 0 0";    // Center of mass for rigid body
   massBox = "0 0 0";         // Size of box used for moment of inertia,
                              // if zero it defaults to object bounding box
   drag = 7;                // Drag coefficient
   bodyFriction = 0.5;
   bodyRestitution = 0.1;
   minImpactSpeed = 500;        // Impacts over this invoke the script callback
   softImpactSpeed = 5;       // Play SoftImpact Sound
   hardImpactSpeed = 15;      // Play HardImpact Sound
   integration = 20;           // Physics integration: TickSec/Rate
   collisionTol = 0.1;        // Collision distance tolerance
   contactTol = 0.01;          // Contact velocity tolerance
 
 
isSled = true;  //if its a sled, the wing surfaces dont work unless its on the ground
        
   // Engine
   engineTorque = 400;       // Engine power
   engineBrake = 200;         // Braking when throttle is 0
   brakeTorque = 100;        // When brakes are applied
   maxWheelSpeed = 1;        // Engine scale by current speed / max speed
 
        forwardThrust           = 2000;
        reverseThrust           = 1000;
        lift                    = 0;
        maxForwardVel           = 50;
        maxReverseVel           = 50;
        horizontalSurfaceForce  = 500;   // Horizontal center "wing" (provides "bite" into the wind for climbing/diving and turning)
        verticalSurfaceForce    = 0; 
        rollForce               = 2000;
        yawForce                = 32000;
        pitchForce              = 1000;
        rotationalDrag          = 100;
        stallSpeed              = 0;
        jumpForce       = 100; //havent added this into code yet.
        
 
   // Energy
   maxEnergy = 100;
   jetForce = 3000;
   minJetEnergy = 30;
   jetEnergyDrain = 2;
 
   // Sounds
//   jetSound = ScoutThrustSound;
//   engineSound = ScoutEngineSound;
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
   mountPose[0] = "armreadyboth";
   mountPointTransform[0] = "0 0 0 0 0 1 0";
   isProtectedMountPoint[0] = false;
};
 
function jetski::onAdd(%this,%obj)
{
        //mount the nothing tire and ski spring
        %obj.setWheelTire(0, nothingtire);
        %obj.setWheelTire(1, nothingtire);
        %obj.setWheelTire(2, nothingtire);
        %obj.setWheelTire(3, nothingtire);
 
        %obj.setWheelSpring(0, skiSpring);
        %obj.setWheelSpring(1, skiSpring);
        %obj.setWheelSpring(2, skiSpring);
        %obj.setWheelSpring(3, skiSpring);

   // Enable Mount Points
   %obj.mountable = true;
}  

function ServerCMDjetski(%client) {
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		%t = WheeledVehicleData::create(jetski);
		%t.setTransform(%client.player.getTransform());
	}
}
