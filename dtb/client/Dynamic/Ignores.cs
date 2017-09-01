$NumIgnores = -1;

function IDToName(%ID) {
%name = "";
for(%i = 0; %i <= AdminPlayerList.rowCount(); %i++) {
  if(%ID == AdminPlayerList.getRowID(%i))
    %name = AdminPlayerList.getRowTextByID(%ID);
}
%name = strreplace(%name, "[Mod]", "");
%name = strreplace(%name, "[Admin]", "");
%name = strreplace(%name, "[Super]", "");
return %name;
}

function addToIgnore(%ID) {
if(!%ID || %ID == -1) return -1;
for(%i = 0; %i <= $NumIgnores; %i++) {
  if(%ID == $Ignores[%i]) {
    for(%j = 0; %j <= $NumIgnores-1; %j++) {
      $Ignores[%j] = $Ignores[%j+1];
    }
    $numIgnores--;
    ChatHUD.AddLine("\c3" @ IDToName(%ID) SPC "\c2has been \c3removed from \c2the Ignore list.");
    return 0;
  }
}
$Ignores[$NumIgnores++] = %ID;
ChatHUD.AddLine("\c3" @ IDToName(%ID) SPC "\c2has been \c3added to \c2the Ignore list.");
return 1;
}



package Wiggy_Ignores {
function ChatHUD::AddLine(%this, %line) {
%a = strpos(%line, ":\c2 ");
if(%a == -1) {
  Parent::AddLine(%this, %line);
  return;
}
%name = detagcolors(getsubstr(%line, 0, %a));
for(%i = 0; %i <= $NumIgnores; %i++) {
  if(%name $= IDToName($Ignores[%i])) {
    %j = strlen(strreplace(detagcolors(%line), %name @ ": ", ""));
    %name = getsubstr(%line, 0, %a);
    for(%k = 0; %k < %j; %k++) {
      %l = %l @ "*";
    }
    Parent::AddLine(%this, %name @ ":\c2 " @ %l);
    return;
  }
}
Parent::AddLine(%this,%line);
}
};
activatepackage(Wiggy_Ignores);