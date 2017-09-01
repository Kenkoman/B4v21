//####################################################
//# serverCmd.cs - All Commands to Server from Client?
//#
//#
//#
//####################################################
function serverCmdStoreCash(%client)
{
	$ClientCash[getRawIp(%client)] = %client.money;
	if($Pref::Server::SaveCashonExit $= 1)
	{
	export("$ClientCash*", "rtb/server/clientCash.cs", False);
	}
}

function serverCmdgetTeamList(%client)
{
	for(%t = 0; %t < $Pref::Server::TotalTeams; %t++)
	{
		%team = $Teams[%t];
		messageClient(%client,'MsgSendTeamList',"",%team,%t);

	}
}

function serverCmdgetPlayerTeamList(%client)
{
	for(%i = 0; %i < ClientGroup.getCount(); %i++)
	{
		%cl = ClientGroup.getObject(%i);

		if(%cl.team !$= "")
		{
			%Team = %cl.team;
		}
		else
		{
			%Team = "<None>";
		}
		messageClient(%client,'MsgSendPlayerTeamList',"",%cl.Namebase,%Team,%cl);
	}
}

function serverCmdgetPlayerList(%client)
{
	for(%i = 0; %i < ClientGroup.getCount(); %i++)
	{
		%cl = ClientGroup.getObject(%i);
		if(%cl !$= %client)
		{
			%Friend = getFriends(%client, %cl);
			%Safe = getSafe(%client, %cl);
			messageClient(%client,'MsgSendPlayerList',"",%cl.Namebase,%Friend,%Safe,%cl);
		}
	}
}

function serverCmdModifyShiftSize(%client, %Shift)
{
	if(%Shift $= 1 && %client.ShiftSize !$= 2)
	{
		if(%client.ShiftSize !$= 1)
		   %client.ShiftSize = 1;
		else
		   %client.ShiftSize = 0;
	}
	else if(%Shift $= 2 && %client.ShiftSize !$= 1)
	{
		if(%client.ShiftSize !$= 2)
		   %client.ShiftSize = 2;
		else
		   %client.ShiftSize = 0;
	}
}

function serverCmdDeleteBrickFX(%client)
{
	%brick = %client.FXBrickSelected;
	if(%brick.FXmode $= 1)
	{
		%brick.FXmode = 0;
		%client.FXCount--;
		%brick.flameEmitter.delete();
		%brick.smokeEmitter.delete();
	}
	else if(%brick.FXmode $= 2)
	{
		%brick.FXmode = 0;
		%client.FXCount--;
		%brick.flameEmitter.delete();
	}
	else if(%brick.FXmode $= 3)
	{
		%brick.FXmode = 0;
		%client.FXCount--;
	}
	else if(%brick.FXmode $= 4)
	{
		%brick.FXmode = 0;
		%client.FXCount--;
	}
	else if(%brick.FXmode $= 5)
	{
		%brick.FXmode = 0;
		%client.FXCount--;
		%brick.bubbleEmitter.delete();
	}
	else
	{
		messageClient(%client,'',"\c3This Brick has no FX Properties!");
		return;
	}
}

function serverCmdApplyBrickFX(%client,%FX)
{
	if(%client.FXCount <= ($Pref::Server::MaxFXBricks-1))
	{
		%brick = %client.FXBrickSelected;
		if(%brick.FXmode >= 1)
		{
			messageClient(%client,'',"\c3This Brick already has FX Properties!");
			return;
		}



		//Fire without Smoke
		if(%FX $= 1)
		{
			   %brick.FXmode = 1;
		 	   %client.FXCount++;
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
			   messageClient(%client,'',"\c0Fire\c4 Properties have been Applied to this Brick.");
			   commandtoclient(%client,'',"pop",brickFX);
		}
		//Smoke
		else if(%FX $= 2)
		{
			   %brick.FXmode = 2;
		 	   %client.FXCount++;
			   %curtrans = %brick.getTransform();
			   %curx = getword(%curtrans,0);
		           %cury = getword(%curtrans,1);
		           %curz = getword(%curtrans,2);
			   %newX = %curx + 0.5;
			   %newY = %cury + 0.5;
			   %newZ = %curz + 0.2;
			   %newTrans = %newX SPC %newY SPC %newZ;
			   %brick.smokeEmitter = new ParticleEmitterNode(brickFireNode) {
     			   position = %newTrans;
      			   rotation = "1 0 0 0";
      			   scale = "1 1 1";
      			   dataBlock = "SmokeParticleEmitterNode";
     		   	   emitter = "SmokeParticleEmitter";
      			   velocity = "1.0";
   			   };
			   messageClient(%client,'',"\c0Smoke\c4 Properties have been Applied to this Brick.");
			   commandtoclient(%client,'',"pop",brickFX);
		}
		//Light Pulsing
		else if(%FX $= 3)
		{
    			messageAll('name',"\c3Case 3.");
			return;
		}
		//Light Steady
		else if(%FX $= 4)
		{
      			messageAll('name',"\c3Case 4.");
			return;
		}
		//Bubbles
		else if(%FX $= 5)
		{
			   %brick.FXmode = 5;
		 	   %client.FXCount++;
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
     		   	   emitter = "bubbleParticleEmitter";
      			   velocity = "1.0";
   			   };
			   messageClient(%client,'',"\c0Bubble\c4 Properties have been Applied to this Brick.");
			   commandtoclient(%client,'',"pop",brickFX);
		}
		//We got an Error!
		else
		{
			return;
		}
	}
	else
	{
		messageClient(%client,'','\c3You can only have \c0%1\c3 Special FX Bricks!',$Pref::Server::MaxFXBricks);
	}
}

function eulerToQuat( %euler )
{
	%euler = VectorScale(%euler, $pi / 180);
	%matrix = MatrixCreateFromEuler(%euler);
	%xvec = getWord(%matrix, 3);
	%yvec = getWord(%matrix, 4);
	%zvec = getWord(%matrix, 5);
	%ang  = getWord(%matrix, 6);
	%ang = %ang * 180 / $pi;
	%rotationMatrix = %xvec @ " " @ %yvec @ " " @ %zvec @ " " @ %ang;
	return %rotationMatrix;
}

