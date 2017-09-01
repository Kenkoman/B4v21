//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Override base controls
GuiButtonProfile.soundButtonOver = "AudioButtonOver";

//-----------------------------------------------------------------------------
// Chat Hud profiles

new GuiControlProfile (GuiButtonProfile) 
{ 
   opaque = true; 
   border = false; 

   fontColor = "0 0 0"; 
   fontColorHL = "200 50 50";
   fillColor = "223 223 223";
   fixedExtent = true; 
   justify = "center"; 
     canKeyFocus = false; 

}; 
new GuiControlProfile (GuiDefaultProfile)
{
   tab = false;
   canKeyFocus = false;
   hasBitmapArray = false;
   mouseOverSelected = false;

   // fill color
   opaque = false;
   fillColor = "223 223 223";
   fillColorHL = "190 210 215";
   fillColorNA = "220 220 220";

   // border color
   border = false;
   borderColor   = "109 114 134";
   borderColorHL = "128 128 128";
   borderColorNA = "64 64 64";

   // font
   fontType = "Arial";
   fontSize = 14;

   fontColor = "0 0 0";
   fontColorHL = "200 50 50";
   fontColorNA = "255 255 255";
   fontColorSEL= "255 255 255";

   // bitmap information
   bitmap = "./rtbWindow";
   bitmapBase = "";
   textOffset = "0 0";

   // used by guiTextControl
   modal = true;
   justify = "left";
   autoSizeWidth = false;
   autoSizeHeight = false;
   returnTab = false;
   numbersOnly = false;
   cursorColor = "0 0 0 255";

   // sounds
   soundButtonDown = "";
   soundButtonOver = "";
};

new GuiControlProfile( GuiInputCtrlProfile )
{
   tab = true;
	canKeyFocus = true;
};

new GuiControlProfile(GuiDialogProfile);


new GuiControlProfile (GuiSolidDefaultProfile)
{
   opaque = true;
   border = true;
};

new GuiControlProfile (GuiWindowProfile)
{
   opaque = true;
   border = 2;
   fillColor = ($platform $= "macos") ? "211 211 211" : "223 223 223";
   fillColorHL = ($platform $= "macos") ? "190 255 255" : "64 150 150";
   fillColorNA = ($platform $= "macos") ? "255 255 255" : "150 150 150";

   fontColor = "0 0 0";
   fontColorHL = "200 50 50";
   fontColorNA = "255 255 255";
   fontColorSEL= "255 255 255";

   text = "GuiWindowCtrl test";
   bitmap = "./rtbWindow";
   textOffset = "9 9";
   hasBitmapArray = true;
   justify = ($platform $= "macos") ? "center" : "left";
};

new GuiControlProfile (RTBGuiWindowProfile)
{
   opaque = true;
   border = 2;
   fillColor = ($platform $= "macos") ? "211 211 211" : "223 223 223";
   fillColorHL = ($platform $= "macos") ? "190 255 255" : "64 150 150";
   fillColorNA = ($platform $= "macos") ? "255 255 255" : "150 150 150";
   fontColor = ($platform $= "macos") ? "255 255 255" : "255 255 255";
   fontColorHL = ($platform $= "macos") ? "200 200 200" : "0 0 0";
   text = "GuiWindowCtrl test";
   bitmap = "./rtbWindow";
   textOffset = ($platform $= "macos") ? "5 5" : "6 6";
   hasBitmapArray = true;
   justify = ($platform $= "macos") ? "center" : "left";
};

new GuiControlProfile (GuiToolWindowProfile)
{
   opaque = true;
   border = 2;
   fillColor = "223 223 223";
   fillColorHL = "64 150 150";
   fillColorNA = "150 150 150";
   fontColor = "255 255 255";
   fontColorHL = "0 0 0";
   bitmap = "./rtbWindow";
   hasBitmapArray = true;
   textOffset = "6 6";
};

new GuiControlProfile (GuiContentProfile)
{
   opaque = true;
   fillColor = "255 255 255";
};

new GuiControlProfile("GuiModelessDialogProfile")
{
   modal = false;
};

new GuiControlProfile (GuiBorderButtonProfile)
{
   fontColorHL = "0 0 0";
};

new GuiControlProfile (GuiMenuBarProfile)
{
   opaque = true;
   fillColorHL = "0 0 96";
   border = 4;
   fontColor = "0 0 0";
   fontColorHL = "255 255 255";
   fontColorNA = "128 128 128";
   fixedExtent = true;
   justify = "center";
   canKeyFocus = false;
   mouseOverSelected = true;
   bitmap = "./rtbMenu";
   hasBitmapArray = true;
};

new GuiControlProfile (GuiButtonSmProfile : GuiButtonProfile)
{
   fontSize = 14;
   bitmap = "./rtbButton";
   hasBitmapArray = true;
};

new GuiControlProfile (GuiRadioProfile)
{
   fontSize = 14;
   fillColor = "232 232 232";
   fontColorHL = "200 50 50";
   fixedExtent = true;
   bitmap = "./rtbRadio";
   hasBitmapArray = true;
};

new GuiControlProfile (GuiScrollProfile)
{
   opaque = true;
   fillColor = "255 255 255";
   border = 3;
   borderThickness = 1;
   borderColor = "0 0 0";
   bitmap = "./rtbScroll";
   hasBitmapArray = true;
};

new GuiControlProfile (GuiSliderProfile);

