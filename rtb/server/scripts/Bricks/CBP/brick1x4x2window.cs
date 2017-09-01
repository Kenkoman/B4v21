//brick1x4x2window.cs


//Item
datablock ItemData(brick1x4x2window : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/CBP/brick1x4x2window.dts";
	pickUpName = 'a 4x3 slope';
	invName = '1x4x2 Win';
	image = brick1x4x2windowImage;
};

//Static Shape
datablock StaticShapeData(staticbrick1x4x2window : staticBrick2x2)
{
	item = brick1x4x2window;
	ghost = ghostbrick1x4x2window;
	shapeFile = "~/data/shapes/bricks/CBP/brick1x4x2window.dts";

	//lego scale dimensions
	x = 1;
	y = 4;
	z = 6;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(brick1x4x2windowImage : brick2x2Image)
{
	staticShape = staticbrick1x4x2window;
	ghost = ghostbrick1x4x2window;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/Window1x4x2.png";
	item = brick1x4x2window;
};
