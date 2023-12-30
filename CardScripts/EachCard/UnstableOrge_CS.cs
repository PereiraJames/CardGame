using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnstableOrge_CS : CardAbilities
{
    public override void OnEntry()
    {
        GameObject PlayerSlot = PlayerManager.PlayerSlot;

        foreach (Transform child in PlayerSlot.GetComponentsInChildren<Transform>())
        {
            if (child.gameObject.tag == "Cards")
            {
                PlayerManager.CmdDealDamage(child.gameObject, 2);
            }
        }
    }

    public override void OnExecute()
    {
        PlayerManager.CmdChangeBP(2,1);
    }

    public override void OnSpecial()
    {
        Debug.Log(" ");
    }
}
