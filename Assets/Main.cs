using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public RoomSelect currentRoom;
    public GameObject characterMapView;
    public GameObject camera;
    public List<GameObject> deck;
    public List<GameObject> handCards;
    public int health;
    public int maxHealth;
    public HpBar hpBar;
    public Enemy enemy;
    public bool playerTurn = true;
    public GameObject endTurnButton;

    private Collider2D UI_blocker;
    private Animation characterAnimations;

    private readonly Vector3 usedCardCoords = new Vector3(20.8f, 0, -4);
    private readonly Vector3 usedCardScale = new Vector3(1.3f, 1.3f, 1f);

    // Start is called before the first frame update
    void Start()
    {
        characterMapView.transform.position = new Vector3(currentRoom.transform.position.x, currentRoom.transform.position.y, -1f);

        UI_blocker = GameObject.Find("UI_blocker").GetComponent<Collider2D>();
        characterAnimations = GameObject.Find("Battlefield").GetComponent<Animation>();
        UI_blocker.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerTurn) UI_blocker.enabled = true;
    }
    public void onClickedRoom(RoomSelect room)
    {
        if (room.connectedRooms.Contains(currentRoom))
        {
            currentRoom = room;
            if(currentRoom.roomContent.GetType() == typeof(RoomEnemy)) {
                startFight((RoomEnemy)room.roomContent);
            }
            characterMapView.transform.position = room.transform.position;
        }
    }
    void startFight(RoomEnemy room)
    {
        //camera.transform.position = new Vector3(20.8f, 0, -10);
        move(camera, new Vector3(20.8f, 0, -10), 1);

        disableUI(1f);

        drawCards(3);
        moveAndScaleCards(true);

        room.enter();
    }
    public void onClickedCard(Card card)
    {
        card.GetComponent<Collider2D>().enabled = false;
        handCards.Remove(card.gameObject);
        moveAndScaleCards(false, 0.1f);

        showUsedCard(card.gameObject);

        disableUI(1.5f);

        StartCoroutine(destroyCard());

        IEnumerator destroyCard()
        {
            yield return new WaitForSeconds(1.5f);
            disableUI(1.9f);

            characterAnimations.Play(card.animation);
            Destroy(card.gameObject);
            yield return new WaitForSeconds(card.damageDelay);
            enemy.receiveDamage(card.damage);

            StartCoroutine(delayed());

            IEnumerator delayed() 
            {
                yield return new WaitForSeconds(1f);

                moveAndScaleCards(true);
            }
        }
    }

    public List<GameObject> drawCards(int count)
    {
        List<GameObject> drawnCards = new List<GameObject>();
        for(int i = 0; i < count; i++)
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
        GameObject handcard = Instantiate(card, new Vector3(35f, -3, -3), Quaternion.identity);
        handCards.Add(handcard);
        moveAndScaleCards(true);
        disableUI(0.3f);
        return handcard;
    }

    private void moveAndScaleCards(bool beingUsed, float duration = 0.3f)
    {
        moveCards(beingUsed, duration);
        scaleCards(beingUsed, duration);
    }

    private void moveCards(bool beingUsed, float duration = 0.3f)
    {
        float gap = beingUsed ? 3f : 1.5f;
        float calc = ((float)handCards.Count - 1) / 2 * gap;
        float offset = 20.8f - calc;


        for (int i = 0; i < handCards.Count; i++)
        {
            move(handCards[i], new Vector3(offset + i * gap, beingUsed ? -3 : -5, -3f - 0.01f*i), duration);
        }
    }

    private void scaleCards(bool beingUsed, float duration = 0.3f)
    {
        for (int i = 0; i < handCards.Count; i++)
        {
            scale(handCards[i], new Vector3(beingUsed ? 1f : 0.75f, beingUsed ? 1f: 0.75f, 1), duration);
        }
    }

    public void move(GameObject gameobject, Vector3 endPos, float duration)
    {
        Vector3 startPos = gameobject.transform.position;

        StartCoroutine(Lerp());

        IEnumerator Lerp()
        {
            float timeElapsed = 0;
            while (timeElapsed < duration)
            {
                gameobject.transform.position = Vector3.Lerp(startPos, endPos, timeElapsed/duration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            gameobject.transform.position = endPos;
        }
    }
    private void showUsedCard(GameObject gameObject)
    {
        move(gameObject, usedCardCoords, 0.1f);
        scale(gameObject, usedCardScale, 0.1f);
    }

    public void scale(GameObject gameobject, Vector3 endScale, float duration)
    {
        Vector3 startScale = gameobject.transform.localScale;

        StartCoroutine(Lerp());

        IEnumerator Lerp()
        {
            float timeElapsed = 0;
            while (timeElapsed < duration)
            {
                gameobject.transform.localScale = Vector3.Lerp(startScale, endScale, timeElapsed / duration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            gameobject.transform.localScale = endScale;
        }
    }

    public void disableUI(float duration)
    {
        StartCoroutine(delay());

        IEnumerator delay()
        {
            float timeElapsed = 0;
            while(timeElapsed < duration)
            {
                timeElapsed += Time.deltaTime;
                UI_blocker.enabled = true;
                yield return null;
            }
            UI_blocker.enabled = false;
        }
    }

    public void endTurn()
    {
        playerTurn = false;
        endTurnButton.SetActive(false);
        moveAndScaleCards(false);
        enemy.startTurn();
    }

    public void startTurn()
    {
        endTurnButton.SetActive(true);
        playerTurn = true;
        moveAndScaleCards(true);
        drawRandomCard();
    }
    public void receiveDamage(int damage)
    {
        health -= damage;
        if (health < 0)
        {
            health = 0;
        }
        hpBar.scaleHpBar(health, maxHealth);
    }
}
