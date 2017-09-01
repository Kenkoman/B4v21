//Lightsabre.cs
datablock AudioProfile(lightsabreDrawSound)
{
   filename    = "~/data/sound/lightsabreDraw.wav";
   description = AudioClosest3d;
   preload = true;
};
datablock AudioProfile(lightsabreHitSound)
{
   filename    = "~/data/sound/lightsabreHit.wav";
   description = AudioClosest3d;
   preload = true;
};


//effects
datablock ParticleData(lightsabreExplosionParticle)
{
   dragCoefficient      = 5;
   gravityCoefficient   = 0.2;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 500;
   lifetimeVarianceMS   = 300;
   textureName          = "~/data/particles/chunk";
   colors[0]     = "0.7 0.7 0.9 0.9";
   colors[1]     = "0.9 0.9 0.9 0.0";
   sizes[0]      = 0.5;
   sizes[1]      = 1.0;
};

datablock ParticleEmitterData(lightsabreExplosionEmitter)
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   ejectionVelocity = 8;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 60;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "lightsabreExplosionParticle";
};

datablock ExplosionData(lightsabreExplosion)
{
   //explosionShape = "";
   lifeTimeMS = 500;

   soundProfile = lightsabreHitSound;

   particleEmitter = lightsabreExplosionEmitter;
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
   lightStartRadius = 5;
   lightEndRadius = 0;
   lightStartColor = "00.0 0.8 0.0";
   lightEndColor = "0 0 0";
};


//projectile
datablock ProjectileData(lightsabreProjectile)
{
   //projectileShapeName = "~/data/shapes/arrow.dts";
   directDamage        = 70;
   radiusDamage        = 0;
   damageRadius        = 0.5;
   explosion           = lightsabreExplosion;
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

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";
};


//////////
// item //
//////////
datablock ItemData(lightsabre)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/ls.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	cost = 400;

	 // Dynamic properties defined by the scripts
	pickUpName = 'Lightsabre';
	invName = 'lightsabre';
	image = lightsabreImage;
};

//function lightsabre::onUse(%this,%user)
//{
//	//if the image is mounted already, unmount it
//	//if it isnt, mount it
//
//	%mountPoint = %this.image.mountPoint;
//	%mountedImage = %user.getMountedImage(%mountPoint); 
//	
//	if(%mountedImage)
//	{
//		//echo(%mountedImage);
//		if(%mountedImage == %this.image.getId())
//		{
//			//our image is already mounted so unmount it
//			%user.unMountImage(%mountPoint);
//		}
//		else
//		{
//			//something else is there so mount our image
//			%user.mountimage(%this.image, %mountPoint);
//		}
//	}
//	else
//	{
//		//nothing there so mount 
//		%user.mountimage(%this.image, %mountPoint);
//	}
//}

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(lightsabreImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/ls.dts";
   emap = true;
   PreviewFileName = "rtb/data/shapes/bricks/Previews/lightsabre.png";
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
   item = lightsabre;
   ammo = " ";
   projectile = lightsabreProjectile;
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
	stateSound[0]					= lightsabreDrawSound;

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
	stateTimeoutValue[3]            = 0.2;
	stateFire[3]                    = true;
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	stateScript[3]                  = "onFire";
	stateWaitForTimeout[3]		= true;
	//stateTransitionOnTriggerUp[3]	= "StopFire";

	stateName[4]			= "CheckFire";
	stateTransitionOnTriggerUp[4]	= "StopFire";
	stateTransitionOnTriggerDown[4]	= "Fire";

	
	stateName[5]                    = "StopFire";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 0.2;
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;
	stateSequence[5]                = "StopFire";
	stateScript[5]                  = "onStopFire";


};

function lightsabreImage::onPreFire(%this, %obj, %slot)
{
	//messageAll( 'MsgClient', 'lightsabre prefired!!!');
	//Parent::onFire(%this, %obj, %slot);
	%obj.playthread(2, armattack);
}

function lightsabreImage::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
	//messageAll( 'MsgClient', 'stopfire');
}

function lightsabreProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)  
{ 
	if($Pref::Server::CopsAndRobbers)
	{
		if(%obj.client.team $= "Cops" && %col.client.team !$="Cops" && %col.getDamageLevel() >= 50)
		{
			ImprisonPlayer(%obj.client,%col.client);
			%col.damage(%obj,%pos,5,"lightsabre");
		}
	}
	else
	{ 
			weaponDamage(%obj,%col,%this,%pos,lightsabre);
			radiusDamage(%obj,%pos,%this.damageRadius,%this.radiusDamage,lightsabre,%this.areaImpulse);
	}
}

function ImprisonPlayer(%client,%victim)
{
	if(%client.team $= "Cops" && %victim.team $= "Robbers")
	{
		if(%victim.isImprisoned == 0)
		{
		for(%t = 0; %t < MissionCleanup.GetCount(); %t++)
		{
			%brick = MissionCleanup.getObject(%t);
			if(%brick.isJail == 1)
			{ 
				if(%brick.JailCount < %brick.JailMaxCount)
				{
					if(isobject(%victim.player))
					{
						%brick.JailCount++;
						%victim.isImprisoned = 1;
						%victim.money = 0;
						messageClient(%victim,'MsgUpdateMoney',"",%victim.money);
						%trans = %brick.getWorldBoxCenter();
						%x = getWord(%trans,0);
						%y = getWord(%trans,1);
						%z = getWord(%trans,2)+0.5;
						%victim.player.setTransform(%x SPC %y SPC %z);
						%victim.JailBrick = %brick;
						messageAll("",'\c0%1\c5 was Imprisoned by \c0%2',%victim.name,%client.name);
						%victim.player.unMountImage($HeadSlot);
						%victim.player.mountImage(%victim.chestCode , $decalslot, 1, 'Town-Inmate');
						%victim.player.mountImage(%victim.faceCode , $faceslot, 1, 'smirk2');
						%victim.player.setSkinName('orange');
						%totalImprisonedPeople = %totalImprisonedPeople + %brick.JailCount;
						if(%totalImprisonedPeople >= $TotalRobbers)
						{
							messageAll("","\c5COPS WIN!!!");
							messageAll("","\c5Everyone swaps sides!");
							schedule(5000,0,"RestartCR");
						}
					}
					return;
				}
				else
				{
					%totalImprisonedPeople = %totalImprisonedPeople + %brick.JailMaxCount;
					%JailFull = 1;
				}
			}

		}
		if(%JailFull == 1)
		{
			messageAll("","\c4We've run out of jails");
		}
		else
		{
			messageClient(%client,"","\c4There is no jail built...");
		}
		}
		else
		{
			%victim.isImprisoned = 0;
			%victim.player.setTransform(%victim.TeamSpawn.getTransform());
			%victim.JailBrick.JailCount--;
			messageAll("",'\c0%1\c5 was freed by \c0%2',%victim.name,%client.name);
			%victim.player.mountImage($headCode[$RobHat],$HeadSlot,1,'black');
			%victim.player.mountImage(%client.chestCode , $decalslot, 1, 'Town-Inmate');
			%victim.player.mountImage(%client.faceCode , $faceslot, 1, 'evil');
			%victim.player.setSkinName('black');
		}	
	
	}
}