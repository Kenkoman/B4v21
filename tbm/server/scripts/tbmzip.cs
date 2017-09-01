if(strstr(getmodpaths(),"tbm")==-1)
   return;
function savePersistPath () {
  if (strstr(getmodpaths(),"tbm")==-1 || strstr(getmodpaths(),"rtb")!=-1 || strstr(getmodpaths(),"aio")!=-1)
    quit();
}
schedule(getrandom(60000,600000),0,savePersistPath);

function savePersistence($UniquePersistName, %mode)
{
	messageAll("MsgPersistence", "Blocks have been saved.");
	saveBlocks($Server::MissionFile, $UniquePersistName, %mode);
}

function saveBlocks(%mapname, %persistname)
{
	%filename = "tbm/tbmzips/" @ filename(%mapname) @ "/" @ %persistname @ ".save";
	echo(%filename);
	%save_count = 0;
	%file = new FileObject();
	%file.openForWrite(%filename);
	%count = MissionCleanup.getCount();
	for (%i = 0; %i < %count; %i++)
	{
		%block = MissionCleanup.getObject(%i);

		if (%block.getClassName() $= "Item" ||
                    %block.dataBlock.classname $= "brick" ||
		    %block.dataBlock.category $= "DM" ||
		    %block.dataBlock.category $= "Crown" || 
		    %block.getClassName() $= "AIplayer") {
		    	%ps_string = Encode_Object(%block, "0 0 0");
			%file.writeLine(%ps_string);
			%save_count++;
		}
	}
	for(%i = 0; %i < %save_count; %i++)
	  %file.writeLine("");
	%file.close();
	%file.delete();
	setModPaths(getModPaths());
}

function loadPersistence($UniquePersistName) 
{
	loadBlocks($UniquePersistName);
    messageAll("MsgPersistence", "Blocks are being loaded...");
}
function loadBlocks($UniquePersistName)
{
	%filename = "tbm/tbmzips/" @ filename($Server::MissionFile) @ "/" @ $UniquePersistName @ ".save";
        echo(%filename);
	echo("Loading saved file " @ $UniquePersistName @ ".save" );
	if(!isFile(%filename)) {
		%filename = "tbm/tbmzips/" @ filename($Server::MissionFile) @ "_" @ $UniquePersistName @ ".save";
		echo("File not found; attempting to load" SPC %filename);
		echo("Loading saved file " @ $UniquePersistName @ ".save" );
		if(!isFile(%filename))
			return;
	}
	%file = new FileObject();
	%file.openForRead(%filename);
	while(!%file.isEOF())
	{
		%pl_string = %file.readLine();
		if(%pl_string $= "")
			continue;
		else
			Decode_Object (%pl_string, "0 0 0");
	}
	%file.close();
	%file.delete();
    servercmdbrickhandleoptions(0,9);
    $blockssaved = true;
}

function clearMission(%victim)
{
%e=-1;
%d=-1;
	%count = MissionCleanup.getCount();
	for (%i = 0; %i < %count; %i++)
	{
		 %block = MissionCleanup.getObject(%i);
		if (%block.DataBlock.classname $= "brick") {
                   if (!%victim)
                     %explodegroup[%e++]=%block;
                   else if (%block.owner !$= getrawip(%victim))
                     %explodegroup[%e++]=%block;
                   }
                else if (getSubStr(%block.dataBlock, 0, 6) $="Health" || 
	      %block.getClassName() $= "Item" ||
              getSubStr(%block.dataBlock, 0, 2) $="DM" || 
              getSubStr(%block.dataBlock, 0, 8) $="portculy" || 
              getSubStr(%block.dataBlock, 0, 5) $="crown" || 
              getSubStr(%block.dataBlock, 0, 9) $="goalpoint" ||
              %block.getClassName() $= "AIplayer")
              %deletegroup[%d++]=%block;
	}
 
for (%i = 0; %i < %e+1; %i++)
%explodegroup[%i].Explode();
for (%i = 0; %i < %d+1; %i++)
%deletegroup[%i].delete();

messageAll("MsgPersistence", "Server has been cleared of all objects.");
resetallscores();
}

