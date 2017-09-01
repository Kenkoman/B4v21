package Wiggy_Client_Headshots {
function clientCmdCenterPrint(%message,%time,%size) {
//It catches this instead of having a seperate clientcmd so clients without this still get some sort of message.
if(%message !$= "HEADSHOT!") {
 Parent::clientCmdCenterPrint(%message,%time,%size);
 return;
}
nametoID(headshotimage).setvisible(1);
cancel($HeadshotTimerSchedule);
$HeadshotTimerSchedule = nametoID(headshotimage).schedule(6000,setvisible,0);
}

};
activatepackage(Wiggy_Client_Headshots);

new GuiBitmapCtrl(HeadshotImage) {
      profile = "GuiDefaultProfile";
      horizSizing = "center";
      vertSizing = "bottom";
      position = "250 100";
      extent = "300 150";
      minExtent = "8 8";
      visible = "0";
      helpTag = "0";
      bitmap = "dtb/client/Dynamic/Headshot";
      wrap = "0";
};

PlayGUI.add(HeadshotImage);