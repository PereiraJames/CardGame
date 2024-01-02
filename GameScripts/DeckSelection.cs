using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DeckSelection : NetworkBehaviour
{
    public GameObject DeckSelectionUI;
    public PlayerManager PlayerManager;

    public string DeckTag;

    public void OnSelected()
    {
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        PlayerManager.CmdDeckSelection(DeckTag);
        
        DeckSelectionUI = GameObject.Find("DeckSelectionUI");
        Destroy(DeckSelectionUI);

        UIManager UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        // UIManager.SetPlayerUI();
    }
}