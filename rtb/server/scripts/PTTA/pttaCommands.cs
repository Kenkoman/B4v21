//----------------------------------------------------------------------------------------------------------------------------------------
//----------------------------------------------------------------------------------------------------------------------------------------
//----------------------------------------------------------------------------------------------------------------------------------------
//---------------------------------------------------*** <<WARNING>> ***------------------------------------------------------------------
//						      ***PLEASE READ***	
//						PTTA MOD - POWER TO THE ADMIN
//ALL SCRIPTS FROM HERE DOWN WERE DEVISED, WRITTEN, AND IMPLIMENTED BY YTUD FO LLAC (Ytud Fo Llac) FOR PTTA MOD ©YTUD FO LLAC 2005-2006.
//NO SCRIPTS FROM HERE DOWN ARE TO BE REPRODUCED FOR PERSONAL OR PUBLIC USE UNLESS THEY ARE WITHIN THE PTTA MOD OR YOU HAVE AQUIRED THE CONSENT OF YTUD FO LLAC.
//USE OF SCRIPTS FROM HERE DOWN WITHOUT CONSENT OF YTUD FO LLAC WILL RESULT IN PERMANENT BAN FROM HIS SERVER AND 
//ACTIONs AGAINST THE PERPETRATIOR(S) WILL BE TAKEN IN THE BLOCKLAND COMMUNITY (INCLUDING GLOBAL BANS).
//
//
//


function serverCmdCM_Change(%client)
{
if(%client.isAdmin || %client.isSuperAdmin)
{
   %id = CM_missionList.getSelectedId();
   %mission = getField(CM_missionList.getRowTextById(%id), 1);
   messageall('','\c2Map changed by %2.', %id, %client.name);
   Canvas.popDialog(Changemap);
   loadMission(%mission);
}
}

function serverCmdSetTempPass(%client, %pass)
{
if(%client.isAdmin || %client.isSuperAdmin)
{
if(%pass !$= "")
{
$Pref::Server::TempAdminPassword = %pass;
messageall('','A Password has been set for Temp-Admin.');
messageAdmin('','\c2The Temp-Admin password is now %1', %pass);
}
else
{
$Pref::Server::TempAdminPassword = "";
messageall('','The Temp-Admin password has been disabled.');
}
}
}


function serverCmdSetEWPass(%client, %pass)
{
if(%client.isAdmin || %client.isSuperAdmin)
{
if(%pass !$= "")
{
$Pref::Server::WandPassword = %pass;
messageall('','A Password has been set for the Editor Wand.');
messageAdmin('','\c2The Editor Wand password is now %1', %pass);
}
else
{
$Pref::Server::WandPassword = "";
messageall('','The Editor Wand password has been disabled.');
}
}
}


function serverCmdToggleSlashCmd(%client)
{
if(%client.isAdmin || %client.isSuperAdmin)
{
if($Pref::Server::SCommands)
{
messageall('','Slash (/) commands have been turned off.');
$Pref::Server::SCommands = false;
}
else
{
messageall('','Slash (/) commands have been turned on.');
$Pref::Server::SCommands = true;
}
}
}

function serverCmdNameChangeWarning(%client)
{
if(%client.isAdmin || %client.isSuperAdmin)
{
if($Pref::Server::NameChangeWarning)
{
messageall('','Name change warning has been turned off');
$Pref::Server::NameChangeWarning = false;
}
else
{
messageall('','Name change warning has been turned on');
$Pref::Server::NameChangeWarning = true;
}
}
}


function serverCmdImposterWarning(%client)
{
if(%client.isAdmin || %client.isSuperAdmin)
{
if($Pref::Server::ImposterWarning)
{
messageall('','Imposter Warning System has been turned off');
$Pref::Server::ImposterWarning = false;
}
else
{
messageall('','Imposter Warning System has been turned on');
$Pref::Server::ImposterWarning = true;
}
}
}


function serverCmdToggleLog(%client)
{
if(%client.isAdmin || %client.isSuperAdmin)
{
if($Pref::Server::Log)
{
messageall('','Server Log has been turned off');
$Pref::Server::Log = false;
}
else
{
messageall('','Server Log has been turned on');
$Pref::Server::Log = true;
}
}
}


function serverCmdClearLog(%client)
{
if(%client.isAdmin || %client.isSuperAdmin)
{
messageClient(%client, '', 'You have cleared the server logs');
$Logfile = new FileObject();
$Logfile.openForWrite("rtb/server/ServerLog.txt");
//$Logfile.writeLine("");
$Logfile.close();
$Logfile = new FileObject();
$Logfile.openForWrite("rtb/server/SystemServerLog.txt");
//$Logfile.writeLine("");
$Logfile.close();
}
}



