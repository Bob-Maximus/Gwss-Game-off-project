using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;         // Max health
    private int currentHealth;          // Current health

    public Image healthBarFill;         

    void Start()
    {
        currentHealth = maxHealth;

        // Initialize health bar
        if (healthBarFill != null)
            healthBarFill.fillAmount = 1f;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0)
            currentHealth = 0;

        // Update health bar
        if (healthBarFill != null)
            healthBarFill.fillAmount = (float)currentHealth / maxHealth;

        Debug.Log("Player hit! Health: " + currentHealth);

        if (currentHealth == 0)
            Die();
    }

    private void Die()
    {
        Debug.Log("Player died");
        Destroy(gameObject);  
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            TakeDamage(maxHealth);  // instantly kill player
        }
    }

}
