function addServerRenderJob(%shape, %frame, %fill, %arg1, %arg2, %arg3, %arg4, %arg5, %arg6, %id, %type, %quiet) {
if(!isObject(ServerRenderJobs)) {
  new scriptObject(ServerRenderJobs);
  RootGroup.add(ServerRenderJobs);
}
if(!%id)
  %id = ServerRenderJobs.renderJobs++;
ServerRenderJobs.renderJobs = ServerRenderJobs.renderJobs > %id ? ServerRenderJobs.renderJobs : %id;
//Add the job with any objects to the list
ServerRenderJobs.shape[%id] = %shape;
ServerRenderJobs.frameColor[%id] = %frame;
ServerRenderJobs.fillColor[%id] = %fill;
for(%i = 1; %i <= 6; %i++)
  ServerRenderJobs.arg[%i, %id] = %arg[%i];
ServerRenderJobs.type[%id] = %type;
//Convert any objects to ghost data for transmission, because this isn't T3D
for(%i = 1; %i <= 6; %i++) {
  if(getFieldCount(%arg[%i]) > 1) {
    %obj = getField(%arg[%i], 0);
    for(%j = 0; %j < ClientGroup.getCount(); %j++)
      %obj.scopeToClient(ClientGroup.getObject(%j));
    %obj.setScopeAlways();
    %arg[%i] = encodeGhostData(%obj) TAB getField(%arg[%i], 1);
  }
}
//Send job to clients
if(!%quiet) {
  for(%i = 0; %i < ClientGroup.getCount(); %i++)
    commandToClient(ClientGroup.getObject(%i), 'addRenderJob', %shape, %frame, %fill, %arg1, %arg2, %arg3, %arg4, %arg5, %arg6, %id, %type);
}
return %id;
}

function removeServerRenderJob(%id) {
if(%id $= "")
  %id = ServerRenderJobs.renderJobs;
for(%i = 1; %i <= 6; %i++) { //For each argument, 
  if(getFieldCount(ServerRenderJobs.arg[%i, %id]) > 1) { //if the argument is an object,
    %obj = getField(ServerRenderJobs.arg[%i, %id], 0);
    for(%j = 1; %j <= ServerRenderJobs.RenderJobs; %j++) { //check each other job
      if(getField(ServerRenderJobs.arg[1, %j], 0) == %obj || getField(ServerRenderJobs.arg[2, %j], 0) == %obj || getField(ServerRenderJobs.arg[3, %j], 0) == %obj) {//and if the object is any of the specified job's three arguments,
        %obj = ""; //remove the object
        break;
      }
    }
    if(%obj !$= "") { //If the object isn't being used by another job,
      for(%k = 0; %k < ClientGroup.getCount(); %k++)
        %obj.clearScopeToClient(ClientGroup.getObject(%k)); //descope it
    }
  }
}
ServerRenderJobs.shape[%id] = "";
ServerRenderJobs.frameColor[%id] = "";
ServerRenderJobs.fillColor[%id] = "";
for(%i = 1; %i <= 6; %i++)
  ServerRenderJobs.arg[%i, %id] = "";
ServerRenderJobs.type[%id] = "";
//Tell clients to remove job
for(%i = 0; %i < ClientGroup.getCount(); %i++)
  commandToClient(ClientGroup.getObject(%i), 'removeRenderJob', %id);
}

function GameConnection::sendRenderJob(%client, %id) {
for(%i = 1; %i <= 6; %i++) {
  %arg[%i] = ServerRenderJobs.arg[%i, %id];
  if(getFieldCount(%arg[%i]) > 1) {
    %obj = getField(%arg[%i], 0);
    for(%j = 0; %j < ClientGroup.getCount(); %j++)
      %obj.scopeToClient(ClientGroup.getObject(%j));
    %obj.setScopeAlways();
    %arg[%i] = encodeGhostData(%obj) TAB getField(%arg[%i], 1);
  }
}
commandToClient(%client, 'addRenderJob', ServerRenderJobs.shape[%id], ServerRenderJobs.frameColor[%id], ServerRenderJobs.fillColor[%id], %arg[1], %arg[2], %arg[3], %arg[4], %arg[5], %arg[6], %id, ServerRenderJobs.type[%id]);
}

package Server_RenderJobs {
function GameConnection::onClientEnterGame(%this) {
Parent::onClientEnterGame(%this);
if(!%this.receivedRenderJobs) {
  for(%id = 1; %id <= $ServerRenderJobs.renderJobs; %id++)
    %client.sendRenderJob(%i);
  %this.receivedRenderJobs = 1;
}
}

function serverCmdQueryObj(%client) {
Parent::serverCmdQueryObj(%client);
if(isObject(%client.lastswitch) && %client.edit) {
  if(%client.lastswitchAxesRJID !$= "")
    commandToClient(%client, 'removeRenderJob', %client.lastswitchAxesRJID);
  %client.sendRenderJob(%client.lastswitchAxesRJID = addServerRenderJob(6, "", "", %client.lastswitch TAB "obj", 3, 2, "", "", "", 0, "", 1));
}
}
};
activatePackage(Server_RenderJobs);

//Default things
function serverCmdCloseSpecOps(%client) {
commandToClient(%client, 'removeRenderJob', %client.lastswitchAxesRJID);
%client.lastswitchAxesRJID = "";
}