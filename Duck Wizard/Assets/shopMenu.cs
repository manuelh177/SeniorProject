using UnityEngine;
using UnityEngine.SceneManagement;

public class shopMenu : MonoBehaviour
{
    public void goToWorld()
    {
        SceneManager.LoadScene("World");
    }

    public void goToCraftingMenu()
    {
        SceneManager.LoadScene("craftingScene");
    }
}
