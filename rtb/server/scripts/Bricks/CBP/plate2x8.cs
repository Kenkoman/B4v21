//plate2x8.cs


//Item
datablock ItemData(plate2x8 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/CBP/plate2x8.dts";
	pickUpName = 'a 4x3 slope';
	invName = '2x8 F';
	image = plate2x8Image;
};

//Static Shape
datablock StaticShapeData(staticplate2x8 : staticBrick2x2)
{
	item = plate2x8;
	ghost = ghostplate2x8;
	shapeFile = "~/data/shapes/bricks/CBP/plate2x8.dts";

	//lego scale dimensions
	x = 2;
	y = 8;
	z = 1;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(plate2x8Image : brick2x2Image)
{
	staticShape = staticplate2x8;
	ghost = ghostplate2x8;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/Plate2x8.png";
	item = plate2x8;
};
