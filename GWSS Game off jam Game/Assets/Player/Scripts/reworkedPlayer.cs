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
    public bool isGrounded;
    public Transform rightGroundCheck;
    public Transform leftGroundCheck;
    public float groundCheckRadius = 0.1f;

    public SpriteRenderer sprite;
    public Animator anim;

    public LayerMask whatIsGround;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
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

        if (!isGrounded)
        {
            if (!isLeftGrounded && !isRightGrounded)
            {
                anim.Play("falling");
            } else
            {
                anim.Play("climbing");
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

        if (rb.velocityX != 0 && isGrounded)
        {
            anim.Play("Walk");
        } else if (rb.velocityX == 0 && isGrounded && !anim.GetCurrentAnimatorStateInfo(0).IsName("casting"))
        {
            anim.Play("Idle");
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
        sprite.flipX = !sprite.flipX;
        /*
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
        */
    }
}