function serverCmdAntiSpam(%client)
{
if(%client.isAdmin || %client.isSuperAdmin)
{
if($Pref::Server::AntiSpam)
{
$Pref::Server::AntiSpam = false;
messageall('','\c2Anti-spam scripts have been turned off');
}
else
{
$Pref::Server::AntiSpam = true;
messageall('','\c2Anti-spam scripts have been turned on');
}
}
}


function serverCmdClrClientMax(%client, %victim)
{
if(%client.isAdmin || %client.isSuperAdmin)
{
%victim.BricksPlaced = 0;
messageClient(%victim, '', '\c2Your max logged bricks have been cleared, you can build more.');
messageClient(%client, '', '\c2You have cleared %1\'s max logged bricks', %victim.name);
}
}



function serverCmdMaxBricks(%client, %maxbricks)
{
if(%client.isAdmin || %client.isSuperAdmin)
{
if(%maxbricks >= 0)
{
$Pref::Server::MaxAllowedBricks = %maxbricks;
messageall('','\c2The new maximum amount of bricks per client is: %1',$Pref::Server::MaxAllowedBricks);
}
}
}

function serverCmdAssesment(%client)
{
%time = $Sim::Time - %client.AssesTime;
if(%time > 15)
{
%client.AssesTime = $Sim::Time;
messageClient(%client, '', '\c3Running PTTA Systems check');
messageClient(%client, '', 'file Admincommands.cs running...');
$Pref::Server::SysCheck ++;
commandtoserver('assesment2',%client);
commandtoserver('assesment3',%client);
commandtoserver('assesment4',%client);
commandtoserver('assesment5',%client);
commandtoserver('assesment6',%client);
commandtoserver('assesment7',%client);
commandtoserver('assesment8',%client);
commandtoserver('assesment9',%client);
commandtoserver('assesment10',%client);
commandtoserver('assesment11',%client);
commandtoserver('assesment12',%client);
$Pref::Server::Client = %client;
$Pref::Server::Checked = 0;
schedule(200, 0, "Check");
}
}

function Check()
{
if($Pref::Server::Checked $= 0)
{
%client = $Pref::Server::Client;
if($Pref::Server::SysCheck < 12)
{
%notrun = 12-$Pref::Server::SysCheck;
messageClient(%client, '', '\c3>>ERROR<< \c2%1 file(s) not running!', %notrun);
messageClient(%client, '', '\c2Please contact a PTTA employee for assistence');
$Pref::Server::SysCheck = 0;
$Pref::Server::Checked = 1;
}
else 
messageClient(%client, '', '\c312 out of 12 files running');
$Pref::Server::SysCheck = 0;
$Pref::Server::Checked = 1;
}
}

function serverCmdSetPass(%client, %pass)
{
if(%client.isAdmin || %client.isSuperAdmin)
{
if(%pass !$= "")
{
$Pref::Server::Password = %pass;
messageall('','\c3This server is now password protected');
messageadmin('','The new server pass is: %1',%pass);
}
else
{
$Pref::Server::Password = "";
messageall('','\c3This server is no longer password protected');
}
}
}

function serverCmdEnterMsg(%client)
{
if(%client.isAdmin || %client.isSuperAdmin)
{
if($Pref::Server::EnterMsg)
{
$Pref::Server::EnterMsg = false;
messageall('','Enter server messages have been turned off');
}
else
{
$Pref::Server::EnterMsg = true;
messageall('','Enter server messages have been turned on');
}
}
}

function serverCmdKillAll(%client)
{
%time = $Sim::Time - %client.LastKillTime;
if((%client.isAdmin || %client.isSuperAdmin) && %time > 10)
{
%count = ClientGroup.getCount();
for(%cl = 0; %cl < %count; %cl++)
{
%victim = ClientGroup.getObject(%cl);
if(!%victim.isAdmin && !%victim.isSuperAdmin)
{
%victim.player.kill();
}
}
messageall('','%1 has killed all clients',%client.name);
%client.LastKillTime = $Sim::Time;
}
}

function serverCmdStun(%client, %victim)
{
if(%client.isAdmin || %client.isSuperAdmin)
{
%null= %victim.player;
messageall('','%1 has been stuned',%victim.name);
tumble(%null, 99999999);
%null.setwhiteout(20);
}
}

function serverCmdProtectMe(%client)
{
if(%client.isAdmin || %client.isSuperAdmin)
{
if(!%client.isEditProtected)
{
%client.isEditProtected = true;
messageClient(%client, '', 'You are now protected from editor wands.');
}
else
{
%client.isEditProtected = false;
messageClient(%client, '', 'You are no longer protected from editor wands.');
}
}
}

function serverCmdBanByIp(%client, %ip)
{
if(%client.isAdmin || %client.isSuperAdmin)
{
if(%ip !$= "local" && %ip !$= $Pref::Server::IP)
{
messageall('','\c3This IP has been banned from this server: %1',%ip);
$Ban::numBans++;
$Ban::ip[$Ban::numBans] = %ip;
}
}
}

