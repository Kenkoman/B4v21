//brick2x2x3slope.cs


//Item
datablock ItemData(brick2x2x3slope : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/brick2x2x3slope.dts";
	pickUpName = 'a 2x2x3 slope';
	invName = '2x2x3slope';
	image = brick2x2x3slopeImage;
	cost = 6;
};

//Static Shape
datablock StaticShapeData(staticbrick2x2x3slope : staticBrick2x2)
{
	item = brick2x2x3slope;
	ghost = ghostbrick2x2x3slope;
	shapeFile = "~/data/shapes/bricks/brick2x2x3slope.dts";

	//lego scale dimensions
	x = 2;
	y = 2;
	z = 12;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};

//Image
datablock ShapeBaseImageData(brick2x2x3slopeImage : brick2x2Image)
{
	staticShape = staticbrick2x2x3slope;
	ghost = ghostbrick2x2x3slope;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/brick2x2x3slope.png";
	item = brick2x2x3slope;
};
