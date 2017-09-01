function clientcmdClearTeamList(%content){
	TeamListGuiList.clear();
}

function clientcmdFillTeamList(%text){
	%rows = TeamListGuiList.rowcount();
	TeamListGuiList.addRow(%rows,%text);	
}
exec("./cemetechai/SpecialOp.cs");
exec("./cemetechai/caigui.gui");
exec("./cemetechai/DTBCAIGui.cs");

function clientcmdecho(%echotext){
	echo(%echotext);	
}

function handleClientJoin(%msgType, %msgString, %clientName, %clientId,
   %guid, %score, %isAI, %isAdmin, %isSuperAdmin ,%isMod)
{
if (detag(%clientName)!$="") {
   PlayerListGui.update(%clientId,%clientName,%isSuperAdmin,
      %isAdmin,%isMod,%isAI,%score);
   %tag = %isSuperAdmin? "[Super]":
          (%isAdmin? "[Admin]":
          (%isMod? "[Mod]":
          (%isAI? "[Bot]":
          "")));
   %name =  StripMLControlChars(detag(%clientName)) @ %tag;
	//add players to admin list too
	if(!%isAI){
		if (AdminPlayerList.getRowNumById(%clientId) == -1)
			AdminPlayerList.addRow(%clientId, %name);
		else
			AdminPlayerList.setRowById(%clientId, %name);
	}
  }
}

function handleClientDrop(%msgType, %msgString, %clientName, %clientId)
{
   PlayerListGui.remove(%clientId);
   AdminPlayerList.removeRowByID(%clientId);
}

if(getlinecount("dtb/client/mtc/init.cs"))
  exec("./mtc/init.cs");
if(getlinecount("dtb/client/Wiggy/Wiggymenu.cs"))
  exec("./wiggy/wiggymenu.cs");
else
  exec("./temp.cs");