//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Override base controls
GuiButtonProfile.soundButtonOver = "AudioButtonOver";

//-----------------------------------------------------------------------------
// Chat Hud profiles


new GuiControlProfile ("ChatHudMessageProfile")
{
   fontType = "Arial";
   fontSize = 16;
   //fontColor = "44 172 181";      // default color (death msgs, scoring, inventory)
   //fontColors[1] = "4 235 105";   // client join/drop, tournament mode
   //fontColors[2] = "219 200 128"; // gameplay, admin/voting, pack/deployable
   //fontColors[3] = "77 253 95";   // team chat, spam protection message, client tasks
   //fontColors[4] = "40 231 240";  // global chat
   //fontColors[5] = "200 200 50 200";  // used in single player game

   fontColor = "165 165 165";      // default color (death msgs, scoring, inventory)
   fontColors[1] = "224 255 176";   // client join/drop, tournament mode
   fontColors[2] = "224 255 176"; // gameplay, admin/voting, pack/deployable
   fontColors[3] = "255 255 255";   // white team chat, spam protection message, client tasks
   fontColors[4] = "248 204 0";  // yellow global chat
   fontColors[5] = "200 200 50 200";  // used in single player game

   // WARNING! Colors 6-9 are reserved for name coloring 
   autoSizeWidth = true;
   autoSizeHeight = true;
};

new GuiControlProfile ("ChatHudScrollProfile")
{
   opaque = false;
   border = true;
   borderColor = "0 0 0 0";
   bitmap = "common/ui/darkScroll";
   hasBitmapArray = true;
};


//-----------------------------------------------------------------------------
// Common Hud profiles

new GuiControlProfile ("HudScrollProfile")
{
   opaque = false;
   border = true;
   borderColor = "0 255 0";
   bitmap = "common/ui/darkScroll";
   hasBitmapArray = true;
};

new GuiControlProfile ("HudTextProfile")
{
   opaque = false;
   fillColor = "128 128 128";
   fontColor = "248 204 0";
   border = true;
   borderColor = "0 255 0";
};


//-----------------------------------------------------------------------------
// Center and bottom print

new GuiControlProfile ("CenterPrintProfile")
{
   opaque = false;
   fillColor = "128 128 128";
   fontColor = "248 204 0";
   border = true;
   borderColor = "0 255 0";
};

new GuiControlProfile ("CenterPrintTextProfile")
{
   opaque = false;
   fontType = "Arial";
   fontSize = 12;
   fontColor = "248 204 0";
};


//inventory display profiles
new GuiControlProfile ("InventoryTextProfile")
{
   opaque = false;
   fillColor = "128 128 128";
   fontColor = "255 255 255";
   border = false;
   borderColor = "0 255 0";
   justify = "Center";
};

new GuiControlProfile ("ClockProfile")
{
	bitmap = "~/client/ui/GUIbrickSide.png";
	bitmapBase = "~/client/ui/GUIbrickSide.png";
};

