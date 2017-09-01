//msgCallbacks.cs

//client script, handles messages from the server
//##########################################
//# 04 STUFF
//##########################################
addMessageCallback('updatemBrick', updateMBrickPos);

function updateMBrickPos(%msgType, %string, %Pos, %Rot)
{
	mBrickXYZPos.setValue(%Pos);
	mBrickXYZRot.setValue(%Rot);
}

addMessageCallback('setRules', setServerRulestxt);

function setServerRulestxt(%msgType, %string, %rules)
{
	txtRules.setValue(%rules);
}

addMessageCallback('MsgUpdateWrenchB', updateWrenchSettingsB);

function updateWrenchSettingsB(%msgType, %string, %IsImpulseTrigger, %returnDelay, %returnToggle, %password, %DoorType, %TriggerDoorID, %NoCollision, %YesNo)
{
	if(%YesNo $= 1)
	{
		$Pref::Wrench::TriggerImp = %IsImpulseTrigger;
		$Pref::Wrench::ReturnDelay = %returnDelay;
		$Pref::Wrench::ReturnToggle = %returnToggle;
		$Pref::Wrench::Password = %Password;
		$Pref::Wrench::MTypeS = %DoorType;
		$Pref::Wrench::DoorSetID = %TriggerDoorID;
		$Pref::Wrench::NoCollision = %noCollision;
	}

	Password.setValue($Pref::Wrench::Password);
	ReturnDelay.setValue($Pref::Wrench::ReturnDelay);
	ReturnToggle.setValue($Pref::Wrench::ReturnToggle);
	if($Pref::Wrench::MTypeS $= 1)
	{
		triggertxt.setValue("Trigger Options:");
		nocollision.setVisible(false);
		noCollision.setValue(0);
		isJumperTrigger.setValue($Pref::Wrench::TriggerImp);
		isJumperTrigger.setVisible(true);
		dsid.setValue($Pref::Wrench::DoorSetID);
		dsid.setVisible(true);
		dsidDown.setVisible(true);
		dsidUp.setVisible(true);
		dsidtxt.setVisible(true);
		$Pref::Wrench::MTypeS = "Trigger";
		optTrigger.setValue(1);
		optNormal.setValue(0);
		optTriggered.setValue(0);
	}
	else if($Pref::Wrench::MTypeS $= 2)
	{
		triggertxt.setValue("Triggered Options:");
		nocollision.setVisible(true);
		noCollision.setValue($Pref::Wrench::NoCollision);
		isJumperTrigger.setValue(0);
		isJumperTrigger.setVisible(false);
		dsid.setValue($Pref::Wrench::DoorSetID);
		dsid.setVisible(true);
		dsidDown.setVisible(true);
		dsidUp.setVisible(true);
		dsidtxt.setVisible(true);
		$Pref::Wrench::MTypeS = "Triggered";
		isJumperTrigger.setValue(0);
		optTriggered.setValue(1);
		optTrigger.setValue(0);
		optNormal.setValue(0);
	}
	else if($Pref::Wrench::MTypeS $= 3)
	{
		triggertxt.setValue("");
		nocollision.setVisible(false);
		isJumperTrigger.setVisible(false);
		dsid.setVisible(false);
		dsidDown.setVisible(false);
		dsidUp.setVisible(false);
		dsidtxt.setVisible(false);
		$Pref::Wrench::MTypeS = "Normal";
		noCollision.setValue(0);
		isJumperTrigger.setValue(0);
		optTriggered.setValue(0);
		optTrigger.setValue(0);
		optNormal.setValue(1);
	}

}

addMessageCallback('MsgUpdateWrenchA', updateWrenchSettingsA);

function updateWrenchSettingsA(%msgType, %string, %MoveXYZ, %RotateXYZ, %Steps, %StepTime, %Elevator, %TriggerCloak, %Private, %Team, %Group, %YesNo)
{
	if(%YesNo $= 1)
	{
		$Pref::Wrench::MoveXYZ = %MoveXYZ;
		$Pref::Wrench::RotateXYZ = %RotateXYZ;
		$Pref::Wrench::Steps = %Steps;
		$Pref::Wrench::StepTime = %StepTime;
		$Pref::Wrench::Elevator = %elevator;
		$Pref::Wrench::TriggerCloak = %triggerCloak;
		$Pref::Wrench::Private = %Private;
		$Pref::Wrench::Team = %Team;
		$Pref::Wrench::Group = %Group;
	}
	
	MoveXYZ.setValue($Pref::Wrench::MoveXYZ);
	RotateXYZ.setValue($Pref::Wrench::RotateXYZ);
	Steps.setValue($Pref::Wrench::Steps);
	StepTime.setValue($Pref::Wrench::StepTime);
	Elevator.setValue($Pref::Wrench::Elevator);
	TriggerCloak.setValue($Pref::Wrench::TriggerCloak);
	Private.setValue($Pref::Wrench::Private);
	Team.setValue($Pref::Wrench::Team);
	Group.setValue($Pref::Wrench::Group);
}

//##########################################


