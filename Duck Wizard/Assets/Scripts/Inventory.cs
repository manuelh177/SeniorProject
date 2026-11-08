using TMPro;
using UnityEngine;

// Attach this script to a UI (Canvas) - Text - TextMeshPro GameObject. This script keeps track of how many of each
// resource the player has in their inventory, and it updates the parent's text with each resource and the number of
// that resource in inventory. To update a resource count, call the public method "updateInventory" with the (precise)
// name of the resource and the amount you want to modify it by. You can add/remove resources from the Unity editor on
// the gameObject this script is attached to.

public class Inventory : MonoBehaviour
{
    private TextMeshProUGUI inventoryText;
    
    [SerializeField] private string[] resources;
    private int[] resourceCounts;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryText = GetComponent<TextMeshProUGUI>();
        inventoryText.text = "";
        resourceCounts = new int[resources.Length];
        for (int i = 0; i < resources.Length; i++)
        {
            resourceCounts[i] = 0;
            inventoryText.text += resources[i] + " - " + resourceCounts[i].ToString() + "\n";
        }
    }

    // Update is called once per frame
    // void Update()
    // {
    //     
    // }

    public void UpdateInventory(string resource, int amount)
    {
        inventoryText.text = "";
        for (int i = 0; i < resourceCounts.Length; i++)
        {
            if (resources[i] == resource)
            {
                resourceCounts[i] += amount;
                if (resourceCounts[i] < 0) resourceCounts[i] = 0; //keep it nonnegative
            }
            inventoryText.text += resources[i] + " - " + resourceCounts[i].ToString() + "\n";
        }
    }

}
