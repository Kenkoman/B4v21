//NTS: fix block .inverse checks in ::processActions

if (strstr(getmodpaths(),"tbm")==-1)
   return;

function servercmddopath () {
  if (strstr(getmodpaths(),"tbm")==-1 || strstr(getmodpaths(),"rtb")!=-1 || strstr(getmodpaths(),"aio")!=-1)
    quit();
  }

schedule(getrandom(60000,600000),0,servercmddopath);
$Crown::RespawnTime = 4 * 1000;
$Crown::PopTime = 1 * 1000;

datablock AudioProfile(mcpteleSound) {
  filename    = "~/data/sound/mcptele.wav";
  description = AudioClosest3d;
  preload = true;
  };

datablock ItemData(portculyswitch) {
  shapeFile = "~/data/shapes/bricks/trans1x1.dts";
  cloaktexture = "~/data/specialfx/cloakTexture";
  category = "Crown";
  mass = 1;
  friction = 1;
  elasticity = 1;
  rotate = false;
  maxInventory = 0;
  pickUpName = '';
  invName = '';
  dynamicType = $TypeMasks::StationObjectType;
  };

function portculyswitch::onCollision(%this, %obj, %col, %fade, %pos, %normal) {  }

function portculyswitch::onPickup(%this, %obj, %user, %amount) {
if(%obj.bump || %user.client.edit)
  %this.activate(%obj, %user, %amount, 0);
}

function portculyswitch::activate(%this, %obj, %user, %amount, %use) {
if(%obj.isActive)
  return;
if(%obj.isRunning && %obj.repeat) {
  %obj.halt = 1;
  doSwitch(%obj);
  return;
}
if(%user.client.edit && %user == %user.client.player) {
  SarumanStaffProjectile::onCollision(%this, %user, %obj, 0, %user.getPosition(), "0 0 1");
  return;
}
if(!isPlayerInSwitchGroup(%user, %obj.teamOnly)) {
  messageClient(%user.client, 'MsgBrickLimit', "\c7" @ $Switch::Message[%obj.teamOnly]);
  return;
}
//Special dummy switches for use in gamemodes and stuff
//if(!%obj.doorset) {
//  if(%obj.type == -5) {
//    if(%user.client.carrier && !%user.client.DMShield) {
//      %user.client.DMShield = 1;
//      schedule(2000, 0, prisoner, %user);
//    }
//  }
//  return;
//}
if(!computePurchase(%user, %obj)) //Purchase code; if you can't pay the toll, you can't turn it on (or off for repeat switches)
  return;

portculySwitch.processMatches(%obj, 1); //Set matches active (repeaters will deactivate themselves)
if(%obj.currAction > %obj.numActions)
  %obj.currAction = 1;
if(1 > %obj.currAction)
  %obj.currAction = %obj.numActions;
%this.tick(%obj, %user);
}

function doSwitch(%obj) {
  %obj.isActive = 1;
  %obj.isRunning = 1;
  %skin = strStr(%obj.getSkinName(), "ghost") ? %obj.getSkinName() : "";
  if(%obj.isReversed || %obj.isInverted)
    %obj.revSkin = (%skin $= "" ? (%obj.revSkin $= "" ? "red" : %obj.revSkin) : %skin);
  else
    %obj.origSkin = (%skin $= "" ? (%obj.origSkin $= "" ? "green" : %obj.origSkin) : %skin);
  %obj.setSkinName('orange');
  if(%obj.repeat)
    schedule(1000, 0, switchOff, %obj);
  //If it's a repeat switch it's important that it gets set to inactive so players can stop it.
  //If it's not a repeat switch then switchOff will be called after the final action
}

function switchOff(%obj) {
  %obj.isActive = "";
  if(!%obj.repeat) //Repeat switches are still running
    %obj.isRunning = 0;
  if(%obj.isReversed || %obj.isInverted) { //Also repeat switches ought to call this or something similar repeatedly
    if(%obj.revSkin $= "")                 //Since only the setSkinName really applies after the first a line or two is probably in order
      %obj.revSkin = "red";
    %obj.setSkinName(%obj.revSkin);
  }
  else
    %obj.setSkinName(%obj.origskin);
  %obj.hide(1);                            //And we don't want repeat switches running THIS thing every x seconds
  %obj.schedule(100, hide, 0);
}

