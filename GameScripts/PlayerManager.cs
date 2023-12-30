using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerManager : NetworkBehaviour
{
    // public GameObject Ping;
    // public GameObject UD;
    // public GameObject FD;
    // public GameObject RS;
    // public GameObject MG;
    public GameObject WinText;

    public GameObject PlayerArea;
    public GameObject EnemyArea;
    public GameObject PlayerSlot;
    public GameObject EnemySlot;

    public GameObject PlayerImage;
    public GameObject EnemyImage;

    public GameObject PlayerYard;
    public GameObject EnemyYard;
    public List <GameObject> PlayerSockets = new List<GameObject>();
    public List <GameObject> EnemySockets = new List<GameObject>();
    public int cardsPlayed = 0;

    public bool AttackBeingMade = false;
    public bool DestroyBeingMade = false;

    public bool AttackDisplayOpened = false;

    public NetworkManager NetworkManager;

    [SyncVar]
    public GameObject AttackedTarget;

    [SyncVar]
    public GameObject AttackingTarget;

    public GameManager GameManager;

    public int CardsPlayed = 0;

    public bool IsMyTurn = false;

    private List <GameObject> cards = new List<GameObject>();

    // [SyncVar]
    // int cardsPlayed = 0;

    // Start is called before the first frame update
    public override void OnStartClient()
    {
        base.OnStartClient();
        
        NetworkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();

        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        PlayerArea = GameObject.Find("PlayerArea");
        EnemyArea = GameObject.Find("EnemyArea");

        PlayerYard = GameObject.Find("PlayerYard");
        EnemyYard = GameObject.Find("EnemyYard");

        PlayerSlot = GameObject.Find("PlayerSlot");
        EnemySlot = GameObject.Find("EnemySlot");

        PlayerImage = GameObject.Find("PlayerImage");
        EnemyImage = GameObject.Find("EnemyImage");

        PlayerSockets.Add(PlayerSlot);
        EnemySockets.Add(EnemySlot);

        foreach (GameObject tests in NetworkManager.spawnPrefabs) //Auto puts prefabs into a list(or a deck)
        {
            if(tests.tag == "Cards")
            {
                cards.Add(tests);
            }
        }

        if(isClientOnly)
        {
            IsMyTurn = true;
        }
    }

    [Server]
    public override void OnStartServer()
    {
        // cards.Add(Ping);
        // cards.Add(UD);
        // cards.Add(FD);
        // cards.Add(MG);
        // cards.Add(RS);
    }

    [Command]
    public void CmdDealCards(int cardAmount)
    {
        for (int i = 0; i < cardAmount; i++)
        {
            if(GameManager.amountofPlayerCards < 8)
            {
                GameObject card = Instantiate(cards[Random.Range(0,cards.Count)], new Vector3(0,0,0), Quaternion.identity);
                NetworkServer.Spawn(card, connectionToClient);
                RpcShowCard(card, "Dealt");
                if(isOwned)
                {
                    GameManager.amountofPlayerCards++;
                }
            }
        }
    }

    public void PlayCard(GameObject card)
    {
        card.GetComponent<CardAbilities>().OnEntry();
        CmdPlayCard(card);
    }

    [Command]
    void CmdPlayCard(GameObject card)
    {
        RpcShowCard(card, "Played");
    }

    [ClientRpc]
    void RpcShowCard(GameObject card, string type)
    {
        if (type == "Dealt")
        {
            if (isOwned)
            {
                card.transform.SetParent(PlayerArea.transform, false);
                card.GetComponent<CardFlipper>().SetSprite("cyan");
            }
            else
            {
                card.transform.SetParent(EnemyArea.transform, false);
                card.GetComponent<CardFlipper>().SetSprite("magenta");
                card.GetComponent<CardFlipper>().Flip();
            }
        }
        else if (type == "Played")
        {
            if (CardsPlayed == 5)
            {
                CardsPlayed = 0;
            }
            if (isOwned)
            {
                card.transform.SetParent(PlayerSlot.transform, false); //Make sure its the right dropzone variable
                CmdGMCardPlayed();
            }
            if(!isOwned)
            {
                card.transform.SetParent(EnemySlot.transform, false); //Make sure its the right dropzone variable
                card.GetComponent<CardFlipper>().Flip();
            }
            CardsPlayed++;
        }
        else
        {
            Debug.Log("ASJKASJKDNAJISBNDASBDJ");
        }
    }

    [Command]
    public void CmdEndTurn()
    {
        RpcEndTurn();
    }

    [ClientRpc]
    public void RpcEndTurn()
    {
        PlayerManager pm = NetworkClient.connection.identity.GetComponent<PlayerManager>();
        pm.IsMyTurn = !pm.IsMyTurn;
        GameManager.EndTurn();
    }

    [Command]
    public void CmdResetAttackTurns()
    {
        RpcResetAttackTurns();
    }
    
    [ClientRpc]
    public void RpcResetAttackTurns()
    {
        foreach (Transform child in PlayerSlot.GetComponentsInChildren<Transform>())
        {
            if (child.gameObject.tag == "Cards")
            {
                child.GetComponent<CardDetails>().AttackTurn(true);
            }
        }

        foreach (Transform child in EnemySlot.GetComponentsInChildren<Transform>())
        {
            if (child.gameObject.tag == "Cards")
            {
                child.GetComponent<CardDetails>().AttackTurn(true);
            }
        }
        
    }

    [Command]
    public void CmdGMChangeState(string stateRequest)
    {
        RpcGMChangeState(stateRequest);
    }


    [ClientRpc]
    void RpcGMChangeState(string stateRequest)
    {
        GameManager.ChangeGameState(stateRequest);
        if (stateRequest == "End Turn")
        {
            GameManager.ChangeReadyClicks();
        }
    }

    [Command]
    void CmdGMCardPlayed()
    {
        RpcGMCardPlayed();
    }

    [ClientRpc]
    void RpcGMCardPlayed()
    {
        GameManager.CardPlayed();
    }

    //CARD ABILITIES
    [Command]
    public void CmdDealDamage(GameObject Target, int Damage)
    {
        RpcDealDamage(Target, Damage);
    }

    [ClientRpc]
    public void RpcDealDamage(GameObject Target, int Damage)
    {   
        Target.GetComponent<CardDetails>().SetCardHealth(Damage);

        int TargetHealth = Target.GetComponent<CardDetails>().GetCardHealth();
        if(TargetHealth < 1)
        {
            Target.GetComponent<CardZoom>().OnHoverExit();
            Destroy(Target);
        }
    }

    [Command]
    public void CmdDestroyTarget(GameObject Target)
    {
        RpcDestoryTarget(Target);
    }

    [ClientRpc]
    public void RpcDestoryTarget(GameObject Target)
    {
        Target.GetComponent<CardZoom>().OnHoverExit();
        Destroy(Target);
    }

    [Command]
    public void CmdAttackingDetails(GameObject target, int targetNum)
    {
        RpcAttackingDetails(target, targetNum);
    }

    [ClientRpc]
    public void RpcAttackingDetails(GameObject target, int targetNum)
    {
        if(targetNum == 1)
        {
            AttackedTarget = target;
            Debug.Log("Attacked Target: " + AttackedTarget);
        }
        else if(targetNum == 0)
        {
            AttackingTarget = target;
            Debug.Log("Attacking Target: " + AttackingTarget);
        }

        AttackingTarget.GetComponent<CardAttack>().DealAttack();
    }

    [Command]
    public void CmdCardAttack()
    {
        RpcCardAttack();
    }

    [ClientRpc]
    public void RpcCardAttack()
    {
        int AttackDamage;
        int EnemyHealth;

        AttackDamage = AttackingTarget.GetComponent<CardDetails>().GetCardAttack();
        EnemyHealth = AttackedTarget.GetComponent<CardDetails>().GetCardHealth();

        EnemyHealth -= AttackDamage;
        Debug.Log("Enemy Health: " + EnemyHealth);
        AttackedTarget.GetComponent<CardDetails>().SetCardHealth(EnemyHealth);
        if(EnemyHealth < 1)
        {
            AttackedTarget.GetComponent<CardZoom>().OnHoverExit();
            Destroy(AttackedTarget);
        }
        AttackedTarget.GetComponent<CardDetails>().UpdateCardText();
        AttackingTarget.GetComponent<CardDetails>().AttackTurn(false);
        AttackingTarget = null;
        AttackedTarget = null;
    }

    [Command]
    public void CmdCardSpecial(GameObject card)
    {
        RpcCardSpecial(card);
    }

    [ClientRpc]
    void RpcCardSpecial(GameObject card)
    {
        // card.GetComponent<CardAbilities>().OnSpec();
    }

    [Command]
    public void CmdUpdateDoubloons(int amount, bool stealing)
    {
        RpcUpdateDoubloons(amount, stealing);
    }

    [ClientRpc]
    public void RpcUpdateDoubloons(int amount, bool stealing)
    {
        GameManager.UpdateDoubloons(amount, isOwned, stealing);
    }

    [Command]
    public void CmdChangeBP(int playerBp, int enemyBp)
    {
        RpcChangeBP(playerBp, enemyBp);
    }

    [ClientRpc]
    public void RpcChangeBP(int playerBp, int enemyBp)
    {
        GameManager.ChangeBP(playerBp, enemyBp, isOwned);
    }

    [Command]
    public void CmdGMPlayerHealth(int health)
    {
        RpcGMPlayerHealth(health);
    }

    [ClientRpc]
    public void RpcGMPlayerHealth(int health)
    {
        GameManager.AdjustPlayerHealth(health, isOwned);
    }

    [Command]
    public void CmdGMEnemyHealth(int health)
    {
        RpcGMPEnemyHealth(health);
    }

    [ClientRpc]
    public void RpcGMPEnemyHealth(int health)
    {
        GameManager.AdjustEnemyHealth(health, isOwned);
    }
}
