//###########################################
//# 04 STUFF
//###########################################
function serverCmdModerateServer(%client)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		if($Pref::Server::Moderated $= 1)
		{
			$Pref::Server::Moderated = 0;
			messageAll('name', '\c3The Server Moderation is now \c0OFF\c3.');
		}
		else
		{
			$Pref::Server::Moderated = 1;
			messageAll('name', '\c3The Server Moderation is now \c0ON\c3.');
		}
	}
}

function serverCmdDeleteByIP(%client, %ip)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		for (%i = 0; %i < MissionCleanup.getCount(); %i++)
		{
			%brick = MissionCleanup.getObject(%i);
			if(%brick.OwnerIP == %ip)
			{
				%brick.dead = true;
				%brick.schedule(10, explode);
				if(%brick.Datablock $= "staticbrickFire")
				{
					%client.fireBrickCount--;
					%brick.flameEmitter.delete();
					%brick.smokeEmitter.delete();
				}
			} 
		}
	}	
}

function serverCmdsetIndivPlantCost(%client, %cost, %victim)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		if($Pref::Server::TogglePlantingCosts $= 1)
		{
			%victim.plantingPrice = %cost;
			messageClient(%victim,'','\c3Your Brick Planting Cost has been set to \c0$%1\c3.', %cost);
			messageClient(%client,'','\c3You have set \c0%1\'s\c3 Brick Planting Cost to \c0$%2\c3.', %victim.name, %cost);
		}
		else
		{
		   messageClient(%client,'',"\c4Brick Planting Costs are not On!");
		}
	}
}

function serverCmdTogglePlantingCosts(%client)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		if($Pref::Server::TogglePlantingCosts $= 1)
		{
			$Pref::Server::TogglePlantingCosts = 0;
			messageAll('name', '\c3Brick Planting Prices are now \c0OFF\c3.');
		}
		else
		{
			$Pref::Server::TogglePlantingCosts = 1;
			messageAll('name', '\c3Brick Planting Prices are now \c0ON\c3.');
		}
	}
}

function serverCmdgetBanList(%client)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		for(%i = 1; %i <= $Ban::numBans; %i++)
		{
			messageClient(%client,'MsgBanAdd',"",%i,$Ban::Name[%i],$Ban::IP[%i],$Ban::Subnet[%i]);
		}
	}
}

function servercmdRemoveBan(%client, %ID)
{
	if(%client.isSuperAdmin)
	{
		if($Ban::IP[%ID] $= "")
		{
			serverCmdgetBanList(%client);
			return;
		}
		$Ban::Name[%ID] = "";
		$Ban::IP[%ID] = "";
		$Ban::Subnet[%ID] = "";
		$IDer = 0;
		for(%i = 1; %i <= $Ban::numBans+1; %i++)
		{
			if($Ban::IP[%i] !$= "")
			{
				$IDer++;
				echo($IDer);
				$Ban::Name[$IDer] = $Ban::Name[%i];
				$Ban::IP[$IDer] = $Ban::IP[%i];
				echo($Ban::IP[$IDer]);
				if($Ban::Subnet[%i] !$= "")
				{
					$Ban::Subnet[$IDer] = $Ban::Subnet[%i];
				}
			}
		}
		$Ban::numBans--;
		serverCmdgetBanList(%client);
	}
}

function servercmdmasterKeyHolder(%client, %Victim)
{
	if(%client.isAdmin || %Client.isSuperAdmin)
	{
		if(!%victim.isAdmin || !%victim.isSuperAdmin)
		{
			if(%Victim.isMKH !$= 1)
			{
				messageClient(%client,"",'\c3You have made %1 a Master Key Holder!', %victim.name);
				messageClient(%victim,"",'\c3You have been made a Master Key Holder!');
				%victim.isMKH = 1;
			}
			else
			{
				messageClient(%client,"",'\c3You have removed %1\'s Master Key!', %victim.name);
				messageClient(%victim,"",'\c3Your Master Key has been Removed.');
				%victim.isMKH = 0;
			}
		}
		else
		{
			messageClient(%client,"",'\c3You cannot remove an Admin\'s Master Key.');
		}
	}
}

