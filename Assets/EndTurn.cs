using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurn : MonoBehaviour
{
    private Main main;
    // Start is called before the first frame update
    void Start()
    {
        main = GameObject.FindObjectOfType<Main>();
        main.endTurnButton = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseEnter()
    {
        this.transform.localScale = new Vector3(1.05f, 1.05f, 1);
    }

    private void OnMouseExit()
    {
        this.transform.localScale = Vector3.one;
    }

    private void OnMouseDown()
    {
        main.endTurn();
    }
}
