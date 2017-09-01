//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

// Support function which applies damage to objects within the radius of
// some effect, usually an explosion.  This function will also optionally 
// apply an impulse to each object.

function radiusDamage(%sourceObject, %position, %radius, %damage, %damageType, %impulse)
{
   // Use the container system to iterate through all the objects
   // within our explosion radius.  We'll apply damage to all ShapeBase
   // objects.
	InitContainerRadiusSearch(%position, %radius, $TypeMasks::ShapeBaseObjectType);

	%halfRadius = %radius / 2;
	while ((%targetObject = containerSearchNext()) != 0) {
		%coverage = calcExplosionCoverage(%position, %targetObject,
         	$TypeMasks::InteriorObjectType |  $TypeMasks::TerrainObjectType |
         	$TypeMasks::ForceFieldObjectType | $TypeMasks::VehicleObjectType);
      		if (%coverage == 0)
        		continue;

		%dist = containerSearchCurrRadiusDist();
	  	%distScale = (%dist < %halfRadius)? 1.0:
	      	   1.0 - ((%dist - %halfRadius) / %halfRadius);

		if(%targetObject.getClassname() $= "Player")
		{
%targetObject.damage(%sourceObject, %position, %damage * %coverage * %distScale, %damageType);
		
		}	

		if(%targetObject.getDatablock().classname $= "Brick" && $Pref::Server::DMBreakBricks == 1 && %targetObject.NoDestroy == 0)
		{
			if(%distScale == 1)
			{
				%targetObject.hits++;
				%targetObject.hits++;

			}
			else
			{
				%targetObject.hits++;
			}
			if(%targetObject.hits >= $Pref::Server::DMMaxBrickHits-2)
			{
				%targetObject.setSkinName('red');
			}
			else
			{
				%targetObject.SkinName = %targetObject.getSkinName();
			}
			if(%targetObject.hits >= $Pref::Server::DMMaxBrickHits)
			{
				%targetObject.hits = 0;
				%targetObject.startFade(0, 0, true);
				%targetObject.hide(true);
				%targetObject.setSkinName(%targetObject.SkinName);
				%targetObject.schedule($Pref::Server::DMBrickReSpawnTime*1000, "hide", false);
  				%targetObject.schedule($Pref::Server::DMBrickReSpawnTime*1000 + 100, "startFade", 1000, 0, false);
				
			}
		}


      		// Apply the impulse
     		if (%impulse)
		{
        		%impulseVec = VectorSub(%targetObject.getWorldBoxCenter(), %position);
         		%impulseVec = VectorNormalize(%impulseVec);
         		%impulseVec = VectorScale(%impulseVec, %impulse * %distScale);
         		%targetObject.applyImpulse(%position, %impulseVec);
      		}
    	}	

}
