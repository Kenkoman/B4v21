function OptPlayerSkinColorMenu::onSelect(%this, %id, %text)
{
	if(getWordCount(%text) >= 2)
	{
		if(%text $= "Black Glitter")
		   %text = "blackglitter";
		else
		   %text = getWord(%text, 1)@getWord(%text, 0);
	}
	printbackG.setBitmap("rtb/data/shapes/"@%text@".blank.jpg");
}

function printList::onAction(%this)
{
   %sel = %this.getSelected();
   if(%this.getValue(%sel) !$= "")
   {
   	echo(%this.getValue(%sel));
	printpreview.setBitmap(%this.getValue(%sel));
	%image2 = %this.getValue(%sel);
        %image = getSubStr(fileBase(%image2), 0, StrStr(fileBase(%image2), "."));
	if (Appearance.UpBot) {$pref::BDecal::chest = %image2 = getword(%image,0); clientCmdUpdateBotPrefs();}
	else {$pref::Decal::chest = getword(%image,0); clientCmdUpdatePrefs();}
	clientCmdUpdatePrefs();
   }
}

function appearance::onWake(%this)
{
   printpreview.setBitmap("rtb/data/shapes/player/decals/"@$Pref::Decal::Chest@".decal.png");
   %this = "printList";
   %this.clear();

   %DecalId = "";
   %file = findFirstFile( "rtb/data/shapes/player/decals/*.decal.png" );
   
   while( %file !$= "" ) 
   {
      %split = getSubStr(%file, 30, 300);
      %foldername = getSubStr(%split, 0, StrStr(%split, "-"));
      %parentId = %base;

      %parent = %foldername;
      if ( !%DecalId[%parent] && %parent !$= "")
      {
      %DecalId[%parent] = %this.addGroup( %parentId, %foldername);
      }
      %parentId = %DecalId[%parent];

      // Add the file to the group
      %filename = getSubStr(fileBase(%file), 0, StrStr(fileBase(%file), "."));
      %filename = getSubStr(%filename, StrStr(%filename, "-")+1, 100);
      %this.addItem( %parentId, %filename, %file );
   
      %file = findNextFile( "rtb/data/shapes/player/decals/*.decal.png" );
   }









	FacePrintlist.clear();
	%i = 0;
	for(%file = findFirstFile("*/shapes/player/face decals/*.facedecal.png");
		%file !$= ""; %file = findNextFile("*/shapes/player/face decals/*.facedecal.png"))  
	if (strStr(%file, "CVS/") == -1 && strStr(%file, "common/") == -1)
	{
		Faceprintlist.addRow(%i++, getdecalName(%file) @ "\t" @ %file );
		$TotalFaceDecals++;
	}
	FacePrintlist.sort(0);
	FacePrintlist.scrollVisible(1);
	if (Appearance.UpBot) {FacePrintlist.setSelectedRow($TotalFaceDecals - $Pref::BDecal::FaceRow); echo("boton");}
	else {FacePrintlist.setSelectedRow($TotalFaceDecals - $Pref::Decal::FaceRow); echo("botoff");}

if (Appearance.UpBot) {TxtPlayerName.setValue($pref::BPlayer::Name);}
else {TxtPlayerName.setValue($pref::Player::Name);}
	
	%colorcount = 0;
	%Color[%colorcount] = "Light Red";
	%Color[%colorcount++] = "Red";
	%Color[%colorcount++] = "Dark Red";
	%Color[%colorcount++] = "Orange";
	%Color[%colorcount++] = "Light Yellow";
	%Color[%colorcount++] = "Yellow";
	%Color[%colorcount++] = "Dark Yellow";
	%Color[%colorcount++] = "Light Green";
	%Color[%colorcount++] = "Green";
	%Color[%colorcount++] = "Dark Green";
	%Color[%colorcount++] = "Light Blue";
	%Color[%colorcount++] = "Blue";
	%Color[%colorcount++] = "Dark Blue";
	%Color[%colorcount++] = "Light Purple";
	%Color[%colorcount++] = "Purple";
	%Color[%colorcount++] = "Dark Purple";
	%Color[%colorcount++] = "Tan";
	%Color[%colorcount++] = "Dark Tan";
	%Color[%colorcount++] = "Light Brown";
	%Color[%colorcount++] = "Brown";
	%Color[%colorcount++] = "White";
	%Color[%colorcount++] = "Base";
	%Color[%colorcount++] = "Dark Gray";
	%Color[%colorcount++] = "Black";
	%Color[%colorcount++] = "Black Glitter";



	//clear the menu first to prevent dup entries
	OptPlayerSkinColorMenu.clear();
	
	for(%t=0; %t<%colorcount+1; %t++)
	{
		OptPlayerSkinColorMenu.add(%Color[%t], %t);
	}
	if (Appearance.UpBot) {OptPlayerSkinColorMenu.setSelected($pref::BColor::skin);}
	else {OptPlayerSkinColorMenu.setSelected($pref::Color::skin);}


	%statusTotal = 0;
	StatusSelect.clear();
	StatusSelect.add("None", -1);
	StatusSelect.add("Back Soon", %statusTotal++);
	StatusSelect.add("Away", %statusTotal);
	StatusSelect.add("Scripting", %statusTotal++);
	StatusSelect.add("Eating", %statusTotal++);
	StatusSelect.add("Sleeping", %statusTotal++);
	//StatusSelect.add("", %statusTotal++); 

	OptPlayerHeadMenu.clear();
	OptPlayerHeadMenu.add("None", -1);
	%headTotal = 0;
	OptPlayerHeadMenu.add("Space Helmet", %headTotal);
	OptPlayerHeadMenu.add("Scout Hat", %headTotal++);
	OptPlayerHeadMenu.add("Pointy Helmet",  %headTotal++);
	OptPlayerHeadMenu.add("Hair(male)",  %headTotal++);
	OptPlayerHeadMenu.add("Hair(female)",  %headTotal++);
	OptPlayerHeadMenu.add("Hair(female2)",  %headTotal++);
	OptPlayerHeadMenu.add("Wizard Hat",  %headTotal++);
	OptPlayerHeadMenu.add("Cowboy Hat",  %headTotal++);
	OptPlayerHeadMenu.add("Pirate Hat",  %headTotal++);
	OptPlayerHeadMenu.add("Bicourne",  %headTotal++);
	OptPlayerHeadMenu.add("Darth Vader",  %headTotal++);
	OptPlayerHeadMenu.add("Samurai Helm",  %headTotal++);
	OptPlayerHeadMenu.add("Cap",  %headTotal++);
	OptPlayerHeadMenu.add("Castle Hemet W/Neck Protector",  %headTotal++);
	OptPlayerHeadMenu.add("Chef Hat",  %headTotal++);
	OptPlayerHeadMenu.add("Construction Helmet",  %headTotal++);
	OptPlayerHeadMenu.add("Police Hat",  %headTotal++);
	////added june 22nd 05///
	OptPlayerHeadMenu.add("Storm Trooper Helmet",  %headTotal++);
	OptPlayerHeadMenu.add("Cap 2",  %headTotal++);
	OptPlayerHeadMenu.add("Firemen Helmet",  %headTotal++);
	OptPlayerHeadMenu.add("Imperial Guard Shako",  %headTotal++);
	OptPlayerHeadMenu.add("Knit Cap(Beanie)",  %headTotal++);
	OptPlayerHeadMenu.add("Tophat",  %headTotal++);
	OptPlayerHeadMenu.add("Morion Helmet",  %headTotal++);
	OptPlayerHeadMenu.add("Old Space Helmet",  %headTotal++);
	OptPlayerHeadMenu.add("Pith Helmet", %headtotal++);
	OptPlayerHeadMenu.add("Farmers Hat", %headtotal++);
	OptPlayerHeadMenu.add("Hat MidBrim Flat", %headtotal++);
	OptPlayerHeadMenu.add("Chinstrap WideBrim Helmet", %headtotal++);
	OptPlayerHeadMenu.add("Tricorner", %headtotal++);
 OptPlayerHeadMenu.add("Aviator Cap", %headtotal++);
 OptPlayerHeadMenu.add("Cavalry Cap", %headtotal++);
 OptPlayerHeadMenu.add("Scout Trooper Helmet", %headtotal++);
 OptPlayerHeadMenu.add("Crown", %headtotal++);
 OptPlayerHeadMenu.add("Islander Hair", %headtotal++);



	if (Appearance.UpBot) {OptPlayerHeadMenu.setSelected($pref::BAccessory::headCode);}
	else {OptPlayerHeadMenu.setSelected($pref::Accessory::headCode);}
	OptPlayerHeadMenu.onSelect();

	OptPlayerHeadColorMenu.clear();
	for(%t=0; %t<%colorcount+1; %t++)
	{
		OptPlayerHeadColorMenu.add(%Color[%t], %t);
	}
	if (Appearance.UpBot) {OptPlayerHeadColorMenu.setSelected($pref::BAccessory::headColor);}
	else {OptPlayerHeadColorMenu.setSelected($pref::Accessory::headColor);}

	OptPlayerExtraMenu.clear();
	OptPlayerExtraMenu.add("None", -1);
	%extraTotal = 0;
	OptPlayerExtraMenu.add("Beard", %extraTotal);
	OptPlayerExtraMenu.add("Shield", %extraTotal++);
	OptPlayerExtraMenu.add("Goblet", %extraTotal++);

	OptPlayerExtraMenu.add("Binoculars", %extraTotal++);
	OptPlayerExtraMenu.add("Camera", %extraTotal++);
	OptPlayerExtraMenu.add("Vid Cam", %extraTotal++);
	OptPlayerExtraMenu.add("Drill", %extraTotal++);
	OptPlayerExtraMenu.add("Radio", %extraTotal++);
	OptPlayerExtraMenu.add("Frying Pan", %extraTotal++);
	OptPlayerExtraMenu.add("Briefcase", %extraTotal++);
	OptPlayerExtraMenu.add("Goblet 2", %extraTotal++);
	////added June 22nd 2005//
	OptPlayerExtraMenu.add("100$ Bill", %extraTotal++);
	OptPlayerExtraMenu.add("Shovel", %extraTotal++);
	OptPlayerExtraMenu.add("ScrewDriver", %extraTotal++);
	OptPlayerExtraMenu.add("Fishing Rod", %extraTotal++);
	OptPlayerExtraMenu.add("Magnifying Glass", %extraTotal++);
	OptPlayerExtraMenu.add("Cup", %extraTotal++);

	if (Appearance.UpBot) {OptPlayerExtraMenu.setSelected($pref::BAccessory::LeftHandCode);}
	else {OptPlayerExtraMenu.setSelected($pref::Accessory::LeftHandCode);}

	OptPlayerExtraColorMenu.clear();
	for(%t=0; %t<%colorcount+1; %t++)
	{
		OptPlayerExtraColorMenu.add(%Color[%t], %t);
	}
	if (Appearance.UpBot) {OptPlayerExtraColorMenu.setSelected($pref::BAccessory::LeftHandColor);}
	else {OptPlayerExtraColorMenu.setSelected($pref::Accessory::LeftHandColor);}


	//visor colors
	OptPlayerVisorColorMenu.clear();
	for(%t=0; %t<%colorcount+1; %t++)
	{
		OptPlayerVisorColorMenu.add(%Color[%t], %t);
	}
	if (Appearance.UpBot) {OptPlayerVisorColorMenu.setSelected($pref::BAccessory::VisorColor);}
	else {OptPlayerVisorColorMenu.setSelected($pref::Accessory::VisorColor);}

	OptPlayerBackMenu.clear();
	OptPlayerBackMenu.add("None", -1);
	OptPlayerBackMenu.add("Cape", 0);
	OptPlayerBackMenu.add("Bucket", 1);
	OptPlayerBackMenu.add("Quiver", 2);
	OptPlayerBackMenu.add("Armor", 3);
	OptPlayerBackMenu.add("Pack", 4);
	OptPlayerBackMenu.add("Air Tank", 5);
	OptPlayerBackMenu.add("Cloak", 6);
	OptPlayerBackMenu.add("Samurai Armor", 7);
	OptPlayerBackMenu.add("Life Jacket", 8);
	OptPlayerBackMenu.add("Jet Pack", 9);
	OptPlayerBackMenu.add("Scuba Gear", 10);
	OptPlayerBackMenu.add("Epaulets", 11);

	if (Appearance.UpBot) {OptPlayerBackMenu.setSelected($pref::BAccessory::BackCode);}
	else {OptPlayerBackMenu.setSelected($pref::Accessory::BackCode);}

	OptPlayerBackColorMenu.clear();
	for(%t=0; %t<%colorcount+1; %t++)
	{
		OptPlayerBackColorMenu.add(%Color[%t], %t);
	}
	if (Appearance.UpBot) {OptPlayerBackColorMenu.setSelected($pref::BAccessory::BackColor);}
	else {OptPlayerBackColorMenu.setSelected($pref::Accessory::BackColor);}
}

