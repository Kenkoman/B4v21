//bill100.cs

//bill100 item- just for show i suppose.  



//////////
// item //
//////////

datablock ItemData(bill100)
{
	category = "Item";  // Mission editor category

	equipment = true;	//for gui messages

	//its already a member of item namespace so dont break it
	className = "crap"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/100bill.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;	
	respawn = true;
	cashammount = 100;
	nopickup = 1;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a 100 Dollar Bill';
	invName = 'bill100';
	image =bill100Image;
};


///////////
// image //
///////////
datablock ShapeBaseImageData(bill100Image)
{
	// Basic Item properties
	shapeFile = "~/data/shapes/100bill.dts";
	emap = true;

	// Specify mount point & offset for 3rd person, and eye offset
	// for first person rendering.
	mountPoint = 1;
	offset = "0 0 0";

	// Add the WeaponImage namespace as a parent, WeaponImage namespace
	// provides some hooks into the inventory system.
	className = "ItemImage";
	cloakable = false;
	item = bill100;

	armReady = true;
};



function bill100Image::onMount(%this,%obj)
{
}

function bill100Image::onUnmount(%this,%obj)
{
}

function bill100::onCollision(%this,%obj,%col,%vec,%speed)
{
	%client = %col.client;
	%player = %col;

	%client.money = %client.money + %this.cashammount;
	messageClient(%client,'MsgUpdateMoney','',%client.Money);

	for(%position = 0; %position < 11; %position++)
	{
              if ( %player.inventory[%position] != 0 )
		if( %player.inventory[%position].getID() == %this )
		{
			messageClient(%client, 'MsgItemPickup', '', %position, "");
			%player.inventory[%position] = 0;
		}
	}
}


