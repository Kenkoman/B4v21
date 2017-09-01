//brick3x8x6window.cs


//Item
datablock ItemData(brick3x8x6window : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/brick3x8x6window.dts";
	pickUpName = 'a 3x8x6 window';
	invName = '3x8x6window';
	image = brick3x8x6windowImage;
	cost = 6;
};

//Static Shape
datablock StaticShapeData(staticbrick3x8x6window : staticBrick2x2)
{
	item = brick3x8x6window;
	ghost = ghostbrick3x8x6window;
	shapeFile = "~/data/shapes/bricks/brick3x8x6window.dts";

	//lego scale dimensions
	x = 3;
	y = 8;
	z = 18;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(brick3x8x6windowImage : brick2x2Image)
{
	staticShape = staticbrick3x8x6window;
	ghost = ghostbrick3x8x6window;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/brick3x8x6window.png";
	item = brick3x8x6window;
};
