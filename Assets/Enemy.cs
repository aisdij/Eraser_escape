using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    private int maxHealth;
    public List<GameObject> deck;
    public List<GameObject> handCards;
    public HpBar hpbar;
    public Animation animation;
    public int maxEnergy;
    private int energy;
    private Main main;
    // Start is called before the first frame update
    void Start()
    {
        main = GameObject.FindObjectOfType<Main>();
        maxHealth = health;
        energy = maxEnergy;
        drawCards(3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void receiveDamage(int damage)
    {
        health -= damage;
        if(health < 0)
        {
            health = 0;
        }
        hpbar.scaleHpBar(health, maxHealth);
    }

    public void startTurn()
    {
        energy = maxEnergy;
        drawRandomCard();
        useCard();
    }

    public List<GameObject> drawCards(int count)
    {
        List<GameObject> drawnCards = new List<GameObject>();
        for (int i = 0; i < count; i++)
        {
            drawnCards.Add(drawRandomCard());
        }
        return drawnCards;
    }

    public GameObject drawRandomCard()
    {
        return drawCard(deck[(int)System.Math.Floor(Random.value * deck.Count)]);
    }

    public GameObject drawCard(GameObject card)
    {
        GameObject handcard = Instantiate(card, new Vector3(40f, 0, -1), Quaternion.identity);
        handCards.Add(handcard);
        return handcard;
    }

    public void useCard()
    {
        StartCoroutine(delayed());

        IEnumerator delayed()
        {
            bool used = false;
            for (int i = 0; i < handCards.Count; i++)
            {
                int cost = handCards[i].GetComponent<Card>().energyCost;
                GameObject usedCard = handCards[i];
                if (useCard(usedCard.GetComponent<Card>()))
                {
                    used = true;
                    i = -1;
                    yield return new WaitForSeconds(3);
                }
            }
            main.startTurn();
        }
    }

    public bool useCard(Card card)
    {
        if (energy < card.energyCost)
        {
            return false;
        }

        energy -= card.energyCost;
        handCards.Remove(card.gameObject);
        GameObject handcard = Instantiate(card.gameObject, new Vector3(40f, 0f, -1f), Quaternion.identity);
        main.move(handcard, new Vector3(20.8f, 0, -1), 0.15f);
        main.scale(handcard, new Vector3(1.3f, 1.3f, 1), 0.15f);

        StartCoroutine(destroyCard());

        IEnumerator destroyCard()
        {
            yield return new WaitForSeconds(1.5f);

            animation.Play(card.animation);
            yield return new WaitForSeconds(card.damageDelay);
            main.receiveDamage(card.damage);
            Destroy(handcard);
        }

        return true;
    }
}
