//screwdriver.cs

//screwdriver weapon

//effects
datablock ParticleData(screwdriverExplosionParticle)
{
   dragCoefficient      = 5;
   gravityCoefficient   = 0.1;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 500;
   lifetimeVarianceMS   = 300;
   textureName          = "~/data/particles/chunk";
   colors[0]     = "0.9 0.9 0.7 0.9";
   colors[1]     = "0.9 0.9 0.9 0.0";
   sizes[0]      = 0.2;
   sizes[1]      = 0.1;
};

datablock ParticleEmitterData(screwdriverExplosionEmitter)
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   ejectionVelocity = 9;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 60;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "screwdriverExplosionParticle";
};

datablock ExplosionData(screwdriverExplosion)
{
   //explosionShape = "";
   lifeTimeMS = 500;

   particleEmitter = screwdriverExplosionEmitter;
   particleDensity = 10;
   particleRadius = 0.2;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = true;
   camShakeFreq = "20.0 22.0 20.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 0.5;
   camShakeRadius = 10.0;

   // Dynamic light
   lightStartRadius = 3;
   lightEndRadius = 1;
   lightStartColor = "00.6 0.0 0.0";
   lightEndColor = "0 0 0";
};


//projectile
datablock ProjectileData(screwdriverProjectile)
{
   //projectileShapeName = "~/data/shapes/spearProjectile.dts";
   directDamage        = 10;
   radiusDamage        = 10;
   damageRadius        = 0.5;
   explosion           = screwdriverExplosion;
   //particleEmitter     = as;

   muzzleVelocity      = 50;
   velInheritFactor    = 1;

   armingDelay         = 0;
   lifetime            = 100;
   fadeDelay           = 70;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0.0;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";
};


//////////
// item //
//////////
datablock ItemData(screwdriver)
{
	category = "Tools";  // Mission editor category
	className = "Tool"; // For inventory system
	cost = 50;
	 // Basic Item Properties
	shapeFile = "~/data/shapes/screwdriver.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
        skinName = 'black';

	 // Dynamic properties defined by the scripts
	pickUpName = 'a Screwdriver';
	invName = 'Screwdriver';
	image = screwdriverImage;
};

//function screwdriver::onUse(%this,%user)
//{
//	//mount the image in the right hand slot
//	%user.mountimage(%this.image, $RightHandSlot);
//}

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(screwdriverImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/screwdriver.dts";
   PreviewFileName = "rtb/data/shapes/bricks/Previews/screwdriver.png";
   emap = true;
	cloakable = false;
   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
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
   item = screwdriver;
   ammo = " ";
   projectile = screwdriverProjectile;
   projectileType = Projectile;

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
	stateTransitionOnTimeout[3]     = "Fire";
	stateTimeoutValue[3]            = 0.2;
	stateFire[3]                    = true;
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	stateScript[3]                  = "onFire";
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTriggerUp[3]	= "StopFire";


	stateName[4]                    = "StopFire";
	stateTransitionOnTimeout[4]     = "Ready";
	stateTimeoutValue[4]            = 0.2;
	stateAllowImageChange[4]        = false;
	stateWaitForTimeout[4]		= true;
	stateSequence[4]                = "StopFire";
	stateScript[4]                  = "onStopFire";


};

function screwdriverProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	if($Pref::Server::BombRigging == 1)
	{
		if(%col.bombID > 0)
		{
			if(%col.riggerID.plantedbomb >= 1)
			{
				if(%col.riggerID $= %obj.client)
				{
					%obj.client.plantedbomb = 0;
					%obj.client.plantedplunger = 0;
					messageClient(%obj.client,"","\c4 Deactivated the Rigged Bomb.");
					%col.dead = true;
					%col.schedule(10, explode);
					for(%t = 0; %t < MissionCleanup.getCount(); %t++)
					{
					%possbomb = MissionCleanup.getObject(%t);

					if(%possbomb.bombID $= %col.bombID && %possbomb != %col)
					{
						%possbomb.dead = true;
						%possbomb.schedule(10, explode);
					}
					}
				}
				else
				{
					%col.riggerID.plantedbomb = 0;
					%col.dead = true;
					%col.schedule(10, explode);
					messageAll('name', '\c0%1 \c5Defused \c0%2s \c5Bomb!', %obj.client.name, %col.riggerID.name);
					for(%t = 0; %t < MissionCleanup.getCount(); %t++)
					{
					%possbomb = MissionCleanup.getObject(%t);

					if(%possbomb.bombID $= %col.bombID && %possbomb != %col)
					{
						%possbomb.dead = true;
						%possbomb.schedule(10, explode);
					}
					}
				}
			}
			if(%col.riggerID.timerigged $= 1)
			{
				if(%col.riggerID == %obj.client)
				{
					%obj.client.timerigged = 0;
					%col.bombID = 0;
					cancel(%obj.client.bombschedule);
					%col.riggerID = 0;
					%col.NoDestroy = 0;
					%col.NoBreak = 0;
					messageClient(%obj.client,"","\c4Deactivated the Timed Bomb.");
					%col.dead = true;
					%col.schedule(10, explode);
				}
				else
				{	
					%col.riggerID.timerigged = 0;
					messageAll('name', '\c0%1 \c5Defused \c0%2s \c5Bomb!', %obj.client.name, %col.riggerID.name);
					cancel(%col.riggerID.bombschedule);
					%col.bombID = 0;
					%col.riggerID = 0;
					%col.NoDestroy = 0;
					%col.NoBreak = 0;
					%col.dead = true;
					%col.schedule(10, explode);
				}
			}
			else if(%col.riggerID.plantedbomb $= 0)
			{
				if(%col.riggerID $= %obj.client)
				{
					messageClient(%obj.client,"","\c4Deactivated the Plunger.");
					%col.dead = true;
					%col.schedule(10, explode);
					%obj.client.plantedplunger = 0;
				}
				else
				{
					messageClient(%obj.client,"","\c4You cannot Defuse this Bomb until it is Properly Rigged!");
				}
			}
		}
	}
}