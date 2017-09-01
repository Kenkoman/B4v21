//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//----------------------------------------------------------------------------
// Game start / end events sent from the server
//----------------------------------------------------------------------------
function clientCmdgetVersion()
{
   return $Pref::Player::Version;
}

function clientCmdGameStart(%seq)
{
   PlayerListGui.zeroScores();
}

function clientCmdMBOk(%this, %that)
{
	messageBoxOk(%this,%that);
}

function clientCmdGameEnd(%seq)
{
   // Stop local activity... the game will be destroyed on the server
   alxStopAll();

   // Copy the current scores from the player list into the
   // end game gui (bit of a hack for now).
   EndGameGuiList.clear();
   for (%i = 0; %i < PlayerListGuiList.rowCount(); %i++) {
      %text = PlayerListGuiList.getRowText(%i);
      %id = PlayerListGuiList.getRowId(%i);
      EndGameGuiList.addRow(%id,%text);
   }
   EndGameGuiList.sortNumerical(1,false);

   // Display the end-game screen
   Canvas.setContent(EndGameGui);
}

function clientCmdUpdatePrefs()
{
	commandtoserver('updatePrefs',
			$pref::Player::Name,
			$pref::Color::Skin,
			$pref::Accessory::headCode,
			$pref::Accessory::visorCode,
			$pref::Accessory::backCode,
			$pref::Accessory::leftHandCode,
			$pref::Accessory::headColor,
			$pref::Accessory::visorColor,
			$pref::Accessory::backColor,
			$pref::Accessory::leftHandColor,
			$pref::Decal::chest,
			$pref::Decal::face);

}
