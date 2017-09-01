//brick1x1.cs


//Item
datablock ItemData(brick1x1 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/brick1x1.dts";
        cloaktexture = "~/data/specialfx/cloakTexture";
	pickUpName = 'a 1x1 brick';
	invName = '1x1';
	image = brick1x1Image;
	cost = 5;
};

//Static Shape
datablock StaticShapeData(staticBrick1x1 : staticBrick2x2)
{
	item = brick1x1;
	ghost = ghostBrick1x1;
	shapeFile = "~/data/shapes/bricks/brick1x1.dts";
        cloaktexture = "~/data/specialfx/cloakTexture";

	//lego scale dimensions
	x = 1;
	y = 1;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(brick1x1Image : brick2x2Image)
{
	staticShape = staticBrick1x1;
        cloaktexture = "~/data/specialfx/cloakTexture";
	ghost = ghostBrick1x1;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/Brick1x1.png";
	item = brick1x1;
};
