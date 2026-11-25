using TMPro;
using UnityEngine;

// Displays and manages the player's inventory.
// Resource quantities are stored in PlayerData so they
// persist when transitioning between scenes.
public class Inventory : MonoBehaviour
{
    private TextMeshProUGUI inventoryText;

    [SerializeField] private string[] resources;

    private PlayerData playerData;

    void Awake()
    {
        inventoryText = GetComponent<TextMeshProUGUI>();

        // Access the shared persistent player data.
        playerData = PlayerData.GetOrCreate();
    }

    void Start()
    {
        RefreshInventory();
    }

    // Updates the inventory display using persistent data.
    public void RefreshInventory()
    {
        inventoryText.text = "";

        for (int i = 0; i < resources.Length; i++)
        {
            int amount = playerData.GetResourceAmount(resources[i]);

            inventoryText.text += resources[i] + " - "
                + amount.ToString() + "\n";
        }
    }

    // Adds or removes resources from the player's inventory.
    public void UpdateInventory(string resource, int amount)
    {
        for (int i = 0; i < resources.Length; i++)
        {
            if (resources[i] == resource)
            {
                playerData.UpdateResource(resource, amount);
                break;
            }
        }

        RefreshInventory();
    }

    // Returns the quantity of a specified resource.
    public int GetResourceAmount(string resource)
    {
        return playerData.GetResourceAmount(resource);
    }

    // Returns the resource names configured in the Inspector.
    public string[] GetResourceNames()
    {
        return resources;
    }

    // Returns a snapshot of the current resource quantities.
    // Used by systems such as tombstone resource recovery.
    public int[] GetResourceCounts()
    {
        int[] resourceCounts = new int[resources.Length];

        for (int i = 0; i < resources.Length; i++)
        {
            resourceCounts[i] =
                playerData.GetResourceAmount(resources[i]);
        }

        return resourceCounts;
    }

    // Sets a resource to a specified quantity.
    public void SetResourceAmount(string resource, int amount)
    {
        for (int i = 0; i < resources.Length; i++)
        {
            if (resources[i] == resource)
            {
                playerData.SetResourceAmount(resource, amount);

                RefreshInventory();
                return;
            }
        }
    }
}