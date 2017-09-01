//slopeIC3x3.cs


//Item
datablock ItemData(slopeIC3x3 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/CBP/slopeIC3x3.dts";
	pickUpName = 'a 4x3 slope';
	invName = '3x3 IC Slope';
	image = slopeIC3x3Image;
};

//Static Shape
datablock StaticShapeData(staticslopeIC3x3 : staticBrick2x2)
{
	item = slopeIC3x3;
	ghost = ghostslopeIC3x3;
	shapeFile = "~/data/shapes/bricks/CBP/slopeIC3x3.dts";

	//lego scale dimensions
	x = 3;
	y = 3;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(slopeIC3x3Image : brick2x2Image)
{
	staticShape = staticslopeIC3x3;
	ghost = ghostslopeIC3x3;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/SlopeIC3x3.png";
	item = slopeIC3x3;
};
