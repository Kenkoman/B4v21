$Hfile = new FileObject();
$Hfile.openForWrite("tbm/server/scripts/tools/hammerabuse.txt");
$Hlog = true;

$Sfile = new FileObject();
$Sfile.openForWrite("tbm/server/scripts/tools/sprayabuse.txt");
$Slog = true;

function recordhammerabuse(%client,%col) {
  if ($Hlog)
    $Hfile.writeLine(%client.namebase @ " with an IP of " @ getrawip(%client) @ " has destroyed a brick owned by " @ %col.owner);
  }
function recordsprayabuse(%client,%col) {
  if ($Slog)
    $Sfile.writeLine(%client.namebase @ " with an IP of " @ getrawip(%client) @" has colored a brick owned by " @ %col.owner @ " into the color " @ %col.getskinname());
  }
function abuselogdump() {
  $Hfile.close();
  $Hfile.delete();
  $Hlog = false;
  $Sfile.close();
  $Sfile.delete();
  $Slog = false;
  }

