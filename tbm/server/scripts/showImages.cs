//showimages.cs

////////
//Back//
////////
datablock ShapeBaseImageData(plateMailShowImage) {
	shapeFile = "~/data/shapes/player/back/armor.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 2;
	headUp = true;
	className = "ItemImage";
};
datablock ShapeBaseImageData(bucketPackShowImage) {
	shapeFile = "~/data/shapes/player/back/bucketPack.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 2;
	headUp = true;
	className = "ItemImage";
};
datablock ShapeBaseImageData(capeShowImage) {
	shapeFile = "~/data/shapes/player/back/cape.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 2;
	headUp = true;
	className = "ItemImage";
};
datablock ShapeBaseImageData(cloakShowImage) {
	shapeFile = "~/data/shapes/player/back/cloak.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 2;
	headUp = true;
	className = "ItemImage";
};
datablock ShapeBaseImageData(epauletsShowImage) {
	shapeFile = "~/data/shapes/player/back/epaulets.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 2;
	headUp = true;
	className = "ItemImage";
};
datablock ShapeBaseImageData(packShowImage) {
	shapeFile = "~/data/shapes/player/back/pack.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 2;
	headUp = true;
	className = "ItemImage";
};
datablock ShapeBaseImageData(quiverShowImage) {
	shapeFile = "~/data/shapes/player/back/quiver.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 2;
	headUp = true;
	className = "ItemImage";
};
datablock ShapeBaseImageData(samarmShowImage) {
	shapeFile = "~/data/shapes/player/back/samurai.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 2;
	headUp = true;
	className = "ItemImage";
};
datablock ShapeBaseImageData(scubatankShowImage) {
	shapeFile = "~/data/shapes/player/back/scubatank.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 2;
	headUp = true;
	className = "ItemImage";
};
datablock ShapeBaseImageData(airTankShowImage) {
	shapeFile = "~/data/shapes/player/back/tank.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 2;
	headUp = true;
	className = "ItemImage";
};
datablock ShapeBaseImageData(R2ShowImage) {
	shapeFile = "~/data/shapes/R2/R2.dts";
	cloaktexture = "~/data/shapes/base.transmore";
	mountPoint = -1;
	className = "ItemImage";
	cloakable = false;
	eyeoffset = "0 -2 0";
};

function R2ShowImage::onMount(%this, %obj) {
%obj.setCloaked(1);
}

function R2ShowImage::onUnmount(%this, %obj) {
%obj.setCloaked(0);
}

