//slopeI3x2.cs


//Item
datablock ItemData(slopeI3x2 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/CBP/slopeI3x2.dts";
	pickUpName = 'a 4x3 slope';
	invName = '3x2 I Slope';
	image = slopeI3x2Image;
};

//Static Shape
datablock StaticShapeData(staticslopeI3x2 : staticBrick2x2)
{
	item = slopeI3x2;
	ghost = ghostslopeI3x2;
	shapeFile = "~/data/shapes/bricks/CBP/slopeI3x2.dts";

	//lego scale dimensions
	x = 3;
	y = 2;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(slopeI3x2Image : brick2x2Image)
{
	staticShape = staticslopeI3x2;
	ghost = ghostslopeI3x2;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/SlopeI3x2.png";
	item = slopeI3x2;
};
