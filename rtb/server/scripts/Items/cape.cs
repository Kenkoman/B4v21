//cape.cs

//cape item- makes you invisible when you stand still for a bit
//	-requires 10-20 sec activation time to prevent fast switching


//////////
// item //
//////////

datablock ItemData(cape)
{
	category = "Item";  // Mission editor category

	equipment = true;	//for gui messages

	//its already a member of item namespace so dont break it
	//className = "Item"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/cape.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	cost = 150;
	 // Dynamic properties defined by the scripts
	pickUpName = 'a cape';
	invName = 'Cape';
	image = capeImage;
};

///////////
// image //
///////////
datablock ShapeBaseImageData(capeImage)
{
	// Basic Item properties
	shapeFile = "~/data/shapes/cape.dts";
	emap = true;

	// Specify mount point & offset for 3rd person, and eye offset
	// for first person rendering.
	mountPoint = 2;
	offset = "0 0 0";
	cloakable = false;
	// Add the WeaponImage namespace as a parent, WeaponImage namespace
	// provides some hooks into the inventory system.
	className = "ItemImage";

	item = cape;
	headUp = true;


	stateName[0]			= "Activate";
	stateTimeoutValue[0]		= 10.0;
	stateTransitionOnTimeout[0]	= "Ready";

	stateName[1]			= "Ready";
	stateTimeoutValue[1]		= 1.0;
	stateScript[1]			= onReady;
	stateTransitionOnTimeout[1]	= "Check";

	stateName[2]			= "Check";
	stateTimeoutValue[2]		= 0.5;
	stateScript[2]			= onCheck;
	stateTransitionOnTimeout[2]	= "Delay";

	stateName[3]			= "Delay";
	stateTimeoutValue[3]		= 2.0;
	stateTransitionOnTimeout[3]	= "Check";


};

function capeImage::onReady(%this, %obj, %slot)
{
	messageClient(%obj.client, 'Clientmsg', "Cape Ready!");
}

function capeImage::onCheck(%this, %obj, %slot)
{
	if(VectorLen(%obj.getVelocity()) < 0.01)
	{
		//not moving so cloak
		%obj.setCloaked(1);
		%obj.setShapeName("");
	}
	else
	{
		//you are moving, make sure you are visible
		%obj.setCloaked(0);
		%obj.setShapeName(%obj.client.name);
	}
}

function capeImage::onUnMount(%this, %obj, %slot)
{
	%obj.setCloaked(0);
	%obj.setShapeName(%obj.client.name);
	Parent::onUnMount(%this, %obj, %slot);
}
