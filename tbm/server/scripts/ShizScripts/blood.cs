datablock ItemData(skelarmr)
{
   shapeFile = "~/data/shapes/gore2/right-arm.dts";
   category = "debris";
   mass = 1;
   friction = 1;
   elasticity = 0.5;
   rotate = true;
   maxInventory = 0;
   pickUpName = '';
   invName = '';
};

datablock ItemData(skelarml)
{
   shapeFile = "~/data/shapes/gore2/left-arm.dts";
   category = "debris";
   mass = 1;
   friction = 1;
   elasticity = 0.5;
   rotate = true;
   maxInventory = 0;
   pickUpName = '';
   invName = '';
};

datablock ItemData(skelleg)
{
   shapeFile = "~/data/shapes/gore2/leg.dts";
   category = "debris";
   mass = 1;
   friction = 1;
   elasticity = 0.5;
   rotate = true;
   maxInventory = 0;
   pickUpName = '';
   invName = '';
};

datablock ItemData(skelhead)
{
   shapeFile = "~/data/shapes/gore2/head.dts";
   category = "debris";
   mass = 1;
   friction = 1;
   elasticity = 0.5;
   rotate = true;
   maxInventory = 0;
   pickUpName = '';
   invName = '';
};

datablock ItemData(skeltorso)
{
   shapeFile = "~/data/shapes/gore2/torso.dts";
   category = "debris";
   mass = 1;
   friction = 1;
   elasticity = 0.5;
   rotate = true;
   maxInventory = 0;
   pickUpName = '';
   invName = '';
};

function tbmplayerdebris(%player) {
	%part[0] = nametoid(skelhead);
	%part[1] = nametoid(skelarmr);
	%part[2] = nametoid(skelarml);
	%part[3] = nametoid(skelleg);
	%part[4] = nametoid(skelleg);
	%part[5] = nametoid(skeltorso);
	%part[6] = nametoid(bloodsplash);
	%part[7] = nametoid(bloodsplash);
	%part[8] = nametoid(bloodsplash);
	%part[9] = nametoid(bloodsplash);
	%part[10] = nametoid(bloodsplash);
	%part[11] = nametoid(bloodsplash);
	%part[12] = nametoid(bloodsplat);
	%part[13] = nametoid(bloodsplat);
	%part[14] = nametoid(bloodsplat);
        %velc[0] = getrandom(-10,10)@" "@getrandom(-10,10)@" "@getrandom(0,10);
        %velc[1] = getrandom(-10,10)@" "@getrandom(-10,10)@" "@getrandom(0,10);
        %velc[2] = getrandom(-10,10)@" "@getrandom(-10,10)@" "@getrandom(0,10);
        %velc[3] = getrandom(-10,10)@" "@getrandom(-10,10)@" "@getrandom(0,10);
        %velc[4] = getrandom(-10,10)@" "@getrandom(-10,10)@" "@getrandom(0,10);
        %velc[5] = getrandom(-10,10)@" "@getrandom(-10,10)@" "@getrandom(0,10);
        %velc[6] = getrandom(-10,10)@" "@getrandom(-10,10)@" "@getrandom(0,10);
        %velc[7] = getrandom(-10,10)@" "@getrandom(-10,10)@" "@getrandom(0,10);
        %velc[8] = getrandom(-10,10)@" "@getrandom(-10,10)@" "@getrandom(0,10);
        %velc[9] = getrandom(-10,10)@" "@getrandom(-10,10)@" "@getrandom(0,10);
        %velc[10] = getrandom(-10,10)@" "@getrandom(-10,10)@" "@getrandom(0,10);
        %velc[11] = getrandom(-10,10)@" "@getrandom(-10,10)@" "@getrandom(0,10);
        %velc[12] = getrandom(-3,3)@" "@getrandom(-3,3)@" 1";
        %velc[13] = getrandom(-3,3)@" "@getrandom(-3,3)@" 1";
        %velc[14] = getrandom(-3,3)@" "@getrandom(-3,3)@" 1";
	%muzzlepoint = %player.getposition();
	%muzzlevector = %player.getEyeVector();
	%muzzlepoint = VectorAdd(%muzzlepoint, %muzzleVector);
	%playerRot = rotFromTransform(%player.getTransform());
     for (%i=0;%i<15;%i++) {
	%thrownItem = new (item)()
		{
			datablock = %part[%i];			
		};
	%thrownItem.setScale(%player.getScale());
		MissionCleanup.add(%thrownItem);
                %thrownItem.client = %player.client;
	%thrownItem.settransform(%muzzlepoint @ " " @ %playerRot);
	%objectVelocity = %player.getVelocity();
	%muzzleVelocity = VectorAdd(%velc[%i],%objectVelocity);
	%thrownItem.setVelocity(%muzzleVelocity);
	%thrownItem.schedule(10000, delete);
        %thrownItem.setCollisionTimeout(%player);
        }
}

