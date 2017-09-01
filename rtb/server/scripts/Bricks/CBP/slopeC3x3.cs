//slopeC3x3.cs


//Item
datablock ItemData(slopeC3x3 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/CBP/slopeC3x3.dts";
	pickUpName = 'a 4x3 slope';
	invName = '3x3 C Slope';
	image = slopeC3x3Image;
};

//Static Shape
datablock StaticShapeData(staticslopeC3x3 : staticBrick2x2)
{
	item = slopeC3x3;
	ghost = ghostslopeC3x3;
	shapeFile = "~/data/shapes/bricks/CBP/slopeC3x3.dts";

	//lego scale dimensions
	x = 3;
	y = 3;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(slopeC3x3Image : brick2x2Image)
{
	staticShape = staticslopeC3x3;
	ghost = ghostslopeC3x3;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/SlopeC3x3.png";
	item = slopeC3x3;
};
