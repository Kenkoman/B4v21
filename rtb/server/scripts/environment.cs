//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------



//-----------------------------------------------------------------------------

datablock AudioProfile(HeavyRainSound)
{
   filename    = "~/data/sound/environment/ambient/rain.ogg";
   description = AudioLooping2d;
};

datablock PrecipitationData(HeavyRain)
{
   soundProfile = "HeavyRainSound";

   dropTexture = "~/data/specialfx/rain";
   splashTexture = "~/data/specialfx/water_splash";
   dropSize = 0.75;
   splashSize = 0.2;
   useTrueBillboards = false;
   splashMS = 250;
};

datablock PrecipitationData(HeavyRain2)
{
   dropTexture = "~/data/specialfx/mist";
   splashTexture = "~/data/specialfx/mist2";
   dropSize = 10;
   splashSize = 0.1;
   useTrueBillboards = false;
   splashMS = 250;
};

 //-----------------------------------------------------------------------------

datablock AudioProfile(ThunderCrash1Sound)
{
   filename  = "~/data/sound/environment/ambient/thunder1.ogg";
   description = Audio2d;
};

datablock AudioProfile(ThunderCrash2Sound)
{
   filename  = "~/data/sound/environment/ambient/thunder2.ogg";
   description = Audio2d;
};

datablock AudioProfile(ThunderCrash3Sound)
{
   filename  = "~/data/sound/environment/ambient/thunder3.ogg";
   description = Audio2d;
};

datablock AudioProfile(ThunderCrash4Sound)
{
   filename  = "~/data/sound/environment/ambient/thunder4.ogg";
   description = Audio2d;
};

//datablock AudioProfile(LightningHitSound)
//{
//   filename  = "~/data/sound/crossbow_explosion.ogg";
//   description = AudioLightning3d;
//};

datablock LightningData(LightningStorm)
{
   strikeTextures[0]  = "rtb/data/specialfx/lightning1frame1";
   strikeTextures[1]  = "rtb/data/specialfx/lightning1frame2";
   strikeTextures[2]  = "rtb/data/specialfx/lightning1frame3";
   
   //strikeSound = LightningHitSound;
   thunderSounds[0] = ThunderCrash1Sound;
   thunderSounds[1] = ThunderCrash2Sound;
   thunderSounds[2] = ThunderCrash3Sound;
   thunderSounds[3] = ThunderCrash4Sound;
};

