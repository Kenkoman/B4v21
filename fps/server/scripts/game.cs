//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

// Game duration in secs, no limit if the duration is set to 0
//we're using $Pref::Server::TimeLimit now
//$Game::Duration = 600 * 60;

// When a client score reaches this value, the game is ended.
$Game::EndGameScore = 0;

// Pause while looking over the end game screen (in secs)
$Game::EndGamePause = 10;


//-----------------------------------------------------------------------------
//  Functions that implement game-play
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------

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

   exec("./audioProfiles.cs");
   exec("./camera.cs");
   exec("./markers.cs"); 
   exec("./triggers.cs"); 
   exec("./inventory.cs");
   exec("./shapeBase.cs");
   exec("./item.cs");
   exec("./staticShape.cs");
   //exec("./health.cs");
   exec("./weapon.cs");
   exec("./radiusDamage.cs");
   exec("./player.cs");
   exec("./aiPlayer.cs");

	exec("./hammer.cs");
	exec("./bow.cs");
	exec("./spear.cs");
	exec("./sword.cs");
	exec("./axe.cs");
	exec("./wrench.cs");
	exec("./wand.cs");

	exec("./plateMail.cs");
	exec("./pointyHelmet.cs");
	exec("./helmet.cs");
	exec("./visor.cs");
	exec("./quiver.cs");
	exec("./pack.cs");
	exec("./airtank.cs");
	exec("./ski.cs");
	exec("./shield.cs");
	exec("./scoutHat.cs");
	exec("./bucketPack.cs");
	exec("./triPlume.cs");
	exec("./goblet.cs");
	exec("./cape.cs");

	exec("./sprayCan.cs");

	exec("./brick.cs");

	exec("./baseplate.cs");
	exec("./brick2x2.cs");
	exec("./brick2x4.cs");

	exec("./brick1x1.cs");
	exec("./brick1x2.cs");
	exec("./brick1x4.cs");
	exec("./brick1x4x5window.cs");

	exec("./plate1x1.cs");
	exec("./plate1x2.cs");
	exec("./plate1x4.cs");
	exec("./plate1x6.cs");
	exec("./plate1x8.cs");
	exec("./plate1x10.cs");
	exec("./plate2x4.cs");
	exec("./plate6x12.cs");

	exec("./specialVehicles.cs");

	exec("./precipitation.cs");

   exec("./car.cs");

   exec("./showImages.cs");

   // Keep track of when the game started
   $Game::StartTime = $Sim::Time;
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

function GameConnection::onClientEnterGame(%this)
{
	//get the clients color prefrences.  
  // commandToClient(%this, 'updatePrefs');

   commandToClient(%this, 'SyncClock', $Sim::Time - $Game::StartTime);

   // Create a new camera object.
   %this.camera = new Camera() {
      dataBlock = Observer;
   };
   MissionCleanup.add( %this.camera );
   %this.camera.scopeToClient(%this);

   // Setup game parameters, the onConnect method currently starts
   // everyone with a 0 score.
   %this.score = 0;

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

function GameConnection::onClientLeaveGame(%this)
{
	//remove the client's death vehicle
      %this.tumbleVehicle.delete();

	  //remove the clients temp block
	  %player = %this.player;
		if(isObject(%player.tempBrick))
		{
			%player.tempBrick.delete();
			%player.tempBrick = "";
		}

   if (isObject(%this.camera))
      %this.camera.delete();
   if (isObject(%this.player))
      %this.player.delete();
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
		%player.tempBrick.delete();
		%player.tempBrick = "";
	}

   // Switch the client over to the death cam and unhook the player object.
   if (isObject(%this.camera) && isObject(%this.player)) {
      %this.camera.setMode("Corpse",%this.player);
      %this.setControlObject(%this.camera);
   }
   %this.player = 0;

   // Doll out points and display an appropriate message
   if (%damageType $= "Suicide" || %sourceClient == %this) {
      %this.incScore(-1);
      messageAll('MsgClientKilled','%1 takes his own life!',%this.name);
   }
   else if(isObject(%sourceClient)) {
      %sourceClient.incScore(1);
      messageAll('MsgClientKilled','%1 gets nailed by %2!',%this.name,%sourceClient.name);
      if (%sourceClient.score >= $Game::EndGameScore)
         cycleGame();
   }
   else
   {
	  %this.incScore(-1);
      messageAll('MsgClientKilled','%1 dies.',%this.name);
   }
}


//-----------------------------------------------------------------------------

function GameConnection::spawnPlayer(%this)
{
   // Combination create player and drop him somewhere
   %spawnPoint = pickSpawnPoint();
   %this.createPlayer(%spawnPoint);
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

	if($Server::MissionType $= "SandBox")
	{
		%player.setSkinName(%this.colorSkin);
		%player.mountImage(%this.headCode, $headSlot, 1, %this.headCodeColor);
		%player.mountImage(%this.visorCode, $visorSlot, 1, %this.visorCodeColor);
		%player.mountImage(%this.backCode, $backSlot, 1, %this.backCodeColor);
		%player.mountImage(%this.leftHandCode, $leftHandSlot, 1, %this.leftHandCodeColor);
	}

   //clearplayer's inventory gui
   messageClient(%this, 'MsgClearInv');

   %player.isEquiped[0] = false;
   %player.isEquiped[1] = false;
   %player.isEquiped[2] = false;
   %player.isEquiped[3] = false;
   %player.isEquiped[4] = false;

   %player.currWeaponSlot = -1;

   // Player setup...
   %player.setTransform(%spawnPoint);
   %player.setEnergyLevel(%player.getDataBlock().maxEnergy);
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
			echo("spawn object = ", %spawn);
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



