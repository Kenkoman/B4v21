package Wiggy_Hoverboard_Jump {
function Armor::doDismount(%this, %obj, %forced) {
%car = %obj.getObjectMount();
if(isObject(%car)) {
  if(%car.getDatablock() == nameToID(SkiVehicle) && getSimTime() >= %car.lastJumpTime + 3000 && !!ContainerRayCast(%car.getTransform(), vectorAdd(%car.getTransform(), vectorScale(eulerToVector(vectorAdd("-90 0 0", vectorToEuler(%obj.getEyeVector()))),6)), $TypeMasks::ShapeBaseObjectType | $TypeMasks::TerrainObjectType | $TypeMasks::InteriorObjectType, %obj)) {
    //%car.applyImpulse(vectorAdd(%car.getWorldBoxCenter(), "0 0 -6"), "0 0 250"); //applyImpulse makes it roll to the left.  How very strange.
    %car.setVelocity(vectorAdd(%car.getVelocity(), %a = vectorScale(eulerToVector(vectorAdd("90 0 0", vectorToEuler(%obj.getEyeVector()))),200)));
    %car.lastJumpTime = getSimTime();
  }
  return;
}
Parent::doDismount(%this, %obj, %forced);
}

function ServerCmddropInventory( %client, %position) {
if(%client.player.inventory[%position] == nametoID(ski) && %client.player.getObjectMount().getDatablock() == nameToID(skiVehicle))
  ski.onUse(%client.player);
Parent::ServerCmddropInventory(%client, %position);
}

//This should be called "Hoverboard Stuff.cs" now
function SkiVehicle::onTrigger(%this, %obj, %triggerNum, %val) {
if(%triggerNum == 0) {
  if(%val)
    %obj.getMountNodeObject(0).fireSchedule = %obj.getMountNodeObject(0).schedule(0, fireSchedule);
  else
    cancel(%obj.getMountNodeObject(0).fireSchedule);
}
}
};
activatepackage(Wiggy_Hoverboard_Jump);

function Player::fireSchedule(%this) {
%this.setImageTrigger(0, 1);
%this.fireSchedule = %this.schedule(50, fireSchedule);
}