function serverCmdJailPlayer(%client,%victim,%time)
{
	if(%client.isAdmin || %client.isSuperAdmin || %client.isTempAdmin)
	{
		if(%victim.isTimeout != 1)
		{
		%jailbricks = 0;
		for(%t = 0; %t < MissionCleanup.getCount(); %t++)
		{
			%jail = MissionCleanup.getObject(%t);

			if(%jail.isDetBrick $= 1)
			{
			%jailbricks++;
			}
		}
		if(%jailbricks >= 1)
		{
		for(%t = 0; %t < MissionCleanup.getCount(); %t++)
		{
			%jail = MissionCleanup.getObject(%t);

			if(%jail.isDetBrick $= 1)
			{
				%trans = %jail.getTransform();
				%x = getWord(%trans,0);
				%y = getWord(%trans,1);
				%z = getWord(%trans,2);
				%player = %victim.player;
				%player.setTransform(%x SPC %y SPC %z);
				%victim.isTimeout = 1;
				%STime = %time*60;
				%STime = %STime*1000;
				%victim.TimeoutSchedule = Schedule(%STime,0,"FreeVictim",%client,%victim,%time);
				messageClient(%victim, '', '\c3You have been jailed by an Admin for %1 minutes!', %time);
				messageClient(%client, '', '\c3You have jailed %1 for %2 minutes.', %victim.namebase, %time);
				messageAllExcept(%victim, '\c3%1 was Jailed by %2 (%3 Minutes).', %victim.name, %client.name, %time);
			}
		}
		}
		else
		{
		messageClient(%client, '', '\c3It appears no jails brick has been specified!');
		}
		}
		else
		{
		cancel(%victim.TimeoutSchedule);
		messageClient(%victim, '', '\c3An Admin has released you from Jail!');
		messageClient(%client, '', '\c3You have released %1 from Jail.', %victim.namebase);
		%victim.isTimeout = 0;
		%victim.player.kill();
		}
	}
}

function FreeVictim(%client, %victim, %time)
{
%victim.isTimeout = 0;
messageClient(%victim, '', '\c3Your %1 minutes are up! Now dont do it again!', %time);
%victim.player.kill();
}

function serverCmdToggleWandMode(%client)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		%client.WandMode++;
		if(%client.WandMode > 5)
		{
			%client.WandMode = 0;
		}

	if(%client.WandMode == 0)
	{
		messageClient(%client,"","\c2Wand in Destroy-Mode");
	}
	if(%client.WandMode == 1)
	{
		messageClient(%client,"","\c2Wand in Teleport-Mode");
	}
	if(%client.WandMode == 2)
	{
		messageClient(%client,"","\c2Wand in Safe-Brick-Mode");
	}
	if(%client.WandMode == 3)
	{
		messageClient(%client,"","\c2Wand in Cloak-Mode");
	}
	if(%client.WandMode == 4)
	{
		messageClient(%client,"","\c2Wand in Jail-Mode");
	}
	if(%client.WandMode == 5)
	{
		messageClient(%client,"","\c2Wand in Destroy-By-Owner-Mode");
	}
	}
}

function serverCmdImportBanlist(%client)
{

if(%client.isSuperAdmin)
{
	for(%t = 1; %t<$RBan::numBans+1; %t++)
	{
	%goahead = 1;

		if($RBan::ip[%t] $= "" || $RBan::name[%t] $= "")
		{
		echo("\c2ERROR! Incorrect DATA in file rBans.cs - ID NUM:", %t);
		%goahead = 0;
		}


		%i = 0;
		for(%i = 0; %i <= $Ban::numBans; %i++)
		{
			if($RBan::ip[%t] $= $Ban::ip[%i] && $Ban::ip[%i] !$= "")
			{
				echo("\c2ERROR! IP already in ipBanList.cs - ID NUM:", %t);
				%goahead = 0;
			}

		}


		if(%goahead $= 1)
		{
		$Ban::numBans++;
		$Ban::ip[$Ban::numBans] = $RBan::ip[%t];

		if($RBan::ipsubnet[%t] !$= "")
		{
		$Ban::ipsubnet[$Ban::numBans] = $RBan::ipsubnet[%t];
		}
		else
		{
		echo("No Subnet Ban on IP: ", $RBan::ip[%t]);		
		}

		$Ban::name[$Ban::numBans] = $RBan::name[%t];
		echo("Exporting IP to Server Banlist: ", $RBan::ip[%t]);
		}
	}
	echo("Banlist Import Completed!");
	messageClient(%client,'',"\c3Bans List Imported!");
}
}

//###########################################



function serverCmdSpawnGuardBot(%client,%name)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		AiPlayer::SpawnPlayer(%client.player.getTransform(),%name,$GuardBot);
	}
}

function serverCmdBrickBuildThrough(%client)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		if($Pref::Server::BuildThrough)
		{
			$Pref::Server::BuildThrough = 0;
			messageAll('',"\c4Build through bricks turned \c3Off");
		}
		else
		{
			$Pref::Server::BuildThrough = 1;
			messageAll('',"\c4Build through bricks turned \c3On");
		}

	}
}

function servercmdGiveEditorWand(%client,%victim)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		if(%victim.isEWandUser)
		{
			if(!%victim.isAdmin && !%victim.isSuperAdmin)
			{
				%victim.isEWandUser = 0;
				messageClient(%victim,"","\c3Your editor wand priveledges were removed");
				messageClient(%client,"",'\c3%1\'s editor wand priveledges were removed',%victim.name);

				%cl.player.unMountImage($RightHandSlot);
				messageClient(%victim, 'MsgHilightInv', '', -1);
				messageClient(%victim, 'MsgDropItem', '', 6);
				%victim.player.currWeaponSlot = -1;
				%victim.player.inventory[6] = "";
			}
		}
		else
		{
			if(!%victim.isAdmin && !%victim.isSuperAdmin)
			{
				%victim.isEWandUser = 1;
				messageClient(%victim,"","\c3You were given editor wand priveledges");
				messageClient(%client,"",'\c3%1 was granted editor wand priveledges',%victim.name);
				servercmdAddtoInvent(%victim,6,$StartSpecial);
			}
		}
	}
}

