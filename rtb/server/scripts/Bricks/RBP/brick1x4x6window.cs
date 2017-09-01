//brick1x4x6window.cs


//Item
datablock ItemData(brick1x4x6window : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/brick1x4x6window.dts";
	pickUpName = 'a 1x4x6 window';
	invName = '1x4x6 Win';
	image = brick1x4x6windowImage;
	cost = 15;
};

//Static Shape
datablock StaticShapeData(staticbrick1x4x6window : staticBrick2x2)
{
	item = brick1x4x6window;
	ghost = ghostbrick1x4x6window;
	shapeFile = "~/data/shapes/bricks/brick1x4x6window.dts";

	//lego scale dimensions
	x = 1;
	y = 4;
	z = 18;	//15 plates = 5 bricks

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 1.5;
};


//Image
datablock ShapeBaseImageData(brick1x4x6windowImage : brick2x2Image)
{
	staticShape = staticbrick1x4x6window;
	ghost = ghostbrick1x4x6window;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/Window1x4x6.png";
	item = brick1x4x6window;
};
