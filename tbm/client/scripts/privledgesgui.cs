function PrivledgesGUI::OnWake(%this) {
$privguiTog = 1;
}
function PrivledgesGUI::OnSleep(%this) {
$privguiTog = 0;
}

function privtog(%panelnum) {
    switch$(%panelnum) {
    case "1":
        Privpanel1.setVisible(1);
        Privpanel2.setVisible(0);
        Privpanel3.setVisible(0);
        Privpanel4.setVisible(0);
    case "2":
        Privpanel1.setVisible(0);
        Privpanel2.setVisible(1);
        Privpanel3.setVisible(0);
        Privpanel4.setVisible(0);
    case "3":
        Privpanel1.setVisible(0);
        Privpanel2.setVisible(0);
        Privpanel3.setVisible(1);
        Privpanel4.setVisible(0);
    case "4":
        Privpanel1.setVisible(0);
        Privpanel2.setVisible(0);
        Privpanel3.setVisible(0);
        Privpanel4.setVisible(1);
    }
}

function buildprivlists() {
	%file = new FileObject();
	%file.openForRead("tbm/server/admins.cs");
	while(!%file.isEOF())
	{
		%name = %file.readLine();
        %ip = %file.readLine();
		if(%ip $= "")
			continue;
		else
            txtprivlist2.addRow(%i++,%name TAB %ip);
	}
	%file.close();
	%file.delete();
}
