//editorWand.cs
//this is an admin only tool.  its just like the editorWand, except you can kill blocks from the bottom of a pile
$AdmineditorWandMessage = "Admin was Here";
$editorWandMode = 0;

//effects
datablock ParticleData(editorWandExplosionParticle)
{
   dragCoefficient      = 5;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 500;
   lifetimeVarianceMS   = 300;
   textureName          = "~/data/particles/snowflake";
   colors[0]     = "255 0 0";
   colors[1]     = "255 0 0";
   sizes[0]      = 0.7;
   sizes[1]      = 0.2;
};

datablock ParticleEmitterData(editorWandExplosionEmitter)
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
   particles = "editorWandExplosionParticle";
};

datablock ExplosionData(editorWandExplosion)
{
   //explosionShape = "";
   lifeTimeMS = 500;

   particleEmitter = editorWandExplosionEmitter;
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
   lightStartColor = "255 0 0";
   lightEndColor = "0 0 0";
};


//projectile
datablock ProjectileData(editorWandProjectile)
{
   //projectileShapeName = "~/data/shapes/arrow.dts";
   directDamage        = 10;
   radiusDamage        = 10;
   damageRadius        = 0.5;
   explosion           = editorWandExplosion;
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
datablock ItemData(editorWand)
{
	category = "Tools";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/wand.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	skinName = 'rainbow';
	 // Dynamic properties defined by the scripts
	pickUpName = 'an editorwand';
	invName = 'editorWand';
	image = editorwandImage;
};

//function editorWand::onUse(%this,%user)
//{
//	//mount the image in the right hand slot
//	%user.mountimage(%this.image, $RightHandSlot);
//}

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(editorWandImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/Wand.dts";
   emap = true;
	cloakable = false;
   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
//Scale = "2 2 2";
   //eyeOffset = "0.1 0.2 -0.55";
	skinName = 'rainbow';

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = false;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = editorWand;
   editorWand = 1;
   ammo = " ";
   projectile = editorWandProjectile;
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






function editorWandImage::onPreFire(%this, %obj, %slot)
{
	//messageAll( 'MsgClient', 'editorWand prefired!!!');
	//Parent::onFire(%this, %obj, %slot);
	%obj.playthread(2, armattack);
}









function editorWandImage::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
	//messageAll( 'MsgClient', 'stopfire');
}
















function editorWandProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	if(%obj.client.isAdmin || %obj.client.isSuperAdmin || %obj.client.isEWandUser)
	{ 
		%player = %obj.client.player;

		//if theres no player, (or client) bail out now
		if(!%player)
			return;

		if(%col.dataBlock.classname $= "")
		{
		return;
		}
		
		if(%col.isBrickGhost $= 1)
		{
			return;
		}

		if(%obj.client.isAdmin || %obj.client.isSuperAdmin)
		{
			if(%col.isBrickGhostMoving $= 1 && %player.tempBrick !$= %col)
			{
				%col.BrickGhostOwner.SelectedObject = "";
				messageClient(%col.BrickGhostOwner,'','\c3An Admin (\c0%1\c3) has deselected your Brick!',%obj.client.name);
				messageClient(%client,'','\c3%1\'s Brick has been Deselected!',%col.BrickGhostOwner.name);
				%col.BrickGhostOwner.tempBrick = "";
			}
			if(isObject(%player.tempBrick))
			{
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
			}
			else
			{
					messageClient(%obj.client,'',"\c4You are in \c0Edit \c4Mode.");
			}

			%col.oldskinname = %col.getSkinname();
			%col.setSkinName('construction');
			%col.isBrickGhost = 1;
			%col.isBrickGhostMoving = 1;
			%col.BrickGhostOwner = %obj.client;
			%player.tempBrick = %col;
			%obj.client.SelectedObject = %col;

			if(%obj.client.autoewand $= 1)
			{
				%obj.client.SelectedObject = %col;

				if(%obj.client.aerot !$= "")
				{
				serverCmdEditorRot(%obj.client, %obj.client.aerot);
				}
				if(%obj.client.aescale !$= "")
				{
				serverCmdEditorScale(%obj.client, %obj.client.aescale);
				}

				return;
			}
		}

		if (%col.getClassName() !$= "StaticShape")
		return;

		%colData = %col.getDataBlock();
		%colDataClass = %colData.classname;
		%isTrusted = checkSafe(%col,%obj.client);

		if(%obj.client.isEWandUser && (%colDataClass $= "brick" || %colDataClass $= "baseplate") && (%col.owner $= %obj.client || %obj.client.isAdmin || %obj.client.isSuperAdmin || %obj.client.isTempAdmin || %isTrusted))
		{
			
			%obj.client.SelectedObject = %col;
			%s = %client.SelectedObject;
						if(%col.isBrickGhostMoving $= 1)
			{
				messageClient(%obj.client,"",'\c0%1\c4 has already Selected This!', %col.BrickGhostOwner.namebase);
				return;
			}
			if(isObject(%player.tempBrick))
			{
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
			}
			else
			{
					messageClient(%obj.client,'',"\c4You are in \c0Edit \c4Mode.");
			}

			%col.oldskinname = %col.getSkinname();
			%col.setSkinName('construction');
			%col.isBrickGhost = 1;
			%col.isBrickGhostMoving = 1;
			%col.BrickGhostOwner = %obj.client;
			%obj.client.player.tempBrick = %col;


			if(%obj.client.autoewand $= 1)
			{
				%obj.client.SelectedObject = %col;

				if(%obj.client.aerot !$= "")
				{
				serverCmdEditorRot(%obj.client, %obj.client.aerot);
				}
				if(%obj.client.aescale !$= "")
				{
				serverCmdEditorScale(%obj.client, %obj.client.aescale);
				}

				return;
			}
		}
	}
}























