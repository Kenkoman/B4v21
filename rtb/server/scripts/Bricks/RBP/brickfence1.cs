//brickfence1.cs


//Item
datablock ItemData(brickfence1 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/brickfence.dts";
	pickUpName = 'a fence brick';
	invName = 'fence1x8';
	image = brickfence1Image;
	cost = 5;
};

//Static Shape
datablock StaticShapeData(staticbrickfence1 : staticBrick2x2)
{
	item = brickfence1;
	ghost = ghostbrickfence1;
	shapeFile = "~/data/shapes/bricks/brickfence.dts";

	//lego scale dimensions
	x = 1;
	y = 8;
	z = 8;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(brickfence1Image : brick2x2Image)
{
	staticShape = staticbrickfence1;
	ghost = ghostbrickfence1;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/brickfence1.png";
	item = brickfence1;
};
