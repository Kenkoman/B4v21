function startMissionGui::onWake(%this)
{
	SM_missionList.clear();
	%i = 0;
	for(%file = findFirstFile($Server::MissionFileSpec);
		%file !$= ""; %file = findNextFile($Server::MissionFileSpec))  
	if (strStr(%file, "CVS/") == -1 && strStr(%file, "common/") == -1)
		SM_missionList.addRow(%i++, getMissionDisplayName(%file) @ "\t" @ %file );
	SM_missionList.sort(0);
	SM_missionList.setSelectedRow(0);
	SM_missionList.scrollVisible(0);

	//load the saved server prefs
	TxtServerName.setValue($Pref::Server::Name);
	TxtServerInfo.setValue($Pref::Server::Info);
	TxtServerPassword.setValue($Pref::Server::Password);
	TxtServerAdminPassword.setValue($Pref::Server::AdminPassword);

	SliderNumPlayers.setValue($Pref::Server::MaxPlayers);
}

function startMissionGui::onSleep(%this)
{
	//save the server prefs from the screen
	//will this work? we might have started the server by the time this gets called.
	//UPDATE: nope, it doesnt
		
	
}

//----------------------------------------
function SM_StartMission()
{
	//save the pref values
	$Pref::Server::Name = TxtServerName.getValue();
	$Pref::Server::Info = TxtServerInfo.getValue();
	$Pref::Server::Password = TxtServerPassword.getValue();
	$Pref::Server::AdminPassword = TxtServerAdminPassword.getValue();
	$Pref::Server::MaxPlayers = SliderNumPlayers.getValue();

	//set your join password so you can join your own server
	$Client::Password = TxtServerPassword.getValue();


   %id = SM_missionList.getSelectedId();
   %mission = getField(SM_missionList.getRowTextById(%id), 1);

   if ($pref::HostMultiPlayer)
      %serverType = "MultiPlayer";
   else
      %serverType = "SinglePlayer";

   createServer(%serverType, %mission);
   %conn = new GameConnection(ServerConnection);
   RootGroup.add(ServerConnection);
   %conn.setConnectArgs($pref::Player::Name);
   %conn.setJoinPassword($Client::Password);
	error("-----------CONN = ", %conn);
   %conn.connectLocal();
}

//----------------------------------------
function getMissionDisplayName( %missionFile ) 
{
   %file = new FileObject();
   
   %MissionInfoObject = "";
   
   if ( %file.openForRead( %missionFile ) ) {
		%inInfoBlock = false;
		
		while ( !%file.isEOF() ) {
			%line = %file.readLine();
			%line = trim( %line );
			
			if( %line $= "new ScriptObject(MissionInfo) {" )
				%inInfoBlock = true;
			else if( %inInfoBlock && %line $= "};" ) {
				%inInfoBlock = false;
				%MissionInfoObject = %MissionInfoObject @ %line; 
				break;
			}
			
			if( %inInfoBlock )
			   %MissionInfoObject = %MissionInfoObject @ %line @ " "; 	
		}
		
		%file.close();
	}
	%MissionInfoObject = "%MissionInfoObject = " @ %MissionInfoObject;
	eval( %MissionInfoObject );
	
   %file.delete();

   if( %MissionInfoObject.name !$= "" )
      return %MissionInfoObject.name;
   else
      return fileBase(%missionFile); 
}
