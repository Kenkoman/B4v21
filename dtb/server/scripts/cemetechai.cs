//----------------------------------------AI PLAYER SCRIPT
//written by members of Cemetech.net including Kerm Martian, elfprince13, & jpez 

///////////////////
//sounds added by DShiz
///////////////////
datablock AudioProfile(shipengine1Sound)
{
   filename    = "dtb/data/sound/shipengine1.wav";
   description = AudioDefaultLooping3d;
   preload = true;
};
datablock AudioProfile(shipengine2Sound)
{
   filename    = "dtb/data/sound/shipengine2.wav";
   description = AudioDefaultLooping3d;
   preload = true;
};


///////////////////
/// server commands
///////////////////
function serverCmdcFA(%client,%wep,%number,%color) 
{ 
	if(%client.isSuperAdmin)
	{
   		createFighterArmy(%client,%wep,%number,%color); 
   	}
   
}


function serverCmdcloneBotAdd(%client){
	if(%client.rankCheck(1))
	{
		serverCmdadjustobj(%client,1000);
		if(%client.edit){ $gdelbots = 0; $botlist[$numbots] = %client.lastswitch; $numbots++; clonedonada(%client.lastswitch);}
	}
}
function serverCmdrVB(%client,%numred,%numblue) 
{ 
	if(%client.isSuperAdmin)
	{
  		 createRandomFighterArmy(%numred,"red");
   		createRandomFighterArmy(%numblue,"blue"); 
   	}
   
} 
function serverCmdcRFA(%client,%number,%color) 
{ 
	if(%client.isSuperAdmin)
	{
  		 createRandomFighterArmy(%number,%color); 
  	}
   
} 

function servercmdAddfbot(%client)
{
	if(%client.isSuperAdmin)
	{
		addbot(%client);
	}
}


function servercmdSetArmy(%client,%color)
{
	if(%client.isSuperAdmin)
	{
		%firstword = getword(%client.player.getshapename(),0);
		if(%firstword $= "") %firstword = "namelesswonder";
		%client.player.setshapename(%firstword SPC "army" SPC %color);
	}
}

function serverCmdCbg(%client){
	if(%client.isSuperAdmin)
	{
		createBodyGuard(%client.namebase);
	}
}

function serverCmdpedestrianbot(%client){
	if(%client.rankCheck(2))
	{
		pedestrianbot(%client);
	}
}



///////////////////
///end of server commands
///////////////////

///////////
///general
///////////
$botlist[0] = "DUMMY"; 
$numbots = 0; 

function blist(){ 
   for(%i = 0; %i <= $numbots; %i++){ 
      %bot = $botlist[%i]; 
      if(isobject(%bot)){
	      echo(%bot SPC %bot.getName()); 
      }
   } 
} 

//clearbots() server command 
function serverCmdcB() 
{ 
   clearbots(); 
} 

function clearbots(){ 
   $gdelbots = 1;
   $botlist[0] = "DUMMY"; 
   $numbots = 0; 
   echo("killing bots...");
   
} 
function randomWeapon(){ 
   %index = getRandom($normalWepTotal) + 1;
   return $normalWeapon[%index].getname();
}

function randomPlayer(){ 
   %index = getRandom(ClientGroup.getCount() - 1); 
   return ClientGroup.getObject(%index); 
}

function move(%who,%targetlocation){
	%targetx = getWord( %targetlocation, 0); 
    %targety = getWord( %targetlocation, 1); 
    %targetz = getWord( %targetlocation, 2); 
    
    %whox = getWord( %who.getTransform(), 0); 
    %whoy = getWord( %who.getTransform(), 1); 
    %whoz = getWord( %who.getTransform(), 2);
	
	 %who.setMoveDestination(%targetlocation); 
     if ( %whoz <  %targetz) 
	  {
    	%bvel = %who.getVelocity();
	   	%xvel = getWord(%bvel, 0);
	   	%yvel = getWord(%bvel, 0);
	   	%zvel = getWord(%bvel, 0);
	   	%who.setVelocity(%xvel SPC %yvel SPC (%zvel + 17)); 
	  } 
		  
      if (inWater(%who) != 0){
	      %who.playThread(0,crouch);
	      if(%whoz > %targetz){
		   	%bvel = %who.getVelocity();
		   	%xvel = getWord(%bvel, 0);
		   	%yvel = getWord(%bvel, 0);
		   	%zvel = getWord(%bvel, 0);
		   	%who.setVelocity(%xvel SPC %yvel SPC (%zvel - 20));    
	      }
      } else{
	      %who.stopThread(0);
      } 
}

function listplayers(){ 

		%center = "0 0 0";
  	 	initContainerRadiusSearch(%center,1000000000.0,$TypeMasks::PlayerObjectType);
		%next = containerSearchNext();
		while(isObject(%next)){
			echo("ID:" SPC %next SPC "NAME:" SPC %next.getshapename());
			echo(" "@getarmy(%next));
			%next = containerSearchNext();
		}
}


function inWater(%bot){
	if(isObject(%bot)){
		%bt = %bot.getTransform();
		%center = getWords(%bt, 0 ,2);
		
		initContainerRadiusSearch(%center,1.0,$TypeMasks::WaterObjectType);
		return containerSearchNext();
	}
	return 0;
}


function clonedonada(%who){
	if(isobject(%who)){
	   if($gdelbots == 1){
		   %who.kill();
		   return;
	   }
		schedule(1000,0,clonedonada,%who); 
    }
}
///////////
///end of general
///////////

////////////////////////////////
/// team stuff
///////////////////////////////



function createFighterArmy(%client,%wep,%number,%color) 
{ 
   createFighterBot(%wep,%client.namebase,%color); 
   %number--; 
   if (%number > 0) 
   { 
      schedule(2000,0,createFighterArmy,%client,%wep,%number,%color); 
   } 
} 
function createRandomFighterArmy(%number,%color) 
{ 
   cfb(%color); 
   %number--; 
   if (%number > 0) 
   { 
      schedule(2000,0,createRandomFighterArmy,%number,%color); 
   } 
} 



function cfb(%army){ 
   %randomplayer = ClientGroup.getObject(getRandom(ClientGroup.getCount() - 1)); 
   createFighterBot(randomWeapon(),%randomplayer.namebase,%army); 
} 

function createFighterBot(%wep,%targetName,%armyName) 
{ 
	$gdelbots = 0;
    %who = new AIPlayer() { 
        dataBlock = LightMaleHumanArmor; 
        aiPlayer = true; 
    }; 
   
   %conn = new GameConnection(ZombieConnection);
   %conn.setConnectArgs(getWord(%targetname,0) SPC "Bot" SPC %armyName);
   %conn.setJoinPassword($pref::Server::Password);
   %who.client = %conn;
   %conn.player = %who;
   
   if(%wep $= "") %wep = "sword"; 
   %wep = %wep@"Image"; 

    %target = getPlayerByName(%targetName); 
    MissionCleanup.add(%who); 
    echo("ID :"@%who); 
    // Player setup 
    %who.setMoveSpeed(1); 
    %who.setTransform(pickSpawnPoint()); 
    %who.setEnergyLevel(60);
    %name = getWord(%targetname,0) SPC "Bot" SPC %armyName;
    echo(%name);
   	%who.setName(%name);
   	%who.setshapename(%name); 
   	botaddmsg(%who);
    %who.setSkinName(%armyName); //bots wear their army color 
    %who.mountImage(%target.headCode, $headSlot, 1, %target.headCodeColor);
    %who.mountImage(%target.visorCode, $visorSlot, 1, %target.visorCodeColor);
    %who.mountImage(%target.backCode, $backSlot, 1, %target.backCodeColor);
    %who.mountImage(%target.leftHandCode, $leftHandSlot, 1, %target.leftHandCodeColor);
    %who.mountImage(%target.chestCode, $chestSlot, 1, %target.chestdecalcode);
    %who.mountImage(%target.faceCode, $faceSlot, 1, %target.facedecalcode);
    %who.mountImage(nametoid(%wep),$rightHandSlot);
   $botlist[$numbots] = %who;
   $numbots++;
   if($TeamCount >= 2){
	   assignateam(%who.client);
   }
    schedule(1000,0,mybotreaim,%who,%name);
} 

