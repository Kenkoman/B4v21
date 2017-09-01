/////////////////////////////////////////////////
// Function to set teams for TDM
//////////////////////////////////////////////////
function createteams (%setup) {
  $TeamCount = 0;
  switch$ (%setup) {
    case "default2":
	$Teams[1] = "red";
 	$Teams[2] = "blue";
        $TeamCount = 2;
    case "default3":
	$Teams[1] = "red";
 	$Teams[2] = "blue";
 	$Teams[3] = "green";
        $TeamCount = 3;
    case "default4":
	$Teams[1] = "red";
 	$Teams[2] = "blue";
 	$Teams[3] = "green";
 	$Teams[4] = "yellow";
        $TeamCount = 4;
    }
  }

function serverCmdGetTeamList(%client) {
  %count = 0;
  for (%i = 1; %i <= $TeamCount; %i++) {
    %team = "\c3\x96" @ $Teams[%i] @ " Team";
    commandToClient(%client, 'FillTeamList', %team);
    for (%clientIndex = 0; %clientIndex < ClientGroup.getCount(); %clientIndex++) {
      %cl = ClientGroup.getObject( %clientIndex );
      if ($Teams[%i] $= %cl.team) {
        %name = %cl.namebase;
        commandToClient(%client, 'FillTeamList', %name);
        }
      }
    }
  }

function assignateam(%client) {
  %red=0;
  %blue=0;
  %green=0;
  %yellow=0;
  for( %i = 0; %i < ClientGroup.getCount(); %i++) {
        %vclient = ClientGroup.getObject(%i);
        if (%vclient.team $= "red")
          %red++;
        else if (%vclient.team $= "blue")
          %blue++;
        else if (%vclient.team $= "green")
          %green++;
        else if (%vclient.team $= "yellow")
          %yellow++;
    }
  if ($TeamCount == 2) {
    if (%red <= %blue)
      %client.team = "red";
    else if (%blue < %red)
      %client.team = "blue";
    }
  else if ($TeamCount == 3) {
    if (%red <= %blue && %red <= %green)
      %client.team = "red";
    else if (%blue <= %red && %blue <= %green)
      %client.team = "blue";
    else if (%green <= %red && %green <= %blue)
      %client.team = "green";
    }
  else if ($TeamCount == 4) {
    if (%red <= %blue && %red <= %green && %red <= %yellow)
      %client.team = "red";
    else if (%blue <= %red && %blue <= %green && %blue <= %yellow)
      %client.team = "blue";
    else if (%green <= %red && %green <= %blue && %green <= %yellow)
      %client.team = "green";
    else if (%yellow <= %red && %yellow <= %blue && %yellow <= %green )
      %client.team = "yellow";
    }
  echo("your team is "@%client.team);
}

function servercmdteamrebal(%client) {
  if ($TeamCount <= 0) {
    echo("WTF");
    return;
    }
  if (%client.isadmin || %client.issuperadmin || %client.ismod || %client==0) {
    for( %i = 0; %i <= ClientGroup.getCount(); %i+=$TeamCount) {
      ClientGroup.getObject(%i).team = "red";
      ClientGroup.getObject(%i+1).team = "blue";
      if ($TeamCount >= 3)
        ClientGroup.getObject(%i+2).team = "green";
      if ($TeamCount == 4)
        ClientGroup.getObject(%i+3).team = "yellow";

      }
    for( %i = 0; %i < ClientGroup.getCount(); %i++ )
      ClientGroup.getObject(%i).player.kill();
    }
  }

function serverCmdJoinTeam(%client, %team) {
  if ($pref::server::lockteams || $TeamCount == 0)
    return;
  for (%i = 1; %i <= $TeamCount; %i++) {
    if ($Teams[%i] $= %team) {
      %client.team = %i;
      %msgteamsting = "\c2\x96\c3" @ %victim.namebase @ " \c2just joined the \c3" @ %team @ " \c2team.";
      messageAll("MsgClientJoinedATeam", %msgteamstring);
      }
    }
  }

function serverCmdcreateteams (%client,%setup) {
  if (%client.isadmin) {
    createteams (%setup);
    switch ($TeamCount) {
      case 2:
        messageAll('Msg', "\c3The admin ("@%client.namebase@") has created Red and Blue teams, please pick a team to play, or have the admin lock teams, assign current clients to a team, and turn on auto assign for furture clients.");
      case 3:
        messageAll('Msg', "\c3The admin ("@%client.namebase@") has created Red, Blue, and Green teams, please pick a team to play, or have the admin lock teams, assign current clients to a team, and turn on auto assign for furture clients.");
      case 4:
        messageAll('Msg', "\c3The admin ("@%client.namebase@") has created Red, Blue, Green, and Yellow teams, please pick a team to play, or have the admin lock teams, assign current clients to a team, and turn on auto assign for furture clients.");
      }
    }
  }

function serverCmdclearteams (%client) {
  if (%client.isadmin) {
    $teamcount=0;
    for( %i = 0; %i < ClientGroup.getCount(); %i++) {
      %cl = ClientGroup.getObject(%i);
      %cl.team="";
      commandToClient(%cl, 'updatePrefs');
      if (%cl.carrier)
        ServerCmddropInventory( %cl, 9);
      }
    $pref::server::autoteambalance = 0;
    messageAll('Msg', "\c3The admin ("@%client.namebase@") has cleared the teams.  Join team and autobalance are disabled.");
    }
  }