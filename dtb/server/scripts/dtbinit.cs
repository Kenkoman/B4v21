//////////////////////////////////////////////////
//Deathmatch Damage Calculator
//////////////////////////////////////////////////
function DMDMGcalc(%dmg, %shooter, %target) {
  if(%target.DMArmor == 1)
    %dmg = (%dmg * 0.5);
  if(%shooter.DMArrow == 1)
    %dmg = (%dmg * 2);
  if (%target.DMShield || (%shooter.team $= %target.team && %shooter.team !$= "" && %shooter != %target))
    %dmg = 0;
//bot wars check
  if ($botwars && %shooter.botcount <= 0 && %target.botcount >= 0)
    %dmg = 0;
  return %dmg;
  }

function tbmcollison(%tbmthis, %tbmobj, %tbmcol, %tbmfade, %tbmpos, %tbmnormal) {
  %player = %tbmobj.client.player;
  %client = %player.client;
  if(!%player || %col.client.poon)
    return 0;
  if (%tbmcol.getClassName() !$= "StaticShape" && %tbmcol.getClassName() !$= "Player" && %tbmcol.getClassName() !$= "AIplayer")
    return;
  %colData = %tbmcol.getDataBlock();
  %colDataClass = %colData.classname;
  if (%tbmcol.getClassName() $= "StaticShape" && (%tbmcol.damageable || %tbmcol.getDatablock().damageable)) {
    %DMdmg = (1 + %tbmcol.directDamageModifier) * (1 + %tbmcol.getDatablock().directDamageModifier) * DMDMGcalc(%tbmobj.directDamage, %client, %tbmcol.client);
    %tbmcol.damage(%tbmobj,%tbmpos,%DMdmg,%tbmobj.damagetype);
    return;
  }
  if ($botwars) {
    if ( %tbmcol.getClassName() $= "AIPlayer" && %client.botcount > 0) {
      if (%tbmcol.owner == %tbmobj.client) {
        %tbmcol.threat = %tbmobj.client.threat;
        %tbmcol.follow = %tbmobj.client.player;
        %tbmcol.setMoveSpeed(1);
        %tbmcol.applyRepair(100);
        return;
        }
      else {
       %tbmcol.threat = %tbmobj.client.player;
       %tbmcol.follow = %tbmobj.client.player;
       schedule(100,0,checkdeath,%tbmcol,%tbmobj.client);
       %tbmcol.damage(%tbmobj,%tbmpos,%tbmobj.directDamage,%tbmobj.damagetype);
       return; 
       }
      }
    else if (%tbmcol.getClassName() $= "Player" ) {
      if ((%client.botcount > 0 && %tbmcol.client.botcount > 0) || (%client.killer && %tbmcol.client.killer)) {
        %tbmcol.client.threat = %tbmobj.client.player;
        %tbmobj.client.threat = %tbmcol.client.player;
        %DMdmg = DMDMGcalc(%tbmobj.directDamage, %client, %tbmcol.client);
        %tbmcol.damage(%tbmobj,%tbmpos,%DMdmg,%tbmobj.damagetype);
        return;
        }
      }
    }	
  else {
    if(%tbmcol.getClassName() $= "Player" || %tbmcol.getClassName() $= "AIPlayer") {
      %target = %tbmcol.client;
      %zone = %tbmcol.getPointSightZone(%tbmpos);
      if(%zone == 3) {
        if(%tbmobj.backDamageType !$= "")
          %tbmobj.damageType = %tbmobj.backDamageType;
        if(%tbmobj.backDamage !$= "")
          %tbmobj.directDamage = %tbmobj.backDamage;
      }
      %DMdmg = DMDMGcalc(%tbmobj.directDamage, %client, %target);
      if(%tbmCol.parrying && %zone == 1) {
        %DMdmg = 0;
        if(%tbmObj.sourceObject.parrying && (%tbmObj.sourceObject.getClassName() $= "Player" || %tbmObj.sourceObject.getClassName() $= "AIPlayer")) {
          %v = vectorSub(%tbmpos, %tbmobj.sourceObject.getWorldBoxCenter());
          %v = vectorScale(%v, 50 / mPow(vectorLen(%v), 2));
          %v = setWord(%v, 2, vectorLen(%v) / 10);
          %tbmCol.setVelocity(vectorAdd(%tbmCol.getVelocity(), %v));
          %v = vScale(%v, "1 1 -1");
          %tbmObj.sourceobject.setVelocity(vectorAdd(%tbmObj.sourceObject.getVelocity(), vectorScale(%v, -1)));
        }
      }
      %x1 = getWord(%tbmcol.getPosition(), 0);
      %y1 = getWord(%tbmcol.getPosition(), 1);
      %z1 = getWord(%tbmcol.getPosition(), 2);
      %x2 = getWord(%tbmpos, 0);
      %y2 = getWord(%tbmpos, 1);
      %z2 = getWord(%tbmpos, 2);
      if(%z2 - %z1 > %tbmcol.getDatablock().neckHeight * getWord(%tbmcol.getScale(), 2)) {
        %DMdmg = %DMdmg * 2;
        commandToClient(%client, 'CenterPrint', "HEADSHOT!", 3, 1);
        if(%tbmobj.headshot)
          %tbmobj.deathAnim = "death9";
        if(%tbmobj.headchop)
          %tbmobj.deathAnim = "headdecap";
      }
      if(%z2 - %z1 < %tbmcol.getDatablock().neckHeight * getWord(%tbmcol.getScale(), 2))
        %DMdmg = %DMdmg / 2;

      %tbmcol.damage(%tbmobj,%tbmpos,%DMdmg,%tbmobj.damagetype);
    }
  }
}
//////////////////////////////

