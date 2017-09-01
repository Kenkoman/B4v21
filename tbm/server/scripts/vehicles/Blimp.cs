// TBM Copter //modifyed by Feedback and Shiznit for use as a blimp...
// Model and texturing by Satan
// Scripting, animation, and conversion by Luquado
// Physics by Rob
// Taken from Garage Game's sample scripts and modified.
//---------------------------------------------------------------------------------------
// Let's define our shiznit.

datablock FlyingVehicleData(Blimp)
{
	emap							= true;
	category						= "Vehicles";
	shapeFile						= "~/data/shapes/Bricks/FeedBack/airship.dts";
	multipassenger					= false;
	computeCRC						= true;
									

	drag							= 0.65;
	density							= 3.0;

	mountPose[0]					= sitting;
	numMountPoints					= 2;
	isProtectedMountPoint[0]		= false;
	cameraMaxDist					= 15.0; // Why so much? So that 3rd person would look right, that's why!
	cameraOffset					= 4.5;
	cameraLag						= 0.0;
    cameraRoll = true;         // Roll the camera with the vehicle for extra disorienting fun!

    // explosion						= VehicleExplosion; // Maybe later, guys.haehaheha
	explosionDamage					= 10.5;
	explosionRadius					= 15.0;

	maxDamage						= 100;
	destroyedLevel					= 1;
									

	minDrag							= 30;           // Linear Drag (eventually slows you down when not thrusting...constant drag)
	rotationalDrag					= 10;        // Anguler Drag (dampens the drift after you stop moving the mouse...also tumble drag)

	maxAutoSpeed					= 10;       // Autostabilizer kicks in when less than this speed. (meters/second)
	autoAngularForce				= 200;       // Angular stabilizer force (this force levels you out when autostabilizer kicks in)
	autoLinearForce					= 200;        // Linear stabilzer force (this slows you down when autostabilizer kicks in)
	autoInputDamping				= 0.95;      // Dampen control input so you don't` whack out at very slow speeds
    integration = 6;           // Physics integration: TickSec/Rate
    collisionTol = 0.2;        // Collision distance tolerance
    contactTol = 0.1;
   
	// Maneuvering
	maxSteeringAngle				= 1;    // Max radiens you can rotate the wheel. Smaller number is more maneuverable.
	horizontalSurfaceForce			= 200;   // Horizontal center "wing" (provides "bite" into the wind for climbing/diving and turning)
	verticalSurfaceForce			= 200;     // Vertical center "wing" (controls side slip. lower numbers make MORE slide.)
	maneuveringForce				= 1000;      // Horizontal jets (W,S,D,A key thrust)
	steeringForce					= 100;         // Steering jets (force applied when you move the mouse)
	steeringRollForce				= 120;      // Steering jets (how much you heel over when you turn)
	rollForce						= 80;  // Auto-roll (self-correction to right you after you roll/invert)
	hoverHeight						= 80;       // Height off the ground at rest
	createHoverHeight				= 80;  // Height off the ground when created
	maxForwardSpeed					= 50;  // speed in which forward thrust force is no longer applied (meters/second)

	// Turbo Jet
	engineSound = CopterSound;
	jetForce						= 3000;      // Afterburner thrust (this is in addition to normal thrust)
	minJetEnergy					= 28;     // Afterburner can't be used if below this threshhold.
	jetEnergyDrain					= 2.8;       // Energy use of the afterburners (low number is less drain...can be fractional)                                                                                                                                                                                                                                                                                          // Auto stabilize speed
	vertThrustMultiple				= 3.0;

	// Rigid body
	mass							= 30;        // Mass of the vehicle
	bodyFriction					= 0;     // Don't mess with this.
	bodyRestitution					= 0.1;   // When you hit the ground, how much you rebound. (between 0 and 1)
	minRollSpeed					= 2000;     // Don't mess with this.
	softImpactSpeed					= 3;       // Sound hooks. This is the soft hit.
	hardImpactSpeed					= 15;    // Sound hooks. This is the hard hit.

	// Ground Impact Damage (uses DamageType::Ground)
	minImpactSpeed					= 10;      // If hit ground at speed above this then it's an impact. Meters/second
	speedDamageScale				= 0.06;

	// Object Impact Damage (uses DamageType::Impact)
	collDamageThresholdVel			= 23.0;
	collDamageMultiplier			= 0.02;

	minTrailSpeed					= 15;      // The speed your contrail shows up at.
	
	triggerDustHeight				= 4.0; // These don't matter, currently. Maybe later I'll add emitters for dust and contrail. Awww yeah.//Think again beeeotch
	dustHeight						= 1.0;

	damageEmitterOffset[0]			= "0.0 -3.0 0.0 ";
	damageLevelTolerance[0]			= 0.3;
	damageLevelTolerance[1]			= 0.7;
	numDmgEmitterAreas				= 1;
						
	minMountDist					= 20;
						
	checkRadius						= 5.5;
	observeParameters				= "0 0 0";
									
	shieldEffectScale				= "0.937 1.125 0.60";

        damageEmitterOffset[0]                  = "0 0 0";
        damageLevelTolerance[0]                 = 0.0;
        damageEmitter[0]                                = spearTrailEmitter;

};

//----------------------------------------------------------------------------------------

function Blimp::onAdd(%this,%obj)
{

   // Enable Mount Points
   %obj.mountable = true;
}

function Blimp::onCollision(%this,%obj,%col,%vec,%speed)
{
   // Collision with other objects, including items
   	//Parent::onDamage(%this, %obj);
} 

function servercmdblimp(%client) {
if(%client.isAdmin || %client.isSuperAdmin)
	{
  %block = new FlyingVehicle() {
    position = vectoradd(%client.getcontrolobject().position,vectoradd("0 0 8",vectorscale(%client.getcontrolobject().getforwardvector(),"10 10 0")));
    rotation = "1 0 0 0";
    scale = "1 1 1";
    dataBlock = "Blimp";
    owner = getrawip(%client);
  };
  %block.mountable = true;
  %block.setEnergyLevel(60);
	}
}