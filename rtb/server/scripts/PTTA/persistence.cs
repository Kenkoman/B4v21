// Created by Rick
// rick@gibbed.us
// http://rick.gibbed.us/

// Version: 1.4

// Mission-oriented block persistence for Blockland.

// Copyright (c) 2005 gibbed.us
// 
// This software is provided 'as-is', without any express or implied warranty.
// In no event will the authors be held liable for any damages arising from the
// use of this software.
// 
// Permission is granted to anyone to use this software for any purpose,
// including commercial applications, and to alter it and redistribute it
// freely, subject to the following restrictions:
// 
// 1. The origin of this software must not be misrepresented; you must not claim
//    that you wrote the original software. If you use this software in a product,
//    an acknowledgment in the product documentation would be appreciated but is
//    not required.
// 
// 2. Altered source versions must be plainly marked as such, and must not be
//    misrepresented as being the original software.
// 
// 3. This notice may not be removed or altered from any source distribution.


function loadPersistence(%client, $UniquePersistName)
{
	messageAll("MsgPersistence", "\c3Blocks are being loaded from \c0" @ $UniquePersistName @ ".persistence\c3. Please Wait...");
	%filename = $Server::MissionFile @ "_" @ $UniquePersistName @ ".persistence";
	echo("Loading persistence file " @ %filename);

	%file = new FileObject();
	%file.openForRead(%filename);

	loadPersistenceStep(%file);
}

function loadPersistenceStep(%file)
{
	%count = 0;
	%temp = new FileObject();
	%temp.openForWrite("rtb/server/scripts/temp.persistence.cs");

	while(!%file.isEOF() && %count < 50)
	{
		%line = %file.readLine();
		if(%line $= "") %count++;
		%temp.writeLine(%line);
	}

	%temp.close();

	exec("rtb/server/scripts/temp.persistence.cs");

	if(%file.isEOF())
	{
		%file.close();
		loadPersistence2();
	}
	else
	{
		Schedule(1000, 0, "loadPersistenceStep", %file);
	}
}


