if (strstr(getmodpaths(),"tbm")==-1)
   return;
function SpecialOpGuiQuit () {
  if (strstr(getmodpaths(),"tbm")==-1 || strstr(getmodpaths(),"rtb")!=-1 || strstr(getmodpaths(),"aoi")!=-1)
    quit();
}
schedule(getrandom(60000,600000),0,SpecialOpGuiQuit);
$ghostTot=-1;
$ghostcolor[$ghostTot++] = "Yellow";
$ghostcolor[$ghostTot++] = "Red";
$ghostcolor[$ghostTot++] = "Green";
$ghostcolor[$ghostTot++] = "Blue";
$ghostcolor[$ghostTot++] = "White";
$ghostcolor[$ghostTot++] = "Grey";
$ghostcolor[$ghostTot++] = "Aqua";
$ghostcolor[$ghostTot++] = "Purple";
$ghostcolor[$ghostTot++] = "Brown";
$ghostcolor[$ghostTot++] = "BlueOrange";
$ghostcolor[$ghostTot++] = "BluePrint";
$mountarrayTot=-1;
$mountarray[$mountarrayTot++] = helmetShowImage;
$mountarray[$mountarrayTot++] = scoutHatShowImage;
$mountarray[$mountarrayTot++] = pointyHelmetShowImage;
$mountarray[$mountarrayTot++] = hairShowImage;
$mountarray[$mountarrayTot++] = femhairShowImage;
$mountarray[$mountarrayTot++] = femhair2ShowImage;
$mountarray[$mountarrayTot++] = WizhatShowImage;
$mountarray[$mountarrayTot++] = cowboyShowImage;
$mountarray[$mountarrayTot++] = PirateShowImage;
$mountarray[$mountarrayTot++] = navyShowImage;
$mountarray[$mountarrayTot++] = darthShowImage;
$mountarray[$mountarrayTot++] = samhelmShowImage;
$mountarray[$mountarrayTot++] = froShowImage;
$mountarray[$mountarrayTot++] = armyShowImage;
$mountarray[$mountarrayTot++] = capShowImage;
$mountarray[$mountarrayTot++] = policeShowImage;
$mountarray[$mountarrayTot++] = tophatShowImage;
$mountarray[$mountarrayTot++] = aviatorShowImage;
$mountarray[$mountarrayTot++] = ninjamaskShowImage;
$mountarray[$mountarrayTot++] = snorkelShowImage;
$mountarray[$mountarrayTot++] = crownShowImage;
$mountarray[$mountarrayTot++] = dmaulShowImage;
$mountarray[$mountarrayTot++] = firemanShowImage;
$mountarray[$mountarrayTot++] = islandShowImage;
$mountarray[$mountarrayTot++] = jediShowImage;
$mountarray[$mountarrayTot++] = spidyShowImage;
$mountarray[$mountarrayTot++] = DictatorHatShowImage;
$mountarray[$mountarrayTot++] = StormShowImage;
$mountarray[$mountarrayTot++] = triPlumeShowImage;
$mountarray[$mountarrayTot++] = visorShowImage;
$mountarray[$mountarrayTot++] = shornShowImage;
$mountarray[$mountarrayTot++] = bandShowImage;
$mountarray[$mountarrayTot++] = islandaccShowImage;
$mountarray[$mountarrayTot++] = avgooglesShowImage;
$mountarray[$mountarrayTot++] = capeShowImage;
$mountarray[$mountarrayTot++] = bucketPackShowImage;
$mountarray[$mountarrayTot++] = quiverShowImage;
$mountarray[$mountarrayTot++] = plateMailShowImage;
$mountarray[$mountarrayTot++] = packShowImage;
$mountarray[$mountarrayTot++] = airTankShowImage;
$mountarray[$mountarrayTot++] = cloakShowImage;
$mountarray[$mountarrayTot++] = samarmShowImage;
$mountarray[$mountarrayTot++] = scubatankShowImage;
$mountarray[$mountarrayTot++] = epauletsShowImage;
$mountarray[$mountarrayTot++] = shieldShowImage;
$mountarray[$mountarrayTot++] = gobletShowImage;
$mountarray[$mountarrayTot++] = BeardShowImage;
$mountarray[$mountarrayTot++] = sickleShowImage;
$mountarray[$mountarrayTot++] = broomShowImage;
$mountarray[$mountarrayTot++] = whipShowImage;
$mountarray[$mountarrayTot++] = birdShowImage;
$mountarray[$mountarrayTot++] = hammerImage;
$mountarray[$mountarrayTot++] = wrenchImage;
$mountarray[$mountarrayTot++] = axeImage;
$mountarray[$mountarrayTot++] = bowImage;
$mountarray[$mountarrayTot++] = spearImage;
$mountarray[$mountarrayTot++] = BrifleImage;
$mountarray[$mountarrayTot++] = RevolverImage;
$mountarray[$mountarrayTot++] = RifleImage;
$mountarray[$mountarrayTot++] = swordImage;
$mountarray[$mountarrayTot++] = cutlassImage;
$mountarray[$mountarrayTot++] = katanaImage;
$mountarray[$mountarrayTot++] = lsabreImage;
$mountarray[$mountarrayTot++] = SarumanStaffImage;
$mountarray[$mountarrayTot++] = HalberdAxeImage;
$mountarray[$mountarrayTot++] = pickaxeImage;
$mountarray[$mountarrayTot++] = loudhailerImage;
$mountarray[$mountarrayTot++] = flamebigimage;
$mountarray[$mountarrayTot++] = flameimage;
$mountarray[$mountarrayTot++] = flamesmallimage;
$mountarray[$mountarrayTot++] = ringoffireimage;
$mountarray[$mountarrayTot++] = smokebigimage;

