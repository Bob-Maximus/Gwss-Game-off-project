using UnityEngine;

public class LargeCrabMovement : MonoBehaviour
{
    public float speed = 2f;
    public float switchTime = 2f; // how long to walk before turning

    private float timer;
    private int direction = 1; // 1 = right, -1 = left

    private Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;

    // GroundChecks
    public GroundCheck frontCheck; // detects walls
    public GroundCheck downCheck;  // detects floor or drop-off

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = switchTime;
    }

    void Update()
    {
        // Countdown timer
        timer -= Time.deltaTime;

        // Check environment
        bool hitWall = frontCheck.grounded;   // front touches a wall
        bool noGround = !downCheck.grounded;  // nothing under crab -> drop

        // Turn around if any condition triggers
        if (timer <= 0f || hitWall || noGround)
        {
            direction *= -1;   // reverse direction
            timer = switchTime;
        }

        // Flip sprite
        spriteRenderer.flipX = direction > 0;
    }

    void FixedUpdate()
    {
        // Smooth horizontal movement
        float targetX = direction * speed;
        float smoothedX = Mathf.Lerp(rb.velocity.x, targetX, 0.2f);
        rb.velocity = new Vector2(smoothedX, rb.velocity.y);
    }
}
