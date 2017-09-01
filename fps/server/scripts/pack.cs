//pack.cs

//pack item - does something.  More life or run faster? healing? camping?

//////////
// item //
//////////

datablock ItemData(pack)
{
	category = "Item";  // Mission editor category

	equipment = true;	//for gui messages

	//its already a member of item namespace so dont break it
	//className = "Item"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/pack.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a pack';
	invName = 'Pack';
	image = packImage;
};

//function pack::onUse(%this,%user)
//{
//	//mount the image in the right hand slot
//	%user.mountimage(%this.image, $BackSlot);
//}
///////////
// image //
///////////
datablock ShapeBaseImageData(packImage)
{
	// Basic Item properties
	shapeFile = "~/data/shapes/pack.dts";
	emap = true;

	// Specify mount point & offset for 3rd person, and eye offset
	// for first person rendering.
	mountPoint = 2;
	offset = "0 0 0";

	// Add the WeaponImage namespace as a parent, WeaponImage namespace
	// provides some hooks into the inventory system.
	className = "ItemImage";

	item = pack;
	headUp = true;
};