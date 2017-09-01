//armor.cs

//armor item- using mounts armor image on your torso and makes your torso immune to arrows



//////////
// item //
//////////

//we're calling this platemail because armor is a class name

datablock ItemData(plateMail)
{
	category = "Item";  // Mission editor category

	equipment = true;	//for gui messages

	//its already a member of item namespace so dont break it
	//className = "Item"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/armor.dts";
	mass = 1;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	cost = 40;
	 // Dynamic properties defined by the scripts
	pickUpName = 'a suit of armor';
	invName = 'Armor';
	image = plateMailImage;
};

//function plateMail::onUse(%this,%user)
//{
//	//if the image is mounted already, unmount it
//	//if it isnt, mount it
//
//	%mountPoint = %this.image.mountPoint;
//	%mountedImage = %user.getMountedImage(%mountPoint); 
//	
//	if(%mountedImage)
//	{
//		echo(%mountedImage);
//		if(%mountedImage == %this.image.getId())
//		{
//			//our image is already mounted so unmount it
//			%user.unMountImage(%mountPoint);
//		}
//		else
//		{
//			//something else is there so mount our image
//			%user.mountimage(%this.image, %mountPoint);
//		}
//	}
//	else
//	{
//		//nothing there so mount 
//		%user.mountimage(%this.image, %mountPoint);
//	}
//	
//}

///////////
// image //
///////////
datablock ShapeBaseImageData(plateMailImage)
{
	// Basic Item properties
	shapeFile = "~/data/shapes/armor.dts";
	emap = true;
	cloakable = false;
	// Specify mount point & offset for 3rd person, and eye offset
	// for first person rendering.
	mountPoint = 2;
	offset = "0 0 0";

	// Add the WeaponImage namespace as a parent, WeaponImage namespace
	// provides some hooks into the inventory system.
	className = "ItemImage";

	item = plateMail;
	headUp = true;

	stateName[0]                    = "Activate";
	stateSound[0]					= swordDrawSound;
	stateAllowImageChange[0]		= true;

};