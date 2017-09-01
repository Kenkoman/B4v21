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

//-----------------------------------------------------------------------------

function handleClientJoin(%msgType, %msgString, %clientName, %clientId,
   %guid, %score, %isAI, %isAdmin, %isSuperAdmin )
{
   PlayerListGui.update(%clientId,detag(%clientName),%isSuperAdmin,
      %isAdmin,%isAI,%score);

	//add players to admin list too
	%name = StripMLControlChars(detag(%clientName));
	if (lstAdminPlayerList.getRowNumById(%clientId) == -1)
		lstAdminPlayerList.addRow(%clientId, %name);
	else
		lstAdminPlayerList.setRowById(%clientId, %name);
}

function handleClientDrop(%msgType, %msgString, %clientName, %clientId)
{
   PlayerListGui.remove(%clientId);
   lstAdminPlayerList.removeRowByID(%clientId);
}

function handleClientScoreChanged(%msgType, %msgString, %score, %clientId)
{
   PlayerListGui.updateScore(%clientId,%score);
}
