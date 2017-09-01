// TBM SigmaFighter Aircraft
// Model and texturing by Kerm Martian - http://www.cemetech.met
// Scripting by Luquado
// Physics by Rob & Kerm
// Taken from Garage Game's sample scripts and modified.
//---------------------------------------------------------------------------------------
// Let's define our shiznit.
datablock ParticleData(SigmaEngineParticle)
{
   textureName          = "tbm/data/shapes/bricks/vehicles/dustParticle";
   dragCoefficient      = 5.0;
   gravityCoefficient   = 0;
   inheritedVelFactor   = -0.5;
   constantAcceleration = 0.0;
   lifetimeMS           = 300;
   lifetimeVarianceMS   = 0;
   colors[0]     = "0.1 0.36 0.9 1.0";
   colors[1]     = "0.15 0.46 0.85 0.0";
   sizes[0]      = 0.50;
   sizes[1]      = 0.40;
};
datablock ParticleEmitterData(SigmaEmitter)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   ejectionVelocity = 1;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 5;
   thetaMax         = 20;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   particles = "SigmaEngineParticle";
};

datablock FlyingVehicleData(SigmaFighter)
{
	emap							= true;
	category						= "Vehicles";
	shapeFile						= "tbm/data/shapes/bricks/vehicles/sigmafighter2.dts";
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
	cameraMaxDist					= 20.0; // Why so much? So that 3rd person would look right, that's why!
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
	enginesound = shipengine2sound;
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
	damageEmitterOffset[0]			= "0.5 2.0 -2.8 ";
	damageEmitterOffset[1]			= "-0.5 2.0 -2.8 ";
	damageLevelTolerance[0]			= 0.0;
	damageLevelTolerance[1]			= 0.5;
	damageEmitter[0] 				= SigmaEmitter;
	damageEmitter[1] 				= SigmaEmitter;
	numDmgEmitterAreas				= 2;
//---						
	minMountDist					= 20;
						
	checkRadius						= 5.5;
	observeParameters				= "0 0 0";
									
	shieldEffectScale				= "0.937 1.125 0.60";
};

function servercmdsigfight(%client) {
if(%client.isAdmin || %client.isSuperAdmin)
	{
  %block = new FlyingVehicle() {
    position = vectoradd(%client.getcontrolobject().position,vectoradd("0 0 8",vectorscale(%client.getcontrolobject().getforwardvector(),"10 10 0")));
    rotation = "1 0 0 0";
    scale = "1 1 1";
    dataBlock = "SigmaFighter";
    owner = getrawip(%client);
  };
  %block.mountable = true;
  %block.setEnergyLevel(60);
  %block.mountImage(SFImagec,4);
  %block.mountImage(SFImagel,3);
  %block.mountImage(SFImager,2);
  %block.mountImage(mountedImage,6);
  //%block.mountImage(mountedImagel,7);
	}
}

function SigmaFighter::onCollision(%this,%obj,%col,%vec,%speed)
{
   // Collision with other objects, including items
   %this.damage(0, VectorAdd(%obj.getPosition(),%vec), 20, "Impact");
} 
function SigmaFighter::onTrigger(%data, %obj, %trigger, %state)
{
//echo("trigger" SPC %trigger);
	if(%trigger == 0)
	{
		%obj.setImageTrigger(4, %state);
		%obj.setImageTrigger(3, %state);
		%obj.setImageTrigger(2, %state);
	}
}

//projectile
datablock ParticleData(SigmaGunTrailParticle)
{
textureName = "tbm/data/shapes/bricks/vehicles/riftspin";
	useInvAlpha =  false;
	windCoefficient		= 0.0;
	dragCoeffiecient = 100.0;
	inheritedVelFactor = 0.01;

	lifetimeMS = 1500;
	
	lifetimeVarianceMS = 10;

	times[0] = 0.0;
	times[1] = 0.8;
	times[2] = 1.0;

	colors[0] = "0.1 0.1 0.6 0.8";
	colors[1] = "0.1 0.1 0.7 0.8";
	colors[2] = "0.1 0.0 0.8 0.0";
	sizes[0] = 1.5;
	sizes[1] = 1.0;
	sizes[2] = 1.0;
};

datablock ParticleEmitterData(SigmaGunTrailEmitter)
{
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;

   ejectionVelocity = 0.5;
   velocityVariance = 0.10;

   ejectionOffset = 0;

   thetaMin         = 0.0;
   thetaMax         = 90.0;  

   particles = SigmaGunTrailParticle;
};
datablock ProjectileData(SigmaProjectile)
{
   projectileShapeName = "dtb/data/shapes/weapons/bluemissile/Missile.dts";
   explosion           = ConRifleBulletExplosion;
   particleEmitter     = SigmaGunTrailEmitter;

   scale               = "1 6 1";
   armingDelay         = 0;
   lifetime            = 80000;
   fadeDelay           = 75000;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 0;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "0.9 0 0";
};


