//roadstraight.cs


//Item
datablock ItemData(roadstraight : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/roadstraight.dts";
	pickUpName = 'a Cross road';
	invName = 'Straight road';
	image = roadstraightImage;
	cost = 5;
};

//Static Shape
datablock StaticShapeData(staticroadstraight : staticBrick2x2)
{
	item = roadstraight;
	ghost = ghostroadstraight;
	shapeFile = "~/data/shapes/bricks/roadstraight.dts";

	//lego scale dimensions
	x = 32;
	y = 32;
	z = 1;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(roadstraightImage : brick2x2Image)
{
	staticShape = staticroadstraight;
	ghost = ghostroadstraight;
	PreviewFileName = "rtb/data/shapes/bricks/79_straight.png";
	item = roadstraight;
};