////////
//hats//
////////
datablock ShapeBaseImageData(arcShowImage) {
	shapeFile = "~/data/shapes/player/hats/ARCtrooper.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
	  offset = ".01 .02 -.12";
	className = "ItemImage";
	eyeOffset = "0 -2 0";
};
datablock ShapeBaseImageData(armyShowImage) {
	shapeFile = "~/data/shapes/FeedBack/army.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
	className = "ItemImage";
	eyeOffset = "0 -2 0";
};
datablock ShapeBaseImageData(aviatorShowImage) {
	shapeFile = "~/data/shapes/player/hats/aviatorcap.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
	className = "ItemImage";
        offset = "0 0 0.04"; 
	eyeOffset = "0 -2 0";
};
datablock ShapeBaseImageData(capShowImage) {
	shapeFile = "~/data/shapes/player/hats/cap.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
	rotation = "0 0 1 180";
	className = "ItemImage";
	eyeOffset = "0 -2 0";
};
datablock ShapeBaseImageData(cowboyShowImage) {
	shapeFile = "~/data/shapes/player/hats/cowboy.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
	className = "ItemImage";
	eyeOffset = "0 -2 0";
};
datablock ShapeBaseImageData(crownShowImage) {
	shapeFile = "~/data/shapes/player/hats/crown.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
	className = "ItemImage";
	eyeOffset = "0 -2 0";
};
datablock ShapeBaseImageData(dmaulShowImage) {
	shapeFile = "~/data/shapes/player/hats/darthmaul.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
	className = "ItemImage";
	eyeOffset = "0 -2 0";
};
datablock ShapeBaseImageData(DictatorHatShowImage) {
	shapeFile = "~/data/shapes/player/hats/DictatorHat.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
	className = "ItemImage";
	eyeOffset = "0 -2 0";
};
datablock ShapeBaseImageData(darthShowImage) {
	shapeFile = "~/data/shapes/player/hats/dv.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
	className = "ItemImage";
	eyeOffset = "0 -2 0";
};
datablock ShapeBaseImageData(femhair2ShowImage) {
	shapeFile = "~/data/shapes/player/hats/femhair2.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
	className = "ItemImage";
	eyeOffset = "0 -2 0";
};
datablock ShapeBaseImageData(femhairShowImage) {
	shapeFile = "~/data/shapes/player/hats/femhair.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
	className = "ItemImage";
	eyeOffset = "0 -2 0";
};
datablock ShapeBaseImageData(firemanShowImage) {
	shapeFile = "~/data/shapes/player/hats/firehat.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
	className = "ItemImage";
	eyeOffset = "0 -2 0";
};
datablock ShapeBaseImageData(froShowImage) {
	shapeFile = "~/data/shapes/player/hats/fro.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
	className = "ItemImage";
	eyeOffset = "0 -2 0";
};
datablock ShapeBaseImageData(hairShowImage) {
	shapeFile = "~/data/shapes/player/hats/hair.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
	className = "ItemImage";
	eyeOffset = "0 -2 0";
};
datablock ShapeBaseImageData(helmetShowImage) {
	shapeFile = "~/data/shapes/player/hats/helmet.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
	className = "ItemImage";
	eyeOffset = "0 -2 0";
};
datablock ShapeBaseImageData(hornsShowImage) {
	shapeFile = "~/data/shapes/player/hats/horns.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
        offset = "0 0 0.15"; 
        rotation = "0 0 2"; 
	className = "ItemImage";
	eyeOffset = "0 -2 0";
};
datablock ShapeBaseImageData(islandShowImage) {
	shapeFile = "~/data/shapes/player/hats/islandermask.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
	className = "ItemImage";
	eyeOffset = "0 -2 0";
};
datablock ShapeBaseImageData(jediShowImage) {
	shapeFile = "~/data/shapes/player/hats/jedihood.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
	className = "ItemImage";
	eyeOffset = "0 -2 0";
};
datablock ShapeBaseImageData(mandalorianShowImage) {
	shapeFile = "~/data/shapes/player/hats/mandalorian.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
        offset = ".01 .02 -.12";
	className = "ItemImage";
	eyeOffset = "0 -2 0";
};
datablock ShapeBaseImageData(navyShowImage) {
	shapeFile = "~/data/shapes/player/hats/navy.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
	className = "ItemImage";
	eyeOffset = "0 -2 0";
};
datablock ShapeBaseImageData(ninjamaskShowImage) {
	shapeFile = "~/data/shapes/player/hats/ninjamask2.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
	className = "ItemImage";
	eyeOffset = "0 -2 0";
};
datablock ShapeBaseImageData(PirateShowImage) {
	shapeFile = "~/data/shapes/player/hats/Pirate.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
	className = "ItemImage";
	eyeOffset = "0 -2 0";
};
datablock ShapeBaseImageData(pointyHelmetShowImage) {
	shapeFile = "~/data/shapes/player/hats/pointyHelmet.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
	className = "ItemImage";
	eyeOffset = "0 -2 0";
};
datablock ShapeBaseImageData(policeShowImage) {
	shapeFile = "~/data/shapes/player/hats/police.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
	className = "ItemImage";
	eyeOffset = "0 -2 0";
};
datablock ShapeBaseImageData(samhelmShowImage) {
	shapeFile = "~/data/shapes/player/hats/samhelm.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
	className = "ItemImage";
	eyeOffset = "0 -2 0";
};
datablock ShapeBaseImageData(scoutHatShowImage) {
	shapeFile = "~/data/shapes/player/hats/scoutHat.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
	className = "ItemImage";
	eyeOffset = "0 -2 0";
};
//this is now an accent
datablock ShapeBaseImageData(snorkelShowImage) {
	shapeFile = "~/data/shapes/player/hats/snorkelmask.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
	className = "ItemImage";
	eyeOffset = "0 -2 0";
};
datablock ShapeBaseImageData(StormShowImage) {
	shapeFile = "~/data/shapes/player/hats/storm2.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
	  offset = ".01 .02 -.12";
	className = "ItemImage";
	eyeOffset = "0 -2 0";
};
datablock ShapeBaseImageData(spidyShowImage) {
	shapeFile = "~/data/shapes/player/hats/spidy.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
	className = "ItemImage";
	eyeOffset = "0 -2 0";
};
datablock ShapeBaseImageData(tophatShowImage) {
	shapeFile = "~/data/shapes/player/hats/tophat.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
        offset = "0 0 0.15"; 
	className = "ItemImage";
	eyeOffset = "0 -2 0";
};
datablock ShapeBaseImageData(WizhatShowImage) {
	shapeFile = "~/data/shapes/player/hats/wizhat.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
	className = "ItemImage";
	eyeOffset = "0 -2 0";
};
datablock ShapeBaseImageData(batmanShowImage) {
	shapeFile = "~/data/shapes/player/hats/batman.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
	className = "ItemImage";
	eyeOffset = "0 -2 0";
};
datablock ShapeBaseImageData(BurgerkingShowImage) {
	shapeFile = "~/data/shapes/player/hats/burgerking.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
	className = "ItemImage";
	eyeOffset = "0 -2 0";
        offset = "0 0.25 0.25"; 
};
datablock ShapeBaseImageData(cthelmShowImage) {
	shapeFile = "~/data/shapes/player/hats/clone.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
	className = "ItemImage";
        offset = "0 0 -.1"; 
	eyeOffset = "0 -2 0";
};
//Added by DShiz 10/28/08
datablock ShapeBaseImageData(FlattopShowImage) {
	shapeFile = "~/data/shapes/player/hats/flattop2.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
	className = "ItemImage";
	eyeOffset = "0 -2 0";
};
datablock ShapeBaseImageData(PonytailShowImage) {
	shapeFile = "~/data/shapes/player/hats/ponytail2.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
	className = "ItemImage";
	eyeOffset = "0 -2 0";
};
//Droid heads
datablock ShapeBaseImageData(DroidBatHeadShowImage) {
	shapeFile = "~/data/shapes/player/mDroid/battlehead.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
	className = "ItemImage";
	eyeOffset = "0 -2 0";
	offset = "0 0 -0.6";
};
datablock ShapeBaseImageData(DroidHeadFlatShowImage) {
	shapeFile = "~/data/shapes/player/mDroid/flathead.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
	className = "ItemImage";
	eyeOffset = "0 -2 0";
	offset = "0 0 -0.6";
};
datablock ShapeBaseImageData(DroidIGHeadShowImage) {
	shapeFile = "~/data/shapes/player/mDroid/IGhead.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
	className = "ItemImage";
	eyeOffset = "0 -2 0";
	offset = "0 0 -0.6";
};