function appdone()
{
	if(OverrideCheck.getValue() == 1)
	{
			if(skeleradio.getValue() == 1)
			{
				echo("done");
				commandtoserver('doskele');
			}
			if(droidradio.getValue() == 1)
			{
				commandtoserver('droidme');
			}
	}
	else
	{
		commandtoserver('noOverride');
	}
	canvas.popdialog(appearance);
}

function appearance::onSleep(%this)
{
	$pref::Player::Name = TxtPlayerName.getValue();

	$pref::Accessory::headCode = OptPlayerHeadMenu.getSelected();
	$pref::Accessory::headColor = OptPlayerHeadColorMenu.getSelected();
	
	$pref::Accessory::visorCode = OptPlayerVisorMenu.getSelected();
	$pref::Accessory::visorColor = OptPlayerVisorColorMenu.getSelected();

	$pref::Accessory::BackCode = OptPlayerBackMenu.getSelected();
	$pref::Accessory::BackColor = OptPlayerBackColorMenu.getSelected();
	
	$pref::Accessory::leftHandCode = OptPlayerExtraMenu.getSelected();
	$pref::Accessory::leftHandColor = OptPlayerExtraColorMenu.getSelected();

	$pref::Color::skin = OptPlayerSkinColorMenu.getSelected();
	
	clientCmdUpdatePrefs();
}

