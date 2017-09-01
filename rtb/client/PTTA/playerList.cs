//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Hook into the client update messages to maintain our player list
// and scoreboard.
//-----------------------------------------------------------------------------

addMessageCallback('MsgClientJoin', handleClientJoin);
addMessageCallback('MsgClientDrop', handleClientDrop);
addMessageCallback('MsgClientScoreChanged', handleClientScoreChanged);

addMessageCallback('MsgTeamAdd', handleTeamAdd);
addMessageCallback('MsgTeamRemove', handleTeamRemove);

addMessageCallback('MsgBanAdd', handleBanAdd);
addMessageCallback('MsgBanRemove', handleBanRemove);

addMessageCallback('MsgSendPlayerList', handlePlayerList);
addMessageCallback('MsgSendPlayerTeamList', handlePlayerTeamList);
addMessageCallback('MsgSendTeamList', handleTeamList);
//-----------------------------------------------------------------------------
function handleTeamList(%msgType, %msgString, %Team, %teamid)
{
	AT_Teams.addRow(%teamid, %Team, 0);
}

function handlePlayerTeamList(%msgType, %msgString, %PlayerName, %Team, %client)
{
	AT_playerTeams.addRow(%client,%PlayerName TAB %Team, 0);
}

function handlePlayerList(%msgType, %msgString, %PlayerName, %Friend, %Safe, %cl)
{
	switch (%friend)
	{
	case 1:
	%Friend = "Yes";
	case 0:
	%Friend = "No";
	}
	switch (%safe)
	{
	case 1:
	%Safe = "Yes";
	case 0:
	%Safe = "No";
	}
	AP_PlayerStatus.addRow(%cl, %PlayerName TAB %Friend TAB %Safe, 0);
}

function handleBanRemove(%msgType, %msgString, %BanID)
{
}

function handleBanAdd(%msgType, %msgString, %BanID, %BanName, %BanIP, %BanSubnet)
{	
	if(%BanSubnet !$= "")
	{
		AL_Banlist.addRow(%BanID,%BanID TAB %BanName TAB %BanIP TAB %BanSubnet, %BanID);
	}
	else
	{
		AL_Banlist.addRow(%BanID,%BanID TAB %BanName TAB %BanIP TAB "No", %BanID);
	}
}

function handleTeamRemove(%msgType, %msgString, %teamID)
{
	lstTeamList.removeRowByID(%teamID);
}

function handleTeamAdd(%msgType, %msgString, %teamName, %teamID)
{
	%clientId = %teamID;
	%name = StripMLControlChars(detag(%teamName));
	
	if (lstTeamList.getRowNumById(%clientId) == -1)
	{
		lstTeamList.addRow(%clientId, %name);
	}
	else
	{
		lstTeamList.setRowById(%clientId, %name);
	}
}

function handleClientJoin(%msgType, %msgString, %clientName, %clientId,
   %guid, %score, %isAI, %isAdmin, %isSuperAdmin )
{
   PlayerListGui.update(%clientId,detag(%clientName),%isSuperAdmin,
      %isAdmin,%isAI,%score);

	//add players to admin list too
	%name = StripMLControlChars(detag(%clientName));
	if (lstAdminPlayerList.getRowNumById(%clientId) == -1)
	{
		lstAdminPlayerList.addRow(%clientId, %name);
	}
	else
	{
		lstAdminPlayerList.setRowById(%clientId, %name);
	}

	if (lstPttaPlayerList.getRowNumById(%clientId) == -1)
	{
		lstPttaPlayerList.addRow(%clientId, %name);
	}
	else
	{
		lstPttaPlayerList.setRowById(%clientId, %name);
	}

	if (lstBACPlayerList.getRowNumById(%clientId) == -1)
	{
		lstBACPlayerList.addRow(%clientId, %name);
	}
	else
	{
		lstBACPlayerList.setRowById(%clientId, %name);
	}

	if (lstPlayersPlayerList.getRowNumById(%clientId) == -1)
	{
		lstPlayersPlayerList.addRow(%clientId, %name);
	}
	else
	{
		lstPlayersPlayerList.setRowById(%clientId, %name);
	}
	
	if (lstMessagePlayerList.getRowNumById(%clientId) == -1)
	{
		lstMessagePlayerList.addRow(%clientId, %name);
	}
	else
	{
		lstMessagePlayerList.setRowById(%clientId, %name);
	}

	if (lstTeamPlayerList.getRowNumById(%clientId) == -1)
	{
		lstTeamPlayerList.addRow(%clientId, %name);
	}
	else
	{
		lstTeamPlayerList.setRowById(%clientId, %name);
	}
	if (BotGUIClientList.getRowNumById(%clientId) == -1)
	{
		BotGUIClientList.addRow(%clientId, %name);
	}
	else
	{
		BotGUIClientList.setRowById(%clientId, %name);
	}
}

function handleClientDrop(%msgType, %msgString, %clientName, %clientId)
{
   PlayerListGui.remove(%clientId);
   lstAdminPlayerList.removeRowByID(%clientId);
   lstPttaPlayerList.removeRowByID(%clientId);
   lstBACPlayerList.removeRowByID(%clientId);
   lstPlayersPlayerList.removeRowByID(%clientId);
   lstMessagePlayerList.removeRowByID(%clientId);
   lstTeamPlayerList.removeRowByID(%clientId);
   BotGUIClientList.removeRowByID(%clientId);
}

function handleClientScoreChanged(%msgType, %msgString, %score, %clientId)
{
   PlayerListGui.updateScore(%clientId,%score);
}