function servercmdmonkeyme(%client,%color)
{
   %client.player.mountimage(monkeyshowimage,2,false,%color); 
   %client.player.setcloaked(1);  
}
function servercmdcatme(%client,%color)
{
   %client.player.mountimage(catshowimage,2,false,%color); 
   %client.player.setcloaked(1);  
}
function botcatme(%player,%color)
{
   %player.mountimage(catshowimage,2,false,%color); 
   %player.setcloaked(1);  
}
//#########################################
//#commandtoserver('starttag','rounds');
//#########################################
function servercmdstarttag(%client,%rounds)
{
   if(%client.isAdmin || %client.isSuperAdmin)
   {
   messageClient(%client,'',"\c5Tag Game started, You're it!");
   messageAll('','\c5A game of tag has started! Get your keys out! %1 is IT!',%client.namebase);
   messageallExcept(%client, -1, '','\c5%1 is IT!', %client.name);
   $taggamestarted = true;
   %client.themanit = true;
   $tagrounds = %rounds;
   }
}

function serverCmdcreateTempBrick(%client)
{
	%player = %client.player;
	%NtempBrick = new StaticShape()
	{
		datablock = %client.player.tempBrick.getDataBlock();
	};
	MissionCleanup.add(%NtempBrick);

	%NtempBrick.EulerRot = %client.player.tempBrick.EulerRot;
	%NtempBrick.setTransform(%client.player.tempBrick.getTransform());
	%NtempBrick.setScale(%client.player.tempBrick.getScale());
	%NtempBrick.setSkinName('construction');
	%NtempBrick.isBrickGhost = 1;

	if(%player.tempBrick.isBrickGhostMoving $= 1)
	{
		%player.tempBrick.setSkinName(%player.tempBrick.oldskinname);
		%player.tempBrick.isBrickGhost = "";
		%player.tempBrick.isBrickGhostMoving = "";
		%player.tempBrick = "";
	}
	else
	{
		%player.tempBrick.delete();
		%player.tempBrick = "";
	}
	%client.player.tempBrick = %NtempBrick;


}

function serverCmdgetServerRules(%client)
{
	%rules = $Pref::Server::Rules;
	messageClient(%client,'setRules',"",%rules);
}

function servercmddoAction(%client)
{
 //Determine how far should the picking ray extend into the world?
   %selectRange = 3;

   // Only search for vehicles
   %searchMasks = $TypeMasks::ItemObjectType;
   %searchMasks2 = $TypeMasks::StaticObjectType;

   %pos = %client.player.getEyePoint();

   // Start with the shape&#180;s eye vector...
   %eye = %client.player.getEyeVector();
   %eye = vectorNormalize(%eye);
   %vec = vectorScale(%eye, %selectRange);

   %end = vectorAdd(%vec, %pos);

   %scanTarg = ContainerRayCast (%pos, %end, %searchMasks);
   %scanTarg2 = ContainerRayCast (%pos, %end, %searchMasks2);

   // a target in range was found so select it
   if (%scanTarg)
   {
	%targetObject = firstWord(%scanTarg);
	%client.player.pickup(%targetObject);
   }
   else if(%scanTarg2)
   {
        %targetObject = firstWord(%scanTarg2);
        testbrick(%client, %targetObject);
   }
   else
   {
	servercmdVehicleMount(%client);
   }

}

function serverCmdsit(%client)
{

	if($Pref::Server::CopsAndRobbers || %client.frozen $= 1)
	{
		return;
	}
	%client.lastsitpos = %client.player.getTransform();
	if(%client.sitting == 0)
	{
		%client.sitting = 1;
		%playpos = %client.player.getTransform();
		%posx = getword(%playpos,0);
		%posy = getword(%playpos,1);
		%posz = getword(%playpos,2);
		
		%posznew = %posz - 0.5;
		%oyefinalpos = %posx SPC %posy SPC %posznew;

		%client.sitobject = new staticshape() 
				{
						position = %oyefinalpos;
      					rotation = "1 0 0 0";
      					scale = "0.001 0.001 0.001";
					    dataBlock = "staticplate1x1";
 				   			};
		%client.sitobject.setcloaked(true);
		%client.sitobject.MountObject(%client.player,1);
		%client.player.playthread(0,fall);
	}	
	else
	{	
		%ababa = %client.player.gettransform();
		%x = getword(%ababa,0);
		%y = getword(%ababa,1);
		%z = getword(%ababa,2)+0.5; 
		%newz = %z + 66;
		%client.sitobject.unMountObject(%client.player);
		%client.player.settransform(%x SPC %y SPC %z);
		%client.sitting = 0;
		%client.sitobject.delete();
		%client.player.playthread(0,root);
	}
}

function serverCmdFreezeMe(%client)
{
	if($pref::server::copsandrobbers || %client.sitting)
	{
		return;
	}
	
	if(%client.frozen == 0)
	{
		%client.frozen = 1;
		%client.freezeObject = new StaticShape() 
				{
      					position = %client.player.getTransform();
      					rotation = "1 0 0 0";
      					scale = "0.001 0.001 0.001";
					dataBlock = "flowers";
 				   			};
		%client.freezeObject.setcloaked(true);
		%client.freezeObject.MountObject(%client.player,1);
	}	
	else
	{
		%client.frozen = 0;
		%client.freezeObject.unMountObject(%client.player);
		%client.freezeObject.delete();
	}
}

function serverCmdClearOwnBricks(%client)
{
	if(%client.wantclearownbricks $= 1)
	{

	for (%i = 0; %i < MissionCleanup.getCount(); %i++)
	{
		%brick = MissionCleanup.getObject(%i);
		if(%brick.Owner == %client)
		{
			%brick.dead = true;
			%brick.schedule(10, explode);
			if(%brick.Datablock $= "staticbrickFire")
			{
				%client.fireBrickCount--;
				%brick.flameEmitter.delete();
				%brick.smokeEmitter.delete();
			}
		} 
	}
	%client.WantClearOwnBricks = 0;
	%client.WaitingforMessage = 0;
	%client.TotalMovers = 0;
	
	}
	else
	{
		%client.WantClearOwnBricks = 0;
		messageClient(%client,'',"\c3If you wish to clear ALL your bricks, Type \c0Yes\c3 Now. If not, Type \c0No\c3.");
		%client.WaitingforMessage = 1;
		%client.schedule(5000, canceldestroyownbricks, %client);
	}

}