function StatusSelect::onSelect( %this, %id, %text )
{
	commandtoserver('setStatus',%text);
}

function OptPlayerHeadMenu::onSelect( %this, %id, %text )
{
	//update visor and visor color menus based on id
	%headcode = OptPlayerHeadMenu.getSelected();

	OptPlayerVisorMenu.clear();
	OptPlayerVisorMenu.add("None", -1);
	OptPlayerVisorMenu.setSelected(-1);

	if(%headCode == 0)
	{
		//space Helmet
		OptPlayerVisorMenu.add("Visor", 1);
		OptPlayerVisorMenu.add("Divers Mask", 3);
		OptPlayerVisorMenu.add("Ice Planet Visor", 5);
	}
	else if(%headCode == 1)
	{
		//scout hat
		OptPlayerVisorMenu.add("Tri-Plume", 0);
	}	
		else if(%headCode == 11)
	{
		//sam hat
		OptPlayerVisorMenu.add("Horns", 2);
	}	
			else if(%headCode == 30)
	{
		//aviator cap
		OptPlayerVisorMenu.add("Goggles", 4);
	}	
				else if(%headCode == 34)
	{
		//aviator cap
		OptPlayerVisorMenu.add("Horn", 6);
	}	

		//if the pref isnt in the list, pick none
	
	if (Appearance.UpBot) {%text = OptPlayerVisorMenu.getTextById($pref::BAccessory::visorCode);}
	else {%text = OptPlayerVisorMenu.getTextById($pref::Accessory::visorCode);}

	if(%text !$= "")
	{
		if (Appearance.UpBot) {OptPlayerVisorMenu.setSelected($pref::BAccessory::visorCode);}
		else {OptPlayerVisorMenu.setSelected($pref::Accessory::visorCode);}
	}
	
}


