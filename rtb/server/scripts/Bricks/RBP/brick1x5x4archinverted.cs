//brick1x5x4archinverted.cs


//Item
datablock ItemData(brick1x5x4archinverted : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/brick1x5x4archinverted.dts";
	pickUpName = 'a 1x5x4archinverted';
	invName = '1x5x4archinverted';
	image = brick1x5x4archinvertedImage;
	cost = 7;
};

//Static Shape
datablock StaticShapeData(staticbrick1x5x4archinverted : staticBrick2x2)
{
	item = brick1x5x4archinverted;
	ghost = ghostbrick1x5x4archinverted;
	shapeFile = "~/data/shapes/bricks/brick1x5x4archinverted.dts";

	//lego scale dimensions
	x = 5;
	y = 1;
	z = 12;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(brick1x5x4archinvertedImage : brick2x2Image)
{
	staticShape = staticbrick1x5x4archinverted;
	ghost = ghostbrick1x5x4archinverted;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/brick1x5x4archinverted.png";
	item = brick1x5x4archinverted;
};
