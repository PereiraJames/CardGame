using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RS_CS : CardAbilities
{
    public override void OnEntry()
    {
        PlayerManager.CmdGMEnemyHealth(-2);
    }

    public override void OnEndTurn()
    {

    }

    public override void OnSpecial()
    {
        
    }
}
