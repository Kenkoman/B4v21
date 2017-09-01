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
	%part[15] = nametoid(bloodsplash);
	%part[16] = nametoid(bloodsplash);
	%part[17] = nametoid(bloodsplash);
	%part[18] = nametoid(bloodsplash);
	%part[19] = nametoid(bloodsplash);
	%part[20] = nametoid(bloodsplash);
	%part[21] = nametoid(bloodsplash);
	%part[22] = nametoid(bloodsplash);
	%part[23] = nametoid(bloodsplash);
	%part[24] = nametoid(bloodsplash);
	%part[25] = nametoid(bloodsplash);
	%part[26] = nametoid(bloodsplash);
	%part[27] = nametoid(bloodsplash);
	%part[28] = nametoid(bloodsplash);
	%part[29] = nametoid(bloodsplash);
	%part[30] = nametoid(bloodsplash);
	%part[31] = nametoid(bloodsplash);
	%part[32] = nametoid(bloodsplash);
	%part[33] = nametoid(bloodsplash);
	%part[34] = nametoid(bloodsplash);
	%part[35] = nametoid(bloodsplash);
	%part[36] = nametoid(bloodsplash);
	%part[37] = nametoid(bloodsplash);
	%part[38] = nametoid(bloodsplash);
        %velc[0] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[1] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[2] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[3] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[4] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[5] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[6] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[7] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[8] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[9] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[10] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[11] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[12] = getrandom(-3,3)@" "@getrandom(-3,3)@" 1";
        %velc[13] = getrandom(-3,3)@" "@getrandom(-3,3)@" 1";
        %velc[14] = getrandom(-3,3)@" "@getrandom(-3,3)@" 1";
        %velc[15] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[16] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[17] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[18] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[19] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[20] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[21] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[22] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[23] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[24] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[25] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[26] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[27] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[28] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[29] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[30] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[31] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[32] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[33] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[34] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[35] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[36] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[37] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[38] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
	%muzzlepoint = %player.getposition();
	%muzzlevector = %player.getEyeVector();
	%muzzlepoint = VectorAdd(%muzzlepoint, %muzzleVector);
	%playerRot = rotFromTransform(%player.getTransform());
     for (%i=0;%i<39;%i++) {
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
	%part[15] = nametoid(bloodsplash);
	%part[16] = nametoid(bloodsplash);
	%part[17] = nametoid(bloodsplash);
	%part[18] = nametoid(bloodsplash);
	%part[19] = nametoid(bloodsplash);
	%part[20] = nametoid(bloodsplash);
	%part[21] = nametoid(bloodsplash);
	%part[22] = nametoid(bloodsplash);
	%part[23] = nametoid(bloodsplash);
	%part[24] = nametoid(bloodsplash);
	%part[25] = nametoid(bloodsplash);
	%part[26] = nametoid(bloodsplash);
	%part[27] = nametoid(bloodsplash);
	%part[28] = nametoid(bloodsplash);
	%part[29] = nametoid(bloodsplash);
	%part[30] = nametoid(bloodsplash);
	%part[31] = nametoid(bloodsplash);
	%part[32] = nametoid(bloodsplash);
	%part[33] = nametoid(bloodsplash);
	%part[34] = nametoid(bloodsplash);
	%part[35] = nametoid(bloodsplash);
	%part[36] = nametoid(bloodsplash);
	%part[37] = nametoid(bloodsplash);
	%part[38] = nametoid(bloodsplash);
        %velc[0] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[1] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[2] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[3] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[4] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[5] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[6] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[7] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[8] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[9] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[10] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[11] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[12] = getrandom(-3,3)@" "@getrandom(-3,3)@" 1";
        %velc[13] = getrandom(-3,3)@" "@getrandom(-3,3)@" 1";
        %velc[14] = getrandom(-3,3)@" "@getrandom(-3,3)@" 1";
        %velc[15] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[16] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[17] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[18] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[19] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[20] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[21] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[22] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[23] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[24] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[25] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[26] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[27] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[28] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[29] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[30] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[31] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[32] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[33] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[34] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[35] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[36] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[37] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
        %velc[38] = getrandom(-20,20)@" "@getrandom(-20,20)@" "@getrandom(0,20);
	%muzzlepoint = %obj.getposition();
	%muzzlevector = %obj.getEyeVector();
	%muzzlepoint = VectorAdd(%muzzlepoint, %muzzleVector);
	%playerRot = rotFromTransform(%obj.getTransform());
     for (%i=0;%i<39;%i++) {
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
