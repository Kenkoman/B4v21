//onewaybrick.cs


//Item
datablock ItemData(onewaybrick : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/KBP/onewaybrick.dts";
	pickUpName = 'a one way brick';
	invName = '1x1 oneway';
	image = onewaybrickImage;
	cost = 5;
};

//Static Shape
datablock StaticShapeData(staticbrickoneway : staticBrick2x2)
{
	item = onewaybrick;
	shapeFile = "~/data/shapes/bricks/KBP/onewaybrick.dts";

	//lego scale dimensions
	x = 1;
	y = 1;
	z = 3;	//3 plates = 1 brick
};

//Image
datablock ShapeBaseImageData(onewaybrickImage : brick2x2Image)
{
	staticShape = staticbrickoneway;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/Brick1x1.png";
	item = onewaybrick;
};
