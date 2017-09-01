function loadwebprojectiles() {
  datablock ProjectileData(WebProjectile) {
   projectileshapename = "tbm/data/shapes/bricks/brick1x1.dts";
   particleEmitter     = WebTrailEmitter;
   muzzleVelocity      = 500;
   scale               = ".5 4 .5";
   armingdelay         = 0;
   lifetime            = 1000;
   fadeDelay           = 1;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod          = 1;
   skinname            = 'white';
   };

  datablock ProjectileData(WebProjectile2) {
   projectileshapename = "tbm/data/shapes/bricks/brick1x1.dts";
   particleEmitter     = WebTrailEmitter;
   muzzleVelocity      = 1000;
   scale               = ".5 4 .5";
   armingdelay         = 0;
   lifetime            = 10000;
   fadeDelay           = 1;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod          = 0;
   skinname            = 'white';
  };
}

loadWebProjectiles();

//////////
// item //
//////////
datablock ItemData(WebSlinger) {
      category   = "Weapon";
      classname  = "tool";
      shapefile  = "dtb/data/shapes/weapons/loudhailer.dts";
      rotate     = true;
      mass       = 1;
      density    = 0.2;
      elasticity = 0.2;
      friction   = 0.6;
      emap       = true;
      skinname   = 'yellow';
      pickupName = 'Spidey Power!';
      invname    = "Web Slinger";
      image      = WebSlingerImage;
      threatlevel = "Normal";
};

addWeapon(WebSlinger);

datablock ShapeBaseImageData(WebSlingerImage)
{
   // Basic Item properties
   shapeFile = "dtb/data/shapes/weapons/sithlightning.dts";
   emap = true;
   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";
  skinname='white';
   // Projectile && Ammo.
   item = WebSlinger;
   ammo = " ";
   projectile = WebProjectile;
   projectileType = Projectile;

   directDamage        = 0;
   radiusDamage        = 0;
   damageRadius        = 0;
   muzzleVelocity      = 500;
   velInheritFactor    = 0;
   damagetype          = "EXPLODY!";

   //melee particles shoot from eye node for consistency
   melee = false;
   //raise your arm up or not
   armReady = true;

   //casing = " ";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordinRPGy.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
//	stateName[0]                     = "Activate";
//	stateTimeoutValue[0]             = 0.1;
//	stateWaitForTimeout[0]           = true;
//	stateTransitionOnTimeout[0]       = "Ready";
//	stateSound[0]			= weaponSwitchSound;

	stateName[0]                     = "Ready";
        stateScript[0]                   = "onstopfiringweb";
	stateTransitionOnTriggerDown[0]  = "Fire";
	stateAllowImageChange[0]         = true;

	stateName[1]                    = "Fire";
	stateFire[1]                    = true;
	statetimeoutvalue[1]            = 0.001;
	stateAllowImageChange[1]        = false;
	stateSequence[1]                = "Fire";
	stateScript[1]                  = "onFireWeb";
	statesound[1]                   = sprayFireSound;
	statetransitionontimeout[1]     = "Fire";
        stateTransitionOnTriggerUp[1]   = "Ready";
};

function WebSlingerImage::onFireWeb(%this,%obj,%slot) {
  if (%obj.webbing) return;
  WebSlingerImage::onFire(%this,%obj,%slot).webid = %obj.shotwebid;
}

function WebSlingerImage::onStopFiringWeb(%this,%obj,%slot) {
  %obj.shotwebid++;
}

$WebSlinger::reelRate = 3;
$WebSlinger::callDelay = 70;
$WebSlinger::webVeloc = 250;
$WebSlinger::Stretchiness = 0.5;
$WebSlinger::Control = 2; 

function WebProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
  %player = %obj.sourceobject;
  if (%player.webbing || %player.shotwebid != %obj.webid) return;
  %player.setEnergyLevel(0);
  %player.setRechargeRate(0);
  %player.webbing = true;
  %player.webDist = VectorDist(%pos, %player.getMuzzlePoint(0));
  %player.webtargetid = %col;
  %player.webtargetoffset = vectorsub(%pos, %col.position);
  %player.reelthismuch = 0;
  %player.webid++;
  doWeb(%player);
}

function WebProjectile2::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
  %player = %obj.sourceobject;
  if (!%player.webbing) return;
  if (%player.webid != %obj.webid) return;
  %dist = vectorDist(%obj.pos,%pos);
  if (%dist > %obj.dist-2) return;
  %player.webDist = %dist;
  %player.webtargetid = %col;
  %player.webtargetoffset = vectorsub(%pos, %col.position);
  %player.webid++;
}

function doWeb(%this) {
  if (!%this.webbing || !isobject(%this.webtargetid)) {
    %this.setEnergyLevel(60);
    %this.setRechargeRate(1);
    return;
    }

  %dist = vectorLen(%diff = vectorsub(vectoradd(%this.webtargetid.position, %this.webtargetoffset),%pos = %this.getmuzzlepoint(0)));

  %proj = new projectile() {
    initialvelocity = vectorscale(%diff,$WebSlinger::webVeloc/%dist);
    initialposition = %pos;
    datablock       = "Webprojectile2";
    sourceobject    = %this;
  };
  %proj.pos = %pos;
  %proj.dist = %dist;
  %proj.webid = %this.webid;
//  %targettype = %this.webtargetid.getClassName();
  %howfarout = %dist - (%this.webdist = max(0,%this.webdist + %this.reelthismuch));
  if (%howfarout > 0) {
//    if (%targettype $= "Player" || %targettype $= "AIPlayer") {
//       %this.webtargetid.setVelocity(vectorAdd(%this.webtargetid.getVelocity(),vectorScale(%diff, -%howfarout * $WebSlinger::Stretchiness / %dist)));
//    }
//    else { 
    %this.setVelocity(vectorAdd(%this.getVelocity(),vectorScale(%diff, %howfarout * $WebSlinger::Stretchiness / %dist)));
//    }
  }
  schedule($WebSlinger::callDelay, %this, "doWeb", %this);
}

function Armor::onTrigger(%this, %obj, %triggerNum, %val)
{


   // This method is invoked when the player receives a trigger
   // move event.  The player automatically triggers slot 0 and
   // slot one off of triggers # 0 & 1.  Trigger # 2 is also used
   // as the jump key.
  if (!%obj.webbing) return;
  switch (%triggerNum) {
    case 0:
    %obj.reelthismuch = %val * $WebSlinger::reelrate;
    case 2:
    %obj.webbing = !%val;
    case 4:
    %obj.reelthismuch = %val * -$WebSlinger::reelrate;
    default:
  }
}

function max(%a,%b) {
  return (%a < %b) ? %b : %a;
}

datablock ParticleData(WebTrailParticle)
{
	dragCoefficient		= 2.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 1000;
	lifetimeVarianceMS	= 0;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= false;
	animateTexture		= false;
	textureName		= "~/data/particles/cloud";


	// Interpolation variables
	colors[0]	= "0.4 0.6 0.9 0.9";
	colors[1]	= "0.5 0.5 0.5 0.8";
	sizes[0]	= 0.45;
	sizes[1]	= 0.45;
	times[0]	= 0.0;
	times[1]	= 1.0;
};

datablock ParticleEmitterData(WebTrailEmitter)
{
  ejectionPeriodMS = 5;
   periodVarianceMS = 0;
   ejectionVelocity = 0;
   velocityVariance = 0;
   ejectionOffset = 0;
   thetaMin         = 0;
   thetaMax         = 90;  
   particles = "WebTrailParticle";
};

loadwebprojectiles();