datablock ParticleData( BubbleParticle )
{
	textureName        = "~/data/particles/bubble";
	dragCoefficient    = 0.0;
	windCoefficient	   = 0.0;
	gravityCoefficient = -0.1;
	inheritedVelFactor = 0.00;
	useInvAlpha        = false;
	spinRandomMin      = -30.0;
	spinRandomMax      = 30.0;

	lifetimeMS         = 5000;
	lifetimeVarianceMS = 250;

	times[0] = 0.0;
	times[1] = 0.9;
	times[2] = 1.0;

	colors[0] = "0.0 0.0 1 1";
	colors[1] = "0.0 0.0 1 1";
	colors[2] = "0.0 0.0 0.0 0";

	sizes[0] = 0.0;
	sizes[1] = 0.9;
	sizes[2] = 1.0;
};

datablock ParticleEmitterData( BubbleParticleEmitter )
{
	particles = "BubbleParticle";

	ejectionPeriodMS = 50;
	periodVarianceMS = 5;

	ejectionVelocity = 0.25;
	velocityVariance = 0.10;

	thetaMin = 0.0;
	thetaMax = 90.0;
};

datablock ParticleData( FireParticle )
{
	textureName        = "~/data/particles/fire";
	dragCoefficient    = 0.0;
	windCoefficient	   = 0.0;
	gravityCoefficient = -0.3;
	inheritedVelFactor = 0.00;
	useInvAlpha        = false;
	spinRandomMin      = -30.0;
	spinRandomMax      = 30.0;

	lifetimeMS         = 500;
	lifetimeVarianceMS = 250;

	times[0] = 0.0;
	times[1] = 0.5;
	times[2] = 1.0;

	colors[0] = "0.8 0.6 0.0 0.1";
	colors[1] = "0.8 0.6 0.0 0.1";
	colors[2] = "0.0 0.0 0.0 0.0";

	sizes[0] = 1.0;
	sizes[1] = 1.0;
	sizes[2] = 5.0;
};

datablock ParticleEmitterData( FireParticleEmitter )
{
	particles = "FireParticle";

	ejectionPeriodMS = 15;
	periodVarianceMS = 5;

	ejectionVelocity = 0.25;
	velocityVariance = 0.10;

	thetaMin = 0.0;
	thetaMax = 90.0;
};

datablock ParticleData( FireParticle2 )
{
	textureName        = "~/data/particles/fire";
	dragCoefficient    = 0.0;
	windCoefficient	   = 0.0;
	gravityCoefficient = -0.3;
	inheritedVelFactor = 0.00;
	useInvAlpha        = false;
	spinRandomMin      = -30.0;
	spinRandomMax      = 30.0;

	lifetimeMS         = 500;
	lifetimeVarianceMS = 250;

	times[0] = 0.0;
	times[1] = 0.5;
	times[2] = 1.0;

	colors[0] = "0 0 1 0.1";
	colors[1] = "0 0 1 0.1";
	colors[2] = "0 0 1 0.0";

	sizes[0] = 1.0;
	sizes[1] = 1.0;
	sizes[2] = 5.0;
};

datablock ParticleEmitterData( FireParticleEmitter2 )
{
	particles = "FireParticle2";

	ejectionPeriodMS = 15;
	periodVarianceMS = 5;

	ejectionVelocity = 0.25;
	velocityVariance = 0.10;

	thetaMin = 0.0;
	thetaMax = 90.0;
};

datablock ParticleData( FireParticle3 )
{
	textureName        = "~/data/particles/fire";
	dragCoefficient    = 0.0;
	windCoefficient	   = 0.0;
	gravityCoefficient = -0.3;
	inheritedVelFactor = 0.00;
	useInvAlpha        = false;
	spinRandomMin      = -30.0;
	spinRandomMax      = 30.0;

	lifetimeMS         = 500;
	lifetimeVarianceMS = 250;

	times[0] = 0.0;
	times[1] = 0.5;
	times[2] = 1.0;

	colors[0] = "0 1 0 0.1";
	colors[1] = "0 1 0 0.1";
	colors[2] = "0 1 0 0.0";

	sizes[0] = 1.0;
	sizes[1] = 1.0;
	sizes[2] = 5.0;
};

