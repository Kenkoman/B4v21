// Powerup for TBM DM.

datablock ItemData(DMArmorPickup)
{
   // Mission editor category, this datablock will show up in the
   // specified category under the "shapes" root category.
   category = "DM";

   // Basic Item properties
   shapeFile = "~/data/shapes/items/DMarmorblue.dts";
   mass = 1;
   friction = 1;
   elasticity = 0.3;
   rotate = true;
   // Dynamic properties defined by the scripts
   maxInventory = 0; // No pickup or throw
};

function DMArmorPickup::onPickup(%this,%obj,%user,%amount)
{
  if (%user.client.bodytype==666) {
    %obj.hide(0);
    return;
    }
  if (%user.client.edit)  {
    SarumanStaffProjectile::onCollision(%this,%user,%obj,0,0,0);
    return;
    }
  if(!computePurchase(%user, %obj)) return;
  %player = %user;
  %client = %player.client;
	if (%client.DMArmor != 1 && %player.getState() !$= "Dead")
	{
		%client.DMArmor = 1;
		schedule( 30000, 0, resetArmorPickup, %player);
		%obj.startFade(0, 0, true);
		%obj.hide(true);
		%obj.schedule(40000, "hide", false);
		%obj.schedule(40000, "startFade", 1000, 0, false);
		if (%client)
			messageClient(%client, 'MsgHealthUsed', "\c2+Armor!");
	}
        else
          %obj.hide(0);
}

function resetArmorPickup( %player )
{
  if (isObject(%player)) 
    %player.client.DMArmor = 0;

}
