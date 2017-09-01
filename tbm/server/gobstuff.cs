

//************************************[gobstuff.cs]**************************************************
//*                                                                                              *
//* gobstuff.cs (by: gobbles[Chris])                                                                *
//* This is my stuff but if you want to add to it feel free and go ahead                         *
//* but don't you DARE touch what is already in here(it could ruin some features)                *
//* logs all the chats in your server and saves them in the server/chatlogs folder               *
//*                                                                                              *
//************************************************************************************************

//************************************[OnExec gobstuff.cs]*******************************************
//*                                                                                                 *
//* This OnExec Script resets the simulated clock whose value is used to timestamp entries in       *
//* the logfile written to in Function LogChat located in "tbm/server/gobstuff.cs".  This clock is  *
//* managed by function ForeverClock located in "tbm/server/gobstuff.cs".                           *
//*                                                                                                 *
//***************************************************************************************************
//cancel($foreverclock);
//cancel($displayclock);
//if($clockloaded == false) {
//  $clockstime = 0;
//  $clocksstime = 0;
//  $clockmtime = 0;
//  $clockmmtime=0;
//  $clockhtime = 0;
//  $clockhhtime=0;
//  $clockloaded = true;
//  }

//************************************[Funcion EG]************************************************
//*                                                                                              *
//* Funcion EG is used to quickly call a recompile of the script "iGobs.cs" located in the       *
//* "tbm/server/" folder.                                                                        *
//*                                                                                              *
//************************************************************************************************
function eg() {
  exec("tbm/server/gobstuff.cs");
}

//************************************[Function LogChat]******************************************
//*                                                                                              *
//* Function LogChat is use to quickly append lines to the already opened logfile                *
//* "chatlog_#.log" located in the "tbm/server/chatlogs/" folder. This logfile is opened for     *
//* write in Function Time located in "tbm/server/iGobs.cs", but it is yet unclear where the     *
//* file is closed.                                                                              *
//*                                                                                              *
//************************************************************************************************
function logchat(%clientname,%msg) {
    $Chatfile.writeline("["@$clocktime@"]"@%clientname @ ": " @ %msg);
  }

//************************************[Function CreateChatFile]***********************************
//*                                                                                              *
//* Function CreateChatFile creates the logfile that will be appened to when function LogChat    *
//* located in "tbm/server/iGobs.cs" is called.  Up to 10 log files (0-9) are stored for later   *
//* reference with each one beinging when a new session begins.  It should be noted that the     *
//* file should also be closed before a new one is opened preferable in function                 *
//* OnServerDestroyed located in "tbm/server/iGobs.cs".                                          *
//*                                                                                              *
//************************************************************************************************
function createChatfile() {
  $chatloaded = true;
  $Pref::Server::SessionNumber++;
  if($Pref::Server::SessionNumber > 10)
    $Pref::Server::SessionNumber = 0;
  $Chatfile = new FileObject();
  $Chatfile.openForWrite("tbm/server/chatlogs/chatlog_"@$Pref::Server::SessionNumber@".log");
  }

//************************************[Function DisplayTime]**************************************
//*                                                                                              *
//* The output of function DisplayTime may appear to be the same as function ClockTimer located  *
//* in "tbm/server/mcptools.cs", but it is not.  Function DisplayTime actually displays the time  *
//* being managed by function ForeverClock located in "tbm/server/iGobs.cs" on a group of 6      *
//* decals whose object numbers are set directly to the Function DisplayTime.  The object        *
//* should be ordered like a digital clock face from left to right.                              *
//* IE displaytime(#H,#h,#M,#m,#S,#s);  Later the user should be allowed to assign a sequential  *
//* doorset number to each of the 6 decals and then only send the doorset number of the first to *
//* function DisplayTime, and for better preservation of CPU resources the seconds should be     *
//* dropped.            .                                                                        *
//*                                                                                              *
//************************************************************************************************
function displaytime(%hh,%h,%mm,%m,%ss,%s) {
  $displayclock = schedule(990,0,displaytime,%hh,%h,%mm,%m,%ss,%s);
  %hh.setSkinName("white_number_"@$clockhhtime);
  %h.setSkinName("white_number_"@$clockhtime);
  %mm.setSkinName("white_number_"@$clockmmtime);
  %m.setSkinName("white_number_"@$clockmtime);
  %ss.setSkinName("white_number_"@$clocksstime);
  %s.setSkinName("white_number_"@$clockstime);
  }


