$commandvalue = 0;

function startConsole()
{
	%obj = new TCPObject(DedicatedConsoleConnection);
	%obj.connect("127.0.0.1:1271");

	//clear command from last time
	%command = new FileObject();
	%command.openForWrite("rtb/command.cs");
	%command.writeLine("$commandcheck = -1;");
	%command.writeLine("function command(){");
	%command.writeLine("}");
	%command.close();
	exec("fps/command.cs");
}

function DedicatedConsoleConnection::onLine(%this, %line)
{
	echo("==>" @ %line);				//echo command to console
	%command = new FileObject();			//create script using command
	%command.openForWrite("rtb/command.cs");
	%command.writeLine("$commandcheck = " @ $commandvalue @ ";");
	%command.writeLine("function command(){");
	%command.writeLine(%line);
	%command.writeLine("}");
	%command.close();
	exec("rtb/command.cs");				//compile script
	if($commandcheck == $commandValue) command();	//if compile success, run entered command
	$commandValue++;				//change value used to check compile success
	if($commandValue > 1000) $commandValue = 0;	//prevents possible overflow
}