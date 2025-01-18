using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEcho : MonoBehaviour
{
    private Vector2 eVelocity;
    private string state;
    private bool facingRight;
    private Animator anim;
    // Start is called before the first frame update
    public void SetValues(Vector2 x, Vector3 pos, bool facing, string animState)
    {
        eVelocity = x;
        transform.position = pos;
        state = animState;
        facingRight = facing;
        // Debug.Log(state);
        anim.Play(animState);
    }
    public Vector2 GetVelocity(){return eVelocity;}
    public string GetState(){return state;}
    public bool GetFacing(){return facingRight;}
    void Start()
    {
        anim = GetComponent<Animator>();
        state = "FallingRight";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
