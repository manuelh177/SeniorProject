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

    public void goToBrewingMenu()
    {
        SceneManager.LoadScene("brewingScene");
    }

    public void goToForgingMenu()
    {
        SceneManager.LoadScene("craftingScene");
        //change
    }

}
