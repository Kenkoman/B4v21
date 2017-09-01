//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//------------------------------------------------------------------------------
function LoadingGui::onAdd(%this)
{
   %this.qLineCount = 0;
}

//------------------------------------------------------------------------------
function LoadingGui::onWake(%this)
{
   // Play sound...
   CloseMessagePopup();

   Canvas.pushDialog( MainChatHud );
   chatHud.attach(HudMessageVector);
   moveMap.push();
   commandtoserver('messagesent',$pref::player::automessage);
}

//------------------------------------------------------------------------------
function LoadingGui::onSleep(%this)
{
   // Clear the load info:
   if ( %this.qLineCount !$= "" )
   {
      for ( %line = 0; %line < %this.qLineCount; %line++ )
         %this.qLine[%line] = "";
   }      
   %this.qLineCount = 0;

//   LOAD_MapName.setText( "Map" );
//   LOAD_MapDescription.setText( "Description" );
//   LoadingProgress.setValue( 0 );
//   LoadingProgressTxt.setValue( "LoadingGUI has been closed!" );

   // Stop sound...
}
