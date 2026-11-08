using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("shopScene");
    }
    public void ExitGame ()
    {
        Debug.Log("Quit option pressed");
        Application.Quit();
    }
}
