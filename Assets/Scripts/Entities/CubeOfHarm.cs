using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeOfHarm : MonoBehaviour
{
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
    public void ExistanceCheck(){Debug.Log("yo, cube here");}
}
