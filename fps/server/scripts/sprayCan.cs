//spraycan.cs
//spray can shoots projectiles that change the color of bricks

//sound
datablock AudioProfile(sprayFireSound)
{
   filename    = "~/data/sound/sprayLoop.wav";
   description = AudioClosestLooping3d;
   preload = true;
};

datablock AudioProfile(sprayHitSound)
{
   filename    = "~/data/sound/sprayHit.wav";
   description = AudioClosest3d;
   preload = true;
};
datablock AudioProfile(sprayActivateSound)
{
   filename    = "~/data/sound/sprayActivate.wav";
   description = AudioClosest3d;
   preload = true;
};


/////////////
//PARTICLES//
/////////////
datablock ParticleData(bluePaintTrailParticle)
{
	dragCoefficient		= 3.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 300;
	lifetimeVarianceMS	= 50;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= true;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "~/data/particles/cloud";

	// Interpolation variables
	colors[0]	= "0.000 0.317 0.745 0.500";
	colors[1]	= "0.000 0.317 0.745 0.000";
	sizes[0]	= 0.5;
	sizes[1]	= 0.7;
	times[0]	= 0.0;
	times[1]	= 1.0;
};

datablock ParticleData(redPaintTrailParticle : bluePaintTrailParticle)
{
	colors[0]	= "0.867 0.000 0.000 0.500";
	colors[1]	= "0.867 0.000 0.000 0.000";
};
datablock ParticleData(greenPaintTrailParticle : bluePaintTrailParticle)
{
	colors[0]	= "0.000 0.471 0.196 0.500";
	colors[1]	= "0.000 0.471 0.196 0.000";
};
datablock ParticleData(yellowPaintTrailParticle : bluePaintTrailParticle)
{
	colors[0]	= "0.973 0.800 0.000 0.500";
	colors[1]	= "0.973 0.800 0.000 0.000";
};
datablock ParticleData(grayPaintTrailParticle : bluePaintTrailParticle)
{
	colors[0]	= "0.647 0.647 0.647 0.500";
	colors[1]	= "0.647 0.647 0.647 0.000";
};
datablock ParticleData(grayDarkPaintTrailParticle : bluePaintTrailParticle)
{
	colors[0]	= "0.471 0.471 0.471 0.500";
	colors[1]	= "0.471 0.471 0.471 0.500";
};
datablock ParticleData(whitePaintTrailParticle : bluePaintTrailParticle)
{
	colors[0]	= "0.996 0.996 0.910 0.500";
	colors[1]	= "0.996 0.996 0.910 0.000";
};
datablock ParticleData(blackPaintTrailParticle : bluePaintTrailParticle)
{
	colors[0]	= "0.200 0.200 0.200 0.500";
	colors[1]	= "0.200 0.200 0.200 0.000";
};
datablock ParticleData(brownPaintTrailParticle : bluePaintTrailParticle)
{
	colors[0]	= "0.400 0.200 0.000 0.500";
	colors[1]	= "0.400 0.200 0.000 0.000";
};

datablock ParticleData(bluePaintExplosionParticle)
{
	dragCoefficient      = 2;
	gravityCoefficient   = 0.0;
	inheritedVelFactor   = 0.0;
	constantAcceleration = 0.0;
	lifetimeMS           = 1000;
	lifetimeVarianceMS   = 200;
	textureName          = "~/data/particles/cloud";
	useInvAlpha		= true;
	spinSpeed		= 100.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	colors[0]     = "0.000 0.317 0.745 0.500";
	colors[1]     = "0.000 0.317 0.745 0.000";
	sizes[0]      = 0.8;
	sizes[1]      = 1.2;
};

