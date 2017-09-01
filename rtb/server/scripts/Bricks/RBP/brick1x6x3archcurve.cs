//brick1x3arch.cs


//Item
datablock ItemData(brick1x6x3archcurve : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/brick1x6x3archcurve.dts";
	pickUpName = 'a 1x6x3 Arch Curve';
	invName = '1x6x3 Arch Curve';
	image = brick1x6x3archcurveImage;
	cost = 6;
};

//Static Shape
datablock StaticShapeData(staticbrick1x6x3archcurve : staticBrick2x2)
{
	item = brick1x6x3archcurve;
	shapeFile = "~/data/shapes/bricks/brick1x6x3archcurve.dts";

	//lego scale dimensions
	x = 1;
	y = 1;
	z = 1;	//3 plates = 1 brick
};

//Image
datablock ShapeBaseImageData(brick1x6x3archcurveImage : brick2x2Image)
{
	staticShape = staticbrick1x6x3archcurve;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/brick1x6x3archcurved.png";
	item = brick1x6x3archcurve;
};
