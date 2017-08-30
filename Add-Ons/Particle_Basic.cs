//basic particle emitters

datablock ParticleData(BurnParticleA)
{
	textureName          = "base/data/particles/cloud";
	dragCoefficient      = 0.0;
	gravityCoefficient   = -0.7;
	inheritedVelFactor   = 0.0;
	windCoefficient      = 0;
	constantAcceleration = 0.0;
	lifetimeMS           = 1100;
	lifetimeVarianceMS   = 300;
	spinSpeed     = 0;
	spinRandomMin = -90.0;
	spinRandomMax =  90.0;
	useInvAlpha   = false;

	colors[0]	= "1   1   0.3 0.0";
	colors[1]	= "1   1   0.3 1.0";
	colors[2]	= "0.6 0.0 0.0 0.0";

	sizes[0]	= 0.0;
	sizes[1]	= 1.0;
	sizes[2]	= 0.6;

	times[0]	= 0.0;
	times[1]	= 0.2;
	times[2]	= 1.0;
};

datablock ParticleEmitterData(BurnEmitterA)
{
   ejectionPeriodMS = 14;
   periodVarianceMS = 4;
   ejectionVelocity = 0.0;
   ejectionOffset   = 0.40;
   velocityVariance = 0.0;
   thetaMin         = 0;
   thetaMax         = 180;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;

   //lifetimeMS = 5000;
   particles = BurnParticleA;   

	uiName = "Fire A";
};

datablock ParticleData(BurnParticleB)
{
	textureName          = "base/data/particles/cloud";
	dragCoefficient      = 0.0;
	gravityCoefficient   = 0.0;
	inheritedVelFactor   = 0.0;
	windCoefficient      = 0;
	constantAcceleration = 3.0;
	lifetimeMS           = 800;
	lifetimeVarianceMS   = 100;
	spinSpeed     = 0;
	spinRandomMin = -90.0;
	spinRandomMax =  90.0;
	useInvAlpha   = false;

	colors[0]	= "1   1   0.3 0.0";
	colors[1]	= "1   1   0.3 1.0";
	colors[2]	= "0.6 0.0 0.0 0.0";

	sizes[0]	= 0.0;
	sizes[1]	= 1.0;
	sizes[2]	= 0.6;

	times[0]	= 0.0;
	times[1]	= 0.2;
	times[2]	= 1.0;
};

datablock ParticleEmitterData(BurnEmitterB)
{
   ejectionPeriodMS = 14;
   periodVarianceMS = 4;
   ejectionVelocity = 3;
   ejectionOffset   = 0.00;
   velocityVariance = 0.0;
   thetaMin         = 0;
   thetaMax         = 5;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;

   //lifetimeMS = 5000;
   particles = BurnParticleB;   

	uiName = "Fire B";
};


datablock ParticleData(LaserParticleA)
{
	textureName          = "base/data/particles/dot";
	dragCoefficient      = 0.0;
	gravityCoefficient   = 0.0;
	inheritedVelFactor   = 0.0;
	windCoefficient      = 0;
	constantAcceleration = 0;
	lifetimeMS           = 1100;
	lifetimeVarianceMS   = 300;
	spinSpeed     = 0;
	spinRandomMin = -90.0;
	spinRandomMax =  90.0;
	useInvAlpha   = false;

	colors[0]	= "1.0 0.0 0.0 1.0";
	colors[1]	= "1.0 0.0 0.0 1.0";
	colors[2]	= "1.0 0.0 0.0 0.0";

	sizes[0]	= 0.1;
	sizes[1]	= 0.1;
	sizes[2]	= 0.1;

	times[0]	= 0.0;
	times[1]	= 0.9;
	times[2]	= 1.0;
};

datablock ParticleEmitterData(LaserEmitterA)
{
   ejectionPeriodMS = 8;
   periodVarianceMS = 0;
   ejectionVelocity = 6.0;
   ejectionOffset   = 0.0;
   velocityVariance = 0.0;
   thetaMin         = 0;
   thetaMax         = 0;
   phiReferenceVel  = 0;
   phiVariance      = 0;
   overrideAdvance = false;

   //lifetimeMS = 5000;
   particles = LaserParticleA;   

	uiName = "Laser A";
};




datablock ParticleData(FogParticleA)
{
	textureName          = "base/data/particles/cloud";
	dragCoefficient      = 0.0;
	gravityCoefficient   = 0.0;
	inheritedVelFactor   = 0.0;
	windCoefficient      = 0;
	constantAcceleration = 0;
	lifetimeMS           = 3100;
	lifetimeVarianceMS   = 300;
	spinSpeed     = 0;
	spinRandomMin = -30.0;
	spinRandomMax =  30.0;
	useInvAlpha   = true;

	colors[0]	= "1 1 1 0.0";
	colors[1]	= "1 1 1 0.5";
	colors[2]	= "1 1 1 0.0";

	sizes[0]	= 1.5;
	sizes[1]	= 2.0;
	sizes[2]	= 1.6;

	times[0]	= 0.0;
	times[1]	= 0.2;
	times[2]	= 1.0;
};