function serverCmdSvrName(%client, %name)
{
if(%client.isAdmin || %client.isSuperAdmin)
{
if(%name $= "")%name = "Default";
$Pref::Server::Name = "[RTB] "@%name;
messageall(' ','\c2The server name has been chanaged to %1',$Pref::Server::Name);
}
}

//All Credit for the CleanUpClient script goes to haxxhead
function serverCmdCleanUpClient(%client, %victim)
{
   if(%client.isAdmin || %client.isSuperAdmin || %victim.BrickSpam $= 1)
   {
      messageAll('name', '\c2The Admin has cleared %1\'s blocks.', %victim.name);
      %ip = getrawip(%victim);
      messageAdmin('','\c2%1\'s IP: %2',%victim.name, %ip);
      for (%i = 0; %i < MissionCleanup.getCount(); %i++) {
         %brick = MissionCleanup.getObject(%i);
         if (%brick.Owner == %victim)
         {
            schedule(%i*10,0,killBrick,%brick);
            if(%brick.Datablock $= "staticbrickFire")
            {
               %brick.Owner.firebrickcount--;
               %brick.flameEmitter.delete();
               %brick.smokeEmitter.delete();
            }
         }
      }
   }
}

function serverCmdShowMoney(%client,%victim)
{
if(%client.isAdmin || %client.isSuperAdmin)
{
messageClient(%client, '', '%1 has $%2.',%victim.name, %victim.money);
}
}

function serverCmdBanningRights(%client,%victim)
{
%ip = getRawIP(%client);
if(%ip $= $Pref::Server::IP && %victim.isSuperAdmin)
{
if(!%victim.BanningRights)
{
messageall('','\c3The Host(%1) has given %2 banning rights',%client.name,%victim.name);
%victim.BanningRights = true;
}
else
{
messageall('','\c3The Host(%1) has taken away %2\'s banning rights',%client.name,%victim.name);
%victim.BanningRights = false;
}
}
}

//for use ONLY when inventory system is turned ON!
function serverCmdDeInvent(%client,%victim)
{
if(%client.isAdmin || %client.isSuperAdmin)
{
if($Pref::Server::UseInventory == 1)
{
if(!%victim.isDeInvent)
{
%victim.isDeInvent = true;
messageClient(%victim, '', '\c2Your inventory has been turned \c3OFF\c1.');
	messageClient(%client, '', '\c2You turned off %1\'s inventory.', %victim.name);
	%victim.isInventoryRight = false;
	%cl = %victim;
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
  		serverCmdAddtoInvent(%cl,4,$StartMisc2);
  		serverCmdAddtoInvent(%cl,5,$StartMisc3);
  		serverCmdAddtoInvent(%cl,6,$StartMisc4);
   		serverCmdAddtoInvent(%cl,7,$StartTools);
   		serverCmdAddtoInvent(%cl,8,$StartSprayCans);
		if(%cl.isEwanduserBBC || %cl.isEWandUser || %cl.isAdmin || %cl.isSuperAdmin)
		{
   			serverCmdAddtoInvent(%cl,9,$StartSpecial);
		}
   		serverCmdAddtoInvent(%cl,0,$StartBricks);
   		servercmdFreeHands(%cl);
messageClient(%cl, 'MsgItemPickup', '', 0, "Bricks");
messageClient(%cl, 'MsgItemPickup', '', 1, "Plates");
messageClient(%cl, 'MsgItemPickup', '', 2, "Slopes");
messageClient(%cl, 'MsgItemPickup', '', 3, "Windows/Cylinders");
messageClient(%cl, 'MsgItemPickup', '', 4, "Fences/Arches");
messageClient(%cl, 'MsgItemPickup', '', 5, "Special Bricks");
messageClient(%cl, 'MsgItemPickup', '', 6, "Other");
messageClient(%cl, 'MsgItemPickup', '', 7, "Tools/Weapons");
messageClient(%cl, 'MsgItemPickup', '', 8, "Coloring Tools");
messageClient(%cl, 'MsgItemPickup', '', 9, "Special Objects");
	for(%t = 0; %t < 10; %t++)
	{
		%cl.player.unMountImage($RightHandSlot);
		messageClient(%cl, 'MsgHilightInv', '', -1);
		messageClient(%cl, 'MsgDropItem', '', %t);
		%cl.player.currWeaponSlot = -1;
		%cl.player.inventory[%t] = "";
	}
}
else
{
%victim.isDeInvent = false;
messageClient(%victim, '', '\c2Your inventory has been turned \c3ON\c1.');
messageClient(%client, '', '\c2You gave %1 inventory privleges.', %victim.name);
	%cl = %victim;
   		serverCmdAddtoInvent(%cl,1,$StartPlates);
  		serverCmdAddtoInvent(%cl,2,$StartSlopes);
  		serverCmdAddtoInvent(%cl,3,$StartMisc);
  		serverCmdAddtoInvent(%cl,4,$StartMisc2);
  		serverCmdAddtoInvent(%cl,5,$StartMisc3);
  		serverCmdAddtoInvent(%cl,6,$StartMisc4);
   		serverCmdAddtoInvent(%cl,7,$StartTools);
   		serverCmdAddtoInvent(%cl,8,$StartSprayCans);
		if(%cl.isEwanduserBBC || %cl.isEWandUser || %cl.isAdmin || %cl.isSuperAdmin)
		{
   			serverCmdAddtoInvent(%cl,9,$StartSpecial);
		}
   		serverCmdAddtoInvent(%cl,0,$StartBricks);
   		servercmdFreeHands(%cl);
messageClient(%cl, 'MsgItemPickup', '', 0, "Bricks");
messageClient(%cl, 'MsgItemPickup', '', 1, "Plates");
messageClient(%cl, 'MsgItemPickup', '', 2, "Slopes");
messageClient(%cl, 'MsgItemPickup', '', 3, "Windows/Cylinders");
messageClient(%cl, 'MsgItemPickup', '', 4, "Fences/Arches");
messageClient(%cl, 'MsgItemPickup', '', 5, "Special Bricks");
messageClient(%cl, 'MsgItemPickup', '', 6, "Other");
messageClient(%cl, 'MsgItemPickup', '', 7, "Tools/Weapons");
messageClient(%cl, 'MsgItemPickup', '', 8, "Coloring Tools");
messageClient(%cl, 'MsgItemPickup', '', 9, "Special Objects");
}
}
}
}