function servercmdEditorDelete(%client)
{
	if(%client.SelectedObject.getClassName() $= "StaticShape")
	{
	if((%client.isAdmin && $Pref::Server::AdminEditor)|| %client.isSuperAdmin)
	{
		if(%client.SelectedObject)
		%client.SelectedObject.delete();
	}
	else
	{
		messageClient(%client,'',"\c4You aren't allowed to delete.");
	}

	}
	else
	{
		messageClient(%client,'',"\c4Sorry! You aren't' allowed to delete people.");
	}

}

function servercmdEditorStoreAuto(%client,%rot,%scale)
{
	%client.aerot = "";
	%client.aescale = "";
	%scalex = getword(%scale,0);
	%scaley = getword(%scale,1);
	%scalez = getword(%scale,2);

	if(%scale && (%scalex $= "" || %scalex $= "0" || %scaley $= "" || %scaley $= "0" || %scalez $= "" || %scalez $= "0"))
	{
					messageClient(%client,'',"\c4You cannot have any Scale value as 0");
					return;
	}
	
	if((%client.isAdmin && $Pref::Server::AdminEditor) || %client.isSuperAdmin || %client.isEWandUser)
	{
		if(%client.SelectedObject)
		if(%rot)
		{
			%client.aerot = %rot;
			%pos = %client.SelectedObject.getPosition();
			%client.SelectedObject.setTransform(%pos@" "@eulerToQuat(%rot));
			%client.selectedObject.eulerrot = %rot;
		}
		if(%scale)
		{
			%client.aescale = %scale;

			if(%client.isEWandUser)
			{
				if(getWord(%scale,0) < 1000 && getWord(%scale,1) < 1000 && getWord(%scale,2) < 1000 && getWord(%scale,0) > -1000 && getWord(%scale,1) > -1000 && getWord(%scale,2) > -1000)
				{
					%client.SelectedObject.setScale(%scale);
					if(%client.SelectedObject.mounteddecal !$= "")
					{
						%client.SelectedObject.mounteddecal.setScale(%scale);
					}
				}
				else
				{
					messageClient(%client,'',"\c4You cannot scale a shape by more than 1000 along any axis");
					return;
				}
			}
			else
			{
				%client.SelectedObject.setScale(%scale);
				if(%client.SelectedObject.mounteddecal !$= "")
				{
					%client.SelectedObject.mounteddecal.setScale(%scale);
				}
			}
		}
		%client.autoewand = 1;
	}
}

function servercmdEditorPos(%client,%pos)
{
	if((%client.isAdmin && $Pref::Server::AdminEditor)|| %client.isSuperAdmin || %client.isEWandUser)
	{
		if(%client.SelectedObject)
			%client.SelectedObject.setTransform(%pos);
	}
}

