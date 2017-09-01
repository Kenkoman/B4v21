//-----------------------------------------------------------------------------
// GuiLobby and Game Browser Code v1.0
// Programmed by Sean Pollock aka DarkRaven
// FREE to use as long as you give me credit
// in your game and you send me an email at sean@darkravenstudios.com
//
// Main Components:
//    1.  guilobby.gui
//    2.  guilobby.cs
//    3.  tgelobby_vars.cs
//    4.  lobby_profiles.cs
//
//-----------------------------------------------------------------------------

new MessageVector(LobbyMessage);
$isConnected = false;


function GuiLobby::onWake(%this)
{
// Put Initialization of Variables here and startup code
 $IRCOpen = 1;
 irc_message.attach(LobbyMessage);
 LobbyMessage.pushBackLine("\c1"@$TgeLobby::Version,0);
 channel_list.clear(); // Clears the Channel List
 GuiLobby::onInit(); // This here initializes the Nickname and Channel SimGroups
 GuiLobby::readIgnoreLst(); // This here reads the ignore list
 $isChannel_button = false;
 $TgeLobby::RetryCount = 1;
 if($isConnected !$= true)
 {
 ConnectMe();
 }
}

function GuiLobby::onSleep(%this)
{
   // This function is called everytime TgeLobby is closed so put all
   // the cleanup code here.
   $IRCOpen = 0;
   cancelServerQuery(); // This cancels any server query's
   cancel($Chat::Schedule1);
   cancel($Chat::Schedule3);

   GuiLobby::saveIgnoreLst(); // This here saves the ignore list
   

}

function GuiLobby::readIgnoreLst()
{

   new fileobject("ignorelst");
   if(ignorelst.openForRead($TgeLobby::Directory@$TgeLobby::ListsDir@"ignore_list.lst"))
   {

         while(!ignorelst.isEOF())
         {
         %person = ignorelst.readLine();

         echo("Reading Ignore Lst: " SPC %person);
         $TgeLobby::People.add(new SimObject(%person)
                          {
                           flags = $TgeLobby::PERSON_IGNORE;
                          });
         }

   }
   else
   {
         error("Error:  Could not open ignore list");
   }
   ignorelst.close();
   ignorelst.delete();

}

function GuiLobby::saveIgnoreLst()
{
   new fileobject("ignorelst");
   ignorelst.openForWrite($TgeLobby::Directory@$TgeLobby::ListsDir@"ignore_list.lst");
   for(%i = 0; %i < $TgeLobby::People.getCount(); %i++)
   {
       %person = $TgeLobby::People.getObject(%i);
       %person = %person.getName();
       if(%person !$= $Pref::Player::Name)
       {
          if(%person.flags $= $TgeLobby::PERSON_IGNORE)
          {
                 ignorelst.writeLine(%person);

                 echo("Exporting" SPC %person SPC "to ignore list...");

          }
          
       }
   }
   // Close the fileobject
   ignorelst.close();
   ignorelst.delete();

}

function GuiLobby::onInit()
{

// This function is called on startup to create the needed SimGroups

$TgeLobby::People = new SimGroup(IRCPeople);
$TgeLobby::Channels = new SimGroup(IRCChannel);

}

function GuiLobby::goForum()
{
  // This function when called opens the users default webbrowser
  // and goes to the specified website.  You can change the website
  // from the tgelobby_vars.cs file.
  
  LobbyMessage.pushBackLine("\c1"@$TgeLobby::Forum_EntryMsg,0);
  gotoWebPage($TgeLobby::Forum);
}

function player_name2::OnSelect(%this,%id,%text)
{
   $playerid = %id;

   if(%text.flags $= $TgeLobby::PERSON_IGNORE)
      ignore_btn.setValue(1);
   else
      ignore_btn.setValue(0);


}

function player_name2::onRightMouseDown(%this,%column,%row,%mousePos)
{
   // Not implimented yet.  This here is where I am going to put
   // a action popup menu for the nickname list.

}

function ConnectMe(%this)
{
   //Checks for login name, if it finds none, then it brings up the login gui.
    if($Pref::Player::Name !$= "")
   {
      %obj = new TCPObject(myIrc);
      %obj.connect($TgeLobby::Host@":"@$TgeLobby::Port);
      if(GuiLogonDlg.isAwake())
        Canvas.popDialog(GuiLogonDlg);
   }
}

