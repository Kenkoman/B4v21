//######################################################################################################################################
//# Mover CMDs
//# ------------
//# Making all your doors work.
//# 
//# ----------
//# Functions:
//# ----------
//# checkColImpulse(%obj)
//# InitTimeStepObj(%client,%obj)
//# TimeStep(%client,%obj)
//# serverCmdApplyDoorSettings(%client,%MoveXYZ,%RotateXYZ,%Steps,%StepTime,%Elevator,%TriggerCloak,%Private,%Team,%Group,%isImpTrigger,%returnDelay,%returnToggle,%Password,%DoorType,%DSID,%NoCollision)
//# serverCmdCheckDoorPassword(%client,%password)
//# ExecuteMoverInstructions(%client, %obj)
//# 
//######################################################################################################################################



//######################################################################################################################################
//# Checking if Col has Impulse Properties.
//######################################################################################################################################
function checkColImpulse(%object)
{
	for(%t = 0; %t<ClientGroup.getCount(); %t++)
	{
		%cl = ClientGroup.getObject(%t);

		InitContainerRadiusSearch(%cl.player.getTransform(), 0.1, $TypeMasks::ShapeBaseObjectType);
		while ((%targetObject = containerSearchNext()) != 0) 
		{
			if(%targetObject $= %object)
			{
				%pos = getwords(%object.getTransform(), 0, 2);
				%cl.player.applyimpulse(%pos,%object.Imp);
			
			}
		}
	}
}


//######################################################################################################################################
//# Initiating the Mover Sequence
//######################################################################################################################################
function InitTimeStepObj(%client,%mover)
{
	%mover.StepsDone = 1;
	schedule(%mover.StepTime,0,"TimeStep",%client,%mover);
}