datablock ParticleData(redPaintExplosionParticle : bluePaintExplosionParticle)
{
	colors[0]	= "0.867 0.000 0.000 0.500";
	colors[1]	= "0.867 0.000 0.000 0.000";
};
datablock ParticleData(greenPaintExplosionParticle : bluePaintExplosionParticle)
{
	colors[0]	= "0.000 0.471 0.196 0.500";
	colors[1]	= "0.000 0.471 0.196 0.000";
};
datablock ParticleData(yellowPaintExplosionParticle : bluePaintExplosionParticle)
{
	colors[0]	= "0.973 0.800 0.000 0.500";
	colors[1]	= "0.973 0.800 0.000 0.000";
};
datablock ParticleData(grayPaintExplosionParticle : bluePaintExplosionParticle)
{
	colors[0]	= "0.647 0.647 0.647 0.500";
	colors[1]	= "0.647 0.647 0.647 0.000";
};
datablock ParticleData(grayDarkPaintExplosionParticle : bluePaintExplosionParticle)
{
	colors[0]	= "0.471 0.471 0.471 0.500";
	colors[1]	= "0.471 0.471 0.471 0.500";
};
datablock ParticleData(whitePaintExplosionParticle : bluePaintExplosionParticle)
{
	colors[0]	= "0.996 0.996 0.910 0.500";
	colors[1]	= "0.996 0.996 0.910 0.000";
};
datablock ParticleData(blackPaintExplosionParticle : bluePaintExplosionParticle)
{
	colors[0]	= "0.200 0.200 0.200 0.500";
	colors[1]	= "0.200 0.200 0.200 0.000";
};
datablock ParticleData(brownPaintExplosionParticle : bluePaintExplosionParticle)
{
	colors[0]	= "0.400 0.200 0.000 0.500";
	colors[1]	= "0.400 0.200 0.000 0.000";
};

////////////
//EMITTERS//
////////////
datablock ParticleEmitterData(bluePaintTrailEmitter)
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
	particles = bluePaintTrailParticle;
};

datablock ParticleEmitterData(redPaintTrailEmitter : bluePaintTrailEmitter){
	particles = redPaintTrailParticle;
};
datablock ParticleEmitterData(greenPaintTrailEmitter : bluePaintTrailEmitter){
	particles = greenPaintTrailParticle;
};
datablock ParticleEmitterData(yellowPaintTrailEmitter : bluePaintTrailEmitter){
	particles = yellowPaintTrailParticle;
};
datablock ParticleEmitterData(grayPaintTrailEmitter : bluePaintTrailEmitter){
	particles = grayPaintTrailParticle;
};	
datablock ParticleEmitterData(grayDarkPaintTrailEmitter : bluePaintTrailEmitter){
	particles = grayDarkPaintTrailParticle;
};
datablock ParticleEmitterData(whitePaintTrailEmitter : bluePaintTrailEmitter){
	particles = whitePaintTrailParticle;
};
datablock ParticleEmitterData(blackPaintTrailEmitter : bluePaintTrailEmitter){
	particles = blackPaintTrailParticle;
};
datablock ParticleEmitterData(brownPaintTrailEmitter : bluePaintTrailEmitter){
	particles = brownPaintTrailParticle;
};

datablock ParticleEmitterData(bluePaintExplosionEmitter)
{
   ejectionPeriodMS = 5;
   periodVarianceMS = 0;
   ejectionVelocity = 5;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 10;
   thetaMax         = 10;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "bluePaintExplosionParticle";
};

datablock ParticleEmitterData(redPaintExplosionEmitter : bluePaintExplosionEmitter){
	particles = redPaintExplosionParticle;
};
datablock ParticleEmitterData(greenPaintExplosionEmitter : bluePaintExplosionEmitter){
	particles = greenPaintExplosionParticle;
};
datablock ParticleEmitterData(yellowPaintExplosionEmitter : bluePaintExplosionEmitter){
	particles = yellowPaintExplosionParticle;
};
datablock ParticleEmitterData(grayPaintExplosionEmitter : bluePaintExplosionEmitter){
	particles = grayPaintExplosionParticle;
};	
datablock ParticleEmitterData(grayDarkPaintExplosionEmitter : bluePaintExplosionEmitter){
	particles = grayDarkPaintExplosionParticle;
};
datablock ParticleEmitterData(whitePaintExplosionEmitter : bluePaintExplosionEmitter){
	particles = whitePaintExplosionParticle;
};
datablock ParticleEmitterData(blackPaintExplosionEmitter : bluePaintExplosionEmitter){
	particles = blackPaintExplosionParticle;
};
datablock ParticleEmitterData(brownPaintExplosionEmitter : bluePaintExplosionEmitter){
	particles = brownPaintExplosionParticle;
};

