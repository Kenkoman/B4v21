//CastleTurretTop4x8x21third.cs


//Item
datablock ItemData(CastleTurretTop4x8x21third : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/CastleTurretTop4x8x21third.dts";
	pickUpName = 'a straight brick';
	invName = 'Straight Road';
	image = CastleTurretTop4x8x21thirdImage;
	cost = 5;
};

//Static Shape
datablock StaticShapeData(staticbrickCastleTurretTop4x8x21third : staticBrick2x2)
{
	item = CastleTurretTop4x8x21third;
	ghost = ghostCastleTurretTop4x8x21third;
	shapeFile = "~/data/shapes/bricks/CastleTurretTop4x8x21third.dts";

	//lego scale dimensions
	x = 4;
	y = 8;
	z = 7;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};

//Image
datablock ShapeBaseImageData(CastleTurretTop4x8x21thirdImage : brick2x2Image)
{
	staticShape = staticbrickCastleTurretTop4x8x21third;
	ghost = ghostCastleTurretTop4x8x21third;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/castletop.png";
	item = CastleTurretTop4x8x21third;
};

//flag1.cs


//Item
datablock ItemData(flag1 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/flag1.dts";
	pickUpName = 'a 4x3 slope';
	invName = 'flag1';
	image = flag1Image;
};

//Static Shape
datablock StaticShapeData(staticbrickflag1 : staticBrick2x2)
{
	item = flag1;
	ghost = ghostflag1;
	shapeFile = "~/data/shapes/bricks/flag1.dts";

	//lego scale dimensions
	x = 2;
	y = 1;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(flag1Image : brick2x2Image)
{
	staticShape = staticbrickflag1;
	ghost = ghostflag1;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/flag.png";
	item = flag1;
};