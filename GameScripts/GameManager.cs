using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : NetworkBehaviour
{
    public UIManager UIManager;
    public int TurnOrder = 0;
    public string GameState = "Ready";
    public int PlayerHealth = 20;
    public int EnemyHealth = 20;

    public int totalPlayerDoubloons = 2;
    public int currentPlayerDoubloons = 0;
    
    public int totalEnemyDoubloons = 2;   
    public int currentEnemyDoubloons = 0;

    public int TotalDoubloons = 40;

    private int ReadyClicks = 0;

    // Start is called before the first frame update
    void Start()
    {
        UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        // UIManager.UpdatePlayerText();
        UIManager.UpdateButtonText(GameState); 

        currentEnemyDoubloons = totalEnemyDoubloons;
        currentPlayerDoubloons = totalPlayerDoubloons;
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
        TurnOrder++;
        UIManager.HighlightTurn(TurnOrder);

        if(isOwned)
        {
            if (PlayerHealth <= 0)
            {
                UIManager.DisplayWin(false);
            }
            else if(EnemyHealth <= 0)
            {
                Debug.Log("You Wins!");
                UIManager.DisplayWin(true);
            }
        }
        else if (!isOwned)
        {
            if (PlayerHealth <= 0)
            {
                Debug.Log("They WINS!");
                UIManager.DisplayWin(false);
            }
            else if(EnemyHealth <= 0)
            {
                Debug.Log("You Wins!");
                UIManager.DisplayWin(true);
            }
        }

        // if (TurnOrder == 10)
        // {
        //     ChangeGameState("Execute {}");

        // }
        UIManager.UpdatePlayerText();
    }

    public void EndTurn()
    {
        TurnOrder++;
        UIManager.HighlightTurn(TurnOrder);
        currentPlayerDoubloons = totalPlayerDoubloons;
        currentEnemyDoubloons = totalEnemyDoubloons;
        UIManager.UpdatePlayerText();
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

    public void UpdateDoubloons(int amount, bool isOwned, bool stealing)
    {
        if (TotalDoubloons < 1)
        {
            return;
        }

        if(amount >= TotalDoubloons)
        {
            amount = TotalDoubloons;
        }

        if(stealing)
        {
            if(isOwned)
            {
                totalPlayerDoubloons += amount;
                TotalDoubloons -= amount;
            }
            else
            {
                totalEnemyDoubloons += amount;
                TotalDoubloons -= amount;
            }
        }
        
        if(!stealing)
        {
            if(isOwned)
            {
                currentPlayerDoubloons -=amount;
            }
            else
            {
                currentEnemyDoubloons -= amount;
            }
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


