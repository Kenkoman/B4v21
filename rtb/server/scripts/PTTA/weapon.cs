//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

// This file contains Weapon and Ammo Class/"namespace" helper methods
// as well as hooks into the inventory system. These functions are not
// attached to a specific C++ class or datablock, but define a set of
// methods which are part of dynamic namespaces "class". The Items
// include these namespaces into their scope using the  ItemData and
// ItemImageData "className" variable.

// All ShapeBase images are mounted into one of 8 slots on a shape.
// This weapon system assumes all primary weapons are mounted into
// this specified slot:
$WeaponSlot = 0;
$War = 0;

//-----------------------------------------------------------------------------
// Weapon Class 
//-----------------------------------------------------------------------------

//generic weapon sounds
datablock AudioProfile(weaponSwitchSound)
{
   filename    = "~/data/sound/weaponSwitch.wav";
   description = AudioClosest3d;
   preload = true;
};


function Weapon::onUse(%this, %player, %InvPosition)
{
   // Default behavoir for all weapons is to mount it into the
   // this object's weapon slot, which is currently assumed
   // to be slot 0
   //if (%obj.getMountedImage($WeaponSlot) != %data.image.getId()) {
   //   %obj.mountImage(%data.image, $WeaponSlot);
   //   if (%obj.client)
   //      messageClient(%obj.client, 'MsgWeaponUsed', '\c0Weapon selected');
   //}

	//if the image is mounted already, unmount it
	//if it isnt, mount it

	%mountPoint = %this.image.mountPoint;
	%mountedImage = %player.getMountedImage(%mountPoint); 
	
	if(%mountedImage)
	{
		//echo(%mountedImage);
		if(%mountedImage == %this.image.getId())
		{
			//our image is already mounted so unmount it
			%player.unMountImage(%mountPoint);
			messageClient(%player.client, 'MsgHilightInv', '', -1);
			%player.currWeaponSlot = -1;
		}
		else
		{
			//something else is there so mount our image
			%player.mountimage(%this.image, %mountPoint,false,$legoColor[%player.client.colorIndex]);

			messageClient(%player.client, 'MsgHilightInv', '', %InvPosition);
			%player.currWeaponSlot = %invposition;
		}
	}
	else
	{
		//nothing there so mount 
		%player.mountimage(%this.image, %mountPoint,false,$legoColor[%player.client.colorIndex]);

		messageClient(%player.client, 'MsgHilightInv', '', %InvPosition);
		%player.currWeaponSlot = %invposition;
	}
}

function Weapon::onPickup(%this, %obj, %shape, %amount)
{
	%player = %shape;
	%data = %player.getDataBlock();
	%client = %player.client;
	if(%player.weaponCount < %data.maxWeapons)
	{
if(%client.WantBuy != 1 && $Pref::Server::ItemsCostMoney $= 1 && %this.cost > 0)
	{
bottomPrint(%client,"Press <B> to Buy this Weapon!",0.5,1);
	}
	if($Pref::Server::ItemsCostMoney $= 1 && %client.money < %this.cost && %client.WantBuy $= 1)
	{
	messageClient(%client, 'MsgNoAfford', '\c4You can\'t afford this Item!');
	}
		if (($Pref::Server::ItemsCostMoney == 1 && %client.money >= %this.cost && %client.WantBuy == 1)|| $Pref::Server::ItemsCostMoney == 0)
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
					%obj.respawn();
				else
					%obj.delete();
	
				%player.weaponCount++;
				%player.inventory[%freeslot] = %this;
				if($Pref::Server::ItemsCostMoney == 1 && %this.cost > 0)
				{
					%client.money -= %this.cost;
					messageclient(%client,"",'\c4Paid \c0$%1\c4 for \c0%2',%this.cost,%this.pickUpName);
					messageClient(%client,'MsgUpdateMoney',"",%client.Money);
				}
 
				// Inform the client what they got.
				if (%client)
					messageClient(%client, 'MsgItemPickup', '', %freeslot, %this.invName);
				return true;
			}
			else
			{
			if (%client)
			messageClient(%client, 'MsgItemPickup', '\c4No free slots available!');
			}
		}
	}
	else
	{
	if (%client)
	messageClient(%client, 'MsgItemFailPickup', '\c4You already have enough weapons!');
	}
	

   // The parent Item method performs the actual pickup.
   // For player's we automatically use the weapon if the
   // player does not already have one in hand.
  // if (Parent::onPickup(%this, %obj, %shape, %amount)) {
  //    if (%shape.getClassName() $= "Player" && 
  //          %shape.getMountedImage($WeaponSlot) == 0)  {
  //       %shape.use(%this);
  //    }
  // }
}

