//--------------------------------------------------------------------------
// Weapon Item.  This is the item that exists in the world, i.e. when it's
// been dropped, thrown or is acting as re-spawnable item.  When the weapon
// is mounted onto a shape, the RifleImage is used.

datablock ItemData(SuicideBomb)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "tbm/data/shapes/bombvest.dts";
	skinName = 'red';
	rotate = true;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a Suicide Bomb.';
	invName = "Suicide Bomb";
	image = SuicideBombImage;
	threatlevel = "Normal";
};

addWeapon(SuicideBomb);

////////////////
//weapon image//
////////////////

datablock ShapeBaseImageData(SuicideBombImage)
{
   // Basic Item properties
   shapeFile = "tbm/data/shapes/bombvest.dts";
   skinName = 'base';
   emap = true;
    
   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "-0.03 -0.25 -0.12";
   //eyeOffset = "0.1 0.2 -0.55";

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = SuicideBomb;
   ammo = " ";
   projectile = hack2Projectile;
   projectileType = Projectile;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = false;
   
   // Initial start up state
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 1;
	stateTransitionOnTimeout[0]      = "Ready";
	stateSound[0]			 = weaponSwitchSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateAllowImageChange[1]         = true;

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "Reload";
	stateTimeoutValue[2]            = 0.2;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]			= true;
	
	stateName[3]			= "Reload";
	stateSequence[3]                = "Reload";
	stateAllowImageChange[3]        = false;
	stateTimeoutValue[3]            = 1;
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTimeout[3]     = "Check";

	stateName[4]			= "Check";
	stateTransitionOnTriggerUp[4]	= "StopFire";
	stateTransitionOnTriggerDown[4]	= "Fire";

	stateName[5]                    = "StopFire";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 0.4;
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;
	//stateSequence[5]                = "Reload";
	stateScript[5]                  = "onStopFire";


};


//-----------------------------------------------------------------------------

function SuicideBombImage::onMount(%this, %obj)
{
	%obj.playthread(1, armreadyboth);
	echo(%obj.client.name @ ": ALAAAAAAAAAAAAAAAAAH"); //tagged string no go here
}

function SuicideBombImage::onUnmount(%this, %obj)
{
	%obj.playthread(1, root);
	echo(%obj.client.name @ ":ALAAAAA- naaaaaaaah");
}

function SuicideBombimage::onFire(%this, %obj)
{
	createExplosion(NameToID(GLExplosion), %obj.getPosition());
	%obj.deathAnim = "explosion";
	tbmradiusDamage(%obj, %obj.getPosition(), 8, 200, '%1 got blown the !@#$ up by %2', 100);
	%obj.deathAnim = "";
}