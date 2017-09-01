//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

// These scripts make use of dynamic attribute values on Item datablocks,
// these are as follows:
//
//    maxInventory      Max inventory per object (100 bullets per box, etc.)
//    pickupName        Name to display when client pickups item
//
// Item objects can have:
//
//    count             The # of inventory items in the object.  This
//                      defaults to maxInventory if not set.

// Respawntime is the amount of time it takes for a static "auto-respawn"
// object, such as an ammo box or weapon, to re-appear after it's been
// picked up.  Any item marked as "static" is automaticlly respawned.
$Item::RespawnTime = 20 * 1000;

// Poptime represents how long dynamic items (those that are thrown or
// dropped) will last in the world before being deleted.
$Item::PopTime = 10 * 1000;


//-----------------------------------------------------------------------------
// ItemData base class methods used by all items
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------

function Item::respawn(%this)
{
   // This method is used to respawn static ammo and weapon items
   // and is usually called when the item is picked up.
   // Instant fade...
   %this.startFade(0, 0, true);
   %this.hide(true);

   // Shedule a reapearance
   %this.schedule(2000, "hide", false);
   %this.schedule(2000, "startFade", 1000, 0, false);
}   

function Item::schedulePop(%this)
{
   // This method deletes the object after a default duration. Dynamic
   // items such as thrown or drop weapons are usually popped to avoid
   // world clutter.
   %this.schedule($Item::PopTime - 1000, "startFade", 1000, 0, true);
   %this.schedule($Item::PopTime, "delete");
}


//-----------------------------------------------------------------------------
// Callbacks to hook items into the inventory system

function ItemData::onThrow(%this,%user,%amount)
{
   // Remove the object from the inventory
   if (%amount $= "")
      %amount = 1;
   if (%this.maxInventory !$= "")
      if (%amount > %this.maxInventory)
         %amount = %this.maxInventory;
   if (!%amount)
      return 0;
   %user.decInventory(%this,%amount);

   // Construct the actual object in the world, and add it to 
   // the mission group so it's cleaned up when the mission is
   // done.  The object is given a random z rotation.
   %obj = new Item() {
      datablock = %this;
      rotation = "0 0 1 " @ (getRandom() * 360);
      count = %amount;
   };
   %obj.thrown = 1;
   MissionGroup.add(%obj);
   %obj.schedulePop();
   %obj.setShapeName(%this.invName);
   echo("HI");
   return %obj;
}

function ItemData::onPickup(%this,%obj,%user,%amount)
{
	%player = %user;
	%data = %player.getDataBlock();
	%client = %player.client;
	%isThrown = 0;
	if(%obj.thrown $= 1)
	{
	%isThrown = 1;
	}
	//if(%client.WantBuy != 1 && $Pref::Server::ItemsCostMoney $= 1 && %this.cost > 0)
	//{
	//bottomPrint(%client,"Press <B> to Buy this Item!",0.5,1);
	//}
	if($Pref::Server::ItemsCostMoney $= 1 && %client.money < %this.cost && %isThrown !$= 1)
	{
	messageClient(%client, 'MsgNoAfford', '\c4You can\'t afford this Item!');
	}
	if (($Pref::Server::ItemsCostMoney $= 1 && %client.money >= %this.cost) || $Pref::Server::ItemsCostMoney == 0 || %obj.thrown $= 1)
	{
		//If there is an open inventory slot put it there
		%freeslot = -1;
		for(%i = 0; %i < %data.maxItems; %i++)
		{
			//echo(%i);
			if(%player.inventory[%i] == 0)
			{
				%freeslot = %i;
				break;
			}
		}
		if(%freeslot != -1)
		{
			if (%obj.isStatic())
			{
				if(%this.respawn !$= false)
				{
					%obj.respawn();
				}
				else
				{
					%obj.delete();
				}
			}
			else
			{
				%obj.delete();
			}

			%player.inventory[%freeslot] = %this;
			if(%isThrown !$= 1)
			{
			if($Pref::Server::ItemsCostMoney == 1 && %this.cost > 0)
			{
				%client.money -= %this.cost;
				messageclient(%client,"",'\c4You bought \c0%1\c4 for \c0$%2.',%this.pickupName,%this.cost);
				messageClient(%client,'MsgUpdateMoney',"",%client.Money);
			}
			}
			else
			{
				messageclient(%client,"",'\c4You got \c0%1.',%this.pickupName);
			}
			// Inform the client what they got.
			if (%user.client)
				messageClient(%user.client, 'MsgItemPickup', '', %freeslot, %this.invName);
				return true;
		}
		else
		{
			//tell client theres no room
			if (%client)
			messageClient(%client, 'MsgInventoryFull', '\c4You can\'t carry anymore!');
		}
	}

}
//-----------------------------------------------------------------------------
// Hook into the mission editor.

