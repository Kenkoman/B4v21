//This fixes a very specific, and also very general, crash, caused mostly by the Cemetech AI bots.
//The crash in question occurs when calling commandToClient and supplying either an AIConnection, or a GameConnection object that does not correspond to a connected client.
//This generally happens when bots that have a .client variable collide with scripts that don't distinguish between Players and AIPlayers.
//Now, the main difference between legit and artificial connection is the value returned by the getAddress method.
//It returns a string containing the IP and port of the connection
//Legit connections return it in the form "IP:(ip):(port)" while connections created by script return "IPX:(ip):(port)"
//This difference causes getRawIP(%client) to return the IP of legit connections, and a blank string when given a script-created connection.
//In lieu of adding this check to every single time client checks need to be made, I'm directly modifying the commandToClient function.
//This should not only fix the current source of the crashes (headshots), but also correct similar crashes caused by possible similar problems, as well as preventing any crashes from arising under similar conditions in the future.

package Wiggy_bot_crash_fix {
function commandToClient(%client, %tag, %a, %b, %c, %d, %e, %f, %g, %h, %i, %j, %k, %l, %m, %n, %o, %p, %q, %r, %s, %t) {
if(getRawIP(%client) $= "")
  return;
Parent::commandToClient(%client, %tag, %a, %b, %c, %d, %e, %f, %g, %h, %i, %j, %k, %l, %m, %n, %o, %p, %q, %r, %s, %t);
}
};
activatepackage(Wiggy_bot_crash_fix);