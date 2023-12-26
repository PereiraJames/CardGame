using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class CardAttack : NetworkBehaviour
{
    public GameManager GameManager;
    public GameObject Canvas;
    public PlayerManager PlayerManager;
    public RectTransform RectPlayerSlot;

    List <GameObject> EnemyPlayedCards = new List<GameObject>();
    List <GameObject> PlayerPlayedCards = new List<GameObject>();

    public GameObject EnemySlot;
    public GameObject PlayerSlot;
    public GameObject AttackingDisplay;
    public GameObject Display;

    private bool isDragging = false;
    private bool isOverDropZone = false;
    private bool isDraggable = true;
    private bool isAttacking = false;

    private GameObject dropZone;
    private GameObject startParent;
    private Vector2 startPosition;

    private void Start()
    {
        PlayerSlot = GameObject.Find("PlayerSlot");
        EnemySlot = GameObject.Find("EnemySlot");
        RectPlayerSlot = PlayerSlot.GetComponent<RectTransform>();
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Canvas = GameObject.Find("Main Canvas");
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        if (!isOwned)
        {
            isDraggable = false;
        }
    }

    void Update()
    {
        if(isDragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            transform.SetParent(Canvas.transform, true);
        }
    }

    public void OnClicked()
    {
        if (!isAttacking)
        {   
            gameObject.GetComponent<Outline>().effectColor = Color.white;
            gameObject.GetComponent<Outline>().effectDistance = new Vector2(10,10);
            if(transform.parent == RectPlayerSlot)
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

                DisplayAttackedCard(1, isAttacking);
                isAttacking = true;
            }
        }
        else
        {
            gameObject.GetComponent<Outline>().effectColor = Color.red;
            gameObject.GetComponent<Outline>().effectDistance = new Vector2(1,1);
            DisplayAttackedCard(1, isAttacking);
            isAttacking = false;
        }
    }

    public void DisplayAttackedCard(int amtCards, bool isAttacking)
    {
        if (isAttacking != true)
        {
            GameObject CopyCard;
            GameObject zoomCard;

            Display = Instantiate(AttackingDisplay, new Vector2(-100,0), Quaternion.identity);
            Display.transform.SetParent(Canvas.transform, false);

            Sprite zoomCardSprite;
            foreach (GameObject card in PlayerPlayedCards)
            {
                CopyCard = card.GetComponent<CardZoom>().Card;
                zoomCardSprite = card.GetComponent<Image>().sprite;
                CopyCard.GetComponent<Image>().sprite = zoomCardSprite;
                zoomCard = Instantiate(CopyCard, new Vector2(0, 0), Quaternion.identity);
                zoomCard.transform.SetParent(Display.transform, false);
                zoomCard.layer = LayerMask.NameToLayer("Zoom");
            }
            Debug.Log(PlayerPlayedCards);       
        }
        else
        {
            // foreach (Transform child in AttackingDisplay.GetComponentsInChildren<Transform>())
            // {
            //     if (child.gameObject.layer == 5)
            //     {
            //         Destroy(child.gameObject);
            //     }
            // }
            Destroy(Display);
            PlayerPlayedCards.Clear();
            Debug.Log(PlayerPlayedCards);    
        }

    }


    // public void StartDrag()
    // {
    //     if(!isDraggable)
    //     {
    //         return;
    //     }
    //     if(transform.parent == RectPlayerSlot)
    //     {
    //         PlayerManager.CmdCardAttack(gameObject);
    //         startPosition = transform.position;
    //         startParent = transform.parent.gameObject;
    //         isDragging = true;
    //     }
    // }

    // public void EndDrag()
    // {
    //     if(!isDraggable)
    //     {
    //         return;
    //     }

    //     isDragging = false;
    //     if (isOverDropZone && PlayerManager.IsMyTurn )
    //     {
    //         transform.SetParent(dropZone.transform, false);
    //         isDraggable = false;
    //         // NetworkIdentity networkIdentity = NetworkClient.connection.identity;
    //         // PlayerManager = networkIdentity.GetComponent<PlayerManager>();
    //         PlayerManager.PlayCard(gameObject);
    //     }   
    //     else
    //     {
    //         transform.position = startPosition;
    //         transform.SetParent(startParent.transform, false);
    //     }
    // }
}
