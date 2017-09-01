//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Server side message commands
//-----------------------------------------------------------------------------

function messageClient(%client, %msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13)
{

   commandToClient(%client, 'ServerMessage', %msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13);
}

function messageTeam(%team, %msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13)
{
   %count = ClientGroup.getCount();
   if(%team $= "")
      return;
   for(%cl= 0; %cl < %count; %cl++)
   {
      %recipient = ClientGroup.getObject(%cl);
	  if(%recipient.team $= %team)
	      messageClient(%recipient, %msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13);
   }
}

function messageTeamExcept(%client, %msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13)
{
   %team = %client.team;
   %count = ClientGroup.getCount();
   for(%cl= 0; %cl < %count; %cl++)
   {
      %recipient = ClientGroup.getObject(%cl);
	  if((%recipient.team == %team) && (%recipient != %client))
	      messageClient(%recipient, %msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13);
   }
}

function messageAll(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13)
{


   %count = ClientGroup.getCount();
   for(%cl = 0; %cl < %count; %cl++)
   {
      %client = ClientGroup.getObject(%cl);
      messageClient(%client, %msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13);
   }
}

function messageAllExcept(%client, %team, %msgtype, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13)
{  
   //can exclude a client, a team or both. A -1 value in either field will ignore that exclusion, so
   //messageAllExcept(-1, -1, $Mesblah, 'Blah!'); will message everyone (since there shouldn't be a client -1 or client on team -1).
   %count = ClientGroup.getCount();
   for(%cl= 0; %cl < %count; %cl++)
   {
      %recipient = ClientGroup.getObject(%cl);
      if((%recipient != %client) && (%recipient.team != %team))
         messageClient(%recipient, %msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13);
   }
}

function messageAllAdmin(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13)
{


   %count = ClientGroup.getCount();
   for(%cl = 0; %cl < %count; %cl++)
   {
      %client = ClientGroup.getObject(%cl);
      if(%client.isAdmin || %client.isSuperAdmin)
      {
      messageClient(%client, %msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13);
      }
   }
}

//---------------------------------------------------------------------------
// Server side client chat'n
//---------------------------------------------------------------------------

//---------------------------------------------------------------------------
// silly spam protection...
$SPAM_PROTECTION_PERIOD     = 10000;
$SPAM_MESSAGE_THRESHOLD     = 4;
$SPAM_PENALTY_PERIOD        = 10000;
$SPAM_MESSAGE               = '\c3FLOOD PROTECTION:\c0 You must wait another %1 seconds.';

function GameConnection::spamMessageTimeout(%this)
{
   if(%this.spamMessageCount > 0)
      %this.spamMessageCount--;
}

function GameConnection::spamReset(%this)
{
   %this.isSpamming = false;
}

function spamAlert(%client)
{
   if($Pref::Server::FloodProtectionEnabled != true || %client.isSuperAdmin)
      return(false);

   if(!%client.isSpamming && (%client.spamMessageCount >= $SPAM_MESSAGE_THRESHOLD))
   {
      %client.spamProtectStart = getSimTime();
      %client.isSpamming = true;
      %client.schedule($SPAM_PENALTY_PERIOD, spamReset);
   }

   if(%client.isSpamming)
   {
      %wait = mFloor(($SPAM_PENALTY_PERIOD - (getSimTime() - %client.spamProtectStart)) / 1000);
      messageClient(%client, "", $SPAM_MESSAGE, %wait);
      return(true);
   }

   %client.spamMessageCount++;
   %client.schedule($SPAM_PROTECTION_PERIOD, spamMessageTimeout);
   return(false);
}


//---------------------------------------------------------------------------

function chatMessageClient( %client, %sender, %voiceTag, %voicePitch, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10 )
{
	//see if the client has muted the sender
	if ( !%client.muted[%sender] )
	{
	   commandToClient( %client, 'ChatMessage', %sender, %voiceTag, %voicePitch, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10 );
	}
}

function chatMessageTeam( %sender, %team, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10 )
{
   if ( ( %msgString $= "" ) || spamAlert( %sender ) )
      return;

   if(%team $= "")
	return;
   %count = ClientGroup.getCount();
   for ( %i = 0; %i < %count; %i++ )
   {
      %obj = ClientGroup.getObject( %i );
      if ( %obj.team $= %sender.team )
         chatMessageClient( %obj, %sender, %sender.voiceTag, %sender.voicePitch, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10 );
   }
}

