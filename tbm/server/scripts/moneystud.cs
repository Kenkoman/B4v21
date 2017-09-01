//Moneystud.cs
//Monetary code by DShiznit, referencing code from GreyMario, Luquado, and Kier a.k.a. MCP
//Improved by Wiggy

//DataBlocks
datablock ItemData(moneyStud)
{
   shapeFile = "~/data/shapes/bricks/coin.dts";
   category = "debris";
   mass = 1;
   friction = 1;
   elasticity = 0.1;
   rotate = true;
   maxInventory = 0;
   pickUpName = '';
   invName = '';
};
datablock AudioProfile(MoneyStudSound)
{
   fileName = "~/data/sound/moneystud.wav";
   description = AudioClose3d;
   preload = true;
};

//Vars
$MoneyStud::color[0] = "grey";
$MoneyStud::value[0] = 1;
$MoneyStud::rProb[0] = 60;
$MoneyStud::color[1] = "coolyellow";
$MoneyStud::value[1] = 10;
$MoneyStud::rProb[1] = 40;
$MoneyStud::color[2] = "neongreen";
$MoneyStud::value[2] = 100;
$MoneyStud::rProb[2] = 1;
$MoneyStud::color[3] = "red";
$MoneyStud::value[3] = 100;
$MoneyStud::rProb[3] = 1;
$MoneyStud::color[4] = "royalblue";
$MoneyStud::value[4] = 100;
$MoneyStud::rProb[4] = 1;
$MoneyStud::totalProb = -1;
for(%i = 0; $MoneyStud::rProb[%i] !$= ""; %i++)
  $MoneyStud::totalProb += $MoneyStud::rProb[%i];

//Functions
function moneyStud::onPickup(%this, %obj, %user, %amount) {
//This plays the ching sound and adds to the player's wallet based on the value of the coin picked up
ServerPlay3D(MoneyStudSound, %user.getTransform());
if(%user.getClassName() $= "Player" && isObject(%user.client))
  %user.client.setStuds(%user.client.studmoney + (%obj.studValue > 0 ? %obj.studValue : 1));
%obj.delete();
}

function moneyStud::create(%this, %probBuff, %value) {
%x = (getRandom() * ($MoneyStud::totalProb - %probBuff)) + 1 + %probBuff; //(P+1) to X
for(%i = 0; $MoneyStud::rProb[%i] !$= ""; %i++) {
  %x -= $MoneyStud::rProb[%i];
  if(%x <= 0)
    break;
}
if($MoneyStud::rProb[%i] $= "")
  %i--;
%stud = new Item() {
  datablock = %this;
};
%stud.setSkinName($MoneyStud::color[%i]);
%stud.schedule($ItemTime, delete);
%stud.studvalue = %value $= "" ? $MoneyStud::value[%i] : %value;
MissionCleanup.add(%stud);
return %stud;
}

function serverCmdStudCount(%client) {
messageClient(%client, 'Msg', "You have $" @ %client.studMoney);
}

//Old function for backwards compatibility
function spewCash(%col, %obj) {
%stud = moneyStud.create(%obj.client.spewCashPBuff);
%stud.setTransform(%col.getTransform());
%stud.setVelocity(getRandom(-9, 9) SPC getRandom(-9, 9) SPC getRandom(0, 9));
}

//Old function for backwards compatibility
function decrementCash(%client, %transform) {
%stud = moneyStud.create();
%client.setStuds(%client.studMoney - %stud.studValue);
%stud.setTransform(%transform);
%stud.setVelocity(getRandom(-9, 9) SPC getRandom(-9, 9) SPC getRandom(0, 9));
}

