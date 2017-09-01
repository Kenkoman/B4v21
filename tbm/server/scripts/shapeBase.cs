//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

// This file contains ShapeBase methods used by all the derived classes

//-----------------------------------------------------------------------------
// ShapeBase object
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------

function ShapeBase::damage(%this, %sourceobject, %position, %damage, %damageType)
{
   // All damage applied by one object to another should go through this
   // method. This function is provided to allow objects some chance of
   // overriding or processing damage values and types.  As opposed to
   // having weapons call ShapeBase::applyDamage directly.
   // Damage is redirected to the datablock, this is standard proceedure
   // for many built in callbacks.
   %this.getDataBlock().damage(%this, %sourceobject, %position, %damage, %damageType);
}


//-----------------------------------------------------------------------------

function ShapeBase::setDamageDt(%this, %damageAmount, %damageType, %sourceObject)
{
   // This function is used to apply damage over time.  The damage
   // is applied at a fixed rate (50 ms).  Damage could be applied
   // over time using the built in ShapBase C++ repair functions
   // (using a neg. repair), but this has the advantage of going
   // through the normal script channels.
   if (%this.getState() !$= "Dead") {
      if (%damageAmount >= 0) {
         %this.damage(%sourceObject, "0 0 0", %damageAmount, %damageType);
      } else {
         %this.applyRepair(-%damageAmount);
      }
      %this.damageSchedule = %this.schedule(50, "setDamageDt", %damageAmount, %damageType, %sourceObject);
   }
   else
      %this.damageSchedule = "";
}

function ShapeBase::clearDamageDt(%this)
{
   if (%this.damageSchedule !$= "") {
      cancel(%this.damageSchedule);
      %this.damageSchedule = "";
   }
}

//-----------------------------------------------------------------------------
// ShapeBase datablock
//-----------------------------------------------------------------------------

function ShapeBaseData::damage(%this, %obj, %position, %source, %amount, %damageType)
{
   // Ignore damage by default. This empty method is here to
   // avoid console warnings.
}
