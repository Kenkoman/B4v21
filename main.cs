//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

$defaultGame = "Add-Ons";
$displayHelp = false;

$Version = "8";

//-----------------------------------------------------------------------------
// Support functions used to manage the mod string

function pushFront(%list, %token, %delim)
{
   if (%list !$= "")
      return %token @ %delim @ %list;
   return %token;
}

function pushBack(%list, %token, %delim)
{
   if (%list !$= "")
      return %list @ %delim @ %token;
   return %token;
}

function popFront(%list, %delim)
{
   return nextToken(%list, unused, %delim);
}

//------------------------------------------------------------------------------
// Process command line arguments

$modcount = 0;
$userMods = $defaultGame;

//-----------------------------------------------------------------------------
// The displayHelp, onStart, onExit and parseArgs function are overriden
// by mod packages to get hooked into initialization and cleanup. 

function onStart()
{	
   echo("\n--------- Initializing FPS ---------");

   // Load the scripts that start it all...
   exec("base/client/init.cs");
   exec("base/server/init.cs");
   exec("base/data/init.cs");

   initCommon();
   
   // Server gets loaded for all sessions, since clients
   // can host in-game servers.
   initServer();

   serverPart2();
}


function onExit()
{
   echo("Exporting server prefs");
   export("$Pref::Server::*",               "base/config/server/prefs.cs", False);
   export("$Pref::Net::PacketRateToClient", "base/config/server/prefs.cs", True);   //true = append
   export("$Pref::Net::PacketRateToServer", "base/config/server/prefs.cs", True);
   export("$Pref::Net::PacketSize",         "base/config/server/prefs.cs", True);
   export("$Pref::Net::LagThreshold",       "base/config/server/prefs.cs", True);

   //remove server variables so they dont pollute the client prefs
	deleteVariables("$Pref::Server::*");

   echo("Exporting client prefs");
   export("$pref::*", "base/config/client/prefs.cs", False);

   echo("Exporting client config");
   if (isObject(moveMap))
      moveMap.save("base/config/client/config.cs", false);  
}

