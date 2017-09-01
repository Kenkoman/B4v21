////////////////////////////////////////////////////////
///////////////////////////////////////////////////////
/////Ok this is a pretty hacky method but I think it's pretty good.
/////there are alot of files edited for this little guy they are server/scripts/servercmd.cs client/scripts/faceprintselect.cs
/////server/scripts/a_briks.cs server/scripts/wrench.cs client/scripts/client.cs server/scripts/hammer.cs
//////////////////
/////im really horrible with explaining how my code works so i'll try and keep this simple
/////on second thought i give up haha.....sorry...
////////////////////////////////////////////////////////


///////////////////////////////////////////////////////
/////////2x2/////////
//////////////////////////////
function staticplate2x2tilePrints::onWake(%this)
{
	commandtoserver('varme',tile2x2);
	BrickPrintlist.clear();
	%i = 0;
	for(%file = findFirstFile("*/shapes/prints/tile2x2/*.2x2.print.png");
		%file !$= ""; %file = findNextFile("*/shapes/prints/*.2x2.print.png"))  
	if (strStr(%file, "CVS/") == -1 && strStr(%file, "common/") == -1)
		Brickprintlist.addRow(%i++, getdecalName(%file) @ "\t" @ %file );
	BrickPrintlist.sort(0);
	BrickPrintlist.setSelectedRow(0);
	BrickPrintlist.scrollVisible(0);

}
//----------------------------------------
function Brickprintlist::onSelect(%this, %row)
{
	if(%row $= "")
	{
		return;
	}
	%image = Brickprintlist.getRowTextById(%row);
	%image2 = getwords(%image,1,2);
	%image3 = getwords(%image,1,2,3);
	%hell = getword(%image,0);
	commandtoserver('printvarme',%hell);
	commandtoserver('brickprint');
}
function getDecalName( %missionFile ) 
{
      return fileBase(fileBase(%missionFile)); 
}
///////////////////////////////////////////////////////
/////////1x2/////////
//////////////////////////////

function staticplate1x2tilePrints::onWake(%this)
{
	commandtoserver('varme',tile1x2);
	BrickPrintlist.clear();
	%i = 0;
	for(%file = findFirstFile("*/shapes/prints/tile1x2/*.1x2.print.png");
		%file !$= ""; %file = findNextFile("*/shapes/prints/*.1x2.print.png"))  
	if (strStr(%file, "CVS/") == -1 && strStr(%file, "common/") == -1)
		Brickprintlist.addRow(%i++, getdecalName(%file) @ "\t" @ %file );
	BrickPrintlist.sort(0);
	BrickPrintlist.setSelectedRow(0);
	BrickPrintlist.scrollVisible(0);

}

///////////////////////////////////////////////////////
/////////2x2round/////////
//////////////////////////////

function statictile2x2roundPrints::onWake(%this)
{
	commandtoserver('varme',tile2x2round);
	BrickPrintlist.clear();
	%i = 0;
	for(%file = findFirstFile("*/shapes/prints/tile2x2round/*.2x2round.print.png");
		%file !$= ""; %file = findNextFile("*/shapes/prints/*.2x2round.print.png"))  
	if (strStr(%file, "CVS/") == -1 && strStr(%file, "common/") == -1)
		Brickprintlist.addRow(%i++, getdecalName(%file) @ "\t" @ %file );
	BrickPrintlist.sort(0);
	BrickPrintlist.setSelectedRow(0);
	BrickPrintlist.scrollVisible(0);
}

///////////////////////////////////////////////////////
/////////1x1/////////
//////////////////////////////
function statictile1x1Prints::onWake(%this)
{
	commandtoserver('varme',tile1x1);
	BrickPrintlist.clear();
	%i = 0;
	for(%file = findFirstFile("*/shapes/prints/tile1x1/*.1x1.print.png");
		%file !$= ""; %file = findNextFile("*/shapes/prints/tile1x1/*.1x1.print.png"))  
	if (strStr(%file, "CVS/") == -1 && strStr(%file, "common/") == -1)
	Brickprintlist.addRow(%i++, getdecalName(%file) @ "\t" @ %file );
	BrickPrintlist.sort(0);
	BrickPrintlist.setSelectedRow(0);
	BrickPrintlist.scrollVisible(0);
}
///////////////////////////////////////////////////////
/////////2x1slope/////////
//////////////////////////////
function staticslope2x1Prints::onWake(%this)
{
	commandtoserver('varme',slope2x1);
	BrickPrintlist.clear();
	%i = 0;
	for(%file = findFirstFile("*/shapes/prints/slope2x1/*.2x1slope.print.png");
		%file !$= ""; %file = findNextFile("*/shapes/prints/slope2x1/*.2x1slope.print.png"))  
	if (strStr(%file, "CVS/") == -1 && strStr(%file, "common/") == -1)
		Brickprintlist.addRow(%i++, getdecalName(%file) @ "\t" @ %file );
	BrickPrintlist.sort(0);
	BrickPrintlist.setSelectedRow(0);
	BrickPrintlist.scrollVisible(0);
}

