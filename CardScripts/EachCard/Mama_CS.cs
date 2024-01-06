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

    public override void OnExecute()
    {
        Debug.Log("");
    }

    public override void OnSpecial()
    {
        Debug.Log("");
    }
}