function parseArgs()
{  
   for ($i = 1; $i < $Game::argc ; $i++)
   {
      %allArgs = %allArgs SPC $Game::argv[$i];
   }
   echo("Parsing command line arguments arguments: " @ trim(%allArgs));

        for ($i = 1; $i < $Game::argc ; $i++)
        {
           $arg = $Game::argv[$i];
           $nextArg = $Game::argv[$i+1];
           $hasNextArg = $Game::argc - $i > 1;
           $logModeSpecified = false;
           
		   //echo("ARG = ", $arg);
		   //echo("-hasnextarg = ", %hasNextArg);

           switch$ ($arg)
           {
              //--------------------
             // TorqueDebugPatcher begin
             case "-dbgPort":
                // we must have a next arg
                $argUsed[$i]++;
                if ($hasNextArg)
                {
                       $GameDebugPort = $nextArg;
                   $argUsed[$i+1]++;
                   $i++;
                }
                 else
                    error("Error: Missing Command Line argument. Usage: -dbgPort <port>");
        
             //--------------------
             case "-dbgPassword":
                // we must have a next arg
                $argUsed[$i]++;
                if ($hasNextArg)
                {
                       $GameDebugPassword = $nextArg;
                   $argUsed[$i+1]++;
                   $i++;
                }
                 else
                    error("Error: Missing Command Line argument. Usage: -dbgPassword <password>");
        
             //--------------------
             case "-dbgEnable":
                    $GameDebugEnable = true;
                $argUsed[$i]++;
        
              //--------------------
              case "-connect":
                 $argUsed[$i]++;
                 if ($hasNextArg)
                 {
                    // mark which server we will automatically connect to
                    setAutoConnect($nextArg);
                    $argUsed[$i+1]++;
                    $i++;
                 }
                 else
                    error("Error: Missing Command Line argument. Usage: -connect <x.x.x.x:port>");
        
             // TorqueDebugPatcher end
        
             //--------------------
              case "-log":
                 $argUsed[$i]++;
                 if ($hasNextArg)
                 {
                    // Turn on console logging
                    if ($nextArg != 0)
                    {
                       // Dump existing console to logfile first.
                       $nextArg += 4;
                    }
                    setLogMode($nextArg);
                    $logModeSpecified = true;
                    $argUsed[$i+1]++;
                    $i++;
                 }
                 else
                    error("Error: Missing Command Line argument. Usage: -log <Mode: 0,1,2>");
        
              //--------------------
              case "-mod":
                 $argUsed[$i]++;
                 if ($hasNextArg)
                 {
                    // Append the mod to the end of the current list
                    $userMods = strreplace($userMods, $nextArg, "");
                    $userMods = pushFront($userMods, $nextArg, ";");
                    $argUsed[$i+1]++;
                    $i++;
                    $modcount++;
                 }
                 else
                    error("Error: Missing Command Line argument. Usage: -mod <mod_name>");
                    
              //--------------------
              case "-game":
                 $argUsed[$i]++;
                 if ($hasNextArg)
                 {
                    // Remove all mods, start over with game
                    $userMods = $nextArg;
                    $argUsed[$i+1]++;
                    $i++;
                    $modcount = 1;
                 }
                 else
                    error("Error: Missing Command Line argument. Usage: -game <game_name>");
                    
              //--------------------
              case "-show":
                 // A useful shortcut for -mod show
                 $userMods = strreplace($userMods, "show", "");
                 $userMods = pushFront($userMods, "show", ";");
                 $argUsed[$i]++;
                 $modcount++;
        
              //--------------------
              case "-console":
                 enableWinConsole(true);
                 $argUsed[$i]++;
              case "-noConsole":
                  $NoConsole = true;
                  $argUsed[$i]++;
              //--------------------
              case "-jSave":
                 $argUsed[$i]++;
                 if ($hasNextArg)
                 {
                    echo("Saving event log to journal: " @ $nextArg);
                    saveJournal($nextArg);
                    $argUsed[$i+1]++;
                    $i++;
                 }
                 else
                    error("Error: Missing Command Line argument. Usage: -jSave <journal_name>");
        
              //--------------------
              case "-jPlay":
                 $argUsed[$i]++;
                 if ($hasNextArg)
                 {
                    %playingJournal = true;
                    playJournal($nextArg,false);
                    $argUsed[$i+1]++;
                    $i++;
                 }
                 else
                    error("Error: Missing Command Line argument. Usage: -jPlay <journal_name>");
        
              //--------------------
              case "-jDebug":
                 $argUsed[$i]++;
                 if ($hasNextArg)
                 {
                    playJournal($nextArg,true);
                    $argUsed[$i+1]++;
                    $i++;
                 }
                 else
                    error("Error: Missing Command Line argument. Usage: -jDebug <journal_name>");
        
              //-------------------
              case "-help":
                 $displayHelp = true;
                 $argUsed[$i]++;
        
                 //--------------------
                 case "-dedicated":
                    $Server::Dedicated = true;
                    enableWinConsole(true);
                    $argUsed[$i]++;
				
				 //--------------------
				 case "-dedicatedLAN":
					$Server::LAN = true;
					$Server::Dedicated = true;
                    enableWinConsole(true);
                    $argUsed[$i]++;

				 case "-port":
					$argUsed[$i]++;
                    if ($hasNextArg) {
                       $portArg = $nextArg;
                       $argUsed[$i+1]++;
                       $i++;
                    }
                    else
                       error("Error: Missing Command Line argument. Usage: -port <number>");

                 //--------------------
                 case "-mission":
                    $argUsed[$i]++;
                    if ($hasNextArg) {
                       $missionArg = $nextArg;
                       $argUsed[$i+1]++;
                       $i++;
                    }
                    else
                       error("Error: Missing Command Line argument. Usage: -mission <filename>");
        
                 //--------------------
                 case "-connect":
                    $argUsed[$i]++;
                    if ($hasNextArg) {
                       $JoinGameAddress = $nextArg;
                       $argUsed[$i+1]++;
                       $i++;
                    }
                    else
                       error("Error: Missing Command Line argument. Usage: -connect <ip_address>");

              case "-serverName":
					$argUsed[$i]++;
                    if ($hasNextArg) {
                       $serverNameArg = $nextArg;
                       $serverNameArg = strReplace($serverNameArg, "_", " ");
                       $argUsed[$i+1]++;
                       $i++;
                    }
                    else
                       error("Error: Missing Command Line argument. Usage: -serverName <name>");
              case "-trace":
                 $argUsed[$i]++; 
                 trace(1);
              //-------------------
              default:
                 $argUsed[$i]++;
                 if($userMods $= "")
                    $userMods = $arg;
           }
        }
        
        if($modcount == 0 && ($defaultGame !$= "")) {
              $userMods = $defaultGame;
              $modcount = 1;
        }
      
         if(!$NoConsole)
            EnableWinConsole(true);
            

         //automatically record journal for debug
         if(!%playingJournal)
            if(getBuildString() $= "Debug")
              saveJournal("auto.jrn");
}   

