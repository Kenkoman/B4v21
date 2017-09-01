//DataBlocks

//Sounds

datablock AudioProfile(waterfallSound)
{
   filename    = "~/data/sound/gobbles/waterfall4.wav";
   description = AudioDefaultLooping3d;
   preload =true;
};

datablock AudioProfile(ambientSound)
{
   filename    = "~/data/sound/gobbles/exterior13.wav";
   description = AudioDefaultLooping3d;
   preload = true;
};

//Particles

datablock ParticleData(Waterfall) 
{ 
textureName = "~/data/particles/waterfall"; 
dragCoeffiecient = 0.0; 
gravityCoefficient = 1; 
inheritedVelFactor = 0.00; 
lifetimeMS = 4250; 
lifetimeVarianceMS = 500; 
useInvAlpha = false; 
spinRandomMin = -10.0; 
spinRandomMax = 10.0; 

colors[0] = "0.1 0.2 0.3 0.4"; 
colors[1] = "0.3 0.4 0.5 0.6"; 
colors[2] = "0.5 0.6 0.7 0.8"; 

sizes[0] = 15 / 1.25; 
sizes[1] = 28.5 / 1.25; 
sizes[2] = 42 /1.25; 

times[0] = 0.4; 
times[1] = 2; 
times[2] = 4; 
}; 

datablock ParticleEmitterData(WaterfallEmitter) 
{ 
ejectionPeriodMS = 50; 
periodVarianceMS = 5; 

ejectionVelocity = 1; 
velocityVariance = 0.50; 

thetaMin = 0; 
thetaMax = 50.0; 

particles = Waterfall; 
}; 

datablock ParticleEmitterNodeData(WaterfallEmitterNode) 
{ 
timeMultiple = 1; 
};

datablock ParticleData(MistParticle)
{
    dragCoefficient = 0;
    gravityCoefficient = -1.2;
    windCoefficient = 0;
    inheritedVelFactor = 0;
    constantAcceleration = 0;
    lifetimeMS = 1000;
    lifetimeVarianceMS = 250;
    useInvAlpha = 0;
    spinRandomMin = 0;
    spinRandomMax = 0;
    textureName = "~/data/particles/cloud";
    spinSpeed = 0;
    times[0] = 0;
    times[1] = 0.7;
    times[2] = 1;
    colors[0] = "0.1 0.2 0.3 0.5"; 
    colors[1] = "0.3 0.4 0.5 0.25"; 
    colors[2] = "0.5 0.6 0.7 0."; 
    sizes[0] = 10.451;
    sizes[1] = 12.451;
    sizes[2] = 14.451;
};

datablock ParticleEmitterData(MistEmitter)
{
    ejectionPeriodMS = 20;
    periodVarianceMS = 2;
    ejectionVelocity = 8;
    velocityVariance = 6;
    ejectionOffset = 6;
    thetaMin = 90;
    thetaMax = 90;
    phiReferenceVel = 360;
    phiVariance = 360;
    orientParticles= 0;
    orientOnVelocity = 1;
    particles = "MistParticle";
};
datablock ParticleData(WaterfallEdgeParticle)
{
    dragCoefficient = 2;
    gravityCoefficient = 2.5;
    windCoefficient = 0;
    inheritedVelFactor = 0;
    constantAcceleration = 0;
    lifetimeMS = 1500;
    lifetimeVarianceMS = 1000;
    useInvAlpha = 0;
    spinRandomMin = -30;
    spinRandomMax = 30;
    textureName = "~/data/particles/waterfall";
    spinSpeed = 0;
    times[0] = 0;
    times[1] = 0.576471;
    times[2] = 1;
    colors[0] = "0.1 0.2 0.3 0.2"; 
    colors[1] = "0.3 0.4 0.5 0.3"; 
    colors[2] = "0.5 0.6 0.7 0.4"; 
    sizes[0] = 18;
    sizes[1] = 24;
    sizes[2] = 32;
};

datablock ParticleEmitterData(WaterfallEdgeEmitter)
{
    ejectionPeriodMS = 30;
    periodVarianceMS = 3;
    ejectionVelocity = 6;
    velocityVariance = 1.5;
    ejectionOffset = 4.04;
    thetaMin = 90;
    thetaMax = 90;
    phiReferenceVel = 360;
    phiVariance = 360;
    orientParticles= 0;
    orientOnVelocity = 0;
    particles = "WaterfallEdgeParticle";
};

datablock ParticleData(lavafallParticle) 
{ 
textureName = "~/data/particles/waterfall"; 
dragCoeffiecient = 0.0; 
gravityCoefficient = 1; 
inheritedVelFactor = 0.00; 
lifetimeMS = 4000; 
lifetimeVarianceMS = 500; 
useInvAlpha = false; 
spinRandomMin = -10.0; 
spinRandomMax = 10.0; 

colors[0] = "0.8 0.1 0.0 0.5"; 
colors[1] = "0.9 0.1 0.0 0.5"; 
colors[2] = "0.7 0.1 0.0 0.6"; 

sizes[0] = 50; 
sizes[1] = 50; 
sizes[2] = 50; 

times[0] = 0.4; 
times[1] = 2; 
times[2] = 5; 
}; 

datablock ParticleEmitterData(lavafallEmitter) 
{ 
ejectionPeriodMS = 100; 
periodVarianceMS = 5; 

ejectionVelocity = 2; 
velocityVariance = 0.50; 

thetaMin = 0; 
thetaMax = 50.0; 

particles = lavafallParticle; 
}; 

datablock ParticleEmitterNodeData(lavafallEmitterNode) 
{ 
timeMultiple = 1; 
};

datablock ParticleData(lavafallMistParticle) 
{ 
textureName = "~/data/particles/cloud"; 
dragCoeffiecient = 0.0; 
gravityCoefficient = -1.2; 
inheritedVelFactor = 0.00; 
lifetimeMS = 1000; 
lifetimeVarianceMS = 250; 
useInvAlpha = false; 
spinRandomMin = -10.0; 
spinRandomMax = 10.0; 

colors[0] = "0.8 0.1 0.0 0.5"; 
colors[1] = "0.9 0.1 0.0 0.25"; 
colors[2] = "0.7 0.1 0.0 0.0"; 

sizes[0] = 10.451;
sizes[1] = 12.451;
sizes[2] = 14.451;

times[0] = 0;
times[1] = 0.7;
times[2] = 1;
}; 

datablock ParticleEmitterData(lavafallMistEmitter) 
{ 
ejectionPeriodMS = 20;
periodVarianceMS = 2;
ejectionVelocity = 8;
velocityVariance = 6;
ejectionOffset = 6;
thetaMin = 90;
thetaMax = 90;
phiReferenceVel = 360;
phiVariance = 360;
orientParticles= 0;
orientOnVelocity = 1;
particles = lavafallMistParticle; 
}; 

datablock ParticleEmitterNodeData(lavafallMistEmitterNode) 
{ 
timeMultiple = 1; 
};
