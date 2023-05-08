using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    
    public float startingScale;
    private float startingWidth;
    private SpriteRenderer renderer;
    private float ratio;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        startingScale = this.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
    }
   public void scaleHpBar(int health, int maxHealth)
    {
        float scale = (float)health / maxHealth;
        this.transform.localScale = new Vector3(startingScale * scale, this.transform.localScale.y, this.transform.localScale.z);
        
    }
}
