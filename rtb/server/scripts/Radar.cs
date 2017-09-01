//Sever side Radar script
//By BlueGreen
//Version 2.0 BETA version 4.0
//March 24, 2006

//Server Features Only are listed here

//Working
//Team colors, first four teams created are given a color:  Blue, Red, Green, or Yellow.  (First team created gets blue, Second gets Red, etc.)
//Admins on radar/hidden/marked on radar (works even if on team)
//Mode 1 (unlimited rage)
//Adding positions (Positions added show up as a blip with an x in it, use R, G, B, Y, or N for color)
//Cloaked Players do not show up on radar
//Auto start

//Not yet added
//Bot detection
//Vehicle detection


$RadarRunning = 0;			//0 = no, 1 = yes
$RadarRange = 0;			//range in blocks, only used in modes 2 and 3
$AdminsOnRadar = 2;			//0 = no, 1 = yes, 2 = yes with special blip
$PositionBlips = 0;			//number of position blips
$PositionBlipName[0] = "default";	//names of position blips
$PositionBlip[0] = "0 0 0";		//position blip locaions
$PositionBlipColor[0] = "N";		//position blip colors
$RadarProcessCount = 0;
$RadarRate = 100;			//Default value.  Don't go too low or bad things will happen.


schedule(10000, 0, initRadar);

function initRadar()
{
	if(ClientGroup.getObject("").player)
	{
		if($RadarRunning) return;
	
		$RadarRunning = 1;		//turn on radar
		updateRadar(1);			//default mode is 1, unrestricted range
	}
	else
	{
			schedule(1000, 0, initRadar);
	}
}

function serverCmdRadarStart(%client, %mode)
{
	if($RadarRunning) return;

	if(%client.isAdmin || %client.isSuperAdmin)
	{
		$RadarRunning = 1;	//turn on radar
		updateRadar(%mode);	//default mode is 1, unrestricted range		
	}	
}

function serverCmdRadarStop(%client)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		messageAll('RadarOff');
		$RadarRunning = 0;	//turn off radar
	}	
}

function updateRadar(%mode)
{
	if(!$Game::Running)
	{
		$RadarRunning = 0;	//turn off radar
		messageAll('RadarOff');
		return;
	}
	//echo("Beep");
	$RadarProcessCount++;
	if($RadarProcessCount > 1)
	{
		echo("WARNING:  Radar overload detected.  Ignoring radar update to prevent crash.");
		$RadarProcessCount--;
		return;
	}

	if($RadarRunning)
	{
		schedule($RadarRate, 0, updateRadar, %mode);
	}

	if(!$RadarRunning)
	{
		$RadarProcessCount = 0;
		return;
	}

	if(%mode < 1 || %mode > 3) return;

	%count = clientGroup.getCount();

	if(%mode = 1)
	{
		%blipData = "";
		%blipCount = 0;

		for(%index = 0; %index < %count; %index++)
		{
			%client = clientGroup.getObject(%index);

			if(%client.player)
			{
				if(%client.team $= "")
				{
					%client.blipColor = "N";
				}
				else if(%client.team $= $Teams[0])
				{
					%client.blipColor = "B";
				}
				else if(%client.team $= $Teams[1])
				{
					%client.blipColor = "R";
				}
				else if(%client.team $= $Teams[2])
				{
					%client.blipColor = "G";
				}
				else if(%client.team $= $Teams[3])
				{
					%client.blipColor = "Y";
				}
				else
				{
					%client.blipColor = "N";
				}

				if(%client.isAdmin || %client.isSuperAdmin)
				{
					if($AdminsOnRadar == 0)		//yes
					{
						%client.blipType = "N";
					}
					else if($AdminsOnRadar == 1)	//no
					{
						%client.blipType = "0";
					}
					else if($AdminsOnRadar == 2)	//yes, but also indicated as admin
					{
						%client.blipType = "A";
					}
				}
				else
				{
					%client.blipType = "N";
				}

				if(!(%client.blipType $= "0"))
				{
					if((%client.hideFromRadar == 0) && (%client.player.isCloaked() == 0))
					{
						%blipData = %blipData @ getWord(%client.player.getTransform(), 0) @ " ";
						%blipData = %blipData @ getWord(%client.player.getTransform(), 1) @ " ";
						%blipData = %blipData @ %client.blipColor @ %client.blipType @ " ";
						%blipCount++;
					}
				}
			}
		}

		%client = clientGroup.getObject(0);

		for(%index = 0; %index < $PositionBlips; %index++)
		{
			%blipData = %blipData @ getWord($PositionBlip[%index], 0) @ " ";
			%blipData = %blipData @ getWord($PositionBlip[%index], 1) @ " ";
			%blipData = %blipData @ $PositionBlipColor[%index] @ "X" @ " ";
			%blipCount++;
		}

		for(%index = 0; %index < %count; %index++)
		{
			%client = clientGroup.getObject(%index);
			messageClient(%client, 'UpdateRadar', "", %client.player.getTransform(), $RadarRange, %blipCount, %blipData);
		}
	}

	$RadarProcessCount--;
}

function serverCmdSetRadarRange(%client, %range)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		$RadarRange = %range;
	}
}

function serverCmdHideFromRadar(%client, %obj)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		%obj.hideFromRadar = true;
	}
}

function serverCmdUnhideFromRadar(%client, %obj)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		%obj.hideFromRadar = false;
	}
}

function serverCmdShowAdminsOnRadar(%client)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		$AdminsOnRadar = 0;
	}
}

function serverCmdMarkAdminsOnRadar(%client)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		$AdminsOnRadar = 2;
	}
}

function serverCmdHideAdminsFromRadar(%client)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		$AdminsOnRadar = 1;
	}
}

function serverCmdAddRadarPosition(%client, %name, %position, %color)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		$PositionBlipName[$PositionBlips] = %name;	//name of position blip
		$PositionBlip[$PositionBlips] = %position;	//position blip locaion
		$PositionBlipColor[$PositionBlips] = %color;	//position blip color
		$PositionBlips++;
	}
}

function serverCmdAddMyRadarPosition(%client, %name)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		if(%client.player)  //can't add if the client isn't alive
		{
			$PositionBlipName[$PositionBlips] = %name;			//name of position blip
			$PositionBlip[$PositionBlips] = %client.player.getTransform();	//position blip locaion
			$PositionBlipColor[$PositionBlips] = %client.blipColor;		//position blip color
			$PositionBlips++;
		}
	}
}

function serverCmdRemoveRadarPosition(%client, %name)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		%number = -1;
	
		for(%index = 0; %index < $PositionBlips; %index++)
		{
			if($PositionBlipName[%index] $= %name)
			{
				%number = %index;
			}
		}
	
		if(%number == -1)
		{
			echo("That named position was not found, no blips were removed.");
			return;
		}
	
		for(%index = 0; %index < $PositionBlips - 1; %index++)
		{
			if(%index >= %number)
			{
				$PositionBlipName[%index] = $PositionBlipName[%index + 1];
				$PositionBlip[%index] = $PositionBlip[%index + 1];
				$PositionBlipColor[%index] = $PositionBlipColor[%index + 1];
			}
		}
		$PositionBlips--;
	}
}