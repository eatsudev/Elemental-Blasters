using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IUnit
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float jumpTime = 0.5f;
    [SerializeField] private float extraHeight = 0.25f;
    [SerializeField] private LayerMask whatIsGround;
    public Animator anim;
    private Rigidbody2D rb;
    private Collider2D coll;
    private float moveInput;
    private CheckPoint lastCheckPoint;


    private bool isFacingRight = true;
    private bool isJumping;
    private bool isFalling;
    private float jumpTimeCounter;
    private bool isMoving;
    private RaycastHit2D groundHit;

    private bool isSliding;
    [SerializeField] private float slideSpeed;
    [SerializeField] private float slideTime;
    private float slideTimer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        isSliding = false;
        slideTimer = slideTime;
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
        isMoving = Mathf.Abs(moveInput) > 0.1f;
        anim.SetFloat("Speed", Mathf.Abs(moveInput));

        if ((moveInput > 0 && !isFacingRight) || (moveInput < 0 && isFacingRight))
        {
            //Flip();
        }
    }

    public bool IsMoving()
    {
        return isMoving;
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
            anim.SetBool("Jumping", true);
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
            anim.SetBool("Jumping", false);
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

    /*private void Slide()
    {
        if (isSliding)
        {
            StartSlide();
        }
        else
        {
            isSliding = true;
        }
    }

    private void StartSlide()
    {
        Vector2 inputDirection = new Vector2 (UserInput.instance.moveInput.x, rb.velocity.y);

        rb.AddForce(inputDirection * slideSpeed, ForceMode2D.Impulse);

        slideTimer -= Time.deltaTime;

        if (slideTimer <= 0 || Mathf.Abs(rb.velocity.x) <= 0.1f)
        {
            stopSlide();
        }
    }
    private void stopSlide()
    {
        isSliding = false;
    }*/

    public void TakeDamage(float damage)
    {

    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
    public CheckPoint GetLastCheckPoint()
    {
        return lastCheckPoint;
    }
    public void SetCheckPoint(CheckPoint checkPoint)
    {
        lastCheckPoint = checkPoint;
    }
}