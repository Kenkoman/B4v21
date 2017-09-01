//Helper scripts

function setupdtbserver() {

exec("./dtbinit.cs");
exec("./TBMRadiusDamage.cs");
exec("./expemitter.cs");
exec("./modes.cs");

//gametypes
exec("./teams.cs");
exec("./killbots.cs");
exec("./CTC.cs");
exec("./CTF.cs");

//Weapon array for editor spawning
$weptotal=0;
exec("./weapons/sounds.cs");
exec("./weapons/acidgun.cs");
exec("./weapons/agc.cs");
exec("./weapons/BatSword.cs");
exec("./weapons/blackholegun.cs");
exec("./weapons/BlasterRifle.cs");
exec("./weapons/bow.cs");
exec("./weapons/ConRifle.cs");
exec("./weapons/cutlass.cs");
exec("./weapons/StormGun.cs");
exec("./weapons/flamethrower.cs");
exec("./weapons/flaregun.cs");
exec("./weapons/Flashlight.cs");
exec("./weapons/freezeGun.cs");
exec("./weapons/GrenadeLauncher.cs");
exec("./weapons/gunpack.cs");
exec("./weapons/HalberdAxe.cs");
exec("./weapons/Handgun.cs");
exec("./weapons/katana.cs");
exec("./weapons/kicker.cs");
exec("./weapons/landmine.cs");
exec("./weapons/lasergun.cs");
exec("./weapons/LaserRepeater.cs");
exec("./weapons/legosniper.cs");
exec("./weapons/lightsabre.cs");
exec("./weapons/MedGun.cs");
exec("./weapons/MiniGun.cs");
exec("./weapons/missilelauncher.cs");
exec("./weapons/MortarCannon.cs");
exec("./weapons/nuke.cs");
exec("./weapons/pickaxe.cs");
exec("./weapons/PlasmaSniper.cs");
exec("./weapons/puller.cs");
exec("./weapons/QuantumGun.cs");
exec("./weapons/revolver.cs");
exec("./weapons/rifle.cs");
exec("./weapons/sabre.cs");
exec("./weapons/saw.cs");
exec("./weapons/shotgun.cs");
exec("./weapons/spear.cs");
exec("./weapons/sword.cs");
$weapon[$weptotal++]=nametoid(flag);
//Orange Block weapons added by DShiz
$weapon[$weptotal++]="----------------------------------";
$weapon[$weptotal++]="-=Orange Block Weapons=-";
$weapon[$weptotal++]="----------------------------------";
//The above separates
exec("./weapons/9MM.cs");
exec("./weapons/AR.cs");
exec("./weapons/bat.cs");
exec("./weapons/bball.cs");
exec("./weapons/combatshotgun.cs");
exec("./weapons/crossbow.cs");
exec("./weapons/ElectroGun.cs");
exec("./weapons/Healing.cs");
exec("./weapons/God.cs");
exec("./weapons/BounceGrenadeLauncher.cs");
exec("./weapons/groove.cs");
$weapon[$weptotal++]=nametoid(Guitar);
exec("./weapons/hailfirerocket.cs");
exec("./weapons/kittycarbine.cs");
exec("./weapons/hominggun.cs");
exec("tbm/server/scripts/items/ski.cs");
exec("./weapons/knife.cs");
exec("./weapons/Magnum.cs");
exec("./weapons/uzi.cs");
exec("./weapons/m23.cs");
exec("./weapons/molotov.cs");
exec("./weapons/MP7.cs");
exec("./weapons/paintball.cs");
exec("./weapons/Piss.cs");
exec("./weapons/pistolpair.cs");
exec("./weapons/Psy.cs");
exec("./weapons/booster.cs");
exec("./cemetechai/snowballgun.cs");
exec("./weapons/SuicideBomb.cs");
exec("./weapons/trainthrow.cs");
exec("./weapons/ThrowAxe.cs");
exec("./weapons/TimeBomb.cs");
exec("./weapons/tommy.cs");
exec("./weapons/webslinger.cs");
exec("./weapons/zombie.cs");

//Orange Block Mod Scripts & Work-arounds
exec("./cemetechai.cs");
exec("./dtbinit.cs");
exec("./HellRain.cs");
exec("./SUMP.cs");
exec("./TBMRadiusDamage.cs");

if(getlinecount("dtb/server/scripts/wiggyserver.cs"))
  exec("./wiggyserver.cs");
else
  exec("./temp.cs");

//DM Items
exec("./items/DMArmor.cs");
exec("./items/DMArrow.cs");
exec("./items/DMCape.cs");
exec("./items/DMHealth.cs");
exec("./items/DMShield.cs");
}