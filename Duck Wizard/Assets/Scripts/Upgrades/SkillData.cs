using System;
using UnityEngine;

// Defines the three branches of the skill tree.
// Used to categorize skills and organize them in the upgrade UI.
public enum SkillBranch
{
    Forager,
    Crafter,
    Combat
}

// Represents a single resource requirement for an upgrade.
// For example, an upgrade might require 5 Wood.
// Serializable allows us to edit these values in the Unity Inspector.
[Serializable]
public class ResourceCost
{
    // Must match the resource name registered in Inventory.cs.
    public string resourceName;

    // Quantity of the resource required.
    public int amount;
}

// Stores the information for one individual upgrade level.
// Each level can have its own description and resource requirements.
[Serializable]
public class SkillLevelData
{
    // Describes what purchasing this level will unlock or improve.
    [TextArea]
    public string description;

    // All resources required to purchase this level.
    // Every requirement must be met before the upgrade is purchased.
    public ResourceCost[] costs;
}

// Defines a reusable skill as a ScriptableObject.
//
// Each skill has its own data asset, allowing us to add new skills
// or modify existing upgrade costs without changing the system's code.
//
// This class only stores skill definitions.
// Player progression, purchases, and gameplay effects are handled
// separately by other systems.
[CreateAssetMenu(
    fileName = "NewSkill",
    menuName = "Upgrades/Skill"
)]
public class SkillData : ScriptableObject
{
    // Unique identifier used to reference the skill in other scripts.
    // This should remain consistent for saving and loading progression.
    public string skillID;

    // Display name shown to the player in the upgrade menu.
    public string skillName;

    // Determines which skill tree branch this skill belongs to.
    public SkillBranch branch;

    // General description of the skill and its purpose.
    [TextArea]
    public string description;

    // Contains the data for every purchasable upgrade level.
    // Index 0 represents Level 1.
    // Index 1 represents Level 2, and so on.
    // Level 0 represents the default, unupgraded state.
    public SkillLevelData[] levels;

    // Automatically determines the maximum level of the skill
    // based on the number of configured upgrade levels.
    public int MaxLevel => levels.Length;
}