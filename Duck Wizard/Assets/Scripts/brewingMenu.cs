using UnityEngine;
using UnityEngine.SceneManagement;

public class brewingMenu : MonoBehaviour
{
    public void goToShop()
    {
        SceneManager.LoadScene("shopScene");
    }
}
