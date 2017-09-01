//brick1x6.cs


//Item
datablock ItemData(brick1x6 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/brick1x6.dts";
	pickUpName = 'a 1x6 brick';
	invName = '1x6';
	image = brick1x6Image;
	cost = 8;
};

//Static Shape
datablock StaticShapeData(staticbrick1x6 : staticBrick2x2)
{
	item = brick1x6;
	ghost = ghostbrick1x6;
	shapeFile = "~/data/shapes/bricks/brick1x6.dts";

	//lego scale dimensions
	x = 1;
	y = 6;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(brick1x6Image : brick2x2Image)
{
	staticShape = staticbrick1x6;
	ghost = ghostbrick1x6;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/brick1x6.png";
	item = brick1x6;
};
