using System;
using System.Collections.Generic;
using UnityEngine;


public class BattleController
{
    public event Action<string> OnDialogueUpdate;
    public event Action<Unit> OnPlayerHPChange;
    public event Action<Unit> OnEnemyHPChange;
    public event Action<BattleState> OnStateChange;
    public event Action<List<Spell>> OnSpellSelectionRequested;

    private BattleState currentState;
    private Unit playerUnit;
    private Unit enemyUnit;
    private MonoBehaviour monoBehaviour;
    
    public List<Spell> playerSpells;

    public BattleController(Unit player, Unit enemy, MonoBehaviour mono)
    {
        playerUnit = player;
        playerSpells = player.GetAvailableSpells();
        if (playerSpells == null)
        {
            Debug.LogError("playerSpells is null in BattleController");
        }
        enemyUnit = enemy;
        monoBehaviour = mono;
        
    }
    
    public void RequestSpellSelection()
    {
        if (playerSpells == null)
        {
            Debug.LogError("playerSpells is null when requesting spell selection");
            return;
        }
        OnSpellSelectionRequested?.Invoke(playerSpells);
    }



    public void StartBattle()
    {
        ChangeState(new StartState(this, monoBehaviour));
    }

    public void ChangeState(BattleState newState)
    {
        currentState = newState;
        OnStateChange?.Invoke(currentState);
        currentState.Enter();
    }

    public void UpdateDialogue(string message)
    {
        OnDialogueUpdate?.Invoke(message);
    }

    public void UpdatePlayerHP()
    {
        OnPlayerHPChange?.Invoke(playerUnit);
    }

    public void UpdateEnemyHP()
    {
        OnEnemyHPChange?.Invoke(enemyUnit);
    }

    // Accessors
    public Unit GetPlayerUnit() => playerUnit;
    public Unit GetEnemyUnit() => enemyUnit;

    // Methods to handle player actions
    public void OnAttackButton()
    {
        if (currentState is PlayerTurnState playerTurnState)
        {
            playerTurnState.OnAttack();
        }
    }

    public void OnHealButton()
    {
        if (currentState is PlayerTurnState playerTurnState)
        {
            playerTurnState.OnHeal();
        }
    }

    public void OnSpellButton()
    {
        if(currentState is PlayerTurnState playerTurnState)
        {
            playerTurnState.OnSpellButton();
        }
    }
    
    public void OnItemButton()
    {
        if(currentState is PlayerTurnState playerTurnState)
        {
            playerTurnState.OnItem();
        }
    }

    public void OnSpellSelected(int spellIndex)
    {
        if (currentState is PlayerTurnState playerTurnState)
        {
            playerTurnState.OnSpellSelected(spellIndex);
        }
    }
}