///////////
//Accents//
///////////
datablock ShapeBaseImageData(emoteShowImage) {
	shapeFile = "~/data/shapes/Elrune/Emotes/status.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
        offset = "0 0 1.8"; 
	className = "ItemImage";
	eyeOffset = "0 -2 0";

	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.5;
	stateTransitionOnTimeout[0]      = "Activate";
	stateSequence[0]		 = "spinning";
};

function emoteShowImage::onUnmount(%this, %obj) {
if(%obj.getClassName() $= "Player") {
  %obj.emote = "";
  commandToClient(%obj.client, 'BottomPrint', "\c2You are displaying \c3No\c2 emote.", 3, 1); 
}
}

datablock ShapeBaseImageData(avgooglesShowImage) {
	shapeFile = "~/data/shapes/player/accent/aviatorgoogles.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
        offset = "0 0 0"; 
	className = "ItemImage";
	eyeOffset = "0 -2 0";
};
datablock ShapeBaseImageData(islandaccShowImage) {
	shapeFile = "~/data/shapes/player/accent/islandermaskacc.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
	className = "ItemImage";
	eyeOffset = "0 -2 0";
};
datablock ShapeBaseImageData(shornShowImage) {
	shapeFile = "~/data/shapes/player/accent/samacc.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
	className = "ItemImage";
	eyeOffset = "0 -2 0";
};
datablock ShapeBaseImageData(bandShowImage) {
	shapeFile = "~/data/shapes/player/accent/tophatband.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
	offset = "0 0 0.22";
	className = "ItemImage";
	eyeOffset = "0 -2 0";
}; 
datablock ShapeBaseImageData(triPlumeShowImage) {
	shapeFile = "~/data/shapes/player/accent/triplume.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
	className = "ItemImage";
	eyeOffset = "0 -2 0";
};
datablock ShapeBaseImageData(visorShowImage) {
	shapeFile = "~/data/shapes/player/accent/visor.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 6;
	className = "ItemImage";
	eyeOffset = "0 -2 0";
}; 

