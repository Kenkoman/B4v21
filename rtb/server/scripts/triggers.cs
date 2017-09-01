datablock TriggerData(SafeTrigger)
{
   tickPeriodMS = 100;
};

function SafeTrigger::onEnterTrigger(%this,%trigger,%obj)
{

	if($Pref::Server::Weapons == 1)
	{
		%obj.client.safe = 1;
		messageClient(%obj.client,"","\c5You are now in the safe zone");
	}
}

function SafeTrigger::onLeaveTrigger(%this,%trigger,%obj)
{
	if($Pref::Server::Weapons == 1)
	{
		%obj.client.safe = 0;
		messageClient(%obj.client,"","\c5You are now in the warzone");
	}
}

datablock TriggerData(HealthTrigger)
{
   tickPeriodMS = 100;
};

function HealthTrigger::onEnterTrigger(%this,%trigger,%obj)
{
	%client = %obj.client;
	messageClient(%client,"","\c5Entering Health Regeneration Area");
	%client.player.setRepairRate(0.1);
	if(%trigger.safe == 1)
	{
		if($Pref::Server::Weapons == 1)
		{
			%obj.client.safe = 1;
			messageClient(%obj.client,"","\c5You are now in the safe zone");
		}
	}
}

function HealthTrigger::onLeaveTrigger(%this,%trigger,%obj)
{
	%client = %obj.client;
	messageClient(%client,"","\c5Leaving Health Regeneration Area");
	%client.player.setRepairRate(0);
	if(%trigger.safe == 1)
	{
		if($Pref::Server::Weapons == 1)
		{
			%obj.client.safe = 0;
			messageClient(%obj.client,"","\c5You are now in the warzone");
		}
	}
}


datablock TriggerData(StealTrigger)
{
   tickPeriodMS = 100;
};
datablock TriggerData(DepositTrigger)
{
   tickPeriodMS = 100;
};

//-----------------------------------------------------------------------------

function StealTrigger::onEnterTrigger(%this,%trigger,%obj)
{
	if($Pref::Server::CopsAndRobbers && %obj.client.team $= "Robbers" && %obj.client.money == 0 && %obj.client.steal == 0 )
	{
		messageAll("",'\c0%1\c5 is stealing from the bank!',%obj.client.name);
		%obj.client.steal = 1;
		schedule(5000,0,"moneyIt",%obj.client);
	}
}

function moneyIt(%client)
{
	%client.money += 20;
	messageClient(%client,'MsgUpdateMoney',"",%client.money);
	messageClient(%client,'',"\c5Run back to base!");
}

function StealTrigger::onLeaveTrigger(%this,%trigger,%obj)
{
	if($Pref::Server::CopsAndRobbers && %obj.client.team $= "Robbers" && %obj.client.steal == 1)
	{
		messageAll("",'\c0%1\c5 is leaving the bank!',%obj.client.name);
   		%obj.client.steal = 0;
	}
}

function DepositTrigger::onEnterTrigger(%this,%trigger,%obj)
{
	if($Pref::Server::CopsAndRobbers && %obj.client.team $= "Robbers")
	{
		if(%obj.client.money > 0)
		{
			$Game::RobbersEarnings += %obj.client.money;
					
			messageAll("",'\c0%1\c5 gets back to base with \c0$%2',%obj.client.name,%obj.client.money);
			%obj.client.money = 0;
			messageClient(%obj.client,'MsgUpdateMoney',"",%obj.client.money);	
			
			messageAll("",'\c5The robbers have a total of \c0$%1',$Game::RobbersEarnings);
			if($Game::RobbersEarnings > $Pref::Server::RobbersWinAmount)
			{
				messageAll("","\c5ROBBERS WIN!!!");
				messageAll("","\c5Everyone swaps sides!");
				schedule(5000,0,"RestartCR");
			}
		}
	}
}

