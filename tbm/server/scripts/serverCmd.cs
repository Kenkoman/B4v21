//serverCmd.cs  contains the server end of client commands
/////////////////////
//Kick player
/////////////////////
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
				%victim.delete("You have been kicked by "@%client.namebase@".");
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


///////////////////
//Ban Player
//////////////////
function serverCmdBan(%client, %victim)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		if(%victim.isSuperAdmin || %victim.isAdmin)
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
				serverIRCannounce(%client.namebase @ " has banned " @ %victim.namebase @ ".");
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


//////////////////////////
//Mass Client Kick - TBM - Courtesy Kier(MCP) and mytourdeforce(Chris)
//////////////////////////
function clearout() {      
%a=-1; 
for (%clientIndex = 0; %clientIndex < ClientGroup.getCount(); %clientIndex++) {      
       %victim = ClientGroup.getObject( %clientIndex );     
       if (!%victim.issuperadmin && !%victim.isadmin)  { 
          %set[%a++]=%victim;    
         } 
       }   
   for (%Index = 0; %Index <= %a; %Index++) {      
       %victim = %set[%Index];     
       %victim.delete("The server has been cleared of all clients.  Don't take it personally.");
       serverIRCannounce("The server has been cleared of all clients.");
   }  
}

////////////////////////////////////////////////////////////
//Function for accessing the magic wand
////////////////////////////////////////////////////////////
function serverCmdMagicWand(%client)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		%player = %client.player;
		%player.mountImage(wandImage, 0);
	}
}


