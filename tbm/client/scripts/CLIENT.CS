//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Server Admin Commands
//-----------------------------------------------------------------------------
function colors(%color) 
{ 
  switch$ (%color) {
     case "red":
       echo("Your color is red."); 
     case "orange": 
       echo("Your color is orange."); 
     case "yellow":
       echo("Your color is yellow.");      
     case "green":
       echo("Your color is green."); 
     case "blue":
       echo("Your color is blue."); 
     case "purple":
        echo("Your color is purple.");
     case "help":
        echo("Valid colors are red, orange, yellow,  green, blue, and purple."); 
     default:
        echo("Your color is invalid. Valid colors are red, orange, yellow, green, blue, and purple."); 
     } 
} 

function SAD(%password)
{
   if (%password !$= "")
      commandToServer('SAD', %password);
}

function SADSetPassword(%password)
{
   commandToServer('SADSetPassword', %password);
}
function cc(%color) {
  commandtoserver('updatePrefs',$pref::Player::Name,%color, 
                 $pref::Accessory::headCode, 
                 $pref::Accessory::visorCode, 
                 $pref::Accessory::backCode, 
                 $pref::Accessory::leftHandCode,%color,%color,%color,%color, 
                 $pref::Decal::chest, 
                 $pref::Decal::face);
}
function rset(%delay) {
 %lopt=getRandom(-1,6);
 if (%lopt==0)
   %lcolor=getRandom(-4,-1);
 else
   %lcolor=getRandom(0,$ColorTotal);
 commandtoserver('updatePrefs',$pref::Player::Name,
                               getRandom(0,$ColorTotal),
                               getRandom(-1,$hatsTot),
                               getRandom(-1,5),
                               getRandom(-1,$backTot),
                               %lopt,
                               getRandom(0,$ColorTotal),
                               getRandom(0,$ColorTotal),
                               getRandom(0,$ColorTotal),
                               %lcolor,
                               getRandom(0,$decalsTot),
                               getRandom(0,$facesTot)); 
 $schedule=schedule(%delay,0,rset,%delay);
}
function MCP() {commandtoserver('updatePrefs',"MCP",0,-1,-1,6,3,0,0,0,0,28,4);}
function mytourdeforce() {commandtoserver('updatePrefs',"mytourdeforce",7,22,-1,9,-1,7,0,7,0,27,27);}
function magicpants() {commandtoserver('updatePrefs',"MagicPants",7,26,-1,-1,-1,7,0,0,0,28,12);}
function weirdo (%delay,%times) {
%a=0;
for (%i=0; %i<%times; %i++) {
  schedule(%delay*%a++,0,MCP);
  schedule(%delay*%a++,0,mytourdeforce);
  schedule(%delay*%a++,0,magicpants);
  }
}  
function fendersblink(%delay) {
for(%i = 0; %i < 36; %i++) {
    schedule(%delay*%i,0,cc,%i);
  }
}

function MSG::onLine(%this, %line) {
echo(%line);
echo(%this);
}

function MSG::onConnectRequest(%this, %address, %id)
{
echo("someone is connecting");
new TCPObject(MSG, %id);
}
function MSG::processLine(%this, %line, %id)
{
echo(%line);
}
function tower(%str) {
for(%i = getwordcount(%str); %i >= 0; %i--) {
  commandtoserver('setsign', %str);
  commandToServer('plantBrick');
  commandToServer('shiftBrick', 0, 0, 3);
}
}

function buildwall()
{
	for(%i = 0; %i < 10; %i++)
	{
		commandToServer('plantBrick');
		commandToServer('shiftBrick', 0, 0, 3);
		commandToServer('plantBrick');
		commandToServer('shiftBrick', 0, 0, 3);
		commandToServer('plantBrick');
		commandToServer('shiftBrick', 0, 0, 3);
		commandToServer('plantBrick');
		commandToServer('shiftBrick', 0, 0, 3);
		commandToServer('plantBrick');
		commandToServer('shiftBrick', 0, 0, 3);
		commandToServer('plantBrick');
		commandToServer('shiftBrick', 0, 0, 3);
		commandToServer('plantBrick');
		commandToServer('shiftBrick', 0, 0, 3);
		commandToServer('plantBrick');
		commandToServer('shiftBrick', 0, 0, 3);
		commandToServer('plantBrick');
		commandToServer('shiftBrick', 0, 0, 3);
		commandToServer('plantBrick');
		commandToServer('shiftBrick', 0, 0, 3);
		commandToServer('shiftBrick', 2, 0, -30);
	}
}
function buildflat(%SFB, %SLR, %SUD, %LL, %LH, %cc, %color)
{
        for(%x = 0; %x < %LL; %x++)
        {
                for(%i = 0; %i < %LH; %i++)
                {
                if (%cc==1) {
                  if (%color++>$LCTotal)
                    %color=0;
                  commandToServer('ColorPicker', %color);
                  echo($LegoColor[%color]);
                  }

                        commandToServer('plantBrick');
                        commandToServer('shiftBrick', 0, 0, %SUD);
                }
        %C = %SUD * %LH * -1;
        commandToServer('shiftBrick', %SFB, %SLR, %C);        
        }
    
}