function serverCmdInitCopsAndRobbers(%client)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		if($Pref::Server::GameMode $= 1)
		{
			messageClient(%client,"",'\c3Cops and Robbers cannot be initiated because a Game Mode (\c0%1\c3) is already in Progress!',$Pref::Server::GameModeType);
			return;
		}
		if($Pref::Server::Weapons $= 1)
		{
			messageClient(%client,"",'\c3Cops and Robbers cannot be initiated because a Game Mode (\c0Deathmatch\c3) is already in Progress!');
			return;			
		}
		$Pref::Server::GameMode = 1;
		$Pref::Server::GameModeType = "Cops and Robbers";

		%randPass = getRandom(100000,999999);
		for(%t = 0; %t < MissionCleanup.getCount(); %t++)
		{
			%brick = MissionCleanup.getObject(%t);
			if(%brick.isAlarmSystem $= 1)
			{
				%brick.Password = %randPass;
			}
			if(%brick.isAlarmSystemCode $= 1)
			{
				%brick.setShapeName("Bank Password: "@%randPass);
			}
		}
		messageAll("","\c5Cops and Robbers mode started!");
		for(%t = 0; %t < $Pref::Server::TotalTeams + 1; %t++)
		{
			if($Teams[%t] $= "Cops")
			{
				%copDone = 1;
				%CopteamID = %t;
			}
			if($Teams[%t] $= "Robbers")
			{
				%robDone = 1;
				%RobTeamID = %t;
			}

		}
		if(!%copDone)
		{
			serverCmdAddTeam(%client,"Cops");
			%CopTeamID = $Pref::Server::TotalTeams;
			//error(%CopTeamID);
		}
		if(!%robDone)
		{
			serverCmdAddTeam(%client,"Robbers");
			%RobTeamID = $Pref::Server::TotalTeams;
			//error(%RobTeamID);
		}
		$teamSwitch = 1;
		$TotalRobbers = 0;
		$TotalCops = 0;
		for(%t = 0; %t<ClientGroup.getCount(); %t++)
		{
			%cl = ClientGroup.getObject(%t);
			if($teamSwitch $= 0)
			{
				$TotalRobbers++;
				%cl.Team = "Robbers";
				$teamSwitch = 1;
				bottomPrint(%cl,"\"Cops And Robbers\" will be starting in 20 Seconds!!!\nYou will be a ROBBER.\nYour goal is to get to the bank and get money, then run back to your base.\nKilling a cop will free another robber from jail.",20,3);
			}
			else	
			{
				$TotalCops++;
				%cl.Team = "Cops";
				$teamSwitch = 0;
				bottomPrint(%cl,"\"Cops And Robbers\" will be starting in 20 Seconds!!!\nYou will be a COP.\nYour goal is to stop the robbers stealing money\nGet them down to less than half health then hit them with a lightsaber to imprison them. Win by imprisoning them all!",20,3);
			}
			
		}
		
		schedule(20000,0,"serverCmdStartCopsAndRobbers",%client);
	
	}
}

function serverCmdStartCopsAndRobbers(%client)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		$Pref::WeakWindowCount = 0;
		$Pref::Server::CopsAndRobbers = 1;
		$Game::RobbersEarnings = 0;
		$Pref::Server::LockTeams = 1;
		serverCmdStartDeathmatch(%client, 0, 0, 0, 0, 0, 1);
		for(%t = 0; %t<ClientGroup.getCount(); %t++)
		{
			
			%cl = ClientGroup.getObject(%t);
			%cl.isImprisoned = 0;
			%cl.money = 0;
			messageClient(%cl,'MsgUpdateMoney',"",%cl.money);
			%cl.player.kill();	

		}
	}
}
function serverCmdEndCopsAndRobbers(%client)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{	
		if($Pref::Server::GameModeType $= "Cops and Robbers")
		{
		$Pref::Server::GameMode = 0;
		$Pref::Server::GameModeType = "";
		$Pref::Server::CopsAndRobbers = 0;
		serverCmdEndDeathmatch(%client);
		for(%t = 0; %t<ClientGroup.getCount(); %t++)
		{
			%cl = ClientGroup.getObject(%t);
			%cl.team = "";
			%cl.player.kill();
		}
		for(%t = 0; %t<MissionCleanup.getCount(); %t++)
		{
			%Window = MissionCleanup.getObject(%t);
			if(%Window.oldPosition !$= "")
			{
				%Window.setTransform(%Window.oldPosition);
			}
		}
		}
		else
		{
			messageClient(%client,"","\c2A Cops and Robbers Game is not in Progress!");
		}
	}
}