function canceldestroyownbricks(%client)
{
	%client.WantClearBricks = 0;
	%client.WaitingforMessage = 0;
}

function ServerCmdShiftBrick( %client, %x, %y, %z)
{
	if(%client.player.tempBrick.FXMode >= 1)
	   return;
	//z dimension is sent in PLATE HEIGHTS, not brick heights
	if(%client.ShiftSize $= 1)
	{
		serverCmdSMALLShiftBrick(%client, %x, %y, %z);
		return;
	}
	else if(%client.ShiftSize $= 2)
	{
		serverCmdBIGShiftBrick(%client, %x, %y, %z);
		return;
	}
	%player = %client.player;
	%tempBrick = %player.tempBrick;
	if(!isObject(%tempBrick))
	{
	return;
	}
	%tempbrick.setSkinName('construction');
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
}

function ServerCmdSMALLShiftBrick( %client, %x, %y, %z)
{
	//z dimension is sent in PLATE HEIGHTS, not brick heights

	%player = %client.player;
	%tempBrick = %player.tempBrick;
	if(!isObject(%tempBrick))
	{
	return;
	}
	%tempbrick.setSkinName('construction');
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
		%x *= 0.1;
		%y *= 0.1;
		%z *= 0.04;
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
}

function ServerCmdBIGShiftBrick( %client, %x, %y, %z)
{
	//z dimension is sent in PLATE HEIGHTS, not brick heights

	%player = %client.player;
	%tempBrick = %player.tempBrick;
	if(!isObject(%tempBrick))
	{
	return;
	}

	%tempbrick.setSkinName('construction');
	
	%carmounts = %player.carmounts;


	if(%x == 1)
	{
		%x = %player.tempBrick.Datablock.x;
		if(%x == 1)
		{
			%x = 3;
		}
	}
	if(%y == 1)
	{
		%y = %player.tempBrick.Datablock.y;
		if(%y == 1)
		{
			%y = 3;
		}
	}
	if(%z == 1)
	{
		%z = %player.tempBrick.Datablock.z;
		if(%z == 1)
		{
			%z = 9;
		}
	}
	
	if(%x == -1)
	{
		%x = -%player.tempBrick.Datablock.x;
		if(%x == -1)
		{
			%x = -3;
		}
	}
	if(%y == -1)
	{
		%y = -%player.tempBrick.Datablock.y;
		if(%y == -1)
		{
			%y = -3;
		}
	}
	if(%z == -1)
	{
		%z = -%player.tempBrick.Datablock.z;
		if(%z == -1)
		{
			%z = -9;
		}
	}
	
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
		%Trans = %tempBrick.getTransform();
		%X = getWord(%Trans, 0);
		%Y = getWord(%Trans, 1);
		%Z = getWord(%Trans, 2);
		%Pos = %X SPC %Y SPC %Z;
		messageClient(%client,'updateMBrick',"",%Pos,%obj.EulerRot);
	}
}


//moves something x,y,z absolute
function shift(%obj, %x, %y, %z)
{
	%trans = %obj.getTransform();

	%transX = getWord(%trans, 0);
	%transY = getWord(%trans, 1);
	%transZ = getWord(%trans, 2);
	%transQuat = getWords(%trans, 3, 6);

	%obj.setTransform(%transX + %x @ " " @ %transY + %y @ " " @ %transZ + %z @ " " @ %transQuat);
}

function ServerCmdRotateBrick( %client, %dir)
{
	//echo(%client, " is rotating brick dir = ", %dir);
	if(%client.player.tempBrick.FXMode >= 1 || %client.player.tempBrick.isMoverGhost $= 1)
	   return;
	%player = %client.player;
	%tempBrick = %player.tempBrick;
	if(!isObject(%tempBrick))
	{
	return;
	}
	if(%tempBrick.Datablock $= Staticbrick2x2FX)
	{
	return;
	}

	%tempbrick.setSkinName('construction');

	if(%tempBrick)
	{

		%tempBrick.currentRotSetting = %tempBrick.currentRotSetting - %dir;
		%Rot = %tempBrick.EulerRot;
		if(%Rot $= "")
		{
		%Rot = "0 0 0";
		}
		%RotX = getWord(%Rot, 0);
		%RotY = getWord(%Rot, 1);
		%RotZ = getWord(%Rot, 2);
		if(%tempBrick.currentRotSetting > 3)
		{
			%tempBrick.currentRotSetting = 0;
		}
		if(%tempBrick.currentRotSetting < 0)
		{
			%tempBrick.currentRotSetting = 3;
		}
		if(%tempBrick.currentRotSetting == 0)
		{
			%RotAngle = 0;
			%tempBrick.settransform(getwords(%tempBrick.gettransform(),0,2) SPC eulertoquat(%RotX SPC %RotY SPC %RotAngle));
			%tempBrick.EulerRot = %RotX SPC %RotY SPC %RotAngle;
		}
		if(%tempBrick.currentRotSetting == 1)
		{
			%RotAngle = 90;
			%tempBrick.settransform(getwords(%tempBrick.gettransform(),0,2) SPC eulertoquat(%RotX SPC %RotY SPC %RotAngle));
			%tempBrick.EulerRot = %RotX SPC %RotY SPC %RotAngle;
		}
		if(%tempBrick.currentRotSetting == 2)
		{
			%RotAngle = 180;
			%tempBrick.settransform(getwords(%tempBrick.gettransform(),0,2) SPC eulertoquat(%RotX SPC %RotY SPC %RotAngle));
			%tempBrick.EulerRot = %RotX SPC %RotY SPC %RotAngle;
		}
		if(%tempBrick.currentRotSetting == 3)
		{
			%RotAngle = 270;
			%tempBrick.settransform(getwords(%tempBrick.gettransform(),0,2) SPC eulertoquat(%RotX SPC %RotY SPC %RotAngle));
			%tempBrick.EulerRot = %RotX SPC %RotY SPC %RotAngle;
		}
		%Trans = %tempBrick.getTransform();
		%X = getWord(%Trans, 0);
		%Y = getWord(%Trans, 1);
		%Z = getWord(%Trans, 2);
		%Pos = %X SPC %Y SPC %Z;
		messageClient(%client,'updateMBrick',"",%Pos,%tempBrick.EulerRot);

	}



}

