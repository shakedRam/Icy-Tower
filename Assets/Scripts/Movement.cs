using UnityEngine;

public class Movement : MonoBehaviour
{
    private float moveSpeed = 7f;
    private Rigidbody2D rb;
    private bool isGrounded;
    private float jumpForce = 8f;
    private float xVelocityThreshold = 6.5f;
    private Animator animator;
    private float boundryLeft, boundryRight;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        boundryLeft = -8.16f;
        boundryRight = 7.55f;
    }

    private void Update()
    {
        // Check for jump input.
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
        
        // Get the current position of the player
        Vector3 currentPosition = transform.position;

        // Restrict the X position within the specified range
        currentPosition.x = Mathf.Clamp(currentPosition.x, boundryLeft, boundryRight);

        // Apply the new position
        transform.position = currentPosition;
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 moveDirection = new Vector2(horizontalInput, 0f);
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
        
        if (Mathf.Abs(rb.velocity.x) < 0.5f) 
            animator.SetBool("IsRunning", false);
        else
            animator.SetBool("IsRunning", true);
        
        if (rb.velocity.y <= 0.05f)
            animator.SetBool("IsJumping", false);
        else
            animator.SetBool("IsJumping", true);
    }

    private void Jump()
    {
        if (Mathf.Abs(rb.velocity.x) > xVelocityThreshold)
            rb.AddForce(Vector2.up * (jumpForce + 5f), ForceMode2D.Impulse);
        else
            rb.AddForce(Vector2.up * (jumpForce), ForceMode2D.Impulse);
        
        isGrounded = false;
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}