// Parse the command line arguments
echo("--------- Parsing Arguments ---------");
parseArgs();

package Help {
   function onExit() {
      // Override onExit when displaying help
   }
};

function displayHelp() {
   activatePackage(Help);

      // Notes on logmode: console logging is written to console.log.
      // -log 0 disables console logging.
      // -log 1 appends to existing logfile; it also closes the file
      // (flushing the write buffer) after every write.
      // -log 2 overwrites any existing logfile; it also only closes
      // the logfile when the application shuts down.  (default)

   error(
      "Torque Demo command line options:\n"@
      "  -log <logmode>         Logging behavior; see main.cs comments for details\n"
@
      "  -game <game_name>      Reset list of mods to only contain <game_name>\n"@
      "  <game_name>            Works like the -game argument\n"@
      "  -mod <mod_name>        Add <mod_name> to list of mods\n"@
      "  -console               Open a separate console\n"@
      "  -show <shape>          Launch the TS show tool\n"@
      "  -jSave  <file_name>    Record a journal\n"@
      "  -jPlay  <file_name>    Play back a journal\n"@
      "  -jDebug <file_name>    Play back a journal and issue an int3 at the end\n"@
    // TorqueDebugPatcher begin
     "  -dbgPort <port>        Set debug port (default = 28040)\n"@
     "  -dbgPassword <pass>    Set debug password (default = password)\n"@
     "  -dbgEnable             Start game in debug mode\n"@
    // TorqueDebugPatcher end
      "  -dedicated             Start as dedicated server\n"@
      "  -connect <address>     For non-dedicated: Connect to a game at <address>\n" 
@
      "  -mission <filename>    For dedicated: Load the mission\n" @
      "  -help                  Display this help message\n"
   );
}


//--------------------------------------------------------------------------

// Default to a new logfile each session.
if (!$logModeSpecified) {
   setLogMode(6);
}

// Set the mod path which dictates which directories will be visible
// to the scripts and the resource engine.
setModPaths($userMods);

// Get the first mod on the list, which will be the last to be applied... this
// does not modify the list.
nextToken($userMods, currentMod, ";");

// Execute startup scripts for each mod, starting at base and working up
function loadDir(%dir)
{
   setModPaths(pushback($userMods, %dir, ";"));
   exec(%dir @ "/main.cs");
}

echo("--------- Loading Common ---------");
//Added to create a single base
// Load up common script base
loadDir("base");
//setModPaths("common");

//-----------------------------------------------------------------------------
// Load up defaults console values.

// Defaults console values
exec("base/client/defaults.cs");
exec("base/server/defaults.cs");

// Preferences (overide defaults)
exec("base/config/client/prefs.cs");
exec("base/config/server/prefs.cs");

//overide servername with command line argument
if($serverNameArg !$= "")
   $Pref::Server::Name = $serverNameArg;

echo("--------- Loading MODS ---------");
function loadMods(%modPath)
{
   %modPath = nextToken(%modPath, token, ";");
   if (%modPath !$= "")
      loadMods(%modPath);
      
   //echo(%token @ "/main.cs");
   if(isFile(%token @ "/main.cs")) //badspot: we specify some "mod paths" just to have access to the directory
   {
      if(exec(%token @ "/main.cs") != true){
         error("Error: Unable to find specified mod: " @ %token );
         $modcount--;
      }
   }
   else
   {
      echo("Skipping mod: ", %token);
      $modcount--;
   }
}

if($modcount != 0) {
   loadMods($userMods);
   echo("");
}

// Either display the help message or startup the app.
if ($displayHelp) {
   enableWinConsole(true);
   displayHelp();
   quit();
}
else {
   onStart();
   echo("Engine initialized...");
}

// Display an error message for unused arguments
for ($i = 1; $i < $Game::argc; $i++)  {
   if (!$argUsed[$i])
      error("Error: Unknown command line argument: " @ $Game::argv[$i]);
}