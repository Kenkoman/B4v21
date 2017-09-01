$QuickInventoryGuiTog = 0; //Noob protect
function togQuickInventoryGui (%val)
{
    if(%val)
	QuickInventoryGui.toggle();
}
//function QuickInventoryGui::OnWake() {
//setquickinvlist($LastInv);
//}

function QuickInv_slot1::onMouseMove(%this) {
setquickinvlist(1);
}
function QuickInv_slot2::onMouseMove(%this) {
setquickinvlist(2);
}
function QuickInv_slot3::onMouseMove(%this) {
setquickinvlist(3);
}
function QuickInv_slot4::onMouseMove(%this) {
setquickinvlist(4);
}
function QuickInv_slot5::onMouseMove(%this) {
setquickinvlist(5);
}
function QuickInv_slot6::onMouseMove(%this) {
setquickinvlist(6);
}
function QuickInv_slot7::onMouseMove(%this) {
setquickinvlist(7);
}
function QuickInv_slot8::onMouseMove(%this) {
setquickinvlist(8);
}
function QuickInv_slot9::onMouseMove(%this) {
setquickinvlist(9);
}
function QuickInv_slot10::onMouseMove(%this) {
setquickinvlist(10);
}

function setquickinvlist(%var) {
if(%var $= $Lastvar)
return; //dont need to request the list we already have.
$LastInv = %var;
QuickInv_list.clear();
commandtoserver('getInventoryInfo',%var);
QuickInv_highlight.position = (getWord(QuickInv_highlight.position,0)) SPC (%var*20)-18;
}

function clientcmdBuildInvlist(%brickID,%brickname){
QuickInv_list.addrow(%brickID,"\c3"@%brickname);
}

function QuickInv_list::OnSelect(%this,%id) {
commandtoserver('quickchangebrick',%id);
}

function quickselectinv() {
commandtoserver('quickchangebrick',QuickInv_list.getSelectedID());
}

function GuiControl::toggle(%this) {
if(!%this.isAwake())
Canvas.pushdialog(%this);
else
Canvas.popDialog(%this);
}

new GuiControlProfile (QuickInventory_WindowProfile)
{
   opaque = true;
   border = 2;
   fillColor = "0 0 0";
   fillColorHL = "64 150 150";
   fillColorNA = "150 150 150";
   fontcolor = "255 255 255";
   fontType = "Arial Bold";
   fontSize = 16;
   fontColor = "255 255 255";
   fontColorHL = "0 0 0";
   text = "TBM_Window";
   bitmap = "tbm/client/ui/window_images/window.chatskin";
   textOffset = "18 13";
   hasBitmapArray = true;
   justify = "left";
};


new GuiControlProfile (QuickInventory_ScrollProfile)
{
   opaque = false;
   fillColor = "0 0 0 0";
   border = 1;
   borderThickness = 1;
   borderColor = "0 255 0 160";
   fontcolorHL = "255 255 255 64";
   bitmap = "~/client/ui/QuickInventory.scroll";
   hasBitmapArray = true;
};
