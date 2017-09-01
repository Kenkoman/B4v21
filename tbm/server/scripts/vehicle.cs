$vehicleexp=30000;

function findEmptySeat(%vehicle)
{
	if(%vehicle.getdatablock().isAIturret)
		return 3*(!%vehicle.getMountNodeObject(2))-1;
	for (%i = 0; %i <  %vehicle.getDataBlock().numMountPoints; %i++)
		if(%vehicle.getMountNodeObject(%i) == 0)
			return %i;
	return -1;
}

function onMountVehicle(%vehicle, %player)
{
	if(%player.client.forbidvehicles) {
		messageClient(%player.client, 'junk', "You are not allowed to enter vehicles.");
		%player.client.resetUseduse();
		return;
		}

	if (!%vehicle.mountable && %vehicle.getdatablock().getname() !$= "staticchair2x2" && %vehicle.getdatablock().getname() !$= "staticdiesel8x16" && %vehicle.getdatablock().getname() !$= "statictrain8x16")
		doPlayerDismount(%player.client);
	if (getsubstr(%vehicle.getskinname(),0,5) $= "ghost" && (%vehicle.getdatablock().getname() $= "staticchair2x2" || %vehicle.getdatablock().getname() $= "staticdiesel8x16" || %vehicle.getdatablock().getname() $= "statictrain8x16")) {
		for( %i = 0; %i < ClientGroup.getCount(); %i++) {
			if(%vehicle == Clientgroup.getobject(%i).player.tempbrick) {
				%player.client.resetUseduse();
				return;
			}
		}
	}

	%seat = findEmptySeat(%vehicle);

	if(%seat > -1)
	{
		%player.isMounted = true;
		%player.mVehicle = %vehicle;
		%player.mSeat = %seat;
		%vehicle.mountObject(%player,%seat);
		%player.playThread(3,%vehicle.getDatablock().mountPose[%seat]);
		if(%seat == 0)
			%player.setControlObject(%vehicle);
		else
			%player.setControlObject(%player);
		if(%vehicle.getdatablock().getname() $= "staticchair2x2" || %vehicle.getdatablock().getname() $= "staticdiesel8x16" || %vehicle.getdatablock().getname() $= "statictrain8x16") {
			%player.playThread(3,"fall");
			%player.setControlObject(%player);
		}
		if(%vehicle.expire_timer)
			cancel(%vehicle.expire_timer);
	}
	else
		%player.client.resetUseduse();
}

function doPlayerDismount(%client)
{
	%player = %client.player;
	%vehicle = %player.mVehicle;
	if(!%player.isMounted || !%vehicle)
		return;

	%pos = getWords(%player.getTransform(), 0, 2);

	%player.mountVehicle = false;
	%player.isMounted = false;
	%player.schedule(4000, "setMountVehicle", true);
	%player.schedule(4000, "mountVehicles", true);

	%player.unmount();
	%player.setControlObject(%player);
	%player.playThread(3, root);

	%player.setTransform(%pos);
	%player.setVelocity(vectorAdd(vectorScale(%vehicle.getVelocity(), 0.75), "0 0 10"));
	if(%vehicle.getMountNodeObject(%player.mSeat+1))
			setActiveSeat(%vehicle.getMountNodeObject(%player.mSeat+1), %vehicle, %player.mSeat);
	%vehicle.mountable = true;

if(%vehicle.getdatablock().getname() !$= "staticchair2x2" || %vehicle.getdatablock().getname() $= "staticdiesel8x16" || %vehicle.getdatablock().getname() $= "statictrain8x16") {
	%vehicle.expire_timer = schedule($vehicleexp, 0, vehicleExpired, %vehicle);
	}
}


function setActiveSeat(%player, %vehicle, %seat)
{
	if(%player.isMounted)
		%player.unMount();
	%vehicle.mountObject(%player,%seat);
	%player.mVehicle = %vehicle;
	%player.mSeat = %seat;

	%player.playThread(0,%vehicle.getDatablock().mountPose[%seat]);

	if (%seat == 0 && %vehicle.getdatablock().getname() !$= "statictrain8x16")
	{
		%player.setControlObject(%vehicle);
		messageClient(%player.client, 'junk', "You now control this vehicle!");
	}
	else
		%player.setControlObject(%player);
	if(%vehicle.getMountNodeObject(%player.mSeat+2))
		setActiveSeat(%vehicle.getMountNodeObject(%player.mSeat+2), %vehicle, %player.mSeat+1);
}