///////////////////////////////////////////////////////
/////////2x2slope/////////
//////////////////////////////
function staticslope2x2Prints::onWake(%this)
{
	commandtoserver('varme',slope2x2);
	BrickPrintlist.clear();
	%i = 0;
	for(%file = findFirstFile("*/shapes/prints/slope2x2/*.2x2slope.print.png");
		%file !$= ""; %file = findNextFile("*/shapes/prints/slope2x2/*.2x2slope.print.png"))  
	if (strStr(%file, "CVS/") == -1 && strStr(%file, "common/") == -1)
		Brickprintlist.addRow(%i++, getdecalName(%file) @ "\t" @ %file );
	BrickPrintlist.sort(0);
	BrickPrintlist.setSelectedRow(0);
	BrickPrintlist.scrollVisible(0);
}
///////////////////////////////////////////////////////
/////////2x3slope/////////
//////////////////////////////
function staticslope2x3Prints::onWake(%this)
{
	commandtoserver('varme',slope2x3);
	BrickPrintlist.clear();
	%i = 0;
	for(%file = findFirstFile("*/shapes/prints/slope2x3/*.2x3slope.print.png");
		%file !$= ""; %file = findNextFile("*/shapes/prints/slope2x3/*.2x3slope.print.png"))  
	if (strStr(%file, "CVS/") == -1 && strStr(%file, "common/") == -1)
		Brickprintlist.addRow(%i++, getdecalName(%file) @ "\t" @ %file );
	BrickPrintlist.sort(0);
	BrickPrintlist.setSelectedRow(0);
	BrickPrintlist.scrollVisible(0);
}

///////////////////////////////////////////////////////
/////////2x4slope/////////
//////////////////////////////
function staticslope2x4Prints::onWake(%this)
{
	commandtoserver('varme',slope2x4);
	BrickPrintlist.clear();
	%i = 0;
	for(%file = findFirstFile("*/shapes/prints/slope2x4/*.2x4slope.print.png");
		%file !$= ""; %file = findNextFile("*/shapes/prints/slope2x4/*.2x4slope.print.png"))  
	if (strStr(%file, "CVS/") == -1 && strStr(%file, "common/") == -1)
		Brickprintlist.addRow(%i++, getdecalName(%file) @ "\t" @ %file );
	BrickPrintlist.sort(0);
	BrickPrintlist.setSelectedRow(0);
	BrickPrintlist.scrollVisible(0);
}

///////////////////////////////////////////////////////
/////////flag1/////////
//////////////////////////////
function staticflag1Prints::onWake(%this)
{
	commandtoserver('varme',flag1);
	BrickPrintlist.clear();
	%i = 0;
	for(%file = findFirstFile("*/shapes/prints/flag/*.flag.print.png");
		%file !$= ""; %file = findNextFile("*/shapes/prints/flag/*.flag.print.png"))  
	if (strStr(%file, "CVS/") == -1 && strStr(%file, "common/") == -1)
		Brickprintlist.addRow(%i++, getdecalName(%file) @ "\t" @ %file );
	BrickPrintlist.sort(0);
	BrickPrintlist.setSelectedRow(0);
	BrickPrintlist.scrollVisible(0);
}

///////////////////////////////////////////////////////
/////////1x2brick/////////
//////////////////////////////
function staticbrick1x2Prints::onWake(%this)
{
	commandtoserver('varme',brick1x2);
	BrickPrintlist.clear();
	%i = 0;
	for(%file = findFirstFile("*/shapes/prints/brick1x2/*.1x2brick.print.png");
		%file !$= ""; %file = findNextFile("*/shapes/prints/brick1x2/*.1x2brick.print.png"))  
	if (strStr(%file, "CVS/") == -1 && strStr(%file, "common/") == -1)
		Brickprintlist.addRow(%i++, getdecalName(%file) @ "\t" @ %file );
	BrickPrintlist.sort(0);
	BrickPrintlist.setSelectedRow(0);
	BrickPrintlist.scrollVisible(0);
}

