if ($Pref::MasterServer $= "")
    $Pref::MasterServer = "b4v21.block.land:80";

function strIPos(%str, %pos, %start)
{
	return strPos(strLwr(%str), strLwr(%pos), %start);
}

function GetB4v21PatchVersion()
{
	if (!isFile("B4v21Patch.ver"))
		return "1.0";
	
	if ($B4v21::Version !$= "")
		return $B4v21::Version;
	
	%SO = new FileObject();
	%SO.openForRead("B4v21Patch.ver");
	%SO.readLine();
	%SO.readLine();
	$B4v21::Version = %SO.readLine();
	%SO.close();
	%SO.delete();
	
	return $B4v21::Version;
}

package CustomMSPackage
{
	function queryMasterTCPObj::onDisconnect(%this)
	{
		Parent::onDisconnect(%this);
		ServerInfoSO_DisplayAll();
	}
	
	function queryMasterTCPObj::onLine(%this,%line)
	{
		if (firstWord(%line) $= "NOTE")
		{
			if (removeWord(%line,0) $= $Pref::CustomMS::IgnoreNote)
			{
				warn("MS Note: " @ removeWord(%line,0));
				return;
			}
			
			$Pref::CustomMS::IgnoreNote = removeWord(%line,0);
			messageBoxOk("Announcement - CustomMS",removeWord(%line,0));
			return;
		}
		
		Parent::onLine(%this,%line);
	}
	
	function queryMasterTcpObj::onLine(%this, %line)
	{
		if (!%this.gotHttpHeader || !%this.gotHeader)
		{
			if (getWord(%line, 0) $= "FIELDS")
				%this.fields    = removeWord(%line, 0);
			
			Parent::onLine(%this, %line);
			return;
		}
		
		if (%line $= "END")
		{
			Parent::onLine(%this, %line);
			return;
		}
				
		for (%i = 0; %i < getFieldCount(%line); %i++)
		{
			%fData = getField(%line, %i);
			%fType = getField(%this.fields, %i);
			
			switch$(%fType)
			{
				case "IP":
					%ip         = %fData;
				case "PORT":
					%port       = %fData;
				case "PASSWORDED":
					%passworded = (%fData $= "1" ? "Yes" : "No");
				case "DEDICATED":
					%dedicated  = (%fData $= "1" ? "Yes" : "No");
				case "SERVERNAME":
					%serverName = %fData;
				case "PLAYERS":
					%players    = %fData;
				case "MAXPLAYERS":
					%maxPlayers = %fData;
				case "MAPNAME":
					%mapName    = %fData;
				case "BRICKCOUNT":
					%brickCount = %fData;
			}
		}
		
		ServerInfoSO_Add(%ip @ ":" @ %port, %passworded, %dedicated, %serverName, %players, %maxPlayers, "", %mapName, %brickCount);
	}
};
activatePackage(CustomMSPackage);

function ServerSO::Display(%this)
{
	%selected = JS_serverList.getSelectedId();
	if (JS_serverList.getRowTextByID(%this.id) $= "")
		return;
	
	JS_serverList.removeRowById(%this.id);
	JS_serverList.addRow(%this.id, %this.serialize());
	if ("JS_serverList".sortedNumerical)
		JS_serverList.sortNumerical(JS_serverList.sortedBy, JS_serverList.sortedAsc);
	else
		JS_serverList.sort(JS_serverList.sortedBy, JS_serverList.sortedAsc);
	
	JS_serverList.setSelectedById(%selected);
}

