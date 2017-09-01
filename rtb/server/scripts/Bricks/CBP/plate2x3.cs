//plate2x3.cs


//Item
datablock ItemData(plate2x3 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/CBP/plate2x3.dts";
	pickUpName = 'a 4x3 slope';
	invName = '2x3 F';
	image = plate2x3Image;
};

//Static Shape
datablock StaticShapeData(staticplate2x3 : staticBrick2x2)
{
	item = plate2x3;
	ghost = ghostplate2x3;
	shapeFile = "~/data/shapes/bricks/CBP/plate2x3.dts";

	//lego scale dimensions
	x = 2;
	y = 3;
	z = 1;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(plate2x3Image : brick2x2Image)
{
	staticShape = staticplate2x3;
	ghost = ghostplate2x3;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/Plate2x3.png";
	item = plate2x3;
};
