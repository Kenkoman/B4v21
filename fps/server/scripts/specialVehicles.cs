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
   mass = 90;		//3100 lbs
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

	rotationalDrag		= 1.5;

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
   textureName          = "~/data/particles/cloud";
   dragCoefficient      = 2.0;
   gravityCoefficient   = -0.1;
   inheritedVelFactor   = 0.1;
   constantAcceleration = 0.0;
   lifetimeMS           = 1000;
   lifetimeVarianceMS   = 0;
   useInvAlpha = false;
   colors[0]     = "0.9 0.9 0.9 1.0";
   colors[1]     = "0.9 0.9 0.9 0.0";
   sizes[0]      = 0.70;
   sizes[1]      = 0.25;
};

datablock ParticleEmitterData(SkiEmitter)
{
   ejectionPeriodMS = 26;
   periodVarianceMS = 0;
   ejectionVelocity = 1;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 5;
   thetaMax         = 20;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   particles = "SkiParticle";
};

datablock WheeledVehicleData(skiVehicle)
{
   category = "Vehicles";
   shapeFile = "~/data/shapes/skiVehicle.dts";
   emap = true;

   maxDamage = 1.0;
   destroyedLevel = 0.5;

   maxSteeringAngle = 0.885;  // Maximum steering angle, should match animation
   tireEmitter = SkiEmitter; // All the tires use the same dust emitter

   // 3rd person camera settings
   cameraRoll = true;         // Roll the camera with the vehicle?
   cameraMaxDist = 6;         // Far distance from vehicle
   cameraOffset = 0;        // Vertical offset from camera mount point
   cameraLag = 1.0;           // Velocity lag of camera
   cameraDecay = 0.75;  //0.75;      // Decay per sec. rate of velocity lag

   // Rigid Body
   mass = 90;		//3100 lbs
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


isSled = true;	//if its a sled, the wing surfaces dont work unless its on the ground
	
   // Engine
   engineTorque = 35000;       // Engine power
   engineBrake = 25;         // Braking when throttle is 0
   brakeTorque = 80000;        // When brakes are applied
   maxWheelSpeed = 30;        // Engine scale by current speed / max speed

	forwardThrust		= 500;
	reverseThrust		= 500;
	lift			= 0;
	maxForwardVel		= 40;
	maxReverseVel		= 10;
	horizontalSurfaceForce	= 50;   // Horizontal center "wing" (provides "bite" into the wind for climbing/diving and turning)
	verticalSurfaceForce	= 0; 
	rollForce		= 900;
	yawForce		= 600;
	pitchForce		= -1000;
	rotationalDrag		= 3;
	stallSpeed		= 0;

	jumpForce	= 100; //havent added this into code yet.
	

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

function skiVehicle::onWreck(%this, %obj)
{
	//echo("script wreck ", %this, " ", %obj);

	%player = %obj.getMountedObject(0);

	//echo("player = ", %player);

	%speed = vectorLen(%obj.getVelocity());

	//messageall('asf', %speed);

	%time = ( ( (%speed-10) / 50) * 7) + 1;

	//echo("%time = ", %time);

	%time = %time * 1000;
	if(%time > 7000)
		%time = 7000;
	if(%time < 1000)
		%time = 1000;


	if(%player)
	{
		%player.setWhiteout(%time/7000 * 1);
		tumble(%player, %time);
		%player.unmountImage($leftfootslot);
		//de-equip proper inv slot;
		//for(%i = 0; i < 5; %i++)
		//{
			
		//}
	}

}
