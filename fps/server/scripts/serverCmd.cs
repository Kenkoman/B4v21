//serverCmd.cs  contains the server end of client commands

function serverCmdKick(%client, %victim)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		//kill the victim client
		if (!%victim.isAIControlled())
		{
			%ip = getRawIP(%victim);
			if(%ip !$= "local")
			{
				//tell everyone about it
				messageAll( 'MsgAdminForce', '\c2The Admin has kicked %1(%2).', %victim.name, getRawIP(%victim));
				//kick them
				%victim.delete("You have been kicked.");
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
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		//add player to ban list
		if (!%victim.isAIControlled())
		{
			//this isnt a bot so add their ip to the banlist
			%ip = getRawIP(%victim);
			if(%ip !$= "local")
			{
				//tell everyone about it
				messageAll( 'MsgAdminForce', '\c2The Admin has banned %1(%2).', %victim.name, getRawIP(%victim));

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
		%victim.delete("You have been banned.");
	}
}

function serverCmdMagicWand(%client)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		%player = %client.player;
		%player.mountImage(wandImage, 0);
	}
}

function ServerCmduseInventory( %client, %position)
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

function ServerCmddropInventory( %client, %position)
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
		};
		MissionCleanup.add(%thrownItem);
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


//	//echo("item = ", %item);
//	if(%item)
//	{
//		%image = %item.image;
//		
//		//echo("image = ", %image);
//		//special case for bow/superbow here
//		if(%image.getId() == bowImage.getId())
//		{
//			if( (%player.getmountedimage(%image.mountpoint) == bowImage.getId()) || (%player.getmountedimage(%image.mountpoint) == superBowImage.getId()) )
//			{
//				%player.unmountImage(%image.mountpoint);
//				return;
//			}
//		}
//		if(%player.getmountedimage(%image.mountpoint) == %image.getId())
//		{
//			%player.unmountImage(%image.mountpoint);
//			return;
//		}
//		else
//		{
//			//we already put it away so this time drop it
//			%muzzlepoint = Vectoradd(%player.getposition(), "0 0 1.5");
//			%muzzlevector = %player.getEyeVector();
//			%muzzlepoint = VectorAdd(%muzzlepoint, %muzzleVector);
//			%thrownItem = new (item)()
//			{
//				datablock = %item;
//				//initialVelocity = vectorScale(%muzzlevector, 15);	//these dont actually work for some reason
//				//initialPosition = "80.0366 -215.868 183.615";
//				count = %player.getinventory(%item);
//			};
//			%thrownItem.settransform(%muzzlepoint @ "0 0 0 0");
//			%thrownItem.setVelocity(vectorScale(%muzzlevector, 20));
//			//%thrownItem.count = %player.getinventory(%item);
//			if(%item.persistant == false)
//			{
//				%thrownItem.schedule($ItemTime, delete);
//			}
//			MissionCleanup.add(%thrownItem);
//
//			//make the slot empty
//			%player.inventory[%position] = 0;	
//			if(%item.className $= "Weapon")
//				%player.weaponCount--;
//		}
//	}

}



function ServerCmdShiftBrick( %client, %x, %y, %z)
{
	//z dimension is sent in PLATE HEIGHTS, not brick heights

	%player = %client.player;
	%tempBrick = %player.tempBrick;
	
	if(%tempBrick)
	{
	
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

	%player = %client.player;
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
		}
		
			//%tempBrick.playthread(0, rotateCW);
			//%tempBrick.schedule(100, playthread, 0, root);
	}
	

}