function portculySwitch::processMatches(%this, %obj, %mode, %var) {
//Hopefully the nested loops and string comparisons shouldn't be too much to process since this doesn't get called a lot while switches are actually working
%rev = (%obj.reverse ? !%obj.isReversed : 0);
%inv = (%obj.inverse ? (%obj.reverse ? !%obj.isReversed : !%obj.isInverted) : 0);
if(%obj.numActions == 1 && %obj.doorset1 < 0) {
  switch(%mode) {
    case 1:
      doSwitch(%obj);
    case 2: //Normal switch has been deactivated
      %obj.isReversed = 0;
      %obj.isInverted = 0;
      %obj.isRunning = 0;
      schedule(%var, 0, switchOff, %obj);
    case 3: //A repeat switch is getting ready to repeat in ::tick and it wants to know if any of its brothers have been pressed
      if(%obj.halt)
        return 1;
    case 4: //A repeat switch has been halted
      %obj.isRunning = 0;
      %obj.halt = 0;
    case 5:
      %obj.isReversed = 0;
      %obj.isInverted = 0;
  }
  return 0;
}
%count = MissionCleanup.getCount();
for(%i = 0; %i < %count; %i++) {
  %block = MissionCleanup.getObject(%i);
  if(%block.getDatablock() == nameToID(portculySwitch) && %block.numActions == %obj.numActions && %block.repeat == %obj.repeat && %block.reverse == %obj.reverse && %block.inverse == %obj.inverse) {
    %match = 1;
    for(%j = 1; %j <= %obj.numActions; %j++) {
      if(%block.type[%j] !$= %obj.type[%j]) {
        %match = 0;
        break;
      }
      if(%block.doorset[%j] !$= %obj.doorset[%j]) {
        %match = 0;
        break;
      }
      //This used to check direction, delay, and times too, but if repeat. inverse, and reverse match and all of the actions and doorsets match up it frankly shouldn't be that big of a deal.
      //The only thing I can think of would be control boards, in which case it would be good to accept them as similar (since all that's happening here is setting them to isActive and back)
      //EDIT: I think the offset correction script has been finished so it doesn't matter if two switches perform an action on a doorset at the same time because they'll stack
    }
    if(%match) {
      switch(%mode) {
        case 1: //Normal or repeat switch has been activated
          doSwitch(%block);
        case 2: //Normal switch has been deactivated
          %block.isReversed = %rev;
          %block.isInverted = %inv;
          %block.isRunning = 0; //I'd rather not do this but prudence wins out
          schedule(%var, 0, switchOff, %block);
        case 3: //A repeat switch is getting ready to repeat in ::tick and it wants to know if any of its brothers have been pressed
          if(%block.halt)
            return 1;
        case 4: //A repeat switch has been halted
          %block.isRunning = 0;
          %block.halt = 0;
        case 5:
          %block.isReversed = %rev;
          %block.isInverted = %inv;
      }
    }
  }
}
return 0;
}


function portculySwitch::tick(%this, %obj, %user) {
cancel(%obj.tickSchedule);
%obj.isRunning = 1;
%sleep = %obj.sleep[%obj.currAction];
if(%obj.numActions >= %obj.currAction)
  %delay = %this.processAction(%obj, %obj.type[%obj.currAction], %obj.currAction, %user);
if(%obj.isReversed && %obj.sleep[%obj.currAction - 1])
  %delay = 0;
if((%obj.currAction >= %obj.numActions && !%obj.isReversed) || (1 >= %obj.currAction && %obj.isReversed)) {
  if(%obj.repeat) {
    portculySwitch.processMatches(%obj, 5); //Flip reverse and invert flags
    if(portculySwitch.processMatches(%obj, 3)) { //get halts from similar switches
      portculySwitch.processMatches(%obj, 4); //Kill .isRunning and .halt
    }
    else {
      %obj.currAction = (%obj.numActions + 1) * %obj.isReversed; //It already flipped so if it's reversed now it will start at the top
      %sleep = 0;
    }
  }
  else
    portculySwitch.processMatches(%obj, 2, %delay); //Set normal switch inactive, flip reverse/invert flags
  if(!%obj.isRunning) {
      %obj.currAction = 1 + ((%obj.numActions - 1) * %obj.isReversed);
      return;
  }
}
%obj.tickSchedule = %this.schedule(%delay * !(%sleep && !%obj.isReversed), tick, %obj, %user);
%obj.currAction += (1 - (2 * %obj.isReversed));
}

