//slopeI3x1.cs


//Item
datablock ItemData(slopeI3x1 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/CBP/slopeI3x1.dts";
	pickUpName = 'a 4x3 slope';
	invName = '3x1 I Slope';
	image = slopeI3x1Image;
};

//Static Shape
datablock StaticShapeData(staticslopeI3x1 : staticBrick2x2)
{
	item = slopeI3x1;
	ghost = ghostslopeI3x1;
	shapeFile = "~/data/shapes/bricks/CBP/slopeI3x1.dts";

	//lego scale dimensions
	x = 3;
	y = 1;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(slopeI3x1Image : brick2x2Image)
{
	staticShape = staticslopeI3x1;
	ghost = ghostslopeI3x1;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/SlopeI3x1.png";
	item = slopeI3x1;
};
