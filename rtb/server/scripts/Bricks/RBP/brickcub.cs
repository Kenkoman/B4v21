//brickcub.cs


//Item
datablock ItemData(brickcub : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/brickcub.dts";
	pickUpName = 'a cub brick';
	invName = 'cub';
	image = brickcubImage;
	cost = 5;
};

//Static Shape
datablock StaticShapeData(staticbrickcub : staticBrick2x2)
{
	item = brickcub;
	ghost = ghostbrickcub;
	shapeFile = "~/data/shapes/bricks/brickcub.dts";

	//lego scale dimensions
	x = 4;
	y = 4;
	z = 12;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};

//Image
datablock ShapeBaseImageData(brickcubImage : brick2x2Image)
{
	staticShape = staticbrickcub;
	ghost = ghostbrickcub;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/brickcub.png";
	item = brickcub;
};