//######################################################################################################################################
//# Carrying out Mover Sequence
//######################################################################################################################################
function TimeStep(%client,%mover)
{
	if(isObject(%mover))
	{
		if(%mover.HasReturned $= 1 && %mover.origrot $= "")
		{
			%mover.origrot = %mover.EulerRot;
		}
		if(%mover.HasReturned $= 1 && %mover.origpos $= "")
		{
			%mover.setTransform(%mover.origpos);
		}

		//Get XYZ Move Factor.
		if(%mover.onmovenum !$= 0){
		%XYZ = %mover.MoveXYZ;
		%X = (getWord(%XYZ,0)*0.5) / (%mover.Steps);
		%Y = (getWord(%XYZ,1)*0.5) / (%mover.Steps);
		%Z = ((3*getWord(%XYZ,2))*0.2) / (%mover.Steps);
		}
		if(%mover.onmovenum $= 2){
		%XYZ = %mover.MoveXYZ2;
		%X = (getWord(%XYZ,0)*0.5) / (%mover.Steps);
		%Y = (getWord(%XYZ,1)*0.5) / (%mover.Steps);
		%Z = ((3*getWord(%XYZ,2))*0.2) / (%mover.Steps);
		}
		if(%mover.onmovenum $= 3){
		%XYZ = %mover.MoveXYZ3;
		%X = (getWord(%XYZ,0)*0.5) / (%mover.Steps);
		%Y = (getWord(%XYZ,1)*0.5) / (%mover.Steps);
		%Z = ((3*getWord(%XYZ,2))*0.2) / (%mover.Steps);
		}
		if(%mover.onmovenum $= 4){
		%XYZ = %mover.MoveXYZ4;
		%X = (getWord(%XYZ,0)*0.5) / (%mover.Steps);
		%Y = (getWord(%XYZ,1)*0.5) / (%mover.Steps);
		%Z = ((3*getWord(%XYZ,2))*0.2) / (%mover.Steps);
		}

		//Calculate Next XYZ Position.
		%CurTrans = %mover.getTransform();
		%CurX = getword(%CurTrans,0);
		%CurY = getword(%CurTrans,1);
		%CurZ = getword(%CurTrans,2);

		//Are we Reversing?
		if(%mover.hasReturned !$= 0)
		{
		%NewX = %CurX - %X;
		%NewY = %curY - %Y;
		%NewZ = %curZ + %Z;
		}
		else
		{
		%NewX = %CurX + %X;
		%NewY = %CurY + %Y;
		%NewZ = %CurZ - %Z;
		}

		//Get Rot Move Factor.
		%Rot = %mover.RotateXYZ;
		%RotX = getWord(%Rot,0) / (%mover.Steps);
		%RotY = getWord(%Rot,1) / (%mover.Steps);
		%RotZ = getWord(%Rot,2) / (%mover.Steps);

		//Calculate Next Rot Position.
		%CurRotX = getWord(%mover.EulerRot,0);		
		%CurRotY = getWord(%mover.EulerRot,1);		
		%CurRotZ = getWord(%mover.EulerRot,2);

		//Are we Reversing?
		if(%mover.hasReturned !$= 0)
		{
		%NewRotX = %RotX + %CurRotX;
		%NewRotY = %RotY + %CurRotY;
		%NewRotZ = %RotZ + %CurRotZ;
		}
		else
		{
		%NewRotX = %CurRotX - %RotX;
		%NewRotY = %CurRotY - %RotY;
		%NewRotZ = %CurRotZ - %RotZ;
		}
 
		//Carry out Movement Sequence.
		%mover.EulerRot = %NewRotX SPC %NewRotY SPC %NewRotZ;
		%newTrans = %newX SPC %newY SPC %newZ SPC eulerToQuat(%NewRotX SPC %NewRotY SPC %NewRotZ);
		%mover.setTransform(%newTrans);

		//Is this object an Elevator?
		if(%mover.Elevator)
		{
			for(%t = 0; %t < ClientGroup.getCount(); %t++)
			{
				%cl = ClientGroup.getObject(%t);
				
				//Is this guy already on an elevator?
				//if(%cl.isElevating !$= "1")
				//{
					InitContainerRadiusSearch(%cl.player.getTransform(), 0.2, $TypeMasks::ShapeBaseObjectType);

					//No, Lets put him on this one.
					while ((%targetObject = containerSearchNext()) != 0) 
					{
						if(%targetObject $= %mover)
						{
							%targetObject.Elevated[%cl] = 1;
							%cl.currentElevatorObj = %targetObject;
							%cl.isElevating = 1;
						}
					}
				//}
				
				//Is this guy ready and waiting on our lift?
				if(%mover.Elevated[%cl] && %mover $= %cl.currentElevatorObj)
				{
					//Yes, lets get his Position.
					%playerTrans = %cl.Player.getTransform();
					%playerX = getWord(%playerTrans,0);
					%playerY = getWord(%playerTrans,1);
					%playerZ = getWord(%playerTrans,2);

					//Has our player fallen off or walked off?
					InitContainerRadiusSearch(%playerX @ " " @ %playerY @ " " @%playerZ+1.5, 0.2, $TypeMasks::ShapeBaseObjectType);
					while ((%targetObject = containerSearchNext()) != 0) 
					{
						if(%targetObject != %cl.player && %targetObject != %cl.currentElevatorObj && %targetObject != %cl.currentElevatorObj.down[0])
						{
							//Yes. Lets stop moving him with us.
							%colData = %obj.getDataBlock();
							%colDataClass = %colData.classname;
							//echo(%targetObject.owner);

							if(%colDataClass !$= "armour" && %colDataClass !$= "baseplate")
							{	
									%cl.isElevating = 0;
									%obj.Elevated[%cl] = 0;
							}
						}

					}


					InitContainerRadiusSearch(%playerTrans, 0.2, $TypeMasks::ShapeBaseObjectType);

					while ((%targetObject = containerSearchNext()) != 0) 
					{
						if(%targetObject $= %cl.currentElevatorObj)
						{
							%onTheLift = 1;
						}
						if(%targetObject != %cl.player && %targetObject != %cl.currentElevatorObj && %targetObject != %cl.currentElevatorObj.down[0])
						{
							if(!%col.Elevator && %colDataClass !$= "player" && %colDataClass !$= "baseplate")
							{
									%cl.isElevating = 0;
									%obj.Elevated[%cl] = 0;
							}	
						}
					}

					//Passed all safety checks. Lets move him up with our lift.
					if(%cl.isElevating)
					{
							if(%onTheLift)
							{
								if(%mover.hasReturned $= 0)
								{
								%playerX = %playerX + %x;	
								%playerY = %playerY + %y;
								%playerZ = %playerZ - %z;
								}
								else
								{
								%playerX = %playerX - %x;	
								%playerY = %playerY - %y;
								%playerZ = %playerZ + %z;
								}
							}
							else
							{
								%obj.Elevated[%cl] = 0;
								%cl.isElevating = 0;
							}	
						
               						%cl.Player.setTransform(%playerX SPC %playerY SPC %playerZ);
					}
				}
			}

		}

		//Special Movement Properties?
		if(%mover.TriggerCloak $= 1 && %mover.isMoving !$= 1 || %mover.TriggerCloak $= 1 && %mover.isMoving $= 1 && %mover.hasReturned $= 0 && %mover.awaitingReturn $= 1)
		{
			if(%mover.isWCloaked !$= 1)
			{
    				%mover.setcloaked(1);
				%mover.isWCloaked = 1;
			}
			else
			{
				%mover.setcloaked(0);
				%mover.isWCloaked = 0;
			}
		}
		%mover.isMoving = 1;
		//Do we need to move it again?
		if((%mover.stepsDone < %mover.Steps && %mover.movenums == 0) || (%mover.stepsDone < %mover.Steps * 2 && %mover.movenums == 2) || (%mover.stepsDone < %mover.Steps * 3 && %mover.movenums == 3) || (%mover.stepsDone < %mover.Steps * 4 && %mover.movenums == 4))
		{
			%mover.stepsDone++;
			if(%mover.movenums > 1)
			{
			if(%mover.hasReturned == 1 && %mover.stepsDone == %mover.Steps && %mover.movenums >= 2){
			%mover.onmoveNum++;}
			if(%mover.hasReturned == 1 && %mover.stepsDone == 2*%mover.Steps && %mover.movenums >= 3){
			%mover.onmoveNum++;}
			if(%mover.hasReturned == 1 && %mover.stepsDone == 3*%mover.Steps && %mover.movenums >= 4){
			%mover.onmoveNum++;}
			if(%mover.hasReturned == 0 && %mover.StepsDone == %mover.Steps && %mover.movenums >= 2){
			%mover.onmoveNum--;}
			if(%mover.hasReturned == 0 && %mover.StepsDone == 2*%mover.Steps && %mover.movenums >= 3){
			%mover.onmoveNum--;}
			if(%mover.hasReturned == 0 && %mover.StepsDone == 3*%mover.Steps && %mover.movenums >= 4){
			%mover.onmoveNum--;}
			}
			schedule(%mover.StepTime,0,"TimeStep",%client,%mover);
			if(%mover.awaitingReturn $= 1)
			   %mover.awaitingReturn = 0;
		}
		else
		{
			%mover.stepsDone = "";
			
			//Have we Returned?
			if(%mover.hasReturned !$= 0)
 			   %mover.hasReturned = 0;
			else
			   %mover.hasReturned = 1;

			//Lets send it back after a few Seconds, if its not Return Toggled.
			if(%mover.ReturnToggle !$= 1 && %mover.hasReturned $= 0)
			{
			//if(%mover.ReturnDelay < 0){
				//%mover.setTransform(%mover.origpos);
				//%mover.EulerRot = %mover.origrot;
				//%mover.isMoving = 0;
			//}
				//%ReturnTime = getword(%mover.ReturnDelay,0);
				schedule(%mover.Returndelay,0,"InitTimeStepObj",%client,%mover);
				%mover.awaitingReturn = 1;
			}
			else
			{
			if(%mover.ReturnToggle !$= 1 || %mover.hasReturned $= 1){
			%mover.setTransform(%mover.origpos);
			//%mover.EulerRot = %mover.origrot;
			}
			if(getword(%mover.ReturnDelay,1) $= "#INF"){
				%mover.isMoving = 0;
				%ReturnTime = getword(%mover.ReturnDelay,0);
				schedule(%mover.Returndelay,0,"ExecuteMoverInstructions",%client, %mover);}
			else
				%mover.isMoving = 0;
			}
		}
	}
}


