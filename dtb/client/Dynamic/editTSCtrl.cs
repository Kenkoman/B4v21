function setFOV(%fov) {
if(%fov $= "")
  %fov = 90;
if(!%fov)
  %fov = 1;
if(%fov < 0)
  %fov = 0.1;
playGUI.forceFOV = $cameraFOV = %fov;
}

if(!isObject(RenderJobs)) {
  new ScriptObject(RenderJobs);
  if(!isObject(MissionGroup))
    new SimSet(MissionGroup);
  MissionGroup.add(RenderJobs);
}

//addRenderJob(2, "255 0 0", "255 0 0 100", "32 104 100", "32 88 100", $targets[$target] TAB "e");

function scriptObject::onEditorRender(%this, %editor, %selected, %expanded, %null) {
if(%this.getName() $= "RenderJobs") {
  %this.rainbow += 2;
  if(%this.rainbow > 1530)
    %this.rainbow = 0;
  for(%i = -1 * %this.clientRenderJobs; %i <= %this.serverRenderJobs; %i++) {
    if(%i == 0)
      continue;
    for(%j = 1; %j <= 6; %j++) { //Object tracking
      if(getFieldCount(%arg[%j] = %this.arg[%j, %i]) > 1) {
        %obj = getField(%arg[%j], 0);
        if(!isObject(%obj)) {
          %arg[%j] = "0 0 0";
          continue;
        }
        switch$(getWord(%arg[%j], 1)) {
          case "m":
            %arg[%j] = %obj.getMuzzlePoint(getWord(%arg[%j], 2));
          case "s":
            %arg[%j] = %obj.getSlotTransform(getWord(%arg[%j], 2));
          case "e":
            %arg[%j] = vectorAdd(%obj.getEyeTransform(), vectorScale(%obj.getEyeVector(), getWord(%arg[%j], 2)));
          case "ah":
            %arg[%j] = vectorAdd(%obj.getEyeTransform(), "0 0" SPC getWord(%arg[%j], 2));
          case "c":
            %arg[%j] = %obj.getWorldBoxCenter();
          case "p":
            %arg[%j] = %obj.getPosition();
          case "ev":
            %arg[%j] = %obj.getEyeVector();
          case "eval":
            eval("%arg[%j] = " @ removeWord(removeField(%arg[%j], 0), 0) @ ";");
          case "obj":
            %arg[%j] = %obj;
          default:
            %arg[%j] = %obj.getPosition();
        }
      }
    }
    %editor.consoleFrameColor = getRenderJobColor(%this.frameColor[%i]);
    %editor.consoleFillColor = getRenderJobColor(%this.fillColor[%i]);
    switch (%this.shape[%i]) {
      case 1:
        %editor.renderLine(%arg[1], %arg[2], %arg[3]);			//Start, end, width
      case 2:
        %editor.renderTriangle(%arg[1], %arg[2], %arg[3]);		//Point A, point B, point C
      case 3:
        %editor.renderCircle(%arg[1], %arg[2], %arg[3]);		//Center, normal, radius
      case 4:
        %editor.renderSphere(%arg[1], %arg[2], %arg[3]);		//Center, radius, smoothness
      case 5:
        %editor.renderPrism(%arg[1], %arg[2], %arg[3]);			//Corner, opposite corner, width
      case 6:
        %editor.renderAxes(%arg[1], %arg[2], %arg[3]);			//Object, width, length
      case 7:
        if(%arg[5] != 0)
          %arg[3] = (%this.arg[3, %i] += %arg[5] + (%this.arg[3, %i] > 360 ? -360 : (%this.arg[3, %i] < 0 ? 360 : 0))); //ought to make this based on getRealTime since right now speed is based on either framerate or cycles per second the process gets
        %editor.renderText(%arg[1], %arg[2], %arg[3], %arg[4], %arg[6]);		//Text, center pos, rotation, size, spin, background color
      default:
        %editor.render(%this.shape[%i], %arg[1], %arg[2], %arg[3], %arg[4], %arg[5], %arg[6]);	//Default render thing
    }
  }
}
}

function EditTSCtrl::render(%this, %shape, %arg1, %arg2, %arg3, %arg4, %arg5, %arg6) {
}

