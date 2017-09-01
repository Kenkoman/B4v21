
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
	if(($Pref::Server::UseInventory == 1 || %client.isInventoryRights) && %client.curInvPos == 8 && !%client.isDeInvent)
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

		if(%position > 7)
		{
			if(%shift == 1)
			{
				servercmduseinv(%client,%position);
			}
			else
			{
				if(%position > 9)
				{
					servercmddropinv(%client,%position);
					%client.CurrentInventoryPosition = -1;
				}
				else if(%position > 7 && %position < 9)
				{
					if(%client.curSprayCan == 3)
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
						%client.textureIndex--;
						if(%client.textureIndex < 0)
						{
							%client.textureIndex = $TotalTextures;
						}	
				commandtoclient(%client,'ShowBrickImage',$TexturePreview[%client.textureIndex]);
						}
					if(%client.curSprayCan == 2)
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
		if(%position == 4)
		{
			if((%player.InvIndex[%position]+%shift)>$Misc2)
			{
				%player.InvIndex[%position] = $StartMisc2-1;
			}	
			if((%player.InvIndex[%position]+%shift)<$StartMisc2)
			{
				%player.InvIndex[%position] = $Misc2+1;
			}	
			serverCmdAddtoInvent(%client,%position,%player.InvIndex[%position]+%shift);
		}
		if(%position == 5)
		{
			if((%player.InvIndex[%position]+%shift)>$Misc3)
			{
				%player.InvIndex[%position] = $StartMisc3-1;
			}	
			if((%player.InvIndex[%position]+%shift)<$StartMisc3)
			{
				%player.InvIndex[%position] = $Misc3+1;
			}	
			serverCmdAddtoInvent(%client,%position,%player.InvIndex[%position]+%shift);
		}
		if(%position == 6)
		{
			if((%player.InvIndex[%position]+%shift)>$Misc4)
			{
				%player.InvIndex[%position] = $StartMisc4-1;
			}	
			if((%player.InvIndex[%position]+%shift)<$StartMisc4)
			{
				%player.InvIndex[%position] = $Misc4+1;
			}	
			serverCmdAddtoInvent(%client,%position,%player.InvIndex[%position]+%shift);
		}
		if(%position == 9)
		{
			if((%player.InvIndex[%position]+%shift)>$Special)
			{
				%player.InvIndex[%position] = $StartSpecial-1;
			}	
			if((%player.InvIndex[%position]+%shift)<$StartSpecial)
			{
				%player.InvIndex[%position] = $Special+1;
			}	
			serverCmdAddtoInvent(%client,%position,%player.InvIndex[%position]+%shift);
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
		if(%position == 7)
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
		if(%position>9)
		{
			%client.CurrentInventoryPosition = -1;
		}		
	}
}


function serverCmdUseInventory(%client,%position)
{
	if(($Pref::Server::UseInventory == 1  || %client.isInventoryRights)&& !%client.isDeInvent)
	{
		%shift = 1;
		if(%position <= 9)
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
function serverCmdMouseScroll(%client,%shift)
{
	if(%client.CurrentInventoryPosition != -1)
	{
		if(($Pref::Server::UseInventory == 1 || %client.isInventoryRights) && %client.CurrentInventoryPosition <= 9 && !%client.isDeInvent)
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
//Meant to overwrite invsel159.cs
$MaxInvent = 200;

//------------Tools--------
$SprayCans = 0;
$StartSprayCans = $SprayCans;
$Inv[$SprayCans,0] = nameToID(SprayCanImage) - 1;
$Inv[$SprayCans,1] = "Colors";
$Inv[$SprayCans++,0] = nameToID(TextureCanImage) - 1;
$Inv[$SprayCans,1] = "Texture Colors";
$Inv[$SprayCans++,0] = nameToID(LetterCanImage) - 1;
$Inv[$SprayCans,1] = "Black Letters";
$Inv[$SprayCans++,0] = nameToID(BlackLetterCanImage) - 1;
$Inv[$SprayCans,1] = "White Letters";

$Tools = $SprayCans + 1;
$StartTools = $Tools;
$Inv[$Tools,0] = nameToID(hammerImage) - 1;
$Inv[$Tools,1] = "Hammer";
$Inv[$Tools++,0] = nameToID(wrenchImage) - 1;
$Inv[$Tools,1] = "Wrench";
$Inv[$Tools++,0] = nameToID(keyImage) - 1;
$Inv[$Tools,1] = "Key";

$Weapons = $Tools + 1;
$StartWeapons = $Weapons;

$Inv[$Weapons,0] = nameToID(FlamethrowerImage) - 1;
$Inv[$Weapons,1] = "Flamethrower";
$Inv[$Weapons++,0] = nameToID(bowImage) - 1;
$Inv[$Weapons,1] = "Bow";
$Inv[$Weapons++,0] = nameToID(SpearImage) - 1;
$Inv[$Weapons,1] = "Spear";
$Inv[$Weapons++,0] = nameToID(AxeImage) - 1;
$Inv[$Weapons,1] = "Axe";
$Inv[$Weapons++,0] = nameToID(SwordImage) - 1;
$Inv[$Weapons,1] = "Sword";
$Inv[$Weapons++,0] = nameToID(ChainsawImage) - 1;
$Inv[$Weapons,1] = "Chainsaw";
$Inv[$Weapons++,0] = nameToID(lightsabreImage) - 1;
$Inv[$Weapons,1] = "Light Sabre";
$CopWeapon1 = $Weapons;

$Inv[$Weapons++,0] = nameToID(katanaImage) - 1;
$Inv[$Weapons,1] = "Katana";
$RobWeapon2 = $Weapons;

$Inv[$Weapons++,0] = nameToID(pickaxeImage) - 1;
$Inv[$Weapons,1] = "Pick-Axe";
//$Inv[$Weapons++,0] = nameToID(cutlassImage) - 1;
//$Inv[$Weapons,1] = "Cutlass";
$Inv[$Weapons++,0] = nameToID(revolverImage) - 1;
$Inv[$Weapons,1] = "Revolver";
$CopWeapon2 = $Weapons;
$RobWeapon1 = $Weapons;

$Inv[$Weapons++,0] = nameToID(rifleImage) - 1;
$Inv[$Weapons,1] = "Rifle";
//$Inv[$Weapons++,0] = nameToID(flashgrenadeImage) - 1;
//$Inv[$Weapons,1] = "Flashgrenade";
$Inv[$Weapons++,0] = nameToID(smokegrenadeImage) - 1;
$Inv[$Weapons,1] = "Smokegrenade";
$Inv[$Weapons++,0] = nameToID(crossbowImage) - 1;
$Inv[$Weapons,1] = "Crossbow";
//$Inv[$Weapons++,0] = nameToID(pistolImage) - 1;
//$Inv[$Weapons,1] = "Pistol";
$Inv[$Weapons++,0] = nameToID(blasterImage) - 1;
$Inv[$Weapons,1] = "Blaster";
$Inv[$Weapons++,0] = nameToID(duallsabreImage) - 1;
$Inv[$Weapons,1] = "Dual LSabre";
$Inv[$Weapons++,0] = nameToID(speargunImage) - 1;
$Inv[$Weapons,1] = "Spear Gun";

//--------Bricks------------

$Bricks = $Weapons + 1;
$StartBricks = $Bricks;

$Inv[$Bricks,0] = nameToID(brick1x1tImage)- 2;
$Inv[$Bricks,1] = "Brick 1x1t";
$Inv[$Bricks++,0] = nameToID(brick1x1Image)- 2;
$Inv[$Bricks,1] = "Brick 1x1";
$Inv[$Bricks++,0] = nameToID(brick1x2Image)- 2;
$Inv[$Bricks,1] = "Brick 1x2";
$Inv[$Bricks++,0] = nameToID(brick1x3Image)- 2;
$Inv[$Bricks,1] = "Brick 1x3";
$Inv[$Bricks++,0] = nameToID(brick1x4Image)- 2;
$Inv[$Bricks,1] = "Brick 1x4";
$Inv[$Bricks++,0] = nameToID(brick1x6Image)- 2;
$Inv[$Bricks,1] = "Brick 1x6";
$Inv[$Bricks++,0] = nameToID(brick1x8Image)- 2;
$Inv[$Bricks,1] = "Brick 1x8";
$Inv[$Bricks++,0] = nameToID(brick2x2Image)- 2;
$Inv[$Bricks,1] = "Brick 2x2";
$Inv[$Bricks++,0] = nameToID(brick2x3Image)- 2;
$Inv[$Bricks,1] = "Brick 2x3";
$Inv[$Bricks++,0] = nameToID(brick2x4Image)- 2;
$Inv[$Bricks,1] = "Brick 2x4";
$Inv[$Bricks++,0] = nameToID(brick2x8Image)- 2;
$Inv[$Bricks,1] = "Brick 2x8";
$Inv[$Bricks++,0] = nameToID(brick2x10Image)- 2;
$Inv[$Bricks,1] = "Brick 2x10";
$Inv[$Bricks++,0] = nameToID(brick1x1x5Image)- 2;
$Inv[$Bricks,1] = "Brick 1x1x5";
$Inv[$Bricks++,0] = nameToID(brick1x2x5Image)- 2;
$Inv[$Bricks,1] = "Brick 1x2x5";
$Inv[$Bricks++,0] = nameToID(brick1x3x5Image)- 2;
$Inv[$Bricks,1] = "Brick 1x3x5";
$Inv[$Bricks++,0] = nameToID(brick1x4x5Image)- 2;
$Inv[$Bricks,1] = "Brick 1x4x5";
$Inv[$Bricks++,0] = nameToID(brick1x6x5Image)- 2;
$Inv[$Bricks,1] = "Brick 1x6x5";
$Inv[$Bricks++,0] = nameToID(brick1x8x5Image)- 2;
$Inv[$Bricks,1] = "Brick 1x8x5";
$Inv[$Bricks++,0] = nameToID(brick1x10x5Image)- 2;
$Inv[$Bricks,1] = "Brick 1x10x5";
$Inv[$Bricks++,0] = nameToID(brick1x12x5Image)- 2;
$Inv[$Bricks,1] = "Brick 1x12x5";

$EndBricks = $Bricks;

//------------Plates------------

$Plates = $Bricks + 1;
$StartPlates = $Plates;
$Inv[$Plates,0] = nameToID(plate1x1Image)- 2;
$Inv[$Plates,1] = "Plate 1x1";
$Inv[$Plates++,0] = nameToID(plate1x2Image)- 2;
$Inv[$Plates,1] = "Plate 1x2";
$Inv[$Plates++,0] = nameToID(plate1x3Image)- 2;
$Inv[$Plates,1] = "Plate 1x3";
$Inv[$Plates++,0] = nameToID(plate1x4Image)- 2;
$Inv[$Plates,1] = "Plate 1x4";
$Inv[$Plates++,0] = nameToID(plate1x6Image)- 2;
$Inv[$Plates,1] = "Plate 1x6";
$Inv[$Plates++,0] = nameToID(plate1x8Image)- 2;
$Inv[$Plates,1] = "Plate 1x8";
$Inv[$Plates++,0] = nameToID(plate1x10Image)- 2;
$Inv[$Plates,1] = "Plate 1x10";
$Inv[$Plates++,0] = nameToID(plate2x2Image)- 2;
$Inv[$Plates,1] = "Plate 2x2";
$Inv[$Plates++,0] = nameToID(plate2x3Image)- 2;
$Inv[$Plates,1] = "Plate 2x3";
$Inv[$Plates++,0] = nameToID(plate2x4Image)- 2;
$Inv[$Plates,1] = "Plate 2x4";
$Inv[$Plates++,0] = nameToID(plate2x6Image)- 2;
$Inv[$Plates,1] = "Plate 2x6";
$Inv[$Plates++,0] = nameToID(plate2x8Image)- 2;
$Inv[$Plates,1] = "Plate 2x8";
$Inv[$Plates++,0] = nameToID(plate2x10Image)- 2;
$Inv[$Plates,1] = "Plate 2x10";
$Inv[$Plates++,0] = nameToID(plate4x6Image)- 2;
$Inv[$Plates,1] = "Plate 4x6";
$Inv[$Plates++,0] = nameToID(plate4x8Image)- 2;
$Inv[$Plates,1] = "Plate 4x8";
$Inv[$Plates++,0] = nameToID(plate6x10Image)- 2;
$Inv[$Plates,1] = "Plate 6x10";
$Inv[$Plates++,0] = nameToID(plate6x12Image)- 2;
$Inv[$Plates,1] = "Plate 6x12";
$Inv[$Plates++,0] = nameToID(plate16x32Image)- 2;
$Inv[$Plates,1] = "Plate 16x32";
$Inv[$Plates++,0] = nameToID(baseplate32Image)- 2;
$Inv[$Plates,1] = "Plate 32x32";
$Inv[$Plates++,0] = nameToID(baseplate48Image)- 2;
$Inv[$Plates,1] = "Plate 48x48";
$Inv[$Plates++,0] = nameToID(roadxImage)- 2;
$Inv[$Plates,1] = "Cross Road";
$Inv[$Plates++,0] = nameToID(roadTImage)- 2;
$Inv[$Plates,1] = "T Road";
$Inv[$Plates++,0] = nameToID(roadstraightxwImage)- 2;
$Inv[$Plates,1] = "Straight Road w Crosswalk";
$Inv[$Plates++,0] = nameToID(roadstraightImage)- 2;
$Inv[$Plates,1] = "Straight Road";
$Inv[$Plates++,0] = nameToID(roaddeadendImage)- 2;
$Inv[$Plates,1] = "Dead End Road";
$Inv[$Plates++,0] = nameToID(roadcurveImage)- 2;
$Inv[$Plates,1] = "Curved Road";
$Inv[$Plates++,0] = nameToID(tile1x1Image)- 2;
$Inv[$Plates,1] = "Tile 1x1";
$Inv[$Plates++,0] = nameToID(tile1x2Image)- 2;
$Inv[$Plates,1] = "Tile 1x2";
$Inv[$Plates++,0] = nameToID(tile2x2Image)- 2;
$Inv[$Plates,1] = "Tile 2x2";
$Inv[$Plates++,0] = nameToID(plate1x2s1Image)- 2;
$Inv[$Plates,1] = "Plate 1x2s1";
$EndPlates = $Plates;

//---------Slopes-----------------

$Slopes = $Plates + 1;
$StartSlopes = $Slopes;
$Inv[$Slopes,0] = nameToID(slope2x1Image)- 2;
$Inv[$Slopes,1] = "Slope 2x1";
$Inv[$Slopes++,0] = nameToID(slope2x2Image)- 2;
$Inv[$Slopes,1] = "Slope 2x2";
$Inv[$Slopes++,0] = nameToID(slope2x3Image)- 2;
$Inv[$Slopes,1] = "Slope 2x3";
$Inv[$Slopes++,0] = nameToID(slope2x4Image)- 2;
$Inv[$Slopes,1] = "Slope 2x4";
$Inv[$Slopes++,0] = nameToID(slope3x1Image)- 2;
$Inv[$Slopes,1] = "Slope 3x1";
$Inv[$Slopes++,0] = nameToID(slope3x2Image)- 2;
$Inv[$Slopes,1] = "Slope 3x2";
$Inv[$Slopes++,0] = nameToID(slope3x3Image)- 2;
$Inv[$Slopes,1] = "Slope 3x3";
$Inv[$Slopes++,0] = nameToID(slope3x4Image)- 2;
$Inv[$Slopes,1] = "Slope 3x4";

$Inv[$Slopes++,0] = nameToID(slopeC2x2Image)- 2;
$Inv[$Slopes,1] = "Slope C2x2";
$Inv[$Slopes++,0] = nameToID(slopeC3x3Image)- 2;
$Inv[$Slopes,1] = "Slope C2x3";
$Inv[$Slopes++,0] = nameToID(slopeCC2x2Image)- 2;
$Inv[$Slopes,1] = "Slope CC2x4";
$Inv[$Slopes++,0] = nameToID(slopeCC3x3Image)- 2;
$Inv[$Slopes,1] = "Slope CC3x3";
$Inv[$Slopes++,0] = nameToID(slopeI2x1Image)- 2;
$Inv[$Slopes,1] = "Slope I2x1";
$Inv[$Slopes++,0] = nameToID(slopeI2x2Image)- 2;
$Inv[$Slopes,1] = "Slope I2x2";

$Inv[$Slopes++,0] = nameToID(slopeI2x3Image)- 2;
$Inv[$Slopes,1] = "Slope I2x3";
$Inv[$Slopes++,0] = nameToID(slopeI2x4Image)- 2;
$Inv[$Slopes,1] = "Slope I2x4";
$Inv[$Slopes++,0] = nameToID(slopeI3x1Image)- 2;
$Inv[$Slopes,1] = "Slope I3x1";
$Inv[$Slopes++,0] = nameToID(slopeI3x2Image)- 2;
$Inv[$Slopes,1] = "Slope I3x2";
$Inv[$Slopes++,0] = nameToID(slopeI3x3Image)- 2;
$Inv[$Slopes,1] = "Slope I3x3";
$Inv[$Slopes++,0] = nameToID(slopeI3x4Image)- 2;
$Inv[$Slopes,1] = "Slope I3x4";

$Inv[$Slopes++,0] = nameToID(slopeIC2x2Image)- 2;
$Inv[$Slopes,1] = "Slope IC2x2";
$Inv[$Slopes++,0] = nameToID(slopeIC3x3Image)- 2;
$Inv[$Slopes,1] = "Slope IC3x3";

$Inv[$Slopes++,0] = nameToID(slopeT2x1eImage)- 2;
$Inv[$Slopes,1] = "Slope T2x1e";
$Inv[$Slopes++,0] = nameToID(slopeT2x1Image)- 2;
$Inv[$Slopes,1] = "Slope T2x1";
$Inv[$Slopes++,0] = nameToID(slopeT2x2Image)- 2;
$Inv[$Slopes,1] = "Slope T2x2";
$Inv[$Slopes++,0] = nameToID(slopeT2x3Image)- 2;
$Inv[$Slopes,1] = "Slope T2x3";
$Inv[$Slopes++,0] = nameToID(slopeT2x4Image)- 2;
$Inv[$Slopes,1] = "Slope T2x4";

$Inv[$Slopes++,0] = nameToID(brick2x1x2slopeImage)- 2;
$Inv[$Slopes,1] = "Slope 2x1x2";
$Inv[$Slopes++,0] = nameToID(brick2x1x3slopeImage)- 2;
$Inv[$Slopes,1] = "Slope 2x1x3";
$Inv[$Slopes++,0] = nameToID(brick2x1x3slopeInvImage)- 2;
$Inv[$Slopes,1] = "Slope I2x1x3";
$Inv[$Slopes++,0] = nameToID(brick2x2x2slopeImage)- 2;
$Inv[$Slopes,1] = "Slope 2x2x2";
$Inv[$Slopes++,0] = nameToID(brick2x2x2slopeQCImage)- 2;
$Inv[$Slopes,1] = "Slope 2x2x2QC";

$Inv[$Slopes++,0] = nameToID(brick2x2x3slopeImage)- 2;
$Inv[$Slopes,1] = "Slope 2x2x3";
$Inv[$Slopes++,0] = nameToID(brick2x2x3slopeDCImage)- 2;
$Inv[$Slopes,1] = "Slope 2x2x3DC";

$Inv[$Slopes++,0] = nameToID(slope6x1x5Image)- 2;
$Inv[$Slopes,1] = "Slope 6x1x5";
$Inv[$Slopes++,0] = nameToID(slope2x3x1Image)- 2;
$Inv[$Slopes,1] = "Slope 2x3x1";
$Inv[$Slopes++,0] = nameToID(slope2x3x1bImage)- 2;
$Inv[$Slopes,1] = "Slope 2x3x1b";
$Inv[$Slopes++,0] = nameToID(slope1x1Image)- 2;
$Inv[$Slopes,1] = "Slope 1x1";
$Inv[$Slopes++,0] = nameToID(slope1x3roundImage)- 2;
$Inv[$Slopes,1] = "Slope 1x3 rounded";
$Inv[$Slopes++,0] = nameToID(brick2x3archImage)- 2;
$Inv[$Slopes,1] = "Brick 2x3 Arched";

//---> No obeject, only collision mesh!
//$Inv[$Slopes++,0] = nameToID(brick2x2x3slopeInvImage)- 2;
//$Inv[$Slopes,1] = "Slope I2x2x3";


$EndSlopes = $Slopes;

//------Misc-----

$Misc = $Slopes + 1;
$StartMisc = $Misc;
$Inv[$Misc,0] = nameToID(brick1x2x2windowImage)- 2;
$Inv[$Misc,1] = "Window 1x2x2";
$Inv[$Misc++,0] = nameToID(brick1x3x2windowImage)- 2;
$Inv[$Misc,1] = "Window 1x3x2";
$Inv[$Misc++,0] = nameToID(brick1x4x2windowImage)- 2;
$Inv[$Misc,1] = "Window 1x4x2";
$Inv[$Misc++,0] = nameToID(brick1x4x5windowImage)- 2;
$Inv[$Misc,1] = "Window 1x4x5";
$Inv[$Misc++,0] = nameToID(brick1x4x6windowImage)- 2;
$Inv[$Misc,1] = "Window 1x4x6";
$Inv[$Misc++,0] = nameToID(brick3x8x6windowImage)- 2;
$Inv[$Misc,1] = "Window 3x8x6";
$Inv[$Misc++,0] = nameToID(brick2x4x3windowImage)- 2;
$Inv[$Misc,1] = "Window 2x4x3";
$Inv[$Misc++,0] = nameToID(cylinder1x1ftImage) - 2;
$Inv[$Misc,1] = "Cylinder 1x1ft";
$Inv[$Misc++,0] = nameToID(cylinder1x1fImage)- 2;
$Inv[$Misc,1] = "Cylinder 1x1f b";
$Inv[$Misc++,0] = nameToID(cylinder1x1fpImage)- 2;
$Inv[$Misc,1] = "Cylinder 1x1f ";
$Inv[$Misc++,0] = nameToID(cylinder1x1tImage)- 2;
$Inv[$Misc,1] = "Cylinder 1x1t";
$Inv[$Misc++,0] = nameToID(cylinder1x1Image)- 2;
$Inv[$Misc,1] = "Cylinder 1x1 b";
$Inv[$Misc++,0] = nameToID(cylinder1x1pImage)- 2;
$Inv[$Misc,1] = "Cylinder 1x1";
$Inv[$Misc++,0] = nameToID(tile2x2roundImage)- 2;
$Inv[$Misc,1] = "Tile 2x2 round";
$Inv[$Misc++,0] = nameToID(cylinder2x2Image)- 2;
$Inv[$Misc,1] = "Cylinder 2x2";
$Inv[$Misc++,0] = nameToID(brick4x4qcylImage)- 2;
$Inv[$Misc,1] = "Cylinder 4x4q";
$Inv[$Misc++,0] = nameToID(rocket1x1Image)- 2;
$Inv[$Misc,1] = "Cone 1x1";
$Inv[$Misc++,0] = nameToID(rocket1x1tImage)- 2;
$Inv[$Misc,1] = "Cone 1x1 t";
$Inv[$Misc++,0] = nameToID(cone2x2x2Image)- 2;
$Inv[$Misc,1] = "Cone 2x2x2";
$Inv[$Misc++,0] = nameToID(cone3x3x2Image)- 2;
$Inv[$Misc,1] = "Cone 3x3x2";
$EndMisc = $Misc;

$Misc2 = $Misc + 1;
$StartMisc2 = $Misc2;
$Inv[$Misc2,0] = nameToID(brickfence1Image)- 2;
$Inv[$Misc2,1] = "Fence";
$Inv[$Misc2++,0] = nameToID(fence1x4x2Image)- 2;
$Inv[$Misc2,1] = "Fence 1x4x2";
$Inv[$Misc2++,0] = nameToID(brick1x4fenceImage)- 2;
$Inv[$Misc2,1] = "Fence 1x4";
$Inv[$Misc2++,0] = nameToID(brick1x4x2fencespindledImage)- 2;
$Inv[$Misc2,1] = "Fence 1x4x2S";
$Inv[$Misc2++,0] = nameToID(brick1x4walkImage)- 2;
$Inv[$Misc2,1] = "1x4 Walk";
$Inv[$Misc2++,0] = nameToID(bar1x4x2Image)- 2;
$Inv[$Misc2,1] = "Bar 1x4x2";
$Inv[$Misc2++,0] = nameToID(bar1x3Image)- 2;
$Inv[$Misc2,1] = "Bar 1x3";
$Inv[$Misc2++,0] = nameToID(brick2x2x5LatticeImage)- 2;
$Inv[$Misc2,1] = "Lattice 2x2x5";
$Inv[$Misc2++,0] = nameToID(brick1x5x4archImage)- 2;
$Inv[$Misc2,1] = "Arch 1x5x4";
$Inv[$Misc2++,0] = nameToID(brick1x5x4archinvertedImage)- 2;
$Inv[$Misc2,1] = "Arch 1x5x4I";
$Inv[$Misc2++,0] = nameToID(brick1x12x3archImage)- 2;
$Inv[$Misc2,1] = "Arch 1x12x3";
$Inv[$Misc2++,0] = nameToID(brick1x3archImage)- 2;
$Inv[$Misc2,1] = "Arch 1x3";
$Inv[$Misc2++,0] = nameToID(brick1x4archImage)- 2;
$Inv[$Misc2,1] = "1x4 Arch";
$Inv[$Misc2++,0] = nameToID(brick1x4x2archImage)- 2;
$Inv[$Misc2,1] = "1x4x2 Arch";
$Inv[$Misc2++,0] = nameToID(brick1x6archImage)- 2;
$Inv[$Misc2,1] = "1x6 Arch";
$Inv[$Misc2++,0] = nameToID(brick1x6x2archImage)- 2;
$Inv[$Misc2,1] = "1x6x2 Arch";
$Inv[$Misc2++,0] = nameToID(brick1x8x2archImage)- 2;
$Inv[$Misc2,1] = "1x8x2 Arch";
$Inv[$Misc2++,0] = nameToID(brick2x8x3archImage)- 2;
$Inv[$Misc2,1] = "Arch 2x8x3";
$Inv[$Misc2++,0] = nameToID(brick1x6x2archroundImage)- 2;
$Inv[$Misc2,1] = "1x6x2 Curved Arch";
$Inv[$Misc2++,0] = nameToID(brick1x6x3archcurveImage)- 2;
$Inv[$Misc2,1] = "1x6x3 Curved Arch";
$Inv[$Misc2++,0] = nameToID(brick1x3x2archcurveImage)- 2;
$Inv[$Misc2,1] = "1x3x2 Curved Arch";
$Inv[$Misc2++,0] = nameToID(brick2x1x1curveImage)- 2;
$Inv[$Misc2,1] = "Brick 2x1x1 curve";
$EndMisc2 = $Misc2;

$Misc3 = $Misc2 + 1;
$StartMisc3 = $Misc3;
$Inv[$Misc3,0] = nameToID(brickantennaImage)- 2;
$Inv[$Misc3,1] = "Antenna";
$Inv[$Misc3++,0] = nameToID(brickantennatImage)- 2;
$Inv[$Misc3,1] = "Antenna t";
$Inv[$Misc3++,0] = nameToID(antenna1x1x8Image)- 2;
$Inv[$Misc3,1] = "Antenna1x1x8";
$Inv[$Misc3++,0] = nameToID(flag1Image)- 2;
$Inv[$Misc3,1] = "Flag";
$Inv[$Misc3++,0] = nameToID(brick1x2handleImage)- 2;
$Inv[$Misc3,1] = "Handle 1x2";
$Inv[$Misc3++,0] = nameToID(brick1x1handleImage)- 2;
$Inv[$Misc3,1] = "Brick 1x1 Handle";
$Inv[$Misc3++,0] = nameToID(plate1x2handleImage)- 2;
$Inv[$Misc3,1] = "Plate 1x2 Handle";
$Inv[$Misc3++,0] = nameToID(plate1x2handlesImage)- 2;
$Inv[$Misc3,1] = "Plate 1x2 Handles";
$Inv[$Misc3++,0] = nameToID(plate1x2hookImage)- 2;
$Inv[$Misc3,1] = "Plate 1x2 Hook";
$Inv[$Misc3++,0] = nameToID(plate1x2jeImage)- 2;
$Inv[$Misc3,1] = "Plate 1x2 Jetengine";
$Inv[$Misc3++,0] = nameToID(plate1x2s1image)- 2;
$Inv[$Misc3,1] = "Plate 1x2 s1";
$Inv[$Misc3++,0] = nameToID(grill1x2Image)- 2;
$Inv[$Misc3,1] = "1x2 Grill";
$Inv[$Misc3++,0] = nameToID(brick8x8fgrillImage)- 2;
$Inv[$Misc3,1] = "8x8f Grill";
$Inv[$Misc3++,0] = nameToID(brickcubImage)- 2;
$Inv[$Misc3,1] = "Cupboard";
$Inv[$Misc3++,0] = nameToID(brickdoorImage)- 2;
$Inv[$Misc3,1] = "Door";
$Inv[$Misc3++,0] = nameToID(brickfausetImage)- 2;
$Inv[$Misc3,1] = "Fauset(Tap)";
$Inv[$Misc3++,0] = nameToID(brickseatImage)- 2;
$Inv[$Misc3,1] = "Seat";
$Inv[$Misc3++,0] = nameToID(rearwingImage)- 2;
$Inv[$Misc3,1] = "Rear Wing";
$Inv[$Misc3++,0] = nameToID(brickGateImage)- 2;
$Inv[$Misc3,1] = "Gate";
$Inv[$Misc3++,0] = nameToID(brickbar1Image)- 2;
$Inv[$Misc3,1] = "Bar";
$Inv[$Misc3++,0] = nameToID(wing8x4lImage)- 2;
$Inv[$Misc3,1] = "Wing 8x4l";
$Inv[$Misc3++,0] = nameToID(wing8x4rImage)- 2;
$Inv[$Misc3,1] = "Wing 8x4r";
$Inv[$Misc3++,0] = nameToID(wing9x4Image)- 2;
$Inv[$Misc3,1] = "Wing 9x4";
$Inv[$Misc3++,0] = nameToID(dynamiteImage)- 2;
$Inv[$Misc3,1] = "Dynamite";
$Inv[$Misc3++,0] = nameToID(plungerImage)- 2;
$Inv[$Misc3,1] = "Plunger";
$Inv[$Misc3++,0] = nameToID(panelwtImage)- 2;
$Inv[$Misc3,1] = "PanelWall6x12t";
$Inv[$Misc3++,0] = nameToID(CastleTurretTop4x8x21thirdImage)- 2;
$Inv[$Misc3,1] = "CastleTop";
$Inv[$Misc3++,0] = nameToID(CastleWall2x5x6Image)- 2;
$Inv[$Misc3,1] = "CastleWall";
$EndMisc3 = $Misc3;

$Misc4 = $Misc3 + 1;
$StartMisc4 = $Misc4;
$Inv[$Misc4,0] = nameToID(brickpinetreeImage)- 2;
$Inv[$Misc4,1] = "Pine Tree Brick";
$Inv[$Misc4++,0] = nameToID(brickflowerImage)- 2;
$Inv[$Misc4,1] = "Flowers Brick";
$Inv[$Misc4++,0] = nameToID(brickcatImage)- 2;
$Inv[$Misc4,1] = "Cat Brick";
$Inv[$Misc4++,0] = nameToID(brickmonkeyImage)- 2;
$Inv[$Misc4,1] = "Monkey Brick";
$Inv[$Misc4++,0] = nameToID(brickcupImage)- 2;
$Inv[$Misc4,1] = "Cup Brick";
$Inv[$Misc4++,0] = nameToID(brickGobletImage)- 2;
$Inv[$Misc4,1] = "Goblet tBrick";
$Inv[$Misc4++,0] = nameToID(brickGoblet2Image)- 2;
$Inv[$Misc4,1] = "Goblet2 Brick";
$Inv[$Misc4++,0] = nameToID(brickbookImage)- 2;
$Inv[$Misc4,1] = "Book Brick";
$Inv[$Misc4++,0] = nameToID(brickwandImage)- 2;
$Inv[$Misc4,1] = "Wand Brick";
$Inv[$Misc4++,0] = nameToID(brickdrillImage)- 2;
$Inv[$Misc4,1] = "Drill Brick";
$Inv[$Misc4++,0] = nameToID(brickscrewdriverImage)- 2;
$Inv[$Misc4,1] = "Screwdriver Brick";
$Inv[$Misc4++,0] = nameToID(brickhammerImage)- 2;
$Inv[$Misc4,1] = "Hammer Brick";
$Inv[$Misc4++,0] = nameToID(brickwrenchImage)- 2;
$Inv[$Misc4,1] = "Wrench Brick";
$EndMisc4 = $Misc4;


$Special = $Misc4 + 1;
$StartSpecial = $Special;
$Inv[$Special,0] = nameToID(editorWandImage) - 1;
$Inv[$Special,1] = "Editor Wand";
$Inv[$Special++,0] = nameToID(brick2x2FXImage)- 2;
$Inv[$Special,1] = "FX Brick";
$Inv[$Special++,0] = nameToID(onewaybrickImage)- 2;
$Inv[$Special,1] = "Observer";
$Inv[$Special++,0] = nameToID(WandImage) - 1;
$Inv[$Special,1] = "Magic Wand";
$EndSpecial = $Special;


function GameConnection::spawnPlayer(%this)
{
   // Combination create player and drop him somewhere
   %spawnPoint = pickSpawnPoint();
   %this.createPlayer(%spawnPoint);
	if($Pref::Server::UseInventory == 1)
	{
   		serverCmdAddtoInvent(%this,1,$StartPlates);
  		serverCmdAddtoInvent(%this,2,$StartSlopes);
  		serverCmdAddtoInvent(%this,3,$StartMisc);
  		serverCmdAddtoInvent(%this,4,$StartMisc2);
  		serverCmdAddtoInvent(%this,5,$StartMisc3);
  		serverCmdAddtoInvent(%this,6,$StartMisc4);
   		serverCmdAddtoInvent(%this,7,$StartTools);
   		serverCmdAddtoInvent(%this,8,$StartSprayCans);
		if(%this.isEwanduserBBC || %this.isEWandUser || %this.isAdmin || %this.isSuperAdmin)
		{
   			serverCmdAddtoInvent(%this,9,$StartSpecial);
		}
   		serverCmdAddtoInvent(%this,0,$StartBricks);
   		servercmdFreeHands(%this);
messageClient(%this, 'MsgItemPickup', '', 0, "Bricks");
messageClient(%this, 'MsgItemPickup', '', 1, "Plates");
messageClient(%this, 'MsgItemPickup', '', 2, "Slopes");
messageClient(%this, 'MsgItemPickup', '', 3, "Windows/Cylinders");
messageClient(%this, 'MsgItemPickup', '', 4, "Fences/Arches");
messageClient(%this, 'MsgItemPickup', '', 5, "Special Bricks");
messageClient(%this, 'MsgItemPickup', '', 6, "Other");
messageClient(%this, 'MsgItemPickup', '', 7, "Tools/Weapons");
messageClient(%this, 'MsgItemPickup', '', 8, "Coloring Tools");
messageClient(%this, 'MsgItemPickup', '', 9, "Special Objects");
	}
}