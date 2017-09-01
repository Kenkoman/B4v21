//brickbar1.cs


//Item
datablock ItemData(brickbar1 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/brickbar1.dts";
	pickUpName = 'a bar brick';
	invName = 'barbrick';
	image = brickbar1Image;
	cost = 5;
};

//Static Shape
datablock StaticShapeData(staticbrickbar1 : staticBrick2x2)
{
	item = brickbar1;
	ghost = ghostbrickbar1;
	shapeFile = "~/data/shapes/bricks/brickbar1.dts";

	//lego scale dimensions
	x = 1;
	y = 4;
	z = 6;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(brickbar1Image : brick2x2Image)
{
	staticShape = staticbrickbar1;
	ghost = ghostbrickbar1;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/brickbar1.png";
	item = brickbar1;
};
