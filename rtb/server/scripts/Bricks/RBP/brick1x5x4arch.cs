//brick1x5x4arch.cs


//Item
datablock ItemData(brick1x5x4arch : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/brick1x5x4arch.dts";
	pickUpName = 'a 1x5x4arch';
	invName = '1x5x4arch';
	image = brick1x5x4archImage;
	cost = 7;
};

//Static Shape
datablock StaticShapeData(staticbrick1x5x4arch : staticBrick2x2)
{
	item = brick1x5x4arch;
	ghost = ghostbrick1x5x4arch;
	shapeFile = "~/data/shapes/bricks/brick1x5x4arch.dts";

	//lego scale dimensions
	x = 5;
	y = 1;
	z = 12;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(brick1x5x4archImage : brick2x2Image)
{
	staticShape = staticbrick1x5x4arch;
	ghost = ghostbrick1x5x4arch;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/brick1x5x4arch.png";
	item = brick1x5x4arch;
};
