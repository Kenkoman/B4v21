//triplume.cs

//tri plume - feather in hat thing, looks cool


//////////
// item //
//////////


datablock ItemData(triPlume)
{
	category = "Item";  // Mission editor category

	equipment = true;	//for gui messages

	//its already a member of item namespace so dont break it
	//className = "Item"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/triPlume.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = "a triple plume";
	image = triPlumeImage;
};

function triPlume::onUse(%this,%user)
{
	%mountPoint = %this.image.mountPoint;
	%mountedImage = %user.getMountedImage(%mountPoint); 

	if(%user.getMountedimage($HeadSlot) == scoutHatImage.getId())
	{
		//scout hat on
		if(%mountedImage == %this.image.getId())
		{
			//plume already on, unmount it
			%user.unMountImage(%mountPoint);
		}
		else
		{
			//plume isnt mounted, mount plume
			%color = %user.client.colorTriPlume;
			%user.mountimage(%this.image, %mountPoint, 0, %color);
		}
		
	}
	else
	{
		if(%user.client)
			messageClient(%user.client, 'MsgItemPickup', 'You need a hat to use this');
	}
}
///////////
// image //
///////////
datablock ShapeBaseImageData(triPlumeImage)
{
	// Basic Item properties
	shapeFile = "~/data/shapes/triPlume.dts";
	emap = true;

	// Specify mount point & offset for 3rd person, and eye offset
	// for first person rendering.
	mountPoint = 6;
	offset = "0 0 0";

	// Add the WeaponImage namespace as a parent, WeaponImage namespace
	// provides some hooks into the inventory system.
	className = "ItemImage";

	item = triPlume;
};

