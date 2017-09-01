//Camerass.cs
//Camera for Taking Screenshots.
//Created by: Ephialtes

datablock AudioProfile(HitSound)
{
   filename    = "~/data/sound/sprayHit.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(FireSound)
{



   filename    = "~/data/sound/camera_click.wav";
   description = AudioClosest3d;
   preload = true;



};

datablock AudioProfile(ActivateSound)
{
   filename    = "~/data/sound/cameraactivate.wav";
   description = AudioClosest3d;
   preload = true;
};


/////////////
//PARTICLES//
/////////////
datablock ParticleData(cameraflashParticle)
{
	dragCoefficient		= 3.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 300;
	lifetimeVarianceMS	= 0;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= true;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "~/data/particles/dot";

	// Interpolation variables
	colors[0]	= "0.914 0.852 0.742 0.500";
	colors[1]	= "0.914 0.852 0.742 0.000";
	sizes[0]	= 0.5;
	sizes[1]	= 0.7;
	times[0]	= 0.0;
	times[1]	= 1.0;
};





////////////
//EMITTERS//
////////////
datablock ParticleEmitterData(cameraflashEmitter)
{
	ejectionPeriodMS = 5;
	periodVarianceMS = 5;
	ejectionVelocity = 0; //0.25;
	velocityVariance = 0; //0.10;
	ejectionOffset   = 0.1;
	thetaMin         = 0.0;
	thetaMax         = 90.0;  
	phiReferenceVel  = 0;
	phiVariance      = 360;
	overrideAdvance = false;
	particles = cameraflashParticle;

   // Dynamic light
   lightStartRadius = 3;
   lightEndRadius = 0;
   lightStartColor = "00.0 0.2 0.6";
   lightEndColor = "0 0 0";
};



///////////////
//PROJECTILES//
///////////////
datablock ProjectileData(cameraProjectile)
{
   //projectileShapeName = "~/data/shapes/arrow.dts";
   particleEmitter     = cameraflashEmitter;

   muzzleVelocity      = 20;
   velInheritFactor    = 0;

   armingDelay         = 0;
   lifetime            = 75;
   fadeDelay           = 0;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0.0;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";
};




/////////
//ITEMS//
/////////
datablock ItemData(SSCamera)
{
	category = "Weapon";  // Mission editor category
	className = "tool";   // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/camera.dts";
	skinName = 'yellow';
	rotate = false;
	mass = 1;
	cost = 100;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = false;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a Camera';
	invName = 'Camera';
	image = camerassImage;
};



//////////
//IMAGES//
//////////
datablock ShapeBaseImageData(camerassImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/camera.dts";
   skinName = 'yellow';
   emap = false;

   mountPoint = 0;
   offset = "-0.25 0 -0.2";

   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = SSCamera;
   ammo = " ";
   projectile = cameraProjectile;
   projectileType = Projectile;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = true;

   //casing = " ";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.5;
	stateTransitionOnTimeout[0]      = "Ready";
	stateSound[0]			 = ActivateSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateAllowImageChange[1]         = true;

	stateName[2]                     = "Fire";
	stateTransitionOnTimeout[2]      = "Reload";
	stateTimeoutValue[2]             = 0.3;
	stateFire[2]                     = true;
	stateAllowImageChange[2]         = false;
	stateSequence[2]                 = "Fire";
	stateScript[2]                   = "onFire";
	stateWaitForTimeout[2]		 = true;
	

	stateName[3]			 = "Reload";
	stateSequence[3]                 = "Reload";
	stateAllowImageChange[3]         = false;
	stateTimeoutValue[3]             = 1;
	stateWaitForTimeout[3]		 = true;
	stateTransitionOnTimeout[3]      = "Check";

	stateName[4]			 = "Check";
	stateTransitionOnTriggerUp[4]	 = "StopFire";
	stateTransitionOnTriggerDown[4]	 = "Fire";

	stateName[5]                     = "StopFire";
	stateTransitionOnTimeout[5]      = "Ready";
	stateTimeoutValue[5]             = 0.2;
	stateAllowImageChange[5]         = false;
	stateWaitForTimeout[5]		 = true;
	//stateSequence[5]               = "Reload";
	stateScript[5]                   = "onStopFire";





};



///////////
//METHODS//
///////////


function SSCamera::onUse(%this,%player, %invPosition)
{
	//if the image is mounted already, unmount it
	//if it isnt, mount it

	%mountPoint = %this.image.mountPoint;
	%mountedImage = %player.getMountedImage(%mountPoint); 
	%mountedCamera = %this.image.getId();
	
	if(%mountedImage)
	{
		//echo(%mountedImage);
		if(%mountedImage == %this.image.getId())
		{
			//our image is already mounted so unmount it
				%player.unMountImage(%mountPoint);
				messageClient(%client, 'MsgHilightInv', '', '0');
				%client.cameramounted = 0;
		}
		else
		{
			//something else is there so mount our image
				%client = %player.client;
				%image = camerassImage;
				%player.mountImage(%image, $RightHandSlot, 1, %image.skinName);
				messageClient(%client, 'MsgHilightInv', '', %InvPosition);
				%player.currWeaponSlot = %invPosition;
				%client.cameramounted = 1;
		}
	}
	else
	{
		//nothing there so mount 
			%client = %player.client;
			%image = camerassImage;
			%player.mountImage(%image, $RightHandSlot, 1, %image.skinName);
			messageClient(%client, 'MsgHilightInv', '', %InvPosition);
			%player.currWeaponSlot = %invPosition;
			%client.cameramounted = 1;
	}
}

function camerassImage::onFire(%this, %obj, %slot)
{
%client = %obj.client;
if(%client.cmode $= 1)
{
%obj.playaudio(0,FireSound);
commandtoclient(%client,'takePicture');
messageClient(%client,"",'You took a Picture with your \c3Camera!');
schedule(30,0,takecamss,%obj.client);
}
else
{
messageClient(%client,"",'You need to be in \c3Camera Mode!');
}
}

function takecamss(%client)
{
screenShot("Photograph_" @ formatImageNumber($screenshotNumber++) @ ".png", "PNG");
toggleflashon(%client);
}

function toggleflashoff(%client)
{

commandtoclient(%client, 'zoomCamReset', %client);

}

function toggleflashon(%client)
{
commandtoclient(%client, 'turnonFlash');
schedule(60,0,toggleflashoff,%client);
}




function serverCmdsczoomin(%client)
{
if(%client.cmode $= 1)
{
commandtoclient(%client, 'zoomCam', %client);
}
}


function serverCmdtogglecammode(%client)
{
if(%client.cameramounted $= 1)
{
if(%client.cmode != 1)
{

	commandtoclient(%client, 'enterCamMode');
	%client.cmode = 1;

}
else
{

	commandtoclient(%client, 'exitCamMode');
	%client.cmode = 0;
	%client.zoomlevel = 0;
}
}
}