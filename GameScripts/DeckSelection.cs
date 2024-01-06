using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DeckSelection : NetworkBehaviour
{
    public GameObject DeckSelectionUI;
    public PlayerManager PlayerManager;
    public GameManager GameManager;

    public string DeckTag;

    public void OnSelected()
    {
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();

        PlayerManager.CmdDeckSelection(DeckTag);
        
        DeckSelectionUI = GameObject.Find("DeckSelectionUI");

        UIManager UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        // UIManager.SetPlayerUI();
    }

    public void PlayerReady()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();

        if(GameManager.PlayerDeck != "")
        {
            bool bothPlayersReady = false;
            Debug.Log("Player Ready");
            if (GameManager.EnemyDeck != "")
            {
                bothPlayersReady = true;
            }
            PlayerManager.CmdDestoryDeckSelectionUI(bothPlayersReady);
        }

        //FOR TESTING!
        // PlayerManager.CmdDestoryDeckSelectionUI(true);

    } 
}