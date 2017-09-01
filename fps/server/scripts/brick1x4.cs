//brick1x4.cs


//Item
datablock ItemData(brick1x4 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/brick1x4.dts";
	pickUpName = 'a 1x4 brick';
	invName = '1x4';
	image = brick1x4Image;
};

//Static Shape
datablock StaticShapeData(staticBrick1x4 : staticBrick2x2)
{
	item = brick1x4;
	ghost = ghostBrick1x4;
	shapeFile = "~/data/shapes/bricks/brick1x4.dts";

	//lego scale dimensions
	x = 1;
	y = 4;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};

//Ghost Shape
datablock StaticShapeData(ghostBrick1x4 : ghostBrick2x2)
{
	item = brick1x4;
	solid = staticBrick1x4;
	shapeFile = "~/data/shapes/bricks/brickghost.dts";
	scale = "1 4 3";
};

//Image
datablock ShapeBaseImageData(brick1x4Image : brick2x2Image)
{
	staticShape = staticBrick1x4;
	ghost = ghostBrick1x4;

	item = brick1x4;
};