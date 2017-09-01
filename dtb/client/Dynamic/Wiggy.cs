//BEGIN
//A pseudo-library of some stuff that I make that actually turns out to be useful that I use in later addons
//This is mostly just so I'll stop worrying about stuff.

$WiggyPatch::ClientLib = 1.4;
//Released with TOB 2.3
//8/14/2013


//All functions, alphabetized

function getClient_c(%n) {
%name = detagColors(%n);
for(%i = 0; %i < AdminPlayerList.rowCount(); %i++) {
  %name = strreplace(AdminPlayerList.getRowText(%i), "[Mod]", "");
  %name = strreplace(%name, "[Admin]", "");
  %name = strreplace(%name, "[Super]", "");
  if(%name $= %n)
    return AdminPlayerList.getRowID(%i);
}
for(%i = 0; %i < AdminPlayerList.rowCount(); %i++) {
  %name = strreplace(AdminPlayerList.getRowText(%i), "[Mod]", "");
  %name = strreplace(%name, "[Admin]", "");
  %name = strreplace(%name, "[Super]", "");
  if(strLwr(%name) $= strLwr(%n))
    return AdminPlayerList.getRowID(%i);
}
for(%i = 0; %i < AdminPlayerList.rowCount(); %i++) {
  %name = strreplace(AdminPlayerList.getRowText(%i), "[Mod]", "");
  %name = strreplace(%name, "[Admin]", "");
  %name = strreplace(%name, "[Super]", "");
  if(strPos(%name, %n) != -1)
    return AdminPlayerList.getRowID(%i);
}
for(%i = 0; %i < AdminPlayerList.rowCount(); %i++) {
  %name = strreplace(AdminPlayerList.getRowText(%i), "[Mod]", "");
  %name = strreplace(%name, "[Admin]", "");
  %name = strreplace(%name, "[Super]", "");
  if(strPos(strLwr(%name),strLwr(%n)) != -1)
    return AdminPlayerList.getRowID(%i);
}
return -1;
}

function getGhost(%str) {
//Ghost data passed from the server; encoded as follows:
//Shapename TAB datablock TAB position TAB scale TAB velocity
%shapeName = getField(%str, 0);
%datablock = getField(%str, 1);
%position = getField(%str, 2);
%scale = getField(%str, 3);
%velocity = getField(%str, 4);
%ghost = -1;
%offset = 100; //So we don't get too off track
for(%i = 0; %i < ServerConnection.getCount(); %i++) {
  %obj = ServerConnection.getobject(%i);
  if(%obj.getShapeName() $= %shapeName && %obj.getDatablock() == %datablock && %obj.getScale() $= %scale) {
    %o = vectorDist(%obj.getPosition(), %position) * (1 + vectorDist(%obj.getVelocity(), %velocity));
    if(%o < %offset) {
      %offset = %o;
      %ghost = %obj;
    }
  }
}
return %ghost;
}

function reloadnews() {
$newshasstarted=0;TCPnews_init();
}
