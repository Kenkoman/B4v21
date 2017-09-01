// Powerup for TBM DM.

datablock ItemData(DMShieldPickup)
{
   // Mission editor category, this datablock will show up in the
   // specified category under the "shapes" root category.
   category = "DM";

   // Basic Item properties
   shapeFile = "~/data/shapes/items/DMshield.dts";
   mass = 1;
   friction = 1;
   elasticity = 0.3;
   rotate = true;
   // Dynamic properties defined by the scripts
   maxInventory = 0; // No pickup or throw
};

function DMShieldPickup::onPickup(%this,%obj,%user,%amount)
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
	if (%client.DMShield != 1 && %player.getState() !$= "Dead") 
	{
		%client.DMShield = 1;
		schedule( 30000, 0, resetShieldPickup, %player);
		%obj.startFade(0, 0, true);
		%obj.hide(true);
		%obj.schedule(60000, "hide", false);
		%obj.schedule(60000, "startFade", 1000, 0, false);
		if (%client)
			messageClient(%client, 'MsgDMPickup', "\c2+Invincibility!");
	}
	else
	  %obj.hide(0);
}

function resetShieldPickup( %player )
{
  if (isObject(%player)) 
    %player.client.DMShield = 0;
}