function EditTSCtrl::renderPrism(%this, %arg1, %arg2, %arg3) {
for(%a = 1; %a <= 2; %a++) {
  for(%b = 0; %b <= 2; %b++) {
    %p[%a, %b] = getWord(%arg[%a], %b);
  }
}
for(%a = 1; %a <= 2; %a++) {
  %this.renderLine(%p1_0 SPC %p1_1 SPC %p[%a, 2], %p2_0 SPC %p1_1 SPC %p[%a, 2], %arg3);
  %this.renderLine(%p1_0 SPC %p1_1 SPC %p[%a, 2], %p1_0 SPC %p2_1 SPC %p[%a, 2], %arg3);
  %this.renderLine(%p2_0 SPC %p1_1 SPC %p[%a, 2], %p2_0 SPC %p2_1 SPC %p[%a, 2], %arg3);
  %this.renderLine(%p1_0 SPC %p2_1 SPC %p[%a, 2], %p2_0 SPC %p2_1 SPC %p[%a, 2], %arg3);
}
%this.renderLine(%p1_0 SPC %p1_1 SPC %p1_2, %p1_0 SPC %p1_1 SPC %p2_2, %arg3);
%this.renderLine(%p1_0 SPC %p2_1 SPC %p1_2, %p1_0 SPC %p2_1 SPC %p2_2, %arg3);
%this.renderLine(%p2_0 SPC %p1_1 SPC %p1_2, %p2_0 SPC %p1_1 SPC %p2_2, %arg3);
%this.renderLine(%p2_0 SPC %p2_1 SPC %p1_2, %p2_0 SPC %p2_1 SPC %p2_2, %arg3);
}

function EditTSCtrl::renderAxes(%this, %obj, %length, %width) {
if(!isObject(%obj))
  return;
%root = %obj.getPosition();
%center = %obj.getWorldBoxCenter();
//welcome to hackville, population 1
%scale = %obj.getScale();
for(%i = 0; %i < 3; %i++) {
  %obj.setScale(setWord(%scale, %i, 2 * getWord(%obj.getScale(), %i)));
  %off[%i] = vectorNormalize(vectorSub(%obj.getWorldBoxCenter(), %center));
}
%obj.setScale(%scale);
//you are now leaving hackville

%this.consoleFrameColor = %this.consoleFillColor = getRenderJobColor(($Pref::Video::CSR::FrameColor["axesx"] $= "" ? "255 0 0" : $Pref::Video::CSR::FrameColor));
%this.renderLine(%root, %pos = vectorAdd(%root, vectorScale(%off[0], %length)), %width);
if($Pref::Video::CSR::AxesLabels)
  %this.renderText("X", vectorAdd(%pos, "0 0 0.3"), "face", 0.15);
%this.consoleFrameColor = %this.consoleFillColor = getRenderJobColor(($Pref::Video::CSR::FrameColor["axesy"] $= "" ? "0 0 255" : $Pref::Video::CSR::FrameColor));
%this.renderLine(%root, %pos = vectorAdd(%root, vectorScale(%off[1], %length)), %width);
if($Pref::Video::CSR::AxesLabels)
  %this.renderText("Y", vectorAdd(%pos, "0 0 0.3"), "face", 0.15);
%this.consoleFrameColor = %this.consoleFillColor = getRenderJobColor(($Pref::Video::CSR::FrameColor["axesz"] $= "" ? "0 255 0" : $Pref::Video::CSR::FrameColor));
%this.renderLine(%root, %pos = vectorAdd(%root, vectorScale(%off[2], %length)), %width);
if($Pref::Video::CSR::AxesLabels)
  %this.renderText("Z", vectorAdd(%pos, "0 0 0.3"), "face", 0.15);
}

