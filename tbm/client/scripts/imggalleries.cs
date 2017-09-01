//Dynamic Gui Image loader for faces and decals
// - Rob - 7/28/05
//
//Function prototypes:
// void loadOptionsImagePreviews(void)
//   -For options gui, loads once, drop into optionsdlg's onWake
// void loadImgPreviews(int %scroll, string %file_path)
//   -For loading images that match %file_path into %scroll, 5 images and buttons per line.
//   -50x50px image 50x25px button. Requires a 330px wide scroll control with Vscroll always on and Hscroll always off.
// string disectFileName(string %file_name, string %delimit, int %index)
//   -returns a section of a filename, last section doesn't work ( extensions not needed anyway )
// void imgPreviewHandler(string %type, string %img_name)
//   -button handling code for the image preview galleries, KIER YOUR CODE GOES IN HERE
//
//
// Requirements for loadOptionsImagePreviews:
//   -A scroll control 330px wide, named PlayerFaceScr and one named PlayerDecalScr
//   -also needs to have dynamic vScroll and alwaysOff hScroll
//   -A button named PlayerFaceBtn and one named PlayerDecalBtn
//
//
// -Chris- 2/11/06
//
// Changed alot of things to create a more BL "retail" look.
// So, most of the stuff Rob said up ^there^ is now obsolete
//
function loadOptionsImagePreviews()
{
	if($ImagePreviewsLoaded)
		return;
	loadImgPreviews(nameToID(PlayerFaceScr), "tbm/data/shapes/player/face/*.face.png");
	loadImgPreviews(nameToID(PlayerDecalScr), "tbm/data/shapes/player/chest/*.decal.png");
	$ImagePreviewsLoaded = 1;
}

function loadImgPreviews(%scroll, %file_path)
{
	%items_on_line = 0;  //Current count of items on the line
	%new_line = 0;  //new line bool
	%line_pos = 0;  //position from top, starting offset of 10
	%item_pos = 0;  //position from side, starting offset of 10
	%first_img = 0;  //stupid hack because the first image doesn't stay put for some reason.
	%num_files = 0;  //number of images loaded into %fn array; since we have enough files for reverse alphabetical output

	if(%scroll == nameToID(PlayerFaceScr))
	{
		%first_img = new GuiBitmapCtrl() {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "1 1";
			extent = "50 50";
			minExtent = "8 2";
			visible = "1";
			helpTag = "0";
			bitmap = "tbm/data/shapes/player/face/previews/none.face.png";
			wrap = "0";
		};
		%first_img.position = %item_pos SPC %line_pos;
		%scroll.add(%first_img);
        	%btn = new GuiButtonCtrl() {
        		profile = "DP_ButtonProfile";
			horizSizing = "right";
        	    	vertSizing = "bottom";
            		position = %item_pos SPC (%line_pos);
	            	extent = "50 50";
        	    	minExtent = "8 2";
            		visible = "1";
	            	helpTag = "0";
			text = " ";
	            	groupNum = "-1";
        	        command = "setdecalvar(\"face\", 0,\"none\");";
            		buttonType = "RadioButton";
	         };
		%scroll.add(%btn);
		decalarraysetup("face", "none");
		%items_on_line++;
		%item_pos += 50;
	}
	if(%scroll == nameToID(PlayerDecalScr))
	{
		%first_img = new GuiBitmapCtrl() {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "1 1";
			extent = "50 50";
			minExtent = "8 2";
			visible = "1";
			helpTag = "0";
			bitmap = "tbm/data/shapes/player/chest/previews/base.decal.png";
			wrap = "0";
		};
		%first_img.position = %item_pos SPC %line_pos;
		%scroll.add(%first_img);
        	%btn = new GuiButtonCtrl() {
        		profile = "DP_ButtonProfile";
			horizSizing = "right";
        	    	vertSizing = "bottom";
            		position = %item_pos SPC (%line_pos);
	            	extent = "50 50";
        	    	minExtent = "8 2";
            		visible = "1";
	            	helpTag = "0";
			text = " ";
	            	groupNum = "-1";
        	        command = "setdecalvar(\"decal\", 0,\"base\");";
            		buttonType = "RadioButton";
	         };
		%scroll.add(%btn);
		decalarraysetup("decal", "base");
		%items_on_line++;
		%item_pos += 50;

		%img = new GuiBitmapCtrl() {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "1 1";
			extent = "50 50";
			minExtent = "8 2";
			visible = "1";
			helpTag = "0";
			bitmap = "tbm/data/shapes/player/chest/previews/broken.decal.png";
			wrap = "0";
		};
		%img.position = %item_pos SPC %line_pos;
		%scroll.add(%img);
        	%btn = new GuiButtonCtrl() {
        		profile = "DP_ButtonProfile";
			horizSizing = "right";
        	    	vertSizing = "bottom";
            		position = %item_pos SPC (%line_pos);
	            	extent = "50 50";
        	    	minExtent = "8 2";
            		visible = "1";
	            	helpTag = "0";
			text = " ";
	            	groupNum = "-1";
        	        command = "setdecalvar(\"decal\", 1,\"broken\");";
            		buttonType = "RadioButton";
	         };
		%scroll.add(%btn);
		decalarraysetup("decal", "broken");
		%items_on_line++;
		%item_pos += 50;
	}

	for(%file_name = findFirstFile(%file_path); %file_name !$= ""; %file_name = findNextFile(%file_path))
	{
        //skip the preview folder
		if(getsubstr(filePath(%file_name),strlen(filePath(%file_name))-8,8) $= "previews")
			continue;
		if(%file_name $= "tbm/data/shapes/player/face/none.face.png" || %file_name $= "tbm/data/shapes/player/chest/base.decal.png" || %file_name $= "tbm/data/shapes/player/chest/broken.decal.png")
			continue;
		%fn[%num_files++] = %file_name;
	}
	for(%i = %num_files; %i > 0; %i--)
	{
		%true_file_name = (%file_name = %fn[%i]);
		while(strstr(%true_file_name, "/") != -1)
			%true_file_name = getSubStr(%true_file_name, strpos(%true_file_name, "/")+1, strlen(%true_file_name));
                %img_name = disectFileName(%true_file_name, ".", 0);
		%img_type = disectFileName(%true_file_name, ".", 1);
                if (%img_name $= "")
                  continue;
		if (isfile(filePath(%file_name)@"/previews/"@%img_name@"."@%img_type@".png"))
                  %pic_file_name=filePath(%file_name)@"/previews/"@%img_name@"."@%img_type@".png";
                else
                  %pic_file_name=%file_name;
			//Create the image and add it
		%img = new GuiBitmapCtrl() {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "1 1";
			extent = "50 50";
			minExtent = "8 2";
			visible = "1";
			helpTag = "0";
			bitmap = %pic_file_name;
			wrap = "0";
		};
		%img.position = %item_pos SPC %line_pos;
		if(!%first_img)  //part two of the first_img hack
			%first_img = %img;
		%scroll.add(%img);
        %img_name = strreplace(%img_name,"-","_9");
			//Create the button and add it
            %btn = new GuiButtonCtrl() {
            		profile = "DP_ButtonProfile";
		            horizSizing = "right";
            		vertSizing = "bottom";
            		position = %item_pos SPC (%line_pos);
            		extent = "50 50";
            		minExtent = "8 2";
            		visible = "1";
            		helpTag = "0";
			        text = " ";
            		groupNum = "-1";
                    command = "setdecalvar("@%img_type@","@%items_on_line@","@%img_name@");";
            		buttonType = "RadioButton";
         		};
		%scroll.add(%btn);
		decalarraysetup(%img_type,%img_name);
			//Increment our values for the constraints
		%items_on_line++;
		%item_pos += 50;
	}

		//create the background, using base.blank for the default torque-gray
	%bg = new GuiBitmapCtrl() {
		profile = "GuiDefaultProfile";
		horizSizing = "right";
		vertSizing = "bottom";
		position = "0 0";
		extent = %item_pos SPC 50;  //skip over the last line and add a 10px bottom border
		minExtent = "8 2";
		visible = "1";
		helpTag = "0";
		bitmap = "tbm/data/shapes/bricks/yellow.blank";
		wrap = "0";
	};
	%scroll.add(%bg);
	//because we added it last, it'll be in front of everything, this is BAD, and we can't add it first because we don't know how big to make it, so we just send it all the way back behind the rest ( yea, it's fucked, bringToFront, I know I know).
	%scroll.bringToFront(%bg);

		//now the third part of first_img hack
	%first_img.position = "0 0";
        if (%img_type$="decal") {
	  $Decal_Chest_ctrl=%bg;
   }

}

