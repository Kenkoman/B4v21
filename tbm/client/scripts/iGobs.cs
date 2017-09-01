function openfile(%filename,%erase,%mod) {
	%filename = "tbm/client/iGob/" @ %filename @ ".save";
	echo(%filename);
	$iGob_file = new FileObject();
	$iGob_file.openForWrite(%filename);
    if(%mod == 1)
    commandtoserver('copybrick');
    else
	commandtoserver('begintransfer',%erase);
}

function clientCmdSaveToClient (%objstr, %current, %total) {
    if (%objstr !$= " ")
	$iGob_file.writeLine(%objstr);
	//echo ("Writing line " @ %current @ " of " @ %total);
    iGobprogress("Saving:",%current, %total);
	if (%current == %total || %objstr $= "")
		closefile();
}

function closefile() {
	$iGob_file.close();
	$iGob_file.delete();
	loadigoblist();
	//echo("iGobs save file closed");
}

function iGob_Load(%filename) {
	%delay=10;
    	%delaycopy = %delay;
	%filename = "tbm/client/iGob/" @ %filename @ ".save";
        echo(%filename);
	if(!isFile(%filename))
		return;
    	%linetotal = getlinecount(%filename);
	%file = new FileObject();
	%file.openForRead(%filename);
	while(!%file.isEOF())
	{
		%pl_string = %file.readLine();
		if(%pl_string $= "")
			continue;
		else {
			schedule(%delay, 0, commandtoserver, 'MakeObject', %pl_string);
            schedule(%delay, 0, iGobprogress,"Loading:",%linenum++,%linetotal);
			%delay+=%delaycopy;
		}
	}
	%file.close();
	%file.delete();
    	schedule(%delay, 0, commandtoserver, 'brickhandleoptions',2);    
}
function iGobprogress(%mode, %count, %total) {
if(%count == 1)
iGobload.setVisible(1);
%percent = %count/%total;
iGob_loadtxt.setValue(%mode SPC mFloor(%percent*100) @"%");
iGob_loadbar.setValue(%percent);
if(%count == %total)
iGobload.setVisible(0);
}

function crazyigob(%times,%delay) {
cancel($igobcolorsched);
for(%i=0;%i<%times;%i++)
$igobcolorsched = schedule(%i*%delay,0,commandtoserver,'igobcolor',$legocolor[getrandom($LCtotal)]);
}
