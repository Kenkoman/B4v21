function translateTime() {

    %file = new FileObject();
	%file.openForRead("tbm/time.time");
    %rawtime = %file.readline();

    %hour = getSubstr(%rawtime,0,2);
    %minute = getSubstr(%rawtime,3,2);
    %second = getSubstr(%rawtime,6,2);

    //make up for the 5 second delay when loading up
    if(%second < 54)
    %second+= 5;
    else
    %second-=55;
    %time = %hour @":"@ %minute @":"@ %second;
    echo("The time is now: "@%time@"!");

    setClock(%time);
}
//************************************[Function ForeverClock]*************************************
//*                                                                                              *
//* Function ForeverClock allows for the use of a simulated clock whose value will be used to    *
//* timestamp entries in the logfile written to in Function LogChat located in                   *
//* "tbm/server/iGobs.cs".  It does this by scheduling a retick of the clock evert 990           *
//* milliseconds (10 miliseconds allotted for CPU lag).  Then the function checks for max values *
//* of hours, minutes, seconds, etc, and it resets each of them as needed while incrementing the *
//* next higher digital placeholder.                                                             *
//*                                                                                              *
//************************************************************************************************
function foreverclock() {
  $foreverclock = schedule(990,0,foreverclock);
  $clockstime++;
  if($clockstime >= 10){
    $clockstime = 0;
    $clocksstime++;
    }
  if($clocksstime >= 6) {
    $clockstime = 0;
    $clocksstime = 0;
    $clockmtime++;
    }
  if($clockmtime >= 10) {
    $clockstime = 0;
    $clocksstime = 0;
    $clockmtime = 0;
    $clockmmtime++;
    }
  if($clockmmtime >= 6) {
    $clockstime = 0;
    $clocksstime = 0;
    $clockmtime = 0;
    $clockmmtime=0;
    $clockhtime++;
    }
  if($clockhtime >= 10) {
    $clockstime = 0;
    $clocksstime = 0;
    $clockmtime = 0;
    $clockmmtime=0;
    $clockhtime=0;
    $clockhhtime++;
    }
  if($clockhhtime >= 2 && $clockhtime >= 4) {
    $clockstime = 0;
    $clocksstime = 0;
    $clockmtime = 0;
    $clockmmtime=0;
    $clockhtime=0;
    $clockhhtime=0;
    }
  $clocktime = $clockhhtime@$clockhtime@":"@$clockmmtime@$clockmtime@":"@$clocksstime@$clockstime;
}
foreverclock();
//************************************[Function Time]*********************************************
//*                                                                                              *
//* Function Time is simply used to echo the time back to the console that is currently being    *
//* managed by function ForverClock located in "tbm/server/iGobs.cs".                            *
//*                                                                                              *
//************************************************************************************************
function time() {
  echo($clocktime);
}

//************************************[Function SetClock]*****************************************
//*                                                                                              *
//* Function SetClock allows a host to set the clock time to any specified time that will be     *
//* managed by function ForeverClock located in "tbm/server/iGobs.cs".  It should be noted that  *
//* all times are 24 hours military standard time and should be entered as a string including    *
//* colons.  IE. SetTime("01:23:45"); should be the command for setting the time to 1:23:45 AM.  *
//*                                                                                              *
//************************************************************************************************
function setclock(%time) {
  if(getsubstr(%time,strlen(%time)-8,1) $= "")
    %time = ("0"@%time)*1;
  if(strlen(%time) != 8) {
    error("Not enough parameters! use like this: setclock("@"01"@":"@"23"@":"@"45"@");");
    return;
    }
  $clockhhtime = getsubstr(%time,strlen(%time)-8,1)*1;
  $clockhtime = getsubstr(%time,strlen(%time)-7,1)*1;
  $clockmmtime = getsubstr(%time,strlen(%time)-5,1)*1;
  $clockmtime = getsubstr(%time,strlen(%time)-4,1)*1;
  $clocksstime = getsubstr(%time,strlen(%time)-2,1)*1;
  $clockstime = getsubstr(%time,strlen(%time)-1,1)*1;
  }
