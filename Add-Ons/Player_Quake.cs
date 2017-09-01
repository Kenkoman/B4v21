//quakePlayer.cs

//a new player datablock with quake-like movement



datablock PlayerData(PlayerQuakeArmor : PlayerStandardArmor)
{
   runForce = 100 * 90;
   runEnergyDrain = 0;
   minRunEnergy = 0;
   maxForwardSpeed = 15;
   maxBackwardSpeed = 15;
   maxSideSpeed = 15;

   maxForwardCrouchSpeed = 7;
   maxBackwardCrouchSpeed = 7;
   maxSideCrouchSpeed = 7;

   jumpForce = 9 * 90; //8.3 * 90;
   jumpEnergyDrain = 0;
   minJumpEnergy = 0;
   jumpDelay = 0;

	minJetEnergy = 0;
	jetEnergyDrain = 0;
	canJet = 0;

	uiName = "Quake-Like Player";
	showEnergyBar = false;

   runSurfaceAngle  = 45;
   jumpSurfaceAngle = 45;
};