//Item
//Static Shape
datablock StaticShapeData(staticprinttile2x2)
{
	shapeFile = "~/data/shapes/prints/tile2x2/print.dts";
};

//print1x2.cs
//Static Shape
datablock StaticShapeData(staticprinttile1x2)
{
	shapeFile = "~/data/shapes/prints/tile1x2/print1x2.dts";
};

//print1x1
datablock StaticShapeData(staticprinttile1x1)
{
	shapeFile = "~/data/shapes/prints/tile1x1/print1x1.dts";
};
//print2x2round
datablock StaticShapeData(staticprinttile2x2round)
{
	shapeFile = "~/data/shapes/prints/tile2x2round/print2x2round.dts";
};

//print2x1slope
datablock StaticShapeData(staticprinttile2x1)
{
	shapeFile = "~/data/shapes/prints/slope2x1/print2x1slope.dts";
};

//print2x2slope
datablock StaticShapeData(staticprintslope2x2)
{
	shapeFile = "~/data/shapes/prints/slope2x2/print2x2slope.dts";
};

//print2x3slope
datablock StaticShapeData(staticprintslope2x3)
{
	shapeFile = "~/data/shapes/prints/slope2x3/print2x3slope.dts";
};

//print2x4slope
datablock StaticShapeData(staticprintslope2x4)
{
	shapeFile = "~/data/shapes/prints/slope2x4/print2x4slope.dts";
};

//printflag
datablock StaticShapeData(staticprintflag1)
{
	shapeFile = "~/data/shapes/prints/flag/printflag.dts";
};
//print1x1brick
datablock StaticShapeData(staticprintbrick1x1)
{
	shapeFile = "~/data/shapes/prints/brick1x1/print1x1brick.dts";
};
//print1x2brick
datablock StaticShapeData(staticprintbrick1x2)
{
	shapeFile = "~/data/shapes/prints/brick1x2/print1x2brick.dts";
};
//print1x3brick
datablock StaticShapeData(staticprintbrick1x3)
{
	shapeFile = "~/data/shapes/prints/brick1x3/print1x3brick.dts";
};
//print1x4brick
datablock StaticShapeData(staticprintbrick1x4)
{
	shapeFile = "~/data/shapes/prints/brick1x4/print1x4brick.dts";
};
//print1x6brick
datablock StaticShapeData(staticprintbrick1x6)
{
	shapeFile = "~/data/shapes/prints/brick1x6/print1x6brick.dts";
};
//print1x8brick
datablock StaticShapeData(staticprintbrick1x8)
{
	shapeFile = "~/data/shapes/prints/brick1x8/print1x8brick.dts";
};


//print1x2x5brick
datablock StaticShapeData(staticprintbrick1x2x5)
{
	shapeFile = "~/data/shapes/prints/brick1x2x5/print1x2x5brick.dts";
};

//print1x6x5brick
datablock StaticShapeData(staticprintbrick1x6x5)
{
	shapeFile = "~/data/shapes/prints/brick1x6x5/print1x6x5brick.dts";
};












////////////////////////////////













////////////////////printcode

function servercmdbrickprint(%client)
{
   echo("\c2Client.Printer:  " @ %client.printer);
   %lastbrick = %client.selectedbrick;
   %col = %lastbrick;
if(%lastbrick)
{
%isTrusted = checkSafe(%col,%client);		
if(%col.Owner == %client || %isTrusted ||%client.isAdmin || %client.isSuperAdmin)
{
   if(%client.printer $= "")
   {
	return;
   }
   if(%lastbrick.mounteddecal)
   {
	%lastbrick.printer = "";
	%lastbrick.mounteddecal.delete();
	%lastbrick.mounteddecal = "";
	%lastbrick.isprinted = "";
	%lastbrick.printname = "";
   }
     	%client.decaler = new StaticShape() 
     	{
	 	dataBlock = "staticprint"@%client.printer;
     	};
     
   %lastbrick.printer = %client.printer;
   %lastbrick.mounteddecal = %client.decaler;
   %lastbrick.isprinted = true;
   %lastbrick.mountobject(%client.decaler,0);
   %lastbrick.printname = %client.printnamevar;
   %client.decaler.setskinname(%client.printnamevar);
   %lastbrick.mounteddecal.setScale(%lastbrick.getScale());
}
}
}

function servercmdbrickprintdelete(%client)
{
   %lastbrick = %client.selectedbrick;
   if(%lastbrick)
   {
      %lastbrick.isprinted = 0;
      %lastbrick.mounteddecal.delete();
      %lastbrick.mounteddecal = "";
   }
}
function servercmdbrickprintpaint(%client)
{
   %lastbrick = %client.selectedbrick;
   %lastbrick.mounteddecal.setskinname(%client.printnamevar);
}
function servercmdvarme(%client,%var)
{
   %client.printer = %var;
}




///////////////////////vehiclespawner


function servercmdspawnplane(%client)
{
   if(%client.isAdmin || %client.isSuperAdmin)
   {
   %pos = %client.player.getposition();
      %obj =new FlyingVehicle() 
      {
            dataBlock = legoplane;
            position = %pos;
            mountable = true;
   };
   return(%obj);
   %obj.mountable = true;
}
   
}

function servercmdspawnbike(%client)
{
   if(%client.isAdmin || %client.isSuperAdmin)
   {
   %pos = %client.player.getposition();
      %obj =new WheeledVehicle() 
      {
            dataBlock = legobike;
            position = %pos;
            mountable = true;
   };
   return(%obj);
   %obj.mountable = true;
}
   
}

/////////////////////////
/////////////////////////
function brickprintper(%brick)
{
   %client = %brick;
   //%lastbrick = %client.selectedbrick;
 	  %client.decaler = new StaticShape() 
	{
      					position = "0 0 0";
      					rotation = "1 0 0 0";
      					scale = "1 1 1";
					dataBlock = "staticprint"@%client.printer;
 	};
 				   			%client.mounteddecal = %client.decaler;
   %brick.mountobject(%client.decaler,0);
   //echo(%brick.printname);
   %client.decaler.setskinname(%brick.printname);
   //echo(%lastbrick);echo(%client.decaler);echo(hewge);
}

function servercmdprintvarme(%client,%hell)
{
   %client.printnamevar = %hell;
   
}

