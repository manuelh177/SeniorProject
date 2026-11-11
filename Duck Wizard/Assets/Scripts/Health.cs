
using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100;

    private int currentHealth;

    public event Action<int, int> OnHealthChanged;
    public event Action OnDeath;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;
    public bool IsDead => currentHealth <= 0;

    void Awake()
    {
        maxHealth = Mathf.Max(1, maxHealth);
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0 || IsDead)
            return;

        currentHealth = Mathf.Max(
            currentHealth - damage, 0
        );

        Debug.Log(gameObject.name + " Health: " +
                  currentHealth);

        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        if (IsDead)
        {
            Debug.Log(gameObject.name + " has died!");
            OnDeath?.Invoke();
        }
    }

    public void Heal(int amount)
    {
        if (amount <= 0 || IsDead)
            return;

        currentHealth = Mathf.Min(
            currentHealth + amount, maxHealth
        );

        Debug.Log(gameObject.name + " Health: " +
                  currentHealth);

        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
    
    //for unit testing
    public void Initialize(int startingMaxHealth = 100)
    {
        maxHealth = Mathf.Max(1, startingMaxHealth);
        currentHealth = maxHealth;
    }
}
