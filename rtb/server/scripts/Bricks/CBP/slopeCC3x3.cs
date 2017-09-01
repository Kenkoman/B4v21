//slopeCC3x3.cs


//Item
datablock ItemData(slopeCC3x3 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/CBP/slopeCC3x3.dts";
	pickUpName = 'a 4x3 slope';
	invName = '3x3 CC Slope';
	image = slopeCC3x3Image;
};

//Static Shape
datablock StaticShapeData(staticslopeCC3x3 : staticBrick2x2)
{
	item = slopeCC3x3;
	ghost = ghostslopeCC3x3;
	shapeFile = "~/data/shapes/bricks/CBP/slopeCC3x3.dts";

	//lego scale dimensions
	x = 3;
	y = 3;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};

//Image
datablock ShapeBaseImageData(slopeCC3x3Image : brick2x2Image)
{
	staticShape = staticslopeCC3x3;
	ghost = ghostslopeCC3x3;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/SlopeCC3x3.png";
	item = slopeCC3x3;
};
