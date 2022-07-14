//###################################
//# 04 Stuff
//###################################
function setPlantingCost()
{
	%cost = txtCashCost.getValue();
	%victim = $Pref::AdminGUI::SelectedPerson;
	if(%cost $= "" || %victim $= "")
	   return;

	commandtoServer('setIndivPlantCost', %cost, %victim);
}

function AM_MinigameList::onSelect( %this, %id, %text )
{
	//Deathmatch
	if(%id $= 0)
	{
		$Pref::Minigames::SelectedMinigame = "Deathmatch";
		AM_MinigameDesc.setText("Deathmatch is just the Standard Minigame in Blockland. You can choose a Variety of Options and Rules to Play by, then blow the crap out of eachother. We also have a Special Weapon only available in DM, which are the Bombs. They may be turned into a new Game Mode eventually.");
		AM_MinigamePic.setBitmap("rtb/client/ui/GameModePreviews/deathmatch.png");
	}
	//Cops and Robbers
	else if(%id $= 1)
	{
		$Pref::Minigames::SelectedMinigame = "Cops and Robbers";
		AM_MinigameDesc.setText("Cops and Robbers is a game almost anyone has heard of. We currently have 2 maps for Cops and Robbers, both different in more than one way. The idea is that there are 2 teams, Cops and Robbers. The Robbers have to infiltrate the Bank, steal cash, and get it back to their base without being killed. The Cops have to prevent Them.");
		AM_MinigamePic.setBitmap("rtb/client/ui/GameModePreviews/copsandrobbers.png");
	}
	//BBC
	else if(%id $= 2)
	{
		$Pref::Minigames::SelectedMinigame = "BBC";
		AM_MinigameDesc.setText("The Best Builders Competition is basically a Competition to see who the Best Builder is on a Server, using all Participants of the Competition as Judges. You start at Round 1, where you claim your plate. There is then Round 2, where if the Admin has enabled warmup time, you can test some structures. Then Round 3 is the Building. After the Building is Done, everyone can go and Rate the builds from 0, to 5. The winner is then announced at the end of this Time Period.                                                               -UNDER CONSTRUCTION-");
		AM_MinigamePic.setBitmap("rtb/client/ui/GameModePreviews/bbc.png");
	}
	//WTF?
	else
	{
	}
}

function MinigameOptions()
{
	%id = AM_MinigameList.getSelectedID();
	if(%id $= 0)
	{
		openClose("AMdeathmatch", "admingui");
	}
	else if(%id $= 1)
	{
		openClose("AMcandr", "admingui");
	}
	else if(%id $= 2)
	{
		openClose("AMbbc", "admingui");
	}
}

function applyPosSettings()
{
	%Pos = mBrickXYZPos.getValue();
	%Rot = mBrickXYZPos.getValue();
	if(%Rot $= "" || %Pos $= "")
	   return;
	commandtoserver('applymBrickPos', %Pos, %Rot);
}

function unBan()
{
	%ID = AL_banList.getSelectedID();
	if(%ID $= "")
	   return;
	
	commandtoserver('RemoveBan', %ID);
	AL_banList.clear();
}

function clearABricks()
{
	MessageBoxYesNo( "Clear Bricks?", "Are you Sure you want to clear ALL the Bricks?", "clearBricksA();", "");
}

function clearBricksA()
{
	commandtoserver('clearAllBricks');
}

function appBP()
{
	%selected = BrickPrintList.getSelectedID();
	if(%selected $= "-1")
	{
		return;
	}
	else
	{
		commandtoserver('brickprint');
	}
}

function adminGui::onWake()
{
	AL_Banlist.clear();
	commandtoserver('getBanList');
	AP_PlayerStatus.clear();
	commandtoserver('getPLayerList');
	AT_PlayerTeams.clear();
	commandtoserver('getPLayerTeamList');
	AT_Teams.clear();
	commandtoserver('getTeamList');
	AM_MinigameList.clear();
	AM_MinigameList.addRow(0,"Standard Deathmatch");
	AM_MinigameList.addRow(1,"Cops and Robbers");
	AM_MinigameList.addRow(2,"Best Builders Competition");
}

function mkh()
{
	%Victim = $Pref::AdminGUI::SelectedPerson;
	commandtoserver('masterKeyHolder', %Victim);
}

function PersistenceList::OnSelect( %this, %id, %text )
{
	txtSaveLoad.setValue(%text);
}

