//wrench.cs
$TotalMoversInGroup = 5;
//effects
datablock ParticleData(wrenchExplosionParticle)
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

datablock ParticleEmitterData(wrenchExplosionEmitter)
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
   particles = "wrenchExplosionParticle";
};

datablock ExplosionData(wrenchExplosion)
{
   //explosionShape = "";
   lifeTimeMS = 500;

   particleEmitter = wrenchExplosionEmitter;
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
datablock ProjectileData(wrenchProjectile)
{
   //projectileShapeName = "~/data/shapes/arrow.dts";
   directDamage        = 10;
   radiusDamage        = 10;
   damageRadius        = 0.5;
   explosion           = wrenchExplosion;
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

};


//////////
// item //
//////////
datablock ItemData(wrench)
{
	category = "Tools";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/wrench.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
        skinName = 'red';
	cost = "30";
	 // Dynamic properties defined by the scripts
	pickUpName = 'a wrench';
	invName = 'Wrench';
	image = wrenchImage;
};

//function wrench::onUse(%this,%user)
//{
//	//mount the image in the right hand slot
//	%user.mountimage(%this.image, $RightHandSlot);
//}

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(wrenchImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/wrench.dts";
   PreviewFileName = "rtb/data/shapes/bricks/Previews/wrench.png";
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
   item = wrench;
   ammo = " ";
   projectile = wrenchProjectile;
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

function wrenchImage::onPreFire(%this, %obj, %slot)
{
	//messageAll( 'MsgClient', 'wrench prefired!!!');
	//Parent::onFire(%this, %obj, %slot);
	%obj.playthread(2, armattack);
}

function wrenchImage::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
	//messageAll( 'MsgClient', 'stopfire');
}

function removeFromMoverList(%obj)
{
	for(%t = 0; %t < $TotalMoversPlaced + 1; %t++)
	{
		if(%obj == $Movers[%t])
		{
			$Movers[%t] = 0;
		}
	} 
}

function wrenchProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
		%client = %obj.client;
		if(%obj.client.isImprisoned)
		{
		messageClient(%client,'',"\c4Trying to wrench are you?!?! Imprisoned scum...");
		return;
		}
		%player = %obj.client.player;

		//if theres no player, (or client) bail out now
		if(!%player)
			return;
		
		if($Pref::BBC::JudgingTime $= 1)
		{
			if(%col.BBCOwner !$= "")
			{
				if(%client.Rated[%col] $= 1)
				{
					messageClient(%obj.client,"","\c4You have already Rated this Build!");
					return;
				}
				else
				{
					commandtoclient(%obj.client,'Push',BBCRate);
					return;
				}
			}
			else
			{
				messageClient(%obj.client,"","\c4This is not somebody's Build! Please Hit their Baseplate!");
				return;
			}
		}

		if($Pref::BBC::RiggingProcess $= 1)
		{
		if(%client.BBCPlate !$= "")
		{
		messageClient(%client,"","\c4You have already chosen a Plate!");
		return;
		}

			for (%j = 0; %j < MissionCleanup.getCount(); %j++)
			{
				%brick = MissionCleanup.getObject(%j);
				if(%brick.isBBCReserved $= 1 && %brick.isBBCReservedN $= %client.namebase)
				{
					%hasReserved = 1;
				}
			}
		if(%hasReserved $= 1)
		{

			if (%col.isBBCPublic && %client.BBCPlate $= "" && %col.BBCOwner $= "")
			{
				messageClient(%client,"","\c4Please find the Plate you have had Reserved for you!");
				return;
			}
			else
			{
				if(%col.isBBCReserved && %col.isBBCReservedN $= %client.namebase)
				{
					messageClient(%client,"","\c5This is now your Plate!");
					%col.BBCOwner = %client;
					%client.BBCPlate = %col;
					addBBCMarker(%col);
					return;
				}
			}
		
		}
		else
		{
			if (%col.isBBCPublic && %client.BBCPlate $= "")
			{
				if(%col.BBCOwner $= "")
				{
					messageClient(%client,"","\c5This Plate is now Yours!");
					%col.BBCOwner = %client;
					%client.BBCPlate = %col;
					addBBCMarker(%col);
					return;
				}
				else
				{
					messageClient(%client,"","\c4This Plate is already Taken!");
					return;
				}
			}
			else if(%col.isBBCReserved $= 1)
			{
				messageClient(%client,"","\c4This Plate has been Reserved!");
				return;
			}
		}
		}	


		if (%col.getClassName() !$= "StaticShape" && %col.getClassName() !$= "Player")
			return;

		%colData = %col.getDataBlock();
		%colDataClass = %colData.classname;

		if(%colDataClass $= "brick")
		{
			
			if(%obj.client.WrenchMode == 0 && (%col.FXMode $= "" || %col.FXMode $= 0))
			{	
				%isTrusted = checkSafe(%col,%obj.client);		
				if(%col.Owner == %obj.client || %isTrusted ||%obj.client.isAdmin || %obj.client.isSuperAdmin)
				{
					%obj.client.WrenchObject = %col;
					commandtoclient(%client,'OpenMoverGui');
				}				
			}
			//Wrench in Ezy-Mover-Mode
			if(%obj.client.WrenchMode == 1 && (%col.FXMode $= "" || %col.FXMode $= 0))
			{
				%isTrusted = checkSafe(%col,%obj.client);		
				if(%col.Owner == %obj.client || %isTrusted ||%obj.client.isAdmin || %obj.client.isSuperAdmin)
				{
					if(%obj.player.tempBrick.isMoverGhost $= 1)
					{
					   messageClient(%client,'',"You already have a Mover Ghost Out!");
					   return;
					}

					%obj.client.WrenchObject = %col;
					if(%col.isDoor !$= 1)
					{
					//New Stuff
					%player = %obj.client.player;
					%NmoverBrick = new StaticShape()
					{
						datablock = %col.getDataBlock();
					};
					MissionCleanup.add(%NmoverBrick);
					
					%NmoverBrick.MStartPos = %col.getTransform();
					%NmoverBrick.EulerRot = %col.EulerRot;
					%NmoverBrick.setTransform(%col.getTransform());
					%NmoverBrick.setScale(%col.getScale());
					%NmoverBrick.setSkinName('construction');
					%NmoverBrick.isBrickGhost = 1;
					%NmoverBrick.isMoverGhost = 1;

					if(%player.tempBrick.isBrickGhostMoving $= 1)
					{
						%player.tempBrick.setSkinName(%player.tempBrick.oldskinname);
						%player.tempBrick.isBrickGhost = "";
						%player.tempBrick.isBrickGhostMoving = "";
						%player.tempBrick = "";
					}
					else
					{
						%player.tempBrick.delete();
						%player.tempBrick = "";
					}

					%player.tempBrick = %NmoverBrick;	
					}
					else
					{
						messageClient(%client,"","\c4This Brick already has Movement Properties, please Remove the Movement Properties to use Ezy-Mover-Mode!");
						commandtoclient(%client,'OpenMoverGui');
					
					}
					
				}	
			}
			//Wrench in Repair-Mode
			if(%obj.client.WrenchMode == 2)
			{
				if(%col.Hits > 0)
				{
					%col.Hits = 0;
					%col.setSkinName(%col.SkinName);
				}
			}
			//Wrench in Brick-Message-Mode
			if(%obj.client.WrenchMode == 3)
			{
				%isSafe = checkSafe(%col,%obj.client);
				if(%col.Owner $= %obj.client || %obj.client.isAdmin || %obj.client.isSuperAdmin || %isSafe)
				{
					%col.setShapeName(%obj.client.BrickMessage);
				}
			}
			//Wrench in Decal-Mode
			if(%obj.client.WrenchMode == 4 && (%col.FXMode $= "" || %col.FXMode $= 0))
			{
				%isTrusted = checkSafe(%col,%obj.client);		
				if(%col.Owner == %obj.client || %isTrusted ||%obj.client.isAdmin || %obj.client.isSuperAdmin)
				{
					%obj.client.selectedbrick = %col;
					%hole = %col.getdatablock().getname();
			   		commandtoclient(%client,'Openbrickprintselect',%hole);
				}
			}
			//Wrench in Impulse-Mode
			if(%obj.client.WrenchMode == 5 && (%col.FXMode $= "" || %col.FXMode $= 0))
			{
				%isSafe = checkSafe(%col,%obj.client);
				if(%col.Owner $= %obj.client || %obj.client.isAdmin || %obj.client.isSuperAdmin || %isSafe)
				{
			   %obj.client.WrenchObject = %col;
			   commandtoclient(%client,'push',impulseGUI);
				}
			}
			//Wrench in Doorbell-Mode
			if(%obj.client.WrenchMode == 6 && (%col.FXMode $= "" || %col.FXMode $= 0))
			{
				%isSafe = checkSafe(%col,%obj.client);
				if(%col.Owner $= %obj.client || %obj.client.isAdmin || %obj.client.isSuperAdmin || %isSafe)
					if(%col.isDoorbell $= 1)
					{
						%col.isDoorbell = 0;
						messageClient(%client,'',"\c4You have removed the Doorbell.");
					}
					else
					{
						%col.isDoorbell = 1;
						messageClient(%client,'',"\c4You have added a Doorbell.");
					}
			}
		}
		if(%col.getClassName() $= "Player")
		{
			if(%obj.client.WrenchMode == 3)
			{
				%col.applyRepair(10);
			}
		}
}

