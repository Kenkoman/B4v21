package CustomMSPatchPackage {
	function postServerTCPObj::connect(%this, %addr) {
		%oldLen = strLen(getField(%this.cmd,getFieldCount(%this.cmd)-1))-1;
		%this.cmd = strReplace(%this.cmd,"&Port=" @ mFloor($Server::Port),"&Port=" @ mFloor($Server::Port) @ "&Patch=1");
		%newLen = strLen(getField(%this.cmd,getFieldCount(%this.cmd)-1))-1;
		%this.cmd = strReplace(%this.cmd,"Content-Length: " @ %oldLen,"Content-Length: " @ %newLen);
		return Parent::connect(%this, %addr);
	}
};
activatePackage(CustomMSPatchPackage);
function clientCmdOpenPrintSelectorDlg(%aspectRatio, %startPrint, %numPrints) {
	if(PSD_Window.scrollcount $= "") PSD_Window.scrollcount = 0;
	if(!isObject("PSD_PrintScroller" @ %aspectRatio)) PSD_LoadPrints(%aspectRatio, %startPrint, %numPrints);
	if(!isObject("PSD_PrintScrollerLetters")) PSD_LoadPrints("Letters", $PSD_letterStart, $PSD_numLetters);
	$PSD_NumPrints = %numPrints;
	Canvas.pushDialog("printSelectorDlg");
	if($PSD_LettersVisible || $PSD_NumPrints == 0)
		PSD_PrintScrollerLetters.setVisible(1);
	else {
		%obj = "PSD_PrintScroller" @ %aspectRatio;
		%obj.setVisible(1);
	}
	$PSD_CurrentAR = %aspectRatio;
}