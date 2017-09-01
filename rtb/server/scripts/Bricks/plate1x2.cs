//plate1x2.cs


//Item
datablock ItemData(plate1x2 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/plate1x2.dts";
	pickUpName = 'a 1x2 plate';
	invName = '1x2 F';
	image = plate1x2Image;
	cost = 2;
};

//Static Shape
datablock StaticShapeData(staticPlate1x2 : staticBrick2x2)
{
	item = plate1x2;
	ghost = ghostPlate1x2;
	shapeFile = "~/data/shapes/bricks/plate1x2.dts";

	//lego scale dimensions
	x = 1;
	y = 2;
	z = 1;	//3 plates = 1 brick
};



//Image
datablock ShapeBaseImageData(plate1x2Image : brick2x2Image)
{
	staticShape = staticPlate1x2;
	ghost = ghostPlate1x2;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/Plate1x2.png";
	item = plate1x2;
};
