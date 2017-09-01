//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Server Admin Commands
//-----------------------------------------------------------------------------


//#########################################
//# 04 STUFF
//#########################################
function login()
{
	%user = txtLIUsername.getValue();
	%pass = txtLIPassword.getValue();
	commandtoserver('Login',%user,%pass);
}

function register()
{
	%user = txtRUsername.getValue();
	%pass = txtRPassword.getValue();
	%conf = txtRConfirm.getValue();
	commandtoserver('Register',%user,%pass,%conf);
}

function applybFX()
{
	%selectedFX = $Pref::FX::Selection;
	if($Pref::FX::Selection $= 1)
	{
		commandtoserver('DeleteBrickFX1',%selectedFX);
		commandtoserver('applyBrickFX1',%selectedFX);
	}
	else
	{
	if($Pref::FX::Selection $= 2)
	{
		commandtoserver('DeleteBrickFX2',%selectedFX);
		commandtoserver('applyBrickFX2',%selectedFX);
	}
	else
	{
	if($Pref::FX::Selection $= 5)
	{
		commandtoserver('DeleteBrickFX5',%selectedFX);
		commandtoserver('applyBrickFX5',%selectedFX);
	}
          }
     }
}

function removeFX()
{
	%selectedFX = $Pref::FX::Selection;
	if($Pref::FX::Selection $= 1)
	{
		commandtoserver('DeleteBrickFX1',%selectedFX);
	}
	else
	{
	if($Pref::FX::Selection $= 2)
	{
		commandtoserver('DeleteBrickFX2',%selectedFX);
	}
	else
	{
	if($Pref::FX::Selection $= 5)
	{
		commandtoserver('DeleteBrickFX5',%selectedFX);
	}
          }
     }
}

function applyRating()
{
	%rating = $Pref::BBC::Rating;
	commandtoserver('rateBuild',%rating);
}

function clientcmdPush(%gui)
{
	canvas.pushDialog(%gui);
}

function clientcmdPop(%gui)
{
	canvas.popdialog(%gui);
}

function clientcmdOpenEditorA(%action)
{
	if(%action $= push)
	{
		canvas.pushDialog(InGameEditorGUI);
	}
	else if(%action $= pop)
	{
		canvas.popDialog(InGameEditorGUI);
	}
}

function clientCmdstopCDC()
{
	cancel($CDS);
	CDTime.visible = 0;
	CDBG.visible = 0;
	CDBGR.visible = 0;
}
function clientCmdsetCDC(%min)
{
	cancel($CDS);
	CDTime.visible = 1;
	CDBG.visible = 1;
	CDTime.setValue(%min@":00");
	$Min = %min;
	$Sec = "a";
	CDM();
}

function CDM()
{
	if($Sec $= "a")
	{
	$Min--;
	$Sec = "59";
	}
	else
	{
	$Sec--;
	}

	if($Sec $= "0" && $Min $= "0")
	{
	CDTime.visible = 0;
	CDBGR.visible = 0;
	cancel($CDS);
	}
	else
	{

	if($Sec $= "-1")
	{
	$Sec = "59";
	$Min--;
	}
	if($Sec <= "9")
	{
	$Zero = "0";
	}
	else
	{
	$Zero = "";
	}
	if($min <= "9")
	{
	$Zero2 = "0";
	}
	else
	{
	$Zero2 = "";
	}
	if($Min $= "0" && $Sec $= "10")
	{
	CDBGR.visible = 1;
	CDBG.visible = 0;
	}

	CDTime.setValue($Zero2@$Min@":"@$Zero@$sec);
	%cdschedule = Schedule(1000,0,"CDM");
	$CDS = %cdschedule;
	}
	
}

function clientcmdOpenbrickprintselect(%tttt)
{
   createprintgui(%tttt@"Prints");
   canvas.pushdialog(%tttt@"Prints");
}

function clientcmdOpenBP(%owner,%ownerip)
{
	$Pref::BP::Owner = %owner;
	$Pref::BP::OwnerIP = %ownerip;
	canvas.pushDialog(delplayerbricks);
}

function clientcmdOpenSRules()
{
	canvas.pushDialog(serverrulesgui);
}

function clientcmdOpenMoverGUI()
{
	canvas.pushDialog(moversGUI);
}

function clientcmdCloseMoverGUI()
{
	canvas.popDialog(moversGUI);
}
//#########################################
//#
//#########################################




function SAD(%password)
{
   if (%password !$= "")
      commandToServer('SAD', %password);
}

function SADSetPassword(%password)
{
   commandToServer('SADSetPassword', %password);
}

function clientcmdOpenPWBox()
{
	canvas.pushDialog(PWBoxGUI);
}

function clientcmdOpenMoverGUI()
{
	canvas.pushDialog(moversGUI);
}

function clientcmdCloseMoverGUI()
{
	canvas.popDialog(moversGUI);
}

function buildwall()
{
	for(%i = 0; %i < 10; %i++)
	{
		commandToServer('plantBrick');
		commandToServer('shiftBrick', 0, 0, 3);
		commandToServer('plantBrick');
		commandToServer('shiftBrick', 0, 0, 3);
		commandToServer('plantBrick');
		commandToServer('shiftBrick', 0, 0, 3);
		commandToServer('plantBrick');
		commandToServer('shiftBrick', 0, 0, 3);
		commandToServer('plantBrick');
		commandToServer('shiftBrick', 0, 0, 3);
		commandToServer('plantBrick');
		commandToServer('shiftBrick', 0, 0, 3);
		commandToServer('plantBrick');
		commandToServer('shiftBrick', 0, 0, 3);
		commandToServer('plantBrick');
		commandToServer('shiftBrick', 0, 0, 3);
		commandToServer('plantBrick');
		commandToServer('shiftBrick', 0, 0, 3);
		commandToServer('plantBrick');
		commandToServer('shiftBrick', 0, 0, 3);
		commandToServer('shiftBrick', 2, 0, -30);
	}
}

function stairs(%total)
{
	for(%i = 0; %i < %total; %i++)
	{
		commandtoserver('plantbrick');
		commandToServer('shiftBrick', 1, 0, 3);
	}
}

//----------------------------------------------------------------------------
// Misc server commands
//----------------------------------------------------------------------------

function clientCmdSyncClock(%time)
{
   // Store the base time in the hud control it will automatically increment.
   HudClock.setTime(%time);
}

function clientCmdshowBrickImage(%this)
{
	changeBrickIcon(%this);
}
function clientCmdshowColorImage(%this)
{
	changeBrickIcon(detag(%this));
}

function GameConnection::prepDemoRecord(%this)
{
   %this.demoChatLines = HudMessageVector.getNumLines();
   for(%i = 0; %i < %this.demoChatLines; %i++)
   {
      %this.demoChatText[%i] = HudMessageVector.getLineText(%i);
      %this.demoChatTag[%i] = HudMessageVector.getLineTag(%i);
      echo("Chat line " @ %i @ ": " @ %this.demoChatText[%i]);
   }
}

function GameConnection::prepDemoPlayback(%this)
{
   for(%i = 0; %i < %this.demoChatLines; %i++)
      HudMessageVector.pushBackLine(%this.demoChatText[%i], %this.demoChatTag[%i]);
   Canvas.setContent(PlayGui);
}

function clientCmdgetClientVers()
{
	commandtoserver('setPlayerVers', $Pref::Player::Version);
}