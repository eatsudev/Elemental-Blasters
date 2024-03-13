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
    private PlayerAimAndShoot playerAimAndShoot;
    private float moveInput;
    private bool isFacingRight = true;
    private bool isMoving; // New variable to track movement state

    private bool isJumping;
    private float jumpTimeCounter;
    private float direction;

    private RaycastHit2D groundHit;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        playerAimAndShoot = GetComponent<PlayerAimAndShoot>();
        
    }

    private void Update()
    {
        if (playerAimAndShoot.IsAiming())
        {

            
        }
        else
        {
            Move();
            Jump();
        }
    }

    private void Move()
    {
        moveInput = UserInput.instance.moveInput.x;
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Update isMoving based on the movement input
        isMoving = Mathf.Abs(moveInput) > 0.1f;

        direction = moveInput;
    }

    public bool IsMoving()
    {
        return isMoving;
    }

    public float PlayerMoveDirection()
    {
        return direction;
    }
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
            if (jumpTimeCounter > 0 && isJumping)
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

        if (groundHit.collider != null)
        {
            return true;
        }

        else
        {
            return false;
        }
    }
}