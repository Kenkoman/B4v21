function BotOpGUI::OnWake(%this) {
commandtoserver('UpdateClientBots');
commandtoserver('UpdateBotPatrols');
	if (!$BotTypesAdded) {
	BotTypePop.clear();
	BotTypePop.add("None", $addedBotTypes);
	$addedBotTypes++;
	BotTypePop.add("Patrol", $addedBotTypes);
	$addedBotTypes++;
	BotTypePop.add("Follow", $addedBotTypes);
	$addedBotTypes++;
	BotTypePop.add("Chop", $addedBotTypes);
	$addedBotTypes++;
	$BotTypesAdded = 1;
	}
}

function SelOwnBot() {
%bot = PsBotsList.getSelectedID();
$curSelBotGUI = %bot;
commandtoserver('BotSelectNorm', %bot);
commandtoserver('GetSelBotType', %bot);
}

function DeleteBotGUI() {
%bot = PsBotsList.getSelectedID();
commandtoserver('BotGUIDelete', %bot);
}

function BotOpGUI::setTypePane(%this, %pane)
{
BotOpGUI.clearTypePane();
%pane = "Bot" @ %pane @ "Pane";
%pane.setVisible(1);
}

function BotOpGUI::clearTypePane(%this)
{
BotPatrolPane.setVisible(0);
BotFollowPane.setVisible(0);
BotChoperPane.setVisible(0);
BotNonePane.setVisible(0);
}

function OpenBotOpGUI()
{
	if (!$botGUIOpen)
	{
	canvas.pushDialog(BotOpGUI);
	$botGUIOpen = 1;
	}
	else if ($botGUIOpen)
	{
	canvas.popDialog(BotOpGUI);
	$botGUIOpen = 0;
	}
}

function BotGUISetType(%val2) {
	%val = BotTypePop.getValue();
	BotOpGUI.setTypePane(%val);
		if (%val2)
		{
		%bot = PsBotsList.getSelectedId();
		commandtoserver('BotGUIType', %val, %bot);
		}
}

function CreateNewBot() {
	%name = BotNameGUI.getValue();
	commandtoserver('clientCreateBot', %name);
}

function BotGUISetPat()	{
	%val = BotPatrolListGUI.getValue();
	commandtoserver('BotStartPatrol', %val);
	echo(%val @ " is the %val");
}

function BotGUIstopPatrol()	{
	commandtoserver('BotStopPatrol');
}

function BotGUIaddPatrol()	{
	%group = BotGUINPN.getValue();
	commandtoserver('addcheck2', %group);
}


function BotGUISetGuard()	{
	%range = GRange.getValue();
	%team = GTTeam.getValue();
	%priv = GTPriv.getValue();
	%admin = GTAdmin.getValue();
	%friend = GTFriend.getValue();
	%kill = GTKillAll.getValue();
	commandtoserver('guardBot2', %range, %team, %priv, %admin, %friend, %kill);
}

function BotGUIEditGuard()	{
	%range = GRange.getValue();
	%team = GTTeam.getValue();
	%priv = GTPriv.getValue();
	%admin = GTAdmin.getValue();
	%friend = GTFriend.getValue();
	%kill = GTKillAll.getValue();
	commandtoserver('guardBot2E', %range, %team, %priv, %admin, %friend, %kill);
}

function BotGUIStopGuard()	{
	commandtoserver('StopGuardBot2');
}

function clientCmdClearBotClientList(%time)	{
	BotGUIClientList.clear();
}

function clientCmdABCL(%clientName, %client)	{
	echo(%clientName SPC "is the client name");
	BotGUIClientList.addRow(%client, %clientName, %client);
}

function BotGUIStartFol()	{
	BotGUIStopFol();
	%id = BotGUIClientList.getSelectedID();
	commandtoserver('BotGUIStartFol', %id);
}
function BotGUIStopFol()  {
	commandtoserver('BotGUIStopFol');
}

function clientCmdClearBotPatrols() {
	BotPatrolListGUI.clear();
	echo("clearing");
}

function clientCmdAP(%name) {
	BotPatrolListGUI.addRow(%name, %name, %name);
	echo("Adding a row");
}

function clientCmdClearClientBots() {
	PsBotsList.clear();
	PsBotsList.TotAm = 0;
	echo("clearing");
}

function clientCmdAPF(%botName, %botNum, %SelIt)	{
	PsBotsList.addRow(%botNum, %botName, %botNum);
	if (%SelIt)	{
	echo("selit = 1 agn");
	PsBotsList.setSelectedByID(%botNum);
	SelOwnBot();
	}
	PsBotsList.TotAm++;
	echo("Adding a row");
}

function clientCmdSetBotType(%type, %val2)	{
		BotTypePop.setValue(%type);
		BotGUISetType(%val2);
		BotOpGUI.setTypePane(%type);
}

function openBotApp() {
Appearance.UpBot = 1;
canvas.pushDialog(Appearance);
}

function clientCmdUpdateBotPrefs()
{
	commandtoserver('updateBotPrefs',
			$pref::BPlayer::Name,
			$pref::BColor::Skin,
			$pref::BAccessory::headCode,
			$pref::BAccessory::visorCode,
			$pref::BAccessory::backCode,
			$pref::BAccessory::leftHandCode,
			$pref::BAccessory::headColor,
			$pref::BAccessory::visorColor,
			$pref::BAccessory::backColor,
			$pref::BAccessory::leftHandColor,
			$pref::BDecal::chest,
			$pref::BDecal::face);

}

function upBotLooks()
{
commandtoserver('BotGUIUpBotAp', PsBotsList.getSelectedID());
}
moveMap.bindCmd(keyboard, "alt b", "OpenBotOpGUI();", "");