//************************************[Function IRCChan]******************************************
//*                                                                                              *
//* Function IRCChan calls functions ServerIRCChangeChannal and ServerIRCupdateNick located in   *
//* "tbm/irc/main.cs".  This allows a host to quickly change the channel if server idles in as   *
//* well as updating the server nick on the IRC server.                                          *
//*                                                                                              *
//************************************************************************************************
function IRCChan(%newChannel) {
    if(strstr(%newchannel,"#") == -1 && strlen(%newchannel) > 0) {
    %newchannel = "#"@%newchannel;
      MessageAll('msgircchan',"Server IRC Channel Changed to" SPC %newchannel);
    }
    else
    MessageAll('msgircoff',"\c2IRC chat has been disabled");
  serverIRCchangeChannel(%newChannel);
  serverIRCupdateNick();
  $Pref::Server::IRC::Channel = %newChannel;
}


function getrandomclient() {
%count = ClientGroup.getCount();
   for ( %i = 0; %i < %count; %i++ ) {
      %clientIDarray[%i] = ClientGroup.getObject(%i);
      return %clientIDarray[getrandom(%count--)];
}
}


function servercmdmassroundbricks(%client) {
if(!%client.isAdmin || !%client.isSuperAdmin)
return;
      for (%current = 1; %current <= %client.iGob.total; %current++) {
      %block = %client.iGob.brick[%current];
      %blockx = roundnumber(getword(%block.position,0),1000);
      %blocky = roundnumber(getword(%block.position,1),1000);
      %blockz = roundnumber(getword(%block.position,2),1000);
      %blockrot = getwords(%block.rotation,0,2) SPC roundnumber(getword(%block.rotation,3),1000);
      %block.rotation = %blockrot;
      %block.setTransform(%blockx SPC %blocky SPC %blockz);
      }
}
function roundnumber(%number,%places) {
%number*=%places;
%number = Mfloor(%number);
%number = %number/%places;
return %number;
}

function getclientIDbyIP(%ip) {
%count = ClientGroup.getCount();
   for ( %i = 0; %i < %count; %i++ ) {
      %clientID = ClientGroup.getObject(%i);
      if(getrawip(%clientID) $= %ip)
        return %clientID;
    }
}

function testchat() {
MessageAll("msg","\c0Test0\c1Test1\c2Test2\c3Test3\c4Test4\c5Test5\c6Test6\c7Test7\c8Test8\c9Test9");
}

function colorPlayerName(%client,%forcecolor) {
%clientname = detagcolors(%client.namebase);
if(%client.isSuperAdmin)
%name = "\c9"@%clientname;
else if(%client.isAdmin)
%name = "\c6"@%clientname;
else if(%client.isMod)
%name = "\c5"@%clientname;
else if(%forcecolor)
%name = %forcecolor@%clientname;
else
%name = "\c2"@%clientname;
return %name;
}
$numEmoWords=-1;
$nonEmoWord[$numEmoWords++] = "(c)";       $EmoWord[$numEmoWords] = "©";
$nonEmoWord[$numEmoWords++] = "(r)";       $EmoWord[$numEmoWords] = "®";
$nonEmoWord[$numEmoWords++] = "(x)";       $EmoWord[$numEmoWords] = "†";
$nonEmoWord[$numEmoWords++] = "(yen)";       $EmoWord[$numEmoWords] = "¥";
$nonEmoWord[$numEmoWords++] = "(tm)";       $EmoWord[$numEmoWords] = "™";
$nonEmoWord[$numEmoWords++] = "(dot)";       $EmoWord[$numEmoWords] = "•";
$nonEmoWord[$numEmoWords++] = "(?)";       $EmoWord[$numEmoWords] = "¿";
$nonEmoWord[$numEmoWords++] = "(!)";       $EmoWord[$numEmoWords] = "¡";
$nonEmoWord[$numEmoWords++] = "(1/2)";       $EmoWord[$numEmoWords] = "½";
$nonEmoWord[$numEmoWords++] = "(1/4)";       $EmoWord[$numEmoWords] = "¼";
$nonEmoWord[$numEmoWords++] = "(1/4)";       $EmoWord[$numEmoWords] = "¼";
//$nonEmoWord[$numEmoWords++] = "()";       $EmoWord[$numEmoWords] = "";
$nonEmoWord[$numEmoWords++] = "(u)";       $EmoWord[$numEmoWords] = "µ";
$nonEmoWord[$numEmoWords++] = "(<)";       $EmoWord[$numEmoWords] = "«";
$nonEmoWord[$numEmoWords++] = "(>)";       $EmoWord[$numEmoWords] = "»";
$nonEmoWord[$numEmoWords++] = "(_)";       $EmoWord[$numEmoWords] = "¯";



