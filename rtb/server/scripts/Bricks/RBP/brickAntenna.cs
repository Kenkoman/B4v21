//brickAntenna.cs


//Item
datablock ItemData(brickAntenna : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/brickAntenna.dts";
	pickUpName = 'a Antenna';
	invName = '1x1x4Antenna';
	image = brickAntennaImage;
	cost = 7;
};

//Static Shape
datablock StaticShapeData(staticbrickAntenna : staticBrick2x2)
{
	item = brickAntenna;
	ghost = ghostbrickAntenna;
	shapeFile = "~/data/shapes/bricks/brickAntenna.dts";

	//lego scale dimensions
	x = 1;
	y = 1;
	z = 12;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(brickAntennaImage : brick2x2Image)
{
	staticShape = staticbrickAntenna;
	ghost = ghostbrickAntenna;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/brickAntenna.png";
	item = brickAntenna;
};
