function AdvancedPrefsGui::onWake(%this)
{
$Advancedprefstog = 1;
}
function AdvancedPrefsGui::onSleep(%this)
{
$Advancedprefstog = 0;
}
function loadadvprefs() {
	%file = new FileObject();
	%file.openForRead("tbm/client/prefs.cs");
 	while(!%file.isEOF()) {
    %pref = %file.readline();
    if(%pref !$= "") {
    %prefname = disectfilename(%pref," ",0);
    %prefvalue = strreplace(strreplace(strreplace(strreplace(%pref,"\"",""),";","")," =",""),%prefname,"");
    %prefvalue = getsubstr(%prefvalue,1,strlen(%prefvalue));
    $Prefarrayname[$prefarraynum++] = %prefname;
    $Prefarrayvalue[$prefarraynum] = %prefvalue;
    //echo(%prefname @ " == " @ %prefvalue@";");
    Advpreflist.addRow( %i++, %prefname TAB %prefvalue);
        }
    }
    %file.close();
	%file.delete();
}
loadadvprefs();
$Advancedprefstog=0;
function Advancedprefstog(%val)
{
	if(%val && $Advancedprefstog == 0)
		canvas.pushDialog(AdvancedPrefsGui);
	else if(%val && $Advancedprefstog == 1)
		canvas.popDialog(AdvancedPrefsGui);
}
function changeadvpref() {
%row = Advpreflist.getselectedID();
%value = Txtadvprefvalue.getValue();
Txtadvprefvalue.setText("");
$Prefarrayvalue[%row] = %value;
resetpreflist(%row);
writeadvprefs();
}
function resetpreflist(%row) {
 Advpreflist.clear();
 for(%i=0;%i<$prefarraynum;0)
Advpreflist.addRow(%i++, $Prefarrayname[%i] TAB $Prefarrayvalue[%i]);
Advpreflist.setselectedrow(%row--);
}
function writeadvprefs() {
	%file = new FileObject();
	%file.openForWrite("tbm/client/prefs.cs");
    for(%i = 1; %i<$prefarraynum; %i++) {
    %file.writeline($Prefarrayname[%i] @ " = " @ "\"" @ $Prefarrayvalue[%i] @ "\";");
    //echo($Prefarrayname[%i] @ " = " @ $Prefarrayvalue[%i] @ ";");
    }
        %file.close();
	%file.delete();
   exec("tbm/client/prefs.cs");
}
