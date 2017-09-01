//////////
// item //
//////////
datablock ItemData(landmine)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/feedback/landmine/mine2.dts";
	skinName = 'black';
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'Landmine';
	invName = "Landmine";
	image = landmineImage;
	threatlevel = "Normal";
};

addWeapon(landmine);

datablock ShapeBaseImageData(landmineImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/feedback/landmine/mine2.dts";
   skinName = 'black';
   emap = true;
    
   mountPoint = 0;
   offset = "0 0 0";
   correctMuzzleVector = true;
   className = "WeaponImage";
   item = Claymore;
   ammo = " ";
   melee = false;
   armReady = true;

   // Initial start up state
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.5;
	stateTransitionOnTimeout[0]       = "Ready";
	stateSound[0]					= weaponSwitchSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateAllowImageChange[1]         = true;

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "Reload";
	stateTimeoutValue[2]            = 0.05;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]			= true;
	stateSound[2]					= arrowExplosionSound;

	stateName[3]			= "Reload";
	stateSequence[3]                = "Reload";
	stateAllowImageChange[3]        = false;
	stateTimeoutValue[3]            = 1.0;
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTimeout[3]     = "Check";

	stateName[4]			= "Check";
	stateTransitionOnTriggerUp[4]	= "StopFire";
	stateTransitionOnTriggerDown[4]	= "Fire";

	stateName[5]                    = "StopFire";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 0.2;
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;
	//stateSequence[5]                = "Reload";
	stateScript[5]                  = "onStopFire";
};

function landmineImage::onMount(%this,%obj)
{
	%obj.playthread(1, armreadyboth);
}

function landmineImage::onUnmount(%this,%obj)
{
	%obj.playthread(1, root);
}

datablock ItemData(landmineMine) {
   shapeFile = "~/data/shapes/feedback/landmine/mine.dts";
   category = "DM";
   mass = 100;
   friction = 1.0;
   elasticity = 0.001;
   rotate = true;
   rotation = "-1 0 0 90";
   damageRadius = 7;
   radiusDamage = 100;
   damagetype = '%1 got blowed to bits by %2\'s claymore trip mine!';
   colidable=true;
   sticky = true;
 
   // Dynamic properties defined by the scripts
   maxInventory = 0;
	pickUpName = 'Claymore... BOOM!';
	invName = 'Claymore';
};

//Static Shape
function landmineMine::onCollision(%this,%obj,%col,%fade,%pos,%normal) {
if (%col.getdatablock() !$= "")
  if (%col.getdatablock().classname $= "armor") {
    if(!%obj.client)
      %obj.client = %col.client;
    LandMineMine.detonate(%obj);
    }
}

function LandmineMine::Detonate(%this, %obj) {
%pos = %obj.getPosition();
%obj.triggered = 1;
%s = mPow(getWord(%obj.getScale(), 0) * getWord(%obj.getScale(), 1) * getWord(%obj.getScale(), 2), 0.333);
 tbmradiusDamage
 (%obj, VectorAdd(%pos, VectorScale(%normal, 0.01)),
 %obj.damageRadius * %s, %obj.radiusDamage * %s, %obj.damageType,100);

createExplosion(nameToID(GLExplosion), %obj.getPosition());
%obj.schedule(10, delete);
}

function LandMineMine::damage(%this, %obj, %sourceobj, %pos, %dmg, %type) {
if(%dmg <= 0)
  return;
%obj.damage += %dmg;
if(%obj.damage > (20 * (getWord(%obj.getScale(), 0) + getWord(%obj.getScale(), 1) + getWord(%obj.getScale(), 2))) && !%obj.triggered) {
  if(!%obj.client)
    %obj.client = %sourceObj.client;
  %obj.triggered = 1;
  LandMineMine.detonate(%obj);
}
}

landMineMine.damageable = 1;

function landmineMine::onPickup(%this,%obj,%user,%amount) {
%obj.hide(0);
}

function landmineImage::onFire(%this,%obj,%slot) {
	%player = %obj.client.player;
	%client = %obj.client;
	%item = nametoid(landmineMine);
	if(%item && %player)
	{
		//throw the item
        %hmm = "0 0 "@getword(%player.getscale(),2);
        %hmm = vectoradd(%hmm,"0 0 0.8");
		%muzzlepoint = Vectoradd(%player.getposition(), %hmm);
		%muzzlevector = vectorScale(%player.getEyeVector(), mPow(getWord(%player.getScale(), 0) * getWord(%player.getScale(), 1) * getWord(%player.getScale(), 2), 0.333));
		%muzzlepoint = VectorAdd(%muzzlepoint, %muzzleVector);
		%playerRot = rotFromTransform(%player.getTransform());
		%thrownItem = new (item)()
		{
		datablock = %item;
		sourceObject     = %obj;
		sourceSlot       = %slot;
		client           = %obj.client;
		deathAnim = "explosion";
		};
		%thrownItem.setscale(%player.getscale());
		MissionCleanup.add(%thrownItem);
	        %thrownItem.client = %player.client;
		%thrownItem.damageRadius = %item.damageRadius;
		%thrownItem.radiusDamage = %item.radiusDamage;
		%thrownItem.damagetype = %item.damagetype;

		%muzzleVelocity = VectorScale(%muzzleVector, 25);
		%thrownItem.settransform(%muzzlepoint @ " " @ %playerRot);
		%thrownItem.setVelocity(%muzzleVelocity );
		%thrownItem.setCollisionTimeout(%player);
		%obj.client.minecount++;
//Stupid way of doing it, but while didn't work right, and I've got a headache
		if(!isobject(%client.mine[0])) {
			%client.mine[0] = %client.mine[1];
			%client.mine[1] = %client.mine[2];
			%client.mine[2] = %client.mine[3];
			%client.mine[3] = %client.mine[4];
			%client.mine[4] = 0;
		}
		if(!isobject(%client.mine[1])) {
			%client.mine[1] = %client.mine[2];
			%client.mine[2] = %client.mine[3];
			%client.mine[3] = %client.mine[4];
			%client.mine[4] = 0;
		}
		if(!isobject(%client.mine[2])) {
			%client.mine[2] = %client.mine[3];
			%client.mine[3] = %client.mine[4];
			%client.mine[4] = 0;
		}
		if(!isobject(%client.mine[3])) {
			%client.mine[3] = %client.mine[4];
			%client.mine[4] = 0;
		}
		if(!isobject(%client.mine[4])) {
			%client.mine[4] = 0;
		}
//That should re-order everything
//This here should put it in the first empty slot
		if(%client.mine[0] == 0) {
			%client.mine[0] = %thrownitem;
			return;
		}
		if(%client.mine[1] == 0) {
			%client.mine[1] = %thrownitem;
			return;
		}
		if(%client.mine[2] == 0) {
			%client.mine[2] = %thrownitem;
			return;
		}
		if(%client.mine[3] == 0) {
			%client.mine[3] = %thrownitem;
			return;
		}
		if(%client.mine[4] == 0) {
			%client.mine[4] = %thrownitem;
			return;
		}
//Okay, so if there are no empty slots, remove the oldest one
		%client.mine[0].delete();
		%client.mine[0] = %client.mine[1];
		%client.mine[1] = %client.mine[2];
		%client.mine[2] = %client.mine[3];
		%client.mine[3] = %client.mine[4];
		%client.mine[4] = %thrownitem;
    }
}