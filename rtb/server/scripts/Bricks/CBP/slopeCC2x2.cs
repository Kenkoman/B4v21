//slopeCC2x2.cs


//Item
datablock ItemData(slopeCC2x2 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/CBP/slopeCC2x2.dts";
	pickUpName = 'a 4x3 slope';
	invName = '2x2 CC Slope';
	image = slopeCC2x2Image;
};

//Static Shape
datablock StaticShapeData(staticslopeCC2x2 : staticBrick2x2)
{
	item = slopeCC2x2;
	ghost = ghostslopeCC2x2;
	shapeFile = "~/data/shapes/bricks/CBP/slopeCC2x2.dts";

	//lego scale dimensions
	x = 2;
	y = 2;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};

//Image
datablock ShapeBaseImageData(slopeCC2x2Image : brick2x2Image)
{
	staticShape = staticslopeCC2x2;
	ghost = ghostslopeCC2x2;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/SlopeCC2x2.png";
	item = slopeCC2x2;
};
