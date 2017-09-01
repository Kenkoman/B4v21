datablock ParticleData(DShizNukeParticle)
{
    dragCoefficient = 0;
    gravityCoefficient = -5.01831;
    windCoefficient = 0;
    inheritedVelFactor = 0;
    constantAcceleration = 0;
    lifetimeMS = 960;
    lifetimeVarianceMS = 0;
    useInvAlpha = 0;
    spinRandomMin = 0;
    spinRandomMax = 50;
    textureName = "~/data/particles/smoke.png";
    spinSpeed = 0;
    times[0] = 0;
    times[1] = 0.776471;
    times[2] = 1;
    colors[0] = "1.000000 0.141732 0.000000 1.000000";
    colors[1] = "1.000000 0.125984 0.000000 1.000000";
    colors[2] = "1.000000 0.070866 0.000000 1.000000";
    sizes[0] = 24.5804;
    sizes[1] = 4.3185;
    sizes[2] = 50;
};

datablock ParticleEmitterData(DShizNukeEmitter)
{
    ejectionPeriodMS = 7;
    periodVarianceMS = 0;
    ejectionVelocity = 0;
    velocityVariance = 0;
    ejectionOffset = 0;
    thetaMin = 0;
    thetaMax = 90;
    phiReferenceVel = 0;
    phiVariance = 360;
    overrideAdvances = 0;
	   lifetimeMS = 556;
    orientParticles= 0;
    orientOnVelocity = 1;
    particles = "DShizNukeParticle";
};
