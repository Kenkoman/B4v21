function ghostplayer(%name) {
  //$pref::server::maxplayers++;
  $Server::PlayerCount++;
  %client = new AIConnection () { };
  %client.nameBase = %name;
  %client.name = addTaggedString("\cp\c8" @ %name @ "\co");
  %client.guid = 0;
  %client.isadmin=1;
  addToServerGuidList( %client.guid );
  //ClientGroup.add(%client);
  %client.score=0;
  $gbc++;
  $ghostbots[$gbc]=%client;
   messageAllExcept(%client, -1, 'MsgClientJoin', '\c2%1 joined the game.',
      %client.name, 
      %client,
      %client.sendGuid,
      %client.score,
      %client.isAiControlled(), 
      %client.isAdmin, 
      %client.isSuperAdmin,
      %client.isMod);
  }

function msg(%str) { messageAll('msgwhatever', "\c2"@$pref::server::hostername@": \c8"@%str); }

function msgf(%str) { messageAll('msgwhatever', %str); }

function plist() { 
  for( %i = 0; %i < ClientGroup.getCount(); %i++) {
    %client = ClientGroup.getObject(%i);
    echo("Client ID: "@%client@" for "@%client.namebase@"  score of: "@%client.score);
    }
}

function chgn(%client,%name)  {
        %client.setPlayerName(%name);
        %client.player.setShapeName(%name);
              messageAll('MsgClientJoin', '', 
			  %client.name, 
			  %client,
			  %client.sendGuid,
			  %client.score,0,
			  %client.isAdmin, 
			  %client.isSuperAdmin,
              %client.isMod);
}
////////////////////////////////////////////////////////////////////////////
//BY MCP:  This function allows for recusive counting with the type decal.
//         You don't have to worry about this funciton because I use it 
//         else where, but if you are interested, it is fed an obj#, a color
//         string, a dealy time in miliseconds, the limit at which the 
//         coutner reverts to 0, and the current count of the object.  Yes
//         I know I could have just pulled that off with getskinname(), so
//         shut up. :)
////////////////////////////////////////////////////////////////////////////
function dcount(%obj,%color,%time,%limit,%count) {
  if (isobject(%obj)) {
    if (%count==10)
      %count=0;
//this wasn't applying %color right in the function that follows, and I like lime -DShiznit
    %obj.setskinname("lime_number_"@%count);
    %count++;
    if (%count>%limit)
      %count=0;
    %obj.cl=schedule(%time,0,dcount,%obj,%color,%time,%limit,%count);
    }
}

////////////////////////////////////////////////////////////////////////////
//BY MCP:  Given the first doorset number in the series of sequential doorset 
//         numbers, this function will cancel any schedules pending on the set
//         of decals.
////////////////////////////////////////////////////////////////////////////
function timerkill(%set) {
  %count = MissionCleanup.getCount();
  for (%i = 1; %i < %count; %i++) {
    %block = MissionCleanup.getObject(%i);
    for (%x = 0; %x <= 6; %x++) 
      if (%block.port == %set + %x) 
	cancel(%block.cl);
    }
}

