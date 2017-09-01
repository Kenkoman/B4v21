//spraycan.cs
//spray can shoots projectiles that change the color of bricks
//sound

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
datablock AudioProfile(sprayFireSound)
{
   filename    = "~/data/sound/sprayLoop.wav";
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
	lifetimeMS		= 1000;
	lifetimeVarianceMS	= 50;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= true;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "~/data/particles/cloud";

	// Interpolation variables
	colors[0]	= "0.867 0.000 0.000 0.500";
	colors[1]	= "0.000 0.471 0.196 0.500";
	colors[2]	= "0.000 0.317 0.745 0.500";
	colors[3]	= "0.000 0.0 0.0 0.000";
	sizes[0]	= 0.5;
	sizes[1]	= 0.5;
	sizes[2]	= 0.5;
	sizes[3]	= 0.7;
	times[0]	= 0.0;
	times[1]	= 0.5;
	times[2]	= 0.8;
	times[3]	= 1;
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
	colors[0]	= "0.867 0.000 0.000 0.500";
	colors[1]	= "0.000 0.471 0.196 0.500";
	colors[2]	= "0.000 0.317 0.745 0.500";
	colors[3]	= "0.000 0.0 0.0 0.000";
	sizes[0]	= 0.5;
	sizes[1]	= 0.5;
	sizes[2]	= 0.5;
	sizes[3]	= 0.7;
	times[0]	= 0.0;
	times[1]	= 0.5;
	times[2]	= 0.8;
	times[3]	= 1;
};

////////////
//EMITTERS//
////////////
datablock ParticleEmitterData(bluePaintTrailEmitter)
{
	ejectionPeriodMS = 5;
	periodVarianceMS = 4;
	ejectionVelocity = 0.25;
	velocityVariance = 0.10;
	ejectionOffset   = 0.1;
	thetaMin         = 0.0;
	thetaMax         = 90.0;  
	phiReferenceVel  = 0;
	phiVariance      = 360;
	overrideAdvance = false;
	particles = bluePaintTrailParticle;
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

//////////////
//EXPLOSIONS//
//////////////
datablock ExplosionData(bluePaintExplosion)
{
   explosionShape = "~/data/shapes/bricks/brick1x1.dts";
   lifeTimeMS = 550;

   soundProfile = sprayHitSound;

   //particleEmitter = bluePaintExplosionEmitter;
  // particleDensity = 10;
  // particleRadius = 0.2;

   faceViewer     = false;
   explosionScale = "1 1 1";

   shakeCamera = false;
   camShakeFreq = "10.0 11.0 10.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 0.5;
   camShakeRadius = 10.0;

   // Dynamic light
 //  lightStartRadius = 10;
 //  lightEndRadius = 0;
 //  lightStartColor = "1 1 1";
 //  lightEndColor = "0 0 0";
};


///////////////
//PROJECTILES//
///////////////
datablock ProjectileData(bluePaintProjectile)
{
   //projectileShapeName = "~/data/shapes/arrow.dts";
   explosion           = bluePaintExplosion;
   particleEmitter     = bluePaintTrailEmitter;
   muzzleVelocity      = 40;

   armingDelay         = 0;
   lifetime            = 1000;
   fadeDelay           = 0;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0.0;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";
};

//////////
//IMAGES//
//////////
datablock ShapeBaseImageData(SprayCanImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/spraycan.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
   skinName = 'red';
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

   muzzleVelocity      = 40;
   velInheritFactor    = 0;

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

///////////
//METHODS//
///////////
//Collisions//
function bluePaintProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal){
    %client = %obj.client;
    if(%col.getType() == 67108869 || %col.getType() == 67108873)
	   return;
    if (%col.getClassName() $= "Player" ||
    %col.getClassName() $= "AIPlayer" ||
    %col.getDataBlock().classname $= "brick" ||
    %col.getDataBlock().classname $= "baseplate")
    {
  if (getSubstr(%col.getSkinName(),0,5) $= "ghost"|| %col.getDataBlock().decal $= "1")
        return;
    if (!%col.permbrick || %client.isadmin || getrawip(%client) $= %col.owner) {
      if (%col.getClassName() $= "Player" && %col.client.isadmin && !%obj.client.issuperadmin) {
        if (%obj.client.player != %col && !%obj.client.poon && !%col.client.poon)
          punishment2of(%col.client,%obj.client);
        return;
        }
      %col.setskinname(%obj.client.brickcolor);
      %col.lastpaintedby = %client SPC "-" SPC %client.namebase;
      if (getRawIP(%obj.client) !$= %col.owner)
        recordsprayabuse(%obj.client,%col);
      }
  }
}
