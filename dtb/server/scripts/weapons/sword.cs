sword.threatlevel = "Normal";
addWeapon(sword);

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(swordImage)
{
   // Basic Item properties
   shapeFile = "tbm/data/shapes/sword.dts";
   emap = true;

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
   item = sword;
   ammo = "";
   projectile = swordProjectile;
   projectileType = Projectile;

   directDamage        = 50;
   radiusDamage        = 0;
   damageRadius        = 0.5;
   damagetype          = '%1 was slashed by %2';
   muzzleVelocity      = 50;
   velInheritFactor    = 1;

   deathAnimationClass = "melee";
   deathAnimation = "death5";
   deathAnimationPercent = 0.4;

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
	stateTimeoutValue[0]             = 0.5;
	stateTransitionOnTimeout[0]       = "Ready";
	stateSound[0]					= swordDrawSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "PreFire";
	stateAllowImageChange[1]         = true;

	stateName[2]			= "PreFire";
	stateScript[2]                  = "onPreFire";
	stateAllowImageChange[2]        = false;
	stateTimeoutValue[2]            = 0.0999;
	stateTransitionOnTimeout[2]     = "Fire";

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "CheckFire";
	stateTimeoutValue[3]            = 0.2;
	stateFire[3]                    = true;
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	stateScript[3]                  = "onFire";
	stateWaitForTimeout[3]		= true;
	//stateTransitionOnTriggerUp[3]	= "StopFire";

	stateName[4]			= "CheckFire";
	stateTimeoutValue[4]            = 0.0501;
	stateScript[4]                  = "onStopFire";
	stateTransitionOnTimeout[4]     = "StopFire";

	
	stateName[5]                    = "StopFire";
	stateTransitionOnTriggerUp[5]     = "Ready";
	stateAllowImageChange[5]        = false;
	stateSequence[5]                = "StopFire";
	stateScript[5]                  = "onStopFire";


};

function swordImage::onPreFire(%this, %obj, %slot)
{
	//Parent::onFire(%this, %obj, %slot);
	%obj.playthread(2, armattack);
	%obj.parrying = 1;
}

function swordImage::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
	//messageAll( 'MsgClient', 'stopfire');
	%obj.parrying = 0;
}

function swordImage::onFire(%this, %obj, %slot)
{
   	%this.projectile.hasLight    = false;
	%p = Parent::onFire(%this, %obj, %slot);
	return %p;
}

function swordProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal) {
if(%obj.isZombie == 1) {
	if (%col.getClassName() $= "Player" || %col.getClassName() $= "AIPlayer") {
		if(%col.getDamageLevel() > getRandom(0,99) && !%col.client.DMShield) {
			//Ordinarily, this would be here:
			//%col.mountimage(zombieImage,0,1,green);
			//That was from when zombie infection was gradual and based on health and luck.
			//Now it occurs automatically on death.
		}
	}
}
if(%obj.isBat == 1) {
	if(%col.getClassName() $= "Player" || %col.getClassName() $= "AIPlayer") {
		//ServerPlay3D(BoinkSound, %obj.getTransform());
		%col.setVelocity(vectorScale(%obj.client.player.getEyeVector(), 200));
	}
}
if(%obj.isBlunt == 1) {
	%s = BashSound;
	%p = arrowExplosionEmitter;
	%t = 150;
}
else {
	if(%obj.isLSabre)
		%s = lsabreHitSound;
	else
		%s = swordHitSound;
	%p = swordExplosionEmitter;
	%t = 100;
}
if(!%obj.noSound)
	ServerPlay3D(%s, %pos);
if(!%obj.noParticles) {
	%p = new ParticleEmitterNode(){
	  datablock = defaultParticleEmitterNode;
	  emitter = %p;
	  scale = "1 1 1";
	  position = %pos;
	};
	%p.schedule(%t, delete);
}
tbmcollison(%this, %obj, %col, %fade, %pos, %normal);
}

package Weapon_SwordChop {
function onAddProjectile(%projectile, %p, %image) {
if(%image != nameToID(ZombieImage) && %image != nameToID(batimage) && %image != nameToID(flashlightimage) && %projectile $= swordProjectile)
  %p.headchop = 1;
if(%image == nameToID(batimage) || %image == nameToID(flashlightimage) && %projectile $= swordProjectile)
  %p.headshot = 1;
Parent::onAddProjectile(%projectile, %p, %image);
}
};
activatepackage(Weapon_SwordChop);