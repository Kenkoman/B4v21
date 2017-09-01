function serverCmdSpawnHealthkit(%client) {
if(%client.isMod || %client.isAdmin) {
  %block = new Item() {
    position = vectorAdd(%client.getControlObject().position, vectorAdd("0 0 1", vectorScale(%client.getControlObject().getForwardVector(), 2)));
    rotation = "1 0 0 0";
    scale = "1 1 1";
    dataBlock = "HealthKit";
    owner = getRawIP(%client);
    static = true;
    rotate = true;
  };
  MissionCleanup.add(%block);
}
}