////////////////////////////////////////////////////////////////////////////
//BY MCP:  Given the first doorset number in the series of sequential doorset 
//         numbers, this function will cancel any schedules pending on the set
//         of decals then it will calulate the the ammount of time each digit
//         must wait before begining its recursive counting.  This is because
//         if there are 15 seconds on the clock then the minitue marker isn't
//         going to wait 60 seconds to invcement like normal.  Instead it will
//         wait for 45 seconds then it will being wiating for 60 seconds for 
//         subsequent calls.
//
//	   Setup: 1. Set out 7 type decals.
//	          2. Put numbers on them, all 0's if you like.
//	          3. Assign some bigass doorset number to the 100's of hours
//                   decal that you know no one will use like 78230.
//                4. Increment this number by one and assign it to the 10's
//                   of hours decal. EI: 78231
//                5. Incement and repeat for each lower measure of time.
//                6. In the consol type clocktimer(set#); where set# is your 
//                   first set number for the 100's of hours.  EI: 78230
//                *  Lucky for you the save will keep this info for you, so
//                   you will only have to use the command to restart it when
//                   you reload the save file.
////////////////////////////////////////////////////////////////////////////
function clocktimer(%set) {
  %color=getsubstr(%block.getskinname(),0,5);
  timerkill(%set);
  %count = MissionCleanup.getCount();
  for (%i = 1; %i < %count; %i++) {
    %block = MissionCleanup.getObject(%i);
    for (%x = 0; %x <= 6; %x++) {
      if (%block.port == %set + %x) {
	%myclock[%x]=%block;
        %precount[%x]=getsubstr(%block.getskinname(),13,1);
        %color[%x] = getsubstr(%block.getskinname(),0,5);
        }
      }
    }

  %pretime[5]=(10-%precount[6])*1000;
  %precount[6]++;
  if (%precount[6]>9)
    %precount[6]=0;

  %pretime[4]=(5-%precount[5])*10000+%pretime[5];
  %precount[5]++;
  if (%precount[5]>5)
    %precount[5]=0;

  %pretime[3]=(9-%precount[4])*60000+%pretime[4];
  %precount[4]++;
  if (%precount[4]>9)
    %precount[5]=0;

  %pretime[2]=(5-%precount[3])*600000+%pretime[3];
  %precount[3]++;
  if (%precount[3]>5)
    %precount[3]=0;

  %pretime[1]=(9-%precount[2])*3600000+%pretime[2];
  %precount[2]++;
  if (%precount[2]>9)
    %precount[2]=0;

  %pretime[0]=(9-%precount[1])*36000000+%pretime[1];
  %precount[1]++;
  if (%precount[1]>9)
    %precount[1]=0;

  %precount[0]++;
  if (%precount[0]>9)
    %precount[0]=0;

  %myclock[0].cl=schedule(%pretime[0],0,dcount,%myclock[0],%color[0],360000000,9,%precount[0]);
  %myclock[1].cl=schedule(%pretime[1],0,dcount,%myclock[1],%color[1],36000000,9,%precount[1]);
  %myclock[2].cl=schedule(%pretime[2],0,dcount,%myclock[2],%color[2],3600000,9,%precount[2]);
  %myclock[3].cl=schedule(%pretime[3],0,dcount,%myclock[3],%color[3],600000,5,%precount[3]);
  %myclock[4].cl=schedule(%pretime[4],0,dcount,%myclock[4],%color[4],60000,9,%precount[4]);
  %myclock[5].cl=schedule(%pretime[5],0,dcount,%myclock[5],%color[5],10000,5,%precount[5]);
  %myclock[6].cl=schedule(1000,0,dcount,%myclock[6],%color[6],1000,9,%precount[6]);
}
////////////////////////////////////////////////////////////////////////////
//BY MCP:  This is just the recursive function for moving the hands of the 
//         clock, but I suppose you could us it for other things such as
//         creating looping fans and such.
////////////////////////////////////////////////////////////////////////////
function tiktoc (%obj,%delay,%degrees) {
  if (!isobject(%obj))
    return;
  %obj.rotsav=rotaddup(%obj.rotsav,%degrees);
  %obj.settransform(%obj.getposition()@" "@rotconv(%obj.rotsav));
  %obj.cl = schedule(%delay,0,tiktoc,%obj,%delay,%degrees);
}

function dingdong (%hrs) {
//sound dongs
//reschedule
  if (%hrs==12)
    %hrs=0;
  else
    %hrs++;
  schedule(1000*60*60,0,dingdong,%hrs);
}

