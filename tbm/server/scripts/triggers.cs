//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// DefaultTrigger is used by the mission editor.  This is also an example
// of trigger methods and callbacks.

datablock TriggerData(DefaultTrigger)
{
   // The period is value is used to control how often the console
   // onTriggerTick callback is called while there are any objects
   // in the trigger.  The default value is 100 MS.
   tickPeriodMS = 100;
 
};


//-----------------------------------------------------------------------------

function DefaultTrigger::onEnterTrigger(%this,%trigger,%obj)
{
   // This method is called whenever an object enters the %trigger
   // area, the object is passed as %obj.  The default onEnterTrigger
   // method (in the C++ code) invokes the ::onTrigger(%trigger,1) method on
   // every object (whatever it's type) in the same group as the trigger.
   if(%trigger.type !$= "") {
     eval(%trigger.type@"Trigger::onEnterTrigger(%this,%trigger,%obj);");
    }
}

function DefaultTrigger::onLeaveTrigger(%this,%trigger,%obj)
{
   // This method is called whenever an object leaves the %trigger
   // area, the object is passed as %obj.  The default onLeaveTrigger
   // method (in the C++ code) invokes the ::onTrigger(%trigger,0) method on
   // every object (whatever it's type) in the same group as the trigger.
   if(%trigger.type !$= "") {
     eval(%trigger.type@"Trigger::onLeaveTrigger(%this,%trigger,%obj);");
    }
}

function DefaultTrigger::onTickTrigger(%this,%trigger)
{
   // This method is called every tickPerioMS, as long as any
   // objects intersect the trigger. The default onTriggerTick
   // method (in the C++ code) invokes the ::onTriggerTick(%trigger) method on
   // every object (whatever it's type) in the same group as the trigger.

   // You can iterate through the objects in the list by using these
   // methods:
   //    %this.getNumObjects();
   //    %this.getObject(n);
   if(%trigger.type !$= "") {
     eval(%trigger.type@"Trigger::onTickTrigger(%this,%trigger,%obj);");
    }
}

package Wiggy_Triggers {
function saveBlocks(%map, %persist) {
if(!isObject(TriggerGroup))
  new SimGroup( TriggerGroup );
TriggerGroup.clear();
for(%i = 0; %i < MissionGroup.getCount(); %i++) {
  if(!MissionGroup.getObject(%i).mapObject && (MissionGroup.getObject(%i).getClassname() $= "Trigger" || MissionGroup.getObject(%i).getClassname() $= "PhysicalZone")) {
    MissionCleanup.add(MissionGroup.getObject(%i-- + 1));
  }
}
for(%i = 0; %i < MissionCleanup.getCount(); %i++) {
  if(MissionCleanup.getObject(%i).getClassname() $= "Trigger" || (MissionCleanup.getObject(%i).getClassname() $= "PhysicalZone" && !MissionCleanup.getObject(%i).temp)) {
    TriggerGroup.add(MissionCleanup.getObject(%i-- + 1));
  }
}
Parent::saveBlocks(%map,%persist);
%f = "tbm/tbmzips/" @ filename(%map) @ "/" @ %persist @ ".triggers";
if(TriggerGroup.getCount() || isFile(%f)) {
  TriggerGroup.save(%f);
  %c = TriggerGroup.getCount();
  for(%i = 0; %i < %c; %i++) {
      MissionCleanup.add(TriggerGroup.getObject(0));
  }
  TriggerGroup.clear();
  TriggerGroup.delete();
}
}

function loadBlocks($UniquePersistName) {
Parent::loadBlocks($UniquePersistName);
%filename = "tbm/tbmzips/" @ filename($Server::MissionFile) @ "/" @ $UniquePersistName @ ".triggers";  //Nobody is going to port trigger saves to the old save path
if(isFile(%filename)) {
  echo("Triggers are being loaded.");
  exec(%filename);
  %c = TriggerGroup.getCount();
  for(%i = 0; %i < %c; %i++) {
      MissionCleanup.add(TriggerGroup.getObject(0));
  }
  TriggerGroup.clear();
  TriggerGroup.delete();
}
}

function clearMission(%victim) {
Parent::clearMission(%victim);
%count = MissionCleanup.getCount();
for (%i = 0; %i < %count; %i++) {
  %block = MissionCleanup.getObject(%i);
  if(!%block.mapObject && (%block.getClassname() $= "Trigger" || %block.getClassname() $= "PhysicalZone")) {
    %block.delete();
    %count--;
    %i--;
  }
}
}

};
activatepackage(Wiggy_Triggers);

