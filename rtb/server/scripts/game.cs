//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

// Game duration in secs, no limit if the duration is set to 0
//we're using $Pref::Server::TimeLimit now
//$Game::Duration = 600 * 60;

// When a client score reaches this value, the game is ended.
$Game::EndGameScore = 1000000;

// Pause while looking over the end game screen (in secs)
$Game::EndGamePause = 2;


//-----------------------------------------------------------------------------
//  Functions that implement game-play
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------



//----------------------------------------
//# 04 STUFF
//----------------------------------------

function GameConnection::onClientLeaveGame(%this)
{

	if($Pref::Server::SaveBrickOwnersOnExit == 1)
	{
		for(%t = 0; %t < MissionCleanup.getCount()+1; %t++)
		{
			%brick = MissionCleanup.getObject(%t);
			if(%brick.Owner == %this)
			{
				%brick.owner = %this;
				%brick.OwnerAway = 1;
			}
		}
	}

	for(%t = 0; %t < MissionCleanup.getCount(); %t++)
	{
	%bombObj = MissionCleanup.getObject(%t);

	if(%bombObj.bombID > 0 && %bombObj.owner $= %this)
	{
	%bombObj.dead = true;
	%bombObj.schedule(10, explode);
	}
	}

	if($pref::server::copsandrobbers)
	{
		if(%this.team $= "Cops")
		{
			$TotalCops--;
		}
		if(%this.team $= "Robbers")
		{
			$TotalRobbers--;
		}
		
	}
	//remove the client's death vehicle
      %this.tumbleVehicle.delete();

	  //remove the clients temp block
	  %player = %this.player;

	if(isObject(%player.tempBrick))
	{
		if(%player.tempBrick.isBrickGhostMoving $= 1)
		{
			%player.tempBrick.setSkinName(%player.tempBrick.oldskinname);
			%player.tempBrick.isBrickGhost = "";
			%player.tempBrick.isBrickGhostMoving = "";
			%player.tempBrick = "";
		}
		else
		{
		%player.tempBrick.delete();
		%player.tempBrick = "";
		}
	}

   if (isObject(%this.camera))
      %this.camera.delete();
   if (isObject(%this.player))
      %this.player.delete();
}


//----------------------------------------


function listClients()
{
	%clientIndex = 0;
	for( %clientIndex = 0; %clientIndex < ClientGroup.getCount(); %clientIndex++ )
	{
		%cl = ClientGroup.getObject( %clientIndex );
		echo(%cl, " ", getTaggedstring(%cl.name), " ", %cl.getAddress());
	}
}

function onServerCreated()
{
   // Server::GameType is sent to the master server.
   // This variable should uniquely identify your game and/or mod.
   $Server::GameType = "Lego";

   // Server::MissionType sent to the master server.  Clients can
   // filter servers based on mission type.
   $Server::MissionType = "SandBox";

   // GameStartTime is the sim time the game started. Used to calculated
   // game elapsed time.
   $Game::StartTime = 0;

   // Load up all datablocks, objects etc.  This function is called when
   // a server is constructed.


   exec("./constants.cs");
   exec("./serverCmd.cs");
   exec("./adminCommands.cs");
   exec("./audioProfiles.cs");
   exec("./camera.cs");
   exec("./markers.cs"); 
   exec("./triggers.cs"); 
   exec("./inventory.cs");
   exec("./shapeBase.cs");
   exec("./item.cs");
   exec("./staticShape.cs");
   exec("./weapon.cs");
   exec("./radiusDamage.cs");
   exec("./player.cs");
   exec("./brick.cs");

   //###################################
   //# Indiv. File Execs.
   //###################################
   exec("./Bricks/Brickexec.cs");
   exec("./Vehicles/Vehicleexec.cs");
   exec("./Tools/Toolexec.cs");
   exec("./Items/Itemexec.cs");
   exec("./AI/AIexec.cs");
   exec("./Weapons/Weaponexec.cs");
   //###################################
   //# End.
   //###################################

   exec("./movers.cs");
   exec("./particles.cs");
   exec("./bombexplosions.cs");
   exec("./bombradiusdamage.cs");
   exec("./message.cs");
   exec("./precipitation.cs");
   exec("./persistence.cs");
   exec("./showImages.cs");
   exec("./invSelect.cs");
   exec("./Brickprints.cs");
   exec("./environment.cs");



   // Keep track of when the game started
   $Game::StartTime = $Sim::Time;

   // Ensure no Gamemodes running that could interfere.
   if($Pref::Server::GameMode $= 1)
   {
	if($Pref::Server::GameModeType $= "Deathmatch")
	{	
		messageAdmin('name', "\c3A Game Mode (\c0Deathmatch\c3) was Running, so it has been Ended.");
		$Pref::Server::GameMode = 0;
		$Pref::Server::GameModeType = "";
		serverCmdEndDeathmatch();
	}
	if($Pref::Server::GameModeType $= "Cops and Robbers")
	{
		messageAdmin('name', "\c3A Game Mode (\c0Cops and Robbers\c3) was Running, so it has been Ended.");
		$Pref::Server::GameMode = 0;
		$Pref::Server::GameModeType = "";
		serverCmdEndCopsandRobbers();
	}
	if($Pref::Server::GameModeType $= "Best Builders Comp.")
	{
		messageAdmin('name', "\c3A Game Mode (\c0BBC\c3) was Running, so it has been Ended.");
		$Pref::Server::GameMode = 0;
		$Pref::Server::GameModeType = "";
		serverCmdEndBBC();
	}
   }
   $Pref::Server::CopsAndRobbers = 0;
   $Pref::Server::GameModeON = 0;
   $Pref::BBC::Initiated = 0;
   $Pref::BBC::NoBuilding = 0;
   $Pref::BBC::JudgingTime = 0;
   $Pref::BBC::RiggingProcess = 0;
   $Pref::BBC::Mode = 0;
   $Pref::BBC::NoBuilding = 0;
   $Pref::Server::TotalTeams = 0;
}

