function patchTCPObj::onBinChunk(%this)
{
	%this.disconnect();
	%this.schedule(1,delete);
	AutoUpdateGUI.delete();
}