function eulerToQuat( %euler )
{
   %euler = VectorScale(%euler, $pi / 180);  //convert euler rotations to radians

   %matrix = MatrixCreateFromEuler(%euler); //make a rotation matrix
   %xvec = getWord(%matrix, 3);   //get the parts of the matrix you need
   %yvec = getWord(%matrix, 4);
   %zvec = getWord(%matrix, 5);
   %ang  = getWord(%matrix, 6);   //this is in radians
   //%ang = %ang * 180 / $pi;   //convert back to degrees

   %rotationMatrix = %xvec @ " " @ %yvec @ " " @ %zvec @ " " @ %ang;  

   return %rotationMatrix;   //send it back
}



function serverCmdToggleScriptBlock(%client)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		if($Pref::Server::BlockScripts == 0)
		{
			$Pref::Server::BlockScripts = 1;
			messageAll('',"\c3Scripting Disabled");
		}
		else
		{
			$Pref::Server::BlockScripts = 0;
			messageAll('',"\c3Scripting Enabled");
		}
	}
}

function serverCmdToggleItemCost(%client)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		if($Pref::Server::ItemsCostMoney == 0)
		{
			for(%t = 0; %t < MissionGroup.getCount(); %t++)
			{
				%obj = MissionGroup.GetObject(%t);
				//echo(%obj.Datablock.Cost);
				if(%obj.Datablock.Cost > 0)
				{
					%obj.setShapeName("$"@%obj.Datablock.Cost);
				}
			}
			$Pref::Server::ItemsCostMoney = 1;
			messageAll("","\c3Items now cost money!");
		}
		else
		{
			for(%t = 0; %t < MissionGroup.getCount(); %t++)
			{
				%obj = MissionGroup.GetObject(%t);
				%obj.setShapeName("");
			}
			$Pref::Server::ItemsCostMoney = 0;
			messageAll("","\c3Items no longer cost money!");
		}	

	}
}

function serverCmdGiveMoney(%client, %victim, %amount)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		%victim.Money += %amount;
		messageClient(%victim,'MsgUpdateMoney','\c3You were given $%2 by %3',%victim.Money,%amount, %client.name);
		messageClient(%client,'','\c3You gave $%1 to %2',%amount, %victim.name);
	
	}
}

function serverCmdCloseServer(%client)
{
	if(%client.isSuperAdmin)
	{
		for(%t = 0; %t < ClientGroup.getCount(); %t++)
		{
			%cl = ClientGroup.GetObject(%t);
			%cl.Delete("Server has closed. Thanks for playing!");
		}
		Disconnect();
	}
}

//Work in Progress...
function servercmdStartSuperManMode(%client, %breakBricks, %friendlyFire, %brickrespawn, %brickHits, %jetPack, %clearInv)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		serverCmdStartDeathmatch(%client,%breakBricks,%friendlyFire,%brickrespawn,%brickHits,%jetPack,%clearInv);
		%totalClients = ClientGroup.getCount();
		%t = getRandom(%totalClients,1);
		%selectedClient = ClientGroup.getObject(%t-1);
		%selectedClient.isSuperMan = 1;
		messageClient(%selectedClient,"","\c5You are superman!");
		%selectedClient.player.setScale("1.5 1.5 1.5");
	}
}
//Work in Progress...

function serverCmdLockTeams(%client)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		if($Pref::Server::LockTeams == 0)
		{
			$Pref::Server::LockTeams = 1;
			messageAll("","\c3Teams Locked");
		}
		else
		{
			$Pref::Server::LockTeams = 0;
			messageAll("","\c3Teams Unlocked");
		}
	}
}

function serverCmdRemTeam(%client,%victim)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		if(%victim.team !$= "")
		{
			%victim.Team = "";
			messageClient(%victim,"",'\c3You were removed from \c0%1',%victim.Team);
			messageAll("",'\c0%1\c3 was removed from \c0%2',%victim.name,%victim.Team);
			setThePlayerName(%victim);
		}
	}
}

function serverCmdSetTeam(%client,%victim,%teamID)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		%victim.Team = $Teams[%TeamID];
		messageClient(%victim,"",'\c3You were assigned to \c0%1',$Teams[%TeamID]);
		messageAll("",'\c0%1\c3 was put into \c0%2',%victim.name,$Teams[%TeamID]);
		setThePlayerName(%victim);
	}
}

function serverCmdAddTeam(%client,%TeamName)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		if(%TeamName $= "")
		{
			messageClient(%client,"","\c3You must enter a team name...");
			return;
		}
		for(%t = 0; %t < $Pref::Server::TotalTeams; %t++)
		{
			if(%TeamName $= $Teams[%t])
			{
				messageClient(%client,"","\c3There is already a team with that name");
				return;
			}
		}
		
		messageAll('MsgTeamAdd','\c0%1 \c5was added to the team list',%TeamName,$Pref::Server::TotalTeams);
		$Teams[$Pref::Server::TotalTeams] = %TeamName;
		$Pref::Server::TotalTeams++;
	}
}
function serverCmdRemoveTeam(%client,%TeamID)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		%team = $Teams[%TeamID];
		if(%team $= "")
		{
			return;
		}
		messageAll('MsgTeamRemove','\c0%2 \c3was disbanded...',%teamID,%team);
		for(%t = 0; %t < ClientGroup.getCount(); %t++)
		{
			%cl = ClientGroup.GetObject(%t);
			if(%cl.team $= $Teams[%teamID])
			{
				%cl.team = "";
				messageClient(%cl,"","\c3You are no longer in a team"); 
				setThePlayerName(%cl);
			}
			
		}
		$Teams[%teamID] = "";
		$Pref::Server::TotalTeams--;
	}
}


