using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WellPaidButcher : CardAbilities
{
    public override void OnEntry()
    {
        PlayerManager.CmdUpdateDoubloons(3, true);
    }

    public override void OnEndTurn()
    {
        
    }

    public override void OnSpecial()
    {
        
    }
}
