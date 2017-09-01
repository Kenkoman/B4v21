//brick2x2x3slopeInv.cs


//Item
datablock ItemData(brick2x2x3slopeInv : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/brick2x2x3slopeInv.dts";
	pickUpName = 'a 2x2x2 slope';
	invName = '2x2x2slope';
	image = brick2x2x3slopeInvImage;
	cost = 6;
};

//Static Shape
datablock StaticShapeData(staticbrick2x2x3slopeInv : staticBrick2x2)
{
	item = brick2x2x3slopeInv;
	ghost = ghostbrick2x2x3slopeInv;
	shapeFile = "~/data/shapes/bricks/brick2x2x3slopeInv.dts";

	//lego scale dimensions
	x = 2;
	y = 2;
	z = 9;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};

//Image
datablock ShapeBaseImageData(brick2x2x3slopeInvImage : brick2x2Image)
{
	staticShape = staticbrick2x2x3slopeInv;
	ghost = ghostbrick2x2x3slopeInv;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/brick2x2x3slopeInv.png";
	item = brick2x2x3slopeInv;
};