function studFountain(%obj, %v) {
if(isObject(%obj)) {
  if(%obj.getClassName() $= "Player" || %obj.getClassName() $= "AIPlayer")
    if(%obj.getState() $= "Dead")
      return;
  %stud = moneyStud.create();
  %stud.setCollisionTimeout(%obj);
  %stud.setTransform(%obj.getWorldBoxCenter());
  %stud.setVelocity(getRandom(-9, 9) SPC getRandom(-9, 9) SPC getRandom(6, 12));
  if(%v > %stud.studValue)
    schedule(80, 0, studFountain, %obj, %v - %stud.studValue);
}
}

function serverCmdStudFountain(%client, %v) {
if(%client.isSuperAdmin)
  studFountain(%client.lastswitch, %v);
}

//Stud bank stuff

//Removes an entry from the stud bank
function removeStudEntry(%number) {
for(%i = 0; %i < $StudBankNumber; %i++) {
  if(%i >= %number) {
    $StudBankName[%i] = $StudBankName[%i + 1];
    $StudBankAmount[%i] = $StudBankAmount[%i + 1];
  }
}
$StudBankName[$StudBankNumber] = "";
$StudBankAmount[$StudBankNumber] = "";
$StudBankNumber--;
}

//Gives studs to the client with the specified name.
//If another client is specified, the message sent will reflect that.
function giveStuds(%name, %amount, %client) {
if(%amount < 0) return;
%c = getClient_s(%name);
if(%c != -1) {
  if(!%client)
    messageclient(%c, 'MsgItemPurchase','\c5You have been paid \c3%1 \c5%2.',%amount, $Pref::Server::CurrencyName);
  else
    messageclient(%c, 'MsgItemPurchase','\c3%2 \c5has paid you \c3%1 \c5%3.',%amount, %client.namebase, $Pref::Server::CurrencyName);
  %c.setStuds(%c.studmoney + %amount);
  return;
}
if($pref::server::StudBank != 1) {
  echo("Studs would be added to an account right now but its disabled.");
  return;
}
for(%i = 0; %i <= $StudBankNumber; %i++) {
  if(strLwr($StudBankName[%i]) $= strLwr(%name))
    $StudBankAmount[%i] += %amount;
}
}

//Parses the stud bank and does general bookkeeping, erasing duplicage entries and blank entries.
//I haven't checked this recently, but it seems to be intact, and there doesn't seem to be anything wrong if you run through it.
function parsestudbank() {
if($Pref::Server::StudBank != 1) {
  echo("Studs would be saved right now but its disabled.");
  return;
}
if(!isObject($studlist))
  $Studlist = new Fileobject();
$Studlist.openForWrite("tbm/server/studs.cs");
for(%j = 0; %j < ClientGroup.getCount(); %j++) {
  %client = ClientGroup.getObject(%j);
  if(!%client.studMoney)
    %client.studMoney = 0;
  %client.accountedFor = 0;
}
for(%i = 0; %i <= $StudBankNumber; %i++) {
  for(%j = 0; %j < ClientGroup.getCount(); %j++) {
    %client = ClientGroup.getObject(%j);
    if(strlwr($StudBankName[%i]) $= strlwr(%client.namebase)) {
      if(%client.accountedfor)
        $StudBankAmount[%i] = 0; //If they're already in the system somewhere, clear this one out
      else {
        %client.accountedfor = 1;
        $StudBankAmount[%i] = %client.studmoney;
      }
    }
  }
  if($StudBankAmount[%i] == 0 || $StudBankAmount[%i] $= "") {
    removeStudEntry(%i); //Remove empty entries
    %i--;
  }
  else {
    $studlist.writeLine("$StudBankName["@%i@"]=\""@$StudBankName[%i]@"\";");
    $studlist.writeLine("$StudBankAmount["@%i@"]="@$StudBankAmount[%i]@";");
  }
}
for(%j = 0; %j < ClientGroup.getCount(); %j++) {
  %client = ClientGroup.getObject(%j);
  if(!%client.accountedfor) {
    $StudBankName[$StudBankNumber++] = strLwr(%client.namebase); //Give new players new entries
    $StudBankAmount[$StudBankNumber] = %client.studmoney;
  }
}
for (%i = %i; %i<$StudBankNumber+1; %i++) {
  $studlist.writeLine("$StudBankName["@%i@"]=\""@$StudBankName[%i]@"\";"); //Write newly created entries
  $studlist.writeLine("$StudBankAmount["@%i@"]="@$StudBankAmount[%i]@";");
}
if($StudbankNumber $= "")
  $StudBankNumber = -1;
$Studlist.writeLine("$StudBankNumber="@$StudBankNumber@";");
$Studlist.close();
}

