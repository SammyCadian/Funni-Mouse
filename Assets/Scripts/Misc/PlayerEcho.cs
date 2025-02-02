using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEcho : MonoBehaviour
{
    private Vector2 eVelocity;
    private string sta;
    private bool facingRight;
    // private Animator anim;
    // Start is called before the first frame update
    public void SetValues(Vector2 x, Vector3 pos, bool facing, string animState)
    {
        eVelocity = x;
        transform.position = pos;
        sta = animState;
        facingRight = facing;
        // Debug.Log(sta);
        GetComponent<Animator>().Play(animState);
        // anim.Play(animState);
    }
    public void SetValues(Vector2 x, Vector3 pos, bool facing)
    {
        eVelocity = x;
        transform.position = pos;
        // sta = animState;
        facingRight = facing;
        // Debug.Log(sta);

    }
    public void SetState(string animState)
    {
        sta = animState;
        // Debug.Log(sta);
        // anim.Play();
    }
    public Vector2 GetVelocity(){return eVelocity;}
    public string GetState(){return sta;}
    public bool GetFacing(){return facingRight;}
    void Start()
    {
        // anim = GetComponent<Animator>();
        // sta = "FallingRight";
        // anim.Play(sta);
        // GetComponent<Animator>().Play(sta);
    }

    private void PlayTheThing()
    {
        // anim.Play("FallingRight");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