function DisconnectMe(%this)
{
   // This here is called when a nickname disconnects from the
   // server.
   
   myIrc::sendMsg("QUIT Bye");
   myIrc.disconnect();
   myIrc.delete();
   player_name2.clear();
   $isConnected = 0;
   if(GuiLogonDlg.isAwake())
   {
      Canvas.popDialog(GuiLogonDlg);
      Canvas.popDialog(GuiLobby);
   }

}

function LogonQuit(%this)
{
   // This function is called whenever a user clicks the cancel button
   // at the logon screen.
   
   if(GuiLogonDlg.isAwake())
   {
      Canvas.popDialog(GuiLogonDlg);
      Canvas.popDialog(GuiLobby);
   }
}

function myIrc::findPerson(%nick)
{

// This function is called to check if a nickname exists and
// returns the nickname if it does.

for( %i = 0; %i < IRCPeople.getCount(); %i++)
{
%person = IRCPeople.getObject(%i);
if(%person.getName() $= %nick)
   return %person.getName();
}

}

function myIrc::findChannel(%channel)
{

// This function is called to check if a channel exists and
// returns the channel if it does.

for(%i = 0; %i < $TgeLobby::Channels.getCount(); %i++)
{
    %ch = $TgeLobby::Channels.getObject(%i);

    if(%ch.getName() $= %channel)
       return %ch.getName();
}

}
function myIrc::ignore(%this)
{

  // This function is called when a user clicks the ignore button
  
  %nick = player_name2.getRowTextbyId($playerid);
  nextToken(%nick,nick2,"@"); // removes the @ from nickname

  if(%nick2 !$= $Pref::Player::Name)
  {

        if(ignore_btn.getValue() $= 1)
        {
           %nick.flags = $TgeLobby::PERSON_IGNORE;
           LobbyMessage.pushBackLine("\c2You are now ignoring "@%nick,0);
        }
        else
        if(ignore_btn.getValue() $= 0)
        {
           %nick.flags = 0;
           LobbyMessage.pushBackLine("\c2You are now speaking to "@%nick,0);
        }

        
  }
  else
  {
      LobbyMessage.pushBackLine("\c3You can not ignore yourself",0);
      ignore_btn.setValue(0);
  }

}

function myIrc::sendMsg(%command)
{
    //This simplifies the myIrc.send() function
    
   myIrc.send(%command@"\r\n");
}

function myIrc::onLine(%this, %line)
{

// This function is the heart of the irc client.  It is here that all
// irc messages and commands are passed.

myIrc::processLine(%line);

}

function myIrc::processLine(%line)
{
   // RFC_1459: Message Packet format
   //
   // <message>  ::= [':' <prefix> <SPACE> ] <command> <params> <crlf>
   // <prefix>   ::= <servername> | <nick> [ '!' <user> ] [ '@' <host> ]
   // <command>  ::= <letter> { <letter> } | <number> <number> <number>
   // <SPACE>    ::= ' ' { ' ' }
   // <params>   ::= <SPACE> [ ':' <trailing> | <middle> <params> ]
   //
   // <middle>   ::= <Any *non-empty* sequence of octets not including SPACE
   //                or NUL or CR or LF, the first of which may not be ':'>
   // <trailing> ::= <Any, possibly *empty*, sequence of octets not including
   //                   NUL or CR or LF>
   // <crlf>     ::= CR LF
   
   %src = %line;
   
      // check for prefix
      if (getSubStr(%src,0,1) $= ":")
	     %src = nextToken(getSubStr(%src,1,strlen(%src)-1),prefix," ");

	  // this is the command
	  %src = nextToken(%src,command," :");

	  // followed by its params
	  %src = nextToken(%src,params,"");
   
      if (!myIrc::dispatch(%prefix,%command,%params))
      {
	    echo("IRCClient: " @ %command @ " not handled by dispatch!");
        echo("(cmd:) " @ %prefix @ " " @ %command @ " " @ %params);
      }
      
}

