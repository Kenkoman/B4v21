//Hell Rain, rain any kind of hell! -DShiznit
//To use, put the name of a projectile first, then the desired x y z velocity
//Yes, I took a minute ammount of code from the shotgun.

//Also this doesn't do damage to anyone and I don't think it ever did.
function RainHell(%obj,%velX,%velY,%velZ) { 
if($hellisraining == 0)
      {
	   %obj.lifetime = 60000; //this'll make it so any projectile used will last a decent time
	   %obj.fadedelay = 59000;
	   %p = new (projectile)()
         {
            dataBlock        = %obj;
            initialVelocity  = %velX@" "@%velY@" "@%velZ; //This is where the vel variables come in
            initialPosition  = getrandom(-2048,2048)@" "@getrandom(-2048,2048)@" 1024"; //This gets a random world coordinate.
         };
         MissionCleanup.add(%p);
        onAddProjectile(%obj, %p, "Hellrain");
  schedule(50,0,RainHell,%obj,%velX,%velY,%velZ);
	}
}

//This function ends hell rain and sets up for another
function endhell(){
$hellisraining = 1;
schedule(1000,0,resetHell);
}

function resetHell(){
$hellisraining = 0;
}

//overriding my existing fires to support explosions for meteors :D
datablock ProjectileData(flame1Projectile) {

   areaImpulse = 0;
   directDamage        = 000;
   radiusDamage        = 400;
   damageRadius        = 24.0 ;
   explosion           = MortarCannonExplosion;
   particleEmitter     = flame1Emitter;
   muzzleVelocity      = 0;
   velInheritFactor    = 0.0;
   armingDelay         = 1000;
   isFluid             = true;
   lifetime            = 500;
   fadeDelay           = 5000;
   bounceElasticity    = 0.0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0.10;
   hasLight    = true;
   lightRadius = 10;
   lightColor  = "1.0 0.65 0.0";
};

datablock ProjectileData(flame2Projectile) {
   areaImpulse = 0;
   directDamage        = 000;
   radiusDamage        = 200;
   damageRadius        = 24.0 ;
   explosion           = MortarCannonExplosion;
   particleEmitter     = flame2Emitter;
   muzzleVelocity      = 0;
   velInheritFactor    = 0.0;
   armingDelay         = 1000;
   isFluid             = true;
   lifetime            = 500;
   fadeDelay           = 5000;
   bounceElasticity    = 0.0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0.10;
   hasLight    = true;
   lightRadius = 5;
   lightColor  = "1.0 0.65 0.0";
};

function Flame1Projectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	tbmcollison(%this,%obj,%col,%fade,%pos,%normal);
       tbmradiusDamage
     (%obj, VectorAdd(%pos, VectorScale(%normal, 0.01)),
      %obj.damageRadius, %obj.radiusDamage, %obj.damageType,20);
}

function Flame2Projectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	tbmcollison(%this,%obj,%col,%fade,%pos,%normal);
       tbmradiusDamage
     (%obj, VectorAdd(%pos, VectorScale(%normal, 0.01)),
      %obj.damageRadius,%obj.radiusDamage,%obj.damageType,20);
}