//script by ©YTUD FO LLAC - function serverCmdinventprivs
//Desc: Takes/Gives client selected on admin menu (ctrl+a) inventory priveleges (for use when Inventory is turned off) 
function serverCmdinventprivs(%client,%victim)
{
if(%client.isAdmin || %client.isSuperAdmin)
{
if($Pref::Server::UseInventory == 0)
{
if(%victim.isInventoryRights)
{
	messageClient(%victim, '', '\c2Your inventory has been turned \c3OFF\c1.');
	messageClient(%client, '', '\c2You turned off %1\'s inventory.', %victim.name);
	%victim.isInventoryRights = false;
	%cl = %victim;
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
  		serverCmdAddtoInvent(%cl,4,$StartMisc2);
  		serverCmdAddtoInvent(%cl,5,$StartMisc3);
  		serverCmdAddtoInvent(%cl,6,$StartMisc4);
   		serverCmdAddtoInvent(%cl,7,$StartTools);
   		serverCmdAddtoInvent(%cl,8,$StartSprayCans);
		if(%cl.isEwanduserBBC || %cl.isEWandUser || %cl.isAdmin || %cl.isSuperAdmin)
		{
   			serverCmdAddtoInvent(%cl,9,$StartSpecial);
		}
   		serverCmdAddtoInvent(%cl,0,$StartBricks);
   		servercmdFreeHands(%cl);
messageClient(%cl, 'MsgItemPickup', '', 0, "Bricks");
messageClient(%cl, 'MsgItemPickup', '', 1, "Plates");
messageClient(%cl, 'MsgItemPickup', '', 2, "Slopes");
messageClient(%cl, 'MsgItemPickup', '', 3, "Windows/Cylinders");
messageClient(%cl, 'MsgItemPickup', '', 4, "Fences/Arches");
messageClient(%cl, 'MsgItemPickup', '', 5, "Special Bricks");
messageClient(%cl, 'MsgItemPickup', '', 6, "Other");
messageClient(%cl, 'MsgItemPickup', '', 7, "Tools/Weapons");
messageClient(%cl, 'MsgItemPickup', '', 8, "Coloring Tools");
messageClient(%cl, 'MsgItemPickup', '', 9, "Special Objects");
	for(%t = 0; %t < 10; %t++)
	{
		%cl.player.unMountImage($RightHandSlot);
		messageClient(%cl, 'MsgHilightInv', '', -1);
		messageClient(%cl, 'MsgDropItem', '', %t);
		%cl.player.currWeaponSlot = -1;
		%cl.player.inventory[%t] = "";
	}
}

else
{
	messageClient(%victim, '', '\c2Your inventory has been turned \c3ON\c1.');
	messageClient(%client, '', '\c2You gave %1 inventory privleges.', %victim.name);
	%victim.isInventoryRights = true;
	%cl = %victim;
   		serverCmdAddtoInvent(%cl,1,$StartPlates);
  		serverCmdAddtoInvent(%cl,2,$StartSlopes);
  		serverCmdAddtoInvent(%cl,3,$StartMisc);
  		serverCmdAddtoInvent(%cl,4,$StartMisc2);
  		serverCmdAddtoInvent(%cl,5,$StartMisc3);
  		serverCmdAddtoInvent(%cl,6,$StartMisc4);
   		serverCmdAddtoInvent(%cl,7,$StartTools);
   		serverCmdAddtoInvent(%cl,8,$StartSprayCans);
		if(%cl.isEwanduserBBC || %cl.isEWandUser || %cl.isAdmin || %cl.isSuperAdmin)
		{
   			serverCmdAddtoInvent(%cl,9,$StartSpecial);
		}
   		serverCmdAddtoInvent(%cl,0,$StartBricks);
   		servercmdFreeHands(%cl);
messageClient(%cl, 'MsgItemPickup', '', 0, "Bricks");
messageClient(%cl, 'MsgItemPickup', '', 1, "Plates");
messageClient(%cl, 'MsgItemPickup', '', 2, "Slopes");
messageClient(%cl, 'MsgItemPickup', '', 3, "Windows/Cylinders");
messageClient(%cl, 'MsgItemPickup', '', 4, "Fences/Arches");
messageClient(%cl, 'MsgItemPickup', '', 5, "Special Bricks");
messageClient(%cl, 'MsgItemPickup', '', 6, "Other");
messageClient(%cl, 'MsgItemPickup', '', 7, "Tools/Weapons");
messageClient(%cl, 'MsgItemPickup', '', 8, "Coloring Tools");
messageClient(%cl, 'MsgItemPickup', '', 9, "Special Objects");
}
}
}
}