function myIrc::dispatch(%prefix,%command,%params)
{

// This function dispatches the commands that are parsed to the
// appropriate functions

      switch$(%command)
      {
         case "PING":
              myIrc::onPing(%prefix,%params);

         case "PRIVMSG":
              myIrc::onPrivMsg(%prefix,%params);
              
         case "NOTICE":
              myIrc::onNotice(%prefix,%params);

         case "JOIN":
              myIrc::onJoin(%prefix,%params);
              
         case "PART":
              myIrc::onPart(%prefix,%params);
              
         case "QUIT":
              myIrc::onQuit(%prefix,%params);
              
         case "ERROR":
		      myIrc::onError(%prefix,%params);
        
         case "MODE":
              myIrc::onMode(%prefix,%params);
              
         case "INVITE":
		      myIrc::onInvite(%prefix,%params);
        
         case "TOPIC":
              myIrc::onTopic2(%prefix,%params);
              
         case "NICK":
              myIrc::onNickChange(%prefix,%params);
        
         case "303":
              myIrc::onUserOn(%prefix,%params);
              
         case "321":
              // LIST START
         case "322":
		      myIrc::onList(%prefix,%params);
        
	     case "323":
		      myIrc::onListEnd(%prefix,%params);
         
         case "331":
		      myIrc::onNoTopic(%prefix,%params);

         case "332":
              myIrc::onTopic(%prefix,%params);
              
         case "353":
		      myIrc::onNameReply(%prefix,%params);
        
         case "366":
              // End of Names
   
         case "372":
              myIrc::onMOTD(%prefix,%params);
              
         case "375":
              // MOTD Start
              
         case "376":
              myIrc::onMOTDEnd(%prefix, %params);
              
         case "401":
		      myIrc::onNoSuchNick(%prefix,%params);
              
         case "422":
              myIrc::onMOTDEnd(%prefix, %params);
              
         case "432":
              // Erroronous Nickname
              
         case "433":
              myIrc::onBadNick(%prefix,%params);
              
         case "444":
		      myIrc::onNoLogin(%prefix,%params);
              
         case "451":  // Must register a nickname first msg
             myIrc::onMustRegister(%prefix,%params);

              
         default:
              return false;
      }
      return true;
}

//-----------------------------------------------------------------------------
// End of Message Handler Stuff!
//-----------------------------------------------------------------------------

function myIrc::onPing(%prefix,%params)
{
myIrc::sendMsg("PONG "@%params);
}

function myIrc::onMustRegister(%prefix,%params)
{
LobbyMessage.pushBackLine("\c3*** Must register nickname. ***",0);
}

function myIrc::onBadNick(%prefix,%params)
{
LobbyMessage.pushBackLine("\c3*** Nickname already in use. ***",0);
}

function myIrc::onNoLogin(%prefix,%params)
{
%msg = nextToken(%params,cmd," :");
LobbyMessage.pushBackLine("\c3*** Could not log in: " SPC %msg SPC "***",0);
}

function myIrc::onPrivMsg(%prefix,%params)
{
%params = nextToken(%params,channel,": ");
%msg = %params;

// messages always lead with a ':'
if (getSubStr(%msg,0,1) $= ":")
   %msg = getSubStr(%msg,1,strlen(%msg)-1);

nextToken(%prefix,nick," !");
%newtext = "\c1<"@%nick@">" SPC %msg;
if(%nick.flags !$= $TgeLobby::PERSON_IGNORE) // Check to see if person is ignored
{
if(%channel $= $pref::Chat::Channel)
    LobbyMessage.pushBackLine(%newtext,0);
else
if(%channel $= $Pref::Player::Name)
{
    LobbyMessage.pushBackLine("\c5<"@%nick@">" SPC %msg,0);
    private_messenger.pushBackLine(%newtext,0);
}

}

}

function myIrc::onNotice(%prefix,%params)
{
%params = nextToken(%params,params,":");
LobbyMessage.pushBackLine("\c2[NOTICE] "@%params,0);
}

function myIrc::onJoin(%prefix,%params)
{
nextToken(%prefix,nick," !");

   if(%nick !$= $Pref::Player::Name) // Checks to see if this is you
   {
       if(player_name2.findTextIndex(%nick) == -1) // If it's not you then continue
       {
           if(%nick !$= $guest_name)
           {
               %newtext = "\c2[JOIN] "@%nick @ " has joined the conversation.";
               LobbyMessage.pushBackLine(%newtext,0);
               //irc_text.scrollToBottom();
               player_name2.addRow(0,%nick,player_name2.entryCount);
               player_name2.entryCount++;

               if(myIrc::findPerson(%nick) !$= %nick)
               {
                    $TgeLobby::People.add(new SimObject(%nick)
                      {
                         flags = 0;
                      });
               }


           }
       }
   }
player_name2.sort(0);

}

