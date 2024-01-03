using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class CardDetails : NetworkBehaviour
{
    public int CardHealth = 1;
    public int CardAttack = 1;
    public bool CanAttack = true;
    public int DoubloonCost = 1;
    public int amountOfEachCard = 1;

    public string DeckTag;

    public GameObject CardStats;

    void Start()
    {
        UpdateCardText();
    }

    public int GetCardHealth()
    {
        return CardHealth;
    }

    public void SetCardHealth(int DamageDone)
    {
        CardHealth -= DamageDone;
    }

    public int GetCardAttack()
    {
        return CardAttack;
    }

    public bool IsAbleToAttack()
    {
        return CanAttack;
    }

    public void AttackTurn(bool HasAttackedThisTurn)
    {
        CanAttack = HasAttackedThisTurn;
    }

    public void UpdateCardText()
    {
        gameObject.GetComponentInChildren<Text>().text = CardAttack + " / " + CardHealth;
    }

}