addMessageCallback('MsgError', handleError);

function handleError()
{
	alxPlay(AudioError);
}

addMessageCallback('MsgChangePreview', handlePreview);

function handlePreview(%msgType, %string, %this)
{
	if(%this $= "")
	{
		%this = "rtb/client/ui/hudfill.png";
	}
	BrickIcon.setBitmap(%this);
}

addMessageCallback('MsgUpdateMoney', handleMoney);

function handleMoney(%msgType, %string, %money)
{
	Money.SetValue("$"@%money);
	commandtoserver('storeCash');
}

addMessageCallback('MsgGetObjectData', handleObject);

function handleObject(%msgType, %string, %pos, %rot, %scale)
{
	txtPosition.setValue(%pos);
	txtRotation.setValue(%rot);
	txtScale.setValue(%scale);

}

addMessageCallback('addToTypingList', addToTyping);

function addToTyping(%msgType, %string, %name)
{
	%nameString = StripMLControlChars(deTag(%name));
	TxtTyping.setValue(%nameString);
}

addMessageCallback('MsgItemPickup', handleItemPickup);

function handleItemPickup(%msgType, %pickupString, %slot, %invName)
{
	%slot = %slot+1;

	%invName = detag(%invName);
	
	switch (%slot)
	{
		case (1):
			TxtInvSlot1.setValue(%invName);
		case (2):
			TxtInvSlot2.setValue(%invName);
		case (3):
			TxtInvSlot3.setValue(%invName);
		case (4):
			TxtInvSlot4.setValue(%invName);
		case (5):
			TxtInvSlot5.setValue(%invName);
		case (6):
			TxtInvSlot6.setValue(%invName);
		case (7):
			TxtInvSlot7.setValue(%invName);
		case (8):
			TxtInvSlot8.setValue(%invName);
		case (9):
			TxtInvSlot9.setValue(%invName);
		case (10):
			TxtInvSlot10.setValue(%invName);

	}
	alxPlay(ItemPickup);
}
        
addMessageCallback('MsgDropItem', handleDropItem);

function handleDropItem(%msgType, %string, %slot)
{
	%slot = %slot+1;

	switch (%slot)
	{
		case (1):
			TxtInvSlot1.setValue("");
			Slot1BG.setBitmap("rtb/client/ui/GUIBrickSide.png");
		case (2):
			TxtInvSlot2.setValue("");
			Slot2BG.setBitmap("rtb/client/ui/GUIBrickSide.png");
		case (3):
			TxtInvSlot3.setValue("");
			Slot3BG.setBitmap("rtb/client/ui/GUIBrickSide.png");
		case (4):
			TxtInvSlot4.setValue("");
			Slot4BG.setBitmap("rtb/client/ui/GUIBrickSide.png");
		case (5):
			TxtInvSlot5.setValue("");
			Slot5BG.setBitmap("rtb/client/ui/GUIBrickSide.png");
		case (6):
			TxtInvSlot6.setValue("");
			Slot6BG.setBitmap("rtb/client/ui/GUIBrickSide.png");
		case (7):
			TxtInvSlot7.setValue("");
			Slot7BG.setBitmap("rtb/client/ui/GUIBrickSide.png");
		case (8):
			TxtInvSlot8.setValue("");
			Slot8BG.setBitmap("rtb/client/ui/GUIBrickSide.png");
		case (9):
			TxtInvSlot9.setValue("");
			Slot9BG.setBitmap("rtb/client/ui/GUIBrickSide.png");
		case (10):
			TxtInvSlot10.setValue("");
			Slot10BG.setBitmap("rtb/client/ui/GUIBrickSide.png");


	}
	//alxPlay(ItemDrop);
}

addMessageCallback('MsgClearInv', handleClearInv);

function handleClearInv(%msgType)
{
	//clear the inventory gui
	$invHilight = -1;

	TxtInvSlot1.setValue("");
	TxtInvSlot2.setValue("");
	TxtInvSlot3.setValue("");
	TxtInvSlot4.setValue("");
	TxtInvSlot5.setValue("");
	TxtInvSlot6.setValue("");
	TxtInvSlot7.setValue("");
	TxtInvSlot8.setValue("");
	TxtInvSlot9.setValue("");
	TxtInvSlot10.setValue("");

	Slot1BG.setBitmap("rtb/client/ui/GUIBrickSide.png");
	Slot2BG.setBitmap("rtb/client/ui/GUIBrickSide.png");
	Slot3BG.setBitmap("rtb/client/ui/GUIBrickSide.png");
	Slot4BG.setBitmap("rtb/client/ui/GUIBrickSide.png");
	Slot5BG.setBitmap("rtb/client/ui/GUIBrickSide.png");
	Slot6BG.setBitmap("rtb/client/ui/GUIBrickSide.png");
	Slot7BG.setBitmap("rtb/client/ui/GUIBrickSide.png");
	Slot8BG.setBitmap("rtb/client/ui/GUIBrickSide.png");
	Slot9BG.setBitmap("rtb/client/ui/GUIBrickSide.png");
	Slot10BG.setBitmap("rtb/client/ui/GUIBrickSide.png");

}
        