//function printlist::onSelect(%this, %row)
//{
	//%image = printlist.getRowTextById(%row);
	//if (Appearance.UpBot) {$Pref::BDecal::ChestRow = %row;}
	//else {$Pref::Decal::ChestRow = %row;}
//	%image2 = getword(%image,1);
//	printpreview.setBitmap(%image2);
//	if (Appearance.UpBot) {$pref::BDecal::chest = %image2 = getword(%image,0); clientCmdUpdateBotPrefs();}
//	else {$pref::Decal::chest = %image2 = getword(%image,0); clientCmdUpdatePrefs();}
//	clientCmdUpdatePrefs();
//}

function Faceprintlist::onSelect(%this, %row)
{
	%image = faceprintlist.getRowTextById(%row);
	if (Appearance.UpBot) {$Pref::BDecal::FaceRow = %row;}
	else {$Pref::Decal::FaceRow = %row;}
	%image2 = getwords(%image,1, 2);
	faceprintpreview.setBitmap(%image2);
	if (Appearance.UpBot) {$pref::BDecal::face = %image2 = getword(%image,0); clientCmdUpdateBotPrefs();}
	else {$pref::Decal::face = %image2 = getword(%image,0); clientCmdUpdatePrefs();}
}
function getDecalName( %missionFile ) 
{
      return fileBase(fileBase(%missionFile)); 
}