function ServerInfoSO_DisplayAll()
{
	JS_serverList.clear();
	
	%TotalServerCount = 0;
	%TotalPlayerCount = 0;
	%i                = 0;
	while(%i < $ServerSO_Count)
	{
		%obj = $ServerSO[%i];
		%TotalServerCount++;
		%TotalPlayerCount = %TotalPlayerCount + %obj.currPlayers;
		%doRow            = 1;
		
		if ($Pref::Filter::gameType != 0)
		{
			%version = getSubStr(%obj.name, 2, strIPos(%obj.name, "]") - 2);
			echo(atoi($Pref::Filter::gameType) SPC atoi(%version) SPC (atoi(%version) != atoi($Pref::Filter::gameType) ? "NO" : "YES"));
			if (atoi(%version) != atoi($Pref::Filter::gameType))
				%doRow = 0;
		}
		
		if ($Pref::Filter::Dedicated && %obj.ded !$= "Yes") %doRow = 0;
		if ($Pref::Filter::NoPassword && %obj.pass $= "Yes") %doRow = 0;
		if ($Pref::Filter::NotEmpty && %obj.currPlayers <= 0) %doRow = 0;
		if ($Pref::Filter::NotFull && %obj.currPlayers >= %obj.maxPlayers) %doRow = 0;
		if (%obj.ping $= "Dead") %doRow = 0;
		if (%obj.ping !$= "???") if (%obj.ping > $pref::Filter::maxPing) %doRow = 0;
		if (%doRow)
		{
			%rowText = %obj.serialize();
			JS_serverList.addRow(%i, %rowText);
		}
		
		%i++;
	}
	
	%text = %TotalPlayerCount SPC "Player" @ (%TotalPlayerCount == 1 ? "" : "s") SPC "/" SPC %TotalServerCount SPC "Server" @ (%TotalServerCount == 1 ? "" : "s");
	JS_window.setText("Join Server - " @ %text);
}

function filtersGui::onWake()
{
	Filter_MasterServer.setValue(getSubStr($Pref::MasterServer,0,(strIPos($Pref::MasterServer,":") != -1 ? strIPos($Pref::MasterServer,":") : strLen($Pref::MasterServer))));
	Filter_GameMenu.clear();
	for (%i = 0; %i < getRecordCount($CustomMS::GameList); %i++)
	{
		%rec = getRecord($CustomMS::GameList,%i);
		Filter_GameMenu.add(getField(%rec,1),getField(%rec,0));
	}
	
	Filter_PingMenu.clear();
	Filter_PingMenu.add(50, 50);
	Filter_PingMenu.add(100, 100);
	Filter_PingMenu.add(150, 150);
	Filter_PingMenu.add(250, 250);
	Filter_PingMenu.add(450, 450);
	Filter_PingMenu.add(999, 999);
	Filter_GameMenu.setSelected(mFloor($pref::Filter::gameType));
	if (Filter_GameMenu.getTextByID(Filter_GameMenu.getSelected()) $= "")
		Filter_GameMenu.setSelected($version);
	
	Filter_PingMenu.setSelected(mFloor($pref::Filter::maxPing));
	if (Filter_PingMenu.getSelected() <= 0)
		Filter_PingMenu.setSelected(999);
}

function filtersGui::onSleep()
{
	%master = trim(Filter_MasterServer.getValue());
	if (strIPos(%master,":") != -1)
		%master = getSubStr(%master,0,strIPos(%master,":"));
	
	if (%master $= "master2.blockland.us")
		messageBoxOk("Error - CustomMS","Failed to set master server. Reason:\n\nSetting the custom ms address to 'master2.blockland.us' can result in your key being revoked (if you host a server on it).");
	else
		$Pref::MasterServer = %master @ ":80";

	$pref::Filter::maxPing = Filter_PingMenu.getSelected();
	$pref::Filter::gameType = Filter_GameMenu.getSelected();
	if ($JoinNetServer)
		ServerInfoSO_DisplayAll();
}
function CustomMS_GetGameList()
{
	$CustomMS::GameList = "";
	echo("CustomMS: Getting game list...");
	if (isObject(CustomMS_TCP))
		CustomMS_TCP.delete();
	
	%this      = new TCPObject(CustomMS_TCP);
	%this.url  = "pastebin.com";
	%this.port = "80";
	%this.path = "/raw/wgHbbwbz";
	%this.connect(%this.url @ ":" @ %this.port);
}

function CustomMS_ConnectionFailure()
{
	error("ERROR: CustomMS_GetGameList() - Failed to connect to pastebin containing game list. Using pre-defined list.");
	$CustomMS::GameList =   "20	Blockland v20\n" @
				"19	Blockland v19\n" @
				"17	Blockland v17\n" @
				"16	Blockland v16\n" @
				"13	Blockland v13\n" @
				"8	Blockland v8\n" @
				"103	Blockland v1.03\n" @
				"2	Blockland v0002\n" @
				"1	GenericTDM\n" @
				"0	All\n";
}

function CustomMS_TCP::onConnectFailed(%this)
{
	CustomMS_ConnectionFailure();
}

function CustomMS_TCP::onDNSFailed(%this)
{
	CustomMS_ConnectionFailure();
}

