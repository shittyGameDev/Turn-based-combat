using System.Collections;
using UnityEngine;
public class StartState : BattleState
{
    public StartState(BattleController battleController, MonoBehaviour mono) : base(battleController, mono) { }

    public override void Enter()
    {
        monoBehaviour.StartCoroutine(SetupBattle());
    }

    public override void Exit()
    {
        // Clean up if necessary
    }

    private IEnumerator SetupBattle()
    {
        controller.UpdateDialogue("A wild " + controller.GetEnemyUnit().unitName + " approaches...");

        // Update HUDs via events
        controller.UpdatePlayerHP();
        controller.UpdateEnemyHP();

        yield return new WaitForSeconds(2f);

        controller.ChangeState(new PlayerTurnState(controller, monoBehaviour));
    }
}