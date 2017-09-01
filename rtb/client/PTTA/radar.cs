//This script is edited in Brain Editor Pro - v1.0.8 DEMO
//Client side Radar script
//By BlueGreen
//Version 2.0 BETA version 4.0
//March 24, 2006

//Client Features Only are listed here

//Working
//show/hide radar commands
//Displays blips
//Displays North (the little N, +Y direction)
//Only shows up if radar is active on server
//repositon/resize radar hud commands
//shortcut keys to make interaction with the radar easier
//Range support for radar ranges not equal to 100
//Autoposition support for resolutions other than 800x600

//Not yet added
//Gui interface to make interaction with the radar easier

$radarBlips = 0;
$OwnBlip = "0 0 0 0 0 0";
$radarArrayBlip[0] = 0;
$radarAdded = 0;
$ignoreRadar = 0;
$clientradarrunning = 0;
$northBlip = 0;
$radardetect = 0;
$autoshutoff = 0;
$currentRange = 100;
$radarData = "";
$zooming = 0;

//Client Options
$radarSize = 1;		//Large by default
$autoZoom = 1;		//Enabled by default
$minRange = 100;	//Minimum range, also the range used when autoZoom is off
$maxRange = 3200;	//$minRange * ($zoomFactor ^ 5)
$zoomFactor = 2;	//Factor used when zooming in/out
$zoomThreshold = 0.05;  //Factor used when determining when to zoom in/out  (like if range is currently 1600, then average distance must be < 1200 or > 2000 before zoom happens)

exec("~/client/ui2/GuiRadar.gui");

addMessageCallback('RadarOff', handleRadarOff);
addMessageCallback('UpdateRadar', handleUpdateRadar);

//moveMap.bindCmd(keyboard, "ctrl r", "", "GuiRadar.toggle();");


//a simple command added to give an in game readme until there is a gui
function radarhelp()
{
	echo("BlueGreen's Radar 2.0 help");
	echo("--------------------------");
	echo("CLIENT COMMANDS:");
	echo("toggleRadar() - Turns the radar display on/off");
	echo("toggleRadarSize() - Changes the size of the radar display big/small");
	echo("toggleAutoZoom() - Turns autozooming on/off (this will lock the range to whatever it was when you turned it off)");
	echo("");
	echo("SERVER COMMANDS:  (You must be an admin to use)");
	echo("commandToServer('RadarStart', 1) - Starts the radar");
	echo("commandToServer('RadarStop') - Stops the radar and hides the hud for all clients");
	echo("commandToServer('HideFromRadar', %obj) - Hides %obj from radar where %obj is a client (not a player)");
	echo("commandToServer('UnhideFromRadar', %obj) - Reverses the effect of HideFromRadar");
	echo("commandToServer('ShowAdminsOnRadar') - Admins appear as normal blips on radar");
	echo("commandToServer('MarkAdminsOnRadar') - Admins appear on the radar with special blips");
	echo("commandToServer('HideAdminsFromRadar') - Admins will not appear on radar");
	echo("commandToServer('AddRadarPosition', %name, %position, %color) - Add a position blip to the radar for %position (x y z) with name %name and color %color, use N, R, G, B, or Y for color");
	echo("commandToServer('AddMyRadarPosition', %name) - Adds your position blip to the radar using your team color and the given name");
	echo("commandToServer('RemoveRadarPosition', %name) - Removes the position from radar with that name");
	echo("");
	echo("NOTES:");
	echo("The radar supports up to 4 different teams, the first team created is given the color blue, the next is given red, then green, and then yellow");
	echo("Anyone not assigned to a team or assigned to the fifth or greater team is given the color grey");
	echo("By default, Admins are marked on radar (meaning they have the special blip for admins)");
}

function autoShutoff()
{
	$autoshutoff = true;
	if($clientRadarRunning && $radarDetect)
	{
		$radarDetect = 0;
		schedule(5000, 0, autoShutoff);
		return;
	}
	Radar.visible = 0;
	$clientradarrunning = false;
	$autoshutoff = false;
}

function toggleRadar()
{
	if($ignoreRadar)
	{
		$ignoreRadar = 0;
	}
	else
	{
		$ignoreRadar = 1;
	}
}

