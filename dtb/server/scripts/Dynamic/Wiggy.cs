//BEGIN
//A pseudo-library of some stuff that I make that actually turns out to be useful that I use in later addons
//This is mostly just so I'll stop worrying about stuff.

$WiggyPatch::ServerLib = 2.6;
//Private use; updated during 2.3 development
//6/22/2012

//Class functions, alphabetized

function AIPlayer::getPointRotation(%this, %point) {
%forward = mAtan(getWord(%this.getForwardVector(), 0), getWord(%this.getForwardVector(), 1));
%point = mAtan(getWord(%point, 0) - getWord(%this.getWorldBoxCenter(), 0), getWord(%point, 1) - getWord(%this.getWorldBoxCenter(), 1));
return (%point - %forward) * 180 / $pi + (%point < %forward ? 360 : 0);
}

function AIPlayer::getPointSightZone(%this, %point) {
//1 is forward, 2 is right, 3 is behind, 4 is left
%angle = %this.getPointRotation(%point);
%zone = mFloor((%angle + 45) / 90);
return %zone + 1 - (%zone > 3);
}

function GameConnection::isHost(%this) {
return getRawIP(%this) $= "local";
}

function GameConnection::rankCheck(%this, %a) {
if(%this.isHost()) //The host can do anything.
  return 1;        //%a = 4 is generally used for host check.
if(%a == 1 || strLwr(%a) $= "mod")
  return %this.isMod || %this.isAdmin || %this.isSuperAdmin;
if(%a == 2 || strLwr(%a) $= "admin")
  return %this.isAdmin || %this.isSuperAdmin;
if(%a == 3 || strLwr(%a) $= "super admin")
  return %this.isSuperAdmin;
if(%a == 4)
  return %this.isHost();
return 0;
}

function GameConnection::respawn(%this) {
if(isObject(%this.player))
  %this.player.delete();
%this.spawnPlayer();
}

function GameConnection::setStuds(%this, %studs) {
%studs += 0;
if(%studs < 0)
  %studs = 0;
%this.studMoney = %studs;
commandToClient(%this, 'setStudCounter', %studs, $Pref::Server::CurrencyName);
}

function Player::getPointRotation(%this, %point) {
%forward = mAtan(getWord(%this.getForwardVector(), 0), getWord(%this.getForwardVector(), 1));
%point = mAtan(getWord(%point, 0) - getWord(%this.getWorldBoxCenter(), 0), getWord(%point, 1) - getWord(%this.getWorldBoxCenter(), 1));
return (%point - %forward) * 180 / $pi + (%point < %forward ? 360 : 0);
}

function Player::getPointSightZone(%this, %point) {
//1 is forward, 2 is right, 3 is behind, 4 is left
%angle = %this.getPointRotation(%point);
%zone = mFloor((%angle + 45) / 90);
return %zone + 1 - 4 * (%zone > 3);
}

function Player::giveItem(%this,%item,%forcename) { //Hmm, what does this do?  I do wonder.
if(!nameToID(%item))
  return;
%freeslot = -1;
for(%i = 0; %i < 20; %i++) {
  if (!%this.inventory[%i] && %freeslot == -1) {
    %freeslot = %i;
    break;
  }
  else if (%this.inventory[%i] == nameToID(%item))
    %freeslot = -2;
}
//If freeslot is -1, that means there isn't any room left.
//If freeslot is -2, that means the player already has that weapon.
//It returns that so you can use it as an error code if you must.
if(%freeslot < 0)
  return %freeslot;
%this.weaponCount++;
%this.inventory[%freeslot] = nameToID(%item);
if(%forcename !$= "")
  messageClient(%this.client, 'MsgItemPickup', '', %freeslot, %forcename);
else
  messageClient(%this.client, 'MsgItemPickup', '', %freeslot, %item.invName);
return %freeslot;
}

function Player::removeCurrentWeapon(%this) {
%this.inventory[%this.currWeaponSlot] = "";
messageClient(%this.client, 'MsgDropItem', '', %this.currWeaponSlot);
%this.currWeaponSlot = 0;
%this.unmountImage(0);
}

function Player::removeImages(%this) {
%this.unMountImage($headSlot);
%this.unMountImage($visorSlot);
%this.unMountImage($backSlot);
%this.unMountImage($leftHandSlot);
%this.unMountImage($chestSlot);
%this.unMountImage($faceSlot);
%this.unMountImage(7);
}

