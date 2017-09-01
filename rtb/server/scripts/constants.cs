//constants.cs - contains constants and some good conversion functions
$currCheckVal = 10;

//Slot contants for mounting images
$RightHandSlot	= 0;
$LeftHandSlot	= 1;
$BackSlot	= 2;
$RightFootSlot	= 3;
$LeftFootSlot	= 4;
$chestSlot	= 3;
$faceSlot	= 4;

$HeadSlot	= 5;
$VisorSlot	= 6;
$decalslot	= 7;

//color codes used for user appearance preferences(and brick colours+color previews)
$TotalColors = 0;
$legoColor[$TotalColors] = 'redlight';
$ColorPreview[$TotalColors] = "rtb/data/shapes/bricks/redLight.brickside";
$legoColor[$TotalColors++] = 'red';
$ColorPreview[$TotalColors] = "rtb/data/shapes/bricks/red.brickside";
$legoColor[$TotalColors++] = 'redDark';
$ColorPreview[$TotalColors] = "rtb/data/shapes/bricks/redDark.brickside";
$legoColor[$TotalColors++] = 'orange';
$ColorPreview[$TotalColors] = "rtb/data/shapes/bricks/orange.brickside";
$legoColor[$TotalColors++] = 'yellowlight';
$ColorPreview[$TotalColors] = "rtb/data/shapes/bricks/yellowLight.brickside";
$legoColor[$TotalColors++] = 'yellow';
$ColorPreview[$TotalColors] = "rtb/data/shapes/bricks/yellow.brickside";
$legoColor[$TotalColors++] = 'yellowDark';
$ColorPreview[$TotalColors] = "rtb/data/shapes/bricks/yellowDark.brickside";
$legoColor[$TotalColors++] = 'greenlight';
$ColorPreview[$TotalColors] = "rtb/data/shapes/bricks/greenlight.brickside";
$legoColor[$TotalColors++] = 'green';
$ColorPreview[$TotalColors] = "rtb/data/shapes/bricks/green.brickside";
$legoColor[$TotalColors++] = 'greenDark';
$ColorPreview[$TotalColors] = "rtb/data/shapes/bricks/greenDark.brickside";
$legoColor[$TotalColors++] = 'lightblue';
$ColorPreview[$TotalColors] = "rtb/data/shapes/bricks/lightblue.brickside";
$legoColor[$TotalColors++] = 'blue';
$ColorPreview[$TotalColors] = "rtb/data/shapes/bricks/blue.brickside";
$legoColor[$TotalColors++] = 'blueDark';
$ColorPreview[$TotalColors] = "rtb/data/shapes/bricks/blueDark.brickside";
$legoColor[$TotalColors++] = 'purplelight';
$ColorPreview[$TotalColors] = "rtb/data/shapes/bricks/purplelight.brickside";
$legoColor[$TotalColors++] = 'purple';
$ColorPreview[$TotalColors] = "rtb/data/shapes/bricks/purple.brickside";
$legoColor[$TotalColors++] = 'purpledark';
$ColorPreview[$TotalColors] = "rtb/data/shapes/bricks/purpledark.brickside";
$legoColor[$TotalColors++] = 'tan';
$ColorPreview[$TotalColors] = "rtb/data/shapes/bricks/tan.brickside";
$legoColor[$TotalColors++] = 'tandark';
$ColorPreview[$TotalColors] = "rtb/data/shapes/bricks/tandark.brickside";
$legoColor[$TotalColors++] = 'brownlight';
$ColorPreview[$TotalColors] = "rtb/data/shapes/bricks/brownlight.brickside";
$legoColor[$TotalColors++] = 'brown';
$ColorPreview[$TotalColors] = "rtb/data/shapes/bricks/brown.brickside";
$legoColor[$TotalColors++] = 'white';
$ColorPreview[$TotalColors] = "rtb/data/shapes/bricks/white.brickside";
$legoColor[$TotalColors++] = 'base';
$ColorPreview[$TotalColors] = "rtb/data/shapes/bricks/base.brickside";
$legoColor[$TotalColors++] = 'graydark';
$ColorPreview[$TotalColors] = "rtb/data/shapes/bricks/graydark.brickside";
$legoColor[$TotalColors++] = 'black';
$ColorPreview[$TotalColors] = "rtb/data/shapes/bricks/black.brickside";
$legoColor[$TotalColors++] = 'blackglitter';
$ColorPreview[$TotalColors] = "rtb/data/shapes/bricks/blackglitter.brickside";


