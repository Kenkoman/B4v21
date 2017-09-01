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
   %guid, %score, %isAI, %isAdmin, %isSuperAdmin ,%isMod)
{
if (detag(%clientName)!$="") {
   PlayerListGui.update(%clientId,%clientName,%isSuperAdmin,
      %isAdmin,%isMod,%isAI,%score);
   %tag = %isSuperAdmin? "[Super]":
          (%isAdmin? "[Admin]":
          (%isMod? "[Mod]":
          (%isAI? "[Bot]":
          "")));
   %name =  StripMLControlChars(detag(%clientName)) @ %tag;
	//add players to admin list too
	if (AdminPlayerList.getRowNumById(%clientId) == -1)
		AdminPlayerList.addRow(%clientId, %name);
	else
		AdminPlayerList.setRowById(%clientId, %name);
  }
}

function handleClientDrop(%msgType, %msgString, %clientName, %clientId)
{
   PlayerListGui.remove(%clientId);
   AdminPlayerList.removeRowByID(%clientId);
}

function handleClientScoreChanged(%msgType, %msgString, %score, %clientId)
{
   PlayerListGui.updateScore(%clientId,%score);
}
