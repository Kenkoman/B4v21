//Built By Ephialtes
//ONLY For the RtB Mod.
//Snippets and Parts of this Code are not allowed into other Mods Without the Authors Permission
//##############################################################################################
function serverCmdBlowBomb(%client,%obj,%waitTime)
{
	if(isObject(%client.player))
	{
if($Pref::Server::BombRigging $= 1)
{
blowbricks(%client,%obj);
}
	}

}

function serverCmdBlowTimedBomb(%client,%obj,%waitTime)
{
	if(isObject(%client.player))
	{
if($Pref::Server::BombRigging == 1)
{
%client.timerigged = 0;
blowbricks(%client,%obj);
messageAll('name', '\c0%1\'s\c5 Bomb was Detonated!', %obj.RiggerID.name);
}
	}

}

function blowbricks(%client,%obj)
{

   %obj.bombemitter = new ParticleEmitterNode(bombexplosioner) {
      position = %obj.getTransform();
      rotation = "1 0 0 0";
      scale = "1 1 1";
      dataBlock = "bombParticleEmitterNode";
      emitter = "bombExplosionEmitter";
      velocity = "1.0";
   };

   %obj.bombemitter2 = new ParticleEmitterNode(bomb2explosioner) {
      position = %obj.getTransform();
      rotation = "1 0 0 0";
      scale = "1 1 1";
      dataBlock = "bombParticleEmitterNode";
      emitter = "bomb2ExplosionEmitter";
      velocity = "1.0";
   };

   %obj.bombemitter3 = new ParticleEmitterNode(bomb3explosioner) {
      position = %obj.getTransform();
      rotation = "1 0 0 0";
      scale = "1 1 1";
      dataBlock = "bombParticleEmitterNode";
      emitter = "bomb3ExplosionEmitter";
      velocity = "1.0";
   };

   %obj.bombemitter4 = new ParticleEmitterNode(bomb4explosioner) {
      position = %obj.getTransform();
      rotation = "1 0 0 0";
      scale = "1 1 1";
      dataBlock = "bombParticleEmitterNode";
      emitter = "bomb4ExplosionEmitter";
      velocity = "1.0";
   };


bombradiusdamage(%obj, %obj.getPosition(), 12, 100, Explosion, 1000, %client);
%obj.dead = true;
%obj.schedule(1001, explode);
Schedule(1000,0,"DestroyNodes",%client,%obj);
}

function destroynodes(%client,%obj)
{
%obj.bombemitter.delete();
%obj.bombemitter2.delete();
%obj.bombemitter3.delete();
%obj.bombemitter4.delete();
}

datablock ParticleEmitterNodeData(bombParticleEmitterNode)
{
	timeMultiple = 1.0;   //time multiple must be between 0.01 and 100.0
};

datablock ParticleData(bombExplosionParticle)
{
	dragCoefficient		= 3.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= 0.5;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 900;
	lifetimeVarianceMS	= 300;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= true;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "~/data/particles/cloud";
	//animTexName		= "~/data/particles/cloud";

	// Interpolation variables
	colors[0]	= "0.4 0.1 0 0.9";
	colors[1]	= "0.4 0.1 0 0.0";
	sizes[0]	= 50.0;
	sizes[1]	= 50.0;
	times[0]	= 0.0;
	times[1]	= 1.0;
};

datablock ParticleEmitterData(bombExplosionEmitter)
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   lifeTimeMS	   = 21;
   ejectionVelocity = 8;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "bombExplosionParticle";
};

datablock ParticleData(bomb2ExplosionParticle)
{
	dragCoefficient		= 3.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= 0.5;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 900;
	lifetimeVarianceMS	= 300;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= true;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "~/data/particles/chunk";
	//animTexName		= "~/data/particles/cloud";

	// Interpolation variables
	colors[0]	= "1 0.2 0 0.9";
	colors[1]	= "1 0.2 0 0.0";
	sizes[0]	= 5;
	sizes[1]	= 5;
	times[0]	= 0.0;
	times[1]	= 1.0;
};

datablock ParticleEmitterData(bomb2ExplosionEmitter)
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   lifeTimeMS	   = 21;
   ejectionVelocity = 8;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "bomb2ExplosionParticle";
};

datablock ParticleData(bomb3ExplosionParticle)
{
	dragCoefficient		= 3.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= 0.5;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 900;
	lifetimeVarianceMS	= 300;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= true;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "~/data/particles/cloud";
	//animTexName		= "~/data/particles/cloud";

	// Interpolation variables
	colors[0]	= "0.3 0.3 0.2 0.9";
	colors[1]	= "0.2 0.2 0.2 0.0";
	sizes[0]	= 50;
	sizes[1]	= 50;
	times[0]	= 0.0;
	times[1]	= 1.0;
};

datablock ParticleEmitterData(bomb3ExplosionEmitter)
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   lifeTimeMS	   = 21;
   ejectionVelocity = 8;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "bomb3ExplosionParticle";
};

datablock ParticleData(bomb4ExplosionParticle)
{
	dragCoefficient		= 0.1;
	windCoefficient		= 0.0;
	gravityCoefficient	= 2.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 1000;
	lifetimeVarianceMS	= 500;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= true;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "~/data/particles/chunk";
	//animTexName		= "~/data/particles/cloud";

	// Interpolation variables
	colors[0]	= "0.0 0.0 0.0 1.0";
	colors[1]	= "0.0 0.0 0.0 0.0";
	sizes[0]	= 5;
	sizes[1]	= 5;
	times[0]	= 0.0;
	times[1]	= 1.0;
};

datablock ParticleEmitterData(bomb4ExplosionEmitter)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   lifetimeMS       = 7;
   ejectionVelocity = 15;
   velocityVariance = 5.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "bomb4ExplosionParticle";
};


datablock TriggerData(bombproofTrigger)
{
   tickPeriodMS = 100;
};

function bombproofTrigger::onEnterTrigger(%this,%trigger,%obj)
{
	if($bombrigging == 1)
	{
		%obj.client.bombproof = 1;
		messageClient(%obj.client,"","\c5You have Entered a Bomb Proof Zone.");
	}
}

function bombproofTrigger::onLeaveTrigger(%this,%trigger,%obj)
{
	if($bombrigging == 1)
	{
		%obj.client.bombproof = 0;
		messageClient(%obj.client,"","\c5You have Left the Bomb Proof Zone.");
	}
}