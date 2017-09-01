//-----------------------------------------------------------------------------
// TBM Functions Designed and coded by Kier Simmons and anonymous programmers
// who are known as Gobbles and ShadowZ.  Redistribution of these functions and
// algorithms without this comment is a violation of intellectual property laws.
//
// Copyright (C) GameDesignMentoring.com, LLP
//-----------------------------------------------------------------------------

datablock ProjectileData(iGobDeployProjectile)
{
	//projectileShapeName = "~/data/shapes/arrow.dts";
	explosion           = brickDeployExplosion;
	particleEmitter     = brickTrailEmitter;
        muzzleVelocity      = 100;

	armingDelay         = 0;
	lifetime            = 1000;
	fadeDelay           = 70;
	bounceElasticity    = 0;
	bounceFriction      = 0;
	isBallistic         = false;
	gravityMod = 0.0;

	hasLight    = false;
	lightRadius = 3.0;
	lightColor  = "0 0 0.5";
};

datablock StaticShapeData(iGobSphere)
{
	category = "iGobs";  // Mission editor category
	className = "iGob";
	shapeFile = "~/data/shapes/bricksphere.dts";
	boxX = -1;
	maxDamage = 800;
	destroyedLevel = 800;
	disabledLevel = 600;
	expDmgRadius = 8.0;
	expDamage = 0.35;
	expImpulse = 500.0;
	dynamicType = $TypeMasks::StationObjectType;
	isShielded = true;
	energyPerDamagePoint = 110;
	maxEnergy = 50;
	rechargeRate = 0.20;
	renderWhenDestroyed = false;
	doesRepair = true;
	deployedObject = true;
};
datablock StaticShapeData(iGobCentroid)
{
	category = "iGobs";  // Mission editor category
	className = "iGob";
	shapeFile = "~/data/shapes/iGobCentroid.dts";
	boxX = -1;
	maxDamage = 800;
	destroyedLevel = 800;
	disabledLevel = 600;
	expDmgRadius = 8.0;
	expDamage = 0.35;
	expImpulse = 500.0;
	dynamicType = $TypeMasks::StationObjectType;
	isShielded = true;
	energyPerDamagePoint = 110;
	maxEnergy = 50;
	rechargeRate = 0.20;
	renderWhenDestroyed = false;
	doesRepair = true;
	deployedObject = true;
};
function iGobDeployProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
        if (%obj.client.edit) {
          %obj.client.edit=0;
          if (isobject(%obj.client.lastswitch))
            %obj.client.lastswitch.setskinname(%obj.client.lastswitchcolor);
          //messageClient(%obj.client, 'Msg', "\c2You are in \c3IGOB\c2 mode.");
          }
	%player = %obj.client.player;
        %client = %obj.client;
        if(%client.igob.placing == 1)
        return;
	if(!%player)
		return;
	%image = %player.getMountedImage(0);	//the thing the guy is holding

    if(%obj.client.iGobtype $= "Cube")
    %ghost = iGobCentroid;
    else
	%ghost = iGobSphere;
	if (%col.DataBlock !$= "")
		%colData = %col.getDataBlock();
	else
	   	return;
	%className = %colData.classname;

	if((%className $= "baseplate") || (%className $= "brick") ){
		%baseplate = %col;
		%baseplateTrans = %baseplate.getTransform();
		%target = %pos;

		%targetX = getword(%target, 0);
		%targetY = getword(%target, 1);
		%targetZ = getword(%target, 2);

		%baseplateX = getword(%baseplateTrans, 0);
		%baseplateY = getword(%baseplateTrans, 1);
		%baseplateZ = getword(%baseplateTrans, 2);

		%xDiff = %targetX - %baseplateX;
		%yDiff = %targetY - %baseplateY;
		%zDiff = %targetZ - %baseplateZ;

		%deployX = %baseplateX + (mFloor(%xDiff * 2 ) / 2);
		%deployY = %baseplateY + (mFloor(%yDiff * 2 ) / 2);
		%deployZ = %baseplateZ + (%colData.z * 0.2 * getword(%col.getscale(),2));
		}
	else {
		%deployX = %targetX;
		%deployy = %targety;
		%deployz = %targetz;
		}

		%d = new StaticShape()
		{
			datablock = %ghost;
		};
       	%d.setskinname(%client.igobcolor);
	if(%client.iGobscale)
                %d.setscale(%client.iGobscale);
       	%d.startfade(500,0,0);
	MissionCleanup.add(%d);
	%d.setTransform(%deployX @ " " @ %deployY @ " " @ %deployZ);
	if (isobject(%client.iGob))
		%client.iGob.delete();
	%client.iGob = %d;
	%client.iGob.rotsave = "0 0 0";
    %client.iGob.Total=0;
}