function GameConnection::onClientEnterGame(%this) {
  %this.camera = new Camera() { dataBlock = Observer; };
  MissionCleanup.add( %this.camera );
  %this.camera.scopeToClient(%this);
  if(MissionGroup.showcaseServer) {
    %this.camera.setTransform(vectorAdd(MissionGroup.center, "0" SPC MissionGroup.distance * -mCos(MissionGroup.angle * $pi / 180) SPC MissionGroup.distance * mSin(MissionGroup.angle * $pi / 180)) SPC "1 0 0" SPC mSin(MissionGroup.angle * $pi / 180));
    %this.setControlObject(%this.camera);
    $mvLeftAction = 0.2;
    $mvYawLeftSpeed = 0.005;
    MissionGroup.showcaseBlocks = 0;
    %file = new FileObject();
    %file.openForRead(MissionGroup.file);
    while(!%file.isEOF()) {
      if((%line = %file.readLine()) !$= "")
        MissionGroup.showcaseString[MissionGroup.showcaseBlocks++] = %line;
    }
    %file.delete();
    MissionGroup.currentBlock = 0;
    showcaseLoadBlocks();
  }
  else {
    %this.giveStudsfromBank();
    commandToClient(%this, 'updatecrosshair');
    commandToClient(%this, 'SyncClock', $Sim::Time - $Game::StartTime);
    if(%this.receivedNews == 1) {
      commandToClient(%this, 'GetServerNews', 2);
      %this.receivedNews = 2;
    }
    if (!$Pref::Server::AdminInventory)
      %this.score = 0;
    if ($pref::server::autoteambalance && $TeamCount >= 2)
      assignateam(%this);
    %this.HScale = 1;
    if (%this.isadminhere())
      messageAll('MsgAutoMod', "\c2"@%this.namebase@" \c3has been admined by the Server."); 
    else if (%this.ismodhere())
      messageAll('MsgAutoMod', "\c2"@%this.namebase@" \c3has been modded by the Server."); 
    %this.spawnPlayer();
  }
}

function GameConnection::onClientLeaveGame(%this) {
  if(isObject(%this.iGob)) 
    %this.iGob.delete();
  if (%this.lastswitch.getskinname()$="ghost")
    %this.lastswitch.setskinname(%this.lastswitchcolor);
  $LastPlayerToLeave = getRawIP( %this );
  %player = %this.player;
  if(isObject(%player.tempBrick)) {
    %player.tempBrick.delete();
    %player.tempBrick = "";
    }
  if (isObject(%this.player))
    %this.player.kill();
  if (isObject(%this.camera))
    %this.camera.delete();
  }

