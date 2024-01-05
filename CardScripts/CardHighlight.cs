using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardHighlight : MonoBehaviour
{
    public UIManager UIManager;

    void Start()
    {
        UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }
    public void CardSelect()
    {
        // if(!UIManager.isCardSelected && isDrag() == false)
        // {
        //     UIManager.isCardSelected = true;

        //     if(UIManager.currentSelectedCard != null)
        //     {
        //         UIManager.currentSelectedCard.GetComponent<Outline>().effectDistance = new Vector2(1,1);
        //     }

        //     UIManager.currentSelectedCard = gameObject;
        //     UIManager.currentSelectedCard.GetComponent<Outline>().effectDistance = new Vector2(10,10);
        // }
        // else if(UIManager.isCardSelected && UIManager.currentSelectedCard == gameObject && isDrag() == false)
        // {
        //     UIManager.isCardSelected = false;
        //     UIManager.currentSelectedCard.GetComponent<Outline>().effectDistance = new Vector2(1,1);
        //     UIManager.currentSelectedCard = null;
        // }
        // else if(UIManager.isCardSelected && UIManager.currentSelectedCard != gameObject && isDrag() == false)
        // {
        //     UIManager.currentSelectedCard.GetComponent<Outline>().effectDistance = new Vector2(1,1);
        //     UIManager.currentSelectedCard = gameObject;
        //     UIManager.currentSelectedCard.GetComponent<Outline>().effectDistance = new Vector2(10,10);
        // }
    }

    // public bool isDrag()
    // {
    //     return gameObject.GetComponent<DragDrop>().isDragging;
    // }

    // public void CardHover()
    // {
        
    //     gameObject.GetComponent<Outline>().effectColor = Color.blue;
    //     gameObject.GetComponent<Outline>().effectDistance = new Vector2(10,10);
    // }

    // public void CardUnHover()
    // {
    //     gameObject.GetComponent<Outline>().effectColor = Color.red;
    //     gameObject.GetComponent<Outline>().effectDistance = new Vector2(1,1);
    // }
}