$TotalLetters = 0;
$Letter[$TotalLetters] = 'letterA';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letterA.brickside.bmp";
$Letter[$TotalLetters++] = 'letterB';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letterB.brickside.bmp";
$Letter[$TotalLetters++] = 'letterC';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letterC.brickside.bmp";
$Letter[$TotalLetters++] = 'letterD';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letterD.brickside.bmp";
$Letter[$TotalLetters++] = 'letterE';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letterE.brickside.bmp";
$Letter[$TotalLetters++] = 'letterF';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letterF.brickside.bmp";
$Letter[$TotalLetters++] = 'letterG';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letterG.brickside.bmp";
$Letter[$TotalLetters++] = 'letterH';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letterH.brickside.bmp";
$Letter[$TotalLetters++] = 'letterI';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letterI.brickside.bmp";
$Letter[$TotalLetters++] = 'letterJ';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letterJ.brickside.bmp";
$Letter[$TotalLetters++] = 'letterK';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letterK.brickside.bmp";
$Letter[$TotalLetters++] = 'letterL';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letterL.brickside.bmp";
$Letter[$TotalLetters++] = 'letterM';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letterM.brickside.bmp";
$Letter[$TotalLetters++] = 'letterN';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letterN.brickside.bmp";
$Letter[$TotalLetters++] = 'letterO';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letterO.brickside.bmp";
$Letter[$TotalLetters++] = 'letterP';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letterP.brickside.bmp";
$Letter[$TotalLetters++] = 'letterQ';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letterQ.brickside.bmp";
$Letter[$TotalLetters++] = 'letterR';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letterR.brickside.bmp";
$Letter[$TotalLetters++] = 'letterS';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letterS.brickside.bmp";
$Letter[$TotalLetters++] = 'letterT';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letterT.brickside.bmp";
$Letter[$TotalLetters++] = 'letterU';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letterU.brickside.bmp";
$Letter[$TotalLetters++] = 'letterV';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letterV.brickside.bmp";
$Letter[$TotalLetters++] = 'letterW';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letterW.brickside.bmp";
$Letter[$TotalLetters++] = 'letterX';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letterX.brickside.bmp";
$Letter[$TotalLetters++] = 'letterY';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letterY.brickside.bmp";
$Letter[$TotalLetters++] = 'letterZ';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letterZ.brickside.bmp";
$Letter[$TotalLetters++] = 'letter0';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letter0.brickside.bmp";
$Letter[$TotalLetters++] = 'letter1';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letter1.brickside.bmp";
$Letter[$TotalLetters++] = 'letter2';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letter2.brickside.bmp";
$Letter[$TotalLetters++] = 'letter3';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letter3.brickside.bmp";
$Letter[$TotalLetters++] = 'letter4';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letter4.brickside.bmp";
$Letter[$TotalLetters++] = 'letter5';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letter5.brickside.bmp";
$Letter[$TotalLetters++] = 'letter6';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letter6.brickside.bmp";
$Letter[$TotalLetters++] = 'letter7';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letter7.brickside.bmp";
$Letter[$TotalLetters++] = 'letter8';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letter8.brickside.bmp";
$Letter[$TotalLetters++] = 'letter9';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letter9.brickside.bmp";
$Letter[$TotalLetters++] = 'letter!';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letter!.brickside.bmp";
$Letter[$TotalLetters++] = 'letterQuestion';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letterQuestion.brickside.bmp";
$Letter[$TotalLetters++] = 'letter(';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letter(.brickside.bmp";
$Letter[$TotalLetters++] = 'letter)';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letter).brickside.bmp";
$Letter[$TotalLetters++] = 'letterQuote';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letterQuote.brickside.bmp";
$Letter[$TotalLetters++] = 'letter$';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letter$.brickside.bmp";
$Letter[$TotalLetters++] = 'letter%';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letter%.brickside.bmp";
$Letter[$TotalLetters++] = 'letter-';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letter-.brickside.bmp";
$Letter[$TotalLetters++] = 'letter+';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letter+.brickside.bmp";
$Letter[$TotalLetters++] = 'letter=';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letter=.brickside.bmp";
$Letter[$TotalLetters++] = 'letterStar';
$LetterPreview[$TotalLetters] = "rtb/data/shapes/bricks/letterStar.brickside.bmp";

$TotalBlackLetters = 0;
$BlackLetter[$TotalBlackLetters] = 'letterAblack';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letterAblack.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letterBblack';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letterBblack.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letterCblack';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letterCblack.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letterDblack';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letterDblack.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letterEblack';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letterEblack.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letterFblack';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letterFblack.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letterGblack';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letterGblack.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letterHblack';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letterHblack.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letterIblack';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letterIblack.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letterJblack';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letterJblack.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letterKblack';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letterKblack.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letterLblack';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letterLblack.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letterMblack';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letterMblack.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letterNblack';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letterNblack.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letterOblack';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letterOblack.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letterPblack';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letterPblack.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letterQblack';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letterQblack.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letterRblack';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letterRblack.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letterSblack';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letterSblack.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letterTblack';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letterTblack.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letterUblack';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letterUblack.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letterVblack';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letterVblack.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letterWblack';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letterWblack.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letterXblack';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letterXblack.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letterYblack';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letterYblack.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letterZblack';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letterZblack.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letter0black';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letter0black.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letter1black';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letter1black.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letter2black';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letter2black.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letter3black';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letter3black.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letter4black';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letter4black.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letter5black';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letter5black.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letter6black';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letter6black.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letter7black';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letter7black.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letter8black';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letter8black.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letter9black';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letter9black.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letter!black';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letter!black.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letterQuestionblack';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letterQuestionblack.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letter(black';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letter(black.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letter)black';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letter)black.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letterQuoteblack';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letterQuoteblack.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letter$black';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letter$black.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letter%black';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letter%black.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letter-black';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letter-black.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letter+black';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letter+black.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letter=black';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letter=black.brickside.bmp";
$BlackLetter[$TotalBlackLetters++] = 'letterStarblack';
$BlackLetterPreview[$TotalBlackLetters] = "rtb/data/shapes/bricks/letterStarblack.brickside.bmp";