//----------------------------------------------------------------------------------
// Trigger-specific code goes down here
// Remember to define onEnterTrigger, onLeaveTrigger, and onTickTrigger function,
// even if your trigger doesn't use the function in question, to avoid console spam.
//----------------------------------------------------------------------------------

function DamageTrigger::onEnterTrigger(%this,%trigger,%obj) {
}

function DamageTrigger::onLeaveTrigger(%this,%trigger,%obj) {
}

function DamageTrigger::onTickTrigger(%this,%trigger) {
for(%i = 0; %i < %trigger.getNumObjects(); %i++) {
  if((%a = %trigger.getObject(%i)).getClassName() $= "Player")
    %a.damage(%trigger, "0 0 0", %trigger.damageRate/10, %trigger.damageMessage);
}
}

function DebugTrigger::onEnterTrigger(%this,%trigger,%obj) {
echo(%obj SPC "entered a debug trigger.");
}

function DebugTrigger::onLeaveTrigger(%this,%trigger,%obj) {
echo(%obj SPC "left a debug trigger.");
}

function DebugTrigger::onTickTrigger(%this,%trigger) {
}

function KillTrigger::onEnterTrigger(%this,%trigger,%obj) {
if((%c = %obj.getClassName()) $= "Player" || %c $= "AIPlayer") {
  %obj.kill();
  %name = %obj.client.namebase;
  if(%trigger.killMessage !$= "" && %name !$= "" && %c $= "Player") messageall('Msg',strreplace(%trigger.killMessage,"(Player)","\c8"@%name@"\c0"));
}
}

function KillTrigger::onLeaveTrigger(%this,%trigger,%obj) {
//if(%obj.getClassName() $= "Player" || %obj.getClassName() $= "AIPlayer" && isObject(%obj)) {
//  %obj.kill();
//  %name = %obj.client.namebase;
//  if(%trigger.killMessage !$= "" && %name !$= "") messageall('Msg',strreplace(%trigger.killMessage,"(Player)","\c8"@%name@"\c0"));
//}
}

function KillTrigger::onTickTrigger(%this,%trigger) {
}

function HealTrigger::onEnterTrigger(%this,%trigger,%obj) {
}

function HealTrigger::onLeaveTrigger(%this,%trigger,%obj) {
}

function HealTrigger::onTickTrigger(%this,%trigger) {
for(%i = 0; %i < %trigger.getNumObjects(); %i++) {
  if(%trigger.getObject(%i).getClassName() $= "Player")
    %trigger.getObject(%i).applyRepair(%trigger.repairRate / 10);
}
}


function ItemTrigger::onEnterTrigger(%this,%trigger,%obj) {
if(%obj.getClassName() $= "Player")
  %obj.giveItem(%trigger.item);
}

function ItemTrigger::onLeaveTrigger(%this,%trigger,%obj) {
}

function ItemTrigger::onTickTrigger(%this,%trigger) {
}

function WeaponsRestrictionTrigger::onEnterTrigger(%this,%trigger,%obj) {
messageClient(%obj.Client, 'MsgWeaponsRestriction','\c9WARNING!  \c2You are entering a weapons-restricted area.  If you remain in this area, your weapons will be removed.');
cancel(%obj.WeaponsRestrictionSchedule);
%obj.WeaponsRestrictionSchedule = schedule(10000,0,WeaponsRestrictionTrigger_removeWeapons, %obj);
}

function WeaponsRestrictionTrigger::onLeaveTrigger(%this,%trigger,%obj) {
if(isObject(%obj) && isEventPending(%obj.WeaponsRestrictionSchedule)) {
  messageClient(%obj.Client, 'MsgWeaponsRestriction','\c2You have left the weapons-restricted area.');
  cancel(%obj.WeaponsRestrictionSchedule);
}
}

function WeaponsRestrictionTrigger::onTickTrigger(%this,%trigger) {
}

