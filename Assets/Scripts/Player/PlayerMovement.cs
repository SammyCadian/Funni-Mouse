using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpStrength;
    [SerializeField] private float dashStrength;
    [SerializeField] private KeyCode dashKey;

    private bool facingRight;
    private bool pauseInput;
    private bool isJumping;
    private bool rewinding;
    private Rigidbody2D rb;
    private Animator anim;
    private float pauseInputTimer;

    public bool IsRewinding(){return rewinding;}
    public bool GetFacing(){return facingRight;}
    public Vector2 GetVelocity(){return rb.velocity;}
    public string GetAnimState()
    {
        string output = "";

        //Add General state to the string
        if(!isJumping){output += "Idle";}else
        if(rb.velocity.y > 0){output += "Airborne";}else{output += "Falling";}

        //Add Direction to the string
        if(facingRight){output += "Right";}else
        {output += "Left";}

        return output;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        facingRight = true;
        isJumping = false;
        pauseInput = false;
        pauseInputTimer = 0;
        rb = GetComponent<Rigidbody2D>();
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("VerticalSpeed", rb.velocity.y);
        // Debug.Log(facingRight);
        if(!pauseInput)
        {
            ProcessMove();
            JumpingInput();
            DashInput();
        }else if(pauseInputTimer > 0)
        {
            pauseInputTimer -= Time.deltaTime;
        }else
        {
            pauseInput = false;
        }
    }
    public void PrayingStart(){if(facingRight){anim.Play("PrayStartRight");}else{anim.Play("PrayStartLeft");}}
    public void PrayingEnd(){if(facingRight){anim.Play("IdleLeft");}else{anim.Play("IdleRight");}}
    public void PrayingContinue(){pauseInputTimer = 0.2f; pauseInput = true;}
    public void RewindOneNode(PlayerEcho l)
    {
        // rb.velocity = l.GetVelocity();
        transform.position = l.transform.position;
        facingRight = l.GetFacing();
        string stateAtNode = l.GetState();
        // Debug.Log(stateAtNode);
        anim.Play(stateAtNode);
    }
    public void RewindFinal(PlayerEcho l)
    {
        rb.velocity = l.GetVelocity();
        transform.position = l.transform.position;
        facingRight = l.GetFacing();
        anim.Play(l.GetState());
        EndRewind();
    }
    public void StartRewind()
    {
        rewinding = true;
        pauseInputTimer = 5 * 0.1f + 0.5f;
        pauseInput = true;
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
    }
    public void EndRewind()
    {
        rewinding = false;
        // pauseInput = false;
        rb.isKinematic = false;
    }
    private void DashInput()
    {
        if (Input.GetKeyDown(dashKey))
        {
            // Debug.Log("pop");s
            float dir = 1;
            pauseInput = true;
            pauseInputTimer = 0.15f;
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
