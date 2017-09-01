//roadx.cs


//Item
datablock ItemData(roadx : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/roadx.dts";
	pickUpName = 'a Cross road';
	invName = 'Cross road';
	image = roadxImage;
	cost = 5;
};

//Static Shape
datablock StaticShapeData(staticroadx : staticBrick2x2)
{
	item = roadx;
	ghost = ghostroadx;
	shapeFile = "~/data/shapes/bricks/roadx.dts";

	//lego scale dimensions
	x = 32;
	y = 32;
	z = 1;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};

//Image
datablock ShapeBaseImageData(roadxImage : brick2x2Image)
{
	staticShape = staticroadx;
	ghost = ghostroadx;
	PreviewFileName = "rtb/data/shapes/bricks/79_4way.png";
	item = roadx;
};