function chainColourOwnBrick(%brick, %checkVal, %iteration, %colour, %client)
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
		chainColourOwnBrick(%brick.up[%i], %checkVal, %iteration++, %colour,%client);
	}
	for(%i = 0; %i < %brick.downSize; %i++)
	{
		chainColourOwnBrick(%brick.down[%i], %checkVal,  %iteration++, %colour,%client);
	}
	if(%brick.Owner == %client)
	{
		%brick.setSkinName(%colour);
	}
}

function colourOwnBrick(%brick,%colour,%client)
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
		chainColourOwnBrick(%child, $currCheckVal++, 0, %colour,%client);
	}
	for(%i = 0; %i < %brick.downSize; %i++)
	{
		%child = %brick.down[%i];
		chainColourOwnBrick(%child, $currCheckVal++, 0, %colour,%client);
	}

	if(%brick.Owner == %client)
	{
		%brick.setskinname(%colour);
	}
}



function killOwnBrick(%brick,%client)
{
	//remove references to this brick from its immediate children's up/down lists
	for(%i = 0; %i < %brick.upSize; %i++)
	{
		%child = %brick.up[%i];
		removeFromDownList(%brick, %child);
	}
	for(%i = 0; %i < %brick.downSize; %i++)
	{
		%child = %brick.down[%i];
		removeFromUpList(%brick, %child);
	}
	
	//if its children have no path to the ground, chain kill em
	$currCheckVal++;
	%groundCheckVal = $currCheckVal;
	for(%i = 0; %i < %brick.upSize; %i++)
	{
		%child = %brick.up[%i];
		if(!hasPathToGround(%child, %groundCheckVal))
		{
			chainKillOwnBrick(%child, $currCheckVal++, 0,%client);
		}
	}
	for(%i = 0; %i < %brick.downSize; %i++)
	{
		%child = %brick.down[%i];
		if(!hasPathToGround(%child, %groundCheckVal))
		{
			chainKillOwnBrick(%child, $currCheckVal++, 0,%client);
		}
	}

	if(%brick.Owner == %client)
	{
		//tag this brick as dead
		%brick.dead = true;

		if(%brick.isDoor)
		{
			%brick.owner.TotalMovers--;
			removeFromMoverList(%brick);
		}

		//explode this brick
		%brick.schedule(10, explode);
		
	}
}