new GuiControlProfile (GuiTextProfile)
{
   fontColor = "0 0 0";
   fontColorLink = "255 96 96";
   fontColorLinkHL = "0 0 255";
   autoSizeWidth = true;
   autoSizeHeight = true;
   bitmap = "./rtbWindow";
   hasBitmapArray = true;
};

new GuiControlProfile (GuiMediumTextProfile : GuiTextProfile)
{
   fontSize = 24;
};

new GuiControlProfile (GuiBigTextProfile : GuiTextProfile)
{
   fontSize = 36;
};

new GuiControlProfile (GuiCenterTextProfile : GuiTextProfile)
{
   justify = "center";
};

new GuiControlProfile (GuiTextEditProfile)
{
   opaque = true;
   fillColor = "255 255 255";
   fillColorHL = "128 128 128";
   border = 3;
   borderThickness = 2;
   borderColor = "0 0 0";
   fontColor = "0 0 0";
   fontColorHL = "255 255 255";
   fontColorNA = "128 128 128";
   textOffset = "0 2";
   autoSizeWidth = false;
   autoSizeHeight = true;
   tab = true;
   canKeyFocus = true;
};

new GuiControlProfile (GuiControlListPopupProfile)
{
   opaque = true;
   fillColor = "255 255 255";
   fillColorHL = "128 128 128";
   border = true;
   borderColor = "0 0 0";
   fontColor = "0 0 0";
   fontColorHL = "255 255 255";
   fontColorNA = "128 128 128";
   textOffset = "0 2";
   autoSizeWidth = false;
   autoSizeHeight = true;
   tab = true;
   canKeyFocus = true;
   bitmap = "./rtbScroll";
   hasBitmapArray = true;
};

new GuiControlProfile (GuiTextArrayProfile : GuiTextProfile)
{
   fontColorHL = "200 50 50";
   fillColorHL = "185 190 200";
};

new GuiControlProfile (GuiTextListProfile : GuiTextProfile) ;

new GuiControlProfile (GuiTreeViewProfile)
{
   fontSize = 13;  // dhc - trying a better fit...
   fontColor = "0 0 0";
   fontColorHL = "64 150 150";
};

new GuiControlProfile (GuiCheckBoxProfile)
{
   opaque = false;
   fillColor = "232 232 232";
   border = false;
   borderColor = "0 0 0";
   fontSize = 14;
   fontColor = "0 0 0";
   fontColorHL = "200 50 50";
   fixedExtent = true;
   justify = "left";
   bitmap = "./rtbCheck";
   hasBitmapArray = true;

};

new GuiControlProfile (GuiPopUpMenuProfile)
{
   opaque = true;
   mouseOverSelected = true;

   border = 4;
   borderThickness = 2;
   borderColor = "0 0 0";
   fontSize = 14;
   fontColor = "0 0 0";
   fontColorHL = "200 50 50";
   fontColorSEL = "200 50 50";
   fixedExtent = true;
   justify = "center";
   bitmap = "./rtbScroll";
   hasBitmapArray = true;
};

new GuiControlProfile ("LoadTextProfile")
{
   fontColor = "66 219 234";
   autoSizeWidth = true;
   autoSizeHeight = true;
};


new GuiControlProfile ("GuiMLTextProfile")
{
   fontColorLink = "255 96 96";
   fontColorLinkHL = "0 0 255";
};

new GuiControlProfile (GuiMLTextEditProfile) 
{ 
   fontColorLink = "255 96 96"; 
   fontColorLinkHL = "0 0 255"; 

   fillColor = "255 255 255"; 
   fillColorHL = "128 128 128"; 

   fontColor = "0 0 0"; 
   fontColorHL = "255 255 255"; 
   fontColorNA = "128 128 128"; 

   autoSizeWidth = true; 
   autoSizeHeight = true; 
   tab = true; 
   canKeyFocus = true; 
}; 

new GuiControlProfile ("GuiProgressProfile")
{
   opaque = false;
   fillColor = "255 0 0 100";
   border = 1;
   borderThickness = 1;
   borderColor   = "0 0 0";
};

new GuiControlProfile ("GuiProgressTextProfile")
{
   fontColor = "0 0 0";
   justify = "center";
};

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

   fontColor = "255 255 255";      // WHITE - Default Colour
   fontColors[1] = "248 204 0";   // ORANGE - Client Messaging. Join/Drop.
   fontColors[2] = "54 239 33"; // PUKE GREEN - General Messages
   fontColors[3] = "255 0 0";   // RED - Server Messages/Warnings
   fontColors[4] = "165 165 165";  // GREY - Free Colour.
   fontColors[5] = "19 136 253";  // BLUE - Game Mode Colours.

   // WARNING! Colors 6-9 are reserved for name coloring 
   autoSizeWidth = true;
   autoSizeHeight = true;
};

new GuiControlProfile ("ChatHudScrollProfile")
{
   opaque = false;
   border = true;
   borderColor = "0 0 0 0";
   bitmap = "./rtbScroll";
   hasBitmapArray = true;
};


//-----------------------------------------------------------------------------
// Common Hud profiles

new GuiControlProfile ("HudScrollProfile")
{
   opaque = false;
   border = true;
   borderColor = "0 255 0";
   bitmap = "./rtbScroll";
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
 
if(!isObject(GuiBorderButtonProfile)) new GuiControlProfile (GuiBorderButtonProfile) 
{ 
   fontColorHL = "0 0 0"; 
   bitmap = "./rtbButton";
	hasBitmapArray = true;
};