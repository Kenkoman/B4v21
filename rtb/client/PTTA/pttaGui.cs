//----------------------------------------------------------------------------------------------------------------------------------------
//----------------------------------------------------------------------------------------------------------------------------------------
//----------------------------------------------------------------------------------------------------------------------------------------
//---------------------------------------------------*** <<WARNING>> ***------------------------------------------------------------------
//						      ***PLEASE READ***	
//ALL GUI SCRIPTS FROM HERE DOWN WERE DEVISED, WRITTEN, AND IMPLIMENTED BY YTUD FO LLAC (Ytud Fo Llac) - ©YTUD FO LLAC 2005-2006.
//NO GUI SCRIPTS FROM HERE DOWN ARE TO BE REPRODUCED FOR PERSONAL OR PUBLIC USE WITHOUT THE CONSENT OF YTUD FO LLAC.
//USE OF GUI SCRIPTS FROM HERE DOWN WITHOUT CONSENT OF YTUD FO LLAC WILL RESULT IN PERMANENT BAN FROM HIS SERVER AND 
//ACTIONs AGAINST THE PERPETRATIOR(S) WILL BE TAKEN IN THE BLOCKLAND COMMUNITY (INCLUDING GLOBAL BANS).
//
//
//
function LoadMapType()
{
%map = lstMaplist.getSelectedId();
commandToServer('TypeMap', %map);
}

function score()
{
%victimId = lstPttaPlayerList.getSelectedId();
commandToServer('Score', %victimId);
}

function setmaxbricks()
{
%maxbricks = txtmaxbricks.getValue();
commandtoserver('MaxBricks',%maxbricks);
}

function clrmaxbricks()
{
%victimId = lstPttaPlayerList.getSelectedId();
commandToServer('ClrClientMax', %victimId);
}

function setsvrname()
{
%name = txtsvrname.getValue();
commandtoserver('SvrName',%name);
}

function setpass()
{
%pass = txtsetpassword.getValue();
commandtoserver('SetPass',%pass);
}

function stun()
{
%victimId = lstPttaPlayerList.getSelectedId();
commandToServer('stun', %victimId);
}

function BanByIp()
{
%ip = txtBanIP.getValue();
commandtoserver('BanByIp',%ip);
}

function cleanupclient()
{
%victimId = lstPttaPlayerList.getSelectedId();
commandToServer('cleanupclient', %victimId);
}

function SvrMsg()
{
%color = txtColor.getValue();
%message = txtShoutmsg.getValue();
commandtoserver('SvrMsg',%color, %message);
}

function SvrName()
{
%name = txt.getValue();
commandtoserver('SvrName',%name);
}

function TypeMap()
{
%mapname = txtMapname.getValue();
commandtoserver('TypeMap',%mapname);
}

function showmoney()
{
%victimId = lstPttaPlayerList.getSelectedId();
commandToServer('showmoney', %victimId);
}

function banningrights()
{
%victimId = lstPttaPlayerList.getSelectedId();
commandToServer('banningrights', %victimId);
}

function deinvent()
{
%victimId = lstPttaPlayerList.getSelectedId();
commandToServer('deinvent', %victimId);
}

function muteclient()
{
%victimId = lstPttaPlayerList.getSelectedId();
commandToServer('muteclient', %victimId);
}

//gui script by ©YTUD FO LLAC - kill
function kill()
{
%victimId = lstPttaPlayerList.getSelectedId();
commandToServer('kill', %victimId);
}

//gui script by ©YTUD FO LLAC - balete
function balete()
{
%victimId = lstPttaPlayerList.getSelectedId();
commandToServer('balete', %victimId);
}

//gui script by ©YTUD FO LLAC - copy
function copy()
{
%victimId = lstPttaPlayerList.getSelectedId();
commandToServer('copy', %victimId);
}

//gui script by ©YTUD FO LLAC - unadmin
function unadmin()
{
%victimId = lstPttaPlayerList.getSelectedId();
commandToServer('unadmin', %victimId);
}

//gui script by ©YTUD FO LLAC - inventprivs
function inventprivs()
{
%victimId = lstPttaPlayerList.getSelectedId();
commandToServer('inventprivs', %victimId);
}

//gui script by ©YTUD FO LLAC - freezeclient
function freezeclient()
{
%victimId = lstPttaPlayerList.getSelectedId();
commandToServer('freezeclient', %victimId);
}

//gui script by ©YTUD FO LLAC - chgname
function chgname()
{
%victim = lstPttaPlayerList.getSelectedId();
%setname = txtsetname.getValue();
commandtoserver('chgname',%victim,%setname);
}

//gui script by **PLOAD** for PTTA - undress
function undress()
{
%victimId = lstPttaPlayerList.getSelectedId();
commandToServer('undress', %victimId);
}

//gui script by ©YTUD FO LLAC - respawn
function respawn()
{
%victimId = lstPttaPlayerList.getSelectedId();
commandToServer('respawn', %victimId);
}

//ADVERTSERVER
function SetSvrMsg()
{
%advertmsg = txtAdvertMessage.getValue();
%time = txtAdvertTime.getValue();
commandtoserver('SetSvrMsg',%advertmsg,%time);
}

//*** END OF SCRIPTS BY YTUD FO LLAC ***