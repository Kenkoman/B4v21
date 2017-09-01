//brick1x3arch.cs


//Item
datablock ItemData(brick1x4arch : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/brick1x4arch.dts";
	pickUpName = 'a 1x4 Arch';
	invName = '1x4 Arch';
	image = brick1x4archImage;
	cost = 6;
};

//Static Shape
datablock StaticShapeData(staticbrick1x4arch : staticBrick2x2)
{
	item = brick1x4arch;
	shapeFile = "~/data/shapes/bricks/brick1x4arch.dts";

	//lego scale dimensions
	x = 1;
	y = 1;
	z = 1;	//3 plates = 1 brick
};

//Image
datablock ShapeBaseImageData(brick1x4archImage : brick2x2Image)
{
	staticShape = staticbrick1x4arch;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/brick1x4arch.png";
	item = brick1x4arch;
};
