// Powerup for TBM DM.

datablock ItemData(DMCapePickup)
{
   // Mission editor category, this datablock will show up in the
   // specified category under the "shapes" root category.
   category = "DM";

   // Basic Item properties
   shapeFile = "~/data/shapes/items/DMcape.dts";
   mass = 1;
   friction = 1;
   elasticity = 0.3;
   rotate = true;
   // Dynamic properties defined by the scripts
   maxInventory = 0; // No pickup or throw
};

function DMCapePickup::onPickup(%this,%obj,%user,%amount)
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
	if (%client.invis != 1 && %player.getState() !$= "Dead") 
	{
		%player.setCloaked(1);
		%player.client.invis = 1;
		schedule( 15000, 0, resetCapePickup, %player); 
		%obj.startFade(0, 0, true);
		%obj.hide(true);
		%obj.schedule(30000, "hide", false);
		%obj.schedule(30000, "startFade", 1000, 0, false);
		if (%client) {
			messageClient(%client, 'MsgDMPickup', "\c2+Invisibility!");
      		        %player.setShapeName(" ");
                  }
	}
        else
          %obj.hide(0);
}

function resetCapePickup(%player)
{
  if (isObject(%player)) {
    %player.setCloaked(0);
    %player.client.invis = 0;
    %player.setShapeName(%player.client.name);
if ($controledspawn){
  if (%player.client.team !$= "")
    %player.setShapeName(%player.client.namebase@" <"@%player.client.team@">");
  else
   %player.setShapeName(%player.client.name);
}
    }
}
