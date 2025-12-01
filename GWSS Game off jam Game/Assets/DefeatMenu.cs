using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatMenu : MonoBehaviour
{
    // Call this when the Replay button is clicked
    public void Replay()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Optional: go back to main menu
    public void QuitToMenu()
    {
        SceneManager.LoadScene("MenuScene"); // replace with your menu scene name
    }
}