////////////////////////////////////////////////////////////////////////////
//BY MCP:  Given the first doorset number in the series of sequential doorset 
//         numbers, this function will cancel any schedules pending on the set
//         clock hands.
////////////////////////////////////////////////////////////////////////////
function cfk (%set) {
  %count = MissionCleanup.getCount();
  for (%i = 1; %i < %count; %i++) {
    %block = MissionCleanup.getObject(%i);
    if (%block.port == %set) 
	cancel(%block.cl);
    else if (%block.port == %set + 1) 
	cancel(%block.cl);
    else if (%block.port == %set + 2) 
	cancel(%block.cl);
    }
}
////////////////////////////////////////////////////////////////////////////
//BY MCP:  Given the first doorset number in the series of sequential doorset 
//         numbers, this function will cancel any schedules pending on the set
//         of clock hands.  This function is simple but powerful, so don't let
//         it overwelm you.  Unless you need your clock to increment negatively
//         on the selected axis you must put a 1 in for %rev, otherwise you don't
//         have to enter a number at all.  Now this function will start, orient,
//         and resync your clock as it does slowly get off as there is some 
//         processing time associated with each tick.  
//	   Setup: 1. Make one clock hand at twelve o'clock with the rotation 
//                   point in the center of the face.
//	          2. Create the rest of the hands with the rotation points all
//                   in the center. Meaning they are all touching there.
//                3. By now you should know what axis you are rotating them on
//                   that identifies what time it is.  Remember it because it will
//                   be the axis you use when you start he clock
//                4. Now pull out your clock and use the command resync(a,b,c,d,e,f);
//			a = the first doorset number in the series of sequential doorset 
//         		    numbers which in this case should be the hour hand.
//			b = number of hours appearing on your system clock (0 for 12)
//			c = number of minutes that will be on your clock when you start
//			    it.
//			d = remember that axis I told you to remember? Enter it as a string 
//			    here. IE: "x" for the X axis.
//			e = This is the number of degrees for the axis you are using when
//			    it is pointing at 12 o'clock.  Check your admin panel after you
//			    select that ONE clock hand you pointed to 12 in step 1.
//			f = If you clock need to move backward along its selected axis
//                          put 1 here otherwise make it 0 or don't bother with it at all.
////////////////////////////////////////////////////////////////////////////
function resync (%set,%hours,%minutes,%axis,%ori,%rev) {
  cfk (%set);
  %hcount = -1;
  %mcount = -1;
  %scount = -1;
  %count = MissionCleanup.getCount();
  for (%i = 1; %i < %count; %i++) {
    %block = MissionCleanup.getObject(%i);
    if (%block.port == %set) 
	%hrs[%hcount++]=%block;
    else if (%block.port == %set + 1) 
	%min[%mcount++]=%block;
    else if (%block.port == %set + 2) 
	%sec[%scount++]=%block;
    }
  switch$ (%axis) {
    case "x":
	%degrees="1 0 0";
    case "y":
	%degrees="0 1 0";
    case "z":
	%degrees="0 0 1";	
    }
  if (%rev)
    vectorscale(%degrees,-1);
  %pmin=%minutes*6+%ori;
  %phrs=%hours*30+%minutes*0.5+%ori;
  for (%i = 0; %i <= %scount; %i++) {
    switch$ (%axis) {
      case "x":
	%sec[%i].rotsav=vectoradd(vscale(%sec[%i].rotsav,"0 1 1"),%ori @ " 0 0");
      case "y":
	%sec[%i].rotsav=vectoradd(vscale(%sec[%i].rotsav,"1 0 1"),"0 " @ %ori @ " 0");
      case "z":
	%sec[%i].rotsav=vectoradd(vscale(%sec[%i].rotsav,"1 1 0"),"0 0 " @ %ori);
      }
        %sec[%i].cl = schedule(1000,0,tiktoc,%sec[%i],1000,vectorscale(%degrees,6)); 
    }
  for (%i = 0; %i <= %mcount; %i++) {
    switch$ (%axis) {
      case "x":
	%min[%i].rotsav=vectoradd(vscale(%min[%i].rotsav,"0 1 1"),%pmin @ " 0 0");
      case "y":
	%min[%i].rotsav=vectoradd(vscale(%min[%i].rotsav,"1 0 1"),"0 " @ %pmin @ " 0");
      case "z":
	%min[%i].rotsav=vectoradd(vscale(%min[%i].rotsav,"1 1 0"),"0 0 " @ %pmin);
      }
    %min[%i].settransform(%min[%i].getposition()@" "@rotconv(%min[%i].rotsav));
    %min[%i].cl = schedule(10000,0,tiktoc,%min[%i],10000,%degrees);
    }

  for (%i = 0; %i <= %hcount; %i++) {
    switch$ (%axis) {
      case "x":
	%hrs[%i].rotsav=vectoradd(vscale(%hrs[%i].rotsav,"0 1 1"),%phrs @ " 0 0");
      case "y":
	%hrs[%i].rotsav=vectoradd(vscale(%hrs[%i].rotsav,"1 0 1"),"0 " @ %phrs @ " 0");
      case "z":
	%hrs[%i].rotsav=vectoradd(vscale(%hrs[%i].rotsav,"1 1 0"),"0 0 " @ %phrs);
      }
    %hrs[%i].settransform(%hrs[%i].getposition()@" "@rotconv(%hrs[%i].rotsav));
    %hrs[%i].cl=schedule(60000,0,tiktoc,%hrs[%i],60000,vectorscale(%degrees,0.5));
    }
}