function mybotreaim(%who,%name) 
{ 
   if(isobject(%who)){
	   if($gdelbots == 1){
		   botdropmsg(%who);
		   %who.delete();
		   return;
	   }
	   %target = findClosestEnemy(%who); 
  
   %whox = getWord( %who.getTransform(), 0 ); 
   %whoy = getWord( %who.getTransform(), 1 ); 
   %whoz = getWord( %who.getTransform(), 2 ); 	    
   if (isobject(%target) && getarmy(%bot) !$= getarmy(%target)) 
   {  
	  %who.setAimObject(%target); 
	  %who.setImageTrigger(0,1); 
      %targetlocation = getWords(%target.getTransform(),0,2);
   } else{
	    %who.clearaim();
	  	%whox = %whox + getrandom(2) - 1;
	  	%whoy = %whox + getrandom(2) - 1; 
	  	%targetlocation = %whox SPC %whoy SPC %whoz; 
    }
    
    %targetx = getWord( %targetlocation, 0); 
    %targety = getWord( %targetlocation, 1); 
    %targetz = getWord( %targetlocation, 2); 
    
      %who.setShapeName(%name);
      
      move(%who,%targetlocation);

    	%who.setAimLocation(%targetx@" "@%targety@" "@%targetz); 
        schedule(1000,0,mybotreaim,%who,%name); 
    }
} 


function getPlayerByName(%playername){ 
   for (%clientIndex = 0; %clientIndex < ClientGroup.getCount(); %clientIndex++) {      
      %victim = ClientGroup.getObject( %clientIndex );    
      if (%victim.namebase $= %playername) return %victim; 
   } 
   return 0; 
} 


function getArmy(%who){ 
	if(isobject(%who)){
		return getWord( %who.getShapeName(), 2);
	}
	return "";
} 

//finds the closest member of a non-friendly army 
function findClosestEnemy(%bot){ 
	if(isobject(%bot)){
		%center = getwords(%bot.getTransform(),0,2);
  	 	initContainerRadiusSearch(%center,1000000000.0,$TypeMasks::PlayerObjectType);
		%next = containerSearchNext();
		while(isObject(%next)){
			if((getarmy(%next) !$= getarmy(%bot) && $TeamCount < 2) || (%bot.client.team !$= %next.client.team && $TeamCount >=2)) return %next;
			%next = containerSearchNext();
		}
		
	}
	return 0;
}

/////////////////////////////////////////
///end of team stuff
/////////////////////////////////////////


////////////////////////////////////////
////begin bodyguard stuff
////////////////////////////////////////


function createBodyGuard(%targetName) 
{ 
	if(!isobject(getPlayerByName(%targetname))) return 0;
	$gdelbots = 0;
    %who = new AIPlayer() { 
        dataBlock = LightMaleHumanArmor; 
        aiPlayer = true; 
    }; 
    
   %conn = new GameConnection(BGConnection);
   %conn.setConnectArgs(getWord(%targetname,0) SPC "Bot" SPC %armyName);
   %conn.setJoinPassword($pref::Server::Password);
   %who.client = %conn;
   %conn.player = %who;
  
    %target = getPlayerByName(%targetName); 
    MissionCleanup.add(%who); 
    echo("ID :"@%who); 
    // Player setup 
    %who.setMoveSpeed(1);
    %tt =  %target.player.getTransform();
    %who.setTransform((getwords(%tt,0) + 3) SPC getwords(%t,1) SPC getwords(%tt,2)); 
    %who.setEnergyLevel(60);
    %name = getWord(%targetname,0)@"'s bodyguard" SPC getarmy(%target.player);
    echo(%name);
   	%who.setName(%name);
   	%who.setshapename(%name); 
   	 botaddmsg(%who);
    %who.setSkinName(%armyName); //bots wear their army color 
    %who.mountImage(%target.headCode, $headSlot, 1, %target.headCodeColor); 
    %who.mountImage(%target.visorCode, $visorSlot, 1, %target.visorCodeColor); 
    %who.mountImage(%target.backCode, $backSlot, 1, %target.backCodeColor); 
    %who.mountImage(%target.leftHandCode, $leftHandSlot, 1, %target.leftHandCodeColor); 
    %who.mountImage(%target.chestCode, $chestSlot, 1, %target.chestdecalcode); 
    %who.mountImage(%target.faceCode, $faceSlot, 1, %target.facedecalcode); 
    %who.mountImage(nametoid(flamethrowerimage),$rightHandSlot); 
   $botlist[$numbots] = %who; 
   $numbots++;
   if($TeamCount >= 2){
	   %who.client.team = %target.team;
	   %who.setSkinName(%target.team);
   }
   schedule(1000,0,bodyguardreaim,%who,%target.player); 
}

function bodyguardreaim(%who,%owner) 
{ 
   if(isobject(%who)){
	    if($gdelbots == 1){
			botdropmsg(%who);
		    %who.delete();
		    return;
	    }
	    if(isobject(%owner)){   
		    %who.client.team = %owner.client.team;
			%target = findThreat(%who,%owner); 
  
   			%whox = getWord( %who.getTransform(), 0 ); 
   			%whoy = getWord( %who.getTransform(), 1 ); 
   			%whoz = getWord( %who.getTransform(), 2 ); 	    
   			if (isobject(%target) && isobject(%owner) && isValidTarget(%bot,%owner,%target)) 
   			{  
				%who.setAimObject(%target); 
	 			%who.setImageTrigger(0,1);
	  			%targetlocation = %target.getTransform();
	   	
      	
   			} else{
	    		%who.clearaim();
	    		%whox = %whox + getrandom(2) - 1;
	  			%whoy = %whox + getrandom(2) - 1; 
	  			%targetlocation = %whox SPC %whoy SPC %whoz; 
    		}
    		if(isobject(%owner) && isobject(%who)){
	    	  	%who.setShapeName(getword(%owner.getshapename(),0)@"'s bodyguard" SPC getarmy(%owner));
      	
	 	 	   	%targetx = getWord( %targetlocation, 0); 
     			%targety = getWord( %targetlocation, 1); 
     			%targetz = getWord( %targetlocation, 2); 
    
      			move(%who,%owner.getTransform());
      
        		%who.setAimLocation(%targetx@" "@%targety@" "@%targetz); 
    			schedule(1000,0,bodyguardreaim,%who,%owner); 
    		}
	    }    
    }
    if(!isObject(%owner)){
	    %army = getarmy(%who) $= "" ? "red" : getarmy(%who);
	    %name = "exbodyguard bot" SPC %army;
	    schedule(1000,0,mybotreaim,%who,%name);
    }
} 