function myIrc::onPart(%prefix,%params)
{
nextToken(%prefix,nick," !");
if(player_name2.findTextIndex(%nick) > -1)
{
   player_name2.removeRow(player_name2.findTextIndex(%nick));
   %newtext = "\c2[PART] "@%nick@" has quit the conversation!";
   LobbyMessage.pushBackLine(%newtext,0);

}

}

function myIrc::onQuit(%prefix,%params)
{
nextToken(%prefix,nick," !");
if(player_name2.findTextIndex(%nick) > -1)
{
   player_name2.removeRow(player_name2.findTextIndex(%nick));
   %newtext = "\c2[QUIT] "@%nick@" has quit the conversation!";
   LobbyMessage.pushBackLine(%newtext,0);

}

}

function myIrc::onError(%prefix,%params)
{
  //Todo
}

function myIrc::onMode(%prefix,%params)
{
  //Todo
}

function myIrc::onNickChange(%prefix,%params)
{
   myIrc::sendMsg("NAMES");
}

function myIrc::onNameReply(%prefix,%params)
{
   // command 353
   // EXAMPLE homer128 = #GarageGames :DarkRaven Fusion^WP KM-UnDead Rick-wrk Lord-Star Apoc0410

   %params = nextToken(%params,nick,":");

   %wordcount = getWordCount(%params);
   player_name2.clear();
   for( %i = 0; %i < %wordcount; %i++)
   {
       %name = getWord(%params,%i);
       if(player_name2.findTextIndex(%name) == -1)
         {

            player_name2.addRow(%i,%name,player_text2.entryCount);
            player_name2.entryCount++;
         }
         player_name2.sort(0);
         player_name2.setSelectedRow(0);

         if(myIrc::findPerson(%name) !$= %name)
         {
                 $TgeLobby::People.add(new SimObject(%name)
                      {
                         flags = 0;
                         
                      });
         }
   }
   


}

function myIrc::onList(%prefix,%params)
{
   //EXAMPLE: :StLouis.MO.US.UnderNet.org 322 homer128 #bmx 9 :BMX Rules!

   %params = nextToken(%params,nick," ");
   %params = nextToken(%params,ch," ");
   %topic  = nextToken(%params,users," :");
   %comb = %ch TAB %users;
   
   nextToken(%ch,newch,"#");

    channel_list.addRow(channel_list.entryCount,%comb,channel_list.entryCount);
    channel_list.entryCount++;

   if(myIrc::findChannel(%newch) !$= %newch)
   {
      $TgeLobby::Channels.add(new SimObject(%newch)
                          {
                           invite = 0;
                           priv = 0;
                           users = %users;
                          });

   }
   
   channel_list.sort(0);
}

function myIrc::onListEnd(%prefix,%params)
{

}

function myIrc::onNoTopic(%prefix,%params)
{
   %params = nextToken(%params,channel," ");
   %params = nextToken(%params,channel," ");

   // Just a message
   LobbyMessage.pushBackLine("\c2[NO TOPIC] "@%channel @": No topic is set.",0);
}

function myIrc::onTopic(%prefix,%params)
{

   %params = nextToken(%params,channel," ");
   %params = nextToken(%params,channel," ");
   %params = nextToken(%params,topic,":");
   LobbyMessage.pushBackLine("\c2[TOPIC] "@%channel@" : "@%topic,0);

}

function myIrc::onTopic2(%prefix,%params)
{

   %params = nextToken(%params,channel,":");
   %params = nextToken(%params,topic,":");
   
   LobbyMessage.pushBackLine("\c2[TOPIC] "@%channel@" : "@%topic,0);

}

function myIrc::onMOTD(%prefix,%params)
{  // command 372
   // EXAMPLE :StLouis.MO.US.UnderNet.org 372 homer128 :- ==> Disclaimer/ Rules:
   %msg = nextToken(%params,prefix,":");
   LobbyMessage.pushBackLine("\c1"@%msg,0);
}

