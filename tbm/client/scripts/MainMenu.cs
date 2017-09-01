//mainmenu.cs

//code for dynamic "film strip"
$FilmImage1 = nametoID("Frame1");
$FilmImage2 = nametoID("Frame2");
$FilmImage3 = nametoID("Frame3");
$FilmImage4 = nametoID("Frame4");
$FilmImage5 = nametoID("Frame5");
$FilmFrame1 = nametoID("MainMenu_Frame1");
$FilmFrame2 = nametoID("MainMenu_Frame2");
$FilmFrame3 = nametoID("MainMenu_Frame3");
$FilmFrame4 = nametoID("MainMenu_Frame4");
$FilmFrame5 = nametoID("MainMenu_Frame5");

function startfilmstrip() {
    
    loadImages();
    
    $FilmImage1.setRandomBitmap();
    $FilmImage2.setRandomBitmap();
    $FilmImage3.setRandomBitmap();
    $FilmImage4.setRandomBitmap();
    $FilmImage5.setRandomBitmap();
    
    //if($Pref::Gui::FilmStrip)
    //moveFrames();
}
function moveFrames() {
    cancel($FrameSched);
    if($Pref::Gui::FilmStrip && MainMenuGui.isAwake())
    $FrameSched = schedule(25,0,moveFrames);
    else
    return;
    %move = 1;
    
    $FilmFrame1.position = (getWord($FilmFrame1.position,0)+%move) SPC getWord($FilmFrame1.position,1);
    $FilmFrame2.position = (getWord($FilmFrame2.position,0)+%move) SPC getWord($FilmFrame2.position,1);
    $FilmFrame3.position = (getWord($FilmFrame3.position,0)+%move) SPC getWord($FilmFrame3.position,1);
    $FilmFrame4.position = (getWord($FilmFrame4.position,0)+%move) SPC getWord($FilmFrame4.position,1);
    $FilmFrame5.position = (getWord($FilmFrame5.position,0)+%move) SPC getWord($FilmFrame5.position,1);

    if(getWord($FilmFrame1.position,0) > getWord(Slideshow_holder.getextent(),0)) {
        $FilmFrame1.position = (getWord($FilmFrame2.position,0)-340) SPC getWord($FilmFrame1.position,1);
        $FilmImage1.setRandomBitmap();
        }
    if(getWord($FilmFrame2.position,0) > getWord(Slideshow_holder.getextent(),0)) {
        $FilmFrame2.position = (getWord($FilmFrame3.position,0)-340) SPC getWord($FilmFrame2.position,1);
        $FilmImage2.setRandomBitmap();
        }
    if(getWord($FilmFrame3.position,0) > getWord(Slideshow_holder.getextent(),0)) {
        $FilmFrame3.position = (getWord($FilmFrame4.position,0)-340) SPC getWord($FilmFrame3.position,1);
        $FilmImage3.setRandomBitmap();
        }
    if(getWord($FilmFrame4.position,0) > getWord(Slideshow_holder.getextent(),0)) {
        $FilmFrame4.position = (getWord($FilmFrame5.position,0)-340) SPC getWord($FilmFrame4.position,1);
        $FilmImage4.setRandomBitmap();
        }
    if(getWord($FilmFrame5.position,0) > getWord(Slideshow_holder.getextent(),0)) {
        $FilmFrame5.position = (getWord($FilmFrame1.position,0)-340) SPC getWord($FilmFrame5.position,1);
        $FilmImage5.setRandomBitmap();
        }

}
function setFilm() {
    if(!$Pref::Gui::FilmStrip)
        cancel($FrameSched);
    else {
        $Pref::Gui::BuildPreviews = 0;
        chkbxPreviewBuilds.setValue(0);
        $FrameSched = schedule(25,0,moveFrames);
    }
}
function loadImages() {
    $Film::ImageTotal = 0;
    
    buildImageList("tbm/screenshots/*.jpg");
    buildImageList("tbm/screenshots/*.png");
    buildImageList("tbm/data/missions/*.jpg");
    buildImageList("tbm/data/missions/*.png");
}
function buildImageList(%type) {
    for(%file = findFirstFile(%type); %file !$= ""; %file = findNextFile(%type))
        $Film::Image[$Film::ImageTotal++] = %file;
}
function GuiBitmapCtrl::setRandomBitmap(%this) {
    echo("Setting Random Bitmap");
    for(%i=0;%i<$Film::ImageTotal;%i++) {
    %random = getRandom(1,$Film::ImageTotal);
    %bitmap = $Film::Image[%random];
    if(!isFile(%bitmap))
    echo("Bitmap Error! If you see this, tell Gobbles.");
    if(checkFrames(%bitmap)) {
        echo("Setting Bitmap: "@%bitmap);
        %this.setBitmap(%bitmap);
        return;
        }
    }
}