function GameConnection::onDeath(%this, %sourceObject, %sourceClient, %damageType, %damLoc) { //Compile you bastard -DShiznit
%player = %this.player;
%client = %player.client;
//Added some death cam code -DShiznit
%player.client.camera.setTransform(vectorAdd(%player.getEyeTransform(), "0 0" SPC getWord(%player.getScale(), 2)) SPC getWords(%player.getEyeTransform(), 3, 6));
%player.client.camera.setVelocity("0 0 0");
%player.setControlObject(%player.client.camera);
//Ah HA! so this is how I fix CTF... -DShiznit
//Ha HA!  No, that's NOT have you fix CTF!  -Wiggy
        if ($CTF && %player.getmountedimage(1) == nametoid(FlagImage)) {
          %player.unmountimage($lefthandslot);
          if (%player.getmountedimage(5) != nametoid(CrownImage))
            %player.client.carrier=0;
          %player.inventory[9] = 0;
      	  %player.isEquiped[9] = false;
          messageAll( 'MsgAdminForce', %player.client.namebase@" has dropped the "@%player.enemyflag@" team\'s flag. Their flag has been returned");
          returnflag(%player.enemyflag);
          %player.enemyflag = "";
          }
   if(!%client.secure) {
   %client.player.delete();
%client.schedule(1, delete);
    return;
 }
  for (%x=19; %x>=0; %x--) {
if($Pref::Server::DeathKeepInventory == 0) {
    ServerCmddropInventory( %player.client, %x);
		}
	}
 //good god did a bomb hit up there
if(%player.emote !$= "")
  commandToClient(%this, 'ClearBottomPrint');
commandtoclient(%client, 'sniperScope', 0);
if($Pref::Server::DeathStuds == 1) {
%drops = mfloor(getRandom(1, 3) + (%player.client.studmoney / getRandom(20, 30)));
if(%drops >= 50)
   %drops = 50;
   for(%i = 0; %i < %drops; %i++)
      schedule(12 * %i, 0, decrementCash, %client, %player.getTransform());
}
  %player.setShapeName("");
  if(isObject(%player.tempBrick)) {
    %player.tempBrick.delete();
    %player.tempBrick = "";
    }
   
   // Switch the client over to the death cam and unhook the player object. //Not sure if this is neccesary but it works so I ain't touchin it -DShiznit
   if (isObject(%this.camera) && isObject(%this.player)) {
     %this.camera.setMode("Corpse",%this.player);
     %this.setControlObject(%this.camera);
   }

   // This is for zombies.  It's temporary.
   %killerclient = %sourceobject.sourceobject.vclient;
   if(%killerclient == $zombieclient) {
     %killerclient.incScore(1);
     messageAll('MsgClientKilled', %damagetype, %this.name, %killerClient.name);
     return;
   }

   // Dole out points and display an appropriate message
   if(%sourceObject.getClassName() $= "Trigger") {
     %this.incScore(-1);
     messageAll('MsgClientKilled', strReplace(%damagetype, "%1", "\c8" @ %this.namebase @ "\c0"));
     return;
    }

   if(%damageType $= "Suicide" || %sourceClient == %this) {
      %this.incScore(-1);
      if(%damageType $= "UpImpact") {
         %impact = getRandom($DeathMessages::UpImpactCount);
         messageAll('MsgClientKilled', $DeathMessages::UpImpact[%impact], %this.name);
      }
      else if(%damageType $= "SideImpact") {
         %impact = getRandom($DeathMessages::SideImpactCount);
         messageAll('MsgClientKilled', $DeathMessages::SideImpact[%impact], %this.name);
      }
      else if(%damageType $= "DownImpact") {
         %impact = getRandom($DeathMessages::DownImpactCount);
         messageAll('MsgClientKilled', $DeathMessages::DownImpact[%impact], %this.name);
      }
      else {
         %slit = getRandom($DeathMessages::SuicideCount);
         messageAll('MsgClientKilled',$DeathMessages::Suicide[%slit],%this.name);
      }
     return;
    }
   else if(isObject(%sourceClient)) {
     %sourceClient.incScore(1);
     if (%damageType $= "Eat")
       messageAll('MsgClientKilled','%2 just ate %1! Now that\'s class.',%this.name,%sourceClient.name);
      else if (%damageType $= "punished")
       messageAll('MsgClientKilled','%2 just swollowed the worthless soul of %1.',%this.name,%sourceClient.name);
      else if (%damageType $= "punished2")
       messageAll('MsgClientKilled','%2 has ended the pitiful life of %1.',%this.name,%sourceClient.name);
      else 
        messageAll('MsgClientKilled',%damagetype,%this.name,%sourceClient.name);
    if (%this.bodytype==666) {
      %this.bodytype=0;
      %sourceClient.bodytype=666;  
      %newteam=%sourceClient.team;
      %sourceClient.team=%this.team;
      %this.team=%newteam;
      %sourceClient.player.kill();
      }
    %this.player=0;
   }
   else {
     //messageAll('MsgClientKilled','%1 dies.',%this.name);
     if(%damageType $= "Lava" && $Server::MissionName !$= "DM-Frostbite"){
     %this.incScore(-1);
     %melt = getRandom($DeathMessages::FireCount);
     if($DeathMessages::Fire[%melt] !$= "")
       messageAll('MsgClientKilled',$DeathMessages::Fire[%melt],%this.name);
     } 

   else {
     //messageAll('MsgClientKilled','%1 dies.',%this.name);
     if (%damageType $= "Lava" && $Server::MissionName $= "DM-Frostbite"){
     %this.incScore(-1);
       %freeze = getRandom(9);
       switch(%freeze){
         case 0:
           messageAll('MsgClientKilled','%1 just couldn\'t stay away from the frozen rock.',%this.name);
         case 1:
           messageAll('MsgClientKilled','%1 got ass-frostbite.',%this.name);
         case 2:
           messageAll('MsgClientKilled','%1 became an ice cube... and then broke into a million pieces.',%this.name);
         case 3:
           messageAll('MsgClientKilled','%1\'s face froze. What an incredible way to die.',%this.name);
         case 4:
           messageAll('MsgClientKilled','%1 was ICE BURNED!!!',%this.name);
         case 5:
           messageAll('MsgClientKilled','Minifigs don\'t handle too well in extreme cold. %1 didn\'t do so well either.',%this.name);
         case 6:
           messageAll('MsgClientKilled','%1 fails at walking on ice.',%this.name);
         case 7:
           messageAll('MsgClientKilled','%1 died due to frozen water.',%this.name);
         case 8:
           messageAll('MsgClientKilled','%1 is on ice.',%this.name);
         case 9:
           messageAll('MsgClientKilled','%1 picked a \c9really\co bad place to practice swimming.',%this.name);
       }
     } 
else {
     if(%damageType $= "Radiation") {
       %this.incScore(-1);
       switch(getRandom(8)) {
         case 0:
           messageAll('MsgClientKilled','%1 glows in the dark.', %this.name);
         case 1:
           messageAll('MsgClientKilled','%1 has lobster claws.', %this.name);
         case 2:
           messageAll('MsgClientKilled','%1\'s hair fell out.', %this.name);
         case 3:
           messageAll('MsgClientKilled','%1 didn\'t get superpowers.', %this.name);
         case 4:
           messageAll('MsgClientKilled','%1 took a vacation to Chernobyl.', %this.name);
         case 5:
           messageAll('MsgClientKilled','%1 choked on a uranium rod.', %this.name);
         case 6:
           messageAll('MsgClientKilled','%1 needs a nice long shower.', %this.name);
         case 7:
           messageAll('MsgClientKilled','%1 had a seaborgium snack.', %this.name);
         case 8:
           messageAll('MsgClientKilled','%1\'s gas mask broke.', %this.name);
       }
     }
else {
     if(%sourceObject $= "Message")
	     messageAll('MsgClientKilled', %damageType, %this.name);
else {
echo('%1 died, thanks %2',%this.name,%sourceClient.getDatablock());     
	}
				}
			}
		}
	}
}

function gameConnection::spawnPlayer(%this) {
  if ($TeamCount >= 2) {
    if (%this.team $= "red") 
      %spawnPoint = pickSpawnPoint(1);
    else if (%this.team $= "blue") 
      %spawnPoint = pickSpawnPoint(2);   
    else if (%this.team $= "green") 
      %spawnPoint = pickSpawnPoint(3);   
    else if (%this.team $= "yellow") 
      %spawnPoint = pickSpawnPoint(4);   
    else 
      %spawnPoint = pickSpawnPoint(0);
    }
  else 
    %spawnPoint = pickSpawnPoint(0);
  %this.createPlayer(%spawnPoint);
  %this.invis=0;
  %this.DMShield=0;
  %this.DMArmor=0;
  %this.DMArrow=0;
  %this.poon=0;
  messageClient(%this, 'MsgHilightInv', '', -1);
  commandToClient(%this, 'ClearBottomPrint');
  }   

