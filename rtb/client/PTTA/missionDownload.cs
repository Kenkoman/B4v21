//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//----------------------------------------------------------------------------
// Mission Loading & Mission Info
// The mission loading server handshaking is handled by the
// common/client/missingLoading.cs.  This portion handles the interface
// with the game GUI.
//----------------------------------------------------------------------------

//----------------------------------------------------------------------------
// Loading Phases:
// Phase 1: Download Datablocks
// Phase 2: Download Ghost Objects
// Phase 3: Scene Lighting

//----------------------------------------------------------------------------
// Phase 1
//----------------------------------------------------------------------------

function onMissionDownloadPhase1(%missionName, %musicTrack)
{
   // Close and clear the message hud (in case it's open)
   MessageHud.close();
   //cls();

   // Reset the loading progress controls:
   LoadingProgress.setValue(0);
   DownloadProgress.setValue(0);
   LoadingProgressTxt.setValue("Recieved Server Loading Information!");
   DownloadProgressTxt.setValue("No Download in Progress.");
}

function onPhase1Progress(%progress)
{
   LoadingProgress.setValue(%progress);
   Canvas.repaint();
   LoadingProgressTxt.setValue("Loading Bricks:" @ mFloor(%progress*100) @"%");
}

function onPhase1Complete()
{
   LoadingProgressTxt.setValue("Done Loading Bricks!");
}

//----------------------------------------------------------------------------
// Phase 2
//----------------------------------------------------------------------------

function onMissionDownloadPhase2()
{
   // Reset the loading progress controls:
   LoadingProgress.setValue(0);
   LoadingProgressTxt.setValue("Done Loading Bricks!");
   Canvas.repaint();
}

function onPhase2Progress(%progress)
{
   LoadingProgress.setValue(%progress);
   LoadingProgressTxt.setValue("Loading Ghosts:" @ mFloor(%progress*100) @"%");
   Canvas.repaint();
}

function onPhase2Complete()
{
   LoadingProgressTxt.setValue("Done Loading Ghosts!");
}   

function onFileChunkReceived(%fileName, %ofs, %size)
{
   DownloadProgress.setValue(%ofs / %size);
   %progress = (%ofs / %size);
   %fileName2 = strreplace(%fileName,"rtb/data/","");
   DownloadProgressTxt.setValue("Downloading " @ %fileName2 SPC mFloor(%progress*100) @"%");
   if ( %fileName $=  "*.ter" ) 
   {
   MessageBoxOK( "NOTICE", "You are downloading a terrain file!  This may take a while...");
   }
   if( %ofs == %size)
   {
	DownloadProgressTxt.setValue("Downloads Completed.");
   }
}

//----------------------------------------------------------------------------
// Phase 3
//----------------------------------------------------------------------------

function onMissionDownloadPhase3()
{
   LoadingProgress.setValue(0);
   LoadingProgressTxt.setValue("Done Loading Ghosts!");
   Canvas.repaint();
}

function onPhase3Progress(%progress)
{
   LoadingProgress.setValue(%progress);
   LoadingProgressTxt.setValue("Lighting Mission:" @ mFloor(%progress*100) @"%");
}

function onPhase3Complete()
{
   LoadingProgress.setValue( 1 );
   LoadingProgressTxt.setValue("Waiting for Server...");
   $lightingMission = false;
}

//----------------------------------------------------------------------------
// Mission loading done!
//----------------------------------------------------------------------------

function onMissionDownloadComplete()
{
   LoadingProgress.setValue( 1 );
   LoadingProgressTxt.setValue("Waiting for Server...");
}


//------------------------------------------------------------------------------
// Before downloading a mission, the server transmits the mission
// information through these messages.
//------------------------------------------------------------------------------

addMessageCallback( 'MsgLoadInfo', handleLoadInfoMessage );
addMessageCallback( 'MsgLoadDescripition', handleLoadDescriptionMessage );
addMessageCallback( 'MsgLoadBitmap', handleLoadBitmap );
addMessageCallback( 'MsgLoadInfoDone', handleLoadInfoDoneMessage );

//------------------------------------------------------------------------------

function handleLoadInfoMessage( %msgType, %msgString, %mapName ) {
	
	// Need to pop up the loading gui to display this stuff.
   if (!LoadingGui.isAwake())
   	Canvas.setContent("LoadingGui");

	// Clear all of the loading info lines:
	for( %line = 0; %line < LoadingGui.qLineCount; %line++ )
		LoadingGui.qLine[%line] = "";
	LoadingGui.qLineCount = 0;

   //
	if(%mapName $="The Green Grass Land(RTB)")
	  %mapname = "The Green Grass Land";

	LOAD_MapName.setText( %mapName );

	if(%mapName $= "Green Hills")
	  %mapname = "greenhills";
	if(%mapName $= "The Slopes")
	  %mapname = "slopes";
	if(%mapName $= "Celestial Dreams")
	  %mapname = "celestialdreams";
	if(%mapName $= "Death Valley")
	  %mapname = "deathvalley";
	if(%mapName $= "Lavapit Update")
	  %mapname = "lavapit2";
	if(%mapName $= "Pigs Backyard")
	  %mapname = "pigbackyard";
	if(%mapName $= "RTB Isles")
	  %mapname = "rtbisle";
	if(%mapName $= "The Islands")
	  %mapname = "theislands";
	loadingbitmap.setBitmap("rtb/data/missions/previews/"@%mapName@".png");
}

function handleLoadBitmap( %msgType, %msgString, %mapName ) {

	if(%mapName $= "Green Hills")
	  %mapname = "greenhills";
	if(%mapName $= "The Slopes")
	  %mapname = "slopes";
	if(%mapName $= "Celestial Dreams")
	  %mapname = "celestialdreams";
	if(%mapName $= "Death Valley")
	  %mapname = "deathvalley";
	if(%mapName $= "Lavapit Update")
	  %mapname = "lavapit2";
	if(%mapName $= "Pigs Backyard")
	  %mapname = "pigbackyard";
	if(%mapName $= "RTB Isles")
	  %mapname = "rtbisle";
	if(%mapName $= "The Islands")
	  %mapname = "theislands";
	if(%mapName $="The Green Grass Land(RTB)")
	  %mapname = "The Green Grass Land";
	loadingbitmap.setBitmap("rtb/data/missions/previews/"@%mapName@".png");
}

//------------------------------------------------------------------------------

function handleLoadDescriptionMessage( %msgType, %msgString, %line )
{
	LoadingGui.qLine[LoadingGui.qLineCount] = %line;
	LoadingGui.qLineCount++;

   // Gather up all the previous lines, append the current one
   // and stuff it into the control
	%text = "<spush><font:Arial:16>";
	
	for( %line = 0; %line < LoadingGui.qLineCount - 1; %line++ )
		%text = %text @ LoadingGui.qLine[%line] @ " ";
   %text = %text @ LoadingGui.qLine[%line] @ "<spop>";

	LOAD_MapDescription.setText( %text );
}

//------------------------------------------------------------------------------

function handleLoadInfoDoneMessage( %msgType, %msgString )
{
   // This will get called after the last description line is sent.
}