function Player::removeWeapons(%this) {
%this.removeCurrentWeapon();
for(%i = 0; %i < 20; %i++) {
  if(%this.inventory[%i].classname !$= "BrickItem") {
    %this.inventory[%i] = 0;
    messageClient(%this.client, 'MsgDropItem', "", %i);
  }
}
}

function Projectile::getCurrentPosition(%this) {
return vectorAdd(%this.initPos, vectorScale(%this.Velocity, (getSimTime() - %this.creationTime) / 1000));
}

function SimObject::getDatablock(%this) { //Major spam removal that DOESN'T BREAK ANYTHING.
return;
}

function SimObject::getShapeName(%this) {
return;
}

function TerrainBlock::setVelocity(%this) {
return;
}


//Non-class functions, alphabetized

function activateSwitches(%doorset) {
for(%i = 0; %i < MissionCleanup.getCount(); %i++) {
  %block = MissionCleanup.getObject(%i);
  if(%doorset == %block.port && %block.getDatablock() == nameToID(portculySwitch))
    Portculyswitch::onPickup(portculySwitch, %block, getLocal().player);
}
}

function changeFlower(%this) {
if(%this.getDatablock() != nameToID(staticFlowerBrick) && %this.getDatablock() != nameToID(flowers))
  return;
%this.setSkinName($flowerColor[getRandom($flowerColors)]);
}

function encodeGhostData(%obj) {
return %obj.getShapeName() TAB %obj.getDatablock() TAB %obj.getPosition() TAB %obj.getScale() TAB %obj.getVelocity();
}

function eulerToVector(%a) {
%z = mSin(getWord(%a, 0) * $pi / 180);
%xy = mSqrt(1 - mPow(%z, 2));
%x = mSin(getWord(%a, 2) * $pi / 180) * mCos(getWord(%a, 0) * $pi / 180);
%y = mCos(getWord(%a, 2) * $pi / 180) * mCos(getWord(%a, 0) * $pi / 180);
return vectorScale(%x SPC %y SPC %z, 1);
}

function evalAll(%code, %returncode, %a1, %a2, %a3) {
for(%i = 0; %i < ClientGroup.getCount(); %i++) {
  %client = ClientGroup.getObject(%i);
  eval(%code);
}
if(%returncode !$= "") eval(%returncode);
}

//A very slight modification of serverBrickCount in tbmservercmd.cs.dso.
//Not my own original work.
function getBrickCount() {
%c = MissionCleanup.getCount();
for(%i = 0; %i < %c; %i++) {
  %block = MissionCleanup.getObject(%i);
  if(%block.getClassName() !$= "StaticShape")
    continue;
  if (%block.getDataBlock().classname $= "brick")
    %brickCount++;
}
if(!%brickcount)
  %brickCount = 0;
return %brickCount;
}

function getClient_s(%name) {
%name = detagColors(%name);
for(%i = 0; %i < ClientGroup.getCount(); %i++) {
  if(ClientGroup.getObject(%i).namebase $= %name)
    return ClientGroup.getObject(%i);
}
for(%i = 0; %i < ClientGroup.getCount(); %i++) {
  if(strLwr(ClientGroup.getObject(%i).namebase) $= strLwr(%name))
    return ClientGroup.getObject(%i);
}
for(%i = 0; %i < ClientGroup.getCount(); %i++) {
  if(strPos(ClientGroup.getObject(%i).namebase, %name) != -1)
    return ClientGroup.getObject(%i);
}
for(%i = 0; %i < ClientGroup.getCount(); %i++) {
  if(strPos(strLwr(ClientGroup.getObject(%i).namebase), strLwr(%name)) != -1)
    return ClientGroup.getObject(%i);
}
return -1;
}

function getHobo() {
return getClient_s("Vulpes");
}