//Text, center pos, rotation, spin, size
function EditTSCtrl::renderText(%this, %text, %center, %rot, %size, %backColor) {
if(%rot $= "face")
  %rot = 360 - getWord(vectorToEuler(serverConnection.getControlObject().getForwardVector()), 2);
if(%size $= "")
  %size = "1 1";
if(getWordCount(%size) == 1)
  %size = %size SPC %size;
%x = mCos(%rot * $pi / 180) * getWord(%size, 0);
%y = mSin(%rot * $pi / 180) * getWord(%size, 0);
//Default font is 4 by 7
%lines = buildEditTSTextLines(%text);
%center = vectorSub(%center, vectorScale(%x SPC %y SPC 0, %length = getField(%lines, 0)));
%lines = removeField(%lines, 0);
if(strLen(%backColor) > 1) {
  %border = 0.75;
  %fr = %this.consoleFrameColor;
  %fi = %this.consoleFillColor;
  %this.consoleFrameColor = "0 0 0 0";
  %this.consoleFillColor = getRenderJobColor(%backColor);
  %length = (%length + %border) * 2 - 1;
  %start = vectorAdd(vectorAdd(%center, (-0.02 * mSin(%rot * $pi / 180)) SPC (0.02 * mCos(%rot * $pi / 180)) SPC 0), (-%border * %x) SPC (-%border * %y) SPC (-%border * getWord(%size, 1)));
  %end = (getWord(%start, 0) + (%x * %length)) SPC (getWord(%start, 1) + (%y * %length));
  %this.renderTriangle(%start, %end SPC getWord(%start, 2), %end SPC (getWord(%start, 2) + (%size * 7) + (2 * %border * getWord(%size, 1))));
  %this.renderTriangle(%start, vectorAdd(%start, "0 0" SPC (%size * 7) + (2 * %border * getWord(%size, 1))), %end SPC (getWord(%start, 2) + (%size * 7) + (2 * %border * getWord(%size, 1))));
  %this.consoleFrameColor = %fr;
  %this.consoleFillColor = %fi;
}

for(%i = 0; %i < getFieldCount(%lines); %i++) {
  %line = getField(%lines, %i);
  %lp = vectorAdd(%center, (%x * getWord(%line, 0)) SPC (%y * getWord(%line, 0)) SPC (getWord(%size, 1) * getWord(%line, 1)));
  for(%j = 2; %j < getWordCount(%line); %j += 2) {
    %p = vectorAdd(%center, (%x * getWord(%line, %j)) SPC (%y * getWord(%line, %j)) SPC (getWord(%size, 1) * getWord(%line, %j + 1)));
    %this.renderLine(%lp, %p, 1);
    %lp = %p;
  }
}
}

//Converts a string into 2D vectors
function buildEditTSTextLines(%text) {
for(%i = 0; %i < strLen(%text); %i++)
  %ret = %ret @ buildEditTSLetter(getSubStr(%text, %i, 1), %i) @ "\t";
return (2.5 * %i) TAB %ret;
}

