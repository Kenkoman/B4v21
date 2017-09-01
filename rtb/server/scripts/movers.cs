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
		//Get XYZ Move Factor.
		%XYZ = %mover.MoveXYZ;
		%X = (getWord(%XYZ,0)*0.5) / (%mover.Steps);
		%Y = (getWord(%XYZ,1)*0.5) / (%mover.Steps);
		%Z = ((3*getWord(%XYZ,2))*0.2) / (%mover.Steps);

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
		if(%mover.stepsDone < %mover.Steps)
		{
			%mover.stepsDone++;
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
				schedule(%mover.ReturnDelay,0,"InitTimeStepObj",%client,%mover);
				%mover.awaitingReturn = 1;
			}
			else
			{
				%mover.isMoving = 0;
			}	
		}
      }
}


//######################################################################################################################################
//# Applying Door Commands - SET A
//######################################################################################################################################
function serverCmdApplyDoorSettings(%client,%MoveXYZ,%RotateXYZ,%Steps,%StepTime,%Elevator,%TriggerCloak,%Private,%Team,%Group,%isImpTrigger,%returnDelay,%returnToggle,%Password,%DoorType,%DSID,%NoCollision)
{
	%client.WrenchMoveXYZ = %MoveXYZ;
	%client.WrenchRotateXYZ = %RotateXYZ;
	%client.WrenchSteps = %Steps;
	%client.WrenchStepTime = %StepTime;
	%client.WrenchElevator = %elevator;
	%client.WrenchTriggerCloak = %triggercloak;
	%client.WrenchPrivate = %Private;
	%client.WrenchTeam = %team;
	%client.WrenchGroup = %group;
	%client.WrenchImpTrigger = %isImpTrigger;
	%client.WrenchReturnDelay = %returnDelay;
	%client.WrenchReturnToggle = %returnToggle;
	%client.WrenchPassword = %password;
	%client.WrenchDoorType = %DoorType;
	%client.WrenchNoCollision = %nocollision;
	%client.WrenchDoorSetID = %DSID;

	setBricktoMover(%client);
}


//######################################################################################################################################
//# Check if door has PW
//######################################################################################################################################
function serverCmdCheckDoorPassword(%client,%password)
{
	%col = %client.PWDoor;

	if(%col.password $= %password || (%client.isSuperAdmin || %client.isAdmin && $Pref::Server::CopsAndRobbers !$= 1))
	{
		messageClient(%client,"",'\c4Password Accepted!');
		executemoverinstructions(%client, %col);
	}
	else
	{
		messageClient(%client,"",'\c4Password Rejected!');
	}
}


//######################################################################################################################################
//# Inspect Mover Instructions + Check Links
//######################################################################################################################################
function ExecuteMoverInstructions(%client, %col)
{
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
					return;
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
//#End Mover CMDs
//######################################################################################################################################