function findThreat(%bot,%owner){
	
	if(isobject(%bot)){
		%center = getwords(%bot.getTransform(),0,2);
  	 	initContainerRadiusSearch(%center,1000000000.0,$TypeMasks::PlayerObjectType);
		%next = containerSearchNext();
		while(isObject(%next)){
			if(isValidTarget(%bot,%owner,%next)) return %next;
			%next = containerSearchNext();
		}
		
	}
	return 0;
	
}

function isValidTarget(%bot,%owner,%next){
	if(isobject(%bot) && isobject(%owner) && isobject(%next)) return (%next != %owner) && (%next !$= %bot) && ((getarmy(%bot) !$= getarmy(%next) && getarmy(%owner) !$= "" && $TeamCount < 2) || (%owner.client.team !$= %target.client.team && $TeamCount >= 2));
	return 0;
}
////////////////////////////////////////
////end bodyguard stuff
////////////////////////////////////////




////////////////////////////////////////
////Pedestrian stuff
////////////////////////////////////////
//Yeah, this is Wiggy's pedestrian code 1.1
//If you read this hopefully it's because I've released this, and you're either looking to see what i did
//So I'm leaving all of my notes in and stuff
//Please don't laugh at me or in any way make me regret leaving this stuff in
$pedbotdestdist = 100;
//This is how far Bob is allowed to wander at a time
$pedbotplatedist = 10;
//This is about how far Bob can stray from the plates
function pedbottick(%bot)
{
if ($gdelbots)
  {
  %bot.delete();
  return;
  }
if ( !%bot )
  return;

//First up is seeing if Bob's going somewhere, and if not make him move

if (%bot.getState() $= "Dead")
	return;


if (!isObject(%bot))
	return;
//Okay, Bob's alive, so now we make him move if he's not already

//If Bob gets zombified, he becomes a zombie
if(%bot.getmountedimage(0) == nametoid(zombieimage)) {
zombiebottick(%bot);return;}

%velocity = %bot.getvelocity();
       %velx = mabs(getWord( %velocity, 0));
       %vely = mabs(getWord( %velocity, 1));
%totalvel = %velx + %vely;
if ( %totalvel <= 1.25 && isobject(%bot)) {
getnewpeddest(%bot);
}
//In the name of redundancy, I'll do a second check
if (%bot.gettransform() != %bot.dest) {
}
else {
//Okay, Bob must have just arrived, so make him go somewhere else
getnewpeddest(%bot);
}

//Now for a third check, to see if Bob is close enough to give him a new destination
       %destpos = %bot.dest;
       %destx = getWord( %destpos, 0);
       %desty = getWord( %destpos, 1);
   %xdiff = mabs(getWord( %bot.getTransform(), 0 ) - %destx);
   %ydiff = mabs(getWord( %bot.getTransform(), 1 ) - %desty);
	%diff = %xdiff + %ydiff;
if (%diff <= 4) {
//Bob is close enough, make him go somewhere else to make it look more real and less choppy
getnewpeddest(%bot);
}

%dest = %bot.dest;
 %xdest = getWord(%dest, 0);
 %ydest = getWord(%dest, 1);
 %zdest = getWord(%dest, 2);
%trans = %bot.gettransform();
 %xtrans = getWord(%trans, 0);
 %ytrans = getWord(%trans, 1);
 %ztrans = getWord(%trans, 2);


//Fiveforce suggested keeping him on plates
//I like the idea, but what if you don't use plates and such?
//So I'm going to use a pref to define whether he has to stay by/on plates
//Default is no, because I don't like making stuff like plates for Bob to walk on


if($pref::server::pedbotstayonplates) {
initContainerRadiusSearch(%bot.getposition(),$pedbotplatedist,$TypeMasks::StaticShapeObjectType);
if (containerSearchNext()) {
//There's a plate nearby, so Bob is still in the city
}
else {
//Bob is trying to leave, we can't allow that!
initContainerRadiusSearch(%bot.getposition(),1000000000.0,$TypeMasks::StaticShapeObjectType);
%nearestplate = containerSearchNext();
	//This is the nearest brick
	//It might be a building or something, though, so you can't just make Bob go to it, he'd hit his head on the building
	//However, with some luck, the raycast will make Bob walk around it.
%newdest = %nearestplate.getposition();
 %bot.dest = %newdest;
 %bot.setMoveDestination(%bot.dest);
%bot.setAimLocation(%bot.dest);


//This should make Bob go towards the plate
}
}

//Okay, so Bob is ready
//Hmm, I say we figure out if Bob's under attack, and if so then tell him to run away
//Then figure out collision stuff


     if (isobject(%bot.threat) || %bot.threat !$= "null")
       {
//Oh no!  Some evil-doer is shooting Bob!
//Make Bob speed up, and tell him to go away from the bad person
       %bot.threatcount++; //increment how long he's been threatened
	if(%bot.threatcount >= 90) {
	   %bot.setMoveSpeed(0.5);
		%bot.threatcount = 3141;
		%bot.threat = "null";
		//If Bob survives thirty seconds he can survive anything
		}
//Bob doesn't need to go to the grocery store if he's under sniper fire, so scrap the old destination and run
       %threatpos = %bot.threat.getTransform();
       %threatx = getWord( %threatpos, 0);
       %threaty = getWord( %threatpos, 1);
       %threatz = getWord( %threatpos, 2);
	   %xdiff = mabs(getWord( %bot.getTransform(), 0 ) - %threatx);
	   %ydiff = mabs(getWord( %bot.getTransform(), 1 ) - %threaty);
	   %xchange = getWord( %bot.getTransform(), 0 ) - %threatx;
	   %ychange = getWord( %bot.getTransform(), 1 ) - %threaty;
%changevel = vectorScale(vectorNormalize(%xchange@" "@%ychange@" 0"), 10);
 %xchange = getWord(%trans, 0);
 %ychange = getWord(%trans, 1);
       %botx = %xtrans + (%xchange * 2);
       %boty = %ytrans + (%ychange * 2);
       %botz = %ztrans;

if(%bot.threatcount <= 1) {
//Only tell Bob to run away initially, otherwise he'll run by himself
   %bot.setMoveSpeed(1); 
	%bot.oldDest = %bot.Dest;
       %bot.Dest = %botx@" "@%boty@" "@%botz;
 %bot.setMoveDestination(%bot.dest);
 %bot.setAimLocation(%bot.dest);


%dest = %bot.dest;
 %xdest = getWord(%dest, 0);
 %ydest = getWord(%dest, 1);
 %zdest = getWord(%dest, 2);
}
if(%bot.threatcount == 3141)
%bot.threatcount = 0;
       }
     else
       {
//No threat, it's either dead or there wasn't one, so let's keep going the same direction
//At least, I hope there's no threat
//Hmm, maybe saving pedestrians by killing their threat makes them grateful to you

//I'll fill up this section later, I promise
       }






//Bob doesnt swim too well, so keep him out of water
//Aww dangit, Force Islands is too flat, need to fix the water thing
      if (inWater(%bot) != 0){
	//Bob looks really choppy when he falls in the water.  I'll try to fix this later
//	      %bot.playThread(0,crouch);
	      if(%ztrans > %zdest){
		   	%bvel = %bot.getVelocity();
		   	%xvel = getWord(%bvel, 0);
		   	%yvel = getWord(%bvel, 1);
		   	%zvel = getWord(%bvel, 2);
		   	%bot.setVelocity(%xvel SPC %yvel SPC (%zvel - 1));    
	      }
	      else {
		   	%bvel = %bot.getVelocity();
		   	%xvel = getWord(%bvel, 0);
		   	%yvel = getWord(%bvel, 1);
		   	%zvel = getWord(%bvel, 2);
		   	%bot.setVelocity(%xvel SPC %yvel SPC (%zvel + 1));    
	      }
} else {
//Phew, Bob isn't in water, so we can change his Z destination without drowning him
%dest = %bot.dest;
 %xdest = getWord(%dest, 0);
 %ydest = getWord(%dest, 1);
 %zdest = getWord(%dest, 2);
%bot.Dest = %xdest@" "@%ydest@" "@%ztrans;
//     %bot.stopThread(0);
//     %bot.playThread(0,root);
//This should keep him looking straight when he's not in water
//When Bob is in water he's too twitchy to tell
//Since the Z of his destination isn't updated when he enters the water, the Z will be the surface till he comes out
//Well, the surface, give or take his jumping
}

//Okay, Bob has a direction and threats are taken care of.  Now for collision
//And no jets for Bob, he doesn't wander around town with a jetpack like we do
 %bot.setMoveDestination(%bot.dest);
	%tempmove = %bot.getMoveDestination();
	%x = getWord(%tempmove,0);
	%y = getWord(%tempmove,1);
	%z = getWord(%bot.gettransform(),2);
%bot.setAimLocation(%x@" "@%y@" "@%z);




	   %eyepoint = %bot.getEyepoint();
	%x = getWord(%eyepoint,0);
	%y = getWord(%eyepoint,1);
	%z = getWord(%eyepoint,2);
	%eyepoint = %x @ " " @ %y @ " " @ %z;
%botvel = %bot.getvelocity();
 %xvel = getWord(%dest, 0);
 %yvel = getWord(%dest, 1);
 %zvel = getWord(%dest, 2);
   %veltest = %xvel@" "@%yvel@" 0";
if(%scanTarg = getWord(ContainerRayCast(%EyePoint, vectorAdd(vectorScale(vectorNormalize(%veltest), 5), %EyePoint), $TypeMasks::StaticShapeObjectType), 0)) {
%forvector = %bot.getforwardvector();
%vecx = getWord( %forvector, 0);
%vecy = getWord( %forvector, 1);
%dest = %bot.dest;
 %xdest = getWord(%dest, 0);
 %ydest = getWord(%dest, 1);
 %zdest = getWord(%dest, 2);
%trans = %bot.gettransform();
 %xtrans = getWord(%trans, 0);
 %ytrans = getWord(%trans, 1);
 %ztrans = getWord(%trans, 2);
%xchange = %xdest - %xtrans;
%ychange = %ydest - %ytrans;
%zchange = %zdest - %ztrans;

  for(%scantestnum = 1; %scantestnum <= 7; %scantestnum++){ 
	%newvec = getvectorfromvectorangle(%bot.getvelocity(),%scantestnum);
%scanresult[%scantestnum] = 1;

if(%scanTarg = getWord(ContainerRayCast(%EyePoint, vectorAdd(vectorScale(vectorNormalize(%newvec), 5), %EyePoint), $TypeMasks::StaticShapeObjectType,%bot), 0)) {

//It hit something, so Bob can't go that way
%scanresult[%scantestnum] = 0;

		}

	}
//messageall(' ','Raycast time!  FL is %1, FR is %2, L is %3, R is %4, BL is %5, BR is %6, B is %7',%scanresult[1],%scanresult[2],%scanresult[3],%scanresult[4],%scanresult[5],%scanresult[6],%scanresult[7]);
//Okay, so we've got our results.  Im just gonna beat it to death.  I haven't had much sleep as I write this, so I'm not even gonna use elses, I'm gonna go reverse
if(%scanresult[7] == 1) {
	%newvec = getvectorfromvectorangle(%bot.getvelocity(),7);
}
if(%scanresult[6] == 1) {
	%newvec = getvectorfromvectorangle(%bot.getvelocity(),6);
}
if(%scanresult[5] == 1) {
	%newvec = getvectorfromvectorangle(%bot.getvelocity(),5);
}
if(%scanresult[4] == 1) {
	%newvec = getvectorfromvectorangle(%bot.getvelocity(),4);
}
if(%scanresult[3] == 1) {
	%newvec = getvectorfromvectorangle(%bot.getvelocity(),3);
}
if(%scanresult[2] == 1) {
	%newvec = getvectorfromvectorangle(%bot.getvelocity(),2);
}
if(%scanresult[1] == 1) {
	%newvec = getvectorfromvectorangle(%bot.getvelocity(),1);
}
%destoffset = %xchange@" "@%ychange@" "@%zchange;
%destdist = mpow(((%xchange * %xchange) + (%ychange * %ychange) + (%zchange * %zchange)),1/3);
%newvec = vectorScale(%newvec, %destdist);
%newdest = VectorAdd(%bot.getposition(),%newvec);
 %bot.dest = %newdest;
 %bot.setMoveDestination(%bot.dest);
%bot.setAimLocation(%bot.dest);



}//end raycast


    schedule((333),0,pedbottick,%bot);
}
//Thats the end of the long scary function