//Converts a letter into 2D vectors
//Package this function to make a new font if you want to keep the current width
function buildEditTSLetter(%l, %i) {
%i *= 5;
switch$(strUpr(%l)) {
  //Much thanks to Vulpes for making the font
  case "A":
    return (0 + %i) @ " 0 " @ (2 + %i) @ " 7 " @ (4 + %i) @ " 0\t" @ (0.857 + %i) @ " 3 " @ (3.143 + %i) @ " 3";
  case "B":
    return (0 + %i) @ " 4 " @ (3 + %i) @ " 4\t" @ (3 + %i) @ " 7 " @ (0 + %i) @ " 7 " @ (0 + %i) @ " 0 " @ (3 + %i) @ " 0 " @ (4 + %i) @ " 1 " @ (4 + %i) @ " 3 " @ (3 + %i) @ " 4 " @ (4 + %i) @ " 5 " @ (4 + %i) @ " 6 " @ (3 + %i) @ " 7";
  case "C":
    return (4 + %i) @ " 1 " @ (3 + %i) @ " 0 " @ (1 + %i) @ " 0 " @ (0 + %i) @ " 1 " @ (0 + %i) @ " 6 " @ (1 + %i) @ " 7 " @ (3 + %i) @ " 7 " @ (4 + %i) @ " 6";
  case "D":
    return (0 + %i) @ " 0 " @ (0 + %i) @ " 7 " @ (2 + %i) @ " 7 " @ (4 + %i) @ " 5 " @ (4 + %i) @ " 2 " @ (2 + %i) @ " 0 " @ (0 + %i) @ " 0";
  case "E":
    return (4 + %i) @ " 7 " @ (0 + %i) @ " 7 " @ (0 + %i) @ " 0 " @ (4 + %i) @ " 0\t" @ (0 + %i) @ " 4 " @ (3 + %i) @ " 4";
  case "F":
    return (0 + %i) @ " 0 " @ (0 + %i) @ " 7 " @ (4 + %i) @ " 7\t" @ (0 + %i) @ " 4 " @ (3 + %i) @ " 4";
  case "G":
    return (3 + %i) @ " 3 " @ (4 + %i) @ " 3 " @ (4 + %i) @ " 1 " @ (3 + %i) @ " 0 " @ (1 + %i) @ " 0 " @ (0 + %i) @ " 1 " @ (0 + %i) @ " 6 " @ (1 + %i) @ " 7 " @ (3 + %i) @ " 7 " @ (4 + %i) @ " 6";
  case "H":
    return (0 + %i) @ " 0 " @ (0 + %i) @ " 7\t" @ (0 + %i) @ " 4 " @ (4 + %i) @ " 4\t" @ (4 + %i) @ " 0 " @ (4 + %i) @ " 7";
  case "I":
    return (2 + %i) @ " 0 " @ (2 + %i) @ " 7";
  case "J":
    return (0 + %i) @ " 2 " @ (0 + %i) @ " 1 " @ (1 + %i) @ " 0 " @ (3 + %i) @ " 0 " @ (4 + %i) @ " 1 " @ (4 + %i) @ " 7";
  case "K":
    return (0 + %i) @ " 0 " @ (0 + %i) @ " 7\t" @ (3 + %i) @ " 7 " @ (0 + %i) @ " 4 " @ (4 + %i) @ " 0";
  case "L":
    return (4 + %i) @ " 0 " @ (0 + %i) @ " 0 " @ (0 + %i) @ " 7";
  case "M":
    return (0 + %i) @ " 0 " @ (0 + %i) @ " 7 " @ (2 + %i) @ " 4 " @ (4 + %i) @ " 7 " @ (4 + %i) @ " 0";
  case "N":
    return (0 + %i) @ " 0 " @ (0 + %i) @ " 7 " @ (4 + %i) @ " 0 " @ (4 + %i) @ " 7";
  case "O":
    return (4 + %i) @ " 1 " @ (3 + %i) @ " 0 " @ (1 + %i) @ " 0 " @ (0 + %i) @ " 1 " @ (0 + %i) @ " 6 " @ (1 + %i) @ " 7 " @ (3 + %i) @ " 7 " @ (4 + %i) @ " 6 " @ (4 + %i) @ " 1";
  case "P":
    return (0 + %i) @ " 0 " @ (0 + %i) @ " 7 " @ (3 + %i) @ " 7 " @ (4 + %i) @ " 6 " @ (4 + %i) @ " 5 " @ (3 + %i) @ " 4 " @ (0 + %i) @ " 4";
  case "Q":
    return (4 + %i) @ " 1 " @ (3 + %i) @ " 0 " @ (1 + %i) @ " 0 " @ (0 + %i) @ " 1 " @ (0 + %i) @ " 6 " @ (1 + %i) @ " 7 " @ (3 + %i) @ " 7 " @ (4 + %i) @ " 6 " @ (4 + %i) @ " 1\t" @ (4 + %i) @ " 0 " @ (2 + %i) @ " 2";
  case "R":
    return (0 + %i) @ " 0 " @ (0 + %i) @ " 7 " @ (3 + %i) @ " 7 " @ (4 + %i) @ " 6 " @ (4 + %i) @ " 5 " @ (3 + %i) @ " 4 " @ (0 + %i) @ " 4 " @ (4 + %i) @ " 0";
  case "S":
    return (0 + %i) @ " 1 " @ (1 + %i) @ " 0 " @ (3 + %i) @ " 0 " @ (4 + %i) @ " 1 " @ (4 + %i) @ " 3 " @ (3 + %i) @ " 4 " @ (1 + %i) @ " 4 " @ (0 + %i) @ " 5 " @ (0 + %i) @ " 6 " @ (1 + %i) @ " 7 " @ (3 + %i) @ " 7 " @ (4 + %i) @ " 6";
  case "T":
    return (0 + %i) @ " 7 " @ (4 + %i) @ " 7\t" @ (2 + %i) @ " 7 " @ (2 + %i) @ " 0";
  case "U":
    return (0 + %i) @ " 7 " @ (0 + %i) @ " 1 " @ (1 + %i) @ " 0 " @ (3 + %i) @ " 0 " @ (4 + %i) @ " 1 " @ (4 + %i) @ " 7";
  case "V":
    return (0 + %i) @ " 7 " @ (2 + %i) @ " 0 " @ (4 + %i) @ " 7";
  case "W":
    return (0 + %i) @ " 7 " @ (0 + %i) @ " 0 " @ (2 + %i) @ " 3 " @ (4 + %i) @ " 0 " @ (4 + %i) @ " 7";
  case "X":
    return (0 + %i) @ " 0 " @ (4 + %i) @ " 7\t" @ (0 + %i) @ " 7 " @ (4 + %i) @ " 0";
  case "Y":
    return (0 + %i) @ " 7 " @ (2 + %i) @ " 3 " @ (4 + %i) @ " 7\t" @ (2 + %i) @ " 3 " @ (2 + %i) @ " 0";
  case "Z":
    return (0 + %i) @ " 7 " @ (4 + %i) @ " 7 " @ (0 + %i) @ " 0 " @ (4 + %i) @ " 0";
  case "0":
    return (0 + %i) @ " 1 " @ (0 + %i) @ " 6 " @ (1 + %i) @ " 7 " @ (3 + %i) @ " 7 " @ (4 + %i) @ " 6 " @ (4 + %i) @ " 1 " @ (3 + %i) @ " 0 " @ (1 + %i) @ " 0 " @ (0 + %i) @ " 1 " @ (4 + %i) @ " 6";
  case "1":
    return (0 + %i) @ " 5 " @ (2 + %i) @ " 7 " @ (2 + %i) @ " 0\t" @ (0 + %i) @ " 0 " @ (4 + %i) @ " 0";
  case "2":
    return (0 + %i) @ " 6 " @ (1 + %i) @ " 7 " @ (3 + %i) @ " 7 " @ (4 + %i) @ " 6 " @ (4 + %i) @ " 5 " @ (0 + %i) @ " 1 " @ (0 + %i) @ " 0 " @ (4 + %i) @ " 0";
  case "3":
    return (0 + %i) @ " 6 " @ (1 + %i) @ " 7 " @ (3 + %i) @ " 7 " @ (4 + %i) @ " 6 " @ (4 + %i) @ " 5 " @ (3 + %i) @ " 4\t" @ (1 + %i) @ " 4 " @ (3 + %i) @ " 4 " @ (4 + %i) @ " 3 " @ (4 + %i) @ " 1 " @ (3 + %i) @ " 0 " @ (1 + %i) @ " 0 " @ (0 + %i) @ " 1";
  case "4":
    return (3 + %i) @ " 0 " @ (3 + %i) @ " 7 " @ (0 + %i) @ " 4 " @ (4 + %i) @ " 4";
  case "5":
    return (0 + %i) @ " 1 " @ (1 + %i) @ " 0 " @ (3 + %i) @ " 0 " @ (4 + %i) @ " 1 " @ (4 + %i) @ " 3 " @ (3 + %i) @ " 4 " @ (1 + %i) @ " 4 " @ (0 + %i) @ " 5 " @ (0 + %i) @ " 7 " @ (4 + %i) @ " 7";
  case "6":
    return (4 + %i) @ " 6 " @ (3 + %i) @ " 7 " @ (1 + %i) @ " 7 " @ (0 + %i) @ " 6 " @ (0 + %i) @ " 1 " @ (1 + %i) @ " 0 " @ (3 + %i) @ " 0 " @ (4 + %i) @ " 1 " @ (4 + %i) @ " 3 " @ (3 + %i) @ " 4 " @ (1 + %i) @ " 4 " @ (0 + %i) @ " 3";
  case "7":
    return (0 + %i) @ " 0 " @ (4 + %i) @ " 7 " @ (0 + %i) @ " 7";
  case "8":
    return (1 + %i) @ " 4 " @ (0 + %i) @ " 3 " @ (0 + %i) @ " 1 " @ (1 + %i) @ " 0 " @ (3 + %i) @ " 0 " @ (4 + %i) @ " 1 " @ (4 + %i) @ " 3 " @ (3 + %i) @ " 4 " @ (1 + %i) @ " 4 " @ (0 + %i) @ " 5 " @ (0 + %i) @ " 6 " @ (1 + %i) @ " 7 " @ (3 + %i) @ " 7 " @ (4 + %i) @ " 6 " @ (4 + %i) @ " 5 " @ (3 + %i) @ " 4";
  case "9":
    return (0 + %i) @ " 1 " @ (1 + %i) @ " 0 " @ (3 + %i) @ " 0 " @ (4 + %i) @ " 1 " @ (4 + %i) @ " 6 " @ (3 + %i) @ " 7 " @ (1 + %i) @ " 7 " @ (0 + %i) @ " 6 " @ (0 + %i) @ " 4 " @ (1 + %i) @ " 3 " @ (3 + %i) @ " 3 " @ (4 + %i) @ " 4";
  case "$":
    return (0 + %i) @ " 1 " @ (1 + %i) @ " 0 " @ (3 + %i) @ " 0 " @ (4 + %i) @ " 1 " @ (4 + %i) @ " 3 " @ (3 + %i) @ " 4 " @ (1 + %i) @ " 4 " @ (0 + %i) @ " 5 " @ (0 + %i) @ " 6 " @ (1 + %i) @ " 7 " @ (3 + %i) @ " 7 " @ (4 + %i) @ " 6\t" @ (2 + %i) @ " 0 " @ (2 + %i) @ " 7";
  case "#":
    return (1 + %i) @ " 0 " @ (1 + %i) @ " 7\t" @ (3 + %i) @ " 0 " @ (3 + %i) @ " 7\t" @ (0 + %i) @ " 2 " @ (4 + %i) @ " 2\t" @ (0 + %i) @ " 5 " @ (4 + %i) @ " 5";
  case "%":
    return (0 + %i) @ " 0 " @ (4 + %i) @ " 7\t" @ (2 + %i) @ " 0.666 " @ (2 + %i) @ " 1.333 " @ (2.666 + %i) @ " 2 " @ (3.333 + %i) @ " 2 " @ (4 + %i) @ " 1.333 " @ (4 + %i) @ " 0.666 " @ (3.333 + %i) @ " 0 " @ (2.666 + %i) @ " 0 " @ (2 + %i) @ " 0.666\t" @ (0 + %i) @ " 5.666 " @ (0 + %i) @ " 6.333 " @ (0.666 + %i) @ " 7 " @ (1.333 + %i) @ " 7 " @ (2 + %i) @ " 6.333 " @ (2 + %i) @ " 5.666 " @ (1.333 + %i) @ " 5 " @ (0.666 + %i) @ " 5 " @ (0 + %i) @ " 5.666";
  case "(":
    return (3 + %i) @ " 0 " @ (2 + %i) @ " 0 " @ (1 + %i) @ " 1 " @ (1 + %i) @ " 6 " @ (2 + %i) @ " 7 " @ (3 + %i) @ " 7";
  case ")":
    return (1 + %i) @ " 0 " @ (2 + %i) @ " 0 " @ (3 + %i) @ " 1 " @ (3 + %i) @ " 6 " @ (2 + %i) @ " 7 " @ (1 + %i) @ " 7";
  case "!":
    return (2 + %i) @ " 0 " @ (2 + %i) @ " 1\t" @ (2 + %i) @ " 2 " @ (2 + %i) @ " 7";
  case ".":
    return (2 + %i) @ " 0 " @ (2 + %i) @ " 1";
  case ",":
    return (1 + %i) @ " 0 " @ (2 + %i) @ " 0.5 " @ (2 + %i) @ " 1";
  case ":":
    return (2 + %i) @ " 1 " @ (2 + %i) @ " 2\t" @ (2 + %i) @ " 5 " @ (2 + %i) @ " 6";
  case "?":
    return (2 + %i) @ " 0 " @ (2 + %i) @ " 1\t" @ (2 + %i) @ " 2 " @ (2 + %i) @ " 3 " @ (4 + %i) @ " 5 " @ (4 + %i) @ " 6 " @ (3 + %i) @ " 7 " @ (1 + %i) @ " 7 " @ (0 + %i) @ " 6";
  default:
    return "";
}
return %ret;
}

