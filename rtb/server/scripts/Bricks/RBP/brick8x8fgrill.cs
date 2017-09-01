//brick1x3arch.cs


//Item
datablock ItemData(brick8x8fgrill : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/brick8x8fgrill.dts";
	pickUpName = 'an 8x8f Grill';
	invName = '8x8f Grill';
	image = brick8x8fgrillImage;
	cost = 6;
};

//Static Shape
datablock StaticShapeData(staticbrick8x8fgrill : staticBrick2x2)
{
	item = brick8x8fgrill;
	shapeFile = "~/data/shapes/bricks/brick8x8fgrill.dts";

	//lego scale dimensions
	x = 1;
	y = 1;
	z = 1;	//3 plates = 1 brick
};

//Image
datablock ShapeBaseImageData(brick8x8fgrillImage : brick2x2Image)
{
	staticShape = staticbrick8x8fgrill;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/brick8x8fgrill.png";
	item = brick8x8fgrill;
};
