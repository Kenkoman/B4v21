//plate2x10.cs


//Item
datablock ItemData(plate2x10 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/CBP/plate2x10.dts";
	pickUpName = 'a 4x3 slope';
	invName = '2x10 F';
	image = plate2x10Image;
};

//Static Shape
datablock StaticShapeData(staticplate2x10 : staticBrick2x2)
{
	item = plate2x10;
	ghost = ghostplate2x10;
	shapeFile = "~/data/shapes/bricks/CBP/plate2x10.dts";

	//lego scale dimensions
	x = 2;
	y = 10;
	z = 1;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(plate2x10Image : brick2x2Image)
{
	staticShape = staticplate2x10;
	ghost = ghostplate2x10;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/Plate2x10.png";
	item = plate2x10;
};
