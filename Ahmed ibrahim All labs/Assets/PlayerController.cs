using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;          // how fast the character moves
    public float jumpHeight;         // how high the character jumps
    public KeyCode Spacebar;         // Jump key (Spacebar)
    public KeyCode L;                // Left movement key
    public KeyCode R;                // Right movement key
    public Transform groundCheck;    // an invisible point in space (used to detect ground)
    public float groundCheckRadius;  // radius around ground check
    public LayerMask whatIsGround;   // defines what is considered ground
    private bool grounded;           // checks if character is standing on solid ground
private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
       anim.SetFloat("height", GetComponent<Rigidbody2D>().velocity.y);
       anim.SetBool("ground", grounded);
       anim.SetFloat("speed",Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));

        if (Input.GetKeyDown(Spacebar) && grounded)
        {
            Jump(); // call jump function
        }

        // Move left
        if (Input.GetKey(L))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            // move player left without affecting jump

            if (GetComponent<SpriteRenderer>() != null)
            {
                GetComponent<SpriteRenderer>().flipX = true; // flip sprite to face left
            }
        }

        // Move right
        if (Input.GetKey(R))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            // move player right without affecting jump

            if(GetComponent<SpriteRenderer>()!= null)
            {
                GetComponent<SpriteRenderer>().flipX = false; // flip sprite to face right
            }
        }
    }

    void FixedUpdate()
    {
        // Check if player is on ground
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    void Jump()
    {
        // Make player jump vertically without affecting horizontal speed
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
    }
}