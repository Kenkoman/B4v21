//slopeI3x4.cs


//Item
datablock ItemData(slopeI3x4 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/CBP/slopeI3x4.dts";
	pickUpName = 'a 4x3 slope';
	invName = '3x4 I Slope';
	image = slopeI3x4Image;
};

//Static Shape
datablock StaticShapeData(staticslopeI3x4 : staticBrick2x2)
{
	item = slopeI3x4;
	ghost = ghostslopeI3x4;
	shapeFile = "~/data/shapes/bricks/CBP/slopeI3x4.dts";

	//lego scale dimensions
	x = 3;
	y = 4;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(slopeI3x4Image : brick2x2Image)
{
	staticShape = staticslopeI3x4;
	ghost = ghostslopeI3x4;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/SlopeI3x4.png";
	item = slopeI3x4;
};
