function changemapgui::onWake()
{
   CM_missionList.clear();
   %i = 0;
   for(%file = findFirstFile($Server::MissionFileSpec); %file !$= "";
   %file = findNextFile($Server::MissionFileSpec))
      if (strStr(%file, "/CVS/") == -1)
      {
         %mi = getMissionDisplayName(%file);

	 %desc = %mi.desc[0];
	 for(%d = 1; %d < %mi.desclines; %d++)
		%desc = %desc @ " " @ %mi.desc[%d];

         CM_missionList.addRow(%i++, %mi.name @ "\t" @ %file @ "\t" @ %mi.preview @ "\t" @ %desc);
	 %mi.delete();
      }
	CM_missionList.sort(0);
	CM_missionList.setSelectedRow(0);
	CM_missionList.scrollVisible(0);
}
function CM_StartMission() {
    %id = CM_missionList.getSelectedId();
    %mission = getField(CM_missionList.getRowTextById(%id), 1);
    commandToServer('changemap',%mission);
}
function CM_missionList::onSelect(%this, %row)
{
	%image = getField(CM_missionList.getRowTextById(%row), 2);
	PreviewImage2.setBitmap(%image);
	MissionDescription2.setText(getField(CM_missionList.getRowTextById(%row), 3));
}
function changemaplistGui::toggle(%this)
{
   if (%this.isAwake())
      Canvas.popDialog(%this);
   else
      Canvas.pushDialog(%this);
}