function myIrc::onMOTDEnd(%prefix, %params)
{  // command 376
   // EXAMPLE :StLouis.MO.US.UnderNet.org 372 homer128 :- ==> Disclaimer/ Rules:

   // check to see if user is in on and is ingame
   
myIrc::sendMsg("ISON "@$Pref::Player::Name@$TgeLobby::Extension);
}

function myIrc::onNoSuchNick(%prefix,%params)
{
   LobbyMessage.pushBackLine("\c3*** No Such Nick ***",0);
}

function myIrc::onUserOn(%prefix, %params)
{
%nick = $Pref::Player::Name@$TgeLobby::Extension;
%nick2 = %params;

if(%nick2 $= %nick)
{

}
else
{
   myIrc::sendMsg("NICK "@$Pref::Player::Name@$Tgelobby::extension);
   if($pref::Chat::Channel $= "") // This checks to see if $pref::Chat::Channel exists
       myIrc::sendMsg("JOIN "@$TgeLobby::Channel); // If not, then get channel from $TgeLobby::Channel
   else
       myIrc::sendMsg("JOIN "@$pref::Chat::Channel);
       

}

}

function myIrc::onConnected(%this)
{
   cancel($Chat::Schedule4);
   LobbyMessage.pushBackLine("\c1"@$TgeLobby::ConnectMsg1,0);
   $isConnected = true;
   %random = getRandom($TgeLobby::RandomSeed);
   $guest_name = "Guest"@%random;
   %this.send("NICK "@$guest_name@"\r\n");
   %this.send("USER "@$guest_name@" 0 * :"@$pref::Chat::RealName@"\r\n");
   if($pref::Chat::Channel $= "")
         channel_text.setText($TgeLobby::Channel);
   else
         channel_text.setText($pref::Chat::Channel);
   

}

function myIrc::onConnectFailed(%this)
{
   LobbyMessage.pushBackLine("\c3"@$TgeLobby::ConnectMsg3,0);
   LobbyMessage.pushBackLine("\c1Retrying #"@$TgeLobby::RetryCount@" of "@$TgeLobby::Retry@"...in 10 seconds.",0);
   if($TgeLobby::RetryCount !$= $TgeLobby::Retry)
   {

      $Chat::Schedule4 = Schedule(10 * 1000,0,"onConnectRetry");
   }
   else
       LobbyMessage.pushBackLine("\c3*** The server may be down...Please try again in 10 to 15 minutes.***",0);

}

function myIrc::onDNSFailed(%this)
{
   LobbyMessage.pushBackLine("\c3"@$TgeLobby::ConnectMsg4,0);
   LobbyMessage.pushBackLine("\c1Retrying #"@$TgeLobby::RetryCount@" of "@$TgeLobby::Retry@"...in 10 seconds.",0);
   if($TgeLobby::RetryCount !$= $TgeLobby::Retry)
   {
      $Chat::Schedule4 = Schedule(10 * 1000,0,"onConnectRetry");
   }
   else
       LobbyMessage.pushBackLine("\c3*** The server may be down...Please try again in 10 to 15 minutes.***",0);

}

function myIrc::onDNSResolved(%this)
{
   LobbyMessage.pushBackLine("\c1"@$TgeLobby::ConnectMsg2,0);
}

function sendCommand(%this)
{
   if($TgeLobby::PrvMsg $= false)
   {
   %myText = send_text.getValue();
   LobbyMessage.pushBackLine("\c1<"@$Pref::Player::Name@$Tgelobby::extension@"> "@%myText,0);
   myIrc::sendMsg("PRIVMSG "@$pref::Chat::Channel@" :"@%myText);
   send_text.setText("");
   }
   else
   if($TgeLobby::PrvMsg $= true)
   {
   %myText = send_text.getValue();
   %index = player_name2.getSelectedId();
   $messenger_name = player_name2.getRowTextById(%index);
   if(strpos($messenger_name,"@",0) $= 0)
      $messenger_name = strreplace($messenger_name,"@","");

   if($messenger_name !$= $Pref::Player::Name)
   {
        LobbyMessage.pushBackLine("\c5<"@$Pref::Player::Name@$Tgelobby::extension@"> "@%myText,0);
        myIrc::sendMsg("PRIVMSG "@$messenger_name@" :"@%myText);
   }
   else
   {
        LobbyMessage.pushBackLine("\c3You cannot send a private message to yourself.",0);
   }


   }
   send_text.setText("");
}

