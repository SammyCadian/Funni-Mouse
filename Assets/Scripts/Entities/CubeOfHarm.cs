using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeOfHarm : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // private void OnCollisionEnter2D(Collision2D other) {
    //     if(other.gameObject.layer == 9)
    //     {
    //         other.gameObject.GetComponent<PlayerHealth>().GetHit();
    //     }
    // }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == 9)
        {
            other.gameObject.GetComponent<PlayerHealth>().GetHit();
        }
    }
}
