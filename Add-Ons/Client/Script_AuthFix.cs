if($Pref::MasterServer $= "")
 $Pref::MasterServer = "clayhanson.x10host.com:80";

function getBrickCount() {
	// This isn't implemented in v1.03 for some reason
	%count = 0;
	for(%i=0;%i<mainBrickGroup.getCount();%i++) {
		%count += mainBrickGroup.getObject(%i).getCount();
	}
	return %count;
}
package CustomMSPackage {
	function quit() {
		disconnect();
		Parent::quit();
	}
	function authTCPObj::connect(%this, %addr) {
		if($auth_dontConnect) return;
		Parent::connect(%this, %addr);
	}
	function auth_Init() {
		if($AuthingKey != 0) { Parent::setText(%this, %text); return; }
		$auth_dontConnect = 1;
		Parent::auth_Init();
		$auth_dontConnect = 0;
		$AuthingKey = 1;
		if($version !$= "21") $oldver = $version;
		$version = "21";
		auth_Init_Client_Real();
	}
	function authTCPObj::disconnect(%this) {
		Parent::disconnect(%this);
		if($AuthingKey == 2) {
			$AuthingKey = 0;
			$version = $oldver;
		}
		if($AuthingKey != 1) return;
		$AuthingKey = 2;
		schedule(100,0,auth_Init_Client_Real);
	}
	function authTCPObj::onLine(%this, %line) {
		if(getWord(%line,0) $= "PASSPHRASE") {
			%passphrase = getWord(%line, 1);
			if(getKeyID() !$= "") {
				%crc = getPassPhraseResponse(%passphrase, %this.passPhraseCount);
				if(%crc !$= "") {
					%this.filePath = "/authConfirm2.php";
					if($NewNetName !$= "") {
						%postText = "CRC=" @ %crc @ "&NAME=" @ urlEnc($NewNetName);
					} else {
						%postText = "CRC=" @ %crc;
					}
					%postText = %postText @ MM_AuthBar::getExtendedPostString();
					%this.postText = %postText;
					%this.postTextLen = strlen(%postText);
					%this.cmd = "POST " @ %this.filePath @ " HTTP/1.0\r\n" @ "Cookie: " @ %this.cookie @ "\r\n" @ "Host: " @ %this.site @ "\r\n" @ "User-Agent: Blockland-r1988\r\n" @ "Content-Type: application/x-www-form-urlencoded\r\n" @ "Content-Length: " @ %this.postTextLen @ "\r\n" @ "\r\n" @ %this.postText @ "\r\n";
					%this.schedule(0, disconnect);
					%this.schedule(10, connect, authTCPObj.site @ ":" @ authTCPObj.port);
				}
				%this.passPhraseCount++;
			} else {
				echo("Authentication: FAIL No key");
				MM_AuthText.setText("Authentication FAILED: No key found.");
				lock();
			}
			return;
		}
		Parent::onLine(%this,%line);
	}
	function servAuthTCPObj::connect(%this, %address) {
		%this.cmd = strReplace(%this.cmd, %this.filePath, "/authQuery.php");
		%this.cmd = strReplace(%this.cmd, %this.site, "auth.blockland.us");
		Parent::connect(%this, %address);
	}
	function postServerTCPObj::connect(%this, %addr) {
		%urlEncName = urlEnc($Server::Name);
		%urlEncGameMode = urlEnc($GameModeDisplayName);
		%urlEncModPaths = "";
		%postText = "ServerName=" @ urlEnc($Pref::Server::Name);
		%postText = %postText @ "&Port=" @ mFloor($Server::Port);
		%postText = %postText @ "&Players=" @ mFloor($Server::PlayerCount);
		%postText = %postText @ "&MaxPlayers=" @ mFloor($Pref::Server::MaxPlayers);
		%postText = %postText @ "&Map=" @ %urlEncGameMode;
		%postText = %postText @ "&Mod=" @ %urlEncModPaths;
		%postText = %postText @ "&Passworded=" @ ($Pref::Server::Password $= "" ? "0" : "1");
		%postText = %postText @ "&Dedicated=" @ ($Server::Dedicated ? "1" : "0");
		%postText = %postText @ "&BrickCount=" @ mFloor(getBrickCount());
		%postText = %postText @ "&DemoPlayers=" @ mFloor($Pref::Server::AllowDemoPlayers);
		%postText = %postText @ "&blid=" @ urlEnc(getKeyID());
		%postText = %postText @ "&csg=" @ urlEnc(getVersionNumber());
		%postText = %postText @ "&ver="@$version;
		%postText = %postText @ "&ourName=" @ urlEnc(trim($pref::Player::NetName));
		%postText = %postText @ "&build=1988";
		%postText = %postText @ "&Patch=1";
		%this.postText = %postText;
		%this.cmd = %finishedCMD;
		%this.postTextLen = strlen(%postText);
		%this.cmd = "POST /postServer.php HTTP/1.0\r\n" @ "Host: " @ $Pref::MasterServer @ "\r\n" @ "User-Agent: Blockland-r1988\r\n" @ "Content-Type: application/x-www-form-urlencoded\r\n" @ "Content-Length: " @ %this.postTextLen @ "\r\n" @ "\r\n" @ %this.postText @ "\r\n";
		parent::connect(%this, $Pref::MasterServer);
	}
	function queryMasterTCPObj::connect(%this, %addr) {
		%this.cmd = "GET /index.php HTTP/1.0\r\nHost: " @ queryMasterTCPObj.site @ "\r\n\r\n";
		%this.cmd = strReplace(%this.cmd, %this.site, $Pref::MasterServer);
		parent::connect(%this, $Pref::MasterServer);
	}
	function AutoUpdateGui::onWake(%this) {
		Parent::onWake(%this);
		Canvas.popDialog(%this);
	}
	// Auto connect to the server if the -connect argument is specified
	function MM_AuthBar::BlinkSuccess(%this) {
		if ($connectArg !$= "" && !$Server::Dedicated)
		{
			// for lack of a better way to do this, we're going to abuse the manual join gui
			MJ_txtIP.setValue($connectArg);
			MJ_connect();
		}
		return Parent::BlinkSuccess(%this);
	}
	// If the server is passworded, ask for it
	function GameConnection::onConnectRequestRejected(%this, %msg)
	{
		if (%msg $= "CHR_PASSWORD" && $connectArg !$= "")
		{
			$JoinNetServer = 1;
			$ServerInfo::Ping = "???";
			$ServerInfo::Address = $connectArg;
			Canvas.popDialog(connectingGui);
			Canvas.pushDialog(JoinServerPassGui);
			return;
		}
		deleteVariables("$connectArg");
		return Parent::onConnectRequestRejected(%this, %msg);
	}
	// make sure the $connectArg variable becomes unset
	function connectingGui::cancel()
	{
		deleteVariables("$connectArg");
		Parent::cancel(%this);
	}
	function disconnectedCleanup()
	{
		deleteVariables("$connectArg");
		return Parent::disconnectedCleanup();
	}
	function JoinServerPassGui::cancel(%this)
	{
		deleteVariables("$connectArg");
		Parent::cancel(%this);
	}
	function keyGui::cancel(%this)
	{
		deleteVariables("$connectArg");
		Parent::cancel(%this);
	}
};
};
activatePackage(CustomMSPackage);

