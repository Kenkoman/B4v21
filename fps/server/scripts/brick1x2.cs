//brick1x2.cs


//Item
datablock ItemData(brick1x2 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/brick1x2.dts";
	pickUpName = 'a 1x2 brick';
	invName = '1x2';
	image = brick1x2Image;
};

//Static Shape
datablock StaticShapeData(staticBrick1x2 : staticBrick2x2)
{
	item = brick1x2;
	ghost = ghostBrick1x2;
	shapeFile = "~/data/shapes/bricks/brick1x2.dts";

	//lego scale dimensions
	x = 1;
	y = 2;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};

//Ghost Shape
datablock StaticShapeData(ghostBrick1x2 : ghostBrick2x2)
{
	item = brick1x2;
	solid = staticBrick1x2;
	shapeFile = "~/data/shapes/bricks/brickghost.dts";
	scale = "1 2 3";
};

//Image
datablock ShapeBaseImageData(brick1x2Image : brick2x2Image)
{
	staticShape = staticBrick1x2;
	ghost = ghostBrick1x2;

	item = brick1x2;
};