using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class EnemyFist : MonoBehaviour
{
    private Transform target;
    [SerializeField] private float raiseTime;
    [SerializeField] private Vector3 punchPos;
    private Vector3 distancePerSecond;
    private Vector2 startPos;
    private float existanceTimer;
    private float totalTime;
    private bool attacking;
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<BoxCollider2D>().enabled = false;
        startPos = transform.position;
        distancePerSecond = (punchPos - transform.position)/raiseTime;
        existanceTimer = 0f;
        totalTime = raiseTime + 1f + 2f;
    }

    public void SetTarget(Transform x){target = x;}

    // Update is called once per frame
    void Update()
    {
        if(existanceTimer < raiseTime)
        {
            transform.position += distancePerSecond * Time.deltaTime;
            transform.right = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
        }else if(existanceTimer < raiseTime + 1f)
        {
            transform.right = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
        }else if(existanceTimer < totalTime)
        {
            GetComponentInChildren<BoxCollider2D>().enabled = true;
            transform.position += 12f*transform.right * Time.deltaTime;
        }else
        {
            Destroy(gameObject);
        }
        existanceTimer += Time.deltaTime;
    }
}
