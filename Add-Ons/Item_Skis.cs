//ski.cs

//skis let you slide downhill and skim across water




/////////////
// Effects //
/////////////
//skiEffects.cs

//support file for ski.cs

datablock AudioProfile(tumbleImpactASound)
{
   filename    = "./sound/impact2A.wav";
   description = AudioClose3d;
   preload = false;
};
datablock AudioProfile(skiImpactASound)
{
   filename    = "./sound/impact1A.wav";
   description = AudioClose3d;
   preload = false;
};

datablock AudioProfile(Impact1ASound)
{
   filename    = "./sound/impact1A.wav";
   description = AudioClosest3d;
   preload = true;
};
datablock AudioProfile(Impact1BSound)
{
   filename    = "./sound/impact1B.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock ParticleData(tumbleImpactAParticle1)
{
	dragCoefficient		= 3.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= -1.5;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 900;
	lifetimeVarianceMS	= 800;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= true;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "base/data/particles/cloud";
	//animTexName		= "~/data/particles/cloud";

	// Interpolation variables
	colors[0]	= "0.5 0.5 0.5 0.15";
	colors[1]	= "0.5 0.5 0.5 0.25";
	colors[2]	= "0.5 0.5 0.5 0.0";

	sizes[0]	= 2.0;
	sizes[1]	= 6.0;
	sizes[2]	= 4.0;

	times[0]	= 0.0;
	times[1]	= 0.5;
	times[2]	= 1.0;
};

datablock ParticleEmitterData(tumbleImpactAEmitter1)
{
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;
   lifeTimeMS	   = 15;
   ejectionVelocity = 8;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 180;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "tumbleImpactAParticle1";
};
datablock ExplosionData(tumbleImpactAExplosion)
{
   //explosionShape = "";
   lifeTimeMS = 150;

   soundProfile = tumbleImpactASound;

   emitter[0] = tumbleImpactAEmitter1;
   //particleDensity = 30;
   //particleRadius = 1.0;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = false;
   camShakeFreq = "7.0 8.0 7.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 0.5;
   camShakeRadius = 15.0;

};

//projectile
datablock ProjectileData(tumbleImpactAProjectile)
{
   //projectileShapeName = "base/data/shapes/empty.dts";
   directDamage        = 0;
   radiusDamage        = 0;
   damageRadius        = 0;
   explosion           = tumbleImpactAExplosion;
   //particleEmitter     = spearTrailEmitter;

   muzzleVelocity      = 50;
   velInheritFactor    = 1;

   explodeOnDeath		= 1;

   armingDelay         = 0;
   lifetime            = 10;
   fadeDelay           = 10;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 0.50;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";
};






datablock ParticleData(skiImpactAParticle1)
{
	dragCoefficient		= 0.7;
	windCoefficient		= 0.0;
	gravityCoefficient	= 1.5;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 900;
	lifetimeVarianceMS	= 800;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= true;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "base/data/particles/chunk";
	//animTexName		= "~/data/particles/cloud";

	// Interpolation variables
	colors[0]	= "1 1 1 0.2";
	colors[1]	= "1 1 1 0.8";
	colors[2]	= "1 1 1 0.0";

	sizes[0]	= 0.5;
	sizes[1]	= 0.5;
	sizes[2]	= 0.2;

	times[0]	= 0.0;
	times[1]	= 0.2;
	times[2]	= 1.0;
};

datablock ParticleEmitterData(skiImpactAEmitter1)
{
   ejectionPeriodMS = 2;
   periodVarianceMS = 0;
   lifeTimeMS	   = 15;
   ejectionVelocity = 12;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 30;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "skiImpactAParticle1";
};
datablock ExplosionData(skiImpactAExplosion)
{
   //explosionShape = "";
   lifeTimeMS = 150;

   soundProfile = skiImpactASound;

   emitter[0] = skiImpactAEmitter1;
   //particleDensity = 30;
   //particleRadius = 1.0;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = false;
   camShakeFreq = "7.0 8.0 7.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 0.5;
   camShakeRadius = 15.0;

};

//projectile
datablock ProjectileData(skiImpactAProjectile)
{
   //projectileShapeName = "base/data/shapes/empty.dts";
   directDamage        = 0;
   radiusDamage        = 0;
   damageRadius        = 0;
   explosion           = skiImpactAExplosion;
   //particleEmitter     = spearTrailEmitter;

   muzzleVelocity      = 50;
   velInheritFactor    = 1;

   explodeOnDeath		= 1;

   armingDelay         = 0;
   lifetime            = 10;
   fadeDelay           = 10;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 0.50;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";
};








//////////
// item //
//////////

datablock ItemData(SkiItem)
{
	category = "Item";  // Mission editor category

	equipment = true;

	//its already a member of item namespace so dont break it
	//className = "Item"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./shapes/ski.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Skis";
	iconName = "./ItemIcons/Skis";
	doColorShift = true;
	colorShiftColor = "0.000 0.200 0.640 1.000";
	
	 // Dynamic properties defined by the scripts
	image = SkiWeaponImage;
	canDrop = true;
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(SkiWeaponImage)
{
   // Basic Item properties
   shapeFile = "./shapes/ski.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "-0.17 0.17 -0.07";
   rotation = eulerToMatrix("-90 90 0");
   eyeOffset = "0.7 1.2 -0.15";
   eyeRotation = eulerToMatrix("90 -90 0");

   doColorShift = true;
	colorShiftColor = SkiItem.colorShiftColor;
   

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = false;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = SkiItem;

   //melee particles shoot from eye node for consistancy
   melee = true;
   //raise your arm up or not
   armReady = true;

   //casing = " ";

   doColorShift = true;
   colorShiftColor = SkiItem.colorShiftColor; //"0.200 0.200 0.200 1.000";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.5;
	stateTransitionOnTimeout[0]       = "Ready";

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateAllowImageChange[1]         = true;

	stateName[2]                    = "Fire";
	stateTransitionOnTriggerUp[2]	= "Ready";
	stateScript[2]                  = "onFire";
};
function SkiWeaponImage::onFire(%this, %obj, %slot)
{
	%player = %obj;

	if(%player.isMounted())
	{
		%mountedVehicleName = %player.getObjectMount().getDataBlock().getName();
		
		if(%mountedVehicleName !$= "skiVehicle")
		{	
			//we're mounted on some other kind of vehicle
			commandToClient(%player.client, 'CenterPrint', "\c4Can\'t use skis right now.", 2);
			//messageClient(%player.client, 'Clientmsg', 'Can\'t use skis right now.');
			return;
		}
		else
		{
			//we're mounted on a skiVehicle, so stop skiing
			%player.stopSkiing();
			%player.unMount();
			
		}
	}
	else
	{
		//we are not mounted on anything
		if(vectorLen(%player.getVelocity()) <= 10)
		{
			%player.startSkiing();
			//messageClient(%player.client, 'MsgEquipInv', '', %InvPosition);
			commandToClient(%player.client,'setScrollMode', -1);

			//%player.isEquiped[%invPosition] = true;
			%player.unMountimage(%slot);

			fixArmReady(%player);
		}
		else
		{
			//echo(" velocity = ", vectorLen(%player.getVelocity()));
			commandToClient(%player.client, 'CenterPrint', "\c4Can\'t use skis while moving.", 2);
			//messageClient(%player.client, 'CenterPrint', 'Can\'t use skis while moving.');
		}

	}
}
function SkiItem::onUse(%this, %player, %InvPosition)
{

	%playerData = %player.getDataBlock();
	%client = %player.client;

	if(%player.getObjectMount())
		%mountedVehicleName = %player.getObjectMount().getDataBlock().getName();
		
	//if(%mountedVehicleName !$= "skiVehicle")
	//{
		%player.updateArm(SkiWeaponImage);
		%player.MountImage(SkiWeaponImage, 0);
	//}

	return;

	if(%player.isMounted())
	{
		%mountedVehicleName = %player.getObjectMount().getDataBlock().getName();
		
		if(%mountedVehicleName !$= "skiVehicle")
		{	
			//we're mounted on some other kind of vehicle
			messageClient(%player.client, 'Clientmsg', 'Can\'t use skis right now.');
			return;
		}
		else
		{
			//we're mounted on a skiVehicle, so stop skiing
			%player.stopSkiing();
		}
	}
	else
	{
		//we are not mounted on anything
		if(vectorLen(%player.getVelocity()) <= 0.1)
		{
			%player.startSkiing();
			messageClient(%player.client, 'MsgEquipInv', '', %InvPosition);
			%player.isEquiped[%invPosition] = true;
		}
		else
		{
			messageClient(%player.client, 'Clientmsg', 'Can\'t use skis while moving.');
		}

	}
}


function Player::startSkiing(%obj)
{	
	//make a new ski vehicle and mount the player on it
	%client = %obj.client;
	%position = %obj.getTransform();
	%posX = getword(%position, 0);
	%posY = getword(%position, 1);
	%posZ = getword(%position, 2);
	%rot = getWords(%position, 3, 8);

	%posZ += 0.3;

	%vel = %obj.getVelocity();

	%newcar = new WheeledVehicle() 
	{
		dataBlock = skivehicle;
		client = %client;
		initialPosition = %posX @ " " @ %posY @ " " @ %posZ;
	};
	%newcar.setVelocity(%vel);
	//%newcar.setVelocity(%obj.getVelocity() * 90);
	%newcar.setTransform(%posX @ " " @ %posY @ " " @ %posZ @ " " @ %rot);
		
	//%obj.client.setcontrolobject(%newcar);
	//%obj.setArmThread(root);
	%newcar.schedule(250, mountObject, %obj, 0);
	//%newcar.mountObject(%obj,0);
	//%obj.setTransform("0 0 0 0 0 1 0");

	%obj.unhidenode(Lski);
	%obj.unhidenode(Rski);

	%color = getColorIDTable(%client.currentColor);
	%obj.setNodeColor("LSki", %color);
	%obj.setNodeColor("RSki", %color);

	//%obj.setNodeColor("Lski", SkiItem.colorShiftColor);
	//%obj.setNodeColor("Rski", SkiItem.colorShiftColor);
}


function Player::stopSkiing(%obj)
{
	//%skiVehicle = %obj.getObjectMount();
	//if(%skiVehicle)
	//{
	//	if(%skiVehicle.getDataBlock().getName() $= "skiVehicle")
	//	{
	//		%skiVehicle.delete();  
	//	}
	//}
	%obj.hidenode(Lski);
	%obj.hidenode(Rski);

	return;


	//VVV super old inventory stuff VVV

	//de-equip-hilight inv slot
	%player = %obj;
	%playerData = %player.getDataBlock();
	%client = %player.client;
	for(%i = 0; %i < %playerData.maxItems; %i++)							//search through other inv slots
	{
		if(%player.isEquiped[%i] == true)									//if it is equipped then
		{
			if(%player.inventory[%i] == ski.getId())								//if it is skis
			{		
				messageClient(%client, 'MsgDeEquipInv', '', %i);			//then de-equip it 
				%player.isEquiped[%i] = false;
				break;														//we're done because only one item can interfere
			}										
		}
	}

}










//deathVehicle - invisible vehicle that you are mounted on when you die so you bounce around
//ski vehicle - slippery, stable vehicle that lets you ski 



datablock WheeledVehicleData(deathVehicle)
{
	//tagged fields
	doSimpleDismount = true;		//just unmount the player, dont look for a free space


   category = "Vehicles";
   shapeFile = "./shapes/deathVehicle.dts";
   emap = true;

   maxDamage = 1.0;
   destroyedLevel = 0.5;

   maxSteeringAngle = 0.885;  // Maximum steering angle, should match animation
   //tireEmitter = TireEmitter; // All the tires use the same dust emitter

   // 3rd person camera settings
   cameraRoll = false;         // Roll the camera with the vehicle
   cameraMaxDist = 6;         // Far distance from vehicle
   cameraOffset = 0;        // Vertical offset from camera mount point
   cameraLag = 0.0;           // Velocity lag of camera
   cameraDecay = 0.0;        // Decay per sec. rate of velocity lag

   // Rigid Body
   mass = 90;		//3100 lbs
   density = 0.5;
   massCenter = "0.0 0.0 1.25";    // Center of mass for rigid body
   massBox = "1.25 1.25 2.65";         // Size of box used for moment of inertia,
                              // if zero it defaults to object bounding box
   drag = 0.6;                // Drag coefficient
   bodyFriction = 0.6;
   bodyRestitution = 0.7;
   minImpactSpeed = 5;        // Impacts over this invoke the script callback
   softImpactSpeed = 1;       // Play SoftImpact Sound
   hardImpactSpeed = 1;      // Play HardImpact Sound
   integration = 10;          // Physics integration: TickSec/Rate
   collisionTol = 0.1;        // Collision distance tolerance
   contactTol = 0.1;          // Contact velocity tolerance

   // Engine
   engineTorque = 0;       // Engine power
   engineBrake = 0;         // Braking when throttle is 0
   brakeTorque = 0;        // When brakes are applied
   maxWheelSpeed = 0;        // Engine scale by current speed / max speed

	forwardThrust		= 0;
	reverseThrust		= 0;
	lift			= 0;
	maxForwardVel		= 40;
	maxReverseVel		= 10;
	horizontalSurfaceForce	= 0;   // Horizontal center "wing" (provides "bite" into the wind for climbing/diving and turning)
	verticalSurfaceForce	= 0; 
	rollForce		= 0;
	yawForce		= 0;
	pitchForce		= 0;
	rotationalDrag		= 1.0;
	stallSpeed		= 0;


   // Energy
   maxEnergy = 0;
   jetForce = 0;
   minJetEnergy = 0;
   jetEnergyDrain = 0;

   // Sounds
//   jetSound = ScoutThrustSound;
   //engineSound = Impact1ASound;
   //squealSound = Impact1ASound;
   softImpactSound = Impact1ASound;
   hardImpactSound = Impact1BSound;
   //wheelImpactSound = Impact1BSound;

//   explosion = VehicleExplosion;
};



function deathVehicle::onImpact(%this,%obj)
{
	//%trans = %obj.getMountedObject(0).getWorldBoxCenter();
	%trans = %obj.getMountedObject(0).getEyePoint();
	%p = new Projectile()
	{
		dataBlock = tumbleImpactAProjectile;
		initialVelocity  = "0 0 0";
		initialPosition  = %trans;
		sourceObject     = %obj;
		sourceSlot       = 0;
		client           = %obj.client;
	};
	MissionCleanup.add(%p);
	%p.setTransform(%trans);
}

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
   shapeFile = "./shapes/emptyWheel.dts";
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
	textureName          = "base/data/particles/cloud";
	dragCoefficient      = 2.0;
	gravityCoefficient   = -0.1;
	inheritedVelFactor   = 0.1;
	constantAcceleration = 0.0;
	lifetimeMS           = 800;
	lifetimeVarianceMS   = 250;
	useInvAlpha = false;

	colors[0]     = "0.9 0.9 0.9 0.0";
	colors[1]     = "0.9 0.9 0.9 1.0";
	colors[2]     = "0.9 0.9 0.9 0.0";

	sizes[0]      = 0.50;
	sizes[1]      = 0.70;
	sizes[2]      = 0.25;

	times[0] = 0.0;
	times[1] = 0.2;
	times[2] = 1.0;
};

datablock ParticleEmitterData(SkiEmitter)
{
   ejectionPeriodMS = 18;
   periodVarianceMS = 0;
   ejectionVelocity = 1;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 5;
   thetaMax         = 20;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   particles = SkiParticle;
};

datablock WheeledVehicleData(skiVehicle)
{
	//tagged fields
	doSimpleDismount = true;		//just unmount the player, dont look for a free space

   category = "Vehicles";
   shapeFile = "./shapes/skiVehicle.dts";
   emap = true;

   maxDamage = 1.0;
   destroyedLevel = 0.5;

   maxSteeringAngle = 0.885;  // Maximum steering angle, should match animation
   tireEmitter = SkiEmitter; // All the tires use the same dust emitter

   // 3rd person camera settings
   cameraRoll = false;         // Roll the camera with the vehicle?
   cameraMaxDist = 11;         // Far distance from vehicle
   cameraOffset = 6.8;        // Vertical offset from camera mount point
   cameraLag = 0.0;           // Velocity lag of camera
   cameraDecay = 1.75;  //0.75;      // Decay per sec. rate of velocity lag
   cameraTilt = 0.3201; //tilt adjustment for camera: ~20 degrees down

   // Rigid Body
   mass = 90;		//3100 lbs
   density = 0.5;
   massCenter = "0.0 0.0 0.5";    // Center of mass for rigid body
   massBox = "1.5 1.5 1.5";         // Size of box used for moment of inertia,
                              // if zero it defaults to object bounding box
   drag = 0.8;                // Drag coefficient
   bodyFriction = 0.21;
   bodyRestitution = 0.2;
   minImpactSpeed = 3;        // Impacts over this invoke the script callback
   softImpactSpeed = 3;       // Play SoftImpact Sound
   hardImpactSpeed = 10;      // Play HardImpact Sound
   integration = 10;           // Physics integration: TickSec/Rate
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
	pitchForce		= 1000;
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
   //engineSound = "";
   //squealSound = sprayFireSound;
   //softImpactSound = Impact1ASound;
   hardImpactSound = Impact1BSound;
   //wheelImpactSound = Impact1BSound;

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
	if(isObject(%player))
	{
		%player.stopSkiing();
	}
	else
	{	
		//%obj.schedule(10, delete);
		return;
	}

	//this method would be nice, but something goes wrong with the collision hull and mass center
		//%obj.schedule(10, setDatablock, deathVehicle);
		//%obj.schedule(45 * 1000, delete);
		//%obj.schedule(2000, tumbleCheck);
		//%player.canDismount = false;
		//%client = %player.client;
		//if(!isObject(%client))	
		//	return;
		//%client.camera.setMode("Corpse",%obj);
		//%client.setControlObject(%client.camera);
		//return;
	

	//return;
	
	//old tumbling method follows//


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
	

	%player.canDismount = false;
	%player.setWhiteout(%time/7000 * 1);
	tumble(%player, %time);

	return;

	if(!%player.isTumbling)
	{
		%player.canDismount = false;
		%player.setWhiteout(%time/7000 * 1);
		tumble(%player, %time);
		%player.unmountImage($leftfootslot);
		//de-equip proper inv slot;
		//for(%i = 0; i < 5; %i++)
		//{
			
		//}
	}

}


function skiVehicle::onImpact(%this,%obj)
{
	//echo("skivehicle impact");
	%trans = %obj.getTransform();
	%p = new Projectile()
	{
		dataBlock = skiImpactAProjectile;
		initialVelocity  = "0 0 0";
		initialPosition  = %trans;
		sourceObject     = %obj;
		sourceSlot       = 0;
		client           = %obj.client;
	};
	MissionCleanup.add(%p);
	%p.setTransform(%trans);
}


//called when the driver is unmounted for whatever reason
function DeathVehicle::onDriverLeave(%this, %obj, %player)
{
	//echo("**************** skiVehicle::onDriverLeave ", %this, " ", %obj);
	%obj.setTransform("0 0 -9999");
	%obj.schedule(10, delete);

}
function skiVehicle::onDriverLeave(%this, %obj, %player)
{
	//echo("**************** skiVehicle::onDriverLeave ", %this, " ", %obj, " player=", %player);

	if(isObject(%player))
		%player.stopSkiing();

	%obj.setTransform("0 0 -9999");
	%obj.schedule(10, delete);
}






function tumble(%obj, %time)
{
	//return;

	//mount the object on a new deathvehicle for %time milliseconds
	%currentVehicle = %obj.getObjectMount();
	%client = %obj.client;

	%newcar = new WheeledVehicle() 
	{
		dataBlock = deathVehicle;
		client = %client;
		initialPosition = %posX @ " " @ %posY @ " " @ %posZ;
	};
	
	//%newcar = %client.tumbleVehicle;
	%newcar.setVelocity("0 0 0");

	if(!%newcar)
		return;
	
	//neutralize current velocity
	%newcar.applyImpulse( %newcar.getPosition(), vectorScale(%newcar.getVelocity() * -1, %newcar.getDataBlock().mass) );

	//error("player tumbling!");
	%obj.canDismount = false;

	if(%currentVehicle && (%currentVehicle.getDataBlock().getName() $= "skiVehicle") )
	{
		//%obj.client.setControlObject(%obj);
		//match ski vehicle
		//neutralize velocity first
		
		%newcar.setTransform(%currentVehicle.getTransform());
		%newcar.applyImpulse( %newcar.getPosition(), vectorScale(%currentVehicle.getVelocity(), %newcar.getDataBlock().mass) );
		%newcar.mountObject(%obj, 0);

		%currentVehicle.setTransform("0 0 -1000");
		%currentVehicle.schedule(500, delete);
	}
	else
	{
		//match player
		//error("transform = ",%obj.getTransform());
		%newcar.setTransform(%obj.getTransform());
		%newcar.applyImpulse( %newcar.getPosition(), vectorScale(%obj.getVelocity(), %newcar.getDataBlock().mass) );
		//%newcar.setTransform(%obj.getTransform());

		//%obj.setTransform("0 0 0 0 0 1 0");

		%newcar.mountObject(%obj, 0);

		//error("not skiing");
		//error("transform = ",%obj.getTransform());
	}	
		
	//definitely delete after 45 seconds
	%newcar.schedule(45 * 1000, delete);

	%newcar.schedule(2000, tumbleCheck);

	%client.camera.setMode("Corpse",%obj);
	%client.setControlObject(%client.camera);

	//remove %player.istumbling after a we stop
	//schedule(%time, %obj, stopTumble, %obj);


	//%nextTumbleVehicle = new WheeledVehicle() 
	//{
	//	dataBlock = deathVehicle;
	//	client = %client;
	//	initialPosition = %posX @ " " @ %posY @ " " @ %posZ;
	//};
	//%obj.client.tumbleVehicle = %nextTumbleVehicle;
	//%nextTumbleVehicle.setTransform("0 0 -90");



	//%newcar.schedule(%time, unmountobject, %obj);
	//%obj.schedule(%time, setcontrolobject, 0);
	//%newcar.schedule(%time + 250, setTransform, "0 0 -90");
}

function Vehicle::tumbleCheck(%obj)
{
	%obj.getDataBlock().tumbleCheck(%obj);
}

function deathVehicle::tumbleCheck(%this, %obj)
{
	if(vectorLen(%obj.getVelocity()) < 1 || %obj.getWaterCoverage() > 0.3)
	{
		%player = %obj.getMountedObject(0);
		if(isObject(%player))
		{
			%player.canDismount = true;
			%player.stopSkiing();
		}
		%obj.schedule(10, delete);
	}
	else
	{
		%obj.schedule(2000, tumbleCheck);
	}
}

function deathVehicle::onRemove(%this, %obj)
{
//	echo("deathvehicle::onremove ", %this, " ", %obj);
	%player = %obj.getMountedObject(0);
	if(isObject(%player))
	{
		%player.canDismount = true;
	}
}