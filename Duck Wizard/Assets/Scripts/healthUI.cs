using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Health playerHealth;
    public Image healthFill; 

    void Start()
    {
        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged += UpdateBar;
            UpdateBar(playerHealth.CurrentHealth, playerHealth.MaxHealth);
        }
    }

    void UpdateBar(int current, int max)
    {
        if (healthFill == null) return;

        healthFill.fillAmount = (float)current / max;
    }
}