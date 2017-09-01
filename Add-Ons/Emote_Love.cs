//Emote_Love.cs
// type /love, get hearts

function serverCmdLove(%client)
{
	if(isObject(%client.player))
		%client.player.emote(LoveImage);
}

datablock ParticleData(LoveParticle)
{
   dragCoefficient      = 5.0;
   gravityCoefficient   = -0.2;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 1000;
   lifetimeVarianceMS   = 500;
   useInvAlpha          = false;
   textureName          = "base/data/particles/heart";
   colors[0]     = "0.8 0.0 0.0 0.7";
   colors[1]     = "1.0 0.0 0.0 0.5";
   colors[2]     = "0.8 0.0 0.8 0.0";
   sizes[0]      = 0.4;
   sizes[1]      = 0.6;
   sizes[2]      = 0.4;
   times[0]      = 0.0;
   times[1]      = 0.2;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(LoveEmitter)
{
   ejectionPeriodMS = 35;
   periodVarianceMS = 0;
   ejectionVelocity = 0.5;
   ejectionOffset   = 1.0;
   velocityVariance = 0.49;
   thetaMin         = 0;
   thetaMax         = 120;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "LoveParticle";

   uiName = "Emote - Love";
};

datablock ShapeBaseImageData(LoveImage)
{
	shapeFile = "base/data/shapes/empty.dts";
	emap = false;

	mountPoint = $HeadSlot;

	stateName[0]					= "Ready";
	stateTransitionOnTimeout[0]		= "FireA";
	stateTimeoutValue[0]			= 0.01;

	stateName[1]					= "FireA";
	stateTransitionOnTimeout[1]		= "Done";
	stateWaitForTimeout[1]			= True;
	stateTimeoutValue[1]			= 0.350;
	stateEmitter[1]					= LoveEmitter;
	stateEmitterTime[1]				= 0.350;

	stateName[2]					= "Done";
	stateScript[2]					= "onDone";
};
function LoveImage::onDone(%this,%obj,%slot)
{
	%obj.unMountImage(%slot);
}