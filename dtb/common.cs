//BEGIN
//Wiggy's very small library of functions
//This contains mostly string and file operations that were in both the client and server parts, and really ought to be in one place

$WiggyPatch::CommonLib = 1.4;
//Private use; updated during 2.3 development
//7/19/2013


//Constants

$pi = 3.141592653589;

//Functions

function activateChatColors(%text) {
%text = strReplace(%text,"/c0","\c0");
%text = strReplace(%text,"/c1","\c1");
%text = strReplace(%text,"/c2","\c2");
%text = strReplace(%text,"/c3","\c3");
%text = strReplace(%text,"/c4","\c4");
%text = strReplace(%text,"/c5","\c5");
%text = strReplace(%text,"/c6","\c6");
%text = strReplace(%text,"/c7","\c7");
%text = strReplace(%text,"/c8","\c8");
return strReplace(%text,"/c9","\c9");
}

function capitalizeFirstLetter(%t) {
return strUpr(getSubStr(%t,0,1)) @ getSubStr(%t,1,strlen(%t)-1);
}

function decimalToHex(%dec, %fix) {
for(%i = mFloor(mLog(%dec)/mLog(16)); %i > 0; %i--) {
  %hex = %hex @ getSubStr("0123456789abcdef", mFloor(%dec/mPow(16, %i)), 1);
  %dec -= mPow(16, %i) * mFloor(%dec/mPow(16, %i));
}
%hex = %hex @ getSubStr("0123456789abcdef", %dec, 1);
while(strLen(%hex) < %fix)
  %hex = "0" @ %hex;
return %hex;
}

function detagColors(%text) { //Not mine, but it was server-side before
%text = strReplace(%text,"\c0", "");
%text = strReplace(%text,"\c1", "");
%text = strReplace(%text,"\c2", "");
%text = strReplace(%text,"\c3", "");
%text = strReplace(%text,"\c4", "");
%text = strReplace(%text,"\c5", "");
%text = strReplace(%text,"\c6", "");
%text = strReplace(%text,"\c7", "");
%text = strReplace(%text,"\c8", "");
%text = strReplace(%text,"\c9", "");
return %text;
}

function flipText(%text) {
%len = strlen(%text);
%return = "";
for(%i = 1; %i < %len + 1; %i++) {
  %letter = getSubStr(%text,%len-%i,1);
  %return = %return @ %letter;
}
}

function getlinecount(%filename) { //Copied from tbm/client/ui/iGobGUI.gui.  I didn't write this.
	%file = new FileObject();
	%file.openForRead(%filename);
	while(!%file.isEOF())
	{
	%file.readLine();
    %linenum++;
    }
    %file.close();
    %file.delete();
    return %linenum;  //This goes AFTER .close and .delete, not before
}

function hexToDecimal(%hex) {
%hex = strLwr(%hex);
for(%i = 0; %i < strLen(%hex); %i++)
  %dec += (strStr("0123456789abcdef", getSubStr(%hex, %i, 1)) * mPow(16, strLen(%hex) - %i - 1));
return %dec;
}

function isLowercase(%a) {
return %a !$= strReplace(%a,strLwr(%a),"");
}

function isUppercase(%a) {
return %a !$= strReplace(%a,strUpr(%a),"");
}

function max(%a, %b) {
return %a > %b ? %a : %b;
}

function millisecondsToTime(%t, %words) { //Basically the same as secondsToTime but intended for smaller values.
  if(!%t)                                 //Primarily for use by the switch action list, but general enough for other things to be able to use it.
    return "0 s";                         //Short by default out of necessity.
  %a = %t - (1000 * mFloor(%t / 1000));
  %t = (%t - %a) / 1000;
  %b = %t - (60 * mFloor(%t / 60));
  %t = (%t - %b) / 60;
  %c = %t - (60 * mFloor(%t / 60));
  %t = (%t - %c) / 60;
  %z = !!%a + !!%b + !!%c; //total number of thingies
  if(%z > %words) {
    for(%i = 0; %i < %z - %words; %i++) {
      if(%a && (%b || mFloor(%a / 100) == mCeil(%a / 100))) {
        %b += 0.1 * mRound(%a / 100);
        %a = "";
      }
      else if(%b > 0 && %c > 0) {
        %c += 0.1 * mRound(%b / 6);
        %b = "";
      }
    }
  }
  if(%a)
    %a = %a SPC "ms";
  else
    %a = "";
  if(%b)
    %b = %b SPC "s";
  else
    %b = "";
  if(%c)
    %c = %c SPC "m";
  else
    %c = "";
  return %c @ (%c $= "" ? "" : " ") @ %b @ (%b $= "" ? "" : " ") @ %a;
}

