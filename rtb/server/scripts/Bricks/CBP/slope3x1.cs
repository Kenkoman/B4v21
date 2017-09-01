//slope3x1.cs


//Item
datablock ItemData(slope3x1 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/CBP/slope3x1.dts";
	pickUpName = 'a 4x3 slope';
	invName = '3x1 Slope';
	image = slope3x1Image;
};

//Static Shape
datablock StaticShapeData(staticslope3x1 : staticBrick2x2)
{
	item = slope3x1;
	ghost = ghostslope3x1;
	shapeFile = "~/data/shapes/bricks/CBP/slope3x1.dts";

	//lego scale dimensions
	x = 3;
	y = 1;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(slope3x1Image : brick2x2Image)
{
	staticShape = staticslope3x1;
	ghost = ghostslope3x1;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/Slope3x1.png";
	item = slope3x1;
};
