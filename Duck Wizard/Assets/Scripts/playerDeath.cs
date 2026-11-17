using UnityEngine;
using UnityEngine.SceneManagement;
public class playerDeath : MonoBehaviour
{

    public Health playerHealth;
    public GameObject DeathScreen;

    void Start()
    {
        if (playerHealth != null)
        {
            playerHealth.OnDeath += ShowDeathScreen;
        }
    }

    void ShowDeathScreen()
    {
        DeathScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Respawn()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("mainMenu");
    }
}
