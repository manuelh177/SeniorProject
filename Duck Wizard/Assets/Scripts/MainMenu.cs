using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Reset inventory and upgrades when starting a new game.
        PlayerData.GetOrCreate().ResetData();

        SceneManager.LoadScene("shopScene");
    }

    public void ExitGame()
    {
        Debug.Log("Quit option pressed");
        Application.Quit();
    }
}