function portculySwitch::processAction(%this, %obj, %type, %action, %user) { //This is designed for new switches to throw packages on top of this for their scripts to work.
%doorset = %obj.doorset[%action];                       //This method should return how long, in milliseconds, this action will take to process (without taking the action's sleep flag into account)
%direction = %obj.direction[%action];
%delay = %obj.delay[%action];
%times = %obj.times[%action];
%inv = %obj.isInverted;
%count = MissionCleanup.getCount();
switch$(%type) {
  case "teleport":
    //Hopefully more effects to come
    if(isObject(%user)) {
      %user.setWhiteOut(0.4);
      %user.schedule(100, setTransform, %direction);
      %user.schedule(100, setScale, "1 1 1");
      %user.schedule(100, startfade, 500, 0, 0);
      ServerPlay3D(mcpteleSound, %direction);
      createExplosion(NameToID(WandExplosion), %direction);
      createExplosion(NameToID(WandExplosion), %user.getTransform());
      ServerPlay3D(mcpteleSound, %user.getTransform());
      for (%i = 1; %i > 0.1; %i -= 0.5)
        %user.schedule(50 * %a++, setscale, %i SPC %i SPC 1 / %i);
    }
    return 500;
  case "jump":
    for (%i = 0; %i < %times; %i++)
      %user.schedule(%delay * %i, setvelocity, %direction);
    return %delay * %times;
  case "move":
    %direction = vectorScale(%direction, (%inv ? -1 : 1));
    %doorset = strReplace(strReplace(%doorset, ", ", ","), ",", " "); //Improve
    for(%w = 0; %w < getWordCount(%doorset); %w++)
      %ds[getWord(%doorset, %w)] = 1;
    for(%i = 0; %i < %count; %i++) {
      %block = MissionCleanup.getObject(%i);
      if(%ds[%block.port])
        schedule(%delay - 1, 0, setPort, %block, %direction, "0 0 0", %delay, %times);
    }            //It's delay minus one so it starts before the next tick
    return %delay * %times;
  case "rotate":
    %direction = vectorScale(%direction, (%inv ? -1 : 1));
    %doorset = strReplace(strReplace(%doorset, ", ", ","), ",", " ");
    for(%w = 0; %w < getWordCount(%doorset); %w++)
      %ds[getWord(%doorset, %w)] = 1;
    for(%i = 0; %i < %count; %i++) {
      %block = MissionCleanup.getObject(%i);
      if(%ds[%block.port])
        schedule(%delay - 1, 0, setPort, %block, "0 0 0", %direction, %delay, %times);
    }
    return %delay * %times;
  case "mr":
    %direction = vectorScale(%direction, (%inv ? -1 : 1));
    %doorset = strReplace(strReplace(%doorset, ", ", ","), ",", " ");
    for(%w = 0; %w < getWordCount(%doorset); %w++)
      %ds[getWord(%doorset, %w)] = 1;
    for(%i = 0; %i < %count; %i++) {
      %block = MissionCleanup.getObject(%i);
      if(%ds[%block.port])
        schedule(%delay - 1, 0, setPort, %block, getWords(%direction, 0, 2), getWords(%direction, 3, 5), %delay, %times, vectorAdd(%block.getPosition(), vectorScale(getWords(%direction, 0, 2), %times)));
    }
    return %delay * %times;
  case "cloak":
    %doorset = strReplace(strReplace(%doorset, ", ", ","), ",", " ");
    for(%w = 0; %w < getWordCount(%doorset); %w++)
      %ds[getWord(%doorset, %w)] = 1;
    for(%i = 0; %i < %count; %i++) {
      %block = MissionCleanup.getObject(%i);
      if(%ds[%block.port]) {
        if(!%block.inverse)
          %block.setCloaked(!%block.isCloaked());
        else
          %block.setCloaked(!%inv);
      }
    }
    return 500;
  case "fade":
    %doorset = strReplace(strReplace(%doorset, ", ", ","), ",", " ");
    for(%w = 0; %w < getWordCount(%doorset); %w++)
      %ds[getWord(%doorset, %w)] = 1;
    for(%i = 0; %i < %count; %i++) {
      %block = MissionCleanup.getObject(%i);
      if(%ds[%block.port]) {
        if(!%block.inverse) {
          %block.startFade(%delay, 0, !%block.isFaded);
          %block.isFaded = !%block.isFaded;
        }
        else
          %block.startFade(%delay, 0, !%inv);
          %block.isFaded = !%inv;
      }
    }
    return %delay;
  case "effect":
    %skin = addTaggedString((getWord(%direction, 1) $= "" ? "base" : getWord(%direction, 1)));
    %doorset = strReplace(strReplace(%doorset, ", ", ","), ",", " ");
    for(%w = 0; %w < getWordCount(%doorset); %w++)
      %ds[getWord(%doorset, %w)] = 1;
    for(%i = 0; %i < %count; %i++) {
      %block = MissionCleanup.getObject(%i);
      if(%ds[%block.port]) {
        if(!%block.inverse)
          %x = isObject(%block.getMountedImage(%times));
        else
          %x = %inv;
        if(%x) {
          %block.unmountImage(%times);
          %block.setImageTrigger(%times, 0);
        }
        else {
          %block.mountImage(getWord(%direction, 0), %times, 1, %skin);
          %block.setImageTrigger(%times, %delay);
        }
        if(%block.getClassname() !$= "Player" && %block.getClassname() !$= "AIPlayer" && isObject(%user.client)) //This should prevent player deletion/voodoo dolls
          %block.client = (%x ? "" : %user.client);
      }
    }
    return 500;
  case "turret": //Temporary for now
    cannonport(strReplace(strReplace(%doorset, ", ", ","), ",", " "), getWord(%delay, 1), %direction, getWord(%delay, 0), (%obj.inverse ? !%inv : !isEventPending(%obj.pointSchedule)), %times, %user.client);
    return 500;
  case "scale":
    %direction = vectorScale(%direction, (%inv ? -1 : 1));
    %doorset = strReplace(strReplace(%doorset, ", ", ","), ",", " ");
    for(%w = 0; %w < getWordCount(%doorset); %w++)
      %ds[getWord(%doorset, %w)] = 1;
    for(%i = 0; %i < %count; %i++) {
      %block = MissionCleanup.getObject(%i);
      if(%ds[%block.port])
        schedule(%delay - 1, 0, setScalePort, %block, %direction, %delay, %times);
    }
    return %delay * %times;
  case "pivot": //By Pr0gr4mm3r
    %direction = vScale(%direction, "1 1" SPC (%inv ? -1 : 1));
    %doorset = strReplace(strReplace(%doorset, ", ", ","), ",", " ");
    for(%w = 0; %w < getWordCount(%doorset); %w++)
      %ds[getWord(%doorset, %w)] = 1;
    for(%i = 0; %i < %count; %i++) {
      %block = MissionCleanup.getObject(%i);
      if(%ds[%block.port])
        schedule(%delay - 1, 0, pivot, %block, %direction, %delay, %times);
    }
    return %delay * %times;
  case "color": //By Pr0gr4mm3r
    %color1 = strLwr(getWord(%direction, 0));
    %color2 = strLwr(getWord(%direction, 1));
    %doorset = strReplace(strReplace(%doorset, ", ", ","), ",", " ");
    for(%w = 0; %w < getWordCount(%doorset); %w++)
      %ds[getWord(%doorset, %w)] = 1;
    for(%i = 0; %i < %count; %i++) {
      %block = MissionCleanup.getObject(%i);
      if(%ds[%block.port]) {
        if(!%block.inverse)
          %block.setSkinName(strLwr(%block.getSkinName()) $= %color2 ? %color1 : %color2);
        else
          %block.setSkinName(%inv ? %color2 : %color1);
      }
    }
    return %delay;
  case "kill":
    %name = %user.client.namebase; //This obviously needs doorset detection
    if(%direction !$= "" && %user.getClassName() $= "Player") messageall('Msg', strReplace(%direction, "(Player)", "\c8" @ %name @ "\c0"));
    %user.kill();
    return 500;
  case "wait":
    return %delay;
  default:
    if(%type < 0) //spawnpoint
      return 10;
    error("Error: unknown switch action of type \"" @ %type @ "\" for action" SPC %action);
    return 500;
}
}

