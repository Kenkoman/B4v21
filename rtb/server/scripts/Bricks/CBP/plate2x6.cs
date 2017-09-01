//plate2x6.cs


//Item
datablock ItemData(plate2x6 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/CBP/plate2x6.dts";
	pickUpName = 'a 4x3 slope';
	invName = '2x6 F';
	image = plate2x6Image;
};

//Static Shape
datablock StaticShapeData(staticplate2x6 : staticBrick2x2)
{
	item = plate2x6;
	ghost = ghostplate2x6;
	shapeFile = "~/data/shapes/bricks/CBP/plate2x6.dts";

	//lego scale dimensions
	x = 2;
	y = 6;
	z = 1;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(plate2x6Image : brick2x2Image)
{
	staticShape = staticplate2x6;
	ghost = ghostplate2x6;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/Plate2x6.png";
	item = plate2x6;
};