function emoString(%string) {
  for(%i=0; %i<=$numEmoWords; %i++)
    %string = Strreplace( %string, $nonEmoWord[%i], $EmoWord[%i] );
  return %string;
}

$numSwearWords=-1;
$SwearWord[$numSwearWords++] = "ass";           $CleanWord[$numSwearWords] = "donkey";
$SwearWord[$numSwearWords++] = "Ass";           $CleanWord[$numSwearWords] = "Donkey";
$SwearWord[$numSwearWords++] = "ASS";           $CleanWord[$numSwearWords] = "DONKEY";
$SwearWord[$numSwearWords++] = "bitch";           $CleanWord[$numSwearWords] = "dog";
$SwearWord[$numSwearWords++] = "BITCH";           $CleanWord[$numSwearWords] = "DOG";
$SwearWord[$numSwearWords++] = "Bitch";           $CleanWord[$numSwearWords] = "Dog";
$SwearWord[$numSwearWords++] = "fuck";       $CleanWord[$numSwearWords] = "panda";
$SwearWord[$numSwearWords++] = "FUCK";       $CleanWord[$numSwearWords] = "PANDA";
$SwearWord[$numSwearWords++] = "Fuck";       $CleanWord[$numSwearWords] = "Panda";
$SwearWord[$numSwearWords++] = "shit";       $CleanWord[$numSwearWords] = "ice cream";
$SwearWord[$numSwearWords++] = "SHIT";       $CleanWord[$numSwearWords] = "ICE CREAM";
$SwearWord[$numSwearWords++] = "Shit";       $CleanWord[$numSwearWords] = "Ice Cream";
function filterswears(%string) {
  for(%i=0; %i<=$numSwearWords; %i++)
    %string = Strreplace( %string, $SwearWord[%i], $CleanWord[%i] );
  return %string;
}

//****************************************[Function execsth****]**********************************
//*                                                                                              *
//* Function execsth can be used as a quicker means of exec'ing files. It is used by inputting   *
//* a variable assigned as "%filename". To use execsth, put this in the console                  *
//* window: execsth("[filename].cs"); or gui scripts can be exec'ed by switching out the ".cs"   *
//* with a ".gui".                                                                               *
//*                                                                                              *
//************************************************************************************************
function execsth(%filename) {
if(%filename $= "" && $lastexec !$= "") {
execsth($lastexec);
return;
}

//for(%file = findfirstfile("*/"@%filename); %file !$= ""; %file = findnextfile("*/"@%filename))
//exec(%file);  //This is bad.  I learned it the hard way. -Wiggy

for(%file = findfirstfile("*/"@%filename); %file !$= ""; %file = findnextfile("*/"@%filename))
  %f[%i++] = %file;  //If you executed any files here that call findFirstFile you'll get an infinite loop.
for(%j = 1; %j <= %i; %j++) 
  exec(%f[%j]);

$lastexec = %filename;
}


//function servercmdcopybrick(%client) {
//    if(%client.isMod || %client.isAdmin || %client.isSuperAdmin) {
//        %oldbrick = %client.
//    }
//}


//***********************Doo Dee's Anonymous Server stuff***********************
//*                                                                            *
//* To start the anonymous server, type into the console anonserver();         *
//* To turn off the anonymous server, type into the console deanonserver();    *
//* Hope you like :D                                                           *
//******************************************************************************

$PresetTotal = -1;
$PresetName[$PresetTotal++] = "A Duck";
$PresetName[$PresetTotal++] = "Mr. Simpson";
$PresetName[$PresetTotal++] = "Johnny";
$PresetName[$PresetTotal++] = "Steve";
$PresetName[$PresetTotal++] = "Sir Robin";
$PresetName[$PresetTotal++] = "MacMan";

