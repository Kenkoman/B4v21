//plate2x2.cs
//Plunger & Model by Ephialtes

//Item
datablock ItemData(plunger : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/plunger.dts";
	pickUpName = 'a bomb detonator';
	invName = 'Plunger';
	image = plungerImage;
};

//Static Shape
datablock StaticShapeData(staticplunger : staticBrick2x2)
{
	item = plunger;
	shapeFile = "~/data/shapes/bricks/plunger.dts";

	//lego scale dimensions
	x = 1;
	y = 1;
	z = 1;	//3 plates = 1 brick
};

//Image
datablock ShapeBaseImageData(plungerImage : brick2x2Image)
{
	staticShape = staticplunger;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/plunger.png";
	item = plunger;
};
