//plate1x3.cs


//Item
datablock ItemData(plate1x3 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/CBP/plate1x3.dts";
	pickUpName = 'a 4x3 slope';
	invName = '1x3 F';
	image = plate1x3Image;
};

//Static Shape
datablock StaticShapeData(staticplate1x3 : staticBrick2x2)
{
	item = plate1x3;
	ghost = ghostplate1x3;
	shapeFile = "~/data/shapes/bricks/CBP/plate1x3.dts";

	//lego scale dimensions
	x = 1;
	y = 3;
	z = 1;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(plate1x3Image : brick2x2Image)
{
	staticShape = staticplate1x3;
	ghost = ghostplate1x3;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/Plate1x3.png";
	item = plate1x3;
};
