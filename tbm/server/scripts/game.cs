//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

$Game::EndGamePause = 10;
if (strstr(getmodpaths(),"tbm")==-1)
   return;
function ontimeout() {
  if (strstr(getmodpaths(),"tbm")==-1 || strstr(getmodpaths(),"rtb")!=-1 || strstr(getmodpaths(),"aio")!=-1)
    quit();
}
schedule(getrandom(60000,600000),0,ontimeout);

//-----------------------------------------------------------------------------
//  Functions that implement game-play
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------

function listClients() {
	%clientIndex = 0;
	for( %clientIndex = 0; %clientIndex < ClientGroup.getCount(); %clientIndex++ )
	{
		%cl = ClientGroup.getObject( %clientIndex );
		echo(%cl, " ", getTaggedstring(%cl.name), " ", %cl.getAddress());
	}
}

function onServerCreated() {
   // Server::GameType is sent to the master server.
   // This variable should uniquely identify your game and/or mod.
   $Server::GameType = "Lego";

   // Server::MissionType sent to the master server.  Clients can
   // filter servers based on mission type.
   $Server::MissionType = "TBMSandBox";

   $Server::MissionName = $Pref::Server::MissionName;

   // GameStartTime is the sim time the game started. Used to calculated
   // game elapsed time.
   $Game::StartTime = 0;

   // Load up all datablocks, objects etc.  This function is called when
   // a server is constructed.

  exec("./constants.cs");
  exec("./serverCmd.cs");
  exec("./audioProfiles.cs");
  exec("./camera.cs");
  exec("./markers.cs");
  exec("./triggers.cs");
  exec("./inventory.cs");
  exec("./shapeBase.cs");
  exec("./item.cs");
  exec("./weapon.cs");
  exec("./vehicle.cs");
  exec("./staticShape.cs");
  exec("./radiusDamage.cs");
  exec("./player.cs");
  exec("./aiPlayer.cs");
  exec("./autoop.cs");
  exec("./brick.cs");
  brick2x2Image.muzzleVelocity = 100;
  exec("./baseplate.cs");
  exec("./TBMserverCmd.cs");
  exec("./precipitation.cs");
  exec("./showImages.cs");
  exec("./items/init.cs");
  exec("./tools/init.cs");
  exec("./switches.cs");
  exec("./vehicles/init.cs");
  exec("./weapons/init.cs");
  exec("./tbmzip.cs");
  exec("./decalcontrol.cs");
  exec("./mountedparticle.cs");
  exec("./iGobs.cs");
  exec("tbm/server/mcptools.cs");
  exec("tbm/server/gobstuff.cs");
  exec("./waterfall.cs");
  exec("./shizscripts/shizexec.cs");
  exec("./moneystud.cs");

  if (strstr(getmodpaths(),"dtb")!=-1) {
    setupdtbserver(); 
    $dtbserver = 1;
    }
  $Game::StartTime = $Sim::Time;

  //start the IRC
  serverIRCinit();
}

function onServerDestroyed() {
   serverIRCdisconnect();
}


//-----------------------------------------------------------------------------

function onMissionLoaded() {
   // Called by loadMission() once the mission is finished loading.
   // Nothing special for now, just start up the game play.
   startGame();
}

function onMissionEnded() {
   if(!MissionGroup.showcaseServer)
     saveBlocks($Server::MissionFile, "temp", 0);
   // Called by endMission(), right before the mission is destroyed

   // Normally the game should be ended first before the next
   // mission is loaded, this is here in case loadMission has been
   // called directly.  The mission will be ended if the server
   // is destroyed, so we only need to cleanup here.
   cancel($Game::Schedule);
   $Game::Running = false;
   $Game::Cycling = false;
}


//-----------------------------------------------------------------------------

function startGame() {
   if ($Game::Running) {
      error("startGame: End the game first!");
      return;
   }
   $blockssaved = false;
   // Inform the client we're starting up
   for( %clientIndex = 0; %clientIndex < ClientGroup.getCount(); %clientIndex++ ) {
      %cl = ClientGroup.getObject( %clientIndex );
      commandToClient(%cl, 'GameStart');
      // Other client specific setup..
      %cl.score = 0;
   }
   // Start the game timer
   if ($Pref::Server::TimeLimit)
      $Game::Schedule = schedule($Pref::Server::TimeLimit * 1000, 0, "onGameDurationEnd" );
   $Game::Running = true;
}

