datablock ParticleData(eyeDropperExplosionParticle)
{
   dragCoefficient      = 0.5;
   gravityCoefficient   = -0.15;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 500;
   lifetimeVarianceMS   = 300;
   textureName          = "~/data/particles/cloud";
   colors[0]     = "0.5 0.5 0.5 1";
   colors[1]     = "0 0 0 0";
   sizes[0]      = 1;
   sizes[1]      = 0;
};

datablock ParticleEmitterData(eyeDropperExplosionEmitter)
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   ejectionVelocity = 5;
   velocityVariance = 0.1;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 10;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "eyeDropperExplosionParticle";
};

datablock ExplosionData(eyeDropperExplosion)
{
   lifeTimeMS = 500;

   particleEmitter = eyeDropperExplosionEmitter;
   particleDensity = 10;
   particleRadius = 0.2;

   emitter[0] = eyeDropperExplosionEmitter;
   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = false;
   camShakeFreq = "20.0 22.0 20.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 0.5;
   camShakeRadius = 10.0;

   // Dynamic light
   lightStartRadius = 3;
   lightEndRadius = 1;
   lightStartColor = "0.5 0.5 0.5";
   lightEndColor = "0 0 0";
};


//projectile
datablock ProjectileData(eyeDropperProjectile)
{
   explosion           = eyeDropperExplosion;
   muzzleVelocity      = 600;

   armingDelay         = 0;
   lifetime            = 3000;
   fadeDelay           = 3000;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0.0;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(eyeDropperImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/eyedropper.dts";
   emap = false;
   cloakable = false;
   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0.1";
   //eyeOffset = "0.1 0.2 -0.55";

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = false;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = eyeDropper;
   ammo = " ";
   projectile = eyeDropperProjectile;
   projectileType = Projectile;

   directDamage        = 0;
   radiusDamage        = 0;
   damageRadius        = 0.5;
   muzzleVelocity      = 600;
   velInheritFactor    = 1;

   //melee particles shoot from eye node for consistancy
   melee = true;
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
	stateTimeoutValue[0]             = 0.5;
	stateTransitionOnTimeout[0]       = "Ready";

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "PreFire";
	stateAllowImageChange[1]         = true;

	stateName[2]			= "PreFire";
	stateScript[2]                  = "onPreFire";
	stateAllowImageChange[2]        = false;
	stateTimeoutValue[2]            = 0.1;
	stateTransitionOnTimeout[2]     = "Fire";

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "CheckFire";
	stateTimeoutValue[3]            = 0.1;
	stateFire[3]                    = true;
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	stateScript[3]                  = "onFire";
	stateWaitForTimeout[3]		= true;

	stateName[4]			= "CheckFire";
	stateTransitionOnTriggerUp[4]	= "StopFire";
	stateTransitionOnTriggerDown[4]	= "Fire";

	stateName[5]                    = "StopFire";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 0.2;
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;
	stateSequence[5]                = "StopFire";
	stateScript[5]                 = "onStopFire";
};

function eyeDropperProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	tbmcollison(%this, %obj, %col, %fade, %pos, %normal);

	%player = %obj.client.player;
	%client = %player.client;

	if(!%player || %col.getType() == 67108869)
		return;
    %colData = %col.getDataBlock();
	%colDataClass = %colData.classname;
	if(%col.getClassName() $= "Player" ||
		%col.getClassName() $= "AIPlayer" || 
		%colDataClass $= "brick" ||
		%col.getDataBlock().classname $= "baseplate")
	{
        if(getSubstr(%col.getSkinName(),0,5) !$= "ghost" && %colData.decal != 1)
		%client.brickcolor = addTaggedString(%col.getSkinName());
        else if(%col .getSkinName() $= "")
        %client.brickcolor = "base";
	}
}

function serverCmdToggleEyeDropper(%client) { 
	if (%client.player.getmountedimage(0)==nametoid(eyeDropperImage)) 
		%client.player.unmountimage(0); 
	else { 
		%client.player.mountimage(eyeDropperImage,0,1,%client.brickcolor); 
		messageClient(%client, 'MsgHilightInv', '', -1); 
		%client.player.currWeaponSlot = -1; 
	}  
}