function serverCmdStartDeathmatch(%client, %breakBricks, %friendlyFire, %brickrespawn, %brickHits, %jetPack, %clearInv, %bombs)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{	
		if($Pref::Server::GameModeType $= "Best Builders Comp.")
		{
			messageClient(%client,"",'\c3A Deathmatch cannot be initiated because a Game Mode (\c0%1\c3) is already in Progress!',$Pref::Server::GameModeType);
			return;
		}
		if($Pref::Server::Weapons == 0)
		{
			$Pref::Server::Weapons = 1;
			$Pref::Server::DMBreakBricks = %breakBricks;
			$Pref::Server::DMMaxBrickHits = %brickHits;
			$Pref::Server::DMFriendlyFire = %friendlyFire;
			$Pref::Server::DMBrickReSpawnTime = %brickrespawn;
			$Pref::Server::DMJetPack = %jetPack;
			$Pref::Server::DMClearInventory = %clearInv;
			$Pref::Server::BombRigging = %bombs;

			if($Pref::Server::DMClearInventory == 1)	
			{
				$Pref::Server::UseInventory = 0;
				for(%i= 0; %i < ClientGroup.getCount(); %i++)
				{
					%cl = ClientGroup.getObject(%i);
					for(%t = 0; %t < 10; %t++)
					{
												%cl.player.unMountImage($RightHandSlot);
						messageClient(%cl, 'MsgHilightInv', '', -1);
						messageClient(%cl, 'MsgDropItem', '', %t);
						%cl.player.currWeaponSlot = -1;
						%cl.player.inventory[%t] = "";
					}
				}
			}
			else
			{
				for(%i= 0; %i < ClientGroup.getCount(); %i++)
				{
					%cl = ClientGroup.getObject(%i);
					%cl.WrenchMode = 3;
					messageClient(%cl,"","\c4Auto-set your wrench mode to \'Repair\'"); 	
				}
			}
			if($Pref::Server::DMJetPack == 0)	
			{
				$Pref::Server::MaxEnergy = 0;
				exec("./player.cs");
			}
			messageAll("","\c5Deathmatch Mode ON!");
			
			for(%t = 0; %t < MissionGroup.getCount(); %t++)
			{
				%obj = MissionGroup.getObject(%t);
				if(%obj.getClassname() $= "Item")
				{
					%objdata = %obj.getdatablock();
					if(%objdata.classname $= "Weapon") 
					{
						%obj.hide(false);
						%obj.schedule(100, "startFade", 1000, 0, false);
					}
				}
			}
		}
		else
		{
			messageClient(%client,"","\c3Deathmatch already in progress.");
		}
		
	}
}

function serverCmdEndDeathmatch(%client)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		if($Pref::Server::Weapons == 1)
		{
			$Pref::Server::Weapons = 0;
			messageAll("","\c5Deathmatch Mode OFF!");
			for(%t = 0; %t < MissionGroup.getCount(); %t++)
			{
				%obj = MissionGroup.getObject(%t);
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
			$Pref::Server::BombRigging = 0;
			if($Pref::Server::DMJetPack == 0)
			{
				$Pref::Server::MaxEnergy = 100;
				exec("./player.cs");
			}
		
			if($Pref::Server::DMClearInventory == 1)	
			{
				$Pref::Server::UseInventory = 1;
				for(%i= 0; %i < ClientGroup.getCount(); %i++)
				{
					%cl = ClientGroup.getObject(%i);
					for(%t = 0; %t < 10; %t++)
					{
						%cl.player.unMountImage($RightHandSlot);
						messageClient(%cl, 'MsgHilightInv', '', -1);
						messageClient(%cl, 'MsgDropItem', '', %t);
						%cl.player.currWeaponSlot = -1;
						%cl.player.inventory[%t] = "";
					}
					serverCmdAddtoInvent(%cl,1,$StartPlates);
   					serverCmdAddtoInvent(%cl,2,$StartSlopes);
  					serverCmdAddtoInvent(%cl,3,$StartMisc);
   					serverCmdAddtoInvent(%cl,4,$StartTools);
   					serverCmdAddtoInvent(%cl,5,$StartSprayCans);
   					serverCmdAddtoInvent(%cl,6,$StartSpecial);
   					serverCmdAddtoInvent(%cl,0,$StartBricks);
				}
			}
			else
			{
				for(%i= 0; %i < ClientGroup.getCount(); %i++)
				{
					%cl = ClientGroup.getObject(%i);
					for(%t = 0; %t < 10; %t++)
					{
						%cl.player.unMountImage($RightHandSlot);
						messageClient(%cl, 'MsgHilightInv', '', -1);
						messageClient(%cl, 'MsgDropItem', '', %t);
						%cl.player.currWeaponSlot = -1;
						%cl.player.inventory[%t] = "";
					}
					serverCmdAddtoInvent(%cl,1,$StartPlates);
   					serverCmdAddtoInvent(%cl,2,$StartSlopes);
  					serverCmdAddtoInvent(%cl,3,$StartMisc);
   					serverCmdAddtoInvent(%cl,4,$StartTools);
   					serverCmdAddtoInvent(%cl,5,$StartSprayCans);
   					serverCmdAddtoInvent(%cl,6,$StartSpecial);
   					serverCmdAddtoInvent(%cl,0,$StartBricks);
				}
			}
			for (%i = 0; %i < MissionCleanup.getCount(); %i++)
			{
				%brick = MissionCleanup.getObject(%i);
				if(%brick.hits>0)
				{
					%brick.hits = 0;
					%brick.setSkinName(%col.SkinName);
				}
			}		

		}
		else
		{
			messageClient(%client,"","\c3There is no deathmatch in Progress!");
		}



	}
} 

