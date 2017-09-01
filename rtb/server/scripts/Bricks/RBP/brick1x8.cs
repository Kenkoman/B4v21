//brick1x8.cs


//Item
datablock ItemData(brick1x8 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/brick1x8.dts";
	pickUpName = 'a 1x8 brick';
	invName = '1x8';
	image = brick1x8Image;
	cost = 9;
};

//Static Shape
datablock StaticShapeData(staticbrick1x8 : staticBrick2x2)
{
	item = brick1x8;
	ghost = ghostbrick1x8;
	shapeFile = "~/data/shapes/bricks/brick1x8.dts";

	//lego scale dimensions
	x = 1;
	y = 8;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(brick1x8Image : brick2x2Image)
{
	staticShape = staticbrick1x8;
	ghost = ghostbrick1x8;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/brick1x8.png";
	item = brick1x8;
};
