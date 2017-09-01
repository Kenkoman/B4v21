//hammer.cs
//script file for the lego hammer tool/weapon

//effects
datablock ParticleData(hammerExplosionParticle)
{
   dragCoefficient      = 5;
   gravityCoefficient   = -0.15;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 500;
   lifetimeVarianceMS   = 300;
   textureName          = "~/data/particles/cloud";
   colors[0]     = "0.9 0.9 0.7 0.9";
   colors[1]     = "0.9 0.9 0.9 0.0";
   sizes[0]      = 0.5;
   sizes[1]      = 1.0;
};

datablock ParticleEmitterData(hammerExplosionEmitter)
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
   particles = "hammerExplosionParticle";
};

datablock ExplosionData(hammerExplosion)
{
   //explosionShape = "";
   lifeTimeMS = 500;

   Emitter[0] = hammerExplosionEmitter;
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
   lightStartColor = "00.6 0.6 0.0";
   lightEndColor = "0 0 0";
};


//projectile
datablock ProjectileData(hammerProjectile)
{
   //projectileShapeName = "~/data/shapes/arrow.dts";
   explosion           = hammerExplosion;
   //particleEmitter     = as;
   muzzleVelocity      = 50;

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

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(hammerImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/hammer.dts";
   cloaktexture = "~/data/specialfx/cloakTexture";
   emap = true;
    
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
   item = hammer;
   ammo = " ";
   projectile = hammerProjectile;
   projectileType = Projectile;

   directDamage        = 10;
   radiusDamage        = 10;
   damageRadius        = 0.5;
   muzzleVelocity      = 50;
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

function hammerImage::onPreFire(%this, %obj, %slot)
{
	//messageAll( 'MsgClient', 'hammer prefired!!!');
	//Parent::onFire(%this, %obj, %slot);
	%obj.playthread(2, armattack);
}

function hammerImage::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
	//messageAll( 'MsgClient', 'stopfire');
}

function hammerProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	%player = %obj.client.player;
	%client = %player.client;
	//if theres no player, (or client) bail out now
	if(!%player)
		return;
	if (%col.getClassName() !$= "StaticShape")
		return;
  
	%colData = %col.getDataBlock();
	%colDataClass = %colData.classname;
	if( %colDataClass !$= "brick" )
    return;
    
     if (getRawIP(%client) !$= %col.owner)
        recordhammerabuse(%client,%col);
     if(%col.downSize != 0 && %col.upSize != 0) //don't allow the brick to be destroyed if its attached to another brick
        return;
     %brickclient = getClientIDbyIP(%col.owner);
     //echo(%brickclient.namebase @"\'s Brick.");
     if(%client.isSuperAdmin) { //the super admin can destroy all bricks, including permed ones(spam protection)
        killBrick(%obj.client,%col, 1);
        return;
        }
     if(%col.permbrick) //if the brick is permed, forget about letting anyone else destroy it
        return;
     if(%client.isAdmin) { //admins can destroy all bricks--as long as they are not permed
        killBrick(%obj.client,%col, 1);
        return;
        }
     if(%brickclient.isSuperAdmin) //mods and below are not allowed to destroy a super admin's bricks(protection)
        return;
     if(%client.isMod) { //mods can destroy other mod's and below's bricks
        killBrick(%obj.client,%col, 1);
        return;
        }
     else if(!%brickclient.isAdmin && !%brickclient.isMod) { //If the brick placer is not an admin, super, or mod, destroy the brick
        killBrick(%obj.client,%col, 1);
        return;
        }
}


function removeFromUpList(%toRemove, %removeFromBrick)
{
	for(%i = 0; %i < %removeFromBrick.upSize; %i++)
	{
		if(%removeFromBrick.up[%i] == %toRemove)
		{
			%removeFromBrick.up[%i] = %removeFromBrick.up[%removeFromBrick.upSize - 1];
			%removeFromBrick.upSize -= 1;
			return;
		}
	}
}

function removeFromDownList(%toRemove, %removeFromBrick)
{
	for(%i = 0; %i < %removeFromBrick.downSize; %i++)
	{
		if(%removeFromBrick.down[%i] == %toRemove)
		{
			%removeFromBrick.down[%i] = %removeFromBrick.down[%removeFromBrick.downSize - 1];
			%removeFromBrick.downSize -= 1;
			return;
		}
	}
}

