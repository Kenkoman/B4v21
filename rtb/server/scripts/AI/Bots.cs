function ServerCmdBotAim(%client, %obj)
{
	if (%client.isTempAdmin || %client.isAdmin || %client.isSuperAdmin)
	{
%count = %client.bcn;
for (%i = 0; %i <= %count; %i++)
{
%bot = %client.Bots[%i];
if (isObject(%bot))
{
%bot.setAimObject(%obj);
}
}
	}
}

function ServerCmdBotFol(%client, %obj)
{
	if (%client.isTempAdmin || %client.isAdmin || %client.isSuperAdmin)
	{
%count = %client.bcn;
for (%i = 0; %i <= %count; %i++)
{
%bot = %client.Bots[%i];
echo(%bot);
if (isObject(%bot))
{
ffly(%obj, %bot);
}
}
	}
}

function ServerCmdCopyPer(%client)
{
	if (%client.isTempAdmin || %client.isAdmin || %client.isSuperAdmin)
	{
if (!%client.camsp)
{
return;
}
%rightIM = %client.player.getMountedImage($rightHandSlot);
%rightIMC = %rightIM.getSkinName();
$BotCount++;   
$Bot[$BotCount] = new AIPlayer() {
      dataBlock = LightMaleHumanArmor;
      aiPlayer = true;
   };
   %client.bcn++;
%client.Bots[%client.bcn] = $Bot[$BotCount];
   MissionCleanup.add($Bot[$BotCount]);
%pos = %client.player.getTransform();
   // Player setup
   $Bot[$BotCount].setMoveSpeed(1);
   $Bot[$BotCount].setTransform(%pos);
   $Bot[$BotCount].setEnergyLevel(60);
   $Bot[$BotCount].setShapeName(%name);
   $Bot[$BotCount].TargetObj = 0;
   $Bot[$BotCount].Type = %type;
   $Bot[$BotCount].BotOwner = %client;
   $Bot[$BotCount].setSkinName(%client.player.getSkinName());

   $Bot[$BotCount].OrignialRotation = $Bot[$BotCount].GetAimLocation();

			$Bot[$BotCount].mountImage(%client.headCode, $headSlot, 1, %client.headCodeColor);
			$Bot[$BotCount].mountImage(%client.visorCode, $visorSlot, 1, %client.visorCodeColor);
			$Bot[$BotCount].mountImage(%client.backCode, $backSlot, 1, %client.backCodeColor);
			$Bot[$BotCount].mountImage(%client.leftHandCode, $leftHandSlot, 1, %client.leftHandCodeColor);
			$Bot[$BotCount].mountImage(%client.chestCode , $decalslot, 1, %client.chestdecalcode);
			$Bot[$BotCount].mountImage(%client.faceCode , $faceslot, 1, %client.faceprintcode);
			$Bot[$BotCount].mountImage(%rightIM, $rightHandSlot, 1, %rightIMC);
			$Bot[$BotCount].setTransform(%client.player.getTransform());
			$Bot[$BotCount].setShapeName(%client.player.getShapeName());
	%client.curSBot = $Bot[$BotCount];
	ServerCmdUpdateClientBots(%client);
	}
}

function ServerCmdArmyWalk(%client)
{
	if (%client.isTempAdmin || %client.isAdmin || %client.isSuperAdmin)
	{
%player = %client.player;
%pTrans = %player.getTransform();
%px = getWord(%pTrans, 0);
%py = getWord(%pTrans, 1);
%pz = getWord(%pTrans, 2);
ServerCmdCopyPer(%client);
%bot1 = %client.Bots[%client.bcn];
%bot1.setMoveDestination(%bot1.getTransform());
ServerCmdCopyPer(%client);
%bot2 = %client.Bots[%client.bcn];
%bot2.setTransform(%px + 2 SPC %py SPC %pz);
%bot2.setMoveDestination(%bot2.getTransform());
ServerCmdCopyPer(%client);
%bot3 = %client.Bots[%client.bcn];
%bot3.setTransform(%px SPC %py + 2 SPC %pz);
%bot3.setMoveDestination(%bot3.getTransform());
ServerCmdCopyPer(%client);
%bot4 = %client.Bots[%client.bcn];
%bot4.setTransform(%px + 2 SPC %py + 2 SPC %pz);
%bot4.setMoveDestination(%bot4.getTransform());
//%bot1.setScale("0.2 0.2 0.2");
//%bot2.setScale("0.2 0.2 0.2");
//%bot3.setScale("0.2 0.2 0.2");
//%bot4.setScale("0.2 0.2 0.2");
BotMarch(%bot1, %bot2, %bot3, %bot4);
	}
}


function BotMarch(%bot1, %bot2, %bot3, %bot4)
{
%b1T = %bot1.getTransform();
%b2T = %bot2.getTransform();
%b3T = %bot3.getTransform();
%b4T = %bot4.getTransform();
%b1X = RTX(%b1T);
%b2X = RTX(%b2T);
%b3X = RTX(%b3T);
%b4X = RTX(%b4T);
%b1Y = RTY(%b1T);
%b2Y = RTY(%b2T);
%b3Y = RTY(%b3T);
%b4Y = RTY(%b4T);
%b1Z = RTZ(%b1T);
%b2Z = RTZ(%b2T);
%b3Z = RTZ(%b3T);
%b4Z = RTZ(%b4T);
if (!%bot1.MBMN)
{
%bot1.MBMN = 1;
}
%MBMN = %bot1.MBMN;
	if (%MBMN == 1)
	{
	%bot1.MBMN = 2;
	%bot1.setMoveDestination(%b1X + 2 SPC %b1Y SPC %b1Z);
	%bot2.setMoveDestination(%b2X + 2 SPC %b2Y SPC %b2Z);
	%bot3.setMoveDestination(%b3X + 2 SPC %b3Y SPC %b3Z);
	%bot4.setMoveDestination(%b4X + 2 SPC %b4Y SPC %b4Z);
	}
	if (%MBMN == 2)
	{
	%bot1.MBMN = 3;
	%bot1.setMoveDestination(%b1X - 2 SPC %b1Y SPC %b1Z);
	%bot2.setMoveDestination(%b2X - 2 SPC %b2Y SPC %b2Z);
	%bot3.setMoveDestination(%b3X - 2 SPC %b3Y SPC %b3Z);
	%bot4.setMoveDestination(%b4X - 2 SPC %b4Y SPC %b4Z);
	}
	if (%MBMN == 3)
	{
	%bot1.MBMN = 4;
	%bot1.setMoveDestination(%b1X SPC %b1Y + 2 SPC %b1Z);
	%bot2.setMoveDestination(%b2X SPC %b2Y + 2 SPC %b2Z);
	%bot3.setMoveDestination(%b3X SPC %b3Y + 2 SPC %b3Z);
	%bot4.setMoveDestination(%b4X SPC %b4Y + 2 SPC %b4Z);
	}
	if (%MBMN == 4)
	{
	%bot1.MBMN = 5;
	%bot1.setMoveDestination(%b1X SPC %b1Y - 2 SPC %b1Z);
	%bot2.setMoveDestination(%b2X SPC %b2Y - 2 SPC %b2Z);
	%bot3.setMoveDestination(%b3X SPC %b3Y - 2 SPC %b3Z);
	%bot4.setMoveDestination(%b4X SPC %b4Y - 2 SPC %b4Z);
	}
	if (%MBMN == 5)
	{
	%bot1.MBMN = 6;
	%bot1.setMoveDestination(%b1X - 2 SPC %b1Y SPC %b1Z);
	%bot2.setMoveDestination(%b2X - 2 SPC %b2Y SPC %b2Z);
	%bot3.setMoveDestination(%b3X - 2 SPC %b3Y SPC %b3Z);
	%bot4.setMoveDestination(%b4X - 2 SPC %b4Y SPC %b4Z);
	}
	if (%MBMN == 6)
	{
	%bot1.MBMN = 7;
	%bot1.setMoveDestination(%b1X + 2 SPC %b1Y SPC %b1Z);
	%bot2.setMoveDestination(%b2X + 2 SPC %b2Y SPC %b2Z);
	%bot3.setMoveDestination(%b3X + 2 SPC %b3Y SPC %b3Z);
	%bot4.setMoveDestination(%b4X + 2 SPC %b4Y SPC %b4Z);
	}
	if (%MBMN == 7)
	{
	%bot1.MBMN = 8;
	%bot1.setMoveDestination(%b1X SPC %b1Y - 2 SPC %b1Z);
	%bot2.setMoveDestination(%b2X SPC %b2Y - 2 SPC %b2Z);
	%bot3.setMoveDestination(%b3X SPC %b3Y - 2 SPC %b3Z);
	%bot4.setMoveDestination(%b4X SPC %b4Y - 2 SPC %b4Z);
	}
	if (%MBMN == 8)
	{
	%bot1.MBMN = 1;
	%bot1.setMoveDestination(%b1X SPC %b1Y + 2 SPC %b1Z);
	%bot2.setMoveDestination(%b2X SPC %b2Y + 2 SPC %b2Z);
	%bot3.setMoveDestination(%b3X SPC %b3Y + 2 SPC %b3Z);
	%bot4.setMoveDestination(%b4X SPC %b4Y + 2 SPC %b4Z);
	}
if(isObject(%bot1))
{
schedule(700, 0, BotMarch, %bot1, %bot2, %bot3, %bot4);
}	
}
function ServerCmdBotDel(%client)
{
	if (%client.isTempAdmin || %client.isAdmin || %client.isSuperAdmin)
	{
%count = $BotCount;
echo("calling it2");
for (%i = 0; %i <= %count; %i++)
{
%bot = $Bot[%i];
//echo(%bot);
if (isObject(%bot))
{
echo("deleting");
%bot.delete();
}
}
	}
}

