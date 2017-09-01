//slopeT2x1.cs


//Item
datablock ItemData(slopeT2x1 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/CBP/slopeT2x1.dts";
	pickUpName = 'a 4x3 slope';
	invName = '2x1 T Slope';
	image = slopeT2x1Image;
};

//Static Shape
datablock StaticShapeData(staticslopeT2x1 : staticBrick2x2)
{
	item = slopeT2x1;
	ghost = ghostslopeT2x1;
	shapeFile = "~/data/shapes/bricks/CBP/slopeT2x1.dts";

	//lego scale dimensions
	x = 2;
	y = 1;
	z = 2;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};



//Image
datablock ShapeBaseImageData(slopeT2x1Image : brick2x2Image)
{
	staticShape = staticslopeT2x1;
	ghost = ghostslopeT2x1;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/SlopeT2x1.png";
	item = slopeT2x1;
};
