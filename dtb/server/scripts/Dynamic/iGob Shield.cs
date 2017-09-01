package Wiggy_iGob_Shield {
function DMShieldPickup::onPickup(%this,%obj,%user,%amount) {
if (%user.client.bodytype==666 || %user.client.edit || !computePurchase(%user, %obj, 1)) {
	Parent::onPickup(%this,%obj,%user,%amount);
	return;
}
%player = %user;
%client = %player.client;
if (%client.DMShield != 1 && %player.getState() !$= "Dead")  {
	%player.iGobShield = new StaticShape() {
		datablock = iGobSphere;
	};
	if(%player.isCloaked())
		%player.iGobShield.setCloaked(1);
	%player.iGobShield.startFade(0, 0, 1);
	%player.iGobShield.startFade(1000, 10, 0);
	%player.iGobShield.setScale("1.1 1.1 2.2");
	if(%player.getMountedImage(2) == nameToID(cloakShowImage))
		%player.iGobShield.setScale("1.77 1.77 2.2");
	%player.iGobShield.setskinname(%player.getSkinName());
	%player.mountObject(%player.iGobShield, 2);
       	%player.iGobShield.schedule(25000, startFade, 5000, 0, 1);
       	%player.iGobShield.schedule(30000, delete);
	MissionCleanup.add(%player.iGobShield);
}
Parent::onPickup(%this,%obj,%user,%amount);
}

function GameConnection::onDeath(%this, %sourceObject, %sourceClient, %damageType, %damLoc) {
if(isObject(%this.player.iGobShield))
	%this.player.iGobShield.delete();
Parent::onDeath(%this, %sourceObject, %sourceClient, %damageType, %damLoc);
}

function Player::setSkinName(%this, %skin) {
if(isObject(%this.iGobShield))
	%this.iGobShield.setSkinName(%skin);
Parent::setSkinName(%this, %skin);
}

function Player::setCloaked(%this, %cloak) {
if(isObject(%this.iGobShield))
	%this.iGobShield.setCloaked(%cloak);
Parent::setCloaked(%this, %cloak);
}

};
activatepackage(Wiggy_iGob_Shield);