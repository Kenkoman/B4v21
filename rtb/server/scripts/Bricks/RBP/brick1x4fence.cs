//brick1x4fence.cs


//Item
datablock ItemData(brick1x4fence : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/fence1x4.dts";
	pickUpName = 'a 1x4 brick';
	invName = '1x4';
	image = brick1x4fenceImage;
	cost = 7;
};

//Static Shape
datablock StaticShapeData(staticbrick1x4fence : staticBrick2x2)
{
	item = brick1x4fence;
	ghost = ghostbrick1x4fence;
	shapeFile = "~/data/shapes/bricks/fence1x4.dts";

	//lego scale dimensions
	x = 1;
	y = 4;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};

//Image
datablock ShapeBaseImageData(brick1x4fenceImage : brick2x2Image)
{
	staticShape = staticbrick1x4fence;
	ghost = ghostbrick1x4fence;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/brick1x4fence.png";
	item = brick1x4fence;
};