function staminaIncrease()
{
	if($Game::Running)
	{
		%maxClient = ClientGroup.getCount();

		for(%clientNum = 0; %clientNum < %maxClient; %clientNum++)
		{
			%client = ClientGroup.getObject(%clientNum);

			if(%client.BrickStamina < 50)
			{
				%client.BrickStamina++;
			}
		}

		Schedule(1000,0,"staminaIncrease");
	}
}

Schedule(10000,0,"staminaIncrease");

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
			%client.wantclearownbricks = 1;
			serverCmdClearOwnBricks(%client);
			%client.delete("You have been kicked for brick spamming.  Don't worry, your mess has been cleared for you.");
			messageAll('',"\c3Warning:  " @ %client.namebase @ " (" @ getRawIP(%client) @ ") has been auto-kicked for brick spamming and their bricks have been cleared.");
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
				messageClient(%client,"",'\c4You have reached the Max Ammount of FX Bricks, which has been set at %1', $Pref::Server::MaxFXBricks);
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
			%brick = %newBrick;

			%SrtPos = %client.WrenchObject.getTransform();
			%EndPos = %brick.getTransform();
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
			commandtoclient(%client,'OpenMoverGui');

			%newBrick.delete();
			%tempBrick.delete();
			%client.player.tempBrick = "";
			return;
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

function ServerCmdScaleZup(%client)
{
	%player = %client.player;
	if(%player.tempBrick)
	{
	%tempBrick = %player.tempBrick;
	%scale = %tempBrick.getScale();
	%scaleX = getWord(%scale,0);
	%scaleY = getWord(%scale,1);
	%scaleZ = getWord(%scale,2);
	%finalscale = %scaleZ + 0.333333;
	if(%finalscale > 10) %finalscale = 10;
	%tempBrick.customscale = %scaleX SPC %scaleY SPC %finalscale;
	%tempBrick.setScale(%scaleX SPC %scaleY SPC %finalscale);
	}

}

function ServerCmdScaleZdown(%client)
{
	%player = %client.player;
	if(%player.tempBrick)
	{
	%tempBrick = %player.tempBrick;
	%scale = %tempBrick.getScale();
	%scaleX = getWord(%scale,0);
	%scaleY = getWord(%scale,1);
	%scaleZ = getWord(%scale,2);
	%finalscale = %scaleZ - 0.333333;
	if(%finalscale <= "0.333333")
	{
	%finalscale = "0.333333";
	}

	else if(%scaleZ <= "0.333333")
	{
	%finalscale = "0.333333";
	}
	%tempBrick.customscale = %scaleX SPC %scaleY SPC %finalscale;
	%tempBrick.setScale(%scaleX SPC %scaleY SPC %finalscale);
	}

}


function ServerCmdCancelBrick(%client)
{
	%player = %client.player;
	if(%player)
	{
		if(%player.tempBrick)
		{
			if(%player.tempBrick.isBrickGhostMoving $= 1)
			{
				%player.tempBrick.setSkinName(%player.tempBrick.oldskinname);
				%player.tempBrick.isBrickGhost = "";
				%player.tempBrick.isBrickGhostMoving = "";
				%player.tempBrick = "";
			}
			else
			{
				%player.tempBrick.delete();
				%player.tempBrick = "";
			}
		}
	}
}
//############################################











function getIPMask(%ip) {
   if(%ip $= "local")
	return;

   %ip_mask = ""; //init
   %orig_ip = %ip; //error handling
   for(%i = 0; %i < 3; %i++) {
      if (strstr(%ip,".")==-1) {
         echo("Error in getIPMask with IP of " @ %orig_ip @ " on iteration " @ %i @ ". String is missing a period. String in question: " @ %ip @ ".");
         return 0;
      } 
      %ip_mask = %ip_mask @ getSubStr(%ip, 0, strstr(%ip, ".")+1); //get first group and .
      %ip = getSubStr(%ip, strstr(%ip,".")+1, strlen(%ip)); //remove first group
   }
   return getSubStr(%ip_mask,0,strlen(%ip_mask)-1); //return and strip the . off end
}

function serverCmdSetStatus(%client,%status)
{
	%client.GameStatus = %status;
	setThePlayerName(%client);
	if(%status !$= "")
		messageClient(%client,'','\c4Updated your status to \c0%1',%status);
}

function servercmdToggleIgnore(%client,%victim)
{
	if(%client.muted[%victim])
	{
		%client.muted[%victim] = 0;
		messageClient(%client,"",'\c4Un-Ignored: \c0%1',%victim.name);
	}
	else
	{
		if(%client == %victim)
		{
			messageClient(%client,"","\c4You can't ignore yourself!",%victim.name);
			return;
		}
		%client.muted[%victim] = 1;
		messageClient(%client,"",'\c4Ignoring: \c0%1',%victim.name);
	}

}

function servercmdToggleAutoColor(%client)
{
	%client.AutoColorMode++;
	if(%client.AutoColorMode > 2)
	{
		%client.AutoColorMode = 0;
	}
	if(%client.AutoColorMode == 0)
	{
		messageClient(%client,"","\c2No Auto-Coloring");
	}
	if(%client.AutoColorMode == 1)
	{
		messageClient(%client,"","\c2Auto-Coloring");
	}
	if(%client.AutoColorMode == 2)
	{
		messageClient(%client,"","\c2Random Auto-Coloring");
	}

}