function CustomMS_TCP::onConnected(%this)
{
	%this.send("GET" SPC %this.path SPC "HTTP/1.1\r\nHost:" SPC %this.url @ "\r\nConnection: close\r\nUser-Agent: Blockland-r2020\r\n\r\n");
}

function CustomMS_TCP::onDisconnect(%this)
{
	echo("CustomMS: Got " @ getRecordCount($CustomMS::GameList) @ " game" @ (getRecordCount($CustomMS::GameList) == 1 ? "" : "s"));
}

function CustomMS_TCP::onLine(%this,%line)
{
	if (firstWord(%line) !$= "DEFINE")
		return;
	
	%line               = removeWord(%line,0);
	$CustomMS::GameList = trim($CustomMS::GameList NL getField(%line,0) TAB getField(%line,1));
}

if (isObject(Canvas))
{
	if (!filtersGui.isNewGui)
	{
		filtersGui.deleteAll();
		filtersGui.delete();
	}

	// filtersGui.cs
	if (!isObject(filtersGui))
	{
		new GuiControl(filtersGui) {
		   profile = "GuiDefaultProfile";
		   horizSizing = "right";
		   vertSizing = "bottom";
		   position = "0 0";
		   extent = "640 480";
		   minExtent = "8 2";
		   visible = "1";
			  isNewGui = "1";

		   new GuiWindowCtrl() {
			  profile = "BlockWindowProfile";
			  horizSizing = "center";
			  vertSizing = "center";
			  position = "221 111";
			  extent = "197 273";
			  minExtent = "8 2";
			  visible = "1";
			  text = "Filters";
			  maxLength = "255";
			  resizeWidth = "0";
			  resizeHeight = "0";
			  canMove = "1";
			  canClose = "1";
			  canMinimize = "0";
			  canMaximize = "0";
			  minSize = "50 50";
			  closeCommand = "canvas.popDialog(filtersGui);";

			  new GuiCheckBoxCtrl() {
				 profile = "GuiCheckBoxProfile";
				 horizSizing = "right";
				 vertSizing = "bottom";
				 position = "34 71";
				 extent = "127 18";
				 minExtent = "8 2";
				 visible = "1";
				 variable = "$Pref::Filter::NoPassword";
				 text = "No Password";
				 groupNum = "-1";
				 buttonType = "ToggleButton";
			  };
			  new GuiPopUpMenuCtrl(Filter_PingMenu) {
				 profile = "GuiPopUpMenuProfile";
				 horizSizing = "right";
				 vertSizing = "bottom";
				 position = "87 151";
				 extent = "99 18";
				 minExtent = "8 2";
				 visible = "1";
				 maxLength = "255";
				 maxPopupHeight = "200";
			  };
			  new GuiCheckBoxCtrl() {
				 profile = "GuiCheckBoxProfile";
				 horizSizing = "right";
				 vertSizing = "bottom";
				 position = "34 51";
				 extent = "127 18";
				 minExtent = "8 2";
				 visible = "1";
				 variable = "$Pref::Filter::Dedicated";
				 text = "Dedicated";
				 groupNum = "-1";
				 buttonType = "ToggleButton";
			  };
			  new GuiBitmapButtonCtrl() {
				 profile = "BlockButtonProfile";
				 horizSizing = "center";
				 vertSizing = "top";
				 position = "53 221";
				 extent = "90 36";
				 minExtent = "8 2";
				 visible = "1";
				 command = "canvas.popDialog(filtersGui);";
				 text = "OK";
				 groupNum = "-1";
				 buttonType = "PushButton";
				 bitmap = "base/client/ui/button2";
				 lockAspectRatio = "0";
				 alignLeft = "0";
				 overflowImage = "0";
				 mKeepCached = "0";
				 mColor = "255 255 255 255";
					wrap = "0";
			  };
			  new GuiTextCtrl() {
				 profile = "GuiTextProfile";
				 horizSizing = "right";
				 vertSizing = "bottom";
				 position = "34 151";
				 extent = "51 18";
				 minExtent = "8 2";
				 visible = "1";
				 text = "Ping under";
				 maxLength = "255";
			  };
			  new GuiCheckBoxCtrl() {
				 profile = "GuiCheckBoxProfile";
				 horizSizing = "right";
				 vertSizing = "bottom";
				 position = "34 91";
				 extent = "127 18";
				 minExtent = "8 2";
				 visible = "1";
				 variable = "$Pref::Filter::NotEmpty";
				 text = "Not Empty";
				 groupNum = "-1";
				 buttonType = "ToggleButton";
			  };
			  new GuiCheckBoxCtrl() {
				 profile = "GuiCheckBoxProfile";
				 horizSizing = "right";
				 vertSizing = "bottom";
				 position = "34 111";
				 extent = "127 18";
				 minExtent = "8 2";
				 visible = "1";
				 variable = "$Pref::Filter::NotFull";
				 text = "Not full";
				 groupNum = "-1";
				 buttonType = "ToggleButton";
			  };
			  new GuiTextCtrl() {
				 profile = "GuiTextProfile";
				 horizSizing = "right";
				 vertSizing = "bottom";
				 position = "12 30";
				 extent = "140 18";
				 minExtent = "8 2";
				 visible = "1";
				 text = "Only show servers that are..";
				 maxLength = "255";
			  };
			  new GuiPopUpMenuCtrl(Filter_GameMenu) {
				 profile = "GuiPopUpMenuProfile";
				 horizSizing = "right";
				 vertSizing = "bottom";
				 position = "77 131";
				 extent = "109 18";
				 minExtent = "8 2";
				 visible = "1";
				 maxLength = "255";
				 maxPopupHeight = "200";
			  };
			  new GuiTextCtrl() {
				 profile = "GuiTextProfile";
				 horizSizing = "right";
				 vertSizing = "bottom";
				 position = "34 131";
				 extent = "39 18";
				 minExtent = "8 2";
				 visible = "1";
				 text = "Game is";
				 maxLength = "255";
			  };
			  new GuiTextEditCtrl(Filter_MasterServer) {
				 profile = "GuiTextEditProfile";
				 horizSizing = "right";
				 vertSizing = "bottom";
				 position = "53 191";
				 extent = "116 18";
				 minExtent = "8 2";
				 visible = "1";
				 maxLength = "255";
				 historySize = "0";
				 password = "0";
				 tabComplete = "0";
				 sinkAllKeyEvents = "0";
			  };
			  new GuiTextCtrl() {
				 profile = "GuiTextProfile";
				 horizSizing = "right";
				 vertSizing = "bottom";
				 position = "34 191";
				 extent = "18 18";
				 minExtent = "8 2";
				 visible = "1";
				 text = "MS:";
				 maxLength = "255";
			  };
			  new GuiTextCtrl() {
				 profile = "GuiTextProfile";
				 horizSizing = "right";
				 vertSizing = "bottom";
				 position = "171 191";
				 extent = "15 18";
				 minExtent = "8 2";
				 visible = "1";
				 text = ":80";
				 maxLength = "255";
			  };
			  new GuiTextCtrl() {
				 profile = "GuiTextProfile";
				 horizSizing = "right";
				 vertSizing = "bottom";
				 position = "12 171";
				 extent = "95 18";
				 minExtent = "8 2";
				 visible = "1";
				 text = "Custom MS settings";
				 maxLength = "255";
			  };
		   };
		};
	}

	CustomMS_GetGameList();
}

