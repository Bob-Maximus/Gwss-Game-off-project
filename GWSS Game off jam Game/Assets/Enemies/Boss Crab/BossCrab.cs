using UnityEngine;

public class BossCrab : MonoBehaviour
{
    public float speed = 2f;
    public float switchTime = 2f;
    public float sightRange = 5f;
    public bool chasePlayer = true;

    public GameObject largeCrabPrefab; // the small crabs to spawn
    public float spawnInterval = 5f;

    private float timer;
    private float spawnTimer;
    private Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = switchTime;
        spawnTimer = spawnInterval;
    }

    void Update()
    {
        LookForPlayer();
        Move();
        timer += Time.deltaTime;

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnCrab();
            spawnTimer = 0;
        }
    }

    private void LookForPlayer()
    {
        // Offset downward
        Vector2 origin = (Vector2)transform.position + Vector2.down * 2f;

        RaycastHit2D hitRight = Physics2D.Raycast(origin, Vector2.right, sightRange);
        Debug.DrawRay(origin, Vector2.right * sightRange, Color.red);

        RaycastHit2D hitLeft = Physics2D.Raycast(origin, Vector2.left, sightRange);
        Debug.DrawRay(origin, Vector2.left * sightRange, Color.blue);

        if (chasePlayer)
        {
            if (hitRight.collider != null && hitRight.collider.CompareTag("Player"))
                speed = Mathf.Abs(speed);
            else if (hitLeft.collider != null && hitLeft.collider.CompareTag("Player"))
                speed = -Mathf.Abs(speed);
            else
                Wander();
        }
        else
        {
            Wander();
        }
    }


    private void Move()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
        spriteRenderer.flipX = speed > 0;
    }

    private void Wander()
    {
        if (timer >= switchTime)
        {
            speed = -speed;
            timer = 0;
        }
    }

    private void SpawnCrab()
    {
        float horizontalOffset = 5f;  // distance in front of boss
        float verticalOffset = 0.5f;  // small offset so it doesn't spawn inside

        Vector3 spawnPos = transform.position + new Vector3(speed > 0 ? horizontalOffset : -horizontalOffset, verticalOffset, 0);
        GameObject crab = Instantiate(largeCrabPrefab, spawnPos, Quaternion.identity);

        // Set crab speed to match the direction of the boss
        LargeCrabReworked crabScript = crab.GetComponent<LargeCrabReworked>();
        if (crabScript != null)
        {
            crabScript.speed = -speed > 0 ? Mathf.Abs(crabScript.speed) : -Mathf.Abs(crabScript.speed);
        }
    }


    public void TakeDamage()
    {
        Debug.Log("Boss crab hit!");
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
            collision.collider.GetComponent<PlayerHealth>()?.TakeDamage();
    }
}
