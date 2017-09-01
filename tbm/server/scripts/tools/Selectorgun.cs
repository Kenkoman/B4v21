//selectorgun.cs
//Selector gun shoots out an object that on collision will select that object
//sound

/////////////
//PARTICLES//
/////////////
datablock ParticleData(selectorgunTrailParticle)
{
	dragCoefficient		= 3.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 200;
	lifetimeVarianceMS	= 50;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= false;
	animateTexture		= false;

	textureName		= "~/data/particles/ring";

	// Interpolation variables
	colors[0]	= "0.5 0.5 0 0.500";
	sizes[0]	= 0.1;
	times[0]	= 0.0;
};


datablock ParticleData(selectorgunExplosionParticle)
{
	dragCoefficient      = 2;
	gravityCoefficient   = 0.0;
	inheritedVelFactor   = 0.0;
	constantAcceleration = 0.0;
	lifetimeMS           = 300;
	lifetimeVarianceMS   = 200;
	textureName          = "~/data/particles/lbolt";
	useInvAlpha		= false;
	spinSpeed		= 100.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	colors[0]	= "0.5 0.5 0 0.500";
	sizes[0]	= 0.1;
	times[0]	= 0.0;
};

////////////
//EMITTERS//
////////////
datablock ParticleEmitterData(selectorgunTrailEmitter)
{
	ejectionPeriodMS = 20;
	periodVarianceMS = 5;
	ejectionVelocity = 0.5;
	velocityVariance = 0.1;
	ejectionOffset   = 0.1;
	thetaMin         = 0.0;
	thetaMax         = 90.0;  
	phiReferenceVel  = 0;
	phiVariance      = 360;
	overrideAdvance = false;
	particles = selectorgunTrailParticle;
};

datablock ParticleEmitterData(selectorgunExplosionEmitter)
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
   particles = "selectorgunExplosionParticle";
};

//////////////
//EXPLOSIONS//
//////////////
datablock ExplosionData(selectorgunExplosion)
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
};

///////////////
//PROJECTILES//
///////////////
datablock ProjectileData(selectorgunProjectile)
{
   //projectileShapeName = "~/data/shapes/arrow.dts";
   explosion           = selectorgunExplosion;
   particleEmitter     = selectorgunTrailEmitter;
   muzzleVelocity      = 200;

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
datablock ShapeBaseImageData(selectorgunImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/spraycan.dts";
   cloaktexture = "~/data/specialfx/cloakTexture";
   skinName = 'ghostyellow';
   emap = true;
    
   mountPoint = 0;
   offset = "0 0 0";

   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = selectorgun;
   ammo = " ";
   projectile = selectorgunProjectile;
   projectileType = Projectile;

   muzzleVelocity      = 200;
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
	stateEmitter[2]					= selectorgunExplosionEmitter;
	stateEmitterTime[2]				= 0.1;

};

///////////
//METHODS//
///////////
//Collisions//
function selectorgunProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal) {
  if(%col.getType() == 67108869) //terrain type
	return;
  if (%obj.client.edit && %col != %obj.client.lastswitch) {
    if (getsubstr(%col.getskinname(),0,5) $= "ghost" || %col.getdataBlock() $= "" || (%obj.client.ismod && (%col.owner != getrawip(%obj.client) || %col.client.isadmin))) {
      messageClient(%obj.client, 'MsgBrickLimit', "\c3 Sorry but you may not select this object.");
      return;
      }
    messageClient(%obj.client, 'MsgBrickLimit', "\c2"@%col.dataBlock@"\c3 has been selected");
    if (isobject(%obj.client.lastswitch))
      %obj.client.lastswitch.setskinname(%obj.client.lastswitchcolor);
    %obj.client.lastswitch=%col;
    %obj.client.lastswitchcolor=%col.getskinname();
    if (!%col.getdataBlock().decal)
        handleghostcolor(%obj.client,%col);
} else if ($crownchase && %col.client.carrier) {
    if (isobject($prisonlocation))
      %col.settransform($prisonlocation.getposition());
    return;
    }
}
function serverCmdEditorGunMode(%client) {
  if (%client.isadmin || %client.issuperadmin || %client.ismod) {
    if (%client.player.getmountedimage(0)==nametoid(selectorgunImage) && %client.edit) {
      %client.edit=0;
      if (isobject(%client.lastswitch))
        %client.lastswitch.setskinname(%client.lastswitchcolor);
      messageClient(%client, 'MsgNormalMode', "\c2You are in \c3NORMAL\c2 mode.");
      %client.player.unmountimage(0);
      }
    else if (%client.player.getmountedimage(0)!=nametoid(selectorgunImage) && %client.edit) {
      messageClient(%client, 'MsgHilightInv', '', -1);
      %client.player.currWeaponSlot = -1;
      %client.player.mountimage(selectorgunImage,0,1,taggedghostcolor(%client));
      }
    else if (%client.player.getmountedimage(0)!=nametoid(selectorgunImage) && !%client.edit) {
      messageClient(%client, 'MsgHilightInv', '', -1);
      %client.player.currWeaponSlot = -1;
      %client.player.mountimage(selectorgunImage,0,1,taggedghostcolor(%client));
      %client.edit=1;
      messageClient(%client, 'MsgEditMode', "\c2You are in \c3EDIT\c2 mode.");
      if (isobject(%client.lastswitch)) {
         messageClient(%client, 'Msg', "\c2Warning! You have \c3"@%client.lastswitch.dataBlock@"\c2 selected.");
         %client.lastswitchcolor=%client.lastswitch.getskinname();
        if (!%client.lastswitch.getdataBlock().decal)
            handleghostcolor(%client,%client.lastswitch);
         }
      if(%client.player.tempBrick) {
       	%client.player.tempBrick.delete();
	%client.player.tempBrick = "";
      }
    }
  }
}
//stupid function to get the tagged version of a ghost color
//What's wrong with addTaggedString?
function taggedghostcolor(%client) {
switch$(%client.ghostcolor) {
case "ghostyellow":
return 'ghostyellow';
case "ghostred":
return 'ghostred';
case "ghostblue":
return 'ghostblue';
case "ghostgreen":
return 'ghostgreen';
case "ghostwhite":
return 'ghostwhite';
case "ghostgrey":
return 'ghostgrey';
case "ghostaqua":
return 'ghostaqua';
case "ghostpurple":
return 'ghostpurple';
case "ghostbrown":
return 'ghostbrown';
case "ghostblueorange":
return 'ghostblueorange';
case "ghostblueprint":
return 'ghostblueprint';
}
}
