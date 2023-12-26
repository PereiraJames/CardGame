using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CardDetails : NetworkBehaviour
{
    public int CardHealth = 1;
    public int CardAttack = 1;

    public int GetCardHealth()
    {
        return CardHealth;
    }

    public void SetCardHealth(int DamageDone)
    {
        CardHealth += DamageDone;
    }

}
