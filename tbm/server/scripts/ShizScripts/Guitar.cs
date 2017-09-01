//Guitar.cs
//A simple electric guitar made by DShiznit

datablock AudioProfile(Guitar1sound)
{
   filename    = "tbm/data/sound/guitar1.wav";
   description = AudioDefault3d;
   preload = true;
};

datablock AudioProfile(Guitar2sound)
{
   filename    = "tbm/data/sound/guitar2.wav";
   description = AudioDefault3d;
   preload = true;
};

//////////
// item //
//////////
datablock ItemData(Guitar)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "tbm/data/shapes/bricks/guitar.dts";
	rotate = true;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'an electric guitar.';
	invName = "Guitar";
	image = GuitarImage;
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(GuitarImage)
{
   // Basic Item properties
   shapeFile = "tbm/data/shapes/bricks/guitar.dts";
   emap = true;
   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = false;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = Guitar;
   ammo = " ";
   //projectile = NoProjectile;
   //projectileType = Projectile;

   //melee particles shoot from eye node for consistancy
   melee = false;
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
	stateTimeoutValue[0]             = 0.1;
	stateTransitionOnTimeout[0]       = "Ready";
	stateSound[0]					= weaponswitchsound;

	stateName[1]                     = "Ready";
	stateScript[1]                   = "onPreFire";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateAllowImageChange[1]         = true;

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "Reload";
	stateTimeoutValue[2]            = 0.1;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]			= true;
	stateSound[2]					= Guitar1sound;

	stateName[3]                    = "Reload";
	stateScript[3]                   = "onReload";
	stateTransitionOnTriggerUp[3]	= "Ready2";

	stateName[4]                     = "Ready2";
	stateScript[4]                   = "onPreFire";
	stateTransitionOnTriggerDown[4]  = "Fire2";
	stateAllowImageChange[4]         = true;

	stateName[5]                    = "Fire2";
	stateTransitionOnTimeout[5]     = "Reload2";
	stateTimeoutValue[5]            = 0.1;
	stateFire[5]                    = true;
	stateAllowImageChange[5]        = false;
	stateSequence[5]                = "Fire";
	stateScript[5]                  = "onFire";
	stateWaitForTimeout[5]			= true;
	stateSound[5]					= Guitar2sound;

	stateName[6]                    = "Reload2";
	stateScript[6]                   = "onReload";
	stateTransitionOnTriggerUp[6]	= "Ready3";

	stateName[7]                     = "Ready3";
	stateScript[7]                   = "onPreFire";
	stateTransitionOnTriggerDown[7]  = "Fire3";
	stateAllowImageChange[7]         = true;

	stateName[8]                    = "Fire3";
	stateTransitionOnTimeout[8]     = "Reload3";
	stateTimeoutValue[8]            = 0.1;
	stateFire[8]                    = true;
	stateAllowImageChange[8]        = false;
	stateSequence[8]                = "Fire";
	stateScript[8]                  = "onFire";
	stateWaitForTimeout[8]			= true;
	stateSound[8]					= Guitar2sound;

	stateName[9]                    = "Reload3";
	stateScript[9]                   = "onReload";
	stateTransitionOnTriggerUp[9]	= "Ready4";

	stateName[10]                     = "Ready4";
	stateScript[10]                   = "onPreFire";
	stateTransitionOnTriggerDown[10]  = "Fire4";
	stateAllowImageChange[10]         = true;

	stateName[11]                    = "Fire4";
	stateTransitionOnTimeout[11]     = "Reload4";
	stateTimeoutValue[11]            = 0.1;
	stateFire[11]                    = true;
	stateAllowImageChange[11]        = false;
	stateSequence[11]                = "Fire";
	stateScript[11]                  = "onFire";
	stateWaitForTimeout[11]			= true;
	stateSound[11]					= Guitar1sound;

	stateName[12]                    = "Reload4";
	stateScript[12]                   = "onReload";
	stateTransitionOnTriggerUp[12]	= "Ready5";

	stateName[13]                     = "Ready5";
	stateScript[13]                   = "onPreFire";
	stateTransitionOnTriggerDown[13]  = "Fire5";
	stateAllowImageChange[13]         = true;

	stateName[14]                    = "Fire5";
	stateTransitionOnTimeout[14]     = "Reload5";
	stateTimeoutValue[14]            = 0.1;
	stateFire[14]                    = true;
	stateAllowImageChange[14]        = false;
	stateSequence[14]                = "Fire";
	stateScript[14]                  = "onFire";
	stateWaitForTimeout[14]			= true;
	stateSound[14]					= Guitar2sound;

	stateName[15]                    = "Reload5";
	stateScript[15]                   = "onReload";
	stateTransitionOnTriggerUp[15]	= "Ready6";

	stateName[16]                     = "Ready6";
	stateScript[16]                   = "onPreFire";
	stateTransitionOnTriggerDown[16]  = "Fire6";
	stateAllowImageChange[16]         = true;

	stateName[17]                    = "Fire6";
	stateTransitionOnTimeout[17]     = "Reload6";
	stateTimeoutValue[17]            = 0.1;
	stateFire[17]                    = true;
	stateAllowImageChange[17]        = false;
	stateSequence[17]                = "Fire";
	stateScript[17]                  = "onFire";
	stateWaitForTimeout[17]			= true;
	stateSound[17]					= Guitar2sound;

	stateName[18]                    = "Reload6";
	stateScript[18]                   = "onReload";
	stateTransitionOnTriggerUp[18]	= "Ready7";

	stateName[19]                     = "Ready7";
	stateScript[19]                   = "onPreFire";
	stateTransitionOnTriggerDown[19]  = "Fire7";
	stateAllowImageChange[19]         = true;

	stateName[20]                    = "Fire7";
	stateTransitionOnTimeout[20]     = "Reload7";
	stateTimeoutValue[20]            = 0.1;
	stateFire[20]                    = true;
	stateAllowImageChange[20]        = false;
	stateSequence[20]                = "Fire";
	stateScript[20]                  = "onFire";
	stateWaitForTimeout[20]			= true;
	stateSound[20]					= Guitar1sound;

	stateName[21]                    = "Reload7";
	stateScript[21]                   = "onReload";
	stateTransitionOnTriggerUp[21]	= "Ready8";

	stateName[22]                     = "Ready8";
	stateScript[22]                   = "onPreFire";
	stateTransitionOnTriggerDown[22]  = "Fire8";
	stateAllowImageChange[22]         = true;

	stateName[23]                    = "Fire8";
	stateTransitionOnTimeout[23]     = "Reload8";
	stateTimeoutValue[23]            = 0.1;
	stateFire[23]                    = true;
	stateAllowImageChange[23]        = false;
	stateSequence[23]                = "Fire";
	stateScript[23]                  = "onFire";
	stateWaitForTimeout[23]			= true;
	stateSound[23]					= Guitar2sound;

	stateName[24]                    = "Reload8";
	stateScript[24]                   = "onReload";
	stateTransitionOnTriggerUp[24]	= "Ready";

};

function GuitarImage::onPreFire(%this, %obj, %slot)
{
	%obj.playthread(2, armreadyboth);
}

function GuitarImage::onFire(%this, %obj, %slot)
{
	%obj.playthread(2, root);
}

function GuitarImage::onUnmount(%this,%obj)
{
	%obj.playthread(1, root);
	%obj.playthread(2, root);
}
