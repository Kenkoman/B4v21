//msgCallbacks.cs

//client script, handles messages from the server

$invtog=1;
addMessageCallback('MsgError', handleError);

function handleError(){
//alxPlay(AudioError);
}


addMessageCallback('MsgItemPickup', handleItemPickup);

function handleItemPickup(%msgType, %pickupString, %slot, %invName)
{
if(%slot == -1)
return;
   nametoID("TxtInvSlot"@%slot++).setValue(detag(%invName));
   //alxPlay(ItemPickup);
}

       
addMessageCallback('MsgDropItem', handleDropItem);

function handleDropItem(%msgType, %string, %slot)
{
   //alxPlay(ItemDrop);
   nametoid("TxtInvSlot"@%slot++).setValue("");
   nametoid("Slot"@%slot@"BG").setBitmap("TBM/client/ui/GUIBrickSide.png");
}

addMessageCallback('MsgClearInv', handleClearInv);

function handleClearInv(%msgType)
{
    //clear the inventory gui
    $invHilight = -1;
    for(%i=1;%i<=20;%i++) {
        nametoid("TxtInvSlot"@%i).setValue("");
        nametoid("Slot"@%i@"BG").setBitmap("TBM/client/ui/GUIBrickSide.png");
    }
}
       
addMessageCallback('MsgHilightInv', handleHilightInv);

function handleHilightInv(%msgType, %msgString, %slot)
{
    if($invHilight !$= "")
    nametoid("Slot"@$invHilight@"BG").setBitmap("TBM/client/ui/GUIBrickSide.png");
    if(%slot == -1)
    return;
    nametoid("Slot"@%slot++@"BG").setBitmap("TBM/client/ui/GUIBrickSideHilight.png");
   //remember which one we have hilighted
   $invHilight = %slot;
}

addMessageCallback('MsgEquipInv', handleEquipInv);

function handleEquipInv(%msgType, %msgString, %slot)
{
   nametoid("Slot"@%slot++@"BG").setBitmap("TBM/client/ui/GUIBrickSideEquip.png");
}

addMessageCallback('MsgDeEquipInv', handleDeEquipInv);

function handleDeEquipInv(%msgType, %msgString, %slot)
{
   nametoid("Slot"@%slot++@"BG").setBitmap("TBM/client/ui/GUIBrickSide.png");
}