function ServerCmdAddCheck(%client)
{
	if (%client.isTempAdmin || %client.isAdmin || %client.isSuperAdmin)
	{
%player = %client.player;
%Ppos = %player.getPosition();
	if (isObject(%client.curSBot))
	{
	%bot.isCMBot = 1;
	%bot.client = %client;
	%bot = %client.curSBot;
		if (!%bot.CMBN)
		{
		%bot.CMBN = 0;
		}
	%bot.CMBN++;
	%bot.CMs[%bot.CMBN] = %Ppos;
		if (!%bot.isCMBot)
		{
		%bot.isCMBot = 1;
		%bot.CMs[0] = %bot.getPosition();
		%bot.curMark = 0;
		NextBMark(%bot);
		}
	}
	}
}

function NextBMark(%bot)
{
%count = %bot.CMBN;
	for (%i = 0; %i <= %count; %i++)
	{
		if (%bot.curMark == %bot.CMBN)
		{
		echo("going back");
		%bot.curMark = -1;
		}
		if (%i > %bot.curMark)
		{
		%bot.curMark = %i;
		%bot.clearAim();
		%bot.setAimLocation(%bot.CMs[%i]);
		%bot.setMoveDestination(%bot.CMs[%i]);
		return;
		}
	}
}

function ServerCmdCEditC(%client)
{
return;
	if (%client.isTempAdmin || %client.isAdmin || %client.isSuperAdmin)
	{
%bot = %client.curSBot;
%count = %bot.CMBN;
%bot.Tnum = 0;
if (%bot)
{
	for (%i = 0; %i <= %count; %i++)
	{
	%mark = %bot.Cms[%i];
		if (%mark != 0)
		{
			%markOb = new Item() {
				position = %mark;
      			rotation = "1 0 0 0";
      			scale = "1 1 1";
      			dataBlock = brick1x1t;
      			collideable = "0";
      			static = "1";
      			rotate = "0";
      		};
		%bot.CMOs[%i] = %markOb;
		%bot.CMOs[%i].setShapeName(%i);
		%bot.CMOs[%i].setSkinName('bluedark');
		%bot.CMOs[%i].doNothing = 1;
		}
	%bot.Tnum = %i;
	}
}
	}
}

function ServerCmdCERU(%client)
{
%bot = %client.curSBot;
	if (%bot)
	{
	%count = %bot.TNum;
		for (%i = 0; %i <= %count; %i++)
		{
		%mark = %bot.CMOs[%i];
		%Mpos = getWords(%mark.getTransform(), 0, 2);
		%bot.CMs[%i] = %Mpos; 
		%bot.CMos[%i].delete();
		%bot.TNum = 0;
		}
	}
}

function ServerCmdGiveTempB(%client)
{
	%player = %client.player;
	%tempbrick = %player.tempbrick;
	%bot = %client.curSBot;
	if (isObject(%bot) && isObject(%tempbrick))
	{
	%player.tempBrick = 0;
	%bot.tempBrick = %tempbrick;
	}
}

function ServerCmdABCTB(%client, %BValue)
{
%bot = %client.curSBot;
	if (isObject(%bot))
	{
	%bot.NOBN++;
	%bot.BSQs[%bot.NOBN] = %BValue;
	%bot.isBuildB = 1;
	}
}

function ServerCmdAddCheck2(%client, %group)
{
	if (%client.isTempAdmin || %client.isAdmin || %client.isSuperAdmin)
	{
	if (!isObject($patrolGroups))
	{
	$PatrolGroups = new Item() {
      position = "9780 -500 -9000";
      			rotation = "1 0 0 0";
      			scale = "1 1 1";
      			dataBlock = brick1x1t;
      			collideable = "0";
      			static = "1";
      			rotate = "0";
      		};
$PatrolGroups.hide(0);
$PatrolGroups.NumOfGroups = 1;
echo("Patrol group object created");
}
// NOTE: Find a way to set up a system that has a way to name patrol groups with a name and be able to browse the groups and edit them: $PLGroups[num].name and .marks[num]
%player = %client.player;
%Ppos = %player.getPosition();
%groupNum = $PatrolGroups.NumOfGroups;
	for (%i = 1; %i < %groupNum; %i++)
	{
	echo(%i @ " is the number.");
	%groupName = $PatrolGroups.groups[%i].name;
	echo(%groupName @ " is the %groupname.");
	echo(%group @ " is the %group");
		if (%groupName $= %group)
		{
		%gotGroup = 1;
		%youKnow = %i;
		%UG = $PatrolGroups.groups[%youKnow];
		}
	}
	if (!%gotGroup)
	{
	echo("going to create another group. The NumOfGroups is " @ $PatrolGroups.groups[$PatrolGroups.NumOfGroups]);
$PatrolGroups.groups[$PatrolGroups.NumOfGroups] = new Item() {
      position = $PatrolGroups.getTransform();
      			rotation = "1 0 0 0";
      			scale = "1 1 1";
      			dataBlock = brick1x1t;
      			collideable = "0";
      			static = "1";
      			rotate = "0";
      		};
	$PatrolGroups.groups[%groupNum].name = %group;
	echo($PatrolGroups.groups[%groupNum].name @ " is the new group.");
	$PatrolGroups.NumOfGroups++;
	echo($PatrolGroups.NumOfGroups);
	%UG = $PatrolGroups.groups[%groupNum];
	%UG.CMBN = 1;
	}
%UG.CMs[%UG.CMBN] = %Ppos;
%UG.CMBN++;
ServerCmdUpdateBotPatrols(%client);
	}
}

function NextBMark2(%bot)
{
	if (%bot.BotType $= "Patrol")
	{
	%group = %bot.CMgroup;
	%count = %group.CMBN;
	//echo(%count @ " is the %count");
	//echo(%group @ " is the %group");
	//echo(%group.CMs[1] @ " is the %group[0]");
		for (%i = 1; %i < %count; %i++)
		{
		//echo(%i @ " is the %i");
		//echo(%group.CMs[%i] @ " is the %group.CMs[%i]");
			if (%bot.curMark >= %count - 1)
			{
			//echo("going back");
			%bot.curMark = 0;
			}
			if (%i > %bot.curMark)
			{
			%bot.curMark = %i;
			%bot.clearAim();
			%bot.setAimLocation(%bot.CMs[%i]);
				if (!%group.CMs[%i])
				{
				//echo(%i @ " is the %i error");
				//echo(%group @ " is the %group error");
				//echo(%group.CMs[%i] @ "  is the messed up one");
				return;
				}
			%bot.diduh = %group.CMs[%i];
			%bot.diduh = "yay";
			%bot.diduh = %group.CMs[%i];
			%bot.clearAim();
			%bot.setAimLocation(%group.CMs[%i]);
			%bot.setMoveDestination(%group.CMs[%i]);
			return;
			}
		}
	}
}

function ServerCmdBotStartPatrol(%client, %group)
{
	if (%client.isTempAdmin || %client.isAdmin || %client.isSuperAdmin)
	{
%bot = %client.curSBot;
	if (%bot.CMgroup != %group)
	{
	%bot.curMark = 0;
	}
%bot.CMgroup = 0;
%bot.setMoveDestination(%bot.getPosition);
%groupNum = $PatrolGroups.NumOfGroups;
	for (%i = 0; %i < %groupNum; %i++)
	{
	%groupName = $PatrolGroups.groups[%i].name;
	echo(%groupName @ " is the %groupname");
		if (%groupName $= %group)
		{
		%gotGroup = 1;
		%bot.CMgroup = $PatrolGroups.groups[%i];
		}
	}
	if (%gotGroup)
	{
	%bot.isPatBot = 1;
	NextBMark2(%bot);
	}
	}
}

function ServerCmdBotStopPatrol(%client)
{
	if (%client.isTempAdmin || %client.isAdmin || %client.isSuperAdmin)
	{
%bot = %client.curSBot;
%bot.setMoveDestination(%bot.getPosition());
%bot.clearAim();
	}
}

function ServerCmdSBBing(%client)
{
	if (%client.isTempAdmin || %client.isAdmin || %client.isSuperAdmin)
	{
%bot = %client.curSBot;
%bot.CurSAT = 0;
DoBotBing(%bot);
	}
}

function DoBotBing(%bot)
{
	if (%bot.isUsingPath)
	{
	%bot = %bot.PathUsing;
	}
%bot.CurSAT++;
%BA = %bot.BSQs[%bot.CurSAT];
%bot.setAimObject(%bot.tempBrick);
	if (%bot.CurSAT <= %bot.NOBN)
	{
		if (%BA $= "up1")
		{
		BotShiftBrick(%bot, 0, 0, 1);
		}
		if (%BA $= "up3")
		{
		BotShiftBrick(%bot, 0, 0, 3);
		echo("up3");
		}
		if (%BA $= "down1")
		{
		BotShiftBrick(%bot, 0, 0, 1);
		}
		if (%BA $= "left1")
		{
		BotShiftBrick(%bot, 0, 1, 0);
		}
		if (%BA $= "right1")
		{
		BotShiftBrick(%bot, 0, -1, 0);
		echo("right1");
		}
		if (%BA $= "away1")
		{
		BotShiftBrick(%bot, 1, 0, 0);
		}
		if (%BA $= "toward1")
		{
		BotShiftBrick(%bot, -1, 0, 0);
		}
		if (%BA $= "plant")
		{
		BotPlantBrick(%bot);
		echo("plant");
		}
		%randNum = getRandom(6, 2);
		%numUse = %randNum @ "00";
		schedule(%NumUse, 0, "DoBotBing", %bot);
	}
	else
	{
	%bot.clearAim();
	%bot.curSAT = 0;
	}
}



