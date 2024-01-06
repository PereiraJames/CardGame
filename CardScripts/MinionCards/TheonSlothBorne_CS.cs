using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class TheonSlothBorne_CS : CardAbilities
{
    public override void OnEntry()
    {
        Debug.Log("");
    }

    public override void OnEndTurn()
    {
        GameObject EnemySlot = PlayerManager.EnemySlot;

        foreach (Transform child in EnemySlot.GetComponentsInChildren<Transform>())
        {
            if (child.gameObject.tag == "Cards")
            {
                PlayerManager.CmdDealDamage(child.gameObject, 1);
            }
        } 
    }

    public override void OnSpecial()
    {

    }
}