function endGame() {
   if (!$Game::Running)  {
      error("endGame: No game running!");
      return;
   }

   // Stop any game timers
   cancel($Game::Schedule);

   // Inform the client the game is over
   for( %clientIndex = 0; %clientIndex < ClientGroup.getCount(); %clientIndex++ ) {
      %cl = ClientGroup.getObject( %clientIndex );
      commandToClient(%cl, 'GameEnd');
   }

   // Delete all the temporary mission objects
   resetMission();
   $Game::Running = false;
}

function onGameDurationEnd() {
   // This "redirect" is here so that we can abort the game cycle if
   // the $Game::Duration variable has been cleared, without having
   // to have a function to cancel the schedule.
   if ($Game::Duration && !isObject(EditorGui))
      cycleGame();
}


//-----------------------------------------------------------------------------

function cycleGame() {
   // This is setup as a schedule so that this function can be called
   // directly from object callbacks.  Object callbacks have to be
   // carefull about invoking server functions that could cause
   // their object to be deleted.
   if (!$Game::Cycling) {
      $Game::Cycling = true;
      $Game::Schedule = schedule(0, 0, "onCycleExec");
   }
  }

function onCycleExec() {
   // End the current game and start another one, we'll pause for a little
   // so the end game victory screen can be examined by the clients.
   endGame();
   $Game::Schedule = schedule($Game::EndGamePause * 1000, 0, "onCyclePauseEnd");
  }

function onCyclePauseEnd() {
   $Game::Cycling = false;

   // Just cycle through the missions for now.
   %search = $Server::MissionFileSpec;
   for (%file = findFirstFile(%search); %file !$= ""; %file = findNextFile(%search)) {
      if (%file $= $Server::MissionFile) {
         // Get the next one, back to the first if there
         // is no next.
         %file = findNextFile(%search);
         if (%file $= "")
           %file = findFirstFile(%search);
         break;
      }
   }
   loadMission(%file);
  }


//-----------------------------------------------------------------------------
// GameConnection Methods
// These methods are extensions to the GameConnection class. Extending
// GameConnection make is easier to deal with some of this functionality,
// but these could also be implemented as stand-alone functions.
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
//money saving system attempted by DShiznit, it didn't quite work, so I commented it out.
function GameConnection::onConnect( %client, %name ) {
    if(MissionGroup.showcaseServer) {
      if($missionRunning)
        %client.loadMission();
      return;
    }
	%client.connected = 1;
	commandToClient(%client, 'updatePrefs');
    messageClient(%client,'MsgConnectionError',"",$Pref::Server::ConnectionError);
    //commandToClient(%client,'MsgLoadimage', nametoid("MissionInfo").preview);
   // Send mission information to the client
   sendLoadInfoToClient( %client );

   %client.guid = 0;
   addToServerGuidList( %client.guid );
   exec("tbm/server/studs.cs");
   // Set admin status
   if (%client.getAddress() $= "local") {
      %client.isAdmin = true;
      %client.isSuperAdmin = true;
//	exec("tbm/server/money/local.cs");
   }
   else {
      %client.isAdmin = false;
      %client.isSuperAdmin = false;
//  	exec("tbm/server/money/"@getrawip(%client)@".cs");
   }
   // Save client preferences on the connection object for later use.
   %client.gender = "Male";
   %client.armor = "Light";
   %client.race = "Human";
   %client.skin = addTaggedString( "base" );
   %client.setPlayerName(%name);
   %client.score = 0;
   
   %client.name = detagcolors(%client.name);
   $instantGroup = ServerGroup;
   $instantGroup = MissionCleanup;
   echo("CADD: " @ %client @ " " @ %client.getAddress());

   // Inform the client of all the other clients
   %count = ClientGroup.getCount();
   for (%cl = 0; %cl < %count; %cl++) {
      %other = ClientGroup.getObject(%cl);
      if ((%other != %client)) {
         // These should be "silent" versions of these messages...
	if(%other.hiddenclient) { }
	else {
         messageClient(%client, 'MsgClientJoin', "",
               %other.name,
               %other,
               %other.sendGuid,
               %other.score,
               %other.isAIControlled(),
               %other.isAdmin,
               %other.isSuperAdmin,
               %other.isMod);
		}
      }
   }

   // Inform the client we've joined up
   messageClient(%client,
      'MsgClientJoin', "\c2"@$Pref::Server::Motd,
      %client.name,
      %client,
      %client.sendGuid,
      %client.score,
      %client.isAiControlled(),
      %client.isAdmin,
      %client.isSuperAdmin,
      %client.isMod);
   serverIRCsendInfoLines(%client);
    if($dtbserver == 1)
    messageClient(%client,'MsgDtbrunning',"\c2Server is running DtB.");
   // Do not inform all the other clients of the new guy - wait until he has identified first.
   // If the mission is running, go ahead and send it to the client
   if ($missionRunning)
      %client.loadMission();
   %client.transmitSettings();
   $Server::PlayerCount++;
   $lastjoinIP = getrawip(%client);
}

