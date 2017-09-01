function ApplyEditorSettingsAuto()
{
	%scaleA = txtScaleAuto.getValue();
	%rotateA = txtRotationAuto.getValue();
	commandtoserver('EditorStoreAuto',%rotateA,%scaleA);
}
function ApplyBBCSettings()
{
	%public = Public.getValue();
	%reserved = Reserved.getValue();
	%reservedname = ReservedName.getValue();
	canvas.popDialog(bbcew);
	commandtoserver('ApplyBBCSettings',%public,%reserved,%reservedname);
}
function inGameEditorGui::onWake(%this)
{
	commandtoserver('getObjectData');
	$IngameEditorOpen = 1;
}
function inGameEditorGui::onSleep(%this)
{
	$IngameEditorOpen = 0;
}
function CopsAndRobbers::onWake(%this)
{
	txtTriggerSize.setValue("1 1 1");
	txtJailCapacity.setValue("5");
}

function ApplyEditorSettings()
{
	%pos = txtPosition.getValue();
	%rot = txtRotation.getValue();
	%scale = txtScale.getValue();

	commandtoserver('ApplyEditorSettings',%pos,%rot,%scale);
}

function SetPos()
{
	%pos = txtPosition.getValue();
	commandtoserver('EditorPos',%pos);
}
function SetRot()
{
	%rot = txtRotation.getValue();
	commandtoserver('EditorRot',%rot);
}
function SetScale()
{
	%scale = txtScale.getValue();
	commandtoserver('EditorScale',%scale);
}
function deleteObj()
{
	commandtoserver('EditorDelete');
	canvas.popDialog(inGameEditorGui);
}

//cops+robbers stuff



function ApplyCR()
{
	%BankTrigger = BankTrigger.getValue();
	%BaseTrigger = BaseTrigger.getValue();
	%CopSpawn = CopSpawn.getValue();
	%RobberSpawn = RobberSpawn.getValue();
	%JailSpawn = JailSpawn.getValue();

	if(%BankTrigger)
	{
		%size = txtTriggerSize.getValue();
		commandtoserver('setBankTrigger',%size);
	}
	if(%BaseTrigger)
	{
		%size = txtTriggerSize.getValue();
		commandtoserver('setBaseTrigger',%size);
	}
	if(%CopSpawn)
	{
		commandtoserver('setCopSpawnPoint');
	}
	if(%RobberSpawn)
	{
		commandtoserver('setRobberSpawnPoint');
	}
	if(%JailSpawn)
	{
		%cap = txtJailCapacity.getValue();
		commandtoserver('setJailSpawnPoint',%cap);
	}

}