// patch.cs
function clientCmdOpenPrintSelectorDlg(%aspectRatio, %startPrint, %numPrints)
{
	if (PSD_Window.scrollcount $= "")
		PSD_Window.scrollcount = 0;
	if (!isObject("PSD_PrintScroller" @ %aspectRatio))
		PSD_LoadPrints(%aspectRatio, %startPrint, %numPrints);
	if (!isObject("PSD_PrintScrollerLetters"))
		PSD_LoadPrints("Letters", $PSD_letterStart, $PSD_numLetters);
	
	$PSD_NumPrints = %numPrints;
	Canvas.pushDialog("printSelectorDlg");
	
	if ($PSD_LettersVisible || $PSD_NumPrints == 0)
		PSD_PrintScrollerLetters.setVisible(1);
	else
	{
		%obj = "PSD_PrintScroller" @ %aspectRatio;
		%obj.setVisible(1);
	}
	
	$PSD_CurrentAR = %aspectRatio;
}

// script.cs
package B4v21CustomMS
{
	function postServerTCPObj::connect(%this, %addr)
	{
		%oldLen   = strLen(getField(%this.cmd, getFieldCount(%this.cmd) - 1)) - 1;
		%this.cmd = strReplace(%this.cmd, "&Port=" @ mFloor($Server::Port), "&Port=" @ mFloor($Server::Port) @ "&Patch=1&ourName=" @ urlEnc($Pref::Player::NetName) @ "&GamePatch=" @ urlEnc(GetB4v21PatchVersion()));
		if (strPos(%this.cmd, "&ver=") != -1)
		{
			%this.cmd = getSubStr(%this.cmd, 0, strPos(%this.cmd, "&ver="));
			%this.cmd = strReplace(%this.cmd, "&Port=" @ mFloor($Server::Port),"&Port=" @ mFloor($Server::Port) @ "&ver=" @ $Version);
		}
		else
			%this.cmd = strReplace(%this.cmd, "&Port=" @ mFloor($Server::Port),"&Port=" @ mFloor($Server::Port) @ "&ver=" @ $Version);
		
		%newLen   = strLen(getField(%this.cmd, getFieldCount(%this.cmd) - 1)) - 1;
		%this.cmd = strReplace(%this.cmd, "Content-Length: " @ %oldLen, "Content-Length: " @ %newLen + 2);
        	%this.cmd = strReplace(%this.cmd, "master.blockland.us", $Pref::MasterServer);
		%this.cmd = strReplace(%this.cmd, "/master/postServer.asp", "/postServer.php");
		%this.cmd = %this.cmd @ "\r\n";
		Parent::connect(%this, $Pref::MasterServer);
	}

	function queryMasterTCPObj::connect(%this, %addr)
	{
		%this.cmd = "GET /index.php HTTP/1.1\r\nHost: " @ $Pref::MasterServer @ "\r\n\r\n";
		Parent::connect(%this, $Pref::MasterServer);
	}
};

