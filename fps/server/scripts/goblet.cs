//goblet.cs

//goblet item- does some magic thing, who knows.  



//////////
// item //
//////////

datablock ItemData(goblet)
{
	category = "Item";  // Mission editor category

	equipment = true;	//for gui messages

	//its already a member of item namespace so dont break it
	className = "crap"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/goblet.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a goblet';
	invName = 'Goblet';
	image =gobletImage;
};


function crap::oncollision(%this,%obj,%col,%vec,%speed)
{
	echo("crap collision");

}


///////////
// image //
///////////
datablock ShapeBaseImageData(gobletImage)
{
	// Basic Item properties
	shapeFile = "~/data/shapes/goblet.dts";
	emap = true;

	// Specify mount point & offset for 3rd person, and eye offset
	// for first person rendering.
	mountPoint = 1;
	offset = "0 0 0";

	// Add the WeaponImage namespace as a parent, WeaponImage namespace
	// provides some hooks into the inventory system.
	className = "ItemImage";

	item = goblet;

	armReady = true;
};



function gobletImage::onMount(%this,%obj)
{
	%rightImage = %obj.getMountedImage($RightHandSlot);

	if(%rightImage)
	{
		if(%rightImage.armready)
		{
			%obj.playthread(1, armreadyboth);
			return;
		}
	}
	%obj.playthread(1, armReadyLeft);
}

function gobletImage::onUnmount(%this,%obj)
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