function serverCmdMountVehicle(%client)
{
	//Stuff for the "use" key that takes precedence over vehicle stuff goes here
	//Meaning stuff that you want to happen instead of having a client get in or out of a vehicle

	if(!%client.usedUse && %scanTarg = getWord(ContainerRayCast(%client.player.getEyePoint(), vectorAdd(vectorScale(vectorNormalize(%client.player.getEyeVector()), 3), %client.player.getEyePoint()), $TypeMasks::vehicleObjectType | $TypeMasks::StaticShapeObjectType), 0)) {
		%client.usedUse = 1;
		%client.schedule(50,resetUsedUse);
		onMountVehicle(%scanTarg,%client.player);
		return;
	}
	%vehicle = %client.player.mVehicle;
	if(%client.player.isMounted && %vehicle && !%client.usedUse) {
		%client.usedUse = 1;
		%client.schedule(50,resetUsedUse);
		doPlayerDismount(%client);
		return;
	}
	if(isObject(%scanTarg = getWord(ContainerRayCast(%client.player.getEyePoint(), vectorAdd(vectorScale(vectorNormalize(%client.player.getEyeVector()), 4), %client.player.getEyePoint()), $TypeMasks::ItemObjectType), 0)) && !%client.usedUse) {
		if(%scantarg.getDatablock() == nametoID(portculyswitch) && (%scantarg.use || %client.edit)) {
			%client.usedUse = 1;
			%client.schedule(50,resetUsedUse);
			ServerPlay3D(freezeGunExplosionSound, %client.player.getTransform());
			PortculySwitch.activate(%scantarg, %client.player, 0, 1);
			return;
		}
	}

	//Stuff for the "use" key that does NOT take precedence over vehicle stuff goes here
	//Meaning stuff you want to happen if a person is NOT getting into or out of a vehicle goes here
}

function GameConnection::resetUsedUse(%this) {
%this.usedUse = 0;
}


function vehicleExpired(%vehicle) {
switch$(%vehicle.getClassName()) {
	case "WheeledVehicle":
		%type = -11;
	case "FlyingVehicle":
		%type = -12;
	default:
		return;
}
//Make sure the vehicle isn't currently occupied
for(%i = 0; %i <  %vehicle.getDataBlock().numMountPoints; %i++) {
	if(%vehicle.getMountNodeObject(%i)) {
		%vehicle.expire_timer.cancel();
		%vehicle.expire_timer = schedule($vehicleExp, 0, vehicleExpired, %vehicle);
		return;
	}
}
//If respawning is enabled attempt to respawn it
if($Pref::Server::VehicleRespawn) {
	%spawnpoints = -1;
	%c = MissionCleanup.getCount();
	for(%t = 0; %t < %c; %t++) {
		%spawn = MissionCleanup.getObject(%t);
		if(%spawn.type[1] == %type)
			%s[%spawnpoints++] = %spawn;
	}
	if(%spawnpoints > -1) {
		%vehicle.startFade(1000, 0, true);
		%vehicle.schedule(1010, setTransform, pickSpawnPoint(mAbs(%type)));
		%vehicle.startFade(1000, 1500, false);
		return;
	}
}
//Otherwise delete it as normal
%vehicle.startFade(1000, 0, true);
%vehicle.schedule(1000, delete);
}


function clearvehicles() {
%center = "0 0 0";
initContainerRadiusSearch(%center,10000,$TypeMasks::VehicleObjectType);
while(1) {
	%search=containerSearchNext();
	if(isObject(%search))
		%search.delete();
	else
		break;
}
}


function servercmdvehiclerespawn(%client) {
if(%client.isSuperAdmin) {
	if($pref::server::VehicleRespawn != 1) {
		$pref::server::VehicleRespawn = 1;
		messageall(' ','\c3Vehicle respawn\c2 has been \c7enabled.');
	}
	else {
		$pref::server::VehicleRespawn = 0;
		messageall(' ','\c3Vehicle respawn\c2 has been \c7disabled.');
	}
}
}




function servercmdmakevehiclespawn(%client,%mode) {
if(%client.isSuperAdmin) {
	if (!%client.edit) {
		messageClient(%client, 'Msg', "\c9You have to be in \c5EDITOR\c9 mode to use this.");
		return;
	}
	if (%client.lastswitch) {
		messageClient(%client, 'Msg', "\c9If you want to create a new switch you will have to first press 0 on your number pad to deselect all objects");
		return;
	}
	%client.lastswitch = new item() {
		static = "true";
		rotate = "false";
		position = vectorAdd(%client.getControlObject().position, vectorAdd("0 0 1", vectorScale(%client.getControlObject().getForwardVector(), 2)));
		rotation = "1 0 0 0";
		scale = "1 1 1";
		dataBlock = "portculyswitch";
		owner = getrawip(%client);
        };
	missioncleanup.add(%client.lastswitch);     
	handleghostcolor(%client,%client.lastswitch);
	%client.lastswitch.type[1] = %mode ? -12 : -11;
	messageclient(%client, 'Msg', "This is now a" SPC (%mode ? "flying" : "ground") SPC "vehicle respawn point.");
}
}