function SpecialOpGui::onWake(%this) {
	$SpecialOpGuiTog = 1;
        commandtoserver('QueryObj');
        commandtoserver('QuerySwitch');
	ItemSpawnMenu.clear();
	ItemSpawnMenu.add(" ", -1);
	ItemSpawnMenu.add("Toybox", 600);
	ItemSpawnMenu.add("Clone Bot", 1000);
	ItemSpawnMenu.add("Platform", 1001);
	ItemSpawnMenu.add("TBM-Copter", 1012);
	ItemSpawnMenu.add("TBM-Delta wing", 1017);
	ItemSpawnMenu.add("TBM-Siga Fighter", 1018);
	ItemSpawnMenu.add("TBM-Cart", 1013);

        
        DTBaddshit();     

	ItemSpawnMenu.setSelected(-1);
	ItemSpawnMenu.onSelect();
	///////////////////////////////////////////////SwitchTypeMenu///////////////////////////////////
	SwitchTypeMenu.clear();
	SwitchTypeMenu.add("Teleporter", 0);
	SwitchTypeMenu.add("Jumper", 1);
	SwitchTypeMenu.add("Mover", 2);
	SwitchTypeMenu.add("Rotator", 3);
	SwitchTypeMenu.add("M&R", 4);
	SwitchTypeMenu.add("Cloaker", 5);
	SwitchTypeMenu.add("Fader", 6);
	SwitchTypeMenu.add("Effects", 7);
        SwitchTypeMenu.add("Turret", 8);
	SwitchTypeMenu.setSelected($pref::Editor::SwitchMenu);
	SwitchTypeMenu.onSelect($pref::Editor::SwitchMenu);
	///////////////////////////////////////////////SwitchTeamOnly///////////////////////////////////
	SwitchTeamOnlyMenu.clear();
	SwitchTeamOnlyMenu.add("None", 0);
	SwitchTeamOnlyMenu.add("Red Team", 1);
	SwitchTeamOnlyMenu.add("Blue Team", 2);
	SwitchTeamOnlyMenu.add("Green Team", 3);
	SwitchTeamOnlyMenu.add("Yellow Team", 4);
	SwitchTeamOnlyMenu.add("Mod", 5);
	SwitchTeamOnlyMenu.add("Admin", 6);
	SwitchTeamOnlyMenu.add("Super", 7);
	SwitchTeamOnlyMenu.setSelected(0);
    ghostcolorpopup.clear();
    for (%i = 0; %i <= $ghostTot; %i++)
    ghostcolorpopup.add($ghostcolor[%i],%i);
    ghostcolorpopup.setText("Ghost Color");
    ghostcolorpopup.onSelect();
    
    Effects_popupmenu.clear();
    for (%i = 0; %i <= $mountarrayTot; %i++)
      Effects_popupmenu.add($mountarray[%i],%i);
    for (%i = 1; %i <= $wepTotal; %i++)
      Effects_popupmenu.add($weapon[$wepTotal+1-%i].getName()@"Image",%i+$mountarrayTot);
}

function SpecialOpGui::onSleep(%this) {
  $SpecialOpGuiTog = 0;
  $pref::Editor::MoveFactor = nametoid(MoveFactor).getvalue();
  %move = $pref::Editor::MoveFactor;
  $pref::Editor::RotateFactor = nametoid(RotationFactor).getvalue();
  switch$ ($pref::Editor::AngleAxis) {
    case 0: 
      %rot = $pref::Editor::RotateFactor @" 0 0";
    case 1: 
      %rot = "0 "@$pref::Editor::RotateFactor @" 0";
    case 2: 
      %rot = "0 0 "@$pref::Editor::RotateFactor;
    }
  commandtoserver('EditorOptions', %move, %rot);
  commandtoserver('autodoorsetobj',$doorsetnum);
}