function toggleAutoZoom()
{
	if($autoZoom)
	{
		$autoZoom = 0;
		RadarZoom.visible = 0;
	}
	else
	{
		$autoZoom = 1;
		RadarZoom.visible = 1;
	}
}

function toggleRadarSize()
{
	%extent = getWord(PlayGui.extent, 0) - (205 - 100 * $radarSize) @ " 32";

	if($radarSize)
	{
		$radarSize = 0;

		if($radarAdded)
		{
			Radar.delete();
			$northBlip.delete();
		}

		new GuiBitmapCtrl(Radar) {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = %extent;
			extent = "100 100";
			minExtent = "100 100";
			visible = $clientradarrunning;
			helpTag = "0";
			bitmap = "~/client/ui2/radar/radar2.png";
			wrap = "0";
		};

		PlayGui.add(Radar);

		$northBlip = new GuiBitmapCtrl() {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "5 5";
			extent = "10 10";
			minExtent = "10 10";
			visible = "0";
			helpTag = "0";
			bitmap = "~/client/ui2/radar/north.png";
			wrap = "0";
		};

		Radar.add($northBlip);

		new GuiTextCtrl(RadarZoom) {
			profile = "InventoryTextProfile";
			horizSizing = "left";
			vertSizing = "bottom";
			position = "84 0";
			extent = "15 12";
			minExtent = "15 12";
			visible = "1";
			helpTag = "0";
			text = "";
			maxLength = "255";
		};

		Radar.add(RadarZoom);

		$radarAdded = 1;
	}
	else
	{
		$radarSize = 1;

		if($radarAdded)
		{
			Radar.delete();
			$northBlip.delete();
		}

		new GuiBitmapCtrl(Radar) {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = %extent;
			extent = "200 200";
			minExtent = "200 200";
			visible = $clientradarrunning;
			helpTag = "0";
			bitmap = "~/client/ui2/radar/radar.png";
			wrap = "0";
		};
		PlayGui.add(Radar);

		$northBlip = new GuiBitmapCtrl() {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "5 5";
			extent = "10 10";
			minExtent = "10 10";
			visible = "0";
			helpTag = "0";
			bitmap = "~/client/ui2/radar/north.png";
			wrap = "0";
		};

		Radar.add($northBlip);

		new GuiTextCtrl(RadarZoom) {
			profile = "InventoryTextProfile";
			horizSizing = "left";
			vertSizing = "bottom";
			position = "184 0";
			extent = "15 12";
			minExtent = "15 12";
			visible = "1";
			helpTag = "0";
			text = "";
			maxLength = "255";
		};

		Radar.add(RadarZoom);

		$radarAdded = 1;
	}
}

function autoZoom(%progress, %oldrange, %newrange, %blips)
{
	$zooming = 1;
	$currentRange = %oldrange + %progress * (%newrange - %oldrange);
	handleUpdateRadar(0, "", $ownBlip, $currentRange + 10, %blips, $radarData);
	RadarZoom.setValue($currentRange / $minRange);
	if(%progress < 1)
	{
		schedule(20, 0, autoZoom, %progress + 0.1, %oldrange, %newrange, %blips);
		toggleRadarSize();
		toggleRadarSize();
	}
	else
	{
		$zooming = 0;
	}
}

