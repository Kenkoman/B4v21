//wand.cs
//this is an admin only tool.  its just like the wand, except you can kill blocks from the bottom of a pile
$AdminWandMessage = "Admin was Here";
$WandMode = 0;

//effects
datablock ParticleData(wandExplosionParticle)
{
   dragCoefficient      = 5;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 500;
   lifetimeVarianceMS   = 300;
   textureName          = "~/data/particles/snowflake";
   colors[0]     = "0.0 0.7 0.9 0.9";
   colors[1]     = "0.0 0.0 0.9 0.0";
   sizes[0]      = 0.7;
   sizes[1]      = 0.2;
};

datablock ParticleEmitterData(wandExplosionEmitter)
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   ejectionVelocity = 5;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 60;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "wandExplosionParticle";
};

datablock ExplosionData(wandExplosion)
{
   //explosionShape = "";
   lifeTimeMS = 500;

   particleEmitter = wandExplosionEmitter;
   particleDensity = 10;
   particleRadius = 0.2;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = true;
   camShakeFreq = "2.0 2.0 2.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 0.5;
   camShakeRadius = 10.0;

   // Dynamic light
   lightStartRadius = 3;
   lightEndRadius = 1;
   lightStartColor = "00.0 0.6 0.9";
   lightEndColor = "0 0 0";
};


//projectile
datablock ProjectileData(wandProjectile)
{
   //projectileShapeName = "~/data/shapes/arrow.dts";
   directDamage        = 10;
   radiusDamage        = 10;
   damageRadius        = 0.5;
   explosion           = wandExplosion;
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
datablock ItemData(wand)
{
	category = "Tools";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/wand2.dts";
	skinName = 'rainbow';
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a wand';
	invName = 'wand';
	image = wandImage;
};

//function wand::onUse(%this,%user)
//{
//	//mount the image in the right hand slot
//	%user.mountimage(%this.image, $RightHandSlot);
//}

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(wandImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/wand2.dts";
   skinName = 'rainbow';
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
   item = wand;
   ammo = " ";
   projectile = wandProjectile;
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
	stateScript[5]                 = "onStopFire";
};

function wandImage::onPreFire(%this, %obj, %slot)
{
	//messageAll( 'MsgClient', 'wand prefired!!!');
	//Parent::onFire(%this, %obj, %slot);
	%obj.playthread(2, armattack);
}

function wandImage::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
	//messageAll( 'MsgClient', 'stopfire');
}

function wandProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	if(%obj.client.isAdmin || %obj.client.isSuperAdmin || %obj.client.isTempAdmin)
	{ 
		%player = %obj.client.player;

		//if theres no player, (or client) bail out now
		if(!%player)
			return;

		if (%col.getClassName() !$= "StaticShape")
			return;

		%colData = %col.getDataBlock();
		%colDataClass = %colData.classname;
		
		if(%colDataClass $= "brick")
		{
			if(%obj.client.WandMode == 6)
			{
				killBrick(%col);
			}
			if(%obj.client.WandMode == 7)
			{
				if(%col.teleportObj == 0)
				{
				if(%obj.client.teleHits == 0)	
				{
					$Pref::Game::TotalTelePortLinks++;
					%obj.client.teleHits++;
					%col.teleportObj = $Pref::Game::TotalTelePortLinks;
					messageClient(%obj.client,"","\c4Set first teleport link");
				}
				else
				{
					%obj.client.teleHits = 0;
					%col.teleportObj = $Pref::Game::TotalTelePortLinks;
					messageClient(%obj.client,"","\c4Set second teleport link");
					//schedule(500,0,"Telepulse",%col);
				}
				}
				else
				{
					%col.teleportObj = 0;
					messageClient(%obj.client,"","\c4Removed teleport link");
				}
			}
			if(%obj.client.WandMode == 8)
			{
				if(%col.teleportObjByID == 0)
				{
					%obj.client.isMakingLinkByID = 1;
					%obj.client.RequestedLink = %col;
					messageClient(%obj.client,"","\c4Enter a requested ID number.");
					commandtoclient(%obj.client,'OpenPWBox');
				}
				else
				{
					%col.teleportObj = 0;
					messageClient(%obj.client,"","\c4Removed teleport by ID");
				}
			}
			if(%obj.client.WandMode == 9)
			{
				if(%col.teleportObjGateway == 0)
				{
					%col.isteleportObjGateway = 1;
					messageClient(%obj.client,"","\c4Created a teleport by ID Gateway.");
				}
				else
				{
					%col.teleportObjGateway = 0;
					messageClient(%obj.client,"","\c4Removed a teleport by ID Gateway.");
				}
			}
			if(%obj.client.WandMode == 10)
			{
				if(%col.NoDestroy == 0)
				{
					%col.NoDestroy = 1;
					messageClient(%obj.client,"","\c4Made brick safe from deathmatch weapons");
				}
				else
				{
					%col.NoDestroy = 0;
					messageClient(%obj.client,"","\c4Brick is no longer safe from deathmatch weapons");
				}

			}
		}
		if(%obj.client.WandMode == 0)
		{
			if(%col.iswcloaked != 0 && %col.iswcloaked != 1)
			{
				%col.setcloaked(1);
				%col.iswcloaked = 1;
			}
			else if(%col.iswcloaked == 0)
			{
				%col.setcloaked(1);
				%col.iswcloaked = 1;
			}
			else if(%col.iswcloaked == 1)
			{
				%col.setcloaked(0);
				%col.iswcloaked = 0;
			}
		}
		if(%obj.client.WandMode == 1)
		{
			if(%col.isDetBrick $= 1)
			{
				%col.isDetBrick = 0;
				messageClient(%obj.client,"","\c4Removed Jail Spawn.");
			}
			else
			{
			%col.isDetBrick = 1;
			messageClient(%obj.client,"","\c4Assigned Jail Spawn.");
			}
		}


		if(%obj.client.WandMode == 2)
		{
			%owner = %col.ownername;
			%ownerip = %col.ownerip;
			commandtoclient(%obj.client,'openBP',%owner,%ownerip);
			}
		}

		if(%obj.client.WandMode == 3)
		{
			if(%col.iskiller $= 1)
			{
				%col.iskiller = 0;
				messageClient(%obj.client,"","\c4KillerBrick OFF");
			}
			else
			{
				%col.iskiller = 1;
				messageClient(%obj.client,"","\c3KillerBrick ON");
			}
		}

		if(%obj.client.WandMode == 4)
		{
			if(%col.isscale $= 1)
			{
				%col.isscale = 0;
				messageClient(%obj.client,"","\c4ScaleBrick OFF");
			}
			else
			{
				%col.isscale = 1;
				messageClient(%obj.client,"","\c5ScaleBrick ON");
			}
		}

		if(%obj.client.WandMode == 5)
		{
			if(%col.ispaint $= 1)
			{
				%col.ispaint = 0;
				messageClient(%obj.client,"","\c4PaintBrick OFF");
			}
			else
			{
				%col.ispaint = 1;
				messageClient(%obj.client,"","\c5PaintBrick ON");
			}
	}
}

function serverCmdCheckDoorPassword(%client,%password)
{
	%col = %client.PWDoor;
	if(%client.isMakingLinkByID){
		%client.RequestedLink.LinkedByID = 1;
		%client.RequestedLink.LinkNum = %password;
		%client.isMakingLinkByID = 0;
		centerprint(%client,"ID set to " @ %password @ ".",5,1);
		return;
	}
	if(%client.isRequestingTeletoID){
		for(%t = 0; %t < MissionCleanup.getCount(); %t++){
		%teleObj = MissionCleanup.getObject(%t);
		if(%teleObj.LinkedByID == 1 && %teleObj.LinkNum $= %password){
			Schedule(200,0,"ServerCmdTeleportToObj",%client,%teleObj,500);
			%client.isRequestingTeletoID = 0;
			centerprint(%client,"Teleporting to ID " @ %password @ "...",5,1);
			return;
		}}
		messageClient(%client,"",'\c4Invalid ID!',%password);
		return;
	}
	if(%col.password $= %password || (%client.isSuperAdmin && $Pref::Server::CopsAndRobbers !$= 1))
	{
		centerprint(%client,"Password Accepted!",5,1);
		executemoverinstructions(%client, %col);
	}
	else
	{
		centerprint(%client,"Password Rejected!",5,1);
	}
}

