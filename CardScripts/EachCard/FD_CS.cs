using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class FD_CS : CardAbilities
{
    public override void OnEntry()
    {
        PlayerManager.CmdGMPlayerHealth(2);
        PlayerManager.CmdGMEnemyHealth(-1);
    }

    public override void OnExecute()
    {
        PlayerManager.CmdChangeBP(2,1);
    }

    public override void OnSpecial()
    {
        Debug.Log("ATTACKED!");
    }
}