function auth_Init_Client_Real() {
	authTCPObj.site = "auth.blockland.us";
	authTCPObj.port = 80;
	authTCPObj.passPhraseCount = 0;
	authTCPObj.done = 0;
	authTCPObj.success = 0;
	authTCPObj.filePath = "/authInit.php";
	%postText = "ID=" @ getKeyID();
	%postText = %postText @ "&N="@getNonsense("86");
	%postText = %postText @ "&VER="@$version;
	authTCPObj.postText = %postText;
	authTCPObj.postTextLen = strlen(%postText);
	authTCPObj.cmd = "POST " @ authTCPObj.filePath @ " HTTP/1.0\r\n" @ "Host: " @ authTCPObj.site @ "\r\n" @ "User-Agent: Blockland-r1988" @ "\r\n" @ "Content-Type: application/x-www-form-urlencoded\r\n" @ "Content-Length: " @ authTCPObj.postTextLen @ "\r\n" @ "\r\n" @ authTCPObj.postText @ "\r\n";
	authTCPObj.connect(authTCPObj.site @ ":" @ authTCPObj.port);
}
function MM_AuthBar::getExtendedPostString() {
	%postText = "";
	%postText = %postText @ "&DEDICATED=" @ $Server::Dedicated;
	%postText = %postText @ "&PORT=" @ $Pref::Server::Port;
	%postText = %postText @ "&VER=1.03";
	%postText = %postText @ "&BUILD=1988";
	%postText = %postText @ "&RAM=4028";
	%postText = %postText @ "&DIR=C:/fakepath/Blockland";
	%postText = %postText @ "&OSSHORT=, 64-bit";
	%postText = %postText @ "&OSLONG= (build 9200)";
	%postText = %postText @ "&CPU=Intel";
	%postText = %postText @ "&MHZ=3100";
	%postText = %postText @ "&U=342795297342123";
	%postText = %postText @ "&NETTYPE=" @ mFloor($Pref::Net::ConnectionType);
	%postText = %postText @ "&GPUMAN=None";
	%postText = %postText @ "&GPU=None";
	%postText = %postText @ "&GPU=Intel(R)_HD_Graphics";
	%postText = %postText @ "&GLVersion=4.0.0 - Build 10.18.10.4358";
	%postText = %postText @ "&GLEW_ARB_shader_objects=nan";
	%postText = %postText @ "&GLEW_ARB_shading_language_100=nan";
	%postText = %postText @ "&GLEW_EXT_texture_array=nan";
	%postText = %postText @ "&GLEW_EXT_texture3D=nan";
	%postText = %postText @ "&glTexImage3D=nan";
	%postText = %postText @ "&GLEW_EXT_framebuffer_object=nan";
	%postText = %postText @ "&GLEW_ARB_shadow=nan";
	%postText = %postText @ "&GLEW_ARB_texture_rg=nan";
	%postText = %postText @ "&getShaderVersion=1.0";
	return %postText;
}

// Re-parse the command line arguments in order to find what server we're connecting to
for ($i = 1; $i < $Game::argc ; $i++)
{
	%allArgs = %allArgs SPC $Game::argv[$i];
}

for ($i = 1; $i < $Game::argc ; $i++)
{
	$arg = $Game::argv[$i];
	$nextArg = $Game::argv[$i+1];
	$hasNextArg = $Game::argc - $i > 1;
	$logModeSpecified = false;

	switch$ ($arg)
	{
	 //--------------------
	 case "-connect":
		$argUsed[$i]++;
		if ($hasNextArg) {
			$connectArg = $nextArg;
			$argUsed[$i+1]++;
			echo("Saved connection argument " SPC $connectArg);
			$i++;
		}
		else
			error("Error: Missing Command Line argument. Usage: -connect <ip_address>");
	}
}