datablock ParticleEmitterData( FireParticleEmitter3 )
{
	particles = "FireParticle3";

	ejectionPeriodMS = 15;
	periodVarianceMS = 5;

	ejectionVelocity = 0.25;
	velocityVariance = 0.10;

	thetaMin = 0.0;
	thetaMax = 90.0;
};

datablock ParticleData( FireParticle4 )
{
	textureName        = "~/data/particles/fire";
	dragCoefficient    = 0.0;
	windCoefficient	   = 0.0;
	gravityCoefficient = -0.3;
	inheritedVelFactor = 0.00;
	useInvAlpha        = false;
	spinRandomMin      = -30.0;
	spinRandomMax      = 30.0;

	lifetimeMS         = 500;
	lifetimeVarianceMS = 250;

	times[0] = 0.0;
	times[1] = 0.5;
	times[2] = 1.0;

	colors[0] = "0.6 0 0.6 0.1";
	colors[1] = "0.6 0 0.6 0.1";
	colors[2] = "0.6 0 0.6 0.0";

	sizes[0] = 1.0;
	sizes[1] = 1.0;
	sizes[2] = 5.0;
};

datablock ParticleEmitterData( FireParticleEmitter4 )
{
	particles = "FireParticle4";

	ejectionPeriodMS = 15;
	periodVarianceMS = 5;

	ejectionVelocity = 0.25;
	velocityVariance = 0.10;

	thetaMin = 0.0;
	thetaMax = 90.0;
};


datablock ParticleData( FireParticle5 )
{
	textureName        = "~/data/particles/fire";
	dragCoefficient    = 0.0;
	windCoefficient	   = 0.0;
	gravityCoefficient = -0.3;
	inheritedVelFactor = 0.00;
	useInvAlpha        = false;
	spinRandomMin      = -30.0;
	spinRandomMax      = 30.0;

	lifetimeMS         = 500;
	lifetimeVarianceMS = 250;

	times[0] = 0.0;
	times[1] = 0.5;
	times[2] = 1.0;

	colors[0] = "1 0 0 0.1";
	colors[1] = "1 0 0 0.1";
	colors[2] = "1 0 0 0.0";

	sizes[0] = 1.0;
	sizes[1] = 1.0;
	sizes[2] = 5.0;
};

datablock ParticleEmitterData( FireParticleEmitter5 )
{
	particles = "FireParticle5";

	ejectionPeriodMS = 15;
	periodVarianceMS = 5;

	ejectionVelocity = 0.25;
	velocityVariance = 0.10;

	thetaMin = 0.0;
	thetaMax = 90.0;
};

datablock ParticleEmitterNodeData( FireParticleEmitterNode )
{
	timeMultiple = 1;
};

datablock ParticleData( SmokeParticle )
{
	textureName        = "~/data/particles/smoke";
	dragCoefficient    = 0.0;
	windCoefficient	   = 0.0;
	gravityCoefficient = -0.2;
	inheritedVelFactor = 0.00;
	useInvAlpha        = false;
	spinRandomMin      = -30.0;
	spinRandomMax      = 30.0;

	lifetimeMS         = 3000;
	lifetimeVarianceMS = 250;

	times[0] = 0.0;
	times[1] = 0.5;
	times[2] = 1.0;

	colors[0] = "0.6 0.6 0.6 0.1";
	colors[1] = "0.6 0.6 0.6 0.1";
	colors[2] = "0.6 0.6 0.6 0.0";

	sizes[0] = 0.5;
	sizes[1] = 0.75;
	sizes[2] = 1.5;
};

datablock ParticleEmitterData( SmokeParticleEmitter )
{
	particles = SmokeParticle;

	ejectionPeriodMS = 20;
	periodVarianceMS = 5;

	ejectionVelocity = 0.25;
	velocityVariance = 0.10;

	thetaMin = 0.0;
	thetaMax = 90.0;
};

datablock ParticleEmitterNodeData( SmokeParticleEmitterNode )
{
	timeMultiple = 1;
};
