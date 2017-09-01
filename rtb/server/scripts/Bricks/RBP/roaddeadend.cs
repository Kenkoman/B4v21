//roaddeadend.cs


//Item
datablock ItemData(roaddeadend : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/roaddeadend.dts";
	pickUpName = 'a Cross road';
	invName = 'dead end road';
	image = roaddeadendImage;
	cost = 5;
};

//Static Shape
datablock StaticShapeData(staticroaddeadend : staticBrick2x2)
{
	item = roaddeadend;
	ghost = ghostroaddeadend;
	shapeFile = "~/data/shapes/bricks/roaddeadend.dts";

	//lego scale dimensions
	x = 32;
	y = 32;
	z = 1;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(roaddeadendImage : brick2x2Image)
{
	staticShape = staticroaddeadend;
	ghost = ghostroaddeadend;
	PreviewFileName = "rtb/data/shapes/bricks/79_end.png";
	item = roaddeadend;
};