function ServerCmdPlantBrick(%client)
{
	//echo(%client, " is planting brick");

	%player = %client.player;
	%tempBrick = %player.tempBrick;
	
	if(%tempBrick)
	{
		%solid = %tempBrick.getDataBlock().solid;

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
		


		//check for stuff in the way
		%mask = $TypeMasks::StaticShapeObjectType;

		%tempBrickTrans = %tempBrick.getTransform();
		
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

		%tempBrickData = %tempBrick.getDataBlock().solid;

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

				if(%xOverlap && %yOverlap && %zOverlap){	
					//the brick is inside another brick, no need to keep checking
					//tell the player
					messageClient(%client, 'MsgError', '\c3You can\'t put a brick inside another brick.');
					%newBrick.delete();
					return;
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
			//we didnt find anything to attach to so error and bail
			messageClient(%client, 'MsgError', '\c3You can\'t put a brick in mid air.');
			%newBrick.delete();
			return;
		}
	
		if(%hanging == 1)
		{
			//we're hanging
			%newBrick.wasHung = true;
		}

		//success! 
		
		//put the brick where its supposed to be
		%newBrick.setTransform(%tempBrick.getTransform());
		%newBrick.playAudio(0, brickPlantSound);
		
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
	//echo(%client, " is canceling brick");
	%player = %client.player;
	if(%player)
	{
		if(%player.tempBrick)
		{
			%player.tempBrick.delete();	
			%player.tempBrick = "";

			//tell the player to get out of move brick mode
			//some cmdToClient goes here...
		}
	}
}

//function ServerCmdUpdatePrefs(%client, %airTank, %cape, %goblet, %helmet, %pointyHelmet, %scoutHat, %shield, %triPlume, %visor, %skin,
//								%headCode, %visorCode, %backCode, %leftHandCode)

function ServerCmdUpdatePrefs(%client, %name, %skin,
								%headCode, %visorCode, %backCode, %leftHandCode,
								%headCodeColor, %visorCodeColor, %backCodeColor, %leftHandCodeColor)
{
	%client.setPlayerName(%name);

	//%client.color[airTankImage]			= $legoColor[%airTank];
	//%client.color[capeImage]			= $legoColor[%cape];
	//%client.color[gobletImage]			= $legoColor[%goblet];
	//%client.color[helmetImage]			= $legoColor[%helmet];
	//%client.color[pointyHelmetImage]	= $legoColor[%pointyHelmet];
	//%client.color[scoutHatImage]		= $legoColor[%scoutHat];
	//%client.color[shieldImage]			= $shieldColor[%shield];
	//%client.color[triPlumeImage]		= $legoColor[%triPlume];
	//%client.color[visorImage]			= $legoColor[%visor];
	%client.colorSkin					= $legoColor[%skin];

	%client.headCode			= $headCode[%headCode];
	%client.visorCode			= $visorCode[%visorCode];
	%client.backCode			= $backCode[%backCode];
	%client.leftHandCode		= $leftHandCode[%leftHandCode];

	//%client.headCodeColor		= %client.color[%client.headCode];
	//%client.visorCodeColor		= %client.color[%client.visorCode];
	//%client.backCodeColor		= %client.color[%client.backCode];
	//%client.leftHandCodeColor	= %client.color[%client.leftHandCode];

	%client.headCodeColor		= $legoColor[%headCodeColor];
	%client.visorCodeColor		= $legoColor[%visorCodeColor];
	%client.backCodeColor		= $legoColor[%backCodeColor];
	%client.leftHandCodeColor	= $legoColor[%leftHandCodeColor];
	
	%player = %client.player;
	if(isObject(%player))
	{
		if($Server::MissionType $= "SandBox" && %client.player)
		{
			%player.unMountImage($headSlot);
			%player.unMountImage($visorSlot);
			%player.unMountImage($backSlot);
			%player.unMountImage($leftHandSlot);

			%player.setSkinName(%client.colorSkin);
			%player.mountImage(%client.headCode, $headSlot, 1, %client.headCodeColor);
			%player.mountImage(%client.visorCode, $visorSlot, 1, %client.visorCodeColor);
			%player.mountImage(%client.backCode, $backSlot, 1, %client.backCodeColor);
			%player.mountImage(%client.leftHandCode, $leftHandSlot, 1, %client.leftHandCodeColor);

		}
		%player.setShapeName(%client.name);
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