function Weapon::onInventory(%this,%obj,%amount)
{
   // Weapon inventory has changed, make sure there are no weapons
   // of this type mounted if there are none left in inventory.
   if (!%amount && (%slot = %obj.getMountSlot(%this.image)) != -1)
      %obj.unmountImage(%slot);
}   


//-----------------------------------------------------------------------------
// Weapon Image Class
//-----------------------------------------------------------------------------
function WeaponImage::onFire(%this,%obj,%slot)
{
	//messageAll( 'MsgClient', 'weapon fired! %1', %this);

	//shoot projectile
	%projectile = %this.projectile;

if(%obj.client.isImprisoned)
{
return;
}

if(%this.item !$= "wand" && %this.item !$= "axe" && %this.item !$= "wrench" && %this.item !$= "sprayCan" && %this.item !$= "letterCan" && %this.item !$= "blackletterCan" && %projectile !$= "brickDeployProjectile" && %projectile !$= "hammerProjectile" && %obj.client.safe == 1)
		{
			return;
		}	

	if(%this.melee)
	{
		%initPos = %obj.getEyeTransform();
		//%muzzleVector = %obj.getEyeVector();
		%muzzleVector = %obj.getMuzzleVector(%slot);
	}
	else
	{
		%initPos = %obj.getMuzzlePoint(%slot);
		%muzzleVector = %obj.getMuzzleVector(%slot);
	}

	
	%objectVelocity = %obj.getVelocity();
	%muzzleVelocity = VectorAdd(
	  VectorScale(%muzzleVector, %projectile.muzzleVelocity),
	  VectorScale(%objectVelocity, %projectile.velInheritFactor));

	%p = new (%this.projectileType)() 
	{
		dataBlock        = %projectile;
		initialVelocity  = %muzzleVelocity;
		initialPosition  = %initPos;
		sourceObject     = %obj;
		sourceSlot       = %slot;
		client           = %obj.client;
	};
	MissionCleanup.add(%p);
	return %p;
}

function WeaponImage::onMount(%this,%obj,%slot)
{
   // Images assume a false ammo state on load.  We need to
   // set the state according to the current inventory.
	commandtoClient(%obj.client,'showBrickImage',"");
	//fixArmReady(%obj);
	if(%this.armReady)
	{
		if(%obj.getMountedImage($LeftHandSlot))
		{
			if(%obj.getMountedImage($LeftHandSlot).armReady)
				%obj.playthread(1, armReadyBoth);
			else
				%obj.playthread(1, armReadyRight);
		}
		else
		{
			%obj.playthread(1, armReadyRight);
		}
	}
	%thisdata = %this;

	commandtoClient(%obj.client,'showBrickImage',%this.PreviewFileName);

	if(%this.ammo)	//dont check ammo if the image doesnt use ammo
	{
		if (%obj.getInventory(%this.ammo))
		%obj.setImageAmmo(%slot,true);
	}
	else
	{
		%obj.setImageAmmo(%slot,true);
	}

		%player = %obj.client.player;
		%image = %player.getMountedImage(0);	//the thing the guy is holding
		%static = %image.staticShape;
		%ghost = %static;			//ghost is what we're going to deploy


		if(getSubStr(%static, 0, 6) !$= "static")
		{
		if(isObject(%player.tempBrick))
		{
		%player.tempBrick.delete();
		%player.tempBrick = "";
		}
		}

		if(isObject(%player.tempBrick))
		{
		%EulerRot = %player.tempBrick.EulerRot;
		%location = %player.tempBrick.getTransform();
		%player.tempBrick.delete();
		%d = new StaticShape()
		{
			datablock = %ghost;
		};
		MissionCleanup.add(%d);
		%d.setTransform(%location);
		%d.setScale("1 1 1");
		%d.setSkinName('construction');
		%d.isBrickGhost = 1;
		%d.EulerRot = %EulerRot;
		%player.tempBrick = %d;
		}


}