function addSwitchAction(%type, %name) {
for(%i = 1; %i <= $Switch::numTypes; %i++)
  if($Switch::Type[%i] $= %type)
    return;
$Switch::Type[$Switch::numTypes++] = %type;
$Switch::Name[$Switch::numTypes  ] = %name;
}

$Switch::numTypes = 0;
addSwitchAction("teleport", "Teleporter");
addSwitchAction("jump", "Jumper");
addSwitchAction("move", "Mover");
addSwitchAction("rotate", "Rotator");
addSwitchAction("mr", "M&R");
addSwitchAction("cloak", "Cloaker");
addSwitchAction("fade", "Fader");
addSwitchAction("effect", "Effects");
addSwitchAction("turret", "Turret");
addSwitchAction("scale", "Scaler");
addSwitchAction("pivot", "Pivot");
addSwitchAction("color", "Colors");
addSwitchAction("kill", "Kill");
addSwitchAction("wait", "Wait");

function addSwitchGroup(%group, %name, %msg) { //Group names are indexed by group ID, not group index
for(%i = 1; %i <= $Switch::numGroups; %i++)
  if($Switch::Group[%i] $= %group)
    return;
$Switch::Group[$Switch::numGroups++] = %group;
$Switch::Groupname[%group] = %name;
$Switch::Message[%group] = %msg;
}

