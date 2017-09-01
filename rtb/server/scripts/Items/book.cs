//book.cs

//book item- opens f1 help on mount



//////////
// item //
//////////

datablock ItemData(book)
{
	category = "Item";  // Mission editor category

	equipment = true;	//for gui messages

	//its already a member of item namespace so dont break it
	className = "crap"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/book.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'The Guide';
	invName = 'The Guide';
	image =bookImage;
};


function crap::oncollision(%this,%obj,%col,%vec,%speed)
{
	echo("crap collision");

}


///////////
// image //
///////////
datablock ShapeBaseImageData(bookImage)
{
	// Basic Item properties
	shapeFile = "~/data/shapes/book.dts";
	emap = true;

	// Specify mount point & offset for 3rd person, and eye offset
	// for first person rendering.
	mountPoint = 1;
	offset = "0 0.2 -0.4";

	// Add the WeaponImage namespace as a parent, WeaponImage namespace
	// provides some hooks into the inventory system.
	className = "ItemImage";
	cloakable = false;
	item = book;

	armReady = true;
};

function bookImage::onMount(%this,%obj)
{
	%rightImage = %obj.getMountedImage($RightHandSlot);
	commandtoclient(%obj.client,'OpenSRules');
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

function bookImage::onUnmount(%this,%obj)
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



