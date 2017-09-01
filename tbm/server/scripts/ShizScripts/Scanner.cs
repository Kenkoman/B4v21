////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(ScannerImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/bricks/tricorder2.dts";
   emap = false;
   cloakable = false;
   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0.1";
   //eyeOffset = "0.1 0.2 -0.55";

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = false;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = eyedropper;
   ammo = "";
   projectile = eyeDropperProjectile;
   projectileType = Projectile;

   //melee particles shoot from eye node for consistancy
   melee = true;
   //raise your arm up or not
   armReady = true;

   //casing = " ";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.5;
	stateTransitionOnTimeout[0]       = "Ready";

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "PreFire";
	stateAllowImageChange[1]         = true;

	stateName[2]			= "PreFire";
	stateScript[2]                  = "onPreFire";
	stateAllowImageChange[2]        = false;
	stateTimeoutValue[2]            = 0.1;
	stateTransitionOnTimeout[2]     = "Fire";

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "CheckFire";
	stateTimeoutValue[3]            = 0.1;
	stateFire[3]                    = true;
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	stateScript[3]                  = "onFire";
	stateWaitForTimeout[3]		= true;

	stateName[4]			= "CheckFire";
	stateTransitionOnTriggerUp[4]	= "StopFire";
	stateTransitionOnTriggerDown[4]	= "Fire";

	stateName[5]                    = "StopFire";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 0.2;
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;
	stateSequence[5]                = "StopFire";
	stateScript[5]                 = "onStopFire";
};

function scannerimage::onFire(%this, %obj, %slot)
{
	%obj.triresult = "Scanning...";
	if(%scanTarg = getWord(ContainerRayCast(%obj.getMuzzlePoint(0), vectorAdd(vectorScale(vectorNormalize(%obj.geteyeVector()), 256), %obj.getEyePoint()), $TypeMasks::ShapeBaseObjectType | $TypeMasks::ItemObjectType), 0))
		{
		%xhack = getword(%scanTarg.getscale(), 0); 
		%yhack = getword(%scanTarg.getscale(), 1);
		%zhack = getword(%scanTarg.getscale(), 2);

		%xscale = %xhack * %scanTarg.getDatablock().x;
		%yscale = %yhack * %scanTarg.getDatablock().y;
		%zscale = %zhack * %scanTarg.getDatablock().z / 3;

		%healthtotal = 800;

		if(%scanTarg.getClassName() $= "Player" || %scanTarg.getClassName() $= "AIPlayer")
			{
			%xscale = %xhack * 2;
			%yscale = %yhack * 1;
			%zscale = %zhack * 5;
			%healthtotal = 100;
			}
		%health = %healthtotal - %scanTarg.getDamageLevel();

		%obj.triresult = "ID:" SPC %scanTarg SPC "Length:" SPC %xscale SPC "Width:" SPC %yscale SPC "Height:" SPC %zscale SPC "Health:" SPC %health SPC "/" SPC %healthtotal SPC "Type:" SPC %scanTarg.getDatablock().getName() SPC "Class:" SPC %scanTarg.getClassName() SPC "Skin:" SPC %scanTarg.getSkinName() SPC "Doorset:" SPC %scanTarg.port SPC "Client/Owner:" SPC %scanTarg.client.namebase SPC "/" SPC %scanTarg.owner SPC "Image:" SPC %scanTarg.getmountedimage(0).getname();
		}
	commandToClient(%obj.client, 'BottomPrint', %obj.triresult, 3, 1);
}

function serverCmdToggleScanner(%client) { 
	if (%client.player.getmountedimage(0)==nametoid(scannerImage)) 
		%client.player.unmountimage(0); 
	else { 
		%client.player.mountimage(scannerImage,0,1,%client.brickcolor); 
		messageClient(%client, 'MsgHilightInv', '', -1); 
		%client.player.currWeaponSlot = -1; 
	}  
}