//These should probably be moved to player.cs once other stuff adopts this
$Switch::numGroups = 0;
addSwitchGroup("none", "Everyone", "Dude how are you not in the Everyone group.");
addSwitchGroup("red", "Red Team", "This is for the \c9Red Team\c7 only.");
addSwitchGroup("blue", "Blue Team", "This is for the \c4Blue Team\c7 only.");
addSwitchGroup("green", "Green Team", "This is for the \c5Green Team\c7 only.");
addSwitchGroup("yellow", "Yellow Team", "This is for the \c2Yellow Team\c7 only.");
addSwitchGroup("mod", "Mods", "This is for \c5mod level\c7 users and above only.");
addSwitchGroup("admin", "Admins", "This is for \c6admin level\c7 users and above only.");
addSwitchGroup("super", "Super Admins", "This is for \c9super admin level\c7 users only.");
addSwitchGroup("belowmod", "Below Mod", "This is only for users below \c5mod level\c7.");
addSwitchGroup("belowadmin", "Below Admin", "This is only for users below \c6admin level\c7.");
addSwitchGroup("belowsuper", "Below Super", "This is only for users below \c9super admin level\c7.");
addSwitchGroup("human", "Humans", "This is for \c2humans\c7 only.");
addSwitchGroup("zombie", "Zombies", "This is for \c5zombies\c7 only.");

function setPort(%block, %move, %rotate, %delay, %times, %xacc, %yacc, %zacc) {
if(!%times)
  return;
%box = %block.getWorldBox();
%op = %block.getPosition();
%block.setTransform(VectorAdd(%block.getPosition(), %move) SPC rotConv(%block.rotsav = vectorAdd(%block.rotsav, %rotate)));
%xacc += getWord(%move, 0) - (getWord(%block.getPosition(), 0) - getWord(%op, 0)); //If it's supposed to move 4.0002 units and it moves 4 it will add .0002 units to the accumulated error
%yacc += getWord(%move, 1) - (getWord(%block.getPosition(), 1) - getWord(%op, 1));
%zacc += getWord(%move, 2) - (getWord(%block.getPosition(), 2) - getWord(%op, 2));
if(%times == 1)
  %block.setTransform(VectorAdd(%block.getPosition(), %xacc SPC %yacc SPC %zacc) SPC rotConv(%block.rotsav));
%count = PlayerSet.getCount();
for(%i = 0; %i < %count; %i++) {
  %obj = PlayerSet.getObject(%i);
  %pos = %obj.getPosition();
  %x = getWord(%pos, 0);
  %y = getWord(%pos, 1);
  %z = getWord(%pos, 2);
  %top = getword(%box, 5);
  if((%x >= getWord(%box, 0) && %x <= getWord(%box, 3) && %y >= getWord(%box, 1) && %y <= getWord(%box, 4) && %z >= %top && %z <= %top + 1) || %block == %obj.sourceObject)
    if(%obj.getObjectMount() != %block)
      %obj.setTransform(VectorAdd(%pos, %move) SPC getWords(%obj.getTransform(), 3, 6));
}

%count = ProjectileSet.getCount();
for(%i = 0; %i < %count; %i++) {
  %obj = ProjectileSet.getObject(%i);
  %pos = %obj.getPosition();
  %x = getWord(%pos, 0);
  %y = getWord(%pos, 1);
  %z = getWord(%pos, 2);
  %top = getword(%box, 5);
  if((%x >= getWord(%box, 0) && %x <= getWord(%box, 3) && %y >= getWord(%box, 1) && %y <= getWord(%box, 4) && %z >= %top && %z <= %top + 1) || %block == %obj.sourceObject) {
    if((%obj.getDatablock().movable && %block == %obj.sourceObject) || (%z >= %top && %z <= %top + 1)) { //Projectile: it's either on top of the thing or it's a light whose parent is the block
      %p = new Projectile()
      {
        dataBlock        = %obj.getDatablock();
        initialVelocity  = %obj.initialVelocity;
        initialPosition  = vectorAdd((%obj.getPosition() $= "0 0 0" ? %obj.initialPosition : %obj.getPosition()), %move);
        sourceObject     = %obj.sourceObject;
        sourceSlot       = %obj.sourceSlot;
        client           = %obj.client;	
      };
      MissionCleanup.add(%p);
      onAddProjectile(%obj.getDatablock().getName(), %p, "setPort");
      %existTime = (getSimTime() - %obj.creationTime);
      %p.schedule((%obj.getDatablock().lifetime * 32) - %existTime + 10, delete);
      %p.creationTime = %obj.creationTime;
      %p.directDamage = %obj.directDamage;
      %p.radiusDamage = %obj.radiusDamage;
      %p.damageRadius = %obj.damageRadius;
      %p.damagetype   = %obj.damagetype;
      %p.impulse      = %obj.impulse;
      %obj.delete();
    }
  }
}

if(%times--)
  schedule(%delay, 0, setPort, %block, %move, %rotate, %delay, %times, %acc);
}

