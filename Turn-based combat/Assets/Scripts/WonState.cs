using UnityEngine;

public class WonState : BattleState
{
    public WonState(BattleController battleController, MonoBehaviour monoBehaviour) : base(battleController, monoBehaviour) { }

    public override void Enter()
    {
        controller.UpdateDialogue("You won the battle!");
        // Handle win condition, maybe return to overworld or show rewards
    }

    public override void Exit()
    {
        // Clean up if necessary
    }
}