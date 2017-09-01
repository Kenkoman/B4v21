//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Load up defaults console values.

// Defaults console values
exec("./client/defaults.cs");
exec("./server/defaults.cs");

// Preferences (overide defaults)
exec("./client/prefs.cs");
exec("./server/prefs.cs");

// Refresh and load mod list
exec("./mods/modsinit.cs");

//Launcher Dedicated connection script.  Doesn't do anything if not running a dedicated server.
exec("rtb/DedicatedConsole.cs");
startConsole();



//-----------------------------------------------------------------------------
// Package overrides to initialize the mod.
package RTB {

function displayHelp() {
   Parent::displayHelp();
   error(
      "RTB Mod options:\n"@
      "  -dedicated             Start as dedicated server\n"@
      "  -connect <address>     For non-dedicated: Connect to a game at <address>\n" @
      "  -mission <filename>    For dedicated: Load the mission\n"
   );
}

function parseArgs()
{
   // Call the parent
   Parent::parseArgs();

   // Arguments, which override everything else.
   for (%i = 1; %i < $Game::argc ; %i++)
   {
      %arg = $Game::argv[%i];
      %nextArg = $Game::argv[%i+1];
      %hasNextArg = $Game::argc - %i > 1;
   
      switch$ (%arg)
      {
         //--------------------
         case "-dedicated":
            $Server::Dedicated = true;
            enableWinConsole(true);
            $argUsed[%i]++;

         //--------------------
         case "-mission":
            $argUsed[%i]++;
            if (%hasNextArg) {
               $missionArg = %nextArg;
               $argUsed[%i+1]++;
               %i++;
            }
            else
               error("Error: Missing Command Line argument. Usage: -mission <filename>");

         //--------------------
         case "-connect":
            $argUsed[%i]++;
            if (%hasNextArg) {
               $JoinGameAddress = %nextArg;
               $argUsed[%i+1]++;
               %i++;
            }
            else
               error("Error: Missing Command Line argument. Usage: -connect <ip_address>");
      }
   }
}

function onStart()
{
   Parent::onStart();
   echo("\n--------- Initializing MOD: RTB ---------");

   // Load the scripts that start it all...
%PTTAstart = 0;
   exec("./client/PTTAinit.cs");
   exec("./server/PTTAinit.cs");
if(%PTTAstart $= 0)
{
   exec("./client/init.cs");
   exec("./server/init.cs");
}
   exec("./data/init.cs");
   exec("./rss/main.cs");
//   exec("./community/init.cs");

   //load ip banlist
   exec("./server/ipBanList.cs");

   // Server gets loaded for all sessions, since clients
   // can host in-game servers.
   initServer();

   // Start up in either client, or dedicated server mode
   if ($Server::Dedicated)
      initDedicated();
   else
      initClient();
}

function onExit()
{
   echo("Exporting client prefs");
   export("$pref::*", "./client/prefs.cs", False);

   echo("Exporting all clients connected to this session's cash ammount for storage.");
   export("$ClientCash*", "rtb/server/clientCash.cs", False);

   export("$UserInfo*", "rtb/server/clientRecords.cs", False);

   echo("Exporting server prefs");
   export("$Pref::Server::*", "./server/prefs.cs", False);
   BanList::Export("./server/banlist.cs");

   echo("Exporting IP banlist");
   export("$Ban::*", "./server/ipBanList.cs", False);

   Parent::onExit();
}

}; // Client package
  activatePackage(RTB);
  setmodpaths("fps;"@getmodpaths());
