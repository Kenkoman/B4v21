//plate2x4.cs


//Item
datablock ItemData(plate2x4 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/plate2x4.dts";
	pickUpName = 'a 2x4 plate';
	invName = '2x4 F';
	image = plate2x4Image;
	cost = 8;
};

//Static Shape
datablock StaticShapeData(staticPlate2x4 : staticBrick2x2)
{
	item = plate2x4;
	ghost = ghostPlate2x4;
	shapeFile = "~/data/shapes/bricks/plate2x4.dts";

	//lego scale dimensions
	x = 2;
	y = 4;
	z = 1;	//3 plates = 1 brick
};

//Image
datablock ShapeBaseImageData(plate2x4Image : brick2x2Image)
{
	staticShape = staticPlate2x4;
	ghost = ghostPlate2x4;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/Plate2x4.png";
	item = plate2x4;
};
