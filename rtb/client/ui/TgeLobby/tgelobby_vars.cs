// =========================================================================
// TgeLobby for the Torque Game Engine
// Programmer:  Sean Pollock aka DarkRaven
// send email to sean@darkravenstudios.com
// This file is called to initialize certain variables and is a required file.
// Most everything can be configured here.
// =========================================================================

// Host and Port stuff
$TgeLobby::Host = "irc.centralchat.net"; // Host Address
$TgeLobby::Port = "6667";      // Port - Do not change

// Connection Messages goes here
$TgeLobby::ConnectMsg1 = "Connected!";
$TgeLobby::ConnectMsg2 = "Host Found...Connecting!";
$TgeLobby::ConnectMsg3 = "Connection Failed!";
$TgeLobby::ConnectMsg4 = "Could not find Server!";

// Special Flags
$TgeLobby::PERSON_IGNORE   = 4;
$TgeLobby::NOT_USED5       = 5;
$TgeLobby::NOT_USED6       = 6;
$TgeLobby::NOT_USED7       = 7;
$TgeLobby::NOT_USED8       = 8;

// Directories
$TgeLobby::Directory = "rtb/client/ui/TgeLobby"; // Change to what ever directory you want.
//$TgeLobby::ButtonsDir = "/buttons/";
$TgeLobby::ListsDir = "/lists/";


// Misc. stuff
$TgeLobby::Retry = 5; // Max # of connection retries
$TgeLobby::RetryCount = 1; // Leave this set to 1
$TgeLobby::PrvMsg = false; // Leave false
$TgeLobby::Channel = "#rtb"; // Default Channel
$TgeLobby::Version = "RTB IRC Chat";  // Version info
$TgeLobby::Extension = "_rtb"; // This is appended to the end of a nickname when your are in game.
$TgeLobby::RandomSeed = 10000; // Used when figuring Guest_random#

