//airtank.cs

//air tank item - lets you breathe i guess...
//maybe be a jetpack too?


datablock AudioProfile(airTankActivateSound)
{
   filename    = "~/data/sound/airTankActivate.wav";
   description = AudioClosest3d;
   preload = true;
};


//////////
// item //
//////////

datablock ItemData(airTank)
{
	category = "Item";  // Mission editor category

	equipment = true;
	
	//its already a member of item namespace so dont break it
	//className = "Item"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/tank.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'an air tank';
	invName = 'Air Tank';
	image = airTankImage;
};

//function airTank::onUse(%this,%user)
//{
//	//mount the image in the right hand slot
//	%user.mountimage(%this.image, $BackSlot);
//}

///////////
// image //
///////////
datablock ShapeBaseImageData(airTankImage)
{
	// Basic Item properties
	shapeFile = "~/data/shapes/tank.dts";
	emap = true;

	// Specify mount point & offset for 3rd person, and eye offset
	// for first person rendering.
	mountPoint = 2;
	offset = "0 0 0";

	// Add the WeaponImage namespace as a parent, WeaponImage namespace
	// provides some hooks into the inventory system.
	className = "ItemImage";

	item = airTank;
	headUp = true;

	stateName[0]	= "Activate";
	stateSound[0]	= airTankActivateSound;
};