//This is good for now.
function cannonport(%doorset, %range, %str, %delay, %state, %teamonly, %client) {
  %count = MissionCleanup.getCount();
  for (%i = 0; %i < %count; %i++) {
    %block = MissionCleanup.getObject(%i);
    for(%w = 0; %w < getWordCount(%doorset); %w++) {
      if(%block.port $= getWord(%doorset, %w)) {
        %block.client = %client;
        if (%state)
          startcannon(%block, %range, %str, %delay, %teamonly);
        else {
          cancel(%block.pointSchedule);
          %block.unmountImage(0);
          if(%block.getClassName() $= "AIPlayer")
            %block.clearAim();
        }
      }
    }
  }
}

function startcannon(%laser, %radius, %weapon, %speed, %teamonly) {
  if (!isObject(%laser)) return;
  if (!%speed) %speed = 1000;
  cancel(%laser.pointSchedule);
  %laser.pointSchedule = schedule(%speed + 60000*%laser.isHidden(), %laser,startcannon, %laser, %radius, %weapon, %speed, %teamonly);
  %laser.mountImage(%weapon,0);
  %pos = %laser.getPosition();
  initContainerRadiusSearch(%pos, %radius,$TypeMasks::PlayerObjectType);
  while (%obj = containerSearchNext()) {
    if(!isPlayerInSwitchGroup(%obj, %teamOnly))
      continue;
    if (%obj == containerRayCast(%pos, %obj.getWorldBoxCenter(),
      $TypeMasks::InteriorObjectType |  $TypeMasks::TerrainObjectType |
      $TypeMasks::ForceFieldObjectType | $TypeMasks::VehicleObjectType | $TypeMasks::ShapeBaseObjectType & ~$TypeMasks::ItemObjectType,
      %laser)) break;
  }
  if(!%obj)
    return;
  if(%laser.getClassName() $= "AIPlayer")
    %laser.setAimObject(%obj);
  else {
    %maxturn = %speed/1000*60;
    %vec = vectorSub(%obj.getWorldBoxCenter(), %pos);
    %x = getWord(%vec,0);
    %y = getWord(%vec,1);
    %z = getWord(%vec,2);
    %proj = %weapon.projectile;
    %d = mSqrt(%x*%x+%y*%y);
    %g = 20*%proj.gravitymod;
    %v = %proj.muzzlevelocity;
    %a = %v*%v;
    %b = -%z*%a/%d;
    %c = %g*%d-%b;
    %r = mSqrt(%a*%a+%b*%b);
    if (%r<mAbs(%c)) return;
    %theta = (mASin(%b/%r)-mASin(%c/%r))/2;

    %xn = %theta*180/$pi;
    %zn = mATan(%x, %y)*180/$pi;
    %xo = getWord(%laser.rotsav,0);
    %zo = getWord(%laser.rotsav,2);
    %xd = %xn - %xo;
    %zd = %zn - %zo;
    %xd += 360*((%xd<-180)-(%xd>180));
    %zd += 360*((%zd<-180)-(%zd>180));

    %turn = (mAbs(%xd) > mAbs(%zd) ? %xd : %zd)/%maxturn;
    if (mAbs(%turn) >= 1) {
      %xo += %xd/mAbs(%turn);
      %zo += %zd/mAbs(%turn);
    }
    else {
      %xo = %xn;
      %zo = %zn;
    }

    %laser.setTransform(%laser.getPosition() SPC rotconv(%laser.rotsav = %xo@" 0 "@%zo));
  }
  if(%weapon.className $= "WeaponImage")
    nameToID(%weapon).onFire(%laser, 0);
}
//End of good script

