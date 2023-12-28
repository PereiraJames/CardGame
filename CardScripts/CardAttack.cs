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

    List <GameObject> EnemyPlayedCards = new List<GameObject>();
    List <GameObject> PlayerPlayedCards = new List<GameObject>();

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
                    foreach (Transform child in PlayerSlot.GetComponentsInChildren<Transform>())
                    {
                        if (child.gameObject.tag == "Cards")
                        {
                            PlayerPlayedCards.Add(child.gameObject);
                        }
                    }

                    foreach (Transform child in EnemySlot.GetComponentsInChildren<Transform>())
                    {
                        if (child.gameObject.tag == "Cards")
                        {
                            EnemyPlayedCards.Add(child.gameObject);
                        }
                    }
                    PlayerManager.CmdAttackingDetails(gameObject, 0);
                    AttackDisplay(1);
                    PlayerManager.AttackBeingMade = true;
                }
            }
            else
            {
                AttackDisplay(0);
                PlayerManager.AttackBeingMade = false;
            }
        }
        else if(PlayerManager.IsMyTurn)
        {
            AttackDisplay(0);
        }
    }

    public void AttackDisplay(int state)
    {
        if (!PlayerManager.AttackBeingMade && state == 1)
        {
            Debug.Log("OpenedDisplay");
            foreach (GameObject card in EnemyPlayedCards)
            {
                // card.GetComponent<RectTransform>().sizeDelta = new Vector2(200,280);
                card.transform.SetParent(AttackingDisplay.transform, false);
            }
        }
        else if (state == 0)
        {
            foreach (GameObject card in EnemyPlayedCards)
            { 
                card.transform.SetParent(EnemySlot.transform, false);
            }
            EnemyPlayedCards.Clear();
            Debug.Log("ClosedDisplay");
        }
    }

    public void SelectedTarget()
    {
        if(PlayerManager.AttackBeingMade && PlayerManager.AttackingTarget != null && !isOwned)
        {
            if(gameObject != PlayerManager.AttackingTarget)
            {
                PlayerManager.CmdAttackingDetails(gameObject, 1); 
                Debug.Log("SelectedTarget: " + PlayerManager.AttackedTarget);
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
}
