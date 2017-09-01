//slopeT2x1e.cs


//Item
datablock ItemData(slopeT2x1e : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/CBP/slopeT2x1e.dts";
	pickUpName = 'a 4x3 slope';
	invName = '2x1 TE Slope';
	image = slopeT2x1eImage;
};

//Static Shape
datablock StaticShapeData(staticslopeT2x1e : staticBrick2x2)
{
	item = slopeT2x1e;
	ghost = ghostslopeT2x1e;
	shapeFile = "~/data/shapes/bricks/CBP/slopeT2x1e.dts";

	//lego scale dimensions
	x = 2;
	y = 1;
	z = 2;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};



//Image
datablock ShapeBaseImageData(slopeT2x1eImage : brick2x2Image)
{
	staticShape = staticslopeT2x1e;
	ghost = ghostslopeT2x1e;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/SlopeTE2x1.png";
	item = slopeT2x1e;
};
