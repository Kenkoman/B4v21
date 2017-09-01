//brick1x2handle.cs


//Item
datablock ItemData(brick1x2handle : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/brick1x2handle.dts";
	pickUpName = 'a 1x2 brick w handle';
	invName = '1x2handle';
	image = brick1x2handleImage;
	cost = 6;
};

//Static Shape
datablock StaticShapeData(staticbrick1x2handle : staticBrick2x2)
{
	item = brick1x2handle;
	ghost = ghostbrick1x2handle;
	shapeFile = "~/data/shapes/bricks/brick1x2handle.dts";

	//lego scale dimensions
	x = 1;
	y = 2;
	z = 4;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(brick1x2handleImage : brick2x2Image)
{
	staticShape = staticbrick1x2handle;
	ghost = ghostbrick1x2handle;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/brick1x2handle.png";
	item = brick1x2handle;
};
