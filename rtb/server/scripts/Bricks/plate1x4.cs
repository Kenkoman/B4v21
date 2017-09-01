//plate1x4.cs


//Item
datablock ItemData(plate1x4 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/plate1x4.dts";
	pickUpName = 'a 1x4 plate';
	invName = '1x4 F';
	image = plate1x4Image;
	cost = 3;
};

//Static Shape
datablock StaticShapeData(staticPlate1x4 : staticBrick2x2)
{
	item = plate1x4;
	ghost = ghostPlate1x4;
	shapeFile = "~/data/shapes/bricks/plate1x4.dts";

	//lego scale dimensions
	x = 1;
	y = 4;
	z = 1;	//3 plates = 1 brick
};


//Image
datablock ShapeBaseImageData(plate1x4Image : brick2x2Image)
{
	staticShape = staticPlate1x4;
	ghost = ghostPlate1x4;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/Plate1x4.png";
	item = plate1x4;
};