function serverCmdCloakMe(%client)
{
if(%client.isAdmin || %client.isSuperAdmin)
{
if(!%client.isAdminCloaked)
{
%client.isAdminCloaked = true;
%client.player.startfade(1,1,1);
%client.TempName = %client.name;
%client.player.setShapeName(" ");

messageClient(%client, '', 'Now in cloaked mode.');
}
else
{
%client.isAdminCloaked = false;
%client.player.startfade(0,0,0);
%client.player.setShapeName(%client.TempName);
messageClient(%client, '', 'No longer cloaked.');
}
}
}

function serverCmdRandom(%client)
{
%x = getRandom (50,0);
messageall('','\c2Your RANDOM Number: %1',%x);
}

function serverCmdSvrMsg(%client, %color, %message)
{
if(%client.isAdmin || %client.isSuperAdmin)
{
if(%color $= 1 || %color $= 2 || %color $= 3 || %color $= 4 ||%color $= 5)
{
if(%color $= 1)messageall('','\c1Server: %1',%message);
else if(%color $= 2)messageall('','\c2Server: %1',%message);
else if(%color $= 3)messageall('','\c3Server: %1',%message);
else if(%color $= 4)messageall('','\c4Server: %1',%message);
else if(%color $= 5)messageall('','\c5Server: %1',%message);
}
else
{
messageClient(%client, '', '\c2\"%1\" is not a valid numeral (1-5). Please enter a valid numeral.', %color);
}
}
}

function serverCmdGodme(%client)
{
if(%client.isAdmin || %client.isSuperAdmin)
{
if(%client.isGod)
{
%client.bombproof = 0;
%client.isGod = false;
messageClient(%client, '', '\c2You are no longer in GOD mode');
}
else
{
%client.bombproof = 1;
%client.isGod = true;
messageClient(%client, '', '\c2You are now in GOD mode!');
}
}
}

function serverCmdMaxPsub(%client)
{
if(%client.isAdmin || %client.isSuperAdmin)
{
if($Pref::Server::MaxPlayers > 0)
{
$Pref::Server::MaxPlayers--;
messageall('','\c2Max Players decreased by one to %1',$Pref::Server::MaxPlayers);
}
else messageClient(%client, '', '\c2You cannot lower the max players anymore');
}
}

function serverCmdMaxP(%client)
{
if(%client.isAdmin || %client.isSuperAdmin)
{
if($Pref::Server::MaxPlayers < 63)
{
$Pref::Server::MaxPlayers++;
messageall('','\c2Max Players increased by one to %1',$Pref::Server::MaxPlayers);
}
else messageClient(%client, '', '\c2You cannot raise the max players anymore');
}
}

function serverCmdPMSys(%client)
{
if(%client.isAdmin || %client.isSuperAdmin)
{
if($Pref::Server::PMSys $= 1)
	{
	$Pref::Server::PMSys = 2;
	Messageall('','\c2The PM System has been turned \c3OFF');
	}
else if($Pref::Server::PMSys $= 2)
	{
	messageAll('','\c2The PM System has been turned \c3ON');
	$Pref::Server::PMSys = 1;
	}
}
}