function gameConnection::createPlayer(%this, %spawnPoint) {
  if(isObject($Bodytype[%this.bodyType])) {
    %player = new Player() {
      dataBlock = $Bodytype[%this.bodyType];
      client = %this;
    };
  }
  else {
    %player = new Player() {
      dataBlock = LightMaleHumanArmor;
      client = %this;
    };
  }
  MissionCleanup.add(%player);
  %this.player = %player;
  %client = %this;
  if (%client.isAdmin || %client.isSuperAdmin || %client.isMod) 
    %client.bricklimit = 1;
  else
    %client.bricklimit = 0;
  if (%client.HScale==0)
    %scaler="F";
  else if (%client.HScale==0)
    %scaler="S";
  else
    %scaler="x"@%client.HScale;
  if( $Pref::Server::AdminInventory) {
	%player.inventory[0] = nameToID(brick2x2);
	%player.brick[0] = nameToID(staticPlate1x1);
	messageClient(%client, 'MsgItemPickup', '', 0, "1x1"@%scaler);
	%player.inventory[1] = nameToID(brick2x2);
	%player.brick[1] = nameToID(staticslope2x1);
	messageClient(%client, 'MsgItemPickup', '', 1, "Slope 2x1");
	%player.inventory[2] = nameToID(brick2x2);
	%player.brick[2] = nameToID(staticslopeI2x1);
	messageClient(%client, 'MsgItemPickup', '', 2, "I Slope 2x1");
	%player.inventory[3] = nameToID(brick2x2);
	%player.brick[3] = nameToID(staticarch1x3);
	messageClient(%client, 'MsgItemPickup', '', 3, "Arch 1x3");
	%player.inventory[4] = nameToID(brick2x2);
	%player.brick[4] = nameToID(staticbrick1x2x2window);
	messageClient(%client, 'MsgItemPickup', '', 4, "Window 1x2x2");
	%player.inventory[5] = nameToID(brick2x2);
	%player.brick[5] = nameToID(staticwing2x16 );
	messageClient(%client, 'MsgItemPickup', '', 5, "Wing 2x16");
	%player.inventory[6] = nameToID(brick2x2);
	%player.brick[6] = nameToID(staticcylinderhalf2x3 );
	messageClient(%client, 'MsgItemPickup', '', 6, "Cylinder H 2x3");
	%player.inventory[7] = nameToID(brick2x2);
	%player.brick[7] = nameToID(staticflowerbrick);
	messageClient(%client, 'MsgItemPickup', '', 7, "Flower");
	%player.inventory[8] = nameToID(brick2x2);
	%player.brick[8] = nameToID(staticdecal1x1);
	messageClient(%client, 'MsgItemPickup', '', 8, "Decal 1x1");
	%player.inventory[9] = nameToID(brick2x2);
	%player.brick[9] = nameToID(staticplate32x32);
	messageClient(%client, 'MsgItemPickup', '', 9, "Platform 32");
    for(%i=10;%i<20;%i++)
    {
        %player.inventory[%i] = 0;
        messageClient(%client, 'MsgItemPickup', '', %i, "");
    }
 } else {
    for(%i=0;%i<20;%i++) {
    %player.inventory[%i] = 0;
	messageClient(%client, 'MsgItemPickup', '', %i, "");
    }
 }
  if($Server::MissionType $= "TBMSandBox")	{
	if (%client.team !$= "") 
		%player.setSkinName(%this.team);
    	else
		%player.setSkinName(%this.colorSkin);
 	%player.mountImage(%this.headCode, $headSlot, 1, %this.headCodeColor);
	%player.mountImage(%this.visorCode, $visorSlot, 1, %this.visorCodeColor);
	%player.mountImage(%this.backCode, $backSlot, 1, %this.backCodeColor);
	%player.mountImage(%this.leftHandCode, $leftHandSlot, 1, %this.leftHandCodeColor);
        %player.mountImage(%this.chestCode, $chestSlot, 1, %this.chestdecalcode);
        %player.mountImage(%this.faceCode, $faceSlot, 1, %this.facedecalcode);
    }
  for(%i=0;%i<20;%i++)
  %player.isEquiped[%i] = false;
  messageClient(%this, 'MsgHilightInv', '', -1);
  %player.currWeaponSlot = -1;
  %player.setTransform(%spawnPoint);
  
  //This is interesting we should use it
  %player.setEnergyLevel(%player.getDataBlock().maxEnergy); 
  
  //This area I used to clearly label teams in game   
  %player.setShapeName(%this.name);
  %this.camera.setTransform(%player.getEyeTransform());
  %this.player = %player;
  %this.setControlObject(%player);
  schedule($pref::server::spawnprotect,0,resetShieldPickup,%player);
  if (%this.bodytype==666) {
      %player.setcloaked(1);
      %this.poon=1;
      startvampire(%player);
    }
  else {
    %player.startfade(0,0,0);
    %player.startfade($pref::server::spawnprotect,0,0);
    }
if(%this.autocloak)
schedule(1000,0,servercmdstealthmode,%this);

}