function buildstag(%SFB, %SLR, %SUD, %LH)
{
        for(%i = 0; %i < %LH; %i++)
                {
                        commandToServer('plantBrick');
                        commandToServer('shiftBrick', %SFB, %SLR, %SUD);
                }
        commandToServer('shiftBrick', %SFB * -1, %SLR * -1, %SUD * -1);
        commandToServer('shiftBrick', %SFB * -1, %SLR * -1, %SUD * -1);
    
}

function letter(%var) {
  commandtoserver('shiftbrick',0,0,12);
  for (%a=0; %a<5; %a++) {
    for (%b=0; %b<5; %b++) {
      if (getsubstr(%var,%c,1))
        commandtoserver('plantbrick');
      commandtoserver('shiftbrick',0,-1,0);
      %c++;
      }
    commandtoserver('shiftbrick',0,5,-3);
    }
  commandtoserver('shiftbrick',0,-6,3);
}

function wp(%var) {
for (%i=0; %i<strlen(%var); %i++)
  switch$ (getsubstr(%var,%i,1)) {
        case " " :
         letter("00000"@
                "00000"@
                "00000"@
                "00000"@
                "00000");
        case "a" :
         letter("01110"@
                "10001"@
                "11111"@
                "10001"@
                "10001");
        case "b" :
     letter("11110"@
            "10001"@
            "11110"@
            "10001"@
            "11110");
        case "c" :
        letter("11110"@
        "10000"@
        "10000"@
        "10000"@
        "11110");
        case "d" :
        letter("11110"@
        "10001"@
        "10001"@
        "10001"@
        "11110");
        case "e" :
        letter("11111"@
        "10000"@
        "11110"@
        "10000"@
        "11111");
        case "f" :
        letter("11111"@
        "10000"@
        "11110"@
        "10000"@
        "10000");
        case "g" :
        letter("11111"@
        "10000"@
        "10011"@
        "10001"@
        "11111");
        case "h" :
        letter("10001"@
        "10001"@
        "11111"@
        "10001"@
        "10001");
        case "i" :
        letter("11111"@
        "00100"@
        "00100"@
        "00100"@
        "11111");
        case "j" :
        letter("11111"@
        "00100"@
        "00100"@
        "10100"@
        "11100");
        case "k" :
        letter("10001"@
        "10010"@
        "11100"@
        "10010"@
        "10001");
        case "l" :
        letter("10000"@
        "10000"@
        "10000"@
        "10000"@
        "11111");
        case "m" :
        letter("11111"@
        "10101"@
        "10101"@
        "10101"@
        "10101");
        case "n" :
        letter("10001"@
        "11001"@
        "10101"@
        "10011"@
        "10001");
        case "o" :
        letter("11111"@
        "10001"@
        "10001"@
        "10001"@
        "11111");
        case "p" :
        letter("11111"@
        "10001"@
        "11111"@
        "10000"@
        "10000");
        case "q" :
        letter("11111"@
        "10001"@
        "10001"@
        "11111"@
        "00100");
        case "r" :
        letter("11111"@
        "10001"@
        "11110"@
        "10100"@
        "10010");
        case "s" :
        letter("11111"@
        "10000"@
        "11111"@
        "00001"@
        "11111");
        case "t" :
        letter("11111"@
        "00100"@
        "00100"@
        "00100"@
        "00100");
        case "u" :
        letter("10001"@
        "10001"@
        "10001"@
        "10001"@
        "11111");
        case "v" :
        letter("10001"@
        "01010"@
        "01010"@
        "00100"@
        "00100");
        case "w" :
        letter("10101"@
        "10101"@
        "10101"@
        "10101"@
        "11111");
        case "x" :
        letter("10001"@
        "01010"@
        "00100"@
        "01010"@
        "10001");
        case "y" :
        letter("10001"@
        "10001"@
        "11111"@
        "00100"@
        "00100");
        case "z" :
        letter("11111"@
        "00010"@
        "00100"@
        "01000"@
        "11111");
        case "z" :
        letter("11111"@
        "00010"@
        "00100"@
        "01000"@
        "11111");
        case "0" :
         letter("11111"@
                "10001"@
                "10001"@
                "10001"@
                "11111");
        case "1" :
         letter("01100"@
                "00100"@
                "00100"@
                "00100"@
                "11111");
        case "2" :
         letter("11111"@
                "00001"@
                "11111"@
                "10000"@
                "11111");
		case "3" :
         letter("11111"@
                "00001"@
                "11111"@
                "00001"@
                "11111");

                case "4" :
         letter("10010"@
                "10010"@
                "11111"@
                "00010"@
                "00010");

		case "5" :
         letter("11111"@
                "10000"@
                "11111"@
                "00001"@
                "11111");

		case "6" :
         letter("11111"@
                "10000"@
                "11111"@
                "10001"@
                "11111");

		case "7" :
         letter("11111"@
                "00001"@
                "00010"@
                "00100"@
                "01000");

        case "8" :
         letter("11111"@
                "10001"@
                "11111"@
                "10001"@
                "11111");

		case "9" :
         letter("11111"@
                "10001"@
                "11111"@
                "00001"@
                "11111");

        case "." :
         letter("00000"@
                "00000"@
                "00000"@
                "00000"@
                "00001");

        case "," :
        letter("00000"@
                "00000"@
                "00000"@
                "00001"@
                "00010");

		case "!" :
         letter("00100"@
                "00100"@
                "00100"@
                "00000"@
                "00100");

		case "?" :
         letter("01100"@
                "10010"@
                "00100"@
                "00000"@
                "00100");

        case "%" :
         letter("11001"@
                "11010"@
                "00100"@
                "01011"@
                "10011");

        case "$" :
         letter("11111"@
                "10100"@
                "11111"@
                "00101"@
                "11111");

        case "#" :
         letter("01010"@
                "11111"@
                "01010"@
                "11111"@
                "01010");

        case ")" :
         letter("00100"@
                "00010"@
                "00001"@
                "00010"@
                "00100");

		case "(" :
         letter("00100"@
                "01000"@
                "10000"@
                "01000"@
                "00100");
                
        case "}" :
         letter("00110"@
                "00010"@
                "00011"@
                "00010"@
                "00110");

		case "{" :
         letter("01100"@
                "01000"@
                "11000"@
                "01000"@
                "01100");

        case "|" :
         letter("00100"@
                "00100"@
                "00100"@
                "00100"@
                "00100");

        case "-" :
         letter("00000"@
                "00000"@
                "01110"@
                "00000"@
                "00000");

		case "=" :
         letter("00000"@
                "01110"@
                "00000"@
                "01110"@
                "00000");

         case "+" :
         letter("00000"@
                "00100"@
                "01110"@
                "00100"@
                "00000");

        case "//" :
         letter("00001"@
                "00010"@
                "00100"@
                "01000"@
                "10000");

		case "\\" :
         letter("10000"@
                "01000"@
                "00100"@
                "00010"@
                "00001");

		case "]" :
         letter("01111"@
                "00001"@
                "00001"@
                "00001"@
                "01111");

		case "[" :
         letter("11110"@
                "10000"@
                "10000"@
                "10000"@
                "11110");


		case ":" :
         letter("00000"@
                "00100"@
                "00000"@
                "00100"@
                "00000");

        case ";" :
         letter("00000"@
                "00100"@
                "00000"@
                "00100"@
               "00100");

		case "'" :
         letter("00100"@
                "00100"@
                "00000"@
                "00000"@
                "00000");
         case "~" :
         letter("10111"@
                "10100"@
                "11111"@
                "00101"@
                "11101");
  }
}
function clearout() {     
%a=-1;
for (%clientIndex = 0; %clientIndex < ClientGroup.getCount(); %clientIndex++) {     
       %victim = ClientGroup.getObject( %clientIndex );    
       if (!%victim.issuperadmin && !%victim.isadmin)  {
          %set[%a++]=%victim;   
          echo(%victim.namebase@" is on the list");
         }
       }  
   for (%Index = 0; %Index <= %a; %Index++) {     
       %victim = %set[%Index];    
       echo(%victim.namebase@" is being kicked");
       %victim.delete("Rejoin or eat my poop");   
   } 
}
//----------------------------------------------------------------------------
// Misc server commands
//----------------------------------------------------------------------------

