//plate1x6.cs


//Item
datablock ItemData(plate1x6 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/plate1x6.dts";
	pickUpName = 'a 1x6 plate';
	invName = '1x6 F';
	image = plate1x6Image;
};

//Static Shape
datablock StaticShapeData(staticPlate1x6 : staticBrick2x2)
{
	item = plate1x6;
	ghost = ghostPlate1x6;
	shapeFile = "~/data/shapes/bricks/plate1x6.dts";

	//lego scale dimensions
	x = 1;
	y = 6;
	z = 1;	//3 plates = 1 brick
};

//Ghost Shape
datablock StaticShapeData(ghostPlate1x6 : ghostBrick2x2)
{
	item = plate1x6;
	solid = staticPlate1x6;
	shapeFile = "~/data/shapes/bricks/brickghost.dts";
	scale = "1 6 1";
};

//Image
datablock ShapeBaseImageData(plate1x6Image : brick2x2Image)
{
	staticShape = staticPlate1x6;
	ghost = ghostPlate1x6;

	item = plate1x6;
};