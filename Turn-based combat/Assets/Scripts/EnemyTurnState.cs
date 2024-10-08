using System.Collections;
using UnityEngine;
public class EnemyTurnState : BattleState
{
    public EnemyTurnState(BattleController battleController, MonoBehaviour mono) : base(battleController, mono) { }

    public override void Enter()
    {
        monoBehaviour.StartCoroutine(EnemyAttack());
    }

    public override void Exit() { }

    private IEnumerator EnemyAttack()
    {
        controller.UpdateDialogue(controller.GetEnemyUnit().unitName + " attacks!");

        yield return new WaitForSeconds(1f);

        bool isDead = controller.GetPlayerUnit().TakeDamage(controller.GetEnemyUnit().damage);
        controller.UpdatePlayerHP();

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            controller.ChangeState(new LostState(controller, monoBehaviour));
        }
        else
        {
            controller.ChangeState(new PlayerTurnState(controller, monoBehaviour));
        }
    }
}