//kills brick and all its children
function chainKillOwnBrick(%brick, %checkVal, %iteration,%client)
{
	//make sure there is a brick object
	if(!isObject(%brick))
	{
		//brick tree is broken somewhere
		return;
	}

	//dont recheck bricks and dont kill the baseplate
	if(%brick.checkVal == %checkVal || %brick.getDataBlock().className !$= "brick")
	{
		return;
	}

	//mark this brick as checked
	%brick.checkVal = %checkVal;

	for(%i = 0; %i < %brick.upSize; %i++)
	{
		chainKillOwnBrick(%brick.up[%i], %checkVal, %iteration++,%client);
	}
	for(%i = 0; %i < %brick.downSize; %i++)
	{
		chainKillOwnBrick(%brick.down[%i], %checkVal,  %iteration++,%client);
	}
	if(%brick.Owner == %client)
	{
		//tag this brick as dead
		%brick.dead = true;

		if(%brick.isDoor)
		{
			%brick.owner.TotalMovers--;
			removeFromMoverList(%brick);
		}

		if(%iteration <= 1)
			%brick.schedule((%iteration * 25) + 0, explode);
		else
			%brick.schedule((%iteration * 25) + 50, explode);
	}
}

function serverCmdsetBricktoNorm(%client)
{
	%col = %client.WrenchObject;

	%col.isDoor = "";
	%col.MoveXYZ = "";
	%col.Steps = "";
	%col.StepTime = "";
	%col.RotateXYZ = "";
	%col.ReturnDelay = "";
	%col.ReturnToggle = "";
	%col.Elevator = "";
	%col.Private = "";
	%col.noCollision = "";
	%col.isMoving = "";
	%col.TriggerDoorID = "";
	%col.HasReturned = "";
	%col.Password = "";
	%col.Team = "";
	%col.Group = "";
	%col.DoorType = "";
	%col.TriggerCloak = "";
	%col.Elevator = "";
	%col.isImpulseTrigger = "";
	messageClient(%client,"","\c4You Removed the Brick Properties!");
}

