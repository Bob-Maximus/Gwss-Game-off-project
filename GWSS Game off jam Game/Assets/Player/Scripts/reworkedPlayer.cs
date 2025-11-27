using UnityEngine;

public class PlayerControllerLayerCheck : MonoBehaviour
{
    public float runSpeed = 5f;
    public float jumpForce = 10f;
    public float wallCheckDistance = 0.5f;

    private Rigidbody2D rb;

    private bool facingRight = true;
    private bool isWallSliding;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask whatIsGround;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        RaycastHit2D wallCheckHit = Physics2D.Raycast(
          transform.position,
          new Vector2(facingRight ? 1 : -1, 0),
          wallCheckDistance,
          whatIsGround
        );

        bool isWallContact = wallCheckHit.collider != null && !isGrounded;

        if (isWallContact)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                Jump(jumpForce);
            }
        }

        Debug.Log("Grounded: " + isGrounded + " | Wall Sliding: " + isWallSliding);
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");

        float targetVelX = moveX * runSpeed;
        float smoothedX = Mathf.Lerp(rb.velocity.x, targetVelX, 0.2f);

        rb.velocity = new Vector2(smoothedX, rb.velocity.y);

        if (moveX > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveX < 0 && facingRight)
        {
            Flip();
        }
    }

    void Jump(float force)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(new Vector2(0f, force), ForceMode2D.Impulse);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}