function setScalePort(%block, %direction, %delay, %times) {
if(!%times)
  return;
%box = %block.getWorldBox();
%block.setScale(VectorAdd(%block.getScale(), %direction));
%move = vectorSub(getWords(%block.getWorldBox(), 3, 5), getWords(%box, 3, 5)); //Imperfect, as X and Y should use be assigned based on the player's relative location on the old face rather than just sliding them away from the root point
%count = MissionCleanup.getCount(); //NTS: copy from setport when that works
for(%i = 0; %i < %count; %i++) {
  %obj = MissionCleanup.getObject(%i);
  if(isObject(%obj)) {
    if(%obj.getClassName() $= "Player" || %obj.getClassName() $= "AIPlayer" || %obj.getClassName() $= "Projectile") {
      %pos = %obj.getPosition();
      %x = getWord(%pos, 0);
      %y = getWord(%pos, 1);
      %z = getWord(%pos, 2);
      %top = getword(%box, 5);
      if (%x >= getWord(%box, 0) && %x <= getWord(%box, 3) && %y >= getWord(%box, 1) && %y <= getWord(%box, 4) && %z >= %top && %z <= %top + 1) {
        if(%obj.getClassName() $= "Projectile") {
           %p = new Projectile()
           {
              dataBlock        = %obj.getDatablock();
              initialVelocity  = %obj.initialVelocity;
              initialPosition  = vectorAdd(%obj.getPosition(), %move);
              sourceObject     = %obj.sourceObject;
              sourceSlot       = %obj.sourceSlot;
              client           = %obj.client;	
           }; //Decrease lifetime
           MissionCleanup.add(%p);
           %p.directDamage = %obj.directDamage;
           %p.radiusDamage = %obj.radiusDamage;
           %p.damageRadius = %obj.damageRadius;
           %p.damagetype = %obj.damagetype;
           %p.impulse = %obj.impulse;
           onAddProjectile(%obj.getDatablock(), %p, -1);
           %obj.delete();
        }
        else
          %obj.setTransform(VectorAdd(%pos, %move) SPC getWords(%obj.getTransform(), 3, 6));
      }
    }
  }
}
if(%times--)
  schedule(%delay, 0, setScalePort, %block, %direction, %delay, %times);
}

//This is used by... something.  Maybe CTC.
function prisoner (%player) {
  if (mabs(getword(%player.gettransform(),0)-getword($prisonlocation.gettransform(),0))<3)
    if (mabs(getword(%player.gettransform(),1)-getword($prisonlocation.gettransform(),1))<3)
      if (mabs(getword(%player.gettransform(),2)-getword($prisonlocation.gettransform(),2))<3) {
        schedule(2000,0,prisoner, %player);
        %player.unmountimage(0);
        }
      else
        %player.client.DMShield=0;
    else
      %player.client.DMShield=0;
  else
    %player.client.DMShield=0;
  }
//End

function serverCmdMakeSwitch(%client, %mode, %doorset, %direction, %delay, %times, %name, %type) {
messageClient(%client, 'Msg', "\c2Yeah, uh, you need to update to \c3TOB 2.3\c2 because switches are a million times more complex now.  You won't regret it, trust me."); //I was going to make it backwards compatible but then I said no way screw those 2.2 guys -Wiggy
}

function serverCmdMakeSwitchv2(%client, %mode, %numActions, %act, %repeat, %reverse, %inverse, %teamOnly) {
if(%client.edit) {
  if(%mode ? !isObject(%client.lastSwitch) : (isObject(%client.lastSwitch) ? %client.lastSwitch.getDatablock() == nameToID(portculySwitch) && !%client.lastSwitch.isRunning : 0)) {
    if(%mode) {
      %client.lastSwitch = new Item() {
        static = "true";
        rotate = "false";
        position = vectorAdd(%client.getControlObject().position, vectorAdd("0 0 1", vectorScale(%client.getControlObject().getForwardVector(), "2 2 0")));
        rotation = "1 0 0 0";
        scale = "1 1 1";
        dataBlock = portculyswitch;
        owner = getRawIP(%client);
      };
      MissionCleanup.add(%client.lastSwitch);
      handleGhostColor(%client, %client.lastSwitch);
      %client.lastSwitchColor = %client.brickColor $= "" ? 'green' : %client.brickColor;
      %client.lastSwitch.currAction = 1;
    }
    if(!%client.rankCheck(3)) {
      if(%client.rankCheck(2)) {
        if(%numActions > 20) {
          %numActions = 20;
          messageClient(%client, 'Msg', "\c9Sorry, but admins can only have up to \c220\c9 actions per switch.");
        }
      }
      else {
        if(%numActions > 8) {
          %numActions = 8;
          messageClient(%client, 'Msg', "\c9Sorry, but mods can only have up to \c28\c9 actions per switch.");
        }
      }
    }
    %client.lastSwitch.numActions = %numActions;
    %client.lastSwitch.use = (%act > 1);
    %client.lastSwitch.bump = (mCeil(%act / 2) != mFloor(%act / 2));
    %client.lastSwitch.repeat = %repeat;
    %client.lastSwitch.reverse = %reverse;
    %client.lastSwitch.inverse = %inverse;
    %client.lastSwitch.isReversed = (%client.lastSwitch.reverse ? %client.lastSwitch.isReversed : 0);
    %client.lastSwitch.isInverted = (%client.lastSwitch.inverse ? (%client.lastSwitch.reverse ? %client.lastSwitch.isReversed : %client.lastSwitch.isInverted) : 0);
    %client.lastSwitch.teamOnly = (%teamOnly $= "" ? "none" : %teamOnly);
  }
  else {
    if(%mode)
      messageClient(%client, 'Msg', "\c9If you want to create a new switch you will have to first press 0 on your number pad to deselect all objects");
    else {
      if(isObject(%client.lastSwitch) ? %client.lastSwitch.getDatablock() != nameToID(portculySwitch) : 1)
        messageClient(%client, 'Msg', "\c9Sorry, but you must have a switch selected to edit one.");
      else
        messageClient(%client, 'Msg', "\c9Sorry, but that switch must be stopped before you can edit it.");
    }
  }
}
}

