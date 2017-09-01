//constants.cs - contains constants and some good conversion functions
$currCheckVal = 10;

//Slot contants for mounting images
$RightHandSlot	= 0;
$LeftHandSlot	= 1;
$BackSlot	= 2;
$RightFootSlot	= 3;
$LeftFootSlot	= 4;
$HeadSlot	= 5;
$VisorSlot	= 6;

//color codes used for user appearance preferences
$legoColor[0] = 'red';
$legoColor[1] = 'yellow';
$legoColor[2] = 'green';
$legoColor[3] = 'blue';
$legoColor[4] = 'white';
$legoColor[5] = 'gray';
$legoColor[6] = 'graydark';
$legoColor[7] = 'black';
$legoColor[8] = 'brown';
$legoColor[9] = 'lightblue';
//

//shield color codes
$shieldColor[0] = 'lion';
$shieldColor[1] = 'wolf';
$shieldColor[2] = 'dragon';
$shieldColor[3] = 'deer';

//item codes used for appearance preferences (in sandbox mode only?)
//head
$headCode[0] = helmetShowImage;
$headCode[1] = scoutHatShowImage;
$headCode[2] = pointyHelmetShowImage;

$visorCode[0] = triPlumeShowImage;
$visorCode[1] = visorShowImage;

$backCode[0] = capeShowImage;
$backCode[1] = bucketPackShowImage;
$backCode[2] = quiverShowImage;
$backCode[3] = plateMailShowImage;
$backCode[4] = packShowImage;
$backCode[5] = airTankShowImage;

$leftHandCode[0] = shieldShowImage;
$leftHandCode[1] = gobletShowImage;
//

$pi      = 3.1415927;
$piOver2 = 1.5707963;
$m2pi     = 6.2831853;

$ItemTime	= 10000;	//time until items fade out when you drop them
$ItemDropTime = 1000;	//time from when you drop something till when you can pick it back up

$tallestBrick = 6 * 0.6;

function eulerToMatrix( %euler )
{
	%euler = VectorScale(%euler, $pi / 180);				//convert euler rotations to radians

	%matrix = MatrixCreateFromEuler(%euler);				//make a rotation matrix
	%xvec = getWord(%matrix, 3);						//get the parts of the matrix you need
	%yvec = getWord(%matrix, 4);
	%zvec = getWord(%matrix, 5);
	%ang  = getWord(%matrix, 6);						//this is in radians
	%ang = %ang * 180 / $pi;						//convert back to degrees

	%rotationMatrix = %xvec @ " " @ %yvec @ " " @ %zvec @ " " @ %ang;	//put it all together

	return %rotationMatrix;							//send it back
}

function getWords(%phrase, %start, %end)
{
	if(%start > %end)
		return;

	%returnPhrase = getWord(%phrase, %start);
	if(%start == %end)
		return %returnPhrase;

	for(%i = %start+1; %i <= %end; %i++)
	{
		%returnPhrase = %returnPhrase @ " " @ getWord(%phrase, %i);
	}

	return %returnPhrase;
}


// --------------------------------------------
// miscellaneous yet handy functions - stolen from t2 kekeke

function posFromTransform(%transform)
{
   // the first three words of an object's transform are the object's position
   %position = getWord(%transform, 0) @ " " @ getWord(%transform, 1) @ " " @ getWord(%transform, 2);
   return %position;
}

function rotFromTransform(%transform)
{
   // the last four words of an object's transform are the object's rotation
   %rotation = getWord(%transform, 3) @ " " @ getWord(%transform, 4) @ " " @ getWord(%transform, 5) @ " " @ getWord(%transform, 6);
   return %rotation;
}

function posFromRaycast(%transform)
{
   // the 2nd, 3rd, and 4th words returned from a successful raycast call are the position of the point
   %position = getWord(%transform, 1) @ " " @ getWord(%transform, 2) @ " " @ getWord(%transform, 3);
   return %position;
}

function normalFromRaycast(%transform)
{
   // the 5th, 6th and 7th words returned from a successful raycast call are the normal of the surface
   %norm = getWord(%transform, 4) @ " " @ getWord(%transform, 5) @ " " @ getWord(%transform, 6);
   return %norm;
}

//rounds to the nearest .01
function round(%val)
{
	%val *= 100;
	%val = mFloor(%val);
	%val /= 100;

	return %val;
}