function pedestrianbot(%client)
{
$gdelbots = 0;

%player= AIPlayer::SpawnPlayer();
   %player.setShapeName("Pedestrian");
   %player.owner = %client;
   %clientpos = %client.player.getTransform(); 
   %clientx = getWord( %clientpos, 0)+2; 
   %clienty = getWord( %clientpos, 1)+2; 
   %clientz = getWord( %clientpos, 2); 
   %player.setTransform(%clientx@" "@%clienty@" "@%clientz);
   %player.setSkinName(%client.colorSkin);
   %player.setMoveSpeed(0.5); 
   %player.mountImage(%client.headCode, $headSlot, 1, %client.headCodeColor);
   %player.mountImage(%client.visorCode, $visorSlot, 1, %client.visorCodeColor);
   %player.mountImage(%client.backCode, $backSlot, 1, %client.backCodeColor);
   %player.mountImage(%client.leftHandCode, $leftHandSlot, 1, %client.leftHandCodeColor);
   %player.mountImage(%client.chestCode, $chestSlot, 1, %client.chestdecalcode);
   %player.mountImage(%client.faceCode, $faceSlot, 1, %client.facedecalcode);
   %player.client = %client;
   %player.follow = null;
   %player.threat = "null";
%player.clearaim();
%client.lastswitch = %player;
   pedbottick(%player);
}


function getnewpeddest(%bot) {
%clientpos = %bot.getTransform(); 
%clientx = getWord( %clientpos, 0) + getrandom(-$pedbotdestdist,$pedbotdestdist);
%clienty = getWord( %clientpos, 1) + getrandom(-$pedbotdestdist,$pedbotdestdist);
%clientz = getWord( %clientpos, 2);
%bot.olddest = %bot.dest;
%bot.Dest = %clientx@" "@%clienty@" "@%clientz;
 %bot.setMoveDestination(%bot.dest);
%eyez = getWord( %bot.geteyepoint(), 2);
%pointx = getWord( %bot.dest, 0);
%pointy = getWord( %bot.dest, 1);
%bot.clearaim();
}