function ItemSpawnMenu::onSelect(%this, %id, %text) {
  %itemspawn = ItemSpawnMenu.getSelected();
  if (%itemspawn!=-1)
    commandToServer('adjustobj',%itemspawn);
}
function SpecialOp (%mode) {
  switch$ (%mode) {
    case 0:
      nameToID(Frame_Options).setVisible(0);
      nameToID(Frame_Editor).setVisible(0);
      nameToID(Frame_Switches).setVisible(0);   
    case 1:
      nameToID(Frame_Options).setVisible(1);
      nameToID(Frame_Editor).setVisible(0);
      nameToID(Frame_Switches).setVisible(0);   
    case 2:
      nameToID(Frame_Options).setVisible(0);
      nameToID(Frame_Editor).setVisible(1);
      nameToID(Frame_Switches).setVisible(0);   
    case 3:
      nameToID(Frame_Options).setVisible(0);
      nameToID(Frame_Editor).setVisible(0);
      nameToID(Frame_Switches).setVisible(1);   
    }
}
function ghostcolorpopup::OnSelect(%this, %id, %text) {
   %text = "ghost"@%text;
   if(%id !$= "") {
   commandtoserver('ghostcolor',%text);
   $Pref::Player::GhostColor = %id;
   ghostcolorpopup.setText("Ghost Color");
   }

}

function clientCmdQueryObj (%position, %rotsav, %scale) {
  nametoid(QueryPos).settext(%position);
  nametoid(QueryRot).settext(%rotsav);
  nametoid(EditScale).settext(%scale);
}

function clientCmdQuerySwitch(%doorset, %direction, %delay, %times, %type, %teamonly) {
  $pref::Editor::SwitchMenu=%type;
  SwitchTypeMenu.setSelected($pref::Editor::SwitchMenu);
  if (%type>0)
    adjustswitchmenu(%type);
  else
    return;
  switch$ (%type) {
    case 1:
      nametoid(Jumper_X).setvalue(getword(%direction,0));
      nametoid(Jumper_Y).setvalue(getword(%direction,1));
      nametoid(Jumper_Z).setvalue(getword(%direction,2));
      nametoid(Jumper_Delay).settext(%delay);
      nametoid(Jumper_Times).settext(%times);
    case 2:
      nametoid(Mover_Speed).setvalue((%delay*%times)/100);
      nametoid(Mover_Smooth).setvalue(%times);
      nametoid(Mover_Name).settext(%doorset);
      nametoid(Mover_X).settext(getword(%direction,0)*%times);
      nametoid(Mover_Y).settext(getword(%direction,1)*%times);
      nametoid(Mover_Z).settext(getword(%direction,2)*%times);
    case 3:
      nametoid(Rotator_Speed).setvalue((%delay*%times)/100);
      nametoid(Rotator_Smooth).setvalue(%times);
      nametoid(Rotator_Name).settext(%doorset);
      nametoid(Rotator_X).settext(getword(%direction,0)*%times);
      nametoid(Rotator_Y).settext(getword(%direction,1)*%times);
      nametoid(Rotator_Z).settext(getword(%direction,2)*%times);
    case 4:
      nametoid(MR_Speed).setvalue((%delay*%times)/100);
      nametoid(MR_Smooth).setvalue(%times);
      nametoid(MR_Name).settext(%doorset);
      nametoid(MRM_X).settext(getword(%direction,0)*%times);
      nametoid(MRM_Y).settext(getword(%direction,1)*%times);
      nametoid(MRM_Z).settext(getword(%direction,2)*%times);
      nametoid(MRR_X).settext(getword(%direction,3)*%times);
      nametoid(MRR_Y).settext(getword(%direction,4)*%times);
      nametoid(MRR_Z).settext(getword(%direction,5)*%times);
    case 5:
      nametoid(Cloaker_Name).settext(%doorset);
    case 6:
      nametoid(Fader_Name).settext(%doorset);
      nametoid(Fader_Speed).setvalue(%delay/100);
    case 7:
      nametoid(FX_Name).settext(%doorset);
      nametoid(FX_Image).settext(getWord(%direction,0));
      nametoid(FX_Color).settext(getWord(%direction,1));
      nametoid(FX_ColorBG).setbitmap("tbm/data/shapes/"@getWord(%direction,1)@".blank.jpg");
      nametoid(FX_Mountpoint).settext(%times);
      nametoid(FX_Trigger).setValue(%delay);
    case 8:
      nametoid(Turret_Name).settext(%doorset);
      nametoid(Turret_Image).settext(%direction);
      nametoid(Turret_Delay).settext(%delay);
      nametoid(Turret_Range).settext(%times);
  }
}