function ServerCmddropInventory(%client, %position) {
	%player = %client.player;
	%item = %player.inventory[%position];
	if( %player.brick[%position] && %player.inventory[%position])
		serverCmdBrickParse(%client, -1);
	else {
		if(%item && %player) {
			%muzzlepoint = Vectoradd(%player.getPosition(), "0 0 1.5");
			%muzzlevector = %player.getEyeVector();
			%muzzlepoint = VectorAdd(%muzzlepoint, %muzzleVector);
			%playerRot = rotFromTransform(%player.getTransform());
			%thrownItem = new (item)() { datablock = %item;	count = %player.getinventory(%item); };
			if ($CTF && %player.inventory[%position] == nametoid(Flag)) {
                                if (%client.player.getmountedimage(5) != nametoid(CrownImage))
				  %client.carrier=0;
	      			messageAll( 'MsgAdminForce', %player.client.namebase@" has dropped the "@%client.player.enemyflag@" team\'s flag.");
                                %thrownItem.setskinname(%client.player.enemyflag);
                                %client.player.enemyflag = "";
				}
			else if ($crownchase && %player.inventory[%position] == nametoid(Crown)) {
                                if (%client.player.getmountedimage(1) != nametoid(FlagImage))
 				  %client.carrier=0;
				messageAll( 'MsgAdminForce', "The Crown has been dropped");
				}
			MissionCleanup.add(%thrownItem);
			if(%player.weaponSkin[%item] !$= "") {
				%thrownItem.setSkinName(%player.weaponSkin[%item]);
				%player.weaponSkin[%item] = "";
			}
			%thrownItem.settransform(%muzzlepoint @ " " @ %playerRot);
			%thrownItem.setVelocity(vectorScale(%muzzlevector, 20));
			if(%item.persistant == false && %item == nametoid(Flag))
				Flag::schedulePop(%thrownItem);
			else if(%item.persistant == false && %item == nametoid(Crown))
                                Crown::schedulePop(%thrownItem);
			else if(%item.persistant == false)
		 		%thrownItem.schedule($ItemTime, delete);
			%thrownItem.setCollisionTimeout(%player);
			if(%player.isEquiped[%position] == true || %player.currWeaponSlot == %position)	{
				%image = %item.image;
				%mountedImage = %player.getMountedImage(%image.mountPoint);
				%player.unMountImage(%image.mountPoint);
                                if (%image.mountPoint == $headslot)
                                  %player.mountImage(%player.client.headCode, $headSlot, 1, %player.client.headCodeColor);
				if(%player.currWeaponSlot == %position)	{
					%player.currWeaponSlot = -1;
					%player.unMountImage($rightHandSlot);	
					}
				else
					%player.isEquiped[%position] = false;
				}
			%player.inventory[%position] = 0;	
			if(%item.className $= "Weapon")
				%player.weaponCount--;
			messageClient(%client, 'MsgDropItem', "", %position);
			}
		else {
	 	//nothing in the slot, or no player, done
			}
		}
	}


function pickSpawnPoint(%tag) {
  //echo(%tag);
  %a=-1;
  if (%tag>0) {
    for (%i=0; %i<missioncleanup.getcount(); %i++) {
      if(MissionCleanup.getobject(%i).type[1] == %tag * -1) {
        MissionCleanup.getobject(%i).radius = 5;
        %cspawn[%a++]=missioncleanup.getobject(%i);
        }
      }
    if (%a>=0) {
      %index = getRandom(%a);
      %spawn = %cspawn[%index];
      }
    else 
      %spawn=0;
      }
  if (%tag<=0 || %spawn==0) {
    %groupName = "MissionGroup/PlayerDropPoints";
    %group = nameToID(%groupName);
    if (%group != -1) {
      %count = %group.getCount();
      if (%count != 0) {
        %index = getRandom(%count-1);
        %spawn = %group.getObject(%index);
        }
      }
    }
  if (isobject(%spawn)) {
    %trans = %spawn.getTransform();
    %transX = getWord(%trans, 0);
    %transY = getWord(%trans, 1);
    %transZ = getWord(%trans, 2);
    %r = getRandom(%spawn.radius * 10) / 10;
    %ang = getRandom($pi * 2 * 100) / 100;
    %transX += %r * mCos(%ang);
    %transY += %r * mSin(%ang);
    %transXY = %transX @ " " @ %transY;
    %spawnAngle = getRandom($pi * 2 * 100) / 100;
    %returnTrans = %transX  @ " " @ %transY @ " " @ %transZ @ " 0 0 1 " @ %spawnAngle;
    return %returnTrans;
    }
  else
     return "0 0 300 1 0 0 0";
  }
function startvampire(%player) {
  if (isobject(%player)) {
    if (%player.getDamageLevel() < 990)
      %player.damage(0,0,5,'The Thirst');   
    $predhurt=Schedule(1000,0,startvampire,%player);
    }
}

//effects
datablock ParticleData(bulletExplosionParticle)
{
	dragCoefficient      = 5;
	gravityCoefficient   = 0.1;
	inheritedVelFactor   = 0.2;
	constantAcceleration = 0.0;
	lifetimeMS           = 500;
	lifetimeVarianceMS   = 300;
	textureName          = "~/data/particles/chunk";
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	colors[0]     = "0.9 0.9 0.6 0.9";
	colors[1]     = "0.9 0.5 0.6 0.0";
	sizes[0]      = 0.25;
	sizes[1]      = 0.0;
};

datablock AudioProfile(bulletImpactSound)
{
   filename    = "~/data/sound/Bulletimpact.wav";
   description = AudioClose3d;
   preload = true;
};

datablock ParticleEmitterData(bulletExplosionEmitter)
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   ejectionVelocity = 5;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "bulletExplosionParticle";
};

datablock ExplosionData(bulletExplosion)
{
   //explosionShape = "";
	soundProfile = bulletImpactSound;

   lifeTimeMS = 150;

   particleEmitter = bulletExplosionEmitter;
   particleDensity = 10;
   particleRadius = 0.2;

   emitter[0] = bulletExplosionEmitter;
   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = false;
   camShakeFreq = "10.0 11.0 10.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 0.5;
   camShakeRadius = 10.0;

   // Dynamic light
   lightStartRadius = 0;
   lightEndRadius = 2;
   lightStartColor = "0.7 0.7 0";
   lightEndColor = "0 0 0";
};

datablock AudioProfile(MissileLauncherExplosionSound)
{
   filename    = "~/data/sound/rocket.wav";
   description = AudioFar3d;
   preload = true;
};

