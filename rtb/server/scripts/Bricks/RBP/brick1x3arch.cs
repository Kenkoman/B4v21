//brick1x3arch.cs


//Item
datablock ItemData(brick1x3arch : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/brick1x3arch.dts";
	pickUpName = 'a 1x2 brick w handle';
	invName = '1x2handle';
	image = brick1x3archImage;
	cost = 6;
};

//Static Shape
datablock StaticShapeData(staticbrick1x3arch : staticBrick2x2)
{
	item = brick1x3arch;
	ghost = ghostbrick1x3arch;
	shapeFile = "~/data/shapes/bricks/brick1x3arch.dts";

	//lego scale dimensions
	x = 1;
	y = 2;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(brick1x3archImage : brick2x2Image)
{
	staticShape = staticbrick1x3arch;
	ghost = ghostbrick1x3arch;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/brick1x3arch.png";
	item = brick1x3arch;
};
