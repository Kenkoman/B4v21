//brick2x10.cs


//Item
datablock ItemData(brick2x10 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/CBP/brick2x10.dts";
	pickUpName = 'a 4x3 slope';
	invName = '2x10';
	image = brick2x10Image;
};

//Static Shape
datablock StaticShapeData(staticbrick2x10 : staticBrick2x2)
{
	item = brick2x10;
	ghost = ghostbrick2x10;
	shapeFile = "~/data/shapes/bricks/CBP/brick2x10.dts";

	//lego scale dimensions
	x = 2;
	y = 10;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(brick2x10Image : brick2x2Image)
{
	staticShape = staticbrick2x10;
	ghost = ghostbrick2x10;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/Brick2x10.png";
	item = brick2x10;
};
