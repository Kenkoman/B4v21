//helmet.cs

//motorcycle helmet item- makes your head invulnerable to arrows?
//			- lets you use a visor

//////////
// item //
//////////
datablock AudioProfile(helmetActivateSound)
{
   filename    = "~/data/sound/helmetActivate.wav";
   description = AudioClosest3d;
   preload = true;
};



datablock ItemData(helmet)
{
	category = "Item";  // Mission editor category

	equipment = true;	//for gui messages

	//its already a member of item namespace so dont break it
	//className = "Item"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/helmet.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	cost = 40;
	 // Dynamic properties defined by the scripts
	pickUpName = 'a space helmet';
	invName = 'Helmet';
	image = helmetImage;
};

//function helmet::onUse(%this,%user)
//{
//	//mount the image in the right hand slot
//	%user.mountimage(%this.image, $HeadSlot);
//}
///////////
// image //
///////////
datablock ShapeBaseImageData(helmetImage)
{
	// Basic Item properties
	shapeFile = "~/data/shapes/helmet.dts";
	emap = true;
	cloakable = false;
	// Specify mount point & offset for 3rd person, and eye offset
	// for first person rendering.
	mountPoint = 5;
	offset = "0 0 0";

	// Add the WeaponImage namespace as a parent, WeaponImage namespace
	// provides some hooks into the inventory system.
	className = "ItemImage";

	item = helmet;
	
	stateName[0]	= "Activate";
	stateSound[0]	= helmetActivateSound;

};

function helmetImage::onUnmount(%this, %obj)
{
	//cant have a visor without a helmet so unmount the visor
	%obj.unmountImage($VisorSlot);
}