using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class DeckScript : NetworkBehaviour
{
    public int DeckSize = 30;

    public void UpdateDeckSize(int amount)
    {
        DeckSize -= amount;
    }

    public void OnHoverEnter()
    {
        
    }
}