///////////////////////////////////////////////////////
/////////1x2x5brick/////////
//////////////////////////////
function staticbrick1x2x5Prints::onWake(%this)
{
	commandtoserver('varme',brick1x2x5);
	BrickPrintlist.clear();
	%i = 0;
	for(%file = findFirstFile("*/shapes/prints/brick1x2x5/*.1x2x5brick.print.png");
		%file !$= ""; %file = findNextFile("*/shapes/prints/brick1x2x5/*.1x2x5brick.print.png"))  
	if (strStr(%file, "CVS/") == -1 && strStr(%file, "common/") == -1)
		Brickprintlist.addRow(%i++, getdecalName(%file) @ "\t" @ %file );
	BrickPrintlist.sort(0);
	BrickPrintlist.setSelectedRow(0);
	BrickPrintlist.scrollVisible(0);
}

///////////////////////////////////////////////////////
/////////1x6x5brick/////////
//////////////////////////////
function staticbrick1x6x5Prints::onWake(%this)
{
	commandtoserver('varme',brick1x6x5);
	BrickPrintlist.clear();
	%i = 0;
	for(%file = findFirstFile("*/shapes/prints/brick1x6x5/*.1x6x5brick.print.png");
		%file !$= ""; %file = findNextFile("*/shapes/prints/brick1x6x5/*.1x6x5brick.print.png"))  
	if (strStr(%file, "CVS/") == -1 && strStr(%file, "common/") == -1)
		Brickprintlist.addRow(%i++, getdecalName(%file) @ "\t" @ %file );
	BrickPrintlist.sort(0);
	BrickPrintlist.setSelectedRow(0);
	BrickPrintlist.scrollVisible(0);
}
///////////////////////////////////////////////////////
/////////1x6brick/////////
//////////////////////////////
function staticbrick1x6Prints::onWake(%this)
{
	commandtoserver('varme',brick1x6);
	BrickPrintlist.clear();
	%i = 0;
	for(%file = findFirstFile("*/shapes/prints/brick1x6/*.1x6brick.print.png");
		%file !$= ""; %file = findNextFile("*/shapes/prints/brick1x6/*.1x6brick.print.png"))  
	if (strStr(%file, "CVS/") == -1 && strStr(%file, "common/") == -1)
		Brickprintlist.addRow(%i++, getdecalName(%file) @ "\t" @ %file );
	BrickPrintlist.sort(0);
	BrickPrintlist.setSelectedRow(0);
	BrickPrintlist.scrollVisible(0);
}
///////////////////////////////////////////////////////
/////////1x4brick/////////
//////////////////////////////
function staticbrick1x4Prints::onWake(%this)
{
	commandtoserver('varme',brick1x4);
	BrickPrintlist.clear();
	%i = 0;
	for(%file = findFirstFile("*/shapes/prints/brick1x4/*.1x4brick.print.png");
		%file !$= ""; %file = findNextFile("*/shapes/prints/brick1x4/*.1x4brick.print.png"))  
	if (strStr(%file, "CVS/") == -1 && strStr(%file, "common/") == -1)
		Brickprintlist.addRow(%i++, getdecalName(%file) @ "\t" @ %file );
	BrickPrintlist.sort(0);
	BrickPrintlist.setSelectedRow(0);
	BrickPrintlist.scrollVisible(0);
}

///////////////////////////////////////////////////////
/////////1x8brick/////////
//////////////////////////////
function staticbrick1x8Prints::onWake(%this)
{
	commandtoserver('varme',brick1x8);
	BrickPrintlist.clear();
	%i = 0;
	for(%file = findFirstFile("*/shapes/prints/brick1x8/*.1x8brick.print.png");
		%file !$= ""; %file = findNextFile("*/shapes/prints/brick1x8/*.1x8brick.print.png"))  
	if (strStr(%file, "CVS/") == -1 && strStr(%file, "common/") == -1)
		Brickprintlist.addRow(%i++, getdecalName(%file) @ "\t" @ %file );
	BrickPrintlist.sort(0);
	BrickPrintlist.setSelectedRow(0);
	BrickPrintlist.scrollVisible(0);
}

