//slopeC2x2.cs


//Item
datablock ItemData(slopeC2x2 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/CBP/slopeC2x2.dts";
	pickUpName = 'a 4x3 slope';
	invName = '2x2 C Slope';
	image = slopeC2x2Image;
};

//Static Shape
datablock StaticShapeData(staticslopeC2x2 : staticBrick2x2)
{
	item = slopeC2x2;
	ghost = ghostslopeC2x2;
	shapeFile = "~/data/shapes/bricks/CBP/slopeC2x2.dts";

	//lego scale dimensions
	x = 2;
	y = 2;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};

//Image
datablock ShapeBaseImageData(slopeC2x2Image : brick2x2Image)
{
	staticShape = staticslopeC2x2;
	ghost = ghostslopeC2x2;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/SlopeC2x2.png";
	item = slopeC2x2;
};