function checkFrames(%bitmap) {
if($FilmImage1.bitmap$=%bitmap||$FilmImage2.bitmap$=%bitmap||$FilmImage3.bitmap$=%bitmap||$FilmImage4.bitmap$=%bitmap||$FilmImage5.bitmap$=%bitmap)
        return false;
    else
        return true;
}

function resetFrames() {
$FrameSched = "";
$Pref::Gui::FilmStrip = 0;
$FilmImage1.setbitmap("tbm/client/ui/mainmenuImages/default_frameimage.png");
$FilmImage2.setbitmap("tbm/client/ui/mainmenuImages/default_frameimage.png");
$FilmImage3.setbitmap("tbm/client/ui/mainmenuImages/default_frameimage.png");
$FilmImage4.setbitmap("tbm/client/ui/mainmenuImages/default_frameimage.png");
$FilmImage5.setbitmap("tbm/client/ui/mainmenuImages/default_frameimage.png");
$FilmFrame1.position = -340 SPC getWord($FilmFrame1.position,1);
$FilmFrame2.position = 0 SPC getWord($FilmFrame2.position,1);
$FilmFrame3.position = 340 SPC getWord($FilmFrame3.position,1);
$FilmFrame4.position = 680 SPC getWord($FilmFrame4.position,1);
$FilmFrame5.position = 1020 SPC getWord($FilmFrame5.position,1);
}

startFilmStrip();

function MainMenuGui::OnWake(%this) {
SlideShow_Holder.setVisible(1); //this should probably be moved
$ButtonCheckSchedule = schedule(50,0,"checkbuttons");

if($Pref::Gui::BuildPreviews)
  schedule(0, 0, createShowcaseServer); //In case the mainMenuGUI is opened from inside disconnect()
else if($Pref::Gui::FilmStrip)
  moveFrames();
}

if(strStr(getModPaths(), "dtb") != -1) {
  if(isObject(Mod_Image))
    Mod_Image.setBitmap("tbm/client/ui/mainmenuImages/mod_DtB.png");
  $Buttonpath = "tbm/client/ui/mainmenuimages/dtbbutton_o";
}
else {
  if(isObject(Mod_Image))
    Mod_Image.setBitmap("tbm/client/ui/mainmenuImages/mod_TBM.png");
  $Buttonpath = "tbm/client/ui/mainmenuimages/button_o";
}

function MainMenuGui::OnSleep(%this) {
    cancel($FrameSched);
    cancel($ButtonCheckSchedule);
    MainMenuLoadbar.setValue(0);
    //The current build showcase deactivator should be sufficient to kill it when necessary, but this is a good place to put it otherwise
}

function MainMenu_StartServer::onMouseDown(%this) {
Canvas.pushDialog(startmissiongui);
}

function MainMenu_JoinServer::onMouseDown(%this) {
Canvas.pushDialog(joinservergui);
}

function MainMenu_News::onMouseDown(%this) {
Canvas.pushDialog(NewsDlg);
}

function MainMenu_Options::onMouseDown(%this) {
Canvas.pushDialog(optionsdlg);
}