function handleUpdateRadar(%msgType, %dummy, %myPos, %range, %numBlips, %blipList)
{
	//echo("Update radar.");
	$radarDetect = 1;
	$clientradarrunning = true;
	$radarData = %blipList;

	//Detects is radar is running, hides radar if it isn't
	if(!$autoshutoff) autoshutoff();

	//Creates radar if this is the first time it is needed
	if(!$radarAdded)
	{
		if($radarSize)
		{
			$radarSize = 0;
		}
		else
		{
			$radarSize = 1;
		}
		toggleRadarSize();
	}

	if($ignoreRadar)
	{
		Radar.visible = false;
	}
	else
	{
		Radar.visible = true;
	}

	//Reposition radar if client changes window size
	%extent = getWord(PlayGui.extent, 0) - (105 + 100 * $radarSize) @ " 32";
	Radar.position = %extent;

	//Sets the edge and range of the radar for GUI purposes
	%radarRange = 0;
	if(%range > $currentRange || %range == 0)
	{
		%radarRange = $currentRange;
	}
	else
	{
		%radarRange = %range;
	}
	%radarEdge = (100 / (2 - $radarSize)) - 5;

	//Displays North
	$OwnBlip = %myPos;
	%ang = getWord($OwnBlip, 5) * getWord($OwnBlip, 6);
	%newx = mfloor(mcos(%ang) * %radarEdge + %radarEdge);
	%newy = mfloor(%radarEdge - msin(%ang) * %radarEdge);
	$northBlip.position = %newx @ " " @ %newy;
	$northBlip.visible = 1;

	%avgdist = 0;

	//Updates the radar with the new blip update
	for(%index = 0; %index < %numBlips; %index++)
	{
		$radarArrayBlip[%index].schedule(10, "delete");

		%pos = getWord(%blipList, %index * 3) @ " " @ getWord(%blipList, %index * 3 + 1);
		%type = getWord(%blipList, %index * 3 + 2);
		
		%newBlip = new GuiBitmapCtrl() {
	         profile = "GuiDefaultProfile";
	         horizSizing = "right";
	         vertSizing = "bottom";
	         position = "5 5";
	         extent = "10 10";
	         minExtent = "10 10";
	         visible = "0";
	         helpTag = "0";
	         bitmap = "~/client/ui2/radar/blip" @ %type @ ".png";
	         wrap = "0";
		};
	
		Radar.add(%newBlip);

		$radarArrayBlip[$radarBlips] = %newBlip;
		$radarBlips++;
		//echo("blip added:" @ %newBlip);

		%dist = %radarEdge * (VectorDist(getwords($OwnBlip, 0, 1), getwords(%pos, 0, 1)) / %radarRange);

		%avgdist = %avgdist + VectorDist(getwords($OwnBlip, 0, 1), getwords(%pos, 0, 1));
	
		if(%dist > %radarEdge) %dist = %radarEdge;
	
		//echo("distance: " @ %dist);
	
		%diff = VectorSub(%pos, $OwnBlip);
	
		//echo("difference: " @ %diff);
	
		%ang = mAtan(getWord(%diff, 1), getWord(%diff, 0)) + getWord($OwnBlip, 5) * getWord($OwnBlip, 6);
	
		//echo("ang: " @ mradtodeg(%ang));

		%newx = mfloor(mcos(%ang) * %dist + %radarEdge);
		%newy = mfloor(%radarEdge - msin(%ang) * %dist);
		%newpos = %newx @ " " @ %newy;
	
		//echo("new pos: " @ %newpos);
	
		%newBlip.position = %newx @ " " @ %newy;
		%newBlip.visible = 1;
	}

	//handles auto zoom
	RadarZoom.setValue($currentRange / $minRange);
	if((!$zooming) && $autozoom && ($radarBlips > 1))
	{
		%avgdist = %avgdist / ($radarBlips - 1);

		if(%avgdist > ($currentrange + $currentrange * $zoomThreshold))
		{
			if(%range && ($currentrange < $maxRange))
			{
				if(%range >= ($currentrange * $zoomFactor))
				{
					autoZoom(0, $currentrange, $currentrange * $zoomFactor, $radarBlips);
				}
				else
				{
					autoZoom(0, $currentrange, %range, $radarBlips);
				}
			}
			else if($maxRange >= ($currentrange * $zoomFactor))
			{
				autoZoom(0, $currentrange, $currentrange * $zoomFactor, $radarBlips);
			}
		}
		else if(%avgdist < ($currentrange / $zoomFactor))
		{
			if($minRange <= ($currentRange / $zoomFactor))
			{
				autoZoom(0, $currentrange, ($currentRange / $zoomFactor), $radarBlips);
			}
		}
	}

	//Deletes unused blips from last update
	for(%index = $radarBlips; %index < $oldradarBlips; %index++)
	{
		$radarArrayBlip[%index].schedule(10, "delete");
		//echo("blip deleted:" @ $radarArrayBlip[%index]);
	}

	$oldRadarBlips = $radarBlips;
	$radarBlips = 0;
}

function handleRadarOff(%msgType)
{
	Radar.visible = 0;
	$clientradarrunning = false;
}