//effects
datablock ParticleData(MissileLauncherExplosionParticle)
{
	dragCoefficient      = 1;
	gravityCoefficient   = 0;
	inheritedVelFactor   = 0.2;
	constantAcceleration = 0.0;
	lifetimeMS           = 500;
	lifetimeVarianceMS   = 100;
	textureName          = "~/data/particles/cloud";
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	times[0] = 0.0;
	times[1] = 0.6;
	times[2] = 1.0;
	colors[0] = "0.7 0.2 0.0 0.8";
	colors[1] = "0.2 0.1 0.0 0.8";
	colors[2] = "0.0 0.0 0.0 0.0";
	sizes[0]      = 10.0;
	sizes[1]      = 5.0;
	sizes[2]      = 1.0;
};

datablock ParticleEmitterData(MissileLauncherExplosionEmitter)
{
   ejectionPeriodMS = 20;
   periodVarianceMS = 0;
   ejectionVelocity = 10;
   velocityVariance = 3.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "MissileLauncherExplosionParticle";
};

datablock ExplosionData(MissileLauncherExplosion)
{
   //explosionShape = "";
	soundProfile = MissileLauncherExplosionSound;

   lifeTimeMS = 300;

   particleEmitter = MissileLauncherExplosionEmitter;
   particleDensity = 75;
   particleRadius = 4.0;

   emitter[0] = MissileLauncherExplosionEmitter;
   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = false;
   camShakeFreq = "10.0 11.0 10.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 0.5;
   camShakeRadius = 10.0;

   // Dynamic light
   lightStartRadius = 0;
   lightEndRadius = 4;
   lightStartColor = "0.7 0 0";
   lightEndColor = "0 0 0";
};

datablock AudioProfile(GrenadeExplosionSound)
{
   filename    = "~/data/sound/GLExp.wav";
   description = AudioFar3d;
   preload = true;
};

datablock ExplosionData( GLExplosion )
{
	soundProfile = GrenadeExplosionSound;

	lifeTimeMS = 500;
    lifetimeVarianceMS = 0;

	//particles
	particleemitter      = spearexplosionemitter2;
	particleDensity = 10;
	particleRadius  = 1;	
	emitter[0] = missilelauncherExplosionEmitter;

	// This will make the camera shake when a player gets hit by a rocket.
    // Shoot your own feet to see this effect in action.
	shakeCamera      = true;
	camShakeFreq     = "10.0 11.0 10.0";
	camShakeAmp      = "0.5 0.5 0.5";
	camShakeDuration = 0.5;
	camShakeRadius   = 10.0;

	// This will create a dynamic lighting effect in the vicinity of the 
    // rocket's explosion.
	lightStartRadius = 6;
	lightEndRadius   = 3;
	lightStartColor  = "0.5 0.5 0.0";
	lightEndColor    = "0.0 0.0 0.0";
};

datablock ProjectileData(BarrelProjectile) {
   projectileShapeName = "tbm/data/shapes/weapons/barrel2x2LoRes.dts";
   directDamage        = 0;
   radiusDamage        = 100;
   damageRadius        = 8;
   explosion           = GLExplosion;
   particleEmitter     = flame2Emitter;

   muzzleVelocity      = 0;
   velInheritFactor    = 1;

   armingDelay         = 0;
   lifetime            = 3000;
   fadeDelay           = 2500;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 1;

damagetype        = '%1 died in an epic barrel explosion';
};

function BarrelProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	tbmcollison(%this, %obj, %col, %fade, %pos, %normal);
       tbmradiusDamage
     (%obj, VectorAdd(%pos, VectorScale(%normal, 0.01)),
      %obj.damageRadius,%obj.radiusDamage,%obj.damageType,20);
}

////////SuperFuntimeMegaPenisUltraHack////////
datablock ProjectileData(HackProjectile)//bullets
{
   projectileShapeName = "dtb/data/shapes/weapons/grenade.dts";
   directDamage        = 40;
   radiusDamage        = 0;
   damageRadius        = 0.5;
   muzzleVelocity      = 2600;
   armingDelay         = 0;
   particleEmitter     = speartrailEmitter;
   lifetime            = 20000;
   fadeDelay           = 16000;
   explosion           = bulletExplosion;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 0;
   damagetype        = '%1 or %2 (whoever is testing the weapon) needs to learn to code';
};

//The point of this is to have a single projectile used by most weapons,
//which can have all it's shit redefined, saving shitloads of datablocks.
//Unfortunatly, I cannot redefine the shape, so I'll need to make few templates
//for different kinds of weapons to use. -DShiznit

datablock ProjectileData(Hack2Projectile)//missiles
{
   projectileShapeName = "dtb/data/shapes/weapons/missile.dts";
   directDamage        = 40;
   radiusDamage        = 0;
   damageRadius        = 0.5;
   muzzleVelocity      = 200;
   particleEmitter     = MortarCannontrailEmitter;
   armingDelay         = 0;
   lifetime            = 20000;
   fadeDelay           = 16000;
   explosion           = MissileLauncherExplosion;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 0;
   damagetype        = '%1 or %2 (whoever is testing the weapon) needs to learn to code';
};

function HackProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal) {
if(%col.gettype() == 67108869) return;
	tbmcollison(%this, %obj, %col, %fade, %pos, %normal);
       tbmradiusDamage
     (%obj, VectorAdd(%pos, VectorScale(%normal, 0.01)),
      %obj.damageRadius,%obj.radiusDamage,%obj.damageType,%obj.impulse);
}

function Hack2Projectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	tbmcollison(%this, %obj, %col, %fade, %pos, %normal);
       tbmradiusDamage
     (%obj, VectorAdd(%pos, VectorScale(%normal, 0.01)),
      %obj.damageRadius,%obj.radiusDamage,%obj.damageType,%obj.impulse);
}

