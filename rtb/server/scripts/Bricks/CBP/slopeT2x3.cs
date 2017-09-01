//slopeT2x3.cs


//Item
datablock ItemData(slopeT2x3 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/CBP/slopeT2x3.dts";
	pickUpName = 'a 4x3 slope';
	invName = '2x3 T Slope';
	image = slopeT2x3Image;
};

//Static Shape
datablock StaticShapeData(staticslopeT2x3 : staticBrick2x2)
{
	item = slopeT2x3;
	ghost = ghostslopeT2x3;
	shapeFile = "~/data/shapes/bricks/CBP/slopeT2x3.dts";

	//lego scale dimensions
	x = 2;
	y = 3;
	z = 2;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(slopeT2x3Image : brick2x2Image)
{
	staticShape = staticslopeT2x3;
	ghost = ghostslopeT2x3;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/SlopeT2x3.png";
	item = slopeT2x3;
};
