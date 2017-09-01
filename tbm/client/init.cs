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
   echo("\n--------- Initializing TBM: Client ---------");

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
   initCanvas("The Orange Block");

   /// Load client-side Audio Profiles/Descriptions
   exec("./scripts/audioProfiles.cs");

   // Load up the Game GUIs
   exec("./ui/defaultGameProfiles.cs");
   exec("./ui/defaultProfiles.cs");
   exec("./ui/PlayGui.gui");
   exec("./ui/ChatHud.gui");
   exec("./ui/playerList.gui");
   exec("./ui/DecalPalletGui.gui");
   exec("./ui/MessageBoxYesNoDlg.gui");
   exec("./ui/SaveFileDlg.gui");
   exec("./ui/MessageBoxOKCancelDlg.gui");
   exec("./ui/MessageBoxOkDlg.gui");
   exec("./ui/MessagePopupDlg.gui");
   exec("./ui/RecordingsDlg.gui");
   exec("./ui/helpdlg.gui");
   exec("./ui/loadfiledlg.gui");
   exec("./ui/consoledlg.gui");
   exec("./scripts/quickinventory.cs");
   
   // Load up the shell GUIs
   exec("./ui/mainMenuGui.gui");
   exec("./ui/aboutDlg.gui");
   exec("./ui/loadingGui.gui");
   exec("./ui/startMissionGui.gui");
   exec("./ui/joinServerGui.gui");
   exec("./ui/endGameGui.gui");
   exec("./ui/optionsDlg.gui");
   exec("./ui/remapDlg.gui");
   exec("./ui/SpecialOpGui.gui");
   exec("./ui/TBMPlayerOpGui.gui");
   exec("./ui/teamList.gui");
   exec("./ui/DECALFOUNDGUI.gui");
   exec("./ui/PersListDlg.gui");
   exec("./ui/ColorPalletGui.gui");
   exec("./ui/changemap.gui");
   exec("./ui/tbmhelpdlg.gui");
   exec("./ui/profilegui.gui");
   exec("./ui/playerprofilegui.gui");
   exec("./ui/advancedprefsgui.gui");
   exec("./ui/helpeditgui.gui");
   exec("./ui/igobgui.gui");
   exec("./ui/quickinventorygui.gui");
   exec("./ui/NewsDlg.gui");
   exec("./ui/PresetsGUI.gui");
   exec("./ui/ServerNews.gui");
   exec("./ui/ServerNewsEditor.gui");
   exec("./ui/FirstRunGUI.gui");
   exec("./ui/quitConfirmDialog.gui");
   exec("./ui/switchActionGui.gui");
   exec("./ui/switchAddActionGui.gui");
   exec("./ui/SwitchStuffGui.gui");
   exec("./ui/escapeMenuGui.gui");
   exec("./ui/disconnectConfirmDialog.gui");
   exec("./ui/passwordPromptDlg.gui");

   // Client scripts
   exec("./scripts/mainmenu.cs");
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
   exec("./scripts/TBMPlayerOpGui.cs");
   exec("./scripts/imggalleries.cs");
   exec("./scripts/optionsDlgfunctions.cs");
   exec("./scripts/SpecialOp.cs");
   exec("./scripts/newListLoad.cs");
   exec("./scripts/joinservergui.cs");
   exec("./scripts/changemap.cs");
   exec("./scripts/playerprofile.cs");
   exec("./scripts/advancedprefs.cs");
   exec("./scripts/iGobs.cs");
   exec("./scripts/showcaseServer.cs");
   
   // Default player key bindings
   exec("./scripts/default.bind.cs");
   exec("./config.cs");

   //administrator gui
   exec("./ui/adminGui.gui");
   exec("./scripts/adminGui.cs");

  if (strstr(getmodpaths(),"dtb")>=0)
    setupdtbclient();
   // Really shouldn't be starting the networking unless we are
   // going to connect to a remote server, or host a multi-player
   // game.
   setNetPort(0);

   exec("./scripts/extras.cs");
   translateTime();

   // Copy saved script prefs into C++ code.
   setShadowDetailLevel( $pref::shadows );
   setDefaultFov( $pref::Player::defaultFov );
   setZoomSpeed( $pref::Player::zoomSpeed );

   // Start up the main menu... this is separated out into a
   // method for easier mod override.
   loadMainMenu();
   setupcolorpallet();
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
   Canvas.setCursor($Cursor[$Pref::Gui::Cursor]);
}