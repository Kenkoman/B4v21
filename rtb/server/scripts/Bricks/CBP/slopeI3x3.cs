//slopeI3x3.cs


//Item
datablock ItemData(slopeI3x3 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/CBP/slopeI3x3.dts";
	pickUpName = 'a 4x3 slope';
	invName = '3x3 I Slope';
	image = slopeI3x3Image;
};

//Static Shape
datablock StaticShapeData(staticslopeI3x3 : staticBrick2x2)
{
	item = slopeI3x3;
	ghost = ghostslopeI3x3;
	shapeFile = "~/data/shapes/bricks/CBP/slopeI3x3.dts";

	//lego scale dimensions
	x = 3;
	y = 3;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(slopeI3x3Image : brick2x2Image)
{
	staticShape = staticslopeI3x3;
	ghost = ghostslopeI3x3;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/SlopeI3x3.png";
	item = slopeI3x3;
};