function serverCmdSaveBricks(%client,%file)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		SavePersistence(%file);
	}
}
function serverCmdLoadBricks(%client,%file)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		LoadPersistence(%client,%file);
	}
}

function serverCmdMakeTempAdmin(%client,%victim)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		if(!%victim.isTempAdmin)
		{
			%victim.isTempAdmin = 1;
			messageAll("",'\c0%1 \c3has been made a temporary admin',%victim.name);
		}
		else
		{
			%victim.isTempAdmin = 0;
			messageAll("",'\c0%1 \c3is no longer a temporary admin',%victim.name);
		}
	}
}

function serverCmdAdminToPlayer(%client,%victim)
{
	if(%client.isAdmin || %client.isSuperAdmin || %client.isTempAdmin)
	{
		serverCmdTeleportToObj(%client,%victim.player,500);
	}
}

function serverCmdPlayerToAdmin(%client,%victim)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		if(isObject(%client.Player))
		{
			serverCmdTeleportToObj(%victim,%client.Player,500);
		}
	}
}
function serverCmdTeleportToObj(%client,%obj,%waitTime)
{
	if(isObject(%client.player))
	{
		%trans = %obj.getWorldBoxCenter();
		%x = getWord(%trans,0);
		%y = getWord(%trans,1);
		%z = getWord(%trans,2)+1;
		%trans = %x SPC %y SPC %z;
		if(%client.isSkele !$= 1 && %client.isDroid !$= 1)
		   %client.player.setCloaked(true);
		else
		   %client.player.startFade(0, 0, 1000);
		schedule(%waitTime,0,"Teleport",%client.player,%trans);
	}

}

function Teleport(%obj,%trans)
{
	if(isObject(%obj))
	{
		%obj.setTransform(%trans);
		if(%obj.client.isSkele !$= 1 && %obj.client.isDroid !$= 1)
		   %obj.setCloaked(false);
		else
		   %client.player.startFade(1000);
		%obj.client.NoTeleport = 1;
		%client = %obj.client;
		schedule(3000,0,"TeleportReset",%client);
	}
}

function TeleportReset(%client)
{
	%client.NoTeleport = 0;
}

function serverCmdToggleFallDamage(%client)
{
	if(%client.isAdmin || %client.isSuperAdmin || %client.isTempAdmin)
	{
		if($Pref::Server::FallDamage == 0)
		{
			$Pref::Server::FallDamage = 1;
			messageAll("","\c3Fall Damage is now \c0ON.");
		}
		else
		{
			$Pref::Server::FallDamage = 0;
			messageAll("","\c3Fall Damage is now \c0OFF.");
		}
	}
}

function serverCmdChangeMap(%client)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		cyclegame();
	}
}

function serverCmdJetPackLag(%client,%lag)
{
	if(%client.isAdmin || %client.isSuperAdmin || %client.isTempAdmin)
	{
		%old = $Pref::Server::JetLag;
		if(%lag == 0)
		{
			$Pref::Server::MaxEnergy = 0;
			$Pref::Server::JetLag = 80;

			for(%clientNum = 0; %clientNum < ClientGroup.getCount(); %clientNum++)
			{
				ClientGroup.getObject(%clientNum).player.setRechargeRate(0);
			}

			messageAll("","\c3Jetpacks are turned off");
		}
		else
		{
			$Pref::Server::JetLag = %lag;
			$Pref::Server::MaxEnergy = 60;
			messageAll("",'\c3Jet Pack Lag changed from %1 to %2',%old,$Pref::Server::JetLag);
			messageAll("","\c3Please suicide(\c0ctrl+k\c3) to have changes take place.");
		}
		exec("./player.cs");
		
	}
}


function serverCmdClearAllBricks(%client)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		messageAll("",'\c0%1 \c3cleared all the Bricks.', %client.name);
		for (%i = 0; %i < MissionCleanup.getCount(); %i++)
		{
			%brick = MissionCleanup.getObject(%i);
			schedule(%i*10,0,killBrick,%brick);

		}

		for (%i = 0; %i < ClientGroup.getCount(); %i++)
		{
			%cl = ClientGroup.getObject(%i);
			%cl.TotalMovers = 0;
		}

	}


}

