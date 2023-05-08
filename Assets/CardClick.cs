using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardClick : MonoBehaviour
{
    private Main main;
    private Vector3 defaultScale;

    private const float hoverScale = 1.3f;



    void Start()
    {
        main = GameObject.FindObjectOfType<Main>();
        defaultScale = transform.localScale;
    }

    void OnMouseDown()
    {
        main.onClickedCard(transform.parent.GetComponent<Card>());
    }

    void OnMouseEnter()
    {
        main.scale(this.gameObject, defaultScale * hoverScale, 0.1f);
    }

    void OnMouseExit()
    {
        main.scale(this.gameObject, defaultScale, 0.1f);

    }
}