//

//shield color codes
$shieldColor[0] = 'lion';
$shieldColor[1] = 'wolf';
$shieldColor[2] = 'dragon';
$shieldColor[3] = 'deer';

//item codes used for appearance preferences (in sandbox mode only?)
//head
$headTotal = 0;
$headCode[$headTotal] = helmetShowImage;
$headCode[$headTotal++] = scoutHatShowImage;
$headCode[$headTotal++] = pointyHelmetShowImage;
$headCode[$headTotal++] = HairShowImage;
$headCode[$headTotal++] = femHairShowImage;
$headCode[$headTotal++] = femHair2ShowImage;
$headCode[$headTotal++] = WizhatShowImage;
$headCode[$headTotal++] = cowboyShowImage;
$headCode[$headTotal++] = PirateShowImage;
$headCode[$headTotal++] = NavyShowImage;
$headCode[$headTotal++] = darthShowImage;
$headCode[$headTotal++] = samhelmShowImage;


$headCode[$headTotal++] = capShowImage;
$CopHat = $headTotal;
$headCode[$headTotal++] = helmet2ShowImage;
$headCode[$headTotal++] = chefhatShowImage;
$headCode[$headTotal++] = conhatShowImage;
$headCode[$headTotal++] = hat123ShowImage;

$headCode[$headTotal++] = stormShowImage;
$headCode[$headTotal++] = Cap2ShowImage;
$headCode[$headTotal++] = firehatShowImage;
$headCode[$headTotal++] = fighthatShowImage;
$headCode[$headTotal++] = beanie1ShowImage;
$RobHat = $headTotal;
$headCode[$headTotal++] = tophat2ShowImage;
$headCode[$headTotal++] = englishwarShowImage;
$headCode[$headTotal++] = oldspacehelmetShowImage;
$headCode[$headTotal++] = PithhelmetShowImage;
$headcode[$HeadTotal++] = farmershatShowImage;
$headcode[$headtotal++] = hatmidflatShowImage;
$headcode[$headtotal++] = helmetchinwideShowImage;
$headcode[$headtotal++] = Tricornershowimage;
$headcode[$headtotal++] = aviatorcapshowimage;
$headcode[$headtotal++] = cavalrycapshowimage;
$headcode[$headtotal++] = storm2showimage;
$headcode[$headtotal++] = crownshowimage;
$headcode[$headtotal++] = islanderhatshowimage;


$visorCode[0] = triPlumeShowImage;
$visorCode[1] = visorShowImage;
$visorCode[2] = shornShowImage;
$visorCode[3] = diversmaskShowImage;
$visorCode[4] = aviatoracShowImage;
$visorCode[5] = VisorwithAntennashowimage;
$visorCode[6] = islanderac1showimage;

$backCode[0] = capeShowImage;
$backCode[1] = bucketPackShowImage;
$backCode[2] = quiverShowImage;
$backCode[3] = plateMailShowImage;
$backCode[4] = packShowImage;
$backCode[5] = airTankShowImage;
$backCode[6] = cloakShowImage;
$backCode[7] = SamArmShowImage;
$backCode[8] = lifejacketShowImage;
$backCode[9] = jetpackoldShowImage;
$backCode[10] = scubaShowImage;
$backCode[11] = epauletsShowImage;

%extraTotal = 0;
$leftHandCode[%extraTotal] = BeardShowImage;
$leftHandCode[%extraTotal++] = shieldShowImage;
$leftHandCode[%extraTotal++] = gobletShowImage;

$leftHandCode[%extraTotal++] = binocularsShowImage;
$leftHandCode[%extraTotal++] = camShowImage;
$leftHandCode[%extraTotal++] = vidcamShowImage;
$leftHandCode[%extraTotal++] = drillShowImage;
$leftHandCode[%extraTotal++] = radioShowImage;
$leftHandCode[%extraTotal++] = frypanShowImage;
$leftHandCode[%extraTotal++] = briefcaseShowImage;
$leftHandCode[%extraTotal++] = goblet2ShowImage;

$leftHandCode[%extraTotal++] = MoneyShowImage;
$leftHandCode[%extraTotal++] = ShovelShowImage;
$leftHandCode[%extraTotal++] = ScrewdriverShowImage;
$leftHandCode[%extraTotal++] = FishrodShowImage;
$leftHandCode[%extraTotal++] = MagglassShowImage;
$leftHandCode[%extraTotal++] = Cup1ShowImage;


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
