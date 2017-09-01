//plate6x12.cs


//Item
datablock ItemData(plate6x12 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/plate6x12.dts";
	pickUpName = 'a 6x12 plate';
	invName = '6x12 F';
	image = plate6x12Image;
	cost = 20;
};

//Static Shape
datablock StaticShapeData(staticPlate6x12 : staticBrick2x2)
{
	item = plate6x12;
	ghost = ghostPlate6x12;
	shapeFile = "~/data/shapes/bricks/plate6x12.dts";

	//lego scale dimensions
	x = 6;
	y = 12;
	z = 1;	//3 plates = 1 brick
};

//Image
datablock ShapeBaseImageData(plate6x12Image : brick2x2Image)
{
	staticShape = staticPlate6x12;
	ghost = ghostPlate6x12;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/Plate6x12.png";
	item = plate6x12;
};
