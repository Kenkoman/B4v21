function kick()
{
	//get client id from admin player list
	%victimId = lstAdminPlayerList.getSelectedId();
	//send command to server
	commandToServer('kick', %victimId);
}

function ban()
{
	//get client id from admin player list
	%victimId = lstAdminPlayerList.getSelectedId();
	//send command to server
	commandToServer('ban', %victimId);
}

function changeMap()
{
	//push the map change dialog
	//request maplist from server?

}