function getvectorfromvectorangle(%botvel,%angletest) {
 %xvel = getWord(%botvel, 0);
 %yvel = getWord(%botvel, 1);
 %zvel = getWord(%botvel, 2);
	%xsq = mabs(%xvel * %xvel);
	%ysq = mabs(%yvel * %yvel);
 %radius = mSqrt(%xsq + %ysq);
%preexistangle = matan(%xvel,%yvel) * 180 / $pi;

if(%angleTest == 0){
	%xnew = %xvel;
	%ynew = %yvel;
}
if(%angleTest == 3){
	%xnew = %yvel * -1;
	%ynew = %xvel;
}
if(%angleTest == 4){
	%xnew = %yvel;
	%ynew = %xvel * -1;
}
if(%angleTest == 7){
	%xnew = %xvel * -1;
	%ynew = %yvel * -1;
}


if(%angleTest == 1){
%degrees = 135 + %preexistangle;
%deg = %degrees * $pi / 180;
	%xnew = %radius * mCos(%deg);
	%ynew = %radius * mSin(%deg);
}


if(%angleTest == 2){
%degrees = 45 + %preexistangle;
%deg = %degrees * $pi / 180;
	%xnew = %radius * mCos(%deg);
	%ynew = %radius * mSin(%deg);
}

if(%angleTest == 5){
%degrees = -135 + %preexistangle;
%deg = %degrees * $pi / 180;
	%xnew = %radius * mCos(%deg);
	%ynew = %radius * mSin(%deg);
}

if(%angleTest == 6){
%degrees = -45 + %preexistangle;
%deg = %degrees * $pi / 180;
	%xnew = %radius * mCos(%deg);
	%ynew = %radius * mSin(%deg);
}


return %xnew@" "@%ynew@" 0";
}




//end retrieve location


function getsign(%number) {
if (%number == 0)
return 0;
if (%number >= 0)
return 1;
return -1;
}

////////////////////////////////////////
////end pedestrian stuff
////////////////////////////////////////


//////////////////
//overrides
//////////////////
function tbmcollison(%tbmthis,%tbmobj,%tbmcol,%tbmfade,%tbmpos,%tbmnormal) {
  if (%tbmcol.getClassName() !$= "Player" && %tbmcol.getClassName() !$= "AIplayer")return;
  %colData = %tbmcol.getDataBlock();
  %colDataClass = %colData.classname;
  
    if ( %tbmcol.getClassName() $= "AIPlayer") {   
	   if(%tbmcol.getdamagelevel() + %tbmthis.directDamage >= 100){
		    %tbmcol.setdamagelevel(0);
		    if ($TeamCount >= 2) {
    			if (%tbmcol.client.team $= "red") 
      				%spawnPoint = pickSpawnPoint(1);
    			else if (%tbmcol.client.team $= "blue") 
    				  %spawnPoint = pickSpawnPoint(2);   
    			else if (%tbmcol.client.team $= "green") 
      					%spawnPoint = pickSpawnPoint(3);   
    			else if (%tbmcol.client.team $= "yellow") 
      				%spawnPoint = pickSpawnPoint(4);   
    			else 
      				%spawnPoint = pickSpawnPoint(0);
    		}
  			else{ 
    			%spawnPoint = pickSpawnPoint(0);
			}
    		%tbmcol.setTransform(%spawnPoint);
		    if(%tbmcol != %tbmobj.client.player){
			    %tbmobj.client.player.client.incscore(1);   
		    } else{
     			%tbmcol.client.incScore(-1);
		   	}
		}else{
			%tbmcol.damage(%tbmobj,%tbmpos,%tbmthis.directDamage,%tbmobj.getdatablock().damagemsg);
		}
       	return; 
	}
	
   if (%tbmcol.getClassName() $= "Player" ) {
        %dmg = %tbmthis.directDamage;
        %dmg = %tbmcol.client.DMShield? 0 : (%tbmcol.client.DMArmor == 1? 0.25 * %dmg : %dmg);
        
        %tbmcol.damage(%tbmobj,%tbmpos,%dmg,%tbmobj.getdatablock().damagemsg);
        return;
   }
   return;
  
}

ShotgunImage.ammo = "";


exec("./cemetechai/dwing.cs");
exec("./cemetechai/sigmafighter.cs");
exec("./cemetechai/psiwing.cs");
exec("./cemetechai/legoplane.cs");
exec("./cemetechai/truck.cs");
exec("./cemetechai/dropship.cs");
//exec("./cemetechai/legobike.cs");
if(isFile("./cemetechai/audi.cs"))
  exec("./cemetechai/audi.cs");
if(isFile("./cemetechai/jeep.cs"))
  exec("./cemetechai/jeep.cs");
exec("./cemetechai/snowballgun.cs");

function serverCmdcreateteams (%client,%setup) {
  if(%client.rankCheck(2) && %setup !$= ("default" @ $teamCount)) {
    createteams(%setup);
    switch ($TeamCount) {
      case 2:
        messageAll('Msg', "\c3The admin ("@%client.namebase@") has created Red and Blue teams, please pick a team to play, or have the admin lock teams, assign current clients to a team, and turn on auto assign for future clients.");
      case 3:
        messageAll('Msg', "\c3The admin ("@%client.namebase@") has created Red, Blue, and Green teams, please pick a team to play, or have the admin lock teams, assign current clients to a team, and turn on auto assign for future clients.");
      case 4:
        messageAll('Msg', "\c3The admin ("@%client.namebase@") has created Red, Blue, Green, and Yellow teams, please pick a team to play, or have the admin lock teams, assign current clients to a team, and turn on auto assign for future clients.");
      }
      for( %i = 0; %i < ClientGroup.getCount(); %i++ ) assignateam(ClientGroup.getObject(%i));
	  for( %i = 0; %i <= $numbots; %i++ ){ if(isobject($botlist[%i])) assignateam($botlist[%i].client); }
  $pref::server::autoteambalance = 1;
    }
  }
  
function assignateam(%client) {
  %red=0;
  %blue=0;
  %green=0;
  %yellow=0;
  for( %i = 0; %i < ClientGroup.getCount(); %i++) {
        %vclient = ClientGroup.getObject(%i);
        if (%vclient.team $= "red")
          %red++;
        else if (%vclient.team $= "blue")
          %blue++;
        else if (%vclient.team $= "green")
          %green++;
        else if (%vclient.team $= "yellow")
          %yellow++;
    }
    
    for( %i = 1; %i <= $numbots; %i++) {
	   if(isobject($botlist[%i])){
      	 	 %vclient = $botlist[%i].client;
     	   if (%vclient.team $= "red")
      	    %red++;
      	  else if (%vclient.team $= "blue")
      	    %blue++;
      	  else if (%vclient.team $= "green")
      	    %green++;
      	  else if (%vclient.team $= "yellow")
      	    %yellow++;
  	  }
	}
  if ($TeamCount == 2) {
    if (%red <= %blue)
      %nteam = "red";
    else if (%blue < %red)
      %nteam = "blue";
    }
  else if ($TeamCount == 3) {
    if (%red <= %blue && %red <= %green)
      %nteam = "red";
    else if (%blue <= %red && %blue <= %green)
      %nteam = "blue";
    else if (%green <= %red && %green <= %blue)
      %nteam = "green";
    }
  else if ($TeamCount == 4) {
    if (%red <= %blue && %red <= %green && %red <= %yellow)
      %nteam = "red";
    else if (%blue <= %red && %blue <= %green && %blue <= %yellow)
      %nteam = "blue";
    else if (%green <= %red && %green <= %blue && %green <= %yellow)
      %nteam = "green";
    else if (%yellow <= %red && %yellow <= %blue && %yellow <= %green )
      %nteam = "yellow";
    }
  echo("your team is "@%nteam);
  %client.team = %nteam;

}
  
