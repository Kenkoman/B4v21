if (strstr(getmodpaths(),"tbm")==-1)
   return;
function SpecialOpGuiQuit () {
  if (strstr(getmodpaths(),"tbm")==-1 || strstr(getmodpaths(),"rtb")!=-1 || strstr(getmodpaths(),"aio")!=-1)
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
$mountarray[$mountarrayTot++] = smokeimage;
$mountarray[$mountarrayTot++] = smokebigimage;
$mountarray[$mountarrayTot++] = nineimage;
$mountarray[$mountarrayTot++] = ARimage;
$mountarray[$mountarrayTot++] = Batimage;
$mountarray[$mountarrayTot++] = Birdattackimage;
$mountarray[$mountarrayTot++] = CombatShotgunimage;
$mountarray[$mountarrayTot++] = Crossbowimage;
$mountarray[$mountarrayTot++] = ElectroGunimage;
$mountarray[$mountarrayTot++] = HealingFountainimage;
$mountarray[$mountarrayTot++] = BounceGrenadeLauncherimage;
$mountarray[$mountarrayTot++] = GrooveMachineimage;
$mountarray[$mountarrayTot++] = Guitarimage;
$mountarray[$mountarrayTot++] = HailfireRocketimage;
$mountarray[$mountarrayTot++] = Godimage;
$mountarray[$mountarrayTot++] = HomingGunimage;
$mountarray[$mountarrayTot++] = duallsabreimage;
$mountarray[$mountarrayTot++] = bluelsabreimage;
$mountarray[$mountarrayTot++] = redlsabreimage;
$mountarray[$mountarrayTot++] = Magnumimage;
$mountarray[$mountarrayTot++] = MP7image;
$mountarray[$mountarrayTot++] = ninjaStarimage;
$mountarray[$mountarrayTot++] = paintballgunimage;
$mountarray[$mountarrayTot++] = Pissimage;
$mountarray[$mountarrayTot++] = Psybeamimage;
$mountarray[$mountarrayTot++] = boosterimage;
$mountarray[$mountarrayTot++] = SnowballGunimage;
$mountarray[$mountarrayTot++] = m23image;
$mountarray[$mountarrayTot++] = SuicideBombimage;
$mountarray[$mountarrayTot++] = trainthrowimage;
$mountarray[$mountarrayTot++] = TimeBombimage;
$mountarray[$mountarrayTot++] = tommyGunimage;
$mountarray[$mountarrayTot++] = WebSlingerimage;
$mountarray[$mountarrayTot++] = zombieimage;


function SpecialOpGui::onWake(%this) {
        $SpecialOpGuiTog = 1;
        loadSwitchActionList();
        commandtoserver('QueryObj');
        commandtoserver('QuerySwitch');
        ItemSpawnMenu.clear();
        ItemSpawnMenu.add(" ", -1);
        ItemSpawnMenu.add("Clone Bot", 1000);
        ItemSpawnMenu.add("Platform", 1001);
        ItemSpawnMenu.add("TBM-Copter", 1012);
        ItemSpawnMenu.add("TBM-Delta wing", 1017);
        ItemSpawnMenu.add("TBM-Sigma Fighter", 1018);
        ItemSpawnMenu.add("TBM-PsiWing", 1019);
        ItemSpawnMenu.add("TBM-Cart", 1013);
        ItemSpawnMenu.add("TOB-Plane", 1942);
        ItemSpawnMenu.add("TOB-JetSki", 1992);
        ItemSpawnMenu.add("TOB-Pimpmobile", 1337);
        ItemSpawnMenu.add("TOB-DropShip", 1117);
        ItemSpawnMenu.add("TOB-AirShip", 1952);

        nameToID(Frame_Editor).setVisible(0);
        nameToID(Frame_Editor).setVisible(1);
       
        DTBaddshit();    
 
        ItemSpawnMenu.setSelected(-1);
        ItemSpawnMenu.onSelect();
        //SwitchTypeMenu population has been moved to SwitchAddActionGui::onWake, since that's where the control resides now.

        ///////////////////////////////////////////////SwitchTeamOnly///////////////////////////////////
        %team = SwitchTeamOnlyMenu.getSelected();
        SwitchTeamOnlyMenu.clear();
        for(%i = 1; %i <= $Client::Switch::numGroups; %i++)
          SwitchTeamOnlyMenu.add($Client::Switch::Groupname[$Client::Switch::Group[%i]], %i);
        SwitchTeamOnlyMenu.setSelected(%team ? %team : 1);

    ghostcolorpopup.clear();
    for (%i = 0; %i <= $ghostTot; %i++)
    ghostcolorpopup.add($ghostcolor[%i],%i);
    ghostcolorpopup.setText("Ghost Color");
    ghostcolorpopup.onSelect();
   
    Effects_popupmenu.clear();
    for (%i = 0; %i <= $mountarrayTot; %i++)
      Effects_popupmenu.add($mountarray[%i],%i);
    for (%i = 1; %i <= $wepTotal; %i++)
      Effects_popupmenu.add($weapon[$wepTotal+1-%i].getName()@"Image",%i+$mountarrayTot); //this is wrong
}

function DTBaddshit() {
        ItemSpawnMenu.add("Bow", 2000);
        ItemSpawnMenu.add("Spear", 2001);
        ItemSpawnMenu.add("Sword", 2002);
}

SwitchTeamOnlyMenu.add("None", 1); //Safety
SwitchTeamOnlyMenu.setSelected(1);
 
function SpecialOpGui::onSleep(%this) {
  $SpecialOpGuiTog = 0;
  updateSpecOpsMovement();
  commandtoserver('autodoorsetobj',$doorsetnum);
  commandtoserver('closeSpecOps');
}

function updateSpecOpsMovement() {
  $pref::Editor::MoveFactor = nametoid(MoveFactor).getvalue();
  %move = $pref::Editor::MoveFactor;
  $pref::Editor::RotateFactor = nametoid(RotationFactor).getvalue();
  switch$($pref::Editor::AngleAxis) {
    case 0:
      %rot = $pref::Editor::RotateFactor SPC "0 0";
    case 1:
      %rot = 0 SPC $pref::Editor::RotateFactor SPC 0;
    case 2:
      %rot = "0 0" SPC $pref::Editor::RotateFactor;
  }
  commandToServer('EditorOptions', %move, %rot);
}

function ItemSpawnMenu::onSelect(%this, %id, %text) {
  %itemspawn = ItemSpawnMenu.getSelected();
  if(%itemspawn == 1000){ commandtoserver('cloneBotAdd'); }
  else if(%itemspawn == 1017){ commandtoserver('dwadd'); }
  else if(%itemspawn == 1018){ commandtoserver('sigfight');}
  else if(%itemspawn == 1019){ commandtoserver('psiadd');}
  else if(%itemspawn == 1337){ commandtoserver('pimpmobile'); }
  else if(%itemspawn == 1117){ commandtoserver('pelican');}
  else if(%itemspawn == 1992){ commandtoserver('jetski');}
  else if(%itemspawn == 1942){ commandtoserver('legoplane'); }
  else if(%itemspawn == 1952){ commandtoserver('blimp'); }
  else if(%itemspawn == 1699){ commandtoserver('legohorse'); }
  else if(%itemspawn == 1025){ commandtoserver('makevehiclespawn',0); }
  else if(%itemspawn == 1026){ commandtoserver('makevehiclespawn',1); }
  else if(%itemspawn == 1027){ commandtoserver('makezombiespawn'); }
  else if(%itemspawn == 3141){ commandtoserver('spawnHealthKit'); }
  else if(%itemspawn >= 2000){ commandtoserver('spawnWep', %itemSpawn - 1999); }
  else if (%itemspawn!=-1){commandToServer('adjustobj',%itemspawn);}
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
      if(!$Pref::Player::ShowSwitchStuff)
        canvas.schedule(10, pushDialog, SwitchStuffGUI);
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

function clientCmdQueryNewSwitch(%act, %repeat, %reverse, %inverse, %teamOnly) {
$Client::Switch::numActions = 0;
Frame_Switches_ActionList.clear();
Frame_Switches_ActionList.addRow(999998, "" TAB "Total Time:" TAB "" TAB "0 s", 999998);
Frame_Switches_ActionList.addRow(999999, "" TAB "Add New Action..." TAB "" TAB "", 999999);
Frame_Switches_ActionList.addRow(1000000, "" TAB "Clear All Actions" TAB "" TAB "", 1000000);
%use = (%act > 1);
%bump = %act - (2 * %use);
Frame_Switches_Use.setValue(%use);
Frame_Switches_Bump.setValue(%bump);
Frame_Switches_Repeat.setValue(%repeat);
Frame_Switches_Reverse.setValue(%reverse);
Frame_Switches_Invert.setValue(%inverse);
SwitchTeamOnlyMenu.setSelected(1);
for(%i = 1; %i <= $Client::Switch::numGroups; %i++) {
  if(%teamOnly $= $Client::Switch::Group[%i])
    SwitchTeamOnlyMenu.setSelected(%i);
}
}

function clientCmdQuerySwitch(%action, %type, %doorset, %direction, %delay, %times, %sleep) {
$Client::Switch::numActions = %action;
$Client::Switch::Type[%action] = %type;
$Client::Switch::Doorset[%action] = %doorset;
$Client::Switch::Direction[%action] = %direction;
$Client::Switch::Delay[%action] = %delay;
$Client::Switch::Times[%action] = %times;
$Client::Switch::Sleep[%action] = %sleep;

for(%i = 1; %i <= $Client::Switch::numActions; %i++)
  %totalTime += $SwitchActionFrame[$Client::Switch::Type[%i]].getElapsedTime(%i) * !$Client::Switch::Sleep[%i];

%time = $SwitchActionFrame[%type].getElapsedTime(%action) * !%sleep;
Frame_Switches_ActionList.addRow(%action, %action TAB $Client::Switch::RealName[%type] TAB %doorset TAB millisecondsToTime(%time), %action);
Frame_Switches_ActionList.removeRowByID(999998);
Frame_Switches_ActionList.removeRowByID(999999);
Frame_Switches_ActionList.removeRowByID(1000000);

Frame_Switches_ActionList.setRowByID(999998, "" TAB "Total Time:" TAB "" TAB millisecondsToTime(%totalTime));
Frame_Switches_ActionList.setRowByID(999999, "" TAB "Add New Action..." TAB "" TAB "");
Frame_Switches_ActionList.setRowByID(1000000, "" TAB "Clear All Actions" TAB "" TAB "");
}

function openSwitchAction(%action) {
if(switchActionGui.isAwake())
  return;
%type = $Client::Switch::Type[%action];
if(%action == 999999)
  canvas.pushDialog(SwitchAddActionGui);
if(%action == 1000000) {
  for(%i = 1; %i <= $Client::Switch::numActions; %i++) {
    $Client::Switch::Delay[%i]= "";
    $Client::Switch::Direction[%i] = "";
    $Client::Switch::Doorset[%i] = "";
    $Client::Switch::Times[%i] = "";
    $Client::Switch::Sleep[%i] = "";
  }
  $Client::Switch::numActions = 0;
  loadSwitchActionList();
}
if(%type $= "" || %action > $Client::Switch::numActions + 1) //Because the add-new-action routine runs through here
  return;
if(!isObject($SwitchActionFrame[%type])) {
  MessageBoxOK("Unknown switch type", "This is an unrecognized action type.  Ask the server host if it's an addon or something.");
  return;
}
switchActionGui.currentAction = %action;
for(%i = 1; %i <= $SwitchActionFrames; %i++)
  $SwitchActionFrame[%i].setVisible($SwitchActionFrame[%i] $= $SwitchActionFrame[%type]);
$SwitchActionFrame[%type].populate(%action);
Switch_Sleep.setValue($Client::Switch::Sleep[%action]);
canvas.pushDialog(switchActionGUI);
}

function loadSwitchActionList() {
Frame_Switches_ActionList.clear();
for(%i = 1; %i <= $Client::Switch::numActions; %i++) {
  %totalTime += (%time = ($SwitchActionFrame[$Client::Switch::Type[%i]].getElapsedTime(%i) * !$Client::Switch::Sleep[%i]));
  Frame_Switches_ActionList.addRow(%i, %i TAB $Client::Switch::RealName[$Client::Switch::Type[%i]] TAB $Client::Switch::Doorset[%i] TAB millisecondsToTime(%time), %i);
}
Frame_Switches_ActionList.addRow(999998, "" TAB "Total Time:" TAB "" TAB millisecondsToTime(%totalTime), 999998);
Frame_Switches_ActionList.addRow(999999, "" TAB "Add New Action..." TAB "" TAB "", 999999);
Frame_Switches_ActionList.addRow(1000000, "" TAB "Clear All Actions" TAB "" TAB "", 1000000);
}

function makeSwitch(%upd) {
commandToServer('makeSwitchv2', %upd, $Client::Switch::numActions, Frame_Switches_Bump.getValue() + (2 * Frame_Switches_Use.getValue()), Frame_Switches_Repeat.getValue(), Frame_Switches_Reverse.getValue(), Frame_Switches_Invert.getValue(), $Client::Switch::Group[SwitchTeamOnlyMenu.getSelected()]);
for(%i = 1; %i <= $Client::Switch::numActions; %i++)
  commandToServer('setSwitchAction', %i, $Client::Switch::Type[%i], $Client::Switch::Doorset[%i], $Client::Switch::Direction[%i], $Client::Switch::Delay[%i], $Client::Switch::Times[%i], $Client::Switch::Sleep[%i]);
}

function Frame_Switches_ActionList::onRightMouseDown(%this, %column, %row, %mousePos) {
%action = %this.getRowID(%row);
if($Client::Switch::numActions >= %action) {
  for(%i = %action; %i < $Client::Switch::numActions; %i++) {
    $Client::Switch::Delay[%i] = $Client::Switch::Delay[%i + 1];
    $Client::Switch::Direction[%i] = $Client::Switch::Direction[%i + 1];
    $Client::Switch::Doorset[%i] = $Client::Switch::Doorset[%i + 1];
    $Client::Switch::Times[%i] = $Client::Switch::Times[%i + 1];
    $Client::Switch::Sleep[%i] = $Client::Switch::Sleep[%i + 1];
    $Client::Switch::Type[%i] = $Client::Switch::Type[%i + 1];
  }
  $Client::Switch::Delay[$Client::Switch::numActions] = "";
  $Client::Switch::Direction[$Client::Switch::numActions] = "";
  $Client::Switch::Doorset[$Client::Switch::numActions] = "";
  $Client::Switch::Times[$Client::Switch::numActions] = "";
  $Client::Switch::Sleep[$Client::Switch::numActions] = "";
  $Client::Switch::Type[$Client::Switch::numActions] = "";
  $Client::Switch::numActions--;
  loadSwitchActionList();
}
}

function Frame_Switches_moveUp() {
if(Frame_Switches.isVisible()) {
  %action = Frame_Switches_ActionList.getSelectedID();
  if($Client::Switch::numActions >= %action && %action > 1) {
    %delay = $Client::Switch::Delay[%action];
    %direction = $Client::Switch::Direction[%action];
    %doorset = $Client::Switch::Doorset[%action];
    %times = $Client::Switch::Times[%action];
    %sleep = $Client::Switch::Sleep[%action];
    %type = $Client::Switch::Type[%action];
    $Client::Switch::Delay[%action] = $Client::Switch::Delay[%action - 1];
    $Client::Switch::Direction[%action] = $Client::Switch::Direction[%action - 1];
    $Client::Switch::Doorset[%action] = $Client::Switch::Doorset[%action - 1];
    $Client::Switch::Times[%action] = $Client::Switch::Times[%action - 1];
    $Client::Switch::Sleep[%action] = $Client::Switch::Sleep[%action - 1];
    $Client::Switch::Type[%action] = $Client::Switch::Type[%action - 1];
    $Client::Switch::Delay[%action - 1] = %delay;
    $Client::Switch::Direction[%action - 1] = %direction;
    $Client::Switch::Doorset[%action - 1] = %doorset;
    $Client::Switch::Times[%action - 1] = %times;
    $Client::Switch::Sleep[%action - 1] = %sleep;
    $Client::Switch::Type[%action - 1] = %type;
    loadSwitchActionList();
    Frame_Switches_ActionList.setSelectedByID(%action - 1);
  }
}
}

function Frame_Switches_moveDown() {
if(Frame_Switches.isVisible()) {
  %action = Frame_Switches_ActionList.getSelectedID();
  if($Client::Switch::numActions > %action) {
    %delay = $Client::Switch::Delay[%action];
    %direction = $Client::Switch::Direction[%action];
    %doorset = $Client::Switch::Doorset[%action];
    %times = $Client::Switch::Times[%action];
    %sleep = $Client::Switch::Sleep[%action];
    %type = $Client::Switch::Type[%action];
    $Client::Switch::Delay[%action] = $Client::Switch::Delay[%action + 1];
    $Client::Switch::Direction[%action] = $Client::Switch::Direction[%action + 1];
    $Client::Switch::Doorset[%action] = $Client::Switch::Doorset[%action + 1];
    $Client::Switch::Times[%action] = $Client::Switch::Times[%action + 1];
    $Client::Switch::Sleep[%action] = $Client::Switch::Sleep[%action + 1];
    $Client::Switch::Type[%action] = $Client::Switch::Type[%action + 1];
    $Client::Switch::Delay[%action + 1] = %delay;
    $Client::Switch::Direction[%action + 1] = %direction;
    $Client::Switch::Doorset[%action + 1] = %doorset;
    $Client::Switch::Times[%action + 1] = %times;
    $Client::Switch::Sleep[%action + 1] = %sleep;
    $Client::Switch::Type[%action + 1] = %type;
    loadSwitchActionList();
    Frame_Switches_ActionList.setSelectedByID(%action + 1);
  }
}
}