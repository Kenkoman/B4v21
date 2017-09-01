// TBM DeltaWing Aircraft
// Model and texturing by Kerm Martian - http://www.cemetech.met
// Scripting by Luquado - edited by DShiznit
// Physics by Rob & Kerm
// Taken from Garage Game's sample scripts and modified.
//---------------------------------------------------------------------------------------
// Let's define our shiznit.
datablock ParticleData(JetEngineParticle)
{
   textureName          = "tbm/data/shapes/bricks/vehicles/dustParticle";
   dragCoefficient      = 5.0;
   gravityCoefficient   = 0;
   inheritedVelFactor   = -0.5;
   constantAcceleration = 0.0;
   lifetimeMS           = 700;
   lifetimeVarianceMS   = 0;
   colors[0]     = "0.76 0.36 0.26 1.0";
   colors[1]     = "0.76 0.46 0.36 0.0";
   sizes[0]      = 0.50;
   sizes[1]      = 0.40;
};
datablock ParticleEmitterData(JetEmitter)
{
   ejectionPeriodMS = 5;
   periodVarianceMS = 0;
   ejectionVelocity = 1;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 5;
   thetaMax         = 20;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   particles = "JetEngineParticle";
};

datablock FlyingVehicleData(DeltaWing)
{
	emap							= true;
	category						= "Vehicles";
	shapeFile						= "tbm/data/shapes/bricks/vehicles/dwing.dts";
	cloaktexture 					= "tbm/data/specialfx/cloakTexture";
	multipassenger					= false;
	computeCRC						= true;
									

	drag							= 0.2;
	density							= 3.0;

	//stateEmitter = JetEmitter;
   mountPose[0] = "fall";
   mountPointTransform[0] = "0 0 -1 0 0 1 0";
	numMountPoints					= 1;
	isProtectedMountPoint[0]		= false;
	cameraMaxDist					= 16.0; // Why so much? So that 3rd person would look right, that's why!
	cameraOffset					= 4.5;
	cameraLag						= 5.0;
    cameraRoll = true;         // Roll the camera with the vehicle for extra disorienting fun!

	explosion						= GodExplosion; // How bout now beeotch!
	explosionDamage					= 10.5;
	explosionRadius					= 15.0;

	maxDamage						= 1000;
	destroyedLevel					= 50.40;
									

	minDrag							= 30;           // Linear Drag (eventually slows you down when not thrusting...constant drag)
	rotationalDrag					= 10;        // Anguler Drag (dampens the drift after you stop moving the mouse...also tumble drag)

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
	enginesound = shipengine1sound;
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
	
	triggerDustHeight				= 4.0; // These don't matter, currently. Maybe later I'll add emitters for dust and contrail. Awww yeah.
	dustHeight						= 1.0;
//---This stuff figured out by Kerm
	damageEmitterOffset[0]			= "2.0 -3.0 0.45 ";
	damageEmitterOffset[1]			= "-2.0 -3.0 0.45 ";
	damageLevelTolerance[0]			= 0.0;
	damageLevelTolerance[1]			= 0.5;
	damageEmitter[0] 				= JetEmitter;
	damageEmitter[1] 				= JetEmitter;
	numDmgEmitterAreas				= 2;
//---						
	minMountDist					= 20;
						
	checkRadius						= 5.5;
	observeParameters				= "0 0 0";
									
	shieldEffectScale				= "0.937 1.125 0.60";
};

function servercmddwadd(%client) {
	makedelta(%client);
}
function makedelta(%client){
	if(%client.isAdmin || %client.isSuperAdmin)
	{
  %block = new FlyingVehicle() {
    position = vectoradd(%client.getcontrolobject().position,vectoradd("0 0 2",vectorscale(%client.getcontrolobject().getforwardvector(),"10 10 0")));
    rotation = "1 0 0 0";
    scale = "1 1 1";
    dataBlock = "DeltaWing";
    owner = getrawip(%client);
  };
  %block.mountable = true;
  %block.setEnergyLevel(60);
  %block.mountImage(DWWImagel,3);
  %block.mountImage(DWWImager,2);
  %block.mountImage(mountedImage,6);
}
  //%block.mountImage(mountedImagel,7);
}

function DeltaWing::onCollision(%this,%obj,%col,%vec,%speed)
{
   // Collision with other objects, including items
   %this.damage(0, VectorAdd(%obj.getPosition(),%vec), 20, "Impact");
} 
function DeltaWing::onTrigger(%data, %obj, %trigger, %state)
{
	if(%trigger == 0)
	{
		%obj.setImageTrigger(3, %state);
		%obj.setImageTrigger(2, %state);
	}
}

