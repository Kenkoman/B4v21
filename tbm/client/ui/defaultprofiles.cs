//------------------------------------------------------------------------------
// TBM Default Gui Profiles
// For the custom Gui colors/borders
//------------------------------------------------------------------------------

$wintot = 0;
$wintot2 = 0;
function dynamicguiload() {
%file_path = "tbm/client/ui/window_images/*.png";
for(%file_name = findFirstFile(%file_path); %file_name !$= ""; %file_name = findNextFile(%file_path)) {
			%file_name = strreplace(%file_name,"tbm/client/ui/window_images/","");
            %file_loc = strreplace(%file_name,".png","");
            %file_name = strreplace(%file_name,"_"," ");
            %file_name = strreplace(%file_name,".png","");
            //echo(%file_loc SPC %file_name SPC $wintot+1);
      $windowskin[$wintot++] = %file_loc;
      $windowsn[$wintot2++] = %file_name;
   }
}
dynamicguiload();
new GuiControlProfile (TBM_WindowProfile)
{
   opaque = true;
   border = 2;
   fillColor = "164 164 164";
   fillColorHL = "64 150 150";
   fillColorNA = "150 150 150";
   fontType = "Impact";
   fontSize = 17;
   fontColor = "255 255 255";
   fontColorHL = "0 0 0";
   text = "TBM_Window";
   bitmap = "tbm/client/ui/window_images/Default";
   textOffset = "6 3";
   hasBitmapArray = true;
   justify = "left";
};

function GUIControlProfile::backup(%this) {
if(%this.backup != -1 && !isObject(%this.backup))
  eval("%this.backup = new GuiControlProfile(" @ %this.getName() @ "_Backup :" SPC %this.getName() @ ") { backup = -1; };");
}

function GUIControlProfile::restore(%this) {
if(isObject(%obj = %this.backup)) {
  %this.tab = %obj.tab;
  %this.canKeyFocus = %obj.canKeyFocus;
  %this.mouseOverSelected = %obj.mouseOverSelected;
  %this.modal = %obj.modal;
  %this.opaque = %obj.opaque;
  %this.fillColor = %obj.fillColor;
  %this.fillColorHL = %obj.fillColorHL;
  %this.fillColorNA = %obj.fillColorNA;
  %this.border = %obj.border;
  %this.borderThickness = %obj.borderThickness;
  %this.borderColor = %obj.borderColor;
  %this.borderColorHL = %obj.borderColorHL;
  %this.borderColorNA = %obj.borderColorNA;
  %this.fontType = %obj.fontType;
  %this.fontSize = %obj.fontSize;
  %this.fontColors[0] = %obj.fontColors[0];
  %this.fontColors[1] = %obj.fontColors[1];
  %this.fontColors[2] = %obj.fontColors[2];
  %this.fontColors[3] = %obj.fontColors[3];
  %this.fontColors[4] = %obj.fontColors[4];
  %this.fontColors[5] = %obj.fontColors[5];
  %this.fontColors[6] = %obj.fontColors[6];
  %this.fontColors[7] = %obj.fontColors[7];
  %this.fontColors[8] = %obj.fontColors[8];
  %this.fontColors[9] = %obj.fontColors[9];
  %this.fontColor = %obj.fontColor;
  %this.fontColorHL = %obj.fontColorHL;
  %this.fontColorNA = %obj.fontColorNA;
  %this.fontColorSEL = %obj.fontColorSEL;
  %this.fontColorLink = %obj.fontColorLink;
  %this.fontColorLinkHL = %obj.fontColorLinkHL;
  %this.justify = %obj.justify;
  %this.textOffset = %obj.textOffset;
  %this.autoSizeWidth = %obj.autoSizeWidth;
  %this.autoSizeHeight = %obj.autoSizeHeight;
  %this.returnTab = %obj.returnTab;
  %this.numbersOnly = %obj.numbersOnly;
  %this.cursorColor = %obj.cursorColor;
  %this.bitmap = %obj.bitmap;
  %this.text =  %obj.text; 
  %this.hasBitmapArray =  %obj.hasBitmapArray; 
}
}

function GuiCursor::backup(%this) {
if(%this.backup != -1 && !isObject(%this.backup))
  eval("%this.backup = new GuiCursor(" @ %this.getName() @ "_Backup :" SPC %this.getName() @ ") { backup = -1; };");
}

function GuiCursor::restore(%this) {
if(isObject(%obj = %this.backup)) {
  %this.bitmapName = %obj.bitmapName;
  %this.hotSpot = %obj.hotSpot;
}
}
//Those two should be the only things in GuiDataGroup and we really don't even need to back up GuiCursors

