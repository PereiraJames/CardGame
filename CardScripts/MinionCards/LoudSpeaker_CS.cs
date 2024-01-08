using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class LoudSpeaker_CS : CardAbilities
{
    public override void OnEntry()
    {
        PlayerManager.CmdGMPlayerHealth(-2);
        PlayerManager.CmdDealCards(1, GameManager.PlayerDeck, true);
    }

    public override void OnEndTurn()
    {

    }

    public override void OnHit()
    {

    }
    
    public override void OnLastResort()
    {

    }

    public override void OnSilenced()
    {
        
    }
}
