//slope3x4.cs


//Item
datablock ItemData(slope3x4 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/CBP/slope3x4.dts";
	pickUpName = 'a 4x3 slope';
	invName = '3x4 Slope';
	image = slope3x4Image;
};

//Static Shape
datablock StaticShapeData(staticslope3x4 : staticBrick2x2)
{
	item = slope3x4;
	ghost = ghostslope3x4;
	shapeFile = "~/data/shapes/bricks/CBP/slope3x4.dts";

	//lego scale dimensions
	x = 3;
	y = 4;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(slope3x4Image : brick2x2Image)
{
	staticShape = staticslope3x4;
	ghost = ghostslope3x4;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/Slope3x4.png";
	item = slope3x4;
};
