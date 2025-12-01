using UnityEngine;

public class LargeCrabReworked : MonoBehaviour
{
    public float speed = 2f;
    public float switchTime = 2f;  // how long to walk before turning
    public float sightRange = 5f;  // how far crab can see player
    public bool chasePlayer = true;

    private float timer;
    private Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = switchTime;
    }

    void Update()
    {
        LookForPlayer();
        Move();
        timer += Time.deltaTime;
    }

    private void LookForPlayer()
    {
        Vector2 origin = transform.position;

        // Raycast right
        RaycastHit2D hitRight = Physics2D.Raycast(origin, Vector2.right, sightRange);
        Debug.DrawRay(origin, Vector2.right * sightRange, Color.red);

        // Raycast left
        RaycastHit2D hitLeft = Physics2D.Raycast(origin, Vector2.left, sightRange);
        Debug.DrawRay(origin, Vector2.left * sightRange, Color.blue);

        if (chasePlayer)
        {
            if (hitRight.collider != null && hitRight.collider.CompareTag("Player"))
            {
                speed = Mathf.Abs(speed); // move right
            }
            else if (hitLeft.collider != null && hitLeft.collider.CompareTag("Player"))
            {
                speed = -Mathf.Abs(speed); // move left
            }
            else
            {
                Wander();
            }
        }
        else
        {
            Wander();
        }
    }

    private void Move()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);

        // Flip sprite
        if (speed < 0) spriteRenderer.flipX = false;
        else if (speed > 0) spriteRenderer.flipX = true;
    }

    private void Wander()
    {
        if (timer >= switchTime)
        {
            speed = -speed;
            timer = 0;
        }
    }

    public void TakeDamage()
    {
        Debug.Log("Large crab hit!");
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<PlayerHealth>()?.TakeDamage(10);
        }
    }
}
