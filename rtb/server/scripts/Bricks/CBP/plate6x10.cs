//plate6x10.cs


//Item
datablock ItemData(plate6x10 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/CBP/plate6x10.dts";
	pickUpName = 'a 4x3 slope';
	invName = '6x10 F';
	image = plate6x10Image;
};

//Static Shape
datablock StaticShapeData(staticplate6x10 : staticBrick2x2)
{
	item = plate6x10;
	ghost = ghostplate6x10;
	shapeFile = "~/data/shapes/bricks/CBP/plate6x10.dts";

	//lego scale dimensions
	x = 6;
	y = 10;
	z = 1;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(plate6x10Image : brick2x2Image)
{
	staticShape = staticplate6x10;
	ghost = ghostplate6x10;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/Plate6x10.png";
	item = plate6x10;
};
