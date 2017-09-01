//roadstraightxw.cs


//Item
datablock ItemData(roadstraightxw : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/roadstraightxw.dts";
	pickUpName = 'a Cross road';
	invName = 'Straight road w crosswalk';
	image = roadstraightxwImage;
	cost = 5;
};

//Static Shape
datablock StaticShapeData(staticroadstraightxw : staticBrick2x2)
{
	item = roadstraightxw;
	ghost = ghostroadstraightxw;
	shapeFile = "~/data/shapes/bricks/roadstraightxw.dts";

	//lego scale dimensions
	x = 32;
	y = 32;
	z = 1;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(roadstraightxwImage : brick2x2Image)
{
	staticShape = staticroadstraightxw;
	ghost = ghostroadstraightxw;
	PreviewFileName = "rtb/data/shapes/bricks/79_straight_xwalk.png";
	item = roadstraightxw;
};