using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class UIManager : NetworkBehaviour
{
    public PlayerManager PlayerManager;
    public GameManager GameManager;
    public GameObject Button;
    public GameObject PlayerText;
    public GameObject EnemyText;

    Color blueColor = new Color32(17, 216, 238, 255);

    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void UpdatePlayerText()
    {
        PlayerText.GetComponent<Text>().text = "Player Health: " + GameManager.PlayerHealth + "\nPlayer Variables: " + GameManager.PlayerVariables;
        EnemyText.GetComponent<Text>().text = "Enemy Health: " + GameManager.EnemyHealth + "\nEnemy Variables: " + GameManager.EnemyVariables;
    }

    public void UpdateButtonText(string gameState)
    {
        Button = GameObject.Find("Button");
        Button.GetComponentInChildren<Text>().text = gameState;
    }

    public void HighlightTurn(int turnOrder)
    {
        PlayerManager = NetworkClient.connection.identity.GetComponent<PlayerManager>();

            if (turnOrder == 0)
            {
                if (PlayerManager.IsMyTurn)
                {
                    PlayerManager.PlayerSlot.GetComponent<Outline>().effectColor = Color.red;
                    PlayerManager.EnemySlot.GetComponent<Outline>().effectColor = blueColor;

                }
                else
                {
                    PlayerManager.EnemySlot.GetComponent<Outline>().effectColor = Color.red;
                    PlayerManager.PlayerSlot.GetComponent<Outline>().effectColor = blueColor;
                }
            }
            else if (turnOrder > 0)
            {
                if(PlayerManager.IsMyTurn)
                {
                    PlayerManager.PlayerSlot.GetComponent<Outline>().effectColor = Color.red;
                    PlayerManager.EnemySlot.GetComponent<Outline>().effectColor = blueColor;

                    // if(turnOrder > 1)
                    // {   
                    //     PlayerManager.EnemySlot.GetComponent<Outline>().effectColor = blueColor;
                    // }
                }
                else
                {
                    PlayerManager.EnemySlot.GetComponent<Outline>().effectColor = Color.red;
                    PlayerManager.PlayerSlot.GetComponent<Outline>().effectColor = blueColor;
                    // if (turnOrder > 1)
                    // {
                    //     PlayerManager.PlayerSlot.GetComponent<Outline>().effectColor = blueColor;
                    // }
                }
            }
            
        
    }
}