function WeaponImage::onUnmount(%this,%obj,%slot)
{
	%player = %obj.client.player;

	if(%this.editorWand $= "1")
	{
		if(isObject(%player.tempBrick))
		{
			if(%player.tempBrick.isBrickGhostMoving $= 1)
			{
				%obj.client.SelectedObject = "";
				messageClient(%obj.client,'',"\c4You are out of \c0Edit \c4Mode.");
				%player.tempBrick.setSkinName(%player.tempBrick.oldskinname);
				%player.tempBrick.isBrickGhost = "";
				%player.tempBrick.isBrickGhostMoving = "";
				%player.tempBrick = "";
			}
			else
			{
				%player.tempBrick.delete();
				%player.tempBrick = "";
			}
		}
	}

	%obj.playthread(2, root);	//stop arm swinging 

	%leftimage = %obj.getmountedimage($lefthandslot);
	commandtoClient(%obj.client,'showBrickImage',"");
	if(%leftimage)
	{
		if(%leftimage.armready)
		{
			%obj.playthread(1, armreadyleft);
			return;
		}
	}
	%obj.playthread(1, root);
}


//-----------------------------------------------------------------------------
// Ammmo Class
//-----------------------------------------------------------------------------

function Ammo::onInventory(%this,%obj,%amount)
{
   // The ammo inventory state has changed, we need to update any
   // mounted images using this ammo to reflect the new state.
   for (%i = 0; %i < 8; %i++) {
      if ((%image = %obj.getMountedImage(%i)) > 0)
         if (isObject(%image.ammo) && %image.ammo.getId() == %this.getId())
            %obj.setImageAmmo(%i,%amount != 0);
   }
}   
function weaponDamage(%obj,%col,%this,%pos,%type)
{
	echo(%type);
	if($Pref::Server::Weapons == 1)
	{
		%player = %obj.client.player;
	
		if(%col.getClassName() !$="StaticShape" && %col.getClassName() !$= "Player" && %col.getClassName() !$= "AIPlayer")
		return;
		%colData = %col.getDataBlock();
		%colDataClass = %colData.classname;
		if(%colData.classname !$= "Brick" && %col.getClassName() !$= "Player" && %col.getClassName() !$= "AIPlayer")
		{
			return;
		}
		if(%col.client.safe == 0 && !%col.client.isGod)
		{
			if (%col.getClassName() $= "Player" || %col.getClassName() $= "AIPlayer")
			{
	if(%col.client.team $= %obj.client.team && $Pref::Server::DMFriendlyFire == 0 && %obj.client.team!$="")
				{
					return;
				}
				%col.targetObj = %player;
				%damagedone = %this.directDamage;
				if(%col.getMountedImage($LeftHandSlot) == shieldImage.getId())
				{
					%damagedone = %damagedone/2;
				}
				if(%col.getMountedImage($HeadSlot) == pointyHelmetImage.getId())
				{	
					%damagedone = %damagedone/2;
				}
				if(%col.getMountedImage($BackSlot) == plateMailImage.getId())
				{
					%damagedone = %damagedone/2;
				}
				if(%col.getMountedImage($HeadSlot) == HelmetImage.getId())
				{
					%damagedone = %damagedone/3;
				}
				
				%col.damage(%obj,%pos,%damagedone,%type);
			}
		}
	}

}