function vscale(%v1,%v2) {
  %x1=getword(%v1,0);
  %x2=getword(%v2,0);
  %y1=getword(%v1,1);
  %y2=getword(%v2,1);
  %z1=getword(%v1,2);
  %z2=getword(%v2,2);
  %ret = %x1*%x2 SPC %y1*%y2 SPC %z1*%z2;
  return(%ret);
}

$numBannedWords=-1;

function cleanString(%string) {   
  for(%i=0; %i<=$numBannedWords; %i++) 
    %string = Strreplace( %string, $bannedWords[%i], $replacementWord[%i] );      
  return %string;
  }
function serverCmdMessageSent(%client, %text)
{
	%obj = %client.player;
	//fwar play talk animation
    //   if (!%obj.sitting) {
	// %obj.playthread(0, talk);
	// %obj.schedule(strlen(%text) * 50, stopthread, 0);
     //   }
   if(strlen(%text) >= $Pref::Server::MaxChatLen)
      %text = getSubStr(%text, 0, $Pref::Server::MaxChatLen);
      %num = 0;

   %text = emoString(%text);
   %clientname = colorPlayerName(%client);
   
   if (!%client.issuperadmin) {
     %text = cleanString(%text);
     for(%word=getWord(%text,%num);%num<getWordcount(%text);%word=getWord(%text,%num++)) {
//    if(%word $= "lag" || %word $= "Iag")
//    return;
    }
   }

      if($Pref::Server::FilterSwears)
     %text = filterSwears(%text);
   if (%client.shutup==1) {
	%text = "I am sorry MCP you are right, I will shut up now";
         %client.shutup++;
        }
   else if (%client.shutup==2) {
     %text = "/me shuts up";
         %client.shutup++;
     }
   else if (%client.shutup==3) {
     %text = "/me is really stupid";
         %client.shutup--;
     }
   if (strstr(%text,"/taunt ") == 0) {
     %text = Strreplace( %text, "/taunt ", "");      
     if (%client.bodytype==666 && %client.player.getState() $= "Move")
       %client.player.playAudio(0,"PredTaunt"@%text);
     }
   if(!%client.secure)
     return;
   else if (strstr(%text,"/me ") == 0) {
     %text = Strreplace( %text, "/me ", "");      
     serverIRCchat("*" @ %clientname @ " " @ %text);
     chatMessageAll(%client, '\c1*%1\c1 %2', %clientname, %text);
     return;
     }
   else if (strstr(%text,"/pm ") == 0) {
     %text = Strreplace( %text, "/pm ", "");
     %pmclient = getword(%text,0);
     %text = Strreplace( %text, %pmclient @ " ", "");
     %pmclient = findclientbyname(detagcolors(%pmclient));
     if (isobject(%pmclient)) {
       servercmdpmclient(%client,%pmclient,%text);
	for (%clientnumber = 0; %clientnumber < ClientGroup.getCount(); %clientnumber++) {      
	    %victim = ClientGroup.getObject( %clientnumber );     
		%ip = getRawIP(%victim);
	messageclient(%victim,' ','(From %1 - To %2) %3', nametoid(%client), nametoid(%pmclient), %text);

	}
	}
     return;
     }

   else if (strstr(%text,"/pull") == 0 && %client.isadmin) {
      if(isObject($Bodytype[%client.bodyType])) {
         %p = new AIPlayer() {
            dataBlock = $Bodytype[%client.bodyType];
            aiPlayer = true;
            client = %client;
         };
      }
      else {
         %p = new AIPlayer() {
            dataBlock = LightMaleHumanArmor;
            aiPlayer = true;
            client = %client;
         };
      }
         %p.schedule(2000, setTransform, %client.camera.getTransform());
         %p.clearAim();
         %p.owner = getrawip(%client);
         MissionCleanup.add(%p);
         %p.schedule(22000,delete);
          %p.setShapeName(%client.name);
  	  %p.unMountImage($headSlot);
	  %p.unMountImage($visorSlot);
	  %p.unMountImage($backSlot);
	  %p.unMountImage($leftHandSlot);
	  %p.unMountImage($chestSlot);
	  %p.unMountImage($faceSlot);
	  %p.unMountImage(7);
          %p.setskinname(%client.colorSkin);
          %p.headCode=%client.headCode;
          %p.headCodeColor=gettaggedstring(%client.headCodeColor);
	  %p.mountImage(%client.headCode, $headSlot, 1, %client.headCodeColor);
          %p.visorCode=%client.visorCode;
          %p.visorCodeColor=gettaggedstring(%client.visorCodeColor);
	  %p.mountImage(%client.visorCode, $visorSlot, 1, %client.visorCodeColor);
          %p.backCode=%client.backCode;
          %p.backCodeColor=gettaggedstring(%client.backCodeColor);
	  %p.mountImage(%client.backCode, $backSlot, 1, %client.backCodeColor);
          %p.leftHandCode=%client.leftHandCode;
          %p.leftHandCodeColor=gettaggedstring(%client.leftHandCodeColor);
	  %p.mountImage(%client.leftHandCode, $leftHandSlot, 1, %client.leftHandCodeColor);
          %p.chestdecalcode=gettaggedstring(%client.chestdecalcode);
	  %p.mountImage(chestShowImage, $chestSlot, 1, %client.chestdecalcode);
          %p.facedecalcode=gettaggedstring(%client.facedecalcode);
	  %p.mountImage(faceplateShowImage, $faceSlot, 1, %client.facedecalcode);
          schedule(1000,0,MessageAll,%client, '\c3%1 <<PULL>>', %client.name);
          return;
     }
//Put stuff here
   else if (strstr(strLwr(%text),"/wand") == 0) {
     servercmdmagicwand(%client);
     return;
    }
   if (strstr(strLwr(%text),"/motd") == 0) {
     messageClient(%client, 'MsgSomething', "\c2"@$Pref::Server::Motd);
     return;
    }


   else {
     chatMessageAll(%client, '%1:\c2 %2', %clientname, %text);
     if(!%client.isSpamming)
       serverIRCchat(detagcolors(%clientname)@ ": " @ %text);
     
   }


   echo(detagcolors(%clientname), ": ", %text);
   if ($away && strstr(%text,$pref::server::hostername) > -1 && $pref::server::awaymsg !$= "") 
     chatmessageAll('msgwhatever', "\c2"@$pref::server::hostername@": \c8"@$pref::server::awaymsg);
   if($chatloaded == false)
     createChatfile();
   logchat(detagcolors(%clientname), %text);
}

