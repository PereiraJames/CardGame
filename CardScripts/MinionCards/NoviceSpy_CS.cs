using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NoviceSpy : CardAbilities
{
    public override void OnEntry()
    {
        gameObject.GetComponent<CardDetails>().DestroyTarget();
    }

    public override void OnEndTurn()
    {
     
    }

    public override void OnSpecial()
    {

    }
}