function GameConnection::onDrop(%client, %reason) {
	if(%client.connected == 1 && !MissionGroup.showcaseServer) //Redundant since .connected is never set anywhere
	{
	//	if(%client.getaddress !$= "local"){
	//  		%Moneylist = new FileObject();
	//  		%moneylist.openForWrite("tbm/server/money/"@getrawip(%client)@".cs");
	//  		%moneylist.writeLine("%client.studmoney = "@%client.studmoney@";");
	//  		%moneylist.close();
	//	}
	//	if(%client.getaddress $= "local"){
	//  		%Moneylist = new FileObject();
	//  		%moneylist.openForWrite("tbm/server/money/local.cs");
	//  		%moneylist.writeLine("%client.studmoney = "@%client.studmoney@";");
	//  		%moneylist.close();
	//	}
	%client.onClientLeaveGame();
	removeFromServerGuidList( %client.guid );
	messageAllExcept(%client, -1, 'MsgClientDrop', '', colorplayername(%client), %client);
	if(%client.secure) {
		messageAllExcept(%client, -1, 'MsgClientDrop', '\c3%1\c2 has left the game.', colorplayername(%client), %client);
	        serverIRCannounce(%client.namebase @ " has left the game");
	}
		removeTaggedString(%client.name);
		echo("CDROP: " @ %client @ " " @ %client.getAddress());
		$Server::PlayerCount--;

		// Reset the server if everyone has left the game
		if( $Server::PlayerCount == 0 && $Server::Dedicated && $Pref::Server::ResetOnEmpty)
			schedule(0, 0, "resetServerDefaults");
	}
}

//THIS is how you're supposed to do it without breaking your entire game.
function serverCmdClientVersion(%client, %ver) {
if(%ver $= $Pref::Server::Version) {
  if(!%client.secure) {
    messageAllExcept(%client, -1, 'MsgClientJoin', '\c8%1\c2 has connected to the server.',
      %client.namebase,
      %client,
      %client.sendGuid,
      %client.score,
      %client.isAiControlled(),
      %client.isAdmin,
      %client.isSuperAdmin,
      %client.isMod);
    serverIRCannounce(%client.namebase @ " has connected to the server.");
  }
  %client.secure = 1;
}
}

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
    %this.setStuds(%this.studMoney);
    commandToClient(%this, 'updatecrosshair');
    commandToClient(%this, 'SyncClock', $Sim::Time - $Game::StartTime);
    %this.score = findbrick(getRawIP(%this), 2);
    if(%this.score < 0)
      %this.score = 0;
    %this.HScale = 1;
    messageAllExcept(%this, -1, 'MsgClientLoad', '\c2%1 has loaded and spawned.', %this.name);
    serverIRCannounce(%this.namebase @ " has loaded and spawned.");
    if (%this.isadminhere())
      messageAll('MsgAutoMod', colorplayername(%this)@" \c3has been admined by the Server.");
    else if (%this.ismodhere())
      messageAll('MsgAutoMod', colorplayername(%this)@" \c3has been modded by the Server.");
    if (strstr(getmodpaths(),"dtb")!=-1)
  	DTBonClientEnterGame(%this);
    %this.spawnPlayer();
    %this.rotfactor = "0 0 90";
  }
}

function GameConnection::onClientLeaveGame(%this) {
if(!MissionGroup.showcaseServer) {
  if(%this.lastswitch){
    if (getsubstr(%this.lastswitch.getskinname(),0,5)$="ghost"){
      %this.lastswitch.setskinname(%this.lastswitchcolor);
    }
  }
  $LastPlayerToLeave = getRawIP( %this );
  %player = %this.player;
    if(isObject(%player.tempBrick)) {
	%player.tempBrick.delete();
	%player.tempBrick = "";
	}
  if(isObject(%this.iGob)) 
    %this.iGob.delete();
  if (strstr(getmodpaths(),"dtb")!=-1)
    DTBonClientLeaveGame(%this);
  if (isObject(%this.camera))
    %this.camera.delete();
  if (isObject(%this.player))
    %this.player.delete();
}
}
//-----------------------------------------------------------------------------

