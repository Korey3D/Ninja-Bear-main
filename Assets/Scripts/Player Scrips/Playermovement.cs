using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System;

public class Playermovement : MonoBehaviour
{
    private BoxCollider2D playerHitBox;
    private Rigidbody2D rb;
    private SpriteRenderer spi;
    private Animator anim;
    private int jumpNumber;
    private bool facingLeft;
    bool isGrounded = false;
    float Mx;
    
   

    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(8f, 16f);

    public float ccoyoteTime = 0.2f;
    private float coyoteTimeCounter;
   

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float movementSpeed = 4;
    [SerializeField] private float jumpForce = 20;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private LayerMask jumpableGround;

    [SerializeField] private Transform wallCheckPoint;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] float wallSlideSpeed = 0;
    [SerializeField] Vector2 wallCheckSize;


    [Header("WAll Jump")]
    public float wallJumpTime = 0.2f;
    public float wallslidespeed = 0.3f;
    public float wallDistance = 0.5f;
    bool isWallSliding = false;
    RaycastHit2D WallCheckHit;
    float jumpTime;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spi = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        //playerHitBox = GetComponent<BoxCollider2D>();
    }

   
    private void Update()
    {

        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * movementSpeed, rb.velocity.y);//Movement

        if (IsGrounded())
        {
            coyoteTimeCounter = ccoyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && jumpNumber < 2)
        {
            jumpSound.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);//Jump
            jumpNumber++;
        }

        
      

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }


        if (rb.velocity.x < 0f && !facingLeft)
        {
            spi.flipX = true;
            facingLeft = true;
            anim.SetBool("Running", true);
            anim.SetBool("Idle", false);
            anim.SetBool("Falling", false);
            anim.SetBool("Jumping", false);
        }

        if (rb.velocity.x > 0f && facingLeft)
        {
            spi.flipX = false;
            facingLeft = false;
            anim.SetBool("Running", true);
            anim.SetBool("Idle", false);
            anim.SetBool("Falling", false);
            anim.SetBool("Jumping", false);
        }

        //brute force if statement for right running w/out flipping
        if (rb.velocity.x > 0f && !facingLeft)
        {
            anim.SetBool("Running", true);
            anim.SetBool("Idle", false);
            anim.SetBool("Falling", false);
            anim.SetBool("Jumping", false);
        }

        //brute force if statement for left running w/out flipping
        if (rb.velocity.x < 0f && facingLeft)
        {
            anim.SetBool("Running", true);
            anim.SetBool("Idle", false);
            anim.SetBool("Falling", false);
            anim.SetBool("Jumping", false);
        }

        if (rb.velocity.x == 0f)
        {
            anim.SetBool("Idle", true);
            anim.SetBool("Running", false);
            anim.SetBool("Falling", false);
            anim.SetBool("Jumping", false);
        }

        if (rb.velocity.y > .5f)
        {
            anim.SetBool("Jumping", true);
            anim.SetBool("Idle", false);
            anim.SetBool("Falling", false);
            anim.SetBool("Running", false);
        }

        if (rb.velocity.y < -1f)
        {
            anim.SetBool("Falling", true);
            anim.SetBool("Idle", false);
            anim.SetBool("Jumping", false);
            anim.SetBool("Running", false);
        }

    }

    void FixedUpdate()
    {
        if (facingLeft)
        {
            WallCheckHit = Physics2D.Raycast(transform.position, new Vector2(-wallDistance, 0), wallDistance, groundLayer);
            Debug.DrawRay(transform.position, new Vector2(-wallDistance, 0), Color.blue);
        }
        else
        {
            WallCheckHit = Physics2D.Raycast(transform.position, new Vector2(wallDistance, 0), wallDistance, groundLayer);
            Debug.DrawRay(transform.position, new Vector2(wallDistance, 0), Color.blue);
        }


        if (WallCheckHit && !isGrounded)
        {
            isWallSliding = true;
            jumpTime = Time.time + wallJumpTime;
        }
        else if (jumpTime < Time.time)
        {
            isWallSliding = false;
        }

        if (isWallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, wallslidespeed, float.MaxValue));
        }
    }




    // This line of code makes Player able to Jump
    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    //private bool IsGrounded()
    //{
    //   return Physics2D.BoxCast(playerHitBox.bounds.center, playerHitBox.bounds.size, 0f, Vector2.down, -1f, jumpableGround);

    //}


       

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Test"))
        {
            jumpNumber = 0;
        }
    }

}