function BotShiftBrick(%bot, %x, %y, %z)
{
	//z dimension is sent in PLATE HEIGHTS, not brick heights

	%player = %bot;
	%tempBrick = %player.tempBrick;
	%tempbrick.setSkinName('red');
	%carmounts = %player.carmounts;
	
	if(%tempBrick)
	{
	
        if(%tempBrick.ismounted())
	{   
            %player.carmounts++;
            %player.brickcar.mountobject(%tempbrick,%player.carmounts);
            if(%player.carmounts == 9)
            {
            %player.carmounts = "0";
            }
            if(%player.carmounts == -1)
            {
            %player.carmounts = "9";
            }
            if(%player.carmounts == 0)
            {
            %player.carmounts = "1";
            }
            return;
        }

		//%brickTrans = %tempBrick.getTransform();
		//%brickX = getWord(%brickTrans, 0);
		//%brickY = getWord(%brickTrans, 1);
		//%brickZ = getWord(%brickTrans, 2);
		//%brickRot = getWords(%brickTrans, 3, 6);

		%forwardVec = %player.getForwardVector();
		%forwardX = getWord(%forwardVec, 0);
		%forwardY = getWord(%forwardVec, 1);
		//echo("forwardvec = ", %forwardVec);

		if(%forwardX > 0){
			if(%forwardX > mAbs(%forwardY)){
				//we are facing basically x+
				//this is the default, dont do anything
			}
			else{
				//we are closer to the y axis, but which direction?
				if(%forwardY > 0){
					//we are facing y +
					%newY = %x;
					%newX = -1 * %y; 
					%x = %newX;
					%y = %newY;
				}
				else{
					//we are facing y -
					%newY = -1 * %x;
					%newX = 1 * %y; 
					%x = %newX;
					%y = %newY;
				}
			}
		}
		else
		{
			if(mAbs(%forwardX) > mAbs(%forwardY)){
				//we are facing basically x-
				//this backwards from default, reverse everything
				%x *= -1;
				%y *= -1;
			}
			else{
				//we are closer to the y axis, but which direction?
				if(%forwardY > 0){
					//we are facing y +
					%newY = %x;
					%newX = -1 * %y; 
					%x = %newX;
					%y = %newY;
				}
				else{
					//we are facing y -
					%newY = -1 * %x;
					%newX = 1 * %y; 
					%x = %newX;
					%y = %newY;
				}
			}
		}

		//Convert lego units to world units'
		%x *= 0.5;
		%y *= 0.5;
		%z *= 0.2;
		//if (%z)
		//{
			//%tempBrick.playthread(0, root);
			
			//%tempBrick.playthread(0, shiftUp);
			//%tempBrick.schedule(100, playthread, 0, root);
		//}
		//%tempBrick.setTransform(%brickX + %x @ " " @ %brickY + %y @ " " @ %brickZ + %z @ " " @ %brickRot);
		shift(%tempbrick, %x, %y, %z);

		ServerPlay3D(brickMoveSound, %tempBrick.getTransform());
	}




//--
//--
//--
}
function BotPlantBrick(%bot)
{


		//echo(%client, " is planting brick");
	%player = %bot;
	%tempBrick = %player.tempBrick;
	
	if(%tempBrick)
	{
		if(%tempBrick.ismounted())
		{
			return;
		}
		%solid = %tempBrick.getDataBlock();

		//create the new brick//
		%newBrick = new StaticShape()
		{
			datablock = %solid;
		};
		MissionCleanup.add(%newBrick);

		//intialize upper and lower attachement lists
		%newBrick.upSize = 0;
		%newBrick.downSize = 0;
		%newBrick.up[0] = -1;
		%newBrick.down[0] = -1;
		%eulerRot = %tempBrick.EulerRot;
		


		//check for stuff in the way
		%mask = $TypeMasks.StaticShapeObjectType;

		%tempBrickTrans = %tempBrick.getTransform();

		%tempBrickisRotated = %tempBrick.isRotated;
		//messageAll("",'%1',%tempBrickisRotated);
		//these will be where we start ray casting
		//the starting and ending positions are adjusted to match the rotation
		%tempBrickX = (getWord(%tempBrickTrans, 0));
		%tempBrickY = (getWord(%tempBrickTrans, 1));
		%tempBrickZ = (getWord(%tempBrickTrans, 2));

		%startX = %tempBrickX;
		%startY = %tempBrickY;
		%startZ = %tempBrickZ;

		%startZ += (%solid.z * 0.2) ;			//start at top of brick		

		%loopEndX = 1;
		%loopEndY = 1;
		%xStep = 1;
		%yStep = 1;


		%quatZ = getword(%tempBrickTrans, 5);
		%quatAng = getword(%tempBrickTrans, 6);

		//asumes bricks are never rotated off the verticle axis
		if(%quatZ == -1){
			%quatAng += $pi;
		}

		%angleTest = mFloor(%quatAng / $piOver2);


		if(%angleTest == 0){
			%startX += 0.25;
			%startY += 0.25;

			%loopEndX = %solid.x;
			%loopEndY = %solid.y;
			%xStep = 1;
			%yStep = 1;
		}
		else if(%angleTest == 1){
			%startX += 0.25;
			%startY -= 0.25;

			%loopEndX = %solid.y;
			%loopEndY = %solid.x * -1;
			%xStep = 1;
			%yStep = -1;


		}
		else if(%angleTest == 2){
			%startX -= 0.25;
			%startY -= 0.25;

			%loopEndX = %solid.x * -1;
			%loopEndY = %solid.y * -1;
			%xStep = -1;
			%yStep = -1;
		}
		else if(%angleTest == 3){
			%startX -= 0.25;
			%startY += 0.25;

			%loopEndX = %solid.y * -1;
			%loopEndY = %solid.x;
			%xStep = -1;
			%yStep = 1;
		}
		else{
			error("Error in ServerCmdPlantBrick(): %angleTest > 3 or < 0");
			return;
		}
		

		//===Find Tempbrick start and end box points===//
		/////////////////////////////////////////////////
		%tempBrickTrans = %tempBrick.getTransform();
			
		%tempBrickXpos = getWord(%tempBrickTrans, 0);
		%tempBrickYpos = getWord(%tempBrickTrans, 1);
		%tempBrickZpos = getWord(%tempBrickTrans, 2);

		//determine which way the check brick is facing

		%tempBrickAngle = getWord(%tempBrickTrans, 6);
		%vectorDir = getWord(%tempBrickTrans, 5);

		if(%vectorDir == -1)
			%tempBrickAngle += $pi;

		%tempBrickAngle /= $piOver2;
		%tempBrickAngle = mFloor(%tempBrickAngle + 0.1);

		if (%tempBrickAngle > 4)
			%tempBrickAngle -= 4;
		if (%tempBrickAngle <= 0)
			%tempBrickAngle += 4;
		
		//error("tempbrick angle = ", %tempbrickangle);
		
		//StartX/y is low, endX/Y is high
		%tempBrickStartX = %tempBrickXpos;
		%tempBrickStartY = %tempBrickYpos;
		%tempBrickEndX = %tempBrickXpos;
		%tempBrickEndY = %tempBrickYpos;

		%tempBrickData = %tempBrick.getDataBlock();

		if(%tempBrickAngle == 1)
		{
			%tempBrickEndX += %tempBrickData.y * 0.5;
			%tempBrickStartY -= %tempBrickData.x * 0.5;
		}
		else if(%tempBrickAngle == 2)
		{
			%tempBrickStartX -= %tempBrickData.x * 0.5;
			%tempBrickStartY -= %tempBrickData.y * 0.5;
		}
		else if(%tempBrickAngle == 3)
		{
			%tempBrickStartX -= %tempBrickData.y * 0.5;
			%tempBrickEndY += %tempBrickData.x * 0.5;
		}
		else if(%tempBrickAngle == 4)
		{
			%tempBrickEndX += %tempBrickData.x * 0.5;
			%tempBrickEndY += %tempBrickData.y * 0.5;
		}
		///////////////////////////////////////////////
		//===Done finding temp brick start and end===//

			
		//echo("projectile start ", %startX @ " " @ %startY @ " " @ %startZ);

		//find the middle 
		%xOffset = (%loopEndX * 0.5) / 2;		//convert from lego units, then divide by 2 to get the center
		%yOffset = (%loopEndY * 0.5) / 2;
		%zoffSet = (%solid.z * 0.2) / 2;	

		//echo("offset is ", %xoffset, " ", %yoffset, " ", %zoffset);

		//find the biggest dimension
		%radius = %solid.x * 0.5;

		if( (%solid.y * 0.5) > %radius)
		{
			//echo("y is bigger");
			%radius = %solid.y * 0.5;
		}
		if( (%solid.z * 0.2) > %radius)
		{
			//echo("z is bigger");
			%radius = %solid.z * 0.2;
		}
		//its a radius, so divide by 2;
		%radius /= 2;
		%radius += 0.5;
		
		//echo("Radius = ", %radius);

		%brickCenter = %tempBrickX + %xOffset @ " " @ %tempBrickY + %yOffset @ " " @ %tempBrickZ + %zOffset ;
		//echo("brick center is ", %brickCenter);
		%mask = $TypeMasks::StaticShapeObjectType;
		
		%attachment = 0;
		%hanging = 1;

		InitContainerRadiusSearch(%brickCenter, %radius, %mask);

		while ((%checkObj = containerSearchNext()) != 0){
			%xOverLap = 0;	
			%yOverLap = 0;
			%zOverLap = 0;	
			%zTouch = 0;	//is the brick is right up against another brick vertically? 
			
			%checkData = %checkObj.getDataBlock();

			if(%checkData.className $= "Brick" || %checkData.className $= "Baseplate"){

				%isTrusted = 0;

				for(%trusted = 0; %trusted < clientGroup.GetCount(); %trusted++)
				{
					%cl = clientGroup.getObject(%trusted);
					for(%safe = 0; %safe < %checkObj.Owner.FriendListNum + 1; %safe++)
					{
					}

				}



				if(%checkObj.isBrickGhost != 1)
				{

				//%checkObj.setSkinName('Blue');
				%checkTrans = %checkObj.getTransform();

				%checkXpos = getWord(%checkTrans, 0);
				%checkYpos = getWord(%checkTrans, 1);
				%checkZpos = getWord(%checkTrans, 2);

				//---check for z over touching---
				%comp1 = round(%tempBrickZ + (%solid.z * 0.2));
				%comp2 = round(%checkZpos);
				if(%comp1 == %comp2){
					//placing under checkObj
					%attachedOnTop = true;
					%zTouch = 1;
				}
					
				%comp1 = round(%tempBrickZ);
				%comp2 = round(%checkZpos + (%checkData.Z * 0.2));
				if(%comp1 == %comp2){
					//placing ontop of checkObj
					%attachedOnTop = false;
					%zTouch = 1;
				}
				//---end ztouching check---

				%tempBottom = round(%tempBrickZ);
				%tempTop = round(%tempBrickZ + (%solid.z * 0.2));
				%checkBottom = round(%checkZpos);
				%checkTop = round(%checkZpos + (%checkData.z * 0.2));

				//---test for z overlaps---
				//echo("checktop = ", %checkTop);
				//echo("checkbottom = ", %checkbottom);
				//echo("temptop = ", %tempTop);
				//echo("tempbottom = ", %tempbottom);
				
				if(%tempBottom >= %checkBottom) {
					if(%checkTop > %tempBottom) {
						%zOverlap = 1;
						//%checkObj.setSkinName('red');
					}
				}
				if(%checkBottom >= %tempBottom){
					if(%tempTop > %checkBottom) {
						%zOverlap = 1;
						//%checkObj.setSkinName('red');
					}
				}
				//---end z overlap test---

				//determine which way the check brick is facing

				%checkAngle = getWord(%checkTrans, 6);
				%vectorDir = getWord(%checkTrans, 5);

				if(%vectorDir == -1)
					%checkAngle += $pi;

				%checkAngle /= $piOver2;
				%checkAngle = mFloor(%checkAngle + 0.1);

				if (%checkAngle > 4)
					%checkAngle -= 4;
				if (%checkAngle <= 0)
					%checkAngle += 4;


				//echo("%checkAngle == ", %checkangle);
				//echo("check x dim = ", %checkData.x);
				//echo("check y dim = ", %checkData.y);


				//StartX/y is low, endX/Y is high
				%checkStartX = %checkXpos;
				%checkStartY = %checkYpos;
				%checkEndX = %checkXpos;
				%checkEndY = %checkYpos;

				if(%checkAngle == 1)
				{
					%checkEndX += %checkData.y * 0.5;
					%checkStartY -= %checkData.x * 0.5;
				}
				else if(%checkAngle == 2)
				{
					%checkStartX -= %checkData.x * 0.5;
					%checkStartY -= %checkData.y * 0.5;
				}
				else if(%checkAngle == 3)
				{
					%checkStartX -= %checkData.y * 0.5;
					%checkEndY += %checkData.x * 0.5;
				}
				else if(%checkAngle == 4)
				{
					%checkEndX += %checkData.x * 0.5;
					%checkEndY += %checkData.y * 0.5;
				}
				
				
				//round everything
				%checkStartX = round(%checkStartX);
				%checkStartY = round(%checkStartY);
				%checkEndX = round(%checkEndX);
				%checkEndY = round(%checkEndY);
				%tempBrickStartX = round(%tempBrickStartX);
				%tempBrickStartY = round(%tempBrickStartY);
				%tempBrickEndX = round(%tempBrickEndX);
				%tempBrickEndY = round(%tempBrickEndY);
	
				//echo("check x ", %checkStartX, " ", %checkEndX);
				//echo("check y ", %checkStartY, " ", %checkEndY);
				//echo("tempBrick x ", %tempBrickStartX, " ", %tempBrickEndX);
				//echo("tempBrick y ", %tempBrickStartY, " ", %tempBrickEndY);
			

				//---test for x overlaps---
				if(%tempBrickStartX >= %checkStartX) {
					//echo(%checkEndX, " >= ", %tempBrickStartX, "? ", (%checkEndX >= %tempBrickStartX));
					if(%checkEndX > %tempBrickStartX) {
						%xOverlap = 1;
					}
				}
				if(%checkStartX >= %tempBrickStartX){
					if(%tempBrickEndX > %checkStartX) {
						%xOverlap = 1;
					}
				}
				//---end x overlap test---

				//---test for Y overlaps---
				if(%tempBrickStartY >= %checkStartY) {
					if(%checkEndY > %tempBrickStartY) {
						%yOverlap = 1;
					}
				}
				if(%checkStartY >= %tempBrickStartY){
					if(%tempBrickEndY > %checkStartY) {
						%yOverlap = 1;
					}
				}
				//---end Y overlap test---

				//echo("x overlap = ", %xoverlap);
				//echo("y overlap = ", %yoverlap);
				//echo("z overlap = ", %zoverlap);

				if(%xOverlap && %yOverlap && %zOverlap && !$Pref::Server::BuildThrough){	
//nothing for now
				}
				}

			}//end if brick/baseplate 

			if(%checkObj.dead != true) //dont attach to bricks tagged for destruction
			{
				if(%zTouch && %xOverlap && %yOverLap)
				{
					%attachment = 1;	//we've attached to at least 1 brick
					//%checkObj.setSkinName('White');
					//we've attached to a brick, so add it to the attachement list of the brick we're planting
					if(%attachedOnTop == true)
					{
						//echo("adding to up list");
						//brick we checked is higher than the brick we're planting
						%newBrick.up[%newBrick.upSize] = %checkObj;
						%newBrick.upSize++;
					}
					else
					{
						//echo("adding to down list");
						//brick we checked is lower than the brick we're planting
						%newBrick.down[%newBrick.downSize] = %checkObj;
						%newBrick.downSize++;

						//bricks attached on top of hanging brick should be counted as hanging also
						if(%checkObj.wasHung != true)
						{
							%hanging = 0;
						}
					}
				}
			}

		}//end while search result loop

		if(%attachment == 0)
		{
			if($Pref::Server::FloatingBricks == 0)
			{
//nothing right now
			}
		}
		if($Pref::Server::BombRigging != 1 && %newBrick.Datablock $= StaticDynamite)
		{
//			messageClient(%client, '', "Bomb Rigging Disabled!");
//			%newBrick.delete();
//			return;
		}
		if($Pref::Server::BombRigging != 1 && %newBrick.Datablock $= StaticPlunger)
		{
//			messageClient(%client, '', "Bomb Rigging Disabled!");
//			%newBrick.delete();
//			return;
		}
//		if(%newBrick.Datablock $= staticPlunger && $Pref::Server::BombRigging $= 1)
//		{
//			if(%client.plantedplunger $= 1)
//			{
//			messageClient(%client, '', "You need to plant the Bomb Next!");
//			%newBrick.delete();
//			return;
//			}
//			if(%client.plantedbomb $= 1)
//			{
//			messageClient(%client, '', "You already have a Bomb Placed!");
//			%newBrick.delete();
//		return;
//			}
//			if(%client.timerigged != 0)
//			{
//			messageClient(%client, '', "You already have a Timed Bomb Placed!");
//			%newBrick.delete();
//			return;
//			}
//		}
//		if(%newBrick.Datablock $= StaticDynamite && $Pref::Server::BombRigging $= 1)
//		{	
//			
//
//			if(%client.plantedbomb $= 5)
//			{
//			messageClient(%client, '', "You already have the Max Bombs Placed!");
//			%newBrick.delete();
//			return;
//			}
//			if(%client.timerigged != 0)
//			{
//			messageClient(%client, '', "You already have a Timed Bomb Placed!");
//			%newBrick.delete();
//			return;
//			}
//		}
//		if(%newBrick.Datablock $= StaticbrickFire && %client.fireBrickCount >= 2)
//		{	
//			if(%client.isAdmin != 1 || %client.isSuperAdmin != 1)
//			{
//			messageClient(%client, '', "You can only have 2 Fire Bricks Placed!");		
//			%newBrick.delete();
//			return;
//			}
//		}
//		if(%hanging == 1)
//		{
//			//we're hanging
//			%newBrick.wasHung = true;
//		}

		//success! 
		
		//put the brick where its supposed to be
		%newBrick.setTransform(%tempBrick.getTransform());
		
		%tempbrick.setSkinName('green');



		//update the attachment lists of the bricks we just attached to//
		for(%i = 0; %i < %newBrick.upSize; %i++)
		{
			%attachedBrick = %newBrick.up[%i];
			
			%attachedBrick.down[%attachedBrick.downSize] = %newBrick;
			%attachedBrick.downSize++;
		}
			
		for(%i = 0; %i < %newBrick.downSize; %i++)
		{
			//error("checking down");
			%attachedBrick = %newBrick.down[%i];

			%attachedBrick.up[%attachedBrick.upSize] = %newBrick;
			%attachedBrick.upSize++;
		}
		//done updating attachment lists//
	}


}

