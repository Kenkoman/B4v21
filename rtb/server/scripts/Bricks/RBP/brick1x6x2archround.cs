//brick1x3arch.cs


//Item
datablock ItemData(brick1x6x2archround : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/brick1x6x2archround.dts";
	pickUpName = 'a 1x6x2 Arch Round';
	invName = '1x6x2 Arch Round';
	image = brick1x6x2archroundImage;
	cost = 6;
};

//Static Shape
datablock StaticShapeData(staticbrick1x6x2archround : staticBrick2x2)
{
	item = brick1x6x2archround;
	shapeFile = "~/data/shapes/bricks/brick1x6x2archround.dts";

	//lego scale dimensions
	x = 1;
	y = 1;
	z = 1;	//3 plates = 1 brick
};

//Image
datablock ShapeBaseImageData(brick1x6x2archroundImage : brick2x2Image)
{
	staticShape = staticbrick1x6x2archround;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/brick1x6x2archcurved.png";
	item = brick1x6x2archround;
};
