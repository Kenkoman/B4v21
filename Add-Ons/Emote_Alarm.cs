//Emote_Alarm.cs

datablock AudioProfile(AlarmSound)
{
	filename = "./sound/alarm.wav";
	description = AudioClosest3d;
	preload = true;
};

datablock ParticleData(AlarmParticle)
{
   dragCoefficient      = 5.0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.0;
   windCoefficient      = 0;
   constantAcceleration = 0.0;
   lifetimeMS           = 500;
   lifetimeVarianceMS   = 0;
   useInvAlpha          = false;
   textureName          = "base/data/particles/exclamation";
   colors[0]     = "1 1 1 1";
   colors[1]     = "1 1 1 1";
   colors[2]     = "1 1 1 1";
   sizes[0]      = 0.9;
   sizes[1]      = 0.9;
   sizes[2]      = 0.9;
   times[0]      = 0.0;
   times[1]      = 0.2;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(AlarmEmitter)
{
   ejectionPeriodMS = 35;
   periodVarianceMS = 0;
   ejectionVelocity = 0.0;
   ejectionOffset   = 1.8;
   velocityVariance = 0.0;
   thetaMin         = 0;
   thetaMax         = 0;
   phiReferenceVel  = 0;
   phiVariance      = 0;
   overrideAdvance = false;
   lifeTimeMS = 100;
   particles = "AlarmParticle";

   uiName = "Emote - Alarm";
};

datablock ExplosionData(AlarmExplosion)
{
   lifeTimeMS = 2000;
   emitter[0] = AlarmEmitter;
   soundProfile = AlarmSound;
};

//we cant spawn explosions, so this is a workaround for now
datablock ProjectileData(AlarmProjectile)
{
   explosion           = AlarmExplosion;

   armingDelay         = 0;
   lifetime            = 10;
   explodeOnDeath		= true;
};