//tile2x2r.cs


//Item
datablock ItemData(tile2x2round : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/tile2x2round.dts";
	pickUpName = 'a 2x2 Round Tile';
	invName = '2x2 TR';
	image = tile2x2roundImage;
};

//Static Shape
datablock StaticShapeData(statictile2x2round : staticBrick2x2)
{
	item = tile2x2round;
	ghost = ghosttile2x2round;
	shapeFile = "~/data/shapes/bricks/tile2x2round.dts";

	//lego scale dimensions
	x = 2;
	y = 2;
	z = 1;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(tile2x2roundImage : brick2x2Image)
{
	staticShape = statictile2x2round;
	ghost = ghosttile2x2round;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/tile2x2round.png";
	item = tile2x2round;
};
