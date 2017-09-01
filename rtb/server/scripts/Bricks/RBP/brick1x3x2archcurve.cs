//brick1x3arch.cs


//Item
datablock ItemData(brick1x3x2archcurve : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/brick1x3x2archcurve.dts";
	pickUpName = 'a 1x3x2 Arch Curve';
	invName = '1x3x2 Arch';
	image = brick1x3x2archcurveImage;
	cost = 6;
};

//Static Shape
datablock StaticShapeData(staticbrick1x3x2archcurve : staticBrick2x2)
{
	item = brick1x3x2archcurve;
	shapeFile = "~/data/shapes/bricks/brick1x3x2archcurve.dts";

	//lego scale dimensions
	x = 1;
	y = 1;
	z = 1;	//3 plates = 1 brick
};

//Image
datablock ShapeBaseImageData(brick1x3x2archcurveImage : brick2x2Image)
{
	staticShape = staticbrick1x3x2archcurve;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/brick1x3x2archcurved.png";
	item = brick1x3x2archcurve;
};
