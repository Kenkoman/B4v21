//ski.cs
 
//skis let you slide downhill and skim across water
 
//////////
// item //
//////////
 
datablock ItemData(ski)
{
        category = "Item";  // Mission editor category
 
        equipment = true;
 
        //its already a member of item namespace so dont break it
        //className = "Item"; // For inventory system
 
         // Basic Item Properties
        shapeFile = "~/data/shapes/FeedBack/hoverboard.dts";
        cloaktexture = "~/data/specialfx/cloakTexture";
        mass = 1;
        density = 0.2;
        elasticity = 0.2;
        friction = 0.6;
        emap = true;
 
         // Dynamic properties defined by the scripts
        pickUpName = 'a pair of skis.';
        invName = "Hoverboard";
        image = leftSkiImage;
};

addWeapon(Ski);
 
function ski::onUse(%this, %player, %InvPosition)
{
 
        %playerData = %player.getDataBlock();
        %client = %player.client;
 
        %mountPoint = %this.image.mountPoint - 3;
        %mountSlot = %mountPoint;
        %mountedImage = %player.getMountedImage(%mountSlot); 
        
        if(%player.isMounted())
        {
                if(%player.getObjectMount().getDataBlock().getName() !$= "skiVehicle")
                {
                        messageClient(%player.client, 'Clientmsg', 'Can\'t use hoverboard right now.');
                        return;
                }
        }
 
 
        if(%mountedImage)
        {
                if(%mountedImage == %this.image.getId())
                {
                        //our image is already mounted so unmount it
                        for(%i = 0; %i < %playerData.maxItems; %i++)                                                    //search through other inv slots
                        {
                                if(%player.isEquiped[%i] == true)                                                                       //if it is equipped then
                                {
                                        %checkMountPoint = %player.inventory[%i].image.mountpoint;
                                        if(%mountPoint == %checkMountPoint)                                                             //if it is mounted on the same point
                                        {               
                                                messageClient(%client, 'MsgDeEquipInv', '', %i);                        //then de-equip it 
                                                %player.isEquiped[%i] = false;
                                                break;                                                                                                          //we're done because only one item can interfere
                                        }                                                                               
                                }
                        }
 
                        %player.unMountImage(%mountSlot);
                        messageClient(%player.client, 'MsgDeEquipInv', '', %InvPosition);
                        %player.isEquiped[%invPosition] = false;
 
                }
                else
                {
                        //something else is there so mount our image
                        if(vectorLen(%player.getVelocity()) <= 40)
                        {
                                %player.mountimage(%this.image, %mountSlot);
                                %player.isEquiped[%invPosition] = true;
                               // %player.rotsav=rotaddup(%player.rotsav,vectorscale(%client.rotfactor,1));
                        }
                        else
                        {
                                messageClient(%player.client, 'Clientmsg', 'Can\'t use hoverboard while moving.');
                        }
                }
        }
        else
        {
                //nothing there so mount 
                if(vectorLen(%player.getVelocity()) <= 40)
                {
                        %player.mountimage(%this.image, %mountSlot);
                        messageClient(%player.client, 'MsgEquipInv', '', %InvPosition);
                        %player.isEquiped[%invPosition] = true;
                       // %player.rotsav=rotaddup(%player.rotsav,vectorscale(%client.rotfactor,90));
// %this.setskinname(%player.brickcolor);
                }
                else
                {
                        messageClient(%player.client, 'Clientmsg', 'Can\'t use hoverboard while moving.');
                }
                
        }
 
        
}
///////////
// image //
///////////
datablock ShapeBaseImageData(leftSkiImage)
{
        // Basic Item properties
        shapeFile = "~/data/shapes/FeedBack/hoverboard.dts";
        cloaktexture = "~/data/specialfx/cloakTexture";
        emap = true;
 
        // Specify mount point & offset for 3rd person, and eye offset
        // for first person rendering.
        mountPoint = $LeftFootSlot;
        offset = "0 0 0";
 
        // Add the WeaponImage namespace as a parent, WeaponImage namespace
        // provides some hooks into the inventory system.
        className = "skiImage";
 
        item = ski;
};
 
//datablock ShapeBaseImageData(rightSkiImage)
//{
//        // Basic Item properties
//        shapeFile = "~/data/shapes/FeedBack/hoverboard.dts";
//        cloaktexture = "~/data/specialfx/cloakTexture";
//        emap = true;
// 
//        // Specify mount point & offset for 3rd person, and eye offset
//        // for first person rendering.
//        mountPoint = $RightFootSlot;
//        offset = "0 0 0";
// 
//        // Add the WeaponImage namespace as a parent, WeaponImage namespace
//        // provides some hooks into the inventory system.
//        className = "skiImage";
// 
//        item = ski;
//};
 
function leftSkiImage::onMount(%this, %obj)
{

 
        //make a new ski vehicle and mount the player on it
        %client = %obj.client;
        %position = %obj.getTransform();
 // %this.setskinname(%client.brickcolor);
        %posX = getword(%position, 0);
        %posY = getword(%position, 1);
        %posZ = getword(%position, 2);
        %rot = getWords(%position, 3, 8);
 
        %posZ += 0.1;
 
        %newcar = new WheeledVehicle() 
        {
                dataBlock = skivehicle;
                client = %client;
                initialPosition = %posX @ " " @ %posY @ " " @ %posZ;
        };
        //%newcar.setVelocity(%obj.getVelocity() * 90);
        %newcar.setTransform(%posX @ " " @ %posY @ " " @ %posZ @ " " @ %rot);
        
 
        
        //%obj.client.setcontrolobject(%newcar);
        %obj.setArmThread(root);
        %newcar.schedule(250, mountObject, %obj, 0);
        //%newcar.mountObject(%obj,0);
        //%obj.setTransform("0 0 0 0 0 1 0");
}
 
function leftSkiImage::onUnmount(%this, %obj)

{
        //%obj.unMountImage($RightHandSlot);
        //delete the ski vehicle
        %skiVehicle = %obj.getObjectMount();
        if(%skiVehicle.getDataBlock().getName() $= "skiVehicle")
        {
                //%obj.client.setControlObject(%obj);
                   %obj.unmount();
                //%skiVehicle.unMountObject(%obj);
                //%obj.setTransform(%skivehicle.getTransform());
                   %skiVehicle.schedule(100, delete);
		   %obj.schedule(100, setVelocity, %skivehicle.getVelocity());
                //%obj.setArmThread(look);
        }
}