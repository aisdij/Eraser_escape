using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEnemy : RoomContent
{
    private Main main;
    public GameObject enemy;
    public Vector3 spawnnlocation;
    // Start is called before the first frame update
    void Start()
    {
        main = GameObject.FindObjectOfType<Main>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override public void enter()
    {
        summonEnemy();
    }
    public void summonEnemy()
    {
        Instantiate(enemy, spawnnlocation,Quaternion.identity);
        main.enemy = FindObjectOfType<Enemy>();
        
    }
}
