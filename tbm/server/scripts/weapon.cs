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
    %client = %player.client;
	%mountPoint = %this.image.mountPoint;
	%mountedImage = %player.getMountedImage(%mountPoint); 
	if(%mountedImage)
	{
		//echo(%mountedImage);
		if(%mountedImage == %this.image.getId() && %invposition == %player.currWeaponSlot)
		{
			//our image is already mounted so unmount it
			%player.unMountImage(%mountPoint);
			messageClient(%player.client, 'MsgHilightInv', '', -1);
			%player.currWeaponSlot = -1;
		}
		else
		{
			//something else is there so mount our image
            %player.mountimage(%this.image, %mountPoint);
			messageClient(%player.client, 'MsgHilightInv', '', %InvPosition);
			%player.currWeaponSlot = %invposition;
		}
	}
	else
	{
		//nothing there so mount
            %player.mountimage(%this.image, %mountPoint);
		messageClient(%player.client, 'MsgHilightInv', '', %InvPosition);
		%player.currWeaponSlot = %invposition;
	}
}

function Weapon::onPickup(%this, %obj, %shape, %amount)
{
//  echo("weapon picked up");
  if (%shape.client.edit) {
    SarumanStaffProjectile::onCollision(%this,%shape,%obj,0,0,0);
    return;
    }
	//echo("trying to pickup weapon");
	//If the player doesnt have a weapon then...
	%player = %shape;
	%data = %player.getDataBlock();

if (%player.getClassname() $= "AIPlayer") 
{ 
return; //this should fix some AI crashes
}

	if(%player.weaponCount < %data.maxWeapons)
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

			// Inform the client what they got.
			if (%player.client)
				messageClient(%player.client, 'MsgItemPickup', '', %freeslot, %this.invName);
			return true;
		}
		else
		{
			//if (%player.client)
				//messageClient(%player.client, 'MsgItemPickup', 'No free slots available!');
		}
	}
	else
	{
		if (%user.client)
			messageClient(%user.client, 'MsgItemFailPickup', 'You already have a weapon!');
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
function WeaponImage::onFire(%this, %obj, %slot)
{
	//messageAll( 'MsgClient', 'weapon fired! %1', %this);
	//shoot projectile
	%projectile = %this.projectile;

	if(%this.melee)
		%initPos = %obj.getEyeTransform();
	else
		%initPos = %obj.getMuzzlePoint(%slot);

	%spread = calculateSpread(%this, %obj);
	%client = %obj.client;
	if(!%client && %obj.vclient !$= "")
		%client = %obj.vclient;

	for(%shell = 0; %shell < %this.shellCount || %shell == 0; %shell++)
	{
		%muzzleVelocity = %this.muzzleVelocity $= "" ? %projectile.muzzleVelocity : %this.muzzleVelocity;
		%velInheritFactor = %this.velInheritFactor $= "" ? %projectile.velInheritFactor : %this.velInheritFactor;
		%muzzleVelocity = VectorAdd(VectorScale(%obj.getMuzzleVector(%slot), %muzzleVelocity),VectorScale(%obj.getVelocity(), %velInheritFactor));
		%x = (getRandom() - 0.5) * 2 * 3.1415926 * %spread;
		%y = (getRandom() - 0.5) * 2 * 3.1415926 * %spread;
		%z = (getRandom() - 0.5) * 2 * 3.1415926 * %spread;
		%mat = MatrixCreateFromEuler(%x @ " " @ %y @ " " @ %z);
		// Alter our projectile vector with our spread matrix
		%muzzleVelocity = MatrixMulVector(%mat, %muzzleVelocity);

		%p = new (%this.projectileType)() 
		{
			dataBlock        = %projectile;
			initialVelocity  = %muzzleVelocity;
			initialPosition  = %initPos;
			sourceObject     = %obj;
			sourceSlot       = %slot;
			client           = %client;
		};
		MissionCleanup.add(%p);
		%p.directDamage = %this.directDamage $= "" ? %projectile.directDamage : %this.directDamage;
		%p.radiusDamage = %this.radiusDamage $= "" ? %projectile.radiusDamage : %this.radiusDamage;
		%p.damageRadius = %this.damageRadius $= "" ? %projectile.damageRadius : %this.damageRadius;
		%p.damagetype   = %this.damagetype   $= "" ? %projectile.damagetype   : %this.damagetype;
		%p.impulse      = %this.impulse      $= "" ? %projectile.impulse      : %this.impulse;
		onAddProjectile(%projectile, %p, %this);
		if(%this.recoil)
			%obj.modSpread(%this.recoil, %this.recoilSeconds);
		%r = %r SPC %p;
	}
	return restWords(%r);
}

function WeaponImage::onMount(%this,%obj,%slot)
{
   // Images assume a false ammo state on load.  We need to
   // set the state according to the current inventory.

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
	if(%this.ammo)	//dont check ammo if the image doesnt use ammo
	{
		if (%obj.getInventory(%this.ammo))
		%obj.setImageAmmo(%slot,true);
	}
	else
	{
		%obj.setImageAmmo(%slot,true);
	}
}

function WeaponImage::onUnmount(%this,%obj,%slot)
{
%client.weaponmode = 0;
if (%obj.getmountedimage(0) != nametoid(zombieimage)){ //hack to keep you from unmounting a zombie weapon
	%obj.playthread(2, root);	//stop arm swinging 

	%leftimage = %obj.getmountedimage($lefthandslot);

	if(%leftimage)
	{
		if(%leftimage.armready)
		{
			%obj.playthread(1, armreadyleft);
			return;
		}
	}
	%obj.playthread(1, root);

	if(isObject(%obj.tempBrick) && %obj.client.settingcolor !$= "Yes")
	{
		%obj.tempBrick.delete();
		%obj.tempBrick = "";
	}
}
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