//-----------------------------------------------------------------------------
// Custom Shifting Here:
//-----------------------------------------------------------------------------

function servercmdadmintogglecustoms(%client)
{
	if(%client.isAdmin || %client.isSuperAdmin || %client.isTempAdmin)
	{
		if($Pref::Server::CustomShifting $= 1)
		{
			$Pref::Server::CustomShifting = 0;
			messageAll('name', '\c3Custom Rotation and Scaling is now \c0OFF\c3.');
			commandtoclient(%client,'rotiscompatible',0);
		}
		else
		{
			$Pref::Server::CustomShifting = 1;
			messageAll('name', '\c3Custom Rotation and Scaling is now \c0ON\c3.');
			commandtoclient(%client,'rotiscompatible',2);
		}
	}
}
function ServerCmdRotCompatibility( %client )
{
	if($Pref::server::CustomShifting $= 1)
	{
	commandtoclient(%client,'rotiscompatible',2);
	}
	else
	{
	commandtoclient(%client,'rotiscompatible',0);
	}
}

function ServerCmdcustomscalebrick(%client, %x, %y, %z)
{
	if(%x >= "-250" && %x <= "250")
	{
	if(%y >= "-250" && %y <= "250")
	{
	if(%z >= "-250" && %z <= "250")
	{
	if($Pref::Server::CustomShifting $= 1)
	{
	%player = %client.player;
	if(%player.tempBrick)
	{
	%tempBrick = %player.tempBrick;
	%scale = %tempBrick.getScale();
	%scaleX = getWord(%scale,0);
	%scaleY = getWord(%scale,1);
	%scaleZ = getWord(%scale,2);
	%finalzscale = %scaleZ + %z;
	%finalyscale = %scaley + %y;
	%finalxscale = %scalex + %x;
	if(%finalzscale <= "0")
		%finalzscale = "1";
	if(%finalzscale > "1000")
		%finalzscale = "1000";
	if(%finalxscale <= "0")
		%finalxscale = "1";
	if(%finalxscale > "1000")
		%finalxscale = "1000";
	if(%finalyscale <= "0")
		%finalyscale = "1";
	if(%finalyscale > "1000")
		%finalyscale = "1000";
	%tempBrick.customscale = %finalxscale SPC %finalyscale SPC %finalzscale;
	%tempBrick.setScale(%finalxscale SPC %finalyscale SPC %finalzscale);
	}
	}
	}
	}
	}
}

function ServerCmdCustomRotateBrick( %client, %RotX, %RotY, %RotZ)
{
	if($Pref::Server::CustomShifting $= 1)
	{
	if(%client.player.tempBrick.FXMode >= 1 || %client.player.tempBrick.isMoverGhost $= 1)
	   return;
	%player = %client.player;
	%tempBrick = %player.tempBrick;
	if(!isObject(%tempBrick))
	{
	return;
	}
	if(%tempBrick.Datablock $= Staticbrick2x2FX)
	{
	return;
	}

	%tempbrick.setSkinName('construction');


	if(%tempBrick)
	{
		%OldRot = %tempBrick.EulerRot;
		if(%OldRot $= "")
		{
		%OldRot = "0 0 0";
		}
		%OldRotX = getWord(%OldRot, 0);
		%OldRotY = getWord(%OldRot, 1);
		%OldRotZ = getWord(%OldRot, 2);

		%NewRotX = %OldRotX + %RotX;
		%NewRotY = %OldRotY + %RotY;
		%NewRotZ = %OldRotZ + %RotZ;

		%tempBrick.settransform(getwords(%tempBrick.gettransform(),0,2) SPC eulertoquat(%NewRotX SPC %NewRotY SPC %NewRotZ));
		%tempBrick.EulerRot = %NewRotX SPC %NewRotY SPC %NewRotZ;
		}
	}
}