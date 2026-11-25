using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Connects a skill data asset to its corresponding UI button.
// Allows new skills to be registered through the Inspector
// without modifying the UI system's code.
[Serializable]
public class SkillButtonBinding
{
    public SkillData skill;
    public Button button;
    public TextMeshProUGUI buttonText;
}

// Manages the visual interface of the upgrade menu.
// All purchasing logic is handled separately by UpgradeManager.
public class UpgradeUI : MonoBehaviour
{
    // Reference to the system responsible for upgrade purchases.
    [SerializeField] private UpgradeManager upgradeManager;

    // Connects each skill to its corresponding button.
    [SerializeField] private SkillButtonBinding[] skillButtons;

    // References to the shared skill information panel.
    [SerializeField] private TextMeshProUGUI skillNameText;
    [SerializeField] private TextMeshProUGUI skillDescriptionText;
    [SerializeField] private TextMeshProUGUI skillCostText;
    [SerializeField] private Button purchaseButton;

    // The skill currently selected by the player.
    private SkillData selectedSkill;

    // Initializes the menu and connects button click events.
    private void Start()
    {
        foreach (SkillButtonBinding binding in skillButtons)
        {
            SkillData skill = binding.skill;

            // Display the selected skill when its button is clicked.
            binding.button.onClick.AddListener(
                () => SelectSkill(skill)
            );
        }

        // Connect the shared purchase button.
        purchaseButton.onClick.AddListener(PurchaseSelectedSkill);

        RefreshSkillButtons();

        // Automatically select the first registered skill.
        if (skillButtons.Length > 0)
        {
            SelectSkill(skillButtons[0].skill);
        }
        else
        {
            purchaseButton.interactable = false;
        }
    }

    // Changes the currently selected skill and updates its information.
    public void SelectSkill(SkillData skill)
    {
        selectedSkill = skill;
        RefreshInfoPanel();
    }

    // Updates every skill button with its current and maximum level.
    private void RefreshSkillButtons()
    {
        foreach (SkillButtonBinding binding in skillButtons)
        {
            int currentLevel =
                upgradeManager.GetSkillLevel(binding.skill);

            binding.buttonText.text =
                $"{binding.skill.skillName} " +
                $"({currentLevel}/{binding.skill.MaxLevel})";
        }
    }

    // Displays the selected skill's level, next upgrade description,
    // resource requirements, and purchase availability.
    private void RefreshInfoPanel()
    {
        if (selectedSkill == null)
        {
            purchaseButton.interactable = false;
            return;
        }

        int currentLevel =
            upgradeManager.GetSkillLevel(selectedSkill);

        skillNameText.text =
            $"{selectedSkill.skillName} - " +
            $"Level {currentLevel}/{selectedSkill.MaxLevel}";

        // Prevent additional purchases when the skill is maxed out.
        if (upgradeManager.IsMaxLevel(selectedSkill))
        {
            skillDescriptionText.text = "Maximum level reached.";
            skillCostText.text = "";
            purchaseButton.interactable = false;
            return;
        }

        // Retrieve the next available upgrade.
        SkillLevelData nextLevel =
            upgradeManager.GetNextLevel(selectedSkill);

        skillDescriptionText.text =
            "Next level: " + nextLevel.description;

        // Build a readable list of required resources.
        string costText = "Cost: ";

        for (int i = 0; i < nextLevel.costs.Length; i++)
        {
            ResourceCost cost = nextLevel.costs[i];

            costText += $"{cost.amount} {cost.resourceName}";

            // Separate multiple resource requirements.
            if (i < nextLevel.costs.Length - 1)
            {
                costText += ", ";
            }
        }

        skillCostText.text = costText;

        // Only enable purchases the player can afford.
        purchaseButton.interactable =
            upgradeManager.CanPurchaseUpgrade(selectedSkill);
    }

    // Attempts to purchase the currently selected skill.
    // The UpgradeManager validates and processes the transaction.
    private void PurchaseSelectedSkill()
    {
        if (selectedSkill == null)
        {
            return;
        }

        if (upgradeManager.PurchaseUpgrade(selectedSkill))
        {
            RefreshSkillButtons();
        }

        // Refresh the description, costs, and purchase availability.
        RefreshInfoPanel();
    }
}