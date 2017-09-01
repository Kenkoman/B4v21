//brick1x3arch.cs


//Item
datablock ItemData(brick1x4walk : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/brick1x4walk.dts";
	pickUpName = 'a 1x4 Walk';
	invName = '1x4 Walk';
	image = brick1x4walkImage;
	cost = 6;
};

//Static Shape
datablock StaticShapeData(staticbrick1x4walk : staticBrick2x2)
{
	item = brick1x4walk;
	shapeFile = "~/data/shapes/bricks/brick1x4walk.dts";

	//lego scale dimensions
	x = 1;
	y = 1;
	z = 1;	//3 plates = 1 brick
};

//Image
datablock ShapeBaseImageData(brick1x4walkImage : brick2x2Image)
{
	staticShape = staticbrick1x4walk;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/brick1x4walk.png";
	item = brick1x4walk;
};