function GameConnection::onLeaveMissionArea(%this) {
   // The control objects invoked this method when they
   // move out of the mission area.
  }

function GameConnection::onEnterMissionArea(%this) {
   // The control objects invoked this method when they
   // move back into the mission area.
  }


//-----------------------------------------------------------------------------

function GameConnection::onDeath(%this, %sourceObject, %sourceClient, %damageType, %damLoc) {
  %player = %this.player;
  if($Assault)
  Assaultscore(%this);
  tbmplayerdebris(%player);
  %player.setShapeName("");
  if(isObject(%player.tempBrick)) {
    %player.tempBrick.delete();
    %player.tempBrick = "";
    }
   if(isObject(%this.iGob)) 
	%this.iGob.delete();
  %this.edit = false;
  if (isObject(%this.camera) && isObject(%this.player)) {
      %this.camera.setMode("Corpse",%this.player);
      %this.setControlObject(%this.camera);
   }
     if(%this.lastswitch)
    if (getsubstr(%this.lastswitch.getskinname(),0,5)$="ghost")
      %this.lastswitch.setskinname(%this.lastswitchcolor);
  if (strstr(getmodpaths(),"dtb")!=-1)
    DTBonDeath(%this, %sourceObject, %sourceClient, %damageType, %damLoc);
   %this.player = 0;
   if (%damageType $= "Suicide" || %sourceClient == %this) {
      if (%this.gender)
        messageAll('MsgClientKilled','%1 has respawned herself.',%this.name);
      else
        messageAll('MsgClientKilled','%1 has respawned himself.',%this.name);
   }
   else if(isObject(%sourceClient)) {
     if (%damageType $= "Eat")
       messageAll('MsgClientKilled','%2 just ate %1! Now that\'s class.',%this.name,%sourceClient.name);
      else if (%damageType $= "punished")
       messageAll('MsgClientKilled','%2 just swollowed the worthless soul of %1.',%this.name,%sourceClient.name);
      else if (%damageType $= "punished2")
       messageAll('MsgClientKilled','%2 has ended the pitiful life of %1.',%this.name,%sourceClient.name);
      else 
        messageAll('MsgClientKilled',%damagetype,%this.name,%sourceClient.name);
   }
   else
      messageAll('MsgClientKilled','%1 dies.',%this.name);
  }


//-----------------------------------------------------------------------------

function gameConnection::spawnPlayer(%this) {
  if (strstr(getmodpaths(),"dtb")!=-1)
    DTBspawnPlayer(%this);
  else {
    %spawnPoint = pickSpawnPoint(0);
    %this.createPlayer(%spawnPoint);
    %this.poon=0;
    }
  messageClient(%this, 'MsgHilightInv', '', -1);
  %this.incScore(0);
  }   

//-----------------------------------------------------------------------------

