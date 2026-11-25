using System;
using System.Collections.Generic;
using UnityEngine;

// Manages skill purchases, resource requirements,
// and permanent skill progression during a game session.
public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private Inventory inventory;

    private PlayerData playerData;

    // Notifies other systems when a skill is upgraded.
    public event Action<SkillData, int> OnSkillUpgraded;

    private void Awake()
    {
        // Access the shared persistent player data.
        playerData = PlayerData.GetOrCreate();
    }

    // Retrieves the player's current skill level.
    public int GetSkillLevel(SkillData skill)
    {
        return playerData.GetSkillLevel(skill.skillID);
    }

    // Checks whether a skill has reached its maximum level.
    public bool IsMaxLevel(SkillData skill)
    {
        return GetSkillLevel(skill) >= skill.MaxLevel;
    }

    // Retrieves the data for the next available upgrade.
    public SkillLevelData GetNextLevel(SkillData skill)
    {
        if (IsMaxLevel(skill))
        {
            return null;
        }

        return skill.levels[GetSkillLevel(skill)];
    }

    // Combines resource requirements to prevent duplicate
    // resource entries from bypassing affordability checks.
    private Dictionary<string, int> GetTotalCosts(
        SkillLevelData levelData)
    {
        Dictionary<string, int> totalCosts =
            new Dictionary<string, int>();

        foreach (ResourceCost cost in levelData.costs)
        {
            if (!totalCosts.ContainsKey(cost.resourceName))
            {
                totalCosts[cost.resourceName] = 0;
            }

            totalCosts[cost.resourceName] += cost.amount;
        }

        return totalCosts;
    }

    // Checks whether the player can afford the next upgrade.
    public bool CanPurchaseUpgrade(SkillData skill)
    {
        if (inventory == null || skill == null)
        {
            return false;
        }

        SkillLevelData nextLevel = GetNextLevel(skill);

        if (nextLevel == null)
        {
            return false;
        }

        foreach (var cost in GetTotalCosts(nextLevel))
        {
            if (playerData.GetResourceAmount(cost.Key) < cost.Value)
            {
                return false;
            }
        }

        return true;
    }

    // Purchases an upgrade and saves its new level.
    public bool PurchaseUpgrade(SkillData skill)
    {
        if (!CanPurchaseUpgrade(skill))
        {
            return false;
        }

        SkillLevelData nextLevel = GetNextLevel(skill);

        // Deduct all required resources from shared player data.
        foreach (var cost in GetTotalCosts(nextLevel))
        {
            playerData.UpdateResource(cost.Key, -cost.Value);
        }

        // Refresh the visible inventory.
        inventory.RefreshInventory();

        // Store the new skill level in persistent player data.
        int newLevel = GetSkillLevel(skill) + 1;

        playerData.SetSkillLevel(skill.skillID, newLevel);

        // Notify other systems about the successful upgrade.
        OnSkillUpgraded?.Invoke(skill, newLevel);

        return true;
    }
}