function servercmdDroidMe(%client)
{
	%client.isDroid = 1;
	%client.isSkele = 0;
	%client.player.mountImage(droidbodyShowImage,$BackSlot,1,'base');
	%client.player.mountImage(rightdroidarmShowImage,$decalSlot,1,'base');
	%client.player.mountImage(lefttdroidarmShowImage,$LeftHandSlot,1,'base');
	%client.player.mountImage(droidheadShowImage,$HeadSlot,1,'base');
	%client.player.mountImage(droidfeetShowImage,$RightFootSlot,1,'base');
	%client.player.mountImage(droidfeetShowImage,$LeftFootSlot,1,'base');
	%client.player.setCloaked(true);

}
function servercmdNoOverride(%client)
{
	%client.isDroid = 0;
	%client.isSkele = 0;
	%client.player.setCloaked(false);
	commandtoclient(%client,'updateprefs');
	%client.player.unmountImage($RightFootSlot);
	%client.player.unmountImage($LeftFootSlot);
}
function servercmdDoSkele(%client)
{
	servercmdSkeleMe(%client);
}
function servercmdSkeleMe(%client)
{
	%client.isSkele = 1;
	%client.isDroid = 0;
	%client.player.mountImage(skelbodyShowImage,$BackSlot,1,'base');
	%client.player.mountImage(skelrightarmShowImage,$decalSlot,1,'base');
	%client.player.mountImage(skelleftarmShowImage,$LeftHandSlot,1,'base');
	%client.player.mountImage(skelheadShowImage,$HeadSlot,1,'base');
	%client.player.mountImage(skelrightfootShowImage,$RightFootSlot,1,'base');
	%client.player.mountImage(skelleftfootShowImage,$LeftFootSlot,1,'base');
	%client.player.setCloaked(true);		
}


function serverCmdVehicleMount(%client)
{
	if(!%client.sitting && !%client.frozen)
	{
		if(!%client.player.isMounted())
		{
			servercmdMountVehicle(%client);
		}
		else
		{
			servercmdDismountVehicle(%client);
		}
	}
}

function serverCmdMountVehicle(%client)
{
   //Determine how far should the picking ray extend into the world?
   %selectRange = 3;

   // Only search for vehicles
   %searchMasks = $TypeMasks::vehicleObjectType ;
   %searchMask2 = $TypeMasks::TurretObjectType;

   %pos = %client.player.getEyePoint();

   // Start with the shape&#180;s eye vector...
   %eye = %client.player.getEyeVector();
   %eye = vectorNormalize(%eye);
   %vec = vectorScale(%eye, %selectRange);

   %end = vectorAdd(%vec, %pos);

   %scanTarg = ContainerRayCast (%pos, %end, %searchMasks);
   %scanTarg2 = ContainerRayCast (%pos, %end, %searchMask2);

   // a target in range was found so select it
   if (%scanTarg)
   {
      %targetObject = firstWord(%scanTarg);
      echo("Found a vehicle: " @ %targetObject);
      onMountVehicle(%targetObject.getDataBlock(),
                     %client.player,
                     %targetObject);
   }
   else if (%scanTarg2)
   {
      %targetObject = firstWord(%scanTarg2);
      echo("Found a turret: " @ %targetObject);
      onMountVehicle(%targetObject.getDataBlock(),
                     %client.player,
                     %targetObject);
   }
   else
   {
      echo("No object found");
      commandToServer('DismountVehicle');
   }
}

function serverCmdDismountVehicle(%client)
{
	if(!%client.frozen )
	{
	doPlayerDismount(%client, %client.player, %true);
	}
}

function serverCmdFindNextFreeSeat(%client)
{
   echo("serverCmdFindNextFreeSeat " @ %client.nameBase);

   // Is the vehicle moving? If so, prevent the player from switching seats
   if (isVehicleMoving(%client.player.mvehicle) == true)
      return;

   %newSeat = findNextFreeSeat(%client,
                               %client.player.mvehicle,
                               %client.player.mvehicle.getDataBlock());

   if (%newSeat != -1)
   {
      echo("Found new seat " @ %newSeat);

      setActiveSeat(%client.player,
                    %client.player.mvehicle,
                    %client.player.mvehicle.getDataBlock(),
                    %newSeat);
   }
   else
   {
      echo("No next free seat");
   }
}

function serverCmdPlayerGiveMoney(%client,%victim,%amount)
{
	if(isObject(%victim.player) && isObject(%client.player))
	{
		if(%client.money >= %amount)
		{
			if(%amount > 0)
			{
				%client.money -= %amount;
				%victim.money += %amount;
				messageClient(%client,"",'\c4Gave \c0$%1\c4 to \c0%2',%amount,%victim.name);
				messageClient(%victim,"",'\c0%2\c4 Gave You \c0$%1',%amount,%client.name);
				messageClient(%client,'MsgUpdateMoney',"",%client.money);
				messageClient(%victim,'MsgUpdateMoney',"",%victim.money);
			}
			else
			{
				messageClient(%client,"",'\c0$%1\c4?!?! Well thats not very nice is it?',%amount,%victim.name);
			}
		}
		else
		{
			messageClient(%client,"","\c4You don't have enough money");
		}
	}	
}

function serverCmdJoinTeam(%client,%TeamID)
{
	if($Pref::Server::LockTeams == 0)
	{
		if(%client.Team $= "")
		{
			%client.Team = $Teams[%TeamID];
			messageAll("",'\c0%1\c5 joined \c0%2',%client.name,%client.Team);
			setThePlayerName(%client);
		}
		else
		{
			messageClient(%client,"","\c4You are already in a team");
		}
	}
	else
	{
		messageClient(%client,"","\c4Sorry, teams are locked at the moment");
	}
}

function serverCmdLeaveTeam(%client,%TeamID)
{
	if($Pref::Server::LockTeams == 0)
	{
		if(%client.Team $= $Teams[%TeamID] && %client.Team !$= "")
		{
			%client.Team = "";
			messageAll("",'\c0%1 \c5left \c0%2',%client.name,$Teams[%TeamID]);
			//GameConnection::setPlayerName(%client,%client.namebase@" - "@$Teams[%TeamID]);
			%client.player.setShapeName(%client.namebase@" - "@$Teams[%TeamID]);
			setThePlayerName(%client);
		}
		else
		{
			messageClient(%client,"","\c4You aren't in this team");
		}
	}
	else
	{
		messageClient(%client,"","\c4Sorry, teams are locked at the moment");
	}
}