function ServerCmdUpdateClientBots(%client)
{
	if (%client.isTempAdmin || %client.isAdmin || %client.isSuperAdmin)
	{
%count = %client.bcn;
commandToClient(%client, 'ClearClientBots');
	for (%i = 1; %i <= %count; %i++)
	{
	%bot = %client.Bots[%i];
		if (isObject(%bot))
		{
		%botName = %bot.getShapeName();
		if (%bot == %client.CurSBot) {
		%selectIt = 1;
		echo("selectit is now 1");
		}
		commandToClient(%client,'APF', %botName, %bot, %selectIt);
		%selectIt = 0;
		}
	}
	echo(%toSend SPC "is the %toSend");
	echo(%howMany SPC "is the %howMany");
	}
}

function ServerCmdBotSelectNorm(%client, %bot)
{
	if (%client.isTempAdmin || %client.isAdmin || %client.isSuperAdmin)
	{
	if (%bot.BotOwner $= %client)
	{
	%client.curSBot = %bot;
//	messageClient(%client,'',"\c2You have selected" SPC %bot.getShapeName());
	}
	}
}

function ServerCmdBotGUIDelete(%client, %bot)
{
	if (%client.isTempAdmin || %client.isAdmin || %client.isSuperAdmin)
	{
	if (%bot.BotOwner = %client)
	{
	messageClient(%client,'',"\c2You have deleted" SPC %bot.getShapeName());
	echo(%bot);
	%bot.delete();
	ServerCmdUpdateClientBots(%client);
	}
	}
}


