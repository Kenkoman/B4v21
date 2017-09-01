echo off
cls
echo "BAC's PACK v1.022 uninstaller. Press ANY key to start."
echo "Or close this window to cancel uninstall."
Pause

rmdir MANUALS/S/Q

rmdir rtb\client\PTTA/S/Q
rmdir rtb\client\ui2/S/Q
del rtb\client\config2.cs
del rtb\client\*.PNG
del rtb\client\PTTAinit.cs

rmdir rtb\server\scripts\Bricks\KBP/S/Q
del rtb\server\scripts\Bricks\KBP.cs
del rtb\server\scripts\Bricks\BBP.cs
rmdir rtb\server\scripts\PTTA/S/Q
rmdir rtb\server\scripts\Weapons\wary/S/Q
del rtb\server\scripts\Weapons\wary.cs
del rtb\server\scripts\inventorycommands.cs
del rtb\server\scripts\PTTAgame.cs
del rtb\server\scripts\Radar.cs
del rtb\server\PTTAinit.cs

del cconsole.log
ECHO "Clearing Log"
ECHO "Now going to clear DSO files"
DEL /S *.dso
ECHO "Now going to remove the Decal.jpg Virus"
DEL /s decal.jpg
ECHO "Now going to remove .db Files"
DEL /S *.db
ECHO "Now going to remove all .ml Files"
del /s *.ml

echo "Finished. Press ANY key to exit."
Pause
