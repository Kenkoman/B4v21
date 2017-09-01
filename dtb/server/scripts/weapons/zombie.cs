//zombie weapon - un-unmountable weapon that makes you eat your friends! Inspired by GFeedBack's Zombie juice weapon.
//////////
// item //
//////////
datablock ItemData(zombieweapon)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "dtb/data/shapes/zombiehead.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a zombie bite';
	invName = "Zombie Juice";
	image = ZombieImage;
	threatlevel = "Normal";
};

addWeapon(zombieweapon);

////////////////
//weapon image//
////////////////

datablock ShapeBaseImageData(ZombieImage)
{
   // Basic Item properties
   shapeFile = "dtb/data/shapes/weapons/sithlightning.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;

   offset = "-.5167 -.797 .625";
   eyeOffset = "0 0 -2";

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
   projectile = swordProjectile;
   projectileType = Projectile;

   directDamage        = 10;
   radiusDamage        = 0;
   damageRadius        = 0;
   damageType          = '%1 was eaten by %2.';
   muzzleVelocity      = 60;
   velInheritFactor    = 1.1;

   deathAnimationClass = "default";
   deathAnimation = "zombify";
   deathAnimationPercent = 1;

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
	stateSound[0]					= zombiesound;
	stateTimeoutValue[0]            = 1;
	stateTransitionOnTimeout[0]     = "Ready";
	stateAllowImageChange[0]         = false;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]     = "Prefire";
	stateAllowImageChange[1]         = false;

	stateName[2]			= "PreFire";
	stateScript[2]                  = "onPreFire";
	stateAllowImageChange[2]        = false;
	stateTimeoutValue[2]            = 0.1;
	stateSound[2]					= zombiesound;
	stateTransitionOnTimeout[2]     = "Fire";

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "Reload";
	stateTimeoutValue[3]            = 1;
	stateFire[3]                    = true;
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	stateScript[3]                  = "onFire";
	stateWaitForTimeout[3]		= true;

	stateName[4]			= "Reload";
	stateAllowImageChange[4]        = false;
	stateTransitionOnTriggerup[4]     = "Ready";

};


function zombieImage::onMount(%this,%obj)
{
	if(%this.getDatablock() != nameToID(mDroid)) {
		%obj.playthread(1, armreadyboth);
		if(%obj.getClassName() $= "Player" || %obj.getClassName() $= "AIPlayer") {
			switch(%a = getRandom(1, 3)) {
				case 1:
					%obj.setDatablock(mZombie);
				case 2:
					%obj.setDatablock(mZombie2);
				case 3:
					%obj.setDatablock(mZombie3);
				default:
					%obj.setDatablock(MZombie);
			}
		}
		if(%obj.getClassName() $= "Player")
			messageClient(%obj.client, 'MsgBrickLimit', "\c2 You're a ZOMBIE! Click to lunge at your friends and EAT THEIR BRAAAAAAAAAAAAAAINS!");
	}
}

function zombieImage::onFire(%this, %obj, %slot)
{
   	%this.projectile.hasLight    = false;
	%obj.applyImpulse(0, vectorScale(%obj.getMuzzleVector(%slot), 800));
	%p = Parent::onFire(%this, %obj, %slot);
}

function zombieImage::onUnmount(%this, %obj)
{
	%obj.playthread(1, root);
	if(%obj.getClassName() $= "Player" || %obj.getClassName() $= "AIPlayer")
		commandToClient(%obj.client, 'UpdatePrefs');
}

package ZombieWeapon {
function Player::setDatablock(%this, %db) {
if(%this.getMountedImage(0) == nameToID(zombieImage) && %this.getDatablock() != nameToID(mZombie) && %this.getDatablock() != nameToID(mZombie2) && %this.getDatablock() != nameToID(mZombie3)) {
  %db = mZombie;
  if((%a = getRandom(1, 3)) == 2)
    %db = mZombie2;
  if(%a == 3)
    %db = mZombie3;
}
Parent::setDatablock(%this, %db);
}

function AIPlayer::setDatablock(%this, %db) {
if(%this.getMountedImage(0) == nameToID(zombieImage) && %this.getDatablock() != nameToID(mZombie) && %this.getDatablock() != nameToID(mZombie2) && %this.getDatablock() != nameToID(mZombie3)) {
  %db = mZombie;
  if((%a = getRandom(1, 3)) == 2)
    %db = mZombie2;
  if(%a == 3)
    %db = mZombie3;
}
Parent::setDatablock(%this, %db);
}

function onAddProjectile(%projectile, %p, %image) {
if(%image == nameToID(zombieImage) && %projectile $= swordProjectile) {
  %p.isZombie = 1;
  %p.directDamage = 10;
  %p.radiusDamage = 0;
  %p.damageRadius = 0;
  %p.noSound = 1;
  %p.noParticles = 1;
  %p.damageType = '%1 was eaten by %2.';
}
Parent::onAddProjectile(%projectile, %p, %image);
}

};
activatepackage(ZombieWeapon);