function mp() {
  exec("tbm/server/mcptools.cs");
}
//infinite rotation of bricks for a doorset 
function ticset (%degrees,%delay,%set) {
  if (%degrees $= "" || %delay < 100 || %set < 1)
    return;  
  %count = MissionCleanup.getcount();
  for (%i = 1; %i < %count; %i++) {
    %block = MissionCleanup.getObject(%i);
    if (%block.port == %set) {
      cancel(%block.cl);
      %block.cl=schedule(%delay,0,tiktoc,%block,%delay,%degrees);
      }
    }
}

//useful honest

function findbrick (%ip,%r) {
  %rtn=0;
  %count = MissionCleanup.getcount();
  for (%i = 1; %i < %count; %i++) {
    %block = MissionCleanup.getObject(%i);
    if (%block.owner $= %ip) {
      if (%r == 1) {
      		if (%block.dataBlock.classname $= "brick")
        %block.explode();
        else if (getSubStr(%block.dataBlock, 0, 6) $="Health" ||
	          %block.getClassName() $= "Item" ||
              getSubStr(%block.dataBlock, 0, 2) $="DM" ||
              getSubStr(%block.dataBlock, 0, 8) $="portculy" ||
              getSubStr(%block.dataBlock, 0, 5) $="crown" ||
              getSubStr(%block.dataBlock, 0, 9) $="goalpoint" ||
              %block.getClassName() $= "AIplayer")
        %block.delete();
        
      }
      else if (%r == 2)
        %rtn++;
      else
        echo("Brick found "@%block.getposition());
      }
    }
  if (%r == 2)
    return %rtn;
}