function myIrc::leaveRoom(%this) // function called when user clicks leave room
{

       myIrc::sendMsg("PART "@$pref::Chat::Channel);
       player_name2.clear();
       LobbyMessage.pushBackLine("\c2You have left "@$pref::Chat::Channel@".",0);
       myIrc.send("NAMES\r\n");
       channel_text.setText("<just:center><color:ffffff><shadowcolor:000000><shadow:1:1><font:arial:12>"@"No Channel");

}

function myIrc::createRoom(%chatroom,%topic)
{

   myIrc::sendMsg("JOIN #"@%chatroom);
   myIrc::sendMsg("TOPIC #"@%chatroom@" :"@%topic);
   myIrc::sendMsg("NAMES");
   myIrc::sendMsg("LIST");
   $pref::Chat::Channel = "#"@%chatroom;
   channel_text.setText("<just:center><color:ffffff><shadowcolor:000000><shadow:1:1><font:arial:12>"@$pref::Chat::Channel);
   canvas.popDialog(CreateRoom_Dlg);

}

function myIrc::goMessenger(%this) // This is executed when you click prvmsg button
{
if($TgeLobby::PrvMsg $= false)
   $TgeLobby::PrvMsg = true;
else
   $TgeLobby::PrvMsg = false;
}

function onConnectRetry()
{
myIrc.connect($TgeLobby::Host@":"@$TgeLobby::Port);
$TgeLobby::RetryCount++;
}

function ListChannels(%this)
{
if(switch_channels.getValue() $= 1)
   GuiLobby::showChannels(%this);
else
if(switch_channels.getValue() $= 0)
   GuiLobby::goRoom(%this);
}

function GuiLobby::showChannels(%this)
{
// This function brings up the channel list

channel_list.clear();
Channel_List_Dlg.setVisible(true);
myIrc::sendMsg("LIST");
switch_channels.setBitmap($TgeLobby::Directory@$TgeLobby::ButtonsDir@"go_channel");

if($pref::Chat::Channel !$= "")
   myIrc::sendMsg("NAMES "@$pref::Chat::Channel);
}

function GuiLobby::goRoom(%this)
{
   %channel_index = channel_list.getRowNumbyId(channel_list.getSelectedId());
   %channel_list = channel_list.getRowText(%channel_index);
   %channel_list = strreplace(%channel_list,"^"," ");
   %channel_list = getWord(%channel_list,0);
   if(%channel_index > -1 && %channel_list !$= "")
   {
      myIrc::sendMsg("JOIN "@%channel_list);
      $pref::Chat::Channel = %channel_list;
      channel_text.setText("<just:center><color:ffffff><font:arial:12>"@$pref::Chat::Channel);
      if($pref::Chat::Channel !$= "")
         myIrc::sendMsg("NAMES "@$pref::Chat::Channel);

      $pref::Chat::Channel = %channel_list;
   }

Channel_List_Dlg.setVisible(false);
switch_channels.setBitmap($TgeLobby::Directory@$TgeLobby::ButtonsDir@"switch_rooms");

if(%channel_list !$= "")
{
myIrc::sendMsg("JOIN "@%channel_list);
$pref::Chat::Channel = %channel_list;
channel_text.setText("<just:center><color:ffffff><shadow:1:1><font:arial:12>"@$pref::Chat::Channel);
if($pref::Chat::Channel !$= "")
   myIrc::sendMsg("NAMES "@$pref::Chat::Channel);
$pref::Chat::Channel = %channel_list;
}

}

function switch_nicks(%switch)
{
   // This here function should be used within your game to switch
   // nicks back and forth between nickname and nickname_ingame
   if(%switch $= true)
   {
       myIrc::sendMsg("NICK "@$Pref::Player::Name@$TgeLobby::Extension);
       myIrc::sendMsg("NAMES");
   }
   else
   if(%switch $= false)
   {
       myIrc::sendMsg("NICK "@$Pref::Player::Name);
       myIrc::sendMsg("NAMES");
   }
}