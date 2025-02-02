using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrayingCursor : MonoBehaviour
{
    [SerializeField] private Vector2 offset;
    private Transform followPos;
    private bool ending;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().Play("CursorLoading");
        ending = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!ending)
        {
            int a;
            if(followPos.GetComponent<PlayerMovement>().GetFacing()){a = 1;}else{a = -1;}
            transform.position = followPos.position + (new Vector3(offset.x * a, offset.y, 0))/2;
        }
    }
    public void SetTracking(Transform x){followPos = x;}
    public void FinishPraying(){Destroy(gameObject, 0.5f); GetComponent<Animator>().Play("CursorEnd"); ending = true;}
}
