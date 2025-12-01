using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public List<ProjectileData> attacks;

    public AudioClip shootSFX;  
    public AudioSource audioSource;

    public float attackCooldown = 1f; // 1 second cooldown
    private float lastAttackTime = 0f; // timestamp of last attack

    void Update()
    {
        // Check if cooldown has passed
        if (Time.time - lastAttackTime < attackCooldown)
            return;

        foreach (ProjectileData attack in attacks)
        {
            if (Input.GetKeyDown(attack.key) && GetComponent<PlayerControllerBetter>().isGrounded)
            {
                var sentProjectile = Instantiate(attack.projectile);
                sentProjectile.transform.position = new Vector2(transform.position.x, transform.position.y - 1.2f);
                sentProjectile.GetComponent<Projectiles>().lifeTime = attack.lifeTime;

                if (GetComponent<PlayerControllerBetter>().facingRight)
                {
                    sentProjectile.GetComponent<Projectiles>().speed = attack.speed;
                } 
                else
                {
                    sentProjectile.GetComponent<Projectiles>().speed = -attack.speed;
                    sentProjectile.GetComponent<SpriteRenderer>().flipX = true;
                }

                GetComponent<PlayerControllerBetter>().anim.Play("casting");

                if (audioSource != null && shootSFX != null)
                    audioSource.PlayOneShot(shootSFX);

                lastAttackTime = Time.time; // reset cooldown
                break; // only one attack per key press
            }
        }
    }
}
