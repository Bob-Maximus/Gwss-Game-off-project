using UnityEngine;

public class WaveProjectile : MonoBehaviour
{
    public float speed = 8f;
    public float lifetime = 2f;

    private int direction = 1;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    public void SetDirection(int dir)
    {
        direction = dir;
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * dir;
        transform.localScale = scale;
    }

    void Update()
    {
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.CompareTag("Player")) return; // don't destroy when touching player

        Destroy(gameObject);
    }

}
