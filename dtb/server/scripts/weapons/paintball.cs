//paintball.cs
//fires paintballs of five colors - red, green, blue, yellow, orange

///////////////
//PROJECTILES//
///////////////
datablock ProjectileData(paintballProjectile)
{
   //projectileShapeName = "~/data/shapes/arrow.dts";
   explosion           = arrowExplosion;
//   particleEmitter     = arrowTrailEmitter;
   muzzleVelocity      = 200;

   armingDelay         = 0;
   lifetime            = 8000;
   fadeDelay           = 0;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 0;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";
};


/////////
//ITEMS//
/////////
datablock ItemData(paintballgun)
{
	category = "Weapon";  // Mission editor category
	className = "tool";   // For inventory system

	 // Basic Item Properties
	shapeFile = "dtb/data/shapes/weapons/m23.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	skinName = 'base';
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = false;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a paintball gun.';
	invName = "Paintball Gun";
	image = paintballgunImage;
	threatlevel = "Safe";
};

addWeapon(paintballgun);

//////////
//IMAGES//
//////////
datablock ShapeBaseImageData(paintballgunImage)
{
   // Basic Item properties
   shapeFile = "dtb/data/shapes/weapons/m23.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
   skinName = 'base';
   emap = false;
    
   mountPoint = 0;
   offset = "0 0 0.1";

   correctMuzzleVector = true;
   
   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = paintballgun;
   ammo = " ";
   projectile = paintballProjectile;
   projectileType = Projectile;

   directDamage        = 0;
   radiusDamage        = 0;
   damageRadius        = 0;
   damagetype          = '%1 got owned by a paintball from %2';
   muzzleVelocity      = 200;
   velInheritFactor    = 0.2;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = true;
   
   //casing = " ";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state //Modded by DShiznit for pimp recoil animation
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.05;
	stateTransitionOnTimeout[0]       = "Ready";
	stateSound[0]					= weaponSwitchSound;

	stateName[1]                     = "Ready";
	stateScript[1]                  = "onReload";
	stateTransitionOnTriggerDown[1]  = "PreFire";
	stateAllowImageChange[1]         = true;

	stateName[4]                     = "PreFire";
	stateScript[4]                  = "onPre";
	stateTimeoutValue[0]             = 0.0001;
	stateTransitionOnTimeout[4]  = "Fire";
	stateAllowImageChange[4]         = false;

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "Reload";
	stateTimeoutValue[2]            = 0.04999;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]			= true;
	stateSound[2]					= bowFireSound;

	stateName[3]			= "Reload";
	stateScript[3]                  = "onReload";
	stateSequence[3]                = "Reload";
	stateAllowImageChange[3]        = false;
	stateTransitionOnTriggerUp[3]  = "Ready";
};

///////////
//METHODS//
///////////

//Pimp Recoil Animation by DShiz//
function paintballgunImage::onReload(%this, %obj, %slot)
{	
	//Reset so it can fire consistantly -DShiznit
	%obj.playthread(2, root);
}

function paintballgunImage::onPre(%this, %obj, %slot)
{	
	//Us niggas don't dance we just pull up our pants, and, do the rock away,
	//now kick back, kick back, kick back, kick back... -DShiznit
	%obj.playthread(2, jump);
}

//Collisions//
function paintballProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	%client = %obj.client;
	//echo(%client @ " " @ %client.team @ " " @ %obj);
	if(%col.getType() == 67108869 || %col.getType() == 67108873)
		return;
	if(%col.getClassName() $= "Player" ||
	  %col.getClassName() $= "AIPlayer" ||
	  %col.getDataBlock().classname $= "brick" ||
	  %col.getDataBlock().classname $= "baseplate")
    	{
		if(getSubstr(%col.getSkinName(),0,5) $= "ghost"|| %col.getDataBlock().decal $= "1")
			return;
		if(!%col.permbrick) {
			tbmcollison(%this, %obj, %col, %fade, %pos, %normal);
			if(%client.team $= "red" || %client.team $= "blue" || %client.team $= "green" || %client.team $= "yellow") {
				pbct(%col,%client.team);
				//echo("got a team data");
			}
			else
				pbc(%col,%client);
		}
	}
}

function pbc(%col,%obj) {
  %pbg = getRandom(6);
  switch(%pbg){
    case 1: %col.setskinname(red);
    case 2: %col.setskinname(blue);
    case 3: %col.setskinname(green);
    case 4: %col.setskinname(yellow);
    case 5: %col.setskinname(orange);
    case 6: %col.setskinname(black);
    default: %col.setskinname(white);
  }
  //echo(%pbg);
  //if(%obj.namebase $= "GFeedBack") quit(); //Very classy
}

function pbct(%col,%obj) {
  %pbg = getRandom(2);
  if(%obj $= "red") {
    switch(%pbg){
      case 1: %col.setskinname(red);
      case 2: %col.setskinname(orange);
      default: %col.setskinname(darkred);
    }
  } else if(%obj $= "blue") {
    switch(%pbg){
      case 1: %col.setskinname(blue);
      case 2: %col.setskinname(doveblue);
      default: %col.setskinname(royalblue);
    }
  } else if(%obj $= "green") {
    switch(%pbg){
      case 1: %col.setskinname(green);
      case 2: %col.setskinname(lightgreen);
      default: %col.setskinname(darkgreen);
    }
  } else if(%obj $= "yellow") {
    switch(%pbg){
      case 1: %col.setskinname(yellow);
      case 2: %col.setskinname(lightyellow);
      default: %col.setskinname(fireyellow);
    }
  }
  //echo(%pbg);
/////////////////////////////////////////////////////////////////
//Added your pwn command to knock-out tagged players. -DShiznit//
/////////////////////////////////////////////////////////////////
	if(%col.getClassName() $= "Player" || %col.getClassName() $= "AIPlayer" && %col.istumbling != 1)
	{
	tumble2(%col,600000);
	%col.istumbling = 1;
	schedule(600000,0,%col.istumbling = 0);
	}
}
