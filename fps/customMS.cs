if($version !$= "") {
	error("This version of CustomMS is meant for Blockland v0002.");
	return;
}
package CustomMSPackage {
	function onStart() {
		Parent::onStart();
		WebCom_PostServerUpdateLoop();
	}
	function disconnect() {
		Parent::disconnect();
		cancel($WebCom_PostSchedule);
	}
	function GameConnection::onConnectionAccepted(%this) {
		Parent::onConnectionAccepted(%this);
		WebCom_PostServer();
	}
	function GameConnection::onConnectionDropped(%this) {
		Parent::onConnectionDropped(%this,%reason);
		WebCom_PostServer();
	}
	function GameConnection::onConnectRequestRejected( %this, %msg )
	{
		echo(%msg);
		if(%msg $= "CHR_PASSWORD" && $JoinGameAddress !$= "")
		{
			$JoinNetServer = 1;
			$ServerInfo::Ping = "???";
			$ServerInfo::Address = $JoinGameAddress;
			Canvas.popDialog(connectingGui);
			Canvas.pushDialog(JoinServerPassGui);
			return;
		}
		deleteVariables("$JoinGameAddress");
		return Parent::onConnectRequestRejected(%this, %msg);
	}
};
activatePackage(CustomMSPackage);
if(isObject(Canvas) && isObject(JoinServerGui) && !JoinServerGui.isNew) JoinServerGui.delete();
if(isObject(Canvas) && !isObject(JoinServerGui)) exec("fps/JoinServerGui.gui");
if(isObject(Canvas) && !isObject(JoinServerPassGui)) exec("fps/JoinServerPassGui.gui");
if(isObject(Canvas) && !isObject(ConnectingGui)) exec("fps/ConnectingGui.gui");
if(!isObject(ServerInfoGroup)) new SimGroup(ServerInfoGroup);
$Pref::MasterServer = "clayhanson.x10host.com";
function isUnlocked() { return 1; }
function isNonsenseVerfied() { return 1; }
function pingSingleServer() { return 0; }
function onSendConnectChallengeRequest() {
	echo("Sending challenge request...");
	Connecting_Text.setText(Connecting_Text.getText() @ "\nSending challenge request...");
}
function connectingGui::cancel() {
	if(isObject($conn)) {
		$conn.cancelConnect();
		$conn.delete();
	}
	$ArrangedActive = 0;
	$ArrangedAddyCount = 0;
	if(isObject($ArrangedConnection)) {
		$ArrangedConnection.cancelConnect();
		$ArrangedConnection.delete();
	}
	deleteVariables("$JoinGameAddress");
	if(!JoinServerGui.isAwake()) {
		MainMenuGui.showButtons();
	}
	Canvas.popDialog(connectingGui);
}
function MJ_connect()
{
	cancelServerQuery();
	%ip = MJ_txtIP.getValue();
	%joinPass = MJ_txtJoinPass.getValue();
	echo("Attempting to connect to ", %ip);
	if (%ip)
	{
		if (isObject($conn))
		{
			$conn.cancelConnect();
			$conn.delete();
		}
		Connecting_Text.setText("Connecting to " @ %ip);
		Canvas.pushDialog(connectingGui);
		deleteDataBlocks();
		ConnectToServer(%ip, %joinPass, 1, 1);
	}
}

function ManualJoin::onWake()
{
	MJ_txtJoinPass.setText("");
}

function JoinServerGui::onWake()
{
	if ($launchedFromSteam && !isUnlocked())
	{
		MessageBoxOK("Offline Mode", "You can\'t join a game because you haven\'t authenticated with the master server.  \n\nVerify that your internet connection and steam are both working and try again.");
		Canvas.popDialog(JoinServerGui);
		MainMenuGui.showButtons();
		return;
	}
	if (!"JoinServerGui".hasQueriedOnce)
	{
		JS_sortNumList(3);
	}
	if ($pref::Gui::AutoQueryMasterServer || !isUnlocked())
	{
		if ("JoinServerGui".lastQueryTime == 0.0 || getSimTime() - "JoinServerGui".lastQueryTime > 5.0 * 60.0 * 1000.0)
		{
			JoinServerGui.queryWebMaster();
		}
	}
}

function JoinServerGui::queryWebMaster(%this)
{
	%this.hasQueriedOnce = 1;
	if (!isUnlocked())
	{
		JSG_demoBanner.setVisible(1);
		JSG_demoBanner.setColor("1 1 1 0.65");
		JSG_demoBanner2.setVisible(1);
		JSG_demoBanner2.setColor("1 1 1 0.65");
	}
	else
	{
		JSG_demoBanner.setVisible(0);
		JSG_demoBanner2.setVisible(0);
	}
	"JoinServerGui".lastQueryTime = getSimTime();
	$JoinNetServer = 1;
	$MasterQueryCanceled = 0;
	if (isObject(queryMasterTCPObj))
	{
		queryMasterTCPObj.delete();
	}
	new TCPObject(queryMasterTCPObj){
	};
	"queryMasterTCPObj".site = $Pref::MasterServer;
	"queryMasterTCPObj".port = 80;
	"queryMasterTCPObj".filePath = "/index.php";
	"queryMasterTCPObj".cmd = "GET " @ queryMasterTCPObj.filePath @ " HTTP/1.0\r\nHost: " @ queryMasterTCPObj.site @ "\r\n\r\n";
	queryMasterTCPObj.connect(queryMasterTCPObj.site @ ":" @ queryMasterTCPObj.port);
	JS_queryStatus.setVisible(1);
	JS_statusText.setText("Getting Server List...");
}