datablock ShapeBaseImageData(iGobImage) {
   shapeFile = "~/data/shapes/iGob.dts";
   mountPoint = 0;
   offset = "0 0.2 0.1";
   rotation = "-1 0 0 270";
   correctMuzzleVector = true;
   className = "WeaponImage";
   ammo = " ";
   projectile = iGobDeployProjectile;
   projectileType = Projectile;

   directDamage        = 10;
   radiusDamage        = 10;
   damageRadius        = 0.5;
   muzzleVelocity      = 100;
   velInheritFactor    = 0.0;

   melee = false;
   armReady = true;
   stateName[0]                     = "Ready";
   stateTransitionOnTriggerDown[0]  = "Fire";
   stateAllowImageChange[0]         = true;
   stateName[1]			= "Fire";
   stateScript[1]                  = "onFire";
   stateFire[1]			= true;
   stateAllowImageChange[1]        = false;
   stateTimeoutValue[1]            = 0.5;
   stateTransitionOnTimeout[1]     = "Ready";
};

function iGobImage::onMount(%this,%obj,%slot) {
 	if(%this.armReady)
	{
		if(%obj.getMountedImage($LeftHandSlot))
		{
			if(%obj.getMountedImage($LeftHandSlot).armReady)
				%obj.playthread(1, armReadyBoth);
			else
				%obj.playthread(1, armReadyRight);
		}
		else
		{
			%obj.playthread(1, armReadyRight);
		}
	}
	%obj.setImageAmmo(%slot,true);
        messageClient(%this.client, 'MsgHilightInv', '', -1);
    if(%this.tempbrick)
	%this.tempbrick.delete();
        %this.currWeaponSlot = -1;
        if (%this.client.edit) {
          %this.client.edit=0;
          if (isobject(%this.client.lastswitch))
            %this.client.lastswitch.setskinname(%this.client.lastswitchcolor);
          //messageClient(%this.client, 'Msg', "\c2You are now in \c3IGOB\c2 mode.");
          }
}

function serverCmdbegintransfer (%client,%remove) {
	if (isobject(%client.iGob)) {
        %delay = %client.isSuperAdmin? "0":(%client.isAdmin? "200":"1000");
		%client.iGob.Total = 0;
        if(%client.iGobtype $= "Sphere") {
        gobscrammer(%client,$TypeMasks::StaticShapeObjectType);
        gobscrammer(%client,$TypeMasks::ItemObjectType);
        gobscrammer(%client,$TypeMasks::PlayerObjectType);
        }
        else
        InitBoxContainerSearch(%client);
		schedule(%delay, 0, Send_iGob_String,%client, 0, %client.iGob.position,%remove);
		}
	else
		commandtoclient(%client, 'SaveToClient', " ", 1, 1);
}

function Send_iGob_String (%client, %current, %origin, %remove) {
    //echo("sending string " @ %current);
    %delay = %client.isSuperAdmin? "5":(%client.isAdmin? "50":"100");
	if (isObject(%client) && isObject(%client.iGob)) {
		if (%current <= %client.iGob.Total) {
			if (isObject(%client.iGob.brick[%current]))
				commandtoclient(%client, 'SaveToClient', Encode_Object (%client.iGob.brick[%current], %origin), %current, %client.iGob.Total);
			else
				commandtoclient(%client, 'SaveToClient', " ", %current, %client.iGob.Total);
			if (%remove == 1) {
                		getbrickowner(getrawip(%client),-1);
				%client.iGob.brick[%current].startfade(100,0,1);
 				%client.iGob.brick[%current].schedule(105,delete);
                	}
			%current++;
		if(!%client.iGob.brick[%current].isFaded) {
	            	%client.iGob.brick[%current].startfade(1000,0,true);
        	    	%client.iGob.brick[%current].startfade(2000,1000,false);
		}
		schedule(%delay,0,Send_iGob_String,%client, %current, %origin, %remove);
		}
		else if (%client.iGob.Total < 1)
			commandtoclient(%client, 'SaveToClient', " ", 1, 1);
	else if (isObject(%client))
		commandtoclient(%client, 'SaveToClient', " ", 1, 1);
	}
}

function serverCmdMakeObject (%client, %string) {
    //need to add support for iGob on/off toggle here and/or mod/admin allowances
	if (isobject(%client.iGob))
	  	Decode_Object(%string, %client.iGob.savedpos, 1, %client.iGob.rotsav, %client, %client.iGob.position);
}

