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
$chestSlot	= 3;
$faceSlot	= 4;

//color codes used for user appearance preferences
$LCTotal = -1;
$legoColor[$LCTotal++] = "base";
$legoColor[$LCTotal++] = "red";
$legoColor[$LCTotal++] = "orange";
$legoColor[$LCTotal++] = "yellow";
$legoColor[$LCTotal++] = "green";
$legoColor[$LCTotal++] = "blue";
$legoColor[$LCTotal++] = "purple";
$legoColor[$LCTotal++] = "brown";
$legoColor[$LCTotal++] = "white";
$legoColor[$LCTotal++] = "grayDark";
$legoColor[$LCTotal++] = "grey";
$legoColor[$LCTotal++] = "black";
$legoColor[$LCTotal++] = "brightredlilac";
$legoColor[$LCTotal++] = "brightredorange";
$legoColor[$LCTotal++] = "brightredviolet";
$legoColor[$LCTotal++] = "brightyelloworange";
$legoColor[$LCTotal++] = "brightyellowgreen";
$legoColor[$LCTotal++] = "brightbluegreen";
$legoColor[$LCTotal++] = "brightblueviolet";
$legoColor[$LCTotal++] = "yellowmetallic";
$legoColor[$LCTotal++] = "yellowflipflop";
$legoColor[$LCTotal++] = "warmyelloworange";
$legoColor[$LCTotal++] = "violetmetallic";
$legoColor[$LCTotal++] = "turquoise";
$legoColor[$LCTotal++] = "silver";
$legoColor[$LCTotal++] = "sandyellow";
$legoColor[$LCTotal++] = "sandviolet";
$legoColor[$LCTotal++] = "sandred";
$legoColor[$LCTotal++] = "sandgreen";
$legoColor[$LCTotal++] = "sandblue";
$legoColor[$LCTotal++] = "rust";
$legoColor[$LCTotal++] = "royalblue";
$legoColor[$LCTotal++] = "redlilac";
$legoColor[$LCTotal++] = "redflipflop";
$legoColor[$LCTotal++] = "redbrown";
$legoColor[$LCTotal++] = "nougat";
$legoColor[$LCTotal++] = "neonorange";
$legoColor[$LCTotal++] = "neongreen";
$legoColor[$LCTotal++] = "meduimredviolet";
$legoColor[$LCTotal++] = "mediumyelloworange";
$legoColor[$LCTotal++] = "mediumyellowgreen";
$legoColor[$LCTotal++] = "mediumstonegrey";
$legoColor[$LCTotal++] = "mediumroyalblue";
$legoColor[$LCTotal++] = "mediumred";
$legoColor[$LCTotal++] = "mediumorange";
$legoColor[$LCTotal++] = "mediumlilac";
$legoColor[$LCTotal++] = "mediumgreen";
$legoColor[$LCTotal++] = "mediumblueviolet";
$legoColor[$LCTotal++] = "mediumbluegreen";
$legoColor[$LCTotal++] = "mediumblue";
$legoColor[$LCTotal++] = "lilac";
$legoColor[$LCTotal++] = "lightyelloworange";
$legoColor[$LCTotal++] = "lightyellowgreen";
$legoColor[$LCTotal++] = "lightyellow";
$legoColor[$LCTotal++] = "lightstonegrey";
$legoColor[$LCTotal++] = "lightroyalblue";
$legoColor[$LCTotal++] = "lightreddishviolet";
$legoColor[$LCTotal++] = "lightred";
$legoColor[$LCTotal++] = "lightpurple";
$legoColor[$LCTotal++] = "lightpink";
$legoColor[$LCTotal++] = "lightorangebrown";
$legoColor[$LCTotal++] = "lightorange";
$legoColor[$LCTotal++] = "lightlilac";
$legoColor[$LCTotal++] = "lightgreymetallic";
$legoColor[$LCTotal++] = "lightgrey";
$legoColor[$LCTotal++] = "lightgreen";
$legoColor[$LCTotal++] = "lightbrickyellow";
$legoColor[$LCTotal++] = "lightblueishviolet";
$legoColor[$LCTotal++] = "lightbluegreen";
$legoColor[$LCTotal++] = "lightblue";
$legoColor[$LCTotal++] = "lemonmetallic";
$legoColor[$LCTotal++] = "gunmetallic";
$legoColor[$LCTotal++] = "greyflipflop";
$legoColor[$LCTotal++] = "gold";
$legoColor[$LCTotal++] = "flameyelloworange";
$legoColor[$LCTotal++] = "flameredorange";
$legoColor[$LCTotal++] = "fireyellow";
$legoColor[$LCTotal++] = "fadedgreen";
$legoColor[$LCTotal++] = "earthorange";
$legoColor[$LCTotal++] = "earthgreen";
$legoColor[$LCTotal++] = "earthblue";
$legoColor[$LCTotal++] = "doveblue";
$legoColor[$LCTotal++] = "darkstonegrey";
$legoColor[$LCTotal++] = "darkroyalblue";
$legoColor[$LCTotal++] = "darkred";
$legoColor[$LCTotal++] = "darkorange";
$legoColor[$LCTotal++] = "darknougat";
$legoColor[$LCTotal++] = "darkgreymetallic";
$legoColor[$LCTotal++] = "darkgreen";
$legoColor[$LCTotal++] = "darkcurry";
$legoColor[$LCTotal++] = "curry";
$legoColor[$LCTotal++] = "coolyellow";
$legoColor[$LCTotal++] = "brickyellow";
$legoColor[$LCTotal++] = "bluemetallic";
$legoColor[$LCTotal++] = "blackmetallic";
$legoColor[$LCTotal++] = "greychecker";
$legoColor[$LCTotal++] = "olivechecker";
$legoColor[$LCTotal++] = "blackchecker";
$legoColor[$LCTotal++] = "bluechecker";
$legoColor[$LCTotal++] = "stripe";
$legoColor[$LCTotal++] = "grass";
$legoColor[$LCTotal++] = "ice";
$legoColor[$LCTotal++] = "sand";
$legoColor[$LCTotal++] = "rock";
$legoColor[$LCTotal++] = "water";
$legoColor[$LCTotal++] = "lava";
$legoColor[$LCTotal++] = "wood";
$legoColor[$LCTotal++] = "wood2";
$legoColor[$LCTotal++] = "crate";
$legoColor[$LCTotal++] = "metal";
$legoColor[$LCTotal++] = "concrete";
$legoColor[$LCTotal++] = "stone";
$legoColor[$LCTotal++] = "brick";
$legoColor[$LCTotal++] = "Gird";
$legoColor[$LCTotal++] = "metal2";
$legoColor[$LCTotal++] = "metal3";
$legoColor[$LCTotal++] = "hay";
$legoColor[$LCTotal++] = "shingle";
$legoColor[$LCTotal++] = "camo";
$legoColor[$LCTotal++] = "camo2";
$legoColor[$LCTotal++] = "camo3";
$legoColor[$LCTotal++] = "camo4";

