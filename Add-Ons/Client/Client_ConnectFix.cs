// we need to re-parse the command line arguments because -connect is wasted in the main.cs file
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

package ConnectFix {
	function MM_AuthBar::BlinkSuccess(%this) {
		if ($connectArg !$= "")
		{
			// for lack of a better way to do this, we're going to abuse the manual join gui
			MJ_txtIP.setValue($connectArg);
			MJ_connect();
		}
		return Parent::BlinkSuccess(%this);
	}
	// this is so it asks for a password if the server is passworded
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
activatePackage(ConnectFix);