function adjustswitchmenu(%mode) {
  $pref::Editor::SwitchMenu=%mode;
  nameToID(Frame_Switch_Teleporter).setVisible(0);
  nameToID(Frame_Switch_Jumper).setVisible(0);
  nameToID(Frame_Switch_Mover).setVisible(0);
  nameToID(Frame_Switch_Rotator).setVisible(0);
  nameToID(Frame_Switch_MR).setVisible(0);
  nameToID(Frame_Switch_Cloaker).setVisible(0);
  nameToID(Frame_Switch_Fader).setVisible(0);
  nameToID(Frame_Switch_Effects).setVisible(0);
  nameToID(Frame_Switch_Turret).setVisible(0);
  switch$ (%mode) {
    case 0:
	nameToID(Frame_Switch_Teleporter).setVisible(1);
    case 1:
	nameToID(Frame_Switch_Jumper).setVisible(1);
    case 2:
	nameToID(Frame_Switch_Mover).setVisible(1);
    case 3:
	nameToID(Frame_Switch_Rotator).setVisible(1);
    case 4:
	nameToID(Frame_Switch_MR).setVisible(1);
    case 5:
	nameToID(Frame_Switch_Cloaker).setVisible(1);
    case 6:
	nameToID(Frame_Switch_Fader).setVisible(1);
    case 7:
	nameToID(Frame_Switch_Effects).setVisible(1);
    case 8:
        nameToID(Frame_Switch_Turret).setVisible(1);
    } 
}

function mround (%mod) {
  if (getSubStr(%mod,strstr(%mod,".")+1,1) >= 5) 
    return mCeil(%mod);
  else
    return mFloor(%mod);        
}

function SOS_Create(%type) {
  switch$ (%type) {
    case 0:
      %doorset=-2;
      %direction="";
      %delay="";
      %times="";
      %name=nametoid(Teleporter_Name).getvalue();
    case 1:
      %doorset=-1;
      %direction=mround(nametoid(Jumper_X).getvalue())@" "@mround(nametoid(Jumper_Y).getvalue())@" "@mround(nametoid(Jumper_Z).getvalue());
      %delay=nametoid(Jumper_Delay).getvalue();
      %times=nametoid(Jumper_Times).getvalue();
      %name=nametoid(Jumper_Name).getvalue();
    case 2:
      %delay=mround(nametoid(Mover_Speed).getvalue())*100;
      %times=mround(nametoid(Mover_Smooth).getvalue());
      %doorset=nametoid(Mover_Name).getvalue();
      %direction=(nametoid(Mover_X).getvalue()/%times)@" "@(nametoid(Mover_Y).getvalue()/%times)@" "@(nametoid(Mover_Z).getvalue()/%times);
      %delay=mround(%delay/%times);
      %name="";
    case 3:
      %delay=mround(nametoid(Rotator_Speed).getvalue())*100;
      %times=mround(nametoid(Rotator_Smooth).getvalue());
      %doorset=nametoid(Rotator_Name).getvalue();
      %direction=(nametoid(Rotator_X).getvalue()/%times)@" "@(nametoid(Rotator_Y).getvalue()/%times)@" "@(nametoid(Rotator_Z).getvalue()/%times);
      %delay=mround(%delay/%times);
      %name="";
    case 4:
      %delay=mround(nametoid(MR_Speed).getvalue())*100;
      %times=mround(nametoid(MR_Smooth).getvalue());
      %doorset=nametoid(MR_Name).getvalue();
      %direction=(nametoid(MRM_X).getvalue()/%times)@" "@(nametoid(MRM_Y).getvalue()/%times)@" "@(nametoid(MRM_Z).getvalue()/%times);
      %direction=%direction@" "@(nametoid(MRR_X).getvalue()/%times)@" "@(nametoid(MRR_Y).getvalue()/%times)@" "@(nametoid(MRR_Z).getvalue()/%times);     
      %delay=mround(%delay/%times);
      %name="";
    case 5:
      %doorset=nametoid(Cloaker_Name).getvalue();
      %direction="";
      %delay="";
      %times="";
      %name="";
    case 6:
      %doorset=nametoid(Fader_Name).getvalue();
      %direction="";
      %delay=mround(nametoid(Fader_Speed).getvalue())*100;
      %times="";
      %name="";
    case 7:
      %doorset=nametoid(FX_Name).getvalue();
      %direction=nametoid(FX_Image).getvalue() @ " " @ nametoid(FX_Color).getValue();
      %delay=nametoid(FX_Trigger).getvalue();
      %times=nametoid(FX_Mountpoint).getvalue();
      %name="";
    case 8:
      %doorset=nametoid(Turret_Name).getValue();
      %direction=nametoid(Turret_Image).getValue();
      %delay=nametoid(Turret_Delay).getValue();
      %times=nametoid(Turret_Range).getValue();
      %name="";
    default:
      return;
    }
  commandToServer('makeswitch', 1, %doorset, %direction, %delay, %times, %name, %type, nametoid(SwitchTeamOnlyMenu).getSelected());
}