function ServerCmdClientCreateBot(%client, %name)
{
	if (%client.isTempAdmin || %client.isAdmin || %client.isSuperAdmin)
	{
%rightIM = %client.player.getMountedImage($rightHandSlot);
%rightIMC = %rightIM.getSkinName();
$BotCount++;
$Bot[$BotCount] = new AIPlayer() {
      dataBlock = LightMaleHumanArmor;
      aiPlayer = true;
   };
   %client.bcn++;
%client.Bots[%client.bcn] = $Bot[$BotCount];
   MissionCleanup.add($Bot[$BotCount]);
%pos = %client.player.getTransform();
   // Player setup
   $Bot[$BotCount].setMoveSpeed(1);
   $Bot[$BotCount].setTransform(%pos);
   $Bot[$BotCount].setEnergyLevel(60);
   $Bot[$BotCount].setShapeName(%name);
   $Bot[$BotCount].TargetObj = 0;
   $Bot[$BotCount].Type = %type;
   $Bot[$BotCount].BotOwner = %client;
   $Bot[$BotCount].NoHurt = 1;
   $Bot[$BotCount].setSkinName(%client.player.getSkinName());

   $Bot[$BotCount].OrignialRotation = $Bot[$BotCount].GetAimLocation();

			$Bot[$BotCount].mountImage(%client.headCode, $headSlot, 1, %client.headCodeColor);
			$Bot[$BotCount].mountImage(%client.visorCode, $visorSlot, 1, %client.visorCodeColor);
			$Bot[$BotCount].mountImage(%client.backCode, $backSlot, 1, %client.backCodeColor);
			$Bot[$BotCount].mountImage(%client.leftHandCode, $leftHandSlot, 1, %client.leftHandCodeColor);
			$Bot[$BotCount].mountImage(%client.chestCode , $decalslot, 1, %client.chestdecalcode);
			$Bot[$BotCount].mountImage(%client.faceCode , $faceslot, 1, %client.faceprintcode);
			$Bot[$BotCount].mountImage(%rightIM, $rightHandSlot, 1, %rightIMC);
			$Bot[$BotCount].setTransform(%client.player.getTransform());
			$Bot[$BotCount].setShapeName(%name);
	%client.curSBot = $Bot[$BotCount];
	ServerCmdUpdateClientBots(%client);
	}
}
function ServerCmdBotGUITypeNoCall(%client, %type, %bot) {
	if ((%client.isTempAdmin || %client.isAdmin || %client.isSuperAdmin) && isObject(%client.curSBot))
	{
		//%bot = %client.CurSBot;
		%bot.CMBot = 0;
		%bot.isPatBot = 0;
			if (%type $= "Patrol")
			{
			%bot.isPatBot = 1;
			%bot.BotType = "Patrol";
			}
			else if (%type $= "Follow")
			{
			%bot.isFolBot = 1;
			%bot.BotType = "Follow";
			}
			else if (%type $= "Chop")
			{
			%bot.BotType = "Chop";
			}
			else
			{
			%bot.BotType = "";
			}
	}
}

function ServerCmdBotGUIType(%client, %type, %bot)
{
	if ((%client.isTempAdmin || %client.isAdmin || %client.isSuperAdmin) && isObject(%client.curSBot))
	{
//%bot = %client.CurSBot;
%bot.CMBot = 0;
%bot.isPatBot = 0;
	if (%type $= "Patrol")
	{
	%bot.isPatBot = 1;
	%bot.BotType = "Patrol";
	messageClient(%client,'',"\c2" SPC %bot.getShapeName() SPC "is now a \"Patrol\" bot.");
	}
	else if (%type $= "Follow")
	{
	%bot.isFolBot = 1;
	%bot.BotType = "Follow";
	messageClient(%client,'',"\c2" SPC %bot.getShapeName() SPC "is now a \"Follow\" bot.");
	}
	else if (%type $= "Chop")
	{
	%bot.BotType = "Chop";
	messageClient(%client,'',"\c2" SPC %bot.getShapeName() SPC "is now a \"Chop\" bot.");
	}
	else
	{
	%bot.BotType = "";
	messageClient(%client,'',"\c2" SPC %bot.getShapeName() SPC "is now a \"None\" bot.");
	}}

	}

function ServerCmdUpdateBotPatrols(%client) {
	if (%client.isTempAdmin || %client.isAdmin || %client.isSuperAdmin)
	{
	commandToClient(%client, 'ClearBotPatrols', "");
	%groupNum = $PatrolGroups.NumOfGroups;
		for (%i = 1; %i < %groupNum; %i++) {
			%groupName = $PatrolGroups.groups[%i].name;
			commandToClient(%client,'AP', %groupName);
		}
	}
}

function ServerCmdGetSelBotType(%client, %bot, %val2)
{
	if (%client.isTempAdmin || %client.isAdmin || %client.isSuperAdmin)
	{
	if (%bot.BotOwner $= %client)
	{
		echo(%bot.BotType);
		if (%bot.BotType $= "Patrol")
		{
		commandToClient(%client,'SetBotType', "Patrol", %val2);
		}
		else if (%bot.BotType $= "Follow")
		{
		commandToClient(%client,'SetBotType', "Follow", %val2);
		}
		else if (%bot.BotType $= "Chop")
		{
		commandToClient(%client,'SetBotType', "Chop", %val2);
		}
		else
		{
		commandToClient(%client,'SetBotType', "None", %val2);
		}
	}
	}
}





function GuardBot3(%bot, %num)
{
	if (isObject(%bot))
	{
	%btrans = %bot.getTransform();
	%bx = getWord(%btrans, 0);
	%by = getWord(%btrans, 1);
	%bz = getWord(%btrans, 2);
	%BC = %bot.BotOwner;
	%count = ClientGroup.getCount();
		for (%i = 0; %i < %count; %i++)
		{
		//%client = fgfg;
		%client = ClientGroup.getObject(%i);
		%player = %client.player;
			if (isObject(%player))
			{
			%ptrans = %player.getTransform();
			%px = getWord(%ptrans, 0);
			%py = getWord(%ptrans, 1);
			%pz = getWord(%ptrans, 2);
//			echo(%px SPC " is the %px" SPC %py SPC %pz);
//			echo(%bx SPC " is the %bx" SPC %by SPC %bz);
				if (%bx < %px + %num && %bx > %px - %num && %by < %py + %num && %by > %py - %num && %bz < %pz + 1 && %bz > %pz - %num)
				{
				echo("doing the inside range");
					%didThis = 1;
					//echo(%client.name SPC "is the name of the person" SPC %BC SPC "::" SPC %player SPC "is the player");
					for(%t = 0; %t < %BC.FriendListNum+1; %t++)
					{	
						if(%BC.FriendList[%t] $= %client)
						{
						%isFriend = 1;
						}
					}
					%team = %BC.team;
					if(%client.team $= %team)
					{
						%isTeamMate = 1;
					}
					if (%BC != %client && !%isFriend && %bot.isFriendBot)
					{
						%bot.clearAim();
						%bot.setImageTrigger($RightHandSlot,false);
						%bot.setImageTrigger($RightHandSlot,true);
						%bot.setAimObject(%player);
					}
					else if (%BC != %client && %bot.isPrivBot)
					{
						%bot.clearAim();
						%bot.setImageTrigger($RightHandSlot,false);
						%bot.setImageTrigger($RightHandSlot,true);
						%bot.setAimObject(%player);
					}
					else if (%BC != %client && %isTeamMate && %bot.isTeamBot)
					{
						%bot.clearAim();
						%bot.setImageTrigger($RightHandSlot,false);
						%bot.setImageTrigger($RightHandSlot,true);
						%bot.setAimObject(%player);
					}
					else if (%BC != %client && %bot.isAdminBot && !%client.isAdmin)
					{
						%bot.clearAim();
						%bot.setImageTrigger($RightHandSlot,false);
						%bot.setImageTrigger($RightHandSlot,true);
						%bot.setAimObject(%player);
					}
					else if (%bot.isKillBot)
					{
						%bot.clearAim();
						%bot.setImageTrigger($RightHandSlot,false);
						%bot.setImageTrigger($RightHandSlot,true);
						%bot.setAimObject(%player);
					}
					else
					{
						%bot.setImageTrigger($RightHandSlot,false);
						%bot.clearAim();
					}
				}
				if (!%didThis)
				{
					%bot.setImageTrigger($RightHandSlot,false);
					%bot.clearAim();
				}
			%isFriend = 0;
			%isTeamMate = 0;
		}
		}
	if (%bot.isGuardBot)
	{
	schedule(100, 0, "GuardBot2", %bot, %num);
	}
	else
	{
	%bot.setImageTrigger($RightHandSlot,false);
	%bot.clearAim();
	}
	}
}

