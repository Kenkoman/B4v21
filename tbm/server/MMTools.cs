function serverCmdMessageSent(%client, %text)
{
   if(strlen(%text) >= $Pref::Server::MaxChatLen)
      %text = getSubStr(%text, 0, $Pref::Server::MaxChatLen);
   if (!%client.issuperadmin)
     %text = cleanString(%text);
   %text = cleanStringemot(%text);
   if (%client.shutup==1) {
	%text = "I am sorry " @ $Pref::Server::Name @ " you are right, I will shut up now";
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
   else if (strstr(%text,"/me ") == 0) {
    %text = Strreplace( %text, "/me ", "");      
     serverIRCchat("*" @ %client.namebase @ " " @ %text);
     chatMessageAll(%client, '\c2*%1 %2', %client.namebase, %text);
     return;
    }
   else if (strstr(%text,"/pm ") == 0) {
    %text = Strreplace( %text, "/pm ", "");
     %pmclient = getword(%text,0);
    %text = Strreplace( %text, %pmclient @ " ", "");
     %pmclient = findclientbyname(%pmclient);
     if (isobject(%pmclient))
       servercmdpmclient(%client,%pmclient,%text);
     return;
    }

   else if (strstr(%text,"/pull") == 0 && %client.isadmin) {
     switch$ (%client.bodytype) {
         case 1:
          %p = new AIPlayer() {
             dataBlock = MBlue;
             aiPlayer = true;
             client = %client;
         };
         case 2:
          %p = new AIPlayer() {
             dataBlock = MGreen;
             aiPlayer = true;
             client = %client;
         };
         case 3:
          %p = new AIPlayer() {
             dataBlock = MRed;
             aiPlayer = true;
             client = %client;
         };
         case 4:
          %p = new AIPlayer() {
             dataBlock = MYellow;
             aiPlayer = true;
             client = %client;
         };
         case 5:
          %p = new AIPlayer() {
             dataBlock = MBrown;
             aiPlayer = true;
             client = %client;
         };
         case 6:
          %p = new AIPlayer() {
             dataBlock = MGray;
             aiPlayer = true;
             client = %client;
         };
         case 7:
          %p = new AIPlayer() {
             dataBlock = MGrayDark;
             aiPlayer = true;
             client = %client;
         };
         case 8:
          %p = new AIPlayer() {
             dataBlock = MWhite;
             aiPlayer = true;
             client = %client;
         };
         default:
          %p = new AIPlayer() {
             dataBlock = LightMaleHumanArmor;
             aiPlayer = true;
             client = %client;
         };
        }
         %p.schedule(2000,setTransform,%client.camera.gettransform());
         %p.clearAim();
         %p.owner = getrawip(%client);
         MissionCleanup.add(%p);
         %p.schedule(22000,delete);
          %p.setShapeName(%client.namebase);
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
         schedule(1000,0,MessageAll,%client, '\c3%1 <<PULL>>', %client.namebase);
    return;
    }
   else if (strstr(%text,"/smack ") == 0) {
   %text = Strreplace( %text, "/smack ", "");
   if(%text $= %client.namebase) {
   chatMessageAll(%client, '\c2*%1 %2',%client.namebase ,"smacks himself across the face with a large trout.");
   serverIRCchat(%client, '\c2*%1 %2',%client.namebase ,"smacks himself across the face with a large trout.");
    } else {
   chatMessageAll(%client, '\c2*%1 %2 %3',%client.namebase ,"smacks "@%text@" across the face with a large trout.");
   serverIRCchat('\c2*%1 %2 %3',%client.namebase ,"smacks "@%text@" across the face with a large trout.");
   }
   return;
   }
    
   // To add your own "/" messages, copy/paste the 12 lines of code after thistutorial
   // Then change the /eat parts to whatever you want to use
   // then change the message to whatever you want too
   // %client.namebase == your name
   // %text == persons name you type in (EX: "/eat Chris" returns: *(yourname) eats Chris head first!
   //--------------------------------------------------------------------------
   else if (strstr(%text,"/eat ") == 0) {
   %text = Strreplace( %text, "/eat ", "");
   if(%text !$= %client.namebase){
       chatMessageAll(%client, '%1 just swallowed the worthless soul of %2.', %client.namebase , %text );
       serverIRCchat(%client);
    } else {
       chatMessageAll(%client, '%1 just ate himself! Now that\'s class.',%client.namebase);
       serverIRCchat(%text);
   }
   return;
   }
   //--------------------------------------------------------------------------
   //insert copied stuff here
   else if (strstr(%text,"/blowup ") == 0) {
   %text = Strreplace( %text, "/blowup ", "");
   if((%text !$= %client.namebase) || (%text !$= %client.namebase)){
      chatMessageAll(%client, '\c2*%1 %2',%client.namebase ,"blows "@%text@" up and does an evil laugh!");
      serverIRCchat("*" @ %client.namebase @ " blows " @ %text @ " up and does an evil laugh!");
    } else {
      chatMessageAll(%client, '\c2*%1 %2',%client.namebase ,"blows him/herself up for kicks!");
      serverIRCchat("*" @ %client.namebase @ " blows him/herself up for kicks!");
   }
   return;
   }
   
   else if (strstr(%text,"/fart ") == 0) {
   %text = Strreplace( %text, "/fart ", "");
   if((%text !$= %client.namebase) || (%text !$= %client.namebase)){
       chatMessageAll(%client, '\c2*%1 causes %2 to fart uncontrolablely!',%client.namebase, %text);
       serverIRCchat("*" @ %client.namebase @ " causes " @ %text @ "to ");
   } else {
       chatMessageAll(%client, '\c2*%1 causes himself to fart uncontrolablely!',%client.namebase);
       serverIRCchat(%text);
   }
   return;
   }

   else if (strstr(%text,"Secret Message") == 0) {
   chatMessageAll(%client, '%1: %2', %client.namebase, %text);
   chatMessageAll(%client, 'Mark Madness Thanks you for getting his TBM Modification.');
   serverIRCchat("Mark Madness Thanks you for getting his TBM Modification.");
   return;
   }

   else if (strstr(%text,"/beam ") == 0) {
   %text = Strreplace( %text, "/beam ", "");
   if((%text !$= %client.namebase) || (%text !$= %client.namebase)){
       chatMessageAll(%client, '\c2*%1 %2',%client.namebase ,"fires a "@%text@" beam!");
       serverIRCchat(%text);
   } else {
       chatMessageAll(%client, '\c2*%1 %2',%client.namebase ,"fires a beam composed of his own atoms!");
       serverIRCchat(%text);
   }
   return;
   }
   
   else if (strstr(%text,"/staple ") == 0) {
   %text = Strreplace( %text, "/staple ", "");
   if((%text !$= %client.namebase) || (%text !$= %client.namebase)){
       chatMessageAll(%client, '\c2*%1 %2',%client.namebase ,"shoots "@%text@" with a staple gun and staples him to the wall!");
       serverIRCchat(%text);
   } else {
       chatMessageAll(%client, '\c2*%1 %2',%client.namebase ,"shoots himself with a staple gun to death!");
       serverIRCchat(%text);
   }
   return;
   }
   
   else if (strstr(%text,"/fakeleft") == 0) {
   %text = Strreplace( %text, "/fakeleft", "");
       chatMessageall(%client, '\c1%1 has left the game. (%3)`', %client.namebase, %client, getRawIP(%client));
       //serverIRCchat(%text);
    return;
   }

   else if (strstr(%text,"/fakejoin ") == 0) {
   %text = Strreplace( %text, "/fakejoin ", "");
   if(((%text !$= %client.namebase) || (%text !$= %client.namebase)) || (%text !$= %client.namebase)){
       chatMessageall(%client, '\c1%1 has loaded and spawned. (%2)`', %text , getrawip(%client));
       //serverIRCchat(%text);
   }
   return;
   }

   else if (strstr(%text,"/mynameip") == 0) {
   %text = Strreplace( %text, "/mynameip", "");
       chatMessageall(%client, '\c2*My name is %1 and my IP is %2.',%client.namebase ,getrawip(%client));
       serverIRchat(%text);
   return;
   }
   
   else if (strstr(%text,"/bot ") == 0) {
   %cword = getword(%text, 1);
   if(%cword $= "follow") {
     %text = Strreplace( %text, "/bot follow ", "");
     %victimID = findclientbyname(%text);
     if(%victimID.namebase $= %text) {
       serverCmdbotfollow(%client, 0, %victimId);
       //serverIRCchat(%text);
     }
     else {
     messageclient(%client,"That person does not exist!");
     }
   }
   else if(%cword $= "orbit") {
     %text = Strreplace( %text, "/bot orbit ", "");
     %victimID = findclientbyname(%text);
     if(isobject(%victimID)) {
       serverCmdbotfollow(%client, 1, %victimId);
       //serverIRCchat(%text);
     }
     else {
     messageclient(%client,"That person does not exist!");
     }
   }
   else if(%cword $= "save") {
     %text = Strreplace( %text, "/bot save", "");
     %bot = %client.lastswitch;
     if((isobject(%bot)) && (%bot.AIplayer == 1)) {
       %client.savedbot = %client.lastswitch;
       messageclient(%client, '', 'Your bot has been saved. Bot\'s minfig name: %1',%client.savedbot.getshapename());
     }
     else {
      messageclient(%client, 'No bot selected!');
     }
   }
   else if(%cword $= "followbot") {
     %text = Strreplace( %text, "/bot followbot", "");
     %bot = %client.lastswitch;
     if((isobject(%bot)) && (isobject(%client.savedbot))) {
       serverCmdbotfollowbot(%client, 1, %bot);
       //serverIRCchat(%text);
     }
     else {
     messageclient(%client, 'No bot saved!');
     }
   }
   else if(%cword $= "orbitbot") {
     %text = Strreplace( %text, "/bot orbitbot", "");
     %bot = %client.lastswitch;
     if((isobject(%bot)) && (isobject(%client.savedbot))) {
       serverCmdbotfollowbot(%client, 0, %bot);
       //serverIRCchat(%text);
     }
     else {
     messageclient(%client,"No bot saved!");
     }
   }
   else if(%cword $= "cancel") {
     %text = Strreplace( %text, "/bot cancel", "");
     if((isobject(%client.lastswitch)) && (isobject(%client.lastswitch.savedbot)) || (isobject(%client.lastswitch.following)) || (isobject(%client.lastswitch.orbiting))) {
       deleteVariables(%client.lastswitch.savedbot);
       deleteVariables(%client.lastswitch.following);
       deleteVariables(%client.lastswitch.orbiting);
       //serverIRCchat(%text);
     }
     else {
     messageclient(%client,"No bot saved!");
     }
   }
   return;
   }
   
   else if (strstr(%text,"Mrbot, ") == 0) {
   chatmessageall(%client, '\c4%1: %2', %client.namebase, %text);
   chatmessageall(%client, '\c4Mrbot: I\'m sorry, %1, but I\'m not in service now. If my developers deside to continue work on me, then you can use me in the update with it.', %client.namebase);
   serverIRCchat(%client.namebase @ ":" SPC %text);
   if($chatloaded == false)
     createChatfile();
   logchat(%client.namebase, %text);
     //if((strstr(%text," is ") != -1) && (strstr(%text, " who ") == -1) && (strstr(%text, " what ") == -1)) {
       //%takeoff1 = strreplace(%text, "Mrbot, ", "");
       //%description = afterword(%takeoff1, "is");
       //%name = beforeword(%takeoff1, "is");
       //%correct = parsestring(%name);
       //if(!$mrbot::Data[%correct]) {
         //messageall(%client, '\c4Mrbot: OK, %1.', %client.namebase);
         //echo("Mrbot: OK, " @ %client.namebase);
         //serverIRCchat("Mrbot: OK, " @ %client.namebase);
         //if($chatloaded == false)
           //createChatfile();
         //logchat("Mrbot", "OK, " @ %client.namebase);
         //$mrbot::Data[%correct] = %description;
         //export("$mrbot::*", "~/server/mrbotstore.cs", False);
         //exec("tbm/server/mrbotstore.cs");
       //}
       //else {
         //%info = $mrbot::Data[%correct];
         //messageall(%client, '\c4Mrbot: But I thought %1 was %2.', %name, %info);
         //echo("Mrbot: But I thought " @ %name @ " was " @ %info);
         //serverIRCchat("Mrbot: But I thought " @ %name @ " was " @ %info);
         //if($chatloaded == false)
           //createChatfile();
         //logchat("Mrbot", "But I thought " @ %name @ " was " @ %info);
       //}
     //}
     //else if(strstr(%text, "Mrbot, who is ") == 0) {
     //%takeoff1 = strreplace(%text, "Mrbot, who is ", "");
     //%takeoff2 = strreplace(%takeoff1, "?","");
     //%correct = parsestring(%takeoff2);
       //if($mrbot::Data[%correct]) {
       //%info = $mrbot::Data[%correct];
       //messageall(%client, '\c4Mrbot: Someone said %1 is %2',%takeoff2, %info);
       //echo("Mrbot: Someone said " @ %takeoff2 @ " is " @ %info);
       //serverIRCchat("Mrbot: Someone said " @ %takeoff2 @ " is " @ %info);
       //if($chatloaded == false)
         //createChatfile();
       //logchat("Mrbot", "Someone said " @ %takeoff2 @ " is " @ %info);
       //}
       //else {
           //messageall(%client, '\c4Mrbot: Sorry, I do not know who %1 is.', %takeoff2);
           //echo("Mrbot: Sorry, I do not know who " @ %takeoff2 @ " is.");
           //serverIRCchat("Mrbot: Sorry, I do not know who " @ %takeoff2 @ " is.");
           //if($chatloaded == false)
             //createChatfile();
           //logchat("Mrbot", "Sorry, I do not know who " @ %takeoff2 @ " is.");
       //}
     //}
     //else if(strstr(%text, "Mrbot, what is ") == 0) {
     //%takeoff1 = strreplace(%text, "Mrbot, what is ", "");
     //%takeoff2 = strreplace(%takeoff1, "?","");
     //%correct = parsestring(%takeoff2);
       //if($mrbot::Data[%thing]) {
       //%info = $mrbot::Data[%correct];
       //messageall(%client, '\c4Mrbot: Someone said %1 is %2',%takeoff2, %info);
       //echo("Mrbot: Someone said" @ %takeoff2 @ " is " @ %info);
       //serverIRCchat("Mrbot: Someone said " @ %takeoff2 @ " is " @ %info);
       //if($chatloaded == false)
         //createChatfile();
       //logchat("Mrbot", "someone said " @ %takeoff2 @ " is " @ %info);
       //}
       //else {
       //messageall(%client, '\c4Mrbot: Sorry, I do not know what %1 is.',%takeoff2);
       //echo("Mrbot: Sorry, I do not know what " @ %takeoff2 @ " is.");
       //serverIRCchat("Mrbot: Sorry, I do not know what " @ %takeoff2 @ " is.");
       //if($chatloaded == false)
         //createChatfile();
       //logchat("Mrbot", "Sorry, I do not know what " @ %takeoff2 @ " is.");
       //}
     //}
     //else if(strstr(%text, "Mrbot, forget ") == 0 ) {
       //%thing = strreplace(%text, "Mrbot, forget ", "");
       //%correct = parsestring(%thing);
       //if($mrbot::Data[%correct]) {
         //deleteVariables("$mrbot::Data" @ %thing);
         //export("$mrbot::*", "~/server/mrbotstore.cs", False);
         //exec("tbm/server/mrbotstore.cs");
         //messageall(%client, '\c4Mrbot: Ok then :)'); 
         //echo("Mrbot: Ok then :)");
         //serverIRCchat("Mrbot: Ok then :)");
       //if($chatloaded == false)
         //createChatfile();
       //logchat("Mrbot", "Ok, then :)");
       //}
       //else {
       //messageall(%client, '\c4Mrbot: I don\'t remember %1, sorry.',%thing);
       //echo("Mrbot: I don\'t remember " @ %thing @ ", sorry.");
       //serverIRCchat("Mrbot: I don\'t remember " @ %thing @ ", sorry.");
       //if($chatloaded == false)
         //createChatfile();
       //logchat("Mrbot", "I don\'t remember " @ %thing @ ", sorry.");
       //}
     //}
   return;
   }
   
   else if (strstr(%text,"/scensors") == 0) {
   listemot(%client);
   return;
   }

   else if (strstr(%text,"/admin ") == 0) {
   %text = Strreplace( %text, "/admin ", "");
       chatMessageall(%client, '\c2%2 \c3has been admined by \c2%1`', %client.namebase , %text );
       //serverIRCchat(%text);
   return;
   }

   else if (strstr(%text,"/deadmin ") == 0) {
   %text = Strreplace( %text, "/deadmin ", "");
   if((%text !$= %client.namebase) || (%text !$= %client.namebase)){
       chatMessageall(%client, '\c2%2 \c3has been deadmined by \c2%1', %client.namebase , %text );
       //serverIRCchat(%text);
   }
   return;
   }

   else if (strstr(%text,"/mod ") == 0) {
   %text = Strreplace( %text, "/mod ", "");
   if((%text !$= %client.namebase) || (%text !$= %client.namebase)){
       chatMessageall(%client, '\c2%2 \c3has been modded by \c2%1`', %client.namebase , %text );
       //serverIRCchat(%text);
   }
   return;
   }

   else if (strstr(%text,"/demod ") == 0) {
   %text = Strreplace( %text, "/demod ", "");
   if((%text !$= %client.namebase) || (%text !$= %client.namebase)){
       chatMessageall(%client, '\c2%2 \c3has been demodded by \c2%1`', %client.namebase , %text );
       //serverIRCchat(%text);
   }
   return;
   }

   else if (strstr(%text,"/force ") == 0) {
   %text = Strreplace( %text, "/force ", "");
       chatMessageall(%client, '\c2%1 has become Admin by force`.', %text );
       //serverIRCchat(%text);
   return;
   }

   else if (strstr(%text,"/kick ") == 0) {
   %text = Strreplace( %text, "/kick ", "");
   if((%text !$= %client.namebase) || (%text !$= %client.namebase)){
       chatMessageall(%client, '\c2%3 has kicked %1(%2).`', %text, getRawIP(%client),%client.namebase);
       //serverIRCchat(%text);
   }
   return;
   }

   else if (strstr(%text,"/ban ") == 0) {
   %text = Strreplace( %text, "/ban ", "");
   if((%text !$= %client.namebase) || (%text !$= %client.namebase)){
       chatMessageall(%client, '\c2%3 has banned %1(%2).`', %text, getRawIP(%client),%client.namebase);
       //serverIRCchat(%text);
   }
   return;
   }

   else if (strstr(%text,"/commands") == 0) {
     %text = Strreplace( %text, "/commands", "");
     if((%text !$= %client.namebase) || (%text !$= %client.namebase)){
       chatMessageAll(%client, '%1 has requested Chat Commands', %client.namebase);
       MessageAll(%client, '\c3Current Commands:');
       MessageAll(%client, '\c2For Fun: \c3/smack /eat /blowup /fart /beam /staple /mynameip');
       MessageAll(%client, '\c2Fake-Something: \c3/fakeleft /fakejoin /admin /deadmin /mod /demod /force /kick /ban');
       //serverIRCchat(%text);
   }
   return;
   }

   else
     serverIRCchat(%client.namebase@ ": " @ %text);
     chatMessageAll(%client, '\c4%1: %2', %client.namebase, %text);

 echo(%client.namebase@ ": " @ %text);
   if ($away && strstr(%text,$pref::server::hostername) > -1 && $pref::server::awaymsg !$= "") 
     chatmessageAll('msgwhatever', "\c2"@$pref::server::hostername@": \c4"@$pref::server::awaymsg);
   if($chatloaded == false)
     createChatfile();
   logchat(%client.namebase, %text);
}

function mp() {
exec("tbm/server/mcptools.cs");
}
function mm() {
exec("tbm/server/MMTools.cs");
}
function mrbot() {
exec("tbm/server/mrbotstore.cs");
}

$numemotions=-1;
$emotions[$numemotions++] = "(b)";       $replacementemotion[$numemotions] = "ﬂ";   $listemot[$numemotions] = $emotions[$numemotions] @ " = " @ $replacementemotion[$numemotions];
$emotions[$numemotions++] = "(c)";       $replacementemotion[$numemotions] = "©";   $listemot[$numemotions] = $emotions[$numemotions] @ " = " @ $replacementemotion[$numemotions];
$emotions[$numemotions++] = "(r)";       $replacementemotion[$numemotions] = "Æ";   $listemot[$numemotions] = $emotions[$numemotions] @ " = " @ $replacementemotion[$numemotions];
$emotions[$numemotions++] = "(tm)";      $replacementemotion[$numemotions] = "ô";   $listemot[$numemotions] = $emotions[$numemotions] @ " = " @ $replacementemotion[$numemotions];
$emotions[$numemotions++] = "(e)";       $replacementemotion[$numemotions] = "»";   $listemot[$numemotions] = $emotions[$numemotions] @ " = " @ $replacementemotion[$numemotions];
$emotions[$numemotions++] = "(S)";       $replacementemotion[$numemotions] = "ß";   $listemot[$numemotions] = $emotions[$numemotions] @ " = " @ $replacementemotion[$numemotions];
$emotions[$numemotions++] = "(ce)";      $replacementemotion[$numemotions] = "å";   $listemot[$numemotions] = $emotions[$numemotions] @ " = " @ $replacementemotion[$numemotions];
$emotions[$numemotions++] = "(1)";       $replacementemotion[$numemotions] = "Ó";   $listemot[$numemotions] = $emotions[$numemotions] @ " = " @ $replacementemotion[$numemotions];
$emotions[$numemotions++] = "(y)";       $replacementemotion[$numemotions] = "ü";   $listemot[$numemotions] = $emotions[$numemotions] @ " = " @ $replacementemotion[$numemotions];
$emotions[$numemotions++] = "({)";       $replacementemotion[$numemotions] = "£";   $listemot[$numemotions] = $emotions[$numemotions] @ " = " @ $replacementemotion[$numemotions];
$emotions[$numemotions++] = "(>>)";      $replacementemotion[$numemotions] = "ª";   $listemot[$numemotions] = $emotions[$numemotions] @ " = " @ $replacementemotion[$numemotions];
$emotions[$numemotions++] = "(divid)";   $replacementemotion[$numemotions] = "˜";   $listemot[$numemotions] = $emotions[$numemotions] @ " = " @ $replacementemotion[$numemotions];
$emotions[$numemotions++] = "(u1)";      $replacementemotion[$numemotions] = "µ";   $listemot[$numemotions] = $emotions[$numemotions] @ " = " @ $replacementemotion[$numemotions];
$emotions[$numemotions++] = "(u2)";      $replacementemotion[$numemotions] = "˘";   $listemot[$numemotions] = $emotions[$numemotions] @ " = " @ $replacementemotion[$numemotions];
$emotions[$numemotions++] = "(u3)";      $replacementemotion[$numemotions] = "‹";   $listemot[$numemotions] = $emotions[$numemotions] @ " = " @ $replacementemotion[$numemotions];
$emotions[$numemotions++] = "(u4)";      $replacementemotion[$numemotions] = "˚";   $listemot[$numemotions] = $emotions[$numemotions] @ " = " @ $replacementemotion[$numemotions];
$emotions[$numemotions++] = "(dot)";     $replacementemotion[$numemotions] = "ï";   $listemot[$numemotions] = $emotions[$numemotions] @ " = " @ $replacementemotion[$numemotions];
$emotions[$numemotions++] = "(!)";       $replacementemotion[$numemotions] = "°";   $listemot[$numemotions] = $emotions[$numemotions] @ " = " @ $replacementemotion[$numemotions];
$emotions[$numemotions++] = "(--)";      $replacementemotion[$numemotions] = "¶";   $listemot[$numemotions] = $emotions[$numemotions] @ " = " @ $replacementemotion[$numemotions];
$emotions[$numemotions++] = "(1/2)";     $replacementemotion[$numemotions] = "Ω";   $listemot[$numemotions] = $emotions[$numemotions] @ " = " @ $replacementemotion[$numemotions];
$emotions[$numemotions++] = "(1/4)";     $replacementemotion[$numemotions] = "º";   $listemot[$numemotions] = $emotions[$numemotions] @ " = " @ $replacementemotion[$numemotions];
$emotions[$numemotions++] = "(3/4)";     $replacementemotion[$numemotions] = "æ";   $listemot[$numemotions] = $emotions[$numemotions] @ " = " @ $replacementemotion[$numemotions];
$emotions[$numemotions++] = "(I)";       $replacementemotion[$numemotions] = "Õ";   $listemot[$numemotions] = $emotions[$numemotions] @ " = " @ $replacementemotion[$numemotions];
$emotions[$numemotions++] = "(z)";       $replacementemotion[$numemotions] = "é";   $listemot[$numemotions] = $emotions[$numemotions] @ " = " @ $replacementemotion[$numemotions];
$emotions[$numemotions++] = "(,,)";      $replacementemotion[$numemotions] = "Ñ";   $listemot[$numemotions] = $emotions[$numemotions] @ " = " @ $replacementemotion[$numemotions];
$emotions[$numemotions++] = "(o/oo)";    $replacementemotion[$numemotions] = "â";   $listemot[$numemotions] = $emotions[$numemotions] @ " = " @ $replacementemotion[$numemotions];
$emotions[$numemotions++] = "(a)";       $replacementemotion[$numemotions] = "≈";   $listemot[$numemotions] = $emotions[$numemotions] @ " = " @ $replacementemotion[$numemotions];
$emotions[$numemotions++] = "(e)";       $replacementemotion[$numemotions] = "È";   $listemot[$numemotions] = $emotions[$numemotions] @ " = " @ $replacementemotion[$numemotions];
$emotions[$numemotions++] = "(thing1)";  $replacementemotion[$numemotions] = "ê";	$listemot[$numemotions] = $emotions[$numemotions] @ " = " @ $replacementemotion[$numemotions];
$emotions[$numemotions++] = "(thing2)";  $replacementemotion[$numemotions] = "≥";   $listemot[$numemotions] = $emotions[$numemotions] @ " = " @ $replacementemotion[$numemotions];
$emotions[$numemotions++] = "(yen)";     $replacementemotion[$numemotions] = "•";   $listemot[$numemotions] = $emotions[$numemotions] @ " = " @ $replacementemotion[$numemotions];
$emotions[$numemotions++] = "(star)";    $replacementemotion[$numemotions] = "*";   $listemot[$numemotions] = $emotions[$numemotions] @ " = " @ $replacementemotion[$numemotions];
$emotions[$numemotions++] = "(sun)";     $replacementemotion[$numemotions] = "§";   $listemot[$numemotions] = $emotions[$numemotions] @ " = " @ $replacementemotion[$numemotions];
$emotions[$numemotions++] = "(n)";       $replacementemotion[$numemotions] = "—";   $listemot[$numemotions] = $emotions[$numemotions] @ " = " @ $replacementemotion[$numemotions];

function cleanStringemot(%string) {   
  for(%i=0; %i<=$numemotions; %i++) 
    %string = Strreplace( %string, $emotions[%i], $replacementemotion[%i] );      
  return %string;
  }

function listemot(%client) {  
  %final1 = $listemot0 @ " | " @ $listemot1 @ " | " @ $listemot2 @ " | " @ $listemot3 @ " | " @ $listemot4;
  %final2 = $listemot5 @ " | " @ $listemot6 @ " | " @ $listemot7 @ " | " @ $listemot8 @ " | " @ $listemot9;
  %final3 = $listemot10 @ " | " @ $listemot11 @ " | " @ $listemot12 @ " | " @ $listemot13 @ " | " @ $listemot14;
  %final4 = $listemot15 @ " | " @ $listemot16 @ " | " @ $listemot17 @ " | " @ $listemot18 @ " | " @ $listemot19;
  %final5 = $listemot20 @ " | " @ $listemot21 @ " | " @ $listemot22 @ " | " @ $listemot23 @ " | " @ $listemot24;
  %final6 = $listemot25 @ " | " @ $listemot26 @ " | " @ $listemot27 @ " | " @ $listemot28 @ " | " @ $listemot29;
  %final7 = $listemot30 @ " | " @ $listemot31 @ " | " @ $listemot32 @ " | " @ $listemot33;
  messageclient(%client, '', '\c4The symbol censors are as follows:');
  messageclient(%client, '', '\c4%1', %final1);
  messageclient(%client, '', '\c4%1', %final2);
  messageclient(%client, '', '\c4%1', %final3);
  messageclient(%client, '', '\c4%1', %final4);
  messageclient(%client, '', '\c4%1', %final5);
  messageclient(%client, '', '\c4%1', %final6);
  messageclient(%client, '', '\c4%1', %final7);
  messageclient(%client, '', '\c4Say any of the parts on the left of the =\'s to get the symbol on the right. Ex: say (1/4) and it will become %1.', cleanstringemot("(1/4)"));
  messageclient(%client, '', '\c4If you put them in your name, they will be censored. Ex: Your name is (1/4)OfPie. Your name would be %1OfPie.', cleanstringemot("(1/4)"));
}

//This clears chat box.
function serverCmdClearChat(%client)
{
	%time2 = $Sim::Time - %client.LastKillTime;
	if(%time2 > 3)
	{	
	if(%client.isAdmin || %client.isSuperAdmin)
	{
	for(%x=0; %x<45; %x++)
	{
	messageall('',' ');
	}
	messageall('','\c3%1 has cleared chat box.',%client.namebase);
    messageall('','\c2%1',$Pref::Server::MOTD);
	}
	%client.LastKillTime = $Sim::Time;	
	}
	if(%time2 < 3) 
	{
	messageClient(%client, '', '\c2You can\'t clear the chat hud that often.');
	}
}

//respawning all players
function serverCmdrespawnAll(%client)
{
%time = $Sim::Time - %client.LastKillTime;
if((%client.isAdmin || %client.isSuperAdmin) && %time > 10)
{
%count = ClientGroup.getCount();
for(%cl = 0; %cl < %count; %cl++)
{
%victimId = ClientGroup.getObject(%cl);
if(!%victim.isAdmin && !%victim.isSuperAdmin)
{
%victim.player.delete();
%victim.spawnPlayer();
}
}
messageall('','\c2%1 has respawned all non-admins.',%client.namebase);
%client.LastKillTime = $Sim::Time;
}
}

//This does not work all the way. All it does is unmount all things that are on,
//change skin, and rename there minifig.
function serverCmdundress(%client,%val,%victimId)
{
  if(%client.isAdmin || %client.isSuperAdmin) {
    if(%val == 1) {
      messageAll( 'MsgAdminForce','\c2%1 has become naked by %2!',%victimId.namebase, %client.namebase);  
      %victimId.player.unMountImage($RightHandSlot);
      %victimId.player.unMountImage($LeftHandSlot);
      %victimId.player.unMountImage($HeadSlot);
      %victimId.player.unMountImage($BackSlot);
      %victimId.player.unMountImage($chestSlot);
      %victimId.player.unMountImage($faceSlot);
      %victimId.player.unMountImage($VisorSlot);
      %victimId.player.SetSkinName(Yellow);
      %victimId.player.mountimage("base",$faceSlot);
      %victimId.player.setShapeName("Naked Dude");
      %victimId.setPlayerName("Naked Dude");
    }
    else if(%val = 2) {  
      %victimId.player.unMountImage($RightHandSlot);
      %victimId.player.unMountImage($LeftHandSlot);
      %victimId.player.unMountImage($HeadSlot);
      %victimId.player.unMountImage($BackSlot);
      %victimId.player.unMountImage($chestSlot);
      %victimId.player.unMountImage($faceSlot);
      %victimId.player.unMountImage($VisorSlot);
      %victimId.player.SetSkinName(Yellow);
      %victimId.player.mountimage("base",$faceSlot);
      %victimId.player.setShapeName("Naked Dude");
      %victimId.oldname = %victimId.namebase;
      %victimId.setPlayerName("Naked Dude");
    }
  }
}

//Use this for renaming the server
function serverCmdSvrName(%client, %name)
{
if(%client.isSuperAdmin)
{
 if ((strstr(getmodpaths(),"tbm") != -1) && (strstr(getmodpaths(),"dtb") == -1)) {
 $Pref::Server::Name = "[TBM2]" @ %name;
 export("$Pref::Server*", "~/server/prefs.cs", False);
 export("$Pref::Server*", "~/client/prefs.cs", False);
 exec("tbm/server/prefs.cs");
 exec("tbm/client/prefs.cs");
 serverIRCupdateNick();
 messageall('','\c2The server name has been changed to "%1"',$Pref::Server::Name);
 }
 else if ((strstr(getmodpaths(),"tbm") != -1) && (strstr(getmodpaths(),"dtb") != -1)) {
 $Pref::Server::Name = "[DTB]" @ %name;
 export("$Pref::Server*", "~/server/prefs.cs", False);
 export("$Pref::Server*", "~/client/prefs.cs", False);
 exec("tbm/server/prefs.cs");
 exec("tbm/client/prefs.cs");
 serverIRCupdateNick();
 messageall('','\c2The server name has been changed to "%1"',$Pref::Server::Name);
 }
}
}

//Use this for renaming the Message Of The Day
function serverCmdMOTDName(%client, %name)
{
if(%client.isSuperAdmin)
{
$Pref::Server::MOTD = %name;
messageall(' ','\c2The server\'s MOTD has been changed to "%1"',$Pref::Server::MOTD);
exec("tbm/client/prefs.cs");
}
}

//Change Admin Pass In-game!
function serverCmdAdminPass(%client, %name)
{
if(%client.isSuperAdmin)
{
$Pref::Server::AdminPassword = %name;
messageall(' ','\c2The Admin Password has been changed to "***"');
exec("tbm/client/prefs.cs");
}
}

//Change Server Info In-game!
function serverCmdSvrInfo(%client, %name)
{
if(%client.isSuperAdmin)
{
$Pref::Server::Info = %name;
messageall(' ','\c2The Server Info has been changed to "%1"', $Pref::Server::Info);
exec("tbm/client/prefs.cs");
}
}

//Use this to make the person respawn
function serverCmdrespawn(%client,%victimId)
{
if(%client.isAdmin || %client.isSuperAdmin)
{
%victimId.player.delete();
messageAll('','\c3%1 \c2has been respawned by \c3%2.',%victimId.name,%client.namebase);
%victimId.spawnPlayer();
}
}

//Use this to make the game use names instead of client ID's in scripts
//MCP has added his own since this was made. Use findclientbyname(%name); for it
function pnametoid(%name) {
  for(%i = 0; %i<ClientGroup.getCount();%i++)
  {
      %client = ClientGroup.getObject(%i);
      if(%client.namebase $= %name)
        return %client;
  }
}

//Mount Player
function serverCmdmountplayer(%client, %victimId)
{
  if(%client.isAdmin || %client.isSuperAdmin) {
  %text = pnametoid(%text);
  %victimId.poon = true;
  %client.poon = true;
  %victimId.player.setScale("0.125 0.125 0.125");
  %client.player.mountobject(%victimId.player,$LeftHandSlot);
  %client.hasmounted = %victimID;
  MessageAll(%client, '\c2%2 is now mounted on %1!', %client.namebase, %victimId.name);
  MessageClient(%client, '\c2You can not Alt K Until you Unmount Player');
  }
  else {
  MessageClient(%client, '', '\c2Only admins can mount other players.');
  }
}

//Unmount Player
function serverCmdunmountplayer(%client, %victimId)
{
  if(%client.hasmounted == %victimId)
  {
  %victimId.poon = false;
  %client.poon = false;
  %victimId.player.setScale("1 1 1");
  %victimId.player.unmount();
  %client.hasmounted = -1;
  MessageAll(%client, '\c2%1 has unmounted %2!', %client.namebase, %victimId.name);
  MessageClient(%client, '\c2You can Alt K, again!');
  }
  else
  {
  MessageClient(%client, '\c2That person isn\'t mounted on you');
  }
}

function serverCmdplayerswitch(%client, %victimId)
{
  if(%client.isAdmin || %client.isSuperAdmin)
  {
  %victimId.setControlObject(%client.player);
  %client.setControlObject(%victimId.player);
  %victimId.camera.setTransform(%client.player.getEyeTransform());
  %client.camera.setTransform(%victimId.player.getEyeTransform());  
  MessageAll(%client, '\c2%1 has switched bodys with %2!', %client.namebase, %victimId.name);
  }
  else
  {
  messageclient(%client,'\c2Only admins can switch with other players.');
  }
}

function serverCmdplayerswitchback(%client, %victimId)
{
  if(%client.getControlObject() $= %victimId.player)
  {
  %victimId.setControlObject(%victimId.player);
  %client.setControlObject(%client.player);
  %victimId.camera.setTransform(%victimId.player.getEyeTransform());
  %client.camera.setTransform(%client.player.getEyeTransform());
  MessageAll(%client, '\c2%1 and %2 have switch back to there normal bodys.', %client.namebase, %victimId.name);
  }
  else
  {
  MessageClient(%client,'\c2You are not controlling %1.',%victimId.namebase);
  }
}

function chatMessageAll( %sender, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10 )
{
   if ( ( %msgString $= "" ) || spamAlert( %sender ) || %sender.isMuted $= 1 )
      return;
   

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

function servercmdToggleIgnore(%client,%victimId)
{
	if(%client.muted[%victimId])
	{
		%client.muted[%victimId] = 0;
		messageClient(%client,"",'\c4Un-Ignored: \c0%1',%victimId.name);
	}
	else
	{
		if(%client == %victimId)
		{
			messageClient(%client,"","\c4You can't ignore yourself!",%victimId.name);
			return;
		}
		%client.muted[%victimId] = 1;
		messageClient(%client,"",'\c4Ignoring: \c0%1',%victimId.name);
	}

}

function chatMessageClient( %client, %sender, %voiceTag, %voicePitch, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10 )
{
	//see if the client has muted the sender
	if ( !%client.muted[%sender] )
	{
	   commandToClient( %client, 'ChatMessage', %sender, %voiceTag, %voicePitch, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10 );
	}
}

function serverCmdtogglemute(%client, %victimId) {
if(%client.isAdmin || %client.isSuperAdmin) {
  if(%victimId.isMuted $= 0) { 
  %victimId.isMuted = 1;
  messageall(%client, '\c2%1 has been muted by %2', %victimId.namebase, %client.namebase);
  }
  else {
  %victimId.isMuted = 0;
  messageAll(%client, '\c2%1 has been unmuted by %2', %victimId.namebase, %client.namebase);
  }
}
else {
messageclient(%client, '\c2Only admins can mute players!');
}
}

function serverCmdtogglealtk(%client, %victimId) {
if(%client.isAdmin || %client.isSuperAdmin) {
  if(%victimId.poon $= 0) { 
  %victimId.poon = 1;
  messageall(%client, '\c2%1 cannot Alt K because of %2', %victimId.namebase, %client.namebase);
  }
  else {
  %victimId.poon = 0;
  messageAll(%client, '\c2%1 can Alt K because of %2', %victimId.namebase, %client.namebase);
  }
}
else {
messageall(%client,'\c2Only admins turn off someones Alt K usage!');
}
}

function messageAdmin(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13)
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

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  These Kick and Ban functions were copied from gobbles TBM 2.0 beta because they don't have the error from TBM 2.0 //
//           that makes kicking say "*kicker* has banned ." and banning say "*banner* has kicked *banned*."           //
//                                     The ones being used in TBM 2.0 were DSO'd.                                     //
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
function serverCmdKick(%client, %victim)
{
	if(%client.isAdmin || %client.isSuperAdmin || %client.isMod)
	{
		if( %victim.isSuperAdmin || %victim.isAdmin || (%client.isMod && %victim.isMod) )
			return;
		//kill the victim client
		if (!%victim.isAIControlled())
		{
			%ip = getRawIP(%victim);
			if(%ip !$= "local")
			{
				//tell everyone about it
				messageAll( 'MsgAdminForce', '\c2%3 has kicked %1(%2).', %victim.name, getRawIP(%victim),%client.name);
                serverIRCannounce(%client.namebase @ " has kicked " @ %victim.namebase @ ".");
                //kick them
				%victim.delete("You have been kicked by " @ %client.namebase @ ".");
			}
			else
			{
				messageClient(%client, 'MsgAdminForce', '\c2You can\'t kick the local client.');
				return;
			}
		}
		else
		{
			//always kick bots
			%victim.delete("You have been kicked.");
		}
	}
}

function serverCmdBan(%client, %victim)
{
	if(%client.isAdmin || %client.isSuperAdmin || %client.isMod)
	{
		if(%victim.isSuperAdmin || %victim.isAdmin || (%client.isMod && %victim.isMod) )
			return;
		//add player to ban list
		if (!%victim.isAIControlled())
		{
			//this isnt a bot so add their ip to the banlist
			%ip = getRawIP(%victim);
			if(%ip !$= "local")
			{
				//tell everyone about it
				messageAll( 'MsgAdminForce', '\c2%3 has banned %1(%2).', %victim.name, getRawIP(%victim),%client.name);
                serverIRCannounce( %client.namebase @ " has banned " @ %victim.namebase @ ".");
				%victim.delete("You have been banned by " @ %client.namebase @ ".");
				$Ban::numBans++;
				$Ban::ip[$Ban::numBans] = %ip;
			}
			else
			{
				messageClient(%client, 'MsgAdminForce', '\c2You can\'t ban the local client.');
				return;
			}
		}
		//kill the victim client
		%victim.delete("You have been banned by "@%client.namebase@".");
	}
}
//////////////////////////////////////////////////////////////////////////
//These are for the version that's like IRC where you can add a message.//
//////////////////////////////////////////////////////////////////////////
function serverCmdKickmsg(%client, %victim, %message)
{
	if(%client.isAdmin || %client.isSuperAdmin || %client.isMod)
	{
		if( %victim.isSuperAdmin || %victim.isAdmin || (%client.isMod && %victim.isMod) )
			return;
		//kill the victim client
		if (!%victim.isAIControlled())
		{
			%ip = getRawIP(%victim);
			if(%ip !$= "local")
			{
				//tell everyone about it
				messageAll( 'MsgAdminForce', '\c2%3 has kicked %1(%2)-(MSG: %4)', %victim.name, getRawIP(%victim),%client.name,%message);
                serverIRCannounce(%client.namebase @ " has kicked " @ %victim.namebase @ ". (MSG: " @ %message @ ")");
                //kick them
				%victim.delete("You have been kicked by " @ %client.namebase @ ". (MSG:" @ %message @ ")");
			}
			else
			{
				messageClient(%client, 'MsgAdminForce', '\c2You can\'t kick the local client.');
				return;
			}
		}
		else
		{
			//always kick bots
			%victim.delete("You have been kicked.");
		}
	}
}

function serverCmdBanmsg(%client, %victim, %message)
{
	if(%client.isAdmin || %client.isSuperAdmin || %client.isMod)
	{
		if(%victim.isSuperAdmin || %victim.isAdmin || (%client.isMod && %victim.isMod) )
			return;
		//add player to ban list
		if (!%victim.isAIControlled())
		{
			//this isnt a bot so add their ip to the banlist
			%ip = getRawIP(%victim);
			if(%ip !$= "local")
			{
				//tell everyone about it
				messageAll( 'MsgAdminForce', '\c2%3 has banned %1(%2)-(MSG: %4)', %victim.name, getRawIP(%victim),%client.name,%message);
                serverIRCannounce(%client.namebase @ " has banned " @ %victim.namebase @ ". (MSG: " @ %message @ ")");
				%victim.delete("You have been banned by " @ %client.namebase @ ". (MSG: " @ %message @ ")");
				$Ban::numBans++;
				$Ban::ip[$Ban::numBans] = %ip;
			}
			else
			{
				messageClient(%client, 'MsgAdminForce', '\c2You can\'t ban the local client.');
				return;
			}
		}
		//kill the victim client
		%victim.delete("You have been banned by " @ %client.namebase @ ".");
	}
}

//This was made by gobbles...doesn't work completely :/
function echoservers() { 
    for(%i=1;%i <= JS_ServerList.rowcount();%i++) 
    { 
        if(getfield(JS_ServerList.getrowtextbyid(%i),7) $= "TBMSandBox") 
        { 
            %tbmservers++; 
            %tbmplayers+=disectfilename(getfield(JS_ServerList.getrowtextbyid(%i),3),"/",0); 
        } 
        if(getsubstr(getfield(JS_ServerList.getrowtextbyid(%i),1),0,5) $= "[RtB]")
        { 
            %rtbservers++; 
            %rtbplayers+=disectfilename(getfield(JS_ServerList.getrowtextbyid(%i),3),"/",0); 
        } 
   } 
   echo("TBM: Servers-" SPC %tbmservers SPC "Players:" SPC %tbmplayers); 
   echo("RtB: Servers-" SPC %rtbservers SPC "Players:" SPC %rtbplayers); 
}

function serverCmdbotfollow(%client, %val, %victimId) {
if(!$nobotaction) {
  if (((%client.lastswitch.owner == getrawip(%client)) && (%client.ismod)) || (%client.isadmin || %client.issuperadmin)) {
    if(%client.lastswitch.AIPlayer == 1) {
      if(%val) {
      %bot = %client.lastswitch;
      %bot.orbiting = %victimId;
      if(%bot.following)
        %bot.following = -1;
      chatMessageall(%client, '\c2%1 has set %2-bot to orbit %3 forever!', %client.namebase , %bot.getshapename(), %bot.following.namebase);
      botwalkorbit(%bot);
      }
      else {
      %bot = %client.lastswitch;
      %bot.following = %victimId;
      if(%bot.orbiting)
        %bot.orbiting = -1;
      chatMessageall(%client, '\c2%1 has set %2-bot to follow %3 forever!', %client.namebase , %bot.getshapename(), %bot.following.namebase);
      botwalk(%bot);
      }
    }
    else {
    messageclient(%client, 'You do not have a bot selected!');
    }
  } 
}   
}
$nobotaction = 0;

function serverCmdbotfollowbot(%client, %val, %victimId) {
if(!$nobotaction) {
  if (((%client.lastswitch.owner == getrawip(%client)) && (%client.ismod)) || (%client.isadmin || %client.issuperadmin)) {
    if(%client.lastswitch.AIPlayer == 1) {
      if(%val) {
      %bot = %client.lastswitch;
      %bot.following = %client.savedbot;
      if(%bot.orbiting)
        %bot.orbiting = -1;
      chatMessageall(%client, '\c2%1 has set %2-bot to orbit %3-bot forever!', %client.namebase , %bot.getshapename(), %bot.following.getshapename());
      botorbitbot(%bot);
      }
      else {
      %bot = %client.lastswitch;
      %bot.orbiting = %client.savedbot;
      if(%bot.following)
        %bot.following = -1;
      chatMessageall(%client, '\c2%1 has set %2-bot to follow %3-bot forever!', %client.namebase , %bot.getshapename(), %bot.following.getshapename());
      botfollowbot(%bot);
      }
    }
    else {
    messageclient(%client, 'You do not have a bot selected!');
    }
  } 
}   
}

function botwalk(%bot) {
if(!$nobotaction) {
  if(isobject(%bot.following) && isobject(%bot)) {
  %botplace = %bot.getTransform();
  %victimplace = %bot.following.player.gettransform();
  %botx = getWord(%botplace, 0);
  %victimx = getword(%victimplace, 0);
  %boty = getWord(%botplace, 1);
  %victimy = getword(%victimplace, 1);
  %botz = getWord(%botplace, 2);
  %victimz = getword(%victimplace,3);
  if((%botx > %victimx) && (%boty > %victimy)) {
    %bot.setaimobject(%bot.following.player);
    %bot.setMoveDestination(%victimx + 2 SPC %victimy + 2 SPC %victimz);
  }
  if((%botx < %victimx) && (%boty < %victimy)) {
    %bot.setaimobject(%bot.following.player);
    %bot.setMoveDestination(%victimx - 2 SPC %victimy - 2 SPC %victimz);			
  }
  if((%botx > %victimx) && (%boty < %victimy)) {
    %bot.setaimobject(%bot.following.player);
    %bot.setMoveDestination(%victimx + 2 SPC %victimy - 2 SPC %victimz);		
  }
  if((%botx < %victimx) && (%boty > %victimy)) {
    %bot.setaimobject(%bot.following.player);
    %bot.setMoveDestination(%victimx - 2 SPC %victimy + 2 SPC %victimz);	
  }
  if((%botx == %victimx) && (%boty > %victimy)) {
    %bot.setaimobject(%bot.following.player);
    %bot.setMoveDestination(%victimx SPC %victimy + 2 SPC %victimz);
  }
  if((%botx == %victimx) && (%boty < %victimy)) {
    %bot.setaimobject(%bot.following.player);
    %bot.setMoveDestination(%victimx SPC %victimy - 2 SPC %victimz);			
  }
  if((%botx > %victimx) && (%boty == %victimy)) {
    %bot.setaimobject(%bot.following.player);
    %bot.setMoveDestination(%victimx + 2 SPC %victimy SPC %victimz);		
  }
  if((%botx < %victimx) && (%boty == %victimy)) {
    %bot.setaimobject(%bot.following.player);
    %bot.setMoveDestination(%victimx - 2 SPC %victimy SPC %victimz);	
  }
    if(isObject(%bot)) {
    schedule(10, 0, botwalk, %bot);
    }	
  }
}
else {
%bot.following = -1;
}
}

function botwalkorbit(%bot) {
if(!$nobotaction) {
  if(isobject(%bot.orbiting) && isobject(%bot)) {
  %botplace = %bot.getTransform();
  %victimplace = %bot.orbiting.player.gettransform();
  %botx = getWord(%botplace, 0);
  %victimx = getword(%victimplace, 0);
  %boty = getWord(%botplace, 1);
  %victimy = getword(%victimplace, 1);
  %botz = getWord(%botplace, 2);
  %victimz = getword(%victimplace,3);
  %bot.setaimobject(%bot.orbiting.player);
  schedule(25,0,botmove,%bot,%victimx,%victimy,%victimz,1);
  schedule(225,0,botmove,%bot,%victimx,%victimy,%victimz,2);	
  schedule(425,0,botmove,%bot,%victimx,%victimy,%victimz,3);	
  schedule(625,0,botmove,%bot,%victimx,%victimy,%victimz,4);	
  schedule(825,0,botmove,%bot,%victimx,%victimy,%victimz,5);	
  schedule(1025,0,botmove,%bot,%victimx,%victimy,%victimz,6);	
  schedule(1225,0,botmove,%bot,%victimx,%victimy,%victimz,7);	
  schedule(1425,0,botmove,%bot,%victimx,%victimy,%victimz,8);			
  schedule(1825,0,botwalkorbit,%bot);
  }
}
else {
%bot.following = -1;
}
}

function botmove(%bot,%victimx,%victimy,%victimz,%val) {
if(%val == 1){
%bot.setMoveDestination(%victimx + 5 SPC %victimy + 5 SPC %victimz);
}
else if(%val == 2){
%bot.setMoveDestination(%victimx     SPC %victimy + 5 SPC %victimz);
}
else if(%val == 3){
%bot.setMoveDestination(%victimx - 5 SPC %victimy + 5 SPC %victimz);
}
else if(%val == 4){
%bot.setMoveDestination(%victimx - 5 SPC %victimy     SPC %victimz);
}
else if(%val == 5){
%bot.setMoveDestination(%victimx - 5 SPC %victimy - 5 SPC %victimz);
}			
else if(%val == 6){
%bot.setMoveDestination(%victimx     SPC %victimy - 5 SPC %victimz);
}
else if(%val == 7){
%bot.setMoveDestination(%victimx + 5 SPC %victimy - 5 SPC %victimz);
}				
else if(%val == 8){
%bot.setMoveDestination(%victimx + 5 SPC %victimy     SPC %victimz);
}
}

function botfollowbot(%bot) {
if(!$nobotaction) {
  if(isobject(%bot.following) && isobject(%bot)) {
  %botplace = %bot.getTransform();
  %victimplace = %bot.following.gettransform();
  %botx = getWord(%botplace, 0);
  %victimx = getword(%victimplace, 0);
  %boty = getWord(%botplace, 1);
  %victimy = getword(%victimplace, 1);
  %botz = getWord(%botplace, 2);
  %victimz = getword(%victimplace,3);
  if((%botx > %victimx) && (%boty > %victimy)) {
    %bot.setaimobject(%bot.following);
    %bot.setMoveDestination(%victimx + 2 SPC %victimy + 2 SPC %victimz);
  }
  if((%botx < %victimx) && (%boty < %victimy)) {
    %bot.setaimobject(%bot.following);
    %bot.setMoveDestination(%victimx - 2 SPC %victimy - 2 SPC %victimz);			
  }
  if((%botx > %victimx) && (%boty < %victimy)) {
    %bot.setaimobject(%bot.following.player);
    %bot.setMoveDestination(%victimx + 2 SPC %victimy - 2 SPC %victimz);		
  }
  if((%botx < %victimx) && (%boty > %victimy)) {
    %bot.setaimobject(%bot.following);
    %bot.setMoveDestination(%victimx - 2 SPC %victimy + 2 SPC %victimz);	
  }
  if((%botx == %victimx) && (%boty > %victimy)) {
    %bot.setaimobject(%bot.following);
    %bot.setMoveDestination(%victimx SPC %victimy + 2 SPC %victimz);
  }
  if((%botx == %victimx) && (%boty < %victimy)) {
    %bot.setaimobject(%bot.following);
    %bot.setMoveDestination(%victimx SPC %victimy - 2 SPC %victimz);			
  }
  if((%botx > %victimx) && (%boty == %victimy)) {
    %bot.setaimobject(%bot.following);
    %bot.setMoveDestination(%victimx + 2 SPC %victimy SPC %victimz);		
  }
  if((%botx < %victimx) && (%boty == %victimy)) {
    %bot.setaimobject(%bot.following);
    %bot.setMoveDestination(%victimx - 2 SPC %victimy SPC %victimz);	
  }
    if(isObject(%bot)) {
    schedule(10, 0, botfollowbot, %bot);
    }	
  }
}
else {
%bot.following = -1;
}
}

function botorbitbot(%bot) {
if(!$nobotaction) {
  if(isobject(%bot.orbiting) && isobject(%bot)) {
  %botplace = %bot.getTransform();
  %victimplace = %bot.following.gettransform();
  %botx = getWord(%botplace, 0);
  %victimx = getword(%victimplace, 0);
  %boty = getWord(%botplace, 1);
  %victimy = getword(%victimplace, 1);
  %botz = getWord(%botplace, 2);
  %victimz = getword(%victimplace,3);
  %bot.setaimobject(%bot.orbiting);
  schedule(25,0,botmove,%bot,%victimx,%victimy,%victimz,1);
  schedule(225,0,botmove,%bot,%victimx,%victimy,%victimz,2);	
  schedule(425,0,botmove,%bot,%victimx,%victimy,%victimz,3);	
  schedule(625,0,botmove,%bot,%victimx,%victimy,%victimz,4);	
  schedule(825,0,botmove,%bot,%victimx,%victimy,%victimz,5);	
  schedule(1025,0,botmove,%bot,%victimx,%victimy,%victimz,6);	
  schedule(1225,0,botmove,%bot,%victimx,%victimy,%victimz,7);	
  schedule(1425,0,botmove,%bot,%victimx,%victimy,%victimz,8);			
  schedule(1825,0,botorbitbot,%bot);
  }
}
else {
%bot.orbiting = -1;
}
}

//function beforeword(%str, %word) {
  //if(strstrword(%str, %word) != -1) {
    //%i = strstrword(%str, %word);
    //return getWords(%str, 0, %i - 1);
  //}
//}
 
//function afterword(%str, %word) {
  //if(strstrword(%str, %word) != -1) {
    //%i = strstrword(%str, %word) + 1;
    //%o = getwordcount(%str);
    //return getWords(%str, %i, %o);
  //}
//}
 
//function strstrword(%str, %part) {
 //while(getWord(%str,%i) !$= ""){
  //if(getWord(%str,%i) $= %part) return %i;
  //%i++;
 //}
 //return %i;
//} 


//function strwordlen(%str) {
 //while(getWord(%str,%i) !$= ""){
  //%i++;
 //}
 //return %i;
//}

//-----------------------------------------------------------------------------
// A player's name could be obtained from the auth server, but for
// now we use the one passed from the client.
// %realName = getField( %authInfo, 0 );
// Now it'll include some needed and unneed stuff
function GameConnection::setPlayerName(%client,%name)
{
   %client.sendGuid = 0;

   // Minimum length requirements
   %name = stripTrailingSpaces( strToPlayerName( cleanstringemot( %name ) ) );
   if ( strlen( %name ) < 2 )
      %name = "*UNSPECIFIED*";
      
   // Make sure the alias is unique, we'll hit something eventually
   if (%name $= "Mrbot")
      %name = "*NoBot*";

   // Make sure the alias is unique, we'll hit something eventually
   if (!isNameUnique(%client, %name))
   {
      %isUnique = false;
      for (%suffix = 1; !%isUnique; %suffix++)  {
         %nameTry = %name @ "." @ %suffix;
         %isUnique = isNameUnique(%client, %nameTry);
      }
      %name = %nameTry;
   }

   // Tag the name with the "smurf" color:
   %client.nameBase = %name;
   %client.name = addTaggedString("\cp\c8" @ %name @ "\co");
}
///////////////////////////////////////

///////////////tbm\main////////////////
///////////tbm\client\config///////////
////////////tbm\client\init////////////
////tbm\client\scripts\default.bind////
/////tbm\client\scripts\optionsDlg/////
//tbm\client\scripts\serverConnection//
/////tbm\client\scripts\playerlist/////
///////tbm\client\scripts\mmgui////////
////tbm\client\ui\MMGuiPic\MMGuiPic////
///////////tbm\server\mmtools//////////
/////////tbm\server\mrbotstore/////////
////////tbm\server\scripts\game////////

///////////////////////////////////////