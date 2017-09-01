//groove machine - makes you dance

//////////
// item //
//////////
datablock ItemData(GrooveMachine)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "tbm/data/shapes/player/mGray/minifig.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'Her Groove Back';
	invName = "GrooveMachine";
	spawnName = "Groove Machine";
	image = GrooveMachineImage;
	threatlevel = "Safe";
};

addWeapon(GrooveMachine);

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(GrooveMachineImage)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/weapons/sithlightning.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;

   offset = "0 0 0";
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
   item = Batsword;
   ammo = " ";
   projectile = BatswordProjectile;
   projectileType = Projectile;

   //melee particles shoot from eye node for consistancy
   melee = true;
   //raise your arm up or not
   armReady = false;

   //casing = " ";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]                     = "Activate";
	stateScript[0]                  = "onJiggle";
	stateTimeoutValue[0]             = 0.25;
	stateTransitionOnTimeout[0]     = "Ready";
	stateTransitionOnTriggerDown[0]  = "PreFire";
	stateAllowImageChange[0]         = true;
	
	stateName[1]                     = "Ready";
	stateScript[1]                  = "onChop";
	stateTimeoutValue[1]             = 0.25;
	stateTransitionOnTimeout[1]     = "Set";
	stateTransitionOnTriggerDown[1]  = "PreFire";
	stateAllowImageChange[1]         = true;

	stateName[2]                     = "Set";
	stateScript[2]                  = "onJiggle";
	stateTimeoutValue[2]             = 0.25;
	stateTransitionOnTimeout[2]     = "Go";
	stateTransitionOnTriggerDown[2]  = "PreFire";
	stateAllowImageChange[2]         = true;

	stateName[3]                     = "Go";
	stateScript[3]                  = "onChoptoo";
	stateTimeoutValue[3]             = 0.25;
	stateTransitionOnTimeout[3]     = "Activate";
	stateTransitionOnTriggerDown[3]  = "PreFire";
	stateAllowImageChange[3]         = true;

	stateName[4]			= "PreFire";
	stateScript[4]                  = "onPreFire";
	stateAllowImageChange[4]        = false;
	stateTimeoutValue[4]            = 0.4;
	stateTransitionOnTimeout[4]     = "Fire";

	stateName[5]                    = "Fire";
	stateTransitionOnTimeout[5]     = "CheckFire";
	stateTimeoutValue[5]            = 0.4;
	stateFire[5]                    = true;
	stateAllowImageChange[5]        = false;
	stateSequence[5]                = "Fire";
	stateScript[5]                  = "onFire";
	stateWaitForTimeout[5]		= true;
	
	stateName[6]			= "CheckFire";
	stateScript[6]                  = "onReset";
	stateTransitionOnTriggerUp[6]	= "Activate";
	stateTransitionOnTriggerDown[6]	= "PreFire";

};




function GrooveMachineImage::onPreFire(%this, %obj, %slot)
{
	%obj.playthread(1, root);
	%obj.playthread(2, armreadyboth);
}

function GrooveMachineImage::onFire(%this, %obj, %slot)
{	
  %muzzleVector = %obj.getMuzzleVector(%slot);
  %obj.playThread(2, jump);
  %obj.applyImpulse(0, vectorScale(%muzzleVector, 600));
}

function GrooveMachineImage::onJiggle(%this, %obj, %slot)
{	
	%obj.playthread(1, talk);
}

function GrooveMachineImage::onChop(%this, %obj, %slot)
{	
	%obj.playthread(2, armreadyright);
}

function GrooveMachineImage::onChoptoo(%this, %obj, %slot)
{	
	%obj.playthread(2, armreadyleft);
}

function GroovemachineImage::onUnmount(%this,%obj)
{
	%obj.playthread(1, root);
	%obj.playthread(2, root);
}