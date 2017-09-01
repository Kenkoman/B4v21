//RoadT.cs


//Item
datablock ItemData(RoadT : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/RoadT.dts";
	pickUpName = 'a straight brick';
	invName = 'T Road';
	image = RoadTImage;
	cost = 5;
};

//Static Shape
datablock StaticShapeData(staticRoadT : staticBrick2x2)
{
	item = RoadT;
	ghost = ghostRoadT;
	shapeFile = "~/data/shapes/bricks/RoadT.dts";

	//lego scale dimensions
	x = 32;
	y = 32;
	z = 1;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};

//Image
datablock ShapeBaseImageData(RoadTImage : brick2x2Image)
{
	staticShape = staticRoadT;
	ghost = ghostRoadT;
	PreviewFileName = "rtb/data/shapes/bricks/79_3way.png";
	item = RoadT;
};