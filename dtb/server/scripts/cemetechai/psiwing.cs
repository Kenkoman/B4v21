// TBM Psiwing Aircraft 
// Model and texturing by Kerm Martian - http://www.cemetech.met 
// Scripting by Luquado & Kerm -edit by DShiznit
// Physics by Rob & Kerm 
//--------------------------------------------------------------------------------------- 
// Let's define our shiznit. 

datablock FlyingVehicleData(PsiWing) 
{ 
   emap                     = true; 
   category                  = "Vehicles"; 
   shapeFile                  = "tbm/data/shapes/bricks/vehicles/psiwing.dts"; 
   cloaktexture                = "tbm/data/specialfx/cloakTexture"; 
   multipassenger               = false; 
   computeCRC                  = true; 
                            

   drag                     = 0.1; 
   density                     = 3.0; 

   //stateEmitter = JetEmitter; 
   mountPose[0] = "fall"; 
   mountPointTransform[0] = "0 0 -1 0 0 1 0"; 
   numMountPoints               = 1; 
   isProtectedMountPoint[0]      = false; 
   cameraMaxDist               = 16.0; // Why so much? So that 3rd person would look right, that's why! 
   cameraOffset               = 4.5; 
   cameraLag                  = 5.0; 
    cameraRoll = true;         // Roll the camera with the vehicle for extra disorienting fun! 

	explosion						= GodExplosion; // How bout now beeotch! 
   explosionDamage               = 10.5; 
   explosionRadius               = 15.0; 

   maxDamage                  = 1000; 
   destroyedLevel               = 50.40; 
                            

   minDrag                     = 30;           // Linear Drag (eventually slows you down when not thrusting...constant drag) 
   rotationalDrag               = 5;        // Anguler Drag (dampens the drift after you stop moving the mouse...also tumble drag) 

   maxAutoSpeed               = 5;       // Autostabilizer kicks in when less than this speed. (meters/second) //10 
   autoAngularForce            = 50;       // Angular stabilizer force (this force levels you out when autostabilizer kicks in) //200 
   autoLinearForce               = 1;        // Linear stabilzer force (this slows you down when autostabilizer kicks in) //200 
   autoInputDamping            = 0.95;      // Dampen control input so you don't` whack out at very slow speeds 
    integration = 5;           // Physics integration: TickSec/Rate 
    collisionTol = 0.2;        // Collision distance tolerance 
    contactTol = 0.1; 
    
   // Maneuvering 
   maxSteeringAngle            = 1;    // Max radiens you can rotate the wheel. Smaller number is more maneuverable. 
   horizontalSurfaceForce         = 200;   // Horizontal center "wing" (provides "bite" into the wind for climbing/diving and turning) 
   verticalSurfaceForce         = 200;     // Vertical center "wing" (controls side slip. lower numbers make MORE slide.) 
   maneuveringForce            = 1000;      // Horizontal jets (W,S,D,A key thrust) 
   steeringForce               = 100;         // Steering jets (force applied when you move the mouse) 
   steeringRollForce            = 0;      // Steering jets (how much you heel over when you turn) 
   rollForce                  = 80;  // Auto-roll (self-correction to right you after you roll/invert) //80 
   hoverHeight                  = 0;       // Height off the ground at rest 
   createHoverHeight            = 80;  // Height off the ground when created 
   maxForwardSpeed               = 50;  // speed in which forward thrust force is no longer applied (meters/second) 

   // Turbo Jet 
   enginesound = shipengine2sound;
   jetForce                  = 3000;      // Afterburner thrust (this is in addition to normal thrust) 
   minJetEnergy               = 28;     // Afterburner can't be used if below this threshhold. 
   jetEnergyDrain               = 2.8;       // Energy use of the afterburners (low number is less drain...can be fractional)                                                                                                                                                                                                                                                                                          // Auto stabilize speed 
   vertThrustMultiple            = 3.0; 

   // Rigid body 
   mass                     = 30;        // Mass of the vehicle 
   bodyFriction               = 0;     // Don't mess with this. 
   bodyRestitution               = 0.1;   // When you hit the ground, how much you rebound. (between 0 and 1) 
   minRollSpeed               = 2000;     // Don't mess with this. 
   softImpactSpeed               = 3;       // Sound hooks. This is the soft hit. 
   hardImpactSpeed               = 15;    // Sound hooks. This is the hard hit. 

   // Ground Impact Damage (uses DamageType::Ground) 
   minImpactSpeed               = 10;      // If hit ground at speed above this then it's an impact. Meters/second 
   speedDamageScale            = 0.06; 

   // Object Impact Damage (uses DamageType::Impact) 
   collDamageThresholdVel         = 23.0; 
   collDamageMultiplier         = 0.02; 

   minTrailSpeed               = 15;      // The speed your contrail shows up at. 
    
   triggerDustHeight            = 4.0; // These don't matter, currently. Maybe later I'll add emitters for dust and contrail. Awww yeah. 
   dustHeight                  = 1.0; 
//---This stuff figured out by Kerm 
   damageEmitterOffset[0]         = "2.0 -4.4 1.75 "; 
   damageEmitterOffset[1]         = "-2.0 -4.4 1.75 "; 
   damageLevelTolerance[0]         = 0.0; 
   damageLevelTolerance[1]         = 0.5; 
   damageEmitter[0]             = JetEmitter; 
   damageEmitter[1]             = JetEmitter; 
   numDmgEmitterAreas            = 2; 
//---                   
   minMountDist               = 20; 
                   
   checkRadius                  = 5.5; 
   observeParameters            = "0 0 0"; 
                            
   shieldEffectScale            = "0.937 1.125 0.60"; 
}; 