function ServerCmdGuardBot2(%client, %range, %team, %priv, %admin, %friend, %kill)
{
	if (%client.isTempAdmin || %client.isAdmin || %client.isSuperAdmin)
	{
%bot = %client.curSBot;
%bot.isTeamBot = %team;
%bot.isPrivBot = %priv;
%bot.isAdminBot = %admin;
%bot.isFriendBot = %friend;
%bot.isKillBot = %kill;
%bot.isGuardBot = 1;
%bot.team = %client.team;
GuardBot2(%bot, %range);
	}
}

function ServerCmdGuardBot2E(%client, %range, %team, %priv, %admin, %friend, %kill)
{
	if (%client.isTempAdmin || %client.isAdmin || %client.isSuperAdmin)
	{
%bot = %client.curSBot;
%bot.isTeamBot = %team;
%bot.isPrivBot = %priv;
%bot.isAdminBot = %admin;
%bot.isFriendBot = %friend;
%bot.isKillBot = %kill;
%bot.team = %client.team;
GuardBot2(%bot, %range);
	}
}

function ServerCmdStopGuardBot2(%client)
{
	if (%client.isTempAdmin || %client.isAdmin || %client.isSuperAdmin)
	{
%bot = %client.curSBot;
%bot.isGuardBot = 0;
	}
}





























function GuardBot2(%bot, %num)
{

	if (isObject(%bot))
	{
	%btrans = %bot.getTransform();
	%bx = getWord(%btrans, 0);
	%by = getWord(%btrans, 1);
	%bz = getWord(%btrans, 2);
	%BC = %bot.BotOwner;
	//-- start search
   %searchMasks = $TypeMasks::ShapeBaseObjectType;
   %radius = %num;
   %pos = %bot.getPosition();
   InitContainerRadiusSearch(%pos, %radius, %searchMasks);
  
   while ((%targetObject = containerSearchNext()) != 0) {
   %dist = containerSearchCurrRadiusDist();
   %target = %targetObject.getTransform();
   %id = %targetObject.getId();
   %name = %targetObject.getName();
   %thingy = %id.getclassName();
   //cancel(%player.runaway);
   //%player.setimagetrigger(0,0);
//	echo(%thingy @ " is thingie");
   if (%thingy $= player || %thingy $= AIPlayer)
   {
	%client = %id.client;
   }
   else
   {
						%bot.clearAim();
						%bot.setImageTrigger($RightHandSlot,false);
   }
   }
		//%client = fgfg;
		%player = %client.player;
			if (isObject(%player))
			{
			%ptrans = %player.getTransform();
			%px = getWord(%ptrans, 0);
			%py = getWord(%ptrans, 1);
			%pz = getWord(%ptrans, 2);
//			echo(%px SPC " is the %px" SPC %py SPC %pz);
//			echo(%bx SPC " is the %bx" SPC %by SPC %bz);
//				echo("doing the inside range");
					%didThis = 1;
					//echo(%client.name SPC "is the name of the person" SPC %BC SPC "::" SPC %player SPC "is the player");
					for(%t = 0; %t < %BC.FriendListNum+1; %t++)
					{	
						if(%BC.FriendList[%t] $= %client)
						{
						%isFriend = 1;
						}
					}
					%team = %BC.team;
					if(%client.team $= %team)
					{
						%isTeamMate = 1;
					}
					echo("%isFriend is " @ %isFriend);
					if (%BC != %client && !%isFriend && %bot.isFriendBot)
					{
						%bot.clearAim();
						%bot.setImageTrigger($RightHandSlot,false);
						%bot.setImageTrigger($RightHandSlot,true);
						%bot.setAimObject(%player);
					}
					else if (%BC != %client && %bot.isPrivBot)
					{
						%bot.clearAim();
						%bot.setImageTrigger($RightHandSlot,false);
						%bot.setImageTrigger($RightHandSlot,true);
						%bot.setAimObject(%player);
					}
					else if (%BC != %client && !%isTeamMate && %bot.isTeamBot)
					{
						%bot.clearAim();
						%bot.setImageTrigger($RightHandSlot,false);
						%bot.setImageTrigger($RightHandSlot,true);
						%bot.setAimObject(%player);
					}
					else if (%BC != %client && %bot.isAdminBot && !%client.isAdmin)
					{
						%bot.clearAim();
						%bot.setImageTrigger($RightHandSlot,false);
						%bot.setImageTrigger($RightHandSlot,true);
						%bot.setAimObject(%player);
					}
					else if (%bot.isKillBot)
					{
						%bot.clearAim();
						%bot.setImageTrigger($RightHandSlot,false);
						%bot.setImageTrigger($RightHandSlot,true);
						%bot.setAimObject(%player);
					}
					else
					{
						%bot.setImageTrigger($RightHandSlot,false);
						%bot.clearAim();
					}
				if (!%didThis)
				{
					%bot.setImageTrigger($RightHandSlot,false);
					%bot.clearAim();
				}
			%isFriend = 0;
			%isTeamMate = 0;
		}
		else
		{
								%bot.clearAim();
						%bot.setImageTrigger($RightHandSlot,false);
		}
	if (%bot.isGuardBot)
	{
	schedule(100, 0, "GuardBot2", %bot, %num);
	}
	else
	{
	%bot.setImageTrigger($RightHandSlot,false);
	%bot.clearAim();
	}
	}
}

function ServerCmdGuardBot3(%client, %range, %team, %priv, %admin, %friend, %kill)
{
	if (%client.isTempAdmin || %client.isAdmin || %client.isSuperAdmin)
	{
%bot = %client.curSBot;
%bot.isTeamBot = %team;
%bot.isPrivBot = %priv;
%bot.isAdminBot = %admin;
%bot.isFriendBot = %friend;
%bot.isKillBot = %kill;
%bot.isGuardBot = 1;
GuardBot3(%bot, %range);
	}
}

function ServerCmdGuardBot3E(%client, %range, %team, %priv, %admin, %friend, %kill)
{
	if (%client.isTempAdmin || %client.isAdmin || %client.isSuperAdmin)
	{
%bot = %client.curSBot;
%bot.isTeamBot = %team;
%bot.isPrivBot = %priv;
%bot.isAdminBot = %admin;
%bot.isFriendBot = %friend;
%bot.isKillBot = %kill;
GuardBot3(%bot, %range);
	}
}

function ServerCmdStopGuardBot3(%client)
{
	if (%client.isTempAdmin || %client.isAdmin || %client.isSuperAdmin)
	{
%bot = %client.curSBot;
%bot.isGuardBot = 0;
	}
}

function FollowBot2(%bot, %client, %newCli)
{
if (%newCli)
{
%bot.FolClient = %client;
}
%client = %bot.FolClient;
if (%bot.isRKBot)
{
%checkBoto = 1;
}
//echo(%bot.BotType);
if (!%checkBoto && %bot.BotType !$= "Follow")
{
%bot.clearAim();
%bot.setImageTrigger(4, 0);
%bot.setMoveDestination(%bot.getPosition());
return;
}
%player = %client.player;
	if (isObject(%bot))
	{
	%botVel = %bot.getVelocity();
	%bv1 = getWord(%botVel, 0);
	%bv2 = getWord(%botVel, 1);
	%bv3 = getWord(%botVel, 2);
	if (!%bot.isGuardBot)
	{
	%bot.clearAim();
	%bot.setAimObject(%player);
	}
	%ptrans = %player.getTransform();
	%btrans = %bot.getTransform();
	%bx = getWord(%btrans, 0);
	%by = getWord(%btrans, 1);
	%bz = getWord(%btrans, 2);
	%px = getWord(%ptrans, 0);
	%py = getWord(%ptrans, 1);
	%pz = getWord(%ptrans, 2);
		if (%bx > %px + 10)
		{
//		%bv1 = %bv1 - 2;
		%bv1 = %bv1 - 0.5;
		}
		if (%bx < %px - 10)
		{
//		%bv1 = %bv1 + 2;
		%bv1 = %bv1 + 0.5;
		}
		if (%by > %py + 10)
		{
//		%bv1 = %bv1 - 2;
		%bv2 = %bv2 - 0.5;
		}
		if (%by < %py - 10)
		{
//		%bv2 = %bv2 + 2;
		%bv2 = %bv2 + 0.5;
		}
		if (%bz > %pz + 5)
		{
		%bv3 = %bv3 - 10;
		}
		if (%bx > %px - 4 && %bx < %px + 4)
		{
		}
		if (%bx > %px - 4 && %bx < %px + 4)
		{
		}
		if (%bx > %px - 4 && %bx < %px + 4)
		{
		}
		if (%bx > %px - 4 && %bx < %px + 4)
		{
		}
		%cRan = 7;
		if (%bx > %px - %cRan && %bx < %px + %cRan && %by > %py - %cRan && %by < %py + %cRan && %bz > %pz - %cRan && %bx < %pz + %cRan)
		{
		%bv1 = 0;
		%bv2 = 0;
		%bv3 = 0;
		}
		if (!%bot.folStep)
		{
//		%bv1 = %bv1 / 2;
//		%bv2 = %bv2 / 2;
//		%bv3 = %bv3 / 2;
			if (%bz < %pz - 1)
			{
			%bot.setMoveDestination(%bot.getPosition());
			%bot.setImageTrigger(4, 1);
				if (bz < %pz - 10)
				{
				%bot.setVelocity(%bv1 SPC %bv2 SPC 5);
				}
//			%bot.setVelocity(%bv1 SPC %bv2 SPC %bv3 + 2);
			%bot.FolSched = schedule(200, 0, "FollowBot2", %bot, %client);
//			echo("this1");
			}
			else if (%bz > %pz - 1)
			{
			if (%bz > %pz + 7)
			{
			%bot.setVelocity(%bv1 @ %bv2 @ -5);
			}
			%bot.setMoveDestination(%player.getPosition());
			%bot.setImageTrigger(4, 1);
			%bot.FolSched = schedule(100, 0, "FollowBot2", %bot, %client);
//			echo("this2");
			}
			else
			{
			%bot.setMoveDestination(%px SPC %py + 3 SPC %pz);
			%bot.FolSched = schedule(100, 0, "FollowBot2", %bot, %client);
			}
			%bot.folStep = 1;
		}
		else if (%bot.folStep)
		{
			if (%bz < %pz - 1)
			{
			%bot.setImageTrigger(4, 1);
			%bot.setMoveDestination(%player.getPosition());
			%bot.FolSched = schedule(300, 0, "FollowBot2", %bot, %client);
//			echo("this3");
			}
			else if (%bz > %pz - 1)
			{
//			%bot.setVelocity(%bv1 SPC %bv2 SPC %bv3 - 3);
			%bot.setMoveDestination(%player.getPosition());
			%bot.setImageTrigger(4, 0);
			%bot.FolSched = schedule(100, 0, "FollowBot2", %bot, %client);
//			echo("this4");
			}
			else
			{
			%bot.setMoveDestination(%px SPC %py + 3 SPC %pz);
			%bot.FolSched = schedule(100, 0, "FollowBot2", %bot, %client);
			}
			%bot.folStep = 0;
		}
	}
}