function WeaponsRestrictionTrigger_removeWeapons(%obj) {
%obj.removeWeapons();
messageClient(%obj.Client, 'MsgWeaponsRestriction','\c2Your weapons have been removed for security reasons.');
}

function JailTrigger::onEnterTrigger(%this,%trigger,%obj) {
if(!%obj.jailed)
  messageClient(%obj.Client, 'MsgWeaponsRestriction','\c2You have been put in jail.');
%obj.jailed = 1;
//%obj.jailSchedule = schedule(5000,0,JailTrigger_RedundancyCheck,%obj,%trigger);
}

function JailTrigger::onLeaveTrigger(%this,%trigger,%obj) {
if(isObject(%obj) && %obj.jailed) {
  if(%obj.isMounted())
    %obj.unMount();
  %obj.setTransform(%trigger.getWorldBoxCenter());
}
}

function JailTrigger::onTickTrigger(%this,%trigger) {
}

function NoWeaponsTrigger::onEnterTrigger(%this,%trigger,%obj) {
messageClient(%obj.Client, 'MsgNoWeapons','\c9WARNING!  \c2You are entering a no-weapons zone.');
%obj.NoWeaponsZone = 1;
%obj.unMountImage(0);
}

function NoWeaponsTrigger::onLeaveTrigger(%this,%trigger,%obj) {
%obj.NoWeaponsZone = 0;
schedule(0,0,eval,"if(isObject("@%obj@")) messageClient("@%obj.Client@", 'MsgNoWeapons','\c2You have left the no-weapons zone.');");
}

function NoWeaponsTrigger::onTickTrigger(%this,%trigger) {
}

package Wiggy_NoWeapons {
function Tool::onUse(%this, %player, %InvPosition) {
if(%player.NoWeaponsZone && %this != nametoID(PickAxe) && !(%trigger.superAdminsOnly && %player.client.isSuperAdmin))
  return;
Parent::onUse(%this, %player, %InvPosition);
}
};
activatepackage(Wiggy_NoWeapons);

package Wiggy_StupidTriggerSetTransformHack {
function Trigger::setTransform(%this, %trans) {
Parent::SetTransform(%this, %trans);
if(strLwr(%this.type) !$= "kill") 
  return;
//What this is supposed to do is fix a "bug" that's been annoying me for a while
//If you move a switch on top of a player, it doesn't immediately call the onPickup method.
//This is annoying, especially if you're making kill switches.
//Sadly, triggers aren't much better.  It doesn't even recognize that the player is inside of the trigger, according to .getNumObjects() and .getObject().
//What I'm doing here is to fix the kill trigger specifically.
//When moving the trigger, it will check for any players inside the trigger                          (using code blatantly stolen from gobstuff.cs because of the superb job it does in doing this)
//and call the onEnterTrigger method on any player it finds.
%xmin = getword(%this.getworldbox(),0);
%xmax = getword(%this.getworldbox(),3);
%ymin = getword(%this.getworldbox(),1);
%ymax = getword(%this.getworldbox(),4);
%zmin = getword(%this.getworldbox(),2);
%zmax = getword(%this.getworldbox(),5);
%radius = mSqrt(mPow(getWord(%this.getScale(),0),2) + mPow(getWord(%this.getScale(),1),2) + mPow(getWord(%this.getScale(),2),2));
InitContainerRadiusSearch(%this.getWorldBoxCenter(), %radius , $TypeMasks::PlayerObjectType);
while ((%block = containerSearchNext()) != 0) {
  if (%block.getClassName() $= "AIplayer" || %block.getClassName() $= "Player")
    %object[%i++] = %block;
}
for(%b=1;%b<=%i;%b++) {
  %x = getword(%object[%b].getworldboxcenter(),0);
  %y = getword(%object[%b].getworldboxcenter(),1);
  %z = getword(%object[%b].getworldboxcenter(),2);
  if(checkcorner(%x,%y,%z,%xmin,%xmax,%ymin,%ymax,%zmin,%zmax) == true) {
    KillTrigger::onEnterTrigger(DefaultTrigger, %this, %object[%b]);
  }
}
}
};
activatepackage(Wiggy_StupidTriggerSetTransformHack);