//////////////
//EXPLOSIONS//
//////////////
datablock ExplosionData(bluePaintExplosion)
{
   //explosionShape = "";
   lifeTimeMS = 150;

   soundProfile = sprayHitSound;

   particleEmitter = bluePaintExplosionEmitter;
   particleDensity = 10;
   particleRadius = 0.2;

   faceViewer     = false;
   explosionScale = "1 1 1";

   shakeCamera = false;
   camShakeFreq = "10.0 11.0 10.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 0.5;
   camShakeRadius = 10.0;

   // Dynamic light
   //lightStartRadius = 0;
   //lightEndRadius = 2;
   //lightStartColor = "0.3 0.6 0.7";
   //lightEndColor = "0 0 0";
};


//ok apparently, inheritance doesnt work for explosionDatas. Fucking garage games

datablock ExplosionData(redPaintExplosion){
	lifeTimeMS = 150;
	particleEmitter = redPaintExplosionEmitter;
	particleDensity = 10;
	particleRadius = 0.2;
	faceViewer     = false;
	explosionScale = "1 1 1";
	shakeCamera = false;
	soundProfile = sprayHitSound;
};
datablock ExplosionData(greenPaintExplosion){
	lifeTimeMS = 150;
	particleEmitter = greenPaintExplosionEmitter;
	particleDensity = 10;
	particleRadius = 0.2;
	faceViewer     = false;
	explosionScale = "1 1 1";
	shakeCamera = false;
	soundProfile = sprayHitSound;
};
datablock ExplosionData(yellowPaintExplosion){
	lifeTimeMS = 150;
	particleEmitter = yellowPaintExplosionEmitter;
	particleDensity = 10;
	particleRadius = 0.2;
	faceViewer     = false;
	explosionScale = "1 1 1";
	shakeCamera = false;
	soundProfile = sprayHitSound;
};
datablock ExplosionData(grayPaintExplosion){
	lifeTimeMS = 150;
	particleEmitter = grayPaintExplosionEmitter;
	particleDensity = 10;
	particleRadius = 0.2;
	faceViewer     = false;
	explosionScale = "1 1 1";
	shakeCamera = false;
	soundProfile = sprayHitSound;
};	
datablock ExplosionData(grayDarkPaintExplosion){
	lifeTimeMS = 150;
	particleEmitter = grayDarkPaintExplosionEmitter;
	particleDensity = 10;
	particleRadius = 0.2;
	faceViewer     = false;
	explosionScale = "1 1 1";
	shakeCamera = false;
	soundProfile = sprayHitSound;
};
datablock ExplosionData(whitePaintExplosion ){
	lifeTimeMS = 150;
	particleEmitter = whitePaintExplosionEmitter;
	particleDensity = 10;
	particleRadius = 0.2;
	faceViewer     = false;
	explosionScale = "1 1 1";
	shakeCamera = false;
	soundProfile = sprayHitSound;
};
datablock ExplosionData(blackPaintExplosion){
	lifeTimeMS = 150;
	particleEmitter = blackPaintExplosionEmitter;
	particleDensity = 10;
	particleRadius = 0.2;
	faceViewer     = false;
	explosionScale = "1 1 1";
	shakeCamera = false;
	soundProfile = sprayHitSound;
};
datablock ExplosionData(brownPaintExplosion){
	lifeTimeMS = 150;
	particleEmitter = brownPaintExplosionEmitter;
	particleDensity = 10;
	particleRadius = 0.2;
	faceViewer     = false;
	explosionScale = "1 1 1";
	shakeCamera = false;
	soundProfile = sprayHitSound;
};



///////////////
//PROJECTILES//
///////////////
datablock ProjectileData(bluePaintProjectile)
{
   //projectileShapeName = "~/data/shapes/arrow.dts";
   explosion           = bluePaintExplosion;
   particleEmitter     = bluePaintTrailEmitter;

   muzzleVelocity      = 20;
   velInheritFactor    = 0;

   armingDelay         = 0;
   lifetime            = 250;
   fadeDelay           = 0;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0.0;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";
};