////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(SFImagel) {
   shapeFile = "tbm/data/shapes/bricks/vehicles/dwingwep.dts";
   emap = true;
   offset = "-4.2 -2 -0.75";
   correctMuzzleVector = false;
   className = "WeaponImage";
   mountPoint = 0;
   ammo = " ";
   projectile = SigmaProjectile;
   projectileType = Projectile;

   directDamage        = 200;
   radiusDamage        = 200;
   damageRadius        = 8.0;
   damagetype          = '%2\'s Sigma Fighter tore %1 to shreds';
   muzzleVelocity      = 100;
   velInheritFactor    = 0;

   // Initial start up state
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.5;
	stateTransitionOnTimeout[0]       = "Ready";

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateAllowImageChange[1]         = true;

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "Reload";
	stateTimeoutValue[2]            = 1.0;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]		= true;
	stateSound[2]			= MissileLauncherFireSound;

	stateName[3]			= "Reload";
	stateSequence[3]                = "Reload";
	stateAllowImageChange[3]        = false;
	stateTimeoutValue[3]            = 1.0;
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTimeout[3]     = "Check";

	stateName[4]			= "Check";
	stateTransitionOnTriggerUp[4]	= "StopFire";
	stateTransitionOnTriggerDown[4]	= "Fire";

	stateName[5]                    = "StopFire";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 1.0;
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;
	stateScript[5]                  = "onStopFire";
};

function SFImageL::onFire(%this, %obj, %slot) {
%p = Parent::onFire(%this, %obj, %slot);
%p.sourceObject     = %obj.getMountNodeObject(0);
%p.client           = %obj.getMountNodeObject(0).client;
}

datablock ShapeBaseImageData(SFImager) {
   shapeFile = "tbm/data/shapes/bricks/vehicles/dwingwep.dts";
   emap = true;
   offset = "4.2 -2 -0.75";
   correctMuzzleVector = false;
   className = "WeaponImage";
   mountPoint = 0;
   ammo = " ";
   projectile = SigmaProjectile;
   projectileType = Projectile;

   directDamage        = 200;
   radiusDamage        = 200;
   damageRadius        = 8.0;
   damagetype          = '%2\'s Sigma Fighter tore %1 to shreds';
   muzzleVelocity      = 100;
   velInheritFactor    = 0;

   // Initial start up state
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.5;
	stateTransitionOnTimeout[0]       = "Ready";

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateAllowImageChange[1]         = true;

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "Reload";
	stateTimeoutValue[2]            = 1.0;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]		= true;
	stateSound[2]			= MissileLauncherFireSound;

	stateName[3]			= "Reload";
	stateSequence[3]                = "Reload";
	stateAllowImageChange[3]        = false;
	stateTimeoutValue[3]            = 1.0		;
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTimeout[3]     = "Check";

	stateName[4]			= "Check";
	stateTransitionOnTriggerUp[4]	= "StopFire";
	stateTransitionOnTriggerDown[4]	= "Fire";

	stateName[5]                    = "StopFire";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 1.0;
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;
	stateScript[5]                  = "onStopFire";
};

function SFImageR::onFire(%this, %obj, %slot) {
%p = Parent::onFire(%this, %obj, %slot);
%p.sourceObject     = %obj.getMountNodeObject(0);
%p.client           = %obj.getMountNodeObject(0).client;
}

datablock ProjectileData(SigmaCProjectile)
{
   projectileShapeName = "dtb/data/shapes/weapons/laserblast.dts";
   explosion           = bulletExplosion;
   muzzleVelocity      = 200;

   scale               = "2 3 2";
   armingDelay         = 0;
   lifetime            = 80000;
   fadeDelay           = 75000;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 0;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "0.9 0 0";
};

datablock ShapeBaseImageData(SFImagec) {
   shapeFile = "tbm/data/shapes/bricks/vehicles/dwingwep.dts";
   emap = true;
   offset = "0 3.5 -1";
   correctMuzzleVector = false;
   className = "WeaponImage";
   mountPoint = 0;
   ammo = " ";
   projectile = SigmaCProjectile;
   projectileType = Projectile;

   directDamage        = 100;
   radiusDamage        = 100;
   damageRadius        = 5;
   damagetype          = '%2\'s Sigma Fighter turned %1 into plasma';
   muzzleVelocity      = 200;
   velInheritFactor    = 0;

   // Initial start up state
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.5;
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
	stateSound[2]			= LaserRepeaterFireSound;

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
	stateTimeoutValue[5]            = 1.0;
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;
	stateScript[5]                  = "onStopFire";
};

function SFImageC::onFire(%this, %obj, %slot) {
%p = Parent::onFire(%this, %obj, %slot);
%p.sourceObject     = %obj.getMountNodeObject(0);
%p.client           = %obj.getMountNodeObject(0).client;
}

function SigmaProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	tbmcollison(%this,%obj,%col,%fade,%pos,%normal);
       tbmradiusDamage
     (%obj, VectorAdd(%pos, VectorScale(%normal, 0.01)),
      %obj.damageRadius,%obj.radiusDamage,%obj.damageType,10);
}

function SigmaCProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	tbmcollison(%this,%obj,%col,%fade,%pos,%normal);
       tbmradiusDamage
     (%obj, VectorAdd(%pos, VectorScale(%normal, 0.01)),
      %obj.damageRadius,%obj.radiusDamage,%obj.damageType,10);
}

//----------------------