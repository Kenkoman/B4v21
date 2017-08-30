if(!isObject(jeepExplosionSound))
{
   exec("./Support_Jeep.cs");
}


datablock WheeledVehicleData(FlyingWheeledJeepVehicle)
{
	category = "Vehicles";
	displayName = " ";
	shapeFile = "./shapes/jeep.dts"; //"~/data/shapes/skivehicle.dts"; //
	emap = true;
	minMountDist = 3;
   
   numMountPoints = 7;
   mountThread[0] = "sit";
   mountThread[1] = "sit";
   mountThread[2] = "sit";
   mountThread[3] = "sit";
   mountThread[4] = "sit";
   mountThread[5] = "root";
   mountThread[6] = "root";
   mountThread[7] = "sit";

	maxDamage = 200.00;
	destroyedLevel = 200.00;
	energyPerDamagePoint = 160;
	speedDamageScale = 1.04;
	collDamageThresholdVel = 20.0;
	collDamageMultiplier   = 0.02;

	massCenter = "0 0 0";
   //massBox = "2 5 1";

	maxSteeringAngle = 0.9785;  // Maximum steering angle, should match animation
	integration = 4;           // Force integration time: TickSec/Rate
	tireEmitter = TireEmitter; // All the tires use the same dust emitter

	// 3rd person camera settings
	cameraRoll = false;         // Roll the camera with the vehicle
	cameraMaxDist = 13;         // Far distance from vehicle
	cameraOffset = 7.5;        // Vertical offset from camera mount point
	cameraLag = 0.0;           // Velocity lag of camera
	cameraDecay = 0.75;        // Decay per sec. rate of velocity lag
	cameraTilt = 0.4;
   collisionTol = 0.1;        // Collision distance tolerance
   contactTol = 0.1;

	useEyePoint = false;	

	defaultTire	= jeepTire;
	defaultSpring	= jeepSpring;
	flatTire	= jeepFlatTire;
	flatSpring	= jeepFlatSpring;

   numWheels = 4;

	// Rigid Body
	mass = 200;
	density = 5.0;
	drag = 0.6;
	bodyFriction = 0.6;
	bodyRestitution = 0.6;
	minImpactSpeed = 10;        // Impacts over this invoke the script callback
	softImpactSpeed = 10;       // Play SoftImpact Sound
	hardImpactSpeed = 15;      // Play HardImpact Sound
	groundImpactMinSpeed    = 10.0;

	// Engine
	engineTorque = 12000; //4000;       // Engine power
	engineBrake = 2000;         // Braking when throttle is 0
	brakeTorque = 4000;        // When brakes are applied
	maxWheelSpeed = 20;        // Engine scale by current speed / max speed

	rollForce		= 900;
	yawForce		= 600;
	pitchForce		= 1000;
	rotationalDrag		= 0.2;

	// Energy
	maxEnergy = 100;
	jetForce = 3000;
	minJetEnergy = 30;
	jetEnergyDrain = 2;

   isSled = false;

   forwardThrust		= 3000;
	reverseThrust		= 2000;
	lift			= 100;
	maxForwardVel		= 40;
	maxReverseVel		= 40;
	horizontalSurfaceForce	= 130;   // Horizontal center "wing" (provides "bite" into the wind for climbing/diving and turning)
	verticalSurfaceForce	= 130; 
	rollForce		= 4000;
	yawForce		= 6000;
	pitchForce		= 6000;
	rotationalDrag		= 0.5;
	stallSpeed		= 10;

//   forwardThrust		= 2000;
//	reverseThrust		= 2000;
//	lift			= 100;
//	maxForwardVel		= 70;
//	maxReverseVel		= 70;
//	horizontalSurfaceForce	= 100;   // Horizontal center "wing" (provides "bite" into the wind for climbing/diving and turning)
//	verticalSurfaceForce	= 100; 
//	rollForce		= 6000;
//	yawForce		= 6000;
//	pitchForce		= 4000;
//	rotationalDrag		= 0.5;
//	stallSpeed		= 10;
//
//   forwardThrust		= 2500;
//	reverseThrust		= 500;
//	lift			= 15000;
//	maxForwardVel		= 60;
//	maxReverseVel		= 10;
//	horizontalSurfaceForce	= 500;   // Horizontal center "wing" (provides "bite" into the wind for climbing/diving and turning)
//	verticalSurfaceForce	= 500; 
//	rollForce		= 5000;
//	yawForce		= 5000;
//	pitchForce		= 5000;
//	rotationalDrag		= 0.1;
//	stallSpeed		= 10;

	splash = jeepSplash;
	splashVelocity = 4.0;
	splashAngle = 67.0;
	splashFreqMod = 300.0;
	splashVelEpsilon = 0.60;
	bubbleEmitTime = 1.4;
	splashEmitter[0] = jeepFoamDropletsEmitter;
	splashEmitter[1] = jeepFoamEmitter;
	splashEmitter[2] = jeepBubbleEmitter;
	mediumSplashSoundVelocity = 10.0;   
	hardSplashSoundVelocity = 20.0;   
	exitSplashSoundVelocity = 5.0;
		
	//mediumSplashSound = "";
	//hardSplashSound = "";
	//exitSplashSound = "";
	
	// Sounds
	//   jetSound = ScoutThrustSound;
	//engineSound = idleSound;
	//squealSound = skidSound;
	softImpactSound = slowImpactSound;
	hardImpactSound = fastImpactSound;
	//wheelImpactSound = slowImpactSound;

	//   explosion = VehicleExplosion;
	justcollided = 0;

   uiName = "Flying Wheeled Jeep ";
	rideable = true;
		lookUpLimit = 0.65;
		lookDownLimit = 0.45;

	paintable = true;
   
   damageEmitter[0] = JeepBurnEmitter;
	damageEmitterOffset[0] = "0.0 0.0 0.0 ";
	damageLevelTolerance[0] = 0.99;

   damageEmitter[1] = JeepBurnEmitter;
	damageEmitterOffset[1] = "0.0 0.0 0.0 ";
	damageLevelTolerance[1] = 1.0;

   numDmgEmitterAreas = 1;

   initialExplosionProjectile = jeepExplosionProjectile;
   initialExplosionOffset = 0;         //offset only uses a z value for now

   burnTime = 4000;

   finalExplosionProjectile = jeepFinalExplosionProjectile;
   finalExplosionOffset = 0.5;          //offset only uses a z value for now


   minRunOverSpeed    = 2;   //how fast you need to be going to run someone over (do damage)
   runOverDamageScale = 5;   //when you run over someone, speed * runoverdamagescale = damage amt
   runOverPushScale   = 1.2; //how hard a person you're running over gets pushed
};




