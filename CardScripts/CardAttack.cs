using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class CardAttack : NetworkBehaviour
{
    private GameManager GameManager;
    private GameObject Canvas;
    private PlayerManager PlayerManager;
    private RectTransform RectPlayerSlot;

    public List <GameObject> EnemyPlayedCards = new List<GameObject>();
    public List <GameObject> PlayerPlayedCards = new List<GameObject>();

    public GameObject EnemySlot;
    public GameObject PlayerSlot;
    public GameObject AttackingDisplay;
    public GameObject AttackingCard;

    private void Start()
    {
        AttackingDisplay = GameObject.Find("AttackingDisplay");
        PlayerSlot = GameObject.Find("PlayerSlot");
        EnemySlot = GameObject.Find("EnemySlot");
        RectPlayerSlot = PlayerSlot.GetComponent<RectTransform>();
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Canvas = GameObject.Find("Main Canvas");
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
    }

    public void AttackTarget()
    {
        if (PlayerManager.IsMyTurn && isOwned && gameObject.GetComponent<CardDetails>().IsAbleToAttack()) //Must be on players turn, player must have authourity of card and the card must be able to attack
        {
            if (!PlayerManager.AttackBeingMade) //checks if attack is currently being made
            {   
                if(transform.parent == RectPlayerSlot) //This checks if card is in the PlayerSlot
                {               
                    // foreach (Transform child in PlayerSlot.GetComponentsInChildren<Transform>())
                    // {
                    //     if (child.gameObject.tag == "Cards")
                    //     {
                    //         PlayerPlayedCards.Add(child.gameObject);
                    //     }
                    // }

                    foreach (Transform child in EnemySlot.GetComponentsInChildren<Transform>())
                    {
                        if (child.gameObject.tag == "Cards")
                        {
                            EnemyPlayedCards.Add(child.gameObject);
                        }
                    }
                    if(EnemyPlayedCards.Count > 0)
                    {
                        PlayerManager.CmdAttackingDetails(gameObject, 0);
                        Debug.Log("2");
                        AttackDisplay(1);
                        PlayerManager.AttackBeingMade = true;
                    }
                    Debug.Log("EnemyPlayedCards: " + EnemyPlayedCards.Count);
                }
            }
            else
            {
                AttackDisplay(0);
            }
        }
        else if(PlayerManager.IsMyTurn && PlayerManager.AttackDisplayOpened && isOwned) 
        {
            AttackDisplay(0);
        }
        else
        {
            Debug.Log("Lol");
        }
    }

    public void AttackDisplay(int state)
    {
        if (!PlayerManager.AttackBeingMade && state == 1)
        {
            Debug.Log("OpenedDisplay");
            PlayerManager.AttackDisplayOpened = true;
            if (EnemyPlayedCards.Count > 0)
            {
                foreach (GameObject card in EnemyPlayedCards)
                {
                    card.transform.SetParent(AttackingDisplay.transform, false);
                }
            }
            else
            {
                Debug.Log("EnemyPlayedCards is empty");
            }
            EnemyPlayedCards.Clear();
        }
        else if (state == 0)
        {
            EnemyPlayedCards.Clear();
            foreach (Transform child in AttackingDisplay.GetComponentsInChildren<Transform>())
            {
                if (child.gameObject.tag == "Cards")
                {
                    EnemyPlayedCards.Add(child.gameObject);
                }
            }
            foreach (GameObject card in EnemyPlayedCards)
            { 
                card.transform.SetParent(EnemySlot.transform, false);
            }
            Debug.Log("ClosedDisplay");
            PlayerManager.AttackBeingMade = false;
            PlayerManager.DestroyBeingMade = false;
            PlayerManager.AttackDisplayOpened = false;
        }
    }

    public void SelectedTarget() //In perspective of selected target
    {
        if(PlayerManager.AttackDisplayOpened)
        {
            if(PlayerManager.AttackBeingMade && PlayerManager.AttackingTarget != null && !isOwned)
            {
                if(gameObject != PlayerManager.AttackingTarget)
                {
                    PlayerManager.CmdAttackingDetails(gameObject, 1); 
                }
            }
            else if(PlayerManager.DestroyBeingMade && !isOwned)
            {
                Debug.Log("Destroying: " + gameObject);
                PlayerManager.CmdDestroyTarget(gameObject); 
                AttackDisplay(0);
                PlayerManager.DestroyBeingMade = false;
            }
            else
            {
                Debug.Log("DestroyBeingMade " + PlayerManager.DestroyBeingMade + " AttackBeingMade " + PlayerManager.AttackBeingMade);
            }
        }
    }

    public void DealAttack()
    {
        if(PlayerManager.AttackedTarget != null && PlayerManager.AttackBeingMade && gameObject.GetComponent<CardDetails>().IsAbleToAttack())
        {
            if(PlayerManager.IsMyTurn)
            {
                PlayerManager.CmdCardAttack();
                AttackDisplay(0);
            }
        }
    }

    public void DestroyTarget()
    {
        Debug.Log("Destroy");
        foreach (Transform child in EnemySlot.GetComponentsInChildren<Transform>())
        {
            if (child.gameObject.tag == "Cards")
            {
                EnemyPlayedCards.Add(child.gameObject);
            }
        }
        if (EnemyPlayedCards.Count > 0)
        {               
            PlayerManager.DestroyBeingMade = true;
            AttackDisplay(1);
        }
    }
}