function min(%a, %b) {
return %a > %b ? %b : %a;
}

function mround (%mod) {     //Yanked from SpecialOp.cs and shortened
  return mFloor(%mod + 0.5); //Whoever wrote this the first time doesn't know math
}

function numbertobase2(%number,%fix) { //Wonderful for iterated operations that require Boolean input.  The curse filter is a prime example - it uses this to create an array of numbers from 0 to (2^lettercount), and uses that to iterate through possible variation of a word that can be made by capitalizing one or more letters.
for(%i = mfloor(mlog(%number)/mlog(2)); %i >= 0; %i--) { //Geez, that was long.
%a = mfloor(%number/mpow(2,%i));
%number = %number - %a*mpow(2,%i);
%a = getSubStr("01",%a,1);
%final = %final @ %a;
}
while(strlen(%final) < %fix) %final = "0" @ %final;
return %final;
}

function numbertobase3(%number,%fix) {
for(%i = mfloor(mlog(%number)/mlog(3)); %i >= 0; %i--) {
%a = mfloor(%number/mpow(3,%i));
%number = %number - %a*mpow(3,%i);
%a = getSubStr("012",%a,1);
%final = %final @ %a;
}
while(strlen(%final) < %fix) %final = "0" @ %final;
return %final;
}

function readLine(%file, %line) {
%f = new FileObject();
%f.openForRead(%file);
for(%i = 1; %i < %line; %i++)
  %f.readLine();
%line = %f.readLine();
%f.close();
%f.delete();
return %line;
}

function secondsToTime(%t, %short, %words) {    //Converts some number of seconds to a more easily readable indicator of this length of time (ex. "1 day, 4 hours, 42 minutes and 1 second")
  if(!%t)                                       //It goes up to number of days and doesn't start converting days into weeks once the seventh day is reached.
    return "0" SPC (%short ? "s" : "seconds");  //Poorly optimized and doesn'te even use arrays because I was tired
  %a = %t - (60 * mFloor(%t / 60));          
  %t = (%t - %a) / 60;                       
  %b = %t - (60 * mFloor(%t / 60));
  %t = (%t - %b) / 60;
  %c = %t - (24 * mFloor(%t / 24));
  %t = (%t - %c) / 24;
  %d = %t - (7 * mFloor(%t / 7));
  %t = (%t - %d) / 7;
  %z = !!%a + !!%b + !!%c + !!%d; //total number of thingies
  if(%a)
    %a = %a SPC (%short ? "s" : (%a == 1 ? "second " : "seconds "));
  else
    %a = "";
  if(%b)
    %b = %b SPC (%short ? "m" : (%b == 1 ? "minute " : "minutes "));
  else
    %b = "";
  if(%c)
    %c = %c SPC (%short ? "h" : (%c == 1 ? "hour " : "hours "));
  else
    %c = "";
  if(%d)
    %d = %d SPC (%short ? "d" : (%d == 1 ? "day " : "days "));
  else
    %d = "";
  %x = %d @ %c @ %b @ %a;
  if(%words !$= "") {
    %x = getWords(%x, 0, %words - 1);
    %z = %words;
  }
  if(%z >= 2)
    %x = getWords(%x, 0, getWordCount(%x) - 3) @ (%short ? "," : " and") SPC getWords(%x, getWordCount(%x) - 2, getWordCount(%x));
  if(%z >= 3)
    for(%i = 0; %i <= %z - 3; %i++)
      %x = getWords(%x, 0, 2 * %i + 1) @ "," SPC getWords(%x, 2 * %i + 2, getWordCount(%x));
  return %x;
}



function teamtocolor(%team) {
%team = strLwr(%team);
if(%team $= "red") return "\c9";
if(%team $= "blue") return "\c4";
if(%team $= "green") return "\c5";
if(%team $= "yellow") return "\c2";
return "";
}

//Builds list of flower colors
$flowerColors = -1;
%loc = "tbm/data/shapes/flower/*.flower.png";
for (%filename = findFirstFile(%loc); %filename !$= ""; %filename = findNextFile(%loc)) {
  $flowerColor[$flowerColors++] = strReplace(strReplace(%fileName, "tbm/data/shapes/flower/", ""), ".flower.png", "");
}