function servercmdEditorRot(%client,%pos)
{
	if((%client.isAdmin && $Pref::Server::AdminEditor)|| %client.isSuperAdmin || %client.isEWandUser)
	{
		if(%client.SelectedObject)

%client.SelectedObject.setTransform(%client.selectedObject.getPosition()@" "@eulerToQuat(%pos));
				%client.selectedObject.eulerrot = %pos;
	
	}
}

function servercmdEditorScale(%client,%pos)
{

	%scalex = getword(%pos,0);
	%scaley = getword(%pos,1);
	%scalez = getword(%pos,2);

	if(%scalex $= "" || %scalex $= "0" || %scaley $= "" || %scaley $= "0" || %scalez $= "" || %scalez $= "0")
	{
					messageClient(%client,'',"\c4You cannot have any Scale value as 0");
					return;
	}

	if((%scalex < 0.1 && %scalex > -0.1) || (%scaley < 0.1 && %scaley > -0.1) || (%scalez < 0.1 && %scalez > -0.1))
	{
					messageClient(%client,'',"\c4You cannot have any Scale value with magnitude less than 0.1");
					return;
	}
	
	if((%client.isAdmin && $Pref::Server::AdminEditor) || %client.isSuperAdmin || %client.isEWandUser)
	{
		if(%client.SelectedObject)
			if(%client.isEWandUser)
			{
				%scale = %pos;
				if(getWord(%scale,0) < 1000 && getWord(%scale,1) < 1000 && getWord(%scale,2) < 1000 && getWord(%scale,0) > -1000 && getWord(%scale,1) > -1000 && getWord(%scale,2) > -1000)
				{
					%client.SelectedObject.setScale(%pos);
					if(%client.SelectedObject.mounteddecal !$= "")
					{
						%client.SelectedObject.mounteddecal.setScale(%pos);
					}
				}
				else
				{
					messageClient(%client,'',"\c4You cannot scale a shape by more than 1000 along any axis");
				}
			}
			else
			{
				%client.SelectedObject.setScale(%pos);
				if(%client.SelectedObject.mounteddecal !$= "")
				{
					%client.SelectedObject.mounteddecal.setScale(%pos);
				}
			}
	}
}

function servercmdApplyEditorSettings(%client,%pos,%rot,%scale)
{
	if((%client.isAdmin && $Pref::Server::AdminEditor) || %client.isSuperAdmin || %client.isEWandUser)
	{
		if(%client.SelectedObject)
		{
			%client.selectedObject.setTransform(%pos @ " " @ eulerToQuat(%rot));
			%client.selectedObject.eulerrot = %rot;
			servercmdEditorScale(%client, %scale);
		}
	}
}

function serverCmdGetObjectData(%client)
{
	if((%client.isAdmin && $Pref::Server::AdminEditor) || %client.isSuperAdmin || %client.isEWandUser)
	{
		if(%client.SelectedObject)
			%trans = %client.SelectedObject.getTransform();
			%pos = getWord(%trans,0)@" "@getWord(%trans,1)@" "@getWord(%trans,2);
			%rot = %client.SelectedObject.eulerrot;
			if(%rot $= "")
			{
			%rot = "0 0 0";
			}
			%scale = %client.SelectedObject.getScale();
			messageClient(%client,'MsgGetObjectData',"",%pos,%rot,%scale);
	}
}

function serverCmdSetCopSpawnPoint(%client)
{
	if((%client.isAdmin && $Pref::Server::AdminEditor) || %client.isSuperAdmin || %client.isEWandUser)
	{
		$CopSpawn[$TotalCopSpawnPoints++] = %client.SelectedObject;
		%client.SelectedObject.isCopSpawn = 1;
		messageClient(%client,'',"\c5Added a Cop Spawn Point.");
	}
}

function serverCmdSetRobberSpawnPoint(%client)
{
	if((%client.isAdmin && $Pref::Server::AdminEditor) || %client.isSuperAdmin || %client.isEWandUser)
	{
		$RobberSpawn[$TotalRobberSpawnPoints++] = %client.SelectedObject;
		%client.SelectedObject.isRobberSpawn = 1;
		messageClient(%client,'',"\c5Added a Robber Spawn Point.");
	}
}