function serverCmdColourAllBricks(%client)
{
	if(%client.isAdmin || %client.isSuperAdmin || %client.isTempAdmin)
	{
		for (%i = 0; %i < MissionCleanup.getCount(); %i++)
		{
			%brick = MissionCleanup.getObject(%i);
			%brick.setSkinName($legoColor[%client.colorIndex]);

		}
	}

}

function serverCmdToggleFloatingBricks(%client)
{
	if(%client.isAdmin || %client.isSuperAdmin || %client.isTempAdmin )
	{
		if($Pref::Server::FloatingBricks == 0)
		{
			$Pref::Server::FloatingBricks = 1;
			messageAll("","\c3Floating bricks \c0enabled");
		}
		else
		{
			$Pref::Server::FloatingBricks = 0;
			messageAll("","\c3Floating bricks \c0disabled");
		}
	}

}

function serverCmdDeleteLastAddedMapItem(%client)
{
	if((%client.isAdmin && $Pref::Server::AdminEditor ) || %client.isSuperAdmin)
	{
		
		$TotalAddedMapItems--;
		if(isObject($AddedMapItems[$TotalAddedMapItems]))
		{
			if($AddedMapItems[$TotalAddedMapItems].Datablock $= "gray32")
			{
				%item = "Baseplate";
			}
			else
			{
				%item = $AddedMapItems[$TotalAddedMapItems].Datablock;
			}
			messageClient(%client,"",'\c3Deleted a %1', %item);
			$AddedMapItems[$TotalAddedMapItems].delete();
		}
		$AddedMapItems[$TotalAddedMapItems] = 0;
		if($TotalAddedMapItems < 0)
			$TotalAddedMapItems = 0;
	}
}
function serverCmdDeleteAllAddedMapItems(%client)
{
	if((%client.isAdmin && $Pref::Server::AdminEditor ) || %client.isSuperAdmin)
	{
		for(%t = 0; %t<$TotalAddedMapItems; %t++)
		{
			if(isObject($AddedMapItems[%t]))
			{
				$AddedMapItems[%t].delete();
			}
			$AddedMapItems[%t] = 0;
		}
		$TotalAddedMapItems = 0;
		messageClient(%client,'',"\c3Deleted All Added Map Items");
	}
}

function serverCmdPlaceBase(%client)
{
	if((%client.isAdmin && $Pref::Server::AdminEditor ) || %client.isSuperAdmin)
	{
		%trans = %client.player.gettransform();
		%x = getword(%trans,0)-8;
		%y = getword(%trans,1)-8;
		%z = getword(%trans,2) - 0.1;
		messageClient(%client,"","\c3Added a baseplate");
		$AddedMapItems[$TotalAddedMapItems] = new StaticShape() 
							{
      								position = %x SPC %y SPC %z;
      								rotation = "1 0 0 0";
      								scale = "1 1 1";
      								dataBlock = "gray32";
 				   			};
		MissionGroup.add($AddedMapItems[$TotalAddedMapItems]);
		$TotalAddedMapItems++;
	}
}

function serverCmdPlaceItem(%client,%item)
{

	if((%client.isAdmin && $Pref::Server::AdminEditor ) || %client.isSuperAdmin)
	{
		%trans = %client.player.gettransform();
		%x = getword(%trans,0);
		%y = getword(%trans,1);
		%z = getword(%trans,2) + 1;


      		$AddedMapItems[$TotalAddedMapItems] = new Item() {
      			//position = %x SPC %y SPC %z;
      position = vectoradd(vectorscale(%client.player.getForwardVector(),"2 2 2"), %x SPC %y SPC %z);
      			rotation = "1 0 0 0";
      			scale = "1 1 1";
      			dataBlock = %item;
      			collideable = "0";
      			static = "1";
      			rotate = "0";
      		};

					if($Pref::Server::Weapons $= 0)
					{
					%objdata = $AddedMapItems[$TotalAddedMapItems].getdatablock();
					if(%objdata.classname $= "Weapon") 
					{
						$AddedMapItems[$TotalAddedMapItems].startFade(0, 0, true);
						$AddedMapItems[$TotalAddedMapItems].hide(true);
			messageClient(%client,"",'\c3Because we are not in Deathmatch Mode, your Weapon: \c3%1\c2 has been Hidden.',$AddedMapItems[$TotalAddedMapItems].Datablock);
					}
					}
					else
					{
								messageClient(%client,"",'\c3Added a %1',$AddedMapItems[$TotalAddedMapItems].Datablock);
					}
		if(isObject($AddedMapItems[$TotalAddedMapItems]))
		{
			if($Pref::Server::ItemsCostMoney == 1)
			{	
				if($AddedMapItems[$TotalAddedMapItems].Datablock.Cost > 0)
				{
					$AddedMapItems[$TotalAddedMapItems].setShapeName("$"@$AddedMapItems[$TotalAddedMapItems].Datablock.Cost);
				}

			}
			MissionGroup.add($AddedMapItems[$TotalAddedMapItems]);
			$TotalAddedMapItems++;
		}
	}
}
		

