//tile1x1.cs


//Item
datablock ItemData(tile1x1 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/tile1x1.dts";
	pickUpName = 'a 1x1 tile';
	invName = '1x1 T';
	image = tile1x1Image;
	cost = 1;
};

//Static Shape
datablock StaticShapeData(statictile1x1 : staticBrick2x2)
{
	item = tile1x1;
	ghost = ghosttile1x1;
	shapeFile = "~/data/shapes/bricks/tile1x1.dts";

	//lego scale dimensions
	x = 1;
	y = 1;
	z = 1;	//3 plates = 1 brick
};



//Image
datablock ShapeBaseImageData(tile1x1Image : brick2x2Image)
{
	staticShape = statictile1x1;
	ghost = ghosttile1x1;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/tile1x1.png";
	item = tile1x1;
};
