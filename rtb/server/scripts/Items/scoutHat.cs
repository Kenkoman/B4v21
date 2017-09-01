//scoutHat.cs

//scout hat- lets you see further?

//////////
// item //
//////////


datablock ItemData(scoutHat)
{
	category = "Item";  // Mission editor category

	equipment = true;	//for gui messages

	//its already a member of item namespace so dont break it
	//className = "Item"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/scoutHat.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	cost = "20";
	 // Dynamic properties defined by the scripts
	pickUpName = 'a scout hat';
	invName = 'Scout Hat';
	image = scoutHatImage;
};

//function scoutHat::onUse(%this,%user)
//{
//	//mount the image in the right hand slot
//	%user.mountimage(%this.image, $HeadSlot);
//}
///////////
// image //
///////////
datablock ShapeBaseImageData(scoutHatImage)
{
	// Basic Item properties
	shapeFile = "~/data/shapes/scoutHat.dts";
	emap = true;
	cloakable = false;
	// Specify mount point & offset for 3rd person, and eye offset
	// for first person rendering.
	mountPoint = 5;
	offset = "0 0 0";
	
	renderFirstPerson = false;

	// Add the WeaponImage namespace as a parent, WeaponImage namespace
	// provides some hooks into the inventory system.
	className = "ItemImage";

	item = scoutHat;
};

function scoutHatImage::onUnmount(%this, %obj)
{
	//cant have a feather without a scout hat
	%obj.unmountImage($VisorSlot);
}
