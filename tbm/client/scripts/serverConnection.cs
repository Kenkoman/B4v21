//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

// Functions dealing with connecting to a server


//-----------------------------------------------------------------------------
// Server connection error
//-----------------------------------------------------------------------------

addMessageCallback( 'MsgConnectionError', handleConnectionErrorMessage );

function handleConnectionErrorMessage(%msgType, %msgString, %msgError)
{
   // On connect the server transmits a message to display if there
   // are any problems with the connection.  Most connection errors
   // are game version differences, so hopefully the server message
   // will tell us where to get the latest version of the game.
   $ServerConnectionErrorMessage = %msgError;
}


//----------------------------------------------------------------------------
// GameConnection client callbacks
//----------------------------------------------------------------------------

function GameConnection::initialControlSet(%this)
{
   echo ("*** Initial Control Object");

   // The first control object has been set by the server
   // and we are now ready to go.
   
   // first check if the editor is active
   if (!Editor::checkActiveLoadDone())
   {
      if(Canvas.getContent() != PlayGui.getId() && !MissionGroup.showcaseServer)
        Canvas.setContent(PlayGui);
      if(MissionGroup.showcaseServer) {
         SlideShow_Holder.setVisible(0);
         if(Canvas.getContent() != MainMenuGUI.getId())
            Canvas.setContent(MainMenuGUI);
      }
    }

    if ($Pref::Server::GameType == 1) {
    	if (Canvas.getContent() != chooseteamgui.getId()) {
		Canvas.setContent(TeamSelectDlg);
	}
    }
}

function GameConnection::setLagIcon(%this, %state)
{
   if (%this.getAddress() $= "local")
      return;
   LagIcon.setVisible(%state $= "true");
}

function GameConnection::connect(%this, %address) {
%this.remoteAddress = %address;
Parent::connect(%this, %address);
}

function GameConnection::onConnectionAccepted(%this) {
	// Called on the new connection object after connect() succeeds.
cancel($ShowcaseLoadBlockSchedule);
%name = %this.getName();
%this.setName("ServerConnection_");
if(isObject(serverConnection) && (!isObject(missionGroup) || (isObject(missionGroup) && ($Game::StartTime !$= "" || $Game::StartTime !$= $Sim::Time)))) {
  TCPGroup.add(%this); //Previous serverConnection has been found
  disconnect();
}
%this.setName(%name);

LagIcon.setVisible(false);
commandToServer('clientVersion', $pref::client::version);
commandtoserver('ghostcolor',"ghost"@$ghostcolor[$Pref::Player::Ghostcolor]);
commandtoserver('clientprofile',$Pref::Player::TotalPlantedBricks, $Pref::Player::ProfileInfo);
commandToServer('iGobcolor', $legocolor[$Pref::Player::iGobColor]);
commandtoserver('editoroptions',$pref::Editor::MoveFactor,$pref::Editor::RotateFactor);
//commandToServer('autoperm');
}

//And one of these (I think it's onConnectionTimedOut) needs to not call disconnectedCleanup.  I forget why, but an invalid connection can call the method in quesiton while a valid connection still exists, and disconnectedCleanup will kill the valid one.
function GameConnection::onConnectionTimedOut(%this)
{
   // Called when an established connection times out
   disconnectedCleanup();
   MessageBoxOK( "TIMED OUT", "The server connection has timed out.");
}

function GameConnection::onConnectionDropped(%this, %msg)
{
   // Established connection was dropped by the server
   disconnectedCleanup();
   MessageBoxOK( "DISCONNECT", "The server has dropped the connection: " @ %msg);
}

function GameConnection::onConnectionError(%this, %msg)
{
   // General connection error, usually raised by ghosted objects
   // initialization problems, such as missing files.  We'll display
   // the server's connection error message.
   if (isObject(ServerConnection))
      ServerConnection.schedule(0, delete);
   disconnectedCleanup();
   MessageBoxOK( "DISCONNECT", $ServerConnectionErrorMessage @ " (" @ %msg @ ")" );
}


//----------------------------------------------------------------------------
// Connection Failed Events
//----------------------------------------------------------------------------

function GameConnection::onConnectRequestRejected( %this, %msg )
{
   switch$(%msg)
   {
      case "CR_INVALID_PROTOCOL_VERSION":
         %error = "Incompatible protocol version: Your game version is not compatible with this server.";
      case "CR_INVALID_CONNECT_PACKET":
         %error = "Internal Error: badly formed network packet";
      case "CR_YOUAREBANNED":
         %error = "You are not allowed to play on this server.";
      case "CR_SERVERFULL":
         %error = "This server is full.";
      case "CHR_PASSWORD":
         // XXX Should put up a password-entry dialog.
         if ($Client::Password $= "") {
            passwordPromptDlg.connectAddress = %this.remoteAddress;
            canvas.pushDialog(passwordPromptDlg);
         }
         else {
            $Client::Password = "";
            MessageBoxOK( "REJECTED", "That password is incorrect.");
         }
         return;
      case "CHR_PROTOCOL":
         %error = "Incompatible protocol version: Your game version is not compatible with this server.";
      case "CHR_CLASSCRC":
         %error = "Incompatible game classes: Your game version is not compatible with this server.";
      case "CHR_INVALID_CHALLENGE_PACKET":
         %error = "Internal Error: Invalid server response packet";
      default:
         %error = "Connection error.  Please try another server.  Error code: (" @ %msg @ ")";
   }
   //disconnectedCleanup();
   MessageBoxOK( "REJECTED", %error);
}

function GameConnection::onConnectRequestTimedOut(%this)
{
   //disconnectedCleanup();
   MessageBoxOK( "TIMED OUT", "Your connection to the server timed out." );
}


//-----------------------------------------------------------------------------
// Disconnect
//-----------------------------------------------------------------------------

function disconnect()
{
   // Delete the connection if it's still there.
   if (isObject(ServerConnection))
      ServerConnection.delete();
   disconnectedCleanup();

   // Call destroyServer in case we're hosting
   destroyServer();
}

function disconnectedCleanup()
{
   // Clear misc script stuff
   HudMessageVector.clear();

   // Terminate all playing sounds
   alxStopAll();
   if (isObject(MusicPlayer))
      MusicPlayer.stop();

   //
   LagIcon.setVisible(false);
   PlayerListGui.clear();

   //badspot: clear admin player list
   AdminPlayerList.clear();
   
   // Clear all print messages
   clientCmdclearBottomPrint();
   clientCmdClearCenterPrint();

   // Back to the launch screen
   MainMenuLoadbar.setValue(0);
   Canvas.setContent(MainMenuGui);

   // Dump anything we're not using
   clearTextureHolds();
   purgeResources();

   $mvLeftAction = 0;
   $mvYawLeftSpeed = 0;
   //Add the others

   renderJobs.clientRenderJobs = renderJobs.serverRenderJobs = 0;
}

