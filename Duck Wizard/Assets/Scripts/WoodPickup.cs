
using UnityEngine;

public class WoodPickup : MonoBehaviour
{
    private Inventory inventory;
    private bool collected = false;

    void Awake()
    {
        inventory = FindFirstObjectByType<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (collected || inventory == null)
            return;

        collected = true;

        inventory.UpdateInventory("Wood", 1);

        Debug.Log("Player collected 1 Wood!");

        Destroy(gameObject);
    }
}
