function TBMPlayerOpGui::onWake(%this)
{
commandToServer('PlayerOpCam');
$TBMPlayeropGuiTog = 1;
}

function TBMPlayerOpGui::onSleep(%this)
{
$TBMPlayeropGuiTog = 0;
commandToServer('PlayerResetCam');
}

function color(%switch) {
  if( %switch == 1 ) {
    if( $pref::Color::skin++ > $LCTotal)
      $pref::Color::skin = 0;
    }
  else 
    if( $pref::Color::skin-- < 0 )
      $pref::Color::skin = $LCTotal;
  clientCmdUpdatePrefs();
}

function armsncrotch(%switch) {
  if( %switch == 1 ) {
    if( $pref::Accessory::Armscolor++ > $armsTot)
      $pref::Accessory::Armscolor = 0;
    }
  else
    if( $pref::Accessory::Armscolor-- < 0 )
      $pref::Accessory::Armscolor = $armsTot;
  clientCmdUpdatePrefs();
}

function head(%switch) {
  %count = ($armsColor[$Pref::Accessory::armscolor] !$= "Droid")? $hatsTot : $DHatsTot;
  if( %switch == 1 ) {
    if( $pref::Accessory::headCode++ > %count)
      $pref::Accessory::headCode = -1;
    }
  else 
    if( $pref::Accessory::headCode-- < -1 ) 
      $pref::Accessory::headCode = %count;
  clientCmdUpdatePrefs();
}

function headcolor(%switch) {
  if( %switch == 1 ) {
    if( $pref::Accessory::headColor++ > $LCTotal)
      $pref::Accessory::headColor = 0;
    }
  else
    if( $pref::Accessory::headColor-- < 0 )
      $pref::Accessory::headColor = $LCTotal;
  clientCmdUpdatePrefs();
}

function face(%switch) {
  for (%i=0; %i<=$Decals::face::total; %i++) {
    if ($pref::Decal::face $= $Decals::face[%i])
      %currentface=%i;
    }
  if( %switch == 1 ) {
    if( %currentface++ > $Decals::face::total)
      $pref::Decal::face = $Decals::face[0];
    else
      $pref::Decal::face = $Decals::face[%currentface];	
    }
  else
    if( %currentface-- < 0 )
      $pref::Decal::face = $Decals::face[$Decals::face::total];
    else
      $pref::Decal::face = $Decals::face[%currentface];	
  clientCmdUpdatePrefs();
}
function back(%switch) {
%count = ($armsColor[$Pref::Accessory::armscolor] !$= "Droid")? $backTot : $DbackTot;
  if( %switch == 1 ) {
    if( $pref::Accessory::BackCode++ > %count)
      $pref::Accessory::BackCode = -1;
    }
  else
    if( $pref::Accessory::BackCode-- < -1 )
      $pref::Accessory::BackCode = %count;
    clientCmdUpdatePrefs();
}
function backcolor(%switch) {
  if( %switch == 1 ) {
    if( $pref::Accessory::BackColor++ > $LCTotal) 
      $pref::Accessory::BackColor = 0;
    }
  else
    if( $pref::Accessory::BackColor-- < 0 )
      $pref::Accessory::BackColor = $LCTotal;
  clientCmdUpdatePrefs();
}
function chest(%switch) {
  for (%i=0; %i<=$Decals::chest::total; %i++) {
    if ($pref::Decal::chest $= $Decals::chest[%i])
      %currentchest=%i;
    }
  if( %switch == 1 ) {
    if( %currentchest++ > $Decals::chest::total)
      $pref::Decal::chest = $Decals::chest[0];
    else
      $pref::Decal::chest = $Decals::chest[%currentchest];	
    }
  else
    if( %currentchest-- < 0 )
      $pref::Decal::chest = $Decals::chest[$Decals::chest::total];
    else
      $pref::Decal::chest = $Decals::chest[%currentchest];	
  clientCmdUpdatePrefs();
}
function other(%switch) {
  if( %switch == 1 ) {
    if( $pref::Accessory::leftHandCode++ > 9) 
      $pref::Accessory::leftHandCode = -1;
    }
  else 
    if( $pref::Accessory::leftHandCode-- < -1 )
      $pref::Accessory::leftHandCode = 8;
  clientCmdUpdatePrefs();
}
function othercolor(%switch) {
  if( %switch == 1 ) {
    if ($pref::Accessory::leftHandCode == 0) {
      if( $pref::Accessory::leftHandColor++ > 0) 
        $pref::Accessory::leftHandColor = -4;
      }
    else
      if( $pref::Accessory::leftHandColor++ > $LCTotal)
        $pref::Accessory::leftHandColor = 0;
    }
  else {
    if ($pref::Accessory::leftHandCode == 0) {
      if( $pref::Accessory::leftHandColor-- < -4 )
        $pref::Accessory::leftHandColor = -1;
      }
    else
      if( $pref::Accessory::leftHandColor-- < 0 )
        $pref::Accessory::leftHandColor = $LCTotal;
    }
  clientCmdUpdatePrefs();
}
function accent(%switch) {
%count = ($armsColor[$Pref::Accessory::armscolor] !$= "Droid")? 7 : -1;
  if( %switch == 1 ) {
    if( $pref::Accessory::VisorCode++ > %count) 
      $pref::Accessory::VisorCode = -1;
    }
  else 
    if( $pref::Accessory::VisorCode-- < -1 )
      $pref::Accessory::VisorCode = %count;
  clientCmdUpdatePrefs();
}
function accentcolor(%switch) {
  if ($pref::Accessory::visorCode==0 || $pref::Accessory::visorCode==1)
    %climit=$LCTotal;
  else
    %climit=$LCTotal;
  if( %switch == 1 ) {
    if( $pref::Accessory::visorColor++ > %climit)
      $pref::Accessory::visorColor = 0;
    }
  else
     if( $pref::Accessory::visorColor-- < 0 )
	$pref::Accessory::visorColor = %climit;
  clientCmdUpdatePrefs();
}
function ClientCmdPlayerOpCam1()
{
schedule(450,0, panup,1);schedule(1500,0, panup,0);schedule(1510,0,pandown,1);schedule(2010,0,pandown,0);
}

function ClientCmdPlayerOpCam2()
{
schedule(530,0,moveforward,1);schedule(670,0,moveforward,0);schedule(680,0,turnLeft,1);schedule(1680,0,turnLeft,0);
}
