using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DrawCards : NetworkBehaviour
{
    public PlayerManager PlayerManager;
    public GameManager GameManager;
    public NetworkManager NetworkManager;

    private void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void OnClick()
    {
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();

        // if (GameManager.GameState == "Ready")
        // {
        //     IntializeClick();
        // }
        // else if (GameManager.GameState == "End Turn")
        // {
        //     if (PlayerManager.IsMyTurn)
        //     {
        //         Endturn();
        //     }
        // }

        if (PlayerManager.IsMyTurn)
        {
            Endturn();
        }
        // else if (GameManager.GameState == "Execute {}")
        // {
        //     ExecuteClick();
        // }
    }

    // void IntializeClick()
    // {
    //     PlayerManager.CmdDealCards(5, GameManager.PlayerDeck, true);
    //     PlayerManager.CmdGMChangeState("End Turn");
    // }

    void Endturn()
    {
        PlayerManager.CmdDealCards(1, GameManager.PlayerDeck, true);
        PlayerManager.CmdEndTurn();
    }

    // void ExecuteClick()
    // {
    //     PlayerManager.CmdExecute();
    //     PlayerManager.CmdGMChangeState("Initialize {}");
    // }
}