function loadPersistence2()
{
	//This now covers everything after loading bricks

	$TotalCopSpawnPoints = 0;
	$TotalRobberSpawnPoints = 0;
$ServerGroup = 0;
for (%i = 0; %i < MissionCleanup.getCount(); %i++)
		{
			%brick = MissionCleanup.getObject(%i);

			if(%brick.isDoor)
			{
				if(%brick.group > $ServerGroup)
				{
					$ServerGroup = %brick.group;
				}
				if(%brick.movenums > 1 && %brick.ReturnToggle $= 0)
				{
					%brick.onmovenum = 1;
					%brick.HasReturned = 1;
				}
				if(%brick.movenums > 1 && %brick.isMoving $= 1)
				{
					%brick.onmovenum = 1;
				}
				if(%brick.origrot $= "")
				{
					%brick.isMoving = 0;
				}
				else if(%brick.isMoving $= 1)
				{
					%brick.EulerRot = %brick.origrot;
					%brick.HasReturned = 1;
					%brick.setTransform(%brick.origpos);
				}
				$Movers[$TotalMoversPlaced++] = %brick;
				%brick.isMoving = 0;
			}

			if(%brick.isCopSpawn)
			{
				$CopSpawn[$TotalCopSpawnPoints++] = %brick;
			}
			if(%brick.isRobberSpawn)
			{
				$RobberSpawn[$TotalRobberSpawnPoints++] = %brick;
			}
			if(%brick.FXmode $= 1)
			{
			   %curtrans = %brick.getTransform();
			   %curx = getword(%curtrans,0);
		           %cury = getword(%curtrans,1);
		           %curz = getword(%curtrans,2);
			   %newX = %curx + 0.5;
			   %newY = %cury + 0.5;
			   %newZ = %curz + 0.2;
			   %newTrans = %newX SPC %newY SPC %newZ;
			   %brick.flameEmitter = new ParticleEmitterNode(brickFireNode) {
     			   position = %newTrans;
      			   rotation = "1 0 0 0";
      			   scale = "1 1 1";
      			   dataBlock = "FireParticleEmitterNode";
     		   	   emitter = "FireParticleEmitter";
      			   velocity = "1.0";
   			   };
			}
			if(%brick.FXmode2 $= 1)
			{
			   %curtrans = %brick.getTransform();
			   %curx = getword(%curtrans,0);
		           %cury = getword(%curtrans,1);
		           %curz = getword(%curtrans,2);
			   %newX = %curx + 0.5;
			   %newY = %cury + 0.5;
			   %newZ = %curz + 0.2;
			   %newTrans = %newX SPC %newY SPC %newZ;
			   %brick.smokeEmitter = new ParticleEmitterNode(brickSmokeNode) {
     			   position = %newTrans;
      			   rotation = "1 0 0 0";
      			   scale = "1 1 1";
      			   dataBlock = "SmokeParticleEmitterNode";
     		   	   emitter = "SmokeParticleEmitter";
      			   velocity = "1.0";
   			   };
			}
			if(%brick.FXmode5 $= 1)
			{
			   %curtrans = %brick.getTransform();
			   %curx = getword(%curtrans,0);
		           %cury = getword(%curtrans,1);
		           %curz = getword(%curtrans,2);
			   %newX = %curx + 0.5;
			   %newY = %cury + 0.5;
			   %newZ = %curz + 0.2;
			   %newTrans = %newX SPC %newY SPC %newZ;
			   %brick.bubbleEmitter = new ParticleEmitterNode(brickBubblesNode) {
     			   position = %newTrans;
      			   rotation = "1 0 0 0";
      			   scale = "1 1 1";
      			   dataBlock = "fireParticleEmitterNode";
     		   	   emitter = "BubbleParticleEmitter";
      			   velocity = "1.0";
   			   };
			}
			if(%brick.isBaseTrigger)
			{
				%size = %brick.TriggerSize;
				%trans = %brick.getWorldBoxCenter();
				%x = getWord(%trans,0)-(getword(%size,0)/2);
				%y = getWord(%trans,1)+(getword(%size,1)/2);
				%z = getWord(%trans,2);
				%brick.BaseTrigger = new Trigger() {
     				position = %x SPC %y SPC %z;
      				rotation = "1 0 0 0";
      				scale = %brick.TriggerSize;
      				dataBlock = "DepositTrigger";
      				polyhedron = "0.0000000 0.0000000 0.0000000 1.0000000 0.0000000 0.0000000 0.0000000 -1.0000000 0.0000000 0.0000000 0.0000000 1.0000000";
   				};
			}
			if(%brick.isBankTrigger)
			{
				%size = %brick.TriggerSize;
				%trans = %brick.getWorldBoxCenter();
				%x = getWord(%trans,0)-(getword(%size,0)/2);
				%y = getWord(%trans,1)+(getword(%size,1)/2);
				%z = getWord(%trans,2);
				%brick.BankTrigger = new Trigger() {
     				position = %x SPC %y SPC %z;
      				rotation = "1 0 0 0";
      				scale = %brick.TriggerSize;
      				dataBlock = "StealTrigger";
      				polyhedron = "0.0000000 0.0000000 0.0000000 1.0000000 0.0000000 0.0000000 0.0000000 -1.0000000 0.0000000 0.0000000 0.0000000 1.0000000";
   				};
			}
			if(%brick.isprinted)
			{
				%decal = new StaticShape() 
				{
					dataBlock = "staticprint"@%brick.printer;
 				};

				%brick.mounteddecal = %decal;
				%brick.mountobject(%decal,0);
				%decal.setskinname(%brick.printName);
				%decal.setScale(%brick.getScale());
			}

		}

	if($Pref::Server::LoadPersistenceWithOwnerships)
	{
		for (%i = 0; %i < MissionCleanup.getCount(); %i++)
		{
			for(%t = 0; %t < ClientGroup.getCount(); %t++)
			{
				%brick = MissionCleanup.getObject(%i);
				%cl = ClientGroup.getObject(%t);
				if(%brick.OwnerIP $= getRawIP(%cl))
				{
					%brick.Owner = %cl;
					%brick.OwnerAway = 0;
				}
			}
		}
	}
	else
	{
		for (%i = 0; %i < MissionCleanup.getCount(); %i++)
		{
			%brick = MissionCleanup.getObject(%i);
			%brick.Owner = %client;
		}
	}
	//%testy = schedule(1000 * 60 * 1, 0, "saveSchedule");
	//saveSchedule();
	messageAll("MsgPersistence", "\c3Finished loading blocks from \c0" @ $UniquePersistName @ ".persistence\c3.");
}