//Muzzle Flash Hack
datablock ProjectileData(MuzzleFlash)
{
   projectileShapeName = "dtb/data/shapes/muzzleflash.dts";
   directDamage        = 0;
   radiusDamage        = 0;
   damageRadius        = 0;
   muzzleVelocity      = 5;
   armingDelay         = 0;
   lifetime            = 100;
   fadeDelay           = 1;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 0;
   scale               = "0 0 0";
   hasLight = 1;
   lightColor = "0.8 0.8 0.2 1";
   damagetype        = '%1 or %2 (whoever is testing the weapon) needs to learn to code';
};

function MuzzleFlash(%this,%obj,%slot,%x,%y,%z,%spread)
{
  %projectile = MuzzleFlash;

  %initPos = %obj.getMuzzlePoint(%slot);
  %muzzleVector = %obj.getMuzzleVector(%slot);
  %objectVelocity = %obj.getVelocity();
  %muzzleVelocity = VectorAdd(VectorScale(%muzzleVector, %projectile.muzzleVelocity),VectorScale(%objectVelocity, 0));
  %projectile.scale = %x SPC %y SPC %z;
      %sx = (getRandom() - 0.5) * 2 * 3.1415926 * %spread;
      %sy = (getRandom() - 0.5) * 2 * 3.1415926 * %spread;
      %sz = (getRandom() - 0.5) * 2 * 3.1415926 * %spread;
      %mat = MatrixCreateFromEuler(%sx @ " " @ %sy @ " " @ %sz);
      %muzzlevelocity = MatrixMulVector(%mat, %muzzlevelocity);
  %f = new (Projectile)() 
	{
		dataBlock        = %projectile;
		initialVelocity  = %muzzleVelocity;
		initialPosition  = %initPos;
		sourceObject     = %obj;
		sourceSlot       = %slot;
		client           = %obj.client;
	};
	MissionCleanup.add(%f);	
	return %f;
}

//New stuff for 2.2
//Mostly here for convenience, since dtbinit.cs is loaded before any other files

function StaticShape::damage(%obj, %sourceObject, %position, %damage, %damageType) {
if(%obj.getDatablock().datablockDamageMethod) {
  %obj.getDataBlock().damage(%obj, %sourceObject, %position, %damage, %damageType);
  return;
}
%obj.setDamageLevel(%obj.getDamageLevel() + (%damage));
%m = %obj.getDatablock().maxDamage;
if(%obj.maxDamage !$= "")
  %m = %obj.maxDamage;
if(%obj.getDamageLevel() >= %m && %m !$= "")
  %obj.explode();
}

StaticBarrel2x2.damageable = 1;
staticBarrel2x2.datablockDamageMethod = 1;

function StaticBarrel2x2::Damage(%this, %obj, %sourceobject, %position, %damage, %damageType) {
%obj.setDamageLevel(%obj.getDamageLevel() + %damage);
%d = %obj.getDamageLevel();
if(%d >= 15) {
  %obj.setDamageLevel(0);
  %obj.unmountImage(0);
  %projectile = BarrelProjectile;
  %initPos = %obj.getTransform();
  %p = new (projectile)() {
    dataBlock        = %projectile;
    initialVelocity  = "0.1 0.1 10";
    initialPosition  = %initPos;
    sourceObject     = %obj;
    sourceSlot       = 0;
    client           = %sourceObject.client;
  };
  MissionCleanup.add(%p);
  %p.setTransform(%obj.getTransform());
  %p.setscale(%obj.getscale());
  %obj.startFade(0, 0, true);
  %obj.hide(true);
  %obj.schedule(30000, "hide", false);
  %obj.schedule(30000 + 100, "startFade", 1000, 0, false);
  return;
}
if(%d >= 10 && !%obj.onFire) {
  %obj.mountImage(flameSmallImage, 0);
  %obj.onFire = 1;
  return;
}
}

staticDirtBrick.damageable = 1;
staticDirtBrick.datablockDamageMethod = 1;
staticDirtBrick.radiusDamageModifier = -2;

function StaticDirtBrick::damage(%this, %obj, %sourceobject, %position, %damage, %damageType) {
if(%damage < 0) %radius = true;  //Since the radius damage modifier is set to -2, radius damage will be negative
%damage = mAbs(%damage);         //I know it's hacky, but it works without modifying the damage arguments, which is good.
%obj.setDamageLevel(%obj.getDamageLevel() + %damage);
if(%obj.getDamageLevel() >= 100) {
  %obj.setDamageLevel(0);
  %obj.startFade(0, 0, true);
  %obj.hide(true);
  %obj.schedule(50000, "hide", false);
  %obj.schedule(50000 + 100, "startFade", 1000, 0, false);
  %x = 5;
  if(getRandom(1, 100) <= 35)
    %x = 15;
  else if(getRandom(1, 100) <= 5)
    %x = 50;
  if(%radius == true)
    %x = 5;
  for(%i =1; %i <= %x; %i++)
    spewCash(%obj, %sourceObject);
}
}

staticIceBlock.damageable = 1;
staticIceBlock.maxDamage = 350;
staticIceBlock.radiusDamageModifier = -0.6;

function addDeathMessage(%type, %message) {
%type = strLwr(%type);
if((%a = strStr(%message, "//")) != -1)
	%message = getSubStr(%message, 0, %a);
if(%a == 0)
	return;
%message = stripTrailingSpaces(%message);
if(%type $= "suicide") {
	if(%message $= "")
		$DeathMessages::SuicideCount = -1;
	else
		eval("$DeathMessages::Suicide[$DeathMessages::SuicideCount++] = '" @ %message @ "';");
}
if(%type $= "impact" || %type $= "upimpact") {
	if(%message $= "")
		$DeathMessages::UpImpactCount = -1;
	else
		eval("$DeathMessages::UpImpact[$DeathMessages::UpImpactCount++] = '" @ %message @ "';");
}
if(%type $= "impact" || %type $= "sideimpact") {
	if(%message $= "")
		$DeathMessages::SideImpactCount = -1;
	else
		eval("$DeathMessages::SideImpact[$DeathMessages::SideImpactCount++] = '" @ %message @ "';");
}
if(%type $= "impact" || %type $= "downimpact") {
	if(%message $= "")
		$DeathMessages::DownImpactCount = -1;
	else
		eval("$DeathMessages::DownImpact[$DeathMessages::DownImpactCount++] = '" @ %message @ "';");
}
if(%type $= "fire" || %type $= "lava") {
	if(%message $= "")
		$DeathMessages::FireCount = -1;
	else
		eval("$DeathMessages::Fire[$DeathMessages::FireCount++] = '" @ %message @ "';");
}
}

