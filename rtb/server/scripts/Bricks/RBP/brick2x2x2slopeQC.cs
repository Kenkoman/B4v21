//brick2x2x2slopeQC.cs


//Item
datablock ItemData(brick2x2x2slopeQC : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/brick2x2x2slopeQC.dts";
	pickUpName = 'a 2x2x2 slopeQC';
	invName = '2x2x2slopeQC';
	image = brick2x2x2slopeQCImage;
	cost = 6;
};

//Static Shape
datablock StaticShapeData(staticbrick2x2x2slopeQC : staticBrick2x2)
{
	item = brick2x2x2slopeQC;
	ghost = ghostbrick2x2x2slopeQC;
	shapeFile = "~/data/shapes/bricks/brick2x2x2slopeQC.dts";

	//lego scale dimensions
	x = 2;
	y = 2;
	z = 6;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(brick2x2x2slopeQCImage : brick2x2Image)
{
	staticShape = staticbrick2x2x2slopeQC;
	ghost = ghostbrick2x2x2slopeQC;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/brick2x2x2slopeQC.png";
	item = brick2x2x2slopeQC;
};
