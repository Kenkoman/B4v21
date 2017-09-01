//visor.cs

//visor item- can only use it if you're wearing a motorcycle helmet
//	    - Lets you see secret messages with color overlay?

//////////
// item //
//////////


datablock ItemData(visor)
{
	category = "Item";  // Mission editor category

	equipment = true;	//for gui messages

	//its already a member of item namespace so dont break it
	//className = "Item"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/visor.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a visor';
	invName = 'Visor';
	image = visorImage;
};

function visor::onUse(%this,%user)
{
	%mountPoint = %this.image.mountPoint;
	%mountedImage = %user.getMountedImage(%mountPoint); 

	if(%user.getMountedimage($HeadSlot) == helmetImage.getId())
	{
		//helmet on
		if(%mountedImage == %this.image.getId())
		{
			//visor already on, unmount it
			%user.unMountImage(%mountPoint);
		}
		else
		{
			//visor isnt mounted, mount visor
			%color =  %user.client.colorvisor;
			%user.mountimage(%this.image, %mountPoint, 0, %color);
		}
		
	}
	else
	{
		if(%user.client)
			messageClient(%user.client, 'MsgItemPickup', 'You need a helmet to use this');
	}
}

///////////
// image //
///////////
datablock ShapeBaseImageData(visorImage)
{
	// Basic Item properties
	shapeFile = "~/data/shapes/visor.dts";
	emap = true;

	// Specify mount point & offset for 3rd person, and eye offset
	// for first person rendering.
	mountPoint = 6;
	offset = "0 0 0";

	// Add the WeaponImage namespace as a parent, WeaponImage namespace
	// provides some hooks into the inventory system.
	className = "ItemImage";

	item = visor;
};

