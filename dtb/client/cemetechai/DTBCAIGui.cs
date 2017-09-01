echo("DTB/CemetechAI GUI Scripts");

function caigui::onWake(%this) {
	$CaiGuiTog = 1;
	numteams.clear();
	numteams.add("#teams",-2);
	numteams.add("Clear Teams", -1);
	numteams.add("default2",2);
	numteams.add("default3",3);
	numteams.add("default4",4);
	numteams.setselected(($TeamCount < 2) ? -2 : $TeamCount);
}

function caigui::onSleep(%this) {
  $CaiGuiTog = 0;
}

function numteams::onSelect(%this, %id, %text) {
  if(%id == -1) commandtoserver('clearteams');
  else if(%id >= 2) commandtoserver('createteams',%text);
}

function guictfstart(){
	commandtoserver('ctfsetup',$TeamCount);	
}

function guictcstart(){
	commandtoserver('ctcsetup',$TeamCount);	
}

function caiguitog(%val){
	if(%val && $CaiGuiTog == 0)
		canvas.pushDialog(caigui);
	else if(%val && $CaiGuiTog == 1)
		canvas.popDialog(caigui);
}

$RemapName[$RemapCount] = "Toggle CemetechAI/DTB GUI";
$RemapCmd[$RemapCount] = "caiguitog";
$RemapCount++;