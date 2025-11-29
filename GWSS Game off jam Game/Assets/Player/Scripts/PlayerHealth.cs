using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public void TakeDamage()
    {
        Debug.Log("Player died");

        // TEMP: just destroy player
        Destroy(gameObject);

        // Later you can add respawn logic
    }
}

