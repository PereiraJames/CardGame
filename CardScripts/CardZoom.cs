using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardZoom : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject Card;
    public Sprite zoomCardSprite;
    private GameObject zoomCard;


    public void Awake()
    {
        Canvas = GameObject.Find("Main Canvas");
    }

    public void OnHoverEnter()
    {
        zoomCardSprite = gameObject.GetComponent<Image>().sprite;
        Card.GetComponent<Image>().sprite = zoomCardSprite;
        zoomCard = Instantiate(Card, new Vector2(744, 0), Quaternion.identity);
        zoomCard.transform.SetParent(Canvas.transform, false);
        zoomCard.layer = LayerMask.NameToLayer("Zoom");
    }

    public void OnHoverExit()
    {
        Destroy(zoomCard);
    }
}