function chatMessageAll( %sender, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10 )
{
   if ( ( %msgString $= "" ) || spamAlert( %sender ) || ( %sender.isTimeout $= 1) )
      return;

   if($Pref::Server::Moderated $= 1)
   {
	if(%sender.isSuperAdmin || %sender.isVoiced || %sender.isAdmin || %sender.isTempAdmin)
	{

	}
	else
	{
		return;
	}	
   }

   if(getSubStr(%a2, 0, 3) $= "/pm")
   {
	%username = getWord(%a2, 1);

	%totalchars = strlen(%a2)-(strlen(%username)+4);
	%usernamelength = 5+strlen(%username);
	%message = getSubStr(%a2, %usernamelength, %totalchars);

	%match = 0;
	for(%i = 0; %i<ClientGroup.getCount(); %i++)
	{
		%cl = ClientGroup.getObject(%i);
		if(%cl.namebase $= %username)
		   %match = %cl;
	}
	if(%match !$= 0)
	{
		messageClient(%match,'','\c1%1\c2(PM)\c1: %2',%sender.name,%message);
		messageClient(%sender,'','\c1%1\c2(%2)\c1: %3',%sender.name,%username,%message);
	}
	else
	{
		messageClient(%sender,'','\c4Username \'\c0%1\c4\' not Found!',%username);
	}
	return;
   }
   if(getSubStr(%a2, 0, 3) $= "/me")
   {
	%action = getSubStr(%a2, 4, strlen(%a2)-4);
	messageAll('name', '\c0*%1 %2', %sender.name, %action);
	return;
   }
   if(getSubStr(%a2, 0, 9) $= "/announce" && %sender.isSuperAdmin)
   {
	%message = getSubStr(%a2, 10, strlen(%a2)-10);
	messageAll('name', '\c3%1', %message);
	return;
   }
   if(getSubStr(%a2, 0, 6) $= "/voice" && (%sender.isSuperAdmin || %sender.isTempAdmin))
   {
	%username = getWord(%a2, 1);

	%match = 0;
	for(%i = 0; %i<ClientGroup.getCount(); %i++)
	{
		%cl = ClientGroup.getObject(%i);
		if(%cl.namebase $= %username)
		   %match = %cl;
	}
	if(%match !$= 0)
	{
		if(%match.isVoiced !$= 1)
		{
			messageClient(%match,'','\c3You have been \c0Voiced \c3by an Admin.');
			messageClient(%sender,'','\c3You have Voiced \c0%1\c3.',%username);
			%match.isVoiced = 1;
		}
		else
		{
			messageClient(%match,'','\c3You have been \c0Devoiced \c3by an Admin.');
			messageClient(%sender,'','\c3You have Devoiced \c0%1\c3.',%username);
			%match.isVoiced = 0;
		}
	}
	else
	{
		messageClient(%sender,'','\c4Username \'\c0%1\c4\' not Found!',%username);
	}
	return;
   }
   if(getSubStr(%a2, 0, 5) $= "/help")
   {
	messageClient(%sender, '', "\c0-=Slash Commands Help=-");
	messageClient(%sender, '', "\c3*\c4 = Admin Only.");
	messageClient(%sender, '', "\c4/pm (Name) (Message) will send a PM to that person.");
	messageClient(%sender, '', "\c4/me (Action) will do a *Ephialtes sits on the chair.");
	messageClient(%sender, '', "\c4/announce (Message) will do a red announcement.\c3*");
	messageClient(%sender, '', "\c4/voice (Name) will allow person to speak in Moderation.\c3*");
	return;
   }
   //###############################
   //#Confirmation Coding
   //###############################
   if(%sender.WaitingforMessage $= 1)
   {
	if(%sender.WantClearOwnBricks $= 0)
	{
		if(%a2 $= "Yes")
		{
		%sender.WantClearOwnBricks = 1;
		servercmdClearOwnBricks(%sender);
		return;
		}
		else
		{
		messageClient(%sender,'',"\c3Clear Own Bricks Cancelled!");
		%sender.WaitingForMessage = 0;
		%sender.WantClearOwnBricks = 0;
		return;
		}
	}
   }
	




   %count = ClientGroup.getCount();
   for ( %i = 0; %i < %count; %i++ )
   {
		%obj = ClientGroup.getObject( %i );
		if(%sender.team != 0)
	      chatMessageClient( %obj, %sender, %sender.voiceTag, %sender.voicePitch, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10 );
		else
		{
			// message sender is an observer -- only send message to other observers
			if(%obj.team == %sender.team)
		      chatMessageClient( %obj, %sender, %sender.voiceTag, %sender.voicePitch, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10 );
		}
	}
}


//#############################
//# Overriding Common Cmds
//#############################

function serverCmdTeamMessageSent(%client, %text)
{
	%obj = %client.player;
	%obj.playthread(0, talk);				//fwar play talk animation
	%obj.schedule(strlen(%text) * 50 , stopthread, 0);

   if(strlen(%text) >= $Pref::Server::MaxChatLen)
      %text = getSubStr(%text, 0, $Pref::Server::MaxChatLen);
   chatMessageTeam(%client, %client.team, '\c1%1(\c5%2\c1): %3', %client.name, %client.team,%text);
 //  echo("(T)", getTaggedString(%client.name), ": ", %text);
}

function serverCmdLocalMessageSent(%client, %text)
{
	%obj = %client.player;
	%obj.playthread(0, talk);				//fwar play talk animation
	%obj.schedule(strlen(%text) * 50 , stopthread, 0);

   if(strlen(%text) >= $Pref::Server::MaxChatLen)
      %text = getSubStr(%text, 0, $Pref::Server::MaxChatLen);

   InitContainerRadiusSearch(%client.player.getTransform(), 5, $TypeMasks::PlayerObjectType);

   //Lets find some people around us.
   while ((%targetObject = containerSearchNext()) != 0) 
   {
   	if(%targetObject.client !$= "")
   	{
		chatMessageClient(%targetObject.client, %client, 0, 0, '\c1%1(\c3Local\c1): %2', %client.name, %text);
	}
   }
}

function serverCmdMessageSent(%client, %text)
{
	%obj = %client.player;
	%obj.playthread(0, talk);				//fwar play talk animation
	%obj.schedule(strlen(%text) * 50, stopthread, 0);

   if(strlen(%text) >= $Pref::Server::MaxChatLen)
      %text = getSubStr(%text, 0, $Pref::Server::MaxChatLen);
   chatMessageAll(%client, '\c1%1: %2', %client.name, %text);
//   echo(getTaggedString(%client.name), ": ", %text);
}