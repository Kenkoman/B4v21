//fence1x4x2.cs


//Item
datablock ItemData(fence1x4x2 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/CBP/fence1x4x2.dts";
	pickUpName = 'a 4x3 slope';
	invName = '1x4x2 Fence';
	image = fence1x4x2Image;
};

//Static Shape
datablock StaticShapeData(staticfence1x4x2 : staticBrick2x2)
{
	item = fence1x4x2;
	ghost = ghostfence1x4x2;
	shapeFile = "~/data/shapes/bricks/CBP/fence1x4x2.dts";

	//lego scale dimensions
	x = 1;
	y = 4;
	z = 6;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(fence1x4x2Image : brick2x2Image)
{
	staticShape = staticfence1x4x2;
	ghost = ghostfence1x4x2;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/Fence1x4x2.png";
	item = fence1x4x2;
};
