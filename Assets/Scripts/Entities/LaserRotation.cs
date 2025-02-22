using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserRotation : MonoBehaviour
{
    [SerializeField] private float laserTime;
    [SerializeField] private float laserDelay;
    [SerializeField] private bool startRight;
    [SerializeField] Color warningColor;
    private float existanceTimer;
    private float turnRate;
    private Color Defaultcolor;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
        Defaultcolor = transform.GetChild(0).GetComponent<SpriteRenderer>().color;
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = warningColor;
        existanceTimer = laserTime + laserDelay;
        turnRate = 90f/laserTime;
        if(startRight){turnRate *= -1;}
    }

    // Update is called once per frame
    void Update()
    {
        if(existanceTimer > 0)
        {
            if(existanceTimer <= laserTime){transform.GetChild(0).GetComponent<SpriteRenderer>().color = Defaultcolor; transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = true; transform.Rotate(0,0,turnRate * Time.deltaTime);}
            existanceTimer -= Time.deltaTime;
        }else
        {
            Destroy(gameObject, 0.5f);
        }
    }
}
