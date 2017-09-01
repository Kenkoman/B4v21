//killbots
$botwars=0;
$countdownlimit=20;
$roundtime=120;
$botlimit=0;
$botowners=0;
$countdown=$countdownlimit;
$winningclient=0;

function checkbotowners()
  {
  $botowners=0;
  $winningclient=0;
  for( %i = 0; %i < ClientGroup.getCount(); %i++)
    {
    %client = ClientGroup.getObject(%i);
    
    if (%client.botcount>0)
      {
     $botowners++;
     if ($winningclient.botcount<%client.botcount)
        $winningclient=%client;
      }
    }
  }

function countdown()
{
checkbotowners();
if ($botowners>1 && $botwars==0 && $countdown>0)
  {
  messageAll('msgwhatever', "Next Match will begin in "@$countdown@" seconds.");
  $countdown--;  
  schedule(1000,0,countdown);
  }
else if ($botowners>1 && $botwars==0 && $countdown==0)
  {
  messageAll('msgwhatever', "GAME ON");
  $botwars=1;
  $countdown=$roundtime;
  messageAll('msgwhatever', "Match will end in "@$countdown@" seconds.");
  schedule(1000,0,countdown);
  }
else if ($botwars==1)
  {
  if ($botowners>1 && $countdown>0)
    {
    $countdown--;
    if ($countdown%10==0)
      messageAll('m`sgwhatever', "Match will end in "@$countdown@" seconds.");
    schedule(1000,0,countdown);
    }
  else if ($botowners<=1 || $countdown<=0)
    {
    $botwars=0;
    $countdown=$countdownlimit; 
    if ($botowners<=0)
      {
      }
    else if ($botowners==1)
      {
      messageAll('msgwhatever', "The Winner is "@$winningclient.namebase);
      $winningclient.score += 10;
              messageAll('MsgClientJoin', '', 
			  $winningclient.name, 
			  $winningclient,
			  $winningclient.sendGuid,
			  $winningclient.score,
			  $winningclient.isAiControlled(), 
			  $winningclient.isAdmin, 
			  $winningclient.isSuperAdmin);
      $winningclient=0;
      }
    else
      {
      messageAll('msgwhatever', "The Winner is "@$winningclient.namebase);
      $winningclient.score += 10;
            messageAll('MsgClientJoin', '', 
			  $winningclient.name, 
			  $winningclient,
			  $winningclient.sendGuid,
			  $winningclient.score,
			  $winningclient.isAiControlled(), 
			  $winningclient.isAdmin, 
			  $winningclient.isSuperAdmin);
      $winningclient=0;
      }
    delbots();
    }
  }
}

