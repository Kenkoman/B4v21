new AudioProfile(carEngineSound) {
   fileName = "~/data/sound/car_idle.wav";
   description = "AudioDefaultLooping3d";
   preload = true;
};

new WheeledVehicleTire(TBMTrainTireRear) {
   shapeFile = "~/data/shapes/bricks/vehicles/tbmtrainwheel.dts";
   friction = "6.5";
   lateralForce = "6000";
   lateralDamping = "400";
   lateralRelaxation = "1";
   longitudinalForce = "6000";
   longitudinalDamping = "400";
   longitudinalRelaxation = "1";  //This is actually supposed to be logitudinalRelaxation due to a typo in the engine but I'm keeping it the way the TBM devs wrote it.
};

new WheeledVehicleTire(TBMTrainTireFront) {
   shapeFile = "~/data/shapes/bricks/vehicles/tbmtrainwheel.dts";
   friction = 6.5;
   lateralForce = 6000;
   lateralDamping = 400;
   lateralRelaxation = 1;
   longitudinalForce = 6000;
   longitudinalDamping = 400;
   longitudinalRelaxation = 1;
};

new WheeledVehicleSpring(TBMTrainSpringRear) {
   length = "0.1";
   force = "3000";
   damping = "600";
   antiSwayForce = "3";
};

new WheeledVehicleSpring(TBMTrainSpringFront) {
   length = "0.1";
   force = "3000";
   damping = "600";
   antiSwayForce = "3";
};

new WheeledVehicleData(TBMTrain) {
   category = "Vehicles";
   shapeFile = "~/data/shapes/bricks/vehicles/tbmtrain.dts";
   emap = true;
   mountPose[0] = sitting;
   numMountPoints = "8";
   isProtectedMountPoint[0] = "1";
   maxDamage = "1.5";
   destroyedLevel = "1";
   maxSteeringAngle = "0.2";
   integration = "4";
   massCenter = "0 0 -5";
   cameraRoll = "0";
   cameraMaxDist = "20";
   cameraOffset = "2.5";
   cameraLag = "0.1";
   cameraDecay = "0.75";
   mass = "200";
   drag = "0.6";
   bodyFriction = "0.6";
   bodyRestitution = "0.4";
   minImpactSpeed = "5";
   softImpactSpeed = "5";
   hardImpactSpeed = "15";
   engineTorque = "5000";
   engineBrake = "600";
   brakeTorque = "2000";
   maxWheelSpeed = "40";
   maxEnergy = 100;
   jetForce = "3000";
   minJetEnergy = "30";
   jetEnergyDrain = "2";
   engineSound = carEngineSound;
};

function WheeledVehicleData::create(%block)
{
   %obj = new WheeledVehicle() {
      dataBlock = %block;
   };
   %obj.mountable = true;
   return %obj;
}

function TBMTrain::onAdd(%this, %obj) {
   for(%i = 3; %i >= 0; %i--) {
      %obj.setWheelTire(%i, TBMTrainTireFront);
      %obj.setWheelSpring(%i, TBMTrainSpringFront);
   }

   for(%i = 3; %i >= 2; %i--) {
      %obj.setWheelTire(%i, TBMTrainTireRear);
      %obj.setWheelSpring(%i, TBMTrainSpringRear);
   }
}

function TBMTrain::onCollision(%this, %obj, %col, %vec, %speed) {
}

function serverCMDspawnTrain(%client) {
if(%client.isAdmin || %client.isSuperAdmin || %client.ismod) {
  %t = WheeledVehicleData::create(TBMTrain);
  %t.setTransform(%client.player.getTransform());
}
}