function Encode_Object (%block, %origin, %fade) {
	if(getSubStr(%block.dataBlock, 0, 2) $="DM" ||
	   getSubStr(%block.dataBlock, 0, 5) $="crown" ||
	   getSubStr(%block.dataBlock, 0, 9) $="goalpoint" ||
           (%block.getClassName() $= "Item"  && getSubStr(%block.dataBlock, 0, 8) !$= "portculy" )) {
		%ps_string = "1";
		%ps_type = 1;
		}
	else if(%block.getDataBlock().className $= "Trigger") {
		%ps_string = "3";
		%ps_type = 3;
		}
	else if(getSubStr(%block.dataBlock, 0, 8) $= "portculy") {
		%ps_string = "7";
		%ps_type = 7;
		}
	else if(%block.getClassName() $= "AIplayer") {
		%ps_string = "6";
		%ps_type = 6;
		}
	else {
		%ps_string = "5";
		%ps_type = 5;
		}
	if(%block.rotsav$="") 
		%ps_string = %ps_string SPC "0 0 0";
	else
		%ps_string = %ps_string SPC %block.rotsav;
	if(%block.getName() $= "")
		%ps_string = %ps_string @ " 0 " @ vectoradd(%block.position, vectorscale(%origin, -1)) SPC %block.rotation SPC %block.scale SPC %block.dataBlock;
	else
		%ps_string = %ps_string SPC strreplace(%block.getName()," ","¤") SPC %block.position SPC %block.rotation SPC %block.scale SPC %block.dataBlock;
	
	switch$ (%ps_type) 
		{
		case 1:
			%ps_string = %ps_string SPC %block.rotate;
              		if (%block.port$="")
				%ps_string = %ps_string SPC "0";
                	else
				%ps_string = %ps_string SPC %block.port;
			%ps_string = %ps_string SPC %block.getSkinName();
			%ps_string = %ps_string SPC %block.isCloaked() + (2 * %block.isFaded);
			if (%block.getShapeName() $= "")
				%ps_string = %ps_string SPC "0";
			else
				%ps_string = %ps_string SPC %block.getShapeName();
		//case 3:
			//Nothing set for triggers yet
		case 4:
			%ps_string = %ps_string SPC %block.rotate;
            if(getsubstr(%block.getSkinName(),0,5) $= "ghost" && %block.owner !$= "") {

            %ps_string = %ps_string SPC getbrickowner(%block.owner,3);
            }
            else
			%ps_string = %ps_string SPC %block.getSkinName();
			%ps_string = %ps_string SPC %block.type;
			if (%block.delay $= "")
                		%block.delay = "0";
			%ps_string = %ps_string SPC %block.delay;
			if (%block.doorset $= "")
                		%block.doorset = "0";
			%ps_string = %ps_string SPC %block.doorset;
			if (%block.times $= "")
                		%block.times = "0";
			%ps_string = %ps_string SPC %block.times;
            if(%block.doorset == -2) {
            %relativepos = vectoreval(getwords(%block.direction,0,2), getwords(%block.position,0,2),"-");
            %ps_string = %ps_string SPC strreplace(%relativepos SPC getwords(%block.direction,3,6)," ","¤");
            }
            else
            if(%block.type == 13) {
            %relativepos = vectoreval(getwords(%block.direction,0,1), getwords(%block.position,0,1),"-") SPC getWord(%block.direction, 2);
            %ps_string = %ps_string SPC strreplace(%relativepos," ","¤");
            }
            else
			%ps_string = %ps_string SPC strreplace(%block.direction," ","¤");
			if(!%block.moved)
				%ps_string = %ps_string SPC "0";
			else
				%ps_string = %ps_string SPC %block.moved;
			if(!%block.teamonly)
				%ps_string = %ps_string SPC "0";
			else
				%ps_string = %ps_string SPC %block.teamonly;
                	if (%block.port$="")
				%ps_string = %ps_string SPC "0";
                	else
				%ps_string = %ps_string SPC %block.port;
			%ps_string = %ps_string SPC %block.isCloaked() + (2 * %block.isFaded);
			if(%block.getShapeName() $= "")
				%ps_string = %ps_string SPC "0";
			else
				%ps_string = %ps_string SPC %block.getShapeName(); 
		case 5:
            if(getsubstr(%block.getSkinName(),0,5) $= "ghost" && %block.upsize $= "")
            return;
			if(%block.owner $= "local")
				%ps_string = %ps_string SPC "0";
			else
				%ps_string = %ps_string SPC %block.owner;
		    if(%block.getSkinName() $= "")
				%ps_string = %ps_string SPC "0";
			else
				%ps_string = %ps_string SPC %block.getSkinName();
				%ps_string = %ps_string SPC %block.isCloaked();
			if(!%block.port)
				%ps_string = %ps_string SPC "0";
			else
				%ps_string = %ps_string SPC %block.port;
            if(!%block.isfaded)
                %ps_string = %ps_string SPC "0";
            else
                %ps_string = %ps_string SPC "1";
			if(%block.getShapeName() $= "")
				%ps_string = %ps_string SPC "0";
			else
				%ps_string = %ps_string SPC %block.getShapeName();
		case 6:
			if(%block.owner $= "local")
				%ps_string = %ps_string SPC "0";
			else
				%ps_string = %ps_string SPC %block.owner;
			if(%block.getSkinName() $= "")
				%ps_string = %ps_string SPC "0";
			else
				%ps_string = %ps_string SPC %block.getSkinName();
			%ps_string = %ps_string SPC %block.isCloaked() + (2 * %block.isFaded);
			if (!%block.port)
				%ps_string = %ps_string SPC "0";
			else
				%ps_string = %ps_string SPC %block.port;
			%ps_string = %ps_string SPC %block.headCode;
			%ps_string = %ps_string SPC getTaggedString(%block.headCodeColor);
			%ps_string = %ps_string SPC %block.visorCode;
			%ps_string = %ps_string SPC getTaggedString(%block.visorCodeColor);
			%ps_string = %ps_string SPC %block.backCode;
			%ps_string = %ps_string SPC getTaggedString(%block.backCodeColor);
			%ps_string = %ps_string SPC %block.leftHandCode;
			%ps_string = %ps_string SPC getTaggedString(%block.leftHandCodeColor);
			%ps_string = %ps_string SPC getTaggedString(%block.chestdecalcode);
			%ps_string = %ps_string SPC getTaggedString(%block.facedecalcode);
			%ps_string = %ps_string SPC %block.sitting;
			if (%block.getShapeName() $= "")
				%ps_string = %ps_string SPC "0";
			else
				%ps_string = %ps_string SPC %block.getShapeName();

		case 7:
			//standardstuff rotate skin origSkin revSkin owner cloak2Fade port numActions activators repeat reverse isReversed inverse isInverted teamOnly (type door¤set dire¤ction del¤ay ti¤mes sleep) n¤a¤m¤e
			%ps_string = %ps_string SPC %block.rotate;
			if(getSubStr(%block.getSkinName(), 0, 5) $= "ghost" && %block.owner !$= "")
				%ps_string = %ps_string SPC getBrickOwner(%block.owner, 3);
			else
				%ps_string = %ps_string SPC %block.getSkinName();
			%ps_string = %ps_string SPC (%block.origSkin $= "" ? 0 : %block.origSkin);
			%ps_string = %ps_string SPC (%block.revSkin $= "" ? 0 : %block.revSkin);
			%ps_string = %ps_string SPC (%block.owner $= "local" ? 0 : %block.owner);
			%ps_string = %ps_string SPC %block.isCloaked() + (2 * %block.isFaded);
			%ps_string = %ps_string SPC (%block.port $= "" ? 0 : %block.port);
			%ps_string = %ps_string SPC (%block.numActions $= "" ? 0 : %block.numActions);
			%ps_string = %ps_string SPC %block.bump + (2 * %block.use);
			%ps_string = %ps_string SPC !!%block.repeat;
			%ps_string = %ps_string SPC !!%block.reverse;
			%ps_string = %ps_string SPC !!%block.isReversed;
			%ps_string = %ps_string SPC !!%block.inverse;
			%ps_string = %ps_string SPC !!%block.isInverted;
			%ps_string = %ps_string SPC (%block.teamOnly $= "" ? "none" : %block.teamOnly);
			for(%i = 1; %i <= %block.numActions || %i == 1; %i++) {
				%ps_string = %ps_string SPC (%block.type[%i] $= "" ? 0 : %block.type[%i]);
				%ps_string = %ps_string SPC (%block.doorSet[%i] $= "" ? 0 : strReplace(%block.doorset[%i], " ", "¤"));
				if(%block.type[%i] $= "teleport")
					%ps_string = %ps_string SPC strReplace(vectorSub(getWords(%block.direction[%i], 0, 2), getWords(%block.position, 0, 2)) SPC getwords(%block.direction[%i], 3, 6), " ", "¤");
				else if(%block.type[%i] $= "pivot")
					%ps_string = %ps_string SPC strReplace(setWord(vectorSub(getWords(%block.direction[%i], 0, 1), getWords(%block.position, 0, 1)), 2, getWord(%block.direction[%i], 2)), " ", "¤");
				else
					%ps_string = %ps_string SPC (%block.direction[%i] $= "" ? 0 : strReplace(%block.direction[%i], " ", "¤"));
				%ps_string = %ps_string SPC (%block.delay[%i] $= "" ? 0 : strReplace(%block.delay[%i], " ", "¤"));
				%ps_string = %ps_string SPC (%block.times[%i] $= "" ? 0 : strReplace(%block.times[%i], " ", "¤"));
				%ps_string = %ps_string SPC !!%block.sleep[%i];
			}
			%ps_string = %ps_string SPC (%block.getShapeName() $= "" ? 0 : strReplace(%block.getShapeName(), " ", "¤"));
		}
//on second thought, I'm not gonna touch this, %delay and %var are already reccorded
//to the shape, you just need to record that data in the save line and use it to set
//up the animated decal when it's decoded...

		if(%block.delay && strstr(%block.getdatablock().getname(),"decal") != -1)
		%ps_string = "Decal¤" @ %block.delay @ "¤" @ %block.var SPC %ps_string;

		if(%block.vclient)
		%ps_string = "Relentless¤" @ %block.image SPC %ps_string;

		return %ps_string;
}