function Doinitbestbuilderscompetition()
{
	%alloweditorwand = optAllowEWand.getValue();
	%allowmovers = optAllowMovers.getValue();
	%allowteams = optAllowTeams.getValue();
	%buildtimelimit = BuildingTimeLimit.getValue();
	%warmuptime = WarmUpTime.getValue();
	%warmuptimeYN = optAllowWarmUpTime.getValue();
	%allowjoininwarmup = optAllowJoininWUT.getValue();

	$Pref::BBC::AllowEditorWand = %AllowEditorWand;
	$Pref::BBC::AllowMovers = %AllowMovers;
	$Pref::BBC::AllowTeams = %AllowTeams;
	$Pref::BBC::BuildTimeLimit = %BuildtimeLimit;
	$Pref::BBC::Warmuptime = %warmuptime;
	$Pref::BBC::WarmuptimeYN = %warmuptimeYN;
	$Pref::BBC::Allowjoininwarmup = %allowjoininwarmup;

commandtoserver('PreinitBestBuildersCompetition',%alloweditorwand,%allowmovers,%allowteams,%buildtimelimit,%warmuptime,%warmuptimeYN,%allowjoininwarmup);

}

function PlayerRights()
{
	%victimId = lstAdminPlayerList.getSelectedId();
	if(%victimId $= "-1")
	{
		return;
	}
	$Pref::AdminGUI::SelectedPerson = %victimId;
	openclose(playerrights,admingui);
}

function ApplyImpulseSettings()
{
	$Pref::Player::ImpulseX = sliXimpulse.getValue();
	$Pref::Player::ImpulseY = sliYimpulse.getValue();
	$Pref::Player::ImpulseZ = sliZimpulse.getValue();
	$Pref::Player::ImpulseT = isTriggeredJumper.getValue();
	$Pref::Player::ImpulseTID = JumpSetId.getValue();
	commandtoserver('applyImpulse', $Pref::Player::ImpulseX, $Pref::Player::ImpulseY, $Pref::Player::ImpulseZ, $Pref::Player::ImpulseT, $Pref::Player::ImpulseTID);
	canvas.popDialog(impulseGui);
}

function serverrulesGui::onWake(%this)
{
	commandToServer('getServerRules');
}

function serverConfig::onWake(%this)
{
	txtBans.setValue($RBan::numBans);
}

function serverConfig()
{
	canvas.pushDialog(serverConfig);
}

function delplayerbricks::onWake(%this)
{
	ownername.setValue($Pref::BP::Owner);
	ownerip.setValue($Pref::BP::OwnerIP);
}

function PWBoxGUI::onWake(%this)
{
	txtdoorPassword.setValue($Pref::PW::DoorPassword);
}

function DeletePlayerBricks()
{
	%clientname = ownername.getValue();
	commandtoserver('delplayerbricks',%clientname);
}

function banplayerb()
{
	%victimname = ownername.getValue();
	%victimip = ownerip.getValue();
	commandtoserver('banplayerip',%victimip,%victimname);
}

function persistenceLoad::onWake(%this)
{
	PersistenceList.clear();
	%i = 0;

	%filename = $Server::MissionFile @ "_" @ "*" @ ".persistence";
for(%file = findFirstFile(%filename);%file !$= ""; %file = findNextFile(%filename))  
{
	if (strStr(%file, "CVS/") == -1 && strStr(%file, "common/") == -1)
	{
		%string = getPersName(%file);
		%toAdd = getsubstr(%string,strstr(%string,"_") +1,strlen(%string) - strstr(%string,"_"));
		PersistenceList.addRow(%i++, %toAdd);
		$TotalPersistenceFiles++;
	}
}
	PersistenceList.sort(0);
	PersistenceList.scrollVisible(1);
}

function getPersName( %missionFile ) 
{
      return fileBase(%missionFile); 
}

function deathmatchGUI::onWake(%this)
{
	OptBrickDestroy.setValue($Pref::Server::DMBreakBricks);
	OptFriendlyFire.setValue($Pref::Server::DMFriendlyFire);
	OptJetPack.setValue($Pref::Server::DMJetPack);
	OptClearInventory.setValue($Pref::Server::DMClearInventory);
	Optbombrigging.setValue($Pref::Server::BombRigging);
	TxtBrickRespawnTime.setValue($Pref::Server::DMBrickReSpawnTime);
	TxtBrickMaxHits.setValue($Pref::Server::DMMaxBrickHits);
	
}

function moversGui::onWake(%this)
{

commandtoServer('getWrenchSettings');

}

function impulseGui::onWake(%this)
{

commandtoServer('getImpulseSettings');

}

function ban2()
{
	//get client id from admin player list
	%victimId = lstAdminPlayerList.getSelectedId();
	//send command to server
	commandToServer('ban', %victimId, 1);
}

function VerifyPassword()
{
%doorpassword = txtDoorPassword.getValue();
commandtoserver('checkdoorpassword',%doorpassword);
canvas.popDialog(PWBoxGUI);
}

function DeleteMovement()
{
	commandtoserver('DeleteBrickMovement');
}