/////////////////////////////////////////////////////
//Command to Shift the Temp brick
/////////////////////////////////////////////////////
function ServerCmdShiftBrick( %client, %x, %y, %z)
{
	//z dimension is sent in PLATE HEIGHTS, not brick heights

	%player = %client.player;
if (%client.edit) {
  if (isobject(%client.lastswitch)) 
     %tempBrick = %client.lastswitch;
  else {
    messageClient(%client, 'Msg', "\c2You have nothing selected");	
    return;
  }
 }
else {
  if (%player.getMountedImage(0) == nametoid(iGobImage))
	%tempBrick = %player.client.iGob;
  else
	%tempBrick = %player.tempBrick;	
 }
    if(%tempBrick)
	{
	
		%forwardVec = %player.getForwardVector();
		%forwardX = getWord(%forwardVec, 0);
		%forwardY = getWord(%forwardVec, 1);
  %tempBrick.startfade(0,0,false);
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
                if ((%client.edit || %player.getMountedImage(0) == nametoid(iGobImage)) && %client.shiftfactor!=0) {
                  %x *= %client.shiftfactor;
                  %y *= %client.shiftfactor;
		  %z *= %client.shiftfactor;
                  }
                else {
  		  %x *= 0.5;
                  %y *= 0.5;
		  %z *= 0.2;
                  }
        if (%player.getMountedImage(0) == nametoid(iGobImage)) {
          for (%int = 0; %int <= %client.iGob.total; %int++) {
            if(isObject(%block = %client.iGob.brick[%int])) {
              shift(%client.iGob.brick[%int], %x, %y, %z);
              if(%client.iGob.brick[%int].getDataBlock() == nametoID(portculyswitch)) {
                for(%j = 1; %j <= %block.numActions; %j++) {
                  if(%block.type[%j] $= "teleport")
                    %block.direction[%j] = vectorAdd(getWords(%block.direction[%j], 0, 2), %x SPC %y SPC %z) SPC getWords(%block.direction[%j], 3, 6);
                }
              }
            }
          }
        }
	shift(%tempbrick, %x, %y, %z);
      	ServerPlay3D(brickMoveSound, %tempBrick.getTransform());
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

/////////////////////////////////////
//Rotating Temp Brick
////////////////////////////////////
function ServerCmdRotateBrick( %client, %dir)
{
	//echo(%client, " is rotating brick dir = ", %dir);

	%player = %client.player;
if (%client.edit) {
   if (%client.lastswitch.rotsav !$= "")
      %client.lastswitch.rotsav=rotaddup(%client.lastswitch.rotsav,vectorscale(%client.rotfactor,%dir));
   else
      %client.lastswitch.rotsav=%client.rotfactor;
   %client.lastswitch.settransform(%client.lastswitch.position@" "@rotconv(%client.lastswitch.rotsav));
   ServerPlay3D(brickRotateSound, %client.lastswitch.gettransform());
   return;
   }
else if (%player.getMountedImage(0) == nametoid(iGobImage) && %client.rotfactor !$= "") {
	if (mabs(getword(%client.rotfactor,0)) > 0) 
        	%rotfactor = mabs(getword(%client.rotfactor,0));
	else if (mabs(getword(%client.rotfactor,1)) > 0) 
        	%rotfactor = mabs(getword(%client.rotfactor,1));
	else if (mabs(getword(%client.rotfactor,2)) > 0) 
        	%rotfactor = mabs(getword(%client.rotfactor,2));
	else 
        	%rotfactor = 90;
	%rotfactor = "0 0 " @ %rotfactor;

	if (%dir == -1) {
	  servercmdrotateigob(%client, vectoradd("0 0 360",vectorscale(%rotfactor,-1)));
	  //echo(vectoradd("0 0 360",vectorscale(%rotfactor,-1)));
	  }
	else {
  	  servercmdrotateigob(%client, %rotfactor);
	  //echo(%client.rotfactor);
	  }
	ServerPlay3D(brickRotateSound, %client.iGob.gettransform());
	return;
}
else 
  %tempBrick = %player.tempBrick;	
	if(%tempBrick)
	{
		%brickTrans = %tempBrick.getTransform();

		ServerPlay3D(brickRotateSound, %brickTrans);

		%brickXYZ = getWords(%brickTrans, 0, 2);
		%brickAngle = getWord(%brickTrans, 6);
		%vectorDir = getWord(%brickTrans, 5);

		if(%vectorDir == -1)
			%brickAngle += $pi;
		
		%brickAngle /= $piOver2;
		%brickAngle = mFloor(%brickAngle + 0.1);
		%brickAngle += %dir;

		
		if (%brickAngle > 4)
			%brickAngle -= 4;
		if (%brickAngle <= 0)
			%brickAngle += 4;
                if (%brickangle==1)
		  %tempBrick.rotsav="0 0 "@90;
                else if (%brickangle==2)
		  %tempBrick.rotsav="0 0 "@180;
                else if (%brickangle==3)
		  %tempBrick.rotsav="0 0 "@270;
                else if (%brickangle==4)
		  %tempBrick.rotsav="0 0 0";

		%tempBrick.setTransform(%brickXYZ @ " 0 0 1 " @ %brickAngle * $piOver2);
		if(%dir == 1){
			//dir = 1, clockwise
			if(%brickAngle == 1){
				shift(%tempBrick, 0, 0.5, 0);
			}
			else if(%brickAngle == 2){
				shift(%tempBrick, 0.5, 0, 0);
			}
			else if(%brickAngle == 3){
				shift(%tempBrick, 0, -0.5, 0);
			}
			else if(%brickAngle == 4){
				shift(%tempBrick, -0.5, 0, 0);
			}
   %tempBrick.startfade(0,0,false);
		}
		else {
			//dir = -1, counter-clockwise
			if(%brickAngle == 1){
				shift(%tempBrick, -0.5, 0, 0);
			}
			else if(%brickAngle == 2){
				shift(%tempBrick, 0, 0.5, 0);
			}
			else if(%brickAngle == 3){
				shift(%tempBrick, 0.5, 0, 0);
			}
			else if(%brickAngle == 4){
				shift(%tempBrick, 0, -0.5, 0);
			}
            %tempBrick.startfade(0,0,false);
			//%tempBrick.playthread(0, rotateCW);
			//%tempBrick.schedule(100, playthread, 0, root);
		}
	}
	

}


/////////////////////////////////
//Plant a solid brick
////////////////////////////////
function ServerCmdPlantBrick(%client)
{
	%player = %client.player;
	if (%client.edit) {
	   if (isobject(%client.lastswitch) && %client.lastscale !$= "")
	   %client.lastswitch.setscale(%client.lastscale);
	   return;
	   }
        if (%player.getMountedImage(0) == nametoid(iGobImage)) {
		if (%client.iGob.total > 0)
			servercmddetachigob(%client);
		else
 	      		servercmdattachtoigob(%client);
            return;
 	   }

	%tempBrick = %player.tempBrick;
	if( %client.killer == 1 && $Pref::Server::optDDM == false)
	{
		messageClient( %client, 'KillerMsg', "\c2\x96Sorry, you are a killer, you may not build.");
		return;
	}
if( %client.bricklimit > 0 || $Pref::Server::AdminBrickLimit == -1)
{
	if(%tempBrick)
	{
		
		if(%client.forbidbricks) {
		  messageClient(%client, 'MsgBrickLimit', 'You are not allowed to plant new bricks.');
		  return;
		}		

		%solid = %tempBrick.getDataBlock();

		//create the new brick//
		%newBrick = new StaticShape()
		{
			datablock = %solid;
		};
		MissionCleanup.add(%newBrick);
                %newBrick.setscale(%tempbrick.getscale());
                %newBrick.rotsav=%tempBrick.rotsav;
		//intialize upper and lower attachement lists
		%newBrick.upSize = 0;
		%newBrick.downSize = 0;
		%newBrick.up[0] = -1;
		%newBrick.down[0] = -1;
		%newBrick.owner = getRawIP(%client);
		SetSignText(%client, %newBrick);
		if( %client.autoperm == 1)
			%newBrick.permbrick = 1;
		if(%newBrick.getDatablock().decal $= "1"){
          servercmdsetupdecalplay(%client,%client.brickdecal,%client.decaltime,%client.decalvar,%newBrick);
          servercmdsetupdecal(%client,%client.brickdecal,%client.decaltime,%client.decalvar);
          %newBrick.setSkinName(%client.brickdecalname);
        }
        else {
        if(%client.randombrickcolor == 1)
		%newBrick.setSkinName($legocolor[getRandom(0,$LCTotal)]);
        else
        %newBrick.setSkinName(%client.brickcolor);
        %client.decalPlaced = 0;
        }

		//check for stuff in the way
		%mask = $TypeMasks::StaticShapeObjectType;
		
		%tempBrickTrans = %tempBrick.getTransform();
		
		//these will be where we start ray casting
		//the starting and ending positions are adjusted to match the rotation
		%zscale = (getWord(%tempBrick.getscale(), 2));
		%tempBrickX = (getWord(%tempBrickTrans, 0));
		%tempBrickY = (getWord(%tempBrickTrans, 1));
		%tempBrickZ = (getWord(%tempBrickTrans, 2));

		%startX = %tempBrickX;
		%startY = %tempBrickY;
		%startZ = %tempBrickZ;

		%startZ += (%solid.z * 0.2 * %zscale) ;			//start at top of brick		

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
		%zoffSet = (%solid.z * 0.2 * %zscale) / 2;	

		//echo("offset is ", %xoffset, " ", %yoffset, " ", %zoffset);

		//find the biggest dimension
		%radius = %solid.x * 0.5;

		if( (%solid.y * 0.5) > %radius)
		{
			//echo("y is bigger");
			%radius = %solid.y * 0.5;
		}
		if( (%solid.z * 0.2 * %zscale) > %radius)
		{
			//echo("z is bigger");
			%radius = %solid.z * 0.2 * %zscale;
		}
		//its a radius, so divide by 2;
		%radius /= 2;
		%radius += 0.5;
		
		//echo("Radius = ", %radius);

		%brickCenter = %tempBrickX + %xOffset @ " " @ %tempBrickY + %yOffset @ " " @ %tempBrickZ + %zOffset ;
		//echo("brick center is ", %brickCenter);
		%mask = $TypeMasks::StaticShapeObjectType;
        InitContainerRadiusSearch(%brickCenter, %radius, $TypeMasks::PlayerObjectType);
	    while ((%checkObj = containerSearchNext()) != 0){
		  if (%checkObj.client.isSuperAdmin && %checkObj.client != %client) {
            //echo("fucking jackass");
            %newBrick.delete();
            if (!%client.isSuperAdmin && !%client.isAdmin && !%client.isMod)
            punishmentof(%client);
            return;
            }
          }
		
		%attachment = 0;
		%hanging = 1;

		InitContainerRadiusSearch(%brickCenter, %radius, %mask);

		while ((%checkObj = containerSearchNext()) != 0){
                      if (%tempBrick != %checkObj) {
			%xOverLap = 0;	
			%yOverLap = 0;
			%zOverLap = 0;	
			%zTouch = 0;	//is the brick is right up against another brick vertically? 
			
			%checkData = %checkObj.getDataBlock();
                        %chkzscale = getword(%checkObj.getscale(),2);

			if(%checkData.className $= "Brick" || %checkData.className $= "Baseplate"){

				//%checkObj.setSkinName('Blue');
				%checkTrans = %checkObj.getTransform();

				%checkXpos = getWord(%checkTrans, 0);
				%checkYpos = getWord(%checkTrans, 1);
				%checkZpos = getWord(%checkTrans, 2);

				//---check for z over touching---
				%comp1 = round(%tempBrickZ + (%solid.z * 0.2 * %zscale));
				%comp2 = round(%checkZpos);
				if(%comp1 == %comp2){
					//placing under checkObj
					%attachedOnTop = true;
					%zTouch = 1;
				}
					
				%comp1 = round(%tempBrickZ);
				%comp2 = round(%checkZpos + (%checkData.Z * 0.2 * %chkzscale));
				if(%comp1 == %comp2){
					//placing ontop of checkObj
					%attachedOnTop = false;
					%zTouch = 1;
				}
				//---end ztouching check---

				%tempBottom = round(%tempBrickZ);
				%tempTop = round(%tempBrickZ + (%solid.z * 0.2 * %zscale));
				%checkBottom = round(%checkZpos);
				%checkTop = round(%checkZpos + (%checkData.z * 0.2 * %chkzscale));

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

		if ($Pref::Server::Overlap == 0) {
					if(%xOverlap && %yOverlap && %zOverlap){	
						//the brick is inside another brick, no need to keep checking
						//tell the player
						messageClient(%client, 'MsgError', '\c3You can\'t put a brick inside another brick.');
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
                }
		if( $Pref::Server::Float == false )
		{
			if(%attachment == 0)
			{
					//we didnt find anything to attach to so error and bail
					messageClient(%client, 'MsgError', '\c3You can\'t put a brick in mid air.');
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
		
        if((%client.isMod || %client.isAdmin || %client.isSuperAdmin) && %client.autodoorset == 1)
        %newBrick.port = %client.autoport;

        commandtoclient(%client,'brickplanted');
        %client.totalbrickcount++;
		//put the brick where its supposed to be
        getbrickowner(getrawip(%client),2);
		%newBrick.setTransform(%tempBrick.getTransform());
		%newBrick.playAudio(0, brickPlantSound);
		%tempBrick.startfade(0,0,true);
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
if( %client.isAdmin || %client.isSuperAdmin || %client.isMod || $Pref::Server::AdminBrickLimit == -1)
  %piss=%client;
else {
	%client.bricklimit--;
	schedule(5000, 0, eval, %client @ ".bricklimit++;");
     }
}
else
{
messageClient(%client, 'MsgBrickLimit', 'You are out of bricks.');
}
}

///////////////////////////////////
//Cancel the temp brick
////////////////////////////////////
function ServerCmdCancelBrick(%client)
{
	//echo(%client, " is canceling brick");
	%player = %client.player;
        if (%player.getMountedImage(0) == nametoid(iGobImage)) {
       servercmddetachigob(%client);
 	   //%client.iGob.delete();
           return;
 	   }
	if(%player)
	{
		if(%player.tempBrick)
		{
			%player.tempBrick.delete();	
			%player.tempBrick = "";

			//tell the player to get out of move brick mode
			//some cmdToClient goes here...
		}
                else if (%client.edit && isobject(%client.lastswitch)) {
                  messageClient(%client, 'Msg', "\c2"@%client.lastswitch.dataBlock@" has been unselected.");
                  %client.lastswitch.setSkinName(%client.lastswitchcolor);
                  %client.lastswitch=0;
                }
	}
}

//Respawn code - this goes at the bottom of serverCmd.cs for simplicity and is used by the adminGui.gui modification to permit respawning of a client
function serverCmdrespawn(%client,%victimId)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		%victimId.player.delete();
		messageAll('','\c3%1 \c2has been respawned by \c3%2.',%victimId.name,%client.namebase);
		%victimId.spawnPlayer();
	}
}

//This pwns the target for %time ms.
function serverCmdtumble(%client,%victim,%time)
{
	if(%client.isSuperAdmin && %victim.istumbling != 1)
	{
		tumble2(%victim.player,%time);
		%col.istumbling = 1;
		schedule(%time,0,%col.istumbling = 0);
	}
}


function serverCmdcheckmoney(%client,%victim) {
%name = %victim.namebase;
if(%name $= "")
return;
	if(%client.isSuperAdmin)
	{
		messageclient(%client,'Msg','%1\ has %2 %3.',%victim.name,%victim.studmoney, $Pref::Server::CurrencyName);
	}
}

//Highly defunct.
function serverCmdsetmoneybrick(%client,%victim) {
%name = %victim.namebase;
if(%name $= "")
return;
	if(%client.isSuperAdmin)
	{
		%money = %victim.studmoney;
		%brick = %client.lastswitch;
		%brick.setshapename(%victim.namebase SPC "-" SPC %money); 
		messageclient(%client,'Msg','%1\ has %2 %3.', %victim.name, %money, $Pref::Server::CurrencyName);
	}
}



function serverCmdsetmoney(%client,%victim,%money) {
if(%client.isSuperAdmin && isObject(%victim)) {
	%victim.setStuds(%money);
	messageClient(%client, 'Msg', '%1\ now has %2 %3.', %victim.name, %victim.studMoney, $Pref::Server::CurrencyName);
}
}

function serverCmdsetstuds(%client,%victim,%money) {
if(%client.isSuperAdmin) {
	%victim.studmoney = %money;
	messageClient(%client, 'Msg', '%1\ now has %2 %3.', %victim.name, %victim.studMoney, $Pref::Server::CurrencyName);
}
}

function servercmdwigtogkeepinventory(%client) {
if(%client.isSuperAdmin) {
	if($pref::server::DeathKeepInventory != 1) {
		$pref::server::DeathKeepInventory = 1;
		messageall(' ','\c3Inventory\c2 is no longer \c3dropped on death.');
	}
	else {
		$pref::server::DeathKeepInventory = 0;
		messageall(' ','\c3Inventory\c2 is now \c3dropped on death.');
	}
}
}

function servercmdwigtogdeathstuds(%client) {
if(%client.isSuperAdmin) {
	if($pref::server::DeathStuds != 1) {
		$pref::server::DeathStuds = 1;
		messageall(' ','\c3Death Studs\c2 has been \c3enabled.');
	}
	else {
		$pref::server::DeathStuds = 0;
		messageall(' ','\c3Death Studs\c2 has been \c3disabled.');
	}
}
}

function servercmdwigtogdeathlosestuds(%client) {
if(%client.isSuperAdmin) {
	if($pref::server::DeathLoseStuds != 1) {
		$pref::server::DeathLoseStuds = 1;
		messageall(' ','\c3Death Studs\c2 are now \c3kept.');
	}
	else {
		$pref::server::DeathLoseStuds = 0;
		messageall(' ','\c3Death Studs\c2 are now \c3lost.');
	}
}
}

function servercmdwigtogpedbotstayonplates(%client) {
if(%client.isSuperAdmin) {
	if($pref::server::pedbotstayonplates != 1) {
		$pref::server::pedbotstayonplates = 1;
		messageclient(%client,' ','\c2Pedestrian Bots now \c3stay \c2on plates.');
	}
	else {
		$pref::server::pedbotstayonplates = 0;
		messageclient(%client,' ','\c2Pedestrian Bots \c3no longer stay \c2on plates.');
	}
}
}

function servercmdwigtogpedbotplatedist(%client,%amount) {
if(%client.isSuperAdmin) {
	if(%amount < 0) {
		messageclient(%client,' ','\c3Please choose a value above 0.');
		return;
	}
	$pedbotplatedist = %amount;
	messageclient(%client,' ','\c3Pedestrian Bots now stay within %1 units of plates.',%amount);
}
}

function servercmdwigtogpedbotdestdist(%client,%amount) {
if(%client.isSuperAdmin) {
	if(%amount < 25) {
		messageclient(%client,' ','\c3Please choose a value above 25.');
		return;
	}
	$pedbotdestdist = %amount;
	messageclient(%client, ' ', "\c2Pedestrian Bots now wander \c3" @ %amount SPC "\c2units at a time.");
}
}


//shitbricks by shiznit

function shizgrid(%x, %y)//gets a random x/y coordinate
{
%xvalue = getrandom(%x / -2, %x / 2) / 4;
%yvalue = getrandom(%y / -2, %y / 2) / 4;
%coordinate = %xvalue SPC %yvalue;
return %coordinate;
}

function servercmdshitbricks(%client) {
if(%client.isadmin || %client.issuperadmin || $Pref::Server::CanHasShit == 1) {
//up until recently I had no idea how for() worked, so I'll see if I can explain
//(thanks to foamy3 and the rest of cemetech.net for helping me understand for)
//the first part, %a=0; defines where I'm starting my array. %a < 2; defines where
//I want it to stop, in this case, before 2. %a++ then increases the value of %a
//and whatever is defined within the {} is done and the process repeats until it
//reaches the the number defined in %a < 2;	-DShiznit

for(%a=0; %a < 2; %a++) { 
%v[%a] = shizgrid(2, 2);
}

for(%a=2; %a < 4; %a++) {
%v[%a] = shizgrid(3, 3);
}

for(%a=4; %a < 8; %a++) {
%v[%a] = shizgrid(4, 4);
}

for(%a=8; %a < 12; %a++) {
%v[%a] = shizgrid(5, 5);
}

for(%a=12; %a < 20; %a++) {
%v[%a] = shizgrid(6, 6);
}

for(%a=20; %a < 28; %a++) {
%v[%a] = shizgrid(7, 7);
}

%trans = %client.player.gettransform();
%client.player.playthread(2, fall);

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

for(%d=0; %d < 2; %d++) {
%shit[%d] = new StaticShape()
		{
			datablock = staticPlate1x1;
		};
%shit[%d].setskinname(brown);
%shit[%d].settransform(getword(%trans, 0) + getword(%v[%d], 0) - 0.5 SPC getword(%trans, 1) + getword(%v[%d], 1) - 0.5 SPC getword(%trans, 2));
$shit[$shittotal++] = %shit[%d];
}

for(%s=0; %s < 2; %s++) {
shift(%shit[%s], 0, 0, 0.6);
shift(%client.player, 0, 0, 0.5);
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

for(%d=2; %d < 4; %d++) {
%shit[%d] = new StaticShape()
		{
			datablock = staticPlate1x1;
		};
%shit[%d].setskinname(brown);
%shit[%d].settransform(getword(%trans, 0) + getword(%v[%d], 0) - 0.5 SPC getword(%trans, 1) + getword(%v[%d], 1) - 0.5 SPC getword(%trans, 2));
$shit[$shittotal++] = %shit[%d];
}

for(%s=0; %s < 4; %s++) {
shift(%shit[%s], 0, 0, 0.6);
shift(%client.player, 0, 0, 0.4);
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

for(%d=4; %d < 8; %d++) {
%shit[%d] = new StaticShape()
		{
			datablock = staticPlate1x1;
		};
%shit[%d].setskinname(brown);
%shit[%d].settransform(getword(%trans, 0) + getword(%v[%d], 0) - 0.5 SPC getword(%trans, 1) + getword(%v[%d], 1) - 0.5 SPC getword(%trans, 2));
$shit[$shittotal++] = %shit[%d];
}

for(%s=0; %s < 8; %s++) {
shift(%shit[%s], 0, 0, 0.6);
shift(%client.player, 0, 0, 0.3);
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

for(%d=8; %d < 12; %d++) {
%shit[%d] = new StaticShape()
		{
			datablock = staticPlate1x1;
		};
%shit[%d].setskinname(brown);
%shit[%d].settransform(getword(%trans, 0) + getword(%v[%d], 0) - 0.5 SPC getword(%trans, 1) + getword(%v[%d], 1) - 0.5 SPC getword(%trans, 2));
$shit[$shittotal++] = %shit[%d];
}

for(%s=0; %s < 12; %s++) {
shift(%shit[%s], 0, 0, 0.6);
shift(%client.player, 0, 0, 0.2);
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

for(%d=12; %d < 20; %d++) {
%shit[%d] = new StaticShape()
		{
			datablock = staticPlate1x1;
		};
%shit[%d].setskinname(brown);
%shit[%d].settransform(getword(%trans, 0) + getword(%v[%d], 0) - 0.5 SPC getword(%trans, 1) + getword(%v[%d], 1) - 0.5 SPC getword(%trans, 2));
$shit[$shittotal++] = %shit[%d];
}

for(%s=0; %s < 20; %s++) {
shift(%shit[%s], 0, 0, 0.6);
shift(%client.player, 0, 0, 0.1);
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

for(%d=20; %d < 28; %d++) {
%shit[%d] = new StaticShape()
		{
			datablock = staticPlate1x1;
		};
%shit[%d].setskinname(brown);
%shit[%d].settransform(getword(%trans, 0) + getword(%v[%d], 0) - 0.5 SPC getword(%trans, 1) + getword(%v[%d], 1) - 0.5 SPC getword(%trans, 2));
$shit[$shittotal++] = %shit[%d];
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

%client.player.playthread(2, root);
}
}

function cleanshit()
{
	for(%a=1; %a < $shittotal + 1; %a++)
	{
	$shit[%a].delete();
	}
	$shittotal = 0;
}

function ServerCmdCheckClass(%client)
{
echo("Object class is:" SPC %client.lastswitch.getClassName());
}

function serverCmdInvincibleBricks(%client, %val) {
if(%client.isAdmin || %client.isSuperAdmin) {
  if(%val $= "")
    %val = !$Pref::Server::InvincibleBricks;
  $Pref::Server::InvincibleBricks = !!%val;
  if(%val)
    messageAll('Msg', "\c2Bricks are now \c3invincible\c2.");
  else
    messageAll('Msg', "\c2Bricks are \c3no longer \c2invincible.");
}
}

function serverCmdSpawnWep(%client, %x) {
if((%client.isAdmin || %client.isSuperAdmin) && isObject($weapon[%x]))
  spawnWep(%client, $weapon[%x]);
}

function serverCmdAdjustObjPos(%client, %var1) {
if(%client.edit) {
  if(isObject(%client.lastswitch)) {
    %var1 = strReplace(strReplace(%var1, ",", " "), "  ", " ");
    for(%i = 0; %i <= 2; %i++) {
      %v[%i] = getWord(%var1, %i);
      if(%v[%i] == 0)
        %v[%i] = 0;
    }
    %client.lastswitch.setTransform(%v0 SPC %v1 SPC %v2 SPC getWords(%client.lastswitch.getTransform(), 3, 7));
    ServerPlay3D(brickMoveSound, %client.lastswitch.getTransform());
  }
}
else
  messageClient(%client, 'Msg', "\c2You have to be in \c3EDITOR\c2 mode to use this.");
}

function serverCmdAdjustObjRot(%client, %var1) {
if(%client.edit) {
  if(isObject(%client.lastswitch)) {
    %var1 = strReplace(strReplace(%var1, ",", " "), "  ", " ");
    for(%i = 0; %i <= 2; %i++) {
      %v[%i] = getWord(%var1, %i);
      if(%v[%i] == 0)
        %v[%i] = 0;
      while(%v[%i] >= 360)
        %v[%i] -= 360;
      while(%v[%i] < 0)
        %v[%i] += 360;
    }
    %client.lastswitch.rotsav = %v0 SPC %v1 SPC %v2;
    %client.lastswitch.setTransform(%client.lastswitch.position SPC rotConv(%client.lastswitch.rotsav));
    ServerPlay3D(brickRotateSound, %client.lastswitch.getTransform());
  }
}
else
  messageClient(%client, 'Msg', "\c2You have to be in \c3EDITOR\c2 mode to use this.");
}