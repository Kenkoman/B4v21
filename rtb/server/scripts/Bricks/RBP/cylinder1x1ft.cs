//cylinder1x1ft.cs


//Item
datablock ItemData(cylinder1x1ft : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/cylinder1x1ft.dts";
	pickUpName = 'a 1x1ft cylinder';
	invName = '1x1ft Cylinder';
	image = cylinder1x1ftImage;
};

//Static Shape
datablock StaticShapeData(staticcylinder1x1ft : staticBrick2x2)
{
	item = cylinder1x1ft;
	shapeFile = "~/data/shapes/bricks/cylinder1x1ft.dts";

	//lego scale dimensions
	x = 1;
	y = 1;
	z = 1;	//3 plates = 1 brick

};

//Image
datablock ShapeBaseImageData(cylinder1x1ftImage : brick2x2Image)
{
	staticShape = staticcylinder1x1ft;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/cylinder1x1ft.png";
	item = cylinder1x1ft;
};