//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Hook into the mission editor.
function staticShapedata::onRemove(%this, %obj)
{
	if(%obj.Datablock $= "staticbrick2x2FX")
	{
		if(%obj.flameEmitter !$= "")
		{
			%obj.owner.FXbrickcount--;
			%obj.flameEmitter.delete();
		}
		if(%obj.smokeEmitter !$= "")
		{
			%obj.owner.FXbrickcount--;
			%obj.smokeEmitter.delete();
		}
		if(%obj.bubbleEmitter !$= "")
		{
			%obj.owner.FXbrickcount--;
			%obj.bubbleEmitter.delete();
		}
	}
	if(%obj.mountedDecal !$= "")
	{
		%obj.mounteddecal.delete();
	}
}
function StaticShapeData::create(%data)
{
   // The mission editor invokes this method when it wants to create
   // an object of the given datablock type.
   %obj = new StaticShape() {
      dataBlock = %data;
   };
   return %obj;
}


function StaticShapeData::onAdd(%this, %obj)
{
	%obj.setSkinName(%this.skinName);
}


function StaticShapeData::damage(%this, %obj, %sourceObject, %position, %damage, %damageType)
{
	%obj.setDamageLevel(%obj.getDamageLevel() + (%damage));

	if(%obj.getDamageLevel() >= %this.maxDamage)
	{	
		%obj.team.count[%this.pack] -= 1;
		%trans = %obj.getTransform();
		%exp = new (explosion)()
		{
			datablock = %this.explosion;
		};
		MissionCleanUp.add(%exp);
		%exp.setTransform(%trans);
		%obj.setTransform("0 0 -999");
		%obj.delete();
	}
}


function StaticShape::Explode(%obj)
{
	%obj.setDamageState(destroyed);
	%obj.schedule(100, delete);
	return;


	%data = %obj.getDataBlock();

	//error("static shape exploding");
	%pos = %obj.getWorldBoxCenter();

	%exp = new (explosion)()
	{
		datablock = spearExplosion;
		initialPosition = %pos;
	};
	//%exp.setTransform(%pos);
	%obj.setTransform("0 0 -999");

	//serverplay3D(brickExplosionSound, %obj.getWorldBoxCenter());	
	%obj.schedule(100, delete);
	MissionCleanUp.add(%exp);
}