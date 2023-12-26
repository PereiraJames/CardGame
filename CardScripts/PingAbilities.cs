using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PingAbilities : CardAbilities
{
    public override void OnCompile()
    {
        PlayerManager.CmdGMPlayerHealth(10);
    }

    public override void OnExecute()
    {
        PlayerManager.CmdChangeBP(2,1);
    }
    
    public override void OnAttack()
    {
        Debug.Log(" ");
    }
}
