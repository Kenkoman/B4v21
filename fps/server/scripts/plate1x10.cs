//plate1x10.cs


//Item
datablock ItemData(plate1x10 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/plate1x10.dts";
	pickUpName = 'a 1x10 plate';
	invName = '1x10 F';
	image = plate1x10Image;
};

//Static Shape
datablock StaticShapeData(staticPlate1x10 : staticBrick2x2)
{
	item = plate1x10;
	ghost = ghostPlate1x10;
	shapeFile = "~/data/shapes/bricks/plate1x10.dts";

	//lego scale dimensions
	x = 1;
	y = 10;
	z = 1;	//3 plates = 1 brick
};

//Ghost Shape
datablock StaticShapeData(ghostPlate1x10 : ghostBrick2x2)
{
	item = plate1x10;
	solid = staticPlate1x10;
	shapeFile = "~/data/shapes/bricks/brickghost.dts";
	scale = "1 10 1";
};

//Image
datablock ShapeBaseImageData(plate1x10Image : brick2x2Image)
{
	staticShape = staticPlate1x10;
	ghost = ghostPlate1x10;

	item = plate1x10;
};