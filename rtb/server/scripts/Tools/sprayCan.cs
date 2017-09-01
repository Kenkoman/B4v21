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

datablock ProjectileData(sprayCanProjectile)
{
   //projectileShapeName = "~/data/shapes/arrow.dts";
   //explosion           = sprayCanExplosion;
   //particleEmitter     = sprayCanTrailEmitter;

   muzzleVelocity      = 300;
   velInheritFactor    = 0;

   armingDelay         = 0;
   lifetime            = 100;
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
datablock ItemData(sprayCan)
{
	category = "Tools";  // Mission editor category
	className = "tool";   // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/spraycan.dts";
	skinName = 'rainbow';
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = false;
	cost = 30;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a spray can.';
	invName = 'Spray Can';
	image = blueSprayCanImage;
};



//////////
//IMAGES//
//////////
datablock ShapeBaseImageData(SprayCanImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/spraycan.dts";
   skinName = 'rainbow';
   emap = false;
	cloakable = false;
   mountPoint = 0;
   offset = "0 0 0";

   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = sprayCan;
   ammo = " ";
   projectile = sprayCanProjectile;
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
	//stateEmitter[2]					= sprayCanExplosionEmitter;
	//stateEmitterTime[2]				= 0.1;
	stateSound[2]					= sprayFireSound;

};

///////////
//METHODS//
///////////
//using the paint can item
function sprayCan::onUse(%this,%player, %invPosition)
{
	%client = %player.client;
	if(%client)
	{
		%color = %client.color;
	}
	%mountedImage = %player.getMountedImage($RightHandSlot);

	//if a spray can is already mounted
	if(%mountedImage.item $= "sprayCan" && (%player.currWeaponSlot == %invPosition)) 
	{
		%client.colorIndex++;

		if(%client.colorIndex > $TotalColors)
		{
			%client.colorIndex = 0;
		}	
		commandtoclient(%client,'ShowBrickImage',$ColorPreview[%client.colorIndex]);
	}
	else 
	{
		//if the client has a color, use that
		%image = nameToID("SprayCanImage");
		%player.mountImage(%image, $RightHandSlot, 1, %image.skinName);
		//hilight inv slot
		messageClient(%client, 'MsgHilightInv', '', %InvPosition);
		%player.currWeaponSlot = %invPosition;
		%client.color = $legoColor[%obj.client.colorIndex];
		commandtoclient(%client,'ShowBrickImage',$ColorPreview[%client.colorIndex]);
	}
}

//Collisions//

function sprayCanProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	%isTrusted = checkSafe(%col,%obj.client);
	
	if(%obj.client.isImprisoned)
	{
		messageClient(%client,'',"\c4No painting you imprisoned, no good, piece of poo!");
		return;
	}

	if(%col.owner == %obj.client || (%col.owner.secure == 0 && %col.OwnerAway == 0)|| %isTrusted || %obj.client.isAdmin || %obj.client.isSuperAdmin)
	{
		if(%col.getClassName() $= "WheeledVehicle"|| %col.getClassName() $= "StaticShape" || %col.getClassName() $= "Player")
		{
			if(%col.getSkinName() !$= $legoColor[%obj.client.colorIndex])
			{	
				%obj.client.Undo[0] = 1;
				%obj.client.Undo[1] = %col;
				%obj.client.Undo[2] = %col.getSkinName();
				%col.setSkinName($legoColor[%obj.client.colorIndex]);		
				%col.SkinName = %col.getSkinName();
				if(%col.Datablock $= "staticbrick2x2FX")
				{
					if(%col.FXMode $= 1)
					{
					%position = %col.flameEmitter.getTransform();
					%col.flameEmitter.delete();
					%testcolor = %obj.client.colorIndex;
					if(%testcolor $= "11")
					{
			   			%col.flameEmitter = new ParticleEmitterNode(brickFireNode) {
     			   			position = %position;
      			   			rotation = "1 0 0 0";
	      		   			scale = "1 1 1";
      			   			dataBlock = "FireParticleEmitterNode";
     		   	   			emitter = "FireParticleEmitter2";
      			   			velocity = "1.0";
   			  			};
					}
					else if(%testcolor $= "8")
					{
			   			%col.flameEmitter = new ParticleEmitterNode(brickFireNode) {
     			   			position = %position;
      			   			rotation = "1 0 0 0";
	      		   			scale = "1 1 1";
      			   			dataBlock = "FireParticleEmitterNode";
     		   	   			emitter = "FireParticleEmitter3";
      			   			velocity = "1.0";
   			  			};
					}
					else if(%testcolor $= "14")
					{
			   			%col.flameEmitter = new ParticleEmitterNode(brickFireNode) {
     			   			position = %position;
      			   			rotation = "1 0 0 0";
	      		   			scale = "1 1 1";
      			   			dataBlock = "FireParticleEmitterNode";
     		   	   			emitter = "FireParticleEmitter4";
      			   			velocity = "1.0";
   			  			};
					}
					else if(%testcolor $= "1")
					{
			   			%col.flameEmitter = new ParticleEmitterNode(brickFireNode) {
     			   			position = %position;
      			   			rotation = "1 0 0 0";
	      		   			scale = "1 1 1";
      			   			dataBlock = "FireParticleEmitterNode";
     		   	   			emitter = "FireParticleEmitter5";
      			   			velocity = "1.0";
   			  			};
					}
					else
					{
			   			%col.flameEmitter = new ParticleEmitterNode(brickFireNode) {
     			   			position = %position;
      			   			rotation = "1 0 0 0";
	      		   			scale = "1 1 1";
      			   			dataBlock = "FireParticleEmitterNode";
     		   	   			emitter = "FireParticleEmitter";
      			   			velocity = "1.0";
   			  			};
					}
					}

				}	
			}
		}
	}
}

function checkSafe(%col,%client)
{
	%isTrusted = 0;

	for(%trusted = 0; %trusted < clientGroup.GetCount(); %trusted++)
	{
		//echo(clientGroup.GetCount());
		%cl = clientGroup.getObject(%trusted);
		//echo(%cl);
		for(%safe = 0; %safe < %col.Owner.SafeListNum + 1; %safe++)
		{
			//echo(%col.Owner.SafeList[%safe]);
			if(%col.Owner.SafeList[%safe] == %cl && %cl == %client)
			{
				%isTrusted = 1;
			}
		}

	} 
	return %isTrusted;
}
function getFriends(%client, %person)
{
	%isFriend = 0;

	for(%friend = 0; %friend < %client.FriendListNum + 1; %friend++)
	{
		if(%client.FriendList[%friend] $= %person)
		{
			%isFriend = 1;
		}
	}
	return %isFriend;
}
function getSafe(%client, %person)
{
	%isSafe = 0;

	for(%safe = 0; %safe < %client.SafeListNum + 1; %safe++)
	{
		if(%client.SafeList[%safe] $= %person)
		{
			%isSafe = 1;
		}
	}
	return %isSafe;
}