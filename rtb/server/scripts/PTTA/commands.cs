//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Misc. server commands avialable to clients
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------

//----------------------!Toggle Voting!----------------------

function serverCmdToggleVoting(%client)
{
%ip = getRawIP(%client);
if(%ip $= "local" || %client.isAdmin || %client.isSuperAdmin)
{
if($Pref::Server::Voting)
{
messageall('','\c1Voting on this server has been \c3DISABLED');
$Pref::Server::Voting = false;
if($Pref::Server::VoteInProgress && %ip $= "local")
{
$Pref::Server::finishvote = 0;
messageall('','\c1This vote has been voided by the host');
$Pref::Server::VoteInProgress = false;
}
}
else
{
messageall('','\c1Voting on this server has been \c3ENABLED');
$Pref::Server::Voting = true;
}
}
}

//----------------------!How To Vote!----------------------

function serverCmdVoteYes(%client)
{
if(!%client.hasVotedNo && !%client.hasVotedYes && $Pref::Server::VoteInProgress)
{
$Pref::Server::VoteYes++;
messageClient(%client, '', '\c2You have voted \c3YES');
%client.hasVotedYes = true;
}
else
messageClient(%client, '', 'You have already voted');
}

function serverCmdVoteNo(%client)
{
if(!%client.hasVotedNo && !%client.hasVotedYes && $Pref::Server::VoteInProgress)
{
$Pref::Server::VoteNo++;
messageClient(%client, '', '\c2You have voted \c3NO');
%client.hasVotedNo = true;
}
else
messageClient(%client, '', 'You have already voted');
}

//----------------------!Kick Vote!----------------------

function serverCmdStartKickVote(%client, %victim)
{
%ip = getRawIP(%victim);
if(%ip !$= "local")
{
if($Pref::Server::Voting)
{
%time = $Sim::Time - $Pref::Server::LastVoteTime;
if(%time > 180)
{
//if(!$Pref::Server::VoteInProgress)
if($Server::PlayerCount > 2 && !$Pref::Server::VoteInProgress)
{
if(%victim.isSuperAdmin || %victim.isAdmin)
{
$Pref::Server::MoreThanVote = $Server::PlayerCount - 1;
}
else
{
$Pref::Server::MoreThanVote = $Server::PlayerCount * 0.51;
}
$Pref::Server::VoteInProgress = true;
$Pref::Server::VoteNo = 0;
$Pref::Server::VoteYes = 0;
$Pref::Server::VictimPunish = %victim;
messageall('','\c3%1 has started a vote to kick %2, the vote will last for 1 minute',%client.name, %victim.name);
messageall('','Please vote Yes, No, or Abstain - if you do not have PTTA use the command: commandtoserver(\'VoteYes\'); or commandtoserver(\'VoteNo\');');
$Pref::Server::finishvote = 60000;
schedule($Pref::Server::finishvote, 0, "KickVoteFinished");
}
else messageClient(%client, '', 'There are too few people on this server to start a kick vote or a vote is already in progress');
}
else
{
%wait =  (180- %time);
messageClient(%client, '', 'Votes cannot be held that often, you must wait %1 sec',%wait);
}
}
else messageClient(%client, '', 'Voting on this server is disabled');
}
else messageClient(%client, '', 'you cannot kick the local client');
}


function KickVoteFinished()
{
if($Pref::Server::VoteInProgress)
{
%victim = $Pref::Server::VictimPunish;
messageall('','\c3The Vote to kick %1 has finished',%victim.name);
messageall('','\c3Votes Yes: %1 No: %2 Players: %3',$Pref::Server::VoteYes, $Pref::Server::VoteNo, $Server::PlayerCount);
if($Pref::Server::VoteYes > $Pref::Server::MoreThanVote)
{
if($Pref::Server::Log)
	{
	$Logfile = new FileObject();
	$Logfile.openForAppend("rtb/server/ServerLog.txt");
	$Logfile.writeLine(">>*VOTE KICK*<<");
	$Logfile.writeLine("Name: "@ %victim.namebase @ " ip: " @getrawip(%victim)@ " Time: " @ $Sim::Time);
	$Logfile.close();
	}		
messageall('','\c3The vote has been passed, %1 was kicked from the server',%victim.name);
%victim.delete("You have been voted out of this server.");
}
else messageall('','\c3The vote has Failed');
$Pref::Server::VictimPunish = "";
$Pref::Server::VoteNo = 0;
$Pref::Server::VoteYes = 0;
$Pref::Server::VoteInProgress = false;
%count = ClientGroup.getCount();
for(%cl = 0; %cl < %count; %cl++)
{
%client = ClientGroup.getObject(%cl);
%client.hasVotedNo = false;
%client.hasVotedYes = false;
}
$Pref::Server::LastVoteTime = $Sim::Time;
}
}

