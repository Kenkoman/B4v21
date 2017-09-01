function createShowcaseServer() {

  if(isObject(serverConnection))
    return;

  $BuildPreviewCurrentMission = "";
  //This used to be a lot more involved at first
  %file = new FileObject();
  %f = new FileObject();
  $BuildPreviewSaves = 0;
  if($Pref::GUI::BuildPreviews::useSaves) {
    %file.openForRead("tbm/tbmzips/gallery.txt");
    while(!%file.isEOF()) {
      %fn = "tbm/tbmzips/" @ %file.readLine() @ ".save";
      %f.openForRead(%fn);
      if(%f.isEOF())
        continue;
      %f.close();
      $BuildPreview[$BuildPreviewSaves++] = %fn;
    }
    %file.close();
  }
  if($Pref::GUI::BuildPreviews::useiGobs) {
    %file.openForRead("tbm/client/iGob/gallery.txt");
    while(!%file.isEOF()) {
      %fn = "tbm/client/iGob/" @ %file.readLine() @ ".save";
      %f.openForRead(%fn);
      if(%f.isEOF())
        continue;
      %f.close();
      $BuildPreview[$BuildPreviewSaves++] = %fn;
    }
    %file.close();
  }
  %file.delete();

  //Break into new function here
  if($forceBuildPreview !$= "") {
    %filename = $forceBuildPreview;
    $forceBuildPreview = "";
  }
  else
    %filename = $BuildPreview[getRandom(1, $BuildPreviewSaves)];
  %mission = getShowcaseServerMission(%filename);
  echo("createShowcaseServer: selected mission" SPC %mission);
  echo("createShowcaseServer: selected build file" SPC %filename);
  if(!isObject(BuildPreviewFileObject))
    new FileObject(BuildPreviewFileObject);
  (%file = BuildPreviewFileObject).openForRead(%fileName);
  while(!%file.isEOF()) {
    %line = %file.readLine();
    if(%line !$= "")
      %pos[%count++] = getEncodedObjectInfo(%line, "position");
  }
  %file.close();
  for(%i = 1; %i <= %count; %i++) {
    %x += getWord(%pos[%i], 0);  //Because we need to save these until after the center is computed, and that can't be done until we've gathered every pos anyways
    %y += getWord(%pos[%i], 1);
    %z += getWord(%pos[%i], 2);
  }
  %center = (%x / %count) SPC (%y / %count) SPC (%z / %count);

  for(%i = 1; %i <= %count; %i++) {
    if((%d = vectorDist(%center, %pos[%i])) < 2048 && %d > %dist)
      %dist = %d;
  }
  %dist = 5 + (%dist * 1.066); //Can't hurt
  %angle = 15;  //Arbitrary atm
  if(!isFile(%mission)) {
    error("createShowcaseServer: mission name unspecified");
    return;
  }
  if(!isFile(%filename)) {
    error("createShowcaseServer: save file unspecified");
    return;
  }
  slideshow_Holder.setVisible(0);
  if(isObject(serverConnection) || isObject($serverGroup))
    disconnect();
  $missionSequence = 0;
  $Server::PlayerCount = 0;
  $Server::ServerType = "";
  //Load the mission
  $ServerGroup = new SimGroup(ServerGroup);
  new SimSet(PlayerSet);
  new SimSet(ProjectileSet);
  $ServerGroup.add(PlayerSet);
  $ServerGroup.add(ProjectileSet);
  $Server::MissionName = $Pref::Server::MissionName;
  exec("tbm/server/scripts/constants.cs");
  exec("tbm/server/scripts/audioProfiles.cs");
  exec("tbm/server/scripts/brick.cs");
  exec("tbm/server/scripts/brickData.cs");
  exec("tbm/server/scripts/camera.cs");
  $Camera::MovementSpeed = %dist / 1.5 * mCos(%angle * $pi / 180);
  exec("tbm/server/scripts/tbmzip.cs");
  exec("tbm/server/scripts/tools/sarumanStaff.cs");
  exec("tbm/server/scripts/tbmServerCmd.cs");
  exec("dtb/server/scripts/Dynamic/Wiggy.cs");
  loadMission(%mission, true);
  %conn = new GameConnection(ServerConnection);
  RootGroup.add(ServerConnection);
  %conn.setConnectArgs("Spectator");
  %conn.setJoinPassword($Pref::Server::Password);
  MissionGroup.showcaseServer = 1;
  if(getSubStr(%fileName, 0, 12) $= "tbm/tbmzips/")
    MissionGroup.fileType = 1; //Save
  else
    MissionGroup.fileType = 2; //Gob
  MissionGroup.file = %fileName;
  MissionGroup.center = %center;
  MissionGroup.distance = %dist;
  MissionGroup.angle = %angle;
  %conn.connectLocal();
}

function getShowcaseServerMission(%file) {
if(strStr(%file, "tbm/client/iGob") == 0)
  return "tbm/data/missions/iGobPreviews.cs";
%file = getSubStr(%file, 12, strLen(%file));
%t = strStr(%file, "/");
%t = (%t == - 1 ? strStr(%file, "_") : (strStr(%file, "_") == -1 ? %t : (strStr(%file, "_") > %t ? %t : strStr(%file, "_"))));
if(%t != -1)
  return "tbm/data/missions/" @ getSubStr(%file, 0, %t);
return "";
}

function getEncodedObjectInfo(%string, %i) {
while(strStr(getWord(%string, 0), "¤") != -1)
  %string = removeWord(%string, 0);
switch$(strLwr(%i)) {
  case "type":
    return getWord(%string, 0);
  case "rotation":
    return getWords(%string, 1, 3);
  case "shapename":
    return strReplace(getWord(%string, 4), "¤", " ");
  case "position":
    return getWords(%string, 5, 7);
  case "scale":
    return getWords(%string, 12, 14);
  case "datablock":
    return getWords(%string, 15);
}
//Maybe an error code or something.
}

function showcaseLoadBlocks() {
cancel($ShowcaseLoadBlockSchedule);
Decode_Object(MissionGroup.showcaseString[MissionGroup.currentBlock++] , "0 0 0", 1);
if(MissionGroup.currentBlock < MissionGroup.showcaseBlocks)
  $ShowcaseLoadBlockSchedule = schedule(max(5000 / MissionGroup.showcaseBlocks, 50), 0, showcaseLoadBlocks);
else
  //$ShowcaseLoadBlockSchedule = schedule(30000, XXXX); //load-new-thing routine (not implemented yet; who sits on the main menu for that long?)
  doNothing();
}