$Flag::RespawnTime = 4 * 1000;
$Flag::PopTime = 10 * 1000;

datablock ShapeBaseImageData(FlagImage) {
	shapeFile = "~/data/shapes/items/flag.dts";
	emap = false;
	mountPoint = 1;
	className = "ItemImage";
	item = Flag;
	headUp = false;
        };

datablock ItemData(Flag) {
  shapeFile = "~/data/shapes/items/flag.dts";
  category = "Flag";
  equipment = true;
  image = FlagImage;
  mass = 1;
  friction = 1;
  elasticity = 1;
  rotate = true;
  maxInventory = 1;
  pickUpName = 'You got the enemy flag';
  invName = "The Flag";
  };

function Flag::schedulePop(%this) {  //not quite sure how to call this from GameConnection::OnDeath, can anyone help? -DShiznit
  %side=%this.getskinname();
  %this.schedule($Flag::PopTime - 1000, "startFade", 1000, 0, true);
  %this.schedule($Flag::PopTime, "delete");
  %this.timesup = schedule($Flag::PopTime,0,returnflag,%side);
}

function Flag::onPickup(%this,%obj,%user,%amount) {
  if (!%user.client || %user.client.bodytype==666) {
    %obj.hide(0);
    return;
    }  
  %player = %user;

  //Run over a flag in edit mode

  if (%user.client.edit) {
    SarumanStaffProjectile::onCollision(%this,%user,%obj,0,0,0);
    %obj.hide(0);
    }

  //Run over a flag before game begins
  //Run over a flag and not be on a team

  else if (!$CTF || %player.client.team $= "") 
    %obj.hide(0);

  else if ($CTF) {
    if (!isobject($Teamsflag[1]) && !isobject($Teamsflag[2])) {
      %obj.hide(0);
      $CTF=0;
      return;
      }

    //How do you like this logic? It's perfect but hard to follow at first
    //It basically just checks to make sure the flag is a valid color
    if ((%obj.getskinname() !$= "red" && %obj.getskinname() !$= "blue")) {
      if ($TeamCount == 2) {
        %obj.hide(0);
        return;
        }
      else if (%obj.getskinname() !$= "green") {
        if ($TeamCount == 3) {
	  %obj.hide(0);
	  return;
          }
        else if (%obj.getskinname() !$= "yellow") {
	  if ($TeamCount == 4) {
	    %obj.hide(0);
	    return;
            }
	  }
	}
      }

    //Run over your own flag

    if (%player.client.team $= %obj.getskinname()) {
      //stationary Flag
      if (%obj.static) {
        %obj.hide(0);
        //with a flag
        if (%player.getmountedimage(1) == nametoid(FlagImage)) {
          %player.client.incscore(10);
          %player.unmountimage($lefthandslot);
          if (%player.getmountedimage(5) != nametoid(CrownImage))
            %player.client.carrier=0;
          messageClient(%player.client, 'MsgItemPickup', '', 9, "");
  	  messageClient(%player.client, 'MsgDeEquipInv', '', 9);
          %player.inventory[9] = 0;
      	  %player.isEquiped[9] = false;
          messageAll( 'MsgAdminForce', %player.client.namebase@" has captured the "@%player.enemyflag@" team\'s flag.");
          returnflag(%player.enemyflag);
          %player.enemyflag = "";
          }
        //without a flag
        else
          return;
        }
      //dropped Flag
      else {
        cancel(%obj.timesup);
        %obj.delete();
        %player.client.incscore(5);
        returnflag(%player.client.team);
        }
      }

    //Run over someone else's flag
    else {
      //you already have a flag
      if (%player.getmountedimage(1) == nametoid(FlagImage)) {
        %obj.hide(0);
        return;
        }
      //you don't already have a flag continue
      else {
        %player.enemyflag = %obj.getskinname();
        messageAll( 'MsgAdminForce', %player.client.namebase@" has picked up "@%player.enemyflag@" team\'s flag.");
        %player.client.carrier=1;
        if (%obj.isStatic())
          %obj.hide(1);
        else {
          cancel(%obj.timesup);
          %obj.delete();
          }
        %player.inventory[9] = %this;
        messageClient(%user.client, 'MsgItemPickup', '', 9, %player.enemyflag@" flag");
        Flag::onUse(%this, %player, 9);
        }
      }
    }
  }

function Flag::onUse(%this, %player, %InvPosition) {
  %client = %player.client;
  %mountPoint = 1;
  messageClient(%client, 'MsgEquipInv', '', %InvPosition);
  %player.isEquiped[%InvPosition] = true;
  %player.mountimage(FlagImage, %mountPoint, 1, addTaggedString(%player.enemyflag));
  }

function returnflag(%team) {
  switch$ (%team) {
    case "red":
      $Teamsflag[1].hide(0);
    case "blue":
      $Teamsflag[2].hide(0);
    case "green":
      $Teamsflag[3].hide(0);
    case "yellow":
      $Teamsflag[4].hide(0);
    }
  messageAll( 'MsgAdminForce', %team@"\'s flag has been returned.");
  }

function ctfsetup(%teams) {
  $CTF=0;
  if (%teams <= 1) {
    echo("Sorry but ou need more teams");
    return;
    }
  else
    createteams ("default"@%teams);
  $pref::server::autoteambalance = 1;
  %count = MissionCleanup.getCount();
  if (%count != 0) {
    for (%i = 1; %i < %count; %i++) {
      %obj = MissionCleanup.getObject(%i);
      if (%obj.type[1] < 0)
        %TeamsSpawn[%obj.type[1] * -1] = true;
      if (%obj.getdatablock()==nametoid(flag)) {
        switch$ (%obj.getskinname()) {
          case "red":
            $Teamsflag[1] = %obj;
          case "blue":
            $Teamsflag[2] = %obj;
          case "green":
            $Teamsflag[3] = %obj;
          case "yellow":
            $Teamsflag[4] = %obj;
          }
        }
      }
    }
  if (%teams >=2 && %TeamsSpawn[1] && %TeamsSpawn[2] && isobject($Teamsflag[1]) && isobject($Teamsflag[2])) {
    if (%teams >=3 && %TeamsSpawn[3] && isobject($Teamsflag[3])) {
      if (%teams == 4 && %TeamsSpawn[4] && isobject($Teamsflag[4]))
        $CTF=1;
      else if (%teams == 3) {
        $CTF=1;
        if (isobject($Teamsflag[4]))
	  $Teamsflag[4].delete();
        }
      }
    else if (%teams == 2) {

        $CTF=1;
        if (isobject($Teamsflag[3]))
	  $Teamsflag[3].delete();
        if (isobject($Teamsflag[4]))
	  $Teamsflag[4].delete();
      }
    }
  }


function servercmdctcsetup (%client, %teams) {
  if (%client.isadmin) {
    ctcsetup(%teams);
    if ($Crownchase==0)
      messageAll( 'Msg', "\c3CTC setup failed. Check your team count & team spawns and make sure you have one flag per team.");
    else     
      messageAll( 'Msg', "\c3The admin ("@%client.namebase@") has successfully started a Capture the Crown Round.  Please assign people to a team that are already on the server; new comers will be auto assigned.");
  $pref::server::autoteambalance = 1;
    }
  }