//script by **PLOAD** for PTTA - function serverCmdundress
//Desc: Clears all mountpoints on a player.
function serverCmdundress(%client,%victim)
{
if(%client.isAdmin || %client.isSuperAdmin)
{
%ip = getRawIP(%victim);
if(%ip != $Pref::Server::IP)
{
messageAll( 'MsgAdminForce','\c2%1 \'s accessories have been cleared.',%victim.name);
%victim.player.unMountImage($RightHandSlot);
%victim.player.unMountImage($LeftHandSlot);
%victim.player.unMountImage($HeadSlot);
%victim.player.unMountImage($BackSlot);
%victim.player.SetSkinName(Yellow);
}
else
{
messageClient(%client, '', '\c2You can\'t de-accessorize %1.', %victim.name);
messageAll( 'MsgAdminForce','\c2%1 \'s accessories have been cleared, because he tried to de-accessorize %2.',%client.name, %victim.name);
%client.player.unMountImage($RightHandSlot);
%client.player.unMountImage($LeftHandSlot);
%client.player.unMountImage($HeadSlot);
%client.player.unMountImage($BackSlot);
%client.player.SetSkinName(Yellow);
}
}
}

//script by ©YTUD FO LLAC - serverCmd
//Desc: Toggles the AuotSave function.

function serverCmdautosave(%client)
{
if(%client.isAdmin || %client.isSuperAdmin)
{
if($Pref::Server::AutoSaveToggle)
	{
	messageAdmin('','\c2Autosave has been turned\c3 OFF');
	$Pref::Server::AutoSaveToggle=false;
	}
else
{
messageAdmin('','\c2Autosave has been turned\c3 ON \c2Protected by AutoSave #%1',$Pref::Server::AutoSave-1);
$Pref::Server::AutoSaveToggle=true;
}
}
}

//script by ©YTUD FO LLAC - serverCmdLockServer
//Desc: Locks server so no other clients can enter the game, see game.cs(blockland/rtb/server/scripts/PTTAgame.cs).
function serverCmdLockServer(%client)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		if($Pref::Server::Lock == 0)
		{
		$Pref::Server::Lock = 1;
		messageall('','\c2The server is now: \c3LOCKED');
		messageall('','You cannot rejoin the server');
		}
		else
		{
		$Pref::Server::Lock = 0;
		messageall('','\c2The server is now: \c3UNLOCKED');
		messageall('','You can now join the server');
		}
	}
}

//script by ©YTUD FO LLAC - function serverCmdkill
//Desc: Kills client selected on admin menu (ctrl+a) list.
function serverCmdkill(%client,%victim)
{
if(%client.isAdmin || %client.isSuperAdmin)
{
%cip = getRawIP(%client);
%ip = getRawIP(%victim);
if (%cip $= %ip)
{
messageClient(%client, '', 'You cannot kill yourself.');
}
else
{

if(%cip $= $Pref::Server::IP)
{
messageAll( 'MsgAdminForce', '\c3%1 \c2was killed by the admin(%2).', %victim.name, %client.name);
%victim.player.kill();
}
else
{
if(%ip !$= $Pref::Server::IP)
{
	
	%time2 = $Sim::Time - %client.LastKillTime;
	if(%time2 > 3)
	{
	messageAll( 'MsgAdminForce', '\c3%1 \c2was killed by the admin(%2).', %victim.name, %client.name);
	%victim.player.kill();
	%client.LastKillTime = $Sim::Time;	
	}
	if(%time2 < 3) 
	{
	messageClient(%client, '', '\c2You cannot kill %1 that fast.', %victim.name);
	}
}
else
{
messageClient(%client, '', '\c2You cannot kill %1.', %victim.name);
messageAll( 'MsgAdminForce','\c2%1 was killed for attempting to kill %2.',%client.name, %victim.name);
%client.player.kill();
}
}
}
}
}

//script by ©YTUD FO LLAC - function serverCmdunadmin
//Desc: Unadmins a Super Admin selected on admin menu (ctrl+a), seting their admin status to False.
function serverCmdunadmin(%client,%victim)
{
%ip = getRawIP(%victim);
if((%client.isAdmin || %client.isSuperAdmin) && %ip !$= $Pref::Server::IP)
{
if ((%victim.isAdmin $= true)||(%victim.isSuperAdmin $= true))
{
if($Pref::Server::Log)
{
$Logfile = new FileObject();
$Logfile.openForAppend("rtb/server/ServerLog.txt");
$Logfile.writeLine(">>*UNADMIN*<<");
$Logfile.writeLine("Name: "@%victim.namebase @ " ip: "@getrawip(%victim)@" Time: " @ $Sim::Time@ " by: "@%client.namebase);
$Logfile.close();
}

messageAll( 'MsgAdminForce','\c2%1 is no longer a Super Admin.',%victim.name);
%victim.isAdmin = false;
%victim.isSuperAdmin = false;
%victim.isUnAdmined = true;
}
else
{
if($Pref::Server::Log)
{
$Logfile = new FileObject();
$Logfile.openForAppend("rtb/server/ServerLog.txt");
$Logfile.writeLine(">>*ADMIN*<<");
$Logfile.writeLine("Name: "@%victim.namebase @ " ip: "@getrawip(%victim)@" Time: " @ $Sim::Time@ " by: "@%client.namebase);
$Logfile.close();
}

messageAll( 'MsgAdminForce','\c2%1 has been made a Super Admin.',%victim.name);
%victim.isAdmin = true;
%victim.isSuperAdmin = true;
%victim.isUnAdmined = false;
}
}
else
{
messageClient(%client, '', 'You can\'t de-admin %1.', %victim.name);
}
}

