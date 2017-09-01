//Lego Plane
//---------------------------------------------------------------------------------------
// Drone Definition
datablock FlyingVehicleData(Legoplane)
{
	spawnOffset						= "0 0 2";
	emap							= true;
	category						= "Vehicles";
    shapeFile                       = "~/data/shapes/bricks/LegoPlane.dts";
	multipassenger					= True;
	computeCRC						= true;

	drag							= 0.25;
	density							= 1.0;

   maxMountSpeed = 0.3;
   mountDelay = 8;
   dismountDelay = 1;
   maxDismountSpeed = 0.0;

   stationaryThreshold = 0.5;

   mountPose[0] = "standing";
   mountPose[1] = "standing";
   mountPose[2] = "standing";
   mountPointTransform[0] = "0 0 0 0 0 1 0";
   mountPointTransform[1] = "0 0 0 0 0 1 0";
   mountPointTransform[2] = "0 0 0 0 0 1 0";
   numMountPoints = 3;
   isProtectedMountPoint[0] = true;

    cameraOffset = 4;        // Vertical offset from camera mount point
	cameraMaxDist					= 16;
	cameraOffset					= 3.65;
	cameraLag						= 0.1;
    cameraRoll = true;         // Roll the camera with the vehicle

	explosionDamage					= 10.5;
	explosionRadius					= 15.0;

	maxDamage						= 50.40;
	destroyedLevel					= 50.40;
									
	energyPerDamagePoint			= 160;
	maxEnergy						= 280;      // Afterburner and any energy weapon pool
	rechargeRate					= 0.8;

	minDrag							= 40;           // Linear Drag (eventually slows you down when not thrusting...constant drag)
	rotationalDrag					= 20;        // Anguler Drag (dampens the drift after you stop moving the mouse...also tumble drag)

	maxAutoSpeed					= 10;       // Autostabilizer kicks in when less than this speed. (meters/second)
	autoAngularForce				= 400;       // Angular stabilizer force (this force levels you out when autostabilizer kicks in)
	autoLinearForce					= 300;        // Linear stabilzer force (this slows you down when autostabilizer kicks in)
	autoInputDamping				= 0.55;      // Dampen control input so you don't` whack out at very slow speeds

	// Maneuvering
	maxSteeringAngle				= 3;    // Max radiens you can rotate the wheel. Smaller number is more maneuverable.
	horizontalSurfaceForce			= 20;   // Horizontal center "wing" (provides "bite" into the wind for climbing/diving and turning)
	verticalSurfaceForce			= 20;     // Vertical center "wing" (controls side slip. lower numbers make MORE slide.)
	maneuveringForce				= 6400;      // Horizontal jets (W,S,D,A key thrust)
	steeringForce					= 500;         // Steering jets (force applied when you move the mouse)
	steeringRollForce				= 200;      // Steering jets (how much you heel over when you turn)
	rollForce						= 10;  // Auto-roll (self-correction to right you after you roll/invert)
	hoverHeight						= 0.5;       // Height off the ground at rest
	createHoverHeight				= 0.5;  // Height off the ground when created
	maxForwardSpeed					= 90;  // speed in which forward thrust force is no longer applied (meters/second)

	// Turbo Jet
	jetForce						= 3000;      // Afterburner thrust (this is in addition to normal thrust)
	minJetEnergy					= 28;     // Afterburner can't be used if below this threshhold.
	jetEnergyDrain					= 2.8;       // Energy use of the afterburners (low number is less drain...can be fractional)                                                                                                                                                                                                                                                                                          // Auto stabilize speed
	vertThrustMultiple				= 3.0;

	// Rigid body
	mass							= 100;        // Mass of the vehicle
    integration                     = 3;           // Physics integration: TickSec/Rate
    collisionTol = 0.6; // Collision distance tolerance
    contactTol = 0.4; // Contact velocity tolerance

    bodyFriction					= 0;     // Don't mess with this.
	bodyRestitution					= 0.8;   // When you hit the ground, how much you rebound. (between 0 and 1)
	minRollSpeed					= 2000;     // Don't mess with this.
	softImpactSpeed					= 3;       // Sound hooks. This is the soft hit.
	hardImpactSpeed					= 15;    // Sound hooks. This is the hard hit.

	// Ground Impact Damage (uses DamageType::Ground)
	minImpactSpeed					= 10;      // If hit ground at speed above this then it's an impact. Meters/second
	speedDamageScale				= 0.06;

	// Object Impact Damage (uses DamageType::Impact)
	collDamageThresholdVel			= 23.0;
	collDamageMultiplier			= 0.02;

	//
	minTrailSpeed					= 15;      // The speed your contrail shows up at.
	//trailEmitter					= DroneContrailEmitter;
	//forwardJetEmitter				= DroneJetEmitter;
	//downJetEmitter					= DroneJetEmitter;

	//
	//jetSound						= DroneThrustSound;
	//engineSound						= DroneEngineSound;
	//softImpactSound					= DroneSoftImpactSound;
	//hardImpactSound					= DroneHardImpactSound;
	//
	//softSplashSoundVelocity			= 10.0;
	//mediumSplashSoundVelocity		= 15.0;
	//hardSplashSoundVelocity			= 20.0;
	//exitSplashSoundVelocity			= 10.0;

	//exitingWater					= DroneExitWaterMediumSound;
	//impactWaterEasy					= DroneImpactWaterSoftSound;
	//impactWaterMedium				= DroneImpactWaterMediumSound;
	//impactWaterHard					= DroneImpactWaterMediumSound;
	//waterWakeSound					= DroneWakeMediumSplashSound;

//	dustEmitter						= VehicleLiftoffDustEmitter;
	
	triggerDustHeight				= 4.0;
	dustHeight						= 1.0;

//	damageEmitter[0]				= LightDamageSmoke;

//	damageEmitter[1]				= HeavyDamageSmoke;

//	damageEmitter[2]				= DamageBubbles;

	damageEmitterOffset[0]			= "0.0 -3.0 0.0 ";
	damageLevelTolerance[0]			= 0.3;
	damageLevelTolerance[1]			= 0.7;
	numDmgEmitterAreas				= 3;
						
	//
	//max[RocketAmmo]					= 1000;

	minMountDist					= 2;

	//splashEmitter[0]				= VehicleFoamDropletsEmitter;
	//splashEmitter[1]				= VehicleFoamEmitter;

	//shieldImpact					= VehicleShieldImpact;

	//cmdCategory						= "Tactical";
	//cmdIcon							= CMDFlyingScoutIcon;
	//cmdMiniIconName					= "commander/MiniIcons/com_scout_grey";
	//targetNameTag					= 'Drone';
	//targetTypeTag					= 'FlyingVehicle';
	//sensorData						= AWACPulseSensor;
	//sensorRadius					= AWACPulseSensor.detectRadius;
	//sensorColor						= "255 194 9";
									
	checkRadius						= 5.5;
	observeParameters				= "0 0 1";
									
	shieldEffectScale				= "0.937 1.125 0.60";
};
// End Drone Definition
//----------------------------------------------------------------------------------------
// Vehicle Drone Functions

function Legoplane::onDamage(%this, %obj, %delta)
{
	Parent::onDamage(%this, %obj);
	%currentDamage = %obj.getDamageLevel();
	if(%currentDamage > %obj.destroyedLevel)
	{
		if(%obj.getDamageState() !$= "Destroyed")
		{
			if(%obj.respawnTime !$= "")
				%obj.marker.schedule = %obj.marker.data.schedule(%obj.respawnTime, "respawn", %obj.marker); 
			%obj.setDamageState(Destroyed);
		}
	}
	else
	{
		if(%obj.getDamageState() !$= "Enabled")
			%obj.setDamageState(Enabled);
	}
}