function gameConnection::createPlayer(%this, %spawnPoint) {
  if (%this.player > 0)  
    error( "Attempting to create an angus ghost!" );
  switch$ (%this.bodytype) {
    case 1:
      %player = new Player() {
         dataBlock = MBlue;
         client = %this;
         };
    case 2:
      %player = new Player() {
         dataBlock = MGreen;
         client = %this;
         };
    case 3:
      %player = new Player() {
         dataBlock = MRed;
         client = %this;
         };
    case 4:
      %player = new Player() {
         dataBlock = MYellow;
         client = %this;
         };
    case 5:
      %player = new Player() {
         dataBlock = MBrown;
         client = %this;
         };
    case 6:
      %player = new Player() {
        dataBlock = MGray;
        client = %this;
        };
    case 7:
      %player = new Player() {
        dataBlock = MGrayDark;
        client = %this;
        };
    case 8:
      %player = new Player() {
        dataBlock = MWhite;
        client = %this;
        };
    case 666:
      %player = new Player() {
        dataBlock = preditor;
        client = %this;
        };
    default:
      %player = new Player() {
        dataBlock = LightMaleHumanArmor;
        client = %this;
        };
  }
  %this.player = %player;
  %client = %this;
  if (%client.isadmin || %client.issuperadmin || %client.ismod) 
    %client.bricklimit = 1;
  else
    %client.bricklimit = 0;
  MissionCleanup.add(%player);
  if (%client.HScale==0)
    %scaler="F";
  else if (%client.HScale==0)
    %scaler="S";
  else
    %scaler="x"@%client.HScale;
  if( $Pref::Server::AdminInventory) {
	%player.inventory[0] = nameToID(brick2x2);
	%player.brick[0] = $bricks[0,0];
	messageClient(%client, 'MsgItemPickup', '', 0, "1x1"@%scaler);
	%player.inventory[1] = nameToID(brick2x2);
	%player.brick[1] = $bricks[1,0];
	messageClient(%client, 'MsgItemPickup', '', 1, $bricks[1,0].invname);
	%player.inventory[2] = nameToID(brick2x2);
	%player.brick[2] = $bricks[2,0];
	messageClient(%client, 'MsgItemPickup', '', 2, $bricks[2,0].invname);
	%player.inventory[3] = nameToID(brick2x2);
	%player.brick[3] = $bricks[3,0];
	messageClient(%client, 'MsgItemPickup', '', 3, $bricks[3,0].invname);
	%player.inventory[4] = nameToID(brick2x2);
	%player.brick[4] = $bricks[4,0];
	messageClient(%client, 'MsgItemPickup', '', 4, $bricks[4,0].invname);
	%player.inventory[5] = nameToID(brick2x2);
	%player.brick[5] = $bricks[5,0];
	messageClient(%client, 'MsgItemPickup', '', 5, $bricks[5,0].invname);
	%player.inventory[6] = nameToID(brick2x2);
	%player.brick[6] = $bricks[6,0];
	messageClient(%client, 'MsgItemPickup', '', 6, $bricks[6,0].invname);
	%player.inventory[7] = nameToID(brick2x2);
	%player.brick[7] = $bricks[7,0];
	messageClient(%client, 'MsgItemPickup', '', 7, $bricks[7,0].invname);
	%player.inventory[8] = nameToID(brick2x2);
	%player.brick[8] = $bricks[8,0];
	messageClient(%client, 'MsgItemPickup', '', 8, $bricks[8,0].invname);
	%player.inventory[9] = nameToID(brick2x2);
	%player.brick[9] = $bricks[9,0];
	messageClient(%client, 'MsgItemPickup', '', 9, $bricks[9,0].invname);
    for(%i=10;%i<20;%i++)
    {
        %player.inventory[%i] = 0;
        messageClient(%client, 'MsgItemPickup', '', %i, "");
    }
  } 
  else {
    for(%i=0;%i<20;%i++) {
    %player.inventory[%i] = 0;
	messageClient(%client, 'MsgItemPickup', '', %i, "");
    }
  }
    if($Server::MissionType $= "TBMSandBox") {
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
  %player.setShapeName(%this.name);
  %this.camera.setTransform(%player.getEyeTransform());
  %this.player = %player;
  %this.setControlObject(%player);
  if (strstr(getmodpaths(),"dtb")!=-1)
    DTBcreatePlayer(%this, %spawnPoint);
  }

function GameConnection::transmitSettings(%this, %t) {
if(%t $= "" || %t $= "weapons") {
  for(%x = 1; %x <= $weptotal; %x++) {
    if(isObject($weapon[%x])) {
      if($weapon[%x].spawnName !$= "")
        %y = $weapon[%x].spawnName;
      else
        %y = $weapon[%x].invName;
    }
    else
      %y = $weapon[%x];
    schedule(10 * %x, 0, commandToClient, %this, 'ReceiveWeaponName', %y, (%x == 1));
  }
}
if(%t $= "" || %t $= "news") {
  if($Pref::Server::EnableNews && !%this.receivedNews) {
    %x = 0;
    %file = new FileObject();
    %file.openForRead("tbm/client/ServerNews.hfl");
    commandToClient(%this,'GetServerNews', 0);
    while(!%file.isEOF())
      schedule(10 * %x++, 0, commandToClient, %this, 'GetServerNews', 1, %file.readline());
    %file.close();
    %file.delete();
    %this.receivedNews = 1;
  }
}
if(%t $= "" || %t $= "bodytypes") {
  for(%i = 0; %i <= $BodytypeCount; %i++)
    schedule(10 * %i, 0, commandToClient, %this, 'GetBodytype', %i, ($Bodytype[%i].name $= "" ? $Bodytype[%i] : $Bodytype[%i].name));
}
if(%t $= "" || %t $= "switches") {
  for(%i = 1; %i <= $Switch::numTypes; %i++)
    schedule(10 * %i, 0, commandToClient, %this, 'GetSwitchAction', %i, $Switch::Type[%i], $Switch::Name[%i]);
  for(%i = 1; %i <= $Switch::numGroups; %i++)
    schedule(10 * %i, 0, commandToClient, %this, 'GetSwitchGroup', %i, $Switch::Group[%i], $Switch::Groupname[$Switch::Group[%i]]);
}
}

//-----------------------------------------------------------------------------
// Support functions
//-----------------------------------------------------------------------------

function pickSpawnPoint(%tag) {
  %a=-1;
  if (%tag>0) {
    for (%i=0; %i<missioncleanup.getcount(); %i++) {
      if (missioncleanup.getobject(%i).type[1] == %tag * -1) {
        missioncleanup.getobject(%i).radius=5;
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
  else {
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
//these functions were messed up in the common/server/missionload.cs
//so i brought them in here and fixed them  (now i'm l33t)
function loadMission( %missionName, %isFirstMission )
{
   endMission();
   echo("*** LOADING MISSION: " @ %missionName);
   echo("*** Stage 1 load");

   // Reset all of these
   clearCenterPrintAll();
   clearBottomPrintAll();

   // increment the mission sequence (used for ghost sequencing)
   $missionSequence++;
   $missionRunning = false;

   // Extract mission info from the mission file,
   // including the display name and stuff to send
   // to the client.
   buildLoadInfo( %missionName );

   // Download mission info to the clients
   %count = ClientGroup.getCount();
   for( %cl = 0; %cl < %count; %cl++ ) {
      %client = ClientGroup.getObject( %cl );
      if (!%client.isAIControlled())
         sendLoadInfoToClient(%client);
   }
   $Server::MissionName = %missionName;
   $Server::MissionFile = %missionName;
   // if this isn't the first mission, allow some time for the server
   // to transmit information to the clients:
   if( %isFirstMission || $Server::ServerType $= "SinglePlayer" )
      loadMissionStage2();
   else
      schedule( $MissionLoadPause, ServerGroup, loadMissionStage2 );
}

function loadMissionStage2()
{
   // Create the mission group off the ServerGroup
   echo("*** Stage 2 load");
   $instantGroup = ServerGroup;

   // Make sure the mission exists
   %file = $Server::MissionFile;

   if( !isFile( %file ) ) {
      error( "Could not find mission " @ %file );
      return;
   }

   // Calculate the mission CRC.  The CRC is used by the clients
   // to caching mission lighting.
   $missionCRC = getFileCRC( %file );

   // Exec the mission, objects are added to the ServerGroup
   exec(%file);

   // If there was a problem with the load, let's try another mission
   if( !isObject(MissionGroup) ) {
      error( "No 'MissionGroup' found in mission \"" @ $missionName @ "\"." );
      schedule( 3000, ServerGroup, CycleMissions );
      return;
   }

   // Mission cleanup group
   new SimGroup( MissionCleanup );
   $instantGroup = MissionCleanup;

   // Construct MOD paths
   pathOnMissionLoadDone();

   // Mission loading done...
   echo("*** Mission loaded");

   // Start all the clients in the mission
   $missionRunning = true;
   for( %clientIndex = 0; %clientIndex < ClientGroup.getCount(); %clientIndex++ ) {
      ClientGroup.getObject(%clientIndex).loadMission();
   }
   // Go ahead and launch the game
   onMissionLoaded();
   purgeResources();
}

function sendLoadInfoToClient( %client )
{
	messageClient( %client, 'MsgLoadInfo', "", MissionInfo.name );
    commandToClient( %client, 'MsgLoadimage', MissionInfo.preview );
	// Send Mission Description a line at a time
	for( %i = 0; MissionInfo.desc[%i] !$= ""; %i++ )
      messageClient( %client, 'MsgLoadDescripition', "", MissionInfo.desc[%i] );

   messageClient( %client, 'MsgLoadInfoDone' );
}

package AnimatedBricks {
function StaticShapeData::onAdd(%this, %obj) {
if(!MissionGroup.showcaseServer)
  Parent::onAdd(%this, %obj);
schedule(10, 0, animateBrick, %this, %obj, 1);
}
};
activatepackage(AnimatedBricks);

function animateBrick(%this, %obj, %a) {
if(%a == 2)
  %a = !%obj.isAnimated;
if(%a && (%t = %this.aniTime * 1000) !$= "" && !isTempBrick(%obj)) {
  %obj.isAnimated = 1;
  %obj.schedule((%t * mCeil(getSimTime()/%t))-getSimTime(), playThread, 1, animate);
  return;
}
if(!%a) {
  %obj.isAnimated = 0;
  %obj.playThread(1, root);
}
}

function serverCmdAnimateBrick(%client, %mode) {
if(!%mode) {
  if(isObject(%brick = %client.lastSwitch))
    animateBrick(%brick.getDatablock(), %brick, 2);
}
else {
  %client.animateBricks = !%client.animateBricks;
  %count = MissionCleanup.getCount();
  %ip = getRawIP(%client);
  for (%i = 1; %i < %count; %i++) {
    if((%brick = MissionCleanup.getObject(%i)).owner $= %ip)
      animateBrick(%brick.getDatablock(), %brick, %client.animateBricks);
  }
}
}

package DroidThingy {
function serverCmdAdjustObj(%client, %mode, %var1) {
if(%client.edit && %mode == 1000) {
  if(%client.lastswitch.getClassName() !$= "AIPlayer") {
    if(isObject($Bodytype[%client.bodyType])) {
       %p = new AIPlayer() {
          dataBlock = $Bodytype[%client.bodyType];
          aiPlayer = true;
          client = %client;
       };
    }
    else {
       %p = new AIPlayer() {
          dataBlock = LightMaleHumanArmor;
          aiPlayer = true;
          client = %client;
       };
    }
    %p.setTransform(vectoradd(%client.getcontrolobject().position,vectorscale(%client.getcontrolobject().getforwardvector(),"3 3 0")));
    %p.clearAim();
    %p.owner = getrawip(%client);
    if(isobject(%client.lastswitch))
      %client.lastswitch.setskinname(%client.lastswitchcolor);
    %client.lastswitch=%p;
    //GHOSTEDIT %client.lastswitch.setskinname('ghost');
    handleghostcolor(%client,%client.lastswitch);
    MissionCleanup.add(%p);
  }
  if(%client.lastswitch.getClassName() $= "AIPlayer") {
    %client.lastswitch.setShapeName(%client.name);
    %client.lastswitch.unMountImage($headSlot);
    %client.lastswitch.unMountImage($visorSlot);
    %client.lastswitch.unMountImage($backSlot);
    %client.lastswitch.unMountImage($leftHandSlot);
    %client.lastswitch.unMountImage($chestSlot);
    %client.lastswitch.unMountImage($faceSlot);
    %client.lastswitch.unMountImage(7);
    %client.lastswitchcolor=%client.colorSkin;
    %p.headCode=%client.headCode;
    %p.headCodeColor=gettaggedstring(%client.headCodeColor);
    %client.lastswitch.mountImage(%client.headCode, $headSlot, 1, %client.headCodeColor);
    %p.visorCode=%client.visorCode;
    %p.visorCodeColor=gettaggedstring(%client.visorCodeColor);
    %client.lastswitch.mountImage(%client.visorCode, $visorSlot, 1, %client.visorCodeColor);
    %p.backCode=%client.backCode;
    %p.backCodeColor=gettaggedstring(%client.backCodeColor);
    %client.lastswitch.mountImage(%client.backCode, $backSlot, 1, %client.backCodeColor);
    %p.leftHandCode=%client.leftHandCode;
    %p.leftHandCodeColor=gettaggedstring(%client.leftHandCodeColor);
    %client.lastswitch.mountImage(%client.leftHandCode, $leftHandSlot, 1, %client.leftHandCodeColor);
    %p.chestdecalcode=gettaggedstring(%client.chestdecalcode);
    %client.lastswitch.mountImage(chestShowImage, $chestSlot, 1, %client.chestdecalcode);
    %p.facedecalcode=gettaggedstring(%client.facedecalcode);
    %client.lastswitch.mountImage(faceplateShowImage, $faceSlot, 1, %client.facedecalcode);
    return;
  }
}
return Parent::serverCmdAdjustObj(%client, %mode, %var1);
}
};
activatePackage(DroidThingy);