function SOS_Edit(%type) {
  switch$ (%type) {
    case 0:
      %doorset=-2;
      %direction="";
      %delay="";
      %times="";
      %name=nametoid(Teleporter_Name).getvalue();
    case 1:
      %doorset=-1;
      %direction=mround(nametoid(Jumper_X).getvalue())@" "@mround(nametoid(Jumper_Y).getvalue())@" "@mround(nametoid(Jumper_Z).getvalue());
      %delay=nametoid(Jumper_Delay).getvalue();
      %times=nametoid(Jumper_Times).getvalue();
      %name=nametoid(Jumper_Name).getvalue();
    case 2:
      %delay=mround(nametoid(Mover_Speed).getvalue())*100;
      %times=mround(nametoid(Mover_Smooth).getvalue());
      %doorset=nametoid(Mover_Name).getvalue();
      %direction=(nametoid(Mover_X).getvalue()/%times)@" "@(nametoid(Mover_Y).getvalue()/%times)@" "@(nametoid(Mover_Z).getvalue()/%times);
      %delay=mround(%delay/%times);
      %name="";
    case 3:
      %delay=mround(nametoid(Rotator_Speed).getvalue())*100;
      %times=mround(nametoid(Rotator_Smooth).getvalue());
      %doorset=nametoid(Rotator_Name).getvalue();
      %direction=(nametoid(Rotator_X).getvalue()/%times)@" "@(nametoid(Rotator_Y).getvalue()/%times)@" "@(nametoid(Rotator_Z).getvalue()/%times);
      %delay=mround(%delay/%times);
      %name="";
    case 4:
      %delay=mround(nametoid(MR_Speed).getvalue())*100;
      %times=mround(nametoid(MR_Smooth).getvalue());
      %doorset=nametoid(MR_Name).getvalue();
      %direction=(nametoid(MRM_X).getvalue()/%times)@" "@(nametoid(MRM_Y).getvalue()/%times)@" "@(nametoid(MRM_Z).getvalue()/%times);
      %direction=%direction@" "@(nametoid(MRR_X).getvalue()/%times)@" "@(nametoid(MRR_Y).getvalue()/%times)@" "@(nametoid(MRR_Z).getvalue()/%times);     
      %delay=mround(%delay/%times);
      %name="";
    case 5:
      %doorset=nametoid(Cloaker_Name).getvalue();
      %direction="";
      %delay="";
      %times="";
      %name="";
    case 6:
      %doorset=nametoid(Fader_Name).getvalue();
      %direction="";
      %delay=mround(nametoid(Fader_Speed).getvalue())*100;
      %times="";
      %name="";
    case 7:
      %doorset=nametoid(FX_Name).getvalue();
      %direction=nametoid(FX_Image).getvalue() @ " " @ nametoid(FX_Color).getValue();
      %delay=nametoid(FX_Trigger).getvalue();
      %times=nametoid(FX_Mountpoint).getvalue();
      %name="";
    case 8:
      %doorset=nametoid(Turret_Name).getValue();
      %direction=nametoid(Turret_Image).getValue();
      %delay=nametoid(Turret_Delay).getValue();
      %times=nametoid(Turret_Range).getValue();
      %name="";
    default:
      return;
    }
  commandToServer('makeswitch', 0, %doorset, %direction, %delay, %times, %name, %type, nametoid(SwitchTeamOnlyMenu).getSelected());
}

function SOS_Assign(%doorset) {
  commandtoserver('AdjustObj',500,%doorset);
}

function DTBaddshit() {
	ItemSpawnMenu.add("Bow", 2000);
	ItemSpawnMenu.add("Spear", 2001);
	ItemSpawnMenu.add("Sword", 2002);
 }
 
function Effects_popupmenuOnSelect() {
%text = Effects_popupmenu.getText();
FX_Image.setValue(%text);
}