function servercmdpsiadd(%client) { 
if(%client.isAdmin || %client.isSuperAdmin)
	{
  %block = new FlyingVehicle() { 
    position = vectoradd(%client.getcontrolobject().position,vectoradd("0 0 2",vectorscale(%client.getcontrolobject().getforwardvector(),"10 10 0"))); 
    rotation = "1 0 0 0"; 
    scale = "1 1 1"; 
    dataBlock = "PsiWing"; 
    owner = getrawip(%client); 
  }; 
  %block.mountable = true; 
  %block.setEnergyLevel(60); 
  %block.mountImage(nukebombimage,2);
  //%block.mountImage(mountedImagel,7); 
	}
} 

function servercmdpsiaddm(%client) { 
if(%client.isAdmin || %client.isSuperAdmin)
	{
  %block = new FlyingVehicle() { 
    position = vectoradd(%client.getcontrolobject().position,vectoradd("0 0 2",vectorscale(%client.getcontrolobject().getforwardvector(),"10 10 0"))); 
    rotation = "1 0 0 0"; 
    scale = "1 1 1"; 
    dataBlock = "PsiWing"; 
    owner = getrawip(%client); 
  }; 
  %block.mountable = true; 
  %block.setEnergyLevel(60); 
  %block.mountImage(nukebombimage,2);
  %block.mountImage(mountedImage,6); 
  //%block.mountImage(mountedImagel,7); 
  messageAll('msgwhatever', %client.namebase@" has spawned PsiWing ID# "@%block@"."); 
  schedule((5000),0,PsiWingechospeed,%block,%client); 
  }
} 
function PsiWing::onCollision(%this,%obj,%col,%vec,%speed) 
{ 
   // Collision with other objects, including items 
   %this.damage(0, VectorAdd(%obj.getPosition(),%vec), 20, "Impact"); 
} 
function PsiWing::onTrigger(%data, %obj, %trigger, %state) 
{ 
   if(%trigger == 0) 
   { 
      %obj.setImageTrigger(2, %state); 
   } 
} 
datablock ProjectileData(nukebombProjectile)
{
   projectileShapeName = "dtb/data/shapes/weapons/mortarbomb.dts";
   explosion           = MortarCannonExplosion;
   muzzleVelocity      = 0;

   armingDelay         = 0000;
   lifetime            = 80000;
   fadeDelay           = 75000;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 1;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0.9 0";
};


////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(nukebombimage) {
   shapeFile = "tbm/data/shapes/bricks/vehicles/dwingwep.dts";
   emap = true;
   offset = "0 0 0";
   correctMuzzleVector = false;
   className = "WeaponImage";
   mountPoint = 0;
   ammo = " ";
   projectile = nukebombprojectile;
   projectileType = Projectile;

   directDamage        = 200;
   radiusDamage        = 400;
   damageRadius        = 12;
   damagetype          = '%1 was carpetbombed by %2';
   muzzleVelocity      = 0;
   velInheritFactor    = 0.9;

   // Initial start up state
	stateName[0]                     = "Activate";
	stateTransitionOnTriggerup[0]       = "Ready";

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateAllowImageChange[1]         = true;

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "Reload";
	stateTimeoutValue[2]            = 0.05;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]		= true;
	stateSound[2]			= spearfiresound;

	stateName[3]			= "Reload";
	stateSequence[3]                = "Reload";
	stateAllowImageChange[3]        = false;
	stateTimeoutValue[3]            = 0.05;
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTimeout[3]     = "Check";

	stateName[4]			= "Check";
	stateTransitionOnTriggerUp[4]	= "Activate";
	stateTransitionOnTriggerDown[4]	= "Activate";

};

function nukebombProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
if(%col == %obj.sourceObject || %col == %obj.sourceObject.getMountNodeObject(0))
return;
	tbmcollison(%this,%obj,%col,%fade,%pos,%normal);
       tbmradiusDamage
     (%obj, VectorAdd(%pos, VectorScale(%normal, 0.01)),
      %this.damageRadius,%this.radiusDamage,%this.damageType,100);
}

function nukeBombImage::onFire(%this, %obj, %slot) {
%p = Parent::onFire(%this, %obj, %slot);
if(isObject(%obj.getMountNodeObject(0))) {
  %p.sourceObject     = %obj.getMountNodeObject(0);
  %p.client           = %obj.getMountNodeObject(0).client;
}
}

//----------------------