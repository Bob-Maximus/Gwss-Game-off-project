using NUnit.Framework.Constraints;
using UnityEngine;

public class LargeCrabRewoked : MonoBehaviour
{
   public float speed = 2f;
    public float switchTime = 2f; // how long to walk before turning

    private float timer;
    private int direction = 1; // 1 = right, -1 = left

    private Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;

    public bool chasePlayer;

    public float ssightRange;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = switchTime;
    }

    void Update()
    {
        LookForPlayer();
        Move();
        // Countdown timer
        timer += Time.deltaTime;
    }

    private void LookForPlayer()
    {
        RaycastHit2D rayhit = Physics2D.Raycast(transform.position, new Vector3(transform.forward.z,transform.forward.y, 0), ssightRange);
        RaycastHit2D rayhit2 = Physics2D.Raycast(transform.position, new Vector3(-transform.forward.z,transform.forward.y, 0), ssightRange);
        if (rayhit2.collider != null && rayhit2.collider.gameObject.tag == "Player"&& chasePlayer)
        {
            Attack(rayhit2.collider.transform.position);
        }
        else if (rayhit.collider != null &&rayhit.collider.gameObject.tag == "Player" && chasePlayer)
        {
            Attack(rayhit.collider.transform.position);
        } else 
        {
            Wander();
        }
    }

    private void Move()
    {
        rb.velocityX = speed;

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

    private void Attack(Vector2 playerPos)
    {
        if (transform.position.x > playerPos.x)
        {
            speed = -Mathf.Abs(speed);
        } else
        {
            speed = Mathf.Abs(speed);
        }
    }
}
