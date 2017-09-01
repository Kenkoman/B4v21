
//-----------------------------------------------------------
//Reference the ParticleEmitterNodeData (i.e. "defaultParticleEmitterNode) name 
//in the dataBlock entry of your mission file
//--timeMultiple controls emission frequency 



datablock ParticleEmitterNodeData(twoxParticleEmitterNode)
{
	timeMultiple = 2.0;   //time multiple must be between 0.01 and 100.0
};



datablock ParticleEmitterNodeData(defaultParticleEmitterNode)
{
	timeMultiple = 1.0;   //time multiple must be between 0.01 and 100.0
};

datablock ParticleEmitterNodeData(halfxParticleEmitterNode)
{
	timeMultiple = 0.5;   //time multiple must be between 0.01 and 100.0
};



datablock ParticleEmitterNodeData(tenthTimeParticleEmitterNode)
{
	timeMultiple = 0.1;
};

/////////////////////////////////////////////////////////////////////
datablock ParticleData( PhatsFireParticle )
{
	textureName = "~/data/shapes/weapons/fire";
	useInvAlpha =  false;

	dragCoeffiecient = 6000.0;

	inheritedVelFactor = 0.5;

	lifetimeMS = 1100;
	
	lifetimeVarianceMS = 300;

	times[0] = 0.0;
	times[1] = 0.6;
	times[2] = 1.0;

	colors[0] = "0.2 0.1 0.0 0.8";
	colors[1] = "0.2 0.055 0.0 0.8";
	colors[2] = "0.0 0.0 0.0 0.0";

	sizes[0] = 4.0;
	sizes[1] = 2.9;
	sizes[2] = 1.0;




   // Dynamic light
   lightStartRadius = 8;
   lightEndRadius = 0;
   lightStartColor = "0 1 0";
   lightEndColor = "0 0 0";


};

datablock ParticleEmitterData( PhatsFire )
{
	particles = "PhatsFireParticle";


	
	ejectionPeriodMS = 10;
	periodVarianceMS = 0;

	ejectionVelocity = 5.0;
	velocityVariance = 3.0;

};
/////////////////////////////////////////////////////////////////////
