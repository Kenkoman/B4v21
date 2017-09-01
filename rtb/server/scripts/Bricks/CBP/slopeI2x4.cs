//slopeI2x4.cs


//Item
datablock ItemData(slopeI2x4 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/CBP/slopeI2x4.dts";
	pickUpName = 'a 4x3 slope';
	invName = '2x4 I Slope';
	image = slopeI2x4Image;
};

//Static Shape
datablock StaticShapeData(staticslopeI2x4 : staticBrick2x2)
{
	item = slopeI2x4;
	ghost = ghostslopeI2x4;
	shapeFile = "~/data/shapes/bricks/CBP/slopeI2x4.dts";

	//lego scale dimensions
	x = 2;
	y = 4;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(slopeI2x4Image : brick2x2Image)
{
	staticShape = staticslopeI2x4;
	ghost = ghostslopeI2x4;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/SlopeI2x4.png";
	item = slopeI2x4;
};
