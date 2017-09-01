//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------
//Word Filter stuff.  Its a modded GG resource

function cleanString(%string)
{
   %string = parsestring(%string);
   %wordcount = getWordCount(%string);
   for(%j=0; %j<%wordcount; %j++)
   {
   for(%i=0; %i<$numBannedWords; %i++)
   {
   if(getword(%string,%j) $= $bannedWords[%i])
	return false;
   }
   }
   %string2 = parsestring(%string2);
   %wordcount = getWordCount(%string2);
   for(%j=0; %j<%wordcount; %j++)
   {
   for(%i=0; %i<$numBannedWords; %i++)
   {
   if(getword(%string2,%j) $= $bannedWords[%i])
	return false;
   }
   }
   return true;
}
function readBannedWords(%bannedWordFile)
{
	%file = new FileObject();
	%file.openForRead(%bannedwordFile);

   %i = 0;
   while( !%file.isEOF() )
   {
      %line = %file.readLine();
    if(%line !$= "") {
    $bannedWords[%i] = %line;
      %i++;
      }}
      $numBannedWords = %i;
   %file.close();
}
function parsestring(%word) {
%word = strreplace(%word,"`", "");
%word = strreplace(%word,"~", "");
%word = strreplace(%word,"!", "");
%word = strreplace(%word,"@", "");
%word = strreplace(%word,"\c0", "");
%word = strreplace(%word,"\c1", "");
%word = strreplace(%word,"\c2", "");
%word = strreplace(%word,"\c3", "");
%word = strreplace(%word,"\c4", "");
%word = strreplace(%word,"\c5", "");
%word = strreplace(%word,"\n", "");
%word = strreplace(%word,"/grey", "");
%word = strreplace(%word,"/red", "");
%word = strreplace(%word,"/white", "");
%word = strreplace(%word,"/blue", "");
%word = strreplace(%word,"/green", "");
%word = strreplace(%word,"#", "");
%word = strreplace(%word,"$", "");
%word = strreplace(%word,"%", "");
%word = strreplace(%word,"^", "");
%word = strreplace(%word,"&", "");
%word = strreplace(%word,"*", "");
%word = strreplace(%word,"(", "");
%word = strreplace(%word,")", "");
%word = strreplace(%word,"-", "");
%word = strreplace(%word,"_", "");
%word = strreplace(%word,"=", "");
%word = strreplace(%word,"+", "");
%word = strreplace(%word,"[", "");
%word = strreplace(%word,"]", "");
%word = strreplace(%word,"{", "");
%word = strreplace(%word,"}", "");
%word = strreplace(%word,"|", "");
%word = strreplace(%word,";", "");
%word = strreplace(%word,":", "");
%word = strreplace(%word,",", "");
%word = strreplace(%word,"<", "");
%word = strreplace(%word,".", "");
%word = strreplace(%word,">", "");
%word = strreplace(%word,"?", "");
return %word;
}
function parsestring2(%word) {
%word = strreplace(%word,"`", " ");
%word = strreplace(%word,"~", " ");
%word = strreplace(%word,"!", " ");
%word = strreplace(%word,"@", " ");
%word = strreplace(%word,"\c0", " ");
%word = strreplace(%word,"\c1", " ");
%word = strreplace(%word,"\c2", " ");
%word = strreplace(%word,"\c3", " ");
%word = strreplace(%word,"\c4", " ");
%word = strreplace(%word,"\c5", " ");
%word = strreplace(%word,"\n", " ");
%word = strreplace(%word,"/grey", " ");
%word = strreplace(%word,"/red", " ");
%word = strreplace(%word,"/white", " ");
%word = strreplace(%word,"/blue", " ");
%word = strreplace(%word,"/green", " ");
%word = strreplace(%word,"#", " ");
%word = strreplace(%word,"$", " ");
%word = strreplace(%word,"%", " ");
%word = strreplace(%word,"^", " ");
%word = strreplace(%word,"&", " ");
%word = strreplace(%word,"*", " ");
%word = strreplace(%word,"(", " ");
%word = strreplace(%word,")", " ");
%word = strreplace(%word,"-", " ");
%word = strreplace(%word,"_", " ");
%word = strreplace(%word,"=", " ");
%word = strreplace(%word,"+", " ");
%word = strreplace(%word,"[", " ");
%word = strreplace(%word,"]", " ");
%word = strreplace(%word,"{", " ");
%word = strreplace(%word,"}", " ");
%word = strreplace(%word,"|", " ");
%word = strreplace(%word,";", " ");
%word = strreplace(%word,":", " ");
%word = strreplace(%word,",", " ");
%word = strreplace(%word,"<", " ");
%word = strreplace(%word,".", " ");
%word = strreplace(%word,">", " ");
%word = strreplace(%word,"?", " ");
return %word;
}
readBannedWords("rtb/bannedWords.txt");

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
	if(!%sender.isTotalMuted)
	{
	//see if the client has muted the sender
	
	if ( !%client.muted[%sender] )
	{
	   commandToClient( %client, 'ChatMessage', %sender, %voiceTag, %voicePitch, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10 );
	  
	}
	
	}
	else
	{
	messageClient(%sender,"",'You are not allowed to talk!');
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
   %a2 = strreplace(%a2,"\n","");
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
	if(!%client.isTotalMuted)
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
		$ChatLog = new FileObject();
		$ChatLog.openForAppend("rtb/server/ChatLog.txt");
		$ChatLog.writeLine(%sender.namebase @ "(PM:"@%match@"): "@%message);
		$ChatLog.close();
		messageClient(%match,'','\c1%1\c2(PM)\c1: %2',%sender.name,%message);
		messageClient(%sender,'','\c1%1\c2(%2)\c1: %3',%sender.name,%username,%message);
	}
	else
	{
		messageClient(%sender,'','\c4Username \'\c0%1\c4\' not Found!',%username);
	}
	return;
	}
   }
   if(getSubStr(%a2, 0, 3) $= "/me")
   {
	if($Pref::Server::SCommands)
	{
	%action = getSubStr(%a2, 4, strlen(%a2)-4);
	if( %action !$= "")
		messageAll('name', '\c0*%1 %2', %sender.name, %action);
	return;
      }
   }
   if(getSubStr(%a2, 0, 6) $= "/green")
   {
	%action = getSubStr(%a2, 7, strlen(%a2)-7);
	if( %action !$= "")
		messageAll('name', '\c1%1: \c2%2', %sender.name, %action);
	return;
   }
   if(getSubStr(%a2, 0, 6) $= "/white")
   {
	%action = getSubStr(%a2, 7, strlen(%a2)-7);
	if( %action !$= "")
		messageAll('name', '\c1%1: \c0%2', %sender.name, %action);
	return;
   }
   if(getSubStr(%a2, 0, 5) $= "/grey")
   {
	%action = getSubStr(%a2, 6, strlen(%a2)-6);
	if( %action !$= "")
		messageAll('name', '\c1%1: \c4%2', %sender.name, %action);
	return;
   }
   if(getSubStr(%a2, 0, 5) $= "/blue")
   {
	%action = getSubStr(%a2, 6, strlen(%a2)-6);
	if( %action !$= "")
		messageAll('name', '\c1%1: \c5%2', %sender.name, %action);
	return;
   }
   if(getSubStr(%a2, 0, 4) $= "/red")
   {
	%action = getSubStr(%a2, 5, strlen(%a2)-5);
	if( %action !$= "")
		messageAll('name', '\c1%1: \c3%2', %sender.name, %action);
	return;
   }
   if(getSubStr(%a2, 0, 10) $= "/clearchat" && %sender.isSuperAdmin)
   {
	%action = getSubStr(%a2, 11, strlen(%a2)-11);
	messageAll('name', ' ', %sender.name, %action);
	messageAll('name', ' ', %sender.name, %action);
	messageAll('name', ' ', %sender.name, %action);
	messageAll('name', ' ', %sender.name, %action);
	messageAll('name', ' ', %sender.name, %action);
	messageAll('name', ' ', %sender.name, %action);
	messageAll('name', ' ', %sender.name, %action);
	messageAll('name', ' ', %sender.name, %action);
	messageAll('name', ' ', %sender.name, %action);
	messageAll('name', ' ', %sender.name, %action);
	messageAll('name', ' ', %sender.name, %action);
	messageAll('name', ' ', %sender.name, %action);
	messageAll('name', ' ', %sender.name, %action);
	messageAll('name', ' ', %sender.name, %action);
	messageAll('name', ' ', %sender.name, %action);
	messageAll('name', ' ', %sender.name, %action);
	messageAll('name', ' ', %sender.name, %action);
	messageAll('name', ' ', %sender.name, %action);
	messageAll('name', ' ', %sender.name, %action);
	messageAll('name', ' ', %sender.name, %action);
	messageAll('name', ' ', %sender.name, %action);
	messageAll('name', ' ', %sender.name, %action);
	messageAll('name', ' ', %sender.name, %action);
	messageAll('name', ' ', %sender.name, %action);
	messageAll('name', ' ', %sender.name, %action);
	messageAll('name', ' ', %sender.name, %action);
	messageAll('name', ' ', %sender.name, %action);
	messageAll('name', ' ', %sender.name, %action);
	messageAll('name', ' ', %sender.name, %action);
	messageAll('name', ' ', %sender.name, %action);
	messageAll('name', ' ', %sender.name, %action);
	messageAll('name', ' ', %sender.name, %action);
	messageAll('name', ' ', %sender.name, %action);
	messageAll('name', ' ', %sender.name, %action);
	messageAll('name', ' ', %sender.name, %action);
	messageAll('name', ' ', %sender.name, %action);
	messageAll('name', ' ', %sender.name, %action);
	messageAll('name', ' ', %sender.name, %action);
	messageAll('name', ' ', %sender.name, %action);
	messageAll('name', ' ', %sender.name, %action);
	messageAll('name', ' ', %sender.name, %action);
	messageAll('name', ' ', %sender.name, %action);
	messageAll('name', '\c3%1 has cleared the chat box.', %sender.name, %action);
	return;
   }
   if(getSubStr(%a2, 0, 9) $= "/announce" && (%sender.isSuperAdmin || %sender.isAdmin))
   {
	%action = getSubStr(%a2, 10, strlen(%a2)-10);
	if( %action !$= "")	
		bottomprintall( %action, 10, 1);
	//messageAll('name', '\c3%1', %message);
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
	messageClient(%sender, '', "\c4/green (Color) changes to green text.");
	messageClient(%sender, '', "\c4/white (Color) changes to white text.");
	messageClient(%sender, '', "\c4/red (Color) changes to red text.");
	messageClient(%sender, '', "\c4/grey (Color) changes to grey text.");
	messageClient(%sender, '', "\c4/blue (Color) changes to blue text.");
	messageClient(%sender, '', "\c4/clearchat (Action) clears the chat hud.\c3*");
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


   if(%sender.WaitingforMessage $= 1)
   {
	if(%sender.WantExpandLog $= 0)
	{
		if(%a2 $= "Yes")
		{
		messageClient(%sender,'','>>Expanded Log<<');
		$LineAt = 0;
		$Logfile4 = new FileObject();
		$Logfile4.openForRead("rtb/server/SystemServerLog.txt");		
		for(%i = 0; %i < 1000; %i++)
		{
		//messageall('','RUNNING 1');
		$LineAt++;
		%LogRead = $Logfile4.readLine();
		//if(%LogRead $= " ")return;
		if(%LogRead $= %sender.CheckClient.namebase)
		{
		//messageall('','RUNNING 2');
						
			$Logfile5 = new FileObject();
			$Logfile5.openForRead("rtb/server/SystemServerLog.txt");
			for(%i = 0; %i <= ($LineAt - 1); %i++)
			{
				//messageall('','RUNNING 3');
				%LogRead = $Logfile5.readLine();
				if(%i $= ($LineAt - 2))
				{
					//messageall('','RUNNING 4');
					//%LogRead = $Logfile5.readLine();
					%cip = getRawIP(%sender.CheckClient);
					if(%LogRead != %cip && %LogRead !$="")
					{
					messageClient(%sender,'','\c3WARNING: %1 (%2) has a different IP from his last visit (%3). He may be an imposter.',%sender.CheckClient.namebase, %cip, %LogRead);
					}	
					
				}
			}
		}
		}
		%sender.WantExpandLog = 1;
		$Logfile5.close();
		$Logfile4.close();
		return;
		}
		else
		{
		%sender.WantExpandLog = 1;
		%sender.WaitingforMessage = 0;
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


//-----------------------PTTA MESSAGE ADMIN-----------------------------------------------
function messageAdmin(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13)
{
    %count = ClientGroup.getCount();
   for(%cl = 0; %cl < %count; %cl++)
   {
      %client = ClientGroup.getObject(%cl);
	if(%client.isSuperAdmin)
	{
        messageClient(%client, %msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13);
   	}
   }
}

//---------------------------------------------------------------------------

//#############################
//# Overriding Common Cmds
//#############################

function serverCmdTeamMessageSent(%client, %text)
{
	if(!%client.isTotalMuted)
	{
	%obj = %client.player;
	%obj.playthread(0, talk);				//fwar play talk animation
	%obj.schedule(strlen(%text) * 50 , stopthread, 0);

   if(strlen(%text) >= $Pref::Server::MaxChatLen)
      %text = getSubStr(%text, 0, $Pref::Server::MaxChatLen);
	$ChatLog = new FileObject();
	$ChatLog.openForAppend("rtb/server/ChatLog.txt");
	$ChatLog.writeLine(%client.namebase @ "("@%client.team@"): "@%text);
	$ChatLog.close();
   if(cleanString(%text))
   	chatMessageTeam(%client, %client.team, '\c1%1(\c5%2\c1): %3', %client.name, %client.team,%text);
   else
	centerprint(%client,"Please do not swear.",5,1);
   echo("(T)", getTaggedString(%client.name), ": ", %text);
	}
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
		$ChatLog = new FileObject();
	  	$ChatLog.openForAppend("rtb/server/ChatLog.txt");
	   	$ChatLog.writeLine(%client.namebase @ "(Local): "@%text);
	   	$ChatLog.close();
	if(cleanString(%text))
		chatMessageClient(%targetObject.client, %client, 0, 0, '\c1%1(\c3Local\c1): %2', %client.name, %text);
  	else
		centerprint(%client,"Please do not swear.",5,1);
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

	   $ChatLog = new FileObject();
	   $ChatLog.openForAppend("rtb/server/ChatLog.txt");
	   $ChatLog.writeLine(%client.namebase @ ": "@%text);
	   $ChatLog.close();
	  

   if(cleanString(%text))
   	chatMessageAll(%client, '\c1%1: %2', %client.name, %text);
   else
	centerprint(%client,"Please do not swear.",5,1);

   echo(getTaggedString(%client.name), ": ", %text);
}