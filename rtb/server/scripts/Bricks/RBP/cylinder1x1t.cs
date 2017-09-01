//cylinder1x1t.cs


//Item
datablock ItemData(cylinder1x1t : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/cylinder1x1t.dts";
	pickUpName = 'a 1x1t Cylinder';
	invName = '1x1t Cylinder';
	image = cylinder1x1tImage;
};

//Static Shape
datablock StaticShapeData(staticcylinder1x1t : staticBrick2x2)
{
	item = cylinder1x1t;
	shapeFile = "~/data/shapes/bricks/cylinder1x1t.dts";

	//lego scale dimensions
	x = 1;
	y = 1;
	z = 3;	//3 plates = 1 brick

};

//Image
datablock ShapeBaseImageData(cylinder1x1tImage : brick2x2Image)
{
	staticShape = staticcylinder1x1t;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/cylinder1x1t.png";
	item = cylinder1x1t;
};