function clientCmdSyncClock(%time)
{
   // Store the base time in the hud control it will automatically increment.
   //HudClock.setTime(%time);
}

function GameConnection::prepDemoRecord(%this)
{
   %this.demoChatLines = HudMessageVector.getNumLines();
   for(%i = 0; %i < %this.demoChatLines; %i++)
   {
      %this.demoChatText[%i] = HudMessageVector.getLineText(%i);
      %this.demoChatTag[%i] = HudMessageVector.getLineTag(%i);
      echo("Chat line " @ %i @ ": " @ %this.demoChatText[%i]);
   }
}

function GameConnection::prepDemoPlayback(%this)
{
   for(%i = 0; %i < %this.demoChatLines; %i++)
      HudMessageVector.pushBackLine(%this.demoChatText[%i], %this.demoChatTag[%i]);
   Canvas.setContent(PlayGui);
}

function type (%color,%word) {
  %len=strlen(%word);
  for (%i=0; %i <= %len; %i++) {
    %letter=getsubstr(%word,%i,1);
    if (%letter $= " ")
      continue;
    if (strstr("abcdefghijklmnopqrstuvwxyz",%letter)>-1)
      %decal = "_l_letter_"@%letter;
    else if (strstr("ABCDEFGHIJKLMNOPQRSTUVWXYZ",%letter)>-1)
      %decal = "_u_letter_"@%letter;
    else if (strstr("1234567890",%letter)>-1)
      %decal = "_number_"@%letter;
    else
    switch$ (%letter) {
      case "<":
   %decal = "_lessthan";
      case ">":
   %decal = "_greaterthan";
      case "?":
   %decal = "_questionmark";
      case "*":
   %decal = "_asterisk";
      case "|":
   %decal = "_bar";
      case ":":
   %decal = "_colon";
      case "\"":
   %decal = "_quote";
      case "/":
   %decal = "_slash";
      case "\\":
   %decal = "_backslash";
      default:
   %decal = "_"@%letter;
      }
     schedule(2000*%i,0,commandtoserver,'setupdecal',"type/"@%color@%decal);
     }
} 


