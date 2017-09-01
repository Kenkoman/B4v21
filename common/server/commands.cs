//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Misc. server commands avialable to clients
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------

function serverCmdSAD( %client, %password )
{
   if( %password !$= "" && %password $= $Pref::Server::AdminPassword)
   {
      %client.isAdmin = true;
      %client.isSuperAdmin = true;
      %name = getTaggedString( %client.name );
      if(%client.secure)
        MessageAll( 'MsgAdminForce', "\c2" @ %name @ " has become Admin by force.", %client );   
		messageAll('MsgClientJoin', '', 
			  %client.name, 
			  %client,
			  %client.sendGuid,
			  %client.score,
			  %client.isAiControlled(), 
			  %client.isAdmin, 
			  %client.isSuperAdmin);
   }
   else
   {
		%client.adminTries++;
		if(%client.adminTries > 5)
		{
			messageAll( 'MsgAdminForce', '\c2%1 Guessed failed to guess the admin password.', %client.name);
			%client.delete("You guessed wrong.");
		}
   }
}

function serverCmdSADSetPassword(%client, %password)
{
   if(%client.isSuperAdmin)
      $Pref::Server::AdminPassword = %password;
}


//----------------------------------------------------------------------------
// Server chat message handlers

function serverCmdTeamMessageSent(%client, %text)
{
	%obj = %client.player;
	%obj.playthread(0, talk);				//fwar play talk animation
	%obj.schedule(strlen(%text) * 50 , stopthread, 0);

   if(strlen(%text) >= $Pref::Server::MaxChatLen)
      %text = getSubStr(%text, 0, $Pref::Server::MaxChatLen);
   chatMessageTeam(%client, %client.team, '\c3%1: %2', %client.name, %text);
   echo("(T)", getTaggedString(%client.name), ": ", %text);
}

function serverCmdMessageSent(%client, %text)
{
	%obj = %client.player;
	%obj.playthread(0, talk);				//fwar play talk animation
	%obj.schedule(strlen(%text) * 50, stopthread, 0);

   if(strlen(%text) >= $Pref::Server::MaxChatLen)
      %text = getSubStr(%text, 0, $Pref::Server::MaxChatLen);
   chatMessageAll(%client, '\c4%1: %2', %client.name, %text);
   echo(getTaggedString(%client.name), ": ", %text);
}

