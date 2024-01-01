using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErectileMaleFunction_CS : CardAbilities
{
    public override void OnEntry()
    {
        gameObject.GetComponent<CardAttack>().DestroyTarget();
        Debug.Log("OnE");
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
