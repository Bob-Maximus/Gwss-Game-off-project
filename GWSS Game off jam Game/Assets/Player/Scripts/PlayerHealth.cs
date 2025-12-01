using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;         // Max health
    private int currentHealth;          // Current health

    public Image healthBarFill;         // Drag your player's HealthBarFill image here

    void Start()
    {
        currentHealth = maxHealth;

        // Initialize health bar
        if (healthBarFill != null)
            healthBarFill.fillAmount = 1f;
    }

    // Call this to damage the player
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
}
