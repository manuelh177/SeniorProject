
using UnityEngine;

public class HarvestableResource : MonoBehaviour
{
    [Header("Resource Settings")]
    [SerializeField] private int maxHealth = 3;

    [Header("Drop Settings")]
    [SerializeField] private GameObject woodDropPrefab;
    [SerializeField] private Vector2 dropOffset = new Vector2(0.5f, 0f);

    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0 || currentHealth <= 0)
            return;

        currentHealth -= damage;

        Debug.Log(gameObject.name + " took " + damage +
                  " damage. Remaining HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Harvest();
        }
    }

    private void Harvest()
    {
        Debug.Log(gameObject.name + " was harvested!");

        // Spawn wood next to the tree
        if (woodDropPrefab != null)
        {
            Vector3 spawnPosition =
                transform.position + (Vector3)dropOffset;

            Instantiate(
                woodDropPrefab,
                spawnPosition,
                Quaternion.identity
            );
        }

        Destroy(gameObject);
    }
}
