//slopeT2x4.cs


//Item
datablock ItemData(slopeT2x4 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/CBP/slopeT2x4.dts";
	pickUpName = 'a 4x3 slope';
	invName = '2x4 T Slope';
	image = slopeT2x4Image;
};

//Static Shape
datablock StaticShapeData(staticslopeT2x4 : staticBrick2x2)
{
	item = slopeT2x4;
	ghost = ghostslopeT2x4;
	shapeFile = "~/data/shapes/bricks/CBP/slopeT2x4.dts";

	//lego scale dimensions
	x = 2;
	y = 4;
	z = 2;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};



//Image
datablock ShapeBaseImageData(slopeT2x4Image : brick2x2Image)
{
	staticShape = staticslopeT2x4;
	ghost = ghostslopeT2x4;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/SlopeT2x4.png";
	item = slopeT2x4;
};
