using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class UD_CS : CardAbilities
{
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
        Debug.Log(" ");
    }
}
