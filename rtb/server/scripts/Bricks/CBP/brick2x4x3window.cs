//brick2x4x3window.cs


//Item
datablock ItemData(brick2x4x3window : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/CBP/brick2x4x3window.dts";
	pickUpName = 'a 4x3 slope';
	invName = '2x4x3 Win';
	image = brick2x4x3windowImage;
};

//Static Shape
datablock StaticShapeData(staticbrick2x4x3window : staticBrick2x2)
{
	item = brick2x4x3window;
	ghost = ghostbrick2x4x3window;
	shapeFile = "~/data/shapes/bricks/CBP/brick2x4x3window.dts";

	//lego scale dimensions
	x = 2;
	y = 4;
	z = 9;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};

//Image
datablock ShapeBaseImageData(brick2x4x3windowImage : brick2x2Image)
{
	staticShape = staticbrick2x4x3window;
	ghost = ghostbrick2x4x3window;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/Window2x4x3.png";
	item = brick2x4x3window;
};
