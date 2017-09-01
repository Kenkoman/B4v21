//Loudhailer.cs
//Before you ask, this weapon is now unused.
//It used to fire brick-esque things, and it used your brick count as ammo.

//////////
// item //
//////////
datablock ItemData(Loudhailer)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/weapons/Loudhailer.dts";
	skinName = 'black';
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'Loudhailer';
	invName = "Loudhailer";
	image = LoudhailerImage;
};


datablock ShapeBaseImageData(LoudhailerImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/weapons/Loudhailer.dts";
   skinName = 'black';
   emap = true;
    
   mountPoint = 0;
   offset = "0 0 0";
   correctMuzzleVector = true;
   className = "WeaponImage";
   item = Loudhailer;
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
	stateTimeoutValue[3]            = 0.5;
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

datablock ItemData(transpro)
{
	shapeFile = "tbm/data/shapes/bricks/trans1x1round.dts";
//	image = trans1x1roundImage;
   category = "DM";
   mass = 1;
   friction = 1;
   elasticity = 0.5;
   rotate = true;
   rotation = "-1 0 0 90";
 
   // Dynamic properties defined by the scripts
   maxInventory = 0;
	pickUpName = 'you got some ammo.';
	invName = 'Hailer Ammo';
};

//Static Shape
function transpro::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
if (%col.getdatablock() !$= "")
  if (%col.getdatablock().classname $= "armor") {
    tbmcollison(%obj,%obj,%col,%fade,%pos,%normal);
    %obj.delete();
    }
}

function transpro::onPickup(%this,%obj,%user,%amount)
{
%obj.hide(0);
}
function LoudhailerImage::onFire(%this,%obj,%slot)
{

	%player = %obj.client.player;
        if (%player.client.bricklimit<=4)
          {
          %player.client.bricklimit = 0;
          messageClient(%player.client, 'MsgBrickLimit', 'You are out of ammo.');
          return;
          }
        else
          %player.client.bricklimit -= 5;
	%item = nametoid(transpro);
	if(%item && %player)
	{
		//throw the item
                %hmm = "0 0 "@getword(%player.getscale(),2);
                %hmm = vectoradd(%hmm,"0 0 0.8");
                %mcpnum = getRandom(0,$LCTotal);
		%muzzlepoint = Vectoradd(%player.getposition(), %hmm);
		%muzzlevector = %player.getEyeVector();
		%muzzlepoint = VectorAdd(%muzzlepoint, %muzzleVector);
		%playerRot = rotFromTransform(%player.getTransform());
		%thrownItem = new (item)()
		{
			datablock = %item;
                        directDamage = (%mcpnum+1)*2;
		};
                %thrownItem.setscale(%player.getscale());
		MissionCleanup.add(%thrownItem);
                %thrownItem.client = %player.client;
		%muzzleVelocity = vectoradd(VectorScale(%muzzleVector, 50),%obj.getVelocity());
		%muzzleVelocity = vectorscale(%muzzleVelocity,%player.getscale());
		%thrownItem.settransform(%muzzlepoint @ " " @ %playerRot);
		%thrownItem.setVelocity(%muzzleVelocity );
		%thrownItem.schedule($ItemTime, delete);
		%thrownItem.setCollisionTimeout(%player);
         	%thrownitem.setskinname($legoColor[%mcpnum]);
         }
}

