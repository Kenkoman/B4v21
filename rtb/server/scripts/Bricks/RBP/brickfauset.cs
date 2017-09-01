//brickfauset.cs


//Item
datablock ItemData(brickfauset : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/brickfauset.dts";
	pickUpName = 'a fuaset brick';
	invName = 'fuaset';
	image = brickfausetImage;
	cost = 5;
};

//Static Shape
datablock StaticShapeData(staticbrickfauset : staticBrick2x2)
{
	item = brickfauset;
	ghost = ghostbrickfauset;
	shapeFile = "~/data/shapes/bricks/brickfauset.dts";

	//lego scale dimensions
	x = 2;
	y = 1;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};

//Image
datablock ShapeBaseImageData(brickfausetImage : brick2x2Image)
{
	staticShape = staticbrickfauset;
	ghost = ghostbrickfauset;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/brickfauset.png";
	item = brickfauset;
};
