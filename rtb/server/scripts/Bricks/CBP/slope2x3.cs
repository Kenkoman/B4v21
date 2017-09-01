//slope2x3.cs


//Item
datablock ItemData(slope2x3 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/CBP/slope2x3.dts";
	pickUpName = 'a 4x3 slope';
	invName = '2x3 Slope';
	image = slope2x3Image;
};

//Static Shape
datablock StaticShapeData(staticslope2x3 : staticBrick2x2)
{
	item = slope2x3;
	ghost = ghostslope2x3;
	shapeFile = "~/data/shapes/bricks/CBP/slope2x3.dts";

	//lego scale dimensions
	x = 2;
	y = 3;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(slope2x3Image : brick2x2Image)
{
	staticShape = staticslope2x3;
	ghost = ghostslope2x3;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/Slope2x3.png";
	item = slope2x3;
};