function onServerDestroyed()
{
   // This function is called as part of a server shutdown.
}


//-----------------------------------------------------------------------------

function onMissionLoaded()
{
   // Called by loadMission() once the mission is finished loading.
   // Nothing special for now, just start up the game play.
   startGame();
}

function onMissionEnded()
{
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

function startGame()
{
   if ($Game::Running) {
      error("startGame: End the game first!");
      return;
   }

     $TotalAddedMapItems = 0;

   // Inform the client we're starting up
   for( %clientIndex = 0; %clientIndex < ClientGroup.getCount(); %clientIndex++ ) {
      %cl = ClientGroup.getObject( %clientIndex );
      commandToClient(%cl, 'GameStart');

      // Other client specific setup..
      %cl.score = 0;
   }
	
	for (%i = 0; %i < MissionGroup.getCount(); %i++)
	{
		%obj = MissionGroup.getObject(%i);
		if (%obj.dataBlock $= "gray32")
		{
			%obj.setSkinName(%obj.color);
		}
		if($Pref::Server::Weapons == 0)
		{
			if(%obj.getClassname() $= "Item")
			{
				%objdata = %obj.getdatablock();
				if(%objdata.classname $= "Weapon") 
				{
					%obj.startFade(0, 0, true);
					%obj.hide(true);
				}
			}
		}
		if($Pref::Server::ItemsCostMoney == 1)
		{
			if(%obj.Datablock.Cost > 0)
			{
				%obj.setShapeName("$"@%obj.Datablock.Cost);
			}
		}
	}

   // Start the game timer
   if ($Pref::Server::TimeLimit)
      $Game::Schedule = schedule($Pref::Server::TimeLimit * 1000, 0, "onGameDurationEnd" );
   $Game::Running = true;
}

function endGame()
{
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

function onGameDurationEnd()
{
   // This "redirect" is here so that we can abort the game cycle if
   // the $Game::Duration variable has been cleared, without having
   // to have a function to cancel the schedule.
   if ($Game::Duration && !isObject(EditorGui))
      cycleGame();
}


//-----------------------------------------------------------------------------

function cycleGame()
{
   // This is setup as a schedule so that this function can be called
   // directly from object callbacks.  Object callbacks have to be
   // carefull about invoking server functions that could cause
   // their object to be deleted.
   if (!$Game::Cycling) {
      $Game::Cycling = true;
      $Game::Schedule = schedule(0, 0, "onCycleExec");
   }
}

function onCycleExec()
{
   // End the current game and start another one, we'll pause for a little
   // so the end game victory screen can be examined by the clients.
   endGame();
   $Game::Schedule = schedule($Game::EndGamePause * 1000, 0, "onCyclePauseEnd");
}

function onCyclePauseEnd()
{
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

function GameConnection::onConnectRequest( %client, %netAddress, %name )
{
	echo("Connect request from: " @ %netAddress);

	%client.name = %name;
	%client.HBR = 1;
	//check ban list
	%ip = getRawIP(%client);
	%i = 0;
	for(%i = 0; %i <= $Ban::numBans; %i++)
	{
		echo("Checking IP Ban Entry Number: ", %i);
		if(%ip $= $Ban::ip[%i])
		{
			return "You are banned.";
		}

	}

	
	if($Server::PlayerCount >= $pref::Server::MaxPlayers)
	{
	return "CR_SERVERFULL";
	}


	return "";
}

function GameConnection::onConnect( %client, %name )
{


	%client.connected = 1;
	commandToClient(%client, 'updatePrefs');
   // Send down the connection error info, the client is
   // responsible for displaying this message if a connection
   // error occures.
   messageClient(%client,'MsgConnectionError',"",$Pref::Server::ConnectionError);

   // Send mission information to the client
   sendLoadInfoToClient( %client );

   // Simulated client lag for testing...
   // %client.setSimulatedNetParams(0.1, 30);

   // Get the client's unique id:
   // %authInfo = %client.getAuthInfo();
   // %client.guid = getField( %authInfo, 3 );
   %client.guid = 0;
   addToServerGuidList( %client.guid );
   
   // Set admin status
   if (%client.getAddress() $= "local") {
      %client.isAdmin = true;
      %client.isSuperAdmin = true;
   }
   else {
      %client.isAdmin = false;
      %client.isSuperAdmin = false;
   }

   // Save client preferences on the connection object for later use.
   %client.gender = "Male";
   %client.armor = "Light";
   %client.race = "Human";
   %client.skin = addTaggedString( "base" );
   %client.setPlayerName(%name);
   %client.score = 0;

   // 
   $instantGroup = ServerGroup;
   $instantGroup = MissionCleanup;
   echo("CADD: " @ %client @ " " @ %client.getAddress());

   // Inform the client of all the other clients
   %count = ClientGroup.getCount();
   for (%cl = 0; %cl < %count; %cl++) {
      %other = ClientGroup.getObject(%cl);
      if ((%other != %client)) {
         // These should be "silent" versions of these messages...
         messageClient(%client, 'MsgClientJoin', "", 
               %other.name,
               %other,
               %other.sendGuid,
               %other.score, 
               %other.isAIControlled(),
               %other.isAdmin, 
               %other.isSuperAdmin);
      }
   }

   // Inform the client we've joined up
   messageClient(%client,
      'MsgClientJoin', '\c0Connected to Server, %1.', 
      %client.name, 
      %client,
      %client.sendGuid,
      %client.score,
      %client.isAiControlled(), 
      %client.isAdmin, 
      %client.isSuperAdmin);

   // Inform all the other clients of the new guy
   messageAllExcept(%client, -1, 'MsgClientJoin', '\c4%1 has connected to the server.', 
      %client.name, 
      %client,
      %client.sendGuid,
      %client.score,
      %client.isAiControlled(), 
      %client.isAdmin, 
      %client.isSuperAdmin);


	%ip = getRawIP(%client);
	if(%ip !$= "local")
	{
	%subnet = getIPMask(%ip);
	%i = 1;
	for(%i = 1; %i <= $Ban::numBans; %i++)
	{
		echo("Checking Subnet Log Entry Number: ", %i);
		if(%subnet $= $Ban::ipsubnet[%i])
		{
		messageAll('name', '\c3WARNING: \c0%1\'s IP almost Matches %2 who is Banned.', %client.namebase, $Ban::name[%i]);
		%client.HBR = 0;
		messageClient(%client, '', '\c3You have had your Building Rights Removed from You, as the first 3 Groups of your IP Match someone who is Banned!');
		}
	}
	}

   // If the mission is running, go ahead download it to the client
   if ($missionRunning)
      %client.loadMission();
   $Server::PlayerCount++;
}

function GameConnection::onClientEnterGame(%this)
{




messageAllExcept(%this, -1, 'MsgClientJoin', '\c0%1 has spawned in the server!', 
      %this.name, 
      %this,
      %this.sendGuid,
      %this.score,
      %this.isAiControlled(), 
      %this.isAdmin, 
      %this.isSuperAdmin);
      %this.contactHits = 0;


//get the clients color prefrences.  
  // commandToClient(%this, 'updatePrefs');

   commandToClient(%this, 'SyncClock', $Sim::Time - $Game::StartTime);

   // Create a new camera object.
   %this.camera = new Camera() {
      dataBlock = Observer;
   };
   MissionCleanup.add( %this.camera );
   %this.camera.scopeToClient(%this);
   %this.colorIndex = 0;
   %this.letterIndex = 0;
   %this.BlackletterIndex = 0;

   for(%t = 0; %t < $Pref::Server::TotalTeams; %t++)
   {
	if($Teams[%t] !$= "")
	{
		messageClient(%this,'MsgTeamAdd',"",$Teams[%t],%t);
	}
   }

   // Setup game parameters, the onConnect method currently starts
   // everyone with a 0 score.
   %this.score = 0;

   %this.Money = $Pref::Server::StartMoney;
   messageClient(%this,'MsgUpdateMoney',"",%this.Money);
   messageClient(%this,'','\c5%1', $Pref::Server::MOTD);
   if($Pref::Server::AutoSecure)
   {
	%this.secure = 1;
	messageClient(%this,'',"\c2You were automatically put in secure mode");
	%this.SafeListNum++;
	%this.SafeList[%this.SafeListNum] = %this;
	%this.FriendListNum++;
	%this.FriendList[%this.FriendListNum] = %this;
   }

   if($Pref::Server::SaveBrickOwnersOnExit == 1)
   {
	%ip = getRawIP(%this);
	for(%t = 0; %t < MissionCleanup.getCount()+1; %t++)
	{
		%brick = MissionCleanup.getObject(%t);
		//echo(%brick.OwnerIP);
		//echo(%ip);
		if(%brick.OwnerIP $= %ip)
		{
			%brick.owner = %this;
			%brick.OwnerAway = 0;
			%foundsome = 1;
			//error("check me out");
			//echo(%t);
		}
	}

	if(%foundsome == 1)
	{
		messageClient(%this,'',"\c4There are still brick from your previous visit here");
	}
   }


	if($Pref::Server::AutoFriend)
	{
		for(%t = 0; %t<ClientGroup.getCount(); %t++)
		{
			%cl = ClientGroup.getObject(%t);
			%this.FriendListNum++;
			%this.FriendList[%this.FriendListNum] = %cl;
			%cl.FriendListNum++;
			%cl.FriendList[%cl.FriendListNum] = %this;
			if(%cl != %this)
			{
				messageClient(%cl,'','\c4Auto added %1 to your friends list',%this.name);
			}
		}
	}


if($Pref::Server::CopsAndRobbers)
{
	%cl = %this;

	if($TotalCops > $TotalRobbers)
	{
		%cl.Team = "Robbers";
		%robberplus = 1;
		messageAll("","\c5...and Joined the Robbers");
bottomPrint(%cl,"\"Cops And Robbers\" Another Round!\nYou will be a ROBBER.\nYour aim is to get to the bank and get money, then run back to your base.\nKilling a cop will free another robber from jail.",20,3);
	}
	if($TotalCops < $TotalRobbers)
	{
		%cl.Team = "Cops";
		%copplus = 1;
		messageAll("","\c5...and Joined the Cops");
bottomPrint(%cl,"\"Cops And Robbers\" Another Round!!!!\nYou will be a COP.\nYour aim is to stop the robbers stealing money\nGet them down to less than half health then hit them with a lightsabre to imprison them. Win by imprisoning them all!",20,3);
	}
	if($TotalCops == $TotalRobbers)
	{
		%cl.Team = "Robbers";
		%robberplus = 1;
		messageAll("","\c5...and Joined the Robbers");
bottomPrint(%cl,"\"Cops And Robbers\" Another Round!\nYou will be a ROBBER.\nYour aim is to get to the bank and get money, then run back to your base.\nKilling a cop will free another robber from jail.",20,3);
	}
	if(%copplus == 1)
	{
		$TotalCops++;
	}
	if(%robberplus == 1)
	{
		$TotalRobbers++;
	}

		%cl.isImprisoned = 0;
		//%cl.player.setTransform(%cl.TeamSpawn);
		%cl.money = 0;
		messageClient(%cl,'MsgUpdateMoney',"",%cl.money);
			
}

	//give the client a death vehicle.  
	%newcar = new WheeledVehicle() 
	{
		dataBlock = deathVehicle;
		client = %client;
		initialPosition = "0 0 -90";
	};
	%this.tumbleVehicle = %newcar; 
	%newcar.setTransform("0 0 -90");

   // Create a player object.
   %this.spawnPlayer();
}
		

//-----------------------------------------------------------------------------

function GameConnection::onLeaveMissionArea(%this)
{
   // The control objects invoked this method when they
   // move out of the mission area.
}

function GameConnection::onEnterMissionArea(%this)
{
   // The control objects invoked this method when they
   // move back into the mission area.
}


//-----------------------------------------------------------------------------

function GameConnection::onDeath(%this, %sourceObject, %sourceClient, %damageType, %damLoc)
{

	%player = %this.player;
	
   // Clear out the name on the corpse
   %player.setShapeName("");

   //remove player's temp brick

		if(isObject(%player.tempBrick))
		{
			if(%player.tempBrick.isBrickGhostMoving $= 1)
			{
				echo(%player.tempBrick);
				%player.tempBrick.setSkinName(%player.tempBrick.oldskinname);
				%player.tempBrick.isBrickGhost = "";
				%player.tempBrick.isBrickGhostMoving = "";
				%player.tempBrick = "";
			}
			else
			{
				%player.tempBrick.delete();
				%player.tempBrick = "";
			}
		}

   
   if (%damageType $= "Suicide" || %sourceClient == %this) {
      %this.incScore(-1);
      messageAll('MsgClientKilled','%1 takes the easy way out...',%this.name);
   }
   else if(isObject(%sourceClient)) {
      %sourceClient.incScore(1);
      switch$ (%damageType)
      {
        case axe:
        messageAll('MsgClientKilled','%2\'s Axe got stuck in %1\'s head!',%this.name,%sourceClient.name);
	case blaster:
        messageAll('MsgClientKilled','%1 got blasted by %2!',%this.name,%sourceClient.name);
	case bow:
        messageAll('MsgClientKilled','%1 got nailed by %2!',%this.name,%sourceClient.name);
	case chainsaw:
        messageAll('MsgClientKilled','%1 got sawed in half by %2!',%this.name,%sourceClient.name);
	case crossbow:
        messageAll('MsgClientKilled','%2 bolted an arrow through %1\'s head!',%this.name,%sourceClient.name);
	case duallightsabre:
        messageAll('MsgClientKilled','%1 got mutilated by %2!',%this.name,%sourceClient.name);
        case pickaxe:
        messageAll('MsgClientKilled','%1 got axed in the back by %2!',%this.name,%sourceClient.name);
	case pistol:
        messageAll('MsgClientKilled','%1 got shot by %2!',%this.name,%sourceClient.name);
	case rifle:
        messageAll('MsgClientKilled','%1 got rifled in the crotch by %2!',%this.name,%sourceClient.name);
	case spear:
        messageAll('MsgClientKilled','%1 got speared by %2!',%this.name,%sourceClient.name);
	case speargun:
        messageAll('MsgClientKilled','%1 got speared by %2!',%this.name,%sourceClient.name);
	case sword:
        messageAll('MsgClientKilled','%1 got his arms cut off by %2!',%this.name,%sourceClient.name);
	case cutlass:
        messageAll('MsgClientKilled','%1 got his head severed by %2!',%this.name,%sourceClient.name);
	case katana:
        messageAll('MsgClientKilled','%1 got his body dismembered by %2!',%this.name,%sourceClient.name);
	case lightsabre:
        messageAll('MsgClientKilled','%1 got impaled by %2!',%this.name,%sourceClient.name);
	case revolver:
        messageAll('MsgClientKilled','%1 was riddled with bullets by %2!',%this.name,%sourceClient.name);
	case explosion:
        messageAll('MsgClientKilled','%1 was blown to pieces by %2!',%this.name,%sourceClient.name);
      }

      if (%sourceClient.score >= $Game::EndGameScore)
         cycleGame();
   }
   else
   {
	  %this.incScore(-1);
   //   messageAll('MsgClientKilled','%1 dies.',%this.name);
   }

		
	if(%this.team $= "Cops" && $Pref::Server::CopsAndRobbers)
	{
		%trans = %this.player.getPosition();
		%x = getword(%trans,0);
		%y = getword(%trans,1);
		%z = getword(%trans,2) + 1;

      		%key = new Item() {
      			position = vectoradd(vectorscale(%this.player.getForwardVector(),"2 2 2"), %X SPC %Y SPC %Z);
      			rotation = "1 0 0 0";
      			scale = "1 1 1";
      			dataBlock = key;
      			collideable = "0";
      			static = "0";
      			rotate = "0";
      		};
		%key.schedulePop();

		for(%t = 0; %t<ClientGroup.getCount(); %t++)
		{
			
			%cl = ClientGroup.getObject(%t);
			if(%cl.team $= "Robbers" && %cl.isImprisoned)
			{
				if(isObject(%sourceClient))
				{
				%cl.isImprisoned = 0;
				%cl.JailBrick.JailCount--;
				messageAll("",'\c0%1\c5 was released from jail!',%cl.name);
				%cl.player.kill();
				
   				// Switch the client over to the death cam and unhook the player object.
   				if (isObject(%this.camera) && isObject(%this.player)) {
      					%this.camera.setMode("Corpse",%this.player);
      					%this.setControlObject(%this.camera);
   				}
	
				if(isObject(%this.freezeObject))
				{
					%this.freezeObject.delete();
				}
		

  				%this.player = 0;

				return;
				}
			}
		}
	}

   // Switch the client over to the death cam and unhook the player object.
   if (isObject(%this.camera) && isObject(%this.player)) {
      %this.camera.setMode("Corpse",%this.player);
      %this.setControlObject(%this.camera);
   }
	
	if(isObject(%this.freezeObject))
	{
		%this.freezeObject.delete();
	}
		

   %this.player = 0;

   // Doll out points and display an appropriate message
}
//-----------------------------------------------------------------------------

function GameConnection::spawnPlayer(%this)
{
   // Combination create player and drop him somewhere
   %spawnPoint = pickSpawnPoint();
   %this.createPlayer(%spawnPoint);
	if($Pref::Server::UseInventory == 1)
	{
   		serverCmdAddtoInvent(%this,1,$StartPlates);
   		serverCmdAddtoInvent(%this,2,$StartSlopes);
  		serverCmdAddtoInvent(%this,3,$StartMisc);
   		serverCmdAddtoInvent(%this,4,$StartTools);
   		serverCmdAddtoInvent(%this,5,$StartSprayCans);
		if(%this.isEwanduserBBC || %this.isEWandUser || %this.isAdmin || %this.isSuperAdmin)
		{
   			serverCmdAddtoInvent(%this,6,$StartSpecial);
		}
   		serverCmdAddtoInvent(%this,0,$StartBricks);
   		servercmdFreeHands(%this);
	}

}   


//-----------------------------------------------------------------------------

function GameConnection::createPlayer(%this, %spawnPoint)
{


   if (%this.player > 0)  {
      // The client should not have a player currently
      // assigned.  Assigning a new one could result in 
      // a player ghost.
      error( "Attempting to create an angus ghost!" );
   }

   // Create the player object
   %player = new Player() {
      dataBlock = LightMaleHumanArmor;
      client = %this;
   };
   %this.player = %player;
	%player.inventory[0] = 0;
   	%player.inventory[1] = 0;
	%player.inventory[2] = 0;
	%player.inventory[3] = 0;
	%player.inventory[4] = 0;
	%player.weaponCount = 0;

   MissionCleanup.add(%player);

	if($Pref::Server::CopsAndRobbers && (%this.team $= "Cops" || %this.team $= "Robbers"))
	{
		%cl = %this;
		messageClient(%this, 'MsgClearInv');

			if(%this.team $= "Cops")
			{
				%this.money = 0;
				messageClient(%this,'MsgUpdateMoney',"",%this.money);
				%cl.player.mountImage($headCode[$CopHat],$HeadSlot,1,'blueDark');
				%cl.player.mountImage(%cl.chestCode , $decalslot, 1, 'Town-Police-Sheriff');
				%cl.player.mountImage(%cl.faceCode , $faceslot, 1, 'Shades');
				%cl.player.setSkinName('bluedark');
				serverCmdAddtoInvent(%this,0,$CopWeapon1);
				serverCmdAddtoInvent(%this,1,$CopWeapon2);
				serverCmdAddtoInvent(%this,2,$CopWeapon3);
				serverCmdAddtoInvent(%this,3,$CopWeapon4);
			}	
			if(%this.team $= "Robbers")
			{
				%this.money = 0;
				messageClient(%this,'MsgUpdateMoney',"",%this.money);
				%cl.player.mountImage($headCode[$RobHat],$HeadSlot,1,'black');
				%cl.player.mountImage(%cl.chestCode , $decalslot, 1, 'Town-Inmate');
				%cl.player.mountImage(%cl.faceCode , $faceslot, 1, 'Evil');
				%cl.player.setSkinName('black');
				serverCmdAddtoInvent(%this,0,$RobWeapon1);
				serverCmdAddtoInvent(%this,1,$RobWeapon2);
				serverCmdAddtoInvent(%this,2,$RobWeapon3);
				//serverCmdAddtoInvent(%this,3,$RobWeapon4);
			}
		%player.isEquiped[2] = false;
   		%player.isEquiped[3] = false;
   		%player.isEquiped[4] = false;	
	}
	else
	{
	commandtoclient(%this,'updateprefs');
	if($Server::MissionType $= "SandBox")
	{
		%player.setSkinName(%this.colorSkin);
		%player.mountImage(%this.headCode, $headSlot, 1, %this.headCodeColor);
		%player.mountImage(%this.visorCode, $visorSlot, 1, %this.visorCodeColor);
		%player.mountImage(%this.backCode, $backSlot, 1, %this.backCodeColor);
		%player.mountImage(%this.leftHandCode, $leftHandSlot, 1, %this.leftHandCodeColor);
		%player.mountImage(%this.chestCode , $decalslot, 1, %this.chestdecalcode);
		%player.mountImage(%this.faceCode , $faceprintslot, 1, %this.faceprintcode);
	}
		//clearplayer's inventory gui
  		messageClient(%this, 'MsgClearInv');

   		%player.isEquiped[0] = false;
  		%player.isEquiped[1] = false;
   		%player.isEquiped[2] = false;
   		%player.isEquiped[3] = false;
   		%player.isEquiped[4] = false;

   %player.currWeaponSlot = -1;
	}

	
   
   // Player setup...

if($Pref::Server::CopsAndRobbers)
{
	%CopsRnd = getRandom($TotalCopSpawnPoints,1);
	%RobsRnd = getRandom($TotalRobberSpawnPoints,1);

	if(%this.team $= "Cops")
	{
		%spawnPoint = $CopSpawn[%CopsRnd];
	}
	if(%this.team $= "Robbers")
	{
		%spawnPoint = $RobberSpawn[%RobsRnd];
	}
	
	echo("Spawn:" SPC %spawnPoint);
	%trans = %spawnPoint.getTransform();
	%x = getWord(%trans,0) + getRandom(3,-3);
	%y = getWord(%trans,1) + getRandom(3,-3);
	%z = getWord(%trans,2);
	
	%player.setTransform(%x SPC %y SPC %z);
}
else
{
%player.setTransform(%spawnPoint);
}

   

  // %player.setEnergyLevel(%player.getDataBlock().maxEnergy);
   %player.setShapeName(%this.name);

   // Update the camera to start with the player
   %this.camera.setTransform(%player.getEyeTransform());

   // Give the client control of the player
   %this.player = %player;
   %this.setControlObject(%player);
}


//-----------------------------------------------------------------------------
// Support functions
//-----------------------------------------------------------------------------

function pickSpawnPoint() 
{
   %groupName = "MissionGroup/PlayerDropPoints";
   %group = nameToID(%groupName);

	if (%group != -1) {
		%count = %group.getCount();
		if (%count != 0) {
			%index = getRandom(%count-1);
			%spawn = %group.getObject(%index);
//			echo("spawn object = ", %spawn);
			%trans = %spawn.getTransform();
			
			%transX = getWord(%trans, 0);
			%transY = getWord(%trans, 1);
			%transZ = getWord(%trans, 2);
			
			%r = getRandom(%spawn.radius * 10) / 10;
			%ang = getRandom($pi * 2 * 100) / 100;
			
			//x = r * cos( theta )
			%transX += %r * mCos(%ang);
			%transY += %r * mSin(%ang);
			 
			%transXY = %transX @ " " @ %transY;

			//%transZ = getTerrainHeight(%transXY);

			%spawnAngle = getRandom($pi * 2 * 100) / 100;

			%returnTrans = %transX  @ " " @ %transY @ " " @ %transZ @ " 0 0 1 " @ %spawnAngle;
			
			return %returnTrans;
			//return %spawn.getTransform();
		}
		else
			error("No spawn points found in " @ %groupName);
	}
	else
	error("Missing spawn points group " @ %groupName);

	error("default spawn!");
   // Could be no spawn points, in which case we'll stick the
   // player at the center of the world.
   return "0 0 300 1 0 0 0";
}