function ApplyTriggerMover()
{
	$Pref::Wrench::MoveXYZ = MoveXYZ.getValue();
	$Pref::Wrench::RotateXYZ = RotateXYZ.getValue();
	$Pref::Wrench::Steps = Steps.getValue();
	$Pref::Wrench::StepTime = StepTime.getValue();

	if(Private.getValue() $= 1)
	   $Pref::Wrench::Private = 1;
	else
	   $Pref::Wrench::Private = "";

	if(TeamYN.getValue() $= 1)
	   $Pref::Wrench::Team = Team.getValue();
	else
	   $Pref::Wrench::Team = "";
	
	if(GroupYN.getValue() $= 1)
	   $Pref::Wrench::Group = Group.getValue();
	else
	   $Pref::Wrench::Group = "";
	
	if(PasswordYN.getvalue() $= 1)
  	   $Pref::Wrench::Password = Password.getValue();
	else
  	   $Pref::Wrench::Password = "";

	$Pref::Wrench::ReturnDelay = ReturnDelay.getValue();
	$Pref::Wrench::ReturnToggle = ReturnToggle.getValue();
	$Pref::Wrench::TriggerCloak = triggerCloak.getValue();
	$Pref::Wrench::Elevator = Elevator.getValue();
	$Pref::Wrench::DoorSetID = dsid.getValue();
	if($Pref::Wrench::MTypeS $= "Normal")
	{
		$Pref::Wrench::NoCollision = 0;
		$Pref::Wrench::TriggerImp = 0;
	}
	else if($Pref::Wrench::MTypeS $= "Trigger")
	{
		$Pref::Wrench::NoCollision = 0;
		$Pref::Wrench::TriggerImp = isJumperTrigger.getValue();
	}
	else if($Pref::Wrench::MTypeS $= "Triggered")
	{
		$Pref::Wrench::NoCollision = noCollision.getValue();
		$Pref::Wrench::TriggerImp = 0;
	}

	commandtoserver('ApplyDoorSettings',$Pref::Wrench::MoveXYZ, $Pref::Wrench::RotateXYZ, $Pref::Wrench::Steps, $Pref::Wrench::StepTime, $Pref::Wrench::Elevator, $Pref::Wrench::TriggerCloak, $Pref::Wrench::Private, $Pref::Wrench::Team, $Pref::Wrench::Group, $Pref::Wrench::TriggerImp, $Pref::Wrench::ReturnDelay, $Pref::Wrench::ReturnToggle, $Pref::Wrench::Password, $Pref::Wrench::MTypeS, $Pref::Wrench::DoorSetID, $Pref::Wrench::NoCollision);

}
//###################################

function messageGui::onWake(%this)
{
	commandToServer('isTalking');
}

function ignore()
{
	%victim = AP_playerStatus.getSelectedId();
	if(%victim $= "-1")
	   return;
	commandtoserver('toggleIgnore',%victim);
}

function togeditorWand()
{
	%victim = $Pref::AdminGUI::SelectedPerson;
	commandtoserver('giveeditorwand',%victim);
}

function getPersName( %missionFile ) 
{
      return fileBase(%missionFile); 
}

function GiveMoney()
{
	%victim = lstAdminPlayerList.getSelectedId();
	%amount = txtGiveMoney.getValue();
	commandtoserver('GiveMoney',%victim,%amount);
}
function PlayerGiveMoney()
{
	%victim = lstPlayersPlayerList.getSelectedId();
	%amount = txtPlayerGiveMoney.getValue();
	commandtoserver('PlayerGiveMoney',%victim,%amount);
}

function PlaceItem()
{
	%item = txtItem.getValue();
	commandtoserver('PlaceItem',%item);
}
function RemTeam()
{
	%player = AT_playerTeams.getSelectedID();
	if(%player $= "-1")
	   return;
	commandtoserver('remTeam',%player);
	AT_playerTeams.clear();
	commandtoserver('getPlayerTeamList');
}
function SetTeam()
{
	%team = AT_Teams.getSelectedID();
	%player = AT_playerTeams.getSelectedID();
	if(%player $= "-1" || %team $= "-1")
	   return;
	commandtoserver('setTeam',%player,%team);
	AT_playerTeams.clear();
	commandtoserver('getPlayerTeamList');
}
function Addteam()
{
	%team = txtAddTeam.getValue();
	if(%team $= "")
	   return;
	commandtoserver('addTeam',%team);
	AT_Teams.clear();
	commandtoserver('getTeamList');
}
function Removeteam()
{
	%team = AT_Teams.getSelectedID();
	if(%team $= "-1")
	   return;
	commandtoserver('RemoveTeam',%team);
	AT_Teams.clear();
	AT_playerTeams.clear();
	commandtoserver('getTeamList');
	commandtoserver('getPlayerTeamList');
}
function JoinTeam()
{
	%team = AT_Teams.getSelectedID();
	if(%team $= "-1")
	   return;
	commandtoserver('joinTeam',%team);
	AT_playerTeams.clear();
	commandtoserver('getPlayerTeamList');
}
function LeaveTeam()
{
	%team = AT_Teams.getSelectedID();
	if(%team $= "-1")
	   return;
	commandtoserver('LeaveTeam',%team);
	AT_playerTeams.clear();
	commandtoserver('getPlayerTeamList');
}

