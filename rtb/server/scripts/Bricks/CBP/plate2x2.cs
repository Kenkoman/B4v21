//plate2x2.cs


//Item
datablock ItemData(plate2x2 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/CBP/plate2x2.dts";
	pickUpName = 'a 4x3 slope';
	invName = '2x2 F';
	image = plate2x2Image;
};

//Static Shape
datablock StaticShapeData(staticplate2x2 : staticBrick2x2)
{
	item = plate2x2;
	ghost = ghostplate2x2;
	shapeFile = "~/data/shapes/bricks/CBP/plate2x2.dts";

	//lego scale dimensions
	x = 2;
	y = 2;
	z = 1;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(plate2x2Image : brick2x2Image)
{
	staticShape = staticplate2x2;
	ghost = ghostplate2x2;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/Plate2x2.png";
	item = plate2x2;
};
