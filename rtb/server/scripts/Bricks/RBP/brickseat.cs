//brickseat.cs


//Item
datablock ItemData(brickseat : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/2x2seat.dts";
	pickUpName = 'a seat brick';
	invName = 'seat';
	image = brickseatImage;
	cost = 5;
};

//Static Shape
datablock StaticShapeData(staticbrickseat : staticBrick2x2)
{
	item = brickseat;
	ghost = ghostbrickseat;
	shapeFile = "~/data/shapes/bricks/2x2seat.dts";

	//lego scale dimensions
	x = 2;
	y = 2;
	z = 6;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(brickseatImage : brick2x2Image)
{
	staticShape = staticbrickseat;
	ghost = ghostbrickseat;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/brickseat.png";
	item = brickseat;
};
