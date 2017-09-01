//shield.cs

//shield item - protects you from something
//		-possibly arrows to the legs
//		-possibly you have to actively block


//////////
// item //
//////////

datablock ItemData(shield)
{
	category = "Item";  // Mission editor category

	equipment = true;

	//its already a member of item namespace so dont break it
	//className = "Item"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/shield.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	cost = "40";
	 // Dynamic properties defined by the scripts
	pickUpName = 'a shield';
	invName = 'Shield';
	image = shieldImage;
};

//function shield::onUse(%this,%user)
//{
//	//mount the image in the right hand slot
//	%user.mountimage(%this.image, $LeftHandSlot);
//}
///////////
// image //
///////////
datablock ShapeBaseImageData(shieldImage)
{
	// Basic Item properties
	shapeFile = "~/data/shapes/shield.dts";
	emap = true;
	cloakable = false;
	// Specify mount point & offset for 3rd person, and eye offset
	// for first person rendering.
	mountPoint = 1;
	offset = "0 0 0";

	// Add the WeaponImage namespace as a parent, WeaponImage namespace
	// provides some hooks into the inventory system.
	className = "ItemImage";

	item = shield;

	armReady = true;

	stateName[0]			= "Activate";
	stateTimeoutValue[0]		= 0.5;
	stateTransitionOnTimeout[0]	= "Ready";
	stateSequence[0]		= "activate";
	stateSound[0]			= swordDrawSound;

	stateName[1]			= "Ready";
	stateTransitionOnTriggerDown[1]	= "Block";
	stateAllowImageChange[1]	= true;
	stateSequence[1]		= "activate";

	stateName[2]			= "Block";
	stateTransitionOnTimeout[2]	= "UnBlock";
	stateTimeoutValue[2]		= 0.5;
	stateScript[2]			= "onBlock";
	stateAllowImageChange[2]	= false;
	stateSequence[2]		= "Block";

	stateName[3]			= "UnBlock";
	stateTransitionOnTimeout[2]	= "Ready";
	stateTimeoutValue[3]		= 0.5;
	stateScript[3]			= "onUnBlock";
	stateAllowImageChange[3]	= false;
	stateSequence[3]		= "unBlock";
};

function shieldImage::onBlock(%this, %obj, %slot)
{
	echo("shield block");
}

function shieldImage::onMount(%this,%obj)
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

function shieldImage::onUnmount(%this,%obj)
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