function serverCmdclearteams (%client) {
  if (%client.rankCheck(2)) {
    $teamcount=0;
    for( %i = 0; %i < ClientGroup.getCount(); %i++) {
      %client = ClientGroup.getObject(%i);
      %client.team="";
      commandToClient(%client, 'updatePrefs');
      if (%client.carrier)
        ServerCmddropInventory( %client, 9);
      }
    for (%i = 1; %i <= $numbots; %i++){
	    %bot = $botlist[%i];
	    if(isobject(%bot)) %bot.client.team="";
	}
    $pref::server::autoteambalance = 0;
    messageAll('Msg', "\c3The admin ("@%client.namebase@") has cleared the teams join team and autobalance are disabled.");
    }
  }
  
function serverCmdGetTeamList(%client) {
  %count = 0;
  commandtoclient(%client, 'ClearTeamList', "");
  for (%i = 1; %i <= $TeamCount; %i++) {
	%team = $Teams[%i] @ " Team";
    commandtoclient(%client, 'FillTeamList', %team);
    for (%clientIndex = 0; %clientIndex < ClientGroup.getCount(); %clientIndex++) {
      %cl = ClientGroup.getObject( %clientIndex );
      if ($Teams[%i] $= %cl.team) {
        %name = %cl.namebase;
        commandtoclient(%client, 'FillTeamList', "-"@%name);
        }
      }
    echo("end clients");
    for (%ii = 1; %ii <= $numbots; %ii++) {
      if(isobject($botlist[%ii])){
      	%bot = $botlist[%ii].client;  
      	if ($Teams[%ii] $= %bot.team) {
      	  %name = $botlist[%ii].getshapename();
      	  commandtoclient(%client, 'FillTeamList', "-"@%name);
      	  }
      	}
      }
     echo("end bots");
	}
}

function botAddMsg(%who){
	messageAll('MsgClientJoin', getWord(%who.getshapename(),0) SPC "bot added.",
      %who.getshapename(),
      %who.client,
      %client.sendGuid,
      0,
      true,
      false,
      false,
      false);
}


function botdropmsg(%who){
	messageAll('MsgClientDrop', "bot has been removed", colorbyname(%who.getshapename()), %who.client);
}

function servercmdteamrebal(%client) {
  	if ($TeamCount <= 0) {
    	echo("WTF");
    	return;
	}
  	if(%client.isadmin || %client.issuperadmin || %client==0) {
    	for( %i = 0; %i <= ClientGroup.getCount(); %i+=$TeamCount) {
    	  	ClientGroup.getObject(%i).team = "red";
    	  	ClientGroup.getObject(%i+1).team = "blue";
    	  	if ($TeamCount >= 3)
    	  	  ClientGroup.getObject(%i+2).team = "green";
    	  	if ($TeamCount == 4)
    	  	  ClientGroup.getObject(%i+3).team = "yellow";
	
	    }
	    for( %i = 0; %i < ClientGroup.getCount(); %i++ ) ClientGroup.getObject(%i).player.kill();
	    	
	        for( %i = 1; %i <= $numbots; %i+=$TeamCount) {
		      $botlist[%i].client.team = "red";
		      $botlist[%i+1].client.team = "blue";
    		  if ($TeamCount >= 3)
        		$botlist[%i+2].client.team = "green";
      		  if ($TeamCount == 4)
        	     $botlist[%i+3].client.team = "yellow";

    		}
    		for( %i = 1; %i <= $numbots; %i++ ) $botlist[%i].setskinname($botlist[%i].client.team);
	}


}

function tbmradiusDamage(%sourceObject, %position, %radius, %damage, %damageType, %impulse)
{
   // Use the container system to iterate through all the objects
   // within our explosion radius.  We'll apply damage to all ShapeBase
   // objects.
   InitContainerRadiusSearch(%position, %radius, $TypeMasks::ShapeBaseObjectType);

   %halfRadius = %radius / 2;
   while ((%targetObject = containerSearchNext()) != 0) {

      // Calculate how much exposure the current object has to
      // the explosive force.  The object types listed are objects
      // that will block an explosion.  If the object is totally blocked,
      // then no damage is applied.
      %coverage = calcExplosionCoverage(%position, %targetObject,
         $TypeMasks::InteriorObjectType |  $TypeMasks::TerrainObjectType |
         $TypeMasks::ForceFieldObjectType | $TypeMasks::VehicleObjectType);
      if (%coverage == 0)
         continue;

      // Radius distance subtracts out the length of smallest bounding
      // box axis to return an appriximate distance to the edge of the
      // object's bounds, as opposed to the distance to it's center.
      %dist = containerSearchCurrRadiusDist();

      // Calculate a distance scale for the damage and the impulse.
      // Full damage is applied to anything less than half the radius away,
      // linear scale from there.
      %distScale = (%dist < %halfRadius)? 1.0:
         1.0 - ((%dist - %halfRadius) / %halfRadius);

      // Apply the damage
      if(%targetObject.getClassName() $= "AIPlayer" && %targetObject.getDamageLevel() + (%damage * %coverage * %distScale) >= 100){
	      %targetObject.setDamageLevel(0);
	      if ($TeamCount >= 2) {
    			if (%tbmcol.client.team $= "red") 
      				%spawnPoint = pickSpawnPoint(1);
    			else if (%tbmcol.client.team $= "blue") 
    				  %spawnPoint = pickSpawnPoint(2);   
    			else if (%tbmcol.client.team $= "green") 
      					%spawnPoint = pickSpawnPoint(3);   
    			else if (%tbmcol.client.team $= "yellow") 
      				%spawnPoint = pickSpawnPoint(4);   
    			else 
      				%spawnPoint = pickSpawnPoint(0);
    		}
  			else{ 
    			%spawnPoint = pickSpawnPoint(0);
			}
    		%tbmcol.setTransform(%spawnPoint);
		    if(%tbmcol != %tbmobj.client.player){
			    %tbmobj.client.player.client.incscore(1);   
		    } else{
     			%tbmcol.client.incScore(-1);
		   	}
	  }
      else{
      	%targetObject.damage(%sourceObject, %position,
        %damage * %coverage * %distScale, %damageType);
  	  }
      // Apply the impulse
      if (%impulse) {
         %impulseVec = VectorSub(%targetObject.getWorldBoxCenter(), %position);
         %impulseVec = VectorNormalize(%impulseVec);
         %impulseVec = VectorScale(%impulseVec, %impulse * %distScale);
         %targetObject.applyImpulse(%position, %impulseVec);
      }
   }
}


/////////////////////

function servercmdctfsetup (%client, %teams) {
  if (%client.rankCheck(2)) {
	 servercmdcreateteams (%client,"default"@%teams);
    ctfsetup(%teams);
    if ($CTF==0)
      messageAll( 'Msg', "\c3CTF setup failed. Check your team count & team spawns and make sure you have one flag per team.");
    else     
      messageAll( 'Msg', "\c3The admin ("@%client.namebase@") has successfully started a Capture the Flag Round.  Please assign people to a team that are already on the server; new comers will be auto assigned.");
    }
  }
  
function servercmdctcsetup (%client, %teams) {
  if (%client.rankCheck(2)) {
	servercmdcreateteams(%client,"default"@%teams);
    crownsetup(%teams);
    if ($crownchase==0)
      messageAll( 'Msg', "\c3CTC setup failed. Check your team count & team spawns and make sure you have one crown and at least one goal point.");
    else     
      messageAll( 'Msg', "\c3The admin ("@%client.namebase@") has successfully started a Capture the Crown Round.  Please assign people to a team that are already on the server; new comers will be auto assigned.");
    }
  }





