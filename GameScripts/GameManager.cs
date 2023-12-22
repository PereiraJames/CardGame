using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : NetworkBehaviour
{
    public UIManager UIManager;
    public int TurnOrder = 0;
    public string GameState = "Ready";
    public int PlayerHealth = 40;
    public int EnemyHealth = 40;
    public int PlayerVariables = 0;
    public int EnemyVariables = 0;

    private int ReadyClicks = 0;

    // Start is called before the first frame update
    void Start()
    {
        UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        UIManager.UpdatePlayerText();
        UIManager.UpdateButtonText(GameState); 
    }

    public void Update()
    {

    }

    public void ChangeGameState(string stateRequest)
    {
        if (stateRequest == "Initialize {}")
        {
            ReadyClicks = 0;
            GameState = "Initialize {}";
        }
        else if (stateRequest == "End Turn")
        {
            if (ReadyClicks == 1)
            {
                GameState = "End Turn";
                UIManager.HighlightTurn(TurnOrder);
            }
        }
        else if (stateRequest == "Execute {}")
        {
            GameState = "Execute {}";
            TurnOrder = 0;
        }
        UIManager.UpdateButtonText(GameState);
    }

    public void ChangeReadyClicks()
    {
        ReadyClicks++;
    }

    public void CardPlayed()
    {
        Debug.Log("Player: " + PlayerHealth);
        Debug.Log("Enemy" + EnemyHealth);
        TurnOrder++;
        UIManager.HighlightTurn(TurnOrder);
        if(isOwned)
        {
            if (PlayerHealth <= 0)
            {
                Debug.Log("PLAYER2 WINS!");
            }
            else if(EnemyHealth <= 0)
            {
                Debug.Log("PLAYER1 Wins!");
            }
        }
        else if (!isOwned)
        {
            if (PlayerHealth <= 0)
            {
                Debug.Log("PLAYER2 WINS!");
            }
            else if(EnemyHealth <= 0)
            {
                Debug.Log("PLAYER1 Wins!");
            }
        }

        // if (TurnOrder == 10)
        // {
        //     ChangeGameState("Execute {}");

        // }
    }

    public void EndTurn()
    {
        TurnOrder++;
        UIManager.HighlightTurn(TurnOrder);
    }

    public void ChangeBP(int playerBp, int enemyBp, bool isOwned)
    {
        if(isOwned)
        {
            PlayerHealth += playerBp;
            EnemyHealth -= enemyBp;
        }
        else
        {
            PlayerHealth -= playerBp;
            EnemyHealth += enemyBp;
        }
        UIManager.UpdatePlayerText();
    }

    public void ChangeVariables(int variables, bool isOwned)
    {
        if(isOwned)
        {
            PlayerVariables += variables;
        }
        else
        {
            EnemyVariables += variables;
        }
        UIManager.UpdatePlayerText();
    }

    public void AdjustPlayerHealth(int health, bool isOwned) // For adjusting health of first-person player
    {
        if (isOwned)
        {
            PlayerHealth += health;
        }
        else
        {
            EnemyHealth += health;
        }
        UIManager.UpdatePlayerText();
    } 

    public void AdjustEnemyHealth(int health, bool isOwned) // For adjusting health of first-person enemy
    {
        if (isOwned)
        {
            EnemyHealth += health;
        }
        else
        {
            PlayerHealth += health;
        }
        UIManager.UpdatePlayerText();
    } 
}


