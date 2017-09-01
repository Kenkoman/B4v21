//-----------------------------------------------------------------------------
// These are variables used to control the shell scripts and
// can be overriden by mods:
//-----------------------------------------------------------------------------

package dtbclient {
function DTBaddshit() {
  ItemSpawnMenu.add("CTC-Goal", 1002);
  ItemSpawnMenu.add("CTC-Crown", 1003);
  ItemSpawnMenu.add("CTC-Prison", 1004);
  ItemSpawnMenu.add("Spawn-Red", 1021);
  ItemSpawnMenu.add("Spawn-Blue", 1022);
  ItemSpawnMenu.add("Spawn-Green", 1023);
  ItemSpawnMenu.add("Spawn-Yellow", 1024);
  ItemSpawnMenu.add("Spawn-Car", 1025);
  ItemSpawnMenu.add("Spawn-Plane", 1026);
  ItemSpawnMenu.add("Spawn-Zombies", 1027);
  ItemSpawnMenu.add("DM-Armor", 1007);
  ItemSpawnMenu.add("DM-Arrow", 1008);
  ItemSpawnMenu.add("DM-Cape", 1009);
  ItemSpawnMenu.add("DM-Health", 1010);
  ItemSpawnMenu.add("DM-Shield", 1011);
  ItemSpawnMenu.add("Health Kit", 3141);
  DTB_PopulateSpawnMenu();
 }
function DTB_PopulateSpawnMenu() {
  for (%x=1; %x <= $weptot; %x++) 
	ItemSpawnMenu.add($wepName[%x], %x+1999);
}

};


function setupdtbclient() {
//These are the defaults
$weptot=0;
$wepName[$weptot++]="Acid Gun";
$wepName[$weptot++]="Anti Gravity Gun";
$wepName[$weptot++]="Bat Sword";
$wepName[$weptot++]="Blackhole Gun";
$wepName[$weptot++]="Blaster Rifle";
$wepName[$weptot++]="Bow";
$wepName[$weptot++]="Con Rifle";
$wepName[$weptot++]="cutlass";
$wepName[$weptot++]="Flame Thrower";
$wepName[$weptot++]="Flare Gun";
$wepName[$weptot++]="Flash Light";
$wepName[$weptot++]="Freeze Gun";
$wepName[$weptot++]="Grenade Launcher";
$wepName[$weptot++]="Gun Pack";
$wepName[$weptot++]="Halberd Axe";
$wepName[$weptot++]="Handgun";
$wepName[$weptot++]="Katana";
$wepName[$weptot++]="Kicker";
$wepName[$weptot++]="Landmine";
$wepName[$weptot++]="Laser Gun";
$wepName[$weptot++]="Laser Repeater";
$wepName[$weptot++]="Lego Sniper";
$wepName[$weptot++]="Light Sabre";
$wepName[$weptot++]="Loudhailer";
$wepName[$weptot++]="Med Gun";
$wepName[$weptot++]="Minigun";
$wepName[$weptot++]="Missile Launcher";
$wepName[$weptot++]="Mortar Cannon";
$wepName[$weptot++]="Nuke";
$wepName[$weptot++]="Pickaxe";
$wepName[$weptot++]="Plasma Sniper";
$wepName[$weptot++]="Puller";
$wepName[$weptot++]="Quantum Gun";
$wepName[$weptot++]="Revolver";
$wepName[$weptot++]="Rifle";
$wepName[$weptot++]="Sabre";
$wepName[$weptot++]="Saw";
$wepName[$weptot++]="Shotgun";
$wepName[$weptot++]="Spear";
$wepName[$weptot++]="Storm Gun";
$wepName[$weptot++]="Sword";
$wepName[$weptot++]="Flag";
//Orange Block weapons added by DShiznit
$wepName[$weptot++]="----------------------------------";
$wepName[$weptot++]="-=Orange Block Weapons=-";
$wepName[$weptot++]="----------------------------------";
//The Above Descriminates the new weps from teh old
$wepName[$weptot++]="9MM Pistol";
$wepName[$weptot++]="Assault Rifle";
$wepName[$weptot++]="Baseball Bat";
$wepName[$weptot++]="Basket Ball";
$wepName[$weptot++]="Combat Shotgun";
$wepName[$weptot++]="Crossbow";
$wepName[$weptot++]="Electro-Gun";
$wepName[$weptot++]="Fire Hose";
$wepName[$weptot++]="Grenades";
$wepName[$weptot++]="Groove Machine";
$wepName[$weptot++]="Guitar";
$wepName[$weptot++]="Hailfire Rocket Launcher";
$wepName[$weptot++]="Hand of God";
$wepName[$weptot++]="Hello Kitty Assault Rifle";
$wepName[$weptot++]="Homing Cannon";
$wepName[$weptot++]="Hover Board";
$wepName[$weptot++]="Knife";
$wepName[$weptot++]="Light Sabre Dual";
$wepName[$weptot++]="Light Sabre Blue";
$wepName[$weptot++]="Light Sabre Red";
$wepName[$weptot++]="Magnum";
$wepName[$weptot++]="Micro-Uzi Pair";
$wepName[$weptot++]="Molotov Cocktails";
$wepName[$weptot++]="MP7 Sub-machine gun";
$wepName[$weptot++]="Ninja Stars";
$wepName[$weptot++]="Paintball Gun";
$wepName[$weptot++]="Piss";
$wepName[$weptot++]="Pistol Pair";
$wepName[$weptot++]="Psy Beam";
$wepName[$weptot++]="Rocket Booster";
$wepName[$weptot++]="Snowballs";
$wepName[$weptot++]="SOCOM Surpressed Pistol";
$wepName[$weptot++]="Suicide Bomb";
$wepName[$weptot++]="Throwing Train";
$wepName[$weptot++]="Time Bomb";
$wepName[$weptot++]="Tommy Gun";
$wepName[$weptot++]="Web-Slinger";
$wepName[$weptot++]="Zombie Weapon";
activatePackage(dtbclient);
exec("./cemetechai.cs");
}

function ClientCmdReceiveWeaponName(%name, %clear) {
if(%clear) $weptot=0;
$wepName[$weptot++]=%name;
}

function ClientCmdGetBodytype(%n, %name) {
$armsColor[$armsTot = %n] = %name;
}

function ClientCmdGetSwitchAction(%n, %type, %name) {
$Client::Switch::numTypes = %n;
$Client::Switch::Typelist[%n] = %type;
$Client::Switch::RealName[%type] = %name;
}

function ClientCmdGetSwitchGroup(%n, %group, %name) {
$Client::Switch::numGroups = %n;
$Client::Switch::Group[%n] = %group;
$Client::Switch::Groupname[%group] = %name;
}