function serverCmdToggleiGob(%client) {
	if ($Pref::Server::AdminInventory) {
  		if (%client.player.getmountedimage(0)==nametoid(iGobImage)) {
  			messageClient(%client, 'Msg', "\c2You are now in \c3NORMAL\c2 mode.");
    			%client.player.unmountimage(0);
    			if (isobject(%client.iGob))
      				%client.iGob.delete();
    			}
  		else {
    			if (%client.isadmin || %client.isMod ||%client.isSuperAdmin) {
	    			%client.player.mountimage(iGobImage,0);
	    			messageClient(%client, 'MsgHilightInv', '', -1);
	    			%client.player.currWeaponSlot = -1;
  				messageClient(%client, 'Msg', "\c2You are now in \c3iGob\c2 mode.");
			 }
    else
    messageClient(%client, 'Msg', "\c2You must be at least a Mod to use the iGob.");
    		}
  	}
	else
  		%client.player.unmountimage(0);
}

function iGobImage::onUnmount(%this,%obj,%slot) {
	%obj.playthread(2, root);	//stop arm swinging
	%leftimage = %obj.getmountedimage($lefthandslot);
	if(%leftimage)	{
		if(%leftimage.armready)	{
			%obj.playthread(1, armreadyleft);
			return;
		}
	}
	%obj.playthread(1, root);
	if (%obj.client.iGob)
		%obj.client.iGob.delete();
}

function servercmdiGobcolor(%client,%color) {
	if(isObject(%client.iGob))
		%client.iGob.setskinname(%color);
	%client.igobcolor = %color;
}

function servercmdattachtoigob(%client) {
  if (isobject(%client.iGob)) {
    %client.iGob.Total = 0;
    if(%client.iGobtype $= "Sphere") {
        gobscrammer(%client,$TypeMasks::StaticShapeObjectType);
        gobscrammer(%client,$TypeMasks::ItemObjectType);
        gobscrammer(%client,$TypeMasks::PlayerObjectType);
    }
    else
        InitBoxContainerSearch(%client);
    messageClient(%client, 'Msg', "\c2"@%client.iGob.Total@" Objects have now been \c3attached\c2.");
  }
}

function servercmddetachigob(%client) {
  messageClient(%client, 'Msg', "\c2"@%client.iGob.Total@" Objects have now been \c3detached\c2.");
  if (isobject(%client.iGob)) {
    for(%i=1;%i<=%client.iGob.Total;%i++)
    %client.iGob.brick[%i] = "";
    %client.iGob.Total = 0;
  }
}
function gobscrammer(%client,%mask) {
  InitContainerRadiusSearch(%client.iGob.position, getWord(%client.iGob.getscale(),0), %mask);
  while ((%block = containerSearchNext()) != 0) {
    if (%block.dataBlock.classname $= "brick" ||
    %block.getClassName() $= "Item" ||
    %block.dataBlock.category $= "DM" ||
    %block.dataBlock.category $= "Crown" ||
    %block.getClassName() $= "AIplayer")
    if(%client.isSuperAdmin||(%client.isAdmin && (%block.iGobperm == 0))||%block.owner $= getrawip(%client))
    %client.iGob.brick[%client.iGob.Total++] = %block;
    else
    %badcount++;
  }
  if(%mask $= $TypeMasks::StaticShapeObjectType && %badcount > 0)
  MessageClient(%client,'msgofghey','\c2You were unable to select/save \c3%1\c2 object(s)',%badcount);
}

function servercmdrotateigob(%client, %rotfactor) {
%origin = %client.iGob.position;
for (%x = 0; %x <= %client.iGob.total; %x++) {
  if (isobject(%client.iGob.brick[%x])) {
    if (getwords(%rotfactor,0,1) $= "0 0") {
      %client.iGob.brick[%x].rotsav = rotaddup(%client.iGob.brick[%x].rotsav, %rotfactor);
      %theta = (360 - getword(%rotfactor,2))/90/2*$pi;
      %pos = vectoradd(%client.iGob.brick[%x].position ,vectorscale(%origin, -1));
      %rx = getWord(%pos,0);
      %ry = getWord(%pos,1);
      %rz = getWord(%pos,2);
      %newpos = %rx * mcos(%theta) - %ry * msin(%theta);
      %newpos = %newpos SPC %rx * msin(%theta) + %ry * mcos(%theta);
      %newpos = %newpos SPC %rz;
      if(%client.iGob.brick[%x].getDataBlock() == nametoID(portculyswitch)) {
        %block = %client.iGob.brick[%x];
        for(%i = 1; %i <= %block.numActions; %i++) {
          if(%block.type[%i] $= "teleport") {
            %theta = mDegToRad((360 - getword(%rotfactor,2)));
            %pos = vectorSub(%block.direction[%i], %origin);
            %rx = getWord(%pos, 0);
            %ry = getWord(%pos, 1);
            %rz = getWord(%pos, 2);
            %newdir = %rx * mcos(%theta) - %ry * msin(%theta);
            %newdir = %newdir SPC %rx * msin(%theta) + %ry * mcos(%theta) SPC getWord(%pos, 2);
            %newdir = vectorAdd(%newdir, %origin) SPC getWords(%block.direction[%i], 3, 6);
            %block.direction[%i] = %newdir;
          }
        }
      }
      %newpos = vectoradd(%newpos, %origin);
      %client.iGob.brick[%x].settransform(%newpos@" "@rotconv(%client.iGob.brick[%x].rotsav));
    }
  }
}
}