//shield color codes
$legoColor[-1] = "lion";
$legoColor[-2] = "wolf";
$legoColor[-3] = "dragon";
$legoColor[-4] = "deer";

//item codes used for appearance preferences (in sandbox mode only?)
//head
$headTotal = 0;
$headcode[$headTotal] = helmetShowImage;
$headCode[$headTotal++] = scoutHatShowImage;
$headCode[$headTotal++] = pointyHelmetShowImage;
$headCode[$headTotal++] = hairShowImage;
$headCode[$headTotal++] = femhairShowImage;
$headCode[$headTotal++] = femhair2ShowImage;
$headCode[$headTotal++] = WizhatShowImage;
$headCode[$headTotal++] = cowboyShowImage;
$headCode[$headTotal++] = PirateShowImage;
$headCode[$headTotal++] = navyShowImage;
$headCode[$headTotal++] = darthShowImage;
$headCode[$headTotal++] = samhelmShowImage;
$headCode[$headTotal++] = froShowImage;
$headCode[$headTotal++] = armyShowImage;
$headCode[$headTotal++] = capShowImage;
$headCode[$headTotal++] = policeShowImage;
$headCode[$headTotal++] = tophatShowImage;
$headCode[$headTotal++] = aviatorShowImage;
$headCode[$headTotal++] = ninjamaskShowImage;
$headCode[$headTotal++] = batmanShowImage;
$headCode[$headTotal++] = crownShowImage;
$headCode[$headTotal++] = dmaulShowImage;
$headCode[$headTotal++] = firemanShowImage;
$headCode[$headTotal++] = islandShowImage;
$headCode[$headTotal++] = jediShowImage;
$headCode[$headTotal++] = spidyShowImage;
$headCode[$headTotal++] = DictatorHatShowImage;
$headCode[$headTotal++] = StormShowImage;
$headCode[$headTotal++] = BurgerkingShowImage;
$headCode[$headTotal++] = FlattopShowImage;
$headCode[$headTotal++] = PonytailShowImage;
$headCode[$headTotal++] = cthelmShowImage;
$headCode[$headTotal++] = mandalorianShowImage;
$headCode[$headTotal++] = arcShowImage;

