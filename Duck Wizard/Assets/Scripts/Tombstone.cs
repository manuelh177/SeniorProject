using UnityEngine;
using System.Collections.Generic;

public class Tombstone : MonoBehaviour
{
    private Dictionary<string, int> storedItems = new Dictionary<string, int>();
    private bool playerInRange = false;
    private Inventory playerInventory;

    void Start()
    {
        playerInventory = FindAnyObjectByType<Inventory>();
    }
    public void Setup(string[] resourceNames, int[] resourceCounts)
    {
        storedItems.Clear();
        for (int i = 0; i < resourceNames.Length; i++)
        {
            if (resourceCounts[i] > 0)
            {
                storedItems.Add(resourceNames[i], resourceCounts[i]);
            }
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E)) CollectItems();
    }

    void CollectItems()
    {
        if (playerInventory != null)
        {
            foreach (KeyValuePair<string, int> item in storedItems)
            {
                //Give the items back
                playerInventory.UpdateInventory(item.Key, item.Value);
                Debug.Log("Restored " + item.Value + " " + item.Key);
            }
        }
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other) { if (other.CompareTag("Player")) playerInRange = true; }
    void OnTriggerExit2D(Collider2D other) { if (other.CompareTag("Player")) playerInRange = false; }
}