//######################################################################################################################################
//# Applying Door Commands - SET A
//######################################################################################################################################
function serverCmdApplyDoorSettings(%client,%MoveXYZ,%RotateXYZ,%Steps,%StepTime,%Elevator,%TriggerCloak,%Private,%Team,%Group,%isImpTrigger,%returnDelay,%returnToggle,%Password,%DoorType,%DSID,%NoCollision,%KeyProtected)
{
	%Steps = strreplace(%Steps,"-",0);
	%StepTime = strreplace(%StepTime,"-",0);
	%client.WrenchMoveXYZ = getword(%MoveXYZ,0) SPC getword(%MoveXYZ,1) SPC getword(%MoveXYZ,2);
	%client.WrenchMoveXYZ2 = "";
	%client.WrenchMoveXYZ3 = "";
	%client.WrenchMoveXYZ4 = "";
	%client.WrenchMoveNums = "";
	if(getword(%MoveXYZ,3) !$= "" && getword(%MoveXYZ,4) !$= "" && getword(%MoveXYZ,5) !$= ""){
		%client.WrenchMoveXYZ2 = getword(%MoveXYZ,3) SPC getword(%MoveXYZ,4) SPC getword(%MoveXYZ,5);
		%client.WrenchMoveNums = 2;}
	if(getword(%MoveXYZ,6) !$= "" && getword(%MoveXYZ,7) !$= "" && getword(%MoveXYZ,8) !$= ""){
		%client.WrenchMoveXYZ3 = getword(%MoveXYZ,6) SPC getword(%MoveXYZ,7) SPC getword(%MoveXYZ,8);
		%client.WrenchMoveNums = 3;}
	if(getword(%MoveXYZ,9) !$= "" && getword(%MoveXYZ,10) !$= "" && getword(%MoveXYZ,11) !$= ""){
		%client.WrenchMoveXYZ4 = getword(%MoveXYZ,9) SPC getword(%MoveXYZ,10) SPC getword(%MoveXYZ,11);
		%client.WrenchMoveNums = 4;}
	%client.WrenchRotateXYZ = %RotateXYZ;
	%client.WrenchSteps = %Steps;
	%client.WrenchStepTime = %StepTime;
	%client.WrenchElevator = %elevator;
	%client.WrenchTriggerCloak = %triggercloak;
	%client.WrenchPrivate = %Private;
	%client.WrenchTeam = %team;
	if(getword(%password,0) $= "team"){
	for(%i = 0; %i<=$Pref::Server::TotalTeams; %i++)
	{
		if($Teams[%i] $= %Password){
			%client.WrenchTeam = strreplace(%password,"team ","");
			%match++;
		}
	}
	if(%match == 0)
		messageClient(%client,"","\c4Unable to find that team!");
		%password = "";
	}
	%client.WrenchGroup = %group;
	if(getword(%password,0) $= "group"){
	if((getword(%password,1) $= "Friend") || (getword(%password,1) $= "Safe") || (getword(%password,1) $= "Admin") || (getword(%password,1) $= "SuperAdmin"))
		%client.WrenchGroup = getword(%password,1);
	else
		centerprint(%client,"That is not a valid group.\nValid selections are Friend, Safe, Admin, and SuperAdmin.",5,2);
		%password = "";
	}
	%client.WrenchImpTrigger = %isImpTrigger;
	%client.WrenchReturnDelay = %returnDelay;
	%client.WrenchReturnToggle = %returnToggle;
	if(getword(%password,0) $= "money"){
		if(getword(%password,1) $= "greater"){
			%client.WrenchPassword = getword(%password,2);
			%client.WrenchSpecialCase = "mgt";
			%password = "";
		}
		if(getword(%password,1) $= "less"){
			%client.WrenchSpecialCase = "mlt";
			%client.WrenchPassword = getword(%password,2);
			%password = "";
		}
	}
	if(getword(%password,0) $= "score"){
		if(getword(%password,1) $= "greater"){
			%client.WrenchPassword = getword(%password,2);
			%client.WrenchSpecialCase = "sgt";
			%password = "";
		}
		if(getword(%password,1) $= "less"){
			%client.WrenchSpecialCase = "slt";
			%client.WrenchPassword = getword(%password,2);
			%password = "";
		}
	}
	%client.WrenchPassword = %password;
	%client.WrenchDoorType = %DoorType;
	%client.WrenchNoCollision = %nocollision;
	%client.WrenchDoorSetID = %DSID;
	%client.WrenchKeyProtected = %KeyProtected;

	setBricktoMover(%client);
}

//######################################################################################################################################
//# Check if door has PW
//######################################################################################################################################
function serverCmdCheckDoorPassword(%client,%password)
{
	%col = %client.PWDoor;
	if(%client.isMakingLinkByID){
		%client.RequestedLink.LinkedByID = 1;
		%client.RequestedLink.LinkNum = %password;
		%client.isMakingLinkByID = 0;
		centerprint(%client,"ID set to " @ %password @ ".",5,1);
		return;
	}
	if(%client.isRequestingTeletoID){
		for(%t = 0; %t < MissionCleanup.getCount(); %t++){
		%teleObj = MissionCleanup.getObject(%t);
		if(%teleObj.LinkedByID == 1 && %teleObj.LinkNum == %password){
			Schedule(200,0,"ServerCmdTeleportToObj",%client,%teleObj,500);
			%client.isRequestingTeletoID = 0;
			centerprint(%client,"Teleporting to ID " @ %password @ "...",5,1);
			return;
		}}
		messageClient(%client,"",'\c4Invalid ID!',%password);
		return;
	}
	if(%col.password $= %password || (%client.isSuperAdmin && $Pref::Server::CopsAndRobbers !$= 1))
	{
		centerprint(%client,"Password Accepted!",5,1);
		executemoverinstructions(%client, %col);
	}
	else
	{
		centerprint(%client,"Password Rejected!",5,1);
	}
}


//######################################################################################################################################
//# Inspect Mover Instructions + Check Links
//######################################################################################################################################
function ExecuteMoverInstructions(%client, %col)
{
	if(%col.ReturnToggle !$= 1 || %col.hasReturned !$= 0){
	%col.origpos = %col.gettransform();
	//%col.origpos = posfromtransform(%origpos2);
	%col.origrot = %col.EulerRot;}
	if(%col.Elevator $= 1)
	{
		InitContainerRadiusSearch(%col.getWorldBoxCenter(), 0.3, $TypeMasks::ShapeBaseObjectType);
		while ((%targetObject = containerSearchNext()) != 0) 
		{
			if(%targetObject !$= %col)
			{
				%col.Elevated[%targetObject.client] = 1;
				%targetObject.client.currentElevatorObj = %col;
				%targetObject.client.isElevating = 1;
			}
		}
	}

	if(%col.DoorType $= 1)
	{
		InitTimeStepObj(%client,%col);
		for(%t = 0; %t < MissionCleanup.getCount(); %t++)
		{
			%moveObj = MissionCleanup.getObject(%t);
			if(%moveObj.owner $= %col.owner && %moveObj.isDoor $= 1 && %moveObj.TriggerDoorID $= %col.TriggerDoorID && %moveObj != %col && %moveObj.isMoving != 1 && %moveObj.DoorType $= 2 || %moveObj.owner $= %col.owner && getWord(%moveObj.TriggerDoorID, 2) $= %col.TriggerDoorID && %col.isImpulseTrigger $= 1 && %moveObj.isTriggerImp $= 1)
			{
				if(%moveObj.isTriggerImp && %col.isImpulseTrigger $= 1)
				{
					checkColImpulse(%moveObj);
				}
				InitTimeStepObj(%client,%moveObj);
				%totalStarts++;
			}
		}
	}
	else
	{
		InitTimeStepObj(%client,%col);
	}
}