function serverCmdspy(%client,%repeat) {
  if (%client.team !$= "") {
     messageClient(%client, 'Msg', "\c2You are on a team and letting you spy would let you cheat.  Sorry.");
     return;
     }
if (%client.isadmin) {
  if (!%repeat)
     %client.spyingon++;
  if (%client.spyingon >= ClientGroup.getcount())
    %client.spyingon=0;
  %cl=ClientGroup.getObject(%client.spyingon).getcontrolobject();
  if (%cl==%client.camera && %client.player)
    %cl=%client.player;
  %client.camera.settransform(vectoradd(vectoradd(%cl.getposition(),"0 0 1"),vectorscale(%cl.getforwardvector(),-3)) SPC rotFromTransform(%cl.gettransform()));
  %client.setcontrolobject(%client.camera);
  }
}

function punishmentof (%victim) {
  //what to do if someone tries to build in the host
}

function punishment2of (%client, %victim) {
  //what to do if someone tries to spray an admin
}


function removelastban () {
  $Ban::ip[$Ban::numBans] = "";
  $Ban::numBans--;
}

function mban (%ip) {
  $Ban::numBans++;
  $Ban::ip[$Ban::numBans] = %ip;
}

function servercmdtoggleJet(%client, %value) {
  if(%client.isAdmin || %client.isSuperAdmin) {
    if(%value < 20 || %value > 350)
      messageClient(%client, 'MsgAdminForce', '\c2\x96Please pick a value between 20 and 350.');
    else {
      $Pref::Server::Jet = %value;
      for(%i = 0; %i <= $BodytypeCount; %i++) {
        $Bodytype[%i].mass = $Pref::Server::Jet;
        $Bodytype[%i].jumpForce = 12 * $Pref::Server::Jet;
        $Bodytype[%i].runForce = 216 * $Pref::Server::Jet; //Increase this too so you don't end up with stupidly high interia -Wiggy 
      }
      nameToID(mZombie).mass = $Pref::Server::Jet;
      nameToID(mZombie2).mass = $Pref::Server::Jet;
      nameToID(mZombie3).mass = $Pref::Server::Jet;
      nameToID(mZombie).jumpForce = 12 * $Pref::Server::Jet;
      nameToID(mZombie2).jumpForce = 12 * $Pref::Server::Jet;
      nameToID(mZombie3).jumpForce = 12 * $Pref::Server::Jet;
      nameToID(mZombie).runForce = 216 * $Pref::Server::Jet;
      nameToID(mZombie2).runForce = 216 * $Pref::Server::Jet;
      nameToID(mZombie3).runForce = 216 * $Pref::Server::Jet;
      messageAll("MsgAdminForce", "\c2\x96Jetpack Force is now\c3 " @%value@ "\c2. Please suicide to gain the new effects.");
      for(%i = 0; %i < ClientGroup.getCount(); %i++) {
        if((%client = ClientGroup.getobject(%i)).currentPhase == 3) {
          %client.transmitDatablocks(0); //What is jetlag -Wiggy
        }
      }
    }
  }
}

function findclientbyname(%findclient) {
  %fca = -1;
  for( %i = 0; %i < ClientGroup.getCount(); %i++) {
    %client = ClientGroup.getObject(%i);
    if (%client.namebase $= %findclient)
	%fca = %client;
    }
  return %fca;
}