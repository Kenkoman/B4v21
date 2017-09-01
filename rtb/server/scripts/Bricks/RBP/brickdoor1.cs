//brickdoor.cs


//Item
datablock ItemData(brickdoor : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/brickdoor.dts";
	pickUpName = 'a door brick';
	invName = 'door';
	image = brickdoorImage;
	cost = 5;
};

//Static Shape
datablock StaticShapeData(staticbrickdoor : staticBrick2x2)
{
	item = brickdoor;
	ghost = ghostbrickdoor;
	shapeFile = "~/data/shapes/bricks/brickdoor.dts";

	//lego scale dimensions
	x = 1;
	y = 4;
	z = 15;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};

//Image
datablock ShapeBaseImageData(brickdoorImage : brick2x2Image)
{
	staticShape = staticbrickdoor;
	ghost = ghostbrickdoor;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/brickdoor.png";
	item = brickdoor;
};
