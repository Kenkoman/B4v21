 $Crown::RespawnTime = 4 * 1000;
$Crown::PopTime = 10 * 1000;

datablock ShapeBaseImageData(CrownImage) {
  shapeFile = "tbm/data/shapes/player/hats/Crown.dts";
  mountPoint = 5;
  className = "ItemImage";
  item = Crown;
  headUp = false;
  };

datablock ItemData(Crown) {
  shapeFile = "tbm/data/shapes/player/hats/Crown.dts";
  category = "Crown";
  equipment = true;
 image = CrownImage;
  mass = 1;
  friction = 1;
  elasticity = 1;
  rotate = true;
  maxInventory = 1;
  pickUpName = 'You got the Crown';
  invName = 'The Crown';
  };

function Crown::schedulePop(%this) {
  %this.schedule($Crown::PopTime - 1000, "startFade", 1000, 0, true);
  %this.schedule($Crown::PopTime, "delete");
  %this.timesup = schedule($Crown::PopTime,0,movecrown);
}

function Crown::onPickup(%this,%obj,%user,%amount) {
  if (!%user.client || %user.client.bodytype==666) {
    %obj.hide(0);
    return;
    }
  %player = %user;

  //Run over the crown in edit mode

  if (%user.client.edit) {
    SarumanStaffProjectile::onCollision(%this,%user,%obj,0,0,0);
    %obj.hide(0);
    }

//////////////////////////////////////////////////////////
  //Run over the crown before game begins
  //Run over the crown and not be on a team

  else if (!$crownchase || %player.client.team $= "") 
    %obj.hide(0);

  else if ($crownchase) { 
    if (!isobject($crownlocation) || $TeamCount <= 1){
      %obj.hide(0);
      $crownchase=0;
      return;
      }
    //red guy runs over the crown

    if (%player.client.team $= "red") {
      //stationary crown
      if (%obj.static) {
        %obj.hide(0);
        return;
        }
      //dropped crown
      else {
        cancel(%obj.timesup);
        %obj.delete();
        %player.client.incscore(5);
        movecrown();
        }
      }

    //some other guy runs over the crown
    else {
      messageAll( 'MsgAdminForce', %player.client.namebase@" has picked up the crown");
      %player.client.carrier=1;
      if (%obj.isStatic())
        %obj.hide(1);
      else {
        cancel(%obj.timesup);
        %obj.delete();
        }
      %player.inventory[8] = %this;
      messageClient(%user.client, 'MsgItemPickup', '', 8, "The Crown");
      Crown::onUse(%this, %player, 8);
      }
    }
  }

function Crown::onUse(%this, %player, %InvPosition) {
  %client = %player.client;
  %mountPoint = %this.image.mountPoint;
  messageClient(%client, 'MsgEquipInv', '', %InvPosition);
  %player.isEquiped[%InvPosition] = true;
  %player.mountimage(CrownImage, 5, 1, 'base');
  }

function movecrown() {
  $crownlocation.hide(0);
  messageAll( 'MsgAdminForce', "The Crown has been returned.");
  }

datablock ItemData(goalpoint)
{
   shapeFile = "tbm/data/shapes/sword.dts";
   category = "Crown";
   mass = 1;
   friction = 1;
   elasticity = 1;
   rotate = true;
   maxInventory = 0;
   pickUpName = '';
   invName = '';
};

function goalpoint::onPickup(%this,%obj,%user,%amount) {
  if (!%user.client)
    return;
  if (%user.client.edit) 
    SarumanStaffProjectile::onCollision(%this,%user,%obj,0,0,0);
  %player = %user;
  %client = %player.client;
  if (%player.getmountedimage($headslot) == nametoid(CrownImage)) {
    %player.client.incscore(10);
    %player.unmountimage($headslot);
    %player.mountImage(%player.client.headCode, $headSlot, 1, %player.client.headCodeColor);
    if (%player.getmountedimage(1) != nametoid(FlagImage))
      %player.client.carrier=0;
    messageClient(%player.client, 'MsgItemPickup', '', 8, "");
    messageClient(%player.client, 'MsgDeEquipInv', '', 8);
    %player.inventory[8] = 0;
    %player.isEquiped[8] = false;
    messageAll( 'MsgAdminForce', %client.namebase@" has stolen the crown!");
    movecrown();
    crownedking(%client);    
  }
}

function crownsetup(%teams) {
  $crownlocation=0;
  $crownchase=0;
  $prisonlocation = 0;
  if (%teams <= 1) {
    echo("Sorry but ou need more teams");
    return;
    }
  else
    createteams ("default"@%teams);
  $pref::server::autoteambalance = 1;
  %count = MissionCleanup.getCount();
  if (%count != 0) {
    for (%i = 0; %i <= %count; %i++) {
	%obj = MissionCleanup.getObject(%i);
        if (%obj.type[1] == -5) 
          $prisonlocation=%obj;
        else if (%obj.type[1] < 0)
          %TeamsSpawn[%obj.type[1] * -1] = true;
        if (%obj.getdatablock()==nametoid(Crown))
	  $crownlocation=%obj;
        if (%obj.getdatablock()==nametoid(goalpoint))
	  %goalpoint=true;
        }
    }

  if (%teams >=2 && %TeamsSpawn[1] && %TeamsSpawn[2] && isobject($crownlocation) && %goalpoint) {
    if (%teams >=3 && %TeamsSpawn[3]) {
      if (%teams == 4 && %TeamsSpawn[4])
        $crownchase=1;
      else if (%teams == 3)
        $crownchase=1;
      }
    else if (%teams == 2)
      $crownchase=1;
    }
  }

function servercmdctcsetup (%client, %teams) {
  if (%client.isadmin) {
    crownsetup(%teams);
    if ($crownchase==0)
      messageAll( 'Msg', "\c3CTC setup failed. Check your team count & team spawns and make sure you have one crown and at least one goal point.");
    else     
      messageAll( 'Msg', "\c3The admin ("@%client.namebase@") has successfully started a Capture the Crown Round.  Please assign people to a team that are already on the server; new comers will be auto assigned.");
    }
  }

function crownedking(%client) {
  %highscore=32000;
  %highscorer=0;
  for( %i = 0; %i < ClientGroup.getCount(); %i++) {
    %dclient = ClientGroup.getObject(%i);
    if (%dclient.team $= "red" && %dclient.score < %highscore) {
      %highscorer=%dclient;
      %highscore=%dclient.score;
      }
    }
  if (%highscore==32000)
    return;
  else {
    %highscorer.team = %client.team;
    %client.team = "red";
    %client.player.kill();
    %highscorer.player.kill();
    }
}
