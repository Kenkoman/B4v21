//toybox.cs

//toybox which refills bricklimits.


//////////
// item //
//////////

datablock ItemData(toybox)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon";
	equipment = true;

	//its already a member of item namespace so dont break it
	//className = "Item"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/legobucket.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	rotate = true;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a toybox';
	invName = 'Toy Box';
};


function toybox::onPickup(%this,%obj,%user,%amount) {
  if (%user.client.edit) {
    SarumanStaffProjectile::onCollision(%this,%user,%obj,0,0,0);
    return;
    }
  %obj.hide(1);
  if (%user.client.bricklimit < $Pref::Server::AdminBricklimit && $Pref::Server::AdminBricklimit > 0) {
    if (%user.client.bricklimit > 0 && (%user.client.isadmin || %user.client.issuperadmin)) {
      %obj.hide(0);
      return;
      }
    else {
      %user.client.bricklimit = $Pref::Server::AdminBricklimit;
      messageClient(%user.client, 'MsgBrickLimit', 'Brick Limit Renewed.');
      %obj.schedule(5000,hide,0);
      }
    }
  else
    %obj.hide(0);
}