function OpenClose(%open,%close)
{
	canvas.popDialog(%close);
	canvas.pushDialog(%open);
}

function StartDM()
{
	%breakBricks = OptBrickDestroy.getValue();
	%friendlyFire = OptFriendlyFire.getValue();
	%brickrespawn = TxtBrickRespawnTime.getValue();
	%brickHits = TxtBrickMaxHits.getValue();
	%jetPack = OptJetPack.getValue();
	%clearInv = OptClearInventory.getValue();
	%bombs = Optbombrigging.getValue();
	commandToServer('startDeathmatch',%breakBricks, %friendlyFire, %brickrespawn, %brickHits, %jetPack, %clearInv, %bombs);
}
function StartSuperMan()
{
	%breakBricks = OptBrickDestroy.getValue();
	%friendlyFire = OptFriendlyFire.getValue();
	%brickrespawn = TxtBrickRespawnTime.getValue();
	%brickHits = TxtBrickMaxHits.getValue();
	%jetPack = OptJetPack.getValue();
	%clearInv = OptClearInventory.getValue();
	commandToServer('startSuperManMode',%breakBricks, %friendlyFire, %brickrespawn, %brickHits, %jetPack, %clearInv);  
}

function kick()
{
	//get client id from admin player list
	%victimId = lstAdminPlayerList.getSelectedId();
	//send command to server
	if(%victimId $= "-1")
	   return;
	commandToServer('kick', %victimId);
}

function ban()
{
	//get client id from admin player list
	%victimId = lstAdminPlayerList.getSelectedId();
	//send command to server
	if(%victimId $= "-1")
	   return;
	commandToServer('ban', %victimId);
}

function SB()
{

	%victimId = $Pref::AdminGUI::SelectedPerson;
	commandtoserver('DisableBuildingRights', %victimId);

}

function Jail()
{

	%victimId = lstAdminPlayerList.getSelectedId();
	if(%victimId $= "-1")
	   return;
	%JLength = jailLength.getValue();
	if(%JLength $= "")
	{
		%JLength = "5";
	}
	commandtoserver('JailPlayer', %victimId, %JLength);

}

function getIP()
{
	%victimId = lstAdminPlayerList.getSelectedId();
	if(%victimId $= "-1")
	   return;
	commandToServer('getIP', %victimId);
}

function changeMap()
{
	//push the map change dialog
	//request maplist from server?

}

function ToggleSafe()
{
	%victimId = AP_playerStatus.getSelectedId();
	//send command to server
	if(%victimId $= "-1")
	   return;
	commandToServer('toggleSafe', %victimId);
	AP_playerStatus.clear();
	commandtoserver('getPlayerList');

}
function ToggleFriend()
{
	%victimId = AP_playerStatus.getSelectedId();
	//send command to server
	if(%victimId $= "-1")
	   return;
	commandToServer('toggleFriend', %victimId);
	AP_playerStatus.clear();
	commandtoserver('getPlayerList');

}
function VerifyPassword()
{
%doorpassword = txtDoorPassword.getValue();
commandtoserver('checkdoorpassword',%doorpassword);
canvas.popDialog(PWBoxGUI);
}
function Send()
{
	%victimId = lstMessagePlayerList.getSelectedId();
	%message = txtMessage.getValue();
	commandtoServer('sendMessage',%victimId,%message);
	txtMessage.setValue("");
	canvas.popDialog(messagegui);
	commandToServer('stopTalking');
}

function AdminToPlayer()
{
	%victimId = lstAdminPlayerList.getSelectedId();
	commandtoServer('AdminToPlayer',%victimId);
}

function PlayerToAdmin()
{
	%victimId = lstAdminPlayerList.getSelectedId();
	commandtoServer('PlayerToAdmin',%victimId);
}

function SetBrickMessage()
{
	%msg = txtBrickMessage.getValue();
	commandtoserver('setBrickMessage',%msg);
}

function MakeTempAdmin()
{
	%victimId = $Pref::AdminGUI::SelectedPerson;
	commandtoserver('makeTempAdmin',%victimId);
}

function Save()
{
	%filename = txtSaveLoad.getValue();
	commandtoserver('saveBricks',%filename);
}

function Load()
{
	%filename = txtSaveLoad.getValue();
	if(%filename $= "")
	{
		return;
	}
	commandtoserver('loadBricks',%filename);
}

function JetLag()
{
	%lag = txtJet.getValue();
	commandToserver('jetpacklag',%lag);
}

