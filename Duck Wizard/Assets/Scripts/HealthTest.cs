
using UnityEngine;

[RequireComponent(typeof(Health))]
public class HealthTester : MonoBehaviour
{
    private Health health;

    void Awake()
    {
        health = GetComponent<Health>();
    }

    void Update()
    {
        // Press T to take 20 damage
        if (Input.GetKeyDown(KeyCode.T))
        {
            health.TakeDamage(20);
        }

        // Press H to heal 10 health
        if (Input.GetKeyDown(KeyCode.H))
        {
            health.Heal(10);
        }
    }
}
