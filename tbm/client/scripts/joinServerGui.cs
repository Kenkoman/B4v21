$setFSLvis = 1;
function setFSLvis()
{
if( $setFSLvis == 1 )
{
%visnorm = 0;
%visfsl = 1;
$setFSLvis = 0;
}
else
{
%visnorm = 1;
%visfsl = 0;
$setFSLvis = 1;
}
Pass.setVisible(%visnorm);
SN.setVisible(%visnorm);
Ping.setVisible(%visnorm);
Players.setVisible(%visnorm);
scrServer.visible = %visnorm;
JS_serverlist.setVisible(%visnorm);
btnServername.setVisible(%visnorm);
btnPing.setVisible(%visnorm);
btnPass.setVisible(%visnorm);
btnplayers.setVisible(%visnorm);
scrFSL.setVisible( %visfsl );
FSL_FavServList.setVisible(%visfsl);
AddIP.setVisible(%visfsl);
AddName.setVisible(%visfsl);
FSL_SN.setVisible(%visfsl);
FSL_IP.setVisible(%visfsl);
txtIP.setVisible(%visfsl);
txtName.setVisible(%visfsl);
btnAddFS.setVisible(%visfsl);
btnDelFS.setVisible(%visfsl);
btnJoinFS.setVisible(%visfsl);
}
function FillFSL()
{
	FSL_FavServList.clear();
	for( %i = 1; %i <= $Pref::Client::FavServListNum; %i++)
		if( $FavServList::ip[%i] !$= "" )
			FSL_FavServList.addRow( %i, buildFavServListString( %i ) );
}

function buildFavServListString( %index )
{
	if( $FavServList::ip[%index] !$="" )
	{
	%ip = $FavServList::ip[%index];
	%servname = $FavServList::name[%index];
	return( %ip TAB %servname );
	}
}

function addFavServ( %ip, %name )
{
    %ip = getsubstr(%ip, 3,strlen(%ip)-9);
	%addFavServ = 1;
	for(%i = 1; %i <= $FavServListLoad; %i++)
		if(%ip $= $FavServList::ip[%i])
			%addFavServ = 0;
	if(%addFavServ == 1)
	{
		$Pref::Client::FavServListNum++;
		$FavServList::ip[$Pref::Client::FavServListNum] = %ip;
		$FavServList::name[$Pref::Client::FavServListNum] = %name;
		$FavServListAdd++;
	}
	saveFavServList();
	loadFavServList();
	FillFSL();
}

function delFavServ()
{
	$FavServListDel = $FavServListAdd;
	%id = FSL_FavServList.getSelectedId();
	%ip = getField(FSL_FavServList.getRowTextByID(%id), 0);
	for( %i = 1; %i <= $FavServListDel; %i++ )
	{
		if( $FavServList::ip[%i] $= %ip )
		{
			$FavServList::ip[%i] = "";
			$FavServList::name[%i] = "";
			$Pref::Client::FavServListNum--;
		}
	}
	saveFavServList();
	loadFavServList();
	FillFSL();
}

function loadFavServList()
{
	%file = new FileObject();
	%file.openForRead("TBM/client/scripts/FSL.cs");
	$Pref::Client::FavServListNum = 0;
	while( !%file.isEOF() )
	{
		%ipline = %file.readLine();
		%nameline = %file.readLine();
		 if( %ipline !$= "" )
		{
		$Pref::Client::FavServListNum++;
		$FavServList::ip[$Pref::Client::FavServListNum] = %ipline;
		$FavServList::name[$Pref::Client::FavServListNum] = %nameline;
		//$Pref::Client::FavServListNum++;
		}
	}
	%file.close();
	%file.delete();
	$FavServListLoad = $Pref::Client::FavServListNum;
	$FavServListAdd = $Pref::Client::FavServListNum;
}

function saveFavServList()
{
	$FavServListSave = 0;
	%file = new FileObject();
	%file.openForWrite("TBM/client/scripts/FSL.cs");
	for( %i = 1; %i <= $FavServListAdd; %i++)
	{
		if( $FavServList::ip[%i] !$= "" )
		{
			%file.writeLine( $FavServList::ip[%i] );
			%file.writeLine( $FavServList::name[%i] );
			$FavServListSave++;
		}
	}
	if( $FavServListSave < $FavServListLoad )
	{
		%FSLdiff = $FavServListLoad - $FavServListSave;
		for( %i = 0; %i < %FSLdiff; %i++)
		{
			%file.writeLine("");
			%file.writeLine("");
		}
	}
	%file.close();
	%file.delete();
}

function JoinServerGui::FSLJoin(%this, %pass)
{
   cancelServerQuery();
	%id = FSL_FavServList.getSelectedId();
	%ip = getField(FSL_FavServList.getRowTextByID(%id), 0);
	saveFavServList();
	$setFSLvisFSL = 0;
   // The server info index is stored in the row along with the
   // rest of displayed info.

   %conn = new GameConnection(ServerConnection);
   %conn.setConnectArgs($pref::Player::Name);
   %conn.setJoinPassword(%pass);
   %conn.connect(%ip);
}
