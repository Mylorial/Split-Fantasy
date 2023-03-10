using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float wallJumpForce = 10f;
    [SerializeField] private float wallSlideSpeed = 1.5f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance = 0.5f; // add this variable to control the distance of the wall check

// ...

    private Rigidbody2D rb;
    private int lives;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool canWallJump;
    private Vector2 wallJumpDirection;
    private bool isFacingRight = true;

    private float horizontal;
    private float moveSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = speed;
    }

private void Update()
{
    isGrounded = IsGrounded();
    isTouchingWall = IsTouchingWall();
    canWallJump = isTouchingWall && !isGrounded;

    // Reset jump ability if currently wall sliding
    if (!canWallJump && rb.velocity.y < 0f)
    {
        rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
    }

    if (canWallJump)
    {
        wallJumpDirection = new Vector2(Input.GetAxis("Horizontal") == -1f ? 1f : -1f, 1f).normalized;
    }

    if (Input.GetKeyDown(KeyCode.Space))
    {
        if (isGrounded)
        {
            Jump(Vector2.up);
        }
        else if (canWallJump)
        {
            Jump(wallJumpDirection);
        }
    }
}

private void FixedUpdate()
{
    horizontal = Input.GetAxis("Horizontal");

    Flip();

    if (canWallJump)
    {
        rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
    }
    else
    {
        // Check if player is touching wall and moving towards it
        if (isTouchingWall && Mathf.Sign(horizontal) == Mathf.Sign(transform.localScale.x))
        {
            // Set horizontal velocity to 0 to prevent sliding down wall
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
        }
    }
}

private void Jump(Vector2 direction)
{
    rb.velocity = new Vector2(rb.velocity.x, 0f);

    // Ensure player is not wall sliding when they jump
    if (rb.velocity.y < 0f)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
    }

    rb.AddForce(direction * (isGrounded ? jumpForce : wallJumpForce), ForceMode2D.Impulse);
}

    private bool IsGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.1f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                return true;
            }
        }
        return false;

        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

private bool IsTouchingWall()
{
    Collider2D[] colliders = Physics2D.OverlapCircleAll(wallCheck.position + new Vector3(wallCheckDistance * transform.localScale.x, 0f, 0f), 0.5f);
    foreach (Collider2D collider in colliders)
    {
        if (collider.gameObject != gameObject)
        {
            return true;
        }
    }
    return false;
}

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
