// Health kits for TBM DM.

datablock ItemData(DMHealthPickup)
{
   // Mission editor category, this datablock will show up in the
   // specified category under the "shapes" root category.
   category = "DM";

   // Basic Item properties
   shapeFile = "~/data/shapes/items/DMaidpack.dts";
   mass = 1;
   friction = 1;
   elasticity = 0.3;
   rotate = true;
   // Dynamic properties defined by the scripts
   repairAmount = 25;
   maxInventory = 0; // No pickup or throw
};

function DMHealthPickup::onPickup(%this,%obj,%user,%amount)
{
  if (%user.client.bodytype==666) {
    %obj.hide(0);
    return;
    }
  if(!computePurchase(%user, %obj)) return;  
  if (%user.client.edit)  {
    SarumanStaffProjectile::onCollision(%this,%user,%obj,0,0,0);
    return;
    }
  %player = %user;
  %client = %player.client;
	if (%player.getDamageLevel() != 0 && %player.getState() !$= "Dead")
	{
		%player.applyRepair(%this.repairAmount);
		%obj.startFade(0, 0, true);
		%obj.hide(true);
		%obj.schedule(15000, "hide", false);
		%obj.schedule(15000, "startFade", 1000, 0, false);
		if (%client)
			messageClient(%client, 'MsgHealthUsed', "\c2+25 Health.");
	}
        else
          %obj.hide(0);
}

datablock ItemData(HealthKit)
{
   // Mission editor category, this datablock will show up in the
   // specified category under the "shapes" root category.
   category = "DM";
   className = "Tool";

   // Basic Item properties
   shapeFile = "~/data/shapes/items/HealthKit/DMaidpack.dts";
   mass = 1;
   friction = 1;
   elasticity = 0.3;

   // Dynamic properties defined by the scripts
   pickupName = "a health kit";
   invName = "Health Kit";
   ShizRepair = 50;
   maxInventory = 1;
};

function HealthKit::onUse(%this,%user)
{
   // Apply some health to whoever uses it, the health kit is only
   // used if the user is currently damaged.
   if (%user.getDamageLevel() != 0 && %user.getState() !$= "Dead") {
    removefrominventory(%user,HealthKit);
      %user.applyRepair(%this.ShizRepair);
      if (%user.client)
         messageClient(%user.client, 'MsgHealthUsed', '\c2Health Kit Applied');
   }
}