//glassb.cs

//contains baseplate static shapes



datablock StaticShapeData(glassB)
{
	category = "Glass";  // Mission editor category

	item = brick2x2;
	ghost = ghostBrick2x2;
	className = "Brick";
	shapeFile = "~/data/shapes/Chris/glassB.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";	

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