function MainMenu_Help::onMouseDown(%this) {
Canvas.pushDialog(tbmhelpdlg);
}

function MainMenu_Quit::onMouseDown(%this) {
canvas.pushDialog(quitConfirmDialog);
}

function Options_Graphics::onMouseDown(%this) {
	optionsDlg.setPane(Graphics);
}

function Options_Audio::onMouseDown(%this) {
	optionsDlg.setPane(Audio);
}

function Options_Network::onMouseDown(%this) {
	optionsDlg.setPane(Network);
}

function Options_Controls::onMouseDown(%this) {
	optionsDlg.setPane(Controls);
}

function Options_Player::onMouseDown(%this) {
	optionsDlg.setPane(Player);
}


//------------------------------------------------------------------------------
//pwny cursor pos function
function isMouseoverbutton(%button) {
%parent = %button;
while(%parent.getGroup() != nameToID(Canvas) && %parent.getGroup() != nameToID(GUIGroup)) {
  %xMin += getWord(%parent.position, 0);
  %yMin += getWord(%parent.position, 1);
  %parent = %parent.getGroup();
}
if(Canvas.getObject(Canvas.getCount() - 1) != nameToID(%parent))
  return false;
%xMax = %xMin + getword(%button.extent, 0);
%yMax = %yMin + getword(%button.extent, 1);
%mousex = getword(Canvas.getCursorpos(), 0);
%mousey = getword(Canvas.getCursorpos(), 1);
if(%mousex < %xmin || %mousex > %xmax || %mousey < %ymin || %mousey > %ymax)
  return false;
else
  return true;
}

function fadebutton(%bitmap,%button) {
%per = %button.isVisible() ? strreplace(strreplace(%bitmap.bitmap,$Buttonpath,""),".png","")*1 : 0;
if(isMouseoverbutton(%button) == true && %per <=90 && %button.isVisible())
  %bitmap.setbitmap($Buttonpath@%per+10);
else if(isMouseoverbutton(%button) == false && %per >= 10)
  %bitmap.setbitmap($Buttonpath@%per-10);
if(%per == 0 && isMouseoverbutton(%button) == false) {
  %button.on = "";
  return;
}
%button.on = 1;
schedule(20,0,"fadebutton",%bitmap,%button);
return;
}

function checkbuttons() {
    if(Canvas.getCount() == 1 || (Canvas.getCount() == 2 && Canvas.getObject(1) == nameToID(optionsDlg))) {
		cancel($ButtonCheckSchedule);
		//check all the buttons to see if they're being "touched"
		if(isMouseoverbutton("MainMenu_StartServer") == 1 && !MainMenu_StartServer.on)
			fadebutton("Button_StartServer_Bitmap","MainMenu_StartServer");

		else if(isMouseoverbutton("MainMenu_JoinServer") == 1 && !MainMenu_JoinServer.on)
			fadebutton("Button_JoinServer_Bitmap","MainMenu_JoinServer");

		else if(isMouseoverbutton("MainMenu_News") == 1 && !MainMenu_News.on)
			fadebutton("Button_News_Bitmap","MainMenu_News");

		else if(isMouseoverbutton("MainMenu_Options") == 1 && !MainMenu_Options.on)
			fadebutton("Button_Options_Bitmap","MainMenu_Options");

		else if(isMouseoverbutton("MainMenu_Help") == 1 && !MainMenu_Help.on)
			fadebutton("Button_Help_Bitmap","MainMenu_Help");

		else if(isMouseoverbutton("MainMenu_Quit") == 1 && !MainMenu_Quit.on)
			fadebutton("Button_Quit_Bitmap","MainMenu_Quit");
    }
    $ButtonCheckSchedule = schedule(50,0,"checkbuttons");
}
checkbuttons();

function buddylist() {
exec("tbm/client/ui/buddylistgui.gui");canvas.pushdialog(buddylistgui);
}