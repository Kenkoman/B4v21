//brick1x1x5.cs


//Item
datablock ItemData(brick1x1x5 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/CBP/brick1x1x5.dts";
	pickUpName = 'a 4x3 slope';
	invName = '1x1x5';
	image = brick1x1x5Image;
};

//Static Shape
datablock StaticShapeData(staticbrick1x1x5 : staticBrick2x2)
{
	item = brick1x1x5;
	ghost = ghostbrick1x1x5;
	shapeFile = "~/data/shapes/bricks/CBP/brick1x1x5.dts";

	//lego scale dimensions
	x = 1;
	y = 1;
	z = 15;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(brick1x1x5Image : brick2x2Image)
{
	staticShape = staticbrick1x1x5;
	ghost = ghostbrick1x1x5;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/Brick1x1x5.png";
	item = brick1x1x5;
};