$PreCharTot = -1;
$PresetChar[$PreCharTot++] = "6,5,-1,-1,-1,6,0,27,81,logo_rolling,female1,4";
$PresetChar[$PreCharTot++] = "20,7,-1,-1,6,9,0,34,0,Western_Bow,badguy3,2";
$PresetChar[$PreCharTot++] = "0,-1,-1,-1,-1,0,0,0,0,tbm,base,6";
package AnonymousServer {

function GameConnection::setRandomName(%this) {
if(nameAlreadySet(%this.namebase))
return;
for(%i=0;%i<$PresetTotal;%i++) {
%name = $PresetName[getRandom($PresetTotal)];
if(isNameUnique(%this,%name)) {
%this.setPlayername(emostring(%name));
return;
}
else
continue;
}
}
function nameAlreadySet(%name) {
    for(%i=0;%i<$PresetNameTotal;%i++) {
        if(%name $= $PresetName[%i])
            return true;
        }
    return false;
}
function GameConnection::onConnect( %client, %name )
{
    //%client.setRandomName();
	%client.connected = 1;
	commandToClient(%client, 'updatePrefs');
    messageClient(%client,'MsgConnectionError',"",$Pref::Server::ConnectionError);
    //commandToClient(%client,'MsgLoadimage', nametoid("MissionInfo").preview);
   // Send mission information to the client
   sendLoadInfoToClient( %client );

   %client.guid = 0;
   addToServerGuidList( %client.guid );

   // Set admin status
   if (%client.getAddress() $= "local") {
      %client.isAdmin = true;
      %client.isSuperAdmin = true;
   }
   else {
      %client.isAdmin = false;
      %client.isSuperAdmin = false;
   }
   // Save client preferences on the connection object for later use.
   %client.gender = "Male";
   %client.armor = "Light";
   %client.race = "Human";
   %client.skin = addTaggedString( "base" );
   //%client.setPlayerName(%name);
   %client.score = 0;
   
   %client.namebase = "Anonymous";
   %client.name = addTaggedString("\cp\c8" @ %client.namebase @ "\co");

   $instantGroup = ServerGroup;
   $instantGroup = MissionCleanup;
   echo("CADD: " @ %client @ " " @ %client.getAddress());

   // Inform the client of all the other clients
   %count = ClientGroup.getCount();
   for (%cl = 0; %cl < %count; %cl++) {
      %other = ClientGroup.getObject(%cl);
      if ((%other != %client)) {
         // These should be "silent" versions of these messages...
         messageClient(%client, 'MsgClientJoin', "",
               %other.name,
               %other,
               %other.sendGuid,
               %other.score,
               %other.isAIControlled(),
               %other.isAdmin,
               %other.isSuperAdmin,
               %other.isMod);
      }
   }

   // Inform the client we've joined up
   messageClient(%client,
      'MsgClientJoin', "\c2"@$Pref::Server::Motd,
      %client.name,
      %client,
      %client.sendGuid,
      %client.score,
      %client.isAiControlled(),
      %client.isAdmin,
      %client.isSuperAdmin,
      %client.isMod);
   serverIRCsendInfoLines(%client);
    if($dtbserver == 1)
    messageClient(%client,'MsgDtbrunning',"\c2Server is running DtB.");
   // Inform all the other clients of the new guy
   messageAllExcept(%client, -1, 'MsgClientJoin', '\c3%1\c2 has connected to the server.',
      %client.name,
      %client,
      %client.sendGuid,
      %client.score,
      %client.isAiControlled(),
      %client.isAdmin,
      %client.isSuperAdmin,
      %client.isMod);
   serverIRCannounce(%client.namebase @ " has connected to the server.");

   // If the mission is running, go ahead download it to the client
   if ($missionRunning)
      %client.loadMission();
   $Server::PlayerCount++;
}

function ServerCmdUpdatePrefs(%client, %name, %skin, %headCode, %visorCode, %backCode, %leftHandCode, %headCodeColor, %visorCodeColor, %backCodeColor, %leftHandCodeColor, %chestdecalcode, %facedecalcode, %bodytype)
{
    //do nothing? nahhh!!!!!
    if($RandomPreset)
    eval("updateAnonPrefs("@%client@", "@%name@","@ $PresetChar[getRandom($PreCharTot)]@");");
    else {
    %local = getlocal();
    %client.namebase = "Anonymous";
    %client.name = addTaggedString("\cp\c8" @ %client.namebase @ "\co");
 	%client.colorSkin	= %local.colorSkin;
	%client.headCode	= %local.headCode;
	%client.visorCode	= %local.visorCode;
	%client.backCode	= %local.backCode;
	%client.leftHandCode= %local.leftHandCode;
    %client.bodytype    = %local.bodytype;
	%client.headCodeColor	= %local.headCodeColor;
	%client.visorCodeColor	= %local.visorCodeColor;
	%client.backCodeColor	= %local.backCodeColor;
    %client.leftHandCodeColor = %local.leftHandCodeColor;
	%client.chestdecalcode		= %local.chestdecalcode;
	%client.facedecalcode   = %local.facedecalcode;
    %bodytype = %local.bodytype;
    servercmdarmsncrotch(%client,%bodytype);
    
	%player = %client.player;
	if(isObject(%player))
	{
		if($Server::MissionType $= "TBMSandBox" && %client.player)
		{
			%player.unMountImage($headSlot);
			%player.unMountImage($visorSlot);
			%player.unMountImage($backSlot);
			%player.unMountImage($leftHandSlot);
			%player.unMountImage($chestSlot);
			%player.unMountImage($faceSlot);
			%player.unMountImage(7);
            if (%client.team $= "")
		 	  %player.setSkinName(%client.colorSkin);
			%player.mountImage(%client.headCode, $headSlot, 1, %client.headCodeColor);
			%player.mountImage(%client.visorCode, $visorSlot, 1, %client.visorCodeColor);
			%player.mountImage(%client.backCode, $backSlot, 1, %client.backCodeColor);
            %player.mountImage(%client.leftHandCode, $leftHandSlot, 1, %client.leftHandCodeColor);
			%player.mountImage(%client.chestCode, $chestSlot, 1, %client.chestdecalcode);
			%player.mountImage(%client.faceCode, $faceSlot, 1, %client.facedecalcode);
        }
    %player.setShapeName("Anonymous");
	}
	messageAll('MsgClientJoin', '',
			  %client.name,
			  %client,
			  %client.sendGuid,
			  %client.score,
			  %client.isAiControlled(),
			  %client.isAdmin,
			  %client.isSuperAdmin,
			  %client.isMod);
    }
}
function updateAnonPrefs(%client, %name, %skin, %headCode, %visorCode, %backCode, %leftHandCode, %headCodeColor, %visorCodeColor, %backCodeColor, %leftHandCodeColor, %chestdecalcode, %facedecalcode, %bodytype)
{
if (%client.poon || %client.carrier)
  return;
        if (getsubstr(%facedecalcode,0,6) $= "female" || %facedecalcode $= "town4")
          %client.gender=1;
        else
          %client.gender=0;
    %client.setRandomName();
	%client.colorSkin	= $legoColor[%skin];
	%client.headCode	= $headCode[%headCode];
	%client.visorCode	= $visorCode[%visorCode];
	%client.backCode	= $backCode[%backCode];
	%client.leftHandCode    = $leftHandCode[%leftHandCode];
	%client.chestCode       = chestShowImage;
	%client.faceCode        = faceplateShowImage;
    %client.bodytype = %bodytype;
    servercmdarmsncrotch(%client,%bodytype);

	%client.headCodeColor	= addTaggedString($legoColor[%headCodeColor]);
	%client.visorCodeColor	= addTaggedString($legoColor[%visorCodeColor]);
	%client.backCodeColor	= addTaggedString($legoColor[%backCodeColor]);
    %client.leftHandCodeColor = addTaggedString($legoColor[%leftHandCodeColor]);
	%client.chestdecalcode		= addTaggedString(%chestdecalcode);
	%client.facedecalcode   	= addTaggedString(%facedecalcode);
	%player = %client.player;
	if(isObject(%player))
	{
		if($Server::MissionType $= "TBMSandBox" && %client.player)
		{


			%player.unMountImage($headSlot);
			%player.unMountImage($visorSlot);
			%player.unMountImage($backSlot);
			%player.unMountImage($leftHandSlot);
			%player.unMountImage($chestSlot);
			%player.unMountImage($faceSlot);
			%player.unMountImage(7);
            if (%client.team $= "")
		 	  %player.setSkinName(%client.colorSkin);
			%player.mountImage(%client.headCode, $headSlot, 1, %client.headCodeColor);
			%player.mountImage(%client.visorCode, $visorSlot, 1, %client.visorCodeColor);
			%player.mountImage(%client.backCode, $backSlot, 1, %client.backCodeColor);
            %player.mountImage(%client.leftHandCode, $leftHandSlot, 1, %client.leftHandCodeColor);
			%player.mountImage(%client.chestCode, $chestSlot, 1, %client.chestdecalcode);
			%player.mountImage(%client.faceCode, $faceSlot, 1, %client.facedecalcode);
        }
    %player.setShapeName(emostring(%client.name));

	}

	//update everyone's scoreboard with the new name
	messageAll('MsgClientJoin', '',
			  %client.name,
			  %client,
			  %client.sendGuid,
			  %client.score,
			  %client.isAiControlled(),
			  %client.isAdmin,
			  %client.isSuperAdmin,
			  %client.isMod);
  if (!%client.secure) {
     echo("not secure");
     if (getRawIP(%client) $= "local")
       messageClient(%client, 'Msg', "\c2Dude your version number is messed up, no one will be able too connect to you, until you fix it.");
     else
       %client.delete("Goto http://www.theorangeblock.org to get the latest version");
     }
}
function bluePaintProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal){
    %client = %obj.client;
    if(%col.getType() == 67108869 || %col.getType() == 67108873)
	   return;
    if (%col.getClassName() $= "AIPlayer" ||
    %col.getDataBlock().classname $= "brick" ||
    %col.getDataBlock().classname $= "baseplate")
    {
  if (getSubstr(%col.getSkinName(),0,5) $= "ghost"|| %col.getDataBlock().decal $= "1")
        return;
    if (!%col.permbrick || %client.isadmin || getrawip(%client) $= %col.owner) {
      if (%col.getClassName() $= "Player" && %col.client.isadmin && !%obj.client.issuperadmin) {
        if (%obj.client.player != %col && !%obj.client.poon && !%col.client.poon)
          punishment2of(%col.client,%obj.client);
        return;
        }
      %col.setskinname(%obj.client.brickcolor);
      if (getRawIP(%obj.client) !$= %col.owner)
        recordsprayabuse(%obj.client,%col);
      }
  }
}
};
function anonserver() {
activatePackage(AnonymousServer);
%count = ClientGroup.getCount();
   for (%cl = 0; %cl < %count; %cl++) {
      %client = ClientGroup.getObject(%cl);
      servercmdupdateprefs(%client,"Anonymous");
      }
}
function deanonserver() {
deactivatePackage(AnonymousServer);
%count = ClientGroup.getCount();
   for (%cl = 0; %cl < %count; %cl++) {
      %client = ClientGroup.getObject(%cl);
      commandtoclient(%client,'updateprefs');
      }
   }
