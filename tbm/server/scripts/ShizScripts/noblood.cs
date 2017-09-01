datablock ItemData(skelarmr)
{
   shapeFile = "~/data/shapes/right-arm.dts";
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
   shapeFile = "~/data/shapes/left-arm.dts";
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
   shapeFile = "~/data/shapes/leg.dts";
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
   shapeFile = "~/data/shapes/head.dts";
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
   shapeFile = "~/data/shapes/torso.dts";
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
        %velc[0] = "-10 -7 15";
        %velc[1] = "0 15 10";
        %velc[2] = "8 -10 10";
        %velc[3] = "15 -4 1";
        %velc[4] = "-10 8 1";
        %velc[5] = "2 2 5";
	%muzzlepoint = %player.getposition();
	%muzzlevector = %player.getEyeVector();
	%muzzlepoint = VectorAdd(%muzzlepoint, %muzzleVector);
	%playerRot = rotFromTransform(%player.getTransform());
     for (%i=0;%i<6;%i++) {
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
        %velc[0] = "-10 -7 15";
        %velc[1] = "0 15 10";
        %velc[2] = "8 -10 10";
        %velc[3] = "15 -4 1";
        %velc[4] = "-10 8 1";
        %velc[5] = "2 2 5";
	%muzzlepoint = %obj.getposition();
	%muzzlevector = %obj.getEyeVector();
	%muzzlepoint = VectorAdd(%muzzlepoint, %muzzleVector);
	%playerRot = rotFromTransform(%obj.getTransform());
     for (%i=0;%i<6;%i++) {
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
