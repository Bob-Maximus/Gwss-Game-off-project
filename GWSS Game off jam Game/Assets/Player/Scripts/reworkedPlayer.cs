using UnityEngine;
//check heavly before merge
//reworked everything
public class PlayerControllerBetter : MonoBehaviour
{
    public float runSpeed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;

    public bool facingRight = true;

    public Transform groundCheck;
    public Transform rightGroundCheck;
    public Transform leftGroundCheck;
    public float groundCheckRadius = 0.2f;

    public LayerMask whatIsGround;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        bool isLeftGrounded = Physics2D.OverlapCircle(leftGroundCheck.position, groundCheckRadius, whatIsGround);
        bool isRightGrounded = Physics2D.OverlapCircle(rightGroundCheck.position, groundCheckRadius, whatIsGround);

        if (isLeftGrounded || isRightGrounded)
        {
            rb.velocityY = 0;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                Jump();
            } else if (isLeftGrounded)
            {
                WallJump();
            } else if (isRightGrounded)
            {
                WallJump();
            }
        }
    }


    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(moveX * runSpeed, rb.velocity.y);

        if (moveX > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveX < 0 && facingRight)
        {
            Flip();
        }
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }

    void WallJump()
    {
        if (!facingRight)
        {
            rb.AddForce(new Vector2(jumpForce*4, jumpForce), ForceMode2D.Impulse);
            transform.position = new Vector2(transform.position.x + 1, transform.position.y);
        }
        else
        {
            rb.AddForce(new Vector2(-jumpForce*4, jumpForce), ForceMode2D.Impulse);
            transform.position = new Vector2(transform.position.x - 1, transform.position.y);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        GetComponent<SpriteRenderer>().flipX = true;
        /*
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
        */
    }
}
