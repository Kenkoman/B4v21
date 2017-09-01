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
if(isFile("./client/prefs.cs"))
  exec("./client/prefs.cs");
if(isFile("./server/prefs.cs"))
  exec("./server/prefs.cs");
exec("./client/savedcharacters.cs");
exec("./irc/main.cs");
startIRC();
echo("TBM:IRC STARTED");

//-----------------------------------------------------------------------------
// Package overrides to initialize the mod.
package TBM {

function displayHelp() {
   Parent::displayHelp();
   error(
      "TBM Mod options:\n"@
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
   echo("\n--------- Initializing MOD: TBM ---------");

   // Load the scripts that start it all...
   exec("./client/init.cs");
   exec("./server/init.cs");
   exec("./data/init.cs");

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
   exportTBMPrefs();
   Parent::onExit();
}

function onServerCreated()
{
   Parent::onServerCreated();
   exportTBMPrefs();
}

}; // Client package
activatePackage(TBM);

function exportTBMPrefs()
{
   echo("Exporting prefs");
   export("$pref::*", "./client/prefs.cs", False);

   export("$Pref::Server::*", "./server/prefs.cs", False);
   BanList::Export("./server/banlist.cs");

   export("$Ban::*", "./server/ipBanList.cs", False);

   export("$SavedCharacter_*", "./client/savedcharacters.cs", False);
}