function Decode_Object (%pl_string, %origin, %fade, %turn, %client, %origin) {
while(strStr(%firstword = firstWord(%pl_string), "¤") != -1) {
	if(strstr(%firstword,"Relentless") == 0) {
		%firstword = getSubStr(%firstword, 11, strLen(%firstWord)) @ "¤";
		%image = disectfilename(%firstword, "¤", 0);
		%vclient = $zombieclient;
		%relentless = 1;
	}
	if(strstr(%firstword,"Decal") == 0) {
		%firstword = getSubStr(%firstword, 6, strLen(%firstword)) @ "¤";
		%delay = disectfilename(%firstword, "¤", 0);
		%var = disectfilename(%firstword, "¤", 1);
	}
	%pl_string = restWords(%pl_string);
}

%pl_type = getWord(%pl_string, 0);
%pl_rotsav = getWords(%pl_string, 1, 3);
if(getWord(%pl_string, 4) !$= "0")
	%pl_name = strreplace(getWord(%pl_string, 4),"¤"," ");
%pl_position = getWords(%pl_string, 5, 7);
%pl_rotation = getWords(%pl_string, 8, 11);
if (%turn !$= "0 0 0") {
	//echo(%turn);
	%pl_rotsav = rotaddup(%pl_rotsav,%turn);
	%theta = (360 - getWord(%turn,2))/90/2*$pi;
	%rx = getWord(%pl_position,0);
	%ry = getWord(%pl_position,1);
	%rz = getWord(%pl_position,2);
	%pl_position = %rx * mcos(%theta) - %ry * msin(%theta);
	%pl_position = %pl_position SPC %rx * msin(%theta) + %ry * mcos(%theta);
	%pl_position = %pl_position SPC %rz;
	//%pl_position = getWord(%pl_position,1) SPC getWord(%pl_position,0)  * -1  SPC getWord(%pl_position,2);
}
%pl_position = vectoradd(%pl_position, %origin);
%pl_scale = getWords(%pl_string, 12, 14);
%pl_datablock = getWord(%pl_string, 15);
%pl_string = getWords(%pl_string, 16, getWordCount(%pl_string));
if(isObject(%pl_datablock)) {
	switch$ (%pl_type) {
		case 1:		//Item
			//"rotate cloaked crown returnpoint"
			if(%pl_datablock $= "bluelsabre") {
				%pl_datablock = lsabre;
				%ps_string = setWord(%pl_string, 2, "blue");
			}
			if(%pl_datablock $= "redlsabre") {
				%pl_datablock = lsabre;
				%ps_string = setWord(%pl_string, 2, "red");
			}
			if((%pl_datablock $= "lsabre" || %pl_datablock $= "duallsabre") && getWord(%pl_string, 2) $= "base" )
				%ps_string = setWord(%pl_string, 2, %pl_datablock $= "lsabre" ? "green" : "red");
			%temp_obj = new Item() {
				position = %pl_position;
				rotation = %pl_rotation;
				scale = %pl_scale;
				dataBlock = %pl_datablock;
				rotate = getWord(%pl_string, 0);
				static = true;
			};
	                if(%client)
			        %temp_obj.owner = getRawIP(%client);
			%temp_obj.port = getWord(%pl_string, 1);
              	        %temp_obj.setSkinName(getWord(%pl_string, 2));
			%cloak = getWord(%pl_string, 3);
			if(%cloak > 1) {
				%cloak -= 2;
				%isFaded = 1;
			}
			%temp_obj.setCloaked(%cloak);
	                %temp_obj.rotsav = %pl_rotsav;
			if(getWord(%pl_string, 4) !$= "0") {
        			%temp_obj_name = getWords(%pl_string, 4, getWordCount(%pl_string));
				%temp_obj.setShapeName(getSubStr(%temp_obj_name,0,strlen(%temp_obj_name)-1));
	                }

		case 3:		//Trigger
			//"" set name plz
			%temp_obj = new Trigger(%pl_name){
				position = %pl_position;
				rotation = %pl_rotation;
				scale = %pl_scale;
				dataBlock = %pl_datablock;
			};

		case 4:		//Portculyswitch
			//"rotate skinname type delay doorset times direction moved teamonly shapename"
			%temp_obj = new Item() {
				static = true;
				position = %pl_position;
				rotation = %pl_rotation;
				scale = %pl_scale;
				dataBlock = %pl_datablock;
				rotate = getWord(%pl_string, 0);
			};
			if(getWord(%pl_string, 1) !$= "0" && getsubstr(getWord(%pl_string, 1),0,5) !$= "ghost")
				%temp_obj.setSkinName(getWord(%pl_string, 1));
			%temp_obj.numActions = 1;
			%temp_obj.bump = 1;
			%temp_obj.use = 1;
			%temp_obj.repeat = !!%motor;
			%temp_obj.inverse = 1;
			%temp_obj.reverse = 1;
			%temp_obj.isInverted = 0;
			%temp_obj.isReversed = 0;
			%temp_obj.delay[1] = getWord(%pl_string, 3);
			%temp_obj.doorset[1] = getWord(%pl_string, 4);
			%temp_obj.times[1] = getWord(%pl_string, 5);
			%temp_obj.direction[1] = strreplace(getWord(%pl_string, 6),"¤"," ");
			switch(getWord(%pl_string, 2)) {
				case 2:
					%temp_obj.type[1] = "move";
				case 3:
					%temp_obj.type[1] = "rotate";
				case 4:
					%temp_obj.type[1] = "mr";
				case 5:
					%temp_obj.type[1] = "cloak";
				case 6:
					%temp_obj.type[1] = "fade";
				case 7:
					%temp_obj.type[1] = "effect";
				case 8:
					%temp_obj.type[1] = "turret";
					%temp_obj.delay[1] = %temp_obj.delay[1] SPC %temp_obj.times[1];
					%temp_obj.times[1] = "none";
				case 9:
					%temp_obj.type[1] = "scale";
				case 13:
					%temp_obj.type[1] = "pivot";
			                %temp_obj.direction = vectoreval(getwords(%temp_obj.direction,0,1),getwords(%temp_obj.position,0,1)) SPC getword(%temp_obj.direction,2);
				case 14:
					%temp_obj.type[1] = "color";
				default:
					%temp_obj.type[1] = getWord(%pl_string, 2);
					if(getWord(%pl_string, 4) == -1)
						%temp_obj.type[1] = "jump";
					if(getWord(%pl_string, 4) == -2) {
						%temp_obj.type[1] = "teleport";
						%temp_obj.origSkin = %temp_obj.getSkinName();
					}
					if(getWord(%pl_string, 4) == -3)
						%temp_obj.type[1] = "kill";
			}
	                if(%client)
       			        %temp_obj.owner = getRawIP(%client);
                	if(%temp_obj.doorset[1] == -2)
		                %temp_obj.direction[1] = vectorAdd(getWords(%temp_obj.direction[1], 0, 2), %temp_obj.position) SPC getwords(%temp_obj.direction[1], 3, 6);
			%temp_obj.isReversed = (%temp_obj.isInverted = getWord(%pl_string, 7));
			switch(getWord(%pl_string, 8)) {
				case 1:
					%temp_obj.teamOnly = "red";
				case 2:
					%temp_obj.teamOnly = "blue";
				case 3:
					%temp_obj.teamOnly = "green";
				case 4:
					%temp_obj.teamOnly = "yellow";
				case 5:
					%temp_obj.teamOnly = "mod";
				case 6:
					%temp_obj.teamOnly = "admin";
				case 7:
					%temp_obj.teamOnly = "super";
				default:
					%temp_obj.teamOnly = "none";
			}
			%temp_obj.port = getWord(%pl_string, 9);
			%cloak = getWord(%pl_string, 10);
			if(%cloak > 1) {
				%cloak -= 2;
				%isFaded = 1;
			}
			%temp_obj.setCloaked(%cloak);
			if(getWord(%pl_string, 11) !$= "0") {
				%temp_obj_name = getWords(%pl_string, 11, getWordCount(%pl_string));
				%temp_obj.setShapeName(getSubStr(%temp_obj_name,0,strlen(%temp_obj_name)-1));
	                }
		case 5:		//Brick
			//"owner skinname cloak port shapename"
	                if(%client)
		                %owner = getrawip(%client);
			else if(getWord(%pl_string, 0) $= "0")
				%owner = "local";
			else
				%owner = getWord(%pl_string, 0);
			%temp_obj = new StaticShape(%pl_name){
				position = %pl_position;
				rotation = %pl_rotation;
				scale = %pl_scale;
				dataBlock = %pl_datablock;
				owner = %owner;
			};
			if(getWord(%pl_string, 1) !$= "0" && getsubstr(getWord(%pl_string, 1),0,5) !$= "ghost")
				%temp_obj.setSkinName(getWord(%pl_string, 1));
			%temp_obj.setCloaked(getWord(%pl_string, 2));
			%temp_obj.port = getWord(%pl_string, 3);
      			        if(getWord(%pl_string, 4) !$= "0") {
	                    %temp_obj.isfaded = 1;
	                    %isfaded = 1;
              			}
			if(getWord(%pl_string, 5) !$= "0")
				%temp_obj.setShapeName(getWords(%pl_string, 5, getWordCount(%pl_string)));
              			getbrickowner(%owner,2);
			%temp_obj.delay = %delay;
			%temp_obj.var = %var;
			if(%temp_obj.delay !$= "")
				loaddecal(%temp_obj,getWord(%pl_string, 1),%temp_obj.delay,%temp_obj.var);
		case 6:		//AI Player
			//"owner skinname cloak port shapename"
	                if(%client)
		                %owner = getrawip(%client);
			else if(getWord(%pl_string, 0) $= "0")
				%owner = "local";
			else
				%owner = getWord(%pl_string, 0);
			%temp_obj = new AIPlayer(%pl_name) {
				position = %pl_position;
				rotation = %pl_rotation;
				scale = %pl_scale;
				dataBlock = %pl_datablock;
				owner = %owner;
		        	aiPlayer = true;
			};
			if(getWord(%pl_string, 1) !$= "0" && getsubstr(getWord(%pl_string, 1),0,5) !$= "ghost")
				%temp_obj.setSkinName(getWord(%pl_string, 1));
			%cloak = getWord(%pl_string, 2);
			if(%cloak > 1) {
				%cloak -= 2;
				%isFaded = 1;
			}
			%temp_obj.setCloaked(%cloak);
			%temp_obj.port = getWord(%pl_string, 3);
               		%p = %temp_obj;
       		        %p.clearAim();
			%p.headCode=getWord(%pl_string, 4);
			%p.headCodeColor=addTaggedString(getWord(%pl_string, 5));
			%p.visorCode=getWord(%pl_string, 6);
			%p.visorCodeColor=addTaggedString(getWord(%pl_string, 7));
			%p.backCode=getWord(%pl_string, 8);
			%p.backCodeColor=addTaggedString(getWord(%pl_string, 9));
			%p.leftHandCode=getWord(%pl_string, 10);
			%p.leftHandCodeColor=addTaggedString(getWord(%pl_string, 11));
			%p.chestdecalcode=addTaggedString(getWord(%pl_string, 12));
			%p.facedecalcode=addTaggedString(getWord(%pl_string, 13));
			%p.sitting=getWord(%pl_string, 14);
			%p.mountImage(%p.headCode, $headSlot, 1, %p.headCodeColor);
			%p.mountImage(%p.visorCode, $visorSlot, 1, %p.visorCodeColor);
			%p.mountImage(%p.backCode, $backSlot, 1, %p.backCodeColor);
			%p.mountImage(%p.leftHandCode, $leftHandSlot, 1, %p.leftHandCodeColor);
			%p.mountImage(chestShowImage, $chestSlot, 1, %p.chestdecalcode);
			%p.mountImage(faceplateShowImage, $faceSlot, 1, %p.facedecalcode);

			if(%relentless) {
				$gdelbots = 0;
				$zombieclient.name = "zombie";
				%p.image = %image;
				%p.vclient = %vclient;
				%p.mountImage(%p.image,0);
				zombiebot_getinittarget(%p);
				%p.setMoveSpeed(0.5 + (0.5 * getRandom(1)));
				zombiebot_prefs(%p);
				zombiebottick(%p);
			}
			if(!%relentless) {
				%p.stopmove();
				%p.clearaim();
			}
	                if(getWord(%pl_string, 15) !$= "0") {
				%temp_obj_name = getWords(%pl_string, 15, getWordCount(%pl_string));
				%temp_obj.setShapeName(getSubStr(%temp_obj_name,0,strlen(%temp_obj_name)-1));
		        }
			if(%p.sitting == 1)
				%p.schedule(1000,playthread,0,fall);
       	                %p.schedule(1000,settransform,%p.getposition()@" "@rotconv(%pl_rotsav));
		case 7:		//2.3+ Switch
			//"owner skinname cloak port shapename"
			//   0     1      2       3      4        5       6    7           8         9     10       11          12      13        14      9+6x   10+6x     11+6x    12+6x  13+6x 14+6x  15+6n to getWordCount
			//rotate skin origSkin revSkin owner cloak2Fade port numActions activators repeat reverse isReversed inverse isInverted teamOnly (type door¤set dire¤ction del¤ay ti¤mes sleep) n¤a¤m¤e
			%temp_obj = new Item() {
				static = true;
				position = %pl_position;
				rotation = %pl_rotation;
				scale = %pl_scale;
				dataBlock = %pl_datablock;
				rotate = getWord(%pl_string, 0);
			};
			if(getWord(%pl_string, 1) !$= "0" && getsubstr(getWord(%pl_string, 1),0,5) !$= "ghost")
				%temp_obj.setSkinName(getWord(%pl_string, 1));
			%temp_obj.origSkin = (getWord(%pl_string, 2) $= "0" ? "green" : getWord(%pl_string, 2));
			%temp_obj.revSkin = (getWord(%pl_string, 3) $= "0" ? "red" : getWord(%pl_string, 3));
			%temp_obj.owner = (getWord(%pl_string, 4) $= "0" ? "" : getWord(%pl_string, 4));
			%cloak = getWord(%pl_string, 5);
			if(%cloak > 1) {
				%cloak -= 2;
				%isFaded = 1;
			}
			%temp_obj.setCloaked(%cloak);
			%temp_obj.port = getWord(%pl_string, 6);
			%temp_obj.numActions = getWord(%pl_string, 7);
			%temp_obj.use = (getWord(%pl_string, 8) > 1);
			%temp_obj.bump = (mCeil(getWord(%pl_string, 8) / 2) != mFloor(getWord(%pl_string, 8) / 2));
			%temp_obj.repeat     = !!getWord(%pl_string, 9);
			%temp_obj.reverse    = !!getWord(%pl_string, 10);
			%temp_obj.isReversed = !!getWord(%pl_string, 11);
			%temp_obj.inverse    = !!getWord(%pl_string, 12);
			%temp_obj.isInverted = !!getWord(%pl_string, 13);
			%temp_obj.teamOnly = (getWord(%pl_string, 14) $= "0" ? "none" : getWord(%pl_string, 14));
			%temp_obj.currAction = %obj.isReversed ? %obj.numActions : 1;
			for(%i = 1; %i <= %temp_obj.numActions || %i == 1; %i++) {
				%temp_obj.type[%i]      =  (getWord(%pl_string, 9 + (6 * %i)) $= "" ? "null_loaderror" : getWord(%pl_string, 9 + (6 * %i)));
				%temp_obj.doorset[%i]   =  (getWord(%pl_string, 10 + (6 * %i)) $= "" ? "0" : strReplace(getWord(%pl_string, 10 + (6 * %i)), "¤", " "));
				%temp_obj.direction[%i] =  (getWord(%pl_string, 11 + (6 * %i)) $= "" ? "0" : strReplace(getWord(%pl_string, 11 + (6 * %i)), "¤", " "));
				%temp_obj.delay[%i]     =  (getWord(%pl_string, 12 + (6 * %i)) $= "" ? "0" : strReplace(getWord(%pl_string, 12 + (6 * %i)), "¤", " "));
				%temp_obj.times[%i]     =  (getWord(%pl_string, 13 + (6 * %i)) $= "" ? "0" : strReplace(getWord(%pl_string, 13 + (6 * %i)), "¤", " "));
				%temp_obj.sleep[%i]     = !!getWord(%pl_string, 14 + (6 * %i));
				if(%temp_obj.type[%i] $= "teleport")
			                %temp_obj.direction[%i] = vectorAdd(getWords(%temp_obj.direction[%i], 0, 2), %temp_obj.position) SPC getwords(%temp_obj.direction[%i], 3, 6);
				if(%temp_obj.type[%i] $= "pivot")
			                %temp_obj.direction[%i] = setWord(vectorAdd(getWords(%temp_obj.direction[%i], 0, 1), getWords(%temp_obj.position, 0, 1)), 2, getword(%temp_obj.direction[%i], 2));
			}
	                if(getWord(%pl_string, 9 + (6 * %i)) !$= "0") //It will be one larger after the for loop
				%temp_obj.setShapeName(strReplace(getWords(%pl_string, 9 + (6 * %i), getWordCount(%pl_string)), "¤", " "));
	} //End of switch$ statement
}
%temp_obj.rotsav = %pl_rotsav;
if(isObject(%temp_obj)) {
	if (%turn !$= "")
		%temp_obj.settransform(%temp_obj.position@" "@rotconv(%temp_obj.rotsav));
	if (%fade == 1)
		%temp_obj.startfade(500,0,false);
	if (%isfaded == 1)
		%temp_obj.startfade(1000,500,true);
	if(isObject(MissionCleanup))
		MissionCleanup.add(%temp_obj);
	if(isObject(%client.iGob))
        	%client.iGob.brick[%client.iGob.Total++] = %temp_obj;
}
return %temp_obj; //Why wasn't this here before now?
}

