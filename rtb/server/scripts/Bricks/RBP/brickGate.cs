//brickGate.cs


//Item
datablock ItemData(brickGate : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/brickGate.dts";
	pickUpName = 'a Gate';
	invName = 'Gate';
	image = brickGateImage;
	cost = 7;
};

//Static Shape
datablock StaticShapeData(staticbrickGate : staticBrick2x2)
{
	item = brickGate;
	shapeFile = "~/data/shapes/bricks/brickGate.dts";

	//lego scale dimensions
	x = 1;
	y = 1;
	z = 1;	//3 plates = 1 brick
};


//Image
datablock ShapeBaseImageData(brickGateImage : brick2x2Image)
{
	staticShape = staticbrickGate;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/brickGate.png";
	item = brickGate;
};
