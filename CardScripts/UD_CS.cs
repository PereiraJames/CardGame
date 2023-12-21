using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class UD_CS : CardAbilities
{
    public override void OnCompile()
    {
        PlayerManager.CmdGMPlayerHealth(10); // For adjusting player health
        PlayerManager.CmdGMEnemyHealth(10); // For adjusting enemy health
    }

    public override void OnExecute()
    {
        PlayerManager.CmdChangeBP(2,1);
    }
}
