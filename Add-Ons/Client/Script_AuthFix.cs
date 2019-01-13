if($Pref::MasterServer $= "")
 $Pref::MasterServer = "clayhanson.x10host.com:80";

package CustomMSPackage {
	function authTCPObj::disconnect(%this) {
		Parent::disconnect(%this);
		if($AuthingKey == 2) $AuthingKey = 0;
		if($AuthingKey != 1) return;
		$AuthingKey = 2;
		schedule(100,0,auth_Init_Client_Real);
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
		%postText = %postText @ "&BrickCount=" @ mFloor($Server::BrickCount);
		%postText = %postText @ "&DemoPlayers=" @ mFloor($Pref::Server::AllowDemoPlayers);
		%postText = %postText @ "&blid=" @ urlEnc(getKeyID());
		%postText = %postText @ "&csg=" @ urlEnc(getVersionNumber());
		%postText = %postText @ "&ver=" @ ($version $= "4_01" ? getStringCRC("BLACKDONG") : $version);
		%postText = %postText @ "&build=1988";
		%postText = %postText @ "&ourName=" @ urlEnc($Pref::Player::NetName);
		%this.postText = %postText;
		%this.cmd = %finishedCMD;
		%this.postTextLen = strlen(%postText);
		%this.cmd = "POST /postServer.php HTTP/1.0\r\n" @ "Host: " @ $Pref::MasterServer @ "\r\n" @ "User-Agent: Blockland-r1988\r\n" @ "Content-Type: application/x-www-form-urlencoded\r\n" @ "Content-Length: " @ %this.postTextLen @ "\r\n" @ "\r\n" @ %this.postText @ "\r\n";
		%oldLen = strLen(getField(%this.cmd,getFieldCount(%this.cmd)-1))-1;
		%this.cmd = setField(%this.cmd,getFieldCount(%this.cmd)-1,getField(%this.cmd,getFieldCount(%this.cmd)-1)@"&Patch=1");
		%newLen = strLen(getField(%this.cmd,getFieldCount(%this.cmd)-1))-1;
		%this.cmd = strReplace(%this.cmd,"Content-Length: " @ %oldLen,"Content-Length: " @ %newLen);
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
	function MM_AuthText::setText(%this, %text) {
		if(%text $= "Authentication: Validating key...") {
			if($AuthingKey != 0) { Parent::setText(%this, %text); return; }
			$AuthingKey = 1;
			$oldver = $version;
			$version = "21";
			auth_Init_Client_Real();
			return;
		}
		Parent::setText(%this, %text);
	}
	function MM_AuthBar::blinkSuccess(%this) {
		Parent::blinkSuccess(%this);
		if(!$AuthingKey) return;
		$version = $oldver;
		$AuthingKey = 0;
	}
};
activatePackage(CustomMSPackage);
function clientCmdOpenPrintSelectorDlg(%aspectRatio, %startPrint, %numPrints) {
	if(PSD_Window.scrollcount $= "") PSD_Window.scrollcount = 0;
	if(!isObject("PSD_PrintScroller" @ %aspectRatio)) PSD_LoadPrints(%aspectRatio, %startPrint, %numPrints);
	if(!isObject("PSD_PrintScrollerLetters")) PSD_LoadPrints("Letters", $PSD_letterStart, $PSD_numLetters);
	$PSD_NumPrints = %numPrints;
	Canvas.pushDialog("printSelectorDlg");
	if($PSD_LettersVisible || $PSD_NumPrints == 0)
		PSD_PrintScrollerLetters.setVisible(1);
	else {
		%obj = "PSD_PrintScroller" @ %aspectRatio;
		%obj.setVisible(1);
	}
	$PSD_CurrentAR = %aspectRatio;
}
function auth_Init_Client_Real() {
	authTCPObj.site = "auth.blockland.us";
	authTCPObj.port = 80;
	if($AuthingKey == 0) $AuthingKey = 1;
	switch($AuthingKey) {
		case 1:
			authTCPObj.passPhraseCount = 0;
			authTCPObj.done = 0;
			authTCPObj.success = 0;
			authTCPObj.filePath = "/authInit.php";
			%postText = "ID=" @ getKeyID();
			%N = getNonsense("86");
			%postText = %postText @ "&N="@%N;
			%postText = %postText @ "&VER="@$version;
			authTCPObj.postText = %postText;
			authTCPObj.postTextLen = strlen(%postText);
			authTCPObj.cmd = "POST " @ authTCPObj.filePath @ " HTTP/1.0\r\n" @ "Host: " @ authTCPObj.site @ "\r\n" @ "User-Agent: Blockland-r1988" @ "\r\n" @ "Content-Type: application/x-www-form-urlencoded\r\n" @ "Content-Length: " @ authTCPObj.postTextLen @ "\r\n" @ "\r\n" @ authTCPObj.postText @ "\r\n";
			authTCPObj.connect(authTCPObj.site @ ":" @ authTCPObj.port);
		case 2:
			authTCPObj.cmd = strReplace(authTCPObj.cmd,authTCPObj.filePath,"/authConfirm2.php");
			authTCPObj.connect(authTCPObj.site @ ":" @ authTCPObj.port);
	}
}
