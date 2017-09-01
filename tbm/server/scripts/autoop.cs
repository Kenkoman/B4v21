///////////////////BY: MCP////////////////////////
function gameConnection::isadminhere(%this) {
if(%this.secure) {
  for (%i=1; %i<$admin::num+1; %i++) {
    if (getRawIP(%this)$=$admin::ip[%i]) {
        %this.isadmin=1;
        %this.qupdate();
        return 1;
        }
    }
  }
  return 0;
}

function gameConnection::admined(%this) {
  %this.isadmin=1;
  %this.qupdate();
  if(getRawIP(%this) !$= "local") {
    $admin::name[$admin::num++] = getTaggedstring(%this.name);
    $admin::ip[$admin::num] = getRawIP(%this);
    saveAMlist();
  }
}	

function gameConnection::deadmin(%this) {
  %this.isadmin=0;
  for (%i=1; %i<$admin::num+1; %i++) {
    if (getRawIP(%this)$=$admin::ip[%i])
      $admin::name[%i]="";
    }
  %this.qupdate();
  saveAMlist();
  exec("tbm/server/admins.cs");
}

function saveAMlist() {
  $AMlist = new FileObject();
  $AMlist.openForWrite("tbm/server/admins.cs");
  %cor=0;
  for (%i=1; %i<$admin::num+1; %i++) {
    if ($admin::name[%i]$="")
      %cor++;
    else {
      $AMlist.writeLine("$admin::name["@%i-%cor@"]=\""@$admin::name[%i]@"\";");
      $AMlist.writeLine("$admin::ip["@%i-%cor@"]=\""@$admin::ip[%i]@"\";");
      }
    }
  $AMlist.writeLine("$admin::num="@$admin::num-%cor@";");
  $AMlist.close();
}

function servercmdstudbank(%client) {
if(%client.isSuperAdmin) {
  if($Pref::Server::StudBank != 1)  {
    $Pref::Server::StudBank = 1;
    messageAll(' ','\c3$tud banking\c2 has been \c7enabled.');
    parseStudBank();
  }
  else {
    $Pref::Server::StudBank = 0;
    messageAll(' ','\c3$tud banking\c2 has been \c7disabled.');
  }
}
}

function gameConnection::ismodhere(%this) {
if(%this.secure) {
  for (%i=1; %i<$mod::num+1; %i++) {
    if (getRawIP(%this)$=$mod::ip[%i]) {
        %this.ismod=1;
        %this.qupdate();
        return 1;
      }
  }
}
return 0;
}

function gameConnection::moded(%this) {
  %this.ismod=1;
  %this.qupdate();
  if(getRawIP(%this) !$= "local") {
    $mod::name[$mod::num++] = getTaggedstring(%this.name);
    $mod::ip[$mod::num] = getRawIP(%this);
    saveMDlist();
  }
}

function gameConnection::demod(%this) {
  %this.ismod=0;
  for (%i=1; %i<$mod::num+1; %i++) {
    if (getRawIP(%this)$=$mod::ip[%i])
      $mod::name[%i]="";
    }
  %this.qupdate();
  saveMDlist();
  exec("tbm/server/mods.cs");
}

function saveMDlist() {
  $MDlist = new FileObject();
  $MDlist.openForWrite("tbm/server/mods.cs");
  %cor=0;
  for (%i=1; %i<$mod::num+1; %i++) {
    if ($mod::name[%i]$="")
      %cor++;
    else {
      $MDlist.writeLine("$mod::name["@%i-%cor@"]=\""@$mod::name[%i]@"\";");
      $MDlist.writeLine("$mod::ip["@%i-%cor@"]=\""@$mod::ip[%i]@"\";");
      }
    }
  $MDlist.writeLine("$mod::num="@$mod::num-%cor@";");
  $MDlist.close();
}

function gameConnection::qupdate(%this) {
	messageAll('MsgClientJoin', '', 
			  %this.name, 
			  %this,
			  %this.sendGuid,
			  %this.score,0,
			  %this.isAdmin, 
			  %this.isSuperAdmin,
			  %this.isMod);
//			  %this.isAiControlled(), 
}

  exec("tbm/server/admins.cs");
  exec("tbm/server/mods.cs");
  exec("tbm/server/studs.cs");
  parseStudBank();