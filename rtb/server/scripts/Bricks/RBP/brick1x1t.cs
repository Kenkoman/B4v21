//brick1x1t.cs


//Item
datablock ItemData(brick1x1t : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/brick1x1t.dts";
	pickUpName = 'a 1x1t brick';
	invName = '1x1 Trans';
	image = brick1x1tImage;
	cost = 5;
};

//Static Shape
datablock StaticShapeData(staticBrick1x1t : staticBrick2x2)
{
	item = brick1x1t;
	shapeFile = "~/data/shapes/bricks/brick1x1t.dts";

	//lego scale dimensions
	x = 1;
	y = 1;
	z = 3;	//3 plates = 1 brick
};

//Image
datablock ShapeBaseImageData(brick1x1tImage : brick2x2Image)
{
	staticShape = staticBrick1x1t;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/Brick1x1.png";
	item = brick1x1t;
};