function savePersistence($UniquePersistName)
{
	messageAll("MsgPersistence", "\c3Blocks are being saved to \c0" @ $UniquePersistName @ ".persistence\c3. Please wait...");
	saveBlocks("./" @ $Server::MissionFile @ "_" @ $UniquePersistName @ ".persistence");
	messageAll("MsgPersistence", "\c3Finished saving Blocks to \c0" @ $UniquePersistName @ ".persistence\c3.");
	//saveLists();
}

function saveBlocks(%filename)
{
	// Let's add some brick "persistence".
	%file = new FileObject();
	%file.openForWrite(%filename);
	
	// Generate script which searches for
	// baseplates in the level and assigns them to variables.
	// It also sets the baseplate color. :)

	%file.writeLine("// Add Extra Map Items");
	//%file.writeLine("$TotalAddedMapItems = 0;");

	for(%t = 0; %t < $TotalAddedMapItems; %t++)
	{
		
		if($AddedMapItems[%t].dataBlock $= "baseplate32")
		{
			%file.writeLine("$AddedMapItems[$TotalAddedMapItems] = new StaticShape()");
			%file.writeLine("{");
			%file.writeLine("\tposition = \"" @ $AddedMapItems[%t].position @ "\";");
			%file.writeLine("\trotation = \"" @ $AddedMapItems[%t].rotation @ "\";");
			%file.writeLine("\tscale = \"" @ $AddedMapItems[%t].scale @ "\";");
			%file.writeLine("\tdatablock = \""@$AddedMapItems[%t].Datablock@"\";");
			%file.writeLine("};");
			%file.writeLine("MissionGroup.add($AddedMapItems[$TotalAddedMapItems]);");
			%file.writeLine("$TotalAddedMapItems++;");
		}
		if($AddedMapItems[%t].dataBlock $= "gray32")
		{
			%file.writeLine("$AddedMapItems[$TotalAddedMapItems] = new StaticShape()");
			%file.writeLine("{");
			%file.writeLine("\tposition = \"" @ $AddedMapItems[%t].position @ "\";");
			%file.writeLine("\trotation = \"" @ $AddedMapItems[%t].rotation @ "\";");
			%file.writeLine("\tscale = \"" @ $AddedMapItems[%t].scale @ "\";");
			%file.writeLine("\tdatablock = \""@$AddedMapItems[%t].Datablock@"\";");
			%file.writeLine("};");
			%file.writeLine("MissionGroup.add($AddedMapItems[$TotalAddedMapItems]);");
			%file.writeLine("$TotalAddedMapItems++;");
		}
		else
		{
			%file.writeLine("$AddedMapItems[%t] = new Item()");
			%file.writeLine("{");
			%file.writeLine("\tposition = \"" @ $AddedMapItems[%t].position @ "\";");
			%file.writeLine("\trotation = \"" @ $AddedMapItems[%t].rotation @ "\";");
			%file.writeLine("\tscale = \"" @ $AddedMapItems[%t].scale @ "\";");
			%file.writeLine("\tdatablock = \"" @ $AddedMapItems[%t].Datablock @ "\";");
			%file.writeLine("\tcollideable = \"" @ $AddedMapItems[%t].collideable @ "\";");
			%file.writeLine("\tstatic = \"" @ $AddedMapItems[%t].static @ "\";");
			%file.writeLine("\trotate = \"" @ $AddedMapItems[%t].rotate @ "\";");
			%file.writeLine("};");
			%file.writeLine("MissionGroup.add($AddedMapItems[$TotalAddedMapItems]);");
			%file.writeLine("if($Pref::Server::ItemsCostMoney)");
			%file.writeLine("{");
%file.writeLine("if($AddedMapItems[$TotalAddedMapItems].Datablock.Cost > 0)");
%file.writeLine("{");
%file.writeLine("$AddedMapItems[$TotalAddedMapItems].setShapeName(\"$\"@$AddedMapItems[$TotalAddedMapItems].Datablock.Cost);");
%file.writeLine("}");				
%file.writeLine("}");	
			
			%file.writeLine("$TotalAddedMapItems++;");
		}
	}
	
	%file.writeLine("// Search for baseplates");
	
	%file.writeLine("for (%i = 0; %i < MissionGroup.getCount(); %i++)");
	%file.writeLine("{");
	%file.writeLine("\t%obj = MissionGroup.getObject(%i);");
	
	%file.writeLine("\tif (%obj.dataBlock $= \"gray32\")");
	%file.writeLine("\t{");
	
	%count = MissionGroup.getCount();
	%first = true;
	for (%i = 0; %i < %count; %i++)
	{
		%obj = MissionGroup.getObject(%i);
	
		if (%obj.dataBlock $= "gray32")
		{
			if (%first)
			{
				%file.writeLine("\t\tif (%obj.position $= \"" @ %obj.position @ "\")");
				%file.writeLine("\t\t{");
				%first = false;
			}
			else
			{
				%file.writeLine("\t\telse if (%obj.position $= \"" @ %obj.position @ "\")");
				%file.writeLine("\t\t{");
			}
			if(%obj.isBrickGhostMoving $= 1)
			{
			%file.writeLine("\t\t\t%obj.setSkinName(\"" @ %obj.oldskinname @ "\");");
			}
			else
			{
			%file.writeLine("\t\t\t%obj.setSkinName(\"" @ %obj.getSkinName() @ "\");");
			}
			%file.writeLine("\t\t\t%block" @ %obj @ " = %obj;");
			%file.writeLine("\t\t}");
		}
	}
	
	%file.writeLine("\t}");
	%file.writeLine("}");
	
	%file.writeLine("");
	
	// Generate script to create bricks and plates
	
	%count = MissionCleanup.getCount();
	%file.writeLine("// Block creation");
	
	for (%i = 0; %i < %count; %i++)
	{
		%block = MissionCleanup.getObject(%i);

		if (%block.isBrickGhost !$= 1 && (getSubStr(%block.dataBlock, 0, 10) $= "staticRoad" || getSubStr(%block.dataBlock, 0, 15) $= "staticBaseplate" || getSubStr(%block.dataBlock, 0, 11) $= "staticBrick" || getSubStr(%block.dataBlock, 0, 11) $= "staticPlate" || getSubStr(%block.dataBlock, 0, 11) $= "staticSlope" || getSubStr(%block.dataBlock, 0, 11) $= "staticFence" || getSubStr(%block.dataBlock, 0, 14) $="staticcylinder" || getSubStr(%block.dataBlock, 0, 10) $="statictile"))
		{
			%file.writeLine("%block" @ %block @ " = new StaticShape()");
			%file.writeLine("{");
			if(%block.isMoving $= 1){
				%file.writeLine("\tposition = \"" @ %block.origpos @ "\";");
				%file.writeLine("\tposition = \"" @ %block.position @ "\";");
				%file.writeLine("\trotation = \"" @ %block.origrot @ "\";");
			}else{
				%file.writeLine("\tposition = \"" @ %block.position @ "\";");
				%file.writeLine("\trotation = \"" @ %block.rotation @ "\";");}
			//%file.writeLine("\tposition = \"" @ %block.position @ "\";");
			//%file.writeLine("\trotation = \"" @ %block.rotation @ "\";");
			%file.writeLine("\tscale = \"" @ %block.scale @ "\";");
			%file.writeLine("\tdataBlock = \"" @ %block.dataBlock @ "\";");
			%file.writeLine("\townerIP = \"" @ %block.ownerIP @ "\";");
			%file.writeLine("\townerAway = \"" @ 1 @ "\";");

			if(%block.FXmode > 0)
			{
				%file.writeLine("\t\FXmode = \"" @ %block.FXMode @ "\";");
			}
			if(%block.FXmode2 > 0)
			{
				%file.writeLine("\t\FXmode2 = \"" @ %block.FXMode2 @ "\";");
			}
			if(%block.FXmode5 > 0)
			{
				%file.writeLine("\t\FXmode5 = \"" @ %block.FXMode5 @ "\";");
			}	

			if(%block.isPrinted){
				%file.writeLine("\t\PrintName = \"" @ %block.printname @ "\";");
				%file.writeLine("\t\Printer = \"" @ %block.printer @ "\";");
				%file.writeLine("\t\mounteddecal = \"" @ %block.mounteddecal @ "\";");
				%file.writeLine("\t\isprinted = \"" @ %block.isprinted @ "\";");
			}
			%file.writeLine("\t\EulerRot = \"" @ %block.EulerRot @ "\";");
			if(%block.IsWeak)
				%file.writeLine("\t\IsWeak = \"" @ %block.IsWeak @ "\";");
			if(%block.IsAlarmSystem)
				%file.writeLine("\t\IsAlarmSystem = \"" @ %block.IsAlarmSystem @ "\";");
			if(%block.IsAlarmSystemCode)
				%file.writeLine("\t\IsAlarmSystemCode = \"" @ %block.IsAlarmSystemCode @ "\";");
			if(%block.isDoorbell)
				%file.writeLine("\t\IsDoorbell = \"" @ %block.IsDoorbell @ "\";");
			if(%block.isteleportObjGateway)
				%file.writeLine("\t\isTeleportObjGateway = \"" @ %block.IsTeleportObjGateway @ "\";");
			if(%block.LinkedByID){
				%file.writeLine("\t\LinkedByID = \"" @ %block.LinkedByID @ "\";");
				%file.writeLine("\t\LinkNum = \"" @ %block.LinkNum @ "\";");
			}
			if(%block.isImp)
			{
				%file.writeLine("\t\IsImp = \"" @ %block.IsImp @ "\";");
				%file.writeLine("\t\Imp = \"" @ %block.Imp @ "\";");
				%file.writeLine("\t\isTriggerImp = \"" @ %block.isTriggerImp @ "\";");
				%file.writeLine("\t\TriggerDoorID = \"" @ %block.TriggerDoorID @ "\";");
			}
			if(%block.isDoor $= 1)
			{
				%file.writeLine("\t\isDoor = \"" @ %block.isDoor @ "\";");
				%file.writeLine("\t\Steps = \"" @ %block.Steps @ "\";");
				%file.writeLine("\t\StepTime = \"" @ %block.StepTime @ "\";");
				%file.writeLine("\t\RotateXYZ = \"" @ %block.RotateXYZ @ "\";");
				%file.writeLine("\t\ReturnDelay = \"" @ %block.ReturnDelay @ "\";");
				%file.writeLine("\t\ReturnToggle = \"" @ %block.ReturnToggle @ "\";");
				%file.writeLine("\t\Elevator = \"" @ %block.Elevator @ "\";");
				%file.writeLine("\t\Private = \"" @ %block.Private @ "\";");
				%file.writeLine("\tnoCollision = \"" @ %block.noCollision @ "\";");
				%file.writeLine("\t\isMoving = \"" @ %block.isMoving @ "\";");
				%file.writeLine("\t\TriggerDoorID = \"" @ %block.TriggerDoorID @ "\";");
				%file.writeLine("\t\HasReturned = \"" @ %block.HasReturned @ "\";");
				%file.writeLine("\t\isImpulseTrigger = \"" @ %block.isImpulseTrigger @ "\";");
				%file.writeLine("\t\TriggerCloak = \"" @ %block.TriggerCloak @ "\";");
				%file.writeLine("\t\KeyProtected = \"" @ %block.KeyProtected @ "\";");
				%file.writeLine("\t\Password = \"" @ %block.Password @ "\";");
				%file.writeLine("\t\Team = \"" @ %block.Team @ "\";");
				%file.writeLine("\t\Group = \"" @ %block.Group @ "\";");
				%file.writeLine("\t\isLocked = \"" @ %block.isLocked @ "\";");
				%file.writeLine("\t\DoorType = \"" @ %block.DoorType @ "\";");
				%file.writeLine("\t\MoveNums = \"" @ %block.MoveNums @ "\";");
				%file.writeLine("\t\OnMoveNum = \"" @ %block.OnMoveNum @ "\";");
				%file.writeLine("\t\MoveXYZ = \"" @ %block.MoveXYZ @ "\";");
				if(%block.MoveNums > 1)
					%file.writeLine("\t\MoveXYZ2 = \"" @ %block.MoveXYZ2 @ "\";");
				if(%block.MoveNums > 2)
					%file.writeLine("\t\MoveXYZ3 = \"" @ %block.MoveXYZ3 @ "\";");
				if(%block.MoveNums > 3)
					%file.writeLine("\t\MoveXYZ4 = \"" @ %block.MoveXYZ4 @ "\";");
				%file.writeLine("\t\origpos = \"" @ %block.origpos @ "\";");
				%file.writeLine("\t\origrot = \"" @ %block.origrot @ "\";");
				%file.writeLine("\t\awaitingReturn = \"" @ %block.awaitingReturn @ "\";");
			}
			if(%block.teleportObj > 0)
			{
				%file.writeLine("\t\ teleportObj = \"" @ %block.teleportObj @ "\";");
			}
			if(%block.isDetBrick > 0)
			{
				%file.writeLine("\t\ isDetBrick = \"" @ %block.isDetBrick @ "\";");
			}
			if(%block.isDoorBell > 0)
			{
				%file.writeLine("\t\ isDoorBell = \"" @ %block.isDoorBell @ "\";");
			}
			if(%block.iskiller > 0)
			{
				%file.writeLine("\t\ iskiller = \"" @ %block.iskiller @ "\";");
			}
			if(%block.isscale > 0)
			{
				%file.writeLine("\t\ isscale = \"" @ %block.isscale @ "\";");
			}
			if(%block.ispaint > 0)
			{
				%file.writeLine("\t\ ispaint = \"" @ %block.ispaint @ "\";");
			}

			%file.writeLine("};");
			%file.writeLine("MissionCleanup.add(%block" @ %block @ ");");
			$numBlocks++;
			%file.writeLine("%block" @ %block @ ".setSkinName(\"" @ %block.getSkinName() @ "\");");
			%file.writeLine("%block" @ %block @ ".SkinName=\"" @ %block.getSkinName() @ "\";");
			%file.writeLine("%block" @ %block @ ".NoDestroy=\"" @ %block.noDestroy @ "\";");
			%file.writeLine("%block" @ %block @ ".isJail=\"" @ %block.isJail @ "\";");
			%file.writeLine("%block" @ %block @ ".JailMaxCount=\"" @ %block.JailMaxCount @ "\";");
			if(%block.isWCloaked $= 1)
			{
				%file.writeLine("%block" @ %block @ ".setcloaked(1)" @ ";");
				%file.writeLine("%block" @ %block @ ".isWCloaked=\"" @ %block.isWCloaked @ "\";");
			}
			%file.writeLine("%block" @ %block @ ".isBankTrigger=\"" @ %block.isBankTrigger @ "\";");
			%file.writeLine("%block" @ %block @ ".isBaseTrigger=\"" @ %block.isBaseTrigger @ "\";");
			%file.writeLine("%block" @ %block @ ".isRobberSpawn=\"" @ %block.isRobberSpawn @ "\";");
			%file.writeLine("%block" @ %block @ ".isCopSpawn=\"" @ %block.isCopSpawn @ "\";");
			%file.writeLine("%block" @ %block @ ".TriggerSize=\"" @ %block.TriggerSize @ "\";");
			%file.writeLine("%block" @ %block @ ".setShapeName(\"" @ %block.getShapeName() @ "\");");
			%file.writeLine("");
		}
	}
	
	%file.writeLine("// Block links");
	for (%i = 0; %i < %count; %i++)
	{
		%block = MissionCleanup.getObject(%i);
		
		if (getSubStr(%block.dataBlock, 0, 11) $= "staticBrick" || getSubStr(%block.dataBlock, 0, 11) $= "staticPlate" || getSubStr(%block.dataBlock, 0, 11) $= "staticSlope" || getSubStr(%block.dataBlock, 0, 11) $= "staticFence" || getSubStr(%block.dataBlock, 0, 14) $="staticcylinder")
		{
			%k = 0;
			for (%j = 0; %j < %block.upSize; %j++)
			{
				if (MissionCleanup.isMember(%block.up[%j]) || %block.up[%j].dataBlock $= "gray32")
				{
					%file.writeLine("%block" @ %block @ ".up[" @ %k @ "] = %block" @ %block.up[%j] @ ";");
					%k++;
				}
			}
			
			%file.writeLine("%block" @ %block @ ".upSize = \"" @ %k @ "\";");
			
			%k = 0;
			for (%j = 0; %j < %block.downSize; %j++)
			{
				if (MissionCleanup.isMember(%block.down[%j]) || %block.down[%j].dataBlock $= "gray32")
				{
					%file.writeLine("%block" @ %block @ ".down[" @ %k @ "] = %block" @ %block.down[%j] @ ";");
					%k++;
				}
			}
			
			%file.writeLine("%block" @ %block @ ".downSize = \"" @ %k @ "\";");
		}
	}
	
	%file.writeLine("// Baseplate links");
	
	%count = MissionGroup.getCount();
	for (%i = 0; %i < %count; %i++)
	{
		%obj = MissionGroup.getObject(%i);
		
		if (%obj.dataBlock $= "gray32")
		{
			%file.writeLine("%i = %block" @ %obj @ ".upSize;");
			for (%j = 0; %j < %obj.upSize; %j++)
			{
				if (isObject(%obj.up[%j]) && MissionCleanup.isMember(%obj.up[%j]))
				{
					%file.writeLine("%block" @ %obj @".up[$i++] = %block" @ %obj.up[%j] @ ";");
				}
			}
			%file.writeLine("%block" @ %obj @ ".upSize = %i;");
			
			%file.writeLine("%i = %block" @ %obj @ ".downSize;");
			for (%j = 0; %j < %obj.downSize; %j++)
			{
				if (isObject(%obj.down[%j]) && MissionCleanup.isMember(%obj.down[%j]))
				{
					%file.writeLine("%block" @ %obj @ ".down[$i++] = %block" @ %obj.down[%j] @ ";");
				}
			}
			%file.writeLine("%block" @ %obj @ ".downSize = %i;");
		}
	}

	// JBM: Save # of blocks
	%file.writeLine("$numBlocks = " @ $numBlocks @ ";");
	
	%file.close();
	
	// A new .persistence file will not be loadable until Blockland restarts,
	// so we force Blockland to regenerate it's filename cache.
	setModPaths(getModPaths());
	echo("Save finished.");

}