function addRenderJob(%shape, %frame, %fill, %arg1, %arg2, %arg3, %arg4, %arg5, %arg6, %id, %type) {
if(!isObject(RenderJobs))
  new ScriptObject(RenderJobs);
if(!isObject(MissionGroup))
  new SimSet(MissionGroup);
MissionGroup.add(RenderJobs);

if(%shape $= "" || strLwr(%shape) $= "help") {
  echo("addRenderJob usage:");
  echo("addRenderJob(%shape, %frame, %fill, %argA, %argB, %argC, %argD, %argE, %argF, %id, %type);");
  echo("%Shape:");
  echo("1: line");
  echo("2: triangle");
  echo("3: circle");
  echo("4: sphere");
  echo("5: prism");
  echo("6: object axes");
  echo("7: text");
  return;
}
if(%id $= "") //If there's an ID specified, alter that job's data; otherwise, add it to the stack as normal.
  %id = -1 * (RenderJobs.clientRenderJobs++); //Client stack, anyways; anything coming from the server will already be assigned an ID
if(%id > 0)
  RenderJobs.serverRenderJobs = RenderJobs.serverRenderJobs > %id ? RenderJobs.serverRenderJobs : %id;
else
  RenderJobs.clientRenderJobs = RenderJobs.clientRenderJobs > mAbs(%id) ? RenderJobs.clientRenderJobs : mAbs(%id);

RenderJobs.shape[%id] = %shape;
RenderJobs.frameColor[%id] = %frame;
RenderJobs.fillColor[%id] = %fill;
for(%i = 1; %i <= 6; %i++)
  RenderJobs.arg[%i, %id] = %arg[%i];
RenderJobs.type[%id] = %type;
return %id;
}

