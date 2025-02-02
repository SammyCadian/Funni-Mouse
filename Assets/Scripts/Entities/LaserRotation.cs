using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRotation : MonoBehaviour
{
    [SerializeField] private float laserTime;
    [SerializeField] private float laserDelay;
    [SerializeField] private bool startRight;
    private float existanceTimer;
    private float turnRate;
    // Start is called before the first frame update
    void Start()
    {
        existanceTimer = laserTime + laserDelay;
        turnRate = 90f/laserTime;
        if(startRight){turnRate *= -1;}
    }

    // Update is called once per frame
    void Update()
    {
        if(existanceTimer > 0)
        {
            if(existanceTimer <= laserTime){transform.Rotate(0,0,turnRate * Time.deltaTime);}
            existanceTimer -= Time.deltaTime;
        }else
        {
            Destroy(gameObject, 0.5f);
        }
    }
}
