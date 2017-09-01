//slopeIC2x2.cs


//Item
datablock ItemData(slopeIC2x2 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/CBP/slopeIC2x2.dts";
	pickUpName = 'a 4x3 slope';
	invName = '2x2 IC Slope';
	image = slopeIC2x2Image;
};

//Static Shape
datablock StaticShapeData(staticslopeIC2x2 : staticBrick2x2)
{
	item = slopeIC2x2;
	ghost = ghostslopeIC2x2;
	shapeFile = "~/data/shapes/bricks/CBP/slopeIC2x2.dts";

	//lego scale dimensions
	x = 2;
	y = 2;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(slopeIC2x2Image : brick2x2Image)
{
	staticShape = staticslopeIC2x2;
	ghost = ghostslopeIC2x2;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/SlopeIC2x2.png";
	item = slopeIC2x2;
};
