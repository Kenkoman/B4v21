// ---------------------------------------------------------------------------
// TgeLobby for the Torque Game Engine
// Created by Sean Pollock aka DarkRaven
// email:  sean@darkravenstudios.com
// This file defines the profiles for all the controls in TgeLobby
// ---------------------------------------------------------------------------

new GuiControlProfile( TgeLobby_BevelLoweredProfile )
{
   opaque = false;
   fillColor = "0 0 0 80";
   border = 3;
   borderThickness = 2;
   borderColor = "0 0 0 80";
   bitmap = "./demoScroll";
   hasBitmapArray = true;
};

new GuiControlProfile( TgeLobby_EditProfile )
{
   opaque = true;
   fillColor = "255 255 255 80";
   //fillColorHL = "128 128 128";
   border = 3;
   borderThickness = 2;
   borderColor = "0 0 0 80";
   fontType = "Arial";
   fontSize = 14;
   fontColor = "0 0 0";
   fontColorLink = "255 96 96";
   fontColorLinkHL = "0 0 255";
   fontColorNA = "128 128 128";
   textOffset = "0 2";
   autoSizeWidth = false;
   autoSizeHeight = false;
   tab = true;
   canKeyFocus = true;
};

new GuiControlProfile( TgeLobby_Profile )
{
   //fillColorHL = "128 128 128";
   fontType = "Arial";
   fontSize = 14;
   fontColor = "0 0 0";
   fontColorLink = "255 96 96";
   fontColorLinkHL = "0 0 255";
   fontColorNA = "128 128 128";
   autoSizeWidth = true;
   autoSizeHeight = true;
};

new GuiControlProfile( TgeLobby_MessageVectorProfile )
{
   fontType = "Arial";
   fontSize = 14;
   fontColors[1] = "0 0 0"; // Black - Used for everything else
   fontColors[2] = "102 153 102"; // Green - Used for Notification Messages
   fontColors[3] = "153 51 51"; // Red - Used for Connect and Other Errors
   fontColors[4] = "100 100 0";  // Not used
   fontColors[5] = "133 47 133"; // Purple - Used for Private Messages
   fontColors[6] = "150 50 150"; // Not used
   fontColors[7] = "50 150 150"; // Not used
   fontColors[8] = "100 50 100"; // Not used
   fontColors[9] = "255 100 50"; // Not used
};
