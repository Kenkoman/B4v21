//brick2x2x5Lattice.cs


//Item
datablock ItemData(brick2x2x5Lattice : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/brick2x2x5Lattice.dts";
	pickUpName = 'a 2x2x5Lattice';
	invName = '2x2x5Lattice';
	image = brick2x2x5LatticeImage;
	cost = 7;
};

//Static Shape
datablock StaticShapeData(staticbrick2x2x5Lattice : staticBrick2x2)
{
	item = brick2x2x5Lattice;
	ghost = ghostbrick2x2x5Lattice;
	shapeFile = "~/data/shapes/bricks/brick2x2x5Lattice.dts";

	//lego scale dimensions
	x = 2;
	y = 2;
	z = 15;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(brick2x2x5LatticeImage : brick2x2Image)
{
	staticShape = staticbrick2x2x5Lattice;
	ghost = ghostbrick2x2x5Lattice;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/brick2x2x5Lattice.png";
	item = brick2x2x5Lattice;
};
