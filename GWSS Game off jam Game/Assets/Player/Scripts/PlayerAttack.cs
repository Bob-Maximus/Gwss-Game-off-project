using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject projectile;
    public string key;
    public float speed;
    public float lifeTime;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            var sentProjectile = Instantiate(projectile);
            sentProjectile.transform.position = transform.position;
            sentProjectile.GetComponent<Projectiles>().lifeTime = lifeTime;
            if (GetComponent<PlayerControllerBetter>().facingRight)
            {
                sentProjectile.GetComponent<Projectiles>().speed = speed;
            } else
            {
                sentProjectile.GetComponent<Projectiles>().speed = -speed;
            }
        }
    }
}
