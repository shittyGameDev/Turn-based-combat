using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleView : MonoBehaviour
{
    public Text dialogueText;
    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;
    public GameObject combatButtons;

    private BattleController battleController;

    public GameObject spellSelectionUI;
    public Button[] spellButtons;

    
    private void Start()
    {
        spellSelectionUI.SetActive(false);
    }
    public void SetBattleController(BattleController controller)
    {
        battleController = controller;

        // Subscribe to events
        battleController.OnDialogueUpdate += UpdateDialogue;
        battleController.OnPlayerHPChange += UpdatePlayerHUD;
        battleController.OnEnemyHPChange += UpdateEnemyHUD;
        battleController.OnStateChange += OnStateChange;
        battleController.OnSpellSelectionRequested += ShowSpellSelection;
    }

    void OnDestroy()
    {
        if (battleController != null)
        {
            // Unsubscribe from events
            battleController.OnDialogueUpdate -= UpdateDialogue;
            battleController.OnPlayerHPChange -= UpdatePlayerHUD;
            battleController.OnEnemyHPChange -= UpdateEnemyHUD;
            battleController.OnStateChange -= OnStateChange;
            battleController.OnSpellSelectionRequested -= ShowSpellSelection;
        }
    }

    public void UpdateDialogue(string message)
    {
        dialogueText.text = message;
    }

    public void UpdatePlayerHUD(Unit unit)
    {
        playerHUD.SetHUD(unit);
    }

    public void UpdateEnemyHUD(Unit unit)
    {
        enemyHUD.SetHUD(unit);
    }
    
    public void ShowSpellSelection(List<Spell> availableSpells)
    {
        if (spellSelectionUI == null)
        {
            Debug.LogError("spellSelectionUI is null");
            return;
        }

        if (spellButtons == null)
        {
            Debug.LogError("spellButtons array is null");
            return;
        }

        if (availableSpells == null)
        {
            Debug.LogError("availableSpells is null in ShowSpellSelection");
            return;
        }
        
        

        spellSelectionUI.SetActive(true);

        for (int i = 0; i < spellButtons.Length; i++)
        {
            if (spellButtons[i] == null)
            {
                Debug.LogError($"spellButtons[{i}] is null");
                continue;
            }

            if (i < availableSpells.Count)
            {
                spellButtons[i].gameObject.SetActive(true);
                spellButtons[i].GetComponentInChildren<Text>().text = availableSpells[i].spellName;
                int index = i;
                spellButtons[i].onClick.RemoveAllListeners();
                spellButtons[i].onClick.AddListener(() => OnSpellSelected(index));
            }
            else
            {
                spellButtons[i].gameObject.SetActive(false);
            }
        }
    }

    
    public void HideSpellSelection()
    {
        spellSelectionUI.SetActive(false);
    }
    
    private void OnSpellSelected(int index)
    {
        HideSpellSelection();
        battleController.OnSpellSelected(index);
    }

    private void OnStateChange(BattleState state)
    {
        // Optionally handle state changes in the view if needed
    }

    // UI Button Methods
    public void OnAttackButton()
    {
        battleController.OnAttackButton();
    }

    public void OnHealButton()
    {
        battleController.OnHealButton();
    }

    public void OnSpellButton()
    {
        ShowSpellSelection(battleController.playerSpells);
    }
    
    public void OnItemButton()
    {
        battleController.OnItemButton();
    }
}