function queryMasterTCPObj::onConnected(%this)
{
	%this.send(%this.cmd);
	if (MessageBoxOKDlg.isActive())
	{
		if (MBOKFrame.getValue() $= "Query Master Server Failed")
		{
			Canvas.popDialog(MessageBoxOKDlg);
		}
	}
}

function queryMasterTCPObj::onDNSFailed(%this)
{
	MessageBoxOK("Query Master Server Failed", "<just:left>DNS Failed during master server query.\n\n" @ "1.  Verify your internet connection\n\n" @ "2.  Make sure any security software you have is set to allow Blockland.exe to connect to the internet.");
	JS_queryStatus.setVisible(0);
}

function queryMasterTCPObj::onConnectFailed(%this)
{
	MessageBoxOK("Query Master Server Failed", "<just:left>Connection failed during master server query.\n\n" @ "1.  Verify your internet connection\n\n" @ "2.  Make sure any security software you have is set to allow Blockland.exe to connect to the internet.");
	JS_queryStatus.setVisible(0);
}

function queryMasterTCPObj::onDisconnect(%this)
{
}

function queryMasterTCPObj::onLine(%this, %line)
{
	if (%this.done)
	{
		return;
	}
	if (%this.fileSize)
	{
		if (%this.gotHttpHeader)
		{
			%this.buffSize = %this.buffSize + strlen(%line) + 2.0;
			JS_statusBar.setValue(%this.buffSize / %this.fileSize);
		}
		else
		{
			if (%line $= "")
			{
				%this.gotHttpHeader = 1;
			}
		}
	}
	%word = getWord(%line, 0);
	if (%word $= "HTTP/1.1")
	{
		%code = getWord(%line, 1);
		if (%code != 200.0)
		{
			warn("WARNING: queryMasterTCPObj - got non-200 http response " @ %code @ "");
		}
		if (%code >= 400.0 && %code <= 499.0)
		{
			warn("WARNING: 4xx error on queryMasterTCPObj, retrying");
			%this.schedule(0, disconnect);
			%this.schedule(500, connect, %this.site @ ":" @ %this.port);
		}
		if (%code >= 300.0 && %code <= 399.0)
		{
			warn("WARNING: 3xx error on queryMasterTCPObj, will wait for location header");
		}
	}
	else
	{
		if (%word $= "Location:")
		{
			%url = getWords(%line, 1);
			warn("WARNING: queryMasterTCPObj - Location redirect to " @ %url);
			%this.filePath = %url;
			%this.cmd = "GET " @ %this.filePath @ " HTTP/1.0\r\nHost: " @ %this.site @ "\r\n\r\n";
			%this.schedule(0, disconnect);
			%this.schedule(500, connect, %this.site @ ":" @ %this.port);
		}
		else
		{
			if (%word $= "Content-Location:")
			{
				%url = getWords(%line, 1);
				warn("WARNING: queryMasterTCPObj - Content-Location redirect to " @ %url);
				%this.filePath = %url;
				%this.cmd = "GET " @ %this.filePath @ " HTTP/1.0\r\nHost: " @ %this.site @ "\r\n\r\n";
				%this.schedule(0, disconnect);
				%this.schedule(500, connect, %this.site @ ":" @ %this.port);
			}
			else
			{
				if (%word $= "ERROR")
				{
					MessageBoxOK("Error", "Error retrieving server list.");
					return;
				}
				else
				{
					if (%word $= "START")
					{
						%this.gotHeader = 1;
						ServerInfoSO_ClearAll();
						return;
					}
					else
					{
						if (%word $= "FIELDS")
						{
							%wordCount = getWordCount(%line);
							%i = 1;
							while(%i < %wordCount)
							{
								%this.fieldName[%i - 1.0] = getWord(%line, %i);
								%i = %i + 1.0;
							}
						}
						else
						{
							if (%word $= "END")
							{
								JS_queryStatus.setVisible(0);
								ServerInfoSO_DisplayAll();
								ServerInfoSO_StartPingAll();
								%this.done = 1;
								%this.disconnect();
								return;
							}
							else
							{
								if (%word $= "**OLD**")
								{
									return;
								}
								else
								{
									if (%word $= "Content-Length:")
									{
										%fileSize = getWord(%line, 1);
										%this.fileSize = %fileSize;
									}
								}
							}
						}
					}
				}
			}
		}
	}
	if (%this.gotHeader)
	{
		%wordCount = getWordCount(%line);
		%ip = "";
		%port = "";
		%pass = "No";
		%ded = "No";
		%name = "";
		%players = 0;
		%maxPlayers = 0;
		%mods = "";
		%map = "";
		%brickCount = 0;
		%demoPlayers = 0;
		%i = 0;
		while(%i < %wordCount)
		{
			%fieldName = %this.fieldName[%i];
			%field = getField(%line, %i);
			if (%fieldName $= "")
			{
			}
			else
			{
				if (%fieldName $= "IP")
				{
					%ip = %field;
				}
				else
				{
					if (%fieldName $= "PORT")
					{
						%port = %field;
					}
					else
					{
						if (%fieldName $= "PASSWORDED")
						{
							%pass = %field;
						}
						else
						{
							if (%fieldName $= "DEDICATED")
							{
								%ded = %field;
							}
							else
							{
								if (%fieldName $= "SERVERNAME")
								{
									%name = %field;
								}
								else
								{
									if (%fieldName $= "PLAYERS")
									{
										%players = %field;
									}
									else
									{
										if (%fieldName $= "MAXPLAYERS")
										{
											%maxPlayers = %field;
										}
										else
										{
											if (%fieldName $= "MODNAME")
											{
												%mods = %field;
											}
											else
											{
												if (%fieldName $= "MAPNAME")
												{
													%map = %field;
												}
												else
												{
													if (%fieldName $= "BRICKCOUNT")
													{
														%brickCount = %field;
													}
													else
													{
														if (%fieldName $= "DEMOPLAYERS")
														{
															%demoPlayers = %field;
														}
														else
														{
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			%i = %i + 1.0;
		}
		%ping = "???";
		if (%ded)
		{
			%ded = "Yes";
		}
		else
		{
			%ded = "No";
		}
		if (%pass)
		{
			%pass = "Yes";
		}
		else
		{
			%pass = "No";
		}
		if (%ip $= "")
		{
			return;
		}
		ServerInfoSO_Add(%ip @ ":" @ %port, %pass, %ded, %name, %players, %maxPlayers, %mods, %map, %brickCount, %demoPlayers);
	}
}

function JoinServerGui::queryLan(%this)
{
	JoinServerGui.cancel();
	JSG_demoBanner.setVisible(0);
	JSG_demoBanner2.setVisible(0);
	$JoinNetServer = 0;
	%flags = $Pref::Filter::Dedicated | $Pref::Filter::NoPassword << 1 | $Pref::Filter::LinuxServer << 2 | $Pref::Filter::WindowsServer << 3 | $Pref::Filter::TeamDamageOn << 4 | $Pref::Filter::TeamDamageOff << 5 | $Pref::Filter::CurrentVersion << 7;
	queryLanServers(28050, 0, $Client::GameTypeQuery, $Client::MissionTypeQuery, $pref::Filter::minPlayers, 100, 0, 2, $pref::Filter::maxPing, $pref::Filter::minCpu, %flags);
	queryLanServers(28000, 0, $Client::GameTypeQuery, $Client::MissionTypeQuery, $pref::Filter::minPlayers, 100, 0, 2, $pref::Filter::maxPing, $pref::Filter::minCpu, %flags);
	queryLanServers(28051, 0, $Client::GameTypeQuery, $Client::MissionTypeQuery, $pref::Filter::minPlayers, 100, 0, 2, $pref::Filter::maxPing, $pref::Filter::minCpu, %flags);
}

function JoinServerGui::cancel(%this)
{
	$MasterQueryCanceled = 1;
	cancelServerQuery();
	JS_queryStatus.setVisible(0);
}

function JoinServerGui::join(%this)
{
	$MasterQueryCanceled = 1;
	cancelServerQuery();
	%id = JS_serverList.getSelectedId();
	if ($JoinNetServer)
	{
		%idx = JS_serverList.getSelectedId();
		if (%idx < 0.0)
		{
			return;
		}
		%so = $ServerSO[%idx];
		if (isObject(%so))
		{
			$ServerInfo::Address = %so.ip;
			$ServerInfo::Name = %so.name;
			$ServerInfo::MaxPlayers = %so.maxPlayers;
			$ServerInfo::Ping = %so.ping;
			if (%so.pass $= "Yes")
			{
				$ServerInfo::Password = 1;
			}
			else
			{
				$ServerInfo::Password = 0;
			}
		}
		else
		{
			return;
		}
	}
	else
	{
		echo("Joining LAN game...");
		%id = JS_serverList.getSelectedId();
		if (!setServerInfo(%id))
		{
			return;
		}
	}
	if ($ServerInfo::Password)
	{
		Canvas.pushDialog("joinServerPassGui");
	}
	else
	{
		deleteDataBlocks();
		setParticleDisconnectMode(0);
		if (isObject($conn))
		{
			$conn.cancelConnect();
			$conn.delete();
			disconnectedCleanup();
		}
		Connecting_Text.setText("Connecting to " @ $ServerInfo::Address);
		Canvas.pushDialog(connectingGui);
		echo("");
		%so = $ServerSO[JS_serverList.getSelectedId()];
		echo("Connecting to " @ $ServerInfo::Name @ " (" @ $ServerInfo::Address @ ", " @ $ServerInfo::Ping @ "ms)");
		echo("  Download Sounds:      " @ ($Pref::Net::DownloadSounds) ? "True" : "False");
		echo("  Download Music:       " @ ($Pref::Net::DownloadMusic) ? "True" : "False");
		echo("  Download Textures:    " @ ($Pref::Net::DownloadTextures) ? "True" : "False");
		echo("");
		if ($JoinNetServer)
		{
			%doDirect = 1;
			%doArranged = 1;
			if ($ServerInfo::Ping $= "???")
			{
				%doDirect = 1;
				%doArranged = 1;
			}
			else
			{
				if ($ServerInfo::Ping $= "---")
				{
					%doDirect = 0;
					%doArranged = 1;
				}
				else
				{
					if ($ServerInfo::Ping $= mFloor($ServerInfo::Ping))
					{
						%doDirect = 1;
						%doArranged = 0;
					}
					else
					{
						error("ERROR: Strange ping value " @ $ServerInfo::Ping @ "");
						%doDirect = 1;
						%doArranged = 1;
					}
				}
			}
			ConnectToServer($ServerInfo::Address, "", %doDirect, %doArranged);
		}
		else
		{
			ConnectToServer($ServerInfo::Address, "", 1, 0);
		}
	}
}

function handlePunchConnect(%address, %clientNonce)
{
	if (isObject(ServerConnection))
	{
		if (ServerConnection.isConnected())
		{
			echo("Direct connection is good, ignoring arranged connection");
			cancelAllPendingConnections();
			return;
		}
		else
		{
			echo("Direct connection is no good, going with the arranged connection");
			ServerConnection.cancelConnect();
			ServerConnection.delete();
			deleteDataBlocks();
			setParticleDisconnectMode(0);
		}
	}
	cancelAllPendingConnections();
	$conn = new GameConnection(ServerConnection);
	RootGroup.add($conn);
	$conn.setConnectArgs($pref::Player::LANName, $pref::Player::NetName, $Pref::Player::ClanPrefix, $Pref::Player::ClanSuffix, %clientNonce);
	$conn.setJoinPassword($Connection::Password);
	$conn.connect(%address);
}

function JoinServerGui::exit(%this)
{
	$MasterQueryCanceled = 1;
	cancelServerQuery();
	Canvas.setContent(MainMenuGui);
}

function JoinServerGui::update(%this)
{
	JS_queryStatus.setVisible(0);
	JS_serverList.clear();
	%sc = getServerCount();
	%playerCount = 0;
	%i = 0;
	while(%i < %sc)
	{
		setServerInfo(%i);
		%serverName = $ServerInfo::Name;
		if ($Pref::Chat::CurseFilter)
		{
			%serverName = censorString(%serverName);
		}
		JS_serverList.addRow(%i, ($ServerInfo::Password ? "Yes" : "No") TAB ($ServerInfo::Dedicated ? "D" : "L") TAB %serverName TAB $ServerInfo::Ping TAB $ServerInfo::PlayerCount TAB "/" TAB $ServerInfo::MaxPlayers TAB " " TAB $ServerInfo::MissionName TAB $ServerInfo::PlayerCount TAB %i);
		%playerCount = %playerCount + $ServerInfo::PlayerCount;
		%i = %i + 1.0;
	}
	JS_serverList.sort(0);
	JS_serverList.setSelectedRow(0);
	JS_serverList.scrollVisible(0);
	%text = "";
	if (%playerCount == 1.0)
	{
		%text = %playerCount @ " Player / ";
	}
	else
	{
		%text = %playerCount @ " Players / ";
	}
	if (%sc == 1.0)
	{
		%text = %text @ %sc @ " Server";
	}
	else
	{
		%text = %text @ %sc @ " Servers";
	}
	JS_window.setText("Join Server - " @ %text);
}

function onServerQueryStatus(%status, %msg, %value)
{
	if (!JS_queryStatus.isVisible())
	{
		JS_queryStatus.setVisible(1);
	}
	if (%status $= "start")
	{
		JS_statusText.setText(%msg);
		JS_statusBar.setValue(0);
		JS_serverList.clear();
	}
	else
	{
		if (%status $= "ping")
		{
			JS_statusText.setText("Ping Servers");
			JS_statusBar.setValue(%value);
		}
		else
		{
			if (%status $= "query")
			{
				JS_statusText.setText("Query Servers");
				JS_statusBar.setValue(%value);
			}
			else
			{
				if (%status $= "done")
				{
					JS_queryStatus.setVisible(0);
					JoinServerGui.update();
				}
			}
		}
	}
}

function JS_sortList(%col, %defaultDescending)
{
	"JS_serverList".sortedNumerical = 0;
	if ("JS_serverList".sortedBy == %col)
	{
		"JS_serverList".sortedAsc = (!"JS_serverList".sortedAsc);
		JS_serverList.sort(JS_serverList.sortedBy, JS_serverList.sortedAsc);
	}
	else
	{
		"JS_serverList".sortedBy = %col;
		if (%defaultDescending)
		{
			"JS_serverList".sortedAsc = 0;
		}
		else
		{
			"JS_serverList".sortedAsc = 1;
		}
		JS_serverList.sort(JS_serverList.sortedBy, JS_serverList.sortedAsc);
	}
}

function JS_sortNumList(%col, %defaultDescending)
{
	"JS_serverList".sortedNumerical = 1;
	if ("JS_serverList".sortedBy == %col)
	{
		"JS_serverList".sortedAsc = (!"JS_serverList".sortedAsc);
		JS_serverList.sortNumerical(JS_serverList.sortedBy, JS_serverList.sortedAsc);
	}
	else
	{
		"JS_serverList".sortedBy = %col;
		if (%defaultDescending)
		{
			"JS_serverList".sortedAsc = 0;
		}
		else
		{
			"JS_serverList".sortedAsc = 1;
		}
		JS_serverList.sortNumerical(JS_serverList.sortedBy, JS_serverList.sortedAsc);
	}
}

function ServerInfoSO_ClearAll()
{
	%i = 0;
	while(%i < $ServerSO_Count)
	{
		if (isObject($ServerSO[%i]))
		{
			$ServerSO[%i].delete();
			$ServerSO[%i] = "";
		}
		%i = %i + 1.0;
	}
	$ServerSO_Count = 0;
	JS_serverList.clear();
}

function ServerInfoSO_Add(%ip, %pass, %ded, %name, %currPlayers, %maxPlayers, %mods, %map, %brickCount, %demoPlayers)
{
	if ($ServerSO_Count <= 0.0)
	{
		$ServerSO_Count = 0;
	}
	$ServerSO[$ServerSO_Count] = new ScriptObject(ServerSO){
		ping = "???";
		ip = %ip;
		pass = %pass;
		ded = %ded;
		name = %name;
		currPlayers = %currPlayers;
		maxPlayers = %maxPlayers;
		mods = %mods;
		map = %map;
		brickCount = %brickCount;
		demoPlayers = %demoPlayers;
		id = $ServerSO_Count;
	};
	if (!isObject("ServerInfoGroup")) {
		new SimGroup("ServerInfoGroup");
		RootGroup.add("ServerInfoGroup");
	}
	ServerInfoGroup.add($ServerSO[$ServerSO_Count]);
	%strIP = %ip;
	%strIP = strreplace(%strIP, ".", "_");
	%strIP = strreplace(%strIP, ":", "X");
	$ServerSOFromIP[%strIP] = $ServerSO_Count;
	$ServerSO_Count = $ServerSO_Count + 1.0;
}

function ServerInfoSO_DisplayAll()
{
	JS_serverList.clear();
	%TotalServerCount = 0;
	%TotalPlayerCount = 0;
	%i = 0;
	while(%i < $ServerSO_Count)
	{
		%obj = $ServerSO[%i];
		%TotalServerCount = %TotalServerCount + 1.0;
		%TotalPlayerCount = %TotalPlayerCount + %obj.currPlayers;
		%doRow = 1;
		if ($Pref::Filter::Dedicated && %obj.ded !$= "Yes")
		{
			%doRow = 0;
		}
		if ($Pref::Filter::NoPassword && %obj.pass $= "Yes")
		{
			%doRow = 0;
		}
		if ($Pref::Filter::NotEmpty && %obj.currPlayers <= 0.0)
		{
			%doRow = 0;
		}
		if ($Pref::Filter::NotFull && %obj.currPlayers >= %obj.maxPlayers)
		{
			%doRow = 0;
		}
		if (%obj.ping $= "Dead")
		{
			%doRow = 0;
		}
		if (%obj.ping !$= "???")
		{
			if (%obj.ping > $pref::Filter::maxPing)
			{
				%doRow = 0;
			}
		}
		if (%doRow)
		{
			echo(%obj);
			%rowText = %obj.serialize();
			JS_serverList.addRow(%i, %rowText);
		}
		%i = %i + 1.0;
	}
	%text = "";
	if (%TotalPlayerCount == 1.0)
	{
		%text = %TotalPlayerCount @ " Player / ";
	}
	else
	{
		%text = %TotalPlayerCount @ " Players / ";
	}
	if (%TotalServerCount == 1.0)
	{
		%text = %text @ %TotalServerCount @ " Server";
	}
	else
	{
		%text = %text @ %TotalServerCount @ " Servers";
	}
	JS_window.setText("Join Server - " @ %text);
}

function ServerInfoSO_StartPingAll()
{
	echo("");
	echo("\c5Pinging Servers...");
	if ($Pref::Net::MaxSimultaneousPings <= 0.0)
	{
		$Pref::Net::MaxSimultaneousPings = 10;
	}
	$ServerSO_PingCount = 0;
	if ($ServerSO_Count < $Pref::Net::MaxSimultaneousPings)
	{
		%count = $ServerSO_Count;
	}
	else
	{
		%count = $Pref::Net::MaxSimultaneousPings;
	}
	%i = 0;
	while(%i < %count)
	{
		echo("\c2Sending ping to    IP:" @ $ServerSO[$ServerSO_PingCount].ip);
		pingSingleServer($ServerSO[%i].ip, %i);
		$ServerSO_PingCount = %i;
		%i = %i + 1.0;
	}
}

function ServerInfoSO_PingNext(%slot)
{
	if (!$MasterQueryCanceled)
	{
		if ($ServerSO_PingCount < $ServerSO_Count - 1.0)
		{
			$ServerSO_PingCount = $ServerSO_PingCount + 1.0;
			echo("\c2Sending ping to    IP:" @ $ServerSO[$ServerSO_PingCount].ip);
			pingSingleServer($ServerSO[$ServerSO_PingCount].ip, %slot);
		}
		else
		{
			return;
		}
	}
}

function onSimplePingReceived(%ip, %ping, %slot)
{
	if ($JoinNetServer == 0.0)
	{
		return;
	}
	echo("Recieved ping from " @ %ip @ " - " @ %ping @ "ms");
	ServerInfoSO_UpdatePing(%ip, %ping);
	ServerInfoSO_PingNext(%slot);
}

function onSimplePingTimeout(%ip, %slot)
{
	if ($JoinNetServer == 0.0)
	{
		return;
	}
	echo("\c3No response from   ", %ip);
	ServerInfoSO_UpdatePing(%ip, "---");
	ServerInfoSO_PingNext(%slot);
}

function ServerInfoSO_UpdatePing(%ip, %ping)
{
	%strIP = %ip;
	%strIP = strreplace(%strIP, ".", "_");
	%strIP = strreplace(%strIP, ":", "X");
	%idx = $ServerSOFromIP[%strIP];
	%obj = $ServerSO[%idx];
	if (isObject(%obj))
	{
		%obj.ping = %ping;
		%obj.Display();
	}
	else
	{
		error("ERROR: ServerInfoSO_UpdatePing() - No script object found for ip: ", %strIP);
	}
}

function ScriptObject::serialize(%this)
{
	if (%this.ping $= "Dead")
	{
		%ret = "\cr";
	}
	else
	{
		if (%this.pass $= "Yes")
		{
			%ret = "\c2";
		}
		else
		{
			if (%this.currPlayers >= %this.maxPlayers)
			{
				%ret = "\c3";
			}
		}
	}
	%name = %this.name;
	if ($Pref::Chat::CurseFilter)
	{
		%name = censorString(%name);
	}
	%ret = %ret @ %this.pass TAB %this.ded TAB %name TAB %this.ping TAB %this.currPlayers TAB "/" TAB %this.maxPlayers TAB %this.brickCount TAB %this.map TAB %this.ip;
	%simpleName = %this.name;
	%simpleName = strreplace(%simpleName, " ", "_");
	%simpleName = alphaOnlyWhiteListFilter(%simpleName);
	%simpleName = strreplace(%simpleName, "_", " ");
	%simpleName = trim(%simpleName);
	if (%simpleName $= "")
	{
		%simpleName = "zzzzzzz";
	}
	%ret = %ret TAB %simpleName;
	return %ret;
}

function ScriptObject::Display(%this)
{
	%selected = JS_serverList.getSelectedId();
	JS_serverList.removeRowById(%this.id);
	JS_serverList.addRow(%this.id, %this.serialize());
	if ("JS_serverList".sortedNumerical)
	{
		JS_serverList.sortNumerical(JS_serverList.sortedBy, JS_serverList.sortedAsc);
	}
	else
	{
		JS_serverList.sort(JS_serverList.sortedBy, JS_serverList.sortedAsc);
	}
	JS_serverList.setSelectedById(%selected);
}

function JoinServerGui::ClickBack(%this)
{
	Canvas.popDialog(JoinServerGui);
	MainMenuGui.showButtons();
}

function ConnectToServer(%address, %password, %useDirect, %useArranged) {
	%conn = new GameConnection(ServerConnection);
	%conn.setConnectArgs($pref::Player::Name);
	%conn.setJoinPassword(%password);
	%conn.connect(%address);
}

function ReConnectToServer()
{
	if (isObject($conn))
	{
		$conn.cancelConnect();
		$conn.delete();
		$conn = 0;
	}
	if (isObject(ServerConnection))
	{
		ServerConnection.cancelConnect();
		ServerConnection.delete();
	}
	if ($Connection::Direct)
	{
		$conn = new GameConnection(ServerConnection){
		};
		RootGroup.add($conn);
		$conn.setConnectArgs($pref::Player::LANName, $pref::Player::NetName, $Pref::Player::ClanPrefix, $Pref::Player::ClanSuffix, %clientNonce);
		$conn.setJoinPassword($Connection::Password);
		if ($Connection::Address $= "local")
		{
			$conn.connectLocal();
		}
		else
		{
			$conn.connect($Connection::Address);
		}
	}
	if ($Connection::Arranged)
	{
		$MatchMakerRequestID = $MatchMakerRequestID + 1.0;
		%requestId = $MatchMakerRequestID + 1.0;
		$arrangedConnectionRequestTime = getSimTime();
		sendArrangedConnectionRequest($Connection::Address, %requestId);
	}
}

function JoinServerPassGui::enterPass(%this)
{
	%pass = JSP_txtPass.getValue();
	if (%pass !$= "")
	{
		deleteDataBlocks();
		setParticleDisconnectMode(0);
		if (isObject($conn))
		{
			$conn.cancelConnect();
			$conn.delete();
			disconnectedCleanup();
		}
		if (Canvas.getContent().getName() !$= "LoadingGui")
		{
			Connecting_Text.setText("Connecting to " @ $ServerInfo::Address @ " with password");
			Canvas.pushDialog(connectingGui);
		}
		echo("");
		%so = $ServerSO[JS_serverList.getSelectedId()];
		if ($JoinNetServer)
		{
			echo("Connecting to " @ %so.name @ " (" @ $ServerInfo::Address @ ", " @ %so.ping @ "ms) with password");
		}
		else
		{
			echo("Connecting to LAN game with password");
		}
		echo("  Download Sounds:      " @ ($Pref::Net::DownloadSounds) ? "True" : "False");
		echo("  Download Music:       " @ ($Pref::Net::DownloadMusic) ? "True" : "False");
		echo("  Download Textures:    " @ ($Pref::Net::DownloadTextures) ? "True" : "False");
		echo("");
		if ($JoinNetServer)
		{
			%doDirect = 1;
			%doArranged = 1;
			if ($ServerInfo::Ping $= "???")
			{
				%doDirect = 1;
				%doArranged = 1;
			}
			else
			{
				if ($ServerInfo::Ping $= "---")
				{
					%doDirect = 0;
					%doArranged = 1;
				}
				else
				{
					if ($ServerInfo::Ping $= mFloor($ServerInfo::Ping))
					{
						%doDirect = 1;
						%doArranged = 0;
					}
					else
					{
						error("ERROR: Strange ping value " @ $ServerInfo::Ping @ "");
						%doDirect = 1;
						%doArranged = 1;
					}
				}
			}
			ConnectToServer($ServerInfo::Address, %pass, %doDirect, %doArranged);
		}
		else
		{
			ConnectToServer($ServerInfo::Address, %pass, 1, 0);
		}
		JSP_txtPass.setValue("");
		Canvas.popDialog("joinServerPassGui");
	}
}

function JoinServerPassGui::cancel(%this)
{
	Canvas.popDialog("joinServerPassGui");
	if (strlen($JoinGameAddress) > 1.0 || strlen($steamLobbyArg) > 1.0)
	{
		MainMenuGui.showButtons();
	}
	deleteVariables("$JoinGameAddress");
	deleteVariables("$steamLobbyArg");
	if (Canvas.getContent() $= "LoadingGui")
	{
		Canvas.setContent("MainMenuGui");
		MainMenuGui.showButtons();
	}
}

function filtersGui::onWake()
{
	Filter_PingMenu.clear();
	Filter_PingMenu.add(50, 50);
	Filter_PingMenu.add(100, 100);
	Filter_PingMenu.add(150, 150);
	Filter_PingMenu.add(250, 250);
	Filter_PingMenu.add(450, 450);
	Filter_PingMenu.add(999, 999);
	Filter_PingMenu.setSelected(mFloor($pref::Filter::maxPing));
	if (Filter_PingMenu.getSelected() <= 0.0)
	{
		Filter_PingMenu.setSelected(999);
	}
}

function filtersGui::onSleep()
{
	$pref::Filter::maxPing = Filter_PingMenu.getSelected();
	if ($JoinNetServer)
	{
		ServerInfoSO_DisplayAll();
	}
}
function urlEnc(%str) {
	%str = strReplace(%str," ","%20");
	return %str;
}
function getTimeScale() { return 1; }
$WebCom_PostSchedule = 0;
function WebCom_PostServer() {
	if($Server::LAN) {
		echo("Can\'t post to master server in LAN game");
		return;
	}
	if(!$missionRunning) {
		error("ERROR: WebCom_PostServer() - mission is not running");
		return;
	}
	echo("Posting to master server");
	if(isEventPending($WebCom_PostSchedule)) {
		cancel($WebCom_PostSchedule);
	}
	if(isObject(postServerTCPObj)) {
		postServerTCPObj.delete();
	}
	new TCPObject(postServerTCPObj);
	postServerTCPObj.site = $Pref::MasterServer;
	postServerTCPObj.port = 80;
	postServerTCPObj.filePath = "/postServer.php";
	%urlEncGameMode = urlEnc(fileBase($Server::MissionFile));
	%urlEncGameMode = strUpr(getSubStr(%urlEncGameMode,0,1)) @ strLwr(getSubStr(%urlEncGameMode,1,strLen(%urlEncGameMode)));
	%urlEncModPaths = "";
	if ($Pref::Server::Password !$= "") {
		%passworded = 1;
	} else {
		%passworded = 0;
	}
	if ($Server::Dedicated) {
		%dedicated = 1;
	} else {
		%dedicated = 0;
	}
	$Server::PlayerCount = ClientGroup.getCount();
	%postText = "ServerName=" @ urlEnc($Pref::Server::Name);
	%postText = %postText @ "&Port=" @ $Pref::Server::Port;
	%postText = %postText @ "&Players=" @ mFloor($Server::PlayerCount);
	%postText = %postText @ "&MaxPlayers=" @ mFloor($Pref::Server::MaxPlayers);
	%postText = %postText @ "&Map=" @ %urlEncGameMode;
	%postText = %postText @ "&Mod=" @ %urlEncModPaths;
	%postText = %postText @ "&Passworded=" @ %passworded;
	%postText = %postText @ "&Dedicated=" @ %dedicated;
	%postText = %postText @ "&BrickCount=0";
	%postText = %postText @ "&DemoPlayers=0";
	%postText = %postText @ "&blid=AAAAC";
	%postText = %postText @ "&csg=0002";
	%postText = %postText @ "&ver=0002";
	%postText = %postText @ "&build=0002";
	"postServerTCPObj".postText = %postText;
	"postServerTCPObj".postTextLen = strlen(%postText);
	"postServerTCPObj".cmd = "POST " @ postServerTCPObj.filePath @ " HTTP/1.0\r\n" @ "Host: " @ postServerTCPObj.site @ "\r\n" @ "User-Agent: Blockland-r002\r\n" @ "Content-Type: application/x-www-form-urlencoded\r\n" @ "Content-Length: " @ postServerTCPObj.postTextLen @ "\r\n" @ "\r\n" @ postServerTCPObj.postText @ "\r\n";
	postServerTCPObj.connect(postServerTCPObj.site @ ":" @ postServerTCPObj.port);
	%schduleTime = 5.0 * 60.0 * 1000.0 * getTimeScale();
	$WebCom_PostSchedule = schedule(%schduleTime, 0, WebCom_PostServer);
	$Server::lastPostTime = getSimTime();
}

function WebCom_PostServerUpdateLoop() {
	if(!isEventPending($WebCom_PostSchedule)) {
		WebCom_PostServer();
		return;
	}
	%timeLeft = getTimeRemaining($WebCom_PostSchedule);
	%schduleTime = 5.0 * 60.0 * 1000.0 * getTimeScale() * 0.2;
	if(%timeLeft > %schduleTime) {
		cancel($WebCom_PostSchedule);
		$WebCom_PostSchedule = schedule(%schduleTime, 0, WebCom_PostServer);
	}
}

function getMainMod()
{
	%modPaths = getModPaths();
	%modPaths = strreplace(%modPaths, ";", "\t");
	%count = getFieldCount(%modPaths);
	%bestMod = "base";
	%i = 0;
	return urlEnc("AddOns");
}

function postServerTCPObj::onConnected(%this)
{
	%this.send(%this.cmd);
}

function postServerTCPObj::onDNSFailed(%this)
{
	echo("Post to master server FAILED: DNS error. Retrying in 5 seconds...");
	%this.schedule(0, disconnect);
	schedule(5000, 0, "WebCom_PostServer");
}

function postServerTCPObj::onConnectFailed(%this)
{
	echo("Post to master server FAILED: Connection failure.  Retrying in 5 seconds...");
	%this.schedule(0, disconnect);
	schedule(5000, 0, "WebCom_PostServer");
}

function postServerTCPObj::onDisconnect(%this) {
}

function postServerTCPObj::onLine(%this, %line)
{
	%word = getWord(%line, 0);
	if (%word $= "HTTP/1.1")
	{
		%code = getWord(%line, 1);
		if (%code != 200.0)
		{
			warn("WARNING: postServerTCPObj - got non-200 http response " @ %code @ "");
		}
		if (%code >= 400.0 && %code <= 499.0)
		{
			warn("WARNING: 4xx error on postServerTCPObj, retrying");
			%this.schedule(0, disconnect);
			%this.schedule(500, connect, %this.site @ ":" @ %this.port);
		}
		if (%code >= 300.0 && %code <= 399.0)
		{
			warn("WARNING: 3xx error on postServerTCPObj, will wait for location header");
		}
	}
	else
	{
		if (%word $= "Location:")
		{
			%url = getWords(%line, 1);
			warn("WARNING: postServerTCPObj - Location redirect to " @ %url);
			%this.filePath = %url;
			%this.cmd = "POST " @ %this.filePath @ " HTTP/1.0\r\n" @ "Host: " @ %this.site @ "\r\n" @ "User-Agent: Blockland-r" @ getBuildNumber() @ "\r\n" @ "Content-Type: application/x-www-form-urlencoded\r\n" @ "Content-Length: " @ %this.postTextLen @ "\r\n" @ "\r\n" @ %this.postText @ "\r\n";
			%this.schedule(0, disconnect);
			%this.schedule(500, connect, %this.site @ ":" @ %this.port);
		}
		else
		{
			if (%word $= "Content-Location:")
			{
				%url = getWords(%line, 1);
				warn("WARNING: postServerTCPObj - Content-Location redirect to " @ %url);
				%this.filePath = %url;
				%this.cmd = "POST " @ %this.filePath @ " HTTP/1.0\r\n" @ "Host: " @ %this.site @ "\r\n" @ "User-Agent: Blockland-r" @ getBuildNumber() @ "\r\n" @ "Content-Type: application/x-www-form-urlencoded\r\n" @ "Content-Length: " @ %this.postTextLen @ "\r\n" @ "\r\n" @ %this.postText @ "\r\n";
				%this.schedule(0, disconnect);
				%this.schedule(500, connect, %this.site @ ":" @ %this.port);
			}
			else
			{
				if (%word $= "FAIL")
				{
					%reason = getSubStr(%line, 5, 1000);
					if (%reason $= "no host")
					{
						echo("No host entry in master server, re-sending authentication request");
					}
					else
					{
						if (%reason $= "no user, no host")
						{
							echo("No user/host entry in master server, re-sending authentication request");
						}
						else
						{
							echo("Posting to master failed.  Reason: " @ %reason);
						}
					}
				}
				else
				{
					if (%word $= "MMTOK")
					{
						%val = getWord(%line, 1);
					}
					else
					{
						if (%word $= "MATCHMAKER")
						{
							%val = getWord(%line, 1);
						}
						else
						{
							if (%word $= "NOTE")
							{
								%val = getWords(%line, 1, 99);
								echo("NOTE: " @ %val);
							}
						}
					}
				}
			}
		}
	}
}