function removeRenderJob(%id) {
if(!%id)
  %id = -1 * RenderJobs.clientRenderJobs;
%i = %id;
if(RenderJobs.clientRenderJobs <= %id && %id < 0) { //Move client things around
  for(%i = %id; %i > (-1 * RenderJobs.clientRenderJobs); %i--) { //This just moves any lower-numbered jobs up a slot so there's no empty space with no job
    RenderJobs.shape[%i] = RenderJobs.shape[%i-1];
    RenderJobs.frameColor[%i] = RenderJobs.frameColor[%i-1];
    RenderJobs.fillColor[%i] = RenderJobs.fillColor[%i-1];
    for(%j = 1; %j <= 6; %j++)
      RenderJobs.arg[%j, %i] = RenderJobs.arg[%j, %i-1];
    RenderJobs.type[%i] = RenderJobs.type[%i-1];
  }
  RenderJobs.clientRenderJobs = RenderJobs.clientRenderJobs-- > 0 ? RenderJobs.clientRenderJobs : 0; //Reduce the client job count but don't go below 0.
}
RenderJobs.shape[%i] = "";
RenderJobs.frameColor[%i] = "";
RenderJobs.fillColor[%i] = "";
for(%j = 1; %j <= 6; %j++)
  RenderJobs.arg[%j, %i] = "";
RenderJobs.type[%i] = "";
}

