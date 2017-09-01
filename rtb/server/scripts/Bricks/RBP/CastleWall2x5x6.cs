//CastleWall2x5x6.cs


//Item
datablock ItemData(CastleWall2x5x6 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/CastleWall2x5x6.dts";
	pickUpName = 'a straight brick';
	invName = 'Straight Road';
	image = CastleWall2x5x6Image;
	cost = 5;
};

//Static Shape
datablock StaticShapeData(staticbrickCastleWall2x5x6 : staticBrick2x2)
{
	item = CastleWall2x5x6;
	ghost = ghostCastleWall2x5x6;
	shapeFile = "~/data/shapes/bricks/CastleWall2x5x6.dts";

	//lego scale dimensions
	x = 2;
	y = 5;
	z = 18;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};

//Image
datablock ShapeBaseImageData(CastleWall2x5x6Image : brick2x2Image)
{
	staticShape = staticbrickCastleWall2x5x6;
	ghost = ghostCastleWall2x5x6;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/castlewall.png";
	item = CastleWall2x5x6;
};