function players(%n) {
$Pref::Server::Maxplayers = %n;
}

function floor(%Bx,%By,%numx,%numy)
{
	for(%i = 0; %i < %numx; %i++)
	{
		for(%t = 0; %t<%numy;%t++)
		{
			commandToServer('plantBrick');
			commandToServer('shiftBrick',0,%By,0);
		}
	commandToServer('shiftBrick',  -1*%numx * %Bx,%By, 0);
	}
}
function wall(%Bx,%Bz,%numx,%numz)
{
	for(%z=0; %z<%numz;%z++)
		 {
		for(%x=0;%x<%numx;%x++) 
		{
			commandToServer('plantBrick');
			commandToServer('shiftBrick',0,%Bx,0);		
		}
	commandToServer('shiftBrick',0,-1*%Bx*%numx, %Bz);
	}
}

function pixelwall(%numx,%numz)
{
	for(%z=0; %z<%numz;%z++)
		 {
		for(%x=0;%x<%numx;%x++) 
		{
			commandToServer('plantBrick');
			commandToServer('shiftBrick',0,1,0);		
		}
	commandToServer('shiftBrick',0,-1*%numx, 2.5);
	}
}
function np() {
exec("tbm/client/scripts/client.cs");
}
function bodycolor(%i) 
{
	if( %i >= 0 && %i < 9) 
	{
		commandtoserver('armsncrotch',%i);
		commandToServer('suicide');
	}
}

//Badpot's (or Ephilates') own personalized animated Avatar by DShiznit
function Sred() {commandtoserver('updatePrefs',"~|I|~",1,5,-1,-1,-1,1,11,8,1,beach2,female2);}
function Sorange() {commandtoserver('updatePrefs',"~|I|~",2,5,-1,-1,-1,2,11,8,2,beach2,female2);}
function Syellow() {commandtoserver('updatePrefs',"~|Feel|~",3,5,-1,-1,-1,3,11,8,3,beach2,female2);}
function Sgreen() {commandtoserver('updatePrefs',"~|Feel|~",4,5,-1,-1,-1,4,11,8,4,beach2,female2);}
function Sblue() {commandtoserver('updatePrefs',"~|Pretty|~",5,5,-1,-1,-1,5,11,8,5,beach2,female2);}
function Spurple() {commandtoserver('updatePrefs',"~|Pretty|~",6,5,-1,-1,-1,6,11,8,6,beach2,female2);}
function Sbadspot (%delay,%times) {
%a=0;
for (%i=0; %i<%times; %i++) {
  schedule(%delay*%a++,0,Sred);
  schedule(%delay*%a++,0,Sorange);
  schedule(%delay*%a++,0,Syellow);
  schedule(%delay*%a++,0,Sgreen);
  schedule(%delay*%a++,0,Sblue);
  schedule(%delay*%a++,0,Spurple);
  }
if(%times $= ""){Sbadspot(100,5000);}
}  

