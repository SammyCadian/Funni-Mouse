using System.Collections;
using System.Collections.Generic;
// using System.Numerics;
using UnityEngine;
using UnityEngine.LowLevel;

public class PlayerAttackInstance : MonoBehaviour
{
    [SerializeField] private LayerMask hitmask;
    private RaycastHit2D[] hits;
    private PlayerMovement plyrMove;
    private float damage;
    private float existanceTimer;
    private bool facing;
    // Start is called before the first frame update
    void Start()
    {
        //existanceTimer = 0f;
        //plyrMove = GetComponent<PlayerMovement>
    }

    public void AttachMovement(PlayerMovement p, float dmg)
    {
        Destroy(gameObject, 0.5f);
        existanceTimer = 0f;
        damage = dmg;
        plyrMove = p;
        facing = false;
        //GetComponent<Animator>().Play("SwingLeft");
        // if(plyrMove.GetFacing()){GetComponent<Animator>().Play("SwingRight");}else{GetComponent<Animator>().Play("SwingRight");}
    }

    // Update is called once per frame
    void Update()
    {
        int offset = 1;
        if(plyrMove.GetFacing() != facing){transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, 1); facing = plyrMove.GetFacing(); offset *= -1;}
        transform.position = plyrMove.transform.position;
        if(existanceTimer <= 0.30f && existanceTimer >= 0.25f)
        {
            hits = Physics2D.CircleCastAll(transform.position + new Vector3(offset, 0, 0), 1f, transform.right, 0f, hitmask);
            for (int i = 0; i < hits.Length; i++)
            {
                hits[i].transform.GetComponent<EnemyHealth>().TakeDamage(damage);
            }
        }
        existanceTimer += Time.deltaTime;
    }
    // private void OnCollisionEnter(Collision other) {
    //     // Debug.Log("hi");
    //     if(other.gameObject.layer == 12)
    //     {
    //         gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
    //     }
    // }
}
