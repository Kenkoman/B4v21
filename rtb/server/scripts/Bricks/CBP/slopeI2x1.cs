//slopeI2x1.cs


//Item
datablock ItemData(slopeI2x1 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/CBP/slopeI2x1.dts";
	pickUpName = 'a 4x3 slope';
	invName = '2x1 I Slope';
	image = slopeI2x1Image;
};

//Static Shape
datablock StaticShapeData(staticslopeI2x1 : staticBrick2x2)
{
	item = slopeI2x1;
	ghost = ghostslopeI2x1;
	shapeFile = "~/data/shapes/bricks/CBP/slopeI2x1.dts";

	//lego scale dimensions
	x = 2;
	y = 1;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(slopeI2x1Image : brick2x2Image)
{
	staticShape = staticslopeI2x1;
	ghost = ghostslopeI2x1;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/SlopeI2x1.png";
	item = slopeI2x1;
};
