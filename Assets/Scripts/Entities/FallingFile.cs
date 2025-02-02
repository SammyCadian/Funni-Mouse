using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingFile : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject, 1f);
    }
}
