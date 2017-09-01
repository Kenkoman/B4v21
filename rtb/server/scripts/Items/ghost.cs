//ghost.cs

//ghost item- does some magic thing, who knows.  



//////////
// item //
//////////

datablock ItemData(ghost)
{
	category = "Item";  // Mission editor category

	equipment = true;	//for gui messages

	//its already a member of item namespace so dont break it
	className = "crap"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/ghost.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a ghost';
	invName = 'ghost';
	image =ghostImage;
};


function crap::oncollision(%this,%obj,%col,%vec,%speed)
{
	echo("crap collision");

}


///////////
// image //
///////////
datablock ShapeBaseImageData(ghostImage)
{
	// Basic Item properties
	shapeFile = "~/data/shapes/ghost.dts";
	emap = true;

	// Specify mount point & offset for 3rd person, and eye offset
	// for first person rendering.
	mountPoint = 2;
	offset = "0 0.04 -0.05";
	cloakable = false;
	// Add the WeaponImage namespace as a parent, WeaponImage namespace
	// provides some hooks into the inventory system.
	className = "ItemImage";

	item = ghost;

	armReady = true;
};



function ghostImage::onMount(%this,%obj)
{
	%rightImage = %obj.getMountedImage($RightHandSlot);
			%obj.playthread(1, armreadyboth);
}

function ghostImage::onUnmount(%this,%obj)
{
	%rightImage = %obj.getMountedImage($RightHandSlot);

	if(%rightImage)
	{
		if(%rightImage.armready)
		{
			%obj.playthread(1, armreadyright);
			return;
		}
	}
	%obj.playthread(1, root);
}




