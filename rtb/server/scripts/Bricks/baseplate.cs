//baseplate.cs

//contains baseplate static shapes



datablock StaticShapeData(glassA)
{
	category = "Glass";  // Mission editor category

	item = brick2x2;
	ghost = ghostBrick2x2;
	className = "Brick";
	shapeFile = "~/data/shapes/environment/glassA.dts";
	

	maxDamage = 800;
	destroyedLevel = 800;
	disabledLevel = 600;
	explosion  = brickExplosion;
	expDmgRadius = 8.0;
	expDamage = 0.35;
	expImpulse = 500.0;

	dynamicType = $TypeMasks::StationObjectType;
	isShielded = true;
	energyPerDamagePoint = 110;
	maxEnergy = 50;
	rechargeRate = 0.20;
	renderWhenDestroyed = false;
	doesRepair = true;

	deployedObject = true;

	//cmdCategory = "DSupport";
	//cmdIcon = CMDStationIcon;
	//cmdMiniIconName = "commander/MiniIcons/com_inventory_grey";
	//targetNameTag = 'Deployable';
	//targetTypeTag = 'Station';

	//debrisShapeName = "debris_generic_small.dts";
	//debris = DeployableDebris;
	//heatSignature = 0;
};


//static shape
datablock StaticShapeData(gray32)
{
	category = "Baseplates";  // Mission editor category
	className = "baseplate";

	shapeFile = "~/data/shapes/bricks/baseplate32.dts";

	//lego scale dimensions
	x = 32;
	y = 32;
	z = 1;		//1 plate height (1/3 of a brick)

	//item = brick2x2;
	//className = Station;
	//shapeFile = "~/data/shapes/baseplate32.dts";
	//maxDamage = 800;
	//destroyedLevel = 800;
	//disabledLevel = 600;
	//explosion      = DeployablesExplosion;
	//expDmgRadius = 8.0;
	//expDamage = 0.35;
	//expImpulse = 500.0;

	//dynamicType = $TypeMasks::StationObjectType;
	//isShielded = true;
	//energyPerDamagePoint = 110;
	//maxEnergy = 50;
	//rechargeRate = 0.20;
	//renderWhenDestroyed = false;
	//doesRepair = true;

	deployedObject = true;

	//cmdCategory = "DSupport";
	//cmdIcon = CMDStationIcon;
	//cmdMiniIconName = "commander/MiniIcons/com_inventory_grey";
	//targetNameTag = 'Deployable';
	//targetTypeTag = 'Station';

	//debrisShapeName = "debris_generic_small.dts";
	//debris = DeployableDebris;
	//heatSignature = 0;
};

function gray32::onCollision(%this,%obj,%col,%vec,%speed)
{
	//echo("baseplate collsion");
}

function gray32::onImpact(%this, %obj, %collidedObject, %vec, %vecLen)
{
	//echo("baseplate impact");
}
datablock StaticShapeData(flowers)
{
	category = "Statics";  // Mission editor category
	//className = baseplate;

	shapeFile = "~/data/shapes/flowers.dts";
	boundingBox = "1.5 1.5 2.65";

	skinName = 'red';
};

datablock StaticShapeData(pineTree)
{
	category = "Statics";  // Mission editor category
	className = pineTree;

	shapeFile = "~/data/shapes/bricks/pineTree.dts";
	skinName = 'green';
};