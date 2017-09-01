//-----------------------------------------------------------------------------
// This is the Bandit dudes ai functions
//-----------------------------------------------------------------------------

function Ghost::spawn(%client,%weapon)
{ //GhostsTeamDropPoints
   // Create the demo player object
   %player = new AiPlayer() {
      dataBlock = LightMaleHumanArmor;
      path = "";
   };
   ////get in front of the player
   %spawnplace = %client.player.getposition();
   %spawnx = getword(%spawnplace,0);
   %spawny = getword(%spawnplace,1);
   %spawnz = getword(%spawnplace,2);
   %spawny += 3;
   %spawnz += 3;
   
   %finalspawnpos = (%spawnx SPC %spawny SPC %spawnz);


   %player.setShapeName(Bandit);
   %player.setTransform(%finalspawnpos);
   %player.mountimage(scouthatshowimage,5,false,'brown');
   %player.mountimage(packshowimage,4,false,'brown');
   %player.mountimage(facelegoimage,3,false,'evil');
   %player.mountimage(basedecalimage,6,false,'bandit2');
   %player.setskinname(brown);
   if (%weapon $= 1)
   {
	   %player.mountimage(crossbowimage,0);
   }
      if (%weapon $= 0)
   {
	   %player.mountimage(duallsabreimage,0);
   }
   
   %player.isfollowing = false;
   %player.isai = true;
   //%player.setimagetrigger(0,0);
   schedule(1000,0,"aithink", %player, white);
   return %player;
}

function randommove(%bot)
{
	if (!isobject(%bot))
	{
		return;
	}
	//%bot.isfollowing = false;
	//cancel(%bot.follow2);

	%botlocal = %bot.getposition();
	%botx = getword(%botlocal,0);
	%boty = getword(%botlocal,1);
	%botz = getword(%botlocal,2);
    %randX = getRandom(-20,20);
    %randY = getRandom(-20,20);

	%botnewx = %botx + %randx;
    %botnewy = %boty + %randy;

	%bot.setMoveDestination(%botnewx SPC %botnewy SPC %botz);
	%bot.clearaim();
	%ai.randomloop = schedule(2000, 0, "randommove", %ai);
}

function aithink(%ai)
{
	if (!isobject(%ai))
	{
		return;
	}
	servercmdbanditaicheck(%ai);
	//%ai.setimagetrigger(0,0);
	%ai.thinkingloop = schedule(1000, 0, "aithink", %ai);
	
}
function following(%obj, %this)
{
	if (!isobject(%obj) || !isobject(%this))
	{
		%obj.isfollowing = false;
		//%ai.thinkingloop = schedule(300, 0, "aithink", %ai);
		return;
	}
	%obj.isfollowing = true;
 %obj.setaimobject( %this );
  %playerpos = %obj.getaimobject().getposition();
  %obj.setmovedestination( %playerpos );
  %obj.follow2 = schedule(100, 0, "following", %obj, %this);
}
function servercmdbanditaicheck(%player)
{

   %searchMasks = $TypeMasks::ShapeBaseObjectType;
   %radius = 15;
   %pos = %player.getPosition();
   InitContainerRadiusSearch(%pos, %radius, %searchMasks);
  
   while ((%targetObject = containerSearchNext()) != 0) {
   %dist = containerSearchCurrRadiusDist();
   %target = %targetObject.getTransform();
   %id = %targetObject.getId();
   %name = %targetObject.getName();
   %thingy = %id.getclassName();
   //cancel(%player.runawaybitch);
   //%player.setimagetrigger(0,0);

	if (%thingy $= staticshape)
	{
		return;
	}
	echo(%thingy);
   if (%thingy $= player)
   {
		cancel(%player.randomloop);
		following(%player, %id);
		%player.setimagetrigger(0,1);
   }
   else
	   {
	   cancel(%player.follow2);
	   %player.setimagetrigger(0,0);
	   randommove(%player);
	   }
 }
   
   return;
}

function runaway(%obj, %this)
{
   %obj.runawaycheck1 = 1;
   %runawaycheck2 = %obj.runawaycheck1;
	   if (%runawaycheck2 != 1)
	   {
	   return;
	    }
	%this.runawaycheck3 = 2;
   %runawaycheck4 = %this.runawaycheck3;
	   if (%runawaycheck3 != 2)
	   {
	   return;
	    }
 %obj.setaimobject( %this );
 %targetPos = %this.getPosition();
 %x = getWord(%targetPos, 0);
 %y = getWord(%targetPos, 1);
 %z = getWord(%targetPos, 2);
 %y -= 1000;
 %finalPos = %x SPC %y SPC %z;
  %obj.setmovedestination( %finalpos );
  %obj.runawaybitch = schedule(100, 0, "runaway", %obj, %this);
}
