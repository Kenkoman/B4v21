//slope2x4.cs


//Item
datablock ItemData(slope2x4 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/CBP/slope2x4.dts";
	pickUpName = 'a 4x3 slope';
	invName = '2x4 Slope';
	image = slope2x4Image;
};

//Static Shape
datablock StaticShapeData(staticslope2x4 : staticBrick2x2)
{
	item = slope2x4;
	ghost = ghostslope2x4;
	shapeFile = "~/data/shapes/bricks/CBP/slope2x4.dts";

	//lego scale dimensions
	x = 2;
	y = 4;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(slope2x4Image : brick2x2Image)
{
	staticShape = staticslope2x4;
	ghost = ghostslope2x4;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/Slope2x4.png";
	item = slope2x4;
};
