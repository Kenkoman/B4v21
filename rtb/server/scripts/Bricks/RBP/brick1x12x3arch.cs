//brick1x12x3arch.cs


//Item
datablock ItemData(brick1x12x3arch : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/brick1x12x3arch.dts";
	pickUpName = 'a 1x12x3arch';
	invName = '1x12x3arch';
	image = brick1x12x3archImage;
	cost = 7;
};

//Static Shape
datablock StaticShapeData(staticbrick1x12x3arch : staticBrick2x2)
{
	item = brick1x12x3arch;
	ghost = ghostbrick1x12x3arch;
	shapeFile = "~/data/shapes/bricks/brick1x12x3arch.dts";

	//lego scale dimensions
	x = 1;
	y = 12;
	z = 9;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};

//Image
datablock ShapeBaseImageData(brick1x12x3archImage : brick2x2Image)
{
	staticShape = staticbrick1x12x3arch;
	ghost = ghostbrick1x12x3arch;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/brick1x12x3arch.png";
	item = brick1x12x3arch;
};