function serverCmdSetJailSpawnPoint(%client,%cap)
{
	if((%client.isAdmin && $Pref::Server::AdminEditor) || %client.isSuperAdmin || %client.isEWandUser)
	{
		%client.SelectedObject.isJail = 1;
		%client.SelectedObject.JailMaxCount = %cap;
		
		messageClient(%client,'',"\c5Added a jail spawn point.");
	}
}

function serverCmdSetBaseTrigger(%client,%size)
{
	if((%client.isAdmin && $Pref::Server::AdminEditor) || %client.isSuperAdmin || %client.isEWandUser)
	{
		%trans = %client.SelectedObject.getWorldBoxCenter();
		%x = getWord(%trans,0)-(getword(%size,0)/2);
		%y = getWord(%trans,1)+(getword(%size,1)/2);
		%z = getWord(%trans,2);
		%client.SelectedObject.isBaseTrigger = 1;
		%client.SelectedObject.TriggerSize = %size;

		%client.SelectedObject.BaseTrigger = new Trigger() 
		{
      			position = %x SPC %y SPC %z;
      			rotation = "1 0 0 0";
     			scale = %size;
      			dataBlock = "DepositTrigger";
      			polyhedron = "0.0000000 0.0000000 0.0000000 1.0000000 0.0000000 0.0000000 0.0000000 -1.0000000 0.0000000 0.0000000 0.0000000 1.0000000";
		};
		
		messageClient(%client,'',"\c5Added a Robbbers Base trigger.");
	}
}

function serverCmdSetBankTrigger(%client,%size)
{
	if((%client.isAdmin && $Pref::Server::AdminEditor) || %client.isSuperAdmin || %client.isEWandUser)
	{
		if(%client.SelectedObject.isBankTrigger)
		{
			%client.SelectedObject.BankTrigger.delete();
		}
		if(%client.SelectedObject.isBaseTrigger)
		{
			%client.SelectedObject.BaseTrigger.delete();
		}

		%trans = %client.SelectedObject.getWorldBoxCenter();
		%client.SelectedObject.isBankTrigger = 1;
		%client.SelectedObject.TriggerSize = %size;
		
		%x = getWord(%trans,0)-(getword(%size,0)/2);
		%y = getWord(%trans,1)+(getword(%size,1)/2);
		%z = getWord(%trans,2);
		%client.SelectedObject.BankTrigger = new Trigger() {
     			position = %x SPC %y SPC %z;
      			rotation = "1 0 0 0";
      			scale = %size;
      			dataBlock = "StealTrigger";
      			polyhedron = "0.0000000 0.0000000 0.0000000 1.0000000 0.0000000 0.0000000 0.0000000 -1.0000000 0.0000000 0.0000000 0.0000000 1.0000000";
   		};
		
		messageClient(%client,'',"\c5Added a Bank trigger.");
	}
}

function servercmdcancelautosettings(%client)
{
	if(%client.autoewand $= 1)
	{
		%client.autoewand = 0;
		%client.aescale = "";
		%client.aerot = "";
		messageClient(%client,'',"\c4Auto-Editor Wand Settings Removed.");
	}
	else
	{
		messageClient(%client,'',"\c4Auto-Editor Wand Settings are not Applied!");
	}
}

function serverCmdApplyBBCSettings(%client,%public,%reserved,%reservedname)
{
	if(%client.SelectedObject.getDataBlock().getName() $= "staticBaseplate32")
	{
		%col = %client.selectedObject;
		if(%public $= "1")
		{
			%col.isBBCPublic = 1;
			messageClient(%client,'',"\c5You have made this a Public Building Baseplate!");
		}
		else if(%reserved $= "1")
		{
			%col.isBBCReserved = 1;
			%col.isBBCReservedN = %reservedname;
			messageClient(%client,'','\c5You have made this a Reserved Building Baseplate, for \c0%1', %reservedname);
		}
	}
	else
	{
		messageClient(%client,'',"\c4Sorry, you can only turn Baseplates into building Platforms!");
	}
}

function serverCmdOpenIGEditor(%client, %open)
{
	if(isObject(%client.SelectedObject))
	{
		if(%open $= "1")
		   commandtoclient(%client, 'pop', IngameEditorGUI);
		else 
		   commandtoclient(%client, 'push', IngameEditorGUI);
	}
}