//plate4x8.cs


//Item
datablock ItemData(plate4x8 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/CBP/plate4x8.dts";
	pickUpName = 'a 4x3 slope';
	invName = '4x8 F';
	image = plate4x8Image;
};

//Static Shape
datablock StaticShapeData(staticplate4x8 : staticBrick2x2)
{
	item = plate4x8;
	ghost = ghostplate4x8;
	shapeFile = "~/data/shapes/bricks/CBP/plate4x8.dts";

	//lego scale dimensions
	x = 4;
	y = 8;
	z = 1;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(plate4x8Image : brick2x2Image)
{
	staticShape = staticplate4x8;
	ghost = ghostplate4x8;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/Plate4x8.png";
	item = plate4x8;
};