///////////////////
//left hand stuff//
///////////////////
datablock ShapeBaseImageData(BeardShowImage) {
	shapeFile = "~/data/shapes/player/left/beard.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
	className = "ItemImage";
	eyeOffset = "0 -2 0";
};
datablock ShapeBaseImageData(birdShowImage) {
	shapeFile = "~/data/shapes/player/left/bird2.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 2;
	offset = "0.45 0.2 0.2";
	className = "ItemImage";
	eyeOffset = "0 -2 0";
};
datablock ShapeBaseImageData(gobletShowImage) {
	shapeFile = "~/data/shapes/player/left/goblet.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 1;
	className = "ItemImage";
	Offset = "0 0 0.1";
};
datablock ShapeBaseImageData(broomShowImage) {
	shapeFile = "~/data/shapes/player/left/pushbroom.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 1;
	offset = "0 0.05 1.2";
	rotation = "-1 0 0 180";
	className = "ItemImage";
	eyeOffset = "0 -2 0";
};
datablock ShapeBaseImageData(shieldShowImage) {
	shapeFile = "~/data/shapes/player/left/shield.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 1;
	className = "ItemImage";
	eyeOffset = "0 -2 0";
	Offset = "0 0 0.1";
};
datablock ShapeBaseImageData(sickleShowImage) {
	shapeFile = "~/data/shapes/player/left/sickle.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 1;
	offset = "0 0 -0.4";
	rotation = "1 0 0 12";
	className = "ItemImage";
	eyeOffset = "0 -2 0";
};
datablock ShapeBaseImageData(whipShowImage) {
	shapeFile = "~/data/shapes/player/left/whip.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 1;
	scale = '5 5 5';
	className = "ItemImage";
	eyeOffset = "0 -2 0";
	Offset = "0 0.03 0";
};
datablock ShapeBaseImageData(boobsShowImage) {
	shapeFile = "~/data/shapes/bricks/boobs.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 2;
	className = "ItemImage";
	Offset = "0 -0.01 0";
};
datablock ShapeBaseImageData(briefcaseShowImage) {
	shapeFile = "~/data/shapes/player/left/briefcase.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 1;
	className = "ItemImage";
	eyeOffset = "0 -2 0";
	Offset = "0 0 0.1";
};
datablock ShapeBaseImageData(mugShowImage) {
	shapeFile = "~/data/shapes/player/left/mug.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 1;
	className = "ItemImage";
	armready = true;
	Offset = "0 0 0.1";
	eyeOffset = "-1.1 0.68 -0.555";
};
function gobletShowImage::onMount(%this,%obj)
{
	%rightImage = %obj.getMountedImage($RightHandSlot);

	if(%rightImage)
	{
		if(%rightImage.armready)
		{
			%obj.playthread(1, armreadyboth);
			return;
		}
	}
	%obj.playthread(1, armReadyLeft);
}

function gobletShowImage::onUnmount(%this,%obj)
{
	%rightImage = %obj.getMountedImage($RightHandSlot);

	if(%rightImage)
	{
		if(%rightImage.armready)
		{
			%obj.playthread(1, armreadyright);
			return;
		}
	}
	%obj.playthread(1, root);
}
function mugShowImage::onMount(%this,%obj)
{
	%rightImage = %obj.getMountedImage($RightHandSlot);

	if(%rightImage)
	{
		if(%rightImage.armready)
		{
			%obj.playthread(1, armreadyboth);
			return;
		}
	}
	%obj.playthread(1, armReadyLeft);
}

function mugShowImage::onUnmount(%this,%obj)
{
	%rightImage = %obj.getMountedImage($RightHandSlot);

	if(%rightImage)
	{
		if(%rightImage.armready)
		{
			%obj.playthread(1, armreadyright);
			return;
		}
	}
	%obj.playthread(1, root);
}
/////////////////////////////////////////////////////////
datablock ShapeBaseImageData(chestShowImage) {
	shapeFile = "~/data/shapes/player/chest/chest.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 2;
        offset = "0 -0.01 0"; 
	className = "ItemImage";
};
datablock ShapeBaseImageData(faceplateShowImage) {
	shapeFile = "~/data/shapes/player/face/faceplate.dts";
	cloaktexture = "~/data/specialfx/cloakTexture";
	mountPoint = 5;
        offset = "0 0.283 -0.27"; 
	className = "ItemImage";
};