function serverCmdisTalking(%client)
{
	$isTyping[$TotalTyping] = %client;
	for(%t = 0; %t < $TotalTyping+1; %t++)
	{
		%string = %string@""@$isTyping[%t].name;
	}

	for(%i= 0; %i < ClientGroup.getCount(); %i++)
	{
		%cl = ClientGroup.getObject(%i);
		messageClient(%cl, 'addToTypingList', '', %string);
	}
	$TotalTyping++;
}

function serverCmdstopTalking(%client)
{
	for(%t=0; %t < $TotalTyping+1; %t++)
	{
		if($isTyping[%t] == %client)
		{
				$isTyping[%t] = 0;
		}
	}
	%no = 0;
	for(%t=0; %t < $TotalTyping+1; %t++)
	{
		if($isTyping[%t] > 0)
		{
			%no = 1;
		}	
	}
	if(%no=1)
	{
		$TotalTyping = 0;
	}
	for(%t = 0; %t < $TotalTyping+1; %t++)
	{
		%string = %string@""@$isTyping[%t].name;
	}
	for(%i= 0; %i < ClientGroup.getCount(); %i++)
	{
		%cl = ClientGroup.getObject(%i);
		messageClient(%cl, 'addToTypingList', '', %string);
	}


}

function serverCmdUndoLast(%client)
{
	if(%client.Undo[0] == 0)
	{
		%client.Undo[1].dead = true;
		%client.Undo[1].schedule(10, explode);
		%client.Undo[1] = 0;
	}
	if(%client.Undo[0] == 1)
	{
		//echo(%client.Undo[2]);
		//echo(%client.Undo[1]);
		%client.Undo[1].setSkinName(%client.Undo[2]);
	}
}

function serverCmdSetBrickMessage(%client,%message)
{
	%client.BrickMessage = %message;
	messageClient(%client,"",'\c4Brick Message: \c0%1',%message);

}

function serverCmdToggleSecure(%client)
{
	if(%client.secure == 1)
	{
		%client.secure = 0;
		messageClient(%client,"","\c4You are no longer in Secure Mode");
	}
	else
	{
		messageClient(%client,"","\c4You are in Secure Mode");
		%client.secure = 1;
		%client.SafeListNum++;
		%client.SafeList[%client.SafeListNum] = %client;
		%client.FriendListNum++;
		%client.FriendList[%client.FriendListNum] = %client;
	}

}

function serverCmdColourOwnBricks(%client)
{
	for (%i = 0; %i < MissionCleanup.getCount(); %i++)
	{
		%brick = MissionCleanup.getObject(%i);
		if(%brick.Owner == %client)
		{
			%brick.setSkinName($legoColor[%client.colorIndex]);
		}
	}

}

function serverCmdToggleSafe(%client,%victim)
{
	%check = 0;
	if(%client.SafeListNum > 0)
	{
		for(%t = 0; %t < %client.SafeListNum+1; %t++)
		{
			if(%client.SafeList[%t] == %victim)
			{
				%client.SafeList[%t] = 0;
				%check = 1;
				messageClient(%victim,"",'\c4You were removed from \c0%1\'s\c4 safe list.',%client.name);
				messageClient(%client,"",'\c4Removed \c0%1\c4 from your safe list.',%victim.name);
				break;
			}		
		}
		if(%check == 0)
		{
			%client.SafeListNum++;
			%client.SafeList[%client.SafeListNum] = %victim;
			messageClient(%victim,"",'\c4You were added to \c0%1\'s\c4 safe list.',%client.name);
			messageClient(%client,"",'\c4Added \c0%1\c4 to your safe list.',%victim.name);
		}
	}
	else
	{
		%client.SafeListNum++;
		%client.SafeList[%client.SafeListNum] = %victim;
		messageClient(%victim,"",'\c4You were added to \c0%1s\c4 safe list.',%client.name);
		messageClient(%client,"",'\c4Added \c0%1\c4 to your safe list.',%victim.name);
	}


}

function serverCmdToggleFriend(%client,%victim)
{
	%check = 0;
	if(%client.FriendListNum > 0)
	{
		for(%t = 0; %t < %client.FriendListNum+1; %t++)
		{
			if(%client.FriendList[%t] == %victim)
			{
				%client.FriendList[%t] = 0;
				%check = 1;
				messageClient(%victim,"",'\c4You were removed from \c0%1\'s\c4 Friend list.',%client.name);
				messageClient(%client,"",'\c4Removed \c0%1\c4 from your Friend list.',%victim.name);
				break;
			}		
		}
		if(%check == 0)
		{
			%client.FriendListNum++;
			%client.FriendList[%client.FriendListNum] = %victim;
			messageClient(%victim,"",'\c4You were added to \c0%1\'s\c4 Friend list.',%client.name);
			messageClient(%client,"",'\c4Added \c0%1\c4 to your Friend list.',%victim.name);
		}
	}
	else
	{
		%client.FriendListNum++;
		%client.FriendList[%client.FriendListNum] = %victim;
		messageClient(%victim,"",'\c4You were added to \c0%1\'s\c4 Friend list.',%client.name);
		messageClient(%client,"",'\c4Added \c0%1\c4 to your Friend list.',%victim.name);
	}


}

function serverCmdAddToInvent(%client, %position, %index)
{
	if($Pref::Server::Weapons == 0 && %index>=$StartWeapons && %index<= $Weapons)
	{
		return;
	}
	if(!%client.isEWandUser && !%client.isAdmin && !%client.isSuperAdmin && %index == $Special)
	{
		return;
	} 
	%player = %client.player;
	%data = %player.getDataBlock();
	
	%player.unMountImage($RightHandSlot);
	messageClient(%player.client, 'MsgHilightInv', '', -1);
	%player.currWeaponSlot = -1;

	%player.InvIndex[%position] = %index;
	%player.inventory[%position] = $Inv[%index,0];
	%inv = $Inv[%index,1];
	messageClient(%client, 'MsgItemPickup', '', %position, %inv);
	%client.CurrentInventoryPosition = %position;
	serverCmdUseInv(%client,%position);
}

function serverCmdMouseWheelClick(%client)
{
	if($Pref::Server::UseInventory == 1 && %client.curInvPos == 5)
	{
		%client.curSprayCan++;
		if(%client.curSprayCan > $SprayCans)
		{
			%client.curSprayCan = $StartSprayCans;
		}
		serverCmdAddtoInvent(%client,%client.curInvPos,%client.curSprayCan);
	}
	

}

