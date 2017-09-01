//SUPER ULTRA MEGA POWER!!! The ultimate in overcompensating -DShiznit
//more to come when I finish writing this...
function ServerCmdSUMP(%client) { 
if(!$Pref::Server::SUMP)
  return;
%x = getword(%client.player.gettransform(),0);
%y = getword(%client.player.gettransform(),1);
%z = getword(%client.player.gettransform(),2);

//time to set up my array of sexiness:
%tit[0] = %x + 5 SPC %y + 0 SPC %z;
%tit[1] = %x + 0 SPC %y + 5 SPC %z;
%tit[2] = %x - 5 SPC %y + 0 SPC %z;
%tit[3] = %x + 0 SPC %y - 5 SPC %z;
%tit[4] = %x + 3.5 SPC %y + 3.5 SPC %z;
%tit[5] = %x - 3.5 SPC %y + 3.5 SPC %z;
%tit[6] = %x - 3.5 SPC %y - 3.5 SPC %z;
%tit[7] = %x + 3.5 SPC %y - 3.5 SPC %z;
%tit[8] = %x + 4 SPC %y + 3 SPC %z;
%tit[9] = %x + 3 SPC %y + 4 SPC %z;
%tit[10] = %x - 4 SPC %y + 3 SPC %z;
%tit[11] = %x - 3 SPC %y + 4 SPC %z;
%tit[12] = %x + 4 SPC %y - 3 SPC %z;
%tit[13] = %x + 3 SPC %y - 4 SPC %z;
%tit[14] = %x - 4 SPC %y - 3 SPC %z;
%tit[15] = %x - 3 SPC %y - 4 SPC %z;

%pos = %tit[getrandom(0,15)];

if($shitgotreal == 0)
      {
	   %p = new (projectile)()
         {
            dataBlock        = medgunprojectile;
            initialVelocity  = "0 0 100"; //this should look fucking awesome
            initialPosition  = %pos;
};
         MissionCleanup.add(%p);
  schedule(50,0,ServerCmdSUMP,%client);
	}
}

//This function ends the awesomeness and prepares for another(yes I stole from my own hellrain script)
function endSUMP(){
$shitgotreal = 1;
schedule(1000,0,resetSUMP);
}

function resetSUMP(){
$shitgotreal = 0;
}