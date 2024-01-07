using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Mama_CS : CardAbilities
{
    public override void OnEntry()
    {
        GameObject PlayerSlot = PlayerManager.PlayerSlot;

        foreach (Transform child in PlayerSlot.GetComponentsInChildren<Transform>())
        {
            if (child.gameObject.tag == "Cards")
            {
                if(child.gameObject.name == "Joe(Clone)")
                {
                    PlayerManager.CmdUpdateDoubloons(5, true);
                }
            }
        }    
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
