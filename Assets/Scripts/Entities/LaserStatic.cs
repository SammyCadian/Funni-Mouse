using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserStatic : MonoBehaviour
{
    [SerializeField] private float laserTime;
    [SerializeField] private float laserDelay;
    [SerializeField] Color warningColor;
    private float existanceTimer;
    private Color Defaultcolor;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
        Defaultcolor = transform.GetChild(0).GetComponent<SpriteRenderer>().color;
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = warningColor;
        existanceTimer = laserTime + laserDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if(existanceTimer > 0)
        {
            if(existanceTimer <= laserTime){transform.GetChild(0).GetComponent<SpriteRenderer>().color = Defaultcolor; transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = true;}
            existanceTimer -= Time.deltaTime;
        }else
        {
            Destroy(gameObject, 0.5f);
        }
    }
}
