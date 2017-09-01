function LoadSaveList(%dedicated)
{
	%rowcount = 0;
	%mission = filename($Server::MissionFile);
	if (%dedicated) {
		echo("|||||||||||||||||||||||||||||||||||||||||||||");
		echo("----Saved file list for current map----");
		echo("---------------------------------------------");
	}
	for (%filename = findFirstFile("tbm/tbmzips/*.save"); %filename !$= ""; %filename = findNextFile("tbm/tbmzips/*.save"))
	{
	%filename = strReplace(%fileName, "tbm/tbmzips/", "");
	echo(%filename);
	%c = getSubStr(%fileName, strLen(filename($Server::MissionFile)), 1);
	echo(%mission@" compare "@filename(getSubStr(%filename, 0, strstr(%filename, %c))));
		if (%mission !$= filename(getSubStr(%filename, 0, strstr(%filename, %c))))
			continue;
		while(strstr(%filename, %c) != -1)
			%filename = getSubStr(strchr(%filename, %c), 1, strlen(strchr(%filename, %c)));  //grab the 'name'
		if (%dedicated)
			echo("---  " @ %filename);
		else
			PersList.addrow(%rowcount++, %filename);
	}
	if (%dedicated)
	{
		echo("---------------------------------------------------");
		echo("----End of saved file list for current map----");
		echo("|||||||||||||||||||||||||||||||||||||||||||||||||||");
	}
}

function ExecPersistence()
{
	%persname = PersList.getRowTextByID(PersList.getSelectedID());
        $UniquePersistName = Strreplace( %persname, ".save", "" );
	loadPersistence($UniquePersistName);
	canvas.popDialog(PersListDlg);
	canvas.popDialog(AdminGui);
}

function ListSaveFiles()
{
	LoadSaveList(0);
}
function LoadPersistenceList()
{
	LoadSaveList();
}
