// Basic Lights

datablock fxLightData(RedLight)
{
	uiName = "Red Light";

	LightOn = true;
	radius = 10;
	brightness = 9;
	color = "1 0 0 1";

	flareOn = true;
	flarebitmap = "base/lighting/corona";
	NearSize	= 2;
	FarSize = 1;
};

datablock fxLightData(OrangeLight : RedLight)
{
	uiName = "Orange Light";
	color = "1 0.5 0 1";
};
datablock fxLightData(YellowLight : RedLight)
{
	uiName = "Yellow Light";
	color = "1 1 0 1";
};
datablock fxLightData(GreenLight : RedLight)
{
	uiName = "Green Light";
	color = "0 1 0 1";
};
datablock fxLightData(CyanLight : RedLight)
{
	uiName = "Cyan Light";
	color = "0 1 1 1";
};
datablock fxLightData(BlueLight : RedLight)
{	
	uiName = "Blue Light";
	color = "0 0 1 1";
};
datablock fxLightData(PurpleLight : RedLight)
{	
	uiName = "Purple Light";
	color = "0.5 0 1 1";
};

datablock fxLightData(BrightLight)
{
	uiName = "Bright";

	LightOn = true;
	radius = 20;
	brightness = 15;
	color = "0.8 0.9 1 1";

	FlareOn			= true;
	FlareTP			= true;
	Flarebitmap		= "base/lighting/flare";
	FlareColor		= "1 1 1";
	ConstantSizeOn	= false;
	ConstantSize	= 1;
	NearSize		= 1.8;
	FarSize			= 0.9;
	NearDistance	= 10.0;
	FarDistance		= 30.0;
	FadeTime		= 0.1;
	BlendMode		= 0;

	AnimColor		= false;
	AnimBrightness	= false;
	AnimOffsets		= false;
	AnimRotation	= false;
	LinkFlare		= true;
	LinkFlareSize	= false;
	MinColor		= "0 0 0";
	MaxColor		= "1 1 1";
	MinBrightness	= 0.0;
	MaxBrightness	= 1.0;
	MinRadius		= 0.1;
	MaxRadius		= 20;
	StartOffset		= "-5 0 0";
	EndOffset		= "5 0 0";
	MinRotation		= 0;
	MaxRotation		= 359;

	SingleColorKeys	= true;
	RedKeys			= "AZA";
	GreenKeys		= "AZA";
	BlueKeys		= "AZA";
	
	BrightnessKeys	= "AZA";
	RadiusKeys		= "AZA";
	OffsetKeys		= "AZA";
	RotationKeys	= "AZA";

	ColorTime		= 5.0;
	BrightnessTime	= 5.0;
	RadiusTime		= 5.0;
	OffsetTime		= 5.0;
	RotationTime	= 5.0;

	LerpColor		= true;
	LerpBrightness	= true;
	LerpRadius		= true;
	LerpOffset		= true;
	LerpRotation	= true;
};