function setBricktoMover(%client)
{
	%col = %client.WrenchObject;
								
	if(%client.TotalMovers < $Pref::Server::MaxDoors)
	{
		if(%col.isDoor != 1)
		{
			servercmdsetBricktoNorm(%client);
			messageClient(%client,"","\c4Gave Brick Movement Properties");
		}
		else
		{	
			servercmdsetBricktoNorm(%client);
			messageClient(%client,"","\c4You Replaced the Brick Properties!");
		}

		if(!%client.isAdmin || !%client.isSuperAdmin)
		{
			%XYZ = %client.WrenchMoveXYZ;
			%X = getWord(%XYZ, 0);
			%Y = getWord(%XYZ, 1);
			%Z = getWord(%XYZ, 2);
			%client.stepTime = mFloor(%client.stepTime);
			if(%client.StepTime >= 1000 || %client.StepTime < 0)
			{
				messageClient(%client,"","\c4You cannot have more than 1000 Steps, or less than 0.");
				return;				
			}
			if(%client.WrenchSteps >= 1000 || %client.WrenchSteps < 0)
			{
				messageClient(%client,"","\c4You cannot have more than 1000 Steps, or less than 0.");
				return;				
			}
			
			if(%X >= 500 || %Y >= 500 || %Z >= 500 || %X <= -500 || %Y <= -500 || %Z <= -500)
			{
				messageClient(%client,"","\c4You cannot have any Move Value as more than 500.");
				return;
			}
		}

		%col.isDoor = 1;
		%col.MoveXYZ = %client.WrenchMoveXYZ;
		%col.RotateXYZ = %client.WrenchRotateXYZ;
		%col.Steps = %client.WrenchSteps;
		%col.StepTime = %client.WrenchStepTime;
		%col.ReturnDelay = %client.WrenchReturnDelay;
		%col.Elevator = %client.WrenchElevator;
		%col.noCollision = %client.WrenchNoCollision;
		%col.HasReturned = 1;

		if(%client.WrenchDoorType $= "Trigger")
		{
			%col.DoorType = 1;
			%col.TriggerDoorID = %client.WrenchDoorSetID;
		}
		else if(%client.WrenchDoorType $= "Triggered")
		{
			%col.DoorType = 2;
			%col.TriggerDoorID = %client.WrenchDoorSetID;
		}
		else if(%client.WrenchDoorType $= "Normal")
		{
			%col.DoorType = 3;
		}
	
		if(%client.WrenchReturnToggle $= 1)
		{
			%col.ReturnToggle = 1;
		}
		else
		{
			%col.ReturnToggle = 0;
		}

		if(%client.WrenchImpTrigger $= 1)
		{
			%col.TriggerDoorID = %client.WrenchDoorSetID;
			%col.isImpulseTrigger = 1;
		}
		
		if(%client.WrenchTriggerCloak)
		{
			%col.TriggerCloak = %client.WrenchTriggerCloak;
		}
		
		if(%client.WrenchPrivate !$= "")
		{
			%col.Private = %client.WrenchPrivate;
		}
		
		if(%client.WrenchPassword !$= "")
		{
			%col.Password = %client.WrenchPassword;
		}

		if(%client.WrenchTeam !$= "")
		{
			%col.Team = %client.WrenchTeam;
		}

		if(%client.WrenchGroup !$= "")
		{
			%col.Group = %client.WrenchGroup;
		}
	 
		$Movers[$TotalMoversPlaced++] = %col;
		%client.TotalMovers++;
		commandtoclient(%client,'CloseMoverGUI');
	}
	else
	{
		if($Pref::Server::MaxDoors $= 0)
		{
			commandtoclient(%client,'CloseMoverGUI');
			messageClient(%client,"","\c4Movers are not allowed on this Server!");
		}
		else
		{
			commandtoclient(%client,'CloseMoverGUI');
			messageClient(%client,"",'\c4There is a limit of \c0%1',$Pref::Server::MaxDoors);
		}
	}
}


