//brick.cs
//this contains the generic brick deploying projectile 

datablock AudioProfile(brickMoveSound)
{
   filename    = "~/data/sound/clickMove.wav";
   description = AudioClosest3d;
   preload = true;
};
datablock AudioProfile(brickPlantSound)
{
   filename    = "~/data/sound/clickPlant.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(brickRotateSound)
{
   filename    = "~/data/sound/clickRotate.wav";
   description = AudioClosest3d;
   preload = true;
};


datablock AudioProfile(brickExplosionSound)
{
   filename    = "~/data/sound/breakBrick.wav";
   description = AudioClose3d;
   preload = true;
};

datablock ParticleData(brickTrailParticle)
{
	dragCoefficient		= 3.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 200;
	lifetimeVarianceMS	= 0;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= false;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "~/data/particles/dot";
	//animTexName		= "~/data/particles/dot";

	// Interpolation variables
	colors[0]	= "1 1 1 0.5";
	colors[1]	= "0 0 1 0.8";
	colors[2]	= "1 1 1 0.0";
	sizes[0]	= 0.1;
	sizes[1]	= 0.3;
	sizes[2]	= 0.01;
	times[0]	= 0.0;
	times[1]	= 0.3;
	times[2]	= 1.0;
};

datablock ParticleEmitterData(brickTrailEmitter)
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;

   ejectionVelocity = 0; //0.25;
   velocityVariance = 0; //0.10;

   ejectionOffset = 0;

   thetaMin         = 0.0;
   thetaMax         = 90.0;  

   particles = brickTrailParticle;
};

//effects
datablock ParticleData(brickExplosionParticle)
{
	dragCoefficient      = 5;
	gravityCoefficient   = 0.1;
	inheritedVelFactor   = 0.2;
	constantAcceleration = 0.0;
	lifetimeMS           = 500;
	lifetimeVarianceMS   = 300;
	textureName          = "~/data/particles/chunk";
	spinSpeed		= 100.0;
	spinRandomMin		= -150.0;
	spinRandomMax		= 150.0;
	useInvAlpha = false;
	colors[0]     = "0.9 0.9 0.7 1.0";
	colors[1]     = "0.9 0.9 0.7 0.0";
	sizes[0]      = 0.4;
	sizes[1]      = 0.0;
};

datablock ParticleEmitterData(brickExplosionEmitter)
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   ejectionVelocity = 5;
   velocityVariance = 1.0;
   ejectionOffset   = 0.2;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "brickExplosionParticle";
};

datablock ExplosionData(brickExplosion)
{
   //explosionShape = "";
	soundProfile = brickExplosionSound;

   lifeTimeMS = 550;

   particleEmitter = brickExplosionEmitter;
   particleDensity = 10;
   particleRadius = 0.2;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = false;
   camShakeFreq = "10.0 11.0 10.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 0.5;
   camShakeRadius = 10.0;

   // Dynamic light
   lightStartRadius = 0;
   lightEndRadius = 2;
   lightStartColor = "0.3 0.6 0.7";
   lightEndColor = "0 0 0";
};

datablock ExplosionData(brickDeployExplosion)
{
	//explosionShape = "";
	lifeTimeMS = 300;

	//particleEmitter = swordExplosionEmitter;
	//particleDensity = 10;
	//particleRadius = 0.2;

	faceViewer     = true;
	explosionScale = "1 1 1";

	shakeCamera = false;
	camShakeFreq = "20.0 22.0 20.0";
	camShakeAmp = "1.0 1.0 1.0";
	camShakeDuration = 0.5;
	camShakeRadius = 10.0;

	// Dynamic light
	lightStartRadius = 0;
	lightEndRadius = 3;
	lightStartColor = "1 1 1";
	lightEndColor = "0 0 0";
};

//projectile
datablock ProjectileData(brickDeployProjectile)
{
	//projectileShapeName = "~/data/shapes/arrow.dts";
	directDamage        = 10;
	radiusDamage        = 10;
	damageRadius        = 0.5;
	explosion           = brickDeployExplosion;
	particleEmitter     = brickTrailEmitter;

	muzzleVelocity      = 30;
	velInheritFactor    = 0.0;

	armingDelay         = 0;
	lifetime            = 250;
	fadeDelay           = 70;
	bounceElasticity    = 0;
	bounceFriction      = 0;
	isBallistic         = false;
	gravityMod = 0.0;

	hasLight    = false;
	lightRadius = 3.0;
	lightColor  = "0 0 0.5";
};


function brickDeployProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	//messageall(type, 'projectile collision col = %1', %col.getDataBlock().classname $= "baseplate");
	//messageall(type, 'projectile collision col = %1', %col);
	//messageall(type, 'projectile collision player = %1', %obj.client.player);

	%player = %obj.client.player;

	//if theres no player, (or client) bail out now
	if(!%player)
		return;

	%image = %player.getMountedImage(0);	//the thing the guy is holding
	%static = %image.staticShape;
	%ghost = %image.ghost;			//ghost is what we're going to deploy


	//if there's no ghost, the guy switched or dropped guns or maybe died, so bail out
	if(%ghost $= ""){
		error("No ghost brick found");
		return;
	}

	if(%col.getClassname() !$= "StaticShape")
		return;

	%colData = %col.getDataBlock();
	%className = %colData.classname;

	if((%className $= "baseplate") || (%className $= "brick") ){
		//error("hit baseplate");
		//we've hit a baseplate
		//deploy the temporary brick based on what image the player is holding

		%baseplate = %col;
		%baseplateTrans = %baseplate.getTransform();
		//error("pos = ", %pos);
		%target = %pos;

		//echo("los = ", %los);
		//echo("deploying on baseplate = ", %searchresult);
		//echo("baseplate name = ", %searchresult.getdataBlock().getname() );
		//echo("basplate trans = ", %baseplateTrans);

		%targetX = getword(%target, 0);
		%targetY = getword(%target, 1);
		%targetZ = getword(%target, 2);


		//echo("target xyz = ", %targetX @ " " @ %targetY @ " " @ %targetZ);

		%baseplateX = getword(%baseplateTrans, 0);
		%baseplateY = getword(%baseplateTrans, 1);
		%baseplateZ = getword(%baseplateTrans, 2);

		%xDiff = %targetX - %baseplateX;
		%yDiff = %targetY - %baseplateY;
		%zDiff = %targetZ - %baseplateZ;
		
		//round to the nearest 0.5
		%deployX = %baseplateX + (mFloor(%xDiff * 2 ) / 2);
		%deployY = %baseplateY + (mFloor(%yDiff * 2 ) / 2);
		%deployZ = %baseplateZ + (%colData.z * 0.2);			//move it to the top of whatever we're putting it on


		%d = new StaticShape()
		{
			datablock = %ghost;
		};
		MissionCleanup.add(%d);
		%d.setTransform(%deployX @ " " @ %deployY @ " " @ %deployZ);
		%d.setScale(%ghost.scale);
		%d.playthread(0, deploy);

		

		//error("deploying at " @ %deployX @ " " @ %deployY @ " " @ %deployZ);
		
		//remove the players old temp brick if he has one
		if(%player.tempBrick)
			%player.tempBrick.delete();
	
		//set the tempBrick so he can move it
		%player.tempBrick = %d;

		//tell the client to go into "brick moving mode"
		//client brick movement commands are relative to their view, 
		//so tell them what direction they are facing

		//some cmdToClient goes in here...

	}

}

////////////////////////////////////////////////////////
function BrickImage::onDeploy(%this, %obj, %slot)
{

}

function Brick::onUse(%this,%user, %InvPosition)
{
	//if the image is mounted already, unmount it
	//if it isnt, mount it

	%mountPoint = %this.image.mountPoint;
	%mountedImage = %user.getMountedImage(%mountPoint); 
	
	if(%mountedImage)
	{
		//echo(%mountedImage);
		if(%mountedImage == %this.image.getId())
		{
			//our image is already mounted so unmount it
			%user.unMountImage(%mountPoint);
			//hilight nothing
			//messageClient(%user.client, 'MsgHilightInv', -1);
		}
		else
		{
			//something else is there so mount our image
			%user.mountimage(%this.image, %mountPoint);
			//hilight the slot 
			//messageClient(%user.client, 'MsgHilightInv', %InvPosition);
		}
	}
	else
	{
		//nothing there so mount 
		%user.mountimage(%this.image, %mountPoint);
	}
	

}



function BrickImage::onMount(%this,%obj,%slot)
{
   // Images assume a false ammo state on load.  We need to
   // set the state according to the current inventory.

	//fixArmReady(%obj);
	if(%this.armReady)
	{
		if(%obj.getMountedImage($LeftHandSlot))
		{
			if(%obj.getMountedImage($LeftHandSlot).armReady)
				%obj.playthread(1, armReadyBoth);
			else
				%obj.playthread(1, armReadyRight);
		}
		else
		{
			%obj.playthread(1, armReadyRight);
		}
	}

	if(%this.ammo)	//dont check ammo if the image doesnt use ammo
	{
		if (%obj.getInventory(%this.ammo))
		%obj.setImageAmmo(%slot,true);
	}
	else
	{
		%obj.setImageAmmo(%slot,true);
	}
}

function BrickImage::onUnmount(%this,%obj,%slot)
{
	%obj.playthread(2, root);	//stop arm swinging 

	%leftimage = %obj.getmountedimage($lefthandslot);

	if(%leftimage)
	{
		if(%leftimage.armready)
		{
			%obj.playthread(1, armreadyleft);
			return;
		}
	}
	%obj.playthread(1, root);

}




