//pointyHelmet.cs

//pointy helmet item- makes your head invulnerable to arrows

//////////
// item //
//////////


datablock ItemData(pointyHelmet)
{
	category = "Item";  // Mission editor category

	equipment = true;	//for gui messages

	//its already a member of item namespace so dont break it
	//className = "Item"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/pointyHelmet.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	cost = "40";
	 // Dynamic properties defined by the scripts
	pickUpName = 'a pointy helmet';
	invName = 'Pointy Helmet';
	image = pointyHelmetImage;
};

//function pointyHelmet::onUse(%this,%user)
//{
//	//mount the image in the right hand slot
//	%user.mountimage(%this.image, $HeadSlot);
//}
///////////
// image //
///////////
datablock ShapeBaseImageData(pointyHelmetImage)
{
	// Basic Item properties
	shapeFile = "~/data/shapes/pointyHelmet.dts";
	emap = true;
	cloakable = false;
	// Specify mount point & offset for 3rd person, and eye offset
	// for first person rendering.
	mountPoint = 5;
	offset = "0 0 0";

	// Add the WeaponImage namespace as a parent, WeaponImage namespace
	// provides some hooks into the inventory system.
	className = "ItemImage";

	item = pointyHelmet;

	stateName[0]                    = "Activate";
	stateSound[0]					= swordDrawSound;
	stateAllowImageChange[0]		= true;
};