function disectFileName(%file_name, %delimit, %index)
{
	for(%i = 0; %i < %index; %i++)
		%file_name = getSubStr(%file_name, strpos(%file_name, %delimit)+1, strlen(%file_name));  //grab the index + rest of line
	%file_name = getSubStr(%file_name, 0, strpos(%file_name, %delimit));  //strip the remaning words
	return(%file_name);
}

function setdecalvar(%type,%num,%name) {
%name = strreplace(%name, "_9","-");
if (%type$="decal") {
  $pref::Decal::chest=%name;
  previewdecal.setBitmap("tbm/data/shapes/player/chest/"@$Decals::chest[%num]@".decal.png");
  }
if (%type$="face") {
  $pref::Decal::face=%name;
  previewface.setBitmap("tbm/data/shapes/player/face/"@$Decals::face[%num]@".face.png");
  }
}
function DecalOPBG(%color) {
%color=strlwr(%color);
if (strpos(%color," ") != -1)
  %color=getSubStr(%color,0,strpos(%color," "))@getSubStr(%color,strpos(%color," ")+1,strlen(%color));
$Decal_Chest_ctrl.setBitmap("tbm/data/shapes/bricks/"@%color@".blank.jpg");
previewbodycolor.setbitmap("tbm/data/shapes/bricks/"@%color@".blank.jpg");
bodycolorbitmap.setbitmap("tbm/data/shapes/bricks/"@%color@".blank.jpg");
}
function decalarraysetup(%type,%name) {
%name = strreplace(%name, "_9","-");
if (%type$="decal")
  $Decals::chest[$Decals::chest::total++]=%name;
if (%type$="face")
  $Decals::face[$Decals::face::total++]=%name;
}
$Decals::face::total=-1;
$Decals::chest::total=-1;
loadOptionsImagePreviews();
function playerPV() {
previewdecal.setBitmap("tbm/data/shapes/player/chest/"@$pref::Decal::chest@".decal.png");
previewface.setBitmap("tbm/data/shapes/player/face/"@$pref::Decal::face@".face.png");
}
playerPV();

function makecolorarray() {
  %colorarray = new FileObject();
  %colorarray.openForWrite("tbm/colors.cs");
	for(%file_name = findFirstFile("tbm/data/shapes/*.blank.jpg"); %file_name !$= ""; %file_name = findNextFile("tbm/data/shapes/*.blank.jpg")){
   %filestr=getSubStr(filename(%file_name),0,strlen(filename(%file_name))-10);
   %colorarray.writeLine("$legoColor[$LCTotal++] = \""@%filestr@"\";");     
   echo(%filestr);   
   }
   %colorarray.close();
}
function clearscr() {
playerdecalscr.clear();
playerfacescr.clear();
}

