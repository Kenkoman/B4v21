//slope2x1.cs


//Item
datablock ItemData(slope2x1 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/CBP/slope2x1.dts";
	pickUpName = 'a 4x3 slope';
	invName = '2x1 Slope';
	image = slope2x1Image;
};

//Static Shape
datablock StaticShapeData(staticslope2x1 : staticBrick2x2)
{
	item = slope2x1;
	ghost = ghostslope2x1;
	shapeFile = "~/data/shapes/bricks/CBP/slope2x1.dts";

	//lego scale dimensions
	x = 2;
	y = 1;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(slope2x1Image : brick2x2Image)
{
	staticShape = staticslope2x1;
	ghost = ghostslope2x1;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/Slope2x1.png";
	item = slope2x1;
};