//######################################################################################################################################
//#End Mover CMDs... And on with Bleh's Updated CMDs!
//######################################################################################################################################
//Updated Wrench.cs Files.
function serverCmdsetBricktoNorm(%client)
{
	%col = %client.WrenchObject;

	%col.isDoor = "";
	%col.MoveXYZ = "";
	%col.MoveXYZ2 = "";
	%col.MoveXYZ3 = "";
	%col.MoveXYZ4 = "";
	%col.MoveNums = "";
	%col.onmovenum = 1;
	%col.Steps = "";
	%col.StepTime = "";
	%col.RotateXYZ = "";
	%col.ReturnDelay = "";
	%col.ReturnToggle = "";
	%col.Elevator = "";
	%col.Private = "";
	%col.noCollision = "";
	%col.isMoving = "";
	%col.TriggerDoorID = "";
	%col.HasReturned = "";
	%col.Password = "";
	%col.Team = "";
	%col.Group = "";
	%col.DoorType = "";
	%col.TriggerCloak = "";
	%col.Elevator = "";
	%col.isImpulseTrigger = "";
	%col.KeyProtected = "";
	messageClient(%client,"","\c4You Removed the Brick\'s Properties.");
}
function serverCmdsetBricktoNorm2(%client)
{
	%col = %client.WrenchObject;

	%col.isDoor = "";
	%col.MoveXYZ = "";
	%col.MoveXYZ2 = "";
	%col.MoveXYZ3 = "";
	%col.MoveXYZ4 = "";
	%col.MoveNums = "";
	%col.origpos = "";
	%col.origrot = "";
	%col.onmovenum = 1;
	%col.Steps = "";
	%col.StepTime = "";
	%col.RotateXYZ = "";
	%col.ReturnDelay = "";
	%col.ReturnToggle = "";
	%col.Elevator = "";
	%col.Private = "";
	%col.noCollision = "";
	%col.isMoving = "";
	%col.TriggerDoorID = "";
	%col.HasReturned = "";
	%col.Password = "";
	%col.Team = "";
	%col.Group = "";
	%col.DoorType = "";
	%col.TriggerCloak = "";
	%col.Elevator = "";
	%col.isImpulseTrigger = "";
	%col.KeyProtected = "";
}
function serverCmdgetWrenchSettings(%client)
{
	%col = %client.WrenchObject;

	if(%col.isDoor $= 1)
	   %yesno = 1;
	else
	   %yesno = 0;

	if(%col.MoveXYZ2 $= "")
	messageClient(%client,'MsgUpdateWrenchA',"",%col.MoveXYZ,%col.RotateXYZ,%col.Steps,%col.StepTime,%col.Elevator,%col.TriggerCloak,%col.Private,%col.Team,%col.Group,%yesno);
	else if(%col.MoveXYZ3 $= "")
	messageClient(%client,'MsgUpdateWrenchA',"",%col.MoveXYZ SPC %col.MoveXYZ2,%col.RotateXYZ,%col.Steps,%col.StepTime,%col.Elevator,%col.TriggerCloak,%col.Private,%col.Team,%col.Group,%yesno);
	else if(%col.MoveXYZ4 $= "")
	messageClient(%client,'MsgUpdateWrenchA',"",%col.MoveXYZ SPC %col.MoveXYZ2 SPC %col.MoveXYZ3,%col.RotateXYZ,%col.Steps,%col.StepTime,%col.Elevator,%col.TriggerCloak,%col.Private,%col.Team,%col.Group,%yesno);
	else
	messageClient(%client,'MsgUpdateWrenchA',"",%col.MoveXYZ SPC %col.MoveXYZ2 SPC %col.MoveXYZ3 SPC %col.MoveXYZ4,%col.RotateXYZ,%col.Steps,%col.StepTime,%col.Elevator,%col.TriggerCloak,%col.Private,%col.Team,%col.Group,%yesno);
	messageClient(%client,'MsgUpdateWrenchB',"",%col.isImpulseTrigger,%col.returnDelay,%col.returnToggle,%col.password,%col.DoorType,%col.TriggerDoorID,%col.NoCollision,%yesno,%col.KeyProtected);
}
function setBricktoMover(%client)
{
	%col = %client.WrenchObject;
								
	if(%client.TotalMovers < $Pref::Server::MaxDoors)
	{
		if(%col.isDoor != 1)
		{
			servercmdsetBricktoNorm2(%client);
			messageClient(%client,"","\c4You Gave the Brick Movement Properties.");
		}
		else
		{	
			servercmdsetBricktoNorm2(%client);
			messageClient(%client,"","\c4You Replaced the Brick Properties.");
		}

		%col.isDoor = 1;
		%col.MoveXYZ = %client.WrenchMoveXYZ;
		%col.MoveXYZ2 = %client.WrenchMoveXYZ2;
		%col.MoveXYZ3 = %client.WrenchMoveXYZ3;
		%col.MoveXYZ4 = %client.WrenchMoveXYZ4;
		%col.MoveNums = %client.WrenchMoveNums;
		%col.onmovenum = 1;
		%col.RotateXYZ = %client.WrenchRotateXYZ;
		%col.Steps = %client.WrenchSteps;
		%col.StepTime = %client.WrenchStepTime;
		%col.ReturnDelay = %client.WrenchReturnDelay;
		%col.Elevator = %client.WrenchElevator;
		%col.noCollision = %client.WrenchNoCollision;
		%col.HasReturned = 1;

		if(%client.WrenchDoorType $= "Trigger")
		{
			%col.DoorType = 1;
			%col.TriggerDoorID = %client.WrenchDoorSetID;
		}
		else if(%client.WrenchDoorType $= "Triggered")
		{
			%col.DoorType = 2;
			%col.TriggerDoorID = %client.WrenchDoorSetID;
		}
		else if(%client.WrenchDoorType $= "Normal")
		{
			%col.DoorType = 3;
		}

		if(%client.WrenchReturnToggle $= 1)
		{
			%col.ReturnToggle = 1;
		}
		else
		{
			%col.ReturnToggle = 0;
		}

		if(%client.WrenchImpTrigger $= 1)
		{
			%col.TriggerDoorID = %client.WrenchDoorSetID;
			%col.isImpulseTrigger = 1;
		}

		if(%client.WrenchTriggerCloak)
		{
			%col.TriggerCloak = %client.WrenchTriggerCloak;
		}

		if(%client.WrenchPrivate !$= "")
		{
			%col.Private = %client.WrenchPrivate;
		}

		if(%client.WrenchPassword !$= "")
		{
			%col.Password = %client.WrenchPassword;
			if(%client.WrenchSpecialCase !$= "")
				%col.SpecialCase = %client.WrenchSpecialCase;
		}

		if(%client.WrenchTeam !$= "")
		{
			%col.Team = %client.WrenchTeam;
		}

		if(%client.WrenchGroup !$= "")
		{
			%col.Group = %client.WrenchGroup;
		}

		if(%client.WrenchKeyProtected $= 1)
		{
			%col.KeyProtected = 1;
		}
		else
		{
			%col.KeyProtected = 0;
		}

		$Movers[$TotalMoversPlaced++] = %col;
		%client.TotalMovers++;
		commandtoclient(%client,'CloseMoverGUI');
	}
	else
	{
		if($Pref::Server::MaxDoors $= 0)
		{
			commandtoclient(%client,'CloseMoverGUI');
			messageClient(%client,"","\c4Movers are not allowed on this Server!");
		}
		else
		{
			commandtoclient(%client,'CloseMoverGUI');
			messageClient(%client,"",'\c4You have reached the mover limit of \c0%1',$Pref::Server::MaxDoors);
		}
	}
}

