// BAC's Brick Pack
// --------------------

///////////////////////////////////////////////////////////////
//Item pinetree
///////////////////////////////////////////////////////////////
datablock ItemData(brickpinetree : brick2x2)
{
	shapeFile = "~/data/shapes/pinetree.dts";
	pickUpName = 'a tree brick';
	invName = 'Pine Tree';
	image = brickpinetreeImage;
};

//Static Shape
datablock StaticShapeData(staticbrickpinetree : staticBrick2x2)
{
	item = brickpinetree;
	ghost = ghostbrickpinetree;
	shapeFile = "~/data/shapes/pinetree.dts";

	//lego scale dimensions
	x = 4;
	y = 4;
	z = 15;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(brickpinetreeImage : brick2x2Image)
{
	staticShape = staticbrickpinetree;
	ghost = ghostbrickpinetree;
	item = pinetree;
};

///////////////////////////////////////////////////////////////
//Item flower
///////////////////////////////////////////////////////////////
datablock ItemData(brickflower : brick2x2)
{
	shapeFile = "~/data/shapes/flowers.dts";
	pickUpName = 'a flower brick';
	invName = 'Flowers';
	image = brickflowerImage;
};

//Static Shape
datablock StaticShapeData(staticbrickflower : staticBrick2x2)
{
	item = brickflower;
	ghost = ghostbrickflower;
	shapeFile = "~/data/shapes/flowers.dts";

	//lego scale dimensions
	x = 1;
	y = 1;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(brickflowerImage : brick2x2Image)
{
	staticShape = staticbrickflower;
	ghost = ghostbrickflower;
	item = flower;
};

///////////////////////////////////////////////////////////////
//Item cat
///////////////////////////////////////////////////////////////
datablock ItemData(brickcat : brick2x2)
{
	shapeFile = "~/data/shapes/cat.dts";
	pickUpName = 'a cat brick';
	invName = 'A cat';
	image = brickcatImage;
};

//Static Shape
datablock StaticShapeData(staticbrickcat : staticBrick2x2)
{
	item = brickcat;
	ghost = ghostbrickcat;
	shapeFile = "~/data/shapes/cat.dts";

	//lego scale dimensions
	x = 2;
	y = 1;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};

//Image
datablock ShapeBaseImageData(brickcatImage : brick2x2Image)
{
	staticShape = staticbrickcat;
	ghost = ghostbrickcat;
	item = cat;
};

///////////////////////////////////////////////////////////////
//Item monkey
///////////////////////////////////////////////////////////////
datablock ItemData(brickmonkey : brick2x2)
{
	shapeFile = "~/data/shapes/monkeyB.dts";
	pickUpName = 'a monkey brick';
	invName = 'A monkey';
	image = brickmonkeyImage;
};

//Static Shape
datablock StaticShapeData(staticbrickmonkey : staticBrick2x2)
{
	item = brickmonkey;
	ghost = ghostbrickmonkey;
	shapeFile = "~/data/shapes/monkeyB.dts";

	//lego scale dimensions
	x = 2;
	y = 1;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};

//Image
datablock ShapeBaseImageData(brickmonkeyImage : brick2x2Image)
{
	staticShape = staticbrickmonkey;
	ghost = ghostbrickmonkey;
	item = monkey;
};

///////////////////////////////////////////////////////////////
//Item cup
///////////////////////////////////////////////////////////////
datablock ItemData(brickcup : brick2x2)
{
	shapeFile = "~/data/shapes/cup1B.dts";
	pickUpName = 'a cup brick';
	invName = 'Cup';
	image = brickcupImage;
};

//Static Shape
datablock StaticShapeData(staticbrickcup : staticBrick2x2)
{
	item = brickcup;
	ghost = ghostbrickcup;
	shapeFile = "~/data/shapes/cup1B.dts";

	//lego scale dimensions
	x = 1;
	y = 1;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(brickcupImage : brick2x2Image)
{
	staticShape = staticbrickcup;
	ghost = ghostbrickcup;
	item = cup;
};

///////////////////////////////////////////////////////////////
//Item goblet
///////////////////////////////////////////////////////////////
datablock ItemData(brickgoblet : brick2x2)
{
	shapeFile = "~/data/shapes/gobletB.dts";
	pickUpName = 'a goblet brick';
	invName = 'goblet';
	image = brickgobletImage;
};

//Static Shape
datablock StaticShapeData(staticbrickgoblet : staticBrick2x2)
{
	item = brickgoblet;
	ghost = ghostbrickgoblet;
	shapeFile = "~/data/shapes/gobletB.dts";

	//lego scale dimensions
	x = 1;
	y = 1;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};

//Image
datablock ShapeBaseImageData(brickgobletImage : brick2x2Image)
{
	staticShape = staticbrickgoblet;
	ghost = ghostbrickgoblet;
	item = goblet;
};

///////////////////////////////////////////////////////////////
//Item goblet2
///////////////////////////////////////////////////////////////
datablock ItemData(brickgoblet2 : brick2x2)
{
	shapeFile = "~/data/shapes/goblet2B.dts";
	pickUpName = 'a goblet2 brick';
	invName = 'goblet2';
	image = brickgoblet2Image;
};

//Static Shape
datablock StaticShapeData(staticbrickgoblet2 : staticBrick2x2)
{
	item = brickgoblet2;
	ghost = ghostbrickgoblet2;
	shapeFile = "~/data/shapes/goblet2B.dts";

	//lego scale dimensions
	x = 1;
	y = 1;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(brickgoblet2Image : brick2x2Image)
{
	staticShape = staticbrickgoblet2;
	ghost = ghostbrickgoblet2;
	item = goblet2;
};

///////////////////////////////////////////////////////////////
//Item book
///////////////////////////////////////////////////////////////
datablock ItemData(brickbook : brick2x2)
{
	shapeFile = "~/data/shapes/bookB.dts";
	pickUpName = 'a book brick';
	invName = 'A Book';
	image = brickbookImage;
};

//Static Shape
datablock StaticShapeData(staticbrickbook : staticBrick2x2)
{
	item = brickbook;
	ghost = ghostbrickbook;
	shapeFile = "~/data/shapes/bookB.dts";

	//lego scale dimensions
	x = 1;
	y = 1;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(brickbookImage : brick2x2Image)
{
	staticShape = staticbrickbook;
	ghost = ghostbrickbook;
	item = book;
};

///////////////////////////////////////////////////////////////
//Item wand
///////////////////////////////////////////////////////////////
datablock ItemData(brickwand : brick2x2)
{
	shapeFile = "~/data/shapes/wandB.dts";
	pickUpName = 'a wand brick';
	invName = 'A wand';
	image = brickwandImage;
};

//Static Shape
datablock StaticShapeData(staticbrickwand : staticBrick2x2)
{
	item = brickwand;
	ghost = ghostbrickwand;
	shapeFile = "~/data/shapes/wandB.dts";

	//lego scale dimensions
	x = 1;
	y = 1;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(brickwandImage : brick2x2Image)
{
	staticShape = staticbrickwand;
	ghost = ghostbrickwand;
	item = wand;
};

///////////////////////////////////////////////////////////////
//Item drill
///////////////////////////////////////////////////////////////
datablock ItemData(brickdrill : brick2x2)
{
	shapeFile = "~/data/shapes/drillB.dts";
	pickUpName = 'a drill brick';
	invName = 'A drill';
	image = brickdrillImage;
};

//Static Shape
datablock StaticShapeData(staticbrickdrill : staticBrick2x2)
{
	item = brickdrill;
	ghost = ghostbrickdrill;
	shapeFile = "~/data/shapes/drillB.dts";

	//lego scale dimensions
	x = 1;
	y = 1;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(brickdrillImage : brick2x2Image)
{
	staticShape = staticbrickdrill;
	ghost = ghostbrickdrill;
	item = drill;
};

///////////////////////////////////////////////////////////////
//Item screwdriver
///////////////////////////////////////////////////////////////
datablock ItemData(brickscrewdriver : brick2x2)
{
	shapeFile = "~/data/shapes/screwdriverB.dts";
	pickUpName = 'a screwdriver brick';
	invName = 'A screwdriver';
	image = brickscrewdriverImage;
};

//Static Shape
datablock StaticShapeData(staticbrickscrewdriver : staticBrick2x2)
{
	item = brickscrewdriver;
	ghost = ghostbrickscrewdriver;
	shapeFile = "~/data/shapes/screwdriverB.dts";

	//lego scale dimensions
	x = 1;
	y = 1;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(brickscrewdriverImage : brick2x2Image)
{
	staticShape = staticbrickscrewdriver;
	ghost = ghostbrickscrewdriver;
	item = screwdriver;
};

///////////////////////////////////////////////////////////////
//Item hammer
///////////////////////////////////////////////////////////////
datablock ItemData(brickhammer : brick2x2)
{
	shapeFile = "~/data/shapes/hammerB.dts";
	pickUpName = 'a hammer brick';
	invName = 'A hammer';
	image = brickhammerImage;
};

//Static Shape
datablock StaticShapeData(staticbrickhammer : staticBrick2x2)
{
	item = brickhammer;
	ghost = ghostbrickhammer;
	shapeFile = "~/data/shapes/hammerB.dts";

	//lego scale dimensions
	x = 1;
	y = 1;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(brickhammerImage : brick2x2Image)
{
	staticShape = staticbrickhammer;
	ghost = ghostbrickhammer;
	item = hammer;
};

///////////////////////////////////////////////////////////////
//Item wrench
///////////////////////////////////////////////////////////////
datablock ItemData(brickwrench : brick2x2)
{
	shapeFile = "~/data/shapes/wrenchB.dts";
	pickUpName = 'a wrench brick';
	invName = 'A wrench';
	image = brickwrenchImage;
};

//Static Shape
datablock StaticShapeData(staticbrickwrench : staticBrick2x2)
{
	item = brickwrench;
	ghost = ghostbrickwrench;
	shapeFile = "~/data/shapes/wrenchB.dts";

	//lego scale dimensions
	x = 1;
	y = 1;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(brickwrenchImage : brick2x2Image)
{
	staticShape = staticbrickwrench;
	ghost = ghostbrickwrench;
	item = wrench;
};