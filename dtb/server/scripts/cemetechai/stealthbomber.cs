// TBM Steath Bomber
// Model and texturing by ShadowZ
// Scripting by Luquado -Edits by DShiznit
// Physics by Rob & Kerm
// Taken from Garage Game's sample scripts and modified for Delta Wing, then modified again.
//---------------------------------------------------------------------------------------
// Let's define our shiznit.

datablock FlyingVehicleData(StealthBomber)
{
	emap							= true;
	category						= "Vehicles";
	shapeFile						= "tbm/data/shapes/ShadowZ/plane.dts";
	cloaktexture 					= "tbm/data/specialfx/cloakTexture";
	multipassenger					= false;
	computeCRC						= true;
									

	drag							= 0.02;
	density							= 3.0;

	//stateEmitter = JetEmitter;
   mountPose[0] = "fall";
   mountPointTransform[0] = "0 0 -1 0 0 1 0";
	numMountPoints					= 1;
	isProtectedMountPoint[0]		= false;
	cameraMaxDist					= 32.0; // Why so much? So that 3rd person would look right, that's why!
	cameraOffset					= 4.5;
	cameraLag						= 5.0;
    cameraRoll = true;         // Roll the camera with the vehicle for extra disorienting fun!

    // explosion						= VehicleExplosion; // Maybe later, guys.
	explosionDamage					= 10.5;
	explosionRadius					= 15.0;

	maxDamage						= 1000;
	destroyedLevel					= 50.40;
									

	minDrag							= 30;           // Linear Drag (eventually slows you down when not thrusting...constant drag)
	rotationalDrag					= 5;        // Anguler Drag (dampens the drift after you stop moving the mouse...also tumble drag)

	maxAutoSpeed					= 5;       // Autostabilizer kicks in when less than this speed. (meters/second) //10
	autoAngularForce				= 50;       // Angular stabilizer force (this force levels you out when autostabilizer kicks in) //200
	autoLinearForce					= 1;        // Linear stabilzer force (this slows you down when autostabilizer kicks in) //200
	autoInputDamping				= 0.95;      // Dampen control input so you don't` whack out at very slow speeds
    integration = 5;           // Physics integration: TickSec/Rate
    collisionTol = 0.2;        // Collision distance tolerance
    contactTol = 0.1;
   
	// Maneuvering
	maxSteeringAngle				= 1;    // Max radiens you can rotate the wheel. Smaller number is more maneuverable.
	horizontalSurfaceForce			= 200;   // Horizontal center "wing" (provides "bite" into the wind for climbing/diving and turning)
	verticalSurfaceForce			= 200;     // Vertical center "wing" (controls side slip. lower numbers make MORE slide.)
	maneuveringForce				= 1000;      // Horizontal jets (W,S,D,A key thrust)
	steeringForce					= 100;         // Steering jets (force applied when you move the mouse)
	steeringRollForce				= 120;      // Steering jets (how much you heel over when you turn)
	rollForce						= 60;  // Auto-roll (self-correction to right you after you roll/invert) //80
	hoverHeight						= 0;       // Height off the ground at rest
	createHoverHeight				= 80;  // Height off the ground when created
	maxForwardSpeed					= 50;  // speed in which forward thrust force is no longer applied (meters/second)

	// Turbo Jet
	jetForce						= 3000;      // Afterburner thrust (this is in addition to normal thrust)
	minJetEnergy					= 2.8;     // Afterburner can't be used if below this threshhold.
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
	
	triggerDustHeight				= 4.0; // These don't matter, currently. Maybe later I'll add emitters for dust and contrail. Awww yeah.
	dustHeight						= 1.0;
//---This stuff figured out by Kerm
	damageEmitterOffset[0]			= "0 0 0 ";
	damageEmitterOffset[1]			= "0 0 0 ";
	damageLevelTolerance[0]			= 0.0;
	damageLevelTolerance[1]			= 0.5;
	damageEmitter[0] 				= flame2Emitter;
	damageEmitter[1] 				= JetSmokeEmitter;
	numDmgEmitterAreas				= 2;
//---						
	minMountDist					= 20;
						
	checkRadius						= 5.5;
	observeParameters				= "0 0 0";
									
	shieldEffectScale				= "0.937 1.125 0.60";
};

function servercmdstealthadd(%client) {
	makestealth(%client);
}
function makestealth(%client){
	if(%client.isAdmin || %client.isSuperAdmin || %client.isMod)
	{
  %block = new FlyingVehicle() {
    position = vectoradd(%client.getcontrolobject().position,vectoradd("0 0 2",vectorscale(%client.getcontrolobject().getforwardvector(),"10 10 0")));
    rotation = "1 0 0 0";
    scale = "1 1 1";
    dataBlock = "StealthBomber";
    owner = getrawip(%client);
  };
  %block.mountable = true;
  %block.setEnergyLevel(60);
  %block.mountImage(nukebombimage,3);
  %block.mountImage(mountedImage,6);
}
  //%block.mountImage(mountedImagel,7);
}

function StealthBomber::onCollision(%this,%obj,%col,%vec,%speed)
{
   // Collision with other objects, including items
   %this.damage(0, VectorAdd(%obj.getPosition(),%vec), 20, "Impact");
} 
function StealthBomber::onTrigger(%data, %obj, %trigger, %state)
{
	if(%trigger == 0)
	{
		%obj.setImageTrigger(3, %state);
	}
}

function ServerCMDspawnStealthBomber(%client) {
	makestealth(%client);
}
//----------------------
