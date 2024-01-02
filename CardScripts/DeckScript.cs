using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class DeckScript : NetworkBehaviour
{
    public GameManager GameManager;

    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void UpdateDeckSize(int amount)
    {
        GameManager.PlayerDeckSize -= amount;
    }

    public void OnHoverEnter()
    {
        foreach (Transform child in gameObject.GetComponentInChildren<Transform>())
        {
            child.GetComponent<Image>().enabled = true;
            child.GetComponentInChildren<Text>().enabled = true;
            if(gameObject == GameObject.Find("PlayerDeckUI"))
            {
                child.GetComponentInChildren<Text>().text = "Player Deck Size: " + GameManager.PlayerDeckSize;
            }
            else if (gameObject == GameObject.Find("EnemyDeckUI"))
            {
                child.GetComponentInChildren<Text>().text = "Enemy Deck Size: " + GameManager.EnemyDeckSize;
            }
        }
    }

    public void OnHoverExit()
    {
        foreach (Transform child in gameObject.GetComponentInChildren<Transform>())
        {
            child.GetComponent<Image>().enabled = false;
            child.GetComponentInChildren<Text>().enabled = false;
        } 
    }
}