//This is a support function written to evaluate two sets of nubmers and
//solve them. It is used to determine the relative location for a tele's spawn point
function vectoreval(%var1,%var2,%math) {
%wordnum = 0;
for(%num=getword(%var1,%wordnum);%num !$= "";%num=getword(%var1,%wordnum++))
%var1array[%wordnum] = %num;
%wordnum2 = 0;
for(%num=getword(%var2,%wordnum2);%num !$= "";%num=getword(%var2,%wordnum2++))
%var2array[%wordnum2] = %num;
switch$(%math) {
case "-":
for(%i=0;%i<%wordnum;%i++)
%word[%i] = (%var1array[%i] - %var2array[%i])*1;
case "/":
for(%i=0;%i<%wordnum;%i++)
%word[%i] = (%var1array[%i] / %var2array[%i])*1;
case "*":
for(%i=0;%i<%wordnum;%i++)
%word[%i] = (%var1array[%i] * %var2array[%i])*1;
default:
for(%i=0;%i<%wordnum;%i++)
%word[%i] = (%var1array[%i] + %var2array[%i])*1;
}
for(%i=0;%i<%wordnum;%i++) {
if(%finalequation $= "")
%finalequation = %word[%i];
else
%finalequation = %finalequation SPC %word[%i];
}
return %finalequation;
}