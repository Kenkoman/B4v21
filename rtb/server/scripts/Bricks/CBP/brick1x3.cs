//brick1x3.cs


//Item
datablock ItemData(brick1x3 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/CBP/brick1x3.dts";
	pickUpName = 'a 4x3 slope';
	invName = '1x3';
	image = brick1x3Image;
};

//Static Shape
datablock StaticShapeData(staticbrick1x3 : staticBrick2x2)
{
	item = brick1x3;
	ghost = ghostbrick1x3;
	shapeFile = "~/data/shapes/bricks/CBP/brick1x3.dts";

	//lego scale dimensions
	x = 1;
	y = 3;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(brick1x3Image : brick2x2Image)
{
	staticShape = staticbrick1x3;
	ghost = ghostbrick1x3;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/Brick1x3.png";
	item = brick1x3;
};
