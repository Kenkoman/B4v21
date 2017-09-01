//
//
//
//	Filename : Vehicles.cs

function VehicleData::onAdd(%data, %obj)
{
	%obj.setRechargeRate(%data.rechargeRate);

//	%obj.mountImage(MissileLauncherImage, 0); // Optional Weapon Mounting

	if(%obj.disableMove)
		%obj.immobilized = true;
}

function VehicleData::onRemove(%this, %obj)
{
	// if there are passengers/driver, kick them out
	for(%i = 0; %i < %obj.getDatablock().numMountPoints; %i++)
	{
		if (%obj.getMountNodeObject(%i)) 
		{
			%passenger = %obj.getMountNodeObject(%i);
			%passenger.unmount();
		}
	}
	if(%obj.lastPilot.lastVehicle == %obj)
		%obj.lastPilot.lastVehicle = "";      
}

function VehicleData::playerDismounted(%data, %obj, %player)
{
	if( %player.client.observeCount > 0 )
		resetObserveFollow( %player.client, true );
}

function VehicleData::hasDismountOverrides(%data, %obj)
{
   return false;
}

function FlyingVehicleData::create(%data, %team, %oldObj)
{
   if(%oldObj $= "")
   {
      %obj = new FlyingVehicle() 
      {
         dataBlock  = %data;
         respawn    = "0";
      };
   }
   else
   {
      %obj = new FlyingVehicle() 
      {
         dataBlock  = %data;
         respawn    = "0";
         mountable = %oldObj.mountable;
         disableMove = %oldObj.disableMove;
         resetPos = %oldObj.resetPos;
         deployed = %oldObj.deployed;
         respawnTime = %oldObj.respawnTime;
         marker = %oldObj;
      };   
   }
   %obj.mountable = true;
   
   return(%obj);
}
