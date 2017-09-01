//roadcurve.cs


//Item
datablock ItemData(roadcurve : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/roadcurve.dts";
	pickUpName = 'a straight brick';
	invName = 'Curved Road';
	image = roadcurveImage;
	cost = 5;
};

//Static Shape
datablock StaticShapeData(staticroadcurve : staticBrick2x2)
{
	item = roadcurve;
	ghost = ghostroadcurve;
	shapeFile = "~/data/shapes/bricks/roadcurve.dts";

	//lego scale dimensions
	x = 32;
	y = 32;
	z = 1;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(roadcurveImage : brick2x2Image)
{
	staticShape = staticroadcurve;
	ghost = ghostroadcurve;
	PreviewFileName = "rtb/data/shapes/bricks/79_curve.png";
	item = roadcurve;
};