function chainColourBrick(%brick, %checkVal, %iteration, %colour)
{
	//make sure there is a brick object
	if(!isObject(%brick))
	{
		//brick tree is broken somewhere
		return;
	}

	//dont recheck bricks and dont paint the baseplate
	if(%brick.checkVal == %checkVal || %brick.getDataBlock().className !$= "brick")
	{
		return;
	}

	//mark this brick as checked
	%brick.checkVal = %checkVal;

	for(%i = 0; %i < %brick.upSize; %i++)
	{
		chainColourBrick(%brick.up[%i], %checkVal, %iteration++, %colour);
	}
	for(%i = 0; %i < %brick.downSize; %i++)
	{
		chainColourBrick(%brick.down[%i], %checkVal,  %iteration++, %colour);
	}
	%brick.setSkinName(%colour);
}

function colourBrick(%brick,%colour)
{
	//remove references to this brick from its immediate children's up/down lists
	for(%i = 0; %i < %brick.upSize; %i++)
	{
		%child = %brick.up[%i];
	}
	for(%i = 0; %i < %brick.downSize; %i++)
	{
		%child = %brick.down[%i];
	}
	
	//if its children have no path to the ground, chain kill em
	$currCheckVal++;
	%groundCheckVal = $currCheckVal;
	for(%i = 0; %i < %brick.upSize; %i++)
	{
		%child = %brick.up[%i];
		chainColourBrick(%child, $currCheckVal++, 0, %colour);
	}
	for(%i = 0; %i < %brick.downSize; %i++)
	{
		%child = %brick.down[%i];
		chainColourBrick(%child, $currCheckVal++, 0, %colour);
	}

	%brick.setskinname(%colour);
}

function serverCmdBanPlayerIP(%client, %victimip, %victimname)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		for(%t = 0; %t<ClientGroup.getCount(); %t++)
		{
			%cl = ClientGroup.getObject(%t);
			if(%cl.name $= %victimname)
			{
				%victim = %cl;
			}
		}

		//add player to ban list
		if (!%victim.isAIControlled())
		{

			//this isnt a bot so add their ip to the banlist
			%ip = %victimip;
			if(%ip !$= "local")
			{
				messageAll( 'MsgAdminForce', '\c0%3\c3 has banned \c0%1\c3(\c0%2\c3) for Brickspamming.', %victimname, %victimip, %client.name);

				$Ban::numBans++;
				$Ban::ip[$Ban::numBans] = %ip;
				$Ban::ipsubnet[$Ban::numBans] = getIPMask(%ip);
				$Ban::name[$Ban::numBans] = %victimname;
			}
			else
			{
				messageClient(%client, 'MsgAdminForce', '\c4You can\'t ban the Server Host!');
				return;
			}
		}

		if(%victim)
		{
		%victim.delete("You have been banned.");
		}
	}
}

function serverCmdDelPlayerBricks(%client,%victim)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
	  	for(%t = 0; %t < MissionCleanup.getCount(); %t++)
		{
			%brick = MissionCleanup.getObject(%t);

			if(%brick.ownername $= %victim && %victim !$= "")
			{
				%brick.schedule(10, explode);				
				if(%brick.Datablock $= "staticbrickFire")
				{
					%brick.Owner.firebrickcount--;
					%brick.flameEmitter.delete();
					%brick.smokeEmitter.delete();
				}
			}
		}
		messageAll('adminmsg', '\c0%1s\c3 Bricks have been cleared by the Admin(\c0%2\c3)', %victim, %client.name);
	}
}


