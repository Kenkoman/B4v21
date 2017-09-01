//brick2x8.cs


//Item
datablock ItemData(brick2x8 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/CBP/brick2x8.dts";
	pickUpName = 'a 4x3 slope';
	invName = '2x8';
	image = brick2x8Image;
};

//Static Shape
datablock StaticShapeData(staticbrick2x8 : staticBrick2x2)
{
	item = brick2x8;
	ghost = ghostbrick2x8;
	shapeFile = "~/data/shapes/bricks/CBP/brick2x8.dts";

	//lego scale dimensions
	x = 2;
	y = 8;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(brick2x8Image : brick2x2Image)
{
	staticShape = staticbrick2x8;
	ghost = ghostbrick2x8;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/Brick2x8.png";
	item = brick2x8;
};
