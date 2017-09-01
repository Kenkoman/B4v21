//slope1x3round.cs
//Item
datablock ItemData(slope1x3round : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/a_briks/slope1x3round.dts";
	pickUpName = 'a slope 1x3 round';
	invName = "slope1x3round";
	image = slope1x3roundImage;
	cost = 10;
};

//Static Shape
datablock StaticShapeData(staticslope1x3round : staticBrick2x2)
{
	item = slope1x3round;
	ghost = ghostslope1x3round;
	shapeFile = "~/data/shapes/bricks/a_briks/slope1x3round.dts";

	//lego scale dimensions
	x = 1;
	y = 3;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(slope1x3roundImage : brick2x2Image)
{
	staticShape = staticslope1x3round;
	ghost = ghostslope1x3round;
	PreviewFileName = "rtb/data/shapes/bricks/a_briks/slope1x3round.png";
	item = slope1x3round;
};

//brick2x1x1curve.cs
//Item
datablock ItemData(brick2x1x1curve : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/a_briks/brick2x1x1curve.dts";
	pickUpName = 'a brick 2x1x1 curve';
	invName = "brick2x1x1curve";
	image = brick2x1x1curveImage;
	cost = 10;
};

//Static Shape
datablock StaticShapeData(staticbrick2x1x1curve : staticBrick2x2)
{
	item = brick2x1x1curve;
	ghost = ghostbrick2x1x1curve;
	shapeFile = "~/data/shapes/bricks/a_briks/brick2x1x1curve.dts";

	//lego scale dimensions
	x = 1;
	y = 2;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(brick2x1x1curveImage : brick2x2Image)
{
	staticShape = staticbrick2x1x1curve;
	ghost = ghostbrick2x1x1curve;
	PreviewFileName = "rtb/data/shapes/bricks/a_briks/brick2x1x1curve.png";
	item = brick2x1x1curve;
};

//slope1x1.cs
//Item
datablock ItemData(slope1x1 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/a_briks/slope1x1.dts";
	pickUpName = 'a slope 1x1';
	invName = "Slope 1x1";
	image = slope1x1Image;
	cost = 10;
};

//Static Shape
datablock StaticShapeData(staticslope1x1 : staticBrick2x2)
{
	item = slope1x1;
	ghost = ghostslope1x1;
	shapeFile = "~/data/shapes/bricks/a_briks/slope1x1.dts";

	//lego scale dimensions
	x = 1;
	y = 1;
	z = 2;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(slope1x1Image : brick2x2Image)
{
	staticShape = staticslope1x1;
	ghost = ghostslope1x1;
	PreviewFileName = "rtb/data/shapes/bricks/a_briks/slope1x1.png";
	item = slope1x1;
};

//brick1x1handle.cs
//Item
datablock ItemData(brick1x1handle : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/a_briks/brick1x1handle.dts";
	pickUpName = 'a handle brick 1x1';
	invName = "Bar 1x4x2";
	image = brick1x1handleImage;
	cost = 10;
};

//Static Shape
datablock StaticShapeData(staticbrick1x1handle : staticBrick2x2)
{
	item = brick1x1handle;
	ghost = ghostbrick1x1handle;
	shapeFile = "~/data/shapes/bricks/a_briks/brick1x1handle.dts";

	//lego scale dimensions
	x = 1;
	y = 2;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(brick1x1handleImage : brick2x2Image)
{
	staticShape = staticbrick1x1handle;
	ghost = ghostbrick1x1handle;
	PreviewFileName = "rtb/data/shapes/bricks/a_briks/brick1x1handle.png";
	item = brick1x1handle;
};

//bar1x4x2.cs
//Item
datablock ItemData(bar1x4x2 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/a_briks/bar1x4x2.dts";
	pickUpName = 'a Bar 1x4x2';
	invName = "Bar 1x4x2";
	image = bar1x4x2Image;
	cost = 10;
};

//Static Shape
datablock StaticShapeData(staticbrickbar1x4x2 : staticBrick2x2)
{
	item = bar1x4x2;
	ghost = ghostbar1x4x2;
	shapeFile = "~/data/shapes/bricks/a_briks/bar1x4x2.dts";

	//lego scale dimensions
	x = 1;
	y = 4;
	z = 2;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(bar1x4x2Image : brick2x2Image)
{
	staticShape = staticbrickbar1x4x2;
	ghost = ghostbar1x4x2;
	PreviewFileName = "rtb/data/shapes/bricks/a_briks/bar1x4x2.png";
	item = bar1x4x2;
};

//bar1x3.cs
//Item
datablock ItemData(Bar1x3 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/a_briks/bar1x3.dts";
	pickUpName = 'a 4x3 slope';
	invName = "Bar 1x3";
	image = Bar1x3Image;
	cost = 10;
};

//Static Shape
datablock StaticShapeData(staticbrickBar1x3 : staticBrick2x2)
{
	item = Bar1x3;
	ghost = ghostBar1x3;
	shapeFile = "~/data/shapes/bricks/a_briks/Bar1x3.dts";

	//lego scale dimensions
	x = 3;
	y = 1;
	z = 2;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(Bar1x3Image : brick2x2Image)
{
	staticShape = staticbrickBar1x3;
	ghost = ghostBar1x3;
	PreviewFileName = "rtb/data/shapes/bricks/a_briks/bar1x3.png";
	item = Bar1x3;
};
//wing9x4.cs
//Item
datablock ItemData(wing9x4 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/a_briks/wing9x4.dts";
	pickUpName = 'a 4x3 slope';
	invName = "Wing 9x4";
	image = wing9x4Image;
	cost = 10;
};

//Static Shape
datablock StaticShapeData(staticplatewing9x4 : staticBrick2x2)
{
	item = wing9x4;
	ghost = ghostwing9x4;
	shapeFile = "~/data/shapes/bricks/a_briks/wing9x4.dts";

	//lego scale dimensions
	x = 4;
	y = 9;
	z = 1;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(wing9x4Image : brick2x2Image)
{
	staticShape = staticplatewing9x4;
	ghost = ghostwing9x4;
	PreviewFileName = "rtb/data/shapes/bricks/a_briks/wing9x4.png";
	item = wing9x4;
};

//panelwt.cs
//Item
datablock ItemData(panelwt : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/a_briks/domewallt.dts";
	pickUpName = 'a 4x3 slope';
	invName = 'Panelwall t';
	image = panelwtImage;
};

//Static Shape
datablock StaticShapeData(staticbrickpanelwt : staticBrick2x2)
{
	item = panelwt;
	shapeFile = "~/data/shapes/bricks/a_briks/domewallt.dts";

	//lego scale dimensions
	x = 6;
	y = 10;
	z = 33;	//3 plates = 1 brick
};

//Image
datablock ShapeBaseImageData(panelwtImage : brick2x2Image)
{
	staticShape = staticbrickpanelwt;
	PreviewFileName = "rtb/data/shapes/bricks/a_briks/domewallt.png";
	item = panelwt;
};

//plate1x2handles.cs
//Item
datablock ItemData(plate1x2handles : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/a_briks/plate1x2handles.dts";
	pickUpName = 'a 4x3 slope';
	invName = "Plate 1x2 Handles";
	image = plate1x2handlesImage;
	cost = 10;
};

//Static Shape
datablock StaticShapeData(staticplate1x2handles : staticBrick2x2)
{
	item = plate1x2handles;
	ghost = ghostplate1x2handles;
	shapeFile = "~/data/shapes/bricks/a_briks/plate1x2handles.dts";

	//lego scale dimensions
	x = 1;
	y = 2;
	z = 1;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(plate1x2handlesImage : brick2x2Image)
{
	staticShape = staticplate1x2handles;
	ghost = ghostplate1x2handles;
	PreviewFileName = "rtb/data/shapes/bricks/a_briks/plate1x2handles.png";
	item = plate1x2handles;
};

//plate1x2handles.cs
//Item
datablock ItemData(plate1x2handles : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/a_briks/plate1x2handles.dts";
	pickUpName = 'a 4x3 slope';
	invName = "Plate 1x2 Handles";
	image = plate1x2handlesImage;
	cost = 10;
};

//Static Shape
datablock StaticShapeData(staticplate1x2handles : staticBrick2x2)
{
	item = plate1x2handles;
	ghost = ghostplate1x2handles;
	shapeFile = "~/data/shapes/bricks/a_briks/plate1x2handles.dts";

	//lego scale dimensions
	x = 1;
	y = 2;
	z = 1;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(plate1x2handlesImage : brick2x2Image)
{
	staticShape = staticplate1x2handles;
	ghost = ghostplate1x2handles;
	PreviewFileName = "rtb/data/shapes/bricks/a_briks/plate1x2handles.png";
	item = plate1x2handles;
};

//baseplate48
datablock ItemData(baseplate48 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/a_briks/baseplate48.dts";
	pickUpName = 'a 4x3 slope';
	invName = 'baseplate48';
	image = baseplate48Image;
	cost = 7;
};

//Static Shape
datablock StaticShapeData(staticbrickbaseplate48 : staticBrick2x2)
{
	item = baseplate48;
	ghost = ghostbrickbaseplate48;
	shapeFile = "~/data/shapes/bricks/a_briks/baseplate48.dts";

	//lego scale dimensions
	x = 48;
	y = 48;
	z = 1;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(baseplate48Image : brick2x2Image)
{
	staticShape = staticbrickbaseplate48;
	ghost = ghostbrickbaseplate48;
	PreviewFileName = "rtb/data/shapes/bricks/a_briks/baseplate48.png";
	item = baseplate48;
};

//antenna1x1x8
datablock ItemData(antenna1x1x8 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/a_briks/antenna8.dts";
	pickUpName = 'a 4x3 slope';
	invName = 'antenna1x1x8';
	image = antenna1x1x8Image;
	cost = 7;
};

//Static Shape
datablock StaticShapeData(staticbrickantenna1x1x8 : staticBrick2x2)
{
	item = antenna1x1x8;
	ghost = ghostbrickantenna1x1x8;
	shapeFile = "~/data/shapes/bricks/a_briks/antenna8.dts";

	//lego scale dimensions
	x = 1;
	y = 1;
	z = 24;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(antenna1x1x8Image : brick2x2Image)
{
	staticShape = staticbrickantenna1x1x8;
	ghost = ghostbrickantenna1x1x8;
	PreviewFileName = "rtb/data/shapes/bricks/a_briks/antenna8.png";
	item = antenna1x1x8;
};

//plate1x2hook.cs

//Item
datablock ItemData(plate1x2hook : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/a_briks/plate1x2hook.dts";
	pickUpName = 'a 4x3 slope';
	invName = "Plate 1x2 Hook";
	image = plate1x2hookImage;
	cost = 10;
};

//Static Shape
datablock StaticShapeData(staticplate1x2hook : staticBrick2x2)
{
	item = plate1x2hook;
	ghost = ghostplate1x2hook;
	shapeFile = "~/data/shapes/bricks/a_briks/plate1x2hook.dts";

	//lego scale dimensions
	x = 1;
	y = 2;
	z = 1;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(plate1x2hookImage : brick2x2Image)
{
	staticShape = staticplate1x2hook;
	ghost = ghostplate1x2hook;
	PreviewFileName = "rtb/data/shapes/bricks/a_briks/plate1x2hook.png";
	item = plate1x2hook;
};
//brick2x3arch.cs
//Item
datablock ItemData(brick2x3arch : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/a_briks/brick2x3arch.dts";
	pickUpName = 'a 4x3 slope';
	invName = 'brick 2x3 arch';
	image = brick2x3archImage;
};

//Static Shape
datablock StaticShapeData(staticbrick2x3arch : staticBrick2x2)
{
	item = brick2x3arch;
	ghost = ghostbrick2x3arch;
	shapeFile = "~/data/shapes/bricks/a_briks/brick2x3arch.dts";

	//lego scale dimensions
	x = 2;
	y = 3;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(brick2x3archImage : brick2x2Image)
{
	staticShape = staticbrick2x3arch;
	ghost = ghostbrick2x3arch;
	PreviewFileName = "rtb/data/shapes/bricks/a_briks/brick2x3arch.png";
	item = brick2x3arch;
};

//slope2x3x1.cs
//Item
datablock ItemData(slope2x3x1 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/a_briks/slope2x3x1.dts";
	pickUpName = 'a 4x3 slope';
	invName = '2x3x1 Slope r';
	image = slope2x3x1Image;
};

//Static Shape
datablock StaticShapeData(staticslope2x3x1 : staticBrick2x2)
{
	item = slope2x3x1;
	ghost = ghostslope2x3x1;
	shapeFile = "~/data/shapes/bricks/a_briks/slope2x3x1.dts";

	//lego scale dimensions
	x = 2;
	y = 3;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(slope2x3x1Image : brick2x2Image)
{
	staticShape = staticslope2x3x1;
	ghost = ghostslope2x3x1;
	PreviewFileName = "rtb/data/shapes/bricks/a_briks/slope2x3x1.png";
	item = slope2x3x1;
};

//slope2x3x1b.cs
//Item
datablock ItemData(slope2x3x1b : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/a_briks/slope2x3x1b.dts";
	pickUpName = 'a 4x3 slope';
	invName = '2x3x1 Slope l';
	image = slope2x3x1bImage;
};

//Static Shape
datablock StaticShapeData(staticslope2x3x1b : staticBrick2x2)
{
	item = slope2x3x1b;
	ghost = ghostslope2x3x1b;
	shapeFile = "~/data/shapes/bricks/a_briks/slope2x3x1b.dts";

	//lego scale dimensions
	x = 2;
	y = 3;
	z = 3;	//3 plates = 1 brick

	//boxX = -2;
	//boxY = 1;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(slope2x3x1bImage : brick2x2Image)
{
	staticShape = staticslope2x3x1b;
	ghost = ghostslope2x3x1b;
	PreviewFileName = "rtb/data/shapes/bricks/a_briks/slope2x3x1b.png";
	item = slope2x3x1b;
};


//slope6x1x5.cs
//Item
datablock ItemData(slope6x1x5 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/a_briks/slope6x1x5.dts";
	pickUpName = 'a 4x3 slope';
	invName = '6x1x5 Slope';
	image = slope6x1x5Image;
};

//Static Shape
datablock StaticShapeData(staticslope6x1x5 : staticBrick2x2)
{
	item = slope6x1x5;
	ghost = ghostslope6x1x5;
	shapeFile = "~/data/shapes/bricks/a_briks/slope6x1x5.dts";

	//lego scale dimensions
	x = 1;
	y = 6;
	z = 15;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(slope6x1x5Image : brick2x2Image)
{
	staticShape = staticslope6x1x5;
	ghost = ghostslope6x1x5;
	PreviewFileName = "rtb/data/shapes/bricks/a_briks/slope6x1x5.png";

	item = slope6x1x5;
};


//brick4x4qcyl.cs
//Item
datablock ItemData(brick4x4qcyl : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/a_briks/qcyl4x4.dts";
	pickUpName = 'a 3x8x6 window';
	invName = "Cylinder 4x4q";
	image = brick4x4qcylImage;
	cost = 10;
};

//Static Shape
datablock StaticShapeData(staticbrick4x4qcyl : staticBrick2x2)
{
	item = brick4x4qcyl;
	ghost = ghostbrick4x4qcyl;
	shapeFile = "~/data/shapes/bricks/a_briks/qcyl4x4.dts";

	//lego scale dimensions
	x = 4;
	y = 4;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(brick4x4qcylImage : brick2x2Image)
{
	staticShape = staticbrick4x4qcyl;
	ghost = ghostbrick4x4qcyl;
	PreviewFileName = "rtb/data/shapes/bricks/a_briks/qcyl4x4.png";

	item = brick4x4qcyl;
};

//plate1x2s1.cs
//Item
datablock ItemData(plate1x2s1 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/a_briks/plate1x2s.dts";
	pickUpName = 'a 3x8x6 window';
	invName = "plate1x2 s1";
	image = plate1x2s1Image;
	cost = 10;
};

//Static Shape
datablock StaticShapeData(staticplate1x2s1 : staticBrick2x2)
{
	item = plate1x2s1;
	ghost = ghostplate1x2s1;
	shapeFile = "~/data/shapes/bricks/a_briks/plate1x2s.dts";

	//lego scale dimensions
	x = 1;
	y = 2;
	z = 1;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(plate1x2s1Image : brick2x2Image)
{
	staticShape = staticplate1x2s1;
	ghost = ghostplate1x2s1;
	PreviewFileName = "rtb/data/shapes/bricks/a_briks/plate1x2s.png";
	item = plate1x2s1;
};


//plate1x2je.cs
//Item
datablock ItemData(plate1x2je : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/a_briks/jetngn.dts";
	pickUpName = 'a 3x8x6 window';
	invName = "Plate 1x2 je";
	image = plate1x2jeImage;
	cost = 10;
};

//Static Shape
datablock StaticShapeData(staticplate1x2je : staticBrick2x2)
{
	item = plate1x2je;
	ghost = ghostplate1x2je;
	shapeFile = "~/data/shapes/bricks/a_briks/jetngn.dts";

	//lego scale dimensions
	x = 2.5;
	y = 2;
	z = 1;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(plate1x2jeImage : brick2x2Image)
{
	staticShape = staticplate1x2je;
	ghost = ghostplate1x2je;
	PreviewFileName = "rtb/data/shapes/bricks/a_briks/jetngn.png";
	item = plate1x2je;
};
//cone3x3x2.cs
//Item
datablock ItemData(cone3x3x2 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/a_briks/cone3x3x2.dts";
	pickUpName = 'a 3x8x6 window';
	invName = "Cone 3x3x2";
	image = cone3x3x2Image;
	cost = 10;
};

//Static Shape
datablock StaticShapeData(staticcylindercone3x3x2 : staticBrick2x2)
{
	item = cone3x3x2;
	ghost = ghostconecone3x3x2;
	shapeFile = "~/data/shapes/bricks/a_briks/cone3x3x2.dts";

	//lego scale dimensions
	x = 3;
	y = 3;
	z = 6;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(cone3x3x2Image : brick2x2Image)
{
	staticShape = staticcylindercone3x3x2;
	ghost = ghostcone3x3x2;
	PreviewFileName = "rtb/data/shapes/bricks/a_briks/con3x2.png";
	item = cone3x3x2;
};

//cone2x2x2.cs
//Item
datablock ItemData(cone2x2x2 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/a_briks/cone2x2x2.dts";
	pickUpName = 'a 3x8x6 window';
	invName = "Cone 2x2x2";
	image = cone2x2x2Image;
	cost = 10;
};

//Static Shape
datablock StaticShapeData(staticcylindercone2x2x2 : staticBrick2x2)
{
	item = cone2x2x2;
	ghost = ghostcone2x2x2;
	shapeFile = "~/data/shapes/bricks/a_briks/cone2x2x2.dts";

	//lego scale dimensions
	x = 2;
	y = 2;
	z = 6;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(cone2x2x2Image : brick2x2Image)
{
	staticShape = staticcylindercone2x2x2;
	ghost = ghostcone2x2x2;
	PreviewFileName = "rtb/data/shapes/bricks/a_briks/con2x2.png";
	item = cone2x2x2;
};

//cone1x1.cs
//Item
datablock ItemData(rocket1x1 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/a_briks/rocket1x1.dts";
	pickUpName = 'a 3x8x6 window';
	invName = "Cone 1x1";
	image = rocket1x1Image;
	cost = 10;
};

//Static Shape
datablock StaticShapeData(staticcylinder1x1rocket : staticBrick2x2)
{
	item = cone1x1;
	ghost = ghostrocket1x1;
	shapeFile = "~/data/shapes/bricks/a_briks/rocket1x1.dts";

	//lego scale dimensions
	x = 1;
	y = 1;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(rocket1x1Image : brick2x2Image)
{
	staticShape = staticcylinder1x1rocket;
	ghost = ghostrocket1x1;
	PreviewFileName = "rtb/data/shapes/bricks/a_briks/roc1X1.png";
	item = cone1x1;
};

//trans cone1x1.cs
//Item
datablock ItemData(rocket1x1t : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/a_briks/rocket1x1t.dts";
	pickUpName = 'a 3x8x6 window';
	invName = "Cone 1x1 t";
	image = rocket1x1tImage;
	cost = 10;
};

//Static Shape
datablock StaticShapeData(staticcylinder1x1rockett : staticBrick2x2)
{
	item = cone1x1t;
	ghost = ghostrocket1x1t;
	shapeFile = "~/data/shapes/bricks/a_briks/rocket1x1t.dts";

	//lego scale dimensions
	x = 1;
	y = 1;
	z = 3;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(rocket1x1tImage : brick2x2Image)
{
	staticShape = staticcylinder1x1rockett;
	ghost = ghostrocket1x1t;
	PreviewFileName = "rtb/data/shapes/bricks/a_briks/roc1x1t.png";
	item = cone1x1t;
};

//trans antenna
datablock ItemData(brickAntennat : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/a_briks/antennat.dts";
	pickUpName = 'a transparent Antenna';
	invName = '1x1x4Antennat';
	image = brickAntennatImage;
	cost = 7;
};

//Static Shape
datablock StaticShapeData(staticbrickAntennat : staticBrick2x2)
{
	item = brickAntennat;
	ghost = ghostbrickAntennat;
	shapeFile = "~/data/shapes/bricks/a_briks/antennat.dts";

	//lego scale dimensions
	x = 1;
	y = 1;
	z = 12;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(brickAntennatImage : brick2x2Image)
{
	staticShape = staticbrickAntennat;
	ghost = ghostbrickAntennat;
	PreviewFileName = "rtb/data/shapes/bricks/a_briks/antennat.png";

	item = brickAntennat;
};

//plate1x2handle.cs

//Item
datablock ItemData(plate1x2handle : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/a_briks/plate1x2handle.dts";
	pickUpName = 'a 4x3 slope';
	invName = "Plate 1x2 Handle";
	image = plate1x2handleImage;
	cost = 10;
};

//Static Shape
datablock StaticShapeData(staticplate1x2handle : staticBrick2x2)
{
	item = plate1x2handle;
	ghost = ghostplate1x2handle;
	shapeFile = "~/data/shapes/bricks/a_briks/plate1x2handle.dts";

	//lego scale dimensions
	x = 2;
	y = 2;
	z = 1;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(plate1x2handleImage : brick2x2Image)
{
	staticShape = staticplate1x2handle;
	ghost = ghostplate1x2handle;
	PreviewFileName = "rtb/data/shapes/bricks/a_briks/1x2han.png";
	item = plate1x2handle;
};

//grill1x2.cs


//Item
datablock ItemData(grill1x2 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/a_briks/grill1x2.dts";
	pickUpName = 'a 3x8x6 window';
	invName = "Grill 1x2";
	image = grill1x2Image;
	cost = 10;
};

//Static Shape
datablock StaticShapeData(staticplate1x2grill : staticBrick2x2)
{
	item = grill1x2;
	ghost = ghostgrill1x2;
	shapeFile = "~/data/shapes/bricks/a_briks/grill1x2.dts";

	//lego scale dimensions
	x = 1;
	y = 2;
	z = 1;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(grill1x2Image : brick2x2Image)
{
	staticShape = staticplate1x2grill;
	ghost = ghostgrill1x2;
	PreviewFileName = "rtb/data/shapes/bricks/a_briks/1x2grll.png";
	item = grill1x2;
};


//plate16x32.cs


//Item
datablock ItemData(plate16x32 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/a_briks/baseplate16x32.dts";
	pickUpName = 'a 4x3 slope';
	invName = "Plate 16x32";
	image = plate16x32Image;
	cost = 10;
};

//Static Shape
datablock StaticShapeData(staticplate16x32 : staticBrick2x2)
{
	item = plate16x32;
	ghost = ghostplate16x32;
	shapeFile = "~/data/shapes/bricks/a_briks/baseplate16x32.dts";

	//lego scale dimensions
	x = 32;
	y = 16;
	z = 1;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(plate16x32Image : brick2x2Image)
{
	staticShape = staticplate16x32;
	ghost = ghostplate16x32;
	PreviewFileName = "rtb/data/shapes/bricks/a_briks/plate16x32.png";
	item = plate16x32;
};

//brick2x4.cs


//Item
datablock ItemData(wing8x4l : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/a_briks/wing8x4l.dts";
	pickUpName = 'a 3x8x6 window';
	invName = "Wing 8x4l";
	image = wing8x4lImage;
	cost = 10;
};

//Static Shape
datablock StaticShapeData(staticplate8x4wingl : staticBrick2x2)
{
	item = wing8x4l;
	ghost = ghostwing8x4l;
	shapeFile = "~/data/shapes/bricks/a_briks/wing8x4l.dts";

	//lego scale dimensions
	x = 4;
	y = 8;
	z = 1;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(wing8x4lImage : brick2x2Image)
{
	staticShape = staticplate8x4wingl;
	ghost = ghostwing8x4l;
	PreviewFileName = "rtb/data/shapes/bricks/a_briks/wing8x4l.png";
	item = wing8x4l;
};

//wing8x4r.cs


//Item
datablock ItemData(wing8x4r : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/a_briks/wing8x4r.dts";
	pickUpName = 'a 3x8x6 window';
	invName = "Wing 8x4r";
	image = wing8x4rImage;
	cost = 10;
};

//Static Shape
datablock StaticShapeData(staticplate8x4wingr : staticBrick2x2)
{
	item = wing8x4r;
	ghost = ghostwing8x4r;
	shapeFile = "~/data/shapes/bricks/a_briks/wing8x4r.dts";

	//lego scale dimensions
	x = 4;
	y = 8;
	z = 1;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(wing8x4rImage : brick2x2Image)
{
	staticShape = staticplate8x4wingr;
	ghost = ghostwing8x4r;
	PreviewFileName = "rtb/data/shapes/bricks/a_briks/wing8x4r.png";
	item = wing8x4r;
};

//tile1x2.cs


//Item
datablock ItemData(tile1x2 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/a_briks/tile1x2.dts";
	pickUpName = 'a 4x3 slope';
	invName = "Tile 1x2";
	image = tile1x2Image;
	cost = 10;
};

//Static Shape
datablock StaticShapeData(staticplate1x2tile : staticBrick2x2)
{
	item = tile1x2;
	ghost = ghosttile1x2;
	shapeFile = "~/data/shapes/bricks/a_briks/tile1x2.dts";

	//lego scale dimensions
	x = 1;
	y = 2;
	z = 1;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(tile1x2Image : brick2x2Image)
{
	staticShape = staticplate1x2tile;
	ghost = ghosttile1x2;
	PreviewFileName = "rtb/data/shapes/bricks/a_briks/tile1x2.png";
	item = tile1x2;
};

//tile2x2.cs


//Item
datablock ItemData(tile2x2 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/a_briks/tile2x2.dts";
	pickUpName = 'a 4x3 slope';
	invName = "Tile 2x2";
	image = tile2x2Image;
	cost = 10;
};

//Static Shape
datablock StaticShapeData(staticplate2x2tile : staticBrick2x2)
{
	item = tile2x2;
	ghost = ghosttile2x2;
	shapeFile = "~/data/shapes/bricks/a_briks/tile2x2.dts";

	//lego scale dimensions
	x = 2;
	y = 2;
	z = 1;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(tile2x2Image : brick2x2Image)
{
	staticShape = staticplate2x2tile;
	ghost = ghosttile2x2;
	PreviewFileName = "rtb/data/shapes/bricks/a_briks/tile2x2.png";
	item = tile2x2;
};


//cylinder1x1.cs
//Item
datablock ItemData(cylinder1x1 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/a_briks/cylinder1x1.dts";
	pickUpName = 'a 4x3 slope';
	invName = '1x1 Cylinder b';
	image = cylinder1x1Image;
};

//Static Shape
datablock StaticShapeData(staticcylinder1x1 : staticBrick2x2)
{
	item = cylinder1x1;
	shapeFile = "~/data/shapes/bricks/a_briks/cylinder1x1.dts";

	//lego scale dimensions
	x = 1;
	y = 1;
	z = 3;	//3 plates = 1 brick
};

//Image
datablock ShapeBaseImageData(cylinder1x1Image : brick2x2Image)
{
	staticShape = staticcylinder1x1;
	PreviewFileName = "rtb/data/shapes/bricks/a_briks/cylinder1x1.png";

	item = cylinder1x1;
};

//cylinder1x1p.cs with studs
//Item
datablock ItemData(cylinder1x1p : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/a_briks/cylinder1x1p.dts";
	pickUpName = 'a 4x3 slope';
	invName = '1x1 Cylinder';
	image = cylinder1x1pImage;
};

//Static Shape
datablock StaticShapeData(staticcylinder1x1p : staticBrick2x2)
{
	item = cylinder1x1p;
	shapeFile = "~/data/shapes/bricks/a_briks/cylinder1x1p.dts";

	//lego scale dimensions
	x = 1;
	y = 1;
	z = 3;	//3 plates = 1 brick
};

//Image
datablock ShapeBaseImageData(cylinder1x1pImage : brick2x2Image)
{
	staticShape = staticcylinder1x1p;
	PreviewFileName = "rtb/data/shapes/bricks/a_briks/cylinder1x1p.png";

	item = cylinder1x1p;
};

//cylinder1x1f.cs
//Item
datablock ItemData(cylinder1x1f : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/a_briks/cylinder1x1f.dts";
	pickUpName = 'a 4x3 slope';
	invName = '1x1 Cylinder F b';
	image = cylinder1x1fImage;
};

//Static Shape
datablock StaticShapeData(staticcylinder1x1f : staticBrick2x2)
{
	item = cylinder1x1f;
	shapeFile = "~/data/shapes/bricks/a_briks/cylinder1x1f.dts";

	//lego scale dimensions
	x = 1;
	y = 1;
	z = 1;	//3 plates = 1 brick
};

//Image
datablock ShapeBaseImageData(cylinder1x1fImage : brick2x2Image)
{
	staticShape = staticcylinder1x1f;
	PreviewFileName = "rtb/data/shapes/bricks/a_briks/cylinder1x1f.png";
	item = cylinder1x1f;
};

//cylinder1x1fp.cs
//Item
datablock ItemData(cylinder1x1fp : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/a_briks/cylinder1x1fs.dts";
	pickUpName = 'a 4x3 slope';
	invName = '1x1 Cylinder F';
	image = cylinder1x1fpImage;
};

//Static Shape
datablock StaticShapeData(staticcylinder1x1fp : staticBrick2x2)
{
	item = cylinder1x1f;
	shapeFile = "~/data/shapes/bricks/a_briks/cylinder1x1fs.dts";

	//lego scale dimensions
	x = 1;
	y = 1;
	z = 1;	//3 plates = 1 brick
};

//Image
datablock ShapeBaseImageData(cylinder1x1fpImage : brick2x2Image)
{
	staticShape = staticcylinder1x1fp;
	PreviewFileName = "rtb/data/shapes/bricks/a_briks/cylinder1x1fs.png";
	item = cylinder1x1fp;
};

//tile2x2.cs





//Image
datablock ShapeBaseImageData(print1x2Image : brick2x2Image)
{
	staticShape = staticprint1x2;
	ghost = ghostprint1x2;
	PreviewFileName = "rtb/data/shapes/bricks/a_briks/tile2x2.png";
	item = print1x2;
};

////////////////////////////////tile1x1.cs


//Item
datablock ItemData(tile1x1 : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/tile1x1.dts";
	pickUpName = 'tile1x1';
	invName = "tile1x1";
	image = tile1x1Image;
	cost = 10;
};

//Static Shape
datablock StaticShapeData(statictile1x1 : staticBrick2x2)
{
	item = tile1x1;
	ghost = ghosttile1x1;
	shapeFile = "~/data/shapes/bricks/tile1x1.dts";

	//lego scale dimensions
	x = 1;
	y = 1;
	z = 1;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(tile1x1Image : brick2x2Image)
{
	staticShape = statictile1x1;
	ghost = ghosttile1x1;
	PreviewFileName = "rtb/data/shapes/bricks/Previews/Tile1x1.PNG";
	item = tile1x1;
};

////////////////////////////////tile2x2round.cs


//Item
datablock ItemData(tile2x2round : brick2x2)
{
	shapeFile = "~/data/shapes/bricks/tile2x2round.dts";
	pickUpName = 'tile2x2round';
	invName = "tile2x2round";
	image = tile2x2roundImage;
	cost = 10;
};

//Static Shape
datablock StaticShapeData(statictile2x2round : staticBrick2x2)
{
	item = tile2x2round;
	ghost = ghosttile2x2round;
	shapeFile = "~/data/shapes/bricks/tile2x2round.dts";

	//lego scale dimensions
	x = 2;
	y = 2;
	z = 1;	//3 plates = 1 brick

	//boxX = 0.25;
	//boxY = 1.0;
	//boxZ = 0.3;
};


//Image
datablock ShapeBaseImageData(tile2x2roundImage : brick2x2Image)
{
	staticShape = statictile2x2round;
	ghost = ghosttile2x2round;
	PreviewFileName = "rtb/data/shapes/bricks/previews/Tile2x2Round.PNG";
	item = tile2x2round;
};

// stick.cs a evil,deadly weapon :D
//--------------------------------------------------------------------------

datablock AudioProfile(stickDrawSound)
{
   filename    = "~/data/sound/stickDraw.wav";
   description = AudioClosest3d;
   preload = true;
};
datablock AudioProfile(stickHitSound)
{
   filename    = "~/data/sound/stickHit.wav";
   description = AudioClosest3d;
   preload = true;
};


//effects
datablock ParticleData(stickExplosionParticle)
{
   dragCoefficient      = 5;
   gravityCoefficient   = 0.2;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 500;
   lifetimeVarianceMS   = 300;
   textureName          = "~/data/particles/chunk";
   colors[0]     = "0.7 0.7 0.9 0.9";
   colors[1]     = "0.9 0.9 0.9 0.0";
   sizes[0]      = 0.5;
   sizes[1]      = 1.0;
};

datablock ParticleEmitterData(stickExplosionEmitter)
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   ejectionVelocity = 8;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 60;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "stickExplosionParticle";
};

datablock ExplosionData(stickExplosion)
{
   //explosionShape = "";
   lifeTimeMS = 500;

   soundProfile = stickHitSound;

   particleEmitter = stickExplosionEmitter;
   particleDensity = 10;
   particleRadius = 0.2;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = true;
   camShakeFreq = "20.0 22.0 20.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 0.5;
   camShakeRadius = 10.0;

   // Dynamic light
   lightStartRadius = 3;
   lightEndRadius = 0;
   lightStartColor = "00.0 0.2 0.6";
   lightEndColor = "0 0 0";
};


//projectile
datablock ProjectileData(stickProjectile)
{
   //projectileShapeName = "~/data/shapes/arrow.dts";
   directDamage        = 6;
   radiusDamage        = 6;
   damageRadius        = 0.5;
   explosion           = stickExplosion;
   //particleEmitter     = as;

   muzzleVelocity      = 50;
   velInheritFactor    = 1;

   armingDelay         = 0;
   lifetime            = 100;
   fadeDelay           = 70;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0.0;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";
};


//////////
// item //
//////////
datablock ItemData(stick)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/bricks/a_briks/stick.dts";
	skinName = 'brown';
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	cost = "12";
	 // Dynamic properties defined by the scripts
	pickUpName = 'a stick';
	invName = 'stick';
	image = stickImage;
};



////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(stickImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/bricks/a_briks/stick.dts";
   PreviewFileName = "rtb/data/shapes/bricks/Previews/stick.png";
   emap = true;
   	skinName = 'brown';
	cloakable = false;
   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   //eyeOffset = "0.1 0.2 -0.55";

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.
   correctMuzzleVector = false;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = stick;
   ammo = " ";
   projectile = stickProjectile;
   projectileType = Projectile;

   //melee particles shoot from eye node for consistancy
   melee = true;
   //raise your arm up or not
   armReady = true;

   //casing = " ";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.5;
	stateTransitionOnTimeout[0]       = "Ready";
	stateSound[0]					= stickDrawSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "PreFire";
	stateAllowImageChange[1]         = true;

	stateName[2]			= "PreFire";
	stateScript[2]                  = "onPreFire";
	stateAllowImageChange[2]        = false;
	stateTimeoutValue[2]            = 0.1;
	stateTransitionOnTimeout[2]     = "Fire";

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "CheckFire";
	stateTimeoutValue[3]            = 0.2;
	stateFire[3]                    = true;
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	stateScript[3]                  = "onFire";
	stateWaitForTimeout[3]		= true;
	//stateTransitionOnTriggerUp[3]	= "StopFire";

	stateName[4]			= "CheckFire";
	stateTransitionOnTriggerUp[4]	= "StopFire";
	stateTransitionOnTriggerDown[4]	= "Fire";


	stateName[5]                    = "StopFire";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 0.2;
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;
	stateSequence[5]                = "StopFire";
	stateScript[5]                  = "onStopFire";


};

function stickImage::onPreFire(%this, %obj, %slot)
{
	//messageAll( 'MsgClient', 'stick prefired!!!');
	//Parent::onFire(%this, %obj, %slot);
	%obj.playthread(2, armattack);
}

function stickImage::onStopFire(%this, %obj, %slot)
{
	%obj.playthread(2, root);
	//messageAll( 'MsgClient', 'stopfire');
}

function stickProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{echo(%this SPC %obj SPC %col SPC %fade SPC %pos SPC %normal);
weaponDamage(%obj,%col,%this,%pos);
	if (%obj.client && (%col.getClassName() $= "Player" || %col.getClassName() $= "AIPlayer")){

	if (%obj.client.player){
  	%col.setvelocity(vectorscale(%obj.client.player.geteyevector(),16));

	//%col.applyimpulse(0,vectorscale(%muzzleVector,500));
  	}
  	weaponDamage(%obj,%col,%this,%pos);
	}else if(%obj.sourceobject.aiplayer==1){
  	%col.setvelocity(vectorscale(%obj.sourceobject.geteyevector(),16));
  	}
}
function BrickImage::onMount(%this,%player,%slot)
{
	%skin = %this.skinname;
	%this.setskin(%skin);
}

datablock ShapeBaseImageData(spacevisorShowImage)
{
	shapeFile = "~/data/shapes/bricks/a_briks/svis.dts";
	emap = true;
	mountPoint = 6;
	headUp = true;
	className = "ItemImage";
};