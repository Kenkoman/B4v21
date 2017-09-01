//brick1x4.cs


//Item
datablock ItemData(brick1x4x5window : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/brick1x4x5window.dts";
	pickUpName = 'a 1x4x5 window';
	invName = '1x4x5 Win';
	image = brick1x4x5windowImage;
};

//Static Shape
datablock StaticShapeData(staticBrick1x4x5window : staticBrick2x2)
{
	item = brick1x4x5window;
	ghost = ghostBrick1x4x5window;
	shapeFile = "~/data/shapes/bricks/brick1x4x5window.dts";

	//lego scale dimensions
	x = 1;
	y = 4;
	z = 15;	//15 plates = 5 bricks

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 1.5;
};

//Ghost Shape
datablock StaticShapeData(ghostBrick1x4x5window : ghostBrick2x2)
{
	item = brick1x4x5window;
	solid = staticBrick1x4x5window;
	shapeFile = "~/data/shapes/bricks/brickghost.dts";
	scale = "1 4 15";
};

//Image
datablock ShapeBaseImageData(brick1x4x5windowImage : brick2x2Image)
{
	staticShape = staticBrick1x4x5window;
	ghost = ghostBrick1x4x5window;

	item = brick1x4x5window;
};