if($StudbankNumber $= "")
  $StudBankNumber = -1;

//This used to be called setStuds until that method was replaced with one that actually sets people's studs.
//I'm assuming that the name is self-explanatory.
function GameConnection::giveStudsfromBank(%client) {
if($Pref::Server::StudBank != 1) {
  echo("Studs would be given right now but its disabled.");
  %client.setStuds(0);
  return;
}
for(%i = 0; %i <= $StudBankNumber; %i++) {
  if(strLwr(%client.namebase) $= $StudBankName[%i]) {
    %client.setStuds($StudBankAmount[%i]);
  }
}
if(%client.studMoney $= "")
  %client.setStuds(0);
}

//Self-explanatory.
function echoStudBank() {
for(%i = 0; %i <= $StudBankNumber; %i++) {
  echo($StudBankName[%i] SPC "has" SPC $StudBankAmount[%i] SPC "studs.");
}
echo($StudBankNumber SPC "entries.");
}

//Catch-all function called when any user interacts with an object (weapon, switch, etc.) that has a cost as part of its name.
//Returning 1 means that the user can use the object; returning 0 denotes some sort of error - usually, insufficient funds.
//%Check means that it just checks to see if it CAN be purchased, and doesn't modify money or give any messages or anything.
//It's mostly used in packages
function computePurchase(%user, %obj, %check) {
%name = %obj.getShapeName();
%client = %user.client;
%x = -1;
for(%i = 0; %i <= getWordCount(%name) - 2; %i++) {
  if(getWord(%name, %i) $= "$" && getWord(%name, %i + 1) != 0) {
    %x = %i;
    break;
  }
}
%cost = getWord(%name, %x + 1);
if(%x == -1)
  return 1;
%money = %user.client.studmoney;
if(%money < %cost) {
  if(!%check)
    messageclient(%client, 'MsgItemPurchase', '\c5You cannot afford this.  You need \c3%1 \c5more %2.', %cost - %money, $Pref::Server::CurrencyName);
  return 0;
}
if(!%check)
  ServerPlay3D(MoneyStudSound, %user.getTransform());
%y = -1;
for(%i = %x; %i <= getWordCount(%name) - 1; %i++) {
  if(getSubStr(getWord(%name, %i), 0, 1) $= ":") {
    %y = %i + 1;
    break;
  }
}
if(%y != -1) {
  %owner = getWords(%name, %y, getWordCount(%name) - 1);
  %owner = getSubStr(%owner, 1, strLen(%owner) - 1);
  %ownerC = getClient_s(%owner);
  if(%ownerC == %client) {
    if(!%check)
      messageclient(%user.client,'MsgItemPurchase','\c5This is your own merchandise, so this is free.');
    return 1;
  }
  if(!%check)
    giveStuds(%owner, %cost, %client);
}
if(!%check) {
  if(%cost > 0)
    messageclient(%client, 'MsgItemPurchase','\c5You spent \c3%1 \c5%2.', %cost, $Pref::Server::CurrencyName);
  else
    messageclient(%client, 'MsgItemPurchase','\c5You were given \c3%1 \c5%2.', mAbs(%cost), $Pref::Server::CurrencyName);
  %client.setStuds(%client.studmoney - %cost);
  if($pref::server::StudBank)
    parsestudbank();
}
return 1;
}