function getlocal() {
%count = ClientGroup.getCount();
   for (%cl = 0; %cl < %count; %cl++) {
      %client = ClientGroup.getObject(%cl);
      if(getRawIP(%client) $= "local")
      return %client;
      }
   }
   
function strWordlen(%str) {
    while(getWord(%str,%i) !$= "")
    %i++;
    return %i;
}

function servercmdgetInventoryInfo(%client,%slot) {
%slot--;
%first = $bricks[%slot,0];
%last = $bricks[%slot,1];
for(%i=%first;%i<=%last;%i++){
%brickID = %i;
%brickname = %i.invname;
commandtoclient(%client,'BuildInvlist',%brickID,%brickname);
}
}
function servercmdquickchangebrick(%client,%brickid) {
if(!%client.player.tempbrick) {
%client.player.brick[%client.player.currWeaponSlot] = %brickID;
return;
}
%client.lastquickbrick = %brickid;
if(%brickid < $bricks[0,0] || %brickid > $bricks[9,1])
return;
%client.player.tempbrick.setdatablock(%brickid);
if(%brickid <= $bricks[0,1])
%client.player.tempbrick.setscale("1 1 "@%client.hscale);
else
%client.player.tempbrick.setscale("1 1 1");
}

// Test function to see if we can get saving with box shapes
// instead of the sphere, and still make it quick.
function InitBoxContainerSearch(%client) {

    %iGob = %client.iGob;
    %iGobxmin = getword(%iGob.getworldbox(),0);
    %iGobxmax = getword(%iGob.getworldbox(),3);
    %iGobymin = getword(%iGob.getworldbox(),1);
    %iGobymax = getword(%iGob.getworldbox(),4);
    %iGobzmin = getword(%iGob.getworldbox(),2);
    %iGobzmax = getword(%iGob.getworldbox(),5);

        //Check to make sure this object is what we're looking for
        %radius = vectordist(%iGob.getWorldBoxCenter(),getwords(%igob.getworldbox(),3,5));
		InitContainerRadiusSearch(%iGob.position, %radius , $TypeMasks::StaticShapeObjectType);
		while ((%block = containerSearchNext()) != 0){
			if (%block.dataBlock.classname $= "brick")
			    %object[%i++] = %block;
		}
        InitContainerRadiusSearch(%iGob.position, %radius, $TypeMasks::ItemObjectType);
		while ((%block = containerSearchNext()) != 0){
			if (%block.getClassName()$="Item"||%block.dataBlock.category$="DM"||%block.dataBlock.category$="Crown")
			    %object[%i++] = %block;
		}
        InitContainerRadiusSearch(%iGob.position, %radius , $TypeMasks::StaticShapeObjectType);
		while ((%block = containerSearchNext()) != 0){
			if (%block.getClassName() $= "AIplayer")
			    %object[%i++] = %block;
		}
        for(%b=1;%b<=%i;%b++) {
        //%xmin = getword(%object[%b].getworldbox(),0);
        //%xmax = getword(%object[%b].getworldbox(),3);
        //%ymin = getword(%object[%b].getworldbox(),1);
        //%ymax = getword(%object[%b].getworldbox(),4);
        //%zmin = getword(%object[%b].getworldbox(),2);
        //%zmax = getword(%object[%b].getworldbox(),5);
        %x = getword(%object[%b].getworldboxcenter(),0);
        %y = getword(%object[%b].getworldboxcenter(),1);
        %z = getword(%object[%b].getworldboxcenter(),2);
            if(checkcorner(%x,%y,%z,%iGobxmin,%iGobxmax,%iGobymin,%iGobymax,%iGobzmin,%iGobzmax) == false)
            continue;
                if(%client.isSuperAdmin||(%client.isAdmin && (%object[%b].iGobperm == 0))||%object[%b].owner $= getrawip(%client))
                %client.iGob.brick[%client.iGob.Total++] = %object[%b];
                else
                %badcount++;
        }
    if(%mask $= $TypeMasks::StaticShapeObjectType && %badcount > 0)
        MessageClient(%client,'msgofghey','\c2You were unable to select/save \c3%1\c2 object(s)',%badcount);
}

function checkCorner(%x,%y,%z,%xmin,%xmax,%ymin,%ymax,%zmin,%zmax) {
if(%x < %xmin || %x > %xmax || %y < %ymin || %y > %ymax || %z < %zmin || %z > %zmax)
return false;
else
return true;
}

function bricktypecount() {
    for (%i=0; %i<missioncleanup.getcount(); %i++) {
    %block = missioncleanup.getObject(%i);
    if (%block.dataBlock.classname $= "brick" || %block.dataBlock $= "portculyswitch")
        %brick[%count++] = %block;
    }
    for(%i=1;%i<=%count;%i++) {
    %brick = %brick[%i].getdataBlock();
    %bricklist = strreplace(%bricklist,%brick@" ","");
    %bricklist = %bricklist@%brick@" ";
    }
    %num = getWordcount(%bricklist);
echo("I found "@%num@" different bricks!");
}

function servercmdiGobtype(%client,%type) {
    %client.iGobtype = %type;
    if(isObject(%client.iGob)) {
        if(%type $= "Cube")
        %client.iGob.setdatablock("iGobCentroid");
        else {
        %client.iGob.setDatablock("iGobsphere");
        %client.iGob.setScale("1 1 1");
        }
    }
}
