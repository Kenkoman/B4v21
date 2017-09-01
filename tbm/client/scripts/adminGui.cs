function kick()
{
	//get client id from admin player list
	%victimId = AdminPlayerList.getSelectedId();
	//send command to server
	commandToServer('kick', %victimId);
}

function ban()
{
	//get client id from admin player list
	%victimId = AdminPlayerList.getSelectedId();
	//send command to server
	commandToServer('ban', %victimId);
}
function setAdmin()
{
	//get client id from admin player list
	%victimId = AdminPlayerList.getSelectedId();
	//send command to server
	commandToServer('AdminPlayer', %victimId);
}
function unsetAdmin()
{
	//get client id from admin player list
	%victimId = AdminPlayerList.getSelectedId();
	//send command to server
	commandToServer('unAdminPlayer', %victimId);
}
function setMod()
{
	//get client id from admin player list
	%victimId = AdminPlayerList.getSelectedId();
	//send command to server
	commandToServer('setMod', %victimId);
}
function unsetMod()
{
	//get client id from admin player list
	%victimId = AdminPlayerList.getSelectedId();
	//send command to server
	commandToServer('unsetMod', %victimId);
}
function UnownByName()
{
	//get client id from admin player list
	%victimId = AdminPlayerList.getSelectedId();
	//send command to server
	commandToServer('BrickHandleOptions', 1, %victimId);
}
function DestroyByName()
{
	//get client id from admin player list
	%victimId = AdminPlayerList.getSelectedId();
	//send command to server
	commandToServer('BrickHandleOptions', 4, %victimId);
}
function CheckMoney()
{
	%victimId = AdminPlayerList.getSelectedId();
	commandToServer('checkmoney', %victimId);
}
function setmoneybrick()
{
	%victimId = AdminPlayerList.getSelectedId();
	commandToServer('setmoneybrick', %victimId);
}

function SetMoney(%money)
{
	%victimId = AdminPlayerList.getSelectedId();
	commandToServer('setmoney', %victimId, %money);
}




function DePermByName()
{
	//get client id from admin player list
	%victimId = AdminPlayerList.getSelectedId();
	//send command to server
	commandToServer('BrickHandleOptions', 3, %victimId);
}
function DestroyBySelIP()
{
	//send command to server
	commandToServer('deletebyip');
}
function ColorByName()
{
	//get client id from admin player list
	%victimId = AdminPlayerList.getSelectedId();
	//send command to server
	commandToServer('BrickHandleOptions', 6, %victimId);
}
function RespawnPlayer()
{
	%victimId = AdminPlayerList.getSelectedId(); // get client id
	commandtoserver('respawn',%victimId); // respawn the sucker
}
function StunPlayer()
{
	%victimId = AdminPlayerList.getSelectedId(); // get client id
	commandtoserver('tumble',%victimId,60000); // pwn him
}
function changeMap()
{
	//push the map change dialog
	//request maplist from server?

}
function saveStuff()
{
%filepath = "tbm/tbmzips/"@$Server::MissionName@"/"@txtSPName.getValue()@".save";
%filepath = strreplace(%filepath,"tbm/data/missions/", "");
   if(isFile(%filepath))
      MessageBoxYesNo( "Overwrite", "Overwrite old save file?", "savePersistence(txtSPName.getValue(), chkboxPersis.getValue()); savestudlist();", "");
   else {
      savePersistence(txtSPName.getValue(), chkboxPersis.getValue());
	savestudlist();
}
}
function clearallbricks()
{
      MessageBoxYesNo( "Clear", "Clear all bricks in the server?", "clearmission();", "");
}