function clientCmdAddRenderJob(%shape, %frame, %fill, %arg1, %arg2, %arg3, %arg4, %arg5, %arg6, %id, %type) {
if(!%id = mAbs(%id)) {
  echo("Hey, the server is trying to add a new render job without passing an ID.");
  return;
}
for(%c = 1; %c <= 6; %c++) {
  if(getFieldCount(%arg[%c]) > 1)                                             //The %arg args are in the format "ghostData" TAB "locationData," where ghostData is the standard five-field ghost data and locationData
    %arg[%c] = getGhost(getFields(%arg[%c], 0, 4)) TAB getField(%arg[%c], 5); //is either a one- or two-word string, the first indicating the type of location (muzzle point, slot transform,
  if(strStr(%arg[%c], "eval") != -1) {                                        //eye transform) and the second indicating the mountpoint or any argument that might be needed by the first.
    echo("The server is trying to pass code to you!");
    echo(getField(%arg[%c], 1));
    return;
  }
}
if(%type !$= "") {
  if($Pref::Video::CSR::FrameColor[%type] !$= "")
    %frame = $Pref::Video::CSR::FrameColor[%type];
  if($Pref::Video::CSR::FillColor[%type] !$= "")
    %fill = $Pref::Video::CSR::FillColor[%type];
}
%i = addRenderJob(%shape, %frame, %fill, %arg1, %arg2, %arg3, %arg4, %arg5, %arg6, %id, %type); //Add/edit the job
}

function clientCmdRemoveRenderJob(%id) {
if(%id = mAbs(%id))
  removeRenderJob(%id);
}

function getRenderJobColor(%color) {
if(%color $= "random")
  return getRandom(0, 255) SPC getRandom(0, 255) SPC getRandom(0, 255);
if(%color $= "rainbow") {
  %n = mFloor(renderJobs.rainbow);
  %r = %n >= 1275 ? 255 : (%n >= 1020 ? %n - 1020 : (%n >= 510 ? 0 : (%n >= 255 ? 510 - %n : 255)));
  %g = %n >= 1020 ? 0 : (%n >= 765 ? 1020 - %n : (%n >= 255 ? 255 : %n));
  %b = %n >= 1275 ? 1530 - %n : (%n >= 765 ? 255 : (%n >= 510 ? %n - 510 : 0));
  return %r SPC %g SPC %b;
}
return %color;
}