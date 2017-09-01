//quiver.cs

//quiver pack item - required for using bow?


//////////
// item //
//////////

datablock ItemData(quiver)
{
	category = "Item";  // Mission editor category

	equipment = true;	//for gui messages

	//its already a member of item namespace so dont break it
	//className = "Item"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/quiver.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	cost = "50";
	 // Dynamic properties defined by the scripts
	pickUpName = 'a quiver';
	invName = 'Quiver';
	image = quiverImage;
};

//function quiver::onUse(%this,%user)
//{
//	//mount the image in the right hand slot
//	%user.mountimage(%this.image, $BackSlot);
//
//	//check for bow image and replace with super bow image
//
//}
///////////
// image //
///////////
datablock ShapeBaseImageData(quiverImage)
{
	// Basic Item properties
	shapeFile = "~/data/shapes/quiver.dts";
	emap = true;
	cloakable = false;
	// Specify mount point & offset for 3rd person, and eye offset
	// for first person rendering.
	mountPoint = 2;
	offset = "0 0 0";

	// Add the WeaponImage namespace as a parent, WeaponImage namespace
	// provides some hooks into the inventory system.
	className = "ItemImage";

	item = quiver;
	headUp = true;
};




function quiverImage::onMount(%this, %obj)
{
	Parent::onMount(%this, %obj);
	//check for superbow image and replace with bow image
	if(%obj.getMountedImage($righthandSlot) )
	{
		if(%obj.getMountedImage($righthandSlot) == bowImage.getId())
		{
			%obj.mountimage(superBowImage, $RightHandSlot, 0, superBowImage.skinName);
		}
	}
}

function quiverImage::onUnmount(%this, %obj)
{
	Parent::onUnmount(%this, %obj);
	//check for superbow image and replace with bow image
	if(%obj.getMountedImage($righthandSlot) )
	{
		if(%obj.getMountedImage($righthandSlot) == superBowImage.getId())
		{
			%obj.mountimage(bowImage, $RightHandSlot, 0, bowImage.skinName);
		}
	}
}