function serverCmdCycleInventory(%client,%position,%shift)
{	
	%player = %client.player;
	if(%client.curInvPos == %position)
	{
		%client.curInvPos = %position;

		if(%position > 4)
		{
			if(%shift == 1)
			{
				servercmduseinv(%client,%position);
			}
			else
			{
				if(%position > 6)
				{
					servercmddropinv(%client,%position);
					%client.CurrentInventoryPosition = -1;
				}
				else if(%position > 4 && %position < 6)
				{
					if(%client.curSprayCan == 2)
					{
						%client.BlackletterIndex--;
						if(%client.BlackletterIndex < 0)
						{
							%client.BlackletterIndex = $TotalBlackLetters;
						}	
				commandtoclient(%client,'ShowBrickImage',$BlackLetterPreview[%client.BlackletterIndex]);
					}
					if(%client.curSprayCan == 0)
					{
						%client.colorIndex--;
						if(%client.colorIndex < 0)
						{
							%client.colorIndex = $TotalColors;
						}	
				commandtoclient(%client,'ShowBrickImage',$ColorPreview[%client.colorIndex]);
						}
					if(%client.curSprayCan == 1)
					{
						%client.letterIndex--;
						if(%client.letterIndex < 0)
						{
							%client.letterIndex = $TotalLetters;
						}	
				commandtoclient(%client,'ShowBrickImage',$LetterPreview[%client.letterIndex]);
					}

				}
			
			}
			
		}
		if(%position == 3)
		{
			if((%player.InvIndex[%position]+%shift)>$Misc)
			{
				%player.InvIndex[%position] = $StartMisc-1;
			}	
			if((%player.InvIndex[%position]+%shift)<$StartMisc)
			{
				%player.InvIndex[%position] = $Misc+1;
			}	
			serverCmdAddtoInvent(%client,%position,%player.InvIndex[%position]+%shift);
		}
		if(%position == 4)
		{
			if($Pref::Server::Weapons == 0)
			{
				if((%player.InvIndex[%position]+%shift)>$Tools)
				{
					%player.InvIndex[%position] = $StartTools-1;
				}	
				if((%player.InvIndex[%position]+%shift)<$StartTools)
				{
					%player.InvIndex[%position] = $Tools+1;
				}
			}	
			else
			{
				if((%player.InvIndex[%position]+%shift)>$Weapons)
				{
					%player.InvIndex[%position] = $StartTools-1;
				}	
				if((%player.InvIndex[%position]+%shift)<$StartTools)
				{
					%player.InvIndex[%position] = $Weapons+1;
				}

			}
			serverCmdAddtoInvent(%client,%position,%player.InvIndex[%position]+%shift);
		}

		if(%position == 2)
		{
			if((%player.InvIndex[%position]+%shift)>$Slopes)
			{
				%player.InvIndex[%position] = $StartSlopes-1;
			}	
			if((%player.InvIndex[%position]+%shift)<$StartSlopes)
			{
				%player.InvIndex[%position] = $Slopes+1;
			}	
			serverCmdAddtoInvent(%client,%position,%player.InvIndex[%position]+%shift);
		}

		if(%position == 1)
		{
			if((%player.InvIndex[%position]+%shift)>$Plates)
			{
				%player.InvIndex[%position] = $StartPlates-1;
			}	
			if((%player.InvIndex[%position]+%shift)<$StartPlates)
			{
				%player.InvIndex[%position] = $Plates+1;
			}	
			serverCmdAddtoInvent(%client,%position,%player.InvIndex[%position]+%shift);
		}

		if(%position == 0)
		{
			if((%player.InvIndex[%position]+%shift)>$Bricks)
			{
				%player.InvIndex[%position] = $StartBricks-1;
			}	
			if((%player.InvIndex[%position]+%shift)<$StartBricks)
			{
				%player.InvIndex[%position] = $Bricks+1;
			}	
			serverCmdAddtoInvent(%client,%position,%player.InvIndex[%position]+%shift);
		}
		
	}	
	else
	{
		%client.curInvPos = %position;
		servercmduseinv(%client,%position);
		if(%position>7)
		{
			%client.CurrentInventoryPosition = -1;
		}		
	}
}


function serverCmdUseInventory(%client,%position)
{
	if($Pref::Server::UseInventory == 1)
	{
		%shift = 1;
		if(%position < 8)
		{
			%client.CurrentInventoryPosition = %position;
		}
		servercmdCycleInventory(%client,%position,%shift);
	}
	else
	{
		servercmduseinv(%client,%position);
		%client.CurrentInventoryPosition = -1;
	}
	
}
function serverCmdFreeHands(%client)
{
	if(isObject(%player.tempBrick))
	{
		if(%player.tempBrick.isBrickGhostMoving $= 1)
		{
			%obj.client.SelectedObject = "";
			messageClient(%obj.client,'',"\c4You are out of \c0Edit \c4Mode.");
			%player.tempBrick.setSkinName(%player.tempBrick.oldskinname);
			%player.tempBrick.isBrickGhost = "";
			%player.tempBrick.isBrickGhostMoving = "";
			%player.tempBrick = "";
		}
		else
		{
			%player.tempBrick.delete();
			%player.tempBrick = "";
		}

	}
	messageClient(%client, 'MsgHilightInv', '', -1);
	%client.Player.UnmountImage($RightHandSlot);
	if(%tempBrick = %client.player.tempBrick){
		%client.player.tempBrick.delete();
		%client.player.tempBrick = "";
	}
}
function serverCmdMouseScroll(%client,%shift)
{
	if(%client.CurrentInventoryPosition != -1)
	{
		if($Pref::Server::UseInventory == 1 && %client.CurrentInventoryPosition < 6)
		{
			if(%shift == 1)
			{
				%shift = -1;
			}
			else
			{
				%shift = 1;
			}
			servercmdCycleInventory(%client,%client.CurrentInventoryPosition,%shift);
		}
	}
}


