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

function votekick()
{
%victimId = lstVotingPlayerList.getSelectedId();
commandtoserver('StartKickVote',%victimId);
}

function votemap()
{
%mapname = txtMapname2.getValue();
commandtoserver('StartMapChgVote', %mapname);
}