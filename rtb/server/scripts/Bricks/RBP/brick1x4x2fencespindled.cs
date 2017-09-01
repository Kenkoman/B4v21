//brick1x4x2fencespindled.cs


//Item
datablock ItemData(brick1x4x2fencespindled : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/brick1x4x2fencespindled.dts";
	pickUpName = 'a 1x4 brick';
	invName = '1x4';
	image = brick1x4x2fencespindledImage;
	cost = 7;
};

//Static Shape
datablock StaticShapeData(staticbrick1x4x2fencespindled : staticBrick2x2)
{
	item = brick1x4x2fencespindled;
	ghost = ghostbrick1x4x2fencespindled;
	shapeFile = "~/data/shapes/bricks/brick1x4x2fencespindled.dts";

	//lego scale dimensions
	x = 1;
	y = 4;
	z = 6;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};

//Image
datablock ShapeBaseImageData(brick1x4x2fencespindledImage : brick2x2Image)
{
	staticShape = staticbrick1x4x2fencespindled;
	ghost = ghostbrick1x4x2fencespindled;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/brick1x4x2fencespindled.png";
	item = brick1x4x2fencespindled;
};
