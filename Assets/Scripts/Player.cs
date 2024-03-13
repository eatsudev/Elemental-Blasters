using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float jumpTime = 0.5f;
    [SerializeField] private float extraHeight = 0.25f;
    [SerializeField] private LayerMask whatIsGround;
    private Rigidbody2D rb;
    private Collider2D coll;
    private float moveInput;
    private bool isFacingRight = true;

    private bool isJumping;
    private bool isFalling;
    private float jumpTimeCounter;

    private RaycastHit2D groundHit;

    private void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    private void Update()
    {
        Move();
        Jump();
    }

    

    private void Move()
    {
        moveInput = UserInput.instance.moveInput.x;
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if ((moveInput > 0 && !isFacingRight) || (moveInput < 0 && isFacingRight))
        {
            //Flip();
        }
    }

    /*private void Flip()
    {
        // Switch the direction the player is facing
        isFacingRight = !isFacingRight;

        // Flip the player's scale along the X-axis
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }*/

    public bool PlayerFacingRight()
    {
        if (transform.localScale.x > 0) { isFacingRight = true; }
        else { isFacingRight = false; }

        return isFacingRight;
    }

    private void Jump()
    {
        if (UserInput.instance.controls.jumping.Jump.WasPressedThisFrame() && IsGrounded())
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (UserInput.instance.controls.jumping.Jump.IsPressed())
        {
            if(jumpTimeCounter > 0 && isJumping)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (UserInput.instance.controls.jumping.Jump.WasReleasedThisFrame())
        {
            isJumping = false;
        }
    }

    private bool IsGrounded()
    {
        groundHit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, extraHeight, whatIsGround);

        if(groundHit.collider != null)
        {
            return true;
        }

        else
        {
            return false;
        }
    }
}
