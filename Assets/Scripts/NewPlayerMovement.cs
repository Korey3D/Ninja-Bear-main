using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{
    private float horizontal;
    public float speed = 8f;
    public float jumpingPower = 10f;
    private bool isFacingRight = true;
    private SpriteRenderer spi;
    private Animator anim;
    private bool facingLeft;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private AudioSource jumpSound;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spi = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //This is to get the Horizontal input 
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        //Flip();


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


    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y); 
    }


    // This line of code makes Player able to Jump
    private bool IsGounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    // This line of code flips player in the right direction
    //private void Flip()
    //{
        //if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
       // {
            //isFacingRight = !isFacingRight;
            //Vector3 localScale = transform.localScale;
           // localScale.x *= -1f;
           // transform.localScale = localScale;
       // }
    //}
}