///////////////////////////////////////////////////////
/////////1x3brick/////////
//////////////////////////////
function staticbrick1x3Prints::onWake(%this)
{
	commandtoserver('varme',brick1x3);
	BrickPrintlist.clear();
	%i = 0;
	for(%file = findFirstFile("*/shapes/prints/brick1x3/*.1x3brick.print.png");
		%file !$= ""; %file = findNextFile("*/shapes/prints/brick1x3/*.1x3brick.print.png"))  
	if (strStr(%file, "CVS/") == -1 && strStr(%file, "common/") == -1)
		Brickprintlist.addRow(%i++, getdecalName(%file) @ "\t" @ %file );
	BrickPrintlist.sort(0);
	BrickPrintlist.setSelectedRow(0);
	BrickPrintlist.scrollVisible(0);
}

///////////////////////////////////////////////////////
/////////1x1brick/////////
//////////////////////////////
function staticbrick1x1Prints::onWake(%this)
{
	commandtoserver('varme',brick1x1);
	BrickPrintlist.clear();
	%i = 0;
	for(%file = findFirstFile("*/shapes/prints/brick1x1/*.1x1brick.print.png");
		%file !$= ""; %file = findNextFile("*/shapes/prints/brick1x1/*.1x1brick.print.png"))  
	if (strStr(%file, "CVS/") == -1 && strStr(%file, "common/") == -1)
		Brickprintlist.addRow(%i++, getdecalName(%file) @ "\t" @ %file );
	BrickPrintlist.sort(0);
	BrickPrintlist.setSelectedRow(0);
	BrickPrintlist.scrollVisible(0);
}



















///////////////////////////////////////////////////////
///////////////////////////////////////////////////////
function createprintgui(%guiname)
{
   $holyshiz = %guiname;

   //--- OBJECT WRITE BEGIN ---
new GuiControl(%guiname) {
   profile = "GuiDefaultProfile";
   horizSizing = "right";
   vertSizing = "bottom";
   position = "0 0";
   extent = "640 480";
   minExtent = "8 2";
   visible = "1";
   helpTag = "0";

   new GuiWindowCtrl() {
      profile = "GuiWindowProfile";
      horizSizing = "right";
      vertSizing = "bottom";
      position = "304 38";
      extent = "236 443";
      minExtent = "8 2";
      visible = "1";
      helpTag = "0";
      maxLength = "255";
      resizeWidth = "0";
      resizeHeight = "0";
      canMove = "1";
      canClose = "1";
      canMinimize = "1";
      canMaximize = "1";
      minSize = "50 50";
      closeCommand = "canvas.popdialog($holyshiz);$holyshiz.delete();";

      new GuiScrollCtrl() {
         profile = "GuiScrollProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "12 29";
         extent = "213 373";
         minExtent = "8 2";
         visible = "1";
         helpTag = "0";
         willFirstRespond = "1";
         hScrollBar = "alwaysOn";
         vScrollBar = "alwaysOn";
         constantThumbHeight = "0";
         childMargin = "0 0";

         new GuiTextListCtrl(BrickPrintlist) {
            profile = "GuiTextListProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "1 1";
            extent = "194 720";
            minExtent = "8 2";
            visible = "1";
            helpTag = "0";
            enumerate = "0";
            resizeCell = "1";
            columns = "0";
            fitParentWidth = "1";
            clipColumnText = "0";
         };
      };
      new GuiTextCtrl() {
         profile = "GuiTextProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "15 8";
         extent = "137 18";
         minExtent = "8 2";
         visible = "1";
         helpTag = "0";
         text = "Print Selector:";
         maxLength = "255";
      };
      new GuiButtonCtrl() {
         profile = "GuiButtonProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "20 413";
         extent = "101 25";
         minExtent = "8 2";
         visible = "1";
         command = "appBP();canvas.popDialog($holyshiz);$holyshiz.delete();";
         helpTag = "0";
         text = "Apply Print";
         groupNum = "-1";
         buttonType = "PushButton";
      };
      new GuiButtonCtrl() {
         profile = "GuiButtonProfile";
         horizSizing = "right";
         vertSizing = "bottom";
         position = "128 412";
         extent = "101 25";
         minExtent = "8 2";
         visible = "1";
         command = "commandtoserver(\'brickprintdelete\');";
         helpTag = "0";
         text = "Delete Print";
         groupNum = "-1";
         buttonType = "PushButton";
      };
   };
};
//--- OBJECT WRITE END ---
}

function closeguigg(%guiname)
{
   canvas.popDialog(%guiname);
   
   //Schedule(1000,0,%guiname.delete());
}