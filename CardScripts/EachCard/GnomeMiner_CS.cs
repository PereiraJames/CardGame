using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GnomeMiner_CS : CardAbilities
{
    public override void OnCompile()
    {
        
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