//precipitation.cs

//-------------------- SNOW -----------------------------------------------------
datablock PrecipitationData(Snow)
{
	type = 1;
	materialList = "~/data/specialfx/snowflakes.dml";
	sizeX = 0.10;
	sizeY = 0.10;

	movingBoxPer = 0.35;
	divHeightVal = 1.5;
	sizeBigBox = 1;
	topBoxSpeed = 80;
	frontBoxSpeed = 80;
	topBoxDrawPer = 0.5;
	bottomDrawHeight = 40;
	skipIfPer = -0.3;
	bottomSpeedPer = 1.0;
	frontSpeedPer = 1.5;
	frontRadiusPer = 0.5;
};
//-------------------- RAIN -----------------------------------------------------
datablock PrecipitationData(rain)
{
	type = 1;
	materialList = "~/data/specialfx/rain.dml";
	sizeX = 0.4;
	sizeY = 0.4;

	movingBoxPer = 0.35;
	divHeightVal = 1.5;
	sizeBigBox = 1;
	topBoxSpeed = 80;
	frontBoxSpeed = 80;
	topBoxDrawPer = 0.5;
	bottomDrawHeight = 40;
	skipIfPer = -0.3;
	bottomSpeedPer = 1.0;
	frontSpeedPer = 1.5;
	frontRadiusPer = 0.5;
};