function serverCmdapplyImpulse(%client, %X, %Y, %Z, %Trigger, %TriggerID)
{
	%col = %client.WrenchObject;
	%col.isImp = 1;
	if(%Trigger $= 1)
	{
	%col.isTriggerImp = 1;
	%col.TriggerDoorID = %TriggerID;
	}
	%col.Imp = %X * 100 SPC %Y * 100 SPC %Z * 100;
	messageClient(%client,"","\c4You have given the Brick Impulse Properties.");
}


function serverCmdgetWrenchSettings(%client)
{
	%col = %client.WrenchObject;

	if(%col.isDoor $= 1)
	   %yesno = 1;
	else
	   %yesno = 0;

	messageClient(%client,'MsgUpdateWrenchA',"",%col.MoveXYZ,%col.RotateXYZ,%col.Steps,%col.StepTime,%col.Elevator,%col.TriggerCloak,%col.Private,%col.Team,%col.Group,%yesno);
	messageClient(%client,'MsgUpdateWrenchB',"",%col.isImpulseTrigger,%col.returnDelay,%col.returnToggle,%col.password,%col.DoorType,%col.TriggerDoorID,%col.NoCollision,%yesno);
}

function serverCmdgetImpulseSettings(%client)
{
	%col = %client.WrenchObject;
	messageClient(%client,'MsgUpdateWrenchI',"",%col);
}

function serverCmdRateBuild(%client,%rating)
{
	%Build = %client.WrenchObject;
	if(%client.Rated[%Build] $= 1)
	{
	messageClient(%client,"","\c4You have already rated this Build!");
	return;
	}
	else
	{
	%Build.Rating = %Build.Rating + %Rating;
	%client.Rated[%Build] = 1;
	messageClient(%client,"",'\c5Your Rating of <\c0%1\c5> has been added to the Build Score!', %rating);
	commandtoclient(%client,'Pop',BBCRate);
	}
}

function addBBCMarker(%col)
{
	$BBCMarker = new StaticShape()
	{
		datablock = staticBrick1x1;
	};
	MissionCleanup.add($BBCMarker);
	%Pos = %col.getWorldBoxCenter();
	%X = getWord(%Pos, 0);
	%Y = getWord(%Pos, 1);
	%Z = getWord(%Pos, 2);
	%Z = %Z + 4;
	$BBCMarker.noBreak = 1;
	$BBCMarker.setTransform(%X SPC %Y SPC %Z);
	$BBCMarker.setSkinName(construction);
	$BBCMarker.setShapeName(%col.BBCOwner.namebase@"\'s Plate");
	%col.BBCMarker = $BBCMarker;
}


function serverCmdToggleWrenchMode(%client)
{
	%client.WrenchMode++;
	if(%client.WrenchMode > 6)
	{
		%client.WrenchMode = 0;
	}
	if(%client.WrenchMode == 0)
	{
		messageClient(%client,"","\c2Wrench in Mover-Mode");
	}
	if(%client.WrenchMode == 1)
	{
		messageClient(%client,"","\c2Wrench in Ezy-Mover-Mode");
	}
	if(%client.WrenchMode == 2)
	{
		messageClient(%client,"","\c2Wrench in Repair-Mode");
	}
	if(%client.WrenchMode == 3)
	{
		messageClient(%client,"","\c2Wrench in Brick-Message-Mode");
	}
	if(%client.WrenchMode == 4)
	{
		messageClient(%client,"","\c2Wrench in Brick-Decal-Mode");
	}
	if(%client.WrenchMode == 5)
	{
		messageClient(%client,"","\c2Wrench in Impulse-Mode");
	}
	if(%client.WrenchMode == 6)
	{
		messageClient(%client,"","\c2Wrench in Doorbell-Mode");
	}
}