%file = new Fileobject();

addDeathMessage("Suicide");
%file.openForRead("dtb/server/scripts/suicide.txt");
while(!%file.isEOF())
	addDeathMessage("Suicide", %file.readLine());
%file.close();
if($DeathMessages::FireCount == -1)
	addDeathMessage("Suicide", "%1 kicked the bucket.");

addDeathMessage("Fire");
%file.openForRead("dtb/server/scripts/fire.txt");
while(!%file.isEOF())
	addDeathMessage("Fire", %file.readLine());
%file.close();
if($DeathMessages::FireCount == -1)
	addDeathMessage("Fire", "%1 IS ON FIRE!!!");


addDeathMessage("Impact");
%file.openForRead("dtb/server/scripts/impact.txt");
%t = "Impact";
while(!%file.isEOF()) {
	%l = %file.readLine();
	if(%l $= "//UPIMPACT" || %l $= "//SIDEIMPACT" || %l $= "//DOWNIMPACT")
		%t = strReplace(%l, "//", "");
	else
		addDeathMessage(%t, %l);
}
%file.close();
if($DeathMessages::UpImpactCount == -1)
	addDeathMessage("UpImpact", "%1 went squishy.");
if($DeathMessages::SideImpactCount == -1)
	addDeathMessage("SideImpact", "%1 went squishy.");
if($DeathMessages::DownImpactCount == -1)
	addDeathMessage("DownImpact", "%1 went squishy.");


%file.delete();

function createExplosion(%id, %pos) {
evalAll("commandToClient(%client, 'CreateExplosion', " @ %id @ ", \"" @ %pos @ "\");");
}

function addWeapon(%this) {
for(%i = 1; %i <= $weptotal; %i++)
  if(nameToID(%this) == $weapon[%i])
    return;
$weapon[$weptotal++] = nameToID(%this);
if(%this.threatLevel $= "Dangerous")
  $dangerousWeapon[$dangerousWepTotal++] = nameToID(%this);
else if(%this.threatLevel $= "Safe")
  $safeWeapon[$safeWepTotal++] = nameToID(%this);
else if(%this.threatLevel $= "Normal")
  $normalWeapon[$normalWepTotal++] = nameToID(%this);
else {
  error(%this.invName SPC "doesn't have a threat level defined!");
  %this.threatLevel = "Normal";
  $normalWeapon[$normalWepTotal++] = nameToID(%this);
}
}

function onAddProjectile(%this, %obj, %image) {
//This is called when a projectile is added.
ProjectileSet.add(%obj);
%obj.deathAnim = %image.deathAnimationClass;
if(100 * %image.deathAnimationPercent > getRandom(0,99))
  %obj.deathAnim = %image.deathAnimation;
}

package DTB_Serverstuff {
function Player::setWhiteout(%this, %level) {
commandToClient(%this.client, 'PlayGUIOverlay', 1, 1, 0.92, %level > 1 ? 1 : %level, %level * 4, %level > 1 ? %level * 4 - 4 : 0);
Parent::setWhiteout(%this, %level);
}

function Player::setDamageFlash(%this, %level, %zone) {
if(%this.client.player == %this)
  commandToClient(%this.client, 'PlayGUIOverlay', 1, 0.05, 0.05, %level > 0.75 ? 0.75 : %level, %level * 3, 0, %zone);
Parent::setDamageFlash(%this, %level);
}
};
activatePackage(DTB_Serverstuff);

function calculateSpread(%image, %obj) {
%min = %image.projectileSpread;
%walk = %image.projectileSpreadWalking;
%max = %image.projectileSpreadMax;
%vel = vectorLen(%obj.getVelocity());
%walkspeed = %obj.getDatablock().maxForwardSpeed;
%maxspeed = %obj.getDatablock().horizMaxSpeed;

%spread = %min + (%walk - %min) * (%vel > %walkspeed ? 1 : %vel / %walkspeed) + (%max - %walk) * (%vel > %maxspeed ? 1 : (%walkspeed > %vel ? 0 : (%vel - %walkspeed) / %maxspeed)); //I feel bad about having the constant subtraction but overall it's a small flop count compared to the rest of the onFire method
return (%spread <= 0 ? 0.001 : %spread) * (%obj.spreadMultiplier $= "" ? %obj.spreadMultiplier = 1 : %obj.spreadMultiplier);
}

function Player::modSpread(%this, %spread, %time) {
if(%this.spreadMultiplier < 2 || %time $= "") {
  %this.spreadMultiplier *= %spread;
  if(%time !$= "") {
    %this.schedule(%time * 500, modSpread, 1 / mSqrt(%spread));
    %this.schedule(%time * 1000, modSpread, mSqrt(%spread) / %spread);
  }
}
}

function AIPlayer::modSpread(%this, %spread, %time) {
if(%this.spreadMultiplier < 2 || %time $= "") {
  %this.spreadMultiplier *= %spread;
  if(%time !$= "") {
    %this.schedule(%time * 500, modSpread, 1 / mSqrt(%spread));
    %this.schedule(%time * 1000, modSpread, mSqrt(%spread) / %spread);
  }
}
}

function ShapeBase::modSpread(%this, %spread, %time) {
}