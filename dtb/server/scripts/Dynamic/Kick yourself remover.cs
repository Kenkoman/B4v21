package Wiggy_Kick_Yourself_remover {
  function serverCmdunAdminPlayer(%client, %victim) {
    if (%victim.issuperadmin) return;
    Parent::serverCmdunAdminPlayer(%client, %victim);
  }
};
activatepackage(Wiggy_Kick_Yourself_remover);