//emote_hate.cs


function serverCmdHate(%client)
{
	if(isObject(%client.player))
		%client.player.emote(HateImage);
}

datablock ParticleData(HateParticle)
{
   dragCoefficient      = 6.0;
   gravityCoefficient   = -0.0;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 1000;
   lifetimeVarianceMS   = 500;
   useInvAlpha          = true;
   textureName          = "base/data/particles/cloud";
   colors[0]     = "0.2 0.2 0.2 0.5";
   colors[1]     = "0.0 0.0 0.0 1.0";
   colors[2]     = "0.0 0.0 0.0 0.0";
   sizes[0]      = 0.4;
   sizes[1]      = 0.6;
   sizes[2]      = 0.4;
   times[0]      = 0.0;
   times[1]      = 0.2;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(HateEmitter)
{
   ejectionPeriodMS = 35;
   periodVarianceMS = 0;
   ejectionVelocity = 7.0;
   ejectionOffset   = 0.2;
   velocityVariance = 0.5;
   thetaMin         = 0;
   thetaMax         = 10;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "HateParticle";

   uiName = "Emote - Hate";
};
datablock ShapeBaseImageData(HateImage)
{
   shapeFile = "base/data/shapes/empty.dts";
	emap = false;

	mountPoint = $HeadSlot;
   rotation = "1 0 0 -90";

	stateName[0]					= "Ready";
	stateTransitionOnTimeout[0]		= "FireA";
	stateTimeoutValue[0]			= 0.01;

	stateName[1]					= "FireA";
	stateTransitionOnTimeout[1]		= "Done";
	stateWaitForTimeout[1]			= True;
	stateTimeoutValue[1]			= 0.350;
	stateEmitter[1]					= HateEmitter;
	stateEmitterTime[1]				= 0.350;

	stateName[2]					= "Done";
	stateScript[2]					= "onDone";
};
function HateImage::onDone(%this,%obj,%slot)
{
	%obj.unMountImage(%slot);
}