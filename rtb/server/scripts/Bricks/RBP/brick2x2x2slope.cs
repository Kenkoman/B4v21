//brick2x2x2slope.cs


//Item
datablock ItemData(brick2x2x2slope : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/brick2x2x2slope.dts";
	pickUpName = 'a 2x2x2 slope';
	invName = '2x2x2slope';
	image = brick2x2x2slopeImage;
	cost = 6;
};

//Static Shape
datablock StaticShapeData(staticbrick2x2x2slope : staticBrick2x2)
{
	item = brick2x2x2slope;
	ghost = ghostbrick2x2x2slope;
	shapeFile = "~/data/shapes/bricks/brick2x2x2slope.dts";

	//lego scale dimensions
	x = 2;
	y = 2;
	z = 6;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};

//Image
datablock ShapeBaseImageData(brick2x2x2slopeImage : brick2x2Image)
{
	staticShape = staticbrick2x2x2slope;
	ghost = ghostbrick2x2x2slope;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/brick2x2x2slope.png";
	item = brick2x2x2slope;
};
