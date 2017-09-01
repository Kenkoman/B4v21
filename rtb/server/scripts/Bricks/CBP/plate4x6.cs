//plate4x6.cs


//Item
datablock ItemData(plate4x6 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/CBP/plate4x6.dts";
	pickUpName = 'a 4x3 slope';
	invName = '4x6 F';
	image = plate4x6Image;
};

//Static Shape
datablock StaticShapeData(staticplate4x6 : staticBrick2x2)
{
	item = plate4x6;
	ghost = ghostplate4x6;
	shapeFile = "~/data/shapes/bricks/CBP/plate4x6.dts";

	//lego scale dimensions
	x = 4;
	y = 6;
	z = 1;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(plate4x6Image : brick2x2Image)
{
	staticShape = staticplate4x6;
	ghost = ghostplate4x6;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/Plate4x6.png";
	item = plate4x6;
};
