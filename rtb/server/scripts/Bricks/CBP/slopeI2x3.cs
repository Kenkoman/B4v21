//slopeI2x3.cs


//Item
datablock ItemData(slopeI2x3 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/CBP/slopeI2x3.dts";
	pickUpName = 'a 4x3 slope';
	invName = '2x3 I Slope';
	image = slopeI2x3Image;
};

//Static Shape
datablock StaticShapeData(staticslopeI2x3 : staticBrick2x2)
{
	item = slopeI2x3;
	ghost = ghostslopeI2x3;
	shapeFile = "~/data/shapes/bricks/CBP/slopeI2x3.dts";

	//lego scale dimensions
	x = 2;
	y = 3;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(slopeI2x3Image : brick2x2Image)
{
	staticShape = staticslopeI2x3;
	ghost = ghostslopeI2x3;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/SlopeI2x3.png";
	item = slopeI2x3;
};
