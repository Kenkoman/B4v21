//msgCallbacks.cs

//client script, handles messages from the server


addMessageCallback('MsgError', handleError);

function handleError()
{
	alxPlay(AudioError);
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
			Slot1BG.setBitmap("fps/client/ui/GUIBrickSide.png");
		case (2):
			TxtInvSlot2.setValue("");
			Slot2BG.setBitmap("fps/client/ui/GUIBrickSide.png");
		case (3):
			TxtInvSlot3.setValue("");
			Slot3BG.setBitmap("fps/client/ui/GUIBrickSide.png");
		case (4):
			TxtInvSlot4.setValue("");
			Slot4BG.setBitmap("fps/client/ui/GUIBrickSide.png");
		case (5):
			TxtInvSlot5.setValue("");
			Slot5BG.setBitmap("fps/client/ui/GUIBrickSide.png");

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

	Slot1BG.setBitmap("fps/client/ui/GUIBrickSide.png");
	Slot2BG.setBitmap("fps/client/ui/GUIBrickSide.png");
	Slot3BG.setBitmap("fps/client/ui/GUIBrickSide.png");
	Slot4BG.setBitmap("fps/client/ui/GUIBrickSide.png");
	Slot5BG.setBitmap("fps/client/ui/GUIBrickSide.png");
}
        
addMessageCallback('MsgHilightInv', handleHilightInv);

function handleHilightInv(%msgType, %msgString, %slot)
{
	%slot++;

	//turn off current hilight
	switch($invHilight)
	{
		case (1):
			Slot1BG.setBitmap("fps/client/ui/GUIBrickSide.png");
		case (2):
			Slot2BG.setBitmap("fps/client/ui/GUIBrickSide.png");
		case (3):
			Slot3BG.setBitmap("fps/client/ui/GUIBrickSide.png");
		case (4):
			Slot4BG.setBitmap("fps/client/ui/GUIBrickSide.png");
		case (5):
			Slot5BG.setBitmap("fps/client/ui/GUIBrickSide.png");
	}

	switch (%slot)
	{
		case (1):
			Slot1BG.setBitmap("fps/client/ui/GUIBrickSideHilight.png");
		case (2):
			Slot2BG.setBitmap("fps/client/ui/GUIBrickSideHilight.png");
		case (3):
			Slot3BG.setBitmap("fps/client/ui/GUIBrickSideHilight.png");
		case (4):
			Slot4BG.setBitmap("fps/client/ui/GUIBrickSideHilight.png");
		case (5):
			Slot5BG.setBitmap("fps/client/ui/GUIBrickSideHilight.png");
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
			Slot1BG.setBitmap("fps/client/ui/GUIBrickSideEquip.png");
		case (2):
			Slot2BG.setBitmap("fps/client/ui/GUIBrickSideEquip.png");
		case (3):
			Slot3BG.setBitmap("fps/client/ui/GUIBrickSideEquip.png");
		case (4):
			Slot4BG.setBitmap("fps/client/ui/GUIBrickSideEquip.png");
		case (5):
			Slot5BG.setBitmap("fps/client/ui/GUIBrickSideEquip.png");
	}
}

addMessageCallback('MsgDeEquipInv', handleDeEquipInv);

function handleDeEquipInv(%msgType, %msgString, %slot)
{
	%slot++;
	switch (%slot)
	{
		case (1):
			Slot1BG.setBitmap("fps/client/ui/GUIBrickSide.png");
		case (2):
			Slot2BG.setBitmap("fps/client/ui/GUIBrickSide.png");
		case (3):
			Slot3BG.setBitmap("fps/client/ui/GUIBrickSide.png");
		case (4):
			Slot4BG.setBitmap("fps/client/ui/GUIBrickSide.png");
		case (5):
			Slot5BG.setBitmap("fps/client/ui/GUIBrickSide.png");
	}
}