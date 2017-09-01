To create your very own Gui window skins, you will first need a little experience in Photoshop. 

Steps-
1. Open up the TBM_WindowTemplate.psd
2. Check out the diff. layers, find out what each one does.
3. DO NOT EDIT THE STUFF IN THE DO NOT EDIT FOLDER(otherwise your skin will not work)
4. I made two layers that go over the head of the window, so you can edit those in any way you like.
5. when saving(use .png), instead of using spaces you must use _ 's and you cannot use - 's (don't ask) Ex: Cool_Blue_Skin
6. save your skin to the tbm/client/ui/window_images/ folder and load up TBM. your new skin will be automatically loaded into the list of other skins.

If you have any questions, feel free to ask me either on the TBM forums, or on the official tbm irc channel, #tbm2
-gobbles(Chris)

**EDIT: Added a custom profiles option, so you can set up your own fonts, etc.. for your window header.

Here's a list of all the fonts in BL, which i got from the common/ui/cache folder-
>>Arial
>>Arial Bold
>>Comic Sans
>>Courier
>>Impact
>>Lucidia ConsoleMonotype Corsiva
>>System
>>Times New Roman
>>Trebuchet

So, now that you've got those, you can start to work on your custom profile for your skin.
1. goto the tbm/client/ui/window_images/customprofiles/ directory and copy/paste a pre-existing script in there.
2. rename the script to the EXACT SAME NAME as your skin. (EX: Cool_Blue_Glass.png --> Cool_Blue_Glass.cs)
3. open it up in your favorite script editor.
4. change the bitmap name to whatever name your skin is.
5. now's the fun part. Begin testing out the diff. fonts and/or sizes, and colors. 
6. test it out.

If you are having problems, feel free to contact me.







