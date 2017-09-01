//brick2x8x3arch.cs


//Item
datablock ItemData(brick2x8x3arch : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/brick2x8x3arch.dts";
	pickUpName = 'a 2x8x3arch';
	invName = '2x8x3arch';
	image = brick2x8x3archImage;
	cost = 7;
};

//Static Shape
datablock StaticShapeData(staticbrick2x8x3arch : staticBrick2x2)
{
	item = brick2x8x3arch;
	ghost = ghostbrick2x8x3arch;
	shapeFile = "~/data/shapes/bricks/brick2x8x3arch.dts";

	//lego scale dimensions
	x = 2;
	y = 8;
	z = 9;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};

//Image
datablock ShapeBaseImageData(brick2x8x3archImage : brick2x2Image)
{
	staticShape = staticbrick2x8x3arch;
	ghost = ghostbrick2x8x3arch;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/brick2x8x3arch.png";
	item = brick2x8x3arch;
};