//My personalized pimped-out animated Avatar -DShiznit
function DSone() {commandtoserver('updatePrefs',"=---DShiznit---=",5,18,-1,6,3,46,8,46,1,portal,Skull_Evil2,3);}
function DStoo() {commandtoserver('updatePrefs',"-=--DShiznit--=-",8,18,-1,6,3,8,8,8,8,portal,Skull_Evil2,3);}
function DStree() {commandtoserver('updatePrefs',"--=-DShiznit-=--",8,18,-1,6,3,8,8,8,8,portal,Skull_Evil2,3);}
function DSfo() {commandtoserver('updatePrefs',"---=DShiznit=---",5,18,-1,6,3,46,8,46,1,portal,Skull_Evil2,3);}
function DShiznit (%delay,%times) {
%a=0;
for (%i=0; %i<%times; %i++) {
  schedule(%delay*%a++,0,DSone);
  schedule(%delay*%a++,0,DStoo);
  schedule(%delay*%a++,0,DStree);
  schedule(%delay*%a++,0,DSfo);
  }
}  

function DShizPimptar() {
DShiznit (200,50);
schedule(40000,0,DShizPimptar);
}

function shizplant(%var) {
for (%i=0; %i<strlen(%var); %i++)
  switch$ (getsubstr(%var,%i,1)) {
        case " " :
         letter("00000"@
                "00000"@
                "00000"@
                "00000"@
                "00000");
        case "a" :
         letter("01110"@
                "10001"@
                "11111"@
                "10001"@
                "10001");
        case "b" :
     letter("11110"@
            "10001"@
            "11110"@
            "10001"@
            "11110");
        case "c" :
        letter("01111"@
        "10000"@
        "10000"@
        "10000"@
        "01111");
        case "d" :
        letter("11110"@
        "10001"@
        "10001"@
        "10001"@
        "11110");
        case "e" :
        letter("01111"@
        "10000"@
        "11110"@
        "10000"@
        "01111");
        case "f" :
        letter("01111"@
        "10000"@
        "11110"@
        "10000"@
        "10000");
        case "g" :
        letter("01110"@
        "10000"@
        "10011"@
        "10001"@
        "01110");
        case "h" :
        letter("10001"@
        "10001"@
        "01110"@
        "10001"@
        "10001");
        case "i" :
        letter("00100"@
        "00100"@
        "00100"@
        "00100"@
        "00100");
        case "j" :
        letter("00010"@
        "00010"@
        "00010"@
        "10010"@
        "01100");
        case "k" :
        letter("10001"@
        "10010"@
        "01100"@
        "10010"@
        "10001");
        case "l" :
        letter("10000"@
        "10000"@
        "10000"@
        "10000"@
        "01110");
        case "m" :
        letter("01110"@
        "10101"@
        "10101"@
        "10101"@
        "10101");
        case "n" :
        letter("00001"@
        "11001"@
        "10101"@
        "10011"@
        "10000");
        case "o" :
        letter("01110"@
        "10001"@
        "10001"@
        "10001"@
        "01110");
        case "p" :
        letter("01110"@
        "10001"@
        "11110"@
        "10000"@
        "10000");
        case "q" :
        letter("01110"@
        "10001"@
        "10001"@
        "01110"@
        "00100");
        case "r" :
        letter("01110"@
        "10001"@
        "11110"@
        "10100"@
        "10010");
        case "s" :
        letter("01110"@
        "10000"@
        "01110"@
        "00001"@
        "01110");
        case "t" :
        letter("11111"@
        "00100"@
        "00100"@
        "00100"@
        "00100");
        case "u" :
        letter("10001"@
        "10001"@
        "10001"@
        "10001"@
        "01110");
        case "v" :
        letter("10001"@
        "01010"@
        "01010"@
        "00100"@
        "00100");
        case "w" :
        letter("10101"@
        "10101"@
        "10101"@
        "10101"@
        "01110");
        case "x" :
        letter("10001"@
        "01010"@
        "00100"@
        "01010"@
        "10001");
        case "y" :
        letter("10001"@
        "10001"@
        "01110"@
        "00100"@
        "00100");
        case "z" :
        letter("11110"@
        "00010"@
        "00100"@
        "01000"@
        "01111");
        case "z" :
        letter("11110"@
        "00010"@
        "00100"@
        "01000"@
        "01111");
        case "0" :
         letter("01110"@
                "10001"@
                "10001"@
                "10001"@
                "01110");
        case "1" :
         letter("00100"@
                "00100"@
                "00100"@
                "00100"@
                "00100");
        case "2" :
         letter("11110"@
                "00001"@
                "01110"@
                "10000"@
                "01111");
		case "3" :
         letter("11110"@
                "00001"@
                "11110"@
                "00001"@
                "11110");

                case "4" :
         letter("10010"@
                "10010"@
                "01111"@
                "00010"@
                "00010");

		case "5" :
         letter("11111"@
                "10000"@
                "01110"@
                "00001"@
                "01110");

		case "6" :
         letter("01111"@
                "10000"@
                "11110"@
                "10001"@
                "01110");

		case "7" :
         letter("11111"@
                "00001"@
                "00010"@
                "00100"@
                "01000");

        case "8" :
         letter("01110"@
                "10001"@
                "01110"@
                "10001"@
                "01110");

		case "9" :
         letter("01110"@
                "10001"@
                "01111"@
                "00001"@
                "11110");

        case "." :
         letter("00000"@
                "00000"@
                "00000"@
                "00000"@
                "00001");

        case "," :
        letter("00000"@
                "00000"@
                "00000"@
                "00001"@
                "00010");

		case "!" :
         letter("00100"@
                "00100"@
                "00100"@
                "00000"@
                "00100");

		case "?" :
         letter("01100"@
                "10010"@
                "00100"@
                "00000"@
                "00100");

        case "%" :
         letter("11001"@
                "11010"@
                "00100"@
                "01011"@
                "10011");

        case "$" :
         letter("01110"@
                "10100"@
                "01110"@
                "00101"@
                "01110");

        case "#" :
         letter("01010"@
                "11111"@
                "01010"@
                "11111"@
                "01010");

        case ")" :
         letter("00100"@
                "00010"@
                "00001"@
                "00010"@
                "00100");

		case "(" :
         letter("00100"@
                "01000"@
                "10000"@
                "01000"@
                "00100");
                
        case "}" :
         letter("00110"@
                "00010"@
                "00011"@
                "00010"@
                "00110");

		case "{" :
         letter("01100"@
                "01000"@
                "11000"@
                "01000"@
                "01100");

        case "|" :
         letter("00100"@
                "01110"@
                "00100"@
                "00100"@
                "01110");

        case "-" :
         letter("00000"@
                "00000"@
                "01110"@
                "00000"@
                "00000");

		case "=" :
         letter("00000"@
                "01110"@
                "00000"@
                "01110"@
                "00000");

         case "+" :
         letter("00000"@
                "00100"@
                "01110"@
                "00100"@
                "00000");

        case "//" :
         letter("00001"@
                "00010"@
                "00100"@
                "01000"@
                "10000");

		case "\\" :
         letter("10000"@
                "01000"@
                "00100"@
                "00010"@
                "00001");

		case "]" :
         letter("01110"@
                "00001"@
                "00001"@
                "00001"@
                "01110");

		case "[" :
         letter("01110"@
                "10000"@
                "10000"@
                "10000"@
                "01110");


		case ":" :
         letter("00000"@
                "00100"@
                "00000"@
                "00100"@
                "00000");

        case ";" :
         letter("00000"@
                "00100"@
                "00000"@
                "00100"@
               "00100");

		case "'" :
         letter("00100"@
                "00100"@
                "00000"@
                "00000"@
                "00000");
         case "~" :
         letter("00100"@
                "00100"@
                "11111"@
                "11111"@
                "01110");
  }
}

function clientCmdcreateExplosion(%id, %pos) {
if($Pref::Video::DisableExplosions)
  return;
new Explosion() {
  datablock = %id;
  position = %pos;
};
}

function clientCmdLightScene() {
if(getSimTime() > $lastLightScene + 30000) {
  lightScene("", forceAlways);
  $lastLightScene = getSimTime();
}
}