function serverCmdDropInventory(%client,%position)
{
	if($Pref::Server::UseInventory == 1)
	{
		if(%client.curInvPos == %position)
		{
			%shift = -1;
			if(%position < 8)
			{
				%client.CurrentInventoryPosition = %position;
			}
			servercmdCycleInventory(%client,%position,%shift);
		}
		else
		{
			servercmddropinv(%client,%position);
			%client.CurrentInventoryPosition = -1;	
		}
	}
	else
	{
		servercmddropinv(%client,%position);
		%client.CurrentInventoryPosition = -1;	
	}
}


function serverCmdSendMessage(%client,%victim,%message)
{
	if(%message !$= "")
	{
		messageClient(%victim,"",'\c1%1\c2(PM)\c1: %2',%client.name,%message);
		messageClient(%client,"",'\c1%1\c2(%2)\c1: %3',%client.name,%victim.name,%message);
	}

}

function ServerCmduseInv( %client, %position)
{
		%player = %client.player;

		if(%player.inventory[%position])
			%item = %player.inventory[%position].getId();
		else
			%item = 0;

		if(%item)
		{
			%item.onUse(%player, %position); 
		}
}

function ServerCmddropInv( %client, %position)
{
	//throw the item
	//clear the inventory slot
	//if you have another of the same item
		//done
	//else you dropped your last one
		//check if its image is mounted 
			//unmount it

	%player = %client.player;
	%item = %player.inventory[%position];
	
	if(%item && %player)
	{
		//throw the item
		%muzzlepoint = Vectoradd(%player.getposition(), "0 0 1.5");
		%muzzlevector = %player.getEyeVector();
		%muzzlepoint = VectorAdd(%muzzlepoint, %muzzleVector);
		%playerRot = rotFromTransform(%player.getTransform());
		%thrownItem = new (item)()
		{
			datablock = %item;
			//initialVelocity = vectorScale(%muzzlevector, 15);	//these dont actually work for some reason
			//initialPosition = "80.0366 -215.868 183.615";
			count = %player.getinventory(%item);
			thrown = 1;
		};
		MissionCleanup.add(%thrownItem);
		%thrownItem.pickupname = %item.getDataBlock().pickupname;
		%thrownItem.thrown = 1;

		if($Pref::Server::ItemsCostMoney == 1)
		{
			if(%thrownItem.Datablock.Cost > 0)
			{
				%thrownItem.setShapeName("$"@%thrownItem.Datablock.invName);
			}
		}
		%thrownItem.settransform(%muzzlepoint @ " " @ %playerRot);
		%thrownItem.setVelocity(vectorScale(%muzzlevector, 20));
		//%thrownItem.count = %player.getinventory(%item);
		if(%item.persistant == false)
		{
			%thrownItem.schedule($ItemTime, delete);
		}
		//MissionCleanup.add(%thrownItem);

		//%thrownItem.setThrower(%player);
		//%thrownItem.schedule($ItemDropTime, setThrower, 0);
		%thrownItem.setCollisionTimeout(%player);

		//clear inventory slot
		//if our thing is equipped or the currWeapon, unmount the image

		if(%player.isEquiped[%position] == true || %player.currWeaponSlot == %position)
		{
			//this is kind of a backwards way of doing it but it catches things like
			//the bow/superBow and sprayCan combo items.  

			%image = %item.image;
			%mountedImage = %player.getMountedImage(%image.mountPoint);
			
			%player.unMountImage(%image.mountPoint);		
			
			if(%player.currWeaponSlot == %position)
			{
				%player.currWeaponSlot = -1;
				%player.unMountImage($rightHandSlot);	
			}
			else
			{
				%player.isEquiped[%position] = false;
			}
		}

		%player.inventory[%position] = 0;	
		
		if(%item.className $= "Weapon")
			%player.weaponCount--;
		
		messageClient(%client, 'MsgDropItem', "", %position);

	}
	else
	{
		//nothing in the slot, or no player, done
	}
}

function ServerCmdColorLastBrick(%client,%color)
{
	%client.LastBrickPlaced.setSkinName(%color);
}

function ServerCmdUpdatePrefs(%client, %name, %skin,
								%headCode, %visorCode, %backCode, %leftHandCode,
								%headCodeColor, %visorCodeColor, %backCodeColor, %leftHandCodeColor, %decal,%faceprint)
{

if($pref::server::copsandrobbers)
{
return;
}

	%client.setPlayerName(%name);
	%client.colorSkin					= $legoColor[%skin];

	%client.headCode			= $headCode[%headCode];
	%client.visorCode			= $visorCode[%visorCode];
	%client.backCode			= $backCode[%backCode];
	%client.leftHandCode		= $leftHandCode[%leftHandCode];
	%client.chestCode           = basedecalImage;
	%client.faceCode            = facelegoImage;

	%client.headCodeColor		= $legoColor[%headCodeColor];
	%client.visorCodeColor		= $legoColor[%visorCodeColor];
	%client.backCodeColor		= $legoColor[%backCodeColor];
	%client.leftHandCodeColor	= $legoColor[%leftHandCodeColor];
	%client.chestdecalcode		= addTaggedString(%decal);
	%client.faceprintcode		= addTaggedString(%faceprint);
	%player = %client.player;
	if(isObject(%player))
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
	
	//update everyone's scoreboard with the new name
	messageAll('MsgClientJoin', '', 
			  %client.name, 
			  %client,
			  %client.sendGuid,
			  %client.score,
			  %client.isAiControlled(), 
			  %client.isAdmin, 
			  %client.isSuperAdmin);
}

function setThePlayerName(%client)
{
		%player = %client.player;

		if(%client.Team !$= "" && %client.GameStatus !$= "")
		{
			%player.setShapeName("(" @ %client.GameStatus @ ") " @ %client.namebase @ " <" @%client.Team @ ">");
		}
		if(%client.Team $= "" && %client.GameStatus !$= "")
		{
			%player.setShapeName("(" @ %client.GameStatus @ ") " @ %client.namebase);
		}
		if(%client.Team !$= "" && (%client.GameStatus $= "" || %client.GameStatus $= "None"))
		{
			%player.setShapeName(%client.namebase @ " <" @%client.Team @ ">");
		}
		if(%client.Team $= "" && (%client.GameStatus $= "" || %client.GameStatus $= "None"))
		{
			%player.setShapeName(%client.namebase);
		}
}