function ItemData::create(%data)
{
   // The mission editor invokes this method when it wants to create
   // an object of the given datablock type.  For the mission editor
   // we always create "static" re-spawnable rotating objects.
   %obj = new Item() {
      dataBlock = %data;
      static = true;
      rotate = %data.rotate;
   };
   %obj.setSkinName(%data.skinName);
   return %obj;
}

function ItemData::onAdd(%this, %obj)
{
	%obj.setSkinName(%this.skinName);
}

function ItemImage::onMount(%this, %obj, %slot)
{
	if(%this.headUp == true)
		%obj.playthread(3, headup);
}

function ItemImage::onUnMount(%this, %obj, %slot)
{
	if(%this.headUp == true)
		%obj.playthread(3, root);
}


//default behavior for wearable items, you use em, they mount an image
function ItemData::onUse(%this, %player, %InvPosition)
{
	//if the image is mounted already, unmount it
	//if it isnt, mount it
	
	%client = %player.client;
	%playerData = %player.getDataBlock();

	%mountPoint = %this.image.mountPoint;
	%mountSlot = %mountPoint;
	%mountedImage = %player.getMountedImage(%mountPoint); 

	if(%this.equipment == true)
	{
		//hats and such
		if(%player.isEquiped[%invPosition] == true)
		{
			//we used a slot that is already equipped
			//turn the slot off and unmount our image
			messageClient(%client, 'MsgDeEquipInv', '', %InvPosition);
			%player.isEquiped[%InvPosition] = false;
			%player.unMountImage(%mountSlot);
		}
		else
		{
			//we used a slot that is not equipped
			//scan the other slots for interfering items
			//turn them off
			//turn the one we used on
			for(%i = 0; %i < %playerData.maxItems; %i++)							//search through other inv slots
			{
				if(%player.isEquiped[%i] == true)									//if it is equipped then
				{
					%checkMountPoint = %player.inventory[%i].image.mountpoint;
					if(%mountPoint == %checkMountPoint)								//if it is mounted on the same point
					{		
						messageClient(%client, 'MsgDeEquipInv', '', %i);			//then de-equip it 
						%player.isEquiped[%i] = false;
						break;														//we're done because only one item can interfere
					}										
				}
			}

			messageClient(%client, 'MsgEquipInv', '', %InvPosition);			//equip our new item
			%player.isEquiped[%InvPosition] = true;
			%player.mountimage(%this.image, %mountPoint, 1, %color);
		}
	}
	else
	{
		//weapons, bricks and such
		if(%player.currWeaponSlot == %invPosition)
		{
			//we've hit the slot that we're currently using as a weapon
			//unmount the image and unhilight
			%player.unmountImage(%mountSlot);
			messageClient(%client, 'MsgHilightInv', '', -1);
			%player.currWeaponSlot = -1;
		}
		else
		{
			//we've hit an unused slot
			//just equip and hilight
			//set current weapon slot

			messageClient(%client, 'MsgHilightInv', '', %InvPosition);
			%player.mountimage(%this.image, %mountPoint, 1, %color);
			%player.currWeaponSlot = %invPosition;
		}
	
	}


}

function Item::setThrower(%this, %newThrower)
{
	%this.thrower = %newThrower;
}