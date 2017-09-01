//-----------------------------------------------------------------------------

// Variables used by client scripts & code.  The ones marked with (c)
// are accessed from code.  Variables preceeded by Pref:: are client
// preferences and stored automatically in the ~/client/prefs.cs file
// in between sessions.
//
//    (c) Client::MissionFile             Mission file name
//    ( ) Client::Password                Password for server join

//    (?) Pref::Player::CurrentFOV
//    (?) Pref::Player::DefaultFov
//    ( ) Pref::Input::KeyboardTurnSpeed

//    (c) pref::Master[n]                 List of master servers
//    (c) pref::Net::RegionMask     
//    (c) pref::Client::ServerFavoriteCount
//    (c) pref::Client::ServerFavorite[FavoriteCount]
//    .. Many more prefs... need to finish this off

// Moves, not finished with this either...
//    (c) firstPerson
//    $mv*Action...

//-----------------------------------------------------------------------------
// These are variables used to control the shell scripts and
// can be overriden by mods:

//-----------------------------------------------------------------------------

function initClient()
{
   echo("\n--------- Initializing FPS: Client ---------");

   // Make sure this variable reflects the correct state.
   $Server::Dedicated = false;

   // Game information used to query the master server
   $Client::GameTypeQuery = "Lego";
   $Client::MissionTypeQuery = "Any";

   // The common module provides basic client functionality
   initBaseClient();

   // InitCanvas starts up the graphics system.
   // The canvas needs to be constructed before the gui scripts are
   // run because many of the controls assume the canvas exists at
   // load time.
   initCanvas("Return to Blockland 1.045");

   /// Load client-side Audio Profiles/Descriptions
   exec("./scripts/audioProfiles.cs");

   // Load up the Game GUIs
   exec("./ui/defaultGameProfiles.cs");
   exec("./ui/PlayGui.gui");
   exec("./ui/ChatHud.gui");
   exec("./ui/playerList.gui");

   // Load up the shell GUIs
   exec("./ui/mainMenuGui.gui");
   exec("./ui/aboutDlg.gui");
   exec("./ui/PasswordBox.gui");
   exec("./ui/startMissionGui.gui");
   exec("./ui/joinServerGui.gui");
   exec("./ui/endGameGui.gui");
   exec("./ui/loadingGui.gui");
   exec("./ui/optionsDlg.gui");
   exec("./ui/remapDlg.gui");
   exec("./ui/messagegui.gui");
   exec("./ui/EditorGUI.gui");
   exec("./ui/CopsAndRobbers.gui");
   exec("./ui/moversGUI.gui");
   exec("./ui/botOpGUI.gui");
   exec("./ui/HelpDlg.gui");
   exec("./ui/MessageBoxOKDlg.gui");
   exec("./ui/MessageBoxYesNoDlg.gui");
   exec("./ui/messageGUI.gui");
   exec("./ui/MessagePopupDlg.gui");
   exec("./ui/serverconfig.gui");
   exec("./ui/serverrulesgui.gui");
   exec("./ui/UpdateDlg.gui");
   exec("./ui/brickProperties.gui");
   exec("./ui/playerrights.gui");
   exec("./ui/AMbbc.gui");
   exec("./ui/bbcew.gui");
   exec("./ui/bbcrate.gui");
   exec("./ui/brickFX.gui");
   exec("./ui/admingui.gui");
   exec("./ui/fAdmin.gui");
   exec("./ui/persistenceLoad.gui");
   exec("./ui/AMcandr.gui");
   exec("./ui/AMdeathmatch.gui");
   exec("./ui/appearance.gui");
   exec("./ui/register.gui");
   exec("./ui/login.gui");
   exec("./ui/impulseGUI.gui");
   exec("./ui/tgelobby/execall.cs");

   // Client scripts
   exec("./scripts/client.cs");
   exec("./scripts/missionDownload.cs");
   exec("./scripts/serverConnection.cs");
   exec("./scripts/playerList.cs");
   exec("./scripts/loadingGui.cs");
   exec("./scripts/optionsDlg.cs");
   exec("./scripts/chatHud.cs");
   exec("./scripts/messageHud.cs");
   exec("./scripts/playGui.cs");
   exec("./scripts/centerPrint.cs");
   exec("./scripts/game.cs");
   exec("./scripts/msgCallbacks.cs");
   exec("./scripts/startMissionGui.cs");
   exec("./scripts/faceprintselect.cs");
   exec("./scripts/printselect.cs");
   exec("./scripts/EditorGUI.cs");
   exec("./scripts/botOpGUI.cs");
   // Default player key bindings
   exec("./scripts/default.bind.cs");
   exec("./config.cs");
   exec("./version.cs");

   //administrator gui
   exec("./scripts/adminGui.cs");

   // Really shouldn't be starting the networking unless we are
   // going to connect to a remote server, or host a multi-player
   // game.
   setNetPort(0);

   // Copy saved script prefs into C++ code.
   setShadowDetailLevel( $pref::shadows );
   setDefaultFov( $pref::Player::defaultFov );
   setZoomSpeed( $pref::Player::zoomSpeed );

   // Start up the main menu... this is separated out into a 
   // method for easier mod override.
   loadMainMenu();

   // Connect to server if requested.
   if ($JoinGameAddress !$= "") {
      connect($JoinGameAddress, "", $Pref::Player::Name);
   }
}


//-----------------------------------------------------------------------------

function loadMainMenu()
{
   // Startup the client with the Main menu...
   Canvas.setContent( MainMenuGui );
   Canvas.setCursor("DefaultCursor");
}