//----------------------!Change Map Vote!----------------------

function serverCmdStartMapChgVote(%client, %mapname)
{
if($Pref::Server::Voting)
{
if(%mapname $= "bedroom"||%mapname $= "celestialdreams"||%mapname $= "deathvalley"||%mapname $= "greenhills"||%mapname $= "happyvalleys"||%mapname $= "kitchen"||%mapname $= "LavaPit2"||%mapname $= "RPGHaven"||%mapname $= "rtbisle"||%mapname $= "slopes")
{
%time = $Sim::Time - $Pref::Server::LastVoteTime;
if(%time > 180)
{
//if(!$Pref::Server::VoteInProgress)
if($Server::PlayerCount > 2 && !$Pref::Server::VoteInProgress)
{
$Pref::Server::TempClient = %client;
if($Pref::Server::MapName $= %mapname) %client.TallySameVotes++;
else %client.TallySameVotes=0;
if(%client.TallySameVotes < 3)
{
$Pref::Server::VoteInProgress = true;
$Pref::Server::VoteNo = 0;
$Pref::Server::VoteYes = 0;
$Pref::Server::MapName = %mapname;
messageall('','\c3%1 has started a vote to change maps to %2.mis-the vote will last for 1 minute, server will auto save before map is changed',%client.name, %mapname);
messageall('','Please vote Yes, No, or Abstain - if you do not have PTTA use the command: commandtoserver(\'VoteYes\'); or commandtoserver(\'VoteNo\');');
$Pref::Server::finishvote = 60000;
schedule($Pref::Server::finishvote, 0, "MapChgVoteFinished");
}
else
{
messageClient(%client, '', 'You cannot levy the same vote 3 times in a row, please wait 5 minutes to re-levy this vote');
schedule(300000, 0, "ClrTally");
}
}
else messageClient(%client, '', 'There are too few people on this server to start a map change vote or a vote is already in progress');
}
%wait =  (180- %time);
messageClient(%client, '', 'Votes cannot be held that often, you must wait %1 sec',%wait);
}
else messageClient(%client, '', '\"%1\" is not a valid mapname, please enter a valid map name',%mapname);
}
else messageClient(%client, '', 'Voting on this server is disabled');
}


function ClrTally()
{
%client = $Pref::Server::TempClient;
%client.TallySameVotes = 0;
}


function MapChgVoteFinished()
{
if($Pref::Server::VoteInProgress)
{
%mapname = $Pref::Server::MapName;
messageall('','\c3The Vote to change the map to %1 has finished',%mapname);
messageall('','\c3Votes Yes: %1 No: %2 Players: %3',$Pref::Server::VoteYes, $Pref::Server::VoteNo, $Server::PlayerCount);
if($Pref::Server::VoteYes > ($Server::PlayerCount*0.51))
{
if($Pref::Server::Log)
	{
	$Logfile = new FileObject();
	$Logfile.openForAppend("rtb/server/ServerLog.txt");
	$Logfile.writeLine(">>*VOTE CHANGE MAP*<<");
	$Logfile.writeLine(" Map: " @%mapname@ " Time: " @ $Sim::Time);
	$Logfile.close();
	}		
SavePersistence("VoteChangeTemp", 1);
$Pref::Server::LastChangedMap = %mapname;
messageall('','\c2Changing maps to: %1.mis',%mapname);
%mapname = "rtb/data/missions/"@%mapname@".mis";
loadMission(%mapname);
}
else messageall('','\c3The vote has Failed');
$Pref::Server::VoteNo = 0;
$Pref::Server::VoteYes = 0;
$Pref::Server::VoteInProgress = false;
%count = ClientGroup.getCount();
for(%cl = 0; %cl < %count; %cl++)
{
%client = ClientGroup.getObject(%cl);
%client.hasVotedNo = false;
%client.hasVotedYes = false;
}
$Pref::Server::LastVoteTime = $Sim::Time;
}
}


//----------------------!DM Vote!----------------------

function serverCmdStartDMVote(%client)
{
if($Pref::Server::Voting)
{
%time = $Sim::Time - $Pref::Server::LastVoteTime;
if(%time > 180)
{
//if(!$Pref::Server::VoteInProgress)
if($Server::PlayerCount > 2 && !$Pref::Server::VoteInProgress)
{
$Pref::Server::VoteInProgress = true;
$Pref::Server::VoteNo = 0;
$Pref::Server::VoteYes = 0;
messageall('','\c3%1 has started a vote to start/stop DM-the vote will last for 1 minute',%client.name);
messageall('','Please vote Yes, No, or Abstain - if you do not have PTTA use the command: commandtoserver(\'VoteYes\'); or commandtoserver(\'VoteNo\');');
$Pref::Server::finishvote = 60000;
schedule($Pref::Server::finishvote, 0, "DMVoteFinished");
}
else messageClient(%client, '', 'There are too few people on this server to start a DM vote or a vote is already in progress');
}
%wait =  (180- %time);
messageClient(%client, '', 'Votes cannot be held that often, you must wait %1 sec',%wait);
}
else messageClient(%client, '', 'Voting on this server is disabled');
}

