using System.Collections.Generic;
using UnityEngine;

// Stores player data independently of individual scenes.
// Inventory and purchased upgrades remain available
// until the player starts a new game.
public class PlayerData : MonoBehaviour
{
    // The single shared instance of PlayerData.
    public static PlayerData Instance { get; private set; }

    // Stores resource quantities by resource name.
    private Dictionary<string, int> resources =
        new Dictionary<string, int>();

    // Stores purchased upgrade levels by skill ID.
    private Dictionary<string, int> skillLevels =
        new Dictionary<string, int>();

    private void Awake()
    {
        // Prevent multiple PlayerData instances.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // Preserve this object when changing scenes.
        DontDestroyOnLoad(gameObject);
    }

    // Creates the shared data object if one does not exist.
    public static PlayerData GetOrCreate()
    {
        if (Instance == null)
        {
            GameObject dataObject = new GameObject("PlayerData");
            dataObject.AddComponent<PlayerData>();
        }

        return Instance;
    }

    // Returns the quantity of a resource.
    public int GetResourceAmount(string resourceName)
    {
        if (resources.TryGetValue(resourceName, out int amount))
        {
            return amount;
        }

        return 0;
    }

    // Sets a resource quantity, preventing negative values.
    public void SetResourceAmount(string resourceName, int amount)
    {
        resources[resourceName] = Mathf.Max(0, amount);
    }

    // Adds or removes a specified quantity of a resource.
    public void UpdateResource(string resourceName, int amount)
    {
        int currentAmount = GetResourceAmount(resourceName);

        SetResourceAmount(resourceName, currentAmount + amount);
    }

    // Returns the current level of a skill.
    public int GetSkillLevel(string skillID)
    {
        if (skillLevels.TryGetValue(skillID, out int level))
        {
            return level;
        }

        return 0;
    }

    // Stores the current level of a skill.
    public void SetSkillLevel(string skillID, int level)
    {
        skillLevels[skillID] = Mathf.Max(0, level);
    }

    // Clears all progression when starting a new game.
    public void ResetData()
    {
        resources.Clear();
        skillLevels.Clear();
    }

    // Clears the instance reference if this object is destroyed.
    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}