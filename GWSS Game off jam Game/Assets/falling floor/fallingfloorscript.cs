using UnityEngine;

/// <summary>
/// Attaches a Rigidbody and enables gravity when this GameObject is
/// touched by a GameObject with the tag "Player".
/// Note: This script should be attached to the object you want to fall.
/// Both objects must have Collider components. The object this is attached
/// to must NOT have 'Is Trigger' checked on its Collider if using OnCollisionEnter.
/// </summary>
public class PlayerTriggerFall : MonoBehaviour
{
    private bool hasBeenTriggered = false;

    /// <summary>
    /// Called when another collider hits this one.
    /// </summary>
    /// <param name="collision">The Collision data, containing information about the other collider.</param>
    private void OnCollisionEnter(Collision collision)
    {
        // 1. Check if the colliding object has the tag "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // 2. Ensure the falling effect hasn't been applied yet
            if (!hasBeenTriggered)
            {
                MakeObjectFall();
                hasBeenTriggered = true; // Prevents the code from running on subsequent touches
            }
        }
    }

    /// <summary>
    /// Adds a Rigidbody component and initiates the falling motion.
    /// </summary>
    private void MakeObjectFall()
    {
        // Get the Rigidbody component, or add one if it doesn't exist
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            // Add the Rigidbody component
            rb = gameObject.AddComponent<Rigidbody>();
            Debug.Log($"Rigidbody added to {gameObject.name}. Initiating fall.");
        }

        // Ensure gravity is enabled
        rb.useGravity = true;

        // Optionally, reset velocity to ensure a clean fall start
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}