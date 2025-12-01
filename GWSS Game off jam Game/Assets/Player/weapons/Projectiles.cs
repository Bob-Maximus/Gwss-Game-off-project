using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public float speed;
    public float lifeTime;    
    private float timer = 0;

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocityX = speed;

        timer += Time.deltaTime;

        if (timer >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.collider.CompareTag("Enemy"))
        {
            col.collider.GetComponent<LargeCrabReworked>()?.TakeDamage();
            Debug.Log("Hit enemy");
        }

        
        BossCrab boss = col.collider.GetComponent<BossCrab>();
        if (boss != null)
        {
            boss.TakeDamage(10); 
            Debug.Log("Hit boss");
        }
    }


}
