function Gobprojectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%brickdamage,%damage) {
    if(%col.getType() == 67108869)
	   return;
  if (%obj.client.isadmin)
    if (%col.getClassName() $= "AIPlayer") {
      %col.damage(%obj,%pos,100,"builshit");
      MessageAll(%client, '\c3Nice shot master %1', %obj.client.name);
      }
    if($gobblesdm && $gobblesdm.mode != 1){
            if(%col.nodestroy == 1)
            return;
            //if(%this.damageradius > 0)
            //gobsradiusDamage(%this,%obj,%col,%fade,%pos,%normal,%brickdamage,%damage);
            if(%col.brickhealth <= 0)
            %col.oldcolor = %col.getskinname();
        if(%col.getdatablock().classname $= "brick" &&  %col.brickdestroyed != 1)
            %col.brickhealth+=%brickdamage;
            calcbrickskin(%col);
            if(%col.brickhealth>=100 &&  %col.brickdestroyed != 1) {
                %col.brickdestroyed = 1;
                killbrick(%obj.client,%col,1);
                schedule($brickrespawn,0,respawnbrick,%obj.client,%col.getdatablock(),%col.gettransform(),%col.getscale(),%col.oldcolor,%col.getshapeName());
                }
}
    if($gobblesdm)
        if (%col.getClassname() $= "Player")
            %col.damage(%obj,%pos,%damage,"Gobattack");
}
//function gobsradiusDamage(%this,%obj,%col,%fade,%pos,%normal,%brickdamage,%damage) {
//  InitContainerRadiusSearch(%pos, %this.damageRadius, $TypeMasks::StaticShapeObjecttype);
//  while( (%object = containerSearchNext()) != 0 ) {
//    Gobprojectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%brickdamage,%damage);
//  }
//}
function calcbrickskin(%brick) {
            if(%brick.brickhealth <= 0  && %brick.oldcolor !$= "")
            %brick.setskinname(%brick.oldcolor);
            if(%brick.brickhealth==20)
            %brick.setskinname('coolyellow');
            if(%brick.brickhealth==30)
            %brick.setskinname('yellow');
            if(%brick.brickhealth==40)
            %brick.setskinname('lightorangebrown');
            if(%brick.brickhealth==50)
            %brick.setskinname('yellow');
            if(%brick.brickhealth==60)
            %brick.setskinname('flameredorange');
            if(%brick.brickhealth==80)
            %brick.setskinname('rust');
            if(%brick.brickhealth==90)
            %brick.setskinname('redflipflop');
}
function healbrick(%brick) {
if(%brick.brickhealth >= 10)
%brick.brickhealth-=10;
calcbrickskin(%brick);
}
function healallbricks() {
for(%i = 0; %i < MissionCleanup.GetCount(); %i++)
{
   %brick = MissionCleanup.getObject(%i);
   if(%brick.getClassName() !$= "StaticShape")
        continue;
   if(%brick.getdatablock().classname $= "brick")
   healbrick(%brick);
   }
$brickheal = schedule(2000,0,healallbricks);
}
function respawnbrick(%client,%datablock,%pos,%scale,%color,%name) {
%brick = new StaticShape() {
      dataBlock = %datablock;
      owner = getrawip(%client);
      up[0] = 0;
      down[0] = 0;
};
%brick.startfade(0,0,true);
%brick.startfade(1000,0,false);
MissionCleanup.add(%brick);
%brick.settransform(%pos);
%brick.setscale(%scale);
%brick.setskinname(%color);
%brick.setshapename(%name);
}
function setupgobblesdm(%respawntime,%mode) {
if(%mode == -1) {
MessageAll('msggobblesdm', "Gobbles DM has now ended!");
$gobblesdm = 0;
cancel($brickheal);
return;
}
cancel($brickheal);
healallbricks();
$gobblesdm = new ScriptObject() {
mode = %mode;
};
$brickrespawn = %respawntime;
MessageAll('msggobblesdm', "Gobbles DM has now begun!");
}
function execgobblesdm() {
execsth("bow.cs");
execsth("gobblesdm.cs");
execsth("spear.cs");
execsth("sword.cs");
}

function spearProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal) {
    if($gobblesdm)
    Gobprojectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,100,25);
}
function arrowProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal) {
    if(%col.getType() == 67108869)
	   return;
  if (%obj.client.isadmin)
    if (%col.getClassName() $= "AIPlayer") {
      %col.damage(%obj,%pos,100,"builshit");
      MessageAll(%client, '\c3Nice shot master %1', %obj.client.name);
      }
    if($gobblesdm)
    Gobprojectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,10,3);
}
function swordProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal) {
    if($gobblesdm)
    Gobprojectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,20,25);
}