function ServerCMDspawnDeltaWing(%client) {
	makedelta(%client);
}
//projectile
datablock ProjectileData(DWingProjectile)
{
   projectileShapeName = "dtb/data/shapes/weapons/lasergreen.dts";
   explosion           = bulletExplosion;
   muzzleVelocity      = 200;
   
   scale               = "1.3 4 1.3";
   armingDelay         = 0;
   lifetime            = 80000;
   fadeDelay           = 75000;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 0;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "0 0.9 0";
};


////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(DWWImagel) {
   shapeFile = "tbm/data/shapes/bricks/vehicles/dwingwep.dts";
   emap = true;
   offset = "-3.26 2.8 -0.27";
   correctMuzzleVector = false;
   className = "WeaponImage";
   mountPoint = 0;
   ammo = " ";
   projectile = DWingProjectile;
   projectileType = Projectile;

   directDamage        = 100;
   radiusDamage        = 100;
   damageRadius        = 5;
   damagetype          = '%1 was blasted by %2\'s Delta Wing';
   muzzleVelocity      = 200;
   velInheritFactor    = 0;

   // Initial start up state
	stateName[0]                     = "Activate";
	stateTransitionOnTriggerDown[0]       = "Ready";

	stateName[1]                     = "Ready";
	stateTimeoutValue[1]             = 0.1;
	stateTransitionOnTimeout[1]  = "Fire";
	stateAllowImageChange[1]         = true;

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "Reload";
	stateTimeoutValue[2]            = 0.00001;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]		= true;
	stateSound[2]			= gunpackFireSound;

	stateName[3]			= "Reload";
	stateSequence[3]                = "Reload";
	stateAllowImageChange[3]        = false;
	stateTimeoutValue[3]            = 0.2;
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTimeout[3]     = "Check";

	stateName[4]			= "Check";
	stateTransitionOnTriggerUp[4]	= "StopFire";
	stateTransitionOnTriggerDown[4]	= "Fire";

	stateName[5]                    = "StopFire";
	stateTransitionOnTimeout[5]     = "Activate";
	stateTimeoutValue[5]            = 0.2;
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;
	stateScript[5]                  = "onStopFire";
};
datablock ShapeBaseImageData(DWWImager) {
   shapeFile = "tbm/data/shapes/bricks/vehicles/dwingwep.dts";
   emap = true;
   offset = "3.21 2.8 -0.27";
   correctMuzzleVector = false;
   className = "WeaponImage";
   mountPoint = 0;
   ammo = " ";
   projectile = DWingProjectile;
   projectileType = Projectile;

   directDamage        = 100;
   radiusDamage        = 100;
   damageRadius        = 5;
   damagetype          = '%1 was blasted by %2\'s Delta Wing';
   muzzleVelocity      = 200;
   velInheritFactor    = 0;

   // Initial start up state
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.00001;
	stateTransitionOnTimeout[0]       = "Ready";

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateAllowImageChange[1]         = true;

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "Reload";
	stateTimeoutValue[2]            = 0.00001;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]		= true;
	stateSound[2]			= gunpackFireSound;

	stateName[3]			= "Reload";
	stateSequence[3]                = "Reload";
	stateAllowImageChange[3]        = false;
	stateTimeoutValue[3]            = 0.2;
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTimeout[3]     = "Check";

	stateName[4]			= "Check";
	stateTransitionOnTriggerUp[4]	= "StopFire";
	stateTransitionOnTriggerDown[4]	= "Fire";

	stateName[5]                    = "StopFire";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 0.2;
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;
	stateScript[5]                  = "onStopFire";
};
function DWingProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	tbmcollison(%this,%obj,%col,%fade,%pos,%normal);
       tbmradiusDamage
     (%obj, VectorAdd(%pos, VectorScale(%normal, 0.01)),
      %obj.damageRadius, %obj.radiusDamage, %obj.damageType,10);
}

function DWWImagel::onFire(%this, %obj, %slot) {
%p = Parent::onFire(%this, %obj, %slot);
%p.sourceObject     = %obj.getMountNodeObject(0);
%p.client           = %obj.getMountNodeObject(0).client;
}

function DWWImager::onFire(%this, %obj, %slot) {
%p = Parent::onFire(%this, %obj, %slot);
%p.sourceObject     = %obj.getMountNodeObject(0);
%p.client           = %obj.getMountNodeObject(0).client;
}

//----------------------