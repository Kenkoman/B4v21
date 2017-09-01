//plate1x8.cs


//Item
datablock ItemData(plate1x8 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/plate1x8.dts";
	pickUpName = 'a 1x8 plate';
	invName = '1x8 F';
	image = plate1x8Image;
	cost = 5;
};

//Static Shape
datablock StaticShapeData(staticPlate1x8 : staticBrick2x2)
{
	item = plate1x8;
	ghost = ghostPlate1x8;
	shapeFile = "~/data/shapes/bricks/plate1x8.dts";

	//lego scale dimensions
	x = 1;
	y = 8;
	z = 1;	//3 plates = 1 brick
};


//Image
datablock ShapeBaseImageData(plate1x8Image : brick2x2Image)
{
	staticShape = staticPlate1x8;
	ghost = ghostPlate1x8;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/Plate1x8.png";
	item = plate1x8;
};