package B4v21AuthServer
{
	function authTCPObj::connect(%this, %url)
	{
		%url           = strReplace(%url, "master.blockland.us", "b4v21.block.land");
		%this.cmd      = setWord(%this.cmd, 1, (strPos(%this.cmd, "authInit") != -1 ? "/api/authInit.php" : "/api/authConfirm2.php"));
		%this.cmd      = strReplace(%this.cmd, "master.blockland.us", "b4v21.block.land");
		%this.site     = "b4v21.block.land";
		%this.filePath = getWord(%this.cmd, 1);
		%isInit        = (strPos(%this.cmd, "authInit") != -1);
		
		// Revise the post data
		if (%isInit)
		{
			%PostData = getSubStr(getRecord(%this.cmd, getRecordCount(%this.cmd) - 1), 0, strLen(getRecord(%this.cmd, getRecordCount(%this.cmd) - 1)) - 1) @ "&VER=" @ $Version;
			%PostLen  = strLen(%PostData);
			
			// Set new post data
			%this.cmd = setRecord(%this.cmd, getRecordCount(%this.cmd) - 1, %PostData @ "\r");
			
			// Replace content length field
			for (%i = 0; %i < getRecordCount(%this.cmd); %i++)
			{
				if (getWord(getRecord(%this.cmd, %i), 0) !$= "Content-Length:")
					continue;
				
				%this.cmd = setRecord(%this.cmd, %i, "Content-Length:" SPC %PostLen @ "\r");
				break;
			}
		}
		
		Parent::connect(%this, %url);
	}
	function authTCPObj::onLine(%this, %line)
	{
		switch$(firstWord(%line))
		{
			case "NOTE":
				%line = removeWord(%line, 0);
				
				if (!isObject(Canvas))
					echo("NOTE: " @ %line);
				else
					messageBoxOk("Authentication Server Notice", %line);
				
			default:
				Parent::onLine(%this, %line);
		}
	}
	function authTCPObj_Server::connect(%this, %url)
	{
		%url      = strReplace(%url, "master.blockland.us", "b4v21.block.land");
		%this.cmd = setWord(%this.cmd, 1, (strPos(%this.cmd, "authInit") != -1 ? "/api/authInit.php" : "/api/authConfirm2.php"));
		%this.cmd = strReplace(%this.cmd, "master.blockland.us", "b4v21.block.land");
		Parent::connect(%this, %url);
	}
	function servAuthTCPobj::connect(%this, %url)
	{
		%url      = strReplace(%url, "master.blockland.us", "b4v21.block.land");
		%this.cmd = setWord(%this.cmd, 1, "/api/authQuery.php");
		%this.cmd = strReplace(%this.cmd, "master.blockland.us", "b4v21.block.land");
		Parent::connect(%this, %url);
	}
};

activatePackage(B4v21CustomMS);
activatepackage(B4v21AuthServer);