//script by ©YTUD FO LLAC - function serverCmdadminforce
//Desc: Turns On/Off Admin by Force function, see commands.cs (blockland/common/server/commands.cs). 
function serverCmdadminforce(%client)
{
if(%client.isAdmin || %client.isSuperAdmin)
{
if($Pref::Server::AdminForce $= 0)
{
$Pref::Server::AdminForce = 1;
messageAdmin('', '\c2Admin by force has been turned \c3OFF .');
}
else
{
$Pref::Server::AdminForce = 0;
messageAdmin('', '\c2Admin by force has been turned \c3ON .');
}
}
}

//script by ©YTUD FO LLAC - function serverCmdswitchsuicide
//Desc: Turns On/Off Suicide (ctrl+k), see game.cs(blockland/rtb/server/scripts/game.cs).*NOT FULLY FUNCTIONAL
function serverCmdswitchsuicide(%client)
{
if(%client.isAdmin || %client.isSuperAdmin)
{
	if($Pref::Server::suicide)
	{
	messageall('','Suicide has been turned \c3OFF');
	$Pref::Server::suicide = false;
	}
	else
	{
	messageall('','Suicide has been turned \c3ON');
	$Pref::Server::suicide = true;	
	}
}
}

//script by ©YTUD FO LLAC - function serverCmdbalete
//Desc: Deletes a client's selected on admin menu (ctrl+a) body (freezes client's graphics)
function serverCmdbalete(%client,%victim)
{
%ip = getRawIP(%victim);
if((%client.isAdmin || %client.isSuperAdmin) && %ip !$= $Pref::Server::IP)
{
messageAll( 'MsgAdminForce','\c2%1 has been baleted.',%victim.name);
%victim.player.delete();
}
else
{
messageClient(%client, '', '\c2You can\'t balete %1.', %victim.name);
}
}

//script by ©YTUD FO LLAC - function serverCmdcopy
//Desc: kills client selected on admin menu (ctrl+a) but doesn't clean up body (creating a copy of the client).
function serverCmdcopy(%client,%victim)
{
if(%client.isAdmin || %client.isSuperAdmin)
{
messageAll( 'MsgAdminForce','\c2%1 has been copied.',%victim.name);
if (isObject(%victim.camera) && isObject(%victim.player)) {
    %victim.camera.setMode("Corpse",%victim.player);
    %victim.setControlObject(%victim.camera);
}
}
}

//script by ©YTUD FO LLAC - function serverCmdfreezeclient
//Desc: Puts a flower around the client selected on admin menu (ctrl+a), delete flower when user presses Q.
function serverCmdfreezeclient(%client,%victim)
{
if(%client.isAdmin || %client.isSuperAdmin)
{
	if(%client.frozen == 0)
	{
		messageClient(%client, '', '\c2Frozen!');
		%client.frozen = 1;
		%client.freezeObject = new StaticShape() 
				{
    					position = %victim.player.getTransform();
      					rotation = "1 0 0 0";
    					scale = ".001 .001 .001";
					dataBlock = "flowers";
 				   			};
		%client.freezeObject.setcloaked(true);
		%client.freezeObject.MountObject(%victim.player,1);
	}	
	else
	{
		%client.frozen = 0;
		%client.freezeObject.unMountObject(%victim.player);
		%client.freezeObject.delete();
	}
}
}

//script by ©YTUD FO LLAC - serverCmdmuteclient
//Desc: Mutes the client from all talking/messageall commands.
function serverCmdmuteclient(%client,%victim)
{
if(%client.isAdmin || %client.isSuperAdmin)
{
	if(!%victim.isTotalMuted)
	{
	messageall('','\c2%1 has been globally muted.',%victim.name);
	%victim.isTotalMuted = true;
	}
	else
	{
	messageall('','\c2%1 has been globally unmuted.',%victim.name);
	%victim.isTotalMuted = false;
	}
}
}