////////////////////////////////////////
////Zombie stuff
////////////////////////////////////////

function onServerDestroyed() {
   serverIRCdisconnect();
	$stopthezombies = 1;
}

$stopthezombies = 0;
$zombieclient = new GameConnection(ZombieConnection);
$zombieclient.setConnectArgs("Zombies");
$zombieclient.setJoinPassword($pref::Server::Password);
$zombieclient.namebase = "Zombie";
$zombieclient.name = addTaggedString("\cp\c8" @ $zombieclient.namebase @ "\co");
$zombieclient.player = $zombieclient;

function zomexec() {
exec("dtb/server/scripts/cemetechai.cs");
}

function serverCmdzombiebot(%client){
if(%client.rankCheck(2)) { echo(%client.namebase SPC "is spawning a zombie.");zombiebot_create(%client, 1); }
}

function servercmdmakezombiespawn(%client) {
if(%client.rankCheck(2)) {
  %block = new Item() {
    position = vectorAdd(%client.getControlObject().getPosition(), vectorAdd("0 0 1", vectorScale(%client.getControlObject().getForwardVector(), "2 2 0")));
    rotation = "1 0 0 0";
    scale = "1 1 1";
    dataBlock = "portculyswitch";
    owner = getRawIP(%client);
    static = true;
  };
  %block.setSkinName('red');
  %block.setCloaked(1);
  %block.type[1] = -6;
  MissionCleanup.add(%block);
}
}

function zombiebot_create(%client, %d) {
if($stopthezombies || ($gdelbots && !%d)) return;
$gdelbots = 0;
%player= AIPlayer::SpawnPlayer();
%player.vowner = $zombieclient;
%player.vclient = $zombieclient;
%player.setTransform(pickSpawnPoint(6));
%player.setSkinName(%client.colorSkin);
%player.setMoveSpeed(0.5);
zombiebot_prefs(%player);
//Search for the word tuna to change the weapon (this was originally part of a larger AI project that got sidelined)
%player.image = zombieimage;
%player.mountImage(%player.image, 0);
zombieBot_getInitTarget(%bot);
%player.setMoveSpeed(0.5 + (0.5 * getRandom(1))); 
%player.clearAim();
zombieBotTick(%player, %client);
messageAll('MsgClientJoin', "", "The zombies", $zombieClient, 0, $zombieClient.score, true, false, false, false);
}




function zombiebot_hasLOS(%bot,%obj) {
//This tells how much of you a bot can see
%bot.footLOS = 0;
%bot.chestLOS = 0;
%bot.eyeLOS = 0;
if(!isObject(%obj))
  return 0;
 if (%obj == containerRayCast(%bot.geteyetransform(), %obj.gettransform(),
  $TypeMasks::InteriorObjectType |  $TypeMasks::TerrainObjectType |
  $TypeMasks::ForceFieldObjectType | $TypeMasks::VehicleObjectType | $TypeMasks::ShapeBaseObjectType & ~$TypeMasks::ItemObjectType,
  %bot))
%bot.footLOS = 1;

 if (%obj == containerRayCast(%bot.geteyetransform(), %obj.getWorldBoxCenter(),
  $TypeMasks::InteriorObjectType |  $TypeMasks::TerrainObjectType |
  $TypeMasks::ForceFieldObjectType | $TypeMasks::VehicleObjectType | $TypeMasks::ShapeBaseObjectType & ~$TypeMasks::ItemObjectType,
  %bot))
%bot.chestLOS = 1;

 if (%obj == containerRayCast(%bot.geteyetransform(), %obj.geteyetransform(),
  $TypeMasks::InteriorObjectType |  $TypeMasks::TerrainObjectType |
  $TypeMasks::ForceFieldObjectType | $TypeMasks::VehicleObjectType | $TypeMasks::ShapeBaseObjectType & ~$TypeMasks::ItemObjectType,
  %bot))
%bot.eyeLOS = 1;

if(%obj.isCloaked()) %n -= 1;
if(%obj.isFaded) %n -= 2;
if(%obj.getShapeName() $= "") %n -= 1;
return %bot.eyeLOS + %bot.chestLOS + %bot.footLOS + %n;
}

function zombiebot_prefs(%bot) {
%random = getRandom(2);
switch(%random) {
//It's so redundant so I can add more later
  case 0: 
setZomBagPrefs(%bot,"Zombie",5,-1,-1,-1,-1,8,11,8,0,Zombie,zombieone,1);
  case 1: 
setZomBagPrefs(%bot,"Zombie",9,-1,-1,-1,-1,8,11,8,0,Zombie,zombiethree,0);
  case 2: 
setZomBagPrefs(%bot,"Zombie",7,-1,-1,-1,-1,8,11,8,0,Zombie,zombiethree,5);
}
}



function setzombagprefs(%bot, %name, %skin, %headCode, %visorCode, %backCode, %leftHandCode, %headCodeColor, %visorCodeColor, %backCodeColor, %leftHandCodeColor, %chestdecalcode, %facedecalcode, %bodytype)
{
if(!isobject(%bot)) return;
	%bot.colorSkin	= $legoColor[%skin];
	%bot.headCode	= $headCode[%headCode];
	%bot.visorCode	= $visorCode[%visorCode];
	%bot.backCode	= $backCode[%backCode];
	%bot.leftHandCode    = $leftHandCode[%leftHandCode];
	%bot.chestCode       = chestShowImage;
	%bot.faceCode        = faceplateShowImage;
    %bot.bodytype = %bodytype;
	%bot.headCodeColor	= addTaggedString($legoColor[%headCodeColor]);
	%bot.visorCodeColor	= addTaggedString($legoColor[%visorCodeColor]);
	%bot.backCodeColor	= addTaggedString($legoColor[%backCodeColor]);
	%bot.leftHandCodeColor = addTaggedString($legoColor[%leftHandCodeColor]);
	%bot.chestdecalcode		= addTaggedString(%chestdecalcode);
	%bot.facedecalcode   	= addTaggedString(%facedecalcode);
			%bot.unMountImage($headSlot);
			%bot.unMountImage($visorSlot);
			%bot.unMountImage($backSlot);
			%bot.unMountImage($leftHandSlot);
			%bot.unMountImage($chestSlot);
			%bot.unMountImage($faceSlot);
			%bot.unMountImage(7);
			%bot.setSkinName(%bot.colorSkin);
			%bot.mountImage(%bot.headCode, $headSlot, 1, %bot.headCodeColor);
			%bot.mountImage(%bot.visorCode, $visorSlot, 1, %bot.visorCodeColor);
			%bot.mountImage(%bot.backCode, $backSlot, 1, %bot.backCodeColor);
            %bot.mountImage(%bot.leftHandCode, $leftHandSlot, 1, %bot.leftHandCodeColor);
			%bot.mountImage(%bot.chestCode, $chestSlot, 1, %bot.chestdecalcode);
			%bot.mountImage(%bot.faceCode, $faceSlot, 1, %bot.facedecalcode);
    %bot.setShapeName(emostring(%bot.name));
}




