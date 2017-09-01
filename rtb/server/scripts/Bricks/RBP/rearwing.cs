//rearwing.cs


//Item
datablock ItemData(rearwing : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/rearwing.dts";
	pickUpName = 'a 6x12 slanted plate';
	invName = '6x12 SF';
	image = rearwingImage;
	cost = 20;
};

//Static Shape
datablock StaticShapeData(staticrearwing : staticBrick2x2)
{
	item = rearwing;
	ghost = ghostrearwing;
	shapeFile = "~/data/shapes/bricks/rearwing.dts";

	//lego scale dimensions
	x = 5;
	y = 2;
	z = 11;	//3 plates = 1 brick
};

//Image
datablock ShapeBaseImageData(rearwingImage : brick2x2Image)
{
	staticShape = staticrearwing;
	ghost = ghostrearwing;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/rearwing.png";
	item = rearwing;
};
