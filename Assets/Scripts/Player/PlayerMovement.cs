using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpStrength;
    [SerializeField] private float dashStrength;
    [SerializeField] private KeyCode dashKey;

    private bool facingRight;
    private bool dashing;
    private bool isJumping;
    private Rigidbody2D rb;
    private Animator anim;
    private float dashTimer;

    public bool GetFacing(){return facingRight;}
    public Vector2 GetVelocity(){return rb.velocity;}
    public string GetAnimState()
    {
        // Debug.Log("hi");
        string output = "";
        // Debug.Log(output + "Idle");
        //Add General state to the string
        if(!isJumping){output += "Idle";}else
        if(rb.velocity.y > 0){output += "Airborne";}else{output += "Falling";}

        //Add Direction to the string
        if(facingRight){output += "Right";}else
        {output += "Left";}

        // Debug.Log(output);
        return output;
    }
    public void Rewind(Vector2 velo, Vector3 pos, bool facing, string animState)
    {
        rb.velocity = velo;
        transform.position = pos;
        facingRight = facing;
        anim.Play(animState);
        dashing = true;
        dashTimer = 0.5f;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        facingRight = true;
        isJumping = false;
        dashing = false;
        dashTimer = 0;
        rb = GetComponent<Rigidbody2D>();
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isJumping){anim.SetFloat("VerticalSpeed", rb.velocity.y);}
        // Debug.Log(facingRight);
        if(!dashing)
        {
            ProcessMove();
            JumpingInput();
            DashInput();
        }else if(dashTimer > 0)
        {
            dashTimer -= Time.deltaTime;
        }else
        {
            dashing = false;
        }
    }
    private void DashInput()
    {
        if (Input.GetKeyDown(dashKey))
        {
            // Debug.Log("pop");
            float dir = 1;
            dashing = true;
            dashTimer = 0.15f;
            if(facingRight){dir = 1;}else{dir = -1;}
            rb.AddForce(dashStrength * 10f * dir * Vector2.right, ForceMode2D.Impulse);
        }
    }
    private void JumpingInput()
    {
        if (!isJumping && Input.GetAxis("Vertical") > 0)
        {
            isJumping = true;
            anim.SetBool("Grounded", !isJumping);
            if(facingRight){anim.Play("JumpingRight");}else{anim.Play("JumpingLeft");}
            // Debug.Log(Input.GetAxis("Vertical"));
            rb.velocity = new Vector2(rb.velocity.x, jumpStrength);
            // rb.AddForce(dashStrength * Vector2.up, ForceMode2D.Impulse);
        }
    }
    private Vector2 RunningInput()
    {
        Vector2 final = new Vector2(Input.GetAxis("Horizontal"), 0);
        anim.SetFloat("HorizontalInput", Input.GetAxis("Horizontal"));
        if ((final.x > 0 && !facingRight) || (final.x < 0 && facingRight))
        {
            facingRight = !facingRight;
            anim.SetBool("FacingRight", facingRight);
        }
        // Debug.Log(Input.GetAxis("Horizontal"));
        return final;
    }
    private void ProcessMove()
    {
        // rb.AddForce(moveSpeed * RunningInput() * Time.deltaTime, ForceMode2D.Force);
        // rb.velocity.Set(InputManager() * moveSpeed, rb.velocity.y);
        rb.velocity = RunningInput() * moveSpeed + Vector2.up * rb.velocity.y;
    }
    // private void flip
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.layer == 14 && rb.velocity.y < 1)
        {
            isJumping = false;
            anim.SetBool("Grounded", !isJumping);
        }
    }
}
