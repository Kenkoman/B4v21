//brick1x3arch.cs


//Item
datablock ItemData(brick1x6arch : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/brick1x6arch.dts";
	pickUpName = 'a 1x6 Arch';
	invName = '1x6 Arch';
	image = brick1x6archImage;
	cost = 6;
};

//Static Shape
datablock StaticShapeData(staticbrick1x6arch : staticBrick2x2)
{
	item = brick1x6arch;
	shapeFile = "~/data/shapes/bricks/brick1x6arch.dts";

	//lego scale dimensions
	x = 1;
	y = 1;
	z = 1;	//3 plates = 1 brick
};

//Image
datablock ShapeBaseImageData(brick1x6archImage : brick2x2Image)
{
	staticShape = staticbrick1x6arch;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/brick1x6arch.png";
	item = brick1x6arch;
};
