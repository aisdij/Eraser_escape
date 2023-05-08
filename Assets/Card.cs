using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int damage;
    public string title;
    public string description;
    private Main main;
    public Sprite image;
    public int energyCost;
    public float damageDelay;
    public string animation;

    private GameObject frame;

    private const string frame_gameobject_path = "card_frame";
    private const string title_gameobject_path = "card_frame/title";
    private const string description_gameobject_path = "card_frame/description";
    private const string card_image_gameobject_path = "card_frame/card_image";
    private const string energy_cost_gameobject_path = "card_frame/energy_cost";
    private const string energy_icon_gameobject_path = "card_frame/energy_icon";
    private const float scale = 0.42f;

    // Start is called before the first frame update
    void Start()
    {
        setupEnergy();
        setupImage();
        setupScale();
        setTextFromGOPath(title_gameobject_path, title);
        setTextFromGOPath(description_gameobject_path, description);
    }

    private void setupEnergy()
    {
        GameObject energyText = transform.Find(energy_cost_gameobject_path).gameObject;
        GameObject energyIcon = transform.Find(energy_icon_gameobject_path).gameObject;
        if (energyCost == 0)
        {
            energyText.SetActive(false);
            energyIcon.SetActive(false);
        }
        energyText.GetComponent<TextMeshPro>().text = energyCost.ToString();
    }

    private void setupImage()
    {
        GameObject img = transform.Find(card_image_gameobject_path).gameObject;
        img.GetComponent<SpriteRenderer>().sprite = image;
    }

    private void setTextFromGOPath(string gameObjectPath, string text)
    {
        transform.Find(gameObjectPath).GetComponent<TextMeshPro>().text = text;
    }

    private void setupScale()
    {
        frame = transform.Find(frame_gameobject_path).gameObject;
        frame.transform.localScale = frame.transform.localScale * scale;
    }
    
}
