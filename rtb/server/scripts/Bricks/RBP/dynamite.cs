//dynamite.cs
//Dynamite by Ephialtes
//Decal by Mocheeze

//Item
datablock ItemData(dynamite : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/dynamitebrick.dts";
	pickUpName = 'a stick of dynamite';
	invName = 'Dynamite';
	dynamite = '1';
	image = dynamiteImage;
	cost = 20;
};

//Static Shape
datablock StaticShapeData(staticdynamite : staticBrick2x2)
{
	item = dynamite;
	ghost = ghostdynamite;
	shapeFile = "~/data/shapes/bricks/dynamitebrick.dts";

	//lego scale dimensions
	x = 1;
	y = 2;
	z = 1;	//3 plates = 1 brick
};

//Image
datablock ShapeBaseImageData(dynamiteImage : brick2x2Image)
{
	staticShape = staticdynamite;
	ghost = ghostdynamite;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/dynamite.png";
	item = dynamite;
};
