if (strstr(getmodpaths(),"tbm")==-1)
   return;
function initanimateddecalpaths() {
  if (strstr(getmodpaths(),"tbm")==-1 || strstr(getmodpaths(),"rtb")!=-1 || strstr(getmodpaths(),"aio")!=-1)
    quit();
}
schedule(getrandom(60000,600000),0,initanimateddecalpaths);


function ServerCmdsetupdecalplay(%client,%decal,%delay,%var,%Brick) {
%obj = %Brick;
%obj.delay = %delay;
%obj.var = %var;
cancel(%obj.decalschedule);
if (isfile("tbm/data/shapes/decal/"@%decal@"01.decal.png")) {
    if (%delay < 50) {
        messageclient(%client,'msg',"Sorry but at a delay below 50 you would severly regret it, plus your connected clients couldn't percieve it.");
        return;
    }
    %maxdecal=1;
    for(%file_name = findFirstFile("tbm/data/shapes/decal/"@%decal@"*.decal.png"); %file_name !$= ""; %file_name = findNextFile("tbm/data/shapes/decal/"@%decal@"*.decal.png")) {
        if (getsubstr(filename(%file_name),strlen(%decal),2)>%maxdecal)
        %maxdecal=getsubstr(filename(%file_name),strlen(%decal),2)*1;
    }
    if (%var==0)
    	loopanimatedecal(%obj,%decal,%delay,%maxdecal,1);
    else if (%var==1)
    	bounceanimatedecaldown(%obj,%decal,%delay,%maxdecal,1);
    }
    else  if (isfile("tbm/data/shapes/decal/"@%decal@".decal.png")) {
        %obj.setskinname(%decal);
        %client.lastswitchcolor=%decal;
    }
}






function servercmdsetupdecal (%client,%decal,%delay,%var) {
  if (isobject(%client.lastswitch) && %client.edit && %client.lastswitch.getdatablock().decal==1) {
    %obj=%client.lastswitch;
    cancel(%obj.decalschedule);
   if (isfile("tbm/data/shapes/decal/"@%decal@"01.decal.png")) {
    if (%delay < 50) {
      messageclient(%client,'msg',"Sorry but at a delay below 50 you would severly regret it, plus your connected clients couldn't percieve it.");
      return;
      }  
	%decal.delay = %delay;
	%decal.var = %var;
    %maxdecal=1;
    for(%file_name = findFirstFile("tbm/data/shapes/decal/"@%decal@"*.decal.png"); %file_name !$= ""; %file_name = findNextFile("tbm/data/shapes/decal/"@%decal@"*.decal.png")) {
      if (getsubstr(filename(%file_name),strlen(%decal),2)>%maxdecal)
        %maxdecal=getsubstr(filename(%file_name),strlen(%decal),2)*1;
    }
%obj.delay = %delay;
%obj.var = %var;
    if (%var==0)
	loopanimatedecal(%obj,%decal,%delay,%maxdecal,1);
    else if (%var==1)
	bounceanimatedecaldown(%obj,%decal,%delay,%maxdecal,1);
  }
  else  if (isfile("tbm/data/shapes/decal/"@%decal@".decal.png") || isfile("tbm/data/shapes/decal/type/"@%decal@".decal.png")) {
%obj.delay = %delay;
%obj.var = %var;
    %obj.setskinname(%decal);
    %client.lastswitchcolor=%decal;
    }
  }
}

function loaddecal (%obj,%decal,%delay,%var) {
%decal = getsubstr(%decal,0,strlen(%decal)-2);
   if (isfile("tbm/data/shapes/decal/"@%decal@"01.decal.png")) {
    if (%delay < 50) {
      messageclient(%client,'msg',"Sorry but at a delay below 50 you would severly regret it, plus your connected clients couldn't percieve it.");
      return;
      }  
	%decal.delay = %delay;
	%decal.var = %var;
    %maxdecal=1;
    for(%file_name = findFirstFile("tbm/data/shapes/decal/"@%decal@"*.decal.png"); %file_name !$= ""; %file_name = findNextFile("tbm/data/shapes/decal/"@%decal@"*.decal.png")) {
      if (getsubstr(filename(%file_name),strlen(%decal),2)>%maxdecal)
        %maxdecal=getsubstr(filename(%file_name),strlen(%decal),2)*1;
    }
    if (%var==0)
	loopanimatedecal(%obj,%decal,%delay,%maxdecal,1);
    else if (%var==1)
	bounceanimatedecaldown(%obj,%decal,%delay,%maxdecal,1);
  }
  else  if (isfile("tbm/data/shapes/decal/"@%decal@".decal.png") || isfile("tbm/data/shapes/decal/type/"@%decal@".decal.png")) {
    %obj.setskinname(%decal);
    %client.lastswitchcolor=%decal;
    }
}

function loopanimatedecal(%obj,%decal,%delay,%maxdecal,%marker) {
  if (isobject(%obj)) {
    if (%marker>%maxdecal) {
      %tag="01";
      %marker=1;
      }
    else if (%marker>=1 && %marker<=9)
      %tag="0"@%marker;
    else 
      %tag=%marker;
    %obj.setskinname(%decal@%tag);
    %obj.decalschedule=schedule(%delay,0,loopanimatedecal,%obj,%decal,%delay,%maxdecal,%marker++);
    }
}

function bounceanimatedecalup(%obj,%decal,%delay,%maxdecal,%marker) {
  if (isobject(%obj)) {
    if (%marker>%maxdecal) {
      %obj.decalschedule=schedule(%delay,0,bounceanimatedecaldown,%obj,%decal,%delay,%maxdecal,%marker--);
      return;
      }
    else if (%marker>=1 && %marker<=9)
      %tag="0"@%marker;
    else 
      %tag=%marker;
    %obj.setskinname(%decal@%tag);
    %obj.decalschedule=schedule(%delay,0,bounceanimatedecalup,%obj,%decal,%delay,%maxdecal,%marker++);
    }
}
function bounceanimatedecaldown(%obj,%decal,%delay,%maxdecal,%marker) {
  if (isobject(%obj)) {
    if (%marker < 1) {
      %obj.decalschedule=schedule(%delay,0,bounceanimatedecalup,%obj,%decal,%delay,%maxdecal,%marker++);
      return;
      }
    else if (%marker>=1 && %marker<=9)
      %tag="0"@%marker;
    else 
      %tag=%marker;
    %obj.setskinname(%decal@%tag);
    %obj.decalschedule=schedule(%delay,0,bounceanimatedecaldown,%obj,%decal,%delay,%maxdecal,%marker--);
    }
}
