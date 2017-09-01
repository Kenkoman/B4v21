//plate1x1.cs


//Item
datablock ItemData(plate1x1 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/plate1x1.dts";
	pickUpName = 'a 1x1 plate';
	invName = '1x1 F';
	image = plate1x1Image;
	cost = 1;
};

//Static Shape
datablock StaticShapeData(staticPlate1x1 : staticBrick2x2)
{
	item = plate1x1;
	ghost = ghostPlate1x1;
	shapeFile = "~/data/shapes/bricks/plate1x1.dts";

	//lego scale dimensions
	x = 1;
	y = 1;
	z = 1;	//3 plates = 1 brick
};



//Image
datablock ShapeBaseImageData(plate1x1Image : brick2x2Image)
{
	staticShape = staticPlate1x1;
	ghost = ghostPlate1x1;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/Plate1x1.png";
	item = plate1x1;
};