function ServerCmdUpdateBotClientsList(%client)
{
	if (%client.isTempAdmin || %client.isAdmin || %client.isSuperAdmin)
	{
commandToClient(%client, 'ClearBotClientList');
%groupNum = $PatrolGroups.NumOfGroups;
%count = ClientGroup.getCount();
echo("%count is " @ %count);
	for (%i = 0; %i < %count; %i++)
	{
// id text index
	%client = ClientGroup.getObject(%i);
	%clientName = %client.NameBase;
	echo("looping" SPC %client SPC %clientName);
	commandToClient(%client, 'ABCL', %clientName, %client);
	echo("Client added to list");
	}
	}
}

function KeepEchoPos(%obj, %time)
{
	if (isObject(%obj))
	{
	%pos = getWords(%obj.getTransform(), 0, 2);
	messageAll("","\c2" @ %obj.getShapeName() SPC "is at the coordinates:" SPC %pos);
	schedule(%time, 0, "KeepEchoPos", %obj, %time);
	}
}

function ServerCmdBotGUIStartFol(%client, %victim)
{
	if ((%client.isTempAdmin || %client.isAdmin || %client.isSuperAdmin) && !%client.botGuiSelAll)
	{
FollowBot2(%client.CurSBot, %victim, 1);
	}
	else if ((%client.isTempAdmin || %client.isAdmin || %client.isSuperAdmin) && %client.botGuiSelAll)
	{
		%count = %client.bcn;
		for (%i = 1; %i <= %count; %i++)
		{
		%bot = %client.Bots[%i];
		echo("yay going all follow");
			if (isObject(%bot))
			{
			echo("going all follow");
			FollowBot2(%bot, %victim, 1);
			}
		}
	}
}
function ServerCmdBotGUIStopFol(%client)
{
	if (%client.isTempAdmin || %client.isAdmin || %client.isSuperAdmin)
	{
%bot = %client.CurSBot;
cancel(%bot.FolSched);
%bot.setMoveDestination(%bot.getTransform());
%bot.setImageTrigger(4, 0);
%bot.clearAim();
	}
}

function BotRandKill(%bot)
{
%STime = 100;
%bot.isRKBot = 1;
	if (isObject(%bot))
	{
	%bTrans = %bot.getTransform();
	%bx = getWord(%bTrans, 0);
	%by = getWord(%bTrans, 1);
	%bz = getWord(%bTrans, 2);
		if (!%bot.RKFol)
		{
		echo("no rk follow");
		%bot.setImageTrigger($RightHandSlot, 0);
		%bot.setMoveDestination(%bx SPC %by SPC %bz);
		%bot.clearAim();
		   %searchMasks = $TypeMasks::ShapeBaseObjectType;
	   %radius = 30;
	   %pos = %bot.getPosition();
	   InitContainerRadiusSearch(%pos, %radius, %searchMasks);
	  
	   while ((%targetObject = containerSearchNext()) != 0) {
	   %dist = containerSearchCurrRadiusDist();
	   %target = %targetObject.getTransform();
	   %id = %targetObject.getId();
	   %name = %targetObject.getName();
	   %thingy = %id.getclassName();
	   //cancel(%player.runaway);
	   //%player.setimagetrigger(0,0);
		echo(%thingy @ " is thingie");
	   if (%thingy $= player || %thingy $= AIplayer && %id !$= %bot)
	   {
		%client = %id.client;
		%gotPlayer = 1;
	   }
	   else if (!%gotPlayer)
	   {
							%bot.clearAim();
							%bot.setImageTrigger($RightHandSlot,false);
	   }
	   }
		if (%gotPlayer)
		{
		%player = %client.player;
		%bot.RKCurPlay = %player;
		cancel(%bot.FolSched);
		FollowBot2(%bot, %client);
		%bot.RKFol = 1;
		%bot.setImageTrigger($RightHandSlot, 1);
		}
		else
		{
		%STime = 100;
		%ranNum = getRandom(3);
			switch (%ranNum)
			{
			case 0:
				%bot.setMoveDestination(%bx + 2 SPC %by SPC %bz);
			case 1:
				%bot.setMoveDestination(%bx - 2 SPC %by SPC %bz);
			case 2:
				%bot.setMoveDestination(%bx SPC %by + 2 SPC %bz);
			case 3:
				%bot.setMoveDestination(%bx SPC %by - 2 SPC %bz);
			}
		}
	}
	else if (%bot.RKFol)
	{
	echo("RK FOL");
		if (!isObject(%bot.RKCurPlay))
		{
		cancel(%bot.FolSched);
		%bot.setMoveDestination(%btrans);
		%bot.setImageTrigger($rightHandSlot, 0);
		%bot.clearAim();
		%bot.RKCurPlay = 0;
		%bot.RKFol = 0;
		}
	}
	schedule(%STime, 0, "BotRandKill", %bot);
	}
}


function OrigClockNit(%obj)
{
if (!isObject(%obj))
{
return;
}
	if (!%obj.HandsCreated)
	{
	%obj.SecHand = new StaticShape()
	{
		datablock = staticbrick1x1;
		scale = "1 1 3";
	};
	%obj.MinHand = new StaticShape()
	{
		datablock = staticbrick1x1;
		scale = "1 1 4";
	};
	%obj.HourHand = new StaticShape()
	{
		datablock = staticbrick1x1;
		scale = "1 1 2";
	};
	%obj.HandsCreated = 1;
	%obj.ClockSeconds = -1;
	%obj.ClockMinutes = 0;
	%obj.ClockHours = 0;
	}
	%obj.ClockSeconds++;
	if (%obj.ClockSeconds > 59)
	{
	%obj.ClockSeconds = 0;
	%obj.ClockMinutes++;
	}
	if (%obj.ClockMinutes > 59)
	{
	%obj.ClockMinutes = 0;
	%obj.ClockHours += 5;
	}
	if (%obj.ClockHours > 59)
	{
	%obj.ClockHours = 0;
	}
	%CSRotB = 6 * %obj.ClockSeconds SPC 0 SPC 0;
	%CMRotB = 6 * %obj.ClockMinutes SPC 0 SPC 0;
	%CHRotB = 6 * %obj.ClockHours SPC 0 SPC 0;
	echo(%CSRotB);
	%BasePos = getWords(%obj.getTransform(), 0, 2);
	echo(%basePos SPC eulerToQuat(%CSRotB));
	%obj.SecHand.setTransform(%basePos SPC eulerToQuat(%CSRotB));
	%obj.MinHand.setTransform(%basePos SPC eulerToQuat(%CMRotB));
	%obj.HourHand.setTransform(%basePos SPC eulerToQuat(%CHRotB));
	%obj.SecHand.setShapeName("");//%obj.ClockSeconds);
	%obj.MinHand.setShapeName("");//%obj.ClockMinutes);
	%obj.HourHand.setShapeName("");//%obj.ClockHours / 5);
schedule(1000, 0, "OrigClockNit", %obj);
}

