using System.Collections;
using UnityEngine;
public class PlayerTurnState : BattleState
{
    public PlayerTurnState(BattleController battleController, MonoBehaviour mono) : base(battleController, mono) { }

    public override void Enter()
    {
        controller.UpdateDialogue("Choose an action:");
    }

    public override void Exit() { }

    public void OnAttack()
    {
        monoBehaviour.StartCoroutine(PlayerAttack());
    }

    public void OnHeal()
    {
        monoBehaviour.StartCoroutine(PlayerHeal());
    }

    public void OnSpellButton()
    {
        controller.RequestSpellSelection();
    }
    
    public void OnItem()
    {
        throw new System.NotImplementedException();
    }

    private IEnumerator PlayerAttack()
    {
        bool isDead = controller.GetEnemyUnit().TakeDamage(controller.GetPlayerUnit().damage);
        controller.UpdateEnemyHP();

        controller.UpdateDialogue("The attack is successful!");

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            controller.ChangeState(new WonState(controller, monoBehaviour));
        }
        else
        {
            controller.ChangeState(new EnemyTurnState(controller, monoBehaviour));
        }
    }

    private IEnumerator PlayerHeal()
    {
        controller.GetPlayerUnit().Heal(5);
        controller.UpdatePlayerHP();

        controller.UpdateDialogue("You feel renewed strength!");

        yield return new WaitForSeconds(2f);

        controller.ChangeState(new EnemyTurnState(controller, monoBehaviour));
    }



    public void OnSpellSelected(int spellIndex)
    {
        monoBehaviour.StartCoroutine(PlayerCastSpell(spellIndex));
    }
    
    private IEnumerator PlayerCastSpell(int spellIndex)
    {
        Spell selectedSpell = controller.playerSpells[spellIndex];
        controller.UpdateDialogue($"You cast {selectedSpell.spellName}!");
        
        //Apply spell effects
        bool isDead = selectedSpell.Cast(controller.GetPlayerUnit(), controller.GetEnemyUnit());
        controller.UpdateEnemyHP();

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            controller.ChangeState(new WonState(controller, monoBehaviour));
        }
        else
        {
            controller.ChangeState(new EnemyTurnState(controller, monoBehaviour));
        }
    }
}