function serverCmdToggleInventory(%client)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		if($Pref::Server::UseInventory == 1)
		{
			messageAll("","\c3Turned \c0OFF \c3Inventory System"); 
			$Pref::Server::UseInventory = 0;
			for(%i= 0; %i < ClientGroup.getCount(); %i++)
			{
				%cl = ClientGroup.getObject(%i);
				for(%t = 0; %t < 10; %t++)
				{
					%cl.player.unMountImage($RightHandSlot);
					messageClient(%cl, 'MsgHilightInv', '', -1);
					messageClient(%cl, 'MsgDropItem', '', %t);

					%cl.player.currWeaponSlot = -1;
					%cl.player.inventory[%t] = "";
				}
			}
		}
		else
		{
			messageAll("","\c3Turned \c0ON \c3Inventory System"); 
			$Pref::Server::UseInventory = 1;

			for(%i= 0; %i < ClientGroup.getCount(); %i++)
			{
				%cl = ClientGroup.getObject(%i);
					serverCmdAddtoInvent(%cl,1,$StartPlates);
   					serverCmdAddtoInvent(%cl,2,$StartSlopes);
  					serverCmdAddtoInvent(%cl,3,$StartMisc);
   					serverCmdAddtoInvent(%cl,4,$StartTools);
   					serverCmdAddtoInvent(%cl,5,$StartSprayCans);
   					serverCmdAddtoInvent(%cl,0,$StartBricks);
			}

		}
	}
}

function serverCmdInventSearchAdd(%client,%item)
{
	if(%client.isSuperAdmin)
	{

		%player = %client.player;
		%data = %player.getDataBlock();
		%freeslot = -1;
		for(%i = 0; %i < %data.maxItems; %i++)
		{
			//echo(%i);
			if(%player.inventory[%i] == 0)
			{
				%freeslot = %i;
				break;
			}
		}
		if(%freeslot > -1)
		{	
			for(%t = 0; %t < $MaxInvent; %t++)
			{	
				if($Inv[%t,1] $= %item)
				{
					%player.inventory[%position] = $Inv[%t,0];
					%inv = $Inv[%t,1];
					messageClient(%client, 'MsgItemPickup', '', %freeslot, %inv);
					break;
				}		
			}
		}
	}
}

function serverCmdKick(%client, %victim)
{
	if(%client.isAdmin || %client.isSuperAdmin || %client.isTempAdmin)
	{
		//kill the victim client
		if (!%victim.isAIControlled())
		{
			%ip = getRawIP(%victim);
			if(%ip !$= "local")
			{
				//tell everyone about it
				messageAll( 'MsgAdminForce', '\c3The Admin has kicked \c0%1\c3(%2).', %victim.name, getRawIP(%victim));
				//kick them
				%victim.delete("You have been kicked.");
			}
			else
			{
				messageClient(%client, 'MsgAdminForce', '\c3You can\'t kick the local client.');
				return;
			}
		}
		else
		{
			//always kick bots
			%victim.delete("You have been kicked.");
		}
	}
}
function serverCmdBan(%client, %victim, %Subnet)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		//add player to ban list
		if (!%victim.isAIControlled())
		{
			//this isnt a bot so add their ip to the banlist
			%ip = getRawIP(%victim);
			if(%ip !$= "local")
			{
				//tell everyone about it
				messageAll( 'MsgAdminForce', '\c3%3 has banned \c0%1\c3(%2).', %victim.name, getRawIP(%victim), %client.name);

				$Ban::numBans++;
				$Ban::ip[$Ban::numBans] = %ip;
				if(%Subnet $= 1)
				{
				$Ban::ipsubnet[$Ban::numBans] = getIPMask(%ip);
				}
				$Ban::name[$Ban::numBans] = %victim.namebase;
			}
			else
			{
				messageClient(%client, 'MsgAdminForce', '\c3You can\'t ban the local client.');
				return;
			}
		}
		//kill the victim client
		%victim.delete("You have been banned.");
	}
}

function serverCmdDisableBuildingRights(%client,%victim)
{
	if(%client.isAdmin || %client.isSuperAdmin || %client.isTempAdmin)
	{
		if(%victim.HBR $= 1)
		{
		%victim.HBR = 0;
		messageClient(%client, '', '\c3You have removed %1\'s building rights.', %victim.namebase);
		}
		else
		{
		%victim.HBR = 1;
		messageClient(%client, '', '\c3You have given %1 building rights.', %victim.namebase);
		}	
	}
}

function serverCmdGetIP(%client,%victim)
{
if(%client.isAdmin || %client.isSuperAdmin)
{
%ip = getRawIP(%victim);
messageClient(%client,"",'\c3%1\'s IP: \c0%2', %victim.namebase, %ip);
}
}

function serverCmdMagicWand(%client)
{
	if(%client.isAdmin || %client.isSuperAdmin || %client.isTempAdmin)
	{
		%player = %client.player;
		%player.mountImage(wandImage, 0);
	}
}

