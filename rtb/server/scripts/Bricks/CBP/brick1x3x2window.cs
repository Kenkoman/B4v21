//brick1x3x2window.cs


//Item
datablock ItemData(brick1x3x2window : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/CBP/brick1x3x2window.dts";
	pickUpName = 'a 4x3 slope';
	invName = '1x3x2 Win';
	image = brick1x3x2windowImage;
};

//Static Shape
datablock StaticShapeData(staticbrick1x3x2window : staticBrick2x2)
{
	item = brick1x3x2window;
	ghost = ghostbrick1x3x2window;
	shapeFile = "~/data/shapes/bricks/CBP/brick1x3x2window.dts";

	//lego scale dimensions
	x = 1;
	y = 3;
	z = 6;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};



//Image
datablock ShapeBaseImageData(brick1x3x2windowImage : brick2x2Image)
{
	staticShape = staticbrick1x3x2window;
	ghost = ghostbrick1x3x2window;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/Window1x3x2.png";
	item = brick1x3x2window;
};
