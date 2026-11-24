using UnityEngine;
using UnityEngine.SceneManagement;

public class playerDeath : MonoBehaviour
{
    public Health playerHealth;
    public GameObject deathScreen;
    public GameObject tombstonePrefab;

    void Start()
    {
        if (deathScreen != null)
        {
            deathScreen.SetActive(false);
        }

        if (playerHealth != null)
        {
            playerHealth.OnDeath += ShowDeathScreen;
        }
    }

    void ShowDeathScreen()
    {
        Inventory inv = FindAnyObjectByType<Inventory>();

        if (inv != null && tombstonePrefab != null)
        {
            //Create tombstone
            Vector3 spawnPos = new Vector3(playerHealth.transform.position.x, playerHealth.transform.position.y, -1f);
            GameObject tomb = Instantiate(tombstonePrefab, spawnPos, Quaternion.identity);

            //Inventory goes into tomb
            Tombstone tombScript = tomb.GetComponent<Tombstone>();
            if (tombScript != null)
            {
                tombScript.Setup(inv.GetResourceNames(), inv.GetResourceCounts());
            }

            string[] names = inv.GetResourceNames();
            for (int i = 0; i < names.Length; i++)
            {
                inv.SetResourceAmount(names[i], 0);
            }
        }
        deathScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Respawn()
    {
        deathScreen.SetActive(false);
        Time.timeScale = 1f;

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.transform.position = Vector3.zero;
        }

        if (playerHealth != null)
        {
            playerHealth.Initialize(playerHealth.MaxHealth);
        }
    }

    public void GoToMainMenu()
    {
        deathScreen.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("mainMenu");
    }
}