function RestartCR()
{
				$TotalRobbers = 0;
				$TotalCops = 0;
				$Game::RobbersEarnings = 0;
				if($teamSwitch == 0)
				{
					$teamSwitch = 1;
				}
				else
				{
					$teamSwitch = 0;
				}
				%randPass = getRandom(100000,999999);
				for(%t = 0; %t < MissionCleanup.getCount(); %t++)
				{
					%brick = MissionCleanup.getObject(%t);
					if(%brick.isAlarmSystem $= 1)
					{
						%brick.Password = %randPass;
					}
					if(%brick.isAlarmSystemCode $= 1)
					{
						%brick.setShapeName("Bank Password: "@%randPass);
					}
				}

				for(%t = 0; %t<MissionCleanup.getCount(); %t++)
				{
					%Window = MissionCleanup.getObject(%t);
					if(%Window.oldPosition !$= "")
					{
						%Window.setTransform(%Window.oldPosition);
					}
				}
				%teamswitch = $teamswitch;
				for(%t = 0; %t<ClientGroup.getCount(); %t++)
				{
					%cl = ClientGroup.getObject(%t);
					%cl.isImprisoned = 0;
					if(%teamswitch == 0)
					{
						$TotalRobbers++;
						%cl.Team = "Robbers";
						%teamswitch = 1;
						bottomPrint(%cl,"\"Cops And Robbers\" Another Round!\nYou are a ROBBER.\nYour aim is to get to the bank and get money, then run back to your base.\nKilling a cop will free another robber from jail.",20,3);
					}
					else	
					{
						$TotalCops++;
						%cl.Team = "Cops";
						%teamswitch = 0;
						bottomPrint(%cl,"\"Cops And Robbers\" Another Round!!!!\nYou are a COP.\nYour aim is to stop the robbers stealing money\nGet them down to less than half health then hit them with a lightsabre to imprison them. Win by imprisoning them all!",20,3);
					}
					%cl.player.kill();
					%cl.JailBrick.JailCount = 0;
				}


}



datablock TriggerData(KillZone)
{
   // The period is value is used to control how often the console
   // onTriggerTick callback is called while there are any objects
   // in the trigger.  The default value is 100 MS.
   tickPeriodMS = 100;
};

function KillZone::onLeaveTrigger(%this,%trigger,%obj)
{
   %obj.client.safe = 0;
}

function KillZone::onEnterTrigger(%this,%trigger,%obj)
{

//This Trigger is good for Bombing Servers. As it Keeps Non-Admin Away, and Stops Bombs harming You.

  if(!%obj.client.isSuperAdmin || !%obj.client.isAdmin)
  {
  	%obj.kill();
  }
  %obj.client.safe = 1;	
}


datablock TriggerData(Energizer)
{
   // The period is value is used to control how often the console
   // onTriggerTick callback is called while there are any objects
   // in the trigger.  The default value is 100 MS.
   tickPeriodMS = 100;
};

function Energizer::onLeaveTrigger(%this,%trigger,%obj)
{
}

function Energizer::onEnterTrigger(%this,%trigger,%obj)
{
   //Give Player 100% Energy Again.
   %obj.setEnergyLevel(1000);
   messageClient(%obj.client,"","\c3Energized!");
}


datablock TriggerData(AntiGravity)
{
   // The period is value is used to control how often the console
   // onTriggerTick callback is called while there are any objects
   // in the trigger.  The default value is 100 MS.
   tickPeriodMS = 100;
};

function AntiGravity::onLeaveTrigger(%this,%trigger,%obj)
{
jet();
}

function AntiGravity::onEnterTrigger(%this,%trigger,%obj)
{

//This Trigger is just Annoying and Stupid : To be Worked ON!

jet();
}







datablock TriggerData(SizeTrigger:Small)
{
   // The period is value is used to control how often the console
   // onTriggerTick callback is called while there are any objects
   // in the trigger.  The default value is 100 MS.
   tickPeriodMS = 100;
};

function Small::onLeaveTrigger(%this,%trigger,%obj)
{
}

function Small::onEnterTrigger(%this,%trigger,%obj)
{

//This Trigger just makes your scale 0.2 0.2 0.2.

%obj.setScale("0.2 0.2 0.2");
}




datablock TriggerData(SizeTrigger:Normal)
{
   // The period is value is used to control how often the console
   // onTriggerTick callback is called while there are any objects
   // in the trigger.  The default value is 100 MS.
   tickPeriodMS = 100;
};

function Normal::onLeaveTrigger(%this,%trigger,%obj)
{
}

function Normal::onEnterTrigger(%this,%trigger,%obj)
{

//This Trigger just makes your scale 1 1 1.

%obj.setScale("1 1 1");
}




datablock TriggerData(Huge)
{
   // The period is value is used to control how often the console
   // onTriggerTick callback is called while there are any objects
   // in the trigger.  The default value is 100 MS.
   tickPeriodMS = 100;
};

function Huge::onLeaveTrigger(%this,%trigger,%obj)
{
}

function Huge::onEnterTrigger(%this,%trigger,%obj)
{

//This Trigger just makes your scale 5 5 5.

%obj.setScale("5 5 5");
}