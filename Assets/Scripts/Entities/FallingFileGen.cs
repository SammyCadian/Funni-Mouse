using System.Collections;
using System.Collections.Generic;
// using System.Numerics;
using UnityEngine;

public class FallingFileGen : MonoBehaviour
{
    [SerializeField] private float maxOriginDistance;
    [SerializeField] private int amount;
    [SerializeField] private GameObject fallingFile;
    private float existanceTimer;
    private int remainingAmount;
    // Start is called before the first frame update
    void Start()
    {
        remainingAmount = amount;
        existanceTimer = amount * 0.1f + 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if(existanceTimer < remainingAmount * 0.1f)
        {
            Instantiate(fallingFile, transform.position + new Vector3(Random.Range(-maxOriginDistance, maxOriginDistance), 0, 0), transform.rotation);
            remainingAmount -= 1;
        }
        existanceTimer -= Time.deltaTime;
        if(existanceTimer <= 0){Destroy(gameObject);}
    }
}
