using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public List<ProjectileData> attacks;

    // Update is called once per frame
    void Update()
    {
        foreach (ProjectileData attack in attacks)
        {
            if (Input.GetKeyDown(attack.key))
            {
                var sentProjectile = Instantiate(attack.projectile);
                sentProjectile.transform.position = new Vector2(transform.position.x, transform.position.y - 1.2f);
                sentProjectile.GetComponent<Projectiles>().lifeTime = attack.lifeTime;
                if (GetComponent<PlayerControllerBetter>().facingRight)
                {
                    sentProjectile.GetComponent<Projectiles>().speed = attack.speed;
                } else
                {
                    sentProjectile.GetComponent<Projectiles>().speed = -attack.speed;
                    sentProjectile.GetComponent<SpriteRenderer>().flipX = true;
                }
            }
        }
    }
}
