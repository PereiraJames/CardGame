using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DrawCards : NetworkBehaviour
{
    public PlayerManager PlayerManager;
    public GameManager GameManager;

    private void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void OnClick()
    {
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();

        if (GameManager.GameState == "Ready")
        {
            IntializeClick();
        }
        else if (GameManager.GameState == "End Turn")
        {
            Endturn();
        }
        else if (GameManager.GameState == "Execute {}")
        {
            ExecuteClick();
        }
    }

    void IntializeClick()
    {
        PlayerManager.CmdDealCards();
        PlayerManager.CardsPlayed = 0;
    }

    void Endturn()
    {
        PlayerManager.CmdEndTurn();
    }

    void ExecuteClick()
    {
        PlayerManager.CmdExecute();
        PlayerManager.CmdGMChangeState("Initialize {}");
    }
}