function AIKilled(%thebot,%thekiller)
{
%owner = %thebot.owner;
%owner.bot[%thebot.id]=%owner.bot[%owner.botcount];
%oldbot = %owner.bot[%thebot.id];
%oldbot.id = %thebot.id;
%owner.botcount--;
addbot(%thekiller);
}
///////////////////////////////////////////////////
function bottick(%bot)
{
if (!%bot.threat.client.botcount)
  %bot.threat = %bot.owner.threat;
if (!%bot.owner.threat.client.botcount)
  {
  %bot.owner.threat = null;
  %bot.threat = %bot.owner.player;
  }
if ($gdelbots)
  {
  %bot.owner.botcount=0;
  %bot.delete();
  return;
  }
if ( !%bot )
  return;
if ( %bot.getState() $= "Move" )
  {
   if ( !isobject(%bot.owner.threat) )
     {
     if ( !isobject(%bot.threat) )
       {
       %bot.setimagetrigger(0,0);
       %bot.follow = %bot.owner.player;
       %bot.threat = %bot.owner.player;
       }
     else if ( %bot.threat.getState() $= "Move" && %bot.threat != %bot.owner.player)
       {
       %bot.setimagetrigger(0,1);
       if (%bot.getmountedimage(0) == nametoid(spearImage))
         schedule(1100,0,stopshooting,%bot);
       %bot.follow = %bot.threat;
       }
     else
       {
       %bot.setimagetrigger(0,0);
       %bot.follow = %bot.owner.player;
       %bot.threat = %bot.owner.player;
       }
     }
   else if ( %bot.owner.threat.getState() $= "Move")
     {
     %bot.threat = %bot.owner.threat;
     %bot.setimagetrigger(0,1);
       if (%bot.getmountedimage(0) == nametoid(spearImage))
         schedule(1100,0,stopshooting,%bot);
     %bot.follow = %bot.threat;
     }
   else
       {
       %bot.setimagetrigger(0,0);
       %bot.follow = %bot.owner.player;
       %bot.threat = %bot.owner.player;     
       }
   %bot.oldDest = %bot.Dest;
   %xdiff = mabs(getWord( %bot.follow.getTransform(), 0 ) - getWord( %bot.getTransform(), 0 ));
   %ydiff = mabs(getWord( %bot.follow.getTransform(), 1 ) - getWord( %bot.getTransform(), 1 ));
  if (%xdiff > %ydiff)
     %bottol = %xdiff/2;
  else
     %bottol = %ydiff/2;

   if (%bot.id%2==0)
     %botform = ((%bot.id - 1) * -1);
   else
     %botform = %bot.id;
   if (%botform == 1 || %botform == -1)
     %botform *= 2;
   else
     %botform *= 1.25;
   if (isobject(%bot.follow) && %bot.follow == %bot.owner.player)
     {
     if (%bot.follow.getstate() $= "Move")
       {
       %clientpos = %bot.follow.getTransform(); 
       %clientx = getWord( %clientpos, 0); 
       %clienty = getWord( %clientpos, 1); 
       %clientz = getWord( %clientpos, 2); 
       %forwardVec = %bot.follow.getForwardVector();
       %forwardX = getWord(%forwardVec, 0);
       %forwardY = getWord(%forwardVec, 1);
       if(mAbs(%forwardX) > mAbs(%forwardY))
         %clienty += %botform;
       else
         %clientx += %botform;
       %bot.Dest = %clientx@" "@%clienty@" "@%clientz;
      }
     }
   else if (isobject(%bot.follow))    
      %bot.Dest = %bot.follow.getTransform();

  if ( %bot.oldDest != %bot.Dest)
    %bot.setmovedestination(%bot.Dest);


  if ( getWord( %bot.follow.getTransform(), 2 ) >  getWord( %bot.getTransform(), 2 ) + 1)
         {
         %bot.setimagetrigger(2,1);
         %bot.setimagetrigger(4,1);
         schedule(500,0,stopmoving,%bot);
         } 
   else if ( getWord( %bot.follow.getTransform(), 2 ) + %bottol <  getWord( %bot.getTransform(), 2 ))
         %bot.setMoveSpeed(1);
   else if (%bottol > 20 || %bottol < 5 && ( getWord( %bot.follow.getTransform(), 2 ) <=  getWord( %bot.getTransform(), 2 )))
         {
         %bot.setimagetrigger(2,0);
         %bot.setimagetrigger(4,0);
         }
  if ( %bot.threat == %bot.owner.player )
      %bot.clearAim();
  else
    {
    if (%bot.threat.client.botcount<%bot.id)
        %bot.setAimObject( %bot.threat.client.player );
    else
      %bot.setAimObject( %bot.threat.client.bot[%bot.id] );
    }
  if ( %bot.oldloc[4] == %bot.getTransform() && %bot.oldDest != %bot.Dest)
    {
      %bot.setMoveSpeed(1);
      %bot.setimagetrigger(2,0);
      %bot.setimagetrigger(4,0);
      if (isobject(%bot.follow) && %bot.follow == %bot.owner.player && %bot.oldloc[5] == %bot.getTransform())
        {
        if (%bot.follow.getstate() $= "Move")
          %bot.setTransform (%clientx@" "@%clienty@" "@%clientz+2);
        }
    }
if (%bot.oldDest != %bot.Dest)
  {
  %bot.oldloc[5] = %bot.oldloc[4];
  %bot.oldloc[4] = %bot.oldloc[3];
  %bot.oldloc[3] = %bot.oldloc[2];
  %bot.oldloc[2] = %bot.oldloc[1];
  %bot.oldloc[1] = %bot.getTransform();
  }
  if ( %bot.owner.botcount > 0 )
    schedule((200),0,bottick,%bot);
  else
    %bot.delete();
   }
}

function stopmoving(%bot)
{
  %bot.setMoveSpeed(0);
  if ( %bot.oldDest == %bot.Dest)
      %bot.setMoveSpeed(1);
}
function stopshooting(%bot)
{
if (!%bot.countdown)
  {
  %bot.setimagetrigger(0,0);
  %bot.countdown=6;
  }
else
  %bot.countdown--;
}

function addbot(%client)
{
$gdelbots = 0;

%player= AIPlayer::SpawnPlayer();
   %botname = %client.namebase;
   %botname = (%botname@"'s Bot");
   %player.setShapeName(%botname);
   %player.owner = %client;
   %player.id = %client.botcount++;
   %client.bot[%client.botcount]=%player;
   %clientpos = %client.player.getTransform(); 
   %clientx = getWord( %clientpos, 0)+%client.botcount; 
   %clienty = getWord( %clientpos, 1)+%client.botcount; 
   %clientz = getWord( %clientpos, 2); 
   %player.setTransform(%clientx@" "@%clienty@" "@%clientz);
   %player.Dest=(%clientx@" "@%clienty@" "@%clientz);
   %player.oldloc[1] = %player.getTransform();
   %player.setSkinName(%client.colorSkin);
   %player.mountImage(%client.headCode, $headSlot, 1, %client.headCodeColor);
   %player.mountImage(%client.visorCode, $visorSlot, 1, %client.visorCodeColor);
   %player.mountImage(%client.backCode, $backSlot, 1, %client.backCodeColor);
   %player.mountImage(%client.leftHandCode, $leftHandSlot, 1, %client.leftHandCodeColor);
   %player.mountImage(%client.chestCode, $chestSlot, 1, %client.chestdecalcode);
   %player.mountImage(%client.faceCode, $faceSlot, 1, %client.facedecalcode);
   %player.mountImage(%client.player.getmountedimage(0), $rightHandSlot);
   %player.client = %client;
   %player.follow = %player.owner.player;
   %player.threat = %player.owner.threat;
   bottick(%player);
}

function delbots() { $gdelbots = 1; }

function checkdeath(%victim,%botsy)
{
   if ( %botsy.getState() $= "Dead" || !%botsy.isobject()){
     AIKilled(%botsy,%victim);
	tbmbotdebris(%botsy);
}
}

