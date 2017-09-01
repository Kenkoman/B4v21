//This references code from MCP's Loudhailer.cs. -Wiggy

//////////
// item //
//////////
datablock ItemData(ThrowAxe)
{
	category = "Weapon";  // Mission editor category
	className = "Tool"; // For inventory system

	 // Basic Item Properties
	shapeFile = "tbm/data/shapes/legoaxe2.dts";
	skinName = 'white';
	rotate = false;
	mass = 1;
	density = 1;
	elasticity = 0;
	sticky = 1;
	friction = 1;
	emap = true;

	 // Dynamic properties defined by the scripts
	pickUpName = 'a set of Throwing Axes.';
	invName = "Throwing Axes";
	image = ThrowAxeImage;
	threatlevel = "Normal";

	//Projectile data
	directDamage = 200;
	muzzleVelocity = 80;
	damagetype        = '%2 put an axe in %1\'s head';
};

addWeapon(ThrowAxe);

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(ThrowAxeImage)
{
   // Basic Item properties
   shapeFile = "tbm/data/shapes/legoaxe2.dts";
   skinName = 'white';
   emap = true;
    
   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   //eyeOffset = "0.1 0.2 -0.55";

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point. 
   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = ThrowAxe;
   ammo = "";
   projectile = throwAxe;
   projectileType = Projectile;

   deathAnimationClass = "projectile";
   deathAnimation = "axe";
   deathAnimationPercent = 1;

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
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.5;
	stateTransitionOnTimeout[0]       = "Ready";
	stateSound[0]					= swordDrawSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "PreFire";
	stateAllowImageChange[1]         = true;

	stateName[2]			= "PreFire";
	stateScript[2]                  = "onPreFire";
	stateAllowImageChange[2]        = false;
	stateTimeoutValue[2]            = 0.1;
	stateTransitionOnTimeout[2]     = "Fire";

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "StopFire";
	stateTimeoutValue[3]            = 0.2;
	stateFire[3]                    = true;
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Throw";
	stateScript[3]                  = "onFire";   //Everything calls onFire.  Use it.
	stateWaitForTimeout[3]		= true;
	stateSound[3]			= bowFireSound;
	//stateTransitionOnTriggerUp[3]	= "StopFire";

	stateName[4]			= "CheckFire";
	stateTransitionOnTriggerUp[4]	= "Ready";
	
	stateName[5]                    = "StopFire";
	stateTransitionOnTimeout[5]     = "CheckFire";
	stateTimeoutValue[5]            = 0.7;
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;
	stateSequence[5]                = "StopFire";
	stateScript[5]                  = "onStopFire";


};

datablock ShapeBaseImageData(ThrowAxeMountImage)
{
   shapeFile = "tbm/data/shapes/legoaxeMount.dts";
   mountPoint = 5;
   offset = "0 0 0";
   className = "ItemImage";
};

function ThrowAxe::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	if(%obj.armed == 1) {
		%obj.static = 1;
		%obj.playThread(1, stuck);
		if (%col.getDatablock() !$= "") {
			if (%col.getDatablock().classname $= "armor") {
				tbmcollison(%obj,%obj,%col,%fade,%pos,%normal);
				%obj.delete();
				return;
			}
		}
		//%obj.setTransform(vectorAdd(%obj.position, vScale(%obj.getScale(), "0 0 -1"))); //I don't want to use raycasts or normal vector checking just yet.
		%obj.armed = 0;
		cancel(%obj.delsch);
		cancel(%obj.collisionSchedule);
		%obj.schedule($ItemTime, delete);
	}
}

function ThrowAxeImage::onPreFire(%this, %obj, %slot)
{
	//messageAll( 'MsgClient', 'sword prefired!!!');
	//Parent::onFire(%this, %obj, %slot);
	%obj.playthread(2, armattack);
}

function ThrowAxeImage::onFire(%this, %obj, %slot)
{
	%player = %obj;   //Stop assuming this is a player, dammit
	%item = nametoid(ThrowAxe); //this is ripped right out of Loudhailer.cs, I hope MCP doesn't mind.
	if(isObject(%item) && isObject(%player))
	{
		//throw the item
		%thrownItem = new (item)()
		{
			datablock = %item;
                        directDamage = %item.directDamage;
			sourceObject = %player;
			sourceSlot = %slot;
			client = %player.client;
		};
		MissionCleanup.add(%thrownItem);
		%thrownItem.damagetype = %item.damageType;
		%thrownItem.deathAnim = "axe";
                %thrownItem.setScale(%player.getScale());
		%thrownItem.setTransform(vectorAdd(%player.getMuzzlePoint(0), %player.getMuzzleVector(0)) SPC rotFromTransform(%player.getTransform()));
		%thrownItem.setVelocity(vScale(vectorAdd(VectorScale(%player.getMuzzleVector(0), %item.muzzleVelocity), %obj.getVelocity()), %player.getScale())); //I have my doubts about scaling its speed by the player's scale, but whatever
		%thrownItem.setCollisionTimeout(%player);
		%thrownItem.armed = 1;
		%thrownItem.playthread(1, twirl);
		%thrownItem.delsch = %thrownItem.schedule(3000, delete);
		%thrownItem.collisionSchedule = %item.schedule(10, collisionCheck, %thrownItem);
         }
}

function ThrowAxe::collisionCheck(%this, %obj)
{
	if(!isObject(%obj))
		return;
	if(%obj.getVelocity() $= "0 0 0")
		%this.onCollision(%obj, %obj, 0, %obj.getPosition(), "0 0 0");
	else
		%obj.collisionSchedule = %this.schedule(10, collisionCheck, %obj);
}

function ThrowAxeImage::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
	//messageAll( 'MsgClient', 'stopfire');
}