function tbmbotdebris(%obj) {
	%part[0] = nametoid(skelhead);
	%part[1] = nametoid(skelarmr);
	%part[2] = nametoid(skelarml);
	%part[3] = nametoid(skelleg);
	%part[4] = nametoid(skelleg);
	%part[5] = nametoid(skeltorso);
	%part[6] = nametoid(bloodsplash);
	%part[7] = nametoid(bloodsplash);
	%part[8] = nametoid(bloodsplash);
	%part[9] = nametoid(bloodsplash);
	%part[10] = nametoid(bloodsplash);
	%part[11] = nametoid(bloodsplash);
	%part[12] = nametoid(bloodsplat);
	%part[13] = nametoid(bloodsplat);
	%part[14] = nametoid(bloodsplat);
        %velc[0] = getrandom(-10,10)@" "@getrandom(-10,10)@" "@getrandom(0,10);
        %velc[1] = getrandom(-10,10)@" "@getrandom(-10,10)@" "@getrandom(0,10);
        %velc[2] = getrandom(-10,10)@" "@getrandom(-10,10)@" "@getrandom(0,10);
        %velc[3] = getrandom(-10,10)@" "@getrandom(-10,10)@" "@getrandom(0,10);
        %velc[4] = getrandom(-10,10)@" "@getrandom(-10,10)@" "@getrandom(0,10);
        %velc[5] = getrandom(-10,10)@" "@getrandom(-10,10)@" "@getrandom(0,10);
        %velc[6] = getrandom(-10,10)@" "@getrandom(-10,10)@" "@getrandom(0,10);
        %velc[7] = getrandom(-10,10)@" "@getrandom(-10,10)@" "@getrandom(0,10);
        %velc[8] = getrandom(-10,10)@" "@getrandom(-10,10)@" "@getrandom(0,10);
        %velc[9] = getrandom(-10,10)@" "@getrandom(-10,10)@" "@getrandom(0,10);
        %velc[10] = getrandom(-10,10)@" "@getrandom(-10,10)@" "@getrandom(0,10);
        %velc[11] = getrandom(-10,10)@" "@getrandom(-10,10)@" "@getrandom(0,10);
        %velc[12] = getrandom(-3,3)@" "@getrandom(-3,3)@" 1";
        %velc[13] = getrandom(-3,3)@" "@getrandom(-3,3)@" 1";
        %velc[14] = getrandom(-3,3)@" "@getrandom(-3,3)@" 1";
	%muzzlepoint = %obj.getposition();
	%muzzlevector = %obj.getEyeVector();
	%muzzlepoint = VectorAdd(%muzzlepoint, %muzzleVector);
	%playerRot = rotFromTransform(%obj.getTransform());
     for (%i=0;%i<15;%i++) {
	%thrownItem = new (item)()
		{
			datablock = %part[%i];			
		};
	%thrownItem.setScale(%obj.getScale());
		MissionCleanup.add(%thrownItem);
                %thrownItem.client = %obj.client;
	%thrownItem.settransform(%muzzlepoint @ " " @ %playerRot);
	%objectVelocity = %obj.getVelocity();
	%muzzleVelocity = VectorAdd(%velc[%i],%objectVelocity);
	%thrownItem.setVelocity(%muzzleVelocity);
	%thrownItem.schedule(10000, delete);
        %thrownItem.setCollisionTimeout(%obj);
        }
}