function ServerCmdStartDeathBot(%client)
{
	if (%client.isTempAdmin || %client.isAdmin || %client.isSuperAdmin)
	{
%rightIM = %client.player.getMountedImage($rightHandSlot);
%rightIMC = %rightIM.getSkinName();
$BotCount++;   
$Bot[$BotCount] = new AIPlayer() {
      dataBlock = LightMaleHumanArmor;
      aiPlayer = true;
   };
   %client.bcn++;
%client.Bots[%client.bcn] = $Bot[$BotCount];
   MissionCleanup.add($Bot[$BotCount]);
%pos = %client.player.getTransform();
   // Player setup
   $Bot[$BotCount].setMoveSpeed(1);
   $Bot[$BotCount].setTransform(%pos);
   $Bot[$BotCount].setEnergyLevel(60);
   $Bot[$BotCount].setShapeName(%name);
   $Bot[$BotCount].TargetObj = 0;
   $Bot[$BotCount].Type = %type;
   $Bot[$BotCount].BotOwner = %client;
   $Bot[$BotCount].NoHurt = 1;
   $Bot[$BotCount].setSkinName(%client.player.getSkinName());

   $Bot[$BotCount].OrignialRotation = $Bot[$BotCount].GetAimLocation();

			$Bot[$BotCount].mountImage(%client.headCode, $headSlot, 1, %client.headCodeColor);
			$Bot[$BotCount].mountImage(%client.visorCode, $visorSlot, 1, %client.visorCodeColor);
			$Bot[$BotCount].mountImage(%client.backCode, $backSlot, 1, %client.backCodeColor);
			$Bot[$BotCount].mountImage(%client.leftHandCode, $leftHandSlot, 1, %client.leftHandCodeColor);
			$Bot[$BotCount].mountImage(%client.chestCode , $decalslot, 1, %client.chestdecalcode);
			$Bot[$BotCount].mountImage(%client.faceCode , $faceslot, 1, %client.faceprintcode);
			$Bot[$BotCount].mountImage(%rightIM, $rightHandSlot, 1, %rightIMC);
			$Bot[$BotCount].setTransform(%client.player.getTransform());
			$Bot[$BotCount].setShapeName("Death");
	%client.curSBot = $Bot[$BotCount];
	ServerCmdUpdateClientBots(%client);
%client.curSBot.setTransform("10000 10000 500");
%client.curSBot.setScale("60 60 60");
%client.curSBot.BotType = "Follow";
FollowBot2(%client.curSBot, %client);
keepEchoPos(%client.curSBot, 10000);
	}
}
function chopbot2(%bot)
{
	if (!isObject(%bot) || %bot.BotType !$= "Chop")
	{
		%bot.setMoveDestination(%bot.getPosition());
		%bot.setImageTrigger($rightHandSlot, 0);
		%bot.clearAim();
		return;
	}
	if (!%bot.hasATree)
	{
	%num = 20;
   %searchMasks = $TypeMasks::ShapeBaseObjectType;
   %radius = %num;
   %pos = %bot.getPosition();
   InitContainerRadiusSearch(%pos, %radius, %searchMasks);
  
   while ((%targetObject = containerSearchNext()) != 0) {
   %dist = containerSearchCurrRadiusDist();
   %target = %targetObject.getTransform();
   %id = %targetObject.getId();
   %name = %targetObject.getName();
   %thingy = %id.getDataBlock().classname;
   %thingy2 = %id.getDataBlock().ghost;
   //cancel(%player.runaway);
   //%player.setimagetrigger(0,0);
//	echo(%thingy @ " is thingie");
   if ((%thingy $= "pineTree" || %thingy2 $= "ghostBrickPine") && !%id.noUseTree && !%bot.hasATree)
   {
	%bot.botsTree = %id;
	%bot.hasATree = 1;
	%id.noUseTree = 1;
	%bot.setMoveDestination(getWords(%id.getTransform(), 0, 2));
	%bot.setImageTrigger($rightHandSlot, true);
	%bot.clearAim();
	%bot.setAimObject(%id);
	%id.BotTreeAttacker = %bot;
   }
   else if (!%bot.hasATree)
   {
						%bot.clearAim();
						%bot.setImageTrigger($RightHandSlot,false);
						%bot.setMoveDestination(%bot.getPosition());
   }
   }
   }
   else if (%bot.hasATree)
   {
//   echo("has a tree");
	if (%bot.botsTree.botBeDoneT)
	{
	%bot.hasATree = 0;
	%bot.clearAim();
	}
   }
%bot.isAChopBot = 1;
%bot.ChopSched = schedule(100, 0, "chopbot2", %bot);
}

function LetBotUseT(%TheTree)
{
echo("let bot use tree agn");
%TheTree.botBeDoneT = 0;
%TheTree.noUseTree = 0;
}

function ServerCmdStartChopBot(%client)	{
	if ((%client.isTempAdmin || %client.isAdmin || %client.isSuperAdmin) && %client.CurSBot.BotType $= "Chop")
	{
		chopBot2(%client.curSBot);
	}
}

function ServerCmdStopChopBot(%client)	{
	if ((%client.isTempAdmin || %client.isAdmin || %client.isSuperAdmin) && %client.CurSBot.BotType $= "Chop")
	{
		cancel(%bot.ChopSched);
		%bot = %client.curSBot;
		%bot.setMoveDestination(%bot.getPosition());
		%bot.setImageTrigger($rightHandSlot, 0);
		%bot.clearAim();
	}
}

function servercmdmonkeyBot(%client,%color)	{
	monkeyABot(%client.curSBot, %color);
}
function monkeyABot(%bot, %color) {
   %bot.mountimage(monkeyshowimage,2,false,%color); 
   %bot.setcloaked(1);  
}

function ServerCmdBotST(%client)
{
	if (%client.isTempAdmin || %client.isAdmin || %client.isSuperAdmin)
	{
%count = $BotCount;
echo("calling it2");
for (%i = 0; %i <= %count; %i++)
{
%bot = $Bot[%i];
echo(%bot);
if (isObject(%bot))
{
echo("deleting");
%bot.setTransform(%client.player.getTransform());
}
}
	}
}

function ServerCmdBotGuiSelAll(%client)
{
	if (%client.isTempAdmin || %client.isAdmin || %client.isSuperAdmin)
	{
		if (%client.botGuiSelAll)
		{
		%client.botGuiSelAll = 0;
		}
		else
		{
		%client.botGuiSelAll = 1;
		}
	}
}

function ServerCmdallBotFol(%client, %victim)
{
	for (%i = 0; %i < %client.bcn; %i++)
	{
	%bot = %client.Bots[%i];
	if (isObject(%bot))
	{
	%bot.botType = "Follow";
	%bot.FolClient = %victim;
	followbot2(%bot, %victim);
	monkeyABot(%bot, 'brown');
	}
	}
}
function ServerCmdSallBotFol(%client)
{
	for (%i = 0; %i < %client.bcn; %i++)
	{
	%bot = %client.Bots[%i];
	if (isObject(%bot))
	{
	%bot.botType = "Follow";
	%bot.FolClient = %victim;
	cancel(%bot.FolSched);
	%bot.clearAim();
	%bot.setMoveDestination(%bot.getPosition());
	%bot.setimagetrigger(4, 0);
	monkeyABot(%bot, 'brown');
	}
	}
}

function ServerCmdUpdateBotPrefs(%client, %name, %skin,
								%headCode, %visorCode, %backCode, %leftHandCode,
								%headCodeColor, %visorCodeColor, %backCodeColor, %leftHandCodeColor, %decal,%faceprint)
{

	%client.BName = %name;
	%client.colorSkin					= $legoColor[%skin];

	%client.BheadCode			= $headCode[%headCode];
	%client.BvisorCode			= $visorCode[%visorCode];
	%client.BbackCode			= $backCode[%backCode];
	%client.BleftHandCode		= $leftHandCode[%leftHandCode];
	%client.BchestCode           = basedecalImage;
	%client.BfaceCode            = facelegoImage;

	%client.BheadCodeColor		= $legoColor[%headCodeColor];
	%client.BvisorCodeColor		= $legoColor[%visorCodeColor];
	%client.BbackCodeColor		= $legoColor[%backCodeColor];
	%client.BleftHandCodeColor	= $legoColor[%leftHandCodeColor];
	%client.Bchestdecalcode		= addTaggedString(%decal);
	%client.Bfaceprintcode		= addTaggedString(%faceprint);
	%player = %client.player;
	if(isObject(%player) && %nhsadnav) //save this statement for later
	{
		if($Server::MissionType $= "SandBox" && %client.player)
		{
			%player.unMountImage($headSlot);
			%player.unMountImage($visorSlot);
			%player.unMountImage($backSlot);
			%player.unMountImage($leftHandSlot);
			%player.unMountImage($decalslot);
			%player.unMountImage($faceprintslot);

			%player.setSkinName(%client.colorSkin);
			%player.mountImage(%client.headCode, $headSlot, 1, %client.headCodeColor);
			%player.mountImage(%client.visorCode, $visorSlot, 1, %client.visorCodeColor);
			%player.mountImage(%client.backCode, $backSlot, 1, %client.backCodeColor);
			%player.mountImage(%client.leftHandCode, $leftHandSlot, 1, %client.leftHandCodeColor);
			%player.mountImage(%client.chestCode , $decalslot, 1, %client.chestdecalcode);
			%player.mountImage(%client.faceCode , $faceslot, 1, %client.faceprintcode);
		}
		setThePlayerName(%client);
	}
}

function ServerCmdBotGUIUpBotAp(%client, %bot)
{
	if (%bot.BotOwner == %client)
	{
	echo("updating bot");
			%bot.unMountImage($headSlot);
			%bot.unMountImage($visorSlot);
			%bot.unMountImage($backSlot);
			%bot.unMountImage($leftHandSlot);
			%bot.unMountImage($decalslot);
			%bot.unMountImage($faceprintslot);

			%bot.setSkinName(%client.bcolorSkin);
			%bot.mountImage(%client.BheadCode, $headSlot, 1, %client.BheadCodeColor);
			%bot.mountImage(%client.BvisorCode, $visorSlot, 1, %client.BvisorCodeColor);
			%bot.mountImage(%client.BbackCode, $backSlot, 1, %client.BbackCodeColor);
			%bot.mountImage(%client.BleftHandCode, $leftHandSlot, 1, %client.BleftHandCodeColor);
			%bot.mountImage(%client.BchestCode , $decalslot, 1, %client.Bchestdecalcode);
			%bot.mountImage(%client.BfaceCode , $faceslot, 1, %client.Bfaceprintcode);
	}
}