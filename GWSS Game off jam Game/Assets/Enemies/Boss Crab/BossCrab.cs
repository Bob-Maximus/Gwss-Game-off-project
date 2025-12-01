using UnityEngine;
using UnityEngine.UI;

public class BossCrab : MonoBehaviour
{
    public float speed = 2f;
    public float switchTime = 2f;
    public float sightRange = 5f;
    public bool chasePlayer = true;

    public GameObject largeCrabPrefab;
    public float spawnInterval = 5f;

    private float timer;
    private float spawnTimer;
    private Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;

    public bool hasSeenPlayer = false;
    public Transform player;
    public float detectDistance = 10f;

    
    public int maxHealth = 100;
    public int currentHealth;
    public Image healthBarFill; // UI bar

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = switchTime;
        spawnTimer = spawnInterval;

        currentHealth = maxHealth;
        if (healthBarFill != null)
            healthBarFill.fillAmount = 1f;
    }

    void Update()
    {
        // ⭐ NEW — detect player once
        if (!hasSeenPlayer)
        {
            float dist = Vector2.Distance(transform.position, player.position);
            if (dist <= detectDistance)
            {
                hasSeenPlayer = true;
                Debug.Log("Boss has seen the player for the first time — spawning activated!");
            }
        }

        LookForPlayer();
        Move();
        timer += Time.deltaTime;

        // ⭐ NEW — only spawn if boss has seen player
        if (hasSeenPlayer)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnInterval)
            {
                SpawnCrab();
                spawnTimer = 0;
            }
        }
    }

    private void LookForPlayer()
    {
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
        float horizontalOffset = 5f;
        float verticalOffset = 0.5f;

        Vector3 spawnPos = transform.position +
            new Vector3(speed > 0 ? horizontalOffset : -horizontalOffset, verticalOffset, 0);

        Instantiate(largeCrabPrefab, spawnPos, Quaternion.identity);
    }

    // ⭐ HEALTH DAMAGE
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0)
            currentHealth = 0;

        // update bar
        if (healthBarFill != null)
            healthBarFill.fillAmount = (float)currentHealth / maxHealth;

        Debug.Log("Boss crab hit! Health: " + currentHealth);

        if (currentHealth == 0)
            Die();
    }

    private void Die()
    {
        Debug.Log("Boss crab defeated!");
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
            collision.collider.GetComponent<PlayerHealth>()?.TakeDamage();
    }
}