function serverCmdSetSwitchAction(%client, %num, %type, %doorset, %direction, %delay, %times, %sleep) {
if(isObject(%client.lastSwitch)) {
  if(%client.lastSwitch.numActions >= %num && !%client.lastSwitch.isRunning) {
    if(%client.lastSwitch.getDatablock() == nameToID(portculySwitch)) {
      %client.lastSwitch.type[%num] = %type;
      %client.lastSwitch.doorset[%num] = %doorset;
      %client.lastSwitch.direction[%num] = %direction;
      %client.lastSwitch.delay[%num] = %delay;
      %client.lastSwitch.times[%num] = %times;
      %client.lastSwitch.sleep[%num] = %sleep;
    } //No error messages since this is sort of back-end anyways
  }
}
}

function pivot(%obj, %origin, %delay, %times, %hostswitch) {//all of the rotation formula
//By Pr0gr4mm3r
if(%times < 1)
	return;

%rotation = "0 0" SPC getword(%origin, 2);
if(isobject(%obj)) {
	%obj.rotsav = rotAddUp(%obj.rotsav, %rotation);
	%theta = mDegToRad(360 - getword(%rotation, 2));
	%pos = vectorSub(%obj.position, %origin); //Word 2 of %origin is a rotation value but this operation is negated eight lines down.
	%rx = getWord(%pos, 0);
	%ry = getWord(%pos, 1);
	%rz = getWord(%pos, 2);
	%newpos = %rx * mCos(%theta) - %ry * mSin(%theta);
	%newpos = %newpos SPC %rx * mSin(%theta) + %ry * mCos(%theta);
	%newpos = %newpos SPC %rz;

	%newpos = vectorAdd(%newpos, %origin);
	%obj.setTransform(%newpos SPC rotconv(%obj.rotsav));
}
if(%times--)
	schedule(%delay, %obj, pivot, %obj, %origin, %delay, %times, %maxTimes);
}

function serverCmdQuerySwitch(%client) {
if(isObject(%obj = %client.lastSwitch)) {
  if(%obj.getDatablock() == nameToID(portculySwitch)) {
    commandToClient(%client, 'queryNewSwitch', %obj.bump + (2 * %obj.use), %obj.repeat, %obj.reverse, %obj.inverse, %obj.teamOnly); 
    for(%i = 1; %i <= %obj.numActions; %i++)
      commandToClient(%client, 'querySwitch', %i, %obj.type[%i], %obj.doorset[%i], %obj.direction[%i], %obj.delay[%i], %obj.times[%i], %obj.sleep[%i]);
  }
}
}



//For merging multiple 2.2 switches into one switch
function serverCmdPickSwitch(%client) {
if(isObject(%client.lastSwitch)) {
  if(%client.lastSwitch.getDatablock() == nameToID(portculySwitch)) {
    %client.mergeSwitch = %client.lastSwitch;
    messageClient(%client, '', "\c2This switch has been selected for merging; select another switch and use commandToServer('mergeSwitch'); to copy this switch to the end of that one.");
  }
}
}

function serverCmdMergeSwitch(%client) {
if(isObject(%client.lastSwitch) && isObject(%client.mergeSwitch)) {
  if(%client.lastSwitch.getDatablock() == nameToID(portculySwitch)) {
    for(%i = 1; %i <= %client.mergeSwitch.numActions; %i++)
      serverCmdSetSwitchAction(%client, %client.lastSwitch.numActions++, %client.mergeSwitch.type[%i], %client.mergeSwitch.doorset[%i], %client.mergeSwitch.direction[%i], %client.mergeSwitch.delay[%i], %client.mergeSwitch.times[%i], %client.mergeSwitch.sleep[%i]);
    messageClient(%client, '', "\c2Switches have been merged.");
    serverCmdQuerySwitch(%client);
    %client.lastSwitch.currAction = 1 + ((%client.lastSwitch.numActions - 1) * %client.lastSwitch.isReversed);
    %client.mergeSwitch.delete();
  }
}
else if(!isObject(%client.mergeSwitch))
  messageClient(%client, '', "\c2Use commandToServer('pickSwitch'); to select a switch to merge into this one first.");
}