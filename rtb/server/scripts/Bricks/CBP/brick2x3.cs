//brick2x3.cs


//Item
datablock ItemData(brick2x3 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/CBP/brick2x3.dts";
	pickUpName = 'a 4x3 slope';
	invName = '2x3';
	image = brick2x3Image;
};

//Static Shape
datablock StaticShapeData(staticbrick2x3 : staticBrick2x2)
{
	item = brick2x3;
	ghost = ghostbrick2x3;
	shapeFile = "~/data/shapes/bricks/CBP/brick2x3.dts";

	//lego scale dimensions
	x = 2;
	y = 3;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};



//Image
datablock ShapeBaseImageData(brick2x3Image : brick2x2Image)
{
	staticShape = staticbrick2x3;
	ghost = ghostbrick2x3;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/Brick2x3.png";
	item = brick2x3;
};
