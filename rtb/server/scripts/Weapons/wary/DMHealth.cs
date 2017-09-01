// Health kits for TBM DM. <---pff, I'll stole it -.-

datablock ItemData(DMHealthPickup)
{
   // Mission editor category, this datablock will show up in the
   // specified category under the "shapes" root category.
   category = "DM";

   // Basic Item properties
   shapeFile = "~/data/shapes/wary/DMaidpack.dts";
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
  if (%user.client.edit) 
    SarumanStaffProjectile::onCollision(%this,%user,%obj,0,0,0);
  %player = %user;
  %client = %player.client;
	if (%player.getDamageLevel() != 0 && %player.getState() !$= "Dead" && !$Pref::Server::AdminInventory) 
	{
		%player.applyRepair(%this.repairAmount);
		%obj.startFade(0, 0, true);
		%obj.hide(true);
		%obj.schedule(15000, "hide", false);
		%obj.schedule(15000, "startFade", 1000, 0, false);
		if (%client)
			messageClient(%client, 'MsgHealthUsed', "\c2+10 Health.");
	}
        else
          %obj.hide(0);
}