$visorCode[0] = triPlumeShowImage;
$visorCode[1] = visorShowImage;
$visorCode[2] = shornShowImage;
$visorCode[3] = bandShowImage;
$visorCode[4] = islandaccShowImage;
$visorCode[5] = avgooglesShowImage;
$visorCode[6] = hornsShowImage;
$visorCode[7] = snorkelShowImage;

$backTotal = 0;
$backCode[$backTotal] = capeShowImage;
$backCode[$backTotal++] = bucketPackShowImage;
$backCode[$backTotal++] = quiverShowImage;
$backCode[$backTotal++] = plateMailShowImage;
$backCode[$backTotal++] = packShowImage;
$backCode[$backTotal++] = airTankShowImage;
$backCode[$backTotal++] = cloakShowImage;
$backCode[$backTotal++] = samarmShowImage;
$backCode[$backTotal++] = scubatankShowImage;
$backCode[$backTotal++] = epauletsShowImage;
$backCode[$backTotal++] = R2ShowImage;

$leftHandCode[0] = shieldShowImage;
$leftHandCode[1] = gobletShowImage;
$leftHandCode[2] = BeardShowImage;
$leftHandCode[3] = sickleShowImage;
$leftHandCode[4] = broomShowImage;
$leftHandCode[5] = whipShowImage;
$leftHandCode[6] = birdShowImage;
$leftHandCode[7] = boobsShowImage;
$leftHandCode[8] = briefcaseShowImage;
$leftHandCode[9] = mugShowImage;

$rightHandCode[0] = hammerImage;
$rightHandCode[1] = wrenchImage;
$rightHandCode[2] = axeImage;
$rightHandCode[3] = bowImage;
$rightHandCode[4] = spearImage;
$rightHandCode[5] = BrifleImage;
$rightHandCode[6] = RevolverImage;
$rightHandCode[7] = RifleImage;
$rightHandCode[8] = swordImage;
$rightHandCode[9] = cutlassImage;
$rightHandCode[10] = katanaImage;
$rightHandCode[11] = lsabreImage;
$rightHandCode[12] = SarumanStaffImage;
$rightHandCode[13] = HalberdAxeImage;
$rightHandCode[14] = pickaxeImage;
$rightHandCode[15] = loudhailerImage;
//

//Droid items

$DroidHeadTotal = -1;
$DroidHeadcode[$DroidHeadTotal++] = DroidBatHeadShowImage;
$DroidHeadCode[$DroidHeadTotal++] = DroidHeadFlatShowImage;
$DroidHeadCode[$DroidHeadTotal++] = DroidIGHeadShowImage;
$DroidHeadCode[$DroidHeadTotal++] = 0;

$DroidBackTotal = -1;
$DroidBackCode[$DroidBackTotal++] = cloakShowImage;
$DroidBackCode[$DroidBackTotal++] = R2ShowImage;

//end of Droid items

$pi      = 3.1415927;
$piOver2 = 1.5707963;
$m2pi     = 6.2831853;

$ItemTime	= 10000;	//time until items fade out when you drop them
$ItemDropTime = 1000;	//time from when you drop something till when you can pick it back up

$tallestBrick = 6 * 0.6;

function eulerToMatrix( %euler )
{
	%euler = VectorScale(%euler, $pi / 180 * -1);				//convert euler rotations to radians

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

function getzfromtransform (%trans) {
  %trans = getword(%trans,6) * getword(%trans,5) / 2 / $pi * 360;
  return %trans;
}
