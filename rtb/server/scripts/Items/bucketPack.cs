//bucketpack.cs

//bucketpack - lets you carry 10 things

//////////
// item //
//////////

datablock ItemData(bucketPack)
{
	category = "Item";  // Mission editor category

	equipment = true;	//for gui messages

	//its already a member of item namespace so dont break it
	//className = "Item"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/bucketPack.dts";
	mass = 1;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	cost = 20;
	 // Dynamic properties defined by the scripts
	pickUpName = 'a bucket pack';
	invName = 'Bucket';
	image = bucketPackImage;
};

//function bucketPack::onUse(%this,%user)
//{
//	//mount the image in the right hand slot
//	%user.mountimage(%this.image, $BackSlot);
//}
///////////
// image //
///////////
datablock ShapeBaseImageData(bucketPackImage)
{
	// Basic Item properties
	shapeFile = "~/data/shapes/bucketPack.dts";
	emap = true;
	cloakable = false;
	// Specify mount point & offset for 3rd person, and eye offset
	// for first person rendering.
	mountPoint = 2;
	offset = "0 0 0";

	// Add the WeaponImage namespace as a parent, WeaponImage namespace
	// provides some hooks into the inventory system.
	className = "ItemImage";

	item = bucketPack;
	headUp = true;
};