//ServerCmd.cs updates.  Its really long...



function ServerCmdPlantBrick(%client)
{
	//echo(%client, " is planting brick");

	if(%client.player.tempBrick.isMoverGhost)
	{
		%brick = %client.player.tempBrick;

		%SrtPos = %brick.MStartPos;
		%EndPos = %brick.getTransform();
		%SrtX = getWord(%SrtPos,0);
		%SrtY = getWord(%SrtPos,1);
		%SrtZ = getWord(%SrtPos,2);

		%EndX = getWord(%EndPos,0);
		%EndY = getWord(%EndPos,1);
		%EndZ = getWord(%EndPos,2);

		if(%SrtX > %EndX)
		   %XDiff = %SrtX - %EndX;
		else
		   %XDiff = %EndX - %SrtX;

		if(%SrtY > %EndY)
		   %YDiff = %SrtY - %EndY;
		else
		   %YDiff = %EndY - %SrtY;

		if(%SrtZ > %EndZ)
		   %ZDiff = %SrtZ - %EndZ;
		else
		   %ZDiff = %EndZ - %SrtZ;
	}

	%time = $Sim::Time - %client.LastBrickTime; 
	if($Pref::Server::BlockScripts && %time < 0.1)
	{
		%client.LastBrickTime = $Sim::Time;
		return;
	}

	if($Pref::Server::BlockScripts && (!%client.isAdmin && !%client.isSuperAdmin))
	{
		if(%client.BrickStamina == 0)
		{
			//%client.wantclearownbricks = 1;
			//serverCmdClearOwnBricks(%client);
			//%client.delete("You have been kicked for brick spamming.  Don't worry, your mess has been cleared for you.");
			messageAll('',"\c3Warning:  " @ %client.namebase @ " (" @ getRawIP(%client) @ ") may be brick spamming.");
		}
		else if(%client.BrickStamina == 5)
		{
			messageAllExcept(%client,'',"\c3Warning:  " @ %client.namebase @ " (" @ getRawIP(%client) @ ") might be trying to spam the server.  They have continued trying to build even after having their building rights suspended.");
		}
		else if(%client.BrickStamina == 10)
		{
			messageAllExcept(%client,'',"\c3Warning:  " @ %client.namebase @ " is building too quickly and has had their building rights suspended.");
			messageClient(%client,'',"\c3Warning:  Your building rights have been suspended because you were building too quickly.");
			%client.HBR = 0;
		}
		else if(%client.BrickStamina == 20)
		{
			messageClient(%client,'',"\c3Warning:  You are running low on stamina.  You need to stop building so quickly.");
		}

		%client.BrickStamina--;
	}

	if(%client.HBR $= 0 && %client.player.tempBrick.isMoverGhost !$= 1)
	{
		messageClient(%client,'',"\c3You do not have Building Rights!");
		return;
	}
	if(%client.isImprisoned)
	{
		messageClient(%client,'',"\c5No building you imprisoned scum!");
		return;
	}
	if(%client.plantingPrice $= "")
		%client.plantingPrice = 1;		

        if($Pref::Server::TogglePlantingCosts $= 1 && %client.money < %client.plantingPrice && %client.player.tempBrick.isMoverGhost !$= 1 && (!%client.isAdmin || !%client.isSuperAdmin))
	{
		messageClient(%client,'',"\c4You cannot afford to plant this Brick!");
		return;
	}
	%player = %client.player;
	%tempBrick = %player.tempBrick;
	%player.tempBrick.setshapename("");

	//## Delete Temp Brick

	if(%tempBrick.isBrickGhostMoving $= 1)
	{
		return;
	}
	if(%tempBrick)
	{
		if(%tempBrick.ismounted())
		{
			%solid = %tempBrick.getDataBlock();
			//create the new brick//
			%newBrick = new StaticShape()
			{
				datablock = %solid;
			};
			MissionCleanup.add(%newBrick);
			%player.brickcar.mountobject(%newbrick,%player.carmounts);
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
		%newBrick.setShapeName("");
		%eulerRot = %tempBrick.EulerRot;
		


		//check for stuff in the way
		%mask = $TypeMasks::StaticShapeObjectType;

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
						if(%checkObj.Owner.FriendList[%safe] == %cl && %cl == %client)
						{
							%isTrusted = 1;
						}
					}

				}

				if(%checkObj.NoBuild == 1 && %isTrusted == 0 && %client.player.tempBrick.isMoverGhost !$= 1)
				{
					messageClient(%client,"","\c4Sorry, can't build here.");
					return;
				}

				if(%isTrusted == 0 && %checkObj.Owner.Secure == 1 && !%client.isAdmin && !%client.isSuperAdmin && %client.player.tempBrick.isMoverGhost !$= 1)
				{
					messageClient(%client,"",'\c4Sorry, cant build here. \c0%1\c4 has not made you his/her friend.',%checkObj.Owner.name);
					%newBrick.delete();
					return;
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
				%checkObjscale = %checkObj.getScale();
				%checkTop = round(%checkZpos + ((%checkData.Z * getWord(%checkObjscale,2)) * 0.2));

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

				if(%xOverlap && %yOverlap && %zOverlap && !$Pref::Server::BuildThrough && %client.player.tempBrick.isMoverGhost !$= 1){	
					//the brick is inside another brick, no need to keep checking
					//tell the player
					messageClient(%client, 'MsgError', '\c4You can\'t put a brick inside another brick.');
					%newBrick.delete();
					return;
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
				//we didnt find anything to attach to so error and bail
				messageClient(%client, 'MsgError', '\c4You can\'t put a brick in mid air.');
				%newBrick.delete();
				return;
			}
		}
		if($Pref::Server::BombRigging != 1 && %newBrick.Datablock $= StaticDynamite)
		{
			messageClient(%client, '', "\c4Bomb Rigging Disabled!");
			%newBrick.delete();
			return;
		}
		if($Pref::Server::BombRigging != 1 && %newBrick.Datablock $= StaticPlunger)
		{
			messageClient(%client, '', "\c4Bomb Rigging Disabled!");
			%newBrick.delete();
			return;
		}
		if(%newBrick.Datablock $= staticPlunger && $Pref::Server::BombRigging $= 1)
		{
			if(%client.plantedplunger $= 1)
			{
			messageClient(%client, '', "\c4You need to plant the Bomb Next!");
			%newBrick.delete();
			return;
			}
			if(%client.plantedbomb $= 1)
			{
			messageClient(%client, '', "\c4You already have a Bomb Placed!");
			%newBrick.delete();
			return;
			}
			if(%client.timerigged != 0)
			{
			messageClient(%client, '', "\c4You already have a Timed Bomb Placed!");
			%newBrick.delete();
			return;
			}
		}
		if(%newBrick.Datablock $= StaticDynamite && $Pref::Server::BombRigging $= 1)
		{	
			

			if(%client.plantedbomb $= 5)
			{
			messageClient(%client, '', "\c4You already have the Max Bombs Placed!");
			%newBrick.delete();
			return;
			}
			if(%client.timerigged != 0)
			{
			messageClient(%client, '', "\c4You already have a Timed Bomb Placed!");
			%newBrick.delete();
			return;
			}
		}
		if(%newBrick.Datablock $= StaticbrickFire && %client.fireBrickCount >= 2)
		{	
			if(%client.isAdmin != 1 || %client.isSuperAdmin != 1)
			{
			messageClient(%client, '', "\c4You can only have 2 Fire Bricks Placed!");		
			%newBrick.delete();
			return;
			}
		}
		if(%hanging == 1)
		{
			//we're hanging
			%newBrick.wasHung = true;
		}

		//success! 
		
		//put the brick where its supposed to be
		%newBrick.setTransform(%tempBrick.getTransform());
		if(%client.AutoColorMode == 1)
		{
			%newBrick.setSkinName($legoColor[%client.colorIndex]);		
			%newBrick.SkinName = %newBrick.getSkinName();
		}
		if(%client.AutoColorMode == 2)
		{
			%rnd = getRandom($TotalColors);
			%newBrick.setSkinName($legoColor[%rnd]);
			%newBrick.SkinName = %newBrick.getSkinName();
		}
		
		if(%newBrick.Datablock $= Staticbrick2x2FX)
		{
			if(%client.FXCount >= $Pref::Server::MaxFXBricks)
			{
				messageClient(%client,"",'\c4You can\'t make more than %1 FX bricks!', $Pref::Server::MaxFXBricks);
				return;
			}
			else
			{
				%client.FXBrickSelected = %newBrick;
				commandtoclient(%client,'Push',brickFX);
			}
		}
		if(%eulerRot $= "")
		{
		%eulerRot = "0 0 0";
		}
		if(%tempBrick.isBrickGhostN $= 1)
		{
			%newBrick.setSkinName(%tempBrick.SkinNameS);
		}


		if(%tempBrick.isMoverGhost $= 1)
		{
		if(%tempBrick.FirstMoveDone != 1){
			%SrtPos = %client.WrenchObject.getTransform();
			%EndPos = %newbrick.getTransform();
			%tempbrick.FirstMovePos = %EndPos;
			%tempbrick.FirstMoveDone = 1;

			%SrtX = getWord(%SrtPos,0);
			%SrtY = getWord(%SrtPos,1);
			%SrtZ = getWord(%SrtPos,2);

			%EndX = getWord(%EndPos,0);
			%EndY = getWord(%EndPos,1);
			%EndZ = getWord(%EndPos,2);

			%XDiff = %SrtX - %EndX;
			%YDiff = %SrtY - %EndY;
			%ZDiff = %EndZ - %SrtZ;

			%XDiff = %XDiff*2;
			%YDiff = %YDiff*2;
			%ZDiff = ((%ZDiff/3)/0.2);
		
			if(strstr(%ZDiff,".") !$= "-1")
			   %ZDiff = getSubStr(%ZDiff,0,strstr(%ZDiff,"."));

			%col = %client.WrenchObject;
			%col.isDoor = 1;
			%col.MoveXYZ = %XDiff SPC %YDiff SPC %ZDiff;
			%col.Steps = 100;
			%col.StepTime = 10;
			%col.RotateXYZ = "0 0 0";
			%col.ReturnDelay = 2000;
			%col.ReturnToggle = 0;
			%col.RotateXYZ = "0 0 0";
			%col.noCollision = 0;
			%col.isMoving = 0;
			%col.doorReturn = 1;
			%col.doorType = 3;
			centerprint(%client,"Mover Waypoint 1 set.\nPush \'numpad 0\' or \'f\' to end sequence.",5,2);

			%newBrick.delete();
			return;
		}
		if(%tempBrick.SecondMoveDone != 1){
			%SrtPos = %tempbrick.FirstMovePos;
			%EndPos = %newbrick.getTransform();
			%tempbrick.SecondMovePos = %EndPos;
			%tempbrick.SecondMoveDone = 1;

			%SrtX = getWord(%SrtPos,0);
			%SrtY = getWord(%SrtPos,1);
			%SrtZ = getWord(%SrtPos,2);

			%EndX = getWord(%EndPos,0);
			%EndY = getWord(%EndPos,1);
			%EndZ = getWord(%EndPos,2);

			%XDiff = %SrtX - %EndX;
			%YDiff = %SrtY - %EndY;
			%ZDiff = %EndZ - %SrtZ;

			%XDiff = %XDiff*2;
			%YDiff = %YDiff*2;
			%ZDiff = ((%ZDiff/3)/0.2);
		
			if(strstr(%ZDiff,".") !$= "-1")
			   %ZDiff = getSubStr(%ZDiff,0,strstr(%ZDiff,"."));

			%col = %client.WrenchObject;
			%col.isDoor = 1;
			%col.MoveXYZ2 = %XDiff SPC %YDiff SPC %ZDiff;
			%col.Steps = 100;
			%col.StepTime = 10;
			%col.RotateXYZ = "0 0 0";
			%col.ReturnDelay = 2000;
			%col.ReturnToggle = 0;
			%col.RotateXYZ = "0 0 0";
			%col.noCollision = 0;
			%col.isMoving = 0;
			%col.doorReturn = 1;
			%col.doorType = 3;
			centerprint(%client,"Mover Waypoint 2 set.\nPush \'numpad 0\' or \'f\' to end sequence.",5,2);

			%newBrick.delete();
			return;
		}
		if(%tempBrick.ThirdMoveDone != 1){
			%SrtPos = %tempbrick.SecondMovePos;
			%EndPos = %newbrick.getTransform();
			%tempbrick.ThirdMovePos = %EndPos;
			%tempbrick.ThirdMoveDone = 1;

			%SrtX = getWord(%SrtPos,0);
			%SrtY = getWord(%SrtPos,1);
			%SrtZ = getWord(%SrtPos,2);

			%EndX = getWord(%EndPos,0);
			%EndY = getWord(%EndPos,1);
			%EndZ = getWord(%EndPos,2);

			%XDiff = %SrtX - %EndX;
			%YDiff = %SrtY - %EndY;
			%ZDiff = %EndZ - %SrtZ;

			%XDiff = %XDiff*2;
			%YDiff = %YDiff*2;
			%ZDiff = ((%ZDiff/3)/0.2);
		
			if(strstr(%ZDiff,".") !$= "-1")
			   %ZDiff = getSubStr(%ZDiff,0,strstr(%ZDiff,"."));

			%col = %client.WrenchObject;
			%col.isDoor = 1;
			%col.MoveXYZ3 = %XDiff SPC %YDiff SPC %ZDiff;
			%col.Steps = 100;
			%col.StepTime = 10;
			%col.RotateXYZ = "0 0 0";
			%col.ReturnDelay = 2000;
			%col.ReturnToggle = 0;
			%col.RotateXYZ = "0 0 0";
			%col.noCollision = 0;
			%col.isMoving = 0;
			%col.doorReturn = 1;
			%col.doorType = 3;
			centerprint(%client,"Mover Waypoint 3 set.\nPush \'numpad 0\' or \'f\' to end sequence.",5,2);

			%newBrick.delete();
			return;
		}
		if(%tempBrick.FourthMoveDone != 1){
			%SrtPos = %tempbrick.ThirdMovePos;
			%EndPos = %newbrick.getTransform();
			%tempbrick.FourthMovePos = %EndPos;
			%tempbrick.FourthMoveDone = 1;

			%SrtX = getWord(%SrtPos,0);
			%SrtY = getWord(%SrtPos,1);
			%SrtZ = getWord(%SrtPos,2);

			%EndX = getWord(%EndPos,0);
			%EndY = getWord(%EndPos,1);
			%EndZ = getWord(%EndPos,2);

			%XDiff = %SrtX - %EndX;
			%YDiff = %SrtY - %EndY;
			%ZDiff = %EndZ - %SrtZ;

			%XDiff = %XDiff*2;
			%YDiff = %YDiff*2;
			%ZDiff = ((%ZDiff/3)/0.2);
		
			if(strstr(%ZDiff,".") !$= "-1")
			   %ZDiff = getSubStr(%ZDiff,0,strstr(%ZDiff,"."));

			%col = %client.WrenchObject;
			%col.isDoor = 1;
			%col.MoveXYZ4 = %XDiff SPC %YDiff SPC %ZDiff;
			%col.Steps = 100;
			%col.StepTime = 10;
			%col.RotateXYZ = "0 0 0";
			%col.ReturnDelay = 2000;
			%col.ReturnToggle = 0;
			%col.RotateXYZ = "0 0 0";
			%col.noCollision = 0;
			%col.isMoving = 0;
			%col.doorReturn = 1;
			%col.doorType = 3;
			commandtoclient(%client,'OpenMoverGui');

			%newBrick.delete();
			%Tempbrick.FirstMoveDone = 0;
			%Tempbrick.SecondMoveDone = 0;
			%Tempbrick.ThirdMoveDone = 0;
			%Tempbrick.FourthMoveDone = 0;
			%tempBrick = "";
			%TempBrick.delete();
			return;
		}
		}
		if(%client.plantingPrice $= "")
			%client.plantingPrice = 0;

		if($Pref::Server::TogglePlantingCosts $= 1 && (!%client.isAdmin || !%client.isSuperAdmin))
		{
			%client.money = %client.money - %client.plantingPrice;
			messageClient(%client,'MsgUpdateMoney','',%client.Money);
		}
		%newBrick.setScale(%tempBrick.getScale());
		%newBrick.EulerRot = %eulerRot;
		%newBrick.EulerRotation = %eulerRot;
		%newBrick.playAudio(0, brickPlantSound);
		%newBrick.Owner = %client;
		%newBrick.OwnerIP = getRawIP(%client);
		%newBrick.isRotated = %tempBrickisRotated;
		%client.LastBrickTime = $Sim::Time;
		%client.LastBrickPlaced = %newBrick;
		%client.Undo[0] = 0;
		%newBrick.ownername = %client.namebase;
		%client.Undo[1] = %newBrick;
		if(%newBrick.Datablock $= staticPlunger)
		{
			if(%client.plantedplunger != 1)
			{
				%client.plantedbomb = 0;
				%client.plantedplunger = 1;
				%newBrick.bombID = %client;
				%newBrick.NoBreak = 1;
				%newBrick.brickType = 1;
				%newBrick.NoDestroy = 1;
				%newBrick.riggerID = %client;	
				messageClient(%client,"","\c4You now need to plant some Dynamite.");

			}
			else
			{
				messageClient(%client,"","\c4You have already planted the Plunger. Plant your Bomb!");
			}
		
		}


		if(%newBrick.Datablock $= StaticDynamite)
		{
			if(%client.plantedplunger $= 1)
			{
				%client.plantedplunger = 1;
				%client.plantedbomb++;
				%newBrick.bombID = %client;
				%newBrick.NoBreak = 1;
				%newBrick.brickType = 2;
				%newBrick.NoDestroy = 1;
				%newBrick.riggerID = %client;
				if(%client.plantedbomb $= 1)
				{
		
					messageAll('name', '\c0%1\c5 has Rigged a Bomb!', %client.name);
					messageClient(%client,"","\c5You have Rigged a Bomb. Jump on the Detonator to Blow It, Or Plant More Bombs.");
				}
			}
			else
			{
				%client.timerigged = 1;
				%client.timedid++;
				%newBrick.cloaked = 1;
				%newBrick.bombID = %client.timedid;
				%newBrick.NoBreak = 1;
				%newBrick.NoDestroy = 1;
				%newBrick.riggerID = %client;
				messageClient(%client,"","\c5You Rigged a Timed Bomb. Its Set to Detonate in \c010 Seconds\c5!");
				%client.bombschedule = Schedule(10000,0,"ServerCmdBlowTimedBomb",%client,%newBrick,10000);
				messageAll('name', '\c0%1\c5 has Rigged a Timed Bomb!', %client.name);
			}
		}

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



function ServerCmdCancelBrick(%client)
{
	%player = %client.player;
//	if(%player)
//	{
		if(%player.tempBrick)
		{
			if(%player.tempBrick.isBrickGhostMoving $= 1)
			{
				%player.tempBrick.setSkinName(%player.tempBrick.oldskinname);
				%player.tempBrick.isBrickGhost = "";
				%player.tempBrick.isBrickGhostMoving = "";
				%player.tempBrick = "";
			}
			if(%player.tempbrick.isMoverGhost $= 1){
			commandtoclient(%client,'OpenMoverGui');
			%Tempbrick = %player.tempbrick;
			%Tempbrick.FirstMoveDone = 0;
			%Tempbrick.SecondMoveDone = 0;
			%Tempbrick.ThirdMoveDone = 0;
			%Tempbrick.FourthMoveDone = 0;
			%tempBrick.delete();
			%tempBrick = "";
			}
			else
			{
				%player.tempBrick.delete();
				%player.tempBrick = "";
			}
		}
//	}
}
function serverCmdFreeHands(%client)
{
	%player = %client.player;
	if(isObject(%player.tempBrick))
	{
		%player.tempBrick.setShapeName("");
		if(%player.tempBrick.isBrickGhostMoving $= 1)
		{
			%obj.client.SelectedObject = "";
			messageClient(%obj.client,'',"\c4You are out of \c0Edit \c4Mode.");
			%player.tempBrick.setSkinName(%player.tempBrick.oldskinname);
			%player.tempBrick.isBrickGhost = "";
			%player.tempBrick.isBrickGhostMoving = "";
			%player.tempBrick = "";
		}
		if(%player.tempbrick.isMoverGhost $= 1){
			commandtoclient(%client,'OpenMoverGui');
			%Tempbrick = %player.tempbrick;
			%Tempbrick.FirstMoveDone = 0;
			%Tempbrick.SecondMoveDone = 0;
			%Tempbrick.ThirdMoveDone = 0;
			%Tempbrick.FourthMoveDone = 0;
			%tempBrick.delete();
			%tempBrick = "";
		}
		else
		{
			if(isObject(%player.tempbrick)){
			%player.tempBrick.delete();
			%player.tempBrick = "";
			%player.tempBrick.setShapeName("");
			}
		}

	}
	messageClient(%client, 'MsgHilightInv', '', -1);
	%player.UnmountImage($RightHandSlot);
	if(isObject(%player.tempbrick)){
		%player.tempBrick.delete();
		%player.tempBrick = "";
	}
}

//Testbrick function from player.cs

function testbrick(%client, %col)
{
		if(%col.isSwitch){
		if(%col.SwitchOn !$= 0){
			%col.SwitchLight.LightObj.hide(true);
			%col.SwitchOn = 0;
		}
		else{
			%col.SwitchLight.LightObj.hide(false);
			%col.SwitchOn = 1;
		}
		}
		
		if(%col.isDoor $= 1 && %col.isMoving !$= 1 && %col.noCollision !$= 1 && %col.isLocked != 1)
		{
			%isTrusted = checkSafe(%col,%client);

			if(%client.isSuperAdmin || %client.isAdmin)
			{
				executemoverinstructions(%client, %col);	
				return;
			}

			if(%col.Password !$= "" && (!%client.isSuperAdmin))
			{
				if(%col.SpecialCase $= ""){
				%client.PWDoor = %col;
				//schedule(2000,0,commandtoclient,%client,"OpenPWBox");
				commandtoclient(%client,'OpenPWBox');
				return;
				}
				if((%col.SpecialCase $= "mlt" && %client.money < %col.Password) || (%col.SpecialCase $= "mgt" && %client.money > %col.Password) || (%col.SpecialCase $= "slt" && %client.score < %col.Password) || (%col.SpecialCase $= "sgt" && %client.score > %col.Password))
					executemoverinstructions(%client, %col);
				else
					CenterPrint(%client,"You don\'t have sufficient privledges to open this door.",5,2);
			}

			if(%col.Password !$= "" && (%client.isSuperAdmin))
			{
				CenterPrint(%client,"This door\'s password is:\n\n" @ %col.Password @ ".",5,2);
				executemoverinstructions(%client, %col);
				return;
			}

			if(%col.owner $= %client && !$Pref::Server::CopsAndRobbers)
			{
				executemoverinstructions(%client, %col);	
				return;
			}

			if(%col.Private $= "" && %col.Team $= "" && %col.Group $= "") 
			{
				executemoverinstructions(%client, %col);	
				return;
			}
		
			if(%col.group !$= "")
			{
				if(%col.group $= "Friend" && %isTrusted)
				{
				executemoverinstructions(%client, %col);	
				return;
				}
				else if(%col.group $= "Safe" && %isTrusted)
				{
				executemoverinstructions(%client, %col);	
				return;
				}
				else if(%col.group $= "Admin" && %client.isTempAdmin || %col.group $= "Admin" && %client.isAdmin)
				{
				executemoverinstructions(%client, %col);	
				return;
				}
				else if(%col.group $= "SuperAdmin" && %client.isSuperAdmin)
				{
				executemoverinstructions(%client, %col);	
				return;
				}
				else
				{
				return;
				}
			}

			if(%col.Team !$= "" && (%client.team $= %col.team))
			{
				executemoverinstructions(%client, %col);	
				return;
			}

			if(%isTrusted && %col.Private $= 1 && %col.Team $= "" && %col.Group $= "" && (!%client.isSuperAdmin))
			{
				executemoverinstructions(%client, %col);	
				return;
			}

			if(%col.Elevator $= 1 && $Pref::Server::EnabledElevator != 1)
			{
				messageClient(%client,"","\c4Elevators are Disabled on this Server!");
				return;
			}

			

}
}