function hasPathToGround(%brick, %checkVal)
{
	if(!%checkVal)
	{
		error("***hasPathToGround Called without check value!!");
		return false;
	}

	if(!isObject(%brick))
	{
		//the brick tree is broken somehow
		return false;
	}

	if(%brick.dead == true)
	{
		//brick is about to explode
		return false;
	}

	//if this brick is a baseplate, we've hit the ground
	if(%brick.getDataBlock().className $= "Baseplate")
	{
		//leave a little note that says "hey we got to the ground from this brick at this check time"
		%brick.groundCheckVal = %checkVal;
		return true;
	}

	//check for the note:
	//if we're on the same checkval time as the note, we can assume true and skip the rest of the tree
	if(%brick.groundCheckVal == %checkVal)
	{
		return true;
	}

	//dont check any of the bricks more than once
	if(%brick.checkVal == %checkVal)
	{
		//echo(%brick, " has already been checked");
		return false;
	}

	
	//mark this brick as checked so we dont backtrack
	%brick.checkVal = %checkVal;

	//we're checking a brick, so recurse
	for(%i = 0; %i < %brick.downSize; %i++)
	{
		if( hasPathToGround(%brick.down[%i], %checkVal) )
		{
			//leave a little note that says "hey we got to the ground from this brick at this check time"
			%brick.groundCheckVal = %checkVal;
			return true;
		}
	}

	for(%i = 0; %i < %brick.upSize; %i++)
	{
		if( hasPathToGround(%brick.up[%i], %checkVal) )
		{
			//leave a little note that says "hey we got to the ground from this brick at this check time"
			%brick.groundCheckVal = %checkVal;
			return true;
		}
	}


	return false;
}

function killBrick(%client,%brick, %recursefix)
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
		if( %recursefix != 1 )
		{
		%child = %brick.up[%i];
		if(!hasPathToGround(%child, %groundCheckVal))
		{
			chainKillBrick(%child, $currCheckVal++, 0);
		}
		}
	}
	for(%i = 0; %i < %brick.downSize; %i++)
	{
		if( %recursefix != 1 )
		{
		%child = %brick.down[%i];
		if(!hasPathToGround(%child, %groundCheckVal))
		{
			chainKillBrick(%child, $currCheckVal++, 0);
		}
		}
	}

	//tag this brick as dead
	if (!%brick.dead)
	%brick.dead = true;

	//explode this brick
	%brick.schedule(10, explode);
    getBrickOwner(%brick.owner,-1);
}

//kills brick and all its children
function chainKillBrick(%brick, %checkVal, %iteration)
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
		chainKillBrick(%brick.up[%i], %checkVal, %iteration++);
	}
	for(%i = 0; %i < %brick.downSize; %i++)
	{
		chainKillBrick(%brick.down[%i], %checkVal,  %iteration++);
	}
	
	//tag this brick as dead
	if (!%brick.dead)
	%brick.dead = true;

	if(%iteration <= 1)
		%brick.schedule((%iteration * 25) + 0, explode);
	else
		%brick.schedule((%iteration * 25) + 50, explode);

}

function recursionTest(%iteration)
{
	if (%iteration >= 10)
		return;

	echo("this is a stack size test step ", %iteration);
	recursionTest(%iteration++);

}

/////////////////////////////////////
////////old crap follows/////////////
/////////////////////////////////////

//recursion for hanging bricks
//source brick is the one that just died under this one, or -1 if its the start
function killHangBrick(%checkBrick, %sourceBrick, %iteration)
{
	if(%sourceBrick != -1)
	{
		//remove the source brick references from our down list

		//can you have more than one reference to the same brick in the down list?
		//you could probably break; after you found just one.

		for(%i = 0; %i < %checkBrick.downSize; %i++)
		{
			if(%checkBrick.down[%i] == %sourceBrick)
			{
				//match, copy the end of the list into this slot
				%checkBrick.down[%i] = %checkBrick.down[%checkBrick.downSize - 1];
				//cut the end of the list off
				%checkBrick.downSize -= 1;
				//do this slot again
				//%i--;
				break;
			}
		}
	}

	//we've removed all the source brick references from out downward attachment list
	//so now if the down list is empty, we're hanging and we should die

	if(%checkBrick.downSize == 0)
	{
		for(%i = 0; %i < %checkBrick.upSize; %i++)
		{
			//check all the brick this one is hanging from
			killHangBrick(%checkBrick.up[%i], %checkBrick, %iteration++);
		}

		//brick break effects go here.  probably want to schedule these...
		if(%iteration <= 1)
			%brick.schedule((%iteration * 25) + 0, explode);
		else
			%brick.schedule((%iteration * 25) + 50, explode);
	}
}



//stop when... there is at least one brick in the down list that is not hanging

function killTopBrick(%topBrick)
{

	//remove this guy from the top list of everyone who is stuck underneath him
	//if any of those guys underneath is flagged as 'hanging' run a check on them
		//that check should recurse and check for hanging children, 
		//if it finds a non hanging child, it stops
		//if it finds a dead end (no children), 
		//it calls killhangbrick 

	for(%i = 0; %i < %topBrick.downSize; %i++)
	{
		removeFromUpList(%topBrick, %topBrick.down[%i]);
		if(%topBrick.down[%i].wasHung == true)
		{
			//call our recursive 
			hangCheck(%topBrick.down[%i]);
		}
	}

	//kill the original brick
	%topBrick.schedule(10, explode);
}

function hangCheck(%brick)
{
	if(%brick.downSize == 0)
	{
		//dead end, kill em
		killHangBrick(%brick, -1, 0);
		return;
	}

	//check for hanging children
	for(%i = 0; %i < %brick.downSize; %i++)
	{
		if(%brick.down[%i].wasHung != true)
		{
			return;
		}
	}


		
	//all of the children were hung, so check them too
	for(%i = 0; %i < %brick.downSize; %i++)
	{
		hangCheck(%brick.down[%i]);
	}
}