datablock ProjectileData(redPaintProjectile : bluePaintProjectile){
	explosion		= redPaintExplosion;
	particleEmitter = redPaintTrailEmitter;
};
datablock ProjectileData(greenPaintProjectile : bluePaintProjectile){
	explosion		= greenPaintExplosion;
	particleEmitter = greenPaintTrailEmitter;
};
datablock ProjectileData(yellowPaintProjectile : bluePaintProjectile){
	explosion		= yellowPaintExplosion;
	particleEmitter = yellowPaintTrailEmitter;
};
datablock ProjectileData(grayPaintProjectile : bluePaintProjectile){
	explosion		= grayPaintExplosion;
	particleEmitter = grayPaintTrailEmitter;
};	
datablock ProjectileData(grayDarkPaintProjectile : bluePaintProjectile){
	explosion		= grayDarkPaintExplosion;
	particleEmitter = grayDarkPaintTrailEmitter;
};
datablock ProjectileData(whitePaintProjectile : bluePaintProjectile){
	explosion		= whitePaintExplosion;
	particleEmitter = whitePaintTrailEmitter;
};
datablock ProjectileData(blackPaintProjectile : bluePaintProjectile){
	explosion		= blackPaintExplosion;
	particleEmitter = blackPaintTrailEmitter;
};
datablock ProjectileData(brownPaintProjectile : bluePaintProjectile){
	explosion		= brownPaintExplosion;
	particleEmitter = brownPaintTrailEmitter;
};

/////////
//ITEMS//
/////////
datablock ItemData(sprayCan)
{
	category = "Weapon";  // Mission editor category
	className = "tool";   // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/spraycan.dts";
	skinName = 'blue';
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = false;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a spray can.';
	invName = 'Spray Can';
	image = blueSprayCanImage;
};



//////////
//IMAGES//
//////////
datablock ShapeBaseImageData(blueSprayCanImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/spraycan.dts";
   skinName = 'blue';
   emap = false;

   mountPoint = 0;
   offset = "0 0 0";

   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = sprayCan;
   ammo = " ";
   projectile = bluePaintProjectile;
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

   // Initial start up state
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.0;
	stateTransitionOnTimeout[0]       = "Ready";
	stateSound[0]					= sprayActivateSound;

	stateName[1]                    = "Ready";
	stateTransitionOnTriggerDown[1] = "Fire";
	stateAllowImageChange[1]        = true;
	

	stateName[2]					= "Fire";
	stateScript[2]                  = "onFire";
	stateFire[2]					= true;
	stateAllowImageChange[2]        = true;
	stateTimeoutValue[2]            = 0.10;
	stateTransitionOnTimeout[2]     = "Fire";
	stateTransitionOnTriggerUp[2]	= "Ready";
	stateEmitter[2]					= bluePaintExplosionEmitter;
	stateEmitterTime[2]				= 0.1;
	stateSound[2]					= sprayFireSound;

};

datablock ShapeBaseImageData(redSprayCanImage : blueSprayCanImage){
	skinName = 'red';
	projectile = redPaintProjectile;
};
datablock ShapeBaseImageData(greenSprayCanImage : blueSprayCanImage){
	skinName = 'green';
	projectile = greenPaintProjectile;
};
datablock ShapeBaseImageData(yellowSprayCanImage : blueSprayCanImage){
	skinName = 'yellow';
	projectile = yellowPaintProjectile;
};
datablock ShapeBaseImageData(graySprayCanImage : blueSprayCanImage){
	skinName = 'gray';
	projectile = grayPaintProjectile;
};
datablock ShapeBaseImageData(grayDarkSprayCanImage : blueSprayCanImage){
	skinName = 'grayDark';
	projectile = grayDarkPaintProjectile;
};
datablock ShapeBaseImageData(whiteSprayCanImage : blueSprayCanImage){
	skinName = 'white';
	projectile = whitePaintProjectile;
};
datablock ShapeBaseImageData(blackSprayCanImage : blueSprayCanImage){
	skinName = 'black';
	projectile = blackPaintProjectile;
};
datablock ShapeBaseImageData(brownSprayCanImage : blueSprayCanImage){
	skinName = 'brown';
	projectile = brownPaintProjectile;
};



