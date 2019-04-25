# B4v21
Various old blockland releases with extra B4v21 addons included.
This is only for Windows users. Mac users can find a different download on the B4v21 Website.
This is the v20 branch, check the other branches for the other versions.
v20 (Windows) is compatible with both Steam and Regular authentication.

# Instructions
1) Click the 'Clone or download' button on this page, and click on 'Download ZIP'
2) Extract the zip into a folder on your computer. The Documents folder will work fine.
3) Run 'Blocklandv20.exe' to launch the game.
__**v20 Non-steam**__
4) Click "Regular Authentication", then enter your key.
__**v20 Steam**__
5) Click "Steam Authentication", and then enter your in-game name and BLID*.
*If you don't know your BLID, launch v21, wait for the game to authenticate and then enter `echo(getNumKeyId());` into the console.
If it doesn't authenticate, launch v21, wait for it to authenticate and then try again on v20.

# v20

All B4v21 versions have differences to the vanilla game, here they are:

The .exe has been replaced with a .dll loader, which loads

* SteamAuth.dll - Allows the game to auth via Steam.
* Discordv20.dll - Allows the game to show Rich Presence on Discord.

Theres also many additional add-ons included:

* Script_CustomMS - Replaces the master server with the B4v21 custom master server.
* System_ReturnToBlockland - v19 version of RTB, with working IRC, addon manager, prefs, etc.
* Client_SteamAuth - Allows the game to auth through steam. Don't delete this if you use steam!

There's also some additional client add-ons included, which adds essential features to the game. If you want a 'stock' experience, just delete all 'Client_' add-ons.

* Client_AdminGuiEdit - Adds extra features to the admin menu.
* Client_DownloadInfo - Adds extra information when downloading files from a server.
* Client_FOVFix - Adds the v21 FOV slider back into the game.
* Client_MapLightingFix - Fixes lighting on terrain maps (No more red slopes!)
* Client_MissionEditor - Allows you to create/edit maps.
* Client_OrbShift - Fixes brick moving being messed up in the admin orb.
* Client_Quit_Button_Fix and Client_Tutorial_Button_Fix - Adds prompts before launching the tutorial or quitting the game, for accidental presses.
* Client_TabShiftingBrickSelector - Adds arrows to the brick menu to access hidden brick sections.

Addons from v21 have been added too:

* Brick_ModTer_BasicPack
* Brick_ModTer_InvertedPack
* Item_Sports
* Print_ModTer_Default