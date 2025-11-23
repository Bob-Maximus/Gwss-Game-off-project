using UnityEngine;
//check heavly before merge
//reworked everything
public class PlayerControllerLayerCheck : MonoBehaviour
{
    public float runSpeed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;

    private bool facingRight = true;

    public Transform groundCheck;
    private bool isGrounded;

    public Transform rightGroundCheck;
    private bool isRightGrounded;

    public Transform leftGroundCheck;
    private bool isLeftGrounded;

    public float groundCheckRadius = 0.2f;

    public LayerMask whatIsGround;
    public GameObject wavePrefab;
    public Transform firePoint; // where the wave spawns


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
                WallJump(true);
            } else if (isRightGrounded)
            {
                WallJump(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            Shoot();

            Debug.Log("Shoot");
        }
        

    }


    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");

        // Smooth horizontal velocity
        float targetVelX = moveX * runSpeed;

    
        float smoothedX = Mathf.Lerp(rb.velocity.x, targetVelX, 0.2f);

        // Apply new velocity while keeping vertical velocity
        rb.velocity = new Vector2(smoothedX, rb.velocity.y);

        // Flip sprite
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

    void WallJump(bool left)
    {
        if (left)
        {
            rb.AddForce(new Vector2(jumpForce, jumpForce), ForceMode2D.Impulse);
            transform.position = new Vector2(transform.position.x + 1, transform.position.y);
        }
        else
        {
            rb.AddForce(new Vector2(-jumpForce, jumpForce), ForceMode2D.Impulse);
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

    void Shoot()
    {
        GameObject wave = Instantiate(wavePrefab, firePoint.position, Quaternion.identity);

        WaveProjectile w = wave.GetComponent<WaveProjectile>();

        int dir = facingRight ? 1 : -1;
        w.SetDirection(dir);
    }




}