function getServerUptime(%verbose) {
if($Game::StartTime $= "" || $Game::StartTime $= "0") {
  if(%verbose == 2)
    return "0 seconds";
  if(%verbose)
    return "00:00:00";
  else
    return "0 0 0";
}
%t = mFloor($Sim::Time - $Game::StartTime);
%a = %t / 3600;
%b = 60 * (%a - mFloor(%a));
%c = mFloor(60 * (%b - mFloor(%b)));
%a = mFloor(%a);
%b = mFloor(%b);
if(!%verbose)
  return %a SPC %b SPC %c;
if(%verbose != 2) {
  if(strLen(%a) == 1)
    %d = "0";
  %d = %d @ %a @ ":";
  if(strLen(%b) == 1)
    %d = %d @ "0";
  %d = %d @ %b @ ":";
  if(strLen(%c) == 1)
    %d = %d @ "0";
  %d = %d @ %c;
  return %d;
}
//lol long
if(%a && %b && %c) %d = %a SPC "hours," SPC %b SPC "minutes, and" SPC %c SPC "seconds";
if(%a && %b && !%c) %d = %a SPC "hours and" SPC %b SPC "minutes";
if(%a && !%b && %c) %d = %a SPC "hours and" SPC %c SPC "seconds";
if(%a && !%b && !%c) %d = %a SPC "hours";
if(!%a && %b && %c) %d = %b SPC "minutes and" SPC %c SPC "seconds";
if(!%a && %b && !%c) %d = %b SPC "minutes";
if(!%a && !%b && %c) %d = %c SPC "seconds";
if(!%a && !%b && !%c) %d = "0 seconds"; //How the hell?
if(%a == 1) %d = strReplace(%d, "hours", "hour");
if(%b == 1) %d = strReplace(%d, "minutes", "minute");
if(%c == 1) %d = strReplace(%d, "seconds", "second");
return %d;
}

function godSay(%t) {
serverCmdMessageSent($god, %t);
}

function healMe() {
getLocal().player.applyRepair(1000000);
}

function IRC(%text) {
serverIRCchat($pref::player::name @ ": " @ %text);
}

function isCrouching(%player) {
return ((getWord(%player.getWorldBox(), 5) - getWord(%player.getWorldBox(), 2)) < 1.5 * getWord(%player.getScale(), 2));
}

function isTempBrick(%brick) {
for( %i = 0; %i < ClientGroup.getCount(); %i++) {
  if(%brick == Clientgroup.getobject(%i).player.tempbrick)
    return 1;
}
return 0;
}

function onGround(%obj, %dist) {
return !!ContainerRayCast(%obj.getTransform(), vectorAdd(%obj.getTransform(), "0 0" SPC (%dist ? -%dist : -0.6)), $TypeMasks::ShapeBaseObjectType | $TypeMasks::TerrainObjectType | $TypeMasks::InteriorObjectType, %obj); //Longest
}

function playSound(%sound) {
evalall("ServerPlay3D("@%sound@", vectoradd(%client.player.getTransform(), \"0 0 1\") );");
}

function qupdateClients() {
evalall("%client.qupdate();");
}

function rebootIRC() {
serverIRCDisconnect();
serverIRCInit();
}

function returnPlayersOnTeam(%team, %mustBeAlive) {
%count = 0;
for(%i = 0; %i < ClientGroup.getCount(); %i++) {
  if(%team $= Clientgroup.getObject(%i).team) {
    %count++;
    if(%mustBeAlive == 1 && !isObject(Clientgroup.getObject(%i).player))
      %count--;
  }
}
return %count;
}

function support_distance(%a1, %a2) {
//A1 = point, a2 = point; returns distance between two points
//Or just a1 = vector; returns scalar of vector
%a1 = vectorSub(%a1, %a2);
return mSqrt(mPow(getWord(%a1, 0), 2) + mPow(getWord(%a1, 1), 2) + mPow(getWord(%a1, 2), 2));
}

function vectortoEuler(%vector) {
%length = vectorLen(%vector);
%x = getWord(%vector, 0) / %length;
%y = getWord(%vector, 1) / %length;
%z = getWord(%vector, 2) / %length;
%p = mATan(%x, %y) * 180 / $pi;
%y = mATan(%z, support_distance(%x SPC %y SPC "0")) * 180 / $pi;
return %y SPC "0" SPC %p;
}

//The same format as the above (alphabetized class functions, alphabetized console funcitons), in package form.

package Wiggy_Server {
function SimGroup::add(%this, %obj) { //Replace with onAddProjectile
Parent::Add(%this, %obj);
if(nameToID(%this) == nameToID(MissionCleanup)) {
  if(%obj.getClassName() $= "Projectile") {
    %obj.creationTime = getSimTime();
    %obj.velocity = %obj.initialVelocity;
    %obj.initPos = %obj.initialPosition;
  }
}
}

function Encode_Object(%block, %origin, %fade) {
if(!%block.noSave)
  return Parent::Encode_Object(%block, %origin, %fade);
}

function saveBlocks(%map, %persist) {
for(%i = 0; %i < MissionCleanup.getCount(); %i++) {
  if((%block = MissionCleanup.getObject(%i)).noSave) {
    MissionCleanup.remove(%block);
    MissionCleanup.schedule(0, add, %block);
    %i--;
  }
}
return Parent::saveBlocks(%map, %persist);
}
};
activatepackage(Wiggy_Server);
