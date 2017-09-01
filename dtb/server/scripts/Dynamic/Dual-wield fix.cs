package Dual_wield_updateprefs_fix {

function ServerCmdUpdatePrefs(%client, %name, %skin, %headCode, %visorCode, %backCode, %leftHandCode, %headCodeColor, %visorCodeColor, %backCodeColor, %leftHandCodeColor, %chestdecalcode, %facedecalcode, %bodytype) {
Parent::ServerCmdUpdatePrefs(%client, %name, %skin, %headCode, %visorCode, %backCode, %leftHandCode, %headCodeColor, %visorCodeColor, %backCodeColor, %leftHandCodeColor, %chestdecalcode, %facedecalcode, %bodytype);
%i = %client.player.getmountedimage(0);
if(%i == nametoid(uziimage) || %i == nametoid(pistolimage)) {
 %client.player.unmountimage(0);
 %client.player.mountimage(%i,0);
 }
}

function UZIImage::onUnmount(%this, %obj, %slot) {
Parent::onUnMount(%this,%obj,%slot);
commandtoclient(%obj.client,'updateprefs');
fixarmready(%obj);
}

function PistolImage::onUnmount(%this, %obj, %slot) {
Parent::onUnMount(%this,%obj,%slot);
commandtoclient(%obj.client,'updateprefs');
fixarmready(%obj);
}

};
activatepackage(Dual_wield_updateprefs_fix);

//%i.onarm(%client.player);