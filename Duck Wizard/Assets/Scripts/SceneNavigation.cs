using UnityEngine;
using UnityEngine.SceneManagement;

// Provides reusable scene navigation functionality
// that can be called from UI buttons.
public class SceneNavigation : MonoBehaviour
{
    // Loads a scene using its specified name.
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}