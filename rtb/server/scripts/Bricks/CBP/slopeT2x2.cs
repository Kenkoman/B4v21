//slopeT2x2.cs


//Item
datablock ItemData(slopeT2x2 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/CBP/slopeT2x2.dts";
	pickUpName = 'a 4x3 slope';
	invName = '2x2 T Slope';
	image = slopeT2x2Image;
};

//Static Shape
datablock StaticShapeData(staticslopeT2x2 : staticBrick2x2)
{
	item = slopeT2x2;
	ghost = ghostslopeT2x2;
	shapeFile = "~/data/shapes/bricks/CBP/slopeT2x2.dts";

	//lego scale dimensions
	x = 2;
	y = 2;
	z = 2;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(slopeT2x2Image : brick2x2Image)
{
	staticShape = staticslopeT2x2;
	ghost = ghostslopeT2x2;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/SlopeT2x2.png";
	item = slopeT2x2;
};
