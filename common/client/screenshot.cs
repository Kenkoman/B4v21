//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------


function formatImageNumber(%number)
{
   if(%number < 10)
      %number = "0" @ %number;
   if(%number < 100)
      %number = "0" @ %number;
   if(%number < 1000)
      %number = "0" @ %number;
   if(%number < 10000)
      %number = "0" @ %number;
   return %number;
}


//----------------------------------------
function recordMovie(%movieName, %fps)
{
   $timeAdvance = 1000 / %fps;
   $screenGrabThread = schedule($timeAdvance,0,movieGrabScreen,%movieName,0);   
}

function movieGrabScreen(%movieName, %frameNumber)
{
   screenshot(%movieName @ formatImageNumber(%frameNumber) @ ".png");
   $screenGrabThread = schedule($timeAdvance, 0, movieGrabScreen, %movieName, %frameNumber + 1);   
}

function stopMovie()
{
   $timeAdvance = 0;
   cancel($screenGrabThread);
}


//----------------------------------------
$screenshotNumber = 0;

function doScreenShot( %val )
{
   if (%val)
   {
      $pref::interior::showdetailmaps = false;
      if($pref::Video::screenShotFormat $= "JPEG")
         screenShot("screenshot_" @ formatImageNumber($screenshotNumber++) @ ".jpg", "JPEG");
      else if($pref::Video::screenShotFormat $= "PNG")
         screenShot("screenshot_" @ formatImageNumber($screenshotNumber++) @ ".png", "PNG");
      else
         screenShot("screenshot_" @ formatImageNumber($screenshotNumber++) @ ".png", "PNG");
   }
}


// bind key to take screenshots
GlobalActionMap.bind(keyboard, "ctrl p", doScreenShot);

