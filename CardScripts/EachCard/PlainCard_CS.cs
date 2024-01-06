using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlainCard_CS : CardAbilities
{
    public override void OnEntry()
    {
        Debug.Log("PlainCard");
    }

    public override void OnExecute()
    {
        Debug.Log("");
    }

    public override void OnSpecial()
    {
        Debug.Log("");
    }
}