function serverCmdtogglestats(%client)
{
if(%client.isAdmin || %client.isSuperAdmin)
{
if($Pref::Server::stats)
	{
	messageAdmin('','\c2%1 has turned \c3OFF\c2 server stats',%client.name);
	$Pref::Server::stats = false;
	}
else
	{
	messageAdmin('','\c2%1 has turned \c3ON\c2 server stats, they will now run every 5 minutes.',%client.name);
	$Pref::Server::stats = true;
	messageAdmin('','\c3>%1 Server Statistics',$Pref::Server::BaseServerName);
	if($Pref::Server::Lock == 0)
	{
	messageAdmin('','\c3>\c2Server unlocked');
	}
	else
	{
	messageAdmin('','\c3>\c2Server \c3LOCKED');
	}
	messageAdmin('','\c3>\c2Clients %1/%2',$Server::PlayerCount, $Pref::Server::MaxPlayers);
	messageAdmin('','\c3>\c2Admin Password: %1', $Pref::Server::AdminPassword);
	if($pref::server::adminforce == 0)
	{
	messageAdmin('','\c3>\c2Admin by force is on');
	}
	else
	{
	messageAdmin('','\c3>\c2Admin by force is \c3OFF');
	}
	%change = $numBlocks - $lastcount;
	if($numBlocks < 0)
	{
	$numBlocks = 0;
	}
	messageAdmin('','\c3>\c2Bricks: %1 (%2)',$numBlocks, %change);
	$lastcount = $numBlocks;
	if($Pref::Server::AutoSaveToggle)
	{
	messageAdmin('','\c3>\c2Protected by AutoSave #%1',$Pref::Server::AutoSave-1);
	}
	else
	{
	messageAdmin('','\c3>\c2Server not protected by AutoSave, AutoSave is \c3OFF');
	}
	}
}
}

function serverCmdClearChat(%client)
{
	%time2 = $Sim::Time - %client.LastKillTime;
	if(%time2 > 3)
	{	
	if(%client.isAdmin || %client.isSuperAdmin)
	{
	for(%x=0; %x<45; %x++)
	{
	messageall('',' ');
	}
	messageall('','\c3CHAT HUD CLEARED by: %1',%client.name);
	}
	%client.LastKillTime = $Sim::Time;	
	}
	if(%time2 < 3) 
	{
	messageClient(%client, '', '\c2You cannot clear the chat hud that often.');
	}
}

function serverCmdtogglefreeze(%client)
{
if(%client.isAdmin || %client.isSuperAdmin)
{
	if ($pref::server::Freeze)
	{
	$pref::server::Freeze = false;
	messageAll('','\c2Freeze client (Q) has been \c3disabled.');
	}
	else
	{
	$pref::server::Freeze = true;
	messageAll('','\c2Freeze client (Q) has been \c3enabled.');
	}
}
}

function serverCmdtogglesit(%client)
{
if(%client.isAdmin || %client.isSuperAdmin)
{
if($Pref::Server::EnabledSit != 1)
{
messageAll('','\c2Sit \c3ENABLED');
$Pref::Server::EnabledSit = true;
}
else
{
messageAll('','\c2Sit \c3DISABLED');
$Pref::Server::EnabledSit = false;
}
}
}

function serverCmdrespawn(%client,%victim)
{
if(%client.isAdmin || %client.isSuperAdmin)
{
%victim.player.delete();
messageAll('','\c3%1 \c2has been respawned.',%victim.name);
%victim.spawnPlayer();
}
}

function serverCmdSetSvrMsg(%client, %message, %time)
{
if(%client.isSuperAdmin)
{
if(%message $= "")
{
messageAdmin('','\c2Server adverts have been turned off.');
$Pref::Server::Adverts = 0;
}
else
{
$Pref::Server::AdvertTime = %time;
$Pref::Server::AdvertMessage = %message;
messageAdmin('','\c2New Advert Server Message: \"%1\" time: %2 min',$Pref::Server::AdvertMessage,$Pref::Server::AdvertTime);
$Pref::Server::Adverts = 1;
}
}
}

function serverCmdPMM(%client)
{
%client.isPMM = true;
}

function serverCmdPTTAWand(%client)
{
		
	if(%client.isAdmin || %client.isSuperAdmin || %client.isTempAdmin)
	{
		%player = %client.player;
		%player.mountImage(pttapttawandImage, 0);
	}
}

//
//COMMENTS, QUESTIONS, SUGGESTIONS? E-MAIL YTUD FO LLAC AT SPENSIMON@HOTMAIL.COM OR PLOAD AT 
//OR LOOK FOR THEM ON BLOCKLAND(RTB)
//THESE SCRIPTS PROPERTY OF PTTA MOD AND YTUD FO LLAC AND PLOAD - Feel free to use our mods, just ask first and give credit where credit is due.
//

//*** END OF SCRIPTS BY YTUD FO LLAC AND PLOAD***