using UnityEngine;
using UnityEngine.SceneManagement;

public class craftingMenu : MonoBehaviour
{
    public void goToShop()
    {
        SceneManager.LoadScene("shopScene");
    }
}
