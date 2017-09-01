//brick2x2FX.cs


//Item
datablock ItemData(brick2x2FX : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/CBP/plate2x2.dts";
	pickUpName = 'a SpecialFX Brick';
	invName = 'FX Brick';
	image = brick2x2FXImage;
};

//Static Shape
datablock StaticShapeData(staticbrick2x2FX : staticBrick2x2)
{
	item = brick2x2FX;
	ghost = ghostbrick2x2FX;
	shapeFile = "~/data/shapes/bricks/CBP/plate2x2.dts";

	//lego scale dimensions
	x = 2;
	y = 2;
	z = 1;	//3 plates = 1 brick
};

//Image
datablock ShapeBaseImageData(brick2x2FXImage : brick2x2Image)
{
	staticShape = staticbrick2x2FX;
	ghost = ghostbrick2x2FX;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/brickFire.png";
	item = brick2x2FX;
};
