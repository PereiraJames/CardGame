using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MG_CS : CardAbilities
{
    int cardHealth = 1;
    int cardAttack = 1;

    public override void OnCompile()
    {
        PlayerManager.CmdGMEnemyHealth(-10);
    }

    public override void OnExecute()
    {
        PlayerManager.CmdChangeBP(2,1);
    }

    public override void OnSpecial()
    {
        // target.Health -= cardAttack;   
    }
}
