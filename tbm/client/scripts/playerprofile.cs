function playerprofiletoggle() {
commandtoserver('getplayerprofile',adminplayerlist.getselectedID());
}
function clientcmdrecieveprofile(%name,%brickcount,%inedit,%serverpower,%IP,%brick,%info,%client,%cl) {
if(%name $= "")
return;
if((%cl !$= "" && %cl $= %client) || %name $= $Pref::Player::Name) {
profile_edit_scroll.setVisible(1);
profile_edit_txt.setValue($Pref::Player::ProfileInfo);
}
else
profile_edit_scroll.setvisible(0);
PlayerProfile.settext(%name@"'\s Profile");
txtprofilename.setText(%name);
txtprofilebricks.setText(%brickcount @ " bricks ever placed.");
txtprofileedit.setText(%inedit);
txtprofilepower.setText(%serverpower);
txtprofileIP.setText(%IP);
txtprofilebrick.setText(%brick);
txtprofileinfo.setText(%info);
Canvas.pushDialog(PlayerprofileGui);
}
function PlayerProfile::OnWake(%this) { }
function PlayerProfile::OnSleep(%this) {
PlayerProfile.settext("Player Profile");
txtprofilename.setText();
txtprofilebricks.setText();
txtprofileedit.setText();
txtprofilepower.setText();
txtprofileIP.setText();
}
function clientcmdbrickplanted() {
$Pref::Player::TotalPlantedBricks++;
setprofileinfo();
}
function closeprofilegui() {
if(profile_edit_scroll.isvisible()) {
$Pref::Player::ProfileInfo = profile_edit_txt.getValue();
setprofileinfo();
}
Canvas.popDialog(Playerprofilegui);
}
function setProfileInfo() {
commandtoserver('clientprofile',$Pref::Player::TotalPlantedBricks, $Pref::Player::ProfileInfo);
}