function DMVoteFinished()
{
if($Pref::Server::VoteInProgress)
{
messageall('','\c3The Vote to start/stop DM has finished');
messageall('','\c3Votes Yes: %1 No: %2 Players: %3',$Pref::Server::VoteYes, $Pref::Server::VoteNo, $Server::PlayerCount);
if($Pref::Server::VoteYes > ($Server::PlayerCount*0.51))
{
messageall('','\c3The vote has been passed');
$Pref::Server::OverWriteVote = true;
if($Pref::Server::Weapons == 0)commandtoserver('StartDeathmatch',%client);
else commandtoserver('EndDeathmatch',%client);
$Pref::Server::OverWriteVote = false;
}
else messageall('','\c3The vote has Failed');
$Pref::Server::VoteNo = 0;
$Pref::Server::VoteYes = 0;
$Pref::Server::VoteInProgress = false;
%count = ClientGroup.getCount();
for(%cl = 0; %cl < %count; %cl++)
{
%client = ClientGroup.getObject(%cl);
%client.hasVotedNo = false;
%client.hasVotedYes = false;
}
$Pref::Server::LastVoteTime = $Sim::Time;
}
}

//----------------------------!END OF VOTING SCRIPTS!-----------------------------


function serverCmdToggleCamera(%client)
{
   //if ($Server::TestCheats || $Server::ServerType $= "SinglePlayer")
   if(%client.isAdmin || %client.isSuperAdmin || $Server::ServerType $= "SinglePlayer")
   {
      %control = %client.getControlObject();
      if (%control == %client.player)
      {
         %control = %client.camera;
         %control.mode = toggleCameraFly;
      }
      else
      {
         %control = %client.player;
         %control.mode = observerFly;
      }
      %client.setControlObject(%control);
   }
}

function serverCmdDropPlayerAtCamera(%client)
{
   //if ($Server::TestCheats)
   if(%client.isAdmin || %client.isSuperAdmin)
   {
      if (!%client.player.isMounted())
         %client.player.setTransform(%client.camera.getTransform());
      %client.player.setVelocity("0 0 0");
      %client.setControlObject(%client.player);
   }
}

function serverCmdDropCameraAtPlayer(%client)
{
   //if ($Server::TestCheats)
   if(%client.isAdmin || %client.isSuperAdmin)
   {
      %client.camera.setTransform(%client.player.getEyeTransform());
      %client.camera.setVelocity("0 0 0");
      %client.setControlObject(%client.camera);
   }
}


//-----------------------------------------------------------------------------

function serverCmdSuicide(%client)
{
    if (isObject(%client.player) && %client.isImprisoned == 0 && %client.isTimeout != 1 && $Pref::Server::suicide)
      %client.player.kill("Suicide");
}   

function serverCmdPlayCel(%client,%anim)
{
   if (isObject(%client.player))
      %client.player.playCelAnimation(%anim);
}

function serverCmdPlayDeath(%client)
{
   if (isObject(%client.player))
      %client.player.playDeathAnimation();
}

function serverCmdSelectObject(%client, %mouseVec, %cameraPoint)
{
   //Determine how far should the picking ray extend into the world?
   %selectRange = 200;
   // scale mouseVec to the range the player is able to select with mouse
   %mouseScaled = VectorScale(%mouseVec, %selectRange);
   // cameraPoint = the world position of the camera
   // rangeEnd = camera point + length of selectable range
   %rangeEnd = VectorAdd(%cameraPoint, %mouseScaled);

   // Search for anything that is selectable – below are some examples
   %searchMasks = $TypeMasks::PlayerObjectType | $TypeMasks::CorpseObjectType |
      				$TypeMasks::ItemObjectType | $TypeMasks::TriggerObjectType;

   // Search for objects within the range that fit the masks above
   // If we are in first person mode, we make sure player is not selectable by setting fourth parameter (exempt
   // from collisions) when calling ContainerRayCast
   %player = %client.player;
   if ($firstPerson)
   {
	  %scanTarg = ContainerRayCast (%cameraPoint, %rangeEnd, %searchMasks, %player);
   }
   else //3rd person - player is selectable in this case
   {
	  %scanTarg = ContainerRayCast (%cameraPoint, %rangeEnd, %searchMasks);
   }

   // a target in range was found so select it
   if (%scanTarg)
   {
      %targetObject = firstWord(%scanTarg);

      %client.setSelectedObj(%targetObject);
   }
}