datablock ParticleEmitterData(FogEmitterA)
{
   ejectionPeriodMS = 75;
   periodVarianceMS = 4;
   ejectionVelocity = 1.2;
   ejectionOffset   = 0.4;
   velocityVariance = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;

   //lifetimeMS = 5000;
   particles = FogParticleA;   

	uiName = "Fog A";
};


datablock ParticleData(WaterParticleA)
{
	textureName          = "base/data/particles/cloud";
	dragCoefficient      = 0.0;
	gravityCoefficient   = 1.0;
	inheritedVelFactor   = 0.0;
	windCoefficient      = 0;
	constantAcceleration = 0;
	lifetimeMS           = 2000;
	lifetimeVarianceMS   = 100;
	spinSpeed     = 0;
	spinRandomMin = -30.0;
	spinRandomMax =  30.0;
	useInvAlpha   = true;

	colors[0]     = "0.000 0.2 0.5 0.9";
	colors[1]     = "0.000 0.4 0.8 0.8";
	colors[2]     = "0.4 0.85 0.9 0.7";
	colors[3]     = "0.9 0.9 0.9 0.0";

	sizes[0]	= 0.5;
	sizes[1]	= 2.0;
	sizes[2]	= 1.6;
	sizes[3]	= 1.6;

	times[0]	= 0.0;
	times[1]	= 0.2;
	times[2]	= 0.8;
	times[3]	= 1.0;
};

datablock ParticleEmitterData(WaterEmitterA)
{
   ejectionPeriodMS = 50;
   periodVarianceMS = 4;
   ejectionVelocity = 0.9;
   ejectionOffset   = 0.4;
   velocityVariance = 0.0;
   thetaMin         = 0;
   thetaMax         = 5;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;

   //lifetimeMS = 5000;
   particles = WaterParticleA;   

	uiName = "Water A";
};


datablock ParticleData(FogParticle)
{
   textureName          = "base/data/particles/cloud";
   dragCoefficient     = 0.0;
   gravityCoefficient   = 0.0; 
   inheritedVelFactor   = 0.00;
   lifetimeMS           = 6800;
   lifetimeVarianceMS   = 250;
   useInvAlpha = true;
   spinRandomMin = -5.0;
   spinRandomMax = 5.0;

   colors[0]     = "1.0 1.0 1.0 0.0";
   colors[1]     = "0.9 0.9 1.0 0.1";
   colors[2]     = "1.0 1.0 1.0 0.0";

   sizes[0]      = 4.0;
   sizes[1]      = 5.0;
   sizes[2]      = 6.0;

   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(FogEmitter)
{
   ejectionPeriodMS = 200;
   periodVarianceMS = 5;

   ejectionOffset = 1.0;
   ejectionOffsetVariance = 0.5;
	

   ejectionVelocity = 0.5;
   velocityVariance = 0.50;

   thetaMin         = 0.0;
   thetaMax         = 90.0;  

   phiReferenceVel  = 0;
   phiVariance      = 360;

   overrideAdvances = false;
   orientParticles  = false;

	usePlacementForVelocity = 1;

   particles = FogParticle;

   uiName = "Fog B";
};


datablock ParticleData(FridgeFog1Particle)
{
   textureName          = "base/data/particles/cloud";
   dragCoefficient     = 0.0;
   gravityCoefficient   = 0.5; 
   inheritedVelFactor   = 0.00;
   lifetimeMS           = 3800;
   lifetimeVarianceMS   = 2000;
   useInvAlpha = true;
   spinRandomMin = -5.0;
   spinRandomMax = 5.0;

   colors[0]     = "1.0 1.0 1.0 0.0";
   colors[1]     = "0.9 0.9 1.0 0.1";
   colors[2]     = "1.0 1.0 1.0 0.0";

   sizes[0]      = 2.0;
   sizes[1]      = 3.0;
   sizes[2]      = 3.0;

   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(FridgeFog1Emitter)
{
   ejectionPeriodMS = 50;
   periodVarianceMS = 5;

   ejectionOffset = 0.5;
   ejectionOffsetVariance = 0.5;
	

   ejectionVelocity = 0.25;
   velocityVariance = 0.250;

   thetaMin         = 0.0;
   thetaMax         = 90.0;  

   phiReferenceVel  = 0;
   phiVariance      = 360;

   overrideAdvances = false;
   orientParticles  = false;

	usePlacementForVelocity = 1;

   particles = FridgeFog1Particle;

    uiName = "Fog C";
};
