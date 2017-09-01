//baseplate32.cs


//Item
datablock ItemData(baseplate32 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/baseplate32.dts";
	pickUpName = 'a straight brick';
	invName = 'Straight Road';
	image = baseplate32Image;
	cost = 5;
};

//Static Shape
datablock StaticShapeData(staticbaseplate32 : staticBrick2x2)
{
	item = baseplate32;
	ghost = ghostbaseplate32;
	shapeFile = "~/data/shapes/bricks/baseplate32.dts";

	//lego scale dimensions
	x = 32;
	y = 32;
	z = 1;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};

//Image
datablock ShapeBaseImageData(baseplate32Image : brick2x2Image)
{
	staticShape = staticbaseplate32;
	ghost = ghostbaseplate32;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/baseplate.png";
	item = baseplate32;
};