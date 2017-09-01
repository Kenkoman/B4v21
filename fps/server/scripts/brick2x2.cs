//brick2x2.cs

//its a 2 x 2 lego brick

//item
datablock ItemData(brick2x2)
{
	category = "BrickItems";  // Mission editor category
	className = "BrickItem"; // For inventory system

	 // Basic Item Properties
	shapeFile = "~/data/shapes/bricks/brick2x2.dts";
	scale = "2 2 3";

	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = false;
	rotate = false;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a 2x2 brick';
	invName = '2x2';
	image = brick2x2Image;
};

//static shape
datablock StaticShapeData(staticBrick2x2)
{
	category = "Bricks";  // Mission editor category

	item = brick2x2;
	ghost = ghostBrick2x2;
	className = "Brick";
	shapeFile = "~/data/shapes/bricks/brick2x2.dts";
	
	//lego scale dimensions
	x = 2;
	y = 2;
	z = 3;	//3 plates = 1 brick

	boxX = -1;
	//boxY = 0.5;
	//boxZ = 0.3;

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

//ghost brick
datablock StaticShapeData(ghostBrick2x2)
{
	category = "Statics";  // Mission editor category

	item = brick2x2;
	solid = staticBrick2x2;
	//className = Station;
	shapeFile = "~/data/shapes/bricks/brickGhost.dts";
	scale = "2 2 3";

	deployedObject = true;

};

//image
datablock ShapeBaseImageData(brick2x2Image)
{
   // Basic Item properties
   shapeFile = "~/data/shapes/bricks/brickWeapon.dts";
   staticShape = staticBrick2x2;
   ghost = ghostBrick2x2;
   emap = false;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   //eyeOffset = "1 1 -1";

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = brick2x2;
   ammo = " ";
   projectile = brickDeployProjectile;
   projectileType = Projectile;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = true;

   //casing = " ";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	//stateName[0]                     = "Activate";
	//stateTimeoutValue[0]             = 0.5;
	//stateTransitionOnTimeout[0]       = "Ready";

	stateName[0]                     = "Ready";
	stateTransitionOnTriggerDown[0]  = "Fire";
	stateAllowImageChange[0]         = true;

	stateName[1]			= "Fire";
	stateScript[1]                  = "onFire";
	stateFire[1]			= true;
	stateAllowImageChange[1]        = false;
	stateTimeoutValue[1]            = 0.5;
	stateTransitionOnTimeout[1]     = "Ready";

};

