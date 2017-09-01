//emote_confusion.cs

function serverCmdConfusion(%client)
{
	if(isObject(%client.player))
		%client.player.emote(WtfImage);
}
function serverCmdWtf(%client)
{
	if(isObject(%client.player))
		%client.player.emote(WtfImage);
}


datablock ParticleData(WtfParticle)
{
   dragCoefficient      = 6.0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 1000;
   lifetimeVarianceMS   = 500;
   useInvAlpha          = true;
   textureName          = "base/data/particles/qmark";
   colors[0]     = "1.0 1.0 1.0 1.0";
   colors[1]     = "1.0 1.0 1.0 1.0";
   colors[2]     = "1.0 1.0 1.0 0.0";
   sizes[0]      = 0.6;
   sizes[1]      = 0.4;
   sizes[2]      = 0.4;
   times[0]      = 0.0;
   times[1]      = 0.2;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(WtfEmitter)
{
   ejectionPeriodMS = 60;
   periodVarianceMS = 0;
   ejectionVelocity = 0.5;
   ejectionOffset   = 0.9;
   velocityVariance = 0.49;
   thetaMin         = 0;
   thetaMax         = 120;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "WtfParticle";

   uiName = "Emote - Confusion";
};
datablock ShapeBaseImageData(WtfImage)
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
	stateEmitter[1]					= WtfEmitter;
	stateEmitterTime[1]				= 0.350;

	stateName[2]					= "Done";
	stateScript[2]					= "onDone";
};
function WtfImage::onDone(%this,%obj,%slot)
{
	%obj.unMountImage(%slot);
}