///////////
//METHODS//
///////////

//using the paint can item
function sprayCan::onUse(%this,%player, %invPosition)
{
	%client = %player.client;
	if(%client){
		%color = %client.color;
	}
	%mountedImage = %player.getMountedImage($RightHandSlot);

	//if a spray can is already mounted
	if(%mountedImage.item $= "sprayCan" && (%player.currWeaponSlot == %invPosition)) {
		//mount the next color
		if(%mountedImage == nameToID("redSprayCanImage")){
			%image = yellowSprayCanImage;
			%client.color = "yellow";
		}			
		else if(%mountedImage == nameToID("yellowSprayCanImage")){
			%image = greenSprayCanImage;
			%client.color = "green";
		}
		else if(%mountedImage == nameToID("greenSprayCanImage")){
			%image = blueSprayCanImage;
			%client.color = "blue";
		}
		else if(%mountedImage == nameToID("blueSprayCanImage")){
			%image = whiteSprayCanImage;
			%client.color = "white";
		}
		else if(%mountedImage == nameToID("whiteSprayCanImage")){
			%image = graySprayCanImage;
			%client.color = "gray";
		}
		else if(%mountedImage == nameToID("graySprayCanImage")){
			%image = grayDarkSprayCanImage;
			%client.color = "grayDark";
		}
		else if(%mountedImage == nameToID("grayDarkSprayCanImage")){
			%image = blackSprayCanImage;
			%client.color = "black";
		}
		else if(%mountedImage == nameToID("blackSprayCanImage")){
			%image = redSprayCanImage;
			%client.color = "red";
		}
		else if(%mountedImage == nameToID("brownSprayCanImage")){
			%image = redSprayCanImage;
			%client.color = "red";
		}
		%player.mountImage(%image, $RightHandSlot, 1, %image.skinName);
	}
	//else,
	else {
		//if the client has a color, use that
		if(%color !$= ""){
			%image = nameToID(%color @ "SprayCanImage");
			%player.mountImage(%image, $RightHandSlot, 1, %image.skinName);
		}
		else{
			//else, mount the default
			%image = redSprayCanImage;
			%player.mountImage(%image, $RightHandSlot, 1, %image.skinName);
		}
		//hilight inv slot
		messageClient(%client, 'MsgHilightInv', '', %InvPosition);
		%player.currWeaponSlot = %invPosition;
	}
}

//Collisions//
function bluePaintProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	if(%col.getClassName() $= "StaticShape" || %col.getClassName() $= "Player")
		%col.setSkinName('Blue');
}
function redPaintProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	if(%col.getClassName() $= "StaticShape" || %col.getClassName() $= "Player")
		%col.setSkinName('red');
}
function yellowPaintProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	if(%col.getClassName() $= "StaticShape" || %col.getClassName() $= "Player")
		%col.setSkinName('yellow');
}
function greenPaintProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	if(%col.getClassName() $= "StaticShape" || %col.getClassName() $= "Player")
		%col.setSkinName('green');
}
function grayPaintProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	if(%col.getClassName() $= "StaticShape" || %col.getClassName() $= "Player")
		%col.setSkinName('base');
}
function grayDarkPaintProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	if(%col.getClassName() $= "StaticShape" || %col.getClassName() $= "Player")
		%col.setSkinName('graydark');
}
function whitePaintProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	if(%col.getClassName() $= "StaticShape" || %col.getClassName() $= "Player")
		%col.setSkinName('white');
}
function blackPaintProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	if(%col.getClassName() $= "StaticShape" || %col.getClassName() $= "Player")
		%col.setSkinName('black');
}
function brownPaintProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	if(%col.getClassName() $= "StaticShape" || %col.getClassName() $= "Player")
		%col.setSkinName('brown');
}
