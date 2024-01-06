using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MisguidedAssault : CardAbilities
{
    public override void OnEntry()
    {
        PlayerManager.CmdGMPlayerHealth(-3);
        GameObject EnemySlot = PlayerManager.EnemySlot;

        foreach (Transform child in EnemySlot.GetComponentsInChildren<Transform>())
        {
            if (child.gameObject.tag == "Cards")
            {
                PlayerManager.CmdDealDamage(child.gameObject, 2);
            }
        }
    }

    public override void OnEndTurn()
    {
        
    }

    public override void OnSpecial()
    {
        
    }
}
