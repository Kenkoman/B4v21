datablock fxLightData(YellowBlinkLight)
{
	uiName = "Yellow Blink";

	LightOn = true;
	radius = 10;
	brightness = 9;
	color = "1 1 0 1";

	FlareOn			= true;
	FlareTP			= true;
	Flarebitmap		= "base/lighting/corona";
	FlareColor		= "1 1 1";
	ConstantSizeOn	= false;
	ConstantSize	= 1;
	NearSize		= 3;
	FarSize			= 0.5;
	NearDistance	= 10.0;
	FarDistance		= 30.0;
	FadeTime		= 0.1;
	BlendMode		= 0;

	AnimColor		= true;
	AnimBrightness	= true;
	AnimOffsets		= false;
	AnimRotation	= false;
	LinkFlare		= true;
	LinkFlareSize	= true;
	MinColor		= "1 1 0";
	MaxColor		= "0 0 1";
	MinBrightness	= 0.0;
	MaxBrightness	= 1.0;
	MinRadius		= 0.1;
	MaxRadius		= 20;
	StartOffset		= "-5 0 0";
	EndOffset		= "5 0 0";
	MinRotation		= 0;
	MaxRotation		= 359;

	SingleColorKeys	= true;
	RedKeys			= "AAAAAAA";
	GreenKeys		= "DHSFJYJ";
	BlueKeys		= "ZZZZZZZ";
	
	BrightnessKeys	= "AZQZFZA";
	RadiusKeys		= "AZAAAAA";
	OffsetKeys		= "AZAAAAA";
	RotationKeys	= "AZAAAAA";

	ColorTime		= 1.0;
	BrightnessTime	= 1.0;
	RadiusTime		= 1.0;
	OffsetTime		= 1.0;
	RotationTime	= 1.0;

	LerpColor		= false;
	LerpBrightness	= false;
	LerpRadius		= false;
	LerpOffset		= false;
	LerpRotation	= false;
};

datablock fxLightData(StrobeLight)
{
	uiName = "Strobe";

	LightOn = true;
	radius = 10;
	brightness = 30;
	color = "0.7 1 1 1";

	FlareOn			= true;
	FlareTP			= true;
	Flarebitmap		= "base/lighting/flare";
	FlareColor		= "1 1 1";
	ConstantSizeOn	= false;
	ConstantSize	= 1;
	NearSize		= 1;
	FarSize			= 0.5;
	NearDistance	= 10.0;
	FarDistance		= 30.0;
	FadeTime		= 0.1;
	BlendMode		= 0;

	AnimColor		= false;
	AnimBrightness	= true;
	AnimOffsets		= false;
	AnimRotation	= false;
	LinkFlare		= true;
	LinkFlareSize	= true;
	MinColor		= "1 1 0";
	MaxColor		= "0 0 1";
	MinBrightness	= 0.0;
	MaxBrightness	= 1.0;
	MinRadius		= 0.1;
	MaxRadius		= 20;
	StartOffset		= "-5 0 0";
	EndOffset		= "5 0 0";
	MinRotation		= 0;
	MaxRotation		= 359;

	SingleColorKeys	= false;
	RedKeys			= "AAAAAAA";
	GreenKeys		= "DHSFJYJ";
	BlueKeys		= "ZZZZZZZ";
	
	BrightnessKeys	= "AZAZAZA";
	RadiusKeys		= "AZAAAAA";
	OffsetKeys		= "AZAAAAA";
	RotationKeys	= "AZAAAAA";

	ColorTime		= 0.50;
	BrightnessTime	= 0.50;
	RadiusTime		= 0.50;
	OffsetTime		= 0.50;
	RotationTime	= 0.50;

	LerpColor		= false;
	LerpBrightness	= false;
	LerpRadius		= false;
	LerpOffset		= false;
	LerpRotation	= false;
};

datablock fxLightData(RGBLight)
{
	uiName = "RGB";

	LightOn = true;
	radius = 10;
	brightness = 9;
	color = "1 1 1";

	FlareOn			= true;
	FlareTP			= true;
	Flarebitmap		= "base/lighting/corona";
	FlareColor		= "1 1 1";
	ConstantSizeOn	= false;
	ConstantSize	= 1;
	NearSize		= 1;
	FarSize			= 0.5;
	NearDistance	= 10.0;
	FarDistance		= 30.0;
	FadeTime		= 0.1;
	BlendMode		= 0;

	AnimColor		= true;
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

	SingleColorKeys	= false;
	RedKeys			= "AAZA";
	GreenKeys		= "ZAAA";
	BlueKeys		= "AZAA";
	
	BrightnessKeys	= "AAAAAAA";
	RadiusKeys		= "AZAAAAA";
	OffsetKeys		= "AZAAAAA";
	RotationKeys	= "AZAAAAA";

	ColorTime		= 1.0;
	BrightnessTime	= 1.0;
	RadiusTime		= 1.0;
	OffsetTime		= 1.0;
	RotationTime	= 1.0;

	LerpColor		= false;
	LerpBrightness	= true;
	LerpRadius		= true;
	LerpOffset		= true;
	LerpRotation	= true;
};

datablock fxLightData(AlarmLightA)
{
	uiName = "Alarm";

	LightOn = true;
	radius = 10;
	brightness = 9;
	color = "1 1 1";

	FlareOn			= true;
	FlareTP			= true;
	Flarebitmap		= "base/lighting/corona";
	FlareColor		= "1 1 1";
	ConstantSizeOn	= false;
	ConstantSize	= 1;
	NearSize		= 1;
	FarSize			= 0.5;
	NearDistance	= 10.0;
	FarDistance		= 30.0;
	FadeTime		= 0.1;
	BlendMode		= 0;

	AnimColor		= true;
	AnimBrightness	= false;
	AnimOffsets		= false;
	AnimRotation	= false;
	AnimRadius		= true;
	LinkFlare		= true;
	LinkFlareSize	= true;
	MinColor		= "0 0 0";
	MaxColor		= "1 1 1";
	MinBrightness	= 0.0;
	MaxBrightness	= 1.0;
	MinRadius		= 1;
	MaxRadius		= 10;
	StartOffset		= "-5 0 0";
	EndOffset		= "5 0 0";
	MinRotation		= 0;
	MaxRotation		= 359;

	SingleColorKeys	= false;
	RedKeys			= "FZGZFF";
	GreenKeys		= "AAAAAA";
	BlueKeys		= "AAAAAA";
	
	BrightnessKeys	= "AAAAAA";
	RadiusKeys		= "FZGZFF";
	OffsetKeys		= "AAAAAA";
	RotationKeys	= "AAAAAA";

	ColorTime		= 0.8;
	BrightnessTime	= 1.0;
	RadiusTime		= 0.8;
	OffsetTime		= 1.0;
	RotationTime	= 1.0;

	LerpColor		= true;
	LerpBrightness	= true;
	LerpRadius		= true;
	LerpOffset		= true;
	LerpRotation	= true;
};