addMessageCallback('MsgHilightInv', handleHilightInv);

function handleHilightInv(%msgType, %msgString, %slot)
{
	%slot++;

	//turn off current hilight
	switch($invHilight)
	{
		case (1):
			Slot1BG.setBitmap("rtb/client/ui/GUIBrickSide.png");
		case (2):
			Slot2BG.setBitmap("rtb/client/ui/GUIBrickSide.png");
		case (3):
			Slot3BG.setBitmap("rtb/client/ui/GUIBrickSide.png");
		case (4):
			Slot4BG.setBitmap("rtb/client/ui/GUIBrickSide.png");
		case (5):
			Slot5BG.setBitmap("rtb/client/ui/GUIBrickSide.png");
		case (6):
			Slot6BG.setBitmap("rtb/client/ui/GUIBrickSide.png");
		case (7):
			Slot7BG.setBitmap("rtb/client/ui/GUIBrickSide.png");
		case (8):
			Slot8BG.setBitmap("rtb/client/ui/GUIBrickSide.png");
		case (9):
			Slot9BG.setBitmap("rtb/client/ui/GUIBrickSide.png");
		case (10):
			Slot10BG.setBitmap("rtb/client/ui/GUIBrickSide.png");
	}

	switch (%slot)
	{
		case (1):
			Slot1BG.setBitmap("rtb/client/ui/GUIBrickSideHilight.png");
		case (2):
			Slot2BG.setBitmap("rtb/client/ui/GUIBrickSideHilight.png");
		case (3):
			Slot3BG.setBitmap("rtb/client/ui/GUIBrickSideHilight.png");
		case (4):
			Slot4BG.setBitmap("rtb/client/ui/GUIBrickSideHilight.png");
		case (5):
			Slot5BG.setBitmap("rtb/client/ui/GUIBrickSideHilight.png");
		case (6):
			Slot6BG.setBitmap("rtb/client/ui/GUIBrickSideHilight.png");
		case (7):
			Slot7BG.setBitmap("rtb/client/ui/GUIBrickSideHilight.png");
		case (8):
			Slot8BG.setBitmap("rtb/client/ui/GUIBrickSideHilight.png");
		case (9):
			Slot9BG.setBitmap("rtb/client/ui/GUIBrickSideHilight.png");
		case (10):
			Slot10BG.setBitmap("rtb/client/ui/GUIBrickSideHilight.png");
	}

	//remember which one we have hilighted
	$invHilight = %slot;
}

addMessageCallback('MsgEquipInv', handleEquipInv);

function handleEquipInv(%msgType, %msgString, %slot)
{
	%slot++;
	switch (%slot)
	{
		case (1):
			Slot1BG.setBitmap("rtb/client/ui/GUIBrickSideEquip.png");
		case (2):
			Slot2BG.setBitmap("rtb/client/ui/GUIBrickSideEquip.png");
		case (3):
			Slot3BG.setBitmap("rtb/client/ui/GUIBrickSideEquip.png");
		case (4):
			Slot4BG.setBitmap("rtb/client/ui/GUIBrickSideEquip.png");
		case (5):
			Slot5BG.setBitmap("rtb/client/ui/GUIBrickSideEquip.png");
		case (6):
			Slot6BG.setBitmap("rtb/client/ui/GUIBrickSideEquip.png");
		case (7):
			Slot7BG.setBitmap("rtb/client/ui/GUIBrickSideEquip.png");
		case (8):
			Slot8BG.setBitmap("rtb/client/ui/GUIBrickSideEquip.png");
		case (9):
			Slot9BG.setBitmap("rtb/client/ui/GUIBrickSideEquip.png");
		case (10):
			Slot10BG.setBitmap("rtb/client/ui/GUIBrickSideEquip.png");
	}
}

addMessageCallback('MsgDeEquipInv', handleDeEquipInv);

function handleDeEquipInv(%msgType, %msgString, %slot)
{
	%slot++;
	switch (%slot)
	{
		case (1):
			Slot1BG.setBitmap("rtb/client/ui/GUIBrickSide.png");
		case (2):
			Slot2BG.setBitmap("rtb/client/ui/GUIBrickSide.png");
		case (3):
			Slot3BG.setBitmap("rtb/client/ui/GUIBrickSide.png");
		case (4):
			Slot4BG.setBitmap("rtb/client/ui/GUIBrickSide.png");
		case (5):
			Slot5BG.setBitmap("rtb/client/ui/GUIBrickSide.png");
		case (6):
			Slot6BG.setBitmap("rtb/client/ui/GUIBrickSide.png");
		case (7):
			Slot7BG.setBitmap("rtb/client/ui/GUIBrickSide.png");
		case (8):
			Slot8BG.setBitmap("rtb/client/ui/GUIBrickSide.png");
		case (9):
			Slot9BG.setBitmap("rtb/client/ui/GUIBrickSide.png");
		case (10):
			Slot10BG.setBitmap("rtb/client/ui/GUIBrickSide.png");
	}
}