function servercmdigobdelete(%client) {
    servercmdigobhandleoptions(%client,"-1");
    messageClient(%client, 'Msg', '\c2%1 Objects have now been \c3deleted\c2.',%client.iGob.total);
    %client.iGob.total=0;
}

function servercmdigobhandleoptions(%client,%opt,%num)
{
  if (%client.iGob.total<=0)
    return;
    for (%x = 0; %x <= %client.iGob.total; %x++)
    {
        //check for bad objects, otherwise go on and set the...settings.
        if (!isobject(%client.iGob.brick[%x]))
            continue;
        %block = %client.iGob.brick[%x];

        switch$(%opt)
        {
            case "-1":
            getbrickowner(%block.owner,-1);
            if(%block.DataBlock.classname $= "brick")
      		%block.explode();
            else
            %block.delete();
            case "1":
            %block.setskinname(%client.brickcolor);
            case "2":
            %block.port = %num;
            case "3":
            %block.setCloaked(1);
            case "4":
            %block.setCloaked(0);
            case "5":
            %block.startfade(1000,0,true);
            %block.isfaded = 1;
            case "6":
            %block.startfade(1000,0,false);
            %block.isfaded = "";
            case "7":
            if(%block.owner $= getrawip(%client) || %client.isSuperAdmin)
            %block.iGobperm = 1;
            case "8":
            if(%block.owner $= getrawip(%client) || %client.isSuperAdmin)
            %block.iGobperm = "";
            case "9":
            %block.permbrick = 1;
            case "10":
            %block.permbrick = "";
            case "11":
            %block.setScale(%num);
            case "12":
            colorReplace(%client,%block,%num);
        }
    }
}
function servercmdgetbrickcolor(%client) {
MessageClient(%client,"","\c2Brick Color:\c3" SPC %client.lastswitchcolor);
}
function colorReplace(%client,%block,%searchcolor) {
if(%block.getskinname() !$= "")
%color = %block.getskinname();
else
%color = "base";
if(%searchcolor $= %color)
%block.setskinname(%client.brickcolor);
}

function servercmdgimmeigobcolors(%client) {
if(%client.iGob.total <= 0)
return;
for(%x=1;%x<=%client.iGob.total;%x++) {
if(%client.iGob.brick[%x].getskinname() $= "")
%color = "base";
else
%color = %client.iGob.brick[%x].getskinname();
%colors = strreplace(%colors,"\c5|\c2 "@%color@" \c5|\c2","");
%colors = %colors@"\c5|\c2 "@%color@" \c5|\c2";
}
MessageClient(%client,"","\c2Brick\'s colors are: " @ %colors);
}

function servercmdhotsaveiGob(%client,%hotnum) {
  if (isobject(%client.iGob)) {
for(%i=1;%i<=%client.iGob.total;%i++)
%client.hotiGob[%hotnum,%i] = %client.iGob.brick[%i];
%client.hotiGob[%hotnum,-1] = %client.iGob.getposition();
}
%client.hotiGob[%hotnum,-2] = %i;
}

function servercmdhotloadiGob(%client,%hotnum) {
  if (isobject(%client.iGob)) {
  for(%i=1;%i<%client.iGob.Total;%i++)
    %client.iGob.brick[%i] = "";
    %client.iGob.Total = 0;
    for(%i=0;%i < %client.hotiGob[%hotnum,-2];%i++) {
    %client.iGob.brick[%client.iGob.Total++] = %client.hotiGob[%hotnum,%client.iGob.Total];
    }
    %client.iGob.settransform(%client.hotiGob[%hotnum,-1]);
    }
}

function servercmdigobscale(%client,%scalefactor) {
  %origin = %client.iGob.position;
  for (%x = 1; %x <= %client.iGob.total; %x++) {
    %client.iGob.brick[%x].settransform(vectoradd(%origin,vectormult3(%scalefactor,vectorsub(%client.iGob.brick[%x].position,%origin))));
    %client.iGob.brick[%x].setscale(vectormult3(%scalefactor,%client.iGob.brick[%x].scale));
  }
}

function vectormult3(%arg1,%arg2) {
  %output = getword(%arg1,0) * getword(%arg2,0) SPC getword(%arg1,1) * getword(%arg2,1) SPC getword(%arg1,2) * getword(%arg2,2);
  return %output;
}