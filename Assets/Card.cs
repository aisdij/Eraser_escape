using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int damage;
    public string title;
    public string description;
    public TextMeshPro title_tmp;
    public TextMeshPro description_tmp;
    private TextMeshPro energy_tmp;
    private Main main;
    public Sprite image;
    public SpriteRenderer imageRenderer;
    private GameObject energy_go;
    public int energyCost;
    public float damageDelay;
    public string animation;
    // Start is called before the first frame update
    void Start()
    {
        main = GameObject.FindObjectOfType<Main>();
        energy_go = gameObject.transform.Find("template/Energy").gameObject;
        energy_tmp = gameObject.transform.Find("template/Energy/Cost").GetComponent<TextMeshPro>();

        title_tmp.text = title;
        description_tmp.text = description;
        energy_tmp.text = "cost: " + energyCost;
        imageRenderer.sprite = image;

        if (energyCost==0)
        {
            Destroy(energy_go);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown()
    {
        main.onClickedCard(this);
    }
    
}
