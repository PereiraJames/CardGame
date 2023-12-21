using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerManager : NetworkBehaviour
{
    public GameObject Ping;
    public GameObject UD;
    public GameObject PlayerArea;
    public GameObject EnemyArea;
    public GameObject PlayerSlot;
    public GameObject EnemySlot;
    public GameObject PlayerYard;
    public GameObject EnemyYard;
    public List <GameObject> PlayerSockets = new List<GameObject>();
    public List <GameObject> EnemySockets = new List<GameObject>();
    public int cardsPlayed = 0;

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

        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        PlayerArea = GameObject.Find("PlayerArea");
        EnemyArea = GameObject.Find("EnemyArea");

        PlayerYard = GameObject.Find("PlayerYard");
        EnemyYard = GameObject.Find("EnemyYard");

        PlayerSlot = GameObject.Find("PlayerSlot");
        EnemySlot = GameObject.Find("EnemySlot");

        PlayerSockets.Add(PlayerSlot);
        EnemySockets.Add(EnemySlot);

        if(isClientOnly)
        {
            IsMyTurn = true;
        }
    }

    [Server]
    public override void OnStartServer()
    {
        cards.Add(Ping);
        cards.Add(UD);
    }

    [Command]
    public void CmdDealCards()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject card = Instantiate(cards[Random.Range(0,cards.Count)], new Vector3(0,0,0), Quaternion.identity);
            NetworkServer.Spawn(card, connectionToClient);
            RpcShowCard(card, "Dealt");
        }
        RpcGMChangeState("Compile {}");
    }

    public void PlayCard(GameObject card)
    {
        card.GetComponent<CardAbilities>().OnCompile();
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
            PlayerManager pm = NetworkClient.connection.identity.GetComponent<PlayerManager>();
            pm.IsMyTurn = !pm.IsMyTurn;
        }
        else
        {
            Debug.Log("ASJKASJKDNAJISBNDASBDJ");
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
        if (stateRequest == "Compile {}")
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

    [Command]
    public void CmdExecute()
    {
        RpcExecute();
    }

    [ClientRpc]
    void RpcExecute()
    {
        for (int i = 0; i < PlayerSockets.Count; i++)
        {
            PlayerSockets[i].transform.GetComponentInChildren<CardAbilities>().OnExecute();
            PlayerSockets[i].transform.GetChild(0).gameObject.transform.SetParent(PlayerYard.transform, false);
            EnemySockets[i].transform.GetChild(0).gameObject.transform.SetParent(EnemyYard.transform, false);
        }
    }

    [Command]
    public void CmdGMChangeVariables(int variables)
    {
        RpcGMChangeVariables(variables);
    }

    [ClientRpc]
    public void RpcGMChangeVariables(int variables)
    {
        GameManager.ChangeVariables(variables, isOwned);
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