function zombiebot_getnewtarget(%bot) {
if($zombiedebug) echo(%bot SPC "is getting a new target");
  initContainerRadiusSearch(%bot.getposition(),500,$TypeMasks::PlayerObjectType);
  while (%obj = containerSearchNext()) {
	%invalid = 0;
	if(%obj != %bot && $zombiedebug) echo("Found " @ %obj);
	if(%bot.getmountedimage(0) == nametoid(zombieimage) && %obj.getmountedimage(0) == nametoid(zombieimage)) %invalid = 1; //can't attack zombies
	if(!%bot.team && %obj.getclassname() $= "AIPlayer") %invalid = 2;
	if(%obj.team && %obj.getclassname() $= "AIPlayer" && %obj.team == %bot.team) %invalid = 3;
	if(zombiebot_hasLOS(%bot,%obj) <= 0) %invalid = 4;
//echo("Error code:" SPC %invalid);
    if (!%invalid && %bot.target !$= "Start" && zombiebot_hasLOS(%bot,%obj)) break;
  }
if($zombiedebug) echo("Final target: " @ %obj);
if (!%obj) {
if(%bot.target $= "Wander") return;
%bot.target = "StartWander"; return; }
%bot.target = %obj;
return;
}



function zombiebot_getinittarget(%bot) {
if($zombiedebug) echo(%bot SPC "is getting init target");
  initContainerRadiusSearch(%bot.getposition(),1000,$TypeMasks::PlayerObjectType);
  while (%obj = containerSearchNext()) {
	%invalid = 0;
	if(%obj != %bot && $zombiedebug) echo("Found " @ %obj);
	if(%bot.getmountedimage(0) == nametoid(zombieimage) && %obj.getmountedimage(0) == nametoid(zombieimage)) %invalid = 1; //can't attack zombies.
	if(!%bot.team && %obj.getclassname() $= "AIPlayer") %invalid = 2; //If it's FFA, can't attack bots, period.
	if(%obj.team && %obj.getclassname() $= "AIPlayer" && %obj.team == %bot.team) %invalid = 3; //If they are both on the same team, then no attacking.
												   //I hope that team bots wont attack clone bots.
	if(!zombiebot_hasLOS(%bot,%obj) <= 0) %invalid = 5; //Bob isn't psychic, he needs to see the target
//echo("Error code:" SPC %invalid);
    if (!%invalid && %bot.target !$= "Start" && zombiebot_hasLOS(%bot,%obj)) break;
  }
if($zombiedebug) echo("Final target: " @ %obj);
if (!%obj) {
if(%bot.target $= "Wander") return;
%bot.target = "StartWander"; return; }
%bot.target = %obj;
return;
}

function zombiebot_getinittarget(%bot) {
if($zombiedebug) echo(%bot SPC "is getting init target");
  initContainerRadiusSearch(%bot.getposition(),400,$TypeMasks::PlayerObjectType);
  while (%obj = containerSearchNext()) {
    if (%obj.getmountedimage(0) != nametoid(zombieimage)) break;}
if (!%obj) {%bot.target = "StartWander"; return; }
%bot.target = %obj;
return;
}



//Oh no!  Bob the pedestrian bot was attacked by a zombie!  Now he is...
//BOB THE ZOMBIE BOT

function zombiebottick(%bot) {
if($stopthezombies) return;
if ($gdelbots){%bot.delete();return;}
if (%bot.target $= "StartWander") %bot.target = "Wander";
if($zombiedebug) echo(%bot SPC "is bot, " @ %bot.target @ " is the target");
if (%bot.target $= "Dead") {%bot.delete(); return;}


if (!isobject(%bot)) {schedule(5000,0,zombiebot_create,$zombieclient);return;}
if (%bot.getState() $= "Dead") {schedule(10000,0,zombiebot_create,$zombieclient);return;}
   //Okay, Bob is alive, lets make sure he stays that way
%bot.tick = schedule(250,0,zombiebottick,%bot,%client);
%needaction = 1;


//First, make sure Bob's target is valid: non-zombie, LOS, and within 150 units.

if(%bot.target !$= "Wander") {
  if(%bot.getmountedimage(0) == nametoid(zombieimage) && %bot.target.getmountedimage(0) == nametoid(zombieimage)) { if($zombiedebug) echo(%bot SPC "has zombie target"); zombiebot_getnewtarget(%bot);}
    
      //Make sure Bob can see the target

	 if(zombiebot_hasLOS(%bot,%obj) <= 0) {if($zombiedebug) echo(%bot SPC "cannot see target");  zombiebot_getnewtarget(%bot); }

      //Make sure the target is within a reasonable distance
%distance = vectorlen(vectorsub(%bot.getposition(),%bot.target.getposition()));
	if(%distance >= 150) zombiebot_getnewtarget(%bot);
}

if(%needaction && %bot.target $= "Wander") {
if($zombiedebug) echo(%bot SPC "has been wandering");
//Bob's been wandering
%needaction = 0;
  zombiebot_getnewtarget(%bot);
  if(%bot.target $= "StartWander") {
	%bot.target = "Wander";
	//Oh well, Bob will just have to keep wandering
  }
}
if(%needaction && !isobject(%bot.target) && %bot.target !$= "Wander") {
if($zombiedebug) echo(%bot SPC "cant find the target and is not wandering");
//Bob killed someone, so let's see if there's anyone he can eat
%needaction = 0;
  zombiebot_getnewtarget(%bot);
  if(%bot.target $= "StartWander") {
	//Oh well, Bob will just have to start wandering around
  }
}

//if(VectorLen(%bot.getVelocity()) < 0.01) { echo(%bot SPC "is not moving"); %bot.target = "StartWander";}

%bot.setimagetrigger(0,0);
if(%bot.target !$= "Wander" && %bot.target !$= "StartWander") {
if($zombiedebug) echo("MOVING TEH BOT");
if(isobject(%bot.target)) {
  %bot.setmovedestination(%bot.target.getposition());
if(%bot.eyelos && %distance >= 5)  %bot.setaimlocation(vectoradd(%bot.target.geteyetransform(),"0 0 -0.5")); 
else %bot.setaimobject(%bot.target); }
  %bot.setimagetrigger(0,1);
}
if(%bot.target $= "Wander") {
//This means Bob was already wandering
}
if(%bot.target $= "StartWander") {
//This means that Bob has started wandering in this script
%bot.setmovedestination(%bot.getposition());
%bot.clearaim();
}
}

//This should probably be split and moved to Armor::damage in tbm/server/scripts/player.cs eventually
function AIPlayer::onDeath(%this, %sourceObject, %sourceClient, %damageType, %location) {
%this.playDeathAnimation();
%aiclient = %this.vclient;
if(isObject(%aiclient)) {
  %sourceClient.incScore(1);
  if(%damageType $= "UpImpact")
    %damageType = $DeathMessages::UpImpact[getRandom($DeathMessages::UpImpactCount)];
  else if(%damageType $= "SideImpact")
    %damageType = $DeathMessages::SideImpact[getRandom($DeathMessages::SideImpactCount)];
  else if(%damageType $= "DownImpact")
    %damageType = $DeathMessages::DownImpact[getRandom($DeathMessages::DownImpactCount)];
  else if (%damageType $= "Lava" && $Server::MissionName !$= "DM-Frostbite")
    %damageType = $DeathMessages::Fire[getRandom($DeathMessages::FireCount)];
  if(strStr(getTaggedString(%damageType), "%1") != -1)
    messageAll('MsgClientKilled', %damagetype, %aiClient.name, %sourceClient.name);
}
}

//RIP GameConnection::onDeath overwrite