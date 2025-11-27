using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private bool hasBeenTriggered = false;

    // Use OnCollisionEnter2D for 2D physics. It requires the argument type to be Collision2D.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 1. Check if the colliding object has the tag "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // 2. Ensure the falling effect hasn't been applied yet
            if (!hasBeenTriggered)
            {
                // We could add a slight delay here (e.g., Invoke("MakeObjectFall", 0.5f)) 
                // if we want the player to stand on it for a moment before it falls.
                MakeObjectFall();
                hasBeenTriggered = true; // Prevents the code from running on subsequent touches
            }
        }
    }

    /// <summary>
    /// Adds a Rigidbody2D component and initiates the falling motion.
    /// </summary>
    private void MakeObjectFall()
    {
        // Get the Rigidbody2D component, or add one if it doesn't exist
        // 🚨 IMPORTANT: You must use Rigidbody2D for 2D physics.
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();

        if (rb2d == null)
        {
            // Add the Rigidbody2D component
            rb2d = gameObject.AddComponent<Rigidbody2D>();
            Debug.Log($"Rigidbody2D added to {gameObject.name}. Initiating fall.");
        }

        // Ensure gravity is enabled (default scale is 1, which mimics normal gravity)
        rb2d.gravityScale = 1f;

        // Optionally, reset velocity to ensure a clean fall start
        // 🚨 IMPORTANT: Use Vector2 for 2D velocity.
        rb2d.velocity = Vector2.zero;
        rb2d.angularVelocity = 0f;
    }
}