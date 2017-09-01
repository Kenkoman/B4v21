//specialVehicles.cs
 
//deathVehicle - invisible vehicle that you are mounted on when you die so you bounce around
 
//ski vehicle - slippery, stable vehicle that lets you ski 
 
 
datablock WheeledVehicleData(deathVehicle)
{
   category = "Vehicles";
   shapeFile = "~/data/shapes/deathVehicle.dts";
   emap = true;
 
   maxDamage = 1.0;
   destroyedLevel = 0.5;
 
   maxSteeringAngle = 0.885;  // Maximum steering angle, should match animation
   tireEmitter = TireEmitter; // All the tires use the same dust emitter
 
   // 3rd person camera settings
   cameraRoll = true;         // Roll the camera with the vehicle
   cameraMaxDist = 6;         // Far distance from vehicle
   cameraOffset = 1.5;        // Vertical offset from camera mount point
   cameraLag = 0.1;           // Velocity lag of camera
   cameraDecay = 0.75;        // Decay per sec. rate of velocity lag
 
   // Rigid Body
   mass = 90;           //3100 lbs
   density = 2;
   massCenter = "0.0 0.0 1.25";    // Center of mass for rigid body
   massBox = "1.5 1.5 2.65";         // Size of box used for moment of inertia,
                              // if zero it defaults to object bounding box
   drag = 0.6;                // Drag coefficient
   bodyFriction = 0.6;
   bodyRestitution = 0.7;
   minImpactSpeed = 5;        // Impacts over this invoke the script callback
   softImpactSpeed = 5;       // Play SoftImpact Sound
   hardImpactSpeed = 15;      // Play HardImpact Sound
   integration = 4;           // Physics integration: TickSec/Rate
   collisionTol = 0.1;        // Collision distance tolerance
   contactTol = 0.1;          // Contact velocity tolerance
 
   // Engine
   engineTorque = 35000;       // Engine power
   engineBrake = 25;         // Braking when throttle is 0
   brakeTorque = 80000;        // When brakes are applied
   maxWheelSpeed = 190;        // Engine scale by current speed / max speed
 
        rotationalDrag          = 1.5;
 
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
};
 
function deathVehicle::onAdd(%this,%obj)
{
        //do nothing
}       
 
datablock WheeledVehicleTire(NothingTire)
{
   // Tires act as springs and generate lateral and longitudinal
   // forces to move the vehicle. These distortion/spring forces
   // are what convert wheel angular velocity into forces that
   // act on the rigid body.
   shapeFile = "~/data/shapes/emptyWheel.dts";
   staticFriction = 0.0;
   kineticFriction = 0.0;
 
   // Spring that generates lateral tire forces
   lateralForce = 0;
   lateralDamping = 0;
   lateralRelaxation = 1;
 
   // Spring that generates longitudinal tire forces
   longitudinalForce = 0;
   longitudinalDamping = 0;
   longitudinalRelaxation = 1;
};
 
datablock WheeledVehicleSpring(skiSpring)
{
   // Wheel suspension properties
   length = 1.2;             // Suspension travel
   force = 195; //3000;              // Spring force
   damping = 60; //600;             // Spring damping
   antiSwayForce = 0; //3;         // Lateral anti-sway force
};
 
datablock ParticleData(SkiParticle)
{
   textureName          = "~/data/shapes/bricks/vehicles/dustParticle";
   dragCoefficient      = 2.0;
   gravityCoefficient   = -0.1;
   inheritedVelFactor   = 0.1;
   constantAcceleration = 0.0;
   lifetimeMS           = 200;
   lifetimeVarianceMS   = 0;
   colors[0]     = "0.46 0.36 0.26 1.0";
   colors[1]     = "0.46 0.46 0.36 0.0";
   sizes[0]      = 0.50;
   sizes[1]      = 1.0;
};

 

datablock ParticleEmitterData(SkiEmitter)
{
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;
 
   ejectionVelocity = 0.5;
   velocityVariance = 0.05;
 
   ejectionOffset = 0;
 
   thetaMin         = 0.0;
   thetaMax         = 90.0; 
   particles = "SkiParticle";
};
 
 
datablock WheeledVehicleData(skiVehicle)
{
   category = "Vehicles";
   shapeFile = "~/data/shapes/skiVehicle.dts";
   emap = true;
 
   maxDamage = 1.0;
   destroyedLevel = 0.5;
 
   maxSteeringAngle = 0.01;  // Maximum steering angle, should match animation
tireEmitter = SkiEmitter; // All the tires use the same dust emitter
 
        damageEmitterOffset[0]                  = "1.1 -1.2 0 ";
        damageEmitterOffset[1]                  = "-1.1 -1.2 0 ";
        damageLevelTolerance[0]                 = 0.0;
        damageLevelTolerance[1]                 = 0.5;
        damageEmitter[0]                                = brickTrailEmitter;
        damageEmitter[1]                                = brickTrailEmitter;
        numDmgEmitterAreas                              = 2;
 
   // 3rd person camera settings
   cameraRoll = true;         // Roll the camera with the vehicle?
   cameraMaxDist = 6;         // Far distance from vehicle
   cameraOffset = 0;        // Vertical offset from camera mount point
   cameraLag = 1.0;           // Velocity lag of camera
   cameraDecay = 0.75;  //0.75;      // Decay per sec. rate of velocity lag
 
   // Rigid Body
   mass = 10;           //300 lbs (about)
   density = 0.2;
   massCenter = "0.0 0.0 0.5";    // Center of mass for rigid body
   massBox = "1.5 1.5 1.5";         // Size of box used for moment of inertia,
                              // if zero it defaults to object bounding box
   drag = 0.8;                // Drag coefficient
   bodyFriction = 0.21;
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
   engineBrake = 25;         // Braking when throttle is 0
   brakeTorque = 100;        // When brakes are applied
   maxWheelSpeed = 1;        // Engine scale by current speed / max speed
 
        forwardThrust           = 400;
        reverseThrust           = 400;
        lift                    = 0;
        maxForwardVel           = 67;
        maxReverseVel           = 67;
        horizontalSurfaceForce  = 50;   // Horizontal center "wing" (provides "bite" into the wind for climbing/diving and turning)
        verticalSurfaceForce    = 0; 
        rollForce               = 80;
        yawForce                = 20;
        pitchForce              = 10;
        rotationalDrag          = 3;
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
};
 
function skiVehicle::onAdd(%this,%obj)
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
}       
 
function skiVehicle::onUnMount(%this, %obj)
{
        %obj.delete();
}