function getguistyle(%skin) {
if(%skin !$= "")
  $Pref::GUI::Skin = %skin;

%count = GuiDataGroup.getCount();
for(%i = 0; %i < %count; %i++) {
  GuiDataGroup.getObject(%i).restore();
  GuiDataGroup.getObject(%i).backup();
}

if(isFile("tbm/client/ui/window_images/customprofiles/"@$windowskin[$pref::Gui::Skin]@".cs"))
  exec("tbm/client/ui/window_images/customprofiles/"@$windowskin[$pref::Gui::Skin]@".cs");
else {
  if(isFile("tbm/client/ui/window_images/"@$windowskin[$pref::Gui::Skin]@".png"))
    TBM_WindowProfile.bitmap = "tbm/client/ui/window_images/"@$windowskin[$pref::Gui::Skin];
  else {
    error("Error: file" SPC "tbm/client/ui/window_images/"@$windowskin[$pref::Gui::Skin]@".png not found.");
    TBM_WindowProfile.bitmap = "tbm/client/ui/window_images/Default";
  }
}

for(%n = 0; %n < Canvas.getCount(); %n++)
  %array[%j++] = Canvas.getObject(%n);
for(%n = %j; %n >= 1; %n--) {
  canvas.remove(%array[%n]);
}
for(%n = %j; %n >= 1; %n--) {
  canvas.add(%array[%n]);
  if(%array[%n].onWakeOnSkin) //In case there's any processing that needs to be done (like MainChatHUD)
    %array[%n].onWake();
  canvas.bringToFront(%array[%n]);
}
}

function resetWindowID() {
new GuiControlProfile (TBM_WindowProfile) {
  opaque = true;
  border = 2;
  fillColor = "164 164 164";
  fillColorHL = "64 150 150";
  fillColorNA = "150 150 150";
  fontType = "Impact";
  fontSize = 17;
  fontColor = "255 255 255";
  fontColorHL = "0 0 0";
  text = "TBM_Window";
  bitmap = "tbm/client/ui/window_images/"@$windowskin[$pref::Gui::Skin];
  textOffset = "6 3";
  hasBitmapArray = true;
  justify = "left";
};
for(%n = 0; %n <= 1; %n++) {
  %group = %n ? GuiGroup : Canvas;
  for(%i = 0; %i < %group.getcount(); %i++) {
    %Temp_Array[%i] = %group.getObject(%i);
    for(%b = 0; %b < %Temp_Array[%i].getCount(); %b++) {
      if(%Temp_Array[%i].getObject(%b).profile $= "TBM_WindowProfile" || %Temp_Array[%i].getObject(%b).profile $= "GuiWindowProfile")
        %Temp_Array2[%c++] = %Temp_Array[%i].getObject(%b);
    }
  }

  for(%y = 1; %y <= %c; %y++) {
    if(%Temp_Array2[%y] > 0)
      if(%Temp_Array2[%y].profile $= "TBM_WindowProfile")
        %Temp_Array2[%y].setProfile(nametoid("TBM_WindowProfile"));
  }
}
}

schedule(400, 0, resetWindowID);
schedule(410, 0, getGUIStyle, $pref::Gui::Skin);

new GuiControlProfile (TBM_ScrollProfile)
{
   opaque = true;
   fillColor = "225 225 225";
   border = 2;
   borderThickness = 2;
   borderColor = "100 100 100";
   bitmap = "./TBM.scroll";
   hasBitmapArray = true;
};
new GuiControlProfile (TBM_SmallScrollProfile)
{
   opaque = true;
   fillColor = "225 225 225";
   border = 2;
   borderThickness = 2;
   borderColor = "100 100 100";
   bitmap = "./TBM.smallscroll";
   hasBitmapArray = true;
};
new GuiControlProfile (TBM_ButtonProfile)
{
   opaque = true;
   border = true;
   fillColor = "164 164 164";
   fontColor = "20 20 20";
   fontColorHL = "80 80 100";
   fixedExtent = true;
   justify = "center";
   canKeyFocus = false;
   soundButtonOver = "AudioButtonOver";
};
new GuiControlProfile (TBM_CheckBoxProfile)
{
   opaque = false;
   fillColor = "232 232 232";
   border = false;
   borderColor = "0 0 0";
   fontSize = 14;
   fontColor = "0 0 0";
   fontColorHL = "32 100 100";
   fixedExtent = true;
   justify = "left";
   bitmap = "./TBM.check";
   hasBitmapArray = true;
};
new GuiControlProfile (TBM_PopUpMenuProfile)
{
   opaque = true;
   fillColor = "164 164 164";
   mouseOverSelected = true;
   border = true;
   borderThickness = 2;
   borderColor = "0 0 0";
   fontSize = 14;
   fontColor = "20 20 20";
   fontColorHL = "80 80 100";
   fontColorSEL = "32 100 100";
   fixedExtent = true;
   justify = "center";
   bitmap = "./TBM.scroll";
   hasBitmapArray = true;
};
new GuiControlProfile (TBM_ProgressProfile)
{
   opaque = false;
   fillColor = "120 120 120 100";
   border = true;
   borderColor   = "84 84 84";
};
new GuiControlProfile (TBM_ProgressTextProfile)
{
   fontColor = "0 0 0";
   justify = "center";
};
new GuiControlProfile (TBM_RadioProfile)
{
   fontSize = 14;
   fillColor = "232 232 232";
   fontColor = "150 150 150";
   fontColorHL = "200 200 200";
   fixedExtent = true;
   bitmap = "./TBM.radio";
   hasBitmapArray = true;
};
new GuiControlProfile (TBM_TextEditProfile)
{
   opaque = true;
   fillColor = "225 225 225";
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
   bitmap = "tbm/client/ui/window_images/"@$windowskin[$pref::Gui::Skin];
   hasBitmapArray = true;
};
new GuiControlProfile(TBM_MessageVectorProfile)
{
   fontType = "Arial";
   fontSize = 16;

   autoSizeWidth = true;
   autoSizeHeight = true;
   
   fontColor     = "165 165 165";  // Gray(default color) CHECK
   fontColors[1] = "255 0 255"; // *Me messages (purple)
   fontColors[2] = "248 204 0";  // Normal Yellow Text (yellow(duh))
   fontColors[3] = "255 255 255"; // Hilight (white)
   fontColors[4] = "0 150 250";  // Irc (aqua-blue)
   fontColors[5] = "50 240 0";  // Mods (green)
   fontColors[6] = "255 150 0";  // Admins (orangeish)
   fontColors[7] = "224 255 176";  // Notice (leave/join messages) <-- so far unused
   fontColors[8] = "185 0 255";   // PM'ing (purple) <--
   fontColors[9] = "255 0 0";      // ERROR/Supers (red)
};

new GuiControlProfile (DP_TextProfile : GuiTextProfile)
{
   fontColorHL = "32 100 100";
   fontColor = "0 0 0";      // BLACK
   fontColors[1] = "255 255 255";   // WHITE
   justify = "center";
};
new GuiControlProfile (DP_ButtonProfile)
{
   opaque = false;
   border = false;
   fillColor = "0 0 0 0";
   fillColorHL= "255 255 255 20";
   fontColor = "0 0 0";
   fontColorHL = "32 100 100";
   fixedExtent = true;
   mouseoverselected = true;
};

new GuiCursor("MacCursor")
{
   hotSpot = "1 1";
   bitmapName = "./Mac.cursor";
};
new GuiCursor("MacCursorTrans")
{
   hotSpot = "1 1";
   bitmapName = "./Mac_trans.cursor";
};
new GuiCursor("PCCursor")
{
   hotSpot = "1 1";
   bitmapName = "./PC.cursor";
};
new GuiCursor("BlueCursor")
{
   hotSpot = "1 1";
   bitmapName = "./Blue.cursor";
};
//STUPID common code >:(
function cursorOn()
{
   if ( $cursorControlled )
      lockMouse(false);
   Canvas.cursorOn();
   Canvas.setCursor($Cursor[$Pref::Gui::Cursor]);
}

new GuiControlProfile(TBM_TextEmotionsProfile)
{
   fontType = "Wingdings";
   fontSize = 16;

   autoSizeWidth = true;
   autoSizeHeight = true;

   fontColor     = "165 165 165";  // Gray(default color) CHECK
   fontColors[1] = "255 0 255"; // *Me messages (purple)
   fontColors[2] = "248 204 0";  // Normal Yellow Text (yellow(duh))
   fontColors[3] = "255 255 255"; // Hilight (white)
   fontColors[4] = "0 150 250";  // Irc (aqua-blue)
   fontColors[5] = "50 240 0";  // Mods (green)
   fontColors[6] = "255 150 0";  // Admins (orangeish)
   fontColors[7] = "224 255 176";  // Notice (leave/join messages) <-- so far unused
   fontColors[8] = "185 0 255";   // PM'ing (purple) <--
   fontColors[9] = "255 0 0";      // ERROR/Supers (red)
};

function GUIControl::getRealPos(%this) {
while(%this.getGroup() != nameToID(Canvas) && %this.getGroup() != nameToID(GUIGroup)) {
  %x += getWord